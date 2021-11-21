namespace E_Shop.Models.Shirts
{
    using E_Shop.Services.Shirts.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants.Shirt;
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

        public IEnumerable<ShirtMasterShirtModel> MasterShirts { get; set; } = new List<ShirtMasterShirtModel>();
    }
}
