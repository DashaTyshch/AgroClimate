using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dima.Database.Entities;
using Dima.Database.Models;
using System.Security.Cryptography;
using NpgsqlTypes;

namespace Dima.Database.Services
{
    public class PostgresService
    {
        private static PostgresService _instance;

        public static PostgresService Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new PostgresService();
                return _instance;
            }
        }

        private NpgsqlConnectionStringBuilder _builder;

        private static string RequestsInfoQuery => "SELECT r.request_name as request_name, " +
            "r.statereq as state, " +
            "com.name_company as company_name, " +
            "com.code_edrpou AS company_id, " +
            "eng.last_name as engineer_name, " +
            "eng.tab_number AS engineer_id, " +
            "bri.last_name as brigadier_name, " +
            "bri.telephone_number_of_brigadier AS brigadier_id " +
            "FROM request r " +
            "INNER JOIN company com ON r.code_edrpou = com.code_edrpou " +
            "INNER JOIN brigadier bri ON r.telephone_number_of_brigadier = bri.telephone_number_of_brigadier " +
            "INNER JOIN engineeragroclimate eng ON r.tab_number = eng.tab_number;";

        private static string AllRequestsQuery => "SELECT * FROM request";
        private static string GetRequestByIdQuery(string id)
        {
            return "SELECT * " +
                   "FROM request " +
                   $"WHERE request_name = '{id}';";
        }

        private static string AllBrigadiersQuery => "SELECT * FROM brigadier";

        private static string BrigByEmailQuery(string email) => $"SELECT * FROM brigadier WHERE email = '{email}'";

        private static string AuthQuery(string login, string pwd) =>
            "SELECT fullname " +
            "FROM manager " +
            $"WHERE login = '{login}' AND pwdhash = '{Hash(pwd)}'";

        private static string AddBrigadierQuery(Brigadier brigadier)
        {
            return "INSERT INTO brigadier "+ 
                $"VALUES ({brigadier.Telephone_Number_Of_Brigadier}, " +
                $"{ (brigadier.Address != null ? $"'{brigadier.Address}'" : "NULL")}, " +
                $"'{ brigadier.Last_Name}', " +
                $"'{brigadier.First_Name}', " +
                $"{ (brigadier.Patronym != null ? $"'{brigadier.Patronym}'" : "NULL")}, " +
                $"{(brigadier.Email != null ? $"'{brigadier.Email}'" : "NULL")}, " +
                $" {brigadier.Daily_Salary}, " +
                $"{brigadier.Car_Availability}, " +
                $"{brigadier.Mount_Kit_Availability} ); ";
        }

        private static string AddEngineerQuery(EngineerAgroclimate engineer)
        {
            return "INSERT INTO engineeragroclimate " +
                   $"VALUES ({engineer.Tab_Number}, '{engineer.Telephone_Number}', " +
                   $"        '{engineer.Last_Name}', '{engineer.First_Name}', '{engineer.Patronym}', " +
                   $"{ (engineer.Email != null ? $"'{engineer.Email}'" : "NULL") }); ";
        }

        private static string GetEngineerQuery(int tabNum) => 
            "SELECT * " +
            "FROM engineerAgroclimate " +
            $"WHERE tab_number = {tabNum} ;";

        private static string PojectsByReqNameQuery(string reqname) =>
            "SELECT * " +
            "FROM project " +
            $"WHERE request_name = '{reqname}'";

        private static string AddProjectQuery(byte[] file) =>
            "INSERT INTO project (projFile) " +
            $"VALUES({file});";

        private PostgresService()
        {
            _builder = new NpgsqlConnectionStringBuilder
            {
                Host = "ec2-54-228-252-67.eu-west-1.compute.amazonaws.com",
                Port = 5432,
                Username = "ygwfkmlgycwuhh",
                Password = "707b876a2e7ac8d5f4aca6562890a6a478d74d4294d9c3b10486eb95aa5b45f3",
                Database = "d44mr59875tji0",
                SslMode = SslMode.Require,
                UseSslStream = true,
            };
        }

        public List<RequestsInfo> GetRequestsInfo()
        {
            return QueryInternal<RequestsInfo>(RequestsInfoQuery).ToList();
        }


        public List<Request> GetAllRequest()
        {
            return QueryInternal<Request>(AllRequestsQuery).ToList();
        }

        public Request GetRequestById(string id)
        {
            return QueryInternal<Request>(GetRequestByIdQuery(id)).FirstOrDefault();
        }

        public List<Project> GetProjectsByReqName(string reqname)
        {
            return QueryInternal<Project>(PojectsByReqNameQuery(reqname)).ToList();
        }

        public List<Brigadier> GetAllBrigadiers()
        {
            return QueryInternal<Brigadier>(AllBrigadiersQuery).ToList();
        }

        public void AddBrigadier(Brigadier brigadier)
        {
            ExecuteInternal(AddBrigadierQuery(brigadier));
        }

        public string Auth(string login, string pwd)
        {
            return QuerySingleOrDefaultInternal<string>(AuthQuery(login, pwd));
        }

        public Brigadier GetByEmail(string email)
        {
            return QueryInternal<Brigadier>(BrigByEmailQuery(email)).FirstOrDefault();
        }

        public void AddEngineer(EngineerAgroclimate engineer)
        {
            ExecuteInternal(AddEngineerQuery(engineer));
        }

        public EngineerAgroclimate GetEngineer(int tabNum)
        {
            return QueryInternal<EngineerAgroclimate>(GetEngineerQuery(tabNum)).FirstOrDefault();
        }

        public void AddProject(byte[] file, string request)
        {
            
            var conn = new NpgsqlConnection(_builder.ToString())
            {
                UserCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            using (conn)
            {
                conn.Open();
                NpgsqlCommand insertCmd =
                    new NpgsqlCommand("INSERT INTO project (request_name, projFile) " +
                             $"VALUES('{request}', :dataParam);", conn);
                NpgsqlParameter param = new NpgsqlParameter("dataParam", NpgsqlDbType.Bytea);
                param.Value = file;
                insertCmd.Parameters.Add(param);
                insertCmd.ExecuteNonQuery();
            }
        }

        private IEnumerable<T> QueryInternal<T>(string sql)
        {
            IEnumerable<T> result;
            var conn = new NpgsqlConnection(_builder.ToString())
            {
                UserCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            using (conn)
            {
                var _ = conn.GetSchema().ChildRelations;
                result = conn.Query<T>(sql);
            }

            return result;

        }

        private T QuerySingleOrDefaultInternal<T>(string sql)
        {
            T result;
            var conn = new NpgsqlConnection(_builder.ToString())
            {
                // dirty hack. must be removed later
                UserCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            using (conn)
            {
                result = conn.QuerySingleOrDefault<T>(sql);
            }
            return result;
        }

        private void ExecuteInternal(string sql)
        {
            var conn = new NpgsqlConnection(_builder.ToString())
            {
                UserCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            using (conn)
            {
                conn.Execute(sql);
            }
        }


        private static string Hash(string value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
