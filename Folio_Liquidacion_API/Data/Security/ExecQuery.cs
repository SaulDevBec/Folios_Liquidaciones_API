using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Data.Security
{
    public class ExecQuery:DataAccess
    {
        public DataTable ExecQueryDT(string query)
        {
            try
            {
                OpenConnection();
                return GetQuery(query);
            }
            catch (Exception oEx)
            {
                throw new Exception(oEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet ExecQueryDS(string query)
        {
            try
            {
                OpenConnection();
                return GetQuery(query, true);
            }
            catch (Exception oEx)
            {
                throw new Exception(oEx.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool ExecQueryBool(string query)
        {
            try
            {
                OpenConnection();
                BeginTransaction();
                if (ExecuteQuery(query) > 0)
                {
                    Commit();
                    return true;
                }
                else
                {
                    RollBack();
                    return false;
                }
            }
            catch (Exception oEx)
            {
                RollBack();
                throw oEx;
            }
            finally
            {
                CloseConnection();
                GC.Collect();
            }
        }
    }
}