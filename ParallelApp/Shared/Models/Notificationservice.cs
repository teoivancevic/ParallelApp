using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Notificationservice
    {
        public Notificationservice()
        {
            Usernotificationservices = new HashSet<Usernotificationservice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Enabled { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Usernotificationservice> Usernotificationservices { get; set; }
    }
}
