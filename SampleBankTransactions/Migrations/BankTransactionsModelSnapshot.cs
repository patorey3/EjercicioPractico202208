﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleBankTransactions.Model;

#nullable disable

namespace SampleBankTransactions.Migrations
{
    [DbContext(typeof(BankTransactions))]
    partial class BankTransactionsModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SampleBankTransactions.Model.Account", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal>("StartAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("AccountNumber");

                    b.HasIndex("CustomerId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("IdentityDocument")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Phone")
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("Id");

                    b.HasIndex("IdentityDocument")
                        .IsUnique();

                    b.ToTable("Person", (string)null);
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Customer", b =>
                {
                    b.HasBaseType("SampleBankTransactions.Model.Person");

                    b.Property<string>("Password")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Account", b =>
                {
                    b.HasOne("SampleBankTransactions.Model.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Transaction", b =>
                {
                    b.HasOne("SampleBankTransactions.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SampleBankTransactions.Model.Customer", b =>
                {
                    b.HasOne("SampleBankTransactions.Model.Person", null)
                        .WithOne()
                        .HasForeignKey("SampleBankTransactions.Model.Customer", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
