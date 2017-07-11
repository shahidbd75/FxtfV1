using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FXTF.Lib.AdminRepository.Interfaces
{
    interface IDBOperation<T> where T : class
    {
        //Execute Inline Query
        Task<IEnumerable<T>> GetAllExecuteText(string strText);
        Task<T> GetByIDExecuteText(string strText);
        Task<int> ExecuteText(string strText);
        Task<dynamic> GetResultByExecuteText(string strText);
        //End of Execute Inline Query

        //Execute Stored Procedure
        Task<dynamic> ExecuteStoredProc(SqlCommand cmd);
        Task<T> GetByExecuteStoredProc(SqlCommand cmd);
        Task<IEnumerable<T>> GetAllExecuteStoredProc(SqlCommand cmd);
        Task<Tuple<string, int, bool, dynamic>> GetResultByExecuteStoredProc(SqlCommand cmd);
        //End of Execute Stored Procedure      
    }
}
