using AutoMapper;
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
    private readonly IMapper _mapper;

    public OrderFoodController(IFoodService foodService, IMapper mapper)
    {
        _foodService = foodService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        List<FoodDTO> foodList = new();

        APIResponse response = await _foodService.GetAllAsync<APIResponse>("");
        if (response != null && response.IsSuccess == true)
        {
            // baseservice
            var stringList = Convert.ToString(response.Data);
            foodList = JsonConvert.DeserializeObject<List<FoodDTO>>(stringList);
        }
        return View(foodList);
    }

    // Get
    public IActionResult CartItemDetails()
    {
        //CartItem cartItem = new()
        //{
        //    Food = _foodService.GetAsync(foodId, ""),


        //};

        //return View(cartItem);
        return View();
    }




    public IActionResult Cart()
    {
        return View();
    }
}

//@model Client.Models.OrderFood.Food

//@{
//    Layout = "~/Views/Shared/_Layout_OrderFood.cshtml";
//    < style >
//        .footer {
//    position: absolute;
//    bottom: 0;
//    width: 100 %;
//        white - space: nowrap;
//    }
//    </ style >
//}
