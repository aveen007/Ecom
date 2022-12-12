﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string Sku { get; set; }

        public string ImageLink { get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile Imagefile { get; set; }
    }
}
