using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class PromotionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Promotion Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Discount Rate")]
        public decimal DiscountRate { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<CategoryPromotion> CategoryPromotion { get; set; }
        public virtual ICollection<CategoryPromotion> SelectedCategoryPromotion { get; set; }

    }
}
