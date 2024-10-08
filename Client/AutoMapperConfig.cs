﻿using AutoMapper;
using Client.Models.Bank;
using Client.Models.BankDTOs;
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

        CreateMap<BankAccountDTO, BankAccountCreateDTO>().ReverseMap();
        CreateMap<BankAccountDTO, BankAccountUpdateDTO>().ReverseMap();

        CreateMap<TransactionCreateDTO, Transaction>().ReverseMap();
        CreateMap<TransactionUpdateDTO, Transaction>().ReverseMap();
        CreateMap<Transaction, TransactionDTO>().ReverseMap();


    }
}
