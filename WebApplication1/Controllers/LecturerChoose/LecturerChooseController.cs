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

namespace OnlineChat.Controllers.LecturerChoose
{
    public class LecturerChooseController : Controller
    {
        LecturerRepository repos;
        WallMessageRepository repos1;
        UserDAO repos2;
        //[TempData]
        //public byte[] Photo { get; set; }
        public LecturerChooseController(LecturerRepository _repos,WallMessageRepository _repos1,UserDAO _repos2)
        {
            repos = _repos;
            repos1 = _repos1;
            repos2 = _repos2;
        }
        // GET: /<controller>/
        public IActionResult Index(int? catId)
        {
            ViewBag.Edit = false;
            List<QuizModel> quizDetails = repos.getAllQuizes();
            List<double[]> results = new List<double[]>();
            Dictionary<int, int> lecDic= new Dictionary<int, int>();
            double[][] category= new double[7][];
            for (int i = 0; i < 7; i++) {
                category[i] = new double[quizDetails.Count];
            }
            for (int i = 0; i < quizDetails.Count; i++) {
                lecDic.Add(i,quizDetails[i].LecturerId);
                category[0][i] = quizDetails[i].Experience;
                if (quizDetails[i].Aspirantura == true)
                {
                    category[1][i] = 1;
                }
                else
                {
                    category[1][i] = 0;
                }
                category[2][i] = quizDetails[i].Workload;
                category[3] [i] = quizDetails[i].Angular;
                category[4][i] = quizDetails[i].React;
                category[5][i] = quizDetails[i].Vue;
                category[6][i] = quizDetails[i].NodeJs;
            }
            for (int i = 0; i < 7; i++) {
                results.Add(AnalizService.getWeight(category[i]));
            }
            double[] result;
            if (catId==null)
            {
                result = AnalizService.proizvWithoutcat(results, quizDetails.Count);
            }
            else {
                result = AnalizService.proizvWithcat(results, quizDetails.Count, (int)catId);
            }

            List<LecturerModel> lecs = repos.GetAllLecturers().ToList();
            List<LecturerModel> lecsResult = new List<LecturerModel>();
            for (int i = 0; i < result.Length; i++)
            {
                quizDetails[i].Result = result[i]; 
            }
                quizDetails.Sort((x,y)=>  y.Result.CompareTo(x.Result) );
            for (int i = 0; i < quizDetails.Count; i++) {
                lecsResult.Add(lecs.Find((elem)=>(elem.Id == quizDetails[i].LecturerId)));
            }
            foreach (LecturerModel lec in lecsResult) {
                User user = repos2.Get(lec.UserId);
                lec.FIO = user.FIO;
            }
            ViewBag.Users = lecsResult;
            return View();
        }
       
    }
}
