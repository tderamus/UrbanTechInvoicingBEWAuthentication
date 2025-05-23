using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class CovertPaymentTypeToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("51366f44-f09f-4d92-8a2e-56f6e9b82f19"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("69d1928b-1a19-4a66-8fb6-7eb7754db204"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("5818143c-83df-4072-ba90-22dfd68bc82b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("395613b6-8dfe-4817-950c-66a97682ab6a"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("daa36c5e-18a7-4aed-8e60-c07e14a1b158"));

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "Payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("51366f44-f09f-4d92-8a2e-56f6e9b82f19"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "ProductId", "ServiceId", "Status" },
                values: new object[] { new Guid("69d1928b-1a19-4a66-8fb6-7eb7754db204"), null, new DateTime(2025, 6, 20, 1, 39, 29, 538, DateTimeKind.Utc).AddTicks(1014), new DateTime(2025, 5, 21, 1, 39, 29, 538, DateTimeKind.Utc).AddTicks(1012), "INV001", 1000.00m, null, null, 1 });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("5818143c-83df-4072-ba90-22dfd68bc82b"), 1000.00m, new DateTime(2025, 5, 21, 1, 39, 29, 538, DateTimeKind.Utc).AddTicks(1185), 4 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("395613b6-8dfe-4817-950c-66a97682ab6a"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("daa36c5e-18a7-4aed-8e60-c07e14a1b158"), "A service that cleans your house.", "Cleaning Service" });
        }
    }
}
