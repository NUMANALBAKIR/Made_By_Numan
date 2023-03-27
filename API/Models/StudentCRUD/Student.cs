using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.StudentCRUD;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string PassedOrFailed { get; set; }
    public string Country { get; set; }

    public int SubjectsListId { get; set; }
    [ForeignKey(nameof(SubjectsListId))]
    public SubjectsList SubjectsList { get; set; }


    //public ICollection<Subject> Subjects { get; set; }

}
