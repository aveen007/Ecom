using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_Context.Models
{
    public partial class Value
    {
        public Value()
        {
            CategoryValue = new HashSet<CategoryValue>();
            ProductValue = new HashSet<ProductValue>();
        }

        public int Id { get; set; }
        public string Value1 { get; set; }
        public int AttributeId { get; set; }

        public virtual ICollection<CategoryValue> CategoryValue { get; set; }
        public virtual ICollection<ProductValue> ProductValue { get; set; }
    }
}
