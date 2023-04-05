using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUDDTOs;

public class SubjectDTO
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; }
    public int Mark { get; set; }

    public int StudentId { get; set; }
    
    //public int StudentId { get; set; }
    //[ValidateNever, ForeignKey(nameof(StudentId))]
    //public Student Student { get; set; }

}
