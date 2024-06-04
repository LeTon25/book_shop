using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheLoaiSach_BoSuuTap_CollectionID",
                table: "TheLoaiSach");

            migrationBuilder.DropIndex(
                name: "IX_TheLoaiSach_CollectionID",
                table: "TheLoaiSach");

            migrationBuilder.DropColumn(
                name: "CollectionID",
                table: "TheLoaiSach");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionID",
                table: "TheLoaiSach",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TheLoaiSach_CollectionID",
                table: "TheLoaiSach",
                column: "CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_TheLoaiSach_BoSuuTap_CollectionID",
                table: "TheLoaiSach",
                column: "CollectionID",
                principalTable: "BoSuuTap",
                principalColumn: "ID");
        }
    }
}
