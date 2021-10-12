namespace E_Shop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static E_Shop.Data.DataConstants.Shirt;
    public class MasterShirt
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxNameModelLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
        [Required]
        public string ImageURL { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Shirt> Shirts { get; init; } = new List<Shirt>();
    }
}
