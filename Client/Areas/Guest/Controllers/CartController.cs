using AutoMapper;
using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Models.User;
using Client.Models.ViewModels;
using Client.Services;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.CodeAnalysis.Elfie.Extensions;
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

    public CartController(
        //IMapper mapper,
        ICartItemService cartItemService,
        IOrderHeaderService orderHeaderService,
        IOrderDetailService orderDetailService,
        IAppUserService appUserService,
        IEmailSender emailSender)
    {
        //_mapper = mapper;
        _cartItemService = cartItemService;
        _orderHeaderService = orderHeaderService;
        _orderDetailService = orderDetailService;
        _appUserService = appUserService;
        _emailSender = emailSender;
    }


    // Get user-identity
    private string GetNameIdentifier()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        return claim.Value;
    }


    // get this user's cart-items list
    private async Task<List<CartItemDTO>> CartItemsByServiceAsync()
    {
        string appUserId = GetNameIdentifier();

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
        APIResponse response = await _appUserService.GetAsync<APIResponse>(GetNameIdentifier(), "");
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


    // Order Summary with items of this user.
    public async Task<IActionResult> Summary()
    {
        cartVM = new()
        {
            CartItems = await CartItemsByServiceAsync(),
            OrderHeaderDTO = new()
        };
        // populate orderer's (header) information
        cartVM.OrderHeaderDTO.AppUser = await AppUserByServiceAsync();
        cartVM.OrderHeaderDTO.AppUserId = GetNameIdentifier();
        cartVM.OrderHeaderDTO.OrdererName = cartVM.OrderHeaderDTO.AppUser.Name;
        cartVM.OrderHeaderDTO.DeliveryAddress = cartVM.OrderHeaderDTO.AppUser.Address;
        cartVM.OrderHeaderDTO.EmailAddress = cartVM.OrderHeaderDTO.AppUser.Email;

        foreach (var item in cartVM.CartItems)
        {
            cartVM.OrderHeaderDTO.OrderTotal += (item.CurrentPrice * item.Count);
        }
        return View(cartVM);
    }


    // Order placed in summary page.
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
        headerCreateDto.AppUserId = GetNameIdentifier();
        headerCreateDto.TrackingNumber = Guid.NewGuid().ToString();
        headerCreateDto.OrderDate = DateTime.Now;
        headerCreateDto.OrdererName = cartVM.OrderHeaderDTO.OrdererName;
        headerCreateDto.DeliveryAddress = cartVM.OrderHeaderDTO.DeliveryAddress;
        headerCreateDto.EmailAddress = cartVM.OrderHeaderDTO.EmailAddress;

        // send OrderHeaderCreateDTO and get id of the created, to add to orderdetail.
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

        // clear cart
        await ClearCart(cartItems);

        sendEmailMessage(headerDto);

        //return RedirectToAction(nameof(OrderConfirmation));
        return View(nameof(OrderConfirmation), headerDto);
    }


    // OrderConfirmation page
    public IActionResult OrderConfirmation()
    {
        //var appUser = AppUserByServiceAsync();

        //var orderHeaderDTO = _orderHeaderService. 

        // for bank
        OrderHeaderDTO confirmationHeader = new()
        {
            AppUserId = OrderHeaderDTO.AppUserId,
            OrderTotal = OrderHeaderDTO.OrderTotal,
            TrackingNumber = OrderHeaderDTO.TrackingNumber,
            OrderDate = OrderHeaderDTO.OrderDate,
            OrdererName = OrderHeaderDTO.OrdererName,
            DeliveryAddress = OrderHeaderDTO.DeliveryAddress,
            EmailAddress = OrderHeaderDTO.EmailAddress
        };
        return View(confirmationHeader);
    }


    private void sendEmailMessage(OrderHeaderDTO confirmationHeader)
    {
        string emailBody = $"<h2>An Order of total <u>{confirmationHeader.OrderTotal.ToString("c")}</u> placed for your address <u>{confirmationHeader.DeliveryAddress}</u> at <u>{DateTime.Now.ToShortTimeString()}</u>.</h2>";
        _emailSender.SendEmailAsync(confirmationHeader.EmailAddress, "Food Order placed.", emailBody);
    }


}
