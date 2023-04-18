using API.Models;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

// Food Categories
[Route("api/CategoriesAPI")]
[ApiController]
public class CategoriesAPIController : ControllerBase
{
    protected IAPIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = Factory.NewAPIResponse();

    }


    // GET: api/CategoriesAPI
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetCategories()
    {
        try
        {
            IEnumerable<Category> categories;
            categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            _response.Data = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/CategoriesAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetCategory(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            Category category = await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No Category with id= {id} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<CategoryDTO>(category);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/CategoriesAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryCreateDTO createDTO)
    {
        try
        {
            if (await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(c => c.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Category already Exists!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            Category category = _mapper.Map<Category>(createDTO);
            await _unitOfWork.CategoryRepo.CreateAsync(category);

            // response
            _response.Data = _mapper.Map<CategoryDTO>(category);
            return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/CategoriesAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] CategoryUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.CategoryId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }

            Category category = _mapper.Map<Category>(updateDTO);
            await _unitOfWork.CategoryRepo.UpdateAsync(category);

            // response
            _response.Data = _mapper.Map<CategoryDTO>(category);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/CategoriesAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            Category category = await _unitOfWork.CategoryRepo.GetFirstOrDefaultAsync(u => u.CategoryId == id);

            if (category == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No category with id= {id} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.CategoryRepo.RemoveAsync(category);

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
