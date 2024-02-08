using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class themKhoaNgoaiTuPaymentDenOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "ThanhToan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_OrderID",
                table: "ThanhToan",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ThanhToan_DonHang_OrderID",
                table: "ThanhToan",
                column: "OrderID",
                principalTable: "DonHang",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThanhToan_DonHang_OrderID",
                table: "ThanhToan");

            migrationBuilder.DropIndex(
                name: "IX_ThanhToan_OrderID",
                table: "ThanhToan");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "ThanhToan");
        }
    }
}
