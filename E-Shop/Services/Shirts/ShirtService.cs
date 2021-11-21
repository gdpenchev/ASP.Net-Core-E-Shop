namespace E_Shop.Services.Shirts
{
    using E_Shop.Data;
    using E_Shop.Data.Models;
    using E_Shop.Services.Shirts.Models;
    using System.Collections.Generic;
    using System.Linq;
    public class ShirtService : IShirtService
    {
        private readonly EShopDbContext data;

        public ShirtService(EShopDbContext data)
        {
            this.data = data;
        }

        public void Create(int quantity, decimal price, string size, int masterShirtId)
        {
            var newShirt = new Shirt
            {
                Quantity = quantity,
                Price = price,
                Size = size,
                MasterShirtId = masterShirtId,
            };
            data.Shirts.Add(newShirt);
            data.SaveChanges();
        }

        public IEnumerable<ShirtMasterShirtModel> GetMasterShirts()
        => this.data
            .MasterShirts
            .Select(ms => new ShirtMasterShirtModel
            {
                Id = ms.Id,
                Name = ms.Name
            }).ToList();

        public bool MasterShirtExists(int id)
        
          => this.data.MasterShirts.Any(ms => ms.Id == id);
        
    }
}
