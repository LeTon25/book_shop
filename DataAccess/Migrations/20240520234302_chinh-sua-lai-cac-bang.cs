using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class chinhsualaicacbang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_TheLoai_CategoryId",
                table: "Sach");

            migrationBuilder.DropIndex(
                name: "IX_Sach_CategoryId",
                table: "Sach");

            migrationBuilder.DeleteData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "Sach");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Sach",
                newName: "Stock");

            migrationBuilder.AddColumn<int>(
                name: "CollectionID",
                table: "Sach",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoSuuTap",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoSuuTap", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TheLoaiSach",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CollectionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiSach", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TheLoaiSach_BoSuuTap_CollectionID",
                        column: x => x.CollectionID,
                        principalTable: "BoSuuTap",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TheLoaiSach_Sach_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Sach",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TheLoaiSach_TheLoai_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "TheLoai",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sach_CollectionID",
                table: "Sach",
                column: "CollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSach_CategoryID",
                table: "TheLoaiSach",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSach_CollectionID",
                table: "TheLoaiSach",
                column: "CollectionID");

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSach_ProductID",
                table: "TheLoaiSach",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_BoSuuTap_CollectionID",
                table: "Sach",
                column: "CollectionID",
                principalTable: "BoSuuTap",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_BoSuuTap_CollectionID",
                table: "Sach");

            migrationBuilder.DropTable(
                name: "TheLoaiSach");

            migrationBuilder.DropTable(
                name: "BoSuuTap");

            migrationBuilder.DropIndex(
                name: "IX_Sach_CollectionID",
                table: "Sach");

            migrationBuilder.DropColumn(
                name: "CollectionID",
                table: "Sach");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Sach",
                newName: "CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "Sach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "ID", "Author", "CategoryId", "Description", "ISBN", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Tun Phạm", 1, "Vì cậu là bạn nhỏ của tớ là cuốn sách đầu tay đánh dấu chặng hành trình phát triển, nỗ lực không ngừng nghỉ của Tác giả, MC, Content Creator Tun Phạm.", "SWD9999001", 0m, "Vì cậu là bạn nhỏ của tớ" },
                    { 2, "Zbigniew Brzezinski", 2, "“Bàn cờ lớn” thể hiện tầm nhìn địa chiến lược táo bạo và khiêu khích của Brzezinski dành cho sự ưu việt của nước Mỹ trong thế kỷ 21", "CAW777777701", 0m, "Bàn cờ lớn" },
                    { 3, "Lam", 3, "Những cơn gió heo may len lỏi vào từng góc phố nhỏ, mùa thu về gợi nhớ bao yêu thương đong đầy, bao xúc cảm dịu dàng của ký ức. Đó là nỗi nhớ đau đáu những hương vị quen thuộc của đồng nội, là hoài niệm bất chợt khi đi trên con đường cũ in dấu bao kỷ niệm...", "RITO5555501", 0m, "Trốn Lên Mái Nhà Để Khóc" },
                    { 4, "Lưu hiểu huy", 4, "Ghi chép pháp y - Những cái chết bí ẩn là cuốn sách nằm trong hệ liệt với Pháp y Tần Minh - bộ tiểu thuyết nổi đình đám của xứ Trung đã được chuyển thể thành series phim. Cuốn sách tổng hợp những vụ án có thật, được viết bởi bác sĩ pháp y Lưu Hiểu Huy - người có 15 năm kinh nghiệm và từng mổ xẻ hơn 800 tử thi.", "WS3333333301", 0m, "Ghi chép pháp y - Những cái chết bí ẩn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sach_CategoryId",
                table: "Sach",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_TheLoai_CategoryId",
                table: "Sach",
                column: "CategoryId",
                principalTable: "TheLoai",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
