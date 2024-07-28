using AppData.Data;
using AppData.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using AppData.Context;
using Microsoft.Identity.Client;

namespace AppData.Repository
{
    public class UserCbsRepository : IUserCbsRepository
    {
        private IUserCbsDBAccess _cbsDb;
        private readonly DapperContext _dapperContext;

        public UserCbsRepository(IUserCbsDBAccess cbsDb, DapperContext dapperContext)
        {
            _cbsDb = cbsDb;
            _dapperContext = dapperContext;
        }

        public async Task<CustomerCbsData> SaveCustomerCbsData(CustomerCbsData customerCbsData)
        {
            try
            {
                var query = "INSERT INTO CustomerCbsInfoViewTbl (ACC_NO, CUSTOMER_CODE, AC_TITLE, MOBILE_NO, NID_NO, AC_TYPE, TIN_NO, TRADE_LICENSE_NO, PASSPORT_NO, BIRTH_REGISTRATION_NO) VALUES (@ACC_NO, @CUSTOMER_CODE, @AC_TITLE, @MOBILE_NO, @NID_NO, @AC_TYPE, @TIN_NO, @TRADE_LICENSE_NO, @PASSPORT_NO, @BIRTH_REGISTRATION_NO)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

                var parameters = new DynamicParameters();
                parameters.Add("ACC_NO", customerCbsData.ACC_NO, DbType.String);
                parameters.Add("CUSTOMER_CODE", customerCbsData.CUSTOMER_CODE, DbType.String);
                parameters.Add("AC_TITLE", customerCbsData.AC_TITLE, DbType.String);
                parameters.Add("MOBILE_NO", customerCbsData.MOBILE_NO, DbType.String);
                parameters.Add("NID_NO", customerCbsData.NID_NO, DbType.String);
                parameters.Add("AC_TYPE", customerCbsData.AC_TYPE, DbType.String);
                parameters.Add("TIN_NO", customerCbsData.TIN_NO, DbType.String);
                parameters.Add("TRADE_LICENSE_NO", customerCbsData.TRADE_LICENSE_NO, DbType.String);
                parameters.Add("PASSPORT_NO", customerCbsData.PASSPORT_NO, DbType.String);
                parameters.Add("BIRTH_REGISTRATION_NO", customerCbsData.BIRTH_REGISTRATION_NO, DbType.String);

                
                using (var connection = _dapperContext.CreateConnection())
                {
                    //var id = await connection.ExecuteAsync(query, parameters);
                    var id = await connection.QuerySingleAsync<int>(query, parameters);                    

                    var createdCbsData = new CustomerCbsData
                    {
                        //Id = id,
                        ACC_NO = customerCbsData.ACC_NO,
                        CUSTOMER_CODE = customerCbsData.CUSTOMER_CODE,
                        AC_TITLE = customerCbsData.AC_TITLE,
                        MOBILE_NO = customerCbsData.MOBILE_NO,
                        NID_NO = customerCbsData.NID_NO,
                        AC_TYPE = customerCbsData.AC_TYPE,
                        TRADE_LICENSE_NO = customerCbsData.TRADE_LICENSE_NO,
                        PASSPORT_NO = customerCbsData.PASSPORT_NO,
                        BIRTH_REGISTRATION_NO = customerCbsData.BIRTH_REGISTRATION_NO
                    };
                    return createdCbsData;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }            
            
        }

        public async Task ClearTableRecords(string? tableName)
        {
            try
            {
                var query = "TRUNCATE TABLE [ReportLogin].[dbo].[CustomerCbsInfoViewTbl]";

                using (var connection = _dapperContext.CreateConnection())
                {
                    await connection.ExecuteAsync(query);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

