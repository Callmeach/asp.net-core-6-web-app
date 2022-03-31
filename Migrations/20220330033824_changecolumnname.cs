using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    public partial class changecolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Membres_Email",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Notifications",
                newName: "MembreEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_Email",
                table: "Notifications",
                newName: "IX_Notifications_MembreEmail");

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

            migrationBuilder.RenameColumn(
                name: "MembreEmail",
                table: "Notifications",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_MembreEmail",
                table: "Notifications",
                newName: "IX_Notifications_Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Membres_Email",
                table: "Notifications",
                column: "Email",
                principalTable: "Membres",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
