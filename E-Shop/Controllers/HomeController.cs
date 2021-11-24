namespace E_Shop.Controllers
{
    using E_Shop.Data;
    using E_Shop.Models;
    using E_Shop.Models.Home;
    using E_Shop.Services.MasterShirt;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    public class HomeController : Controller
    {

        private readonly IMasterShirtService masterShirtService;
        private readonly IMemoryCache cache;

        public HomeController(IMasterShirtService masterShirtService, IMemoryCache cache)
        {
            this.masterShirtService = masterShirtService;
            this.cache = cache;
        }
        public IActionResult Index()
        {
            //we create cache key, we get the chache for the model
            const string latestMsCacheKey = "LatestMasterShirtCacheKey";

            var latestShirts = this.cache.Get<List<ShirtIndexViewModel>>(latestMsCacheKey);

            if (latestShirts == null)
            {
                //we get the masterShirts from the DB
                latestShirts = this.masterShirtService.Latest().ToList();
                //we set for how long the cache to be applicable
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
                //we set the cache
                this.cache.Set(latestMsCacheKey, latestShirts, cacheOptions);
            }

            
            return View(new IndexViewModel
            {
                Shirts = latestShirts
            });
        }

      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
