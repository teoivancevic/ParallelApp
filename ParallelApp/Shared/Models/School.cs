using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class School
    {
        public School()
        {
            Messages = new HashSet<Message>();
            Tags = new HashSet<Tag>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; } = null!;
        public string LongName { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? PostalCode { get; set; }
        public string? Country { get; set; }
        public int Enabled { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
