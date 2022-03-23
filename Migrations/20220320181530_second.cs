using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");


            migrationBuilder.CreateTable(
                name: "Membres",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nom = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenom = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membres", x => x.Email);
                    table.ForeignKey(
                        name: "FK_Membres_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MailMembre1 = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MailMembre2 = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MessageDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Containt = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Membres_MailMembre1",
                        column: x => x.MailMembre1,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Membres_MailMembre2",
                        column: x => x.MailMembre2,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InviterMail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvitedMail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InvDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Membres_InviterMail",
                        column: x => x.InviterMail,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Membres_InvitedMail",
                        column: x => x.InvitedMail,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MembreMembres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MailMembre1 = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MailMembre2 = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembreMembres", x => new { x.MailMembre1, x.MailMembre2 });
                    table.ForeignKey(
                        name: "FK_MembreMembres_Membres_MailMembre1",
                        column: x => x.MailMembre1,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembreMembres_Membres_MailMembre2",
                        column: x => x.MailMembre2,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    //table.UniqueConstraint("Unique_MembreMembres", x => new { x.MailMembre2, x.MailMembre1 } );
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ressource",
                columns: table => new
                {
                    RessourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nomRessource = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Discriminator = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    libelle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    legende = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taille = table.Column<long>(type: "bigint", nullable: true),
                    path = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressource", x => x.RessourceID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");




            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dateDePublication = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RessourceId = table.Column<int>(type: "int", nullable: false),
                    MembreEmail = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publications_Membres_MembreEmail",
                        column: x => x.MembreEmail,
                        principalTable: "Membres",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publications_Ressource_RessourceId",
                        column: x => x.RessourceId,
                        principalTable: "Ressource",
                        principalColumn: "RessourceID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateTable(
            //    name: "MakingPublications",
            //    columns: table => new
            //    {
            //        messageId = table.Column<int>(type: "int", nullable: false),
            //        lienId = table.Column<int>(type: "int", nullable: false),
            //        photoVideoId = table.Column<int>(type: "int", nullable: false),
            //        publicationId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.ForeignKey(
            //            name: "FK_MakingPublications_Publications_publicationId",
            //            column: x => x.publicationId,
            //            principalTable: "Publications",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MakingPublications_Ressource_lienId",
            //            column: x => x.lienId,
            //            principalTable: "Ressource",
            //            principalColumn: "RessourceID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MakingPublications_Ressource_messageId",
            //            column: x => x.messageId,
            //            principalTable: "Ressource",
            //            principalColumn: "RessourceID",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MakingPublications_Ressource_photoVideoId",
            //            column: x => x.photoVideoId,
            //            principalTable: "Ressource",
            //            principalColumn: "RessourceID",
            //            onDelete: ReferentialAction.Cascade);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MakingPublications_lienId",
            //    table: "MakingPublications",
            //    column: "lienId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MakingPublications_messageId",
            //    table: "MakingPublications",
            //    column: "messageId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MakingPublications_photoVideoId",
            //    table: "MakingPublications",
            //    column: "photoVideoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MakingPublications_publicationId",
            //    table: "MakingPublications",
            //    column: "publicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Membres_RoleId",
                table: "Membres",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_MembreEmail",
                table: "Publications",
                column: "MembreEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Publications_RessourceId",
                table: "Publications",
                column: "RessourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "MakingPublications");

            migrationBuilder.DropTable(
                name: "MembreMembres");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Membres");

            migrationBuilder.DropTable(
                name: "Ressource");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
