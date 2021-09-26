namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
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

        public IActionResult All(AllShirtsModel query)
        {

            var shirtsListQuery = this.data.Shirts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Model))
            {
                shirtsListQuery = shirtsListQuery.Where(s => s.Model == query.Model);
            }

            var shirts = shirtsListQuery.Select(s => new ShirtListingViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Model = s.Model,
                ImageUrl = s.ImageUrl,
                Price = s.Price,
                Size = s.Size,
                Category = s.Category.Name
            }).ToList();

            query.Shirts = shirts;
            return View(query);
        }

        public IActionResult Add() => View(new AddShirtFormModel
        {
            Categories = this.GetShirtCategories()
        });

        [HttpPost]
        public IActionResult Add(AddShirtFormModel shirt)
        {
            if (!this.data.Categories.Any(c=>c.Id == shirt.CategoryId))
            {
                this.ModelState.AddModelError(nameof(shirt.CategoryId), "Category does not exist.");
            }
            if (!ModelState.IsValid)
            {
                shirt.Categories = this.GetShirtCategories();
                return View(shirt);
            }

            var newShirt = new Shirt
            {
                Name = shirt.Name,
                Model = shirt.Model,
                Description = shirt.Description,
                Price = shirt.Price,
                ImageUrl = shirt.ImageUrl,
                Size = shirt.Size,
                CategoryId = shirt.CategoryId
            };

            data.Shirts.Add(newShirt);
            data.SaveChanges();
            return RedirectToAction("Index", "Home");
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
