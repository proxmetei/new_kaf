using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineChat.Models.Chats;
using OnlineChat.Models.Users;
using OnlineChat.Models.Lecturer;
using OnlineChat.Models.Users;
using OnlineChat.Models.Messages;
using Microsoft.AspNetCore.Authorization;
using OnlineChat.ViewModels;
using Microsoft.AspNetCore.SignalR;
using OnlineChat.Hubs;
using Microsoft.AspNetCore.Http;
using System.IO;
using OnlineChat.Models.Documents;
using System.Security.Claims;
using Newtonsoft.Json;
using OnlineChat.Models.CategoryTable;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineChat.Controllers.Quiz
{
    public class LecturerQuizController : Controller
    {
        LecturerRepository repos;
        WallMessageRepository repos1;
        UserDAO repos2;
        const int MAXWorkload = 20;
        //[TempData]
        //public byte[] Photo { get; set; }
        public LecturerQuizController(LecturerRepository _repos,WallMessageRepository _repos1,UserDAO _repos2)
        {
            repos = _repos;
            repos1 = _repos1;
            repos2 = _repos2;
        }
        // GET: /<controller>/
        public IActionResult Quiz()
        {
            ViewBag.Edit = false;
            User user = repos2.GetUserOnEmail(this.User.FindFirstValue(ClaimTypes.Name));
            LecturerModel lecturer = repos.GetLecturerByUserId(user.Id);
            ViewBag.Lecturer = lecturer;
            //List<QuizModel> quizes = repos.getAllQuizes();
            //ViewBag.Quiz = (QuizModel)null;
            //foreach (QuizModel quiz in quizes) {
            //    if (quiz.LecturerId == lecturer.Id) {
            //        ViewBag.Quiz = quiz;
            //    }
            //}
            return View();
        }
        
        public IActionResult Save(QuizModel model)
        {
            repos.setQuiz(model);
            model.Workload = MAXWorkload - model.Workload;

            return RedirectToAction("Index", "Home");
        }
       
    }
}
