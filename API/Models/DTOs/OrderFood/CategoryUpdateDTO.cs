using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.OrderFood;

public class CategoryUpdateDTO
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public string Name { get; set; }
}
