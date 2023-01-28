using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Food
{
    [Key]
    public int FoodId { get; set; }
    [Required, Range(1, 20)]
    public string Name { get; set; }
    [Required]
    public int? Description { get; set; }
    public double Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
    [ValidateNever, ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
}
