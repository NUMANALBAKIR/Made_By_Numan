using AutoMapper;
using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Models.ViewModels;
using Client.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Areas.Guest.Controllers;

//[Authorize]
[Area("Guest")]
public class CartController : Controller
{
    [BindProperty]
    public CartVM cartVM { get; set; }
    private readonly IMapper _mapper;
    private readonly ICartItemService _cartItemService;
    private readonly IOrderHeaderService _orderHeaderService;
    private readonly IOrderDetailService _orderDetailService;


    public CartController(IMapper mapper, ICartItemService cartItemService, IOrderHeaderService orderHeaderService, IOrderDetailService orderDetailService)
    {
        _mapper = mapper;
        _cartItemService = cartItemService;
        _orderHeaderService = orderHeaderService;
        _orderDetailService = orderDetailService;
    }


    // Display list of selected items in cart.
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // populate cart items
        List<CartItemDTO> cartItems = new();

        APIResponse response = await _cartItemService.GetAllAsync<APIResponse>("");
        if (response != null && response.IsSuccess == true)
        {
            var stringList = Convert.ToString(response.Data);
            cartItems = JsonConvert.DeserializeObject<List<CartItemDTO>>(stringList);
        }

        CartVM cartVM = new()
        {
            CartItems = cartItems,
            OrderHeader = new()
        };

        return View(cartVM);
    }


    [HttpGet]
    public async Task<IActionResult> Summary()
    {
        // populate cart items
        List<CartItemDTO> cartItems = new();

        APIResponse response = await _cartItemService.GetAllAsync<APIResponse>("");
        if (response != null && response.IsSuccess == true)
        {
            var stringList = Convert.ToString(response.Data);
            cartItems = JsonConvert.DeserializeObject<List<CartItemDTO>>(stringList);
        }
        cartVM = new()
        {
            CartItems = cartItems,
            OrderHeader = new()
        };

        // populate orderer's (header) information
        cartVM.OrderHeader.OrdererName = "numan";
        cartVM.OrderHeader.DeliveryAddress = "215sher";
        cartVM.OrderHeader.EmailAddress = "n@gmail.com";

        foreach (var item in cartVM.CartItems)
        {
            cartVM.OrderHeader.OrderTotal += (item.CurrentPrice * item.Count);
        }

        return View(cartVM);
    }


    [HttpPost, ActionName("Summary"), ValidateAntiForgeryToken]
    public async Task<IActionResult> SummaryPOST()
    {
        // get cartitems list
        APIResponse response = await _cartItemService.GetAllAsync<APIResponse>("");
        if (response != null && response.IsSuccess == true)
        {
            var stringList = Convert.ToString(response.Data);
            cartVM.CartItems = JsonConvert.DeserializeObject<List<CartItemDTO>>(stringList);
        }

        // populate OrderHeaderCreateDTO to create it.
        foreach (var item in cartVM.CartItems)
        {
            cartVM.OrderHeader.OrderTotal += (item.CurrentPrice * item.Count);
        }
        cartVM.OrderHeader.TrackingNumber = Guid.NewGuid().ToString();
        cartVM.OrderHeader.OrderDate = DateTime.Now;
        OrderHeaderCreateDTO headerCreateDto = _mapper.Map<OrderHeaderCreateDTO>(cartVM.OrderHeader);

        // get OrderHeaderDTO to get its id
        OrderHeaderDTO headerDto = new();
        APIResponse createResponse = await _orderHeaderService.CreateAsync<APIResponse>(headerCreateDto, "");
        if (createResponse != null && createResponse.IsSuccess == true)
        {
            var stringHeader = Convert.ToString(createResponse.Data);
            headerDto = JsonConvert.DeserializeObject<OrderHeaderDTO>(stringHeader);
        }

        // populate each OrderDetailCreateDTO and send it.
        OrderDetailCreateDTO detailCreateDTO;
        foreach (var cartItem in cartVM.CartItems)
        {
            detailCreateDTO = new()
            {
                OrderHeaderId = headerDto.OrderHeaderId,
                FoodId = cartItem.FoodId,
                PurchasePrice = cartItem.CurrentPrice,
                Count = cartItem.Count
            };
            await _orderDetailService.CreateAsync<APIResponse>(detailCreateDTO, "");
        }
        //return RedirectToAction(nameof(OrderConfirmation), cartVM);
        return View(nameof(OrderConfirmation), cartVM);
    }


    [HttpGet]
    public async Task<IActionResult> OrderConfirmation()
    {
        return View();
    }



}
