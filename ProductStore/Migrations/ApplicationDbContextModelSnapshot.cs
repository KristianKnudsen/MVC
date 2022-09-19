﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductStore.Data;

#nullable disable

namespace ProductStore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductStore.Models.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Category = "Verktøy",
                            Name = "Hammer",
                            Price = 121.50m
                        },
                        new
                        {
                            ProductId = 2,
                            Category = "Verktøy",
                            Name = "Vinkelsliper",
                            Price = 1520.00m
                        },
                        new
                        {
                            ProductId = 3,
                            Category = " Kjøretøy",
                            Name = "Volvo XC90",
                            Price = 990000m
                        },
                        new
                        {
                            ProductId = 4,
                            Category = "Kjøretøy",
                            Name = "Volvo XC60",
                            Price = 620000m
                        },
                        new
                        {
                            ProductId = 5,
                            Category = "Dagligvarer",
                            Name = "Brød",
                            Price = 25.50m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
