using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TypeId { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public virtual NotificationType Type { get; set; }
        public virtual User User { get; set; }
    }
}
