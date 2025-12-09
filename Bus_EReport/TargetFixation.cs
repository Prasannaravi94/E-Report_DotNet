using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class TargetFixation
    {
        private string strQry = string.Empty;


        public string RecordHeadAdd(string sf_code, string divition_code, string financialYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {



                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                //strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                //sf_sl_no = db.Exec_Scalar(strQry);
                strQry = "SELECT isnull(max(Trans_sl_No)+1,'1') Trans_sl_No from Trans_TargetFixation_Product_Head ";
                int Trans_sl_No = db.Exec_Scalar(strQry);

                DataSet dshqcode;

                strQry = "select SF_Cat_Code from mas_salesforce where Sf_Code = '" + sf_code + "' and (Division_Code like '" + Division_Code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + Division_Code + ',' + "%')";
                dshqcode = db.Exec_DataSet(strQry);

                string HQ_Code = dshqcode.Tables[0].Rows[0]["SF_Cat_Code"].ToString();

                strQry = " INSERT INTO [Trans_TargetFixation_Product_Head]([Sf_Code] ,[Division_Code] ,[Financial_Year],[Created_Date],[Sf_HQ_Code]) " +
                         " VALUES ( '" + sf_code + "' , '" + Division_Code + "', '" + financialYear + "', getdate(),'" + HQ_Code + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_TargetFixation_Product_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }

        public string RecordDetailsAdd(string TransSlNo, string productCode, string monthName, float Quantity, string sf_code, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            int state_code = -1;
            //DataSet dsPrd;
            DataSet hq;
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {
                strQry = " select State_Code from Mas_Salesforce where Sf_Code='" + sf_code + "' ";
                state_code = db.Exec_Scalar(strQry);

                strQry = " select MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Target_Price " +
                         " from Mas_Product_State_Rates where Division_Code='" + div_code + "' and State_Code='" + state_code + "' " +
                         " and Product_Detail_Code='" + productCode + "' ";

                hq = db.Exec_DataSet(strQry);
                //if (hq.Tables[0].Rows.Count > 0)
                //{

                DataSet dshqcode1;

                strQry = "SELECT SF_Cat_Code from Mas_Salesforce where sf_Code='" + sf_code + "'";
                dshqcode1 = db.Exec_DataSet(strQry);

                string HQ_Code_detail = dshqcode1.Tables[0].Rows[0]["SF_Cat_Code"].ToString();

                

                //    iReturn = db.ExecQry(strQry);
                // }

                //dsPrd = db.Exec_DataSet(strQry);

                //if (dsPrd.Tables[0].Rows.Count > 0)
                //{
                strQry = " INSERT INTO [Trans_TargetFixation_Product_Details]([Trans_sl_No],[Product_Code],[Month],[Quantity],[Division_Code],[MRP_Price],[Retailor_Price],[Distributor_Price],[NSR_Price],[Target_Price],[Sf_HQ_Code]) " +
                         " VALUES ( '" + TransSlNo + "' , '" + productCode + "', '" + monthName + "', '" + Quantity + "','" + div_code + "','" + hq.Tables[0].Rows[0][0].ToString() + "', " +
                         " '" + hq.Tables[0].Rows[0][1].ToString() + "','" + hq.Tables[0].Rows[0][2].ToString() + "','" + hq.Tables[0].Rows[0][3].ToString() + "','" + hq.Tables[0].Rows[0][4].ToString() + "','" + HQ_Code_detail + "') ";


                iReturn = db.ExecQry(strQry);
                // }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }

            //}
            //else
            //{
            //    return "Dup";
            //}
        }

        public DataSet GetTargetFixationList(string hqcode,string sf_code, string divcode, string financialYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_TargetFixation_Productwise '" + hqcode + "','" + sf_code + "','" + divcode + "','" + financialYear + "' ";

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

        public DataSet GetTargetFixationReport(string sf_code, string fromMonthYear,string toMonthYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Division_Code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            Division_Code = db_ER.Exec_Scalar(strQry);


            DataSet dsSF = null;
            strQry = "EXEC sp_TargetFixation_Report '" + sf_code + "','" + Division_Code + "','" + fromMonthYear + "','" + toMonthYear + "' ";

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

        public int RecordUpdate_TargetMain(string TransSlNo)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_TargetFixation_Product_Head " +
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

        public int RecordDelete_TargetDetails(string TransSlNo, int Month)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_TargetFixation_Product_Details WHERE Trans_sl_No = '" + TransSlNo + "' and  Month='" + Month + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        //public int RecordDelete_TargetDetails(string TransSlNo)
        //{
        //    int iReturn = -1;
        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "DELETE FROM Trans_TargetFixation_Product_Details WHERE Trans_sl_No = '" + TransSlNo + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;

        //}
        public DataSet GetTargetFixationValList(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_get_TargetFixationVal_View '" + sf_code + "','" + divcode + "'";

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
        public DataSet TargetFixationValuewise_RecordExist(string Sf_Code, string div_code, int Financial_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT h.Division_Code,h.Financial_Year, d.Sf_Code, d.Sf_Name, d.Jan_Val, d.Feb_Val, d.Mar_Val, d.Apr_Val, d.May_Val, d.Jun_Val, " +
                     " d.Jul_Val, d.Aug_Val, d.Sep_Val, d.Oct_Val, d.Nov_Val, d.Dec_Val FROM Trans_TargetFixation_Valuewise_Head h " +
                     " LEFT JOIN Trans_TargetFixation_Valuewise_Detail d on  h.Trans_Sl_No = d.Trans_Sl_No where " +
                     " d.Sf_Code ='" + Sf_Code + "' AND h.Division_Code ='" + div_code + "' AND h.Financial_Year = '" + Financial_Year + "' ";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public int RecordAddTargetFixation_Valuewise(string hdnSf_Code, int div_code, int year, string lblSf_Name, decimal? txtMonth1, decimal? txtMonth2, decimal? txtMonth3, decimal? txtMonth4, decimal? txtMonth5, decimal? txtMonth6, decimal? txtMonth7, decimal? txtMonth8, decimal? txtMonth9, decimal? txtMonth10, decimal? txtMonth11, decimal? txtMonth12)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_TargetFixation_Valuewise_Insert '" + hdnSf_Code + "', '" + div_code + "', '" + year + "', '" + lblSf_Name + "','" + txtMonth1 + "','" + txtMonth2 + "','" + txtMonth3 + "','" + txtMonth4 + "','" + txtMonth5 + "', " +
                        " '" + txtMonth6 + "','" + txtMonth7 + "','" + txtMonth8 + "','" + txtMonth9 + "','" + txtMonth10 + "','" + txtMonth11 + "','" + txtMonth12 + "'";

                if (strQry != "")
                {
                    string strQry1 = strQry.Replace("''", "NULL");
                    string strQry2 = strQry1.Replace("'0'", "NULL");
                    iReturn = db.ExecQry(strQry2);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordUpdateTargetFixation_Valuewise(string hdnSf_Code, int div_code, int year, decimal? txtMonth1, decimal? txtMonth2, decimal? txtMonth3, decimal? txtMonth4, decimal? txtMonth5, decimal? txtMonth6, decimal? txtMonth7, decimal? txtMonth8, decimal? txtMonth9, decimal? txtMonth10, decimal? txtMonth11, decimal? txtMonth12)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_TargetFixation_Valuewise_Update '" + hdnSf_Code + "', '" + div_code + "', '" + year + "', '" + txtMonth1 + "','" + txtMonth2 + "','" + txtMonth3 + "','" + txtMonth4 + "','" + txtMonth5 + "', " +
                        " '" + txtMonth6 + "','" + txtMonth7 + "','" + txtMonth8 + "','" + txtMonth9 + "','" + txtMonth10 + "','" + txtMonth11 + "','" + txtMonth12 + "' ";

                if (strQry != "")
                {
                    string strQry1 = strQry.Replace("''", "NULL");
                    string strQry2 = strQry1.Replace("'0'", "NULL");
                    iReturn = db.ExecQry(strQry2);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordDelTargetFixation_Valuewise(string Sf_Code, int div_code, int year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_TargetFixation_Valuewise_Del '" + Sf_Code + "', '" + div_code + "', '" + year + "' ";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordFDelTargetFixation_Valuewise(string hdnSf_Code, int div_code, int year, decimal? txtMonth1, decimal? txtMonth2, decimal? txtMonth3, decimal? txtMonth4, decimal? txtMonth5, decimal? txtMonth6, decimal? txtMonth7, decimal? txtMonth8, decimal? txtMonth9, decimal? txtMonth10, decimal? txtMonth11, decimal? txtMonth12, int Mode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "EXEC sp_TargetFixation_Valuewise_Update1 '" + hdnSf_Code + "', '" + div_code + "', '" + year + "', '" + Mode + "', '" + txtMonth1 + "','" + txtMonth2 + "','" + txtMonth3 + "','" + txtMonth4 + "','" + txtMonth5 + "', " +
                        " '" + txtMonth6 + "','" + txtMonth7 + "','" + txtMonth8 + "','" + txtMonth9 + "','" + txtMonth10 + "','" + txtMonth11 + "','" + txtMonth12 + "' ";

                if (strQry != "")
                {
                    string strQry1 = strQry.Replace("''", "NULL");
                    string strQry2 = strQry1.Replace("'0'", "NULL");
                    iReturn = db.ExecQry(strQry2);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordMonDelTargetFixation_Valuewise(string hdnSf_Code, int div_code, int year, decimal? txtMonth, string month)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "";
                strQry = "UPDATE h SET h.Updated_Date= getdate() FROM Trans_TargetFixation_Valuewise_Head h LEFT JOIN Trans_TargetFixation_Valuewise_Detail d ON " +
                        " h.Trans_Sl_No = d.Trans_Sl_No WHERE d.Sf_Code = '" + hdnSf_Code + "' AND h.Division_Code = " + div_code + " AND h.Financial_Year= " + year + " ;" +

                        " UPDATE d SET " + month + " = NULL FROM Trans_TargetFixation_Valuewise_Detail d LEFT JOIN Trans_TargetFixation_Valuewise_Head h ON " +
                        " d.Trans_Sl_No = h.Trans_Sl_No WHERE d.Sf_Code = '" + hdnSf_Code + "' AND h.Division_Code = " + div_code + " AND h.Financial_Year= " + year + " ;";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordExistTargetHead(string Hq_Code, string divCode, string financialYear)
        {

            int iTransSlNo = 0;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select Trans_sl_No from Trans_TargetFixation_Product_Head WHERE  Division_Code='" + divCode + "' AND Financial_Year='" + financialYear + "' and Sf_HQ_Code='" + Hq_Code + "' ";
                iTransSlNo = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iTransSlNo;
        }
        public string RecordHeadAdd_Target_UPL(string division_code, string financialYear, string Hq_Code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {


                strQry = "SELECT isnull(max(Trans_sl_No)+1,'1') Trans_sl_No from Trans_TargetFixation_Product_Head ";
                int Trans_sl_No = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO [Trans_TargetFixation_Product_Head]([Sf_Code] ,[Division_Code] ,[Financial_Year],[Created_Date],Sf_HQ_Code) " +
                       " VALUES ( Null , '" + division_code + "', '" + financialYear + "', getdate(),'" + Hq_Code + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_TargetFixation_Product_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();
            //}
            //else
            //{
            //    return "Dup";
            //}

        }
        public DataSet Get_product_SlNo(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Code_SlNo,Product_Detail_Name,product_detail_code,sample_erp_code,sale_erp_code from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Active_Flag=0 ";

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

        public DataTable Get_product_state_rate(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = " select MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Target_Price,State_Code,Product_Detail_Code " +
                         " from Mas_Product_State_Rates where Division_Code='" + div_code + "'";


            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public string RecordDetailsAdd_Target_UPL(string TransSlNo, string Hq_Code, string Hq_name, string productCode, string monthName, string Rate, string Quantity, string Value, string div_code, string Prod_Sl_No, string Prod_Name, int year)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            int state_code = -1;
            DataSet dsPrd;
            //if (!CheckDupUserName(UsrDfd_UserName))
            //{
            try
            {
                strQry = " select State_Code from Mas_Salesforce where sf_cat_code='" + Hq_Code + "' ";
                state_code = db.Exec_Scalar(strQry);

                strQry = " select MRP_Price,Retailor_Price,Distributor_Price,NSR_Price,Target_Price " +
                         " from Mas_Product_State_Rates where Division_Code='" + div_code + "' and State_Code='" + state_code + "' " +
                         " and Product_Detail_Code='" + productCode + "' ";

                dsPrd = db.Exec_DataSet(strQry);

                if (dsPrd.Tables[0].Rows.Count > 0)
                {

                    strQry = " INSERT INTO [Trans_TargetFixation_Product_Details]([Trans_sl_No],[Product_Code],[Month],[Quantity],[Division_Code],[MRP_Price],[Retailor_Price],[Distributor_Price],[NSR_Price],[Target_Price],Target_Value,Product_Name, " +
                             " Sf_HQ_Code,Year,Product_Sl_No,Sf_HQ_Name ) " +
                             " VALUES ( '" + TransSlNo + "' , '" + productCode + "', '" + monthName + "', '" + Quantity + "','" + div_code + "','" + dsPrd.Tables[0].Rows[0][0].ToString() + "', " +
                             " '" + dsPrd.Tables[0].Rows[0][1].ToString() + "','" + dsPrd.Tables[0].Rows[0][2].ToString() + "','" + dsPrd.Tables[0].Rows[0][3].ToString() + "','" + Rate + "','" + Value + "','" + Prod_Name + "', " +
                             " '" + Hq_Code + "'," + year + ",'" + Prod_Sl_No + "','" + Hq_name + "') ";


                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = " INSERT INTO [Trans_TargetFixation_Product_Details]([Trans_sl_No],[Product_Code],[Month],[Quantity],[Division_Code],[Target_Price],Target_Value,Product_Name, " +
                                " Sf_HQ_Code,Year,Product_Sl_No,Sf_HQ_Name ) " +
                                " VALUES ( '" + TransSlNo + "' , '" + productCode + "', '" + monthName + "', '" + Quantity + "','" + div_code + "', " +
                                " '" + Rate + "','" + Value + "','" + Prod_Name + "', " +
                                " '" + Hq_Code + "'," + year + ",'" + Prod_Sl_No + "','" + Hq_name + "') ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }

            //}
            //else
            //{
            //    return "Dup";
            //}

        }
    }
}
