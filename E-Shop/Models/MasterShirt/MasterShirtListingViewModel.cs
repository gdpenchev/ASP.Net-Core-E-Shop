using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Shirts
{
    public class MasterShirtListingViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }

        // public decimal Price { get; init; }

        //public string Size { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public string Category { get; init; }
    }
}
