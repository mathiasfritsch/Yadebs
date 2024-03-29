﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Yadebs.Db;

#nullable disable

namespace Yadebs.Db.Migrations
{
    [DbContext(typeof(AccountingContext))]
    partial class AccountingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Yadebs.Db.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer");

                    b.Property<bool>("IncreasesDebitWhenMoneyAdded")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPlaceholder")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("Number")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ParentId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Yadebs.Db.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Yadebs.Db.IncomeSurplusCalculation.BankTransfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("GrossAmount")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsIncome")
                        .HasColumnType("boolean");

                    b.Property<decimal?>("NetAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Tax")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("TaxAmount")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("banktransfers", "incomesurpluscalculation");
                });

            modelBuilder.Entity("Yadebs.Db.IncomeSurplusCalculation.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BankTransferId")
                        .HasColumnType("integer");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("DocumentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BankTransferId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DocumentId");

                    b.ToTable("bookings", "incomesurpluscalculation");
                });

            modelBuilder.Entity("Yadebs.Db.IncomeSurplusCalculation.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("categories", "incomesurpluscalculation");
                });

            modelBuilder.Entity("Yadebs.Db.IncomeSurplusCalculation.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("DocumentReference")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.ToTable("document", "incomesurpluscalculation");
                });

            modelBuilder.Entity("Yadebs.Db.Journal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<LocalDate>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("Yadebs.Db.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<bool>("IsDebit")
                        .HasColumnType("boolean");

                    b.Property<int>("JournalId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("JournalId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Yadebs.Db.Account", b =>
                {
                    b.HasOne("Yadebs.Db.Book", null)
                        .WithMany("Accounts")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yadebs.Db.Account", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Yadebs.Db.IncomeSurplusCalculation.Booking", b =>
                {
                    b.HasOne("Yadebs.Db.IncomeSurplusCalculation.BankTransfer", "BankTransfer")
                        .WithMany()
                        .HasForeignKey("BankTransferId");

                    b.HasOne("Yadebs.Db.IncomeSurplusCalculation.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("Yadebs.Db.IncomeSurplusCalculation.Document", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId");

                    b.Navigation("BankTransfer");

                    b.Navigation("Category");

                    b.Navigation("Document");
                });

            modelBuilder.Entity("Yadebs.Db.Transaction", b =>
                {
                    b.HasOne("Yadebs.Db.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yadebs.Db.Journal", "Journal")
                        .WithMany("Transactions")
                        .HasForeignKey("JournalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Journal");
                });

            modelBuilder.Entity("Yadebs.Db.Account", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("Yadebs.Db.Book", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Yadebs.Db.Journal", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
