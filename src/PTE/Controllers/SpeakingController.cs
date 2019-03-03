using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTE.Models;
using PTE.Models.Speaking;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PTE.Controllers
{
    public class SpeakingController : Controller
    {


        private readonly ExamDataContext _db;
        [FromRoute]
        public long? Id {get; set;}

        [FromRoute]
        public long? ExamId { get; set; }

        public SpeakingController(ExamDataContext db)
        {
            this._db = db;
            
        }
        // GET: /<controller>/
        ///////////// Read Aloud /////////////////////
        [Route("exam/{examId}/read_aloud/{id}")]
        public IActionResult ReadAloud(long examId,long id )
        {
            var readAloud = _db.ReadAlouds.FirstOrDefault(e => e.ExamId == examId && e.ReadAloudId == id);

            return View(readAloud);
        }

        [Route("exam/{examId}/read_aloud/answer/{readAloudId}")]
        public IActionResult AnswerReadAloud(long examId,long readAloudId)
        {
            var answerPath = "./answers/" + examId + "/speaking/read_aloud/" + readAloudId + ".wav";
            var temp = new ReadAloudAnswer()
            {
                ExamId = examId,
                User = "Miraj",
                ReadAloudId = readAloudId,
                AnswerPath=answerPath
            };
            
            if (!_db.ReadAloudsAnswers.Any(answer=>answer.ExamId== examId && answer.ReadAloudId==readAloudId))
            {
                _db.Add(temp);
                _db.SaveChanges();

            }
            
            
            var newId = 0;
            switch (readAloudId)
            {
                case 1:
                    newId = 2;
                    break;

                case 2:
                    //set the id for repeat sentence
                    newId = 1;
                    break;
                    //case 2:
                    //    newId = 3;
                    //    break;
                    //case 3:
                    //    newId = 4;
                    //    break;

            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (readAloudId !=2)
                {
                    return Json(new { url = Url.Action("ReadAloud", "Speaking", new { examId, id = newId }) });
                }
                else
                {
                    return Json(new { url = Url.Action("RepeatSentence", "Speaking", new { examId, id = newId }) });
                }
                


                
            }
            else
            {
                return RedirectToAction("ReadAloud", "Speaking", new { examId, id = newId });
            }
                       
        }

        // GET: /<controller>/
        ///////////// Repeat Sentence /////////////////////
        [Route("exam/{examId}/repeat_sentence/{id}")]
        public IActionResult RepeatSentence(long examId, long id)
        {
            var repeatSentence = _db.RepeatSentences.FirstOrDefault(e => e.ExamId == examId && e.RepeatSentenceId == id);

            return View(repeatSentence);
        }

        [Route("exam/{examId}/repeat_sentence/answer/{repeatSentenceId}")]
        public IActionResult AnswerRepeatSentence(long examId, long repeatSentenceId)
        {
            var answerPath = "./answers/" + examId + "/speaking/repeat_sentence/" + repeatSentenceId + ".wav";
            var temp = new RepeatSentenceAnswer()
            {
                ExamId = examId,
                User = "Miraj",
                RepeatSentenceId = repeatSentenceId,
                AnswerPath = answerPath
            };

            if (!_db.RepeatSentenceAnswers.Any(answer => answer.ExamId == examId && answer.RepeatSentenceId == repeatSentenceId))
            {
                _db.Add(temp);
                _db.SaveChanges();

            }


            var newId = 0;
            switch (repeatSentenceId)
            {
                case 1:
                    newId = 2;
                    break;

                case 2:
                    //set the id for repeat sentence
                    newId = 1;
                    break;
                    //case 2:
                    //    newId = 3;
                    //    break;
                    //case 3:
                    //    newId = 4;
                    //    break;

            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (repeatSentenceId != 2)
                {
                    return Json(new { url = Url.Action("RepeatSentence", "Speaking", new { examId, id = newId }) });
                }
                else
                {
                    return Json(new { url = Url.Action("DescribeImage", "Speaking", new { examId, id = newId }) });
                }




            }
            else
            {
                return RedirectToAction("ReadAloud", "Speaking", new { examId, id = newId });
            }

        }

        // GET: /<controller>/
        ///////////// Describe Image /////////////////////
        [Route("exam/{examId}/describe_image/{id}")]
        public IActionResult DescribeImage(long examId, long id)
        {
            var describeImage = _db.DescribeImages.FirstOrDefault(e => e.ExamId == examId && e.DescribeImageId == id);

            return View(describeImage);
        }

        [Route("exam/{examId}/describe_image/answer/{describeImageId}")]
        public IActionResult AnswerDescribeImage(long examId, long describeImageId)
        {
            var answerPath = "./answers/" + examId + "/speaking/describe_image/" + describeImageId + ".wav";
            var temp = new DescribeImageAnswer()
            {
                ExamId = examId,
                User = "Miraj",
                DescribeImageIdId = describeImageId,
                AnswerPath = answerPath
            };

            if (!_db.DescribeImageAnswers.Any(answer => answer.ExamId == examId && answer.DescribeImageIdId == describeImageId))
            {
                _db.Add(temp);
                _db.SaveChanges();

            }


            var newId = 0;
            switch (describeImageId)
            {
                case 1:
                    newId = 2;
                    break;

                case 2:
                    //set the id for repeat sentence
                    newId = 1;
                    break;
                    //case 2:
                    //    newId = 3;
                    //    break;
                    //case 3:
                    //    newId = 4;
                    //    break;

            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (describeImageId != 2)
                {
                    return Json(new { url = Url.Action("DescribeImage", "Speaking", new { examId, id = newId }) });
                }
                else
                {
                    return Json(new { url = Url.Action("RetellLecture", "Speaking", new { examId, id = newId }) });
                }




            }
            else
            {
                return RedirectToAction("ReadAloud", "Speaking", new { examId, id = newId });
            }

        }


        // GET: /<controller>/
        ///////////// Describe Image /////////////////////
        [Route("exam/{examId}/retell_lecture/{id}")]
        public IActionResult RetellLecture(long examId, long id)
        {
            var retellLecture = _db.RetellLectures.FirstOrDefault(e => e.ExamId == examId && e.RetellLectureId == id);

            return View(retellLecture);
        }

        [Route("exam/{examId}/retell_lecture/answer/{retellLectureId}")]
        public IActionResult AnswerRetellLecture(long examId, long retellLectureId)
        {
            var answerPath = "./answers/" + examId + "/speaking/retell_lecture/" + retellLectureId + ".wav";
            var temp = new RetellLectureAnswer()
            {
                ExamId = examId,
                User = "Miraj",
                RetellLectureId = retellLectureId,
                AnswerPath = answerPath
            };

            if (!_db.RetellLectureAnswers.Any(answer => answer.ExamId == examId && answer.RetellLectureId == retellLectureId))
            {
                _db.Add(temp);
                _db.SaveChanges();

            }


            var newId = 0;
            switch (retellLectureId)
            {
                case 1:
                    newId = 2;
                    break;

                case 2:
                    //set the id for answer short questions
                    newId = 1;
                    break;
                    //case 2:
                    //    newId = 3;
                    //    break;
                    //case 3:
                    //    newId = 4;
                    //    break;

            }
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (retellLectureId != 2)
                {
                    return Json(new { url = Url.Action("RetellLecture", "Speaking", new { examId, id = newId }) });
                }
                else
                {
                    return Json(new { url = Url.Action("AnswerShortQuestion", "Speaking", new { examId, id = newId }) });
                }




            }
            else
            {
                return RedirectToAction("ReadAloud", "Speaking", new { examId, id = newId });
            }

        }

        [HttpPost]
        //[Route ("Speaking/Upload/{examId}/{readAloudId}")]
        [Route("Speaking/Upload/{examId}/{typeId}/{itemId}")]
        public async Task<IActionResult> Upload(long examId,string typeId,long itemId)
        //public IActionResult Upload(long examId, long readAloudId)
        {

            IFormFile uploadedFile = Request.Form.Files[0]; //Uploaded file

            
            var fileName = "./answers/"+examId+"/speaking/"+ typeId +"/"+ itemId + ".wav";
            Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            using (var stream = new FileStream(fileName, FileMode.Create,FileAccess.Write))
            {
                await uploadedFile.CopyToAsync(stream);
                
            }

            switch (typeId)
            {
                case "read_aloud":
                    return RedirectToAction("AnswerReadAloud", "Speaking", new { examId, readAloudId=itemId });                    
                case "repeat_sentence":
                    return RedirectToAction("AnswerRepeatSentence", "Speaking", new { examId, repeatSentenceId=itemId });
                case "describe_image":
                    return RedirectToAction("AnswerDescribeImage", "Speaking", new { examId, describeImageId=itemId });
                case "retell_lecture":
                    return RedirectToAction("AnswerRetellLecture", "Speaking", new { examId, retellLectureId=itemId });
                case "answer_short_question":
                    return RedirectToAction("AnswerAnswerShortQuestion", "Speaking", new { examId, itemId });

            }
            return RedirectToAction("AnswerReadAloud", "Speaking",new { examId, itemId });
            
        }

      


    }
}
