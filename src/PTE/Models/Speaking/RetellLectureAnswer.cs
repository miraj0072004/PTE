using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class RetellLectureAnswer
    {
        public long Id { get; set; }
        public string User { get; set; }
        public long ExamId { get; set; }
        public long RetellLectureId { get; set; }
        public string AnswerPath { get; set; }
    }
}
