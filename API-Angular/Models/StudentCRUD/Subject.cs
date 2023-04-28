using System.ComponentModel.DataAnnotations;

namespace API_Angular.Models.StudentCRUD;

public class Subject
{
    [Key]
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int Mark { get; set; }

    public int StudentId { get; set; }

    // applicationUser

}
