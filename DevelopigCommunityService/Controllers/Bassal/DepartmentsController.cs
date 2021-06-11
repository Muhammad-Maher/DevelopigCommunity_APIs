﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevelopigCommunityService.Context;
using DevelopigCommunityService.Models.Aya;
using Microsoft.AspNetCore.Authorization;
using DevelopigCommunityService.Interfaces;

namespace DevelopigCommunityService.Controllers.Bassal
{
    public class DepartmentsController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public DepartmentsController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {

            String authHeaders = Request.Headers["Authorization"].FirstOrDefault();

            var authUser=_tokenService.GetJWTClams(authHeaders);

            if (authUser.IsAdmin == false) return Unauthorized("Only Admin can Update Department");

            if (id != department.Id)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {


            String authHeaders = Request.Headers["Authorization"].FirstOrDefault();

            var authUser = _tokenService.GetJWTClams(authHeaders);

            if (authUser.IsAdmin == false) return Unauthorized("Only Admin can add new Departments");


            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartment", new { id = department.Id }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {

            String authHeaders = Request.Headers["Authorization"].FirstOrDefault();

            var authUser = _tokenService.GetJWTClams(authHeaders);

            if (authUser.IsAdmin == false) return Unauthorized("Only Admin can add new Departments");



            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(int id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
