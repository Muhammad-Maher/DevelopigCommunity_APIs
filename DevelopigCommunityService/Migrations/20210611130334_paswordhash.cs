using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevelopigCommunityService.Migrations
{
    public partial class paswordhash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfPassword",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ConfPassword",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "ConfPassword",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "ConfPassword",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Admins");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Students",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Instructors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Instructors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Individuals",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Individuals",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Admins",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Admins",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Instructors");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Individuals");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Admins");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Admins");

            migrationBuilder.AddColumn<string>(
                name: "ConfPassword",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfPassword",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Instructors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfPassword",
                table: "Individuals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Individuals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfPassword",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Admins",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
