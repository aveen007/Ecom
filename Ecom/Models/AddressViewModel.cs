using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class AddressViewModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string Governorate { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string City { get; set; }

        [Required(ErrorMessage = "{0} must not be empty")]
        public string Region { get; set; }
    }
}
