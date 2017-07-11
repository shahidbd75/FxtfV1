using AutoMapper;
using FXTF.Lib.AdminRepository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace FXTF.Lib.AdminRepository
{
    public abstract class DBOperations<TEntity> where TEntity : class
    {
        private static SqlConnection con = MSSQLConn.MSSQLConnection();

        #region Execute Inline Query
        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="strText"> sql Query</param>
        /// <returns> result </returns>
        public virtual async Task<SqlDataReader> GetDataReader(string strText)
        {
            SqlDataReader sqlreader;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.Connection = con;
                sqlreader = cmd.ExecuteReader();
                try
                {
                    while (sqlreader.Read())
                    {
                        return await Task.FromResult(sqlreader);
                    }
                }
                finally
                {
                    sqlreader.Close();
                }
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(sqlreader);
        }

        /// <summary>
        /// ExecuteText (Only execute)
        /// </summary>
        /// <param name="strText">sql query</param>
        /// <returns>result</returns>
        public virtual async Task<int> ExecuteText(string strText)
        {
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                return await Task.FromResult(cmd.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// GetReult
        /// </summary>
        /// <param name="strText">sql query</param>
        /// <returns>result</returns>
        public virtual async Task<dynamic> GetResultByExecuteText(string strText)
        {
            dynamic record = null;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand(strText);
                cmd.Connection = con;
                var reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = (reader[0] == DBNull.Value) ? 0 : reader[0];
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(record);
        }
        #endregion  Execute Inline Query


        #region Execute Stored Procedure 

        /// <summary>
        /// ExecuteNonQueryProc
        /// </summary>
        /// <param name="cmd"> sql command</param>
        /// <returns>result</returns>
        public virtual async Task<dynamic> ExecuteNonQueryProc(SqlCommand cmd)
        {
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                await Task.FromResult(cmd.ExecuteNonQuery());
                return (string)cmd.Parameters["@Msg"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
                //return ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// GetByExecuteStoredProc
        /// </summary>
        /// <param name="cmd">sql command</param>
        /// <returns>result Tentity</returns>
        public virtual async Task<IEnumerable<TEntity>> GetDataReaderProc(SqlCommand cmd)
        {
            var list = new List<TEntity>();
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();

                try
                {
                    list = DynamicMappingList<TEntity>(reader);

                    //while (reader.Read())
                    //{                     
                    //    //list.Add(PopulateRecord(reader, typeof(TEntity).Name));
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(list);
        }
        public virtual async Task<TEntity> GetByDataReaderProc(SqlCommand cmd)
        {
            //var list = new List<TEntity>();
            TEntity record = null;
            try
            {
                if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                try
                {
                    record = DynamicMapping<TEntity>(reader);
                    //while (reader.Read())
                    //{
                    //    record = PopulateRecord(reader, typeof(TEntity).Name);
                    //}
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return await Task.FromResult(record);
        }
        #endregion Execute Stored Procedure



        public List<T> ReadData<T>(SqlDataReader reader)
        {
            if (reader.HasRows)
                return Mapper.Map<IDataReader, List<T>>(reader);
            return null;
        }


        public virtual List<T> DynamicMappingList<T>(SqlDataReader reader)
        {
            var results = new List<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                var item = Activator.CreateInstance<T>();
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                }
                results.Add(item);
            }
            return results;
        }



        public virtual T DynamicMapping<T>(SqlDataReader reader)
        {
            var item = Activator.CreateInstance<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read())
            {
                foreach (var property in typeof(T).GetProperties())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
                    {
                        Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                        property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                    }
                }
            }
            return item;
        }





        public virtual dynamic PopulateRecord(SqlDataReader sqldatareader, string TableName)
        {
            try
            {
                switch (TableName)
                {
                    case "Holiday": return new HolidayRepository().Mapping(sqldatareader);
                    default: return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
