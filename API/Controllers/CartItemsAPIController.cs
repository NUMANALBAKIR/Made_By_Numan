using API.Models;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Models.User;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/CartItemsAPI")]
[ApiController]
public class CartItemsAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CartItemsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // get all cartitems of this user
    // GET: api/CartItemsAPI?appUserId=abc
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetCartItems(string appUserId)
    {
        try
        {
            IEnumerable<CartItem> cartItems;
            cartItems = await _unitOfWork.CartItemRepo.GetAllAsync(filter: x => x.AppUserId == appUserId, includeProperties: "Food,AppUser");
            _response.Data = _mapper.Map<List<CartItemDTO>>(cartItems);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // Get cart item having this user's this food.
    // api/CartItemsAPI/2/abc
    [HttpGet("{foodId}/{appUserId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetCartItem(int foodId, string appUserId)
    {
        try
        {
            if (foodId == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            CartItem cartItem = await _unitOfWork.CartItemRepo.GetFirstOrDefaultAsync(filter: x => (x.FoodId == foodId) && (x.AppUserId == appUserId), includeProperties: "Food,AppUser");

            if (cartItem == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No CartItem with id= {foodId} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<CartItemDTO>(cartItem);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/CartItemsAPI
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateCartItem([FromBody] CartItemCreateDTO createDTO)
    {
        try
        {
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
            CartItem cartItem = _mapper.Map<CartItem>(createDTO);
            await _unitOfWork.CartItemRepo.CreateAsync(cartItem);

            // response
            _response.Data = _mapper.Map<CartItemDTO>(cartItem);
            return CreatedAtAction(nameof(GetCartItem), new { foodId = cartItem.FoodId, appUserId = cartItem.AppUserId }, cartItem);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/CartItemsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateCartItem(int id, [FromBody] CartItemUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.CartItemId)
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

            CartItem CartItem = _mapper.Map<CartItem>(updateDTO);
            await _unitOfWork.CartItemRepo.UpdateAsync(CartItem);

            // response
            _response.Data = _mapper.Map<CartItemDTO>(CartItem);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/CartItemsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteCartItem")]
    public async Task<ActionResult<APIResponse>> DeleteCartItem(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            CartItem cartItem = await _unitOfWork.CartItemRepo.GetFirstOrDefaultAsync(x => x.CartItemId == id);

            if (cartItem == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Cart Item with id= {id} exists.";
                return NotFound(_response);
            }

            await _unitOfWork.CartItemRepo.RemoveAsync(cartItem);
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
