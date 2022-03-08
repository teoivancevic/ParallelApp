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
//using ParallelApp.Shared.Dto;

namespace ParallelApp.Server.Controllers
{
    //[ApiController]
    //[Route("api/[controllerdapper]")]
    [ApiController]
    //[Route("Api/Userdapper")]
    [Route("api/user")]
    public class UserControllerDapper : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserControllerDapper(IUserRepository userRepo)

        {
            _userRepo = userRepo;
        }

        [HttpGet("getuserid/{auth0id}")]
        //[HttpGet]
        //[Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserId(string auth0id)
        {
            try
            {
                var user_id = await _userRepo.GetUserId(auth0id);
                if (user_id == null)
                    return NotFound();
                return Ok(user_id);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("getuserbyid/{id}")]
        //[HttpGet]
        //[Route("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                    return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepo.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getuserswithtags/{school_id}")]
        public async Task<IActionResult> GetUsersWithTags(int school_id)
        {
            try
            {
                var users = await _userRepo.GetUsersWithTags(school_id);
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getuserwithtags/{user_id}")]
        public async Task<IActionResult> GetUserWithTags(int user_id)
        {
            try
            {
                var user = await _userRepo.GetUserWithTags(user_id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getusersbyschoolid/{school_id}")]
        public async Task<IActionResult> GetUsersBySchoolId(int school_id)
        {
            try
            {
                var users = await _userRepo.GetUsersBySchoolId(school_id);
                return Ok(users);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteuserprofilepicurl/{id}")]
        public async Task<IActionResult> DeleteUserProfilePicUrl(int id)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                    return NotFound();
                await _userRepo.DeleteUserProfilePicUrl(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpPut("updateuserprofilepicurl/{id}/{url}")]
        public async Task<IActionResult> UpdateUserProfilePicUrl(int id, string url)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                    return NotFound();
                await _userRepo.UpdateUserProfilePicUrl(id, url);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getusertags/{id}")]
        public async Task<IActionResult> GetUserTags(int id)
        {
            try
            {
                var tags = await _userRepo.GetUserTags(id);
                if (tags == null)
                    return NotFound();
                return Ok(tags);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("removeusertag/{id}/{tag_id}")]
        public async Task<IActionResult> RemoveuserTag(int id, int tag_id) 
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                    return NotFound();
                await _userRepo.RemoveUserTag(id, tag_id);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("addusertag/{id}")]
        public async Task<IActionResult> AddUserTag(int id, [FromBody] int tag_id)
        {
            try
            {
                var user = await _userRepo.GetUserById(id);
                if (user == null)
                    return NotFound();
                await _userRepo.AddUserTag(id, tag_id);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

    }
}

