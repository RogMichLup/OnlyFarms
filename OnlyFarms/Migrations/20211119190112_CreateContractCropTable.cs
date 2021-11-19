using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlyFarms.Migrations
{
    public partial class CreateContractCropTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CropID",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Crop",
                newName: "SellPricePerTonne");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellPricePerTonne",
                table: "Crop",
                newName: "Price");

            migrationBuilder.AddColumn<int>(
                name: "CropID",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
