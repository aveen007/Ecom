using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class ValueType
    {
        public ValueType()
        {
            Specification = new HashSet<Specification>();
            ProductSpecification = new HashSet<ProductSpecification>();
        }

        public int Id { get; set; }
        public string ValueName { get; set; }

        public virtual ICollection<Specification> Specification { get; set; }
        public virtual ICollection<ProductSpecification> ProductSpecification { get; set; }
    }
}
