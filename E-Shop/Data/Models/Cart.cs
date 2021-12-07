using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Data.Models
{
    public class Cart
    {
        public int Id { get; init; }

        public int CurrentItemId { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
