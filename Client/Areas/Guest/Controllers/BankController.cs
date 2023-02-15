using AutoMapper;
using Client.Models;
using Client.Models.BankDTOs;
using Client.Models.User;
using Client.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Areas.Guest.Controllers;

[Area("Guest"), Authorize]
public class BankController : Controller
{
    private readonly IMapper _mapper;
    private readonly IAppUserService _appUserService;
    private readonly IBankAccountService _bankAccountService;

    [BindProperty]
    public BankAccountDTO bankAccount { get; set; }

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
        bankAccount = await BankAccountByService();

        // if NOT found "this user's bankaccount", create to db.
        if (bankAccount.BankAccountId == 0)
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
                bankAccount = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
                return View(bankAccount);
            }
        }
        // if found, display info.
        return View(bankAccount);
    }


    [HttpPost]
    public async Task<IActionResult> AddToSavings()
    {
        bankAccount.SavingsBalance += bankAccount.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccount);

        // update to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

        TempData["success"] = $"${bankAccount.TransactionAmount} added to Savings.";
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> WithDrawFromCheckings()
    {
        if (bankAccount.CheckingsBalance < bankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't withdraw ${bankAccount.TransactionAmount} from Checkings, because Checkings balance is ${bankAccount.CheckingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            bankAccount.CheckingsBalance -= bankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccount);

            // update to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${bankAccount.TransactionAmount} withdrawn from Checkings.";
            return RedirectToAction(nameof(Index));
        }
    }


    [HttpPost]
    public async Task<IActionResult> SavingsToCheckings()
    {
        if (bankAccount.SavingsBalance < bankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't transfer ${bankAccount.TransactionAmount} from Savings, because Savings balance is ${bankAccount.SavingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            bankAccount.SavingsBalance -= bankAccount.TransactionAmount;
            bankAccount.CheckingsBalance += bankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccount);

            // update to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${bankAccount.TransactionAmount} transferred from Savings to Checkings.";
            return RedirectToAction(nameof(Index));
        }

    }


    [HttpPost]
    public async Task<IActionResult> CheckingsToSavings()
    {
        if (bankAccount.CheckingsBalance < bankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't transfer ${bankAccount.TransactionAmount} from Checkings, because Checkings balance is ${bankAccount.CheckingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            bankAccount.CheckingsBalance -= bankAccount.TransactionAmount;
            bankAccount.SavingsBalance += bankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(bankAccount);

            // update to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${bankAccount.TransactionAmount} transferred from Checkings to Savings.";
            return RedirectToAction(nameof(Index));
        }
    }

}