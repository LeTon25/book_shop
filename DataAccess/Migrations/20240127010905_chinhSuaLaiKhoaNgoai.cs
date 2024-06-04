using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class chinhSuaLaiKhoaNgoai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThongBaoThanhToan_ThongBaoThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentId",
                table: "GiaoDich",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_PaymentId",
                table: "GiaoDich",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_GiaoDich_ThanhToan_PaymentId",
                table: "GiaoDich",
                column: "PaymentId",
                principalTable: "ThanhToan",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThongBaoThanhToan_ThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan",
                column: "NotiPayementId",
                principalTable: "ThanhToan",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GiaoDich_ThanhToan_PaymentId",
                table: "GiaoDich");

            migrationBuilder.DropForeignKey(
                name: "FK_ThongBaoThanhToan_ThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan");

            migrationBuilder.DropIndex(
                name: "IX_GiaoDich_PaymentId",
                table: "GiaoDich");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentId",
                table: "GiaoDich",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ThongBaoThanhToan_ThongBaoThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan",
                column: "NotiPayementId",
                principalTable: "ThongBaoThanhToan",
                principalColumn: "Id");
        }
    }
}
