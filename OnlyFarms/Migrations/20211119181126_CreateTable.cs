using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlyFarms.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCrop_Contract_ContractsID",
                table: "ContractCrop");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCrop_Crop_CropsID",
                table: "ContractCrop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractCrop",
                table: "ContractCrop");

            migrationBuilder.DropIndex(
                name: "IX_ContractCrop_CropsID",
                table: "ContractCrop");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Contract");

            migrationBuilder.RenameColumn(
                name: "AmortizationCost",
                table: "Machine",
                newName: "UtilizationCost");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Field",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "AmortizationCost",
                table: "Equipment",
                newName: "UtilizationCost");

            migrationBuilder.RenameColumn(
                name: "CropsID",
                table: "ContractCrop",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "ContractsID",
                table: "ContractCrop",
                newName: "CropID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HiringDate",
                table: "Worker",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Procedure",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DurationInHours",
                table: "Procedure",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Procedure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Machine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Field",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CropStatus",
                table: "Cultivation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "CropSale",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "ContractCrop",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ContractID",
                table: "ContractCrop",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractCrop",
                table: "ContractCrop",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCrop_ContractID",
                table: "ContractCrop",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractCrop_CropID",
                table: "ContractCrop",
                column: "CropID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCrop_Contract_ContractID",
                table: "ContractCrop",
                column: "ContractID",
                principalTable: "Contract",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCrop_Crop_CropID",
                table: "ContractCrop",
                column: "CropID",
                principalTable: "Crop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractCrop_Contract_ContractID",
                table: "ContractCrop");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractCrop_Crop_CropID",
                table: "ContractCrop");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractCrop",
                table: "ContractCrop");

            migrationBuilder.DropIndex(
                name: "IX_ContractCrop_ContractID",
                table: "ContractCrop");

            migrationBuilder.DropIndex(
                name: "IX_ContractCrop_CropID",
                table: "ContractCrop");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "ContractCrop");

            migrationBuilder.DropColumn(
                name: "ContractID",
                table: "ContractCrop");

            migrationBuilder.RenameColumn(
                name: "UtilizationCost",
                table: "Machine",
                newName: "AmortizationCost");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Field",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "UtilizationCost",
                table: "Equipment",
                newName: "AmortizationCost");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ContractCrop",
                newName: "CropsID");

            migrationBuilder.RenameColumn(
                name: "CropID",
                table: "ContractCrop",
                newName: "ContractsID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HiringDate",
                table: "Worker",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "Procedure",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<double>(
                name: "DurationInHours",
                table: "Procedure",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Procedure",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Machine",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "Field",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CropStatus",
                table: "Cultivation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "SaleDate",
                table: "CropSale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Contract",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractCrop",
                table: "ContractCrop",
                columns: new[] { "ContractsID", "CropsID" });

            migrationBuilder.CreateIndex(
                name: "IX_ContractCrop_CropsID",
                table: "ContractCrop",
                column: "CropsID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCrop_Contract_ContractsID",
                table: "ContractCrop",
                column: "ContractsID",
                principalTable: "Contract",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractCrop_Crop_CropsID",
                table: "ContractCrop",
                column: "CropsID",
                principalTable: "Crop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
