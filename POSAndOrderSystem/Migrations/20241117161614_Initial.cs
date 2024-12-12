using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POSAndOrderSystem.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookupTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupTypes", x => x.ID);
                    table.CheckConstraint("CH_Name_Length1", "Len(Name) >= 3");
                });

            migrationBuilder.CreateTable(
                name: "MenuTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuTypeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LookupItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LookupTypeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupItems", x => x.ID);
                    table.CheckConstraint("CH_Name_Length", "Len(Name) >= 3");
                    table.ForeignKey(
                        name: "FK_LookupItems_LookupTypes_LookupTypeId",
                        column: x => x.LookupTypeId,
                        principalTable: "LookupTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    MenuTypeID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.ID);
                    table.CheckConstraint("CH_Price_Positive", "[Price] >= 0");
                    table.CheckConstraint("CH-MenuItemName_Length", "LEN( MenuItemName ) >= 3");
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuTypes_MenuTypeID",
                        column: x => x.MenuTypeID,
                        principalTable: "MenuTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    OrderNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderStatusID = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodID = table.Column<int>(type: "int", nullable: false),
                    PaymentStatusID = table.Column<int>(type: "int", nullable: false),
                    PickUpTypeID = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Orders_LookupItems_OrderStatusID",
                        column: x => x.OrderStatusID,
                        principalTable: "LookupItems",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Orders_LookupItems_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "LookupItems",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Orders_LookupItems_PaymentStatusID",
                        column: x => x.PaymentStatusID,
                        principalTable: "LookupItems",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Orders_LookupItems_PickUpTypeID",
                        column: x => x.PickUpTypeID,
                        principalTable: "LookupItems",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_LookupItems_RoleId",
                        column: x => x.RoleId,
                        principalTable: "LookupItems",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    MenuItemID = table.Column<int>(type: "int", nullable: false),
                    OrderItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderItemPrice = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    OrderItemNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LookupTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "User Role" },
                    { 2, "Order Status" },
                    { 3, "Payment Method" },
                    { 4, "Payment Status" },
                    { 5, "Pickup Type" }
                });

            migrationBuilder.InsertData(
                table: "MenuTypes",
                columns: new[] { "ID", "Image", "MenuTypeName" },
                values: new object[,]
                {
                    { 1, null, "Shawarma" },
                    { 2, null, "Roasted Chicken" },
                    { 3, null, "Chicken On Charcoal" },
                    { 4, null, "Prorated Chicken" },
                    { 5, null, "Roasted Chicken With Rice" },
                    { 6, null, "Chicken On Charcoal With Rice" },
                    { 7, null, "Snacks" },
                    { 8, null, "Appetizers" },
                    { 9, null, "Drinks" }
                });

            migrationBuilder.InsertData(
                table: "LookupItems",
                columns: new[] { "ID", "LookupTypeId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Admin" },
                    { 2, 1, "Cashier" },
                    { 3, 1, "Customer" },
                    { 4, 1, "Delivery" },
                    { 5, 2, "Pending" },
                    { 6, 2, "In Progress" },
                    { 7, 2, "Completed" },
                    { 8, 2, "Canceld" },
                    { 9, 3, "Cash" },
                    { 10, 3, "Credit Card" },
                    { 11, 3, "Online Wallet" },
                    { 12, 4, "Paid" },
                    { 13, 4, "Unpaid" },
                    { 14, 5, "Pickup" },
                    { 15, 5, "Delivery" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "ID", "MenuItemName", "MenuTypeID", "Price" },
                values: new object[,]
                {
                    { 1, "Chicken Shawarma", 1, 3f },
                    { 2, "Beef Shawarma Plate", 1, 3f },
                    { 3, "Spicy Shawarma Wrap", 1, 3f },
                    { 4, "Half Roasted Chicken", 2, 3f },
                    { 5, "BBQ Glazed Roasted Chicken", 2, 3f },
                    { 6, "Lemon Pepper Roasted Chicken", 2, 3f },
                    { 7, "Charcoal-Grilled Whole Chicken", 3, 3f },
                    { 8, "Charcoal-Grilled Chicken Skewers", 3, 3f },
                    { 9, "Spicy Peri-Peri Charcoal Chicken", 3, 3f },
                    { 10, "Chicken Thighs with Garlic Butter", 4, 3f },
                    { 11, "Boneless Chicken Breast Fillets", 4, 3f },
                    { 12, "Drumsticks in Spicy Marinade", 4, 3f },
                    { 13, "Herb Roasted Chicken with Basmati Rice", 5, 3f },
                    { 14, "Roasted Chicken with Pilaf and Saffron", 5, 3f },
                    { 15, "BBQ Roasted Chicken over Brown Rice", 5, 3f },
                    { 16, "Charcoal Chicken with Turmeric Rice", 6, 3f },
                    { 17, "Grilled Chicken with Vegetable Rice", 6, 3f },
                    { 18, "Smoky Charcoal Chicken with Fried Rice", 6, 3f },
                    { 19, "French Fries with Dipping Sauces", 7, 3f },
                    { 20, "Mozzarella Sticks with Marinara", 7, 3f },
                    { 21, "Chicken Nuggets with Honey Mustard", 7, 3f },
                    { 22, "Hummus with Pita Bread", 8, 3f },
                    { 23, "Stuffed Grape Leaves", 8, 3f },
                    { 24, "Garlic Bread with Cheese", 8, 3f },
                    { 25, "Fresh Lemon Mint Cooler", 9, 3f },
                    { 26, "Iced Hibiscus Tea", 9, 3f },
                    { 27, "Sparkling Berry Lemonade", 9, 3f }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Name", "Password", "RoleId" },
                values: new object[] { 1, "The Admin", "AAAzzz111!", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_LookupItems_LookupTypeId",
                table: "LookupItems",
                column: "LookupTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_ID",
                table: "MenuItems",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_MenuTypeID",
                table: "MenuItems",
                column: "MenuTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuTypes_ID",
                table: "MenuTypes",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusID",
                table: "Orders",
                column: "OrderStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentMethodID",
                table: "Orders",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentStatusID",
                table: "Orders",
                column: "PaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PickUpTypeID",
                table: "Orders",
                column: "PickUpTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "MenuTypes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "LookupItems");

            migrationBuilder.DropTable(
                name: "LookupTypes");
        }
    }
}
