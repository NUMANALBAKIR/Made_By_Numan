using Client.Models;
using Client.Models.BankDTOs;
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
    private readonly IAppUserService _appUserService;
    private readonly IBankAccountService _bankAccountService;

    public BankController(IAppUserService appUserService, IBankAccountService bankAccountService)
    {
        _appUserService = appUserService;
        _bankAccountService = bankAccountService;
    }


    // Get user-identity
    private string GetNameIdentifierClaim()
    {
        var claimsIdentity = (ClaimsIdentity)User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        return claim.Value;
    }


    // Get AppUser info using NameIdentifier claim
    private async Task<AppUserDTO> AppUserByService()
    {
        AppUserDTO appUserFromDb = new();
        APIResponse appUserResponse = await _appUserService.GetAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (appUserResponse != null && appUserResponse.IsSuccess == true)
        {
            var stringAppUserFromDb = Convert.ToString(appUserResponse.Data);
            appUserFromDb = JsonConvert.DeserializeObject<AppUserDTO>(stringAppUserFromDb);
        }
        return appUserFromDb;
    }


    public async Task<IActionResult> Index()
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = new();
        APIResponse bankAccountResponse = await _bankAccountService.GetAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (bankAccountResponse != null && bankAccountResponse.IsSuccess == true)
        {
            var stringBankAccountFromDb = Convert.ToString(bankAccountResponse.Data);
            bankAccountFromDb = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
        }

        // if NOT found "this user's bankaccount", create to db.
        if (bankAccountFromDb.BankAccountId == 0)
        {
            BankAccountCreateDTO bankAccountCreateDTO = new()
            {
                HolderName = AppUserByService().Result.Name,
                AppUserId = GetNameIdentifierClaim(),
                CheckingsBalance = 0,
                SavingsBalance = 0,
                TransactionAmount = 0
            };
            // add to db and redirect.
            APIResponse createResponse = await _bankAccountService.CreateAsync<APIResponse>(bankAccountCreateDTO, "");
            if (createResponse != null && createResponse.IsSuccess == true)
            {
                return View();
            }
        }
        // if found, display info.
        return View(bankAccountFromDb);
    }



    // update mechanism
    /*
     BankAccountUpdateDTO bankAccountUpdateDTO = new()
            {
                BankAccountId = bankAccountFromDb.BankAccountId,
                AppUserId = bankAccountDTO.AppUserId,
                FoodId = bankAccountDTO.FoodId,
                CurrentPrice = bankAccountDTO.CurrentPrice,
                Count = bankAccountFromDb.Count + bankAccountDTO.Count
            };
            // update to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
            if (updateResponse != null && updateResponse.IsSuccess == true)
            {
                TempData["success"] = "Updated item-count in cart.";
                return RedirectToAction(nameof(Index), "OrderFood", "menu");
            }

    */

}