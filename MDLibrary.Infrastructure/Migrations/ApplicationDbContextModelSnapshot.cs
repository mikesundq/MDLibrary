﻿// <auto-generated />
using System;
using MDLibrary.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MDLibrary.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MDLibrary.Domain.Author", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("Author");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Thomas Årnfelt"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Johan Hagesund"
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.BookCopy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookDetailsID")
                        .HasColumnType("int");

                    b.Property<int?>("LoanID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BookDetailsID");

                    b.HasIndex("LoanID");

                    b.ToTable("BookCopy");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            BookDetailsID = 1
                        },
                        new
                        {
                            ID = 2,
                            BookDetailsID = 1
                        },
                        new
                        {
                            ID = 3,
                            BookDetailsID = 2
                        },
                        new
                        {
                            ID = 4,
                            BookDetailsID = 3
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.BookDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuthorID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<string>("Titel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AuthorID");

                    b.ToTable("BookDetails");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AuthorID = 1,
                            Details = "Incidenten i Böhmen är Linköpingsförfattaren Thomas Årnfelts debutroman. I den blandas historia med vidskepelse och ockultism på ett sätt som passar den tidigmoderna världen före upplysningen.",
                            ISBN = "9789198138795",
                            Titel = "Incidenten i Böhmen"
                        },
                        new
                        {
                            ID = 2,
                            AuthorID = 2,
                            Details = "Historien om Linköpings Hockey Club börjar inte den 4 augusti 1976. LHC bildades visserligen den dagen men spelartruppen, utrustningen, platsen i seriesystemet och traditionen var densamma som i BK Kenty som man bröt sig ut från.",
                            ISBN = "9789198075526",
                            Titel = "Linköpings Hockey Club och den förändrade självbilden"
                        },
                        new
                        {
                            ID = 3,
                            AuthorID = 1,
                            Details = "Den som söker är en psykologisk spänningsroman där Johan följer tips som leder honom till makabra brottsplatser och ger hans karriär en skjuts framåt. Men vad är det egentligen som händer och vem är det som tipsar? Vilka mörka krafter är det som har satts i rörelse? Är det verkligen ok att gå över lik för att nå sina drömmars mål?",
                            ISBN = "9789198428506",
                            Titel = "Den som söker"
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.Loan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IsReturned")
                        .HasColumnType("int");

                    b.Property<int>("MemberID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeOfLoan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeToReturnBook")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("MemberID");

                    b.ToTable("Loan");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsReturned = 0,
                            MemberID = 1,
                            TimeOfLoan = new DateTime(2020, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeToReturnBook = new DateTime(2020, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ID = 2,
                            IsReturned = 0,
                            MemberID = 2,
                            TimeOfLoan = new DateTime(2021, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TimeToReturnBook = new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.LoanBook", b =>
                {
                    b.Property<int>("LoanID")
                        .HasColumnType("int");

                    b.Property<int>("BookCopyID")
                        .HasColumnType("int");

                    b.HasKey("LoanID", "BookCopyID");

                    b.HasIndex("BookCopyID");

                    b.ToTable("LoanBook");

                    b.HasData(
                        new
                        {
                            LoanID = 1,
                            BookCopyID = 1
                        },
                        new
                        {
                            LoanID = 2,
                            BookCopyID = 3
                        },
                        new
                        {
                            LoanID = 2,
                            BookCopyID = 2
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.Member", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SSN")
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.HasKey("ID");

                    b.ToTable("Member");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Mikael Sundqvist",
                            SSN = "8004241234"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Daniel Ny",
                            SSN = "8004191234"
                        });
                });

            modelBuilder.Entity("MDLibrary.Domain.BookCopy", b =>
                {
                    b.HasOne("MDLibrary.Domain.BookDetails", "BookDetails")
                        .WithMany("BookCopies")
                        .HasForeignKey("BookDetailsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MDLibrary.Domain.Loan", null)
                        .WithMany("BookCopies")
                        .HasForeignKey("LoanID");
                });

            modelBuilder.Entity("MDLibrary.Domain.BookDetails", b =>
                {
                    b.HasOne("MDLibrary.Domain.Author", "Author")
                        .WithMany("WrittenBooks")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MDLibrary.Domain.Loan", b =>
                {
                    b.HasOne("MDLibrary.Domain.Member", "Member")
                        .WithMany("Loans")
                        .HasForeignKey("MemberID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MDLibrary.Domain.LoanBook", b =>
                {
                    b.HasOne("MDLibrary.Domain.BookCopy", "BookCopy")
                        .WithMany()
                        .HasForeignKey("BookCopyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MDLibrary.Domain.Loan", "Loan")
                        .WithMany("LoanBooks")
                        .HasForeignKey("LoanID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
