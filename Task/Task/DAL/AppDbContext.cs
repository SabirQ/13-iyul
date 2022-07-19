using Microsoft.EntityFrameworkCore;
using System.Linq;
using Task_MVC.Models;

namespace Task_MVC.DAL
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PlantCategory> PlantCategories { get; set; }
        public DbSet<PlantImage> PlantImages { get; set; }
        public DbSet<PlantInformation> PlantInformations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PlantTag> PlantTags { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<PlantColor> PlantColors { get; set; }
        public DbSet<PlantSize> PlantSizes { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                )
            {
                item.SetColumnType("decimal(6,2)");
               
            }
            modelBuilder.Entity<Setting>().HasIndex(p => p.Key).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Color>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Size>().HasIndex(s => s.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
