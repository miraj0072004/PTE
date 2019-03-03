using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PTE.Migrations
{
    public partial class AddedRetellLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RetellLectures",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamId = table.Column<long>(nullable: false),
                    QuestionDuration = table.Column<int>(nullable: false),
                    RetellLectureId = table.Column<long>(nullable: false),
                    RetellLectureImagePath = table.Column<string>(nullable: true),
                    RetellLecturePromptPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetellLectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RetellLectureAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerPath = table.Column<string>(nullable: true),
                    ExamId = table.Column<long>(nullable: false),
                    RetellLectureId = table.Column<long>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetellLectureAnswers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetellLectures");

            migrationBuilder.DropTable(
                name: "RetellLectureAnswers");
        }
    }
}
