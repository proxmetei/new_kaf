using System;
using System.Collections.Generic;
namespace OnlineChat.Models.LearningPrograms
{



        public interface ILecturerRepository
        {
            public LearningProgram GetData();

            public bool SetData(LearningProgram lecturer);


        }
    }
