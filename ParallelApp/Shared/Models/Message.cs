using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Message
    {
        public Message()
        {
            Messagetags = new HashSet<Messagetag>();
            Userlikes = new HashSet<Userlike>();
        }

        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int SchoolId { get; set; }
        public int SenderUserId { get; set; }
        public int Likes { get; set; }
        public DateTime Created { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual User SenderUser { get; set; } = null!;
        public virtual ICollection<Messagetag> Messagetags { get; set; }
        public virtual ICollection<Userlike> Userlikes { get; set; }
    }
}
