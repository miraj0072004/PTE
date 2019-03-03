using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PTE.Migrations
{
    public partial class ReadAloudAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadAloudsAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerPath = table.Column<string>(nullable: true),
                    ExamId = table.Column<long>(nullable: false),
                    ReadAloudId = table.Column<long>(nullable: false),
                    User = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadAloudsAnswers", x => x.Id);
                });

            migrationBuilder.AddColumn<long>(
                name: "ReadAloudId",
                table: "ReadAlouds",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadAloudId",
                table: "ReadAlouds");

            migrationBuilder.DropTable(
                name: "ReadAloudsAnswers");
        }
    }
}
