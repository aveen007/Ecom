using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DbContext.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public byte[] LastName { get; set; }
        public string Email { get; set; }
        public decimal? Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
