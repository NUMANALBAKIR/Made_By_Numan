using API_Angular.Data;
using API_Angular.Models.StudentCRUD;
using API_Angular.Models.StudentCRUDDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Angular.Controllers;

/*
    Note: This Controller is for 'Client-Angular', which is now under development.
*/

[Route("api/[controller]")]
[ApiController]
public class StudentsAPIController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISubjectsAPIController _subjectsAPIController;

    public StudentsAPIController(AppDbContext context, IMapper mapper, ISubjectsAPIController subjectsAPIController)
    {
        _mapper = mapper;
        _context = context;
        _subjectsAPIController = subjectsAPIController;
    }


    [HttpGet]
    [Route("{searchBy}/{searchText}")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Search(string searchBy, string searchText)
    {
        List<Student> students = null;
        searchText = searchText.ToLower();
        searchBy = searchBy.ToLower();

        if (searchBy == "Name".ToLower())
        {
            students = _context.Students
            .Where(x => x.Name.ToLower().Contains(searchText))
            .ToList();
        }      
        else if (searchBy == "gender".ToLower())
        {
           students = _context.Students
           .Include(x=> x.Country)
           .Where(x => x.Gender.ToString().Contains(searchText))
           .ToList();
        }
        else if (searchBy == "passed".ToLower())
        {
            students = _context.Students
            .Where(x => x.Passed.ToString().Contains(searchText))
            .ToList();
        }






        var list = students.Select(x => _mapper.Map<StudentDTO>(x)).ToList();

        return Ok(list);
    }

    // GET: api/StudentsAPI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
    {
        var students = await _context.Students
            .Include(x => x.Country)
            .OrderBy(x => x.Name)  // notice
            .ToListAsync();

        var list = students.Select(x => _mapper.Map<StudentDTO>(x)).ToList();

        return list;
    }


    // student + its subjects
    // GET: api/StudentsAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentDTO>> GetStudent(int id)
    {
        var student = await _context.Students
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.StudentId == id);

        if (student == null)
        {
            return NotFound(student);
        }

        var studentDto = _mapper.Map<StudentDTO>(student);

        // get, set subjects list
        var subjects = await _subjectsAPIController.GetSubjects(id);
        studentDto.Subjects = subjects;

        return studentDto;
    }


    // POST: api/StudentsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(StudentCreateDTO createDto)
    {
        foreach (var subjectCreateDto in createDto.Subjects)
        {
            await _subjectsAPIController.PostSubject(subjectCreateDto);
        }

        Student student = _mapper.Map<Student>(createDto);
        _context.Students.Add(student);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (StudentExists(student.StudentId))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }
        // student.DateOfBirth.ToString("dd/MM/yyyy");
        return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
    }


    // PUT: api/StudentsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, StudentUpdateDTO updateDto)
    {

        foreach (var subjectUpdateDto in updateDto.Subjects)
        {
            try
            {
                await _subjectsAPIController.PutSubject(subjectUpdateDto.SubjectId, subjectUpdateDto);
            }
            catch (NullReferenceException)
            {
                var createDto = _mapper.Map<SubjectCreateDTO>(subjectUpdateDto);
                await _subjectsAPIController.PostSubject(createDto);
            }
        }

        Student student = _mapper.Map<Student>(updateDto);

        if (id != student.StudentId)
        {
            return BadRequest();
        }

        _context.Entry(student).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }


    // DELETE: api/StudentsAPI/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return NotFound();
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.StudentId == id);
    }
}
