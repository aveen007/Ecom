using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "SKU Number")]
        public string Sku { get; set; }

        public int CategoryId { get; set; }

    }
}
