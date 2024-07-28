//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AppData.Data
//{
//    internal interface IDataAccess
//    {
//    }
//}


//============================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters,
     string connectionId = "ConnectionString"
    );
        Task SaveData<P>
            (string query, P parameters, string connectionId = "ConnectionString");
    }
}
