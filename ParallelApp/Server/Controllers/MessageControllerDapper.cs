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
    [Route("api/message")]
    [ApiController]
    public class MessageControllerDapper : ControllerBase
    {
        private readonly IMessageRepository _messageRepo;
        public MessageControllerDapper(IMessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
        }

        [HttpGet("getuserfeed/{user_id}")]
        public async Task<IActionResult> GetUserFeed(int user_id)
        {
            try
            {
                var messages = await _messageRepo.GetUserFeed(user_id);
                if (messages == null)
                    return NotFound();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("createmessage")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageForCreationDto message)
        {
            try
            {
                await _messageRepo.CreateMessage(message);
                return Ok();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("addmessagetags")]
        public async Task<IActionResult> AddMessageTags([FromBody] List<int> tagIds)
        {
            try
            {
                await _messageRepo.AddMessageTags(tagIds);
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

