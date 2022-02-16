﻿using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Userlike
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }

        public virtual Message Message { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
