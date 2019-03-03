using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PTE.Models;

namespace PTE.Migrations
{
    [DbContext(typeof(ExamDataContext))]
    [Migration("20190303130626_AddedRetellLecture")]
    partial class AddedRetellLecture
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PTE.Models.Exam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<int>("Duration");

                    b.Property<string>("Key");

                    b.Property<DateTime>("Posted");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("PTE.Models.Speaking.DescribeImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DescribeImageId");

                    b.Property<string>("DescribeImageImagePath");

                    b.Property<long>("ExamId");

                    b.HasKey("Id");

                    b.ToTable("DescribeImages");
                });

            modelBuilder.Entity("PTE.Models.Speaking.DescribeImageAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerPath");

                    b.Property<long>("DescribeImageIdId");

                    b.Property<long>("ExamId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("DescribeImageAnswers");
                });

            modelBuilder.Entity("PTE.Models.Speaking.ReadAloud", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExamId");

                    b.Property<int>("QuestionDuration");

                    b.Property<long>("ReadAloudId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("ReadAlouds");
                });

            modelBuilder.Entity("PTE.Models.Speaking.ReadAloudAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerPath");

                    b.Property<long>("ExamId");

                    b.Property<long>("ReadAloudId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("ReadAloudsAnswers");
                });

            modelBuilder.Entity("PTE.Models.Speaking.RepeatSentence", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExamId");

                    b.Property<int>("QuestionDuration");

                    b.Property<long>("RepeatSentenceId");

                    b.Property<string>("RepeatSentencePromptPath");

                    b.HasKey("Id");

                    b.ToTable("RepeatSentences");
                });

            modelBuilder.Entity("PTE.Models.Speaking.RepeatSentenceAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerPath");

                    b.Property<long>("ExamId");

                    b.Property<long>("RepeatSentenceId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("RepeatSentenceAnswers");
                });

            modelBuilder.Entity("PTE.Models.Speaking.RetellLecture", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExamId");

                    b.Property<int>("QuestionDuration");

                    b.Property<long>("RetellLectureId");

                    b.Property<string>("RetellLectureImagePath");

                    b.Property<string>("RetellLecturePromptPath");

                    b.HasKey("Id");

                    b.ToTable("RetellLectures");
                });

            modelBuilder.Entity("PTE.Models.Speaking.RetellLectureAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerPath");

                    b.Property<long>("ExamId");

                    b.Property<long>("RetellLectureId");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.ToTable("RetellLectureAnswers");
                });
        }
    }
}
