using API.Models;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/OrderHeadersAPI")]
[ApiController]
public class OrderHeadersAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderHeadersAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // GET: api/OrderHeadersAPI
    //[ResponseCache(CacheProfileName = "Default30s")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetOrderHeaders()
    {
        try
        {
            IEnumerable<OrderHeader> orderHeaders;
            orderHeaders = await _unitOfWork.OrderHeaderRepo.GetAllAsync(includeProperties: "Food");
            _response.Data = _mapper.Map<List<OrderHeaderDTO>>(orderHeaders);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/OrderHeadersAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetOrderHeader(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            OrderHeader orderHeader = await _unitOfWork.OrderHeaderRepo.GetFirstOrDefaultAsync(filter: x => x.OrderHeaderId == id, includeProperties: "Food");

            if (orderHeader == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No OrderHeader with id= {id} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<OrderHeaderDTO>(orderHeader);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/OrderHeadersAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateOrderHeader([FromBody] OrderHeaderCreateDTO createDTO)
    {
        try
        {
            //if (await _unitOfWork.OrderHeaderRepo.GetFirstOrDefaultAsync(x => x.FoodId == createDTO.FoodId) != null)
            //{
            //    ModelState.AddModelError("ErrorMessages", "OrderHeader already Exists!");
            //    return BadRequest(ModelState);
            //}

            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(c => c.FoodId == createDTO.FoodId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "FoodID is Invalid!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            OrderHeader OrderHeader = _mapper.Map<OrderHeader>(createDTO);
            await _unitOfWork.OrderHeaderRepo.CreateAsync(OrderHeader);

            // response
            _response.Data = _mapper.Map<OrderHeaderDTO>(OrderHeader);
            return Ok(_response);
            //return CreatedAtAction("GetOrderHeader", new { id = OrderHeader.OrderHeaderId }, OrderHeader);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/OrderHeadersAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateOrderHeader(int id, [FromBody] OrderHeaderUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.OrderHeaderId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }
            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(c => c.FoodId == updateDTO.FoodId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "Food ID is Invalid!");
                return BadRequest(ModelState);
            }

            OrderHeader OrderHeader = _mapper.Map<OrderHeader>(updateDTO);
            await _unitOfWork.OrderHeaderRepo.UpdateAsync(OrderHeader);

            // response
            _response.Data = _mapper.Map<OrderHeaderDTO>(OrderHeader);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/OrderHeadersAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteOrderHeader")]
    public async Task<ActionResult<APIResponse>> DeleteOrderHeader(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            OrderHeader OrderHeader = await _unitOfWork.OrderHeaderRepo.GetFirstOrDefaultAsync(f => f.OrderHeaderId == id);

            if (OrderHeader == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Cart Item with id= {id} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.OrderHeaderRepo.RemoveAsync(OrderHeader);

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
