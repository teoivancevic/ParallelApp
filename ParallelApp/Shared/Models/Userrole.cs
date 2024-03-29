﻿using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Userrole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime Created { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
