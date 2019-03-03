using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTE.Models.Speaking;

namespace PTE.Models
{
    public class ExamDataContext : DbContext
    {
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ReadAloud> ReadAlouds { get; set; }
        public DbSet<ReadAloudAnswer> ReadAloudsAnswers { get; set; }
        public DbSet<RepeatSentence> RepeatSentences { get; set; }
        public DbSet<RepeatSentenceAnswer> RepeatSentenceAnswers { get; set; }
        public DbSet<DescribeImage> DescribeImages { get; set; }
        public DbSet<DescribeImageAnswer> DescribeImageAnswers { get; set; }
        public DbSet<RetellLecture> RetellLectures { get; set; }
        public DbSet<RetellLectureAnswer> RetellLectureAnswers { get; set; }

        public ExamDataContext(DbContextOptions<ExamDataContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }
    }
}
