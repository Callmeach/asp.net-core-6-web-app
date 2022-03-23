using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    public partial class @null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MembreMembres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MembreMembres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
