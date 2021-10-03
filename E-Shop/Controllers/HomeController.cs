namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Models;
    using E_Shop.Models.Home;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    public class HomeController : Controller
    {

        private readonly EShopDbContext data;

        public HomeController(EShopDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {

            //var shirts = data.Shirts
            //    .OrderByDescending(s => s.Id)
            //    .Select(s => new ShirtIndexViewModel
            //    {
            //        //Name = s.Name,
            //        //Model = s.Model,
            //        Size = s.Size,
            //        ImageUrl = s.ImageUrl
            //    }).Take(5)
            //    .ToList();


            //return View(new IndexViewModel
            //{
            //    Shirts = shirts
            //});

            var shirts = data.MasterShirts
                .OrderByDescending(ms => ms.Id)
                .Select(ms => new ShirtIndexViewModel
                {
                    Name = ms.Name,
                    ImageUrl = ms.ImageURL
                })
                .ToList();
            return View(new IndexViewModel
            {
                Shirts = shirts
            });
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
