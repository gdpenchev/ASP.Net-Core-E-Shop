namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Models.Shirts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections;
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
            Categories = this.GetShirtCategories()
        });

        [HttpPost]
        public IActionResult Add(AddShirtFormModel shirt)
        {
            return View();
        }
        private IEnumerable<ShirtCategoryViewModel> GetShirtCategories()
            => this.data
            .Categories
            .Select(c => new ShirtCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();


    }
}
