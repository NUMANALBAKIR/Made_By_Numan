using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Models.ViewModels;
using Client.Services;
using Client.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Areas.Guest.Controllers;

//[Authorize]
[Area("Guest")]
public class CartController : Controller
{
	private readonly ICartItemService _cartItemService;

	public CartController(ICartItemService cartItemService)
	{
		_cartItemService = cartItemService;
	}


	// Display list of selected items in cart.
	[HttpGet]
	public async Task<IActionResult> Index()
	{
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


	//[HttpGet]
	//public async Task<IActionResult> Summary()
	//{
	//	return Task.CompletedTask;
	//}

}
