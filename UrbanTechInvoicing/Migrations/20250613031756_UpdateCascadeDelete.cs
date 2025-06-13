using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("452dba41-7519-4bd4-868d-338c5a955552"), new Guid("3328a7fb-cae7-4ca1-b830-34fb6884c676") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("a9315d8b-8170-4d1a-b147-ebe905ff2085"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("21987d0d-c8d8-4c74-97d3-d0d2cf1d084e"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("3328a7fb-cae7-4ca1-b830-34fb6884c676"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("452dba41-7519-4bd4-868d-338c5a955552"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("b88b365a-2fd6-48f9-ac8f-ce4be314e87d"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("d2c33367-10c3-420a-99bc-2ca7ce8b4e42"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("d5d83aa3-ee09-48da-999f-2d4d3a6a75ad"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("d24429fe-1d50-4e92-8584-f625041e76b6"), "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("fa794c98-8d47-4ff0-9ae2-2f34fe7dcf26"), new Guid("d2c33367-10c3-420a-99bc-2ca7ce8b4e42"), new DateTime(2025, 7, 13, 3, 17, 56, 358, DateTimeKind.Utc).AddTicks(5326), new DateTime(2025, 6, 13, 3, 17, 56, 358, DateTimeKind.Utc).AddTicks(5325), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("9adb5f53-33d6-478b-becb-e648bbc1b284"), new Guid("fa794c98-8d47-4ff0-9ae2-2f34fe7dcf26"), 1000.00m, new DateTime(2025, 6, 13, 3, 17, 56, 358, DateTimeKind.Utc).AddTicks(5490), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("fa794c98-8d47-4ff0-9ae2-2f34fe7dcf26"), new Guid("9adb5f53-33d6-478b-becb-e648bbc1b284"), 1000.00m, new DateTime(2025, 6, 13, 3, 17, 56, 358, DateTimeKind.Utc).AddTicks(5504) });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices");

            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("fa794c98-8d47-4ff0-9ae2-2f34fe7dcf26"), new Guid("9adb5f53-33d6-478b-becb-e648bbc1b284") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("d5d83aa3-ee09-48da-999f-2d4d3a6a75ad"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("d24429fe-1d50-4e92-8584-f625041e76b6"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("9adb5f53-33d6-478b-becb-e648bbc1b284"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("fa794c98-8d47-4ff0-9ae2-2f34fe7dcf26"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("d2c33367-10c3-420a-99bc-2ca7ce8b4e42"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("b88b365a-2fd6-48f9-ac8f-ce4be314e87d"), "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Description", "ProductName" },
                values: new object[] { new Guid("a9315d8b-8170-4d1a-b147-ebe905ff2085"), "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "Description", "ServiceName" },
                values: new object[] { new Guid("21987d0d-c8d8-4c74-97d3-d0d2cf1d084e"), "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("452dba41-7519-4bd4-868d-338c5a955552"), new Guid("b88b365a-2fd6-48f9-ac8f-ce4be314e87d"), new DateTime(2025, 7, 13, 3, 11, 27, 217, DateTimeKind.Utc).AddTicks(6136), new DateTime(2025, 6, 13, 3, 11, 27, 217, DateTimeKind.Utc).AddTicks(6135), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("3328a7fb-cae7-4ca1-b830-34fb6884c676"), new Guid("452dba41-7519-4bd4-868d-338c5a955552"), 1000.00m, new DateTime(2025, 6, 13, 3, 11, 27, 217, DateTimeKind.Utc).AddTicks(6303), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("452dba41-7519-4bd4-868d-338c5a955552"), new Guid("3328a7fb-cae7-4ca1-b830-34fb6884c676"), 1000.00m, new DateTime(2025, 6, 13, 3, 11, 27, 217, DateTimeKind.Utc).AddTicks(6317) });

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Customers_CustomerId",
                table: "Invoices",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId");
        }
    }
}
