using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Shirts
{
    public class AllShirtsModel
    {
        public const int ShirtPerPage = 3;
        public string Size { get; init; }

        public IEnumerable<string> Sizes { get; set; }

        public int CurrentPage { get; init; } = 1;
        public string Cateogory { get; init; }
        public IEnumerable<string> Categories { get; set; }

        public int TotalShirts { get; set; }

        public string SearchByText { get; set; }

        public IEnumerable<ShirtListingViewModel> Shirts { get; set; }

    }
}
