using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class InputDespatch
    {
        private string strQry = string.Empty;


        public string RecordHeadAdd(string sf_code, string divition_code, string transMonth, string transYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            string trans_month_year = string.Empty;

            trans_month_year = "15" + "-" + transMonth + "-" + transYear;
            DateTime trans_month_year2 = Convert.ToDateTime(trans_month_year.ToString());

            trans_month_year = trans_month_year2.Month + "-" + trans_month_year2.Day + "-" + trans_month_year2.Year;
            
            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);

                int transSlNo = this.RecordExistInputDespatchHead(sf_code, Convert.ToString(Division_Code), transMonth, transYear);
                if (transSlNo > 0)
                {
                    this.RecordDelete_InputDespatchDetails(Convert.ToString(transSlNo));
                    this.RecordDelete_InputDespatchHead(Convert.ToString(transSlNo));
                }

                strQry = " INSERT INTO [Trans_Input_Despatch_Head]([Sf_Code] ,[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Created_Date],Trans_month_year) " +
                       " VALUES ( '" + sf_code + "' , '" + Division_Code + "', '" + transMonth + "', '" + transYear + "', getdate(),'" + trans_month_year + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Input_Despatch_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public string RecordDetailsAdd(string TransSlNo, string sf_code, string divition_code, string productCode, int Quantity)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            
            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_Input_Despatch_Details]([Trans_sl_No],[Division_Code],[Gift_Code],[Despatch_Qty]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + Division_Code + "', '" + productCode + "', '" + Quantity + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }

        public int RecordDelete_InputDespatchHead(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_Input_Despatch_Head WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordDelete_InputDespatchDetails(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_Input_Despatch_Details WHERE Trans_sl_No = '" + TransSlNo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordUpdate_InputDespatchHead(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_Input_Despatch_Head " +
                            " SET Updated_Date = getdate() " +
                            " WHERE Trans_sl_No = '" + TransSlNo + "' ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet GetInputDespatchGifts(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_InputDespatch_Gifts '" + sf_code + "','" + month + "','" + year + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet GetInputDespatchStatus(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_InputDespatch_Status '" + sf_code + "','" + month + "','" + year + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public int RecordExistInputDespatchHead(string sfCode,string divCode, string strmonth, string stryear)
        {

            int iTransSlNo = 0;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select Trans_sl_No from Trans_Input_Despatch_Head WHERE Sf_Code = '" + sfCode + "' AND Division_Code='" + divCode + "' AND Trans_Month='" + strmonth + "' AND Trans_Year ='" + stryear + "' ";
                iTransSlNo = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iTransSlNo;
        }
        public string RecordDetailsAddinput(string TransSlNo, string sf_code, string divition_code, string productCode, int Quantity, string pid)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_Input_Despatch_Details]([Trans_sl_No],[Division_Code],[Gift_Name],[Despatch_Qty],[productc]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + Division_Code + "', '" + productCode + "', '" + Quantity + "','" + pid + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public string Recorddelete(string sf_code, string division_code, string transMonth, string transYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);

                int transSlNo = this.RecordExistInputDespatchHead(sf_code, Convert.ToString(Division_Code), transMonth, transYear);
                if (transSlNo > 0)
                {
                    this.RecordDelete_InputDespatchDetails(Convert.ToString(transSlNo));
                    this.RecordDelete_InputDespatchHead(Convert.ToString(transSlNo));
                }

                //strQry = " INSERT INTO [Trans_Sample_Despatch_Head]([Sf_Code] ,[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Created_Date]) " +
                //       " VALUES ( '" + sf_code + "' , '" + Division_Code + "', '" + transMonth + "', '" + transYear + "', getdate()) ";

                //iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Sample_Despatch_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }
        public string RecordDetailsAddedit(string TransSlNo, string sf_code, string divition_code, string productCode, int Quantity, string pid, string remarks)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_Input_Despatch_Details]([Trans_sl_No],[Division_Code],[Gift_Name],[Despatch_Qty],[productc],[Remarks]) " +
                       " VALUES ( '" + TransSlNo + "' , '" + Division_Code + "', '" + productCode + "', '" + Quantity + "','" + pid + "','"+ remarks + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public DataSet Get_Input_Code(string div_code, string Input_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Gift_Code from Mas_Gift where Division_Code='" + div_code + "' and Gift_Name like '%" + Input_Name + "%' and Gift_Active_Flag=0 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public string RecordHeadAdd_Upl(string sf_code, string divition_code, string transMonth, string transYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);

                //int transSlNo = this.RecordExistInputDespatchHead(sf_code, Convert.ToString(Division_Code), transMonth, transYear);
                //if (transSlNo > 0)
                //{
                //    this.RecordDelete_InputDespatchDetails(Convert.ToString(transSlNo));
                //    this.RecordDelete_InputDespatchHead(Convert.ToString(transSlNo));
                //}

                strQry = " INSERT INTO [Trans_Input_Despatch_Head]([Sf_Code] ,[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Created_Date]) " +
                       " VALUES ( '" + sf_code + "' , '" + Division_Code + "', '" + transMonth + "', '" + transYear + "', getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Input_Despatch_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }
        public DataSet Get_gift_SlNo(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select gift_code,Gift_SName from mas_gift where Division_Code='" + div_code + "' and Gift_Active_Flag=0 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet Get_Input_SName(string div_code, string Input_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Gift_Code,Gift_Name,Gift_SName from Mas_Gift where Division_Code='" + div_code + "' and Gift_SName='" + Input_SName + "' and Gift_Active_Flag=0 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet Get_Input_SName_DeactiveCheck(string div_code, string Input_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Gift_Code,Gift_Name,Gift_SName from Mas_Gift where Division_Code='" + div_code + "' and Gift_SName='" + Input_SName + "' and Gift_Active_Flag=1 ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet Get_Subdivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select stuff((select ','+cast((subdivision_code)as varchar(20)) from mas_subdivision t where SubDivision_Active_Flag =0 and div_code='" + div_code + "' for XML path('')),1,1,'') as subdivision_code   ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet Get_Prod_Brand(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select stuff((select ','+cast((Product_Brd_Code)as varchar(20)) from Mas_Product_Brand t where Product_Brd_Active_Flag =0 and Division_Code='" + div_code + "' for XML path('')),1,1,'') as Product_Brd_Code ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
        public DataSet Get_Input_SName_ALL(string div_code, string Input_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Gift_Code,Gift_Name,Gift_Active_Flag from Mas_Gift where Division_Code='" + div_code + "' and Gift_SName='" + Input_SName + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

    }
}
