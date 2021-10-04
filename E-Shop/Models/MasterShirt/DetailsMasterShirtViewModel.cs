namespace E_Shop.Models.MasterShirt
{
    using E_Shop.Data.Models;
    using System.Collections.Generic;
    public class DetailsMasterShirtViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public List<Shirt> Shirts { get; set; }
    }
}
