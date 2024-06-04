using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class thembangnxb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "Sach",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NhaXuatBan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaXuatBan", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sach_PublisherID",
                table: "Sach",
                column: "PublisherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach",
                column: "PublisherID",
                principalTable: "NhaXuatBan",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach");

            migrationBuilder.DropTable(
                name: "NhaXuatBan");

            migrationBuilder.DropIndex(
                name: "IX_Sach_PublisherID",
                table: "Sach");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Sach");
        }
    }
}
