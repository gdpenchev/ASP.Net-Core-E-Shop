namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
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

            var cart = SessionHelper.GetObjectFromJson<List<Shirt>>(HttpContext.Session, "cart");

            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Price * item.Quantity);
            return View();
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Shirt>>(HttpContext.Session,"cart") == null)
            {
                List<Shirt> cart = new List<Shirt>();
                var currShirt = data.Shirts.Where(s => s.Id == id).FirstOrDefault();
                cart.Add(currShirt);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Shirt> cart = SessionHelper.GetObjectFromJson<List<Shirt>>(HttpContext.Session, "cart");
                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].Quantity++;
                   
                }
                else
                {
                    var currShirt = data.Shirts.Where(s => s.Id == id).FirstOrDefault();
                    cart.Add(currShirt);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            List<Shirt> cart = SessionHelper.GetObjectFromJson<List<Shirt>>(HttpContext.Session, "cart");

            int index = isExist(id);

            cart.RemoveAt(id);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }
        private int isExist(int id)
        {
            List<Shirt> cart = SessionHelper.GetObjectFromJson<List<Shirt>>(HttpContext.Session, "cart");

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
