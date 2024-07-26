using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class M01RenameAttributeValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeDataStorage");

            migrationBuilder.CreateTable(
                name: "ProductAttributeStorage",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachedProductKey = table.Column<string>(name: "Attached Product Key", type: "nvarchar(450)", nullable: false),
                    AttachedAttributeKey = table.Column<string>(name: "Attached Attribute Key", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttributeStorage", x => x.Key);
                    table.ForeignKey(
                        name: "FK_ProductAttributeStorage_AttributeStorage_Attached Attribute Key",
                        column: x => x.AttachedAttributeKey,
                        principalTable: "AttributeStorage",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttributeStorage_ProductStorage_Attached Product Key",
                        column: x => x.AttachedProductKey,
                        principalTable: "ProductStorage",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeStorage_Attached Attribute Key",
                table: "ProductAttributeStorage",
                column: "Attached Attribute Key");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributeStorage_Attached Product Key",
                table: "ProductAttributeStorage",
                column: "Attached Product Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAttributeStorage");

            migrationBuilder.CreateTable(
                name: "AttributeDataStorage",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachedProductKey = table.Column<string>(name: "Attached Product Key", type: "nvarchar(450)", nullable: false),
                    AttachedAttributeKey = table.Column<string>(name: "Attached Attribute Key", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDataStorage", x => x.Key);
                    table.ForeignKey(
                        name: "FK_AttributeDataStorage_AttributeStorage_Attached Attribute Key",
                        column: x => x.AttachedAttributeKey,
                        principalTable: "AttributeStorage",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeDataStorage_ProductStorage_Attached Product Key",
                        column: x => x.AttachedProductKey,
                        principalTable: "ProductStorage",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDataStorage_Attached Attribute Key",
                table: "AttributeDataStorage",
                column: "Attached Attribute Key");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeDataStorage_Attached Product Key",
                table: "AttributeDataStorage",
                column: "Attached Product Key");
        }
    }
}
