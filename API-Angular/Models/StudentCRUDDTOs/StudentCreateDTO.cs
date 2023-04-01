namespace API_Angular.Models.StudentCRUDDTOs;

public class StudentCreateDTO
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string DateOfBirth { get; set; }
    public bool Passed { get; set; } // checkbox
    public string Gender { get; set; } // radio

    public int CountryId { get; set; }

}
