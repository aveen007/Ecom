using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class ProductSpecification
    {
        public ProductSpecification()
        {
            ProductSpecificationValue = new HashSet<ProductSpecificationValue>();
        }

        public int Id { get; set; }
        public string Specification { get; set; }

        public virtual ICollection<ProductSpecificationValue> ProductSpecificationValue { get; set; }
    }
}
