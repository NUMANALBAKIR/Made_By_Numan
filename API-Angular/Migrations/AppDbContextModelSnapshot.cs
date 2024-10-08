﻿// <auto-generated />
using API_Angular.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_Angular.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API_Angular.Models.StudentCRUD.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = 1,
                            Name = "USA"
                        },
                        new
                        {
                            CountryId = 2,
                            Name = "Bangladesh"
                        },
                        new
                        {
                            CountryId = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("API_Angular.Models.StudentCRUD.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Passed")
                        .HasColumnType("bit");

                    b.HasKey("StudentId");

                    b.HasIndex("CountryId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CountryId = 1,
                            DateOfBirth = "2001-12-21",
                            Gender = "Female",
                            Name = "Jina",
                            Passed = true
                        },
                        new
                        {
                            StudentId = 2,
                            CountryId = 2,
                            DateOfBirth = "2002-12-22",
                            Gender = "Male",
                            Name = "Sam",
                            Passed = false
                        },
                        new
                        {
                            StudentId = 3,
                            CountryId = 2,
                            DateOfBirth = "2003-12-23",
                            Gender = "Other",
                            Name = "Ren",
                            Passed = true
                        });
                });

            modelBuilder.Entity("API_Angular.Models.StudentCRUD.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("API_Angular.Models.StudentCRUD.SubjectsList", b =>
                {
                    b.Property<int>("SubjectsListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectsListId"));

                    b.Property<double>("Accounting")
                        .HasColumnType("float");

                    b.Property<double>("Biology")
                        .HasColumnType("float");

                    b.Property<double>("Chemistry")
                        .HasColumnType("float");

                    b.Property<double>("English")
                        .HasColumnType("float");

                    b.Property<double>("Math")
                        .HasColumnType("float");

                    b.HasKey("SubjectsListId");

                    b.ToTable("SubjectsLists");
                });

            modelBuilder.Entity("API_Angular.Models.StudentCRUD.Student", b =>
                {
                    b.HasOne("API_Angular.Models.StudentCRUD.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });
#pragma warning restore 612, 618
        }
    }
}
