using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUD;

public class SubjectsList
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubjectsListId { get; set; }

    public double English { get; set; }
    public double Math { get; set; }
    public double Accounting { get; set; }
    public double Chemistry { get; set; }
    public double Biology { get; set; }


    //public int StudentId { get; set; }
    //[ValidateNever, ForeignKey(nameof(StudentId))]
    //public Student Student { get; set; }

}
