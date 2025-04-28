using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Register_Login.Entity;

namespace PointOfSale.Models
{
    public class CategoriesDbContext : DbContext
    {
        public CategoriesDbContext(DbContextOptions<CategoriesDbContext> options)
           : base(options)
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Brand> Brands {  get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Explicitly set primary keys (optional if using [Key] attributes)
            modelBuilder.Entity<Products>()
                .HasKey(p => p.Product_Id);

            modelBuilder.Entity<Categories>()
                .HasKey(c => c.Cat_Id);

            modelBuilder.Entity<Brand>()
                .HasKey(b => b.Brand_Id);

            // ✅ Category relationship
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Cat_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ Brand relationship
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Brands)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.Brand_Id)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
