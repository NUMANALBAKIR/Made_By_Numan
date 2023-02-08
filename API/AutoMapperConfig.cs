using API.Models.Bank;
using API.Models.BankDTOs;
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

        CreateMap<BankAccount, BankAccountDTO>().ReverseMap();
        CreateMap<BankAccount, BankAccountCreateDTO>().ReverseMap();
        CreateMap<BankAccount, BankAccountUpdateDTO>().ReverseMap();

        CreateMap<CartItem, CartItemDTO>().ReverseMap();
        CreateMap<CartItem, CartItemCreateDTO>().ReverseMap();
        CreateMap<CartItem, CartItemUpdateDTO>().ReverseMap();

        CreateMap<OrderHeader, OrderHeaderDTO>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderCreateDTO>().ReverseMap();
        CreateMap<OrderHeader, OrderHeaderUpdateDTO>().ReverseMap();

        CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailCreateDTO>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailUpdateDTO>().ReverseMap();



    }
}
