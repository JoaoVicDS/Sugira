using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sugira.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacteristicType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacteristicOption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacteristicTypeId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacteristicOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacteristicOption_CharacteristicType_CharacteristicTypeId",
                        column: x => x.CharacteristicTypeId,
                        principalTable: "CharacteristicType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemCharacteristic",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicTypeId = table.Column<int>(type: "integer", nullable: false),
                    CharacteristicOptionId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCharacteristic", x => new { x.ItemId, x.CharacteristicTypeId, x.CharacteristicOptionId });
                    table.ForeignKey(
                        name: "FK_ItemCharacteristic_CharacteristicOption_CharacteristicOptio~",
                        column: x => x.CharacteristicOptionId,
                        principalTable: "CharacteristicOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristic_CharacteristicType_CharacteristicTypeId",
                        column: x => x.CharacteristicTypeId,
                        principalTable: "CharacteristicType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCharacteristic_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPhoto_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CharacteristicType",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7162), true, "Amargura" },
                    { 2, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7805), true, "Doçura" },
                    { 3, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7808), true, "Teor Alcoólico" },
                    { 4, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7809), true, "Sabor" },
                    { 5, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7809), true, "Textura" },
                    { 6, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7810), true, "Cor" },
                    { 7, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7811), true, "Carbonatação" },
                    { 8, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7812), true, "Ponto" },
                    { 9, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7813), true, "Temperatura" },
                    { 10, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7814), true, "Tempero" },
                    { 11, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7814), true, "Acidez" },
                    { 12, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7815), true, "Ardência" },
                    { 13, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7816), true, "Volume" },
                    { 14, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7817), true, "Tamanho" },
                    { 15, new DateTime(2025, 8, 29, 16, 4, 53, 860, DateTimeKind.Utc).AddTicks(7818), true, "Molho" }
                });

            migrationBuilder.InsertData(
                table: "CharacteristicOption",
                columns: new[] { "Id", "CharacteristicTypeId", "CreatedAt", "IsActive", "Value" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(4553), true, "Leve" },
                    { 2, 1, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5276), true, "Moderado" },
                    { 3, 1, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5278), true, "Amargo" },
                    { 4, 1, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5279), true, "Intenso" },
                    { 5, 2, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5280), true, "Baixo" },
                    { 6, 2, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5281), true, "Equilibrado" },
                    { 7, 2, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5282), true, "Alto" },
                    { 8, 3, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5283), true, "Baixo" },
                    { 9, 3, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5284), true, "Moderado" },
                    { 10, 3, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5286), true, "Alto" },
                    { 11, 3, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5287), true, "Muito alto" },
                    { 12, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5287), true, "Adocicado" },
                    { 13, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5288), true, "Cítrico" },
                    { 14, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5289), true, "Torrado" },
                    { 15, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5290), true, "Grãos" },
                    { 16, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5291), true, "Salgado" },
                    { 17, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5292), true, "Herbal" },
                    { 18, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5293), true, "Defumado" },
                    { 19, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5294), true, "Frutado" },
                    { 20, 4, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5294), true, "Maltado" },
                    { 21, 5, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5295), true, "Leve" },
                    { 22, 5, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5296), true, "Cremoso" },
                    { 23, 5, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5297), true, "Seco" },
                    { 24, 5, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5298), true, "Encorpado" },
                    { 25, 5, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5313), true, "Aveludado" },
                    { 26, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5313), true, "Clara" },
                    { 27, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5314), true, "Dourada" },
                    { 28, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5315), true, "Âmbar" },
                    { 29, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5316), true, "Cobre" },
                    { 30, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5317), true, "Marrom" },
                    { 31, 6, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5318), true, "Preta" },
                    { 32, 7, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5319), true, "Baixa" },
                    { 33, 7, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5363), true, "Média" },
                    { 34, 7, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5364), true, "Alta" },
                    { 35, 8, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5365), true, "Mal passado" },
                    { 36, 8, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5366), true, "Ao ponto" },
                    { 37, 8, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5367), true, "Bem passado" },
                    { 38, 9, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5367), true, "Frio" },
                    { 39, 9, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5368), true, "Morno" },
                    { 40, 9, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5369), true, "Quente" },
                    { 41, 10, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5370), true, "Sem tempero" },
                    { 42, 10, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5371), true, "Leve" },
                    { 43, 10, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5372), true, "Moderado" },
                    { 44, 10, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5373), true, "Intenso" },
                    { 45, 11, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5374), true, "Baixa" },
                    { 46, 11, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5374), true, "Moderada" },
                    { 47, 11, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5375), true, "Alta" },
                    { 48, 12, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5376), true, "Sem ardência" },
                    { 49, 12, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5377), true, "Leve" },
                    { 50, 12, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5378), true, "Moderada" },
                    { 51, 12, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5379), true, "Alta" },
                    { 52, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5379), true, "269ml" },
                    { 53, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5380), true, "350ml" },
                    { 54, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5381), true, "355ml" },
                    { 55, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5382), true, "473ml" },
                    { 56, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5383), true, "600ml" },
                    { 57, 13, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5384), true, "1L" },
                    { 58, 14, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5384), true, "Pequeno" },
                    { 59, 14, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5385), true, "Médio" },
                    { 60, 14, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5386), true, "Grande" },
                    { 61, 15, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5387), true, "Com molho" },
                    { 62, 15, new DateTime(2025, 8, 29, 16, 4, 53, 862, DateTimeKind.Utc).AddTicks(5388), true, "Sem molho" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_MenuId",
                table: "Category",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacteristicOption_CharacteristicTypeId",
                table: "CharacteristicOption",
                column: "CharacteristicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristic_CharacteristicOptionId",
                table: "ItemCharacteristic",
                column: "CharacteristicOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCharacteristic_CharacteristicTypeId",
                table: "ItemCharacteristic",
                column: "CharacteristicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPhoto_ItemId",
                table: "ItemPhoto",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCharacteristic");

            migrationBuilder.DropTable(
                name: "ItemPhoto");

            migrationBuilder.DropTable(
                name: "CharacteristicOption");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "CharacteristicType");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
