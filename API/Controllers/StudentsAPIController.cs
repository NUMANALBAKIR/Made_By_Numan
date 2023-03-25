using API.Data;
using API.Models.StudentCRUD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    This Controller is for Student 'Client-Angular', which is now under development.
*/

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsAPIController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsAPIController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet]
    [Route("{searchBy}/{searchText}")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Search(string searchBy, string searchText)
    {
        List<Student> students = null;
        searchText = searchText.ToLower();

        if (searchBy == "StudentId")
        {
            students = _context.Students
            // .Include(x=> x.Subjects)
            .Where(x => x.StudentId.ToString().Contains(searchText))
            .ToList();
        }
        else if (searchBy == "Name")
        {
            students = _context.Students
            // .Include(x=> x.Subjects)
            .Where(x => x.Name.ToLower().Contains(searchText))
            .ToList();
        }
        else if (searchBy == "DateOfBirth")
        {
            students = _context.Students
            // .Include(x=> x.Subjects)
            .Where(x => x.DateOfBirth.ToString().Contains(searchText))
            .ToList();
        }
        else if (searchBy == "Pass")
        {
            students = _context.Students
            // .Include(x=> x.Subjects)
            .Where(x => x.Pass.ToLower().Contains(searchText))
            .ToList();
        }
        return Ok(students);
    }


    // GET: api/StudentsAPI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }


    // GET: api/StudentsAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        return student;
    }


    // PUT: api/StudentsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent(int id, Student student)
    {
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


    // POST: api/StudentsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(Student student)
    {
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

        return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
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
