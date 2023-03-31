using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUDDTOs;

public class StudentDTO
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string DateOfBirth { get; set; } // string
    public bool Passed { get; set; } // checkbox
    public string Gender { get; set; } // radio

    public int CountryId { get; set; }
    public CountryDTO Country { get; set; }    // cl...


}
