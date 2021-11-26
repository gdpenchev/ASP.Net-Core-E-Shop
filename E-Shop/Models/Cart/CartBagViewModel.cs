using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Cart
{
    public class CartBagViewModel
    {
        public List<CartViewModel> CartItems { get; set; }

        public decimal Total { get; set; }
    }
}
