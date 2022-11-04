using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Attribute
    {
        public Attribute()
        {
            CategoryProduct = new HashSet<CategoryProduct>();
            ProductAttribute = new HashSet<ProductAttribute>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProduct { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
    }
}
