using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Cart
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public int MasterShirtId { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }

        public string Size { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
