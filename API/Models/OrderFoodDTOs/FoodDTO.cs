﻿namespace API.Models.OrderFoodDTOs;

public class FoodDTO
{
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string? ImageURL { get; set; }

    public int CategoryId { get; set; }
    public CategoryCreateDTO Category { get; set; }
}