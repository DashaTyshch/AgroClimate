using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dima.Database.Entities;
using Dima.Database.Models;

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
            "eng.last_name as engineer_name, " +
            "bri.last_name as brigadier_name " +
            "FROM request r " +
            "INNER JOIN company com ON r.code_edrpou = com.code_edrpou " +
            "INNER JOIN brigadier bri ON r.telephone_number_of_brigadier = bri.telephone_number_of_brigadier " +
            "INNER JOIN engineeragroclimate eng ON r.tab_number = eng.tab_number;";

        private static string AllBrigadiersQuery => "SELECT * FROM brigadier";
        private static string AllRequestsQuery => "SELECT * FROM request";


        private static string BrigByEmailQuery(string email)
            => $"SELECT * FROM brigadier WHERE email = '{email}'";

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

        public List<Brigadier> GetAllBrigadiers()
        {
            return QueryInternal<Brigadier>(AllBrigadiersQuery).ToList();
        }

        public List<Request> GetAllRequest()
        {
            return QueryInternal<Request>(AllRequestsQuery).ToList();
        }


        public Brigadier GetByEmail(string email)
        {
            return QueryInternal<Brigadier>(BrigByEmailQuery(email)).FirstOrDefault();
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
    }
}
