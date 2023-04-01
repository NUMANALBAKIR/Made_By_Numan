using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUD;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool Passed { get; set; } // checkbox
    public string Gender { get; set; } // radio

    public int CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; }    // cl...

    //public int SubjectsListId { get; set; }
    //[ForeignKey(nameof(SubjectsListId))]
    //public SubjectsList SubjectsList { get; set; }

    //public ICollection<Subject> Subjects { get; set; }

}
