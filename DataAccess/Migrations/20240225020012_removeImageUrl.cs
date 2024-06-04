using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sach");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users",
                column: "CompanyID",
                principalTable: "CongTy",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 1,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 2,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 3,
                column: "ImageUrl",
                value: "");

            migrationBuilder.UpdateData(
                table: "Sach",
                keyColumn: "ID",
                keyValue: 4,
                column: "ImageUrl",
                value: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users",
                column: "CompanyID",
                principalTable: "CongTy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
