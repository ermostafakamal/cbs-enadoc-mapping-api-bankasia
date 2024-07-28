using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public interface IUserCbsRepository
    {
        Task<CustomerCbsData> SaveCustomerCbsData(CustomerCbsData customerCbsData);
        Task ClearTableRecords(string? tablename);
    }
}
