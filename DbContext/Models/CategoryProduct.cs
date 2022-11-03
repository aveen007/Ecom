using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace DbContext.Models
{
    public partial class CategoryProduct
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Attribute Product { get; set; }
    }
}
