namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.MasterShirt;
    using E_Shop.Models.Shirts;
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

        public IActionResult All([FromQuery] AllMasterShirtsModel query)
        {

            var shirtsListQuery = this.data.MasterShirts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Cateogory))
            {
                shirtsListQuery = shirtsListQuery.Where(s => s.Category.Name == query.Cateogory);
            }
            if (!string.IsNullOrWhiteSpace(query.SearchByText))
            {
                shirtsListQuery = shirtsListQuery.Where(ms =>
                (ms.Name + " " + ms.Category.Name).ToLower().Contains(query.SearchByText.ToLower()));
            }

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
            var masterShirts = shirtsListQuery
                .Skip((query.CurrentPage - 1) * AllMasterShirtsModel.MasterShirtPerPage)
                .Take(AllMasterShirtsModel.MasterShirtPerPage)
                .OrderByDescending(ms => ms.Id)
                .Select(ms => new MasterShirtListingViewModel
                {
                    Id = ms.Id,
                    Name = ms.Name,
                    Description = ms.Description,
                    ImageUrl = ms.ImageURL,
                    Category = ms.Category.Name
                }).ToList();

            var masterShirtsCategories = this.data
                .MasterShirts
                .Select(ms => ms.Category.Name)
                .OrderBy(ms => ms)
                .Distinct()
                .ToList();

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

            var totalMasterShirts = shirtsListQuery.Count();

            query.TotalMasterShirts = totalMasterShirts;
            query.Categories = masterShirtsCategories;
            query.MasterShirts = masterShirts;


            return View(query);
        }

        public IActionResult Details(int id)
        {
            var masterShirt = this.data.MasterShirts.Where(ms => ms.Id == id).FirstOrDefault();

            if (masterShirt == null)
            {
                return BadRequest();
            }

            var shirts = this.data.Shirts
                .Where(s => s.MasterShirtId == id)
                .OrderByDescending(s => s)
                .ToList();

            return View(new DetailsMasterShirtViewModel
            {
                Id = masterShirt.Id,
                Name = masterShirt.Name,
                ImageUrl = masterShirt.ImageURL,
                Shirts = shirts
            });
        }
        public IActionResult Edit(int id)
        {
            var masterShirt = this.data.MasterShirts.Where(ms => ms.Id == id).FirstOrDefault();

            if (masterShirt == null)
            {
                return BadRequest();
            }

            return View(new AddMasterShirtFormModel
            {
                Name = masterShirt.Name,
                Description = masterShirt.Description,
                ImageURL = masterShirt.ImageURL,
                Categories = GetShirtCategories()
            });
            

        }
        [HttpPost]
        public IActionResult Edit(int id, AddMasterShirtFormModel updatedMasterShirt)
        {
            
            var masterShirtData = this.data.MasterShirts.Find(id);

            masterShirtData.Name = updatedMasterShirt.Name;
            masterShirtData.Description = updatedMasterShirt.Description;
            masterShirtData.ImageURL = updatedMasterShirt.ImageURL;
            masterShirtData.CategoryId = updatedMasterShirt.CategoryId;

            this.data.SaveChanges();

            return RedirectToAction("All");
        }
        public IActionResult Delete(int id)
        {
            var masterShirtData = this.data.MasterShirts.FirstOrDefault(ms => ms.Id == id);
            var shirtList = this.data.Shirts.Where(s => s.MasterShirtId == id).ToList();
            if (masterShirtData == null)
            {
                return BadRequest();
            }
            this.data.Shirts.RemoveRange(shirtList);
            this.data.MasterShirts.Remove(masterShirtData);
            this.data.SaveChanges();
            return RedirectToAction("All");
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
