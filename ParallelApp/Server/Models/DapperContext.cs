using System.Data;
using Dapper;

//using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySql;

namespace ParallelApp.Server.Models
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ParallelDB");
        }
        public IDbConnection CreateConnection()
            => new MySql.Data.MySqlClient.MySqlConnection(_connectionString);
    }
}
