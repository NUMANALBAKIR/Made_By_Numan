using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.OrderFood;

public class FoodUpdateDTO
{
    [Key]
    public int FoodId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Description { get; set; }
    [Required]
    public double Price { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
