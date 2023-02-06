using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class SpecificationViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Specification Name")]
        public string SpecificationName { get; set; }
        
        [Required(ErrorMessage = "{0} must not be empty")]
        public string Description { get; set; }

        public int ValueTypeId { get; set; }

        [Display(Name = "Value Type")]
        public virtual ValueTypeViewModel ValueType { get; set; }
    }
}