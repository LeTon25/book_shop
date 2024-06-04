using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class themCacBang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CongThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortIndex = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TranMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranRefId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiPaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiPayementId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiNotiStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiResMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongBaoThanhToan_ThongBaoThanhToan_NotiPayementId",
                        column: x => x.NotiPayementId,
                        principalTable: "ThongBaoThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDestinationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLastMessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhToan_CongThanhToan_PaymentDestinationId",
                        column: x => x.PaymentDestinationId,
                        principalTable: "CongThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChuKyThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SignValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignAlgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignOwn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChuKyThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChuKyThanhToan_ThanhToan_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "ThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChuKyThanhToan_PaymentId",
                table: "ChuKyThanhToan",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_PaymentDestinationId",
                table: "ThanhToan",
                column: "PaymentDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan",
                column: "NotiPayementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChuKyThanhToan");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "ThongBaoThanhToan");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "CongThanhToan");
        }
    }
}
