using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PTE.Models;

namespace PTE.Controllers
{
    public class ExamsController : Controller
    {
        private readonly ExamDataContext db_;
        public ExamsController(ExamDataContext db)
        {
            this.db_ = db;
        }

        public IActionResult Index()
        {
            var exams = db_.Exams.ToArray();
            return View(exams);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Route("create")]
        public IActionResult Create(Exam exam)
        {
            exam.Author = User.Identity.Name;
            exam.Posted=DateTime.Now;
            db_.Add(exam);
            db_.SaveChanges();

            return RedirectToAction("Index", "Exams");
        }


        public IActionResult Exam()
        {
            return new ContentResult { Content = "Testing" };
        }
    }
}
