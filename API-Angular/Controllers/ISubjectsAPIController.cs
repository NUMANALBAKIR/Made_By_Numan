using API_Angular.Models.StudentCRUD;
using API_Angular.Models.StudentCRUDDTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_Angular.Controllers
{
    public interface ISubjectsAPIController
    {
        Task<IActionResult> DeleteSubject(int id);
        Task<ActionResult<Subject>> GetSubject(int id);
        Task<List<SubjectDTO>> GetSubjects(int StudentId);
        Task<ActionResult<Subject>> PostSubject(SubjectCreateDTO createDto);
        Task<IActionResult> PutSubject(int id, SubjectUpdateDTO updateDto);
    }
}