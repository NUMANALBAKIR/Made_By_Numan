using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Food
{
    [Key]
    public int FoodId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int? Description { get; set; }
    [Required]
    public string Category { get; set; }
}
