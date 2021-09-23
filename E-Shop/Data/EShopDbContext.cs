using E_Shop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Shop.Data
{
    public class EShopDbContext : IdentityDbContext
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shirt> Shirts;

        public DbSet<Gift> Gifts;

        public DbSet<Category> Categories;


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Shirt>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Shirts)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
