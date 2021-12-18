namespace E_Shop.Areas.Identity.Pages.Account.Controllers
{
    using E_Shop.Areas.Identity.Pages.Account.Models;
    using E_Shop.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    public class PreviousOrdersController : Controller
    {

        private readonly EShopDbContext data;

        public PreviousOrdersController(EShopDbContext data)
        {
            this.data = data;
        }

        [Authorize]
        public IActionResult GetOrders(int id)
        {

            var name = this.User.Identity.Name;

            var user = this.data.Users.Where(u => u.Email == name).FirstOrDefault();

            var orderList = this.data.Orders.Where(o => o.UserId == user.Id).OrderBy(o => o.Id).ToList();

            var orderModel = new ListOrderModel();
            int i = 1;
            foreach (var order in orderList)
            {

                var cartItems = this.data.Carts.Where(c => c.OrderId == order.Id).OrderBy(c => c.Id).ToList();

                var currOrder = new OrderModel();
                currOrder.orderId = i;  

                foreach (var item in cartItems)
                {
                    var cartModel = new CartModel
                    {
                        Name = item.Name,
                        Category = item.Category,
                        CurrentItemId = item.Id,
                        ImageUrl = item.ImageUrl,
                        OrderId = order.Id,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Size = item.Size
                    };

                    currOrder.CartItems.Add(cartModel);
                    
                }

                orderModel.orderModels.Add(currOrder);
                i++;
            }

            if (id == 0)
            {
                orderModel.pageNumber = 1;
            }
            else
            {
                orderModel.pageNumber = id;
            }
            return View(orderModel);
        }
    }
}
