using AutoMapper;
using Client.Models.OrderFood;
using Client.Models.OrderFoodDTOs;
using Client.Models.User;

namespace Client;

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

        CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
        CreateMap<OrderHeaderDTO, OrderHeaderCreateDTO>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderUpdateDTO>().ReverseMap();

        CreateMap<AppUser, AppUserDTO>().ReverseMap();
        //CreateMap<AppUser, AppUserCreateDTO>().ReverseMap();
        //CreateMap<AppUser, AppUserUpdateDTO>().ReverseMap();

    }
}
