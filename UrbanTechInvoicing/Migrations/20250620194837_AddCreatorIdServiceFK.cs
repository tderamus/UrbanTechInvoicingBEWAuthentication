using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorIdServiceFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CreatorUserId", "EmailAddress", "Name", "PhoneNumber" },
                values: new object[] { new Guid("d5956c5c-2b37-4c56-af29-d61011213c0e"), null, "customer1@email.com", "Robots Inc", "1234567890" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CreatorUserId", "Description", "ProductName" },
                values: new object[] { new Guid("8e5a14dc-8a4e-40a3-b283-d741d1059da1"), null, "A robot that cleans your house.", "Robot Cleaner" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "CreatorUserId", "Description", "ServiceName" },
                values: new object[] { new Guid("e4e7a694-ff70-46ee-88e6-75ac6cd7ef6f"), null, "A service that cleans your house.", "Cleaning Service" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceId", "CreatorUserId", "CustomerId", "DueDate", "InvoiceDate", "InvoiceNumber", "InvoiceTotal", "Status" },
                values: new object[] { new Guid("e8c08e5d-7da1-498e-bc65-552ccb9fb96b"), null, new Guid("d5956c5c-2b37-4c56-af29-d61011213c0e"), new DateTime(2025, 7, 20, 19, 48, 36, 627, DateTimeKind.Utc).AddTicks(3308), new DateTime(2025, 6, 20, 19, 48, 36, 627, DateTimeKind.Utc).AddTicks(3305), "INV001", 1000.00m, "Unpaid" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "CreatorUserId", "InvoiceId", "PaymentAmount", "PaymentDate", "PaymentType" },
                values: new object[] { new Guid("95a85e6e-0fd0-45b4-836b-002876f2b044"), null, new Guid("e8c08e5d-7da1-498e-bc65-552ccb9fb96b"), 1000.00m, new DateTime(2025, 6, 20, 19, 48, 36, 627, DateTimeKind.Utc).AddTicks(3562), "CreditCard" });

            migrationBuilder.InsertData(
                table: "InvoicePayments",
                columns: new[] { "InvoiceId", "PaymentId", "PaymentAmount", "PaymentDate" },
                values: new object[] { new Guid("e8c08e5d-7da1-498e-bc65-552ccb9fb96b"), new Guid("95a85e6e-0fd0-45b4-836b-002876f2b044"), 1000.00m, new DateTime(2025, 6, 20, 19, 48, 36, 627, DateTimeKind.Utc).AddTicks(3579) });

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatorUserId",
                table: "Services",
                column: "CreatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_AspNetUsers_CreatorUserId",
                table: "Services",
                column: "CreatorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_AspNetUsers_CreatorUserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_CreatorUserId",
                table: "Services");

            migrationBuilder.DeleteData(
                table: "InvoicePayments",
                keyColumns: new[] { "InvoiceId", "PaymentId" },
                keyValues: new object[] { new Guid("e8c08e5d-7da1-498e-bc65-552ccb9fb96b"), new Guid("95a85e6e-0fd0-45b4-836b-002876f2b044") });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("8e5a14dc-8a4e-40a3-b283-d741d1059da1"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("e4e7a694-ff70-46ee-88e6-75ac6cd7ef6f"));

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: new Guid("95a85e6e-0fd0-45b4-836b-002876f2b044"));

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "InvoiceId",
                keyValue: new Guid("e8c08e5d-7da1-498e-bc65-552ccb9fb96b"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: new Guid("d5956c5c-2b37-4c56-af29-d61011213c0e"));

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
    }
}
