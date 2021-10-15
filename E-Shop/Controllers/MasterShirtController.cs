namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.MasterShirt;
    using E_Shop.Models.Shirts;
    using E_Shop.Services.MasterShirt;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class MasterShirtController : Controller
    {
        private readonly IMasterShirtService masterShirtService;

        public MasterShirtController(IMasterShirtService masterShirtService)
        {
            
            this.masterShirtService = masterShirtService;
        }
        public IActionResult Add() => View(new MasterShirtFormModel
        {
            Categories = masterShirtService.GetAllCategories()
        });
        [HttpPost]
        public IActionResult Add(MasterShirtFormModel masterShirt)
        {
            if (!this.masterShirtService.CategoryExists(masterShirt.CategoryId))
            {
                this.ModelState.AddModelError(nameof(masterShirt.CategoryId), "does not exists");
            }
            if (this.masterShirtService.NameExists(masterShirt.Name))
            {
                this.ModelState.AddModelError(nameof(masterShirt.Name), "already exists");
            }
           
            if (!this.ModelState.IsValid)
            {
                masterShirt.Categories = masterShirtService.GetAllCategories();
                return View(masterShirt);
            }

            this.masterShirtService.Create(masterShirt.Name, masterShirt.Description, masterShirt.ImageURL, masterShirt.CategoryId);
            
            return RedirectToAction("Add", "Shirts");
        }

        public IActionResult All([FromQuery] AllMasterShirtsModel query)
        {
            var queryResult = this.masterShirtService.All(
                query.Category,
                query.SearchByText,
                query.MasterShirt,
                query.CurrentPage
                );
            

            query.TotalMasterShirts = queryResult.TotalMasterShirts;
            query.Categories = queryResult.Categories;
            query.MasterShirts = queryResult.MasterShirts;


            return View(query);
        }

        public IActionResult Details(int id)
        {
            var masterShirt = this.masterShirtService.Details(id);

            if (masterShirt == null)
            {
                return BadRequest();
            }

            return View(masterShirt);
        }
        public IActionResult Edit(int id)
        {
            var masterShirt = this.masterShirtService.Edit(id);

            if (masterShirt == null)
            {
                return BadRequest();
            }
            return View(masterShirt);
        }
        [HttpPost]
        public IActionResult Edit(int id, MasterShirtFormModel updatedMasterShirt)
        {
            this.masterShirtService.Edit(id,
                updatedMasterShirt.Name,
                updatedMasterShirt.Description,
                updatedMasterShirt.ImageURL,
                updatedMasterShirt.CategoryId);
            

            return RedirectToAction(nameof(All));
        }
        public IActionResult Delete(int id)
        {
            bool isDeleted = this.masterShirtService.Delete(id);
            if (!isDeleted)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(All));
         }
    }
    
}
