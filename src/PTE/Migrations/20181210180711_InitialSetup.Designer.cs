﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PTE.Models;

namespace PTE.Migrations
{
    [DbContext(typeof(ExamDataContext))]
    [Migration("20181210180711_ReadAloudEnum")]
    partial class InitialSetup
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

            modelBuilder.Entity("PTE.Models.Speaking.ReadAloud", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ExamId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("ReadAlouds");
                });
        }
    }
}
