using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _config;
        //private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _config = configuration;
            //_connectionString = _config.GetConnectionString("LoginDBConnectionString");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_config.GetConnectionString("LoginDBConnectionString"));
    }
}
