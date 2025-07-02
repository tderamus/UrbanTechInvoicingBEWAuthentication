using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("159a822d-88ed-4c00-9320-18bd3e09b77e"), new Guid("11891631-b997-4a32-851b-0b3fbf233a99") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("189ee6e4-2e11-4232-9530-d9db940d94f6"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("2b72db3e-2b7b-44ee-a5ff-da24fc09b653"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("11891631-b997-4a32-851b-0b3fbf233a99"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("159a822d-88ed-4c00-9320-18bd3e09b77e"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("327bfd40-0f28-4299-8351-c19e98a20492"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1-guid",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9b7ec731-1ba5-455b-967c-86ebc390934d", "cf80f017-4b8d-4a7f-b4e2-ba5f076a36b8" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatorUserId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("f93bf1eb-9e9b-4859-a15a-09cd3654e216"), "user1-guid", "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatorUserId", "Description", "ProductName" },
                values: new object[] { new Guid("8cdf8775-1a11-41ca-9ce7-7165be03c9b2"), "user1-guid", "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "CreatorUserId", "Description", "ServiceName" },
                values: new object[] { new Guid("974bedc7-00b0-4dff-9d79-5c2a740b822a"), "user1-guid", "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreatorUserId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("a2d23f23-00b4-432a-995d-43cd78016821"), null, new Guid("f93bf1eb-9e9b-4859-a15a-09cd3654e216"), new DateTime(2025, 7, 25, 3, 48, 49, 809, DateTimeKind.Utc).AddTicks(7376), new DateTime(2025, 6, 25, 3, 48, 49, 809, DateTimeKind.Utc).AddTicks(7374), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CreatorUserId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("8c507258-23c5-403e-b01a-8c5aa2faab22"), "user1-guid", new Guid("a2d23f23-00b4-432a-995d-43cd78016821"), 1000.00m, new DateTime(2025, 6, 25, 3, 48, 49, 809, DateTimeKind.Utc).AddTicks(7723), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("a2d23f23-00b4-432a-995d-43cd78016821"), new Guid("8c507258-23c5-403e-b01a-8c5aa2faab22"), 1000.00m, new DateTime(2025, 6, 25, 3, 48, 49, 809, DateTimeKind.Utc).AddTicks(7738) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("a2d23f23-00b4-432a-995d-43cd78016821"), new Guid("8c507258-23c5-403e-b01a-8c5aa2faab22") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8cdf8775-1a11-41ca-9ce7-7165be03c9b2"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("974bedc7-00b0-4dff-9d79-5c2a740b822a"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("8c507258-23c5-403e-b01a-8c5aa2faab22"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("a2d23f23-00b4-432a-995d-43cd78016821"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("f93bf1eb-9e9b-4859-a15a-09cd3654e216"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "user1-guid",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ebd52166-8274-4b50-b4c2-98ba978eb723", "eee54ef1-7654-4b3f-a620-2ff3a75dd078" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatorUserId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("327bfd40-0f28-4299-8351-c19e98a20492"), "user1-guid", "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatorUserId", "Description", "ProductName" },
                values: new object[] { new Guid("189ee6e4-2e11-4232-9530-d9db940d94f6"), "user1-guid", "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "CreatorUserId", "Description", "ServiceName" },
                values: new object[] { new Guid("2b72db3e-2b7b-44ee-a5ff-da24fc09b653"), "user1-guid", "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreatorUserId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("159a822d-88ed-4c00-9320-18bd3e09b77e"), null, new Guid("327bfd40-0f28-4299-8351-c19e98a20492"), new DateTime(2025, 7, 21, 3, 37, 57, 240, DateTimeKind.Utc).AddTicks(1614), new DateTime(2025, 6, 21, 3, 37, 57, 240, DateTimeKind.Utc).AddTicks(1610), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CreatorUserId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("11891631-b997-4a32-851b-0b3fbf233a99"), "user1-guid", new Guid("159a822d-88ed-4c00-9320-18bd3e09b77e"), 1000.00m, new DateTime(2025, 6, 21, 3, 37, 57, 240, DateTimeKind.Utc).AddTicks(2006), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("159a822d-88ed-4c00-9320-18bd3e09b77e"), new Guid("11891631-b997-4a32-851b-0b3fbf233a99"), 1000.00m, new DateTime(2025, 6, 21, 3, 37, 57, 240, DateTimeKind.Utc).AddTicks(2022) });
        }
    }
}
