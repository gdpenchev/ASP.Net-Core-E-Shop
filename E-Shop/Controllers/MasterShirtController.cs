namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.MasterShirt;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class MasterShirtController : Controller
    {
        private readonly EShopDbContext data;

        public MasterShirtController(EShopDbContext data)
        {
            this.data = data;
        }
        public IActionResult Add() => View(new AddMasterShirtFormModel
        {
            Categories = this.GetShirtCategories()
        });
        [HttpPost]
        public IActionResult Add(AddMasterShirtFormModel masterShirt)
        {
            if (!this.data.Categories.Any(c=>c.Id == masterShirt.CategoryId))
            {
                this.ModelState.AddModelError(nameof(masterShirt.CategoryId), "does not exists");
            }
            
            if (this.data.MasterShirts.Any(ms => ms.Name == masterShirt.Name))
            {
                this.ModelState.AddModelError(nameof(masterShirt.Name), "already exists");
            }
            if (!this.ModelState.IsValid)
            {
                return View(masterShirt);
            }

            var newMasterShirt = new MasterShirt
            {
                Name = masterShirt.Name,
                Description = masterShirt.Description,
                ImageURL = masterShirt.ImageURL,
                CategoryId = masterShirt.CategoryId
            };
            this.data.MasterShirts.Add(newMasterShirt);
            this.data.SaveChanges();
            return RedirectToAction("Add", "Shirts");
        }

        private IEnumerable<MasterShirtCategoryViewModel> GetShirtCategories()
           => this.data
           .Categories
           .Select(c => new MasterShirtCategoryViewModel
           {
               Id = c.Id,
               Name = c.Name
           }).ToList();
    }
    
}
