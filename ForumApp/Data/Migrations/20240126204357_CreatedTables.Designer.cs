﻿// <auto-generated />
using System;
using ForumApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForumApp.Data.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20240126204357_CreatedTables")]
    partial class CreatedTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ForumApp.Data.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c8a84652-234d-4ddb-bd6b-3c814d236850"),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.",
                            Title = "My first post"
                        },
                        new
                        {
                            Id = new Guid("51e8ff90-6469-4df4-9ec5-9542b6decf16"),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.",
                            Title = "My second post"
                        },
                        new
                        {
                            Id = new Guid("4f3535f7-3974-492f-9028-1cfe7f7e0459"),
                            Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed venenatis libero vel nibh ultricies mattis. Sed sagittis sem in leo.",
                            Title = "My third post"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}