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

//namespace API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentsController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        private readonly IMapper _mapper;

//        public StudentsController(AppDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        // GET: api/Students
//        [HttpGet]
//        public async Task<ActionResult<List<StudentDTO>>> GetStudents()
//        {
//            var s = await _context.Students.ToListAsync();
//            return _mapper.Map<List<StudentDTO>>(s);
//        }

//        // GET: api/Students/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
//        {
//            var student = await _context.Students.FindAsync(id);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return _mapper.Map<StudentDTO>(student);
//        }

//        // PUT: api/Students/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutStudent(int id, StudentUpdateDTO student)
//        {
//            if (id != student.StudentId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(student).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!StudentExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Students
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Student>> PostStudent(StudentCreateDTO student)
//        {
//            var s = _mapper.Map<Student>(student);
//            _context.Students.Add(s);
//            await _context.SaveChangesAsync();

//            s= _context.Students.Where(s=>s.)

//                return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
//        }

//        // DELETE: api/Students/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteStudent(int id)
//        {
//            var student = await _context.Students.FindAsync(id);
//            if (student == null)
//            {
//                return NotFound();
//            }

//            _context.Students.Remove(student);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool StudentExists(int id)
//        {
//            return _context.Students.Any(e => e.StudentId == id);
//        }
//    }
//}
