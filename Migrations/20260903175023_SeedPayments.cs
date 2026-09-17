using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "Status" },
                values: new object[] { 1, 1650m, 1, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
