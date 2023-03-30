using API_Angular.Data;
using API_Angular.Models.StudentCRUD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Angular.Controllers;

/*
    Note: This Controller is for 'Client-Angular', which is now under development.
*/

[Route("api/[controller]")]
[ApiController]
public class SubjectsListsAPIController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubjectsListsAPIController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/SubjectsListsAPI
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubjectsList>>> GetSubjectsLists()
    {
        return await _context.SubjectsLists.ToListAsync();
    }

    // GET: api/SubjectsListsAPI/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectsList>> GetSubjectsList(int id)
    {
        var subjectsList = await _context.SubjectsLists.FindAsync(id);

        if (subjectsList == null)
        {
            return NotFound();
        }

        return subjectsList;
    }

    // PUT: api/SubjectsListsAPI/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubjectsList(int id, SubjectsList subjectsList)
    {
        if (id != subjectsList.SubjectsListId)
        {
            return BadRequest();
        }

        _context.Entry(subjectsList).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SubjectsListExists(id))
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

    // POST: api/SubjectsListsAPI
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<SubjectsList>> PostSubjectsList(SubjectsList subjectsList)
    {
        _context.SubjectsLists.Add(subjectsList);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSubjectsList", new { id = subjectsList.SubjectsListId }, subjectsList);
    }

    // DELETE: api/SubjectsListsAPI/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubjectsList(int id)
    {
        var subjectsList = await _context.SubjectsLists.FindAsync(id);
        if (subjectsList == null)
        {
            return NotFound();
        }

        _context.SubjectsLists.Remove(subjectsList);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubjectsListExists(int id)
    {
        return _context.SubjectsLists.Any(e => e.SubjectsListId == id);
    }
}
