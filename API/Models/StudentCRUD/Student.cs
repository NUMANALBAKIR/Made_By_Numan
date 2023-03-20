﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.StudentCRUD;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string Pass { get; set; }

    //public ICollection<Subject> Subjects { get; set; }

}
