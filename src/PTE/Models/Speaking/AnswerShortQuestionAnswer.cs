using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class AnswerShortQuestionAnswer
    {
        public long Id { get; set; }
        public string User { get; set; }
        public long ExamId { get; set; }
        public long AnswerShortQuestionId { get; set; }
        public string AnswerShortQuestionPath { get; set; }
    }
}
