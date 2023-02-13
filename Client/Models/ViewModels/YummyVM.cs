using Client.Models.OrderFoodDTOs;

namespace Client.Models.ViewModels;

public class YummyVM
{
    public List<FoodDTO> FoodList { get; set; }
    public EmailUsDTO EmailUsDTO { get; set; }
}
