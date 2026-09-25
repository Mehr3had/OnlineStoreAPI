using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductImageUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/images/laptop-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/images/laptop-2.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/images/headphones-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/images/clean-code-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/images/tshirt-1.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "images/laptop-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "images/laptop-2.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "images/headphones-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "image/clean-code-1.jpg");

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "images/tshirt-1.jpg");
        }
    }
}
