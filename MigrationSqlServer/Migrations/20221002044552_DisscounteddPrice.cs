using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationSqlServer.Migrations
{
    public partial class DisscounteddPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountRate",
                table: "Products",
                newName: "DiscountedPrice");

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountedPrice",
                table: "Products",
                newName: "DiscountRate");

            migrationBuilder.AlterColumn<string>(
                name: "Descriptions",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
