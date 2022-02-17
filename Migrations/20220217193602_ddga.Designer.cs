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
    [Migration("20220217193602_ddga")]
    partial class ddga
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProjectFirstSteps.Models.Administrateur", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Email");

                    b.ToTable("Administrateurs");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Membre", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Email");

                    b.ToTable("Membres");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Moderateur", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Email");

                    b.ToTable("Moderateurs");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Publication", b =>
                {
                    b.Property<DateTime>("dateDePublication")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("uneRessource")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("memberMail")
                        .HasColumnType("varchar(255)");

                    b.HasKey("dateDePublication", "uneRessource", "memberMail");

                    b.HasIndex("memberMail");

                    b.HasIndex("uneRessource");

                    b.ToTable("Publications");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Ressource", b =>
                {
                    b.Property<string>("nomRessource")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("nomRessource");

                    b.ToTable("Ressources");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Ressource");
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

                    b.Property<int>("taille")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("PhotoVideo");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Publication", b =>
                {
                    b.HasOne("ProjectFirstSteps.Models.Membre", "Membre")
                        .WithMany("Publications")
                        .HasForeignKey("memberMail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectFirstSteps.Models.Ressource", "Ressource")
                        .WithMany("lesPublications")
                        .HasForeignKey("uneRessource")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Membre");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Membre", b =>
                {
                    b.Navigation("Publications");
                });

            modelBuilder.Entity("ProjectFirstSteps.Models.Ressource", b =>
                {
                    b.Navigation("lesPublications");
                });
#pragma warning restore 612, 618
        }
    }
}
