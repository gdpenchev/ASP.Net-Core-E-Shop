namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.Cart;
    using E_Shop.Services.SessionHelper;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class CartController : Controller
    {
        private readonly EShopDbContext data;

        public CartController(EShopDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {

            var cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Price * item.Quantity);
            return View();
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session,"cart") == null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                var currShirt = data.Shirts.Where(s => s.Id == id).FirstOrDefault();
                var currMasterShirt = data.MasterShirts.Where(ms => ms.Id == currShirt.MasterShirtId).FirstOrDefault();

                var shirToCart = new CartViewModel
                {
                    Id = currShirt.Id,
                    Name = currMasterShirt.Name,
                    Price = currShirt.Price,
                    Size = currShirt.Size,
                    Quantity = currShirt.Quantity
                };
                cart.Add(shirToCart);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].Quantity++;
                   
                }
                else
                {
                    var currShirt = data.Shirts.Where(s => s.Id == id).FirstOrDefault();
                    var currMasterShirt = data.MasterShirts.Where(ms => ms.Id == currShirt.MasterShirtId).FirstOrDefault();

                    var shirToCart = new CartViewModel
                    {
                        Id = currShirt.Id,
                        Name = currMasterShirt.Name,
                        Price = currShirt.Price,
                        Size = currShirt.Size,
                        Quantity = currShirt.Quantity
                    };
                    cart.Add(shirToCart);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");

            int index = isExist(id);

            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id.Equals(id))
                {
                    return i;
                }
            }
            return -1; 
        }
    }
}
