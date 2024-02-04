using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppSecAssignment2RE.Migrations
{
    public partial class updateapplicationclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "IV",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Key",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IV",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "AspNetUsers");
        }
    }
}
