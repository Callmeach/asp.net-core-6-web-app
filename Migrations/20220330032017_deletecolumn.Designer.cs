﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectFirstSteps.Models;

#nullable disable

namespace ProjectFirstSteps.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220330032017_deletecolumn")]
    partial class deletecolumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjectFirstSteps.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Containt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MailMembre1")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MailMembre2")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("InvDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("InvitedMail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("InviterMail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Membre", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.HasIndex("RoleId");

                    b.ToTable("Membres");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.MembreMembre", b =>
                {
                    b.Property<string>("MailMembre2")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MailMembre1")
                        .HasColumnType("varchar(255)");

                    b.HasKey("MailMembre2", "MailMembre1");

                    b.ToTable("MembreMembres");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Notifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MembreEmail")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("MembreEmail");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.PersonalizedClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Legende")
                        .HasColumnType("longtext");

                    b.Property<string>("Libelle")
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RessourceName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Url")
                        .HasColumnType("longtext");

                    b.Property<string>("UserMail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Personalizeds");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Publication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MembreEmail")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("RessourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateDePublication")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("MembreEmail");

                    b.HasIndex("RessourceId");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Ressource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RessourceID");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("nomRessource")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Ressource");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Ressource");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Lien", b =>
                {
                    b.HasBaseType("ProjectFirstSteps.Models.Ressource");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("Lien");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Message", b =>
                {
                    b.HasBaseType("ProjectFirstSteps.Models.Ressource");

                    b.Property<string>("libelle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("Message");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.PhotoVideo", b =>
                {
                    b.HasBaseType("ProjectFirstSteps.Models.Ressource");

                    b.Property<string>("legende")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("path")
                        .HasColumnType("longtext");

                    b.Property<long>("taille")
                        .HasColumnType("bigint");

                    b.Property<int?>("type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("PhotoVideo");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Membre", b =>
                {
                    b.HasOne("ProjectFirstSteps.Models.Role", "Role")
                        .WithMany("Membres")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Notifications", b =>
                {
                    b.HasOne("ProjectFirstSteps.Models.Membre", "Membre")
                        .WithMany("Notifications")
                        .HasForeignKey("MembreEmail");

                    b.Navigation("Membre");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Publication", b =>
                {
                    b.HasOne("ProjectFirstSteps.Models.Membre", "Membre")
                        .WithMany()
                        .HasForeignKey("MembreEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectFirstSteps.Models.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Membre");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Membre", b =>
                {
                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Role", b =>
                {
                    b.Navigation("Membres");
                });
#pragma warning restore 612, 618
        }
    }
}
