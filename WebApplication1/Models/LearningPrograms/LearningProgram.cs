using System;
using OnlineChat.Models.Documents;
namespace OnlineChat.Models.LearningPrograms
{
    public class LearningProgram
    {
	    public int Doc_posl_id { get; set; }
        public Document Doc_posl { get; set; }
        public int Doc_def_id { get; set; }
        public string Doc_def_str { get; set; }
        public string Doc_posl_str { get; set; }
        public Document Doc_def { get; set; }
    }
}
