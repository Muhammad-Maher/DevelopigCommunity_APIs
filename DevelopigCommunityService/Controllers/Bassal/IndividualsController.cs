using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopigCommunityService.Context;
using DevelopigCommunityService.Models.Bassal;
using DevelopigCommunityService.DTOs.Bassal;
using System.Security.Cryptography;
using System.Text;
using DevelopigCommunityService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace DevelopigCommunityService.Controllers.Bassal
{
    public class IndividualsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public IndividualsController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/Individuals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Individual>>> GetIndividuals()
        {
            return await _context.Individuals.ToListAsync();
        }

        // GET: api/Individuals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Individual>> GetIndividual(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);

            if (individual == null)
            {
                return NotFound();
            }

            return individual;
        }

        // PUT: api/Individuals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIndividual(int id, Individual individual)
        {
            if (id != individual.Id)
            {
                return BadRequest();
            }

            _context.Entry(individual).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IndividualExists(id))
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

        // POST: api/Individuals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<Individual>> Register(IndividualRegisterDTOs IndividualRegister)
        {

            if (await IndividualExists(IndividualRegister.UserName.ToLower())) return BadRequest("Username already exists");

            using var hmac = new HMACSHA512();

            var newIndividual = new Individual
            {
                UserName = IndividualRegister.UserName.ToLower(),
                FirstName = IndividualRegister.FirstName,
                LastName = IndividualRegister.LastName,
                Age = IndividualRegister.Age,
                Email = IndividualRegister.Email,
                Phone = IndividualRegister.Phone,
                DepartmentId = IndividualRegister.DepartId,
                PasswordHash = hmac.ComputeHash(Encoding.UTF32.GetBytes(IndividualRegister.Password)),
                PasswordSalt = hmac.Key
            };

            await _context.Individuals.AddAsync(newIndividual);

            await _context.SaveChangesAsync();

            return Ok("Created successfully");

            //_context.Individuals.Add(individual);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetIndividual", new { id = individual.Id }, individual);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<IndividualDTOs>> Login(IndividualLoginDTO IndividualLogin)
        {

            var user = await _context.Individuals
                .SingleOrDefaultAsync(ww => ww.UserName == IndividualLogin.UserName.ToLower());

            if (user == null) return Unauthorized("Username or password is invalid");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF32.GetBytes(IndividualLogin.Password));

            for (int i=0;i<ComputeHash.Length;i++)
            {
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new IndividualDTOs 
            { 
                UserName=user.UserName,
              Token=_tokenService.CreateToken(user)
            };
        }


        // DELETE: api/Individuals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndividual(int id)
        {
            var individual = await _context.Individuals.FindAsync(id);
            if (individual == null)
            {
                return NotFound();
            }

            _context.Individuals.Remove(individual);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IndividualExists(int id)
        {
            return _context.Individuals.Any(e => e.Id == id);
        }

        private async Task<bool> IndividualExists(String UserNameRegistered)
        {

            return await _context.Individuals.AnyAsync(e => e.UserName == UserNameRegistered);
        }

    }
}
