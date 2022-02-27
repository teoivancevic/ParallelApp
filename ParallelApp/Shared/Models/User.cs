using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            Userlikes = new HashSet<Userlike>();
            Usernotificationservices = new HashSet<Usernotificationservice>();
            Userroles = new HashSet<Userrole>();
            Usertags = new HashSet<Usertag>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public int SchoolId { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public int Enabled { get; set; }
        public string? HrEduUserPersistentId { get; set; }
        public DateTime Created { get; set; }
        public byte[]? ProfilePicture { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Userlike> Userlikes { get; set; }
        public virtual ICollection<Usernotificationservice> Usernotificationservices { get; set; }
        public virtual ICollection<Userrole> Userroles { get; set; }
        public virtual ICollection<Usertag> Usertags { get; set; }

        public List<Tag> Tags { get; set; }

        public string FullName()
        {
            return LastName + " " + FirstName;
        }
    }
}
