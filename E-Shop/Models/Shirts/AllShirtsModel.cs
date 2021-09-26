using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Shirts
{
    public class AllShirtsModel
    {
        public string Model { get; init; }

        public IEnumerable<ShirtListingViewModel> Shirts { get; set; }

    }
}
