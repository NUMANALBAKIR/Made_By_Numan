using Client.Models;
using Client.Models.OrderFood;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;

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
            var stringList = Convert.ToString(response.Data);
            foodList = JsonConvert.DeserializeObject<List<FoodDTO>>(stringList);
        }
        return View(foodList);
    }


    // GET
    public async Task<IActionResult> CartItemDetails(int foodId)
    {

        FoodDTO foodDTO = new();
        APIResponse response = await _foodService.GetAsync<APIResponse>(foodId, "");
        if (response != null && response.IsSuccess == true)
        {
            var stingFood = Convert.ToString(response.Data);
            foodDTO = JsonConvert.DeserializeObject<FoodDTO>(stingFood);
        }
        CartItemCreateDTO cartItem = new()
        {
            FoodId = foodId,
            CurrentPrice = foodDTO.Price,
            Count = 1,
            Food = foodDTO
        };
        return View(cartItem);
    }


    //[Authorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CartItemDetails(CartItemCreateDTO CreateDto)
    {
        if (ModelState.IsValid)
        {
            Food food = new();
            APIResponse response = await _foodService.GetAsync<APIResponse>(CreateDto.FoodId, "");
            if (response != null && response.IsSuccess == true)
            {
                var stringFood = Convert.ToString(response.Data);
                food = JsonConvert.DeserializeObject<FoodDTO>(stringFood);
            }

            CreateDto.Food = food;
            response = await _cartItemService.CreateAsync<APIResponse>(CreateDto, "");
            if (response != null && response.IsSuccess == true)
            {
                TempData["success"] = "Item added to Cart.";
                return RedirectToAction(nameof(Index));
            }
        }
        TempData["error"] = "Error encountered.";
        return View(CreateDto);
    }







    public IActionResult Cart()
    {
        return View();
    }
}
