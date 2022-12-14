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
        public string SpecificationName { get; set; }
        public int ValueTypeId { get; set; }

        public virtual ValueType ValueType { get; set; }
        public virtual ICollection<ProductSpecificationValue> ProductSpecificationValue { get; set; }
    }
}
