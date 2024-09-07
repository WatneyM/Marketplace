using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class M05AddFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "575a4866-454a-45b8-bb9b-4788448cb666");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc50b8d3-b6a6-46b6-a910-9a33865f54aa");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a68b87f2-25b6-4670-88a7-8565a6d3cc1e", "47c76f67-dabb-4328-b778-9ef604f56bb7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a68b87f2-25b6-4670-88a7-8565a6d3cc1e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "47c76f67-dabb-4328-b778-9ef604f56bb7");

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "SupplyStorage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "StoredOrderStorage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "OrderStorage",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified At",
                table: "AttributeStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created At",
                table: "AttributeStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Attached To Group",
                table: "AttributeStorage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AddColumn<bool>(
                name: "Allow Filtering",
                table: "AttributeStorage",
                type: "bit",
                nullable: false,
                defaultValue: false)
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6b15cfdd-c98d-4e04-b790-1ca5474affae", "7f8bfe92-6287-4f49-8f2a-643f534fe812", "Unverified", "UNVERIFIED" },
                    { "a04c59e5-37ed-4797-a27c-18a22c58b75c", "845683bf-7c0d-4f7f-8fc0-4fed619ede0b", "Customer", "CUSTOMER" },
                    { "a69ec942-74e2-4a60-9741-38335a7a0dcb", "f93d865b-f6a7-47be-ae77-a792ac697868", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3c7d82c0-e62b-4419-8afa-0312ec732560", 0, "ed9e8914-96d6-411b-9cd5-2ec6cf875d56", "admin@gmail.com", false, "Marketplace", "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMINISTRATOR", "AQAAAAIAAYagAAAAEKeuAaRq5uEMCV9ssTPsi4bfOgCGCjUn4E6dtq4TeUnEMRDWQ9J+DGRkwkzlnXtieQ==", "1234567890", false, "cdd03b0b-4f5a-4cf6-a318-3f0249e21a63", false, "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a69ec942-74e2-4a60-9741-38335a7a0dcb", "3c7d82c0-e62b-4419-8afa-0312ec732560" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplyStorage_Attached Product",
                table: "SupplyStorage",
                column: "Attached Product");

            migrationBuilder.CreateIndex(
                name: "IX_StoredOrderStorage_Attached Product",
                table: "StoredOrderStorage",
                column: "Attached Product");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStorage_Attached Product",
                table: "OrderStorage",
                column: "Attached Product");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStorage_ProductStorage_Attached Product",
                table: "OrderStorage",
                column: "Attached Product",
                principalTable: "ProductStorage",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoredOrderStorage_ProductStorage_Attached Product",
                table: "StoredOrderStorage",
                column: "Attached Product",
                principalTable: "ProductStorage",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupplyStorage_ProductStorage_Attached Product",
                table: "SupplyStorage",
                column: "Attached Product",
                principalTable: "ProductStorage",
                principalColumn: "Key",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStorage_ProductStorage_Attached Product",
                table: "OrderStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_StoredOrderStorage_ProductStorage_Attached Product",
                table: "StoredOrderStorage");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplyStorage_ProductStorage_Attached Product",
                table: "SupplyStorage");

            migrationBuilder.DropIndex(
                name: "IX_SupplyStorage_Attached Product",
                table: "SupplyStorage");

            migrationBuilder.DropIndex(
                name: "IX_StoredOrderStorage_Attached Product",
                table: "StoredOrderStorage");

            migrationBuilder.DropIndex(
                name: "IX_OrderStorage_Attached Product",
                table: "OrderStorage");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b15cfdd-c98d-4e04-b790-1ca5474affae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a04c59e5-37ed-4797-a27c-18a22c58b75c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a69ec942-74e2-4a60-9741-38335a7a0dcb", "3c7d82c0-e62b-4419-8afa-0312ec732560" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a69ec942-74e2-4a60-9741-38335a7a0dcb");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3c7d82c0-e62b-4419-8afa-0312ec732560");

            migrationBuilder.DropColumn(
                name: "Allow Filtering",
                table: "AttributeStorage");

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "SupplyStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "StoredOrderStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Attached Product",
                table: "OrderStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified At",
                table: "AttributeStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created At",
                table: "AttributeStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Attached To Group",
                table: "AttributeStorage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "575a4866-454a-45b8-bb9b-4788448cb666", "e9674a75-1d68-4c0d-b05c-fb1d0e948eb4", "Customer", "CUSTOMER" },
                    { "a68b87f2-25b6-4670-88a7-8565a6d3cc1e", "732f45a6-7c94-4304-8eea-ae18ac97eaaf", "Administrator", "ADMINISTRATOR" },
                    { "dc50b8d3-b6a6-46b6-a910-9a33865f54aa", "04db2153-44fd-40d5-b511-47014def8b50", "Unverified", "UNVERIFIED" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "47c76f67-dabb-4328-b778-9ef604f56bb7", 0, "38a0b8cb-b383-4e34-afb9-1eb5c8cbe188", "admin@gmail.com", false, "Marketplace", "Administrator", false, null, "ADMIN@GMAIL.COM", "ADMINISTRATOR", "AQAAAAIAAYagAAAAEIYZMNKHzVYHmATj+GKLeywssvtsgp93NK8by7DkWJn3Y0kzIrR7UqrUFb+tUQHzcA==", "1234567890", false, "32381913-2f0b-4140-b5e7-c7ad1e24a3a1", false, "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a68b87f2-25b6-4670-88a7-8565a6d3cc1e", "47c76f67-dabb-4328-b778-9ef604f56bb7" });
        }
    }
}
