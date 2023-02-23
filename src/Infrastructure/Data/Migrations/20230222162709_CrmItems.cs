using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanCRM.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CrmItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CrmTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrmItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmItems_CrmTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CrmTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrmTypeFields",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CrmTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldType_Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmTypeFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmTypeFields_CrmTypes_CrmTypeId",
                        column: x => x.CrmTypeId,
                        principalTable: "CrmTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrmItemProperties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FieldId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmItemProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmItemProperties_CrmItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "CrmItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrmItemProperties_CrmTypeFields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "CrmTypeFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrmItemPropertyValues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Raw = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrmItemPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrmItemPropertyValues_CrmItemProperties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "CrmItemProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrmItemProperties_FieldId",
                table: "CrmItemProperties",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmItemProperties_ItemId",
                table: "CrmItemProperties",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmItemPropertyValues_PropertyId",
                table: "CrmItemPropertyValues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmItems_TypeId",
                table: "CrmItems",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CrmTypeFields_CrmTypeId",
                table: "CrmTypeFields",
                column: "CrmTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrmItemPropertyValues");

            migrationBuilder.DropTable(
                name: "CrmItemProperties");

            migrationBuilder.DropTable(
                name: "CrmItems");

            migrationBuilder.DropTable(
                name: "CrmTypeFields");

            migrationBuilder.DropTable(
                name: "CrmTypes");
        }
    }
}
