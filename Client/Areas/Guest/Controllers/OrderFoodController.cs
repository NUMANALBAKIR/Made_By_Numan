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


    [HttpGet]
    public async Task<IActionResult> CartItemDetails(int foodId)
    {

        FoodDTO foodDTO = new();
        APIResponse response = await _foodService.GetAsync<APIResponse>(foodId, "");
        if (response != null && response.IsSuccess == true)
        {
            var stringFood = Convert.ToString(response.Data);
            foodDTO = JsonConvert.DeserializeObject<FoodDTO>(stringFood);
        }

        CartItemDTO cartItemDTO = new()
        {
            FoodId = foodId,
            Food = foodDTO,
            CurrentPrice = foodDTO.Price,
            Count = 1,
        };
        return View(cartItemDTO);
    }


    //[Authorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CartItemDetails(CartItemDTO cartItemDTO)
    {
        FoodDTO foodDto = new();

        if (ModelState.IsValid)
        {
            // Get FoodDTO from Db using FoodID
            APIResponse foodResponse = await _foodService.GetAsync<APIResponse>(cartItemDTO.FoodId, "");
            if (foodResponse != null && foodResponse.IsSuccess == true)
            {
                var stringFood = Convert.ToString(foodResponse.Data);
                foodDto = JsonConvert.DeserializeObject<FoodDTO>(stringFood);
            }

            // Add cart-item to Db
            CartItemCreateDTO cartItemCreateDTO = new()
            {
                FoodId = cartItemDTO.FoodId,
                CurrentPrice = foodDto.Price,
                Count = cartItemDTO.Count
            };
            APIResponse createResponse = await _cartItemService.CreateAsync<APIResponse>(cartItemCreateDTO, "");
            if (createResponse != null && createResponse.IsSuccess == true)
            {
                TempData["success"] = "Item added to Cart.";
                return RedirectToAction(nameof(Index));
            }
        }

        // if modelstate not valid, populate cartItemDTO
        cartItemDTO.Food = foodDto;
        cartItemDTO.CurrentPrice = foodDto.Price;
        cartItemDTO.Count = cartItemDTO.Count;

        TempData["error"] = "Error encountered.";
        return View(cartItemDTO);
    }







    public IActionResult Cart()
    {
        return View();
    }
}
