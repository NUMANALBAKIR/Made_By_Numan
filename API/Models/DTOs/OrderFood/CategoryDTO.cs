﻿using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.OrderFood;

public class CategoryDTO
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
}