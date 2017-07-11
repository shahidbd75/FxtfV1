using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FXTF.Lib.AdminRepository.Interfaces
{
    interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(long ID);
        Task<dynamic> Insert(T Entity);
        Task<dynamic> Delete(long ID);
        Task<dynamic> Update(T Entity);
        T Mapping(SqlDataReader sqldatareader);
    }
}
