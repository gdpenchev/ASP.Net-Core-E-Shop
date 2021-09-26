namespace E_Shop.Models.Shirts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddShirtFormModel
    {
        [Required]
        [MaxLength(MaxNameModelLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(MaxNameModelLength)]
        public string Model { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Size { get; set; }
        [Display(Name = "Image URL")]
        [Required]
        [Url]
        public string ImageUrl { get; init; }
        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<ShirtCategoryViewModel> Categories { get; set; } = new List<ShirtCategoryViewModel>();
    }
}
