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
            //string conn = "ParallelDB-Azure-MySQLInApp";
            _connectionString = _configuration.GetConnectionString("ParallelDB");
            //_connectionString = _configuration.GetConnectionString("ParallelDB-Azure"); ParallelDB - Azure - MySQLInApp
            //_connectionString = _configuration.GetConnectionString("ParallelDB-Azure-MySQLInApp"); 
        }
        public IDbConnection CreateConnection()
            => new MySql.Data.MySqlClient.MySqlConnection(_connectionString);
    }
}
