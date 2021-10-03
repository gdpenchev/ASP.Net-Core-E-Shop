namespace E_Shop.Models.Shirts
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class AddShirtFormModel
    {
        [Required]
        [Range(minRange,maxRange,ErrorMessage = "The range must be between {1} and {2}")]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Size { get; set; }

        public int MasterShirtId { get; init; }

        public IEnumerable<ShirtMasterShirtViewModel> MasterShirts { get; set; } = new List<ShirtMasterShirtViewModel>();
    }
}
