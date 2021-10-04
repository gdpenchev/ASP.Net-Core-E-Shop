namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.Shirts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class ShirtsController : Controller
    {
        private readonly EShopDbContext data;

        public ShirtsController(EShopDbContext data)
        {
            this.data = data;
        }

        

       
        public IActionResult Add() => View(new AddShirtFormModel
        {
            MasterShirts = this.GetMasterShirts()
        });

        [HttpPost]
        public IActionResult Add(AddShirtFormModel shirt)
        {

            if (!this.data.MasterShirts.Any(ms=>ms.Id == shirt.MasterShirtId))
            {
                this.ModelState.AddModelError(nameof(shirt.MasterShirtId), "item does not exist.");
            }
            if (shirt.Quantity <= 0)
            {
                this.ModelState.AddModelError(nameof(shirt.MasterShirtId), "quantity must be at least 1");
            }
            if (!ModelState.IsValid)
            {
                shirt.MasterShirts = this.GetMasterShirts();
                return View(shirt);
            }
            var newShirt = new Shirt
            {
                Price = shirt.Price,
                Quantity = shirt.Quantity,
                Size = shirt.Size,
                MasterShirtId = shirt.MasterShirtId,
            };
            data.Shirts.Add(newShirt);
            data.SaveChanges();
            return RedirectToAction("All", "MasterShirt");
        }
        private IEnumerable<ShirtCategoryViewModel> GetShirtCategories()
            => this.data
            .Categories
            .Select(c => new ShirtCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        private IEnumerable<ShirtMasterShirtViewModel> GetMasterShirts()
            => this.data
            .MasterShirts
            .Select(ms => new ShirtMasterShirtViewModel
            {
                Id = ms.Id,
                Name = ms.Name
            }).ToList();
    }
}
