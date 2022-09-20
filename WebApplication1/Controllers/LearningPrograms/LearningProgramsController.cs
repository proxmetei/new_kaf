using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineChat.ViewModels;
using OnlineChat.Models.LearningPrograms;
using OnlineChat.Models.Documents;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineChat.Controllers.LearningPrograms
{
    public class LearnValue {
        int Subject { get; set; }
        int[] Next { get; set; }
    }
    public class LearnValue1
    {
       public string Subject { get; set; }
       public string Def { get; set; }
    }
    public class LearningProgramsController : Controller
    {
        LearningProgramRepository repos;
        [TempData]
        public string Users { get; set; }

        public LearningProgramsController(LearningProgramRepository _repos)
        {
            repos = _repos;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            LearningProgram learningProgram = repos.GetData();
            if (learningProgram != null)
            {
                ViewBag.PoslName = learningProgram.Doc_posl.Name;
                ViewBag.DefName = learningProgram.Doc_def.Name;
                string str = Encoding.UTF8.GetString(learningProgram.Doc_def.Data, 0, learningProgram.Doc_def.Data.Length);
                //LearnValue1 posl = JsonSerializer.Deserialize<LearnValue1>(str);
                LearnValue1 def = new LearnValue1();
                def.Subject = str.Split("|")[0];
                def.Def = str.Split("|")[1];
                //List<LearnValue> def = JsonSerializer.Deserialize<List<LearnValue>>(this.GetBytes2().ToString());
            }
            return View();
        }
        public IActionResult Generate()
        {
            LearningProgram learningProgram = repos.GetData();
            if (learningProgram != null)
            {
                ViewBag.PoslName = learningProgram.Doc_posl.Name;
                ViewBag.DefName = learningProgram.Doc_def.Name;

                List<LearnValue> posl = JsonSerializer.Deserialize<List<LearnValue>>(this.GetBytes1().ToString());
                List<LearnValue> def = JsonSerializer.Deserialize< List<LearnValue>>(this.GetBytes2().ToString());
            }
            return View();
        }
        public FileResult GetBytes1()
        {
            LearningProgram learningProgram = repos.GetData();
            byte[] mas = learningProgram.Doc_def.Data;
            string type = "";
            for (int i = learningProgram.Doc_def.Name.LastIndexOf('.'); i < learningProgram.Doc_def.Name.Length; i++)
            {
                type += learningProgram.Doc_def.Name[i];
            }
            string file_type = "application/" + type;
            string file_name = learningProgram.Doc_def.Name;
            return File(mas, file_type, file_name);
        }
        public FileResult GetBytes2()
        {
            LearningProgram learningProgram = repos.GetData();
            byte[] mas = learningProgram.Doc_posl.Data;
            string type = "";
            for (int i = learningProgram.Doc_posl.Name.LastIndexOf('.'); i < learningProgram.Doc_posl.Name.Length; i++)
            {
                type += learningProgram.Doc_posl.Name[i];
            }
            string file_type = "application/" + type;
            string file_name = learningProgram.Doc_posl.Name;
            return File(mas, file_type, file_name);
        }
        public IActionResult Edit(CreateModel model)
        {
            LearningProgram learningProgram = new LearningProgram();
            //learningProgram = null;

            if (model.File != null)
                using (var binaryReader = new BinaryReader(model.File.OpenReadStream()))
                {

                    Document document = new Document();
                    document.Name = model.File.FileName;
                    document.Data = binaryReader.ReadBytes((int)model.File.Length);
                    document.GUID = Guid.NewGuid().ToString();
                    learningProgram.Doc_posl = document;
                    //var result = new StringBuilder();
                    //using (var reader = new StreamReader(model.File.OpenReadStream()))
                    //{
                    //    while (reader.Peek() >= 0)
                    //        result.AppendLine(reader.ReadLine());
                    //}
                    //learningProgram.Doc_posl_str = result.ToString();
                }
            if (model.File1 != null)
                using (var binaryReader = new BinaryReader(model.File1.OpenReadStream()))
                {
                    Document document = new Document();
                    document.Name = model.File1.FileName;
                    document.Data = binaryReader.ReadBytes((int)model.File1.Length);
                    document.GUID = Guid.NewGuid().ToString();
                    learningProgram.Doc_def = document;
                    //var result = new StringBuilder();
                    //using (var reader = new StreamReader(model.File1.OpenReadStream())) {
                    //    while (reader.Peek() >= 0)
                    //        result.AppendLine(reader.ReadLine());
                    //}
                    //learningProgram.Doc_def_str = result.ToString();
                }

                repos.setData(learningProgram);
            return RedirectToAction("Index", "LearningPrograms");
        }
    }
}
