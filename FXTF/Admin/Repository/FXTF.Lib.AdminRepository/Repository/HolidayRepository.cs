using FXTF.Common.Infrastructure.CommonMethods;
using FXTF.Common.Infrastructure.Logging;
using FXTF.Lib.AdminModel.Model;
using FXTF.Lib.AdminRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace FXTF.Lib.AdminRepository.Repository
{
    public class HolidayRepository : DBOperations<Holiday>, IRepository<Holiday>
    {
        protected ILogger Logger { get; set; }
        public HolidayRepository()
        {
        }

        public Task<IEnumerable<Holiday>> GetAll()
        {
            try

            {
                var cmd = new SqlCommand("sp_Holiday");
                cmd.Parameters.AddWithValue("@pOptions", 4);
                var result = GetDataReaderProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw ex;
            }
        }

        public Task<Holiday> GetByID(long ID)
        {
            try
            {
                var cmd = new SqlCommand("sp_Holiday");
                cmd.Parameters.AddWithValue("@SL", ID);
                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 5);
                var result = GetByDataReaderProc(cmd);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw ex;
            }
        }

        public async Task<dynamic> Insert(Holiday Entity)
        {
            try
            {
                var cmd = new SqlCommand("sp_Holiday");
                cmd.Parameters.AddWithValue("@SL", Entity.SL);
                cmd.Parameters.AddWithValue("@HolidayStartDate", Entity.HolidayStartDate);
                cmd.Parameters.AddWithValue("@HolidayEndDate", Entity.HolidayEndDate);
                cmd.Parameters.AddWithValue("@Notes", Entity.Notes);
                cmd.Parameters.AddWithValue("@Status", Entity.Status);
                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 1);
                var result = ExecuteNonQueryProc(cmd);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw ex;
            }
        }

        public async Task<dynamic> Update(Holiday Entity)
        {
            try
            {
                var cmd = new SqlCommand("sp_Holiday");
                cmd.Parameters.AddWithValue("@SL", Entity.SL);
                cmd.Parameters.AddWithValue("@HolidayStartDate", Entity.HolidayStartDate);
                cmd.Parameters.AddWithValue("@HolidayEndDate", Entity.HolidayEndDate);
                cmd.Parameters.AddWithValue("@Notes", Entity.Notes);
                cmd.Parameters.AddWithValue("@Status", Entity.Status);


                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 2);

                var result = ExecuteNonQueryProc(cmd);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw ex;
            }
        }
        public async Task<dynamic> Delete(long SL)
        {
            try
            {
                var cmd = new SqlCommand("sp_Holiday");
                cmd.Parameters.AddWithValue("@SL", SL);
                cmd.Parameters.Add("@Msg", SqlDbType.NChar, 500);
                cmd.Parameters["@Msg"].Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@pOptions", 3);
                var result = ExecuteNonQueryProc(cmd);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
                throw ex;
            }
        }

        public Holiday Mapping(SqlDataReader sqldatareader)
        {
            try
            {
                Holiday oHoliday = new Holiday();
                oHoliday.SL = Helper.ColumnExists(sqldatareader, "SL") ? ((sqldatareader["SL"] == DBNull.Value) ? 0 : Convert.ToInt64(sqldatareader["SL"])) : 0;
                oHoliday.HolidayStartDate = Helper.ColumnExists(sqldatareader, "HolidayStartDate") ? ((sqldatareader["HolidayStartDate"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(sqldatareader["HolidayStartDate"])) : Convert.ToDateTime("01/01/1900");
                oHoliday.HolidayEndDate = Helper.ColumnExists(sqldatareader, "HolidayEndDate") ? ((sqldatareader["HolidayEndDate"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(sqldatareader["HolidayEndDate"])) : Convert.ToDateTime("01/01/1900");
                oHoliday.Notes = Helper.ColumnExists(sqldatareader, "Notes") ? sqldatareader["Notes"].ToString() : "";
                oHoliday.Status = Helper.ColumnExists(sqldatareader, "Status") ? ((sqldatareader["Status"] == DBNull.Value) ? 0 : Convert.ToInt16(sqldatareader["Status"])) : 0;
                return oHoliday;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
