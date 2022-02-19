using Microsoft.AspNetCore.Mvc;
using ParallelApp.Server.Models;
using ParallelApp.Shared;
using ParallelApp.Shared.Models;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Dapper;
using ParallelApp.Server.Contracts;
using ParallelApp.Shared.Dto;

namespace ParallelApp.Server.Controllers
{
    //[ApiController]
    //[Route("api/[controllerdapper]")]
    [Route("api/school")]
    [ApiController]
    public class SchoolControllerDapper : ControllerBase
    {
        private readonly ISchoolRepository _schoolRepo;
        public SchoolControllerDapper(ISchoolRepository schoolRepo)
        {
            _schoolRepo = schoolRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSchools()
        {
            try
            {
                var schools = await _schoolRepo.GetSchools();
                return Ok(schools);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name ="SchoolById")]
        public async Task<IActionResult> GetSchool(int id)
        {
            try
            {
                var school = await _schoolRepo.GetSchool(id);
                if (school == null)
                    return NotFound();
                return Ok(school);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolForCreationDto school)
        {
            try
            {
                //var createdSchool = await _schoolRepo.CreateSchool(school);
                //return CreatedAtRoute("SchoolById", new { id = createdSchool.Id }, createdSchool);
                await _schoolRepo.CreateSchool(school);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchool(int id, SchoolForUpdateDto school)
        {
            try
            {
                var dbSchool = await _schoolRepo.GetSchool(id);
                if (dbSchool == null)
                    return NotFound();
                await _schoolRepo.UpdateSchool(id, school);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(int id)
        {
            try
            {
                var dbCompany = await _schoolRepo.GetSchool(id);
                if (dbCompany == null)
                    return NotFound();
                await _schoolRepo.DeleteSchool(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getschooltags/{id}")]
        public async Task<IActionResult> GetSchoolTagsBySchoolId(int id)
        {
            try
            {
                var tags = await _schoolRepo.GetSchoolTagsBySchoolId(id);
                return Ok(tags);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}

