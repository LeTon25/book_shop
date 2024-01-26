
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BookyStore.DataAccess.Data
{
	public class AppDbContext : IdentityDbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
        public DbSet<Company>  Companies { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }  
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }  
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderDetail> OrderDetails { get; set; }    
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
			modelBuilder.Entity<Product>().HasData(
                    new Product
                    {
                        ID = 1,
                        Title = "Vì cậu là bạn nhỏ của tớ",
                        Author = "Tun Phạm",
                        Description = "Vì cậu là bạn nhỏ của tớ là cuốn sách đầu tay đánh dấu chặng hành trình phát triển, nỗ lực không ngừng nghỉ của Tác giả, MC, Content Creator Tun Phạm.",
                        ISBN = "SWD9999001",
                        CategoryId = 1,
                        ImageUrl="",
                    },
                    new Product
                    {
                        ID = 2,
                        Title = "Bàn cờ lớn",
                        Author = "Zbigniew Brzezinski",
                        Description = "“Bàn cờ lớn” thể hiện tầm nhìn địa chiến lược táo bạo và khiêu khích của Brzezinski dành cho sự ưu việt của nước Mỹ trong thế kỷ 21",
                        ISBN = "CAW777777701",
                        CategoryId = 2, 
                        ImageUrl="",
                    },
                    new Product
                    {
                        ID = 3,
                        Title = "Trốn Lên Mái Nhà Để Khóc",
                        Author = "Lam",
                        Description = "Những cơn gió heo may len lỏi vào từng góc phố nhỏ, mùa thu về gợi nhớ bao yêu thương đong đầy, bao xúc cảm dịu dàng của ký ức. Đó là nỗi nhớ đau đáu những hương vị quen thuộc của đồng nội, là hoài niệm bất chợt khi đi trên con đường cũ in dấu bao kỷ niệm...",
                        ISBN = "RITO5555501",
                        CategoryId=3,
                        ImageUrl="",
                    },
                    new Product
                    {
                        ID = 4,
                        Title = "Ghi chép pháp y - Những cái chết bí ẩn",
                        Author = "Lưu hiểu huy",
                        Description = "Ghi chép pháp y - Những cái chết bí ẩn là cuốn sách nằm trong hệ liệt với Pháp y Tần Minh - bộ tiểu thuyết nổi đình đám của xứ Trung đã được chuyển thể thành series phim. Cuốn sách tổng hợp những vụ án có thật, được viết bởi bác sĩ pháp y Lưu Hiểu Huy - người có 15 năm kinh nghiệm và từng mổ xẻ hơn 800 tử thi.",
                        ISBN = "WS3333333301",
                        CategoryId =4,
                        ImageUrl="",
                    }
                );
            modelBuilder.Entity<Company>().HasData(
                    new Company {ID=1,Name="TMA Solutions",StreetAddress="Quang Trung",State="Nguyễn Huệ",City="TPHCM",PostalCode="0045545",PhoneNumber="0923326715"},
                    new Company { ID = 2, Name = "Anh Quân Techs", StreetAddress = "An Dương Vương", State = "Q5", City = "TPHCM", PostalCode = "0041545", PhoneNumber = "0905227951" },
                    new Company { ID = 3, Name = "FPT", StreetAddress = "Nguyễn Hữu Thọ", State = "Nhà Bè", City = "TPHCM", PostalCode = "1045545", PhoneNumber = "0905227445" }
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
