using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class FixedInvoiceStatusToStringValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("f288e266-3de9-4758-9b43-cd50c84b5481"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("8e155139-dc24-44e7-a1b9-1c3e93874729"), null, new DateTime(2025, 6, 27, 0, 35, 24, 773, DateTimeKind.Utc).AddTicks(2844), new DateTime(2025, 5, 28, 0, 35, 24, 773, DateTimeKind.Utc).AddTicks(2843), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("d743ff2e-5a73-4230-b801-4d9363553c37"), 1000.00m, new DateTime(2025, 5, 28, 0, 35, 24, 773, DateTimeKind.Utc).AddTicks(2999), "PayPal" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("ab8a861e-a842-45a9-a3ba-11c71f86b342"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("84163e65-e651-4ca5-b642-cadcdf68c43b"), "A service that cleans your house.", "Cleaning Service" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("f288e266-3de9-4758-9b43-cd50c84b5481"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("8e155139-dc24-44e7-a1b9-1c3e93874729"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("d743ff2e-5a73-4230-b801-4d9363553c37"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ab8a861e-a842-45a9-a3ba-11c71f86b342"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("84163e65-e651-4ca5-b642-cadcdf68c43b"));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
