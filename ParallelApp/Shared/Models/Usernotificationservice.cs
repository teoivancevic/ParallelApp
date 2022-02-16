using System;
using System.Collections.Generic;

namespace ParallelApp.Shared.Models
{
    public partial class Usernotificationservice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NotificationServiceId { get; set; }
        public string Account { get; set; } = null!;
        public DateTime Created { get; set; }

        public virtual Notificationservice NotificationService { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
