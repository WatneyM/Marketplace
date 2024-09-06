using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class M04AddOrderInfrastructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a527f8a9-797b-4c9f-b9ec-93b0f02801d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efa215e0-c035-4947-be52-b81cab552190");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b5fa7ed1-b451-4c71-8605-a2aa06af1b3d", "58bcf5bf-c05b-4d71-9223-35bec0c9ac25" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5fa7ed1-b451-4c71-8605-a2aa06af1b3d");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "58bcf5bf-c05b-4d71-9223-35bec0c9ac25");

            migrationBuilder.AlterColumn<string>(
                name: "Short Description",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductStorage",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified At",
                table: "ProductStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 10)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Long Description",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created At",
                table: "ProductStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.AlterColumn<string>(
                name: "Attached Category Key",
                table: "ProductStorage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AddColumn<string>(
                name: "Product Code",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: true)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.CreateTable(
                name: "OrderStorage",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderNo = table.Column<string>(name: "Order No", type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(name: "Order Status", type: "int", nullable: false),
                    AttachedProduct = table.Column<string>(name: "Attached Product", type: "nvarchar(max)", nullable: true),
                    AttachedUser = table.Column<string>(name: "Attached User", type: "nvarchar(max)", nullable: true),
                    OrderedAt = table.Column<DateTime>(name: "Ordered At", type: "datetime2", nullable: true),
                    OrderedAmount = table.Column<int>(name: "Ordered Amount", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStorage", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "StoredOrderStorage",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderNo = table.Column<string>(name: "Order No", type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<int>(name: "Order Status", type: "int", nullable: false),
                    AttachedProduct = table.Column<string>(name: "Attached Product", type: "nvarchar(max)", nullable: true),
                    AttachedUser = table.Column<string>(name: "Attached User", type: "nvarchar(max)", nullable: true),
                    OrderedAt = table.Column<DateTime>(name: "Ordered At", type: "datetime2", nullable: true),
                    UnorderedAt = table.Column<DateTime>(name: "Unordered At", type: "datetime2", nullable: true),
                    OrderedAmount = table.Column<int>(name: "Ordered Amount", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredOrderStorage", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "SupplyStorage",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AvailableAmount = table.Column<int>(name: "Available Amount", type: "int", nullable: false),
                    AttachedToCategory = table.Column<string>(name: "Attached To Category", type: "nvarchar(max)", nullable: true),
                    AttachedProduct = table.Column<string>(name: "Attached Product", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyStorage", x => x.Key);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStorage");

            migrationBuilder.DropTable(
                name: "StoredOrderStorage");

            migrationBuilder.DropTable(
                name: "SupplyStorage");

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

            migrationBuilder.DropColumn(
                name: "Product Code",
                table: "ProductStorage");

            migrationBuilder.AlterColumn<string>(
                name: "Short Description",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 3)
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "Product",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 2)
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductStorage",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2)
                .Annotation("Relational:ColumnOrder", 6)
                .OldAnnotation("Relational:ColumnOrder", 7);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Modified At",
                table: "ProductStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 9)
                .OldAnnotation("Relational:ColumnOrder", 10);

            migrationBuilder.AlterColumn<string>(
                name: "Long Description",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true)
                .Annotation("Relational:ColumnOrder", 4)
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "ProductStorage",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .Annotation("Relational:ColumnOrder", 5)
                .OldAnnotation("Relational:ColumnOrder", 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created At",
                table: "ProductStorage",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2")
                .Annotation("Relational:ColumnOrder", 8)
                .OldAnnotation("Relational:ColumnOrder", 9);

            migrationBuilder.AlterColumn<string>(
                name: "Attached Category Key",
                table: "ProductStorage",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 7)
                .OldAnnotation("Relational:ColumnOrder", 8);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a527f8a9-797b-4c9f-b9ec-93b0f02801d9", "e71f97e1-aaf9-470e-9018-e89e5d5f8e1f", "Unverified", "UNVERIFIED" },
                    { "b5fa7ed1-b451-4c71-8605-a2aa06af1b3d", "601731a4-19d9-4ced-b77b-0849c385e05c", "Administrator", "ADMINISTRATOR" },
                    { "efa215e0-c035-4947-be52-b81cab552190", "ed7bbd7c-a9e9-44fe-921c-a9b4236033d5", "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Firstname", "Lastname", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "58bcf5bf-c05b-4d71-9223-35bec0c9ac25", 0, "9ec87343-1f5d-4b4e-ad1d-d2c9e2b503fa", "admin@gmail.com", false, null, null, false, null, "ADMIN@GMAIL.COM", "ADMINISTRATOR", "AQAAAAIAAYagAAAAEJgJklgX0KeHsU6dtqLl5SEUaoLHgp3cTNWzl8zUTlDTTkzWgjIqq/h1+1+qzIwSZA==", "1234567890", false, "9b8e63bf-ae6d-48d4-ab2c-c1e9213648da", false, "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b5fa7ed1-b451-4c71-8605-a2aa06af1b3d", "58bcf5bf-c05b-4d71-9223-35bec0c9ac25" });
        }
    }
}
