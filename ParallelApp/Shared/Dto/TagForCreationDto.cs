using ParallelApp.Shared.Models;
using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Dto
{
    public partial class TagForCreationDto
    {
        public TagForCreationDto()
        {
            Messagetags = new HashSet<Messagetag>();
            Usertags = new HashSet<Usertag>();
        }

        public string Name { get; set; } = null!;
        public int SchoolId { get; set; }
        public string Type { get; set; } = null!;
        public string Color { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Messagetag> Messagetags { get; set; }
        public virtual ICollection<Usertag> Usertags { get; set; }

    }
}
