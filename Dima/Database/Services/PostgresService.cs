using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dima.Database.Entities;

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
