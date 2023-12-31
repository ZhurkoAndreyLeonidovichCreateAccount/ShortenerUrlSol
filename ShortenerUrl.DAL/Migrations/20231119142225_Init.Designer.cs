﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShortenerUrl.DAL.Data;

#nullable disable

namespace ShortenerUrl.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231119142225_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShortenerUrl.DAL.Entity.ShortendUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LongUrl")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("NumberOfTransitions")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("LongUrl")
                        .IsUnique();

                    b.ToTable("ShortendUrls");
                });
#pragma warning restore 612, 618
        }
    }
}
