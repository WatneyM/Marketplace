using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class M03AddIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");
        }
    }
}
