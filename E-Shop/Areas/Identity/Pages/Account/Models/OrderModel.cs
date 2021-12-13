using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Areas.Identity.Pages.Account.Models
{
    public class OrderModel
    {
        public int orderId { get; set; }

        public List<CartModel> CartItems { get; set; } = new List<CartModel>();
    }
}
