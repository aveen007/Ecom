using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class ContactViewModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
      

        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
