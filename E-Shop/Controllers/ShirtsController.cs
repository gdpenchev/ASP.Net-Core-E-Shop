namespace E_Shop.Controllers
{
    using E_Shop.Models.Shirts;
    using E_Shop.Services.Shirts;
    using Microsoft.AspNetCore.Mvc;

    public class ShirtsController : Controller
    {
        private readonly IShirtService shirtService;
       

        public ShirtsController(IShirtService shirtService)
        {
            this.shirtService = shirtService;
            
        }
        public IActionResult Add() => View(new AddShirtFormModel
        {
            MasterShirts = this.shirtService.GetMasterShirts()
        });

        [HttpPost]
        public IActionResult Add(AddShirtFormModel shirt)
        {

            if (!this.shirtService.MasterShirtExists(shirt.MasterShirtId))
            {
                this.ModelState.AddModelError(nameof(shirt.MasterShirtId), "item does not exist.");
            }
            if (shirt.Quantity <= 0)
            {
                this.ModelState.AddModelError(nameof(shirt.MasterShirtId), "quantity must be at least 1");
            }
            if (!ModelState.IsValid)
            {
                shirt.MasterShirts = this.shirtService.GetMasterShirts();
                return View(shirt);
            }

            this.shirtService.Create(shirt.Quantity, shirt.Price, shirt.Size, shirt.MasterShirtId);
            return RedirectToAction("All", "MasterShirt");
        }
        //private IEnumerable<ShirtCategoryViewModel> GetShirtCategories()
        //    => this.data
        //    .Categories
        //    .Select(c => new ShirtCategoryViewModel
        //    {
        //        Id = c.Id,
        //        Name = c.Name
        //    }).ToList();

        
    }
}
