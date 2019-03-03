using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class ReadAloud
    {
        public long  Id { get; set; }
        public long ExamId { get; set; }
        public long ReadAloudId { get; set; }

        public string Text { get; set; }
        public int QuestionDuration { get; set; }
    }
}
