using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class UserRating
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductOrderId { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }

        public virtual ProductOrder ProductOrder { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
