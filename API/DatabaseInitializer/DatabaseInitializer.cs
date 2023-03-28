using API.Data;
using API.Models.OrderFood;
using API.Models.StudentCRUD;

namespace API.DatabaseInitializer;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly AppDbContext _db;

    public DatabaseInitializer(AppDbContext db)
    {
        _db = db;
    }

    // Add Food, Category lists to Db
    public void Initialize()
    {
        // Add students
        //if (_db.Students.Count() == 0)
        //{
        //    _db.Students.Add(new Student()
        //    {
        //        StudentId = 1,
        //        Name = "Sam",
        //        DateOfBirth = DateTime.Now,
        //        Age = 23,
        //        PassedOrFailed = "Passed"
        //    });
        //    _db.Students.Add(new Student()
        //    {
        //        StudentId = 2,
        //        Name = "Iram",
        //        DateOfBirth = DateTime.Now,
        //        Age = 24,
        //        PassedOrFailed = "failed"
        //    });
        //    _db.Students.Add(new Student()
        //    {
        //        StudentId = 3,
        //        Name = "Zina",
        //        DateOfBirth = DateTime.Now,
        //        Age = 25,
        //        PassedOrFailed = "passed"
        //    });
        //    _db.SaveChanges();
        //}

        // Add categories
        if (_db.Categories.Count() == 0)
        {
            _db.Categories.Add(new Category
            {
                Name = "Starters"
            });
            _db.Categories.Add(new Category
            {
                Name = "Breakfast"
            });
            _db.Categories.Add(new Category
            {
                Name = "Lunch"
            });
            _db.Categories.Add(new Category
            {
                Name = "Dinner"
            });
            _db.SaveChanges();
        }

        // add foods
        if (_db.Foods.Count() == 0)
        {
            _db.Foods.Add(new Food
            {
                Name = "Magnum Tiste",
                Description = "Lorem, deren, trataro, filede, nerada",
                Price = 5.95M,
                ImageURL = "assets/img/menu/menu-item-1.png",
                CategoryId = 1
            });
            _db.Foods.Add(new Food
            {
                Name = "Aut Luia",
                Description = "deren, trataro, filede, nerada, Lorem",
                Price = 14.95M,
                ImageURL = "assets/img/menu/menu-item-2.png",
                CategoryId = 1
            });
            _db.Foods.Add(new Food
            {
                Name = "Est Eligendi",
                Description = "trataro, filede, nerada, Lorem, deren",
                Price = 8.95M,
                ImageURL = "assets/img/menu/menu-item-3.png",
                CategoryId = 2
            });
            _db.Foods.Add(new Food
            {
                Name = "Eos Luibusdam",
                Description = "filede, nerada, Lorem, deren, trataro",
                Price = 12.95M,
                ImageURL = "assets/img/menu/menu-item-4.png",
                CategoryId = 3
            });
            _db.Foods.Add(new Food
            {
                Name = "Luibusdam Eos",
                Description = "nerada, Lorem, deren, trataro, filede",
                Price = 10.0M,
                ImageURL = "assets/img/menu/menu-item-5.png",
                CategoryId = 3
            });
            _db.Foods.Add(new Food
            {
                Name = "Laboriosam Direva",
                Description = "polao, nerada, Lorem, deren, trataro",
                Price = 9.95M,
                ImageURL = "assets/img/menu/menu-item-6.png",
                CategoryId = 4
            });
            _db.SaveChanges();
        }
        return;
    }
}
