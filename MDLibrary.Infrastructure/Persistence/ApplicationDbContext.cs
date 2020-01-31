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
        public DbSet<Book> Book { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Loan> Loan { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureAuthor(modelBuilder);
            ConfigureBookDetails(modelBuilder);
            ConfigureBook(modelBuilder);
            ConfigureMember(modelBuilder);
            ConfigureLoan(modelBuilder);

            SeedDatabase(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedDatabase(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureLoan(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureMember(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureBook(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDetails>()
        }

        private void ConfigureBookDetails(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void ConfigureAuthor(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property(x => x.Name).HasMaxLength(100);
        }
    }
}
