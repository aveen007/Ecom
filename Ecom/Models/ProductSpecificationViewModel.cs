using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class ProductSpecificationViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Product Specification")]
        public string Specification { get; set; }

    }
}
