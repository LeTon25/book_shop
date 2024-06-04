using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class themCotPriceVaoBangSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Sach",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 1,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 2,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 3,
                column: "Price",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 4,
                column: "Price",
                value: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Sach");
        }
    }
}
