﻿using Microsoft.EntityFrameworkCore;
using Yadebs.Db.IncomeSurplusCalculation;

namespace Yadebs.Db;

public class AccountingContext : DbContext
{
    public AccountingContext()
    {
    }

    public AccountingContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name);
            entity.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(x => x.Amount).HasPrecision(10, 2);
        });

        modelBuilder.Entity<BankTransfer>()
            .ToTable("banktransfers", schema: "incomesurpluscalculation");
        modelBuilder.Entity<Booking>()
            .ToTable("bookings", schema: "incomesurpluscalculation");
        modelBuilder.Entity<Category>()
            .ToTable("categories", schema: "incomesurpluscalculation");
        modelBuilder.Entity<Document>()
            .ToTable("document", schema: "incomesurpluscalculation");

    }

    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Account> Accounts { get; set; } = default!;
    public DbSet<Journal> Journals { get; set; } = default!;
    public DbSet<Transaction> Transactions { get; set; } = default!;
    public DbSet<BankTransfer> BankTransfers { get; set; } = default!;
    public DbSet<Booking> Bookings { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Document> Documents { get; set; } = default!;
}