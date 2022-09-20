using System;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OnlineChat.Models.Users;
using System.Linq;
using System.Threading.Tasks;
using OnlineChat.Models.Chats;
using OnlineChat.Models.Documents;
namespace OnlineChat.Models.LearningPrograms
{
        public class LearningProgramRepository : LearningProgram
        {

        private readonly AESCrypt crypt;
        private readonly IDbConnectionFactory connectionFactory;
        private readonly IDbConnection connection;
        public LearningProgramRepository(IDbConnectionFactory _connectionFactory, AESCrypt _crypt)
        {
            this.connectionFactory = _connectionFactory;
            this.crypt = _crypt;
            connection = connectionFactory.GetDbConnection();
        }
        public LearningProgram GetData()
            {

                    string sql = "SELECT doc_posl_id AS Doc_posl_id, doc_def_id AS Doc_def_id, doc_posl_str AS Doc_posl_str,  doc_def_str AS Doc_def_str " +
                        "FROM public.learning";

                    LearningProgram learningProgram = connection.Query<LearningProgram>(sql).FirstOrDefault();
            if(learningProgram !=null)
            learningProgram.Doc_def = this.GetDoc(learningProgram.Doc_def_id);
            if (learningProgram!= null)
                learningProgram.Doc_posl = this.GetDoc(learningProgram.Doc_posl_id);
            return learningProgram;
        }
            public bool setData(LearningProgram learningProgram)
            {
            var sqlQuery = "TRUNCATE Documents CASCADE";
            connection.Execute(sqlQuery, learningProgram.Doc_def);
            var sqlQuery1 = "INSERT INTO Documents (guid, name, Data) VALUES(@GUID, @Name, @Data)";
            connection.Execute(sqlQuery1, learningProgram.Doc_def);
            var sqlQuery2 = "INSERT INTO Documents (guid, name, Data) VALUES(@GUID, @Name, @Data)";
            connection.Execute(sqlQuery2, learningProgram.Doc_posl);
            learningProgram.Doc_def_id = GetDocument(learningProgram.Doc_def.GUID).Id;
            learningProgram.Doc_posl_id = GetDocument(learningProgram.Doc_posl.GUID).Id;
            var sqlQuery3 = "INSERT INTO Learning (doc_posl_id, doc_def_id, doc_posl_str, doc_def_str) VALUES(@Doc_posl_id, @Doc_def_id,  @Doc_posl_str, @Doc_def_str)";
            connection.Execute(sqlQuery3, learningProgram);
            return true;
                
            }

            public bool UpdateLecturerAchivements(string achivs, int lecId)
            {

                    string sqlCheck = "SELECT lecturer_id FROM public.lecturer " +
                        "WHERE lecturer_id = @ID";
                    string sqlUpd = "UPDATE public.lecturer SET achivements=@ACH WHERE lecturer_id = @ID";

                    if (connection.Query<string>(sqlCheck, new { ID = lecId }).FirstOrDefault() == null)
                    {
                        return false;
                    }

                    connection.Execute(sqlUpd, new { ID = lecId, ACH = achivs });

                    return true;
                
            }
        public Document GetDocument(string GUID)
        {
            return connection.Query<Document>("Select * FROM Documents WHERE GUID = @GUID", new { GUID }).FirstOrDefault();
        }
        public Document GetDoc(int id)
        {
            return connection.Query<Document>("SELECT * FROM Documents WHERE id = @id", new { id }).FirstOrDefault();
        }
    }
    }

