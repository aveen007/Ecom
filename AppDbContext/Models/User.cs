using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class User
    {
        public User()
        {
            Notification = new HashSet<Notification>();
            Order = new HashSet<Order>();
            UserRating = new HashSet<UserRating>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<UserRating> UserRating { get; set; }
    }
}
