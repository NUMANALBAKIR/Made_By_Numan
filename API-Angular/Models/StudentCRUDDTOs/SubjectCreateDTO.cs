namespace API_Angular.Models.StudentCRUDDTOs;

public class SubjectCreateDTO
{
    public string Name { get; set; }
    public double Grade { get; set; }

    //public int StudentId { get; set; }
    //[ValidateNever, ForeignKey(nameof(StudentId))]
    //public Student Student { get; set; }

}
