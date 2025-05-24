using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class FixInvoiceModelForJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Products_ProductId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ProductId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("ef702608-46e9-48e5-9286-cc0cd7177c72"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("c47eddf7-97d9-475f-be7a-0fc2736ec58d"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("38430b4f-d8fe-4166-9ddf-1e743d2f028f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("eaa817cf-7830-442b-9e30-9ceb411539d5"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("4270c076-dea4-435b-ac58-bc5c432c0970"));

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("edb8c5bb-3be4-4c21-b521-6f02d6bf0f2c"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("475f410b-1da9-43b5-aad5-6a44147f9d6d"), null, new DateTime(2025, 6, 23, 21, 13, 27, 25, DateTimeKind.Utc).AddTicks(9896), new DateTime(2025, 5, 24, 21, 13, 27, 25, DateTimeKind.Utc).AddTicks(9894), "INV001", 1000.00m, 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("e819cb00-0074-4adc-9d9c-d08531259263"), 1000.00m, new DateTime(2025, 5, 24, 21, 13, 27, 26, DateTimeKind.Utc).AddTicks(142), "PayPal" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("adcb50f8-d9d8-4f5d-b1a4-4d8b607bb4d0"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("6458395e-5c2e-418e-b601-d9996e12a0a6"), "A service that cleans your house.", "Cleaning Service" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("edb8c5bb-3be4-4c21-b521-6f02d6bf0f2c"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("475f410b-1da9-43b5-aad5-6a44147f9d6d"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("e819cb00-0074-4adc-9d9c-d08531259263"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("adcb50f8-d9d8-4f5d-b1a4-4d8b607bb4d0"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("6458395e-5c2e-418e-b601-d9996e12a0a6"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Invoices",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Invoices",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("ef702608-46e9-48e5-9286-cc0cd7177c72"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "ProductId", "ServiceId", "Status" },
                values: new object[] { new Guid("c47eddf7-97d9-475f-be7a-0fc2736ec58d"), null, new DateTime(2025, 6, 22, 4, 14, 57, 516, DateTimeKind.Utc).AddTicks(9937), new DateTime(2025, 5, 23, 4, 14, 57, 516, DateTimeKind.Utc).AddTicks(9937), "INV001", 1000.00m, null, null, 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("38430b4f-d8fe-4166-9ddf-1e743d2f028f"), 1000.00m, new DateTime(2025, 5, 23, 4, 14, 57, 517, DateTimeKind.Utc).AddTicks(82), "PayPal" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("eaa817cf-7830-442b-9e30-9ceb411539d5"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("4270c076-dea4-435b-ac58-bc5c432c0970"), "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProductId",
                table: "Invoices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ServiceId",
                table: "Invoices",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Products_ProductId",
                table: "Invoices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Services_ServiceId",
                table: "Invoices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }
    }
}
