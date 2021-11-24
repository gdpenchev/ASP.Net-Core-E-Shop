using E_Shop.Models.Home;
using E_Shop.Models.MasterShirt;
using E_Shop.Models.Shirts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace E_Shop.Services.MasterShirt
{
    public interface IMasterShirtService
    {
        AllMasterShirtsModel All(
            string category,
            string searchByText,
            string masterShirt,
            int currentPage);

        List<MasterShirtCategoryServiceModel> GetAllCategories();

        MasterShirtDetailsServiceModel Details(int id);

        MasterShirtFormModel Edit(int id);

        void Edit(int id, string name, string description, string imageUrl, int categoryId);

        bool Delete(int id);

        int Create(string name, string description, string imageUrl, int categoryId);

        bool CategoryExists(int id);

        bool NameExists(string name);

        IQueryable<ShirtIndexViewModel> Latest();
    }
}
