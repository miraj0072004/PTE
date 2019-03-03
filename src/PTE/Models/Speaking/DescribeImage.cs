using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTE.Models.Speaking
{
    public class DescribeImage
    {

        public long Id { get; set; }
        public long ExamId { get; set; }
        public long DescribeImageId { get; set; }

        public string DescribeImageImagePath { get; set; }
    }
}
