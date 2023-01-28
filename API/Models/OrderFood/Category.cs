using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]
    public int Name { get; set; }
}
