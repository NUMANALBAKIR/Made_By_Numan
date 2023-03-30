using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models.StudentCRUDDTOs;

public class SubjectDTO
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public double Grade { get; set; }

    //public int StudentId { get; set; }
    //[ValidateNever, ForeignKey(nameof(StudentId))]
    //public Student Student { get; set; }

}
