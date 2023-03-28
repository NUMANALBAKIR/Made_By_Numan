using API.Models.Bank;
using API.Models.OrderFood;
using API.Models.StudentCRUD;
using API.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Student> Students { get; set; }
    public DbSet<SubjectsList> SubjectsLists { get; set; }
    public DbSet<Country> Countries { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasData(
            new Country() { CountryId = 1, Name = "USA" },
            new Country() { CountryId = 2, Name = "Bangladesh" }
        );

        modelBuilder.Entity<SubjectsList>().HasData(
            new SubjectsList() { SubjectsListId = 1, English = 80, Math = 65, Accounting = 77, Chemistry = 88, Biology = 87 },
            new SubjectsList() { SubjectsListId = 2, English = 60, Math = 85, Accounting = 78, Chemistry = 89, Biology = 84 },
            new SubjectsList() { SubjectsListId = 3, English = 70, Math = 95, Accounting = 87, Chemistry = 85, Biology = 88 }
        );

        modelBuilder.Entity<Student>().HasData(
            new Student() { StudentId = 1, Name = "Zina", Passed = true, Gender = "Female", DateOfBirth = Convert.ToDateTime("2001-1-21"), CountryId = 1, SubjectsListId = 1 },
            new Student() { StudentId = 2, Name = "Sam", Passed = false, Gender = "Male", DateOfBirth = Convert.ToDateTime("2002-2-22"), CountryId = 2, SubjectsListId = 2 },
            new Student() { StudentId = 3, Name = "Ren", Passed = true, Gender = "Other", DateOfBirth = Convert.ToDateTime("2003-3-23"), CountryId = 2, SubjectsListId = 3 }
        );


    }

}
