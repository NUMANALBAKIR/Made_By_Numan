namespace API_Angular.Models.StudentCRUDDTOs;

public class StudentUpdateDTO
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string Pass { get; set; }

    //public ICollection<Subject> Subjects { get; set; }

}
