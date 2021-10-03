namespace E_Shop.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Shirt
    {
        public int Id { get; init; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Size { get; set; }

        public int MasterShirtId { get; set; }

        public MasterShirt MasterShirt { get; set; }
    }
}
