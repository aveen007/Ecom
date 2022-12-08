using System;

namespace Ecom.Models
{
    public class ProductSpecVM
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}