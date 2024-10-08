﻿using API_Angular.Data;
using API_Angular.Models.StudentCRUD;
using API_Angular.Models.StudentCRUDDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Angular.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SubjectsAPIController : ControllerBase, ISubjectsAPIController
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;


    public SubjectsAPIController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    // GET: api/SubjectsAPI
    // Get All subjects of this StudentId
    [HttpGet]
    public async Task<List<SubjectDTO>> GetSubjects(int StudentId)
    {
        var subjects = await _context.Subjects
        .Where(x => x.StudentId == StudentId)
        .ToListAsync();

        var list = _mapper.Map<List<SubjectDTO>>(subjects);

        return list;
    }


    // GET: api/SubjectsAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDTO>> GetSubject(int subjectId)
    {
        Subject subject = await _context.Subjects.FindAsync(subjectId);

        SubjectDTO subjectDto = _mapper.Map<SubjectDTO>(subject);

        if (subject == null)
        {
            return NotFound();
        }
        return subjectDto;
    }


    // PUT: api/SubjectsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubject(int id, SubjectUpdateDTO updateDto)
    {
        Subject subject = _mapper.Map<Subject>(updateDto);

        if (id != subject.SubjectId)
        {
            return BadRequest();
        }

        _context.Entry(subject).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectExists(id))
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


    // POST: api/SubjectsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SubjectDTO>> PostSubject(SubjectCreateDTO createDto)
    {

        Subject subject = _mapper.Map<Subject>(createDto);
        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSubject), new { id = subject.SubjectId }, subject);
    }


    // DELETE: api/SubjectsAPI/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int subjectId)
    {
        var subject = await _context.Subjects.FindAsync(subjectId);
        if (subject == null)
        {
            return NotFound();
        }

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    private bool SubjectExists(int id)
    {
        return _context.Subjects.Any(e => e.SubjectId == id);
    }
}
