using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Models
{
    public class CategorySpecificationViewModel
    {
        public CategoryViewModel Category { get; set; }
        public List<SpecificationViewModel> Specifications { get; set; }

        public CategorySpecificationViewModel(CategoryViewModel Category, List<SpecificationViewModel> Specifications)
        {
            this.Category = Category;
            this.Specifications = Specifications;
        }

    }
}
