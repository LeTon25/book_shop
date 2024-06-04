using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedDuLieuCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CongTy",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "TPHCM", "TMA Solutions", "0923326715", "0045545", "Nguyễn Huệ", "Quang Trung" },
                    { 2, "TPHCM", "Anh Quân Techs", "0905227951", "0041545", "Q5", "An Dương Vương" },
                    { 3, "TPHCM", "FPT", "0905227445", "1045545", "Nhà Bè", "Nguyễn Hữu Thọ" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CongTy",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CongTy",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CongTy",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
