using E_Shop.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {   // extension method for the DB - it has to be static class and static method and we need this
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<EShopDbContext>();

            data.Database.Migrate();

            return app;
        }
    }
}
