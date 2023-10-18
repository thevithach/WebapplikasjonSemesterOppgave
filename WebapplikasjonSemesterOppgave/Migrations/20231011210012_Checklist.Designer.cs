﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebapplikasjonSemesterOppgave.Areas.Identity.Data;

#nullable disable

namespace WebapplikasjonSemesterOppgave.Migrations
{
    [DbContext(typeof(DBContextSample))]
    [Migration("20231011210012_Checklist")]
    partial class Checklist
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Models.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CustomerAgreement")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ModelYear")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("OrderStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReparationDetails")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ReplacedPartsReturned")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ServiceOrRepair")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ShippingMethods")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Warranty")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("OrderEntity");
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Models.ServiceChecklistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BremsekraftKN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Bremser")
                        .HasColumnType("int");

                    b.Property<int>("Bremsesylinder")
                        .HasColumnType("int");

                    b.Property<int>("ClutchlamelerSlitasje")
                        .HasColumnType("int");

                    b.Property<int>("HydraulikkblokkTestbenk")
                        .HasColumnType("int");

                    b.Property<int>("KilepåKjedehjul")
                        .HasColumnType("int");

                    b.Property<int>("Kjedestrammer")
                        .HasColumnType("int");

                    b.Property<int>("Knappekasse")
                        .HasColumnType("int");

                    b.Property<int>("LagerforTrommel")
                        .HasColumnType("int");

                    b.Property<int>("LedningsnettpåVinsj")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("PTOogOpplagring")
                        .HasColumnType("int");

                    b.Property<int>("PinionLager")
                        .HasColumnType("int");

                    b.Property<int>("Ringsylinder")
                        .HasColumnType("int");

                    b.Property<int>("SkiftOljeiTank")
                        .HasColumnType("int");

                    b.Property<int>("SkiftOljepåGirboks")
                        .HasColumnType("int");

                    b.Property<int>("SlangeSkadeLekkasje")
                        .HasColumnType("int");

                    b.Property<int>("SylinderLekkasje")
                        .HasColumnType("int");

                    b.Property<int>("TestRadio")
                        .HasColumnType("int");

                    b.Property<string>("TrekkraftKN")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VinsjKjørAlleFunksjoner")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Wire")
                        .HasColumnType("int");

                    b.Property<string>("XxBar")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("ChecklistItems");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Models.OrderEntity", b =>
                {
                    b.HasOne("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", "User")
                        .WithOne("Order")
                        .HasForeignKey("WebapplikasjonSemesterOppgave.Models.OrderEntity", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Models.ServiceChecklistEntity", b =>
                {
                    b.HasOne("WebapplikasjonSemesterOppgave.Models.OrderEntity", "Order")
                        .WithMany("ChecklistItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Areas.Identity.Data.SampleUser", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("WebapplikasjonSemesterOppgave.Models.OrderEntity", b =>
                {
                    b.Navigation("ChecklistItems");
                });
#pragma warning restore 612, 618
        }
    }
}
