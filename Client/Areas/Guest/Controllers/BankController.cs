using Client.Models;
using Client.Models.User;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Areas.Guest.Controllers;

[Area("Guest"), Authorize]
public class BankController : Controller
{

    private readonly IEmailSender _emailSender;
    private readonly IAppUserService _appUserService;

    public BankController(IEmailSender emailSender, IAppUserService appUserService)
    {
        _emailSender = emailSender;
        _appUserService = appUserService;
    }


    // Get user-identity
    private string GetNameIdentifier()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        return claim.Value;
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


    [HttpGet]
    public IActionResult Index()
    {

        //    if (ModelState.IsValid)
        //    {
        //        // fetch this user's this item.
        //        BankAccountDTO bankAccountFromDb = new();
        //        APIResponse bankAccountResponse = await _bankAccountService.GetAsync<APIResponse>(bankAccountDTO.FoodId, bankAccountDTO.AppUserId, "");
        //        if (bankAccountResponse != null && bankAccountResponse.IsSuccess == true)
        //        {
        //            var stringBankAccountFromDb = Convert.ToString(bankAccountResponse.Data);
        //            bankAccountFromDb = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
        //        }

        //        // if NOT found "this user's bankaccount", add to db.
        //        if (bankAccountFromDb.FoodId == 0)
        //        {
        //            BankAccountCreateDTO bankAccountCreateDTO = new()
        //            {
        //                AppUserId = bankAccountDTO.AppUserId,
        //                FoodId = bankAccountDTO.FoodId,
        //                CurrentPrice = bankAccountDTO.CurrentPrice,
        //                Count = bankAccountDTO.Count
        //            };
        //            // add to db and redirect.
        //            APIResponse createResponse = await _bankAccountService.CreateAsync<APIResponse>(bankAccountCreateDTO, "");
        //            if (createResponse != null && createResponse.IsSuccess == true)
        //            {
        //                TempData["success"] = "Item added to Cart.";
        //                return RedirectToAction(nameof(Index), "OrderFood", "menu");
        //            }
        //        }
        //        // if found, update by adding count.
        //        else
        //        {
        //            BankAccountUpdateDTO bankAccountUpdateDTO = new()
        //            {
        //                BankAccountId = bankAccountFromDb.BankAccountId,
        //                AppUserId = bankAccountDTO.AppUserId,
        //                FoodId = bankAccountDTO.FoodId,
        //                CurrentPrice = bankAccountDTO.CurrentPrice,
        //                Count = bankAccountFromDb.Count + bankAccountDTO.Count
        //            };
        //            // update to db and redirect
        //            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
        //            if (updateResponse != null && updateResponse.IsSuccess == true)
        //            {
        //                TempData["success"] = "Updated item-count in cart.";
        //                return RedirectToAction(nameof(Index), "OrderFood", "menu");
        //            }
        //        }
        //    }

        return View(); //del when uncomment above
    }



}