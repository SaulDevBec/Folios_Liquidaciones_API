using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Data.Security
{
    public class DataAccess
    {
        #region Variables
        private SqlConnection oConnection;
        private SqlTransaction oTransaction;
        #endregion

        public void BeginTransaction() => oTransaction = oConnection.BeginTransaction();

        public void Commit() => oTransaction.Commit();

        public void RollBack() => oTransaction.Rollback();

        public void OpenConnection()
        {
            try
            {
                if (ValidateConnection())
                {
                    CloseConnection();
                }

                //oConnection = new SqlConnection("Data Source=172.35.40.90;Initial Catalog=Tracusa;User Id=trscnn;Password=trsSQL123;Trusted_Connection=False;");
                oConnection = new SqlConnection("Data Source=172.35.40.90;Initial Catalog=TracusaTest;User Id=trscnn;Password=trsSQL123;Trusted_Connection=False;");

                oConnection.Open();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex.InnerException);
            }
        }

        private bool ValidateConnection()
        {
            if (oConnection != null)
                if (oConnection.State != ConnectionState.Closed)
                    return true;

            return false;
        }

        public void CloseConnection()
        {
            try
            {
                if (ValidateConnection())
                {
                    oConnection.Close();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message, Ex.InnerException);
            }
            finally
            {
                oConnection = null;
                GC.Collect();
            }
        }

        public DataTable GetQuery(string sqlQuery)
        {
            SqlDataAdapter oDa;
            DataSet oDs = new DataSet();

            if (ValidateConnection())
            {
                try
                {
                    oDa = new SqlDataAdapter(sqlQuery, oConnection);
                    oDa.SelectCommand.Transaction = oTransaction;
                    oDa.SelectCommand.CommandTimeout = 0;
                    oDa.Fill(oDs);
                    return oDs.Tables[0];
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message, Ex.InnerException);
                }
                finally
                {
                    oDa = null;
                    oDs = null;
                    GC.Collect();
                }
            }
            else
            {
                throw new Exception("No existe conexión con la base de datos.");
            }
        }

        public DataSet GetQuery(string sqlQuery, bool blnFlag)
        {
            SqlDataAdapter oDa;
            DataSet oDs = new DataSet();
            if (ValidateConnection())
            {
                try
                {
                    oDa = new SqlDataAdapter(sqlQuery, oConnection);
                    oDa.SelectCommand.Transaction = oTransaction;
                    oDa.SelectCommand.CommandTimeout = 0;
                    oDa.Fill(oDs);
                    return oDs;
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message, Ex.InnerException);
                }
                finally
                {
                    oDa = null;
                    oDs = null;
                    GC.Collect();
                }
            }
            else
                throw new Exception("No existe conexión con la base de datos.");
        }

        public int ExecuteQuery(string sqlQuery)
        {
            SqlCommand oCmd;

            if (ValidateConnection())
            {
                try
                {
                    oCmd = new SqlCommand(sqlQuery, oConnection);
                    oCmd.Transaction = oTransaction;
                    oCmd.CommandTimeout = 0;
                    return oCmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    throw new Exception(Ex.Message, Ex.InnerException);
                }
                finally
                {
                    oCmd = null;
                    GC.Collect();
                }
            }
            else
                throw new Exception("No existe conexión con la base de datos.");
        }
    
}
}