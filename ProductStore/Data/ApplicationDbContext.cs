using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ProductStore.Models.Entities;


namespace ProductStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Manufacturer>().ToTable("Manufacturers");
            modelBuilder.Entity<Category>().ToTable("Categories");
            //modelBuilder.Entity<Product>().ToTable("Products");
            var cat1 = new Category { CategoryId = 1, Name = "Drikke", Description = "Væsker som trygt kan konsumeres av homosapien" };
            var cat2 = new Category { CategoryId = 2, Name = "Mat", Description = "Spiselige stoffer egnet til konsum for homosapien" };

            modelBuilder.Entity<Category>().HasData(cat1);
            modelBuilder.Entity<Category>().HasData(cat2);

            var man1 = new Manufacturer { ManufacturerId = 1, Name = "Freia", Description = "Spesialister på anskaffelse av diabetes", Address = "Oslofjorden" };
            var man2 = new Manufacturer { ManufacturerId = 2, Name = "First Price", Description = "Lager ukategoriserte stoffer" };


            modelBuilder.Entity<Manufacturer>().HasData(man1);
            modelBuilder.Entity<Manufacturer>().HasData(man2);

            //// Seeding
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Melkesjokolade",
                Price = 11.50m,
                CategoryId = 2,
                ManufacturerId = 1
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Iste",
                Price = 31.70m,
                CategoryId = 1,
                ManufacturerId = 2
            }); ;
        }


    }


}
