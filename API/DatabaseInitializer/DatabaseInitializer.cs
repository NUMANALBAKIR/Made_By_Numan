using API.Data;
using API.Models.OrderFood;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseInitializer;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDbContext _db;

    public DatabaseInitializer(AppDbContext db)
    {
        _db = db;
    }

    // Add Food, Category lists to Db
    public async Task Initialize()
    {
        // add foods
        if (await _db.Foods.CountAsync() == 0)
        {
            await _db.Foods.AddAsync(new Food
            {
                Name = "Magnum Tiste",
                Description = "Lorem, deren, trataro, filede, nerada",
                Price = 5.95M,
                ImageURL = "assets/img/menu/menu-item-1.png",
                CategoryId = 0
            });
            await _db.Foods.AddAsync(new Food
            {
                Name = "Aut Luia",
                Description = "deren, trataro, filede, nerada, Lorem",
                Price = 14.95M,
                ImageURL = "assets/img/menu/menu-item-2.png",
                CategoryId = 0
            });
            await _db.Foods.AddAsync(new Food
            {
                Name = "Est Eligendi",
                Description = "trataro, filede, nerada, Lorem, deren",
                Price = 8.95M,
                ImageURL = "assets/img/menu/menu-item-3.png",
                CategoryId = 1
            });
            await _db.Foods.AddAsync(new Food
            {
                Name = "Eos Luibusdam",
                Description = "filede, nerada, Lorem, deren, trataro",
                Price = 12.95M,
                ImageURL = "assets/img/menu/menu-item-4.png",
                CategoryId = 2
            });
            await _db.Foods.AddAsync(new Food
            {
                Name = "Luibusdam Eos",
                Description = "nerada, Lorem, deren, trataro, filede",
                Price = 10.0M,
                ImageURL = "assets/img/menu/menu-item-5.png",
                CategoryId = 2
            });
            await _db.Foods.AddAsync(new Food
            {
                Name = "Laboriosam Direva",
                Description = "polao, nerada, Lorem, deren, trataro",
                Price = 9.95M,
                ImageURL = "assets/img/menu/menu-item-6.png",
                CategoryId = 3
            });
        }

        // Add categories
        if (await _db.Categories.CountAsync() == 0)
        {
            await _db.Categories.AddAsync(new Category
            {
                Name = "Starters"
            });
            await _db.Categories.AddAsync(new Category
            {
                Name = "Breakfast"
            });
            await _db.Categories.AddAsync(new Category
            {
                Name = "Lunch"
            });
            await _db.Categories.AddAsync(new Category
            {
                Name = "Dinner"
            });
        }
        return;
    }
}
