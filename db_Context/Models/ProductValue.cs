﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace db_Context.Models
{
    public partial class ProductValue
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ValueId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Value Value { get; set; }
    }
}
