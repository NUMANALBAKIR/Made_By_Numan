﻿using API.Data;
using API.Models.StudentCRUD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        private List<Student> students = new()
            {
                new Student()
                {
                    StudentId= 1,
                    Name= "Sam",
                    DateOfBirth= DateTime.Now,
                    Age= 23,
                    Pass= "Passed"
                },
                new Student()
                {
                    StudentId= 2,
                    Name= "Iram",
                    DateOfBirth= DateTime.Now,
                    Age= 24,
                    Pass= "failed"
                },
                new Student()
                {
                    StudentId= 3,
                    Name= "Zina",
                    DateOfBirth= DateTime.Now,
                    Age= 25,
                    Pass= "passed"
                }
            };


        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {

            return students;

            //return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            //var student = await _context.Students.FindAsync(id);
            var student = students.Find(x => x.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // not implemented
        // PUT: api/Students/5
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


        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            //_context.Students.Add(student);
            students.Add(student);

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


        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            //var student = await _context.Students.FindAsync(id);
            var student = students.Find(x => x.StudentId == id);


            if (student == null)
            {
                return NotFound();
            }

            //_context.Students.Remove(student);
            await _context.SaveChangesAsync();

            students.Remove(student);


            return NoContent();
        }


        private bool StudentExists(int id)
        {
            //return _context.Students.Any(e => e.StudentId == id);
            return students.Any(e => e.StudentId == id);

        }
    }
}