﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SnakeandLadders.Data;

#nullable disable

namespace SnakesAndLadders.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230704030520_UpdateRelations")]
    partial class UpdateRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("SnakeandLadders.Models.Board", b =>
                {
                    b.Property<int>("BoardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.HasKey("BoardId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Dice", b =>
                {
                    b.Property<int>("DiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfSides")
                        .HasColumnType("INTEGER");

                    b.HasKey("DiceId");

                    b.ToTable("Dices");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Ladder", b =>
                {
                    b.Property<int>("LadderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BottomPosition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TopPosition")
                        .HasColumnType("INTEGER");

                    b.HasKey("LadderId");

                    b.HasIndex("BoardId");

                    b.ToTable("Ladders");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Snake", b =>
                {
                    b.Property<int>("SnakeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HeadPosition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TailPosition")
                        .HasColumnType("INTEGER");

                    b.HasKey("SnakeId");

                    b.HasIndex("BoardId");

                    b.ToTable("Snakes");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Ladder", b =>
                {
                    b.HasOne("SnakeandLadders.Models.Board", "Board")
                        .WithMany("Ladders")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Snake", b =>
                {
                    b.HasOne("SnakeandLadders.Models.Board", "Board")
                        .WithMany("Snakes")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("SnakeandLadders.Models.Board", b =>
                {
                    b.Navigation("Ladders");

                    b.Navigation("Snakes");
                });
#pragma warning restore 612, 618
        }
    }
}
