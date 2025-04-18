﻿// <auto-generated />
using System;
using BankCreditSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250409160032_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("CreditHistory")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Income")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.CreditApplication", b =>
                {
                    b.Property<int>("CreditApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CreditApplicationId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ApplicationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TermMonths")
                        .HasColumnType("int");

                    b.HasKey("CreditApplicationId");

                    b.HasIndex("ClientId");

                    b.ToTable("CreditApplications");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.CreditAssessment", b =>
                {
                    b.Property<int>("CreditAssessmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CreditAssessmentId"));

                    b.Property<string>("AssessmentResult")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CreditApplicationId")
                        .HasColumnType("int");

                    b.HasKey("CreditAssessmentId");

                    b.HasIndex("CreditApplicationId")
                        .IsUnique();

                    b.ToTable("CreditAssessments");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.CreditApplication", b =>
                {
                    b.HasOne("BankCreditSystem.Domain.Entities.Client", "Client")
                        .WithMany("CreditApplications")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.CreditAssessment", b =>
                {
                    b.HasOne("BankCreditSystem.Domain.Entities.CreditApplication", "CreditApplication")
                        .WithOne("CreditAssessment")
                        .HasForeignKey("BankCreditSystem.Domain.Entities.CreditAssessment", "CreditApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreditApplication");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.Client", b =>
                {
                    b.Navigation("CreditApplications");
                });

            modelBuilder.Entity("BankCreditSystem.Domain.Entities.CreditApplication", b =>
                {
                    b.Navigation("CreditAssessment")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
