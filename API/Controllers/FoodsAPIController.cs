﻿using API.Data;
using API.Models;
using API.Models.DTOs.OrderFood;
using API.Models.OrderFood;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers;

[Route("api/FoodsAPI")]
[ApiController]
public class FoodsAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FoodsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // GET: api/FoodsAPI
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> GetFoods()
    {
        try
        {
            IEnumerable<Food> foods;
            foods = await _unitOfWork.FoodRepo.GetAllAsync(includeProperties: "Category");
            _response.Result = _mapper.Map<List<FoodDTO>>(foods);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
            return _response;
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
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            Food food = await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(filter: f => f.FoodId == id, includeProperties: "Category");

            if (food == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<FoodDTO>(food);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
            return _response;
        }
    }


    // POST: api/FoodsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateFood([FromBody] FoodCreateDTO createDTO)
    {
        try
        {
            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(f => f.Name == createDTO.Name) != null)
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
                return BadRequest(_response);
            }
            Food food = _mapper.Map<Food>(createDTO);
            await _unitOfWork.FoodRepo.CreateAsync(food);

            // response
            _response.Result = _mapper.Map<FoodDTO>(food);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
        }
        return _response;
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
    public async Task<ActionResult<APIResponse>> UpdateFood(int id, [FromBody] FoodUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.FoodId)
            {
                return BadRequest(); // don't enclose _reponse.
            }
            if (await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.CategoryId == updateDTO.CategoryId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "CategoryID is Invalid!");
                return BadRequest(ModelState);
            }

            Food food = _mapper.Map<Food>(updateDTO);
            await _unitOfWork.FoodRepo.UpdateAsync(food);

            // response
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
        }
        return _response;
    }


    // DELETE: api/FoodsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteFood")]
    public async Task<ActionResult<APIResponse>> DeleteFood(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Food food = await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(f => f.FoodId == id);

            if (food == null)
            {
                return NotFound();
            }
            await _unitOfWork.FoodRepo.RemoveAsync(food);

            // response
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
        }
        return _response;
    }
}
