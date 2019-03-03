using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class RetellLecture
    {
        public long Id { get; set; }
        public long ExamId { get; set; }
        public long RetellLectureId { get; set; }

        public string RetellLecturePromptPath { get; set; }
        public string RetellLectureImagePath { get; set; }
        public int QuestionDuration { get; set; }
    }
}
