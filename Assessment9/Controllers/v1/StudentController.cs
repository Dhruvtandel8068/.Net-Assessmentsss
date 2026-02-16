using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assessment9.Data;
using Assessment9.DTOs;
using Assessment9.Models;
using AutoMapper;

namespace Assessment9.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<StudentDTO>>(students));
        }

        // GET: api/v1/student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            return Ok(_mapper.Map<StudentDTO>(student));
        }

        // POST: api/v1/student
        [HttpPost]
        public async Task<ActionResult<StudentDTO>> CreateStudent(StudentDTO studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<StudentDTO>(student);

            return CreatedAtAction(nameof(GetStudent),
                new { id = student.Id, version = "1.0" },
                resultDto);
        }

        // PUT: api/v1/student/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDto)
        {
            if (id != studentDto.Id)
                return BadRequest();

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _mapper.Map(studentDto, student);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/v1/student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return NotFound();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
