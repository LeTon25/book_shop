using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookyStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removepaymentdueorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDueDate",
                table: "DonHang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDueDate",
                table: "DonHang",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
