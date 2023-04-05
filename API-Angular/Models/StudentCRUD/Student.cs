using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUD;

public class Student
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string DateOfBirth { get; set; }
    public bool Passed { get; set; } // checkbox
    public string Gender { get; set; } // radio

    public int CountryId { get; set; }
    [ForeignKey(nameof(CountryId))]
    public Country Country { get; set; }    // cl...

    // public List<Subject> Subjects { get; set; } // sk..



}
