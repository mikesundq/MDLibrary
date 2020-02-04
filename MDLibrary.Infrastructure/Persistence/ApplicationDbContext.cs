using MDLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Infrastructure.Persistence
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<BookCopy> Book { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAuthor(modelBuilder);
            ConfigureBookDetails(modelBuilder);
            ConfigureBookCopy(modelBuilder);
            ConfigureMember(modelBuilder);
            ConfigureLoan(modelBuilder);

            SeedDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
        }

        private void ConfigureLoan(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.BookCopy)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookCopyID);
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Member)
                .WithMany(m => m.Loans)
                .HasForeignKey(l => l.MemberID);
        }

        private void ConfigureMember(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().Property(x => x.SSN).HasMaxLength(12);
        }

        private void ConfigureBookCopy(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCopy>()
                .HasOne(b => b.BookDetails)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(b => b.BookDetailsID);
        }

        private void ConfigureBookDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDetails>().HasKey(x => x.ID);
            modelBuilder.Entity<BookDetails>()
                .HasOne(b => b.Author)
                .WithMany(a => a.WrittenBooks)
                .HasForeignKey(b => b.AuthorID);
            modelBuilder.Entity<BookDetails>().Property(x => x.ISBN).HasMaxLength(13);
        }

        private void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(100);
        }
    }
}
