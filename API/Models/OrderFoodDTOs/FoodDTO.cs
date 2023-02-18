namespace API.Models.OrderFoodDTOs;

public class FoodDTO
{
    public int FoodId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Precision(18, 2)] public decimal Price { get; set; }
    public string? ImageURL { get; set; }

    public int CategoryId { get; set; }
    public CategoryDTO Category { get; set; }
}
