using API.Models;
using API.Models.Bank;
using API.Models.BankDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/BankAccountsAPI")]
[ApiController]
public class BankAccountsAPIController : ControllerBase
{
    protected IAPIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BankAccountsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = Factory.NewAPIResponse();
    }

    // GET: api/BankAccountsAPI
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetBankAccounts()
    {
        try
        {
            IEnumerable<BankAccount> bankAccounts;
            bankAccounts = await _unitOfWork.BankAccountRepo.GetAllAsync(includeProperties: "AppUser");
            _response.Data = _mapper.Map<List<BankAccountDTO>>(bankAccounts);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/BankAccountsAPI/abc
    [HttpGet("{appUserId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetBankAccount(string appUserId)
    {
        try
        {
            if (appUserId == "" || appUserId == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be empty.";
                return BadRequest(_response);
            }
            BankAccount bankAccount = await _unitOfWork.BankAccountRepo.GetFirstOrDefaultAsync(filter: x => x.AppUserId == appUserId, includeProperties: "AppUser");

            if (bankAccount == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No BankAccount with AppUser id= {appUserId} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<BankAccountDTO>(bankAccount);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/BankAccountsAPI
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateBankAccount([FromBody] BankAccountCreateDTO createDTO)
    {
        try
        {
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            BankAccount bankAccount = _mapper.Map<BankAccount>(createDTO);
            await _unitOfWork.BankAccountRepo.CreateAsync(bankAccount);

            // response
            //_response.Data = _mapper.Map<BankAccountDTO>(bankAccount);
            return CreatedAtAction(nameof(GetBankAccount), new { appUserId = bankAccount.AppUserId }, new APIResponse { Data = bankAccount });
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/BankAccountsAPI/5
    //[Authorize(Roles = "Admin")]
    [HttpPut("{bankAccountId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateBankAccount(int bankAccountId, [FromBody] BankAccountUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || bankAccountId != updateDTO.BankAccountId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }

            BankAccount bankAccount = _mapper.Map<BankAccount>(updateDTO);
            await _unitOfWork.BankAccountRepo.UpdateAsync(bankAccount);

            // response
            _response.Data = _mapper.Map<BankAccountDTO>(bankAccount);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/BankAccountsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{bankAccountId:int}", Name = "DeleteBankAccount")]
    public async Task<ActionResult<APIResponse>> DeleteBankAccount(int bankAccountId)
    {
        try
        {
            if (bankAccountId == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            BankAccount bankAccount = await _unitOfWork.BankAccountRepo.GetFirstOrDefaultAsync(x => x.BankAccountId == bankAccountId);

            if (bankAccount == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No bankAccount with id= {bankAccountId} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.BankAccountRepo.RemoveAsync(bankAccount);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }



}
