﻿// <auto-generated />
using System;
using ITest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ITest.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210829095833_ChangeMailOnEmail")]
    partial class ChangeMailOnEmail
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ITest.Data.Entities.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<byte>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Choice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ChoiceString")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionString")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<byte>("QuestionType")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("TestId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<uint>("VisitorsCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.TestAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Answer")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ChoiceId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TestId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ChoiceId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestId");

                    b.ToTable("TestAnswers");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Choice", b =>
                {
                    b.HasOne("ITest.Data.Entities.Tests.Question", "Question")
                        .WithMany("Choices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Question", b =>
                {
                    b.HasOne("ITest.Data.Entities.Tests.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Test", b =>
                {
                    b.HasOne("ITest.Data.Entities.Accounts.Account", "Account")
                        .WithMany("Tests")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.TestAnswer", b =>
                {
                    b.HasOne("ITest.Data.Entities.Accounts.Account", "Account")
                        .WithMany("TestAnswers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITest.Data.Entities.Tests.Choice", "Choice")
                        .WithMany("TestAnswers")
                        .HasForeignKey("ChoiceId");

                    b.HasOne("ITest.Data.Entities.Tests.Question", "Question")
                        .WithMany("TestAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ITest.Data.Entities.Tests.Test", "Test")
                        .WithMany("TestAnswers")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Choice");

                    b.Navigation("Question");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("ITest.Data.Entities.Accounts.Account", b =>
                {
                    b.Navigation("TestAnswers");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Choice", b =>
                {
                    b.Navigation("TestAnswers");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Question", b =>
                {
                    b.Navigation("Choices");

                    b.Navigation("TestAnswers");
                });

            modelBuilder.Entity("ITest.Data.Entities.Tests.Test", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("TestAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
