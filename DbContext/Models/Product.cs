using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DbContext.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductAttribute = new HashSet<ProductAttribute>();
            ProductOrder = new HashSet<ProductOrder>();
            ProductValue = new HashSet<ProductValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<ProductValue> ProductValue { get; set; }
    }
}
