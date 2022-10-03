using Microsoft.EntityFrameworkCore;

namespace Yadebs.Db
{
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
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}