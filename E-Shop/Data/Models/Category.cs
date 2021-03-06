using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Data.Models
{
    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public IEnumerable<MasterShirt> MasterShirts { get; init; } = new List<MasterShirt>();
    }
}
