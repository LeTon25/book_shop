
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BookyStore.DataAccess.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }  
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }  
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderDetail> OrderDetails { get; set; }  
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options) 
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Category>().HasData(
					new Category { ID = 1,Name="Hài hước"},
					new Category { ID = 2,Name="Kinh dị"},
					new Category { ID = 3,Name="Tình cảm"},
					new Category { ID = 4,Name="Lịch sử"}
				);
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            } 
		}
	}
}
