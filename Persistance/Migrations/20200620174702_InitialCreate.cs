using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tBrand",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tSource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sk_ModelId = table.Column<int>(nullable: false),
                    Fk_BrandId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Model", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Model_Brand",
                        column: x => x.Fk_BrandId,
                        principalTable: "tBrand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Sk_SeriesModel",
                        column: x => x.Sk_ModelId,
                        principalTable: "tModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tBrandSource",
                columns: table => new
                {
                    Fk_BrandId = table.Column<int>(nullable: false),
                    Fk_SourceId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_BrandSource", x => new { x.Fk_BrandId, x.Fk_SourceId });
                    table.ForeignKey(
                        name: "Fk_BrandKey_Brand",
                        column: x => x.Fk_BrandId,
                        principalTable: "tBrand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_BrandKey_Source",
                        column: x => x.Fk_SourceId,
                        principalTable: "tSource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tCar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_BrandId = table.Column<int>(nullable: false),
                    Fk_ModelId = table.Column<int>(nullable: false),
                    Fk_SourceId = table.Column<int>(nullable: false),
                    AdditionalInfo = table.Column<string>(nullable: true),
                    AdUrl = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Engine = table.Column<string>(nullable: true),
                    Transmision = table.Column<string>(nullable: true),
                    Millage = table.Column<int>(nullable: true),
                    IsUsed = table.Column<bool>(nullable: true),
                    ManufactureDate = table.Column<DateTime>(nullable: true),
                    EngineDisplacement = table.Column<int>(nullable: true),
                    AirConditioner = table.Column<bool>(nullable: true),
                    ElectricWindows = table.Column<bool>(nullable: true),
                    ElectricMirrors = table.Column<bool>(nullable: true),
                    Stereo = table.Column<bool>(nullable: true),
                    ABS = table.Column<bool>(nullable: true),
                    ESP = table.Column<bool>(nullable: true),
                    Airbag = table.Column<bool>(nullable: true),
                    HalogenHeadlights = table.Column<bool>(nullable: true),
                    Alarm = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_Car", x => x.Id);
                    table.ForeignKey(
                        name: "Fk_Car_Brand",
                        column: x => x.Fk_BrandId,
                        principalTable: "tBrand",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Car_Model",
                        column: x => x.Fk_ModelId,
                        principalTable: "tModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_Car_Source",
                        column: x => x.Fk_SourceId,
                        principalTable: "tSource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tModelKey",
                columns: table => new
                {
                    Fk_ModelId = table.Column<int>(nullable: false),
                    Fk_SourceId = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pk_ModelKey", x => new { x.Fk_ModelId, x.Fk_SourceId });
                    table.ForeignKey(
                        name: "Fk_ModelKey_Model",
                        column: x => x.Fk_ModelId,
                        principalTable: "tModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Fk_ModelKey_Source",
                        column: x => x.Fk_SourceId,
                        principalTable: "tSource",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tBrandSource_Fk_SourceId",
                table: "tBrandSource",
                column: "Fk_SourceId");

            migrationBuilder.CreateIndex(
                name: "UnIndx_AdUrl",
                table: "tCar",
                column: "AdUrl",
                unique: true,
                filter: "[AdUrl] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tCar_Fk_BrandId",
                table: "tCar",
                column: "Fk_BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_tCar_Fk_ModelId",
                table: "tCar",
                column: "Fk_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tCar_Fk_SourceId",
                table: "tCar",
                column: "Fk_SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_tModel_Fk_BrandId",
                table: "tModel",
                column: "Fk_BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_tModel_Sk_ModelId",
                table: "tModel",
                column: "Sk_ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tModelKey_Fk_SourceId",
                table: "tModelKey",
                column: "Fk_SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tBrandSource");

            migrationBuilder.DropTable(
                name: "tCar");

            migrationBuilder.DropTable(
                name: "tModelKey");

            migrationBuilder.DropTable(
                name: "tModel");

            migrationBuilder.DropTable(
                name: "tSource");

            migrationBuilder.DropTable(
                name: "tBrand");
        }
    }
}
