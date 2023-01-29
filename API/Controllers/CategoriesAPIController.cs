using API.Data;
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

[Route("api/CategoriesAPI")]
[ApiController]
public class CategoriesAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // GET: api/CategoriesAPI
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> GetCategories()
    {
        try
        {
            IEnumerable<Category> categories;
            categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            _response.Result = _mapper.Map<List<CategoryDTO>>(categories);
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


    // GET: api/CategoriesAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetCategory(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            Category category = await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<CategoryDTO>(category);
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


    // POST: api/CategoriesAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryCreateDTO createDTO)
    {
        try
        {
            if (await CategoryExists(createDTO.Name) == true)
            {
                ModelState.AddModelError("ErrorMessages", "Villa already Exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(_response);
            }
            Category category = _mapper.Map<Category>(createDTO);
            await _unitOfWork.CategoryRepo.CreateAsync(category);

            // response
            _response.Result = _mapper.Map<CategoryDTO>(category);
            _response.StatusCode = HttpStatusCode.OK;
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
        }
        return _response;
    }


    // PUT: api/CategoriesAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] FoodUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.CategoryId)
            {
                return BadRequest(_response);
            }

            Category category = _mapper.Map<Category>(updateDTO);
            await _unitOfWork.CategoryRepo.UpdateAsync(category);

            // response
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return (_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.ErrorMessages.Add(ex.ToString());
        }
        return _response;
    }


    // DELETE: api/CategoriesAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
    {
        try
        {
            if (id == 0)
            {
                return BadRequest();
            }
            Category category = await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(u => u.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            await _unitOfWork.CategoryRepo.RemoveAsync(category);

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


    [HttpGet("{name}")]
    public async Task<bool> CategoryExists(string name)
    {
        return await _unitOfWork.CategoryRepo.AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }
}
