using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DasJottWeb.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogEntry",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Headline = table.Column<string>(type: "varchar(20)", nullable: false),
                    Text = table.Column<string>(type: "varchar(4000)", nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogEntry", x => x.ID);
                });
            migrationBuilder.CreateTable(
                name: "NewsArticle",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Headline = table.Column<string>(type: "varchar(20)", nullable: true),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticle", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("BlogEntry");
            migrationBuilder.DropTable("NewsArticle");
        }
    }
}
