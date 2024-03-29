﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class CategorySpecification
    {
        public CategorySpecification()
        {
            ProductCategoryValue = new HashSet<ProductCategoryValue>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int SpecificationId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Specification Specification { get; set; }
        public virtual ICollection<ProductCategoryValue> ProductCategoryValue { get; set; }
    }
}
