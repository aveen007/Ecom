using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class CategoryValue
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ValueId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Value Value { get; set; }
    }
}
