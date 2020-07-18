using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class RenameCarFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirConditioner",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "ElectricMirrors",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "ElectricWindows",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Stereo",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "ABS",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Airbag",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Alarm",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "ESP",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "HalogenHeadlights",
                table: "tCar");

            migrationBuilder.AddColumn<bool>(
                name: "HasAirConditioner",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricMirrors",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasElectricWindows",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasStereo",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasABS",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAirbag",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAlarm",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasESP",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasHalogenHeadlights",
                table: "tCar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "AirConditioner",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ElectricMirrors",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ElectricWindows",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Stereo",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ABS",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Airbag",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Alarm",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ESP",
                table: "tCar",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HalogenHeadlights",
                table: "tCar",
                type: "bit",
                nullable: true);
        }
    }
}
