using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class DatabaseFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Engine",
                table: "tCar");

            migrationBuilder.RenameTable(
                name: "tBrandSource",
                newName: "tBrandKey");

            migrationBuilder.RenameIndex(
                name: "IX_tBrandSource_Fk_SourceId",
                table: "tBrandKey",
                newName: "IX_tBrandKey_Fk_SourceId");

            migrationBuilder.AddColumn<string>(
                name: "FuelType",
                table: "tCar",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tBrand",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "Uq_Indx_Brand_Name",
                table: "tBrand",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Uq_Indx_Brand_Name",
                table: "tBrand");

            migrationBuilder.DropColumn(
                name: "FuelType",
                table: "tCar");

            migrationBuilder.RenameTable(
                name: "tBrandKey",
                newName: "tBrandSource");

            migrationBuilder.RenameIndex(
                name: "IX_tBrandKey_Fk_SourceId",
                table: "tBrandSource",
                newName: "IX_tBrandSource_Fk_SourceId");

            migrationBuilder.AddColumn<string>(
                name: "Engine",
                table: "tCar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "tBrand",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
