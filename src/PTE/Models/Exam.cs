using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PTE.Models
{
    public class Exam
    {
        public long Id { get; set; }

        private string _key;

        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }

            set
            {
                _key = value;
            }
        }

        [Display(Name = "Exam Title")]
        [Required]
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Author { get; set; }
        public DateTime Posted { get; set; }
    }
}
