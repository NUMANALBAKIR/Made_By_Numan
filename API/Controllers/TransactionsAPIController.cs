using API.Models;
using API.Models.Bank;
using API.Models.BankDTOs;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/TransactionsAPI")]
[ApiController]
public class TransactionsAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TransactionsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // get all orderheaders of this appUser
    // GET: api/TransactionsAPI?appUserId=abc
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetTransactions(string appUserId)
    {
        try
        {
            IEnumerable<Transaction> transactions;
            transactions = await _unitOfWork.TransactionRepo.GetAllAsync(filter: x => x.AppUserId == appUserId, includeProperties: "AppUser");
            _response.Data = _mapper.Map<List<TransactionDTO>>(transactions);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/TransactionsAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetTransaction(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            Transaction transaction = await _unitOfWork.TransactionRepo.GetFirstOrDefaultAsync(filter: x => x.TransactionId == id);

            if (transaction == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Transaction with id= {id} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<TransactionDTO>(transaction);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/TransactionsAPI
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateTransaction([FromBody] TransactionCreateDTO createDTO)
    {
        try
        {
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            Transaction Transaction = _mapper.Map<Transaction>(createDTO);
            await _unitOfWork.TransactionRepo.CreateAsync(Transaction);

            // response
            _response.Data = _mapper.Map<TransactionDTO>(Transaction);
            return Ok(_response);
            //return CreatedAtAction("GetTransaction", new { id = Transaction.TransactionId }, Transaction);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/TransactionsAPI/5
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateTransaction(int id, [FromBody] TransactionUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.TransactionId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }

            Transaction transaction = _mapper.Map<Transaction>(updateDTO);
            await _unitOfWork.TransactionRepo.UpdateAsync(transaction);

            // response
            _response.Data = _mapper.Map<TransactionDTO>(transaction);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/TransactionsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteTransaction")]
    public async Task<ActionResult<APIResponse>> DeleteTransaction(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            Transaction Transaction = await _unitOfWork.TransactionRepo.GetFirstOrDefaultAsync(x => x.TransactionId == id);

            if (Transaction == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Transaction with id= {id} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.TransactionRepo.RemoveAsync(Transaction);

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
