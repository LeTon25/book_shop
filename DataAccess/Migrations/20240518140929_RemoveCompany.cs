using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChuKyThanhToan");

            migrationBuilder.DropTable(
                name: "CongTy");

            migrationBuilder.DropTable(
                name: "GiaoDich");

            migrationBuilder.DropTable(
                name: "ThongBaoThanhToan");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "CongThanhToan");

            migrationBuilder.DropIndex(
                name: "IX_Users_CompanyID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CongThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DesLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SortIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongThanhToan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CongTy",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongTy", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    PaymentDestinationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentLanguage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLastMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhToan_CongThanhToan_PaymentDestinationId",
                        column: x => x.PaymentDestinationId,
                        principalTable: "CongThanhToan",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThanhToan_DonHang_OrderID",
                        column: x => x.OrderID,
                        principalTable: "DonHang",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChuKyThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    SignAlgo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SignOwn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "GiaoDich",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TranAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranRefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoDich", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaoDich_ThanhToan_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "ThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThongBaoThanhToan",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotiPayementId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotiAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NotiContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiNotiStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiPaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotiResHttpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiResMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotiSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentRefId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongBaoThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThongBaoThanhToan_ThanhToan_NotiPayementId",
                        column: x => x.NotiPayementId,
                        principalTable: "ThanhToan",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CongTy",
                columns: new[] { "ID", "City", "Name", "PhoneNumber", "PostalCode", "State", "StreetAddress" },
                values: new object[,]
                {
                    { 1, "TPHCM", "TMA Solutions", "0923326715", "0045545", "Nguyễn Huệ", "Quang Trung" },
                    { 2, "TPHCM", "Anh Quân Techs", "0905227951", "0041545", "Q5", "An Dương Vương" },
                    { 3, "TPHCM", "FPT", "0905227445", "1045545", "Nhà Bè", "Nguyễn Hữu Thọ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CompanyID",
                table: "Users",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ChuKyThanhToan_PaymentId",
                table: "ChuKyThanhToan",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoDich_PaymentId",
                table: "GiaoDich",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_OrderID",
                table: "ThanhToan",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_PaymentDestinationId",
                table: "ThanhToan",
                column: "PaymentDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_ThongBaoThanhToan_NotiPayementId",
                table: "ThongBaoThanhToan",
                column: "NotiPayementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CongTy_CompanyID",
                table: "Users",
                column: "CompanyID",
                principalTable: "CongTy",
                principalColumn: "ID");
        }
    }
}
