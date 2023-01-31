using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using AutoMapper;

namespace API;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Category, CategoryCreateDTO>().ReverseMap();
        CreateMap<Category, CategoryUpdateDTO>().ReverseMap();

        CreateMap<Food, FoodDTO>().ReverseMap();
        CreateMap<Food, FoodCreateDTO>().ReverseMap();
        CreateMap<Food, FoodUpdateDTO>().ReverseMap();
    }
}
