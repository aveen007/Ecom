using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Order
    {
        public Order()
        {
            ProductOrder = new HashSet<ProductOrder>();
            Shipping = new HashSet<Shipping>();
        }

        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int CustomerId { get; set; }
        public int ShippingId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
