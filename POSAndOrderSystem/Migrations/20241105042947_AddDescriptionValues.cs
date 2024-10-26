using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSAndOrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: "Cashier");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: "Customer");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: "Delivery");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: "Pending");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 6,
                column: "Description",
                value: "In Progress");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 7,
                column: "Description",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 8,
                column: "Description",
                value: "Canceld");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 9,
                column: "Description",
                value: "Cash");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 10,
                column: "Description",
                value: "Credit Card");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 11,
                column: "Description",
                value: "Online Wallet");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 12,
                column: "Description",
                value: "Paid");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 13,
                column: "Description",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 14,
                column: "Description",
                value: "Pickup At Restaurant");

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { "Deliver To Customer", "Deliver To Customer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 5,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 6,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 7,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 8,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 9,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 10,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 11,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 12,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 13,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 14,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "LookupItems",
                keyColumn: "ID",
                keyValue: 15,
                columns: new[] { "Description", "Name" },
                values: new object[] { null, "Deliver To Customer " });
        }
    }
}
