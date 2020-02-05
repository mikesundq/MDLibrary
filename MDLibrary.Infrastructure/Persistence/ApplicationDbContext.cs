﻿using MDLibrary.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDLibrary.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
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
            modelBuilder.Entity<Author>().HasData
            (
                new Author { ID = 1, Name = "Thomas Årnfelt" },
                new Author { ID = 2, Name = "Johan Hagesund" }
            );
            modelBuilder.Entity<BookDetails>().HasData
            (
                new BookDetails { ID = 1, Titel = "Incidenten i Böhmen", AuthorID = 1, ISBN = "9789198138795", Details = "Incidenten i Böhmen är Linköpingsförfattaren Thomas Årnfelts debutroman. I den blandas historia med vidskepelse och ockultism på ett sätt som passar den tidigmoderna världen före upplysningen." },
                new BookDetails { ID = 2, Titel = "Linköpings Hockey Club och den förändrade självbilden", AuthorID = 2, ISBN = "9789198075526", Details = "Historien om Linköpings Hockey Club börjar inte den 4 augusti 1976. LHC bildades visserligen den dagen men spelartruppen, utrustningen, platsen i seriesystemet och traditionen var densamma som i BK Kenty som man bröt sig ut från." },
                new BookDetails { ID = 3, Titel = "Den som söker", AuthorID = 1, ISBN = "9789198428506", Details = "Den som söker är en psykologisk spänningsroman där Johan följer tips som leder honom till makabra brottsplatser och ger hans karriär en skjuts framåt. Men vad är det egentligen som händer och vem är det som tipsar? Vilka mörka krafter är det som har satts i rörelse? Är det verkligen ok att gå över lik för att nå sina drömmars mål?" }
            );
            modelBuilder.Entity<BookCopy>().HasData
            (
                new BookCopy { ID = 1, BookDetailsID = 1 },
                new BookCopy { ID = 2, BookDetailsID = 1 },
                new BookCopy { ID = 3, BookDetailsID = 2 },
                new BookCopy { ID = 4, BookDetailsID = 3 }

            );
            modelBuilder.Entity<Member>().HasData
            (
                new Member { ID = 1, Name = "Mikael Sundqvist", SSN = "800424-1234" },
                new Member { ID = 2, Name = "Daniel Ny", SSN = "800419-1234"}
            );
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
