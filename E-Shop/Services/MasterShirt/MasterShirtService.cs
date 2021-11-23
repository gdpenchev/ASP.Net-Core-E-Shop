namespace E_Shop.Services.MasterShirt
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Models.MasterShirt;
    using E_Shop.Models.Shirts;
    using System.Collections.Generic;
    using System.Linq;

    public class MasterShirtService : IMasterShirtService
    {
        private readonly EShopDbContext data;

        public MasterShirtService(EShopDbContext data) 
            => this.data = data;

        public AllMasterShirtsModel All(
            string category,
            string searchByText,
            string masterShirt,
            int currentPage)
        {
            var shirtsListQuery = this.data.MasterShirts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                shirtsListQuery = shirtsListQuery.Where(s =>
                s.Category.Name == category);
            }
            if (!string.IsNullOrWhiteSpace(searchByText))
            {
                shirtsListQuery = shirtsListQuery.Where(ms =>
                (ms.Name + " " + ms.Category.Name).ToLower().Contains(searchByText.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(masterShirt))
            {
                shirtsListQuery = shirtsListQuery.Where(ms =>
                (ms.Name == masterShirt));
            }
            ///TODO: change the master shirt filter into something else
                var masterShirts = shirtsListQuery
                .Skip((currentPage - 1) * AllMasterShirtsModel.MasterShirtPerPage)
                .Take(AllMasterShirtsModel.MasterShirtPerPage)
                .OrderByDescending(ms => ms.Id)
                .Select(ms => new MasterShirtServiceListingModel
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

            var masterShirtsNames = this.data
                .MasterShirts
                .Select(ms => ms.Name)
                .OrderBy(ms => ms)
                .Distinct()
                .ToList();
                

            var totalMasterShirts = shirtsListQuery.Count();

            return new AllMasterShirtsModel
            {
                TotalMasterShirts = totalMasterShirts,
                Categories = masterShirtsCategories,
                MasterShirts = masterShirts,
                MasterShirtsNames = masterShirtsNames,
                CurrentPage = currentPage
            };
        }

        

        public MasterShirtDetailsServiceModel Details(int id)
        {
            var shirts = this.data.Shirts.Where(s => s.MasterShirtId == id).ToList();
            var masterShirt = this.data.MasterShirts.Where(ms => ms.Id == id)
                .Select(ms => new MasterShirtDetailsServiceModel
                {
                    Id = ms.Id,
                    ImageUrl = ms.ImageURL,
                    Name = ms.Name,
                    Description = ms.Description,
                    Shirts = shirts
                }).FirstOrDefault();
            return masterShirt;
        }

        public MasterShirtFormModel Edit(int id)
        {
            var categories = GetAllCategories();
            var masterShirt = this.data.MasterShirts.Where(ms => ms.Id == id).Select(ms => new MasterShirtFormModel
            {
                Name = ms.Name,
                Description = ms.Description,
                ImageURL = ms.ImageURL,
                Categories = categories
            }).FirstOrDefault();

            return masterShirt;
        }

        public void Edit(int id, string name, string description, string imageUrl, int categoryId)
        {
            var masterShirtData = this.data.MasterShirts.Find(id);

            masterShirtData.Name = name;
            masterShirtData.Description = description;
            masterShirtData.ImageURL = imageUrl;
            masterShirtData.CategoryId = categoryId;

            this.data.SaveChanges();
        }
        

        public int Create(string name, string description, string imageURL, int categoryId)
        {
           
            var newMasterShirt = new MasterShirt
            {
                Name = name,
                Description = description,
                ImageURL = imageURL,
                CategoryId = categoryId
            };
            this.data.MasterShirts.Add(newMasterShirt);
            this.data.SaveChanges();

            return newMasterShirt.Id;
        }

        public bool Delete(int id)
        {
            var masterShirtData = this.data.MasterShirts.FirstOrDefault(ms => ms.Id == id);
            var shirtList = this.data.Shirts.Where(s => s.MasterShirtId == id).ToList();
            if (masterShirtData == null)
            {
                return false;
            }
            this.data.Shirts.RemoveRange(shirtList);
            this.data.MasterShirts.Remove(masterShirtData);
            this.data.SaveChanges();
            return true;
        }
        public bool CategoryExists(int id)
            => this.data.Categories.Any(c => c.Id == id);
        public List<MasterShirtCategoryServiceModel> GetAllCategories()
             => this.data
           .Categories
           .Select(c => new MasterShirtCategoryServiceModel
           {
               Id = c.Id,
               Name = c.Name
           }).ToList();

        public bool NameExists(string name)
        => this.data.MasterShirts.Any(ms => ms.Name == name);
    }
}
