using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace DasJottWeb.Migrations
{
    public partial class AddColumnDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "NewsArticle",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BlogEntry",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Deleted", table: "NewsArticle");
            migrationBuilder.DropColumn(name: "Deleted", table: "BlogEntry");
        }
    }
}
