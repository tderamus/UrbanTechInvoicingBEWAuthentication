using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UrbanTechInvoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
    }
}
