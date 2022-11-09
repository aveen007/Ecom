using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class ProductSpecificationValueViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SpecificationId { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Specification Value")]
        public string Value { get; set; }

    }
}
