﻿using Client.Models;
using Client.Models.OrderFoodDTOs;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Areas.Guest.Controllers;

[Area("Guest"), Authorize]
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
        // Get FoodDTO from Db using FoodID
        FoodDTO foodDTO = new();
        APIResponse response = await _foodService.GetAsync<APIResponse>(foodId, "");
        if (response != null && response.IsSuccess == true)
        {
            var stringFood = Convert.ToString(response.Data);
            foodDTO = JsonConvert.DeserializeObject<FoodDTO>(stringFood);
        }

        // populate cartItemDTO
        CartItemDTO cartItemDTO = new()
        {
            FoodId = foodId,
            Food = foodDTO,
            CurrentPrice = foodDTO.Price,
            Count = 1,
        };
        return View(cartItemDTO);
    }


    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> CartItemDetails(CartItemDTO cartItemDTO)
    {
        // Get user-identity
        //var claimsIdentity = (ClaimsIdentity)User.Identity;
        //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //shoppingCart.AppUserId = claim.Value;

        if (ModelState.IsValid)
        {
            // fetch this user's this item.
            CartItemDTO cartItemFromDb = new();
            APIResponse cartItemResponse = await _cartItemService.GetAsync<APIResponse>(cartItemDTO.FoodId, cartItemDTO.AppUserId, "");
            if (cartItemResponse != null && cartItemResponse.IsSuccess == true)
            {
                var stringCartItemFromDb = Convert.ToString(cartItemResponse.Data);
                cartItemFromDb = JsonConvert.DeserializeObject<CartItemDTO>(stringCartItemFromDb);
            }

            // if NOT found "this user's this item", add to db.
            if (cartItemFromDb.FoodId == 0)
            {
                CartItemCreateDTO cartItemCreateDTO = new()
                {
                    AppUserId = cartItemDTO.AppUserId,
                    FoodId = cartItemDTO.FoodId,
                    CurrentPrice = cartItemDTO.CurrentPrice,
                    Count = cartItemDTO.Count
                };
                // add to db and redirect.
                APIResponse createResponse = await _cartItemService.CreateAsync<APIResponse>(cartItemCreateDTO, "");
                if (createResponse != null && createResponse.IsSuccess == true)
                {
                    TempData["success"] = "Item added to Cart.";
                    return RedirectToAction(nameof(Index), "OrderFood", "menu");
                }
            }
            // if found, update by adding count.
            else
            {
                CartItemUpdateDTO cartItemUpdateDTO = new()
                {
                    CartItemId = cartItemFromDb.CartItemId,
                    AppUserId = cartItemDTO.AppUserId,
                    FoodId = cartItemDTO.FoodId,
                    CurrentPrice = cartItemDTO.CurrentPrice,
                    Count = cartItemFromDb.Count + cartItemDTO.Count
                };
                // update to db and redirect
                APIResponse updateResponse = await _cartItemService.UpdateAsync<APIResponse>(cartItemUpdateDTO, "");
                if (updateResponse != null && updateResponse.IsSuccess == true)
                {
                    TempData["success"] = "Updated item-count in cart.";
                    return RedirectToAction(nameof(Index), "OrderFood", "menu");
                }
            }
        }

        // if modelstate not valid, populate cartItemDTO for view
        // Get FoodDTO from Db using FoodID.
        FoodDTO foodDto = new();
        APIResponse foodResponse = await _foodService.GetAsync<APIResponse>(cartItemDTO.FoodId, "");
        if (foodResponse != null && foodResponse.IsSuccess == true)
        {
            var stringFood = Convert.ToString(foodResponse.Data);
            foodDto = JsonConvert.DeserializeObject<FoodDTO>(stringFood);
        }
        TempData["error"] = "Error encountered.";
        cartItemDTO.Count = 1;
        cartItemDTO.Food = foodDto;
        return View(cartItemDTO);
    }



    public async Task<IActionResult> EmailUs()
    {
        return View();
    }

}
