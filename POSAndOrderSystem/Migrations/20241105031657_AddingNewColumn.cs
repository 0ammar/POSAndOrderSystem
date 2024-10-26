using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSAndOrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "OrderItems",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LookupItems",
                type: "nvarchar(max)",
                nullable: true);

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
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "MenuTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "MenuTypeName",
                value: "Chicken On Charcoal");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderID",
                table: "OrderItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LookupItems");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.UpdateData(
                table: "MenuTypes",
                keyColumn: "ID",
                keyValue: 3,
                column: "MenuTypeName",
                value: "Chicken On Charcoal ");
        }
    }
}
