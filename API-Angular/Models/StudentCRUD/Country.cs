using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Angular.Models.StudentCRUD;

public class Country
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountryId { get; set; }
    public string Name { get; set; }
}
