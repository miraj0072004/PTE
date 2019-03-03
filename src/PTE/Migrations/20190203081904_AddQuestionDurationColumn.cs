using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PTE.Migrations
{
    public partial class AddQuestionDurationColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionDuration",
                table: "RepeatSentences",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionDuration",
                table: "ReadAlouds",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionDuration",
                table: "RepeatSentences");

            migrationBuilder.DropColumn(
                name: "QuestionDuration",
                table: "ReadAlouds");
        }
    }
}
