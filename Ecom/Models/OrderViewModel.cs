using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "User Name")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Shipping Id")]
        public int ShippingId { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public bool IsOrdered { get; set; }

        public virtual ApplicationUserViewModel ApplicationUser { get; set; }

    }
}
