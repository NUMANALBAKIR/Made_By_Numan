using API.Models;
using API.Models.User;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/AppUsersAPI")]
[ApiController]
public class AppUsersAPIController : ControllerBase
{
    protected APIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppUsersAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = new APIResponse();
    }


    // Get appUser having this appUserId.
    // api/AppUsersAPI?appUserId=abc
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetAppUser(string appUserId)
    {
        try
        {
            if (appUserId == "" || appUserId == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be empty.";
                return BadRequest(_response);
            }
            AppUser appUser = await _unitOfWork.AppUserRepo.GetFirstOrDefaultAsync(filter: x => x.Id == appUserId);

            if (appUser == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No AppUser with id={appUserId} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<AppUserDTO>(appUser);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/AppUsersAPI
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("GetAll")]
    public async Task<ActionResult<APIResponse>> GetAppUsers()
    {
        try
        {
            List<AppUser> appUsers;
            appUsers = await _unitOfWork.AppUserRepo.GetAllAsync();
            _response.Data = _mapper.Map<List<AppUserDTO>>(appUsers);
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
