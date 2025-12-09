using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class CallPlan
    {
        private string strQry = string.Empty;

        public DataSet FetchTerritoryName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code Territory_Code, " +
                     " (a.Territory_Name +  ' (' + CAST((select COUNT(distinct b.ListedDrCode) from Call_Plan b " +
                     " where a.Territory_Code=b.Territory_Code and b.ListedDr_Active_Flag=0 and sf_code = '" + sf_code + "' ) as CHAR(3)) " +
                     " + ') ' ) Territory_Name " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
        public DataSet GetTerritoryName(string sf_code,string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT  Territory_Code, " +
                     "  Territory_Name " +
                     " FROM  Mas_Territory_Creation  where sf_code = '" + sf_code + "' AND Territory_Code = '" + terr_code + "'";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }

        public DataTable get_ListedDoctor_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT DISTINCT d.ListedDrCode,d.ListedDr_Name, t.territory_name, c.Territory_Code, c.Plan_No, '' color FROM " +
                        "Mas_ListedDr d, Mas_Territory_Creation t, Call_Plan c " +
                        "WHERE c.Sf_Code =  '" + sfcode + "' and " +
                        " (c.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " c.Territory_Code like '%" + ',' + TerrCode + ',' + "%') and " +
                        " c.ListedDrCode = d.ListedDrCode and " +
                        " c.Territory_Code  = t.Territory_Code and " +
                        " c.ListedDr_Active_Flag = 0 " +
                        " Order By 2";
            try
            {
                //dsListedDR = db_ER.Exec_DataSet(strQry);
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public int Std_WorkPlan(string Territory_Code, string Doc_Code, string sf_code, int Plan_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Call_Plan " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Plan_No = " + Plan_No + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Remove_CallPlan(string Territory_Code, string Doc_Code, string sf_code, int Plan_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Delete from Call_Plan " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Territory_Code = '" + Territory_Code + "'  ";

                iReturn = db.ExecQry(strQry);

                //strQry = "Update Mas_ListedDr " +
                //         " Set Territory_Code = 0 " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                //iReturn = db.ExecQry(strQry);
            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Update_CallPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Call_Plan " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Copy_WorkPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //Insert a record into Call Plan

                strQry = "SELECT ISNULL(MAX(cast(Plan_No as int)),0)+1 FROM Call_Plan ";
                int iPlanNo = db.Exec_Scalar(strQry);

                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Call_Plan values('" + sf_code + "', '" + Territory_Code + "', getdate(), '" + iPlanNo + "', " +
                        " '" + Doc_Code + "', '" + Division_Code + "', 0,'')";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


    }
}
