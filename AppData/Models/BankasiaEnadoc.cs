using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class BankasiaEnadoc
    {

        public long DocumentId { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string CIDNo { get; set; }
        public string AccountType { get; set; }
        public string AccountTitle { get; set; }
        public string MobileNo { get; set; }
        public string DocumentType { get; set; }
        public string NIDNo { get; set; }
        public string TINNo { get; set; }
        public string TradeLicNo { get; set; }
        public string Passport { get; set; }
        public string BirthRegNo { get; set; }
        public string FileLocation { get; set; }
        public string UserName { get; set;}
        public DateTime DateTime { get;set; }
        public int PageCount { get;set; }
        public string? Recipient { get; set; } 
        public string? IPAddress { get; set; } 

    }
}
