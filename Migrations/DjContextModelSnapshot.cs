using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using DasJott.Database;

namespace DasJottWeb.Migrations
{
    [DbContext(typeof(DjContext))]
    partial class DjContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348");

            modelBuilder.Entity("DasJott.Models.BlogEntry", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Headline")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "varchar(4000)");

                    b.Property<DateTime>("Updated");

                    b.HasKey("ID");

                    b.HasAnnotation("Relational:DiscriminatorValue", "BlogEntry");
                });

            modelBuilder.Entity("DasJott.Models.NewsArticle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasAnnotation("Relational:ColumnType", "varchar(2000)");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Headline")
                        .HasAnnotation("Relational:ColumnType", "varchar(20)");

                    b.Property<DateTime>("Updated");

                    b.HasKey("ID");

                    b.HasAnnotation("Relational:DiscriminatorValue", "NewsArticle");
                });
        }
    }
}
