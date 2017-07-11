using FXTF.Lib.AdminModel.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FXTF.BusinessLayer.Admin.Interfaces
{
    interface IHoliDayBL
    {
        Task<IEnumerable<Holiday>> GetAllHoliday();
        Task<Holiday> GetHolidayByID(long ID);
        Task<dynamic> InsertHoliday(Holiday Entity);
        Task<dynamic> DeleteHoliday(long ID);
        Task<dynamic> UpdateHoliday(Holiday Entity);
    }
}
