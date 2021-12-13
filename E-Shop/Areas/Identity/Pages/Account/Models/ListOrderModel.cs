using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Areas.Identity.Pages.Account.Models
{
    public class ListOrderModel
    {
        public int pageNumber { get; set; }

        public List<OrderModel> orderModels { get; set; } = new List<OrderModel>();
    }
}
