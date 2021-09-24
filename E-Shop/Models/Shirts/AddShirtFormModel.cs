using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Shop.Models.Shirts
{
    public class AddShirtFormModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ShirtCategoryViewModel> Categories { get; set; }
    }
}
