using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class RemoveCarBrandId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Fk_Car_Brand",
                table: "tCar");

            migrationBuilder.DropIndex(
                name: "IX_tCar_Fk_BrandId",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Fk_BrandId",
                table: "tCar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
