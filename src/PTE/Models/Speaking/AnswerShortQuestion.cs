using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class AnswerShortQuestion
    {
        public long Id { get; set; }
        public long ExamId { get; set; }
        public long AnswerShortQuestionId { get; set; }

        public string AnswerShortQuestionPromptPath { get; set; }
        public int QuestionDuration { get; set; }
    }
}
