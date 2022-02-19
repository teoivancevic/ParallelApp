using ParallelApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelApp.Shared.Dto
{
    public partial class SchoolForCreationDto
    {
        public SchoolForCreationDto()
        {
            Messages = new HashSet<Message>();
            Tags = new HashSet<Tag>();
            Users = new HashSet<User>();
        }

        public string ShortName { get; set; } = null!;
        public string LongName { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? PostalCode { get; set; }
        public string? Country { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
