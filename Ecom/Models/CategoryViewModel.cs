using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
/*using DynamicVML;*/
namespace Ecom.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string Description { get; set; }

        public virtual ICollection<CategorySpecification> CategorySpecification { get; set; }
        /*       [Display(Name = "Authored books")]
               public virtual DynamicList<SpecificationViewModel> Specs { get; set; } = new DynamicList<SpecificationViewModel>();
           */
    }
}
