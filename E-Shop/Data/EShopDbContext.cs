
namespace E_Shop.Data
{
    using E_Shop.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class EShopDbContext : IdentityDbContext<User>
    {
        public EShopDbContext(DbContextOptions<EShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<Shirt> Shirts { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<MasterShirt> MasterShirts { get; init; }

        public override DbSet<User> Users { get => base.Users; set => base.Users = value; }

        public DbSet<Order> Orders { get; init; }

        public DbSet<Cart> Carts { get; init; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Shirt>()
                .HasOne(s => s.MasterShirt)
                .WithMany(ms => ms.Shirts)
                .HasForeignKey(s => s.MasterShirtId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MasterShirt>()
                .HasOne(ms => ms.Category)
                .WithMany(c => c.MasterShirts)
                .HasForeignKey(ms => ms.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
