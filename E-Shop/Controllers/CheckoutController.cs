using E_Shop.Data;
using E_Shop.Data.Models;
using E_Shop.Models.Cart;
using E_Shop.Services.SessionHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly EShopDbContext data;

        public CheckoutController(EShopDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Price * item.Quantity);

            var name = this.User.Identity.Name;

            var user = this.data.Users.Where(u => u.Email == name).FirstOrDefault();

            var order = new Order();

            order.UserId = user.Id;

            this.data.Orders.Add(order);
            this.data.SaveChanges();
            var currOrder = this.data.Orders.Where(o => o.UserId == user.Id).OrderBy(o => o.Id).Last();

            foreach (var item in cart)
            {
                var cartItem = new Cart
                {
                    CurrentItemId = item.Id,
                    Name = item.Name,
                    Category = item.Category,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Size = item.Size,
                    OrderId = currOrder.Id
                };

                this.data.Carts.Add(cartItem);
            }

            this.data.SaveChanges();

            return View();
        }

        public IActionResult Confirm()
        {

            List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");

            cart.Clear();

            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("All", "MasterShirt");
        }
    }
}
