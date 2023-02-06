using AppDbContext.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class ProductOrderViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        [Display(Name = "Single Price")]
        public decimal SinglePrice { get; set; }

        public virtual Product Product { get; set; }

    }
}
