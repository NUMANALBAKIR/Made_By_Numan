using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Areas.Guest.Controllers;

[Area("Guest")]
public class OrderFoodController : Controller
{
    private readonly IFoodService _foodService;
    private readonly ICartItemService _cartItemService;

    public OrderFoodController(IFoodService foodService, ICartItemService cartItemService)
    {
        _foodService = foodService;
        _cartItemService = cartItemService;
    }

    public async Task<IActionResult> Index()
    {
        List<FoodDTO> foodList = new();

        APIResponse response = await _foodService.GetAllAsync<APIResponse>("");
        if (response != null && response.IsSuccess == true)
        {
            // baseService
            var stringList = Convert.ToString(response.Data);
            foodList = JsonConvert.DeserializeObject<List<FoodDTO>>(stringList);
        }
        return View(foodList);
    }


    // Get
    public async Task<IActionResult> CartItemDetails(int foodId)
    {
        CartItemDTO cartItem = new();

        APIResponse response = await _cartItemService.GetAsync<APIResponse>(foodId, "");
        if (response != null && response.IsSuccess == true)
        {
            // baseService???
            var stringCartItem = Convert.ToString(response.Data);
            cartItem = JsonConvert.DeserializeObject<CartItemDTO>(stringCartItem);
        }
        return View(cartItem);
    }




    public IActionResult Cart()
    {
        return View();
    }
}
