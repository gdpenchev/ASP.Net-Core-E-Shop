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

        public IActionResult All([FromQuery]AllShirtsModel query)
        {

            //var shirtsListQuery = this.data.Shirts.AsQueryable();

            //if (!string.IsNullOrWhiteSpace(query.Cateogory))
            //{
            //    shirtsListQuery = shirtsListQuery.Where(s => s.Category.Name == query.Cateogory);
            //}

            //if (!string.IsNullOrWhiteSpace(query.Size))
            //{
            //    shirtsListQuery = shirtsListQuery.Where(s => s.Size == query.Size);
            //}

            //if (!string.IsNullOrEmpty(query.SearchByText))
            //{
            //    shirtsListQuery = shirtsListQuery.Where(c =>
            //    (c.Name + " " + c.Model).ToLower().Contains(query.SearchByText.ToLower()) ||
            //    (c.Name + " " + c.Size).ToLower().Contains(query.SearchByText.ToLower()));
            //}

            //var shirts = shirtsListQuery
            //    .Skip((query.CurrentPage - 1) * AllShirtsModel.ShirtPerPage)
            //    .Take(AllShirtsModel.ShirtPerPage)
            //    .OrderByDescending(c=>c.Id)
            //    .Select(s => new ShirtListingViewModel
            //{
            //    Id = s.Id,
            //    Name = s.Name,
            //    Model = s.Model,
            //    ImageUrl = s.ImageUrl,
            //    Price = s.Price,
            //    Size = s.Size,
            //    Category = s.Category.Name
            //}).ToList();

            //var shirtSizes = this.data
            //    .Shirts
            //    .Select(s => s.Size)
            //    .OrderBy(s => s)
            //    .Distinct()
            //    .ToList();
            //var shirtCategories = this.data
            //    .Shirts
            //    .Select(s => s.Category.Name)
            //    .OrderBy(s => s)
            //    .Distinct()
            //    .ToList();

            //var totalShirts = shirtsListQuery.Count();

            //query.TotalShirts = totalShirts;
            //query.Sizes = shirtSizes;
            //query.Categories = shirtCategories;
            ////query.Shirts = shirts;

            return View(query);
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
                this.ModelState.AddModelError(nameof(shirt.MasterShirtId), "item does not exist.")
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
