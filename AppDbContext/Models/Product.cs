using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AppDbContext.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductCategoryValue = new HashSet<ProductCategoryValue>();
            ProductOrder = new HashSet<ProductOrder>();
            ProductSpecificationValue = new HashSet<ProductSpecificationValue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int CategoryId { get; set; }
        public string ImageLink { get; set; }
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile Imagefile { get; set; } 

        public virtual Category Category { get; set; }
        public virtual ICollection<ProductCategoryValue> ProductCategoryValue { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<ProductSpecificationValue> ProductSpecificationValue { get; set; }
    }
}
