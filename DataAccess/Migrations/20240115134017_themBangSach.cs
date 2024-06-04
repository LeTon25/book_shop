using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class themBangSach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "ID", "Author", "Description", "ISBN", "Title" },
                values: new object[,]
                {
                    { 1, "Tun Phạm", "Vì cậu là bạn nhỏ của tớ là cuốn sách đầu tay đánh dấu chặng hành trình phát triển, nỗ lực không ngừng nghỉ của Tác giả, MC, Content Creator Tun Phạm.", "SWD9999001", "Vì cậu là bạn nhỏ của tớ" },
                    { 2, "Zbigniew Brzezinski", "“Bàn cờ lớn” thể hiện tầm nhìn địa chiến lược táo bạo và khiêu khích của Brzezinski dành cho sự ưu việt của nước Mỹ trong thế kỷ 21", "CAW777777701", "Bàn cờ lớn" },
                    { 3, "Lam", "Những cơn gió heo may len lỏi vào từng góc phố nhỏ, mùa thu về gợi nhớ bao yêu thương đong đầy, bao xúc cảm dịu dàng của ký ức. Đó là nỗi nhớ đau đáu những hương vị quen thuộc của đồng nội, là hoài niệm bất chợt khi đi trên con đường cũ in dấu bao kỷ niệm...", "RITO5555501", "Trốn Lên Mái Nhà Để Khóc" },
                    { 4, "Lưu hiểu huy", "Ghi chép pháp y - Những cái chết bí ẩn là cuốn sách nằm trong hệ liệt với Pháp y Tần Minh - bộ tiểu thuyết nổi đình đám của xứ Trung đã được chuyển thể thành series phim. Cuốn sách tổng hợp những vụ án có thật, được viết bởi bác sĩ pháp y Lưu Hiểu Huy - người có 15 năm kinh nghiệm và từng mổ xẻ hơn 800 tử thi.", "WS3333333301", "Ghi chép pháp y - Những cái chết bí ẩn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sach");
        }
    }
}
