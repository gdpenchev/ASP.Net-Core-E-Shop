namespace E_Shop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;
    public class Shirt
    {
        public int Id { get; init; }
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
        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
