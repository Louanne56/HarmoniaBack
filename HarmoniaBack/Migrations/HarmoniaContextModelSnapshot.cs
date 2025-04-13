﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HarmoniaBack.Migrations
{
    [DbContext(typeof(HarmoniaContext))]
    partial class HarmoniaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity(
                "Accord",
                b =>
                {
                    b.Property<string>("Id").HasColumnType("TEXT");

                    b.Property<string>("Audio").IsRequired().HasColumnType("TEXT");

                    b.Property<string>("Audio2").HasColumnType("TEXT");

                    b.Property<string>("Diagram1").IsRequired().HasColumnType("TEXT");

                    b.Property<string>("Diagram2").HasColumnType("TEXT");

                    b.Property<string>("Nom").IsRequired().HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accords");
                }
            );

            modelBuilder.Entity(
                "AccordProgressionAccords",
                b =>
                {
                    b.Property<string>("AccordsId").HasColumnType("TEXT");

                    b.Property<string>("ProgressionsId").HasColumnType("TEXT");

                    b.HasKey("AccordsId", "ProgressionsId");

                    b.HasIndex("ProgressionsId");

                    b.ToTable("ProgressionAccord", (string)null);
                }
            );

            modelBuilder.Entity(
                "ProgressionAccords",
                b =>
                {
                    b.Property<string>("Id").HasColumnType("TEXT");

                    b.Property<int>("Mode").HasColumnType("INTEGER");

                    b.Property<string>("Nom").IsRequired().HasColumnType("TEXT");

                    b.Property<int>("Style").HasColumnType("INTEGER");

                    b.Property<int>("Tonalite").HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ProgressionAccords");
                }
            );

            modelBuilder.Entity(
                "SuitesFavorites",
                b =>
                {
                    b.Property<string>("Id").HasColumnType("TEXT");

                    b.Property<string>("ProgressionAccordsId").IsRequired().HasColumnType("TEXT");

                    b.Property<string>("UserId").IsRequired().HasColumnType("TEXT");

                    b.Property<string>("UtilisateurId").HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProgressionAccordsId");

                    b.HasIndex("UtilisateurId");

                    b.ToTable("SuitesFavorites");
                }
            );

            modelBuilder.Entity(
                "Utilisateur",
                b =>
                {
                    b.Property<string>("Id").HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount").HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email").HasMaxLength(256).HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed").HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled").HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd").HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail").HasMaxLength(256).HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash").HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber").HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed").HasColumnType("INTEGER");

                    b.Property<string>("RefreshToken").HasColumnType("TEXT");

                    b.Property<DateTime>("RefreshTokenExpiryTime").HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp").HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled").HasColumnType("INTEGER");

                    b.Property<string>("UserName").HasMaxLength(256).HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail").HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName").IsUnique().HasDatabaseName("UserNameIndex");

                    b.ToTable("Utilisateurs", (string)null);
                }
            );

            modelBuilder.Entity(
                "AccordProgressionAccords",
                b =>
                {
                    b.HasOne("Accord", null)
                        .WithMany()
                        .HasForeignKey("AccordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProgressionAccords", null)
                        .WithMany()
                        .HasForeignKey("ProgressionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                }
            );

            modelBuilder.Entity(
                "SuitesFavorites",
                b =>
                {
                    b.HasOne("ProgressionAccords", "ProgressionAccords")
                        .WithMany()
                        .HasForeignKey("ProgressionAccordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Utilisateur", null)
                        .WithMany("SuitesFavorites")
                        .HasForeignKey("UtilisateurId");

                    b.Navigation("ProgressionAccords");
                }
            );

            modelBuilder.Entity(
                "Utilisateur",
                b =>
                {
                    b.Navigation("SuitesFavorites");
                }
            );
#pragma warning restore 612, 618
        }
    }
}
