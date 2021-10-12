namespace E_Shop.Models.MasterShirt
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static E_Shop.Data.DataConstants.Shirt;
    public class AddMasterShirtFormModel
    {
        [Required]
        [MaxLength(MaxNameModelLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Image URL")]
        [Url]
        public string ImageURL { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<MasterShirtCategoryViewModel> Categories { get; set; } = new List<MasterShirtCategoryViewModel>();
    }
}
