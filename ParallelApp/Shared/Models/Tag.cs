using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Messagetags = new HashSet<Messagetag>();
            Usertags = new HashSet<Usertag>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int SchoolId { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Created { get; set; }
        public string Color { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual ICollection<Messagetag> Messagetags { get; set; }
        public virtual ICollection<Usertag> Usertags { get; set; }

        public string GetColor()
        {
            if (Color is null) { return "#7B37FF"; }
            else { return Color; }
        }
    }
}
