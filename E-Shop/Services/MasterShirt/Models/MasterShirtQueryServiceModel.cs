namespace E_Shop.Services.MasterShirt
{
    using System.Collections.Generic;
    public class MasterShirtQueryServiceModel
    {
        public const int MasterShirtPerPage = 3;

        public int CurrentPage { get; init; } = 1;
        public string Cateogory { get; init; }
        public IEnumerable<string> Categories { get; set; }

        public int TotalMasterShirts { get; set; }

        public string SearchByText { get; set; }

        public string MasterShirt { get; init; }

        public IEnumerable<MasterShirtServiceListingModel> MasterShirts { get; set; }
    }
}
