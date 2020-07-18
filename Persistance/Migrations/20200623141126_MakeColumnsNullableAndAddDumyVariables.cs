using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistance.Migrations
{
    public partial class MakeColumnsNullableAndAddDumyVariables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherInfo",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarComfortInfo",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDoors",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "tCar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CarSafetyInfo",
                table: "tCar",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherInfo",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "CarComfortInfo",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "NumberOfDoors",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "tCar");

            migrationBuilder.DropColumn(
                name: "CarSafetyInfo",
                table: "tCar");
        }
    }
}
