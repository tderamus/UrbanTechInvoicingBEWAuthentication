using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdFiltering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Invoices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorUserId",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatorUserId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("9ea35b29-3d63-4970-863a-c2b527be66fa"), null, "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatorUserId", "Description", "ProductName" },
                values: new object[] { new Guid("36679e0d-1614-4d56-a78a-c4d2ef6cb114"), null, "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "CreatorUserId", "Description", "ServiceName" },
                values: new object[] { new Guid("e213a62c-319a-46ef-9940-8268df11fbf5"), null, "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreatorUserId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("7ceca875-6d88-4600-ac25-9c2f708ae405"), null, new Guid("9ea35b29-3d63-4970-863a-c2b527be66fa"), new DateTime(2025, 7, 14, 3, 1, 31, 619, DateTimeKind.Utc).AddTicks(9240), new DateTime(2025, 6, 14, 3, 1, 31, 619, DateTimeKind.Utc).AddTicks(9238), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CreatorUserId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("deaae9cc-e4d9-4924-bc85-7c478bd2e7f3"), null, new Guid("7ceca875-6d88-4600-ac25-9c2f708ae405"), 1000.00m, new DateTime(2025, 6, 14, 3, 1, 31, 619, DateTimeKind.Utc).AddTicks(9421), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("7ceca875-6d88-4600-ac25-9c2f708ae405"), new Guid("deaae9cc-e4d9-4924-bc85-7c478bd2e7f3"), 1000.00m, new DateTime(2025, 6, 14, 3, 1, 31, 619, DateTimeKind.Utc).AddTicks(9441) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("7ceca875-6d88-4600-ac25-9c2f708ae405"), new Guid("deaae9cc-e4d9-4924-bc85-7c478bd2e7f3") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("36679e0d-1614-4d56-a78a-c4d2ef6cb114"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("e213a62c-319a-46ef-9940-8268df11fbf5"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("deaae9cc-e4d9-4924-bc85-7c478bd2e7f3"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("7ceca875-6d88-4600-ac25-9c2f708ae405"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("9ea35b29-3d63-4970-863a-c2b527be66fa"));

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Customers");

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
        }
    }
}
