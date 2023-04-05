﻿using API_Angular.Data;
using API_Angular.Models.StudentCRUD;
using API_Angular.Models.StudentCRUDDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
    private readonly SubjectsAPIController _subjectsAPIController;

    public StudentsAPIController(AppDbContext context, IMapper mapper, SubjectsAPIController subjectsAPIController)
    {
        _mapper = mapper;
        _subjectsAPIController = subjectsAPIController;
        _context = context;
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
            // .Include(x=> x.Subjects)
            .Where(x => x.Name.ToLower().Contains(searchText))
            .ToList();
        }
        else if (searchBy == "DateOfBirth".ToLower())
        {
            students = _context.Students
            // .Include(x=> x.Subjects)
            .Where(x => x.DateOfBirth.ToString().Contains(searchText))
            .ToList();
        }
        //else if (searchBy == "Passed".ToLower())
        //{
        //    students = _context.Students
        //    // .Include(x=> x.Subjects)
        //    .Where(x => x.Passed.ToString() == searchText))
        //    .ToList();
        //}
        return Ok(students);
    }


    // GET: api/StudentsAPI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        System.Threading.Thread.Sleep(2000);

        var students = await _context.Students
            .Include(x => x.Country)
            //.Include(x => x.SubjectsList)
            .ToListAsync();

        // foreach (var student in students)
        // {
        //     student.DateOfBirth.ToString("dd/MM/yyyy");
        // }
        return students;
    }


    // GET: api/StudentsAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students
             .Include(x => x.Country)
            //.Include(x => x.SubjectsList)
            .FirstOrDefaultAsync(x => x.StudentId == id);

        if (student == null)
        {
            return NotFound(student);
        }
        // student.DateOfBirth.ToString("dd/MM/yyyy");
        return student;
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
            await _subjectsAPIController.PutSubject(subjectUpdateDto.SubjectId, subjectUpdateDto);
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
