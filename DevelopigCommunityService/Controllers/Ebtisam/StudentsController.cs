using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopigCommunityService.Context;
using DevelopigCommunityService.Models.Ebtisam;
using DevelopigCommunityService.DTOs.Ebtisam;
using System.Security.Cryptography;
using System.Text;
using DevelopigCommunityService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DevelopigCommunityService.Controllers.Ebtisam
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        
        public StudentsController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/Students
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
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
        [HttpPost("Register")]
        public async Task<ActionResult<Student>> Register(StudentRegisterDTO StudentRegister)

        {
            if (await StudentExists(StudentRegister.UserName.ToLower())) return BadRequest("Username already exists");
            


            using var hmac = new HMACSHA512();
            var newstudent = new Student
            {

                UserName = StudentRegister.UserName.ToLower(),
                FirstName = StudentRegister.FirstName,
                
                Age = StudentRegister.Age,
                Email = StudentRegister.Email,
                Phone = StudentRegister.Phone,
                PasswordHash = hmac.ComputeHash(Encoding.UTF32.GetBytes(StudentRegister.Password)),
                PasswordSalt = hmac.Key
            };

            object p = await _context.Students.AddAsync(newstudent);

            int v = await _context.SaveChangesAsync();

            return Ok("Created successfully");
            //_context.Students.Add(student);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<StudentsDTO>> Login(StudentloginDTO studentlogin)
        {
            var user = await _context.Students
               .SingleOrDefaultAsync(ww => ww.UserName == studentlogin.UserName.ToLower());

            if (user == null) return Unauthorized("Username or password is invalid");

            using var hmac = new HMACSHA512(user.GetPasswordSalt());
            var ComputeHash = hmac.ComputeHash(Encoding.UTF32.GetBytes(studentlogin.Password));

            byte[] passwordHash = user.GetPasswordHash();
                

            for (int i = 0; i < ComputeHash.Length; i++)
            {
                if (ComputeHash[i] != passwordHash[i]) return Unauthorized("Invalid Password");
            }

            return new StudentsDTO
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

       



        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize]
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
        private async Task<bool> StudentExists(string UserNameRegistered)
        {
            return await _context.Students.AnyAsync(e => e.UserName == UserNameRegistered);
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }


    }
}
