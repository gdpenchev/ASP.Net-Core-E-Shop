using E_Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Services.MasterShirt
{
    public class MasterShirtDetailsServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public Shirt currentShirt { get; set; }

        public List<Shirt> Shirts { get; set; }
    }
}
