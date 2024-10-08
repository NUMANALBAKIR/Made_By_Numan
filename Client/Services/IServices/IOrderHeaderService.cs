﻿using Client.Models.OrderFoodDTOs;

namespace Client.Services.IServices;

public interface IOrderHeaderService
{
    Task<T> GetAllAsync<T>(string appUserId, string token);
    Task<T> GetAsync<T>(int id, string token);
    Task<T> CreateAsync<T>(OrderHeaderCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(OrderHeaderUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
