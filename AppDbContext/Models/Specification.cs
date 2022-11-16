using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Specification
    {
        public Specification()
        {
            CategorySpecification = new HashSet<CategorySpecification>();
        }

        public int Id { get; set; }
        public string Specification1 { get; set; }
        public int ValueType { get; set; }

        public virtual ICollection<CategorySpecification> CategorySpecification { get; set; }
    }
}
