using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PTE.Migrations
{
    public partial class AddingAnswerShortQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnswerShortQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerShortQuestionId = table.Column<long>(nullable: false),
                    AnswerShortQuestionPromptPath = table.Column<string>(nullable: true),
                    ExamId = table.Column<long>(nullable: false),
                    QuestionDuration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerShortQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnswerShortQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerShortQuestionId = table.Column<long>(nullable: false),
                    AnswerShortQuestionPath = table.Column<string>(nullable: true),
                    ExamId = table.Column<long>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerShortQuestionAnswers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerShortQuestions");

            migrationBuilder.DropTable(
                name: "AnswerShortQuestionAnswers");
        }
    }
}
