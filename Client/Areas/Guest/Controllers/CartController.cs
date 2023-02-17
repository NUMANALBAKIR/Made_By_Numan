using AutoMapper;
using Client.Models;
using Client.Models.Bank;
using Client.Models.BankDTOs;
using Client.Models.OrderFoodDTOs;
using Client.Models.User;
using Client.Models.ViewModels;
using Client.Services;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Areas.Guest.Controllers;

[Area("Guest"), Authorize]
public class CartController : Controller
{
    [BindProperty]
    public CartVM cartVM { get; set; }
    [BindProperty]
    public OrderHeaderDTO OrderHeaderDTO { get; set; }

    private readonly ICartItemService _cartItemService;
    private readonly IOrderHeaderService _orderHeaderService;
    private readonly IOrderDetailService _orderDetailService;
    private readonly IAppUserService _appUserService;
    private readonly IEmailSender _emailSender;
    private readonly IBankAccountService _bankAccountService;
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mapper;

    public CartController(
        ICartItemService cartItemService,
        IOrderHeaderService orderHeaderService,
        IOrderDetailService orderDetailService,
        IAppUserService appUserService,
        IEmailSender emailSender,
        IBankAccountService bankAccountService,
        ITransactionService transactionService,
        IMapper mapper)
    {
        _cartItemService = cartItemService;
        _orderHeaderService = orderHeaderService;
        _orderDetailService = orderDetailService;
        _appUserService = appUserService;
        _emailSender = emailSender;
        _bankAccountService = bankAccountService;
        _transactionService = transactionService;
        _mapper = mapper;
    }


    // Get user-identity
    private string GetNameIdentifierClaim()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        return claim.Value;
    }


    // get this user's cart-items list
    private async Task<List<CartItemDTO>> CartItemsByServiceAsync()
    {
        string appUserId = GetNameIdentifierClaim();

        List<CartItemDTO> cartItems = new();
        APIResponse response = await _cartItemService.GetAllAsync<APIResponse>(appUserId, "");
        if (response != null && response.IsSuccess == true)
        {
            var stringList = Convert.ToString(response.Data);
            cartItems = JsonConvert.DeserializeObject<List<CartItemDTO>>(stringList);
        }
        return cartItems;
    }


    // Get AppUser from Db using NameIdentifier
    private async Task<AppUserDTO> AppUserByServiceAsync()
    {
        AppUserDTO appUser = new();
        APIResponse response = await _appUserService.GetAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (response != null && response.IsSuccess == true)
        {
            var stringAppUser = Convert.ToString(response.Data);
            appUser = JsonConvert.DeserializeObject<AppUserDTO>(stringAppUser);
        }
        return appUser;
    }


    // clear cart 
    private async Task ClearCart(List<CartItemDTO> cartItems)
    {
        foreach (var item in cartItems)
        {
            await _cartItemService.DeleteAsync<APIResponse>(item.CartItemId, "");
        }
    }


    public async Task<IActionResult> Index()
    {
        CartVM cartVM = new()
        {
            CartItems = await CartItemsByServiceAsync(),
            OrderHeaderDTO = new()
        };
        return View(cartVM);
    }


    // Order Summary with items of this user. ready to press place order button.
    public async Task<IActionResult> Summary()
    {
        // populate cart items list
        cartVM = new()
        {
            CartItems = await CartItemsByServiceAsync(),
            OrderHeaderDTO = new()
        };

        // prevent browser's back-button after placing order.
        if (cartVM.CartItems.Count == 0)
        {
            return RedirectToAction("Index", "OrderFood", "menu");
        }

        // populate orderer's (header) information
        cartVM.OrderHeaderDTO.AppUser = await AppUserByServiceAsync();
        cartVM.OrderHeaderDTO.AppUserId = GetNameIdentifierClaim();
        cartVM.OrderHeaderDTO.OrdererName = cartVM.OrderHeaderDTO.AppUser.Name;
        cartVM.OrderHeaderDTO.DeliveryAddress = cartVM.OrderHeaderDTO.AppUser.Address;
        cartVM.OrderHeaderDTO.EmailAddress = cartVM.OrderHeaderDTO.AppUser.Email;

        foreach (var item in cartVM.CartItems)
        {
            cartVM.OrderHeaderDTO.OrderTotal += (item.CurrentPrice * item.Count);
        }

        // Balance calculations begin.
        BankAccountDTO bankAccountDTO = await BankAccountByService();

        // if insufficient balance
        if (bankAccountDTO.CheckingsBalance >= cartVM.OrderHeaderDTO.OrderTotal)
        {
            TempData["successBalance"] = bankAccountDTO.CheckingsBalance;
        }
        // if sufficient balance
        else
        {
            TempData["errorBalance"] = bankAccountDTO.CheckingsBalance;
        }

        return View(cartVM);
    }


    // Place Order button pressed in summary page.
    [HttpPost, ActionName("Summary"), ValidateAntiForgeryToken]
    public async Task<IActionResult> SummaryPOST()
    {
        // populate OrderHeaderCreateDTO to create it.
        OrderHeaderCreateDTO headerCreateDto = new();
        // get order total
        var cartItems = await CartItemsByServiceAsync();
        foreach (var item in cartItems)
        {
            headerCreateDto.OrderTotal += (item.CurrentPrice * item.Count);
        }
        headerCreateDto.AppUserId = GetNameIdentifierClaim();
        headerCreateDto.TrackingNumber = Guid.NewGuid().ToString();
        headerCreateDto.OrderDate = DateTime.Now;
        headerCreateDto.OrdererName = cartVM.OrderHeaderDTO.OrdererName;
        headerCreateDto.DeliveryAddress = cartVM.OrderHeaderDTO.DeliveryAddress;
        headerCreateDto.EmailAddress = cartVM.OrderHeaderDTO.EmailAddress;

        // send OrderHeaderCreateDTO and get response orderHeader. need its id.
        OrderHeaderDTO headerDto = new();
        APIResponse createResponse = await _orderHeaderService.CreateAsync<APIResponse>(headerCreateDto, "");
        if (createResponse != null && createResponse.IsSuccess == true)
        {
            var stringHeader = Convert.ToString(createResponse.Data);
            headerDto = JsonConvert.DeserializeObject<OrderHeaderDTO>(stringHeader);
        }

        // populate each OrderDetailCreateDTO and send it. 
        foreach (var item in cartItems)
        {
            OrderDetailCreateDTO detailCreateDTO = new()
            {
                OrderHeaderId = headerDto.OrderHeaderId,
                FoodId = item.FoodId,
                PurchasePrice = item.CurrentPrice,
                Count = item.Count
            };
            await _orderDetailService.CreateAsync<APIResponse>(detailCreateDTO, "");
        }

        // get bank account info, deduct amount, update account.
        BankAccountDTO bankAccount = await BankAccountByService();
        bankAccount.CheckingsBalance -= headerDto.OrderTotal;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccount);
        await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

        // add this transaction to db
        bankAccount.TransactionAmount = headerDto.OrderTotal;
        TransactionCreateDTO createDto = new()
        {
            AppUserId = GetNameIdentifierClaim(),
            PreviousCheckingsBalance = bankAccount.CheckingsBalance + bankAccount.TransactionAmount,
            PreviousSavingsBalance = bankAccount.SavingsBalance,
            TransactionDate = DateTime.Now,
            Message = $"Food Order of ${bankAccount.TransactionAmount} placed using Checkings Balance."
        };
        await AddTransactionByService(createDto);

        await ClearCart(cartItems);

        SendEmailMessage(headerDto);

        //return View(nameof(OrderConfirmation), headerDto);
        return RedirectToAction(nameof(OrderConfirmation), new { orderHeaderid = headerDto.OrderHeaderId });
    }


    // Order Confirmation page
    public async Task<IActionResult> OrderConfirmation(int orderHeaderid)
    {
        // get order info 
        OrderHeaderDTO headerDto = new();
        APIResponse response = await _orderHeaderService.GetAsync<APIResponse>(orderHeaderid, "");
        if (response != null && response.IsSuccess == true)
        {
            var stringHeader = Convert.ToString(response.Data);
            headerDto = JsonConvert.DeserializeObject<OrderHeaderDTO>(stringHeader);
        }
        // add tempdata for bank
        TempData["success"] = $"Food purchase of ${headerDto.OrderTotal} done.";

        return View(headerDto);
    }


    // get this user's bank account details
    public async Task<BankAccountDTO> BankAccountByService()
    {
        BankAccountDTO bankAccountFromDb = new();
        APIResponse bankAccountResponse = await _bankAccountService.GetAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (bankAccountResponse != null && bankAccountResponse.IsSuccess == true)
        {
            var stringBankAccountFromDb = Convert.ToString(bankAccountResponse.Data);
            bankAccountFromDb = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
        }
        return bankAccountFromDb;
    }


    private void SendEmailMessage(OrderHeaderDTO confirmationHeader)
    {
        string emailBody = $"<h2>A Food Order of total <u>{confirmationHeader.OrderTotal.ToString("c")}</u> has been placed for your address <u>{confirmationHeader.DeliveryAddress}</u> at <u>{DateTime.Now.ToShortTimeString()}</u>.</h2>";
        _emailSender.SendEmailAsync(confirmationHeader.EmailAddress, "Food Order placed.", emailBody);
    }


    // add this transaction to db.
    public async Task AddTransactionByService(TransactionCreateDTO createDto)
    {
        await _transactionService.CreateAsync<APIResponse>(createDto, "");
    }


}
