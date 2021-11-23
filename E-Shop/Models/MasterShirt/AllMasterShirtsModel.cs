using E_Shop.Services.MasterShirt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models.Shirts
{
    public class AllMasterShirtsModel
    {
        public const int MasterShirtPerPage = 3;

        public int CurrentPage { get; init; } = 1;
        public string Category { get; init; }
        public IEnumerable<string> Categories { get; set; }

        public int TotalMasterShirts { get; set; }

        public string SearchByText { get; set; }

        public string MasterShirt { get; init; }

        public IEnumerable<string> MasterShirtsNames { get; set; }

        public IEnumerable<MasterShirtServiceListingModel> MasterShirts { get; set; }

    }
}
