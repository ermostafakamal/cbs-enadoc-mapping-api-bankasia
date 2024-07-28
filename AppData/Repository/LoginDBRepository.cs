using AppData.Data;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public class LoginDBRepository: ILoginDBRepository
    {
        private ILoginDBAccess _db;
        public LoginDBRepository(ILoginDBAccess db)
        {
            _db = db;
        }
 
        public async Task<IEnumerable<ReportLoginPerson>> GetReportLoginPerson(string username, string password)
        {
            string query = "SELECT [UserName] As Username, [Password] As Password" +
                " FROM [UploadReport].[dbo].[ReportLoginDetails]" +
                "Where [UserName] = '" + username + "' and [Password] = '" + password + "'";            

            var person = await _db.GetData<ReportLoginPerson, dynamic>(query, new { });
            return person;        

        }
    }
}
