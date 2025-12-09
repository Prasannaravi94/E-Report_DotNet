using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DBase_EReport
{
   public class DB_EReporting
    {

       //private string strConn = System.Configuration.ConfigurationManager.AppSettings["Ereportcon"];
       private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
       private int iReturn = -1;
        
        public DataTable Exec_DataTable(string strQry)
        {
            DataSet ds_EReport = null;
            DataTable dt_EReport = null;

            try
            {
                ds_EReport = Exec_DataSet(strQry);
                dt_EReport = ds_EReport.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt_EReport;
        }

        
        public DataSet Exec_DataSet(string strQry)
        {
            DataSet ds_EReport = new DataSet();

            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
                
                
                SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                selectCMD.CommandTimeout = 800;

                SqlDataAdapter da_EReport = new SqlDataAdapter();
                da_EReport.SelectCommand = selectCMD;

                _conn.Open();
                
                da_EReport.Fill(ds_EReport, "Customers");

               // _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return ds_EReport;
        }

        public DataSet Exec_DataSet(string strQry, SqlCommand cmd)
        {
            DataSet ds_EReport = new DataSet();

            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
                //SqlConnection _conn = new SqlConnection(strConn);

                //SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                cmd.Connection = _conn;
                cmd.CommandText = strQry;
                cmd.CommandTimeout = 800;
                
                SqlDataAdapter da_EReport = new SqlDataAdapter();
                da_EReport.SelectCommand = cmd;

                _conn.Open();

                da_EReport.Fill(ds_EReport, "Customers");

                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return ds_EReport;
        }

        public int Exec_Scalar(string strQry)
        {
            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
                iReturn = -1;
               // SqlConnection _conn = new SqlConnection(strConn);
                SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                selectCMD.CommandTimeout = 800;                             
                _conn.Open();
                iReturn = Convert.ToInt32(selectCMD.ExecuteScalar());
               // _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return iReturn;
        }

        public int Exec_Scalar(string strQry, SqlCommand cmd)
        {
            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
                iReturn = -1;
                //SqlConnection _conn = new SqlConnection(strConn);
                //SqlCommand selectCMD = new SqlCommand(strQry, _conn);
                cmd.Connection = _conn;
                cmd.CommandText = strQry;
                cmd.CommandTimeout = 800;
                _conn.Open();
                iReturn = Convert.ToInt32(cmd.ExecuteScalar());
                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return iReturn;
        }

        public int ExecQry(string sQry)
        {
            iReturn = -1;
            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
                //SqlConnection _conn = new SqlConnection(strConn);
                System.Data.SqlClient.SqlCommand cmd;
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = sQry;
                cmd.CommandTimeout = 800;
                _conn.Open();
                iReturn = cmd.ExecuteNonQuery();
               // _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return iReturn;
        }

        public int ExecQry(string sQry, SqlCommand cmd)
        {
            iReturn = -1;
            SqlConnection _conn = new SqlConnection(strConn);
            try
            {
               // SqlConnection _conn = new SqlConnection(strConn);
                //System.Data.SqlClient.SqlCommand cmd;
                //cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = sQry;
                _conn.Open();
                iReturn = cmd.ExecuteNonQuery();
               // _conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
            return iReturn;
        }
        public int ExecQry(string sQry, SqlConnection _conn, SqlTransaction tran)
        {
            iReturn = -1;
            try
            {
                //SqlConnection _conn = new SqlConnection(strConn);
                System.Data.SqlClient.SqlCommand cmd;
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandText = sQry;
                cmd.Transaction = tran;
                //_conn.Open();
                iReturn = cmd.ExecuteNonQuery();

                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int Exec_Scalar(string strQry, SqlConnection _conn, SqlTransaction tran)
        {
            try
            {
                iReturn = -1;
                //SqlConnection _conn = new SqlConnection(strConn);
                SqlCommand selectCMD = new SqlCommand(strQry, _conn, tran);
                selectCMD.CommandTimeout = 800;
                //_conn.Open();
                iReturn = Convert.ToInt32(selectCMD.ExecuteScalar());
                //_conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        internal DataTable Exec_DataTableWithParam(string CommandName, CommandType cmdType, SqlParameter[] param)
        {
            DataTable table = new DataTable();

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                   
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.CommandTimeout = 800;
                    cmd.Parameters.AddRange(param);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(table);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return table;
        }

        internal bool Exec_NonQueryWithParam(string CommandName, CommandType cmdType, SqlParameter[] pars)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(strConn))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                  
                    cmd.CommandType = cmdType;
                    cmd.CommandText = CommandName;
                    cmd.CommandTimeout = 800;
                    cmd.Parameters.AddRange(pars);

                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }

                        result = cmd.ExecuteNonQuery();
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return (result > 0);
        }    
    }
}
