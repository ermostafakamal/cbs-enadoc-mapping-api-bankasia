//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppData.Data
//{
//    public class UserCbsDBAccess
//    {
//    }
//}

//============================
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AppData.Data;

namespace AppData.Data
{
    public class UserCbsDBAccess : IUserCbsDBAccess
    {
        private readonly IConfiguration _config;

        public UserCbsDBAccess(IConfiguration config)
        {
            _config = config;
        }
        // this method will retuarn a list of type T
        public async Task<IEnumerable<T>> GetData<T, P>(string query, P parameters,
             string connectionId = "LoginDBConnectionString"
            )
        {
            using IDbConnection connection =
                new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(query, parameters);

        }

        //This method will not return anything
        public async Task SaveData<P>
            (string query, P parameters, string connectionId = "LoginDBConnectionString")
        {
            using IDbConnection connection =
                 new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
            //return await connection.QuerySingleAsync<int>(query, parameters);

        }
        
    }
}

