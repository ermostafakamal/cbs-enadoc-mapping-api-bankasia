using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public interface ILoginDBRepository
    {
        Task<IEnumerable<ReportLoginPerson>> GetReportLoginPerson(string username, string password);
    }
}
