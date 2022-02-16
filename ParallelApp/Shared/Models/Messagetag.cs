using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Messagetag
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public int TagId { get; set; }
        public DateTime Created { get; set; }

        public virtual Message Message { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
