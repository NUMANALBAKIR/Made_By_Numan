﻿using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.OrderFood;

public class CategoryDTO
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
}
