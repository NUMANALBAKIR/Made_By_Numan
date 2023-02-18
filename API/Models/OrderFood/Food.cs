using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.OrderFood;

public class Food
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Precision(18, 2)] public decimal Price { get; set; }
    public string? ImageURL { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
}
