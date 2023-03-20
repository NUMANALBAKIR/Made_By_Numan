namespace API.Models.StudentCRUDDTOs;

public class SubjectUpdateDTO
{
    public int SubjectId { get; set; }
    public string Name { get; set; }
    public double Grade { get; set; }

    //public int StudentId { get; set; }
    //[ValidateNever, ForeignKey(nameof(StudentId))]
    //public Student Student { get; set; }

}