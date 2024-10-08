﻿using API.Models;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/FoodsAPI")]
[ApiController]
public class FoodsAPIController : ControllerBase
{
    protected IAPIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FoodsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = Factory.NewAPIResponse();

    }


    // GET: api/FoodsAPI
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetFoods()
    {
        try
        {
            IEnumerable<Food> foods;
            foods = await _unitOfWork.FoodRepo.GetAllAsync(includeProperties: "Category");
            _response.Data = _mapper.Map<List<FoodDTO>>(foods);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/FoodsAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetFood(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            Food food = await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(filter: f => f.FoodId == id, includeProperties: "Category");

            if (food == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Food with id= {id} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<FoodDTO>(food);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/FoodsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateFood([FromBody] FoodCreateDTO createDTO)
    {
        try
        {
            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(f => f.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Food already Exists!");
                return BadRequest(ModelState);
            }
            if (await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.CategoryId == createDTO.CategoryId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "CategoryID is Invalid!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            Food food = _mapper.Map<Food>(createDTO);
            await _unitOfWork.FoodRepo.CreateAsync(food);

            // response
            _response.Data = _mapper.Map<FoodDTO>(food);
            return CreatedAtAction(nameof(GetFood), new { id = food.FoodId }, food);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/FoodsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateFood(int id, [FromBody] FoodUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.FoodId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }
            if (await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.CategoryId == updateDTO.CategoryId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                return BadRequest(ModelState);
            }

            Food food = _mapper.Map<Food>(updateDTO);
            await _unitOfWork.FoodRepo.UpdateAsync(food);

            // response
            _response.Data = _mapper.Map<FoodDTO>(food);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/FoodsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteFood")]
    public async Task<ActionResult<APIResponse>> DeleteFood(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            Food food = await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(f => f.FoodId == id);

            if (food == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No food with id= {id} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.FoodRepo.RemoveAsync(food);

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
