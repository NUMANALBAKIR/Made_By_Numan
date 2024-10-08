﻿using Client.Models.OrderFoodDTOs;

namespace Client.Services.IServices;

public interface IFoodService
{
    Task<T> GetAllAsync<T>(string token);
    Task<T> GetAsync<T>(int id, string token);
    Task<T> CreateAsync<T>(FoodCreateDTO dto, string token);
    Task<T> UpdateAsync<T>(FoodUpdateDTO dto, string token);
    Task<T> DeleteAsync<T>(int id, string token);
}
