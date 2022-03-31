using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    public partial class chgclmtype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPublication",
                table: "Notifications",
                newName: "PersonalizedClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PersonalizedClassId",
                table: "Notifications",
                column: "PersonalizedClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Personalizeds_PersonalizedClassId",
                table: "Notifications",
                column: "PersonalizedClassId",
                principalTable: "Personalizeds",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Personalizeds_PersonalizedClassId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PersonalizedClassId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "PersonalizedClassId",
                table: "Notifications",
                newName: "IdPublication");
        }
    }
}
