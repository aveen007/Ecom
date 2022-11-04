using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryProduct = new HashSet<CategoryProduct>();
            CategoryValue = new HashSet<CategoryValue>();
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProduct { get; set; }
        public virtual ICollection<CategoryValue> CategoryValue { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
