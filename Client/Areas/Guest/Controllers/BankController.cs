using AutoMapper;
using Client.Models;
using Client.Models.Bank;
using Client.Models.BankDTOs;
using Client.Models.User;
using Client.Models.ViewModels;
using Client.Services;
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
    private readonly ITransactionService _transactionService;

    [BindProperty]
    public BankDashboardVM dashboard { get; set; }

    public BankController(IMapper mapper,
        IAppUserService appUserService,
        IBankAccountService bankAccountService,
        ITransactionService transactionService)
    {
        _mapper = mapper;
        _appUserService = appUserService;
        _bankAccountService = bankAccountService;
        _transactionService = transactionService;
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


    // get all transactions of this user using NameIdentifierClaim.
    public async Task<List<TransactionDTO>> TransactionsByService()
    {
        List<TransactionDTO> transactions = new();
        APIResponse response = await _transactionService.GetAllAsync<APIResponse>(GetNameIdentifierClaim(), "");
        if (response != null && response.IsSuccess == true)
        {
            var stringList = Convert.ToString(response.Data);
            transactions = JsonConvert.DeserializeObject<List<TransactionDTO>>(stringList);
        }
        return transactions;
    }


    // add this transaction to db.
    public async Task AddTransactionByService(TransactionCreateDTO createDto)
    {
        await _transactionService.CreateAsync<APIResponse>(createDto, "");
    }


    // home page of Bank
    public async Task<IActionResult> Index()
    {
        dashboard = new()
        {
            BankAccount = new(),
            Transactions = new()
        };

        // fetch this appUser's bankaccount
        dashboard.BankAccount = await BankAccountByService();

        // if NOT found "this user's bank account", create to db.
        if (dashboard.BankAccount.BankAccountId == 0)
        {
            BankAccountCreateDTO bankAccountCreateDTO = new()
            {
                HolderName = AppUserByService().Result.Name,
                AppUserId = GetNameIdentifierClaim(),
                CheckingsBalance = 0,
                SavingsBalance = 0,
                TransactionAmount = 0
            };
            // create to db and redirect.
            APIResponse createResponse = await _bankAccountService.CreateAsync<APIResponse>(bankAccountCreateDTO, "");
            if (createResponse != null && createResponse.IsSuccess == true)
            {
                var stringBankAccountFromDb = Convert.ToString(createResponse.Data);
                dashboard.BankAccount = JsonConvert.DeserializeObject<BankAccountDTO>(stringBankAccountFromDb);
                return View(dashboard);
            }
        }

        // else bankAccount found. add this accounts transactions to dashboard.
        dashboard.Transactions = await TransactionsByService();
        dashboard.Transactions.Reverse(); // to show last one on top.

        return View(dashboard);
    }


    [HttpPost]
    public async Task<IActionResult> AddToSavings()
    {
        dashboard.BankAccount.SavingsBalance += dashboard.BankAccount.TransactionAmount;
        BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(dashboard.BankAccount);

        // update balances to db and redirect
        APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

        TempData["success"] = $"${dashboard.BankAccount.TransactionAmount} added to Savings.";

        // add this transaction to db
        TransactionCreateDTO createDto = new()
        {
            AppUserId = GetNameIdentifierClaim(),
            PreviousCheckingsBalance = dashboard.BankAccount.CheckingsBalance,
            PreviousSavingsBalance = dashboard.BankAccount.SavingsBalance - dashboard.BankAccount.TransactionAmount,
            TransactionDate = DateTime.Now,
            Message = $"${dashboard.BankAccount.TransactionAmount} added to Savings."
        };
        await AddTransactionByService(createDto);

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    public async Task<IActionResult> SavingsToCheckings()
    {
        // in-sufficient balance
        if (dashboard.BankAccount.SavingsBalance < dashboard.BankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't transfer ${dashboard.BankAccount.TransactionAmount} from Savings, because Savings balance is ${dashboard.BankAccount.SavingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        // sufficient balance
        {
            dashboard.BankAccount.SavingsBalance -= dashboard.BankAccount.TransactionAmount;
            dashboard.BankAccount.CheckingsBalance += dashboard.BankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(dashboard.BankAccount);

            // update balances to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${dashboard.BankAccount.TransactionAmount} transferred from Savings to Checkings.";

            // add this transaction to db
            TransactionCreateDTO createDto = new()
            {
                AppUserId = GetNameIdentifierClaim(),
                PreviousSavingsBalance = dashboard.BankAccount.SavingsBalance + dashboard.BankAccount.TransactionAmount,
                PreviousCheckingsBalance = dashboard.BankAccount.CheckingsBalance - dashboard.BankAccount.TransactionAmount,
                TransactionDate = DateTime.Now,
                Message = $"${dashboard.BankAccount.TransactionAmount} transferred from Savings to Checkings."
            };
            await AddTransactionByService(createDto);

            return RedirectToAction(nameof(Index));
        }

    }


    [HttpPost]
    public async Task<IActionResult> CheckingsToSavings()
    {
        // in-sufficient balance
        if (dashboard.BankAccount.CheckingsBalance < dashboard.BankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't transfer ${dashboard.BankAccount.TransactionAmount} from Checkings, because Checkings balance is ${dashboard.BankAccount.CheckingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        // sufficient balance
        {
            dashboard.BankAccount.CheckingsBalance -= dashboard.BankAccount.TransactionAmount;
            dashboard.BankAccount.SavingsBalance += dashboard.BankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(dashboard.BankAccount);

            // update balances to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${dashboard.BankAccount.TransactionAmount} transferred from Checkings to Savings.";

            // add this transaction to db
            TransactionCreateDTO createDto = new()
            {
                AppUserId = GetNameIdentifierClaim(),
                PreviousCheckingsBalance = dashboard.BankAccount.CheckingsBalance + dashboard.BankAccount.TransactionAmount,
                PreviousSavingsBalance = dashboard.BankAccount.SavingsBalance - dashboard.BankAccount.TransactionAmount,
                TransactionDate = DateTime.Now,
                Message = $"${dashboard.BankAccount.TransactionAmount} transferred from Checkings to Savings."
            };
            await AddTransactionByService(createDto);

            return RedirectToAction(nameof(Index));
        }
    }


    [HttpPost]
    public async Task<IActionResult> WithDrawFromCheckings()
    {
        // in-sufficient blance
        if (dashboard.BankAccount.CheckingsBalance < dashboard.BankAccount.TransactionAmount)
        {
            TempData["error"] = $"Can't withdraw ${dashboard.BankAccount.TransactionAmount} from Checkings, because Checkings balance is ${dashboard.BankAccount.CheckingsBalance}.";
            return RedirectToAction(nameof(Index));
        }
        else
        // sufficient balance
        {
            dashboard.BankAccount.CheckingsBalance -= dashboard.BankAccount.TransactionAmount;
            BankAccountUpdateDTO bankAccountUpdateDTO = _mapper.Map<BankAccountUpdateDTO>(dashboard.BankAccount);

            // update balances to db and redirect
            APIResponse updateResponse = await _bankAccountService.UpdateAsync<APIResponse>(bankAccountUpdateDTO, "");

            TempData["success"] = $"${dashboard.BankAccount.TransactionAmount} withdrawn from Checkings.";

            // add this transaction to db
            TransactionCreateDTO createDto = new()
            {
                AppUserId = GetNameIdentifierClaim(),
                PreviousCheckingsBalance = dashboard.BankAccount.CheckingsBalance + dashboard.BankAccount.TransactionAmount,
                PreviousSavingsBalance = dashboard.BankAccount.SavingsBalance,
                TransactionDate = DateTime.Now,
                Message = $"${dashboard.BankAccount.TransactionAmount} cash withdrawn from Checkings."
            };
            await AddTransactionByService(createDto);

            return RedirectToAction(nameof(Index));
        }
    }


}