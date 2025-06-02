using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class FixInvoiceServicesRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("ba0c3e1d-6904-4f7f-9f42-5046d220e1f6"), new Guid("ab346909-c864-466b-a5a9-916be340e134") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("01a315b2-a758-4fb4-9cb5-e7f7e3bceb16"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("b414de90-098c-4fdc-96e2-9579b2555870"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("ab346909-c864-466b-a5a9-916be340e134"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("ba0c3e1d-6904-4f7f-9f42-5046d220e1f6"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("b6ff38ab-175b-4de3-b053-7f67c39b4e5c"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("4ba1b208-0e45-4fcf-9e38-4d41560236cf"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("d00a14cf-2201-4bbf-a041-2e9f6d9fa8cf"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("9be6f733-87cd-4d7b-80b2-5a34419d5e57"), "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("de4e73b3-07cb-4c11-a09d-804c2f66db38"), new Guid("4ba1b208-0e45-4fcf-9e38-4d41560236cf"), new DateTime(2025, 7, 2, 1, 41, 13, 234, DateTimeKind.Utc).AddTicks(3783), new DateTime(2025, 6, 2, 1, 41, 13, 234, DateTimeKind.Utc).AddTicks(3782), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("07426956-44c3-4ec4-9c20-ecfcf28a9ca3"), new Guid("de4e73b3-07cb-4c11-a09d-804c2f66db38"), 1000.00m, new DateTime(2025, 6, 2, 1, 41, 13, 234, DateTimeKind.Utc).AddTicks(3935), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("de4e73b3-07cb-4c11-a09d-804c2f66db38"), new Guid("07426956-44c3-4ec4-9c20-ecfcf28a9ca3"), 1000.00m, new DateTime(2025, 6, 2, 1, 41, 13, 234, DateTimeKind.Utc).AddTicks(3948) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("de4e73b3-07cb-4c11-a09d-804c2f66db38"), new Guid("07426956-44c3-4ec4-9c20-ecfcf28a9ca3") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d00a14cf-2201-4bbf-a041-2e9f6d9fa8cf"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("9be6f733-87cd-4d7b-80b2-5a34419d5e57"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("07426956-44c3-4ec4-9c20-ecfcf28a9ca3"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("de4e73b3-07cb-4c11-a09d-804c2f66db38"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("4ba1b208-0e45-4fcf-9e38-4d41560236cf"));

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
        }
    }
}
