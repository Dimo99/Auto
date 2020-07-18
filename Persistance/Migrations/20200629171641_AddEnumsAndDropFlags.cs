using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class AddEnumsAndDropFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAirConditioner",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasElectricMirrors",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasElectricWindows",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasStereo",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasABS",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasAirbag",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasAlarm",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasESP",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HasHalogenHeadlights",
                table: "tCar");

            migrationBuilder.RenameColumn(
                name: "CarSafetyInfo",
                table: "tCar",
                newName: "SafetyInfoString");

            migrationBuilder.RenameColumn(
                name: "CarComfortInfo",
                table: "tCar",
                newName: "ComfortInfoString");

            migrationBuilder.RenameColumn(
                name: "OtherInfo",
                table: "tCar",
                newName: "OtherInfoString");

            migrationBuilder.AddColumn<int>(
                name: "OtherInfo",
                table: "tCar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ComfortInfo",
                table: "tCar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SafetyInfo",
                table: "tCar",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
