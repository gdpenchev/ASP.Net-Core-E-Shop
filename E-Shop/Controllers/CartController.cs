namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.Cart;
    using E_Shop.Models.MasterShirt;
    using E_Shop.Services.MasterShirt;
    using E_Shop.Services.SessionHelper;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
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

            var bagInfo = new CartBagViewModel
            {
                CartItems = cart,
                Total = cart.Sum(item => item.Price * item.Quantity)
            };

            return View(bagInfo);
        }
        public IActionResult Buy(MasterShirtDetailsServiceModel model, int id)
        {

            //var choosedItem = TempData["cart"];

            //var model = JsonConvert.DeserializeObject<MasterShirtToCartModel>(choosedItem.ToString());
            if (SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<CartViewModel> cart = new List<CartViewModel>();
                var currShirt = this.data.Shirts.Where(s => s.MasterShirtId == model.Id).Where(s => s.Size == model.Size).FirstOrDefault();
                var currMasterShirt = this.data.MasterShirts.Where(ms => ms.Id == model.Id).FirstOrDefault();
                var categoryName = this.data.Categories.Where(c => c.Id == currMasterShirt.CategoryId).FirstOrDefault();

                var shirToCart = new CartViewModel
                {
                    Id = currShirt.Id,
                    Name = currMasterShirt.Name,
                    Category = categoryName.Name,
                    ImageUrl = currMasterShirt.ImageURL,
                    Price = currShirt.Price,
                    Size = currShirt.Size,
                    Quantity = model.Quantity
                };
                cart.Add(shirToCart);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartViewModel> cart = SessionHelper.GetObjectFromJson<List<CartViewModel>>(HttpContext.Session, "cart");
                int index = isExist(model.Id);

                if (index != -1)
                {
                    cart[index].Quantity++;

                }
                else
                {
                    var currShirt = data.Shirts.Where(s => s.Id == model.Id).FirstOrDefault();
                    var currMasterShirt = data.MasterShirts.Where(ms => ms.Id == currShirt.MasterShirtId).FirstOrDefault();
                    var categoryName = this.data.Categories.Where(c => c.Id == currMasterShirt.CategoryId).FirstOrDefault();

                    var shirToCart = new CartViewModel
                    {
                        Id = currShirt.Id,
                        Name = currMasterShirt.Name,
                        Category = categoryName.Name,
                        ImageUrl = currMasterShirt.ImageURL,
                        Price = currShirt.Price,
                        Size = currShirt.Size,
                        Quantity = model.Quantity
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

            for (int i = 0; i < cart.Count; i  ++)
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
