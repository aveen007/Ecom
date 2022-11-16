using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Address
    {
        public Address()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
