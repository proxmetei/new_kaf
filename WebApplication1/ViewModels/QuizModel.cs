using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineChat.Models.News;
using System.ComponentModel.DataAnnotations;
namespace OnlineChat.ViewModels
{
    public class QuizModel
    {
        //[Required(ErrorMessage = "Не указано название")]
        public bool Aspirantura { get; set; }
        [Required]
        public int Experience { get; set; }
        public int Id { get; set; }
        public double Result { get; set; }
        public int LecturerId { get; set; }
        [RegularExpression(@"\b([1-9]|10)\b", ErrorMessage = "Value can be from 1 to 10")]
        public int Angular { get; set; }
        [RegularExpression(@"\b([1-9]|10)\b", ErrorMessage = "Value can be from 1 to 10")]
        public int Vue { get; set; }
        [RegularExpression(@"\b([1-9]|10)\b", ErrorMessage = "Value can be from 1 to 10")]
        public int React { get; set; }
        [RegularExpression(@"\b([1-9]|10)\b", ErrorMessage = "Value can be from 1 to 10")]
        public int NodeJs { get; set; }
        public int Workload { get; set; }
        public int dotNet { get; set; }
        public int JavaSpring { get; set; }
        public int Scala { get; set; }

    }
}
