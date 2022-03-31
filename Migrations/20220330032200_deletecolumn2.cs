using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    public partial class deletecolumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Membres_MembreEmail",
                table: "Notifications");

            migrationBuilder.UpdateData(
                table: "Notifications",
                keyColumn: "MembreEmail",
                keyValue: null,
                column: "MembreEmail",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MembreEmail",
                table: "Notifications",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Membres_MembreEmail",
                table: "Notifications",
                column: "MembreEmail",
                principalTable: "Membres",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Membres_MembreEmail",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "MembreEmail",
                table: "Notifications",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Membres_MembreEmail",
                table: "Notifications",
                column: "MembreEmail",
                principalTable: "Membres",
                principalColumn: "Email");
        }
    }
}
