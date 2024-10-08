﻿using API.Models;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/OrderDetailsAPI")]
[ApiController]
public class OrderDetailsAPIController : ControllerBase
{
    protected IAPIResponse _response;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderDetailsAPIController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _response = Factory.NewAPIResponse();

    }


    // GET: api/OrderDetailsAPI
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetOrderDetails()
    {
        try
        {
            IEnumerable<OrderDetail> orderDetails;
            orderDetails = await _unitOfWork.OrderDetailRepo.GetAllAsync(includeProperties: "Food,OrderHeader");
            _response.Data = _mapper.Map<List<OrderDetailDTO>>(orderDetails);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // GET: api/OrderDetailsAPI/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> GetOrderDetail(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0.";
                return BadRequest(_response);
            }
            OrderDetail orderDetail = await _unitOfWork.OrderDetailRepo.GetFirstOrDefaultAsync(filter: x => x.OrderDetailId == id, includeProperties: "Food,OrderHeader");

            if (orderDetail == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No OrderDetail with id= {id} exists.";
                return NotFound(_response);
            }
            _response.Data = _mapper.Map<OrderDetailDTO>(orderDetail);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // POST: api/OrderDetailsAPI
    //[Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<APIResponse>> CreateOrderDetail([FromBody] OrderDetailCreateDTO createDTO)
    {
        try
        {
            if (await _unitOfWork.OrderHeaderRepo.GetFirstOrDefaultAsync(x => x.OrderHeaderId == createDTO.OrderHeaderId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "OrderHeaderId is Invalid!");
                return BadRequest(ModelState);
            }
            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(x => x.FoodId == createDTO.FoodId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "FoodId is Invalid!");
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "createDTO is null";
                return BadRequest(_response);
            }
            OrderDetail OrderDetail = _mapper.Map<OrderDetail>(createDTO);
            await _unitOfWork.OrderDetailRepo.CreateAsync(OrderDetail);

            // response
            _response.Data = _mapper.Map<OrderDetailDTO>(OrderDetail);
            return Ok(_response);
            //return CreatedAtAction("GetOrderDetail", new { id = OrderDetail.OrderDetailId }, OrderDetail);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // PUT: api/OrderDetailsAPI/5
    //[Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<APIResponse>> UpdateOrderDetail(int id, [FromBody] OrderDetailUpdateDTO updateDTO)
    {
        try
        {
            if (updateDTO == null || id != updateDTO.OrderDetailId)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id or updateDTO has issue(s).";
                return BadRequest(_response);
            }
            if (await _unitOfWork.OrderHeaderRepo.GetFirstOrDefaultAsync(x => x.OrderHeaderId == updateDTO.OrderHeaderId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "OrderHeaderId is Invalid!");
                return BadRequest(ModelState);
            }
            if (await _unitOfWork.FoodRepo.GetFirstOrDefaultAsync(x => x.FoodId == updateDTO.FoodId) == null)
            {
                ModelState.AddModelError("ErrorMessages", "FoodId is Invalid!");
                return BadRequest(ModelState);
            }

            OrderDetail orderDetail = _mapper.Map<OrderDetail>(updateDTO);
            await _unitOfWork.OrderDetailRepo.UpdateAsync(orderDetail);

            // response
            _response.Data = _mapper.Map<OrderDetailDTO>(orderDetail);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessage = ex.ToString();
            return StatusCode(500, _response);
        }
    }


    // DELETE: api/OrderDetailsAPI/5
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("{id:int}", Name = "DeleteOrderDetail")]
    public async Task<ActionResult<APIResponse>> DeleteOrderDetail(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = "id can't be 0";
                return BadRequest(_response);
            }
            OrderDetail orderDetail = await _unitOfWork.OrderDetailRepo.GetFirstOrDefaultAsync(x => x.OrderDetailId == id);

            if (orderDetail == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage = $"No OrderDetail with id= {id} exists.";
                return NotFound(_response);
            }
            await _unitOfWork.OrderDetailRepo.RemoveAsync(orderDetail);

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
