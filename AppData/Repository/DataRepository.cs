using AppData.Data;
using AppData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{

    public class DataRepository : IDataRepository
    {
        private IDataAccess _db;
        public DataRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BankasiaBranch>> GetAllBranches(int documentIndex)
        {
            string query = "SELECT [DocumentIndexPropertyID] As DocumentIndexPropertyID, [Value] As Value" +
                " FROM [Enadoc12663C91F9DE000].[dbo].[IndexReference]" +
                "Where [DocumentIndexPropertyID] = " + documentIndex + "" +
                " ORDER BY [Value] ASC";

            var allBranches = await _db.GetData<BankasiaBranch, dynamic>(query, new { });
            return allBranches;
        }
        public async Task<IEnumerable<BankasiaEnadoc>> GetBankasiaEnadoc(string type, string branch, string user, string? fromDate, string? toDate)
        {
            string query;

            if (branch == null)
            {
                branch = "Paltan Branch";
            }
            if (user == null)
            {
                branch = "Admin";
            }
            if (fromDate == null)
            {
                fromDate = (DateTime.Now.AddYears(-3)).ToString();
            }
            if (toDate == null)
            {
                toDate = (DateTime.Now).ToString();
            }

            if (type == "View Report")
            {
                if (user == "All")
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[View] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[View].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[View].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
                else
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[View] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[View].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[View].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [UserName] = '" + user + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }

            }
            else if (type == "Email Report")
            {
                if (user == "All")
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation, [Recipient] as Recipient, [DateTime] as DateTime, [IPAddress] as IPAddress" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Email] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Email].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Email].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
                else
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation, [Recipient] as Recipient, [DateTime] as DateTime, [IPAddress] as IPAddress" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Email] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Email].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Email].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [UserName] = '" + user + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }

            }
            else if (type == "Upload Report")
            {
                if (user == "All")
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation, [UserName], [DateTime], [PageCount]" +                        
                        "FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[DocumentInformation] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000]. [dbo].[DocumentInformation].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Document] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] = [Enadoc12663C91F9DE000].[dbo].[Document].[ID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[DocumentInformation].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
                else
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation, [UserName], [DateTime], [PageCount]" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[DocumentInformation] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000]. [dbo].[DocumentInformation].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Document] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] = [Enadoc12663C91F9DE000].[dbo].[Document].[ID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[DocumentInformation].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [UserName] = '" + user + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
            }
            else if (type == "Print Report")
            {
                if (user == "All")
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Print] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Print].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Print].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
                else
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Print] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Print].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Print].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [UserName] = '" + user + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }

            }
            else if (type == "Download Report")
            {
                if (user == "All")
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Download] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Download].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Download].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }
                else
                {
                    query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[Download] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[Download].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[Download].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [UserName] = '" + user + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";

                }

            }
            else {
                query = "SELECT [B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] As DocumentId, [Branch Name] As BranchName, [Account No] As AccountNo, [CID No] As CIDNo, [Account Type] as AccountType, [Account Title] As AccountTitle,[Mobile No] as MobileNo, [Document Type] as DocumentType, [NID No] as NIDNo,[TIN No] as TINNo,[Trade Lic No] as TradeLicNo, [Passport] as Passport, [Birth Reg No] as BirthRegNo, [File Location] as FileLocation" +
                        " FROM [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828]" +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[View] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[B2B349EF-89B3-40BB-82B5-E17EDD9F2828].[DOCUMENTID] =[Enadoc12663C91F9DE000].[dbo].[View].[DocumentID] " +
                        "Left Join [Enadoc12663C91F9DE000].[dbo].[User] " +
                        "On [Enadoc12663C91F9DE000].[dbo].[View].[UserID] = [Enadoc12663C91F9DE000].[dbo].[User].[ID] " +
                        "Where [Branch Name] = '" + branch + "' and [DateTime] Between '" + fromDate + "' and '" + toDate + "'" +
                        " ORDER BY [DateTime] DESC";
            }




            var viewreport = await _db.GetData<BankasiaEnadoc, dynamic>(query, new { });
            return viewreport;
        }

        public async Task<IEnumerable<DocUploader>> GetAllUploaders()
        {
            string query = "SELECT [OrganizationID] As OrganizationID, [UserName] As UserName" +
                " FROM [Enadoc12663C91F9DE000].[dbo].[User]" +
                " ORDER BY [UserName] ASC";

            var allDocUploaders = await _db.GetData<DocUploader, dynamic>(query, new { });
            return allDocUploaders;
        }

    }
}

