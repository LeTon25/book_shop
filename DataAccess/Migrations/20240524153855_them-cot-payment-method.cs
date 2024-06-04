using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class themcotpaymentmethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "Sach",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NhaXuatBan",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "DonHang",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach",
                column: "PublisherID",
                principalTable: "NhaXuatBan",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "DonHang");

            migrationBuilder.AlterColumn<int>(
                name: "PublisherID",
                table: "Sach",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "NhaXuatBan",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Sach_NhaXuatBan_PublisherID",
                table: "Sach",
                column: "PublisherID",
                principalTable: "NhaXuatBan",
                principalColumn: "ID");
        }
    }
}
