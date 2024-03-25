using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VoucherApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedVoucherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vouchers",
                columns: new[] { "VoucherId", "DiscountAmount", "MinAmount", "VoucherCode" },
                values: new object[,]
                {
                    { 1, 10.0, 20, "10OFF" },
                    { 2, 20.0, 40, "20OFF" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "VoucherId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vouchers",
                keyColumn: "VoucherId",
                keyValue: 2);
        }
    }
}
