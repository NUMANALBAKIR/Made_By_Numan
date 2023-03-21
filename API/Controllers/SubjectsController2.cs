//using API.Data;
//using API.Models.StudentCRUD;
//using API.Models.StudentCRUDDTOs;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

/*
    This Controller is of project 'Client-Angular', which is now under development.
*/

//namespace API.Controllers;

//[Route("api/[controller]")]
//[ApiController]
//public class SubjectsController : ControllerBase
//{
//    private readonly AppDbContext _context;
//    private readonly IMapper _mapper;

//    public SubjectsController(AppDbContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    // GET: api/Subjects
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<SubjectDTO>>> GetSubjects()
//    {
//        return await _context.Subjects.ToListAsync();
//    }

//    // GET: api/Subjects/5
//    [HttpGet("{id}")]
//    public async Task<ActionResult<Subject>> GetSubject(int id)
//    {
//        var subject = await _context.Subjects.FindAsync(id);

//        if (subject == null)
//        {
//            return NotFound();
//        }

//        return subject;
//    }

//    // PUT: api/Subjects/5
//    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//    [HttpPut("{id}")]
//    public async Task<IActionResult> PutSubject(int id, SubjectCreateDTO subject)
//    {
//        if (id != subject.SubjectId)
//        {
//            return BadRequest();
//        }

//        _context.Entry(subject).State = EntityState.Modified;

//        try
//        {
//            await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//            if (!SubjectExists(id))
//            {
//                return NotFound();
//            }
//            else
//            {
//                throw;
//            }
//        }

//        return NoContent();
//    }

//    // POST: api/Subjects
//    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//    [HttpPost]
//    public async Task<ActionResult<Subject>> PostSubject(SubjectUpdateDTO subject)
//    {
//        _context.Subjects.Add(subject);
//        await _context.SaveChangesAsync();

//        return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
//    }

//    // DELETE: api/Subjects/5
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteSubject(int id)
//    {
//        var subject = await _context.Subjects.FindAsync(id);
//        if (subject == null)
//        {
//            return NotFound();
//        }

//        _context.Subjects.Remove(subject);
//        await _context.SaveChangesAsync();

//        return NoContent();
//    }

//    private bool SubjectExists(int id)
//    {
//        return _context.Subjects.Any(e => e.SubjectId == id);
//    }
//}
