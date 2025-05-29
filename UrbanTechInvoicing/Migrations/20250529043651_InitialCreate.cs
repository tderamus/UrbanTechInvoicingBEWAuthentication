using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InvoiceTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceProducts",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductLineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceProducts", x => new { x.InvoiceId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceServices",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceLineAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    InvoiceQuantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceServices", x => new { x.InvoiceId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => new { x.InvoiceId, x.PaymentId });
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicePayments_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("b6ff38ab-175b-4de3-b053-7f67c39b4e5c"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("01a315b2-a758-4fb4-9cb5-e7f7e3bceb16"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("b414de90-098c-4fdc-96e2-9579b2555870"), "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("ba0c3e1d-6904-4f7f-9f42-5046d220e1f6"), new Guid("b6ff38ab-175b-4de3-b053-7f67c39b4e5c"), new DateTime(2025, 6, 28, 4, 36, 51, 409, DateTimeKind.Utc).AddTicks(2789), new DateTime(2025, 5, 29, 4, 36, 51, 409, DateTimeKind.Utc).AddTicks(2787), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("ab346909-c864-466b-a5a9-916be340e134"), new Guid("ba0c3e1d-6904-4f7f-9f42-5046d220e1f6"), 1000.00m, new DateTime(2025, 5, 29, 4, 36, 51, 409, DateTimeKind.Utc).AddTicks(2936), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("ba0c3e1d-6904-4f7f-9f42-5046d220e1f6"), new Guid("ab346909-c864-466b-a5a9-916be340e134"), 1000.00m, new DateTime(2025, 5, 29, 4, 36, 51, 409, DateTimeKind.Utc).AddTicks(2950) });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePayments_PaymentId",
                table: "InvoicePayments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceProducts_ProductId",
                table: "InvoiceProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_ServiceId",
                table: "InvoiceServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.DropTable(
                name: "InvoiceProducts");

            migrationBuilder.DropTable(
                name: "InvoiceServices");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
