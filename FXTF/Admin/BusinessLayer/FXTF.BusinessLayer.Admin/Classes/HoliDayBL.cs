using FXTF.BusinessLayer.Admin.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FXTF.Lib.AdminModel.Model;
using FXTF.Lib.AdminRepository.Repository;

namespace FXTF.BusinessLayer.Admin.Classes
{
    public class HoliDayBL: IHoliDayBL
    {
        
        public async Task<IEnumerable<Holiday>> GetAllHoliday()
        {
            try
            {
                var result = await new HolidayRepository().GetAll();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<Holiday> GetHolidayByID(long ID)
        {
            try
            {
                var result = new HolidayRepository().GetByID(ID);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<dynamic> InsertHoliday(Holiday Entity)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> UpdateHoliday(Holiday Entity)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> DeleteHoliday(long ID)
        {
            throw new NotImplementedException();
        }
    }
}
