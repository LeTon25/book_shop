using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyFromUserToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyID",
                table: "Users",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users",
                column: "CompanyID",
                principalTable: "CongTy",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Users");
        }
    }
}
