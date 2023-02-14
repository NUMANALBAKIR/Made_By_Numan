using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly IAppUserService _appUserService;
    private readonly IBankAccountService _bankAccountService;

    public BankController(IMapper mapper, IAppUserService appUserService, IBankAccountService bankAccountService)
    {
        _mapper = mapper;
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


    // fetch this appUser's bankaccount
    private async Task<BankAccountDTO> BankAccountByService()
    {
        BankAccountDTO bankAccountFromDb = new();
        APIResponse bankAccountResponse = await _bankAccountService.GetAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (bankAccountResponse != null && bankAccountResponse.IsSuccess == true)
        {
            var stringBankAccountFromDb = Convert.ToString(bankAccountResponse.Data);
            bankAccountFromDb = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
        }
        return bankAccountFromDb;
    }


    public async Task<IActionResult> Index()
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = await BankAccountByService();

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
                var stringBankAccountFromDb = Convert.ToString(createResponse.Data);
                bankAccountFromDb = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
                return View(bankAccountFromDb);
            }
        }
        // if found, display info.
        return View(bankAccountFromDb);
    }


    [HttpPost]
    public async Task<IActionResult> AddToSavings(BankAccountDTO dto)
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = await BankAccountByService();
        bankAccountFromDb.SavingsBalance += dto.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccountFromDb);

        // update to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> WithDrawFromCheckings(BankAccountDTO dto)
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = await BankAccountByService();
        bankAccountFromDb.CheckingsBalance -= dto.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccountFromDb);

        // update to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> SavingsToCheckings(BankAccountDTO dto)
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = await BankAccountByService();
        bankAccountFromDb.SavingsBalance -= dto.TransactionAmount;
        bankAccountFromDb.CheckingsBalance += dto.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccountFromDb);

        // update to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> CheckingsToSavings(BankAccountDTO dto)
    {
        // fetch this appUser's bankaccount
        BankAccountDTO bankAccountFromDb = await BankAccountByService();
        bankAccountFromDb.CheckingsBalance -= dto.TransactionAmount;
        bankAccountFromDb.SavingsBalance += dto.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccountFromDb);

        // update to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");
        return RedirectToAction(nameof(Index));
    }

}