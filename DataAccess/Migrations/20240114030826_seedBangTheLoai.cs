using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedBangTheLoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TheLoai",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "Hài hước" },
                    { 2, "Kinh dị" },
                    { 3, "Tình cảm" },
                    { 4, "Lịch sử" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TheLoai",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TheLoai",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TheLoai",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TheLoai",
                keyColumn: "ID",
                keyValue: 4);
        }
    }
}
