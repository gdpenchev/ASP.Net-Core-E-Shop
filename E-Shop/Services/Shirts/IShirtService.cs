namespace E_Shop.Services.Shirts
{
    using E_Shop.Services.Shirts.Models;
    using System.Collections.Generic;
    public interface IShirtService
    {
        IEnumerable<ShirtMasterShirtModel> GetMasterShirts();

        bool MasterShirtExists(int id);

        void Create(int quantity, decimal price, string size, int masterShirtId);
    }
}
