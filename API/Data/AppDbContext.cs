using API.Models.OrderFood;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Category> Categories { get; set; }



}
