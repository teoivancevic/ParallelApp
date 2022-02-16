using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Role
    {
        public Role()
        {
            Userroles = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }

        public virtual ICollection<Userrole> Userroles { get; set; }
    }
}
