using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

    }
}
