using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Usertag
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TagId { get; set; }
        public DateTime Created { get; set; }

        public virtual Tag Tag { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
