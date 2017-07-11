using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace FXTF.Lib.AdminRepository
{
    public static class MSSQLConn
    {
        public static SqlConnection MSSQLConnection()
        {
            SqlConnection conn = null;
            try
            {
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConAzureProduction"].ConnectionString);
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConAzureProductionSTG"].ConnectionString);
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConAzureDev"].ConnectionString);
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConAzureDevSTG"].ConnectionString);
                //conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConAzureProductionSlaveDB"].ConnectionString);
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FxtfConLocal"].ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }
    }
}
