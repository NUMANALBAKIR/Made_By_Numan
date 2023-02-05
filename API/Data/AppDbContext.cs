﻿using API.Models.Bank;
using API.Models.OrderFood;
using API.Models.OrderFoodDTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }



}
