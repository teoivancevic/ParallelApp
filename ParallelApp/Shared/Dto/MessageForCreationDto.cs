using ParallelApp.Shared.Models;
using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Dto
{
    public partial class MessageForCreationDto
    {
        public MessageForCreationDto()
        {

        }

        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int SchoolId { get; set; }
        public int SenderUserId { get; set; }
        public int Likes { get; set; }


        //public List<Tag> Tags { get; set; }
    }
}
