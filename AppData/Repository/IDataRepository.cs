using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public interface IDataRepository
    {
        Task<IEnumerable<BankasiaBranch>> GetAllBranches(int documentIndex);
        Task<IEnumerable<BankasiaEnadoc>> GetBankasiaEnadoc(string type, string branch, string user, string? fromDate, string? toDate);
        Task<IEnumerable<DocUploader>> GetAllUploaders();

    }
}

