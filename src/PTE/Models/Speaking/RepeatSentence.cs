using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class RepeatSentence
    {
        public long Id { get; set; }
        public long ExamId { get; set; }
        public long RepeatSentenceId { get; set; }

        public string RepeatSentencePromptPath { get; set; }
        public int QuestionDuration { get; set; }
    }
}
