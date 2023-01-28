﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Food
{
    [Key]
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }

    // ???
    public int CategoryId { get; set; }
    [ValidateNever, ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
}
