using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class SecSale
    {
        #region "Variable Declarations"
        private string strQry = string.Empty;
        SqlCommand comm;
        SqlCommand sCommand;
        string sError = string.Empty;
        int iErrReturn = -1;
        #endregion

        DataTable dt = new DataTable();
        DataRow dr = null;
        public DataSet getSaleMaster()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                            " Sec_Sale_Name, " +
                            " Sec_Sale_Short_Name, " +
                            " Sec_Sale_Sub_Name, " +
                            " Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster(bool includeTotal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);

            if (includeTotal)
            {

                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field   " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and mssp.Active=0 and msss.Active=0" +
                        " UNION " +
                        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                        " '+' AS Sel_Sale_Operator , " +
                        " (select value_needed from mas_common_sec_sale_setup  where Division_Code = @Par_Division_Code) AS value_needed, " +
                        " '0' AS calc_needed, '' AS Sub_Needed, '' AS  Sub_Label, 3.1 AS  Order_by, 0 AS Carry_Fwd_Needed, 0 AS Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field  " +
                        " FROM Mas_Sec_Sale_Param " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field    " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and mssp.Active=0  and msss.Active=0 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field    " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and mssp.Active=0  and msss.Active=0 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, mcf.Der_Formula,mcf.CalculationMode,msss.CalcF_Field " +
                        " FROM Mas_Sec_Sale_Setup msss,  Mas_Sec_Sale_Param mssp " +
                        " left outer join Mas_Common_SS_Setup_Formula mcf " +
                        " mssp.Cust_Col_SNo = mcf.Col_SNo " +
                        " WHERE Sel_Sale_Operator = 'D' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and mssp.Active=0  and msss.Active=0" +
                        " ORDER BY msss.Order_by";
            }
            else
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and mssp.Active=0  and msss.Active=0" +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and  mssp.Active=0 and msss.Active=0 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, '' Der_Formula,'' CalculationMode,'' CalcF_Field " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and  mssp.Active=0  and msss.Active=0" +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by, msss.Carry_Fwd_Needed, msss.Carry_Fwd_Field, mcf.Der_Formula,mcf.CalculationMode,msss.CalcF_Field  " +
                        " FROM Mas_Sec_Sale_Setup msss, Mas_Sec_Sale_Param mssp " +
                        " left outer join Mas_Common_SS_Setup_Formula mcf " +
                        " on mssp.Cust_Col_SNo = mcf.Col_SNo " +
                        " WHERE Sel_Sale_Operator = 'D' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 and  mssp.Active=0 and msss.Active=0" +
                        " ORDER BY msss.Order_by";

            }
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }

            return dsSale;
        }

        public DataSet getSaleMaster_ValueNeeded(bool includeTotal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);

            if (includeTotal)
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1  and  mssp.Active=0 and msss.Active=0" +
                        " UNION " +
                        " SELECT  3.1 AS Sec_Sale_Code, 'Total (+)' AS Sec_Sale_Name, " +
                        " 'Total' AS Sec_Sale_Short_Name, 'Total' AS Sec_Sale_Sub_Name,  " +
                        " '+' AS Sel_Sale_Operator , " +
                        " (select value_needed from mas_common_sec_sale_setup  where Division_Code=@Par_Division_Code and value_needed = 1) AS value_needed, " +
                        " '0' AS calc_needed, '' AS Sub_Needed, '' AS  Sub_Label, 3.1 AS  Order_by" +
                        " FROM Mas_Sec_Sale_Param " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 and  mssp.Active=0 and msss.Active=0 " +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 and  mssp.Active=0 and msss.Active=0 " +
                        " ORDER BY Sel_Sale_Operator, msss.Order_by";
            }
            else
            {
                strQry = "SELECT mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE mssp.Sel_Sale_Operator = '+' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 and  mssp.Active=0 and msss.Active=0" +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = '-' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 and  mssp.Active=0 and msss.Active=0" +
                        " UNION " +
                        " SELECT  mssp.Sec_Sale_Code, mssp.Sec_Sale_Name, " +
                        " mssp.Sec_Sale_Short_Name, mssp.Sec_Sale_Sub_Name, mssp.Sel_Sale_Operator, " +
                        " msss.value_needed, msss.calc_needed, msss.Sub_Needed, msss.Sub_Label, msss.Order_by  " +
                        " FROM Mas_Sec_Sale_Param mssp,  Mas_Sec_Sale_Setup msss" +
                        " WHERE Sel_Sale_Operator = 'C' " +
                        " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                        " AND msss.Division_Code = @Par_Division_Code " +
                        " AND mssp.Division_Code = @Par_Division_Code " +
                        " AND msss.Display_Needed = 1 " +
                        " AND msss.Value_Needed = 1 and  mssp.Active=0 and msss.Active=0" +
                        " ORDER BY Sel_Sale_Operator, msss.Order_by";
            }

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }

            return dsSale;
        }

        public DataSet getSaleEnteredQty(string div_code, string sf_code, int cmonth, int cyear, int stock_code, string prod_code, string sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sfcode = new SqlParameter();
            param_sfcode.ParameterName = "@Par_SF_Code";    // Defining Name
            param_sfcode.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sfcode.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_month = new SqlParameter();
            par_month.ParameterName = "@Par_Month";    // Defining Name
            par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_year = new SqlParameter();
            par_year.ParameterName = "@Par_Year";    // Defining Name
            par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_year.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_stock_code = new SqlParameter();
            par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
            par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_prod_code = new SqlParameter();
            par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
            par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_secsale_code = new SqlParameter();
            param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);
            sCommand.Parameters.Add(param_sfcode);
            sCommand.Parameters.Add(par_month);
            sCommand.Parameters.Add(par_year);
            sCommand.Parameters.Add(par_stock_code);
            sCommand.Parameters.Add(par_prod_code);
            sCommand.Parameters.Add(param_secsale_code);

            // Setting values of Parameter

            sec_sale_code = Convert.ToString((int)Convert.ToDouble(sec_sale_code));

            par_div_code.Value = Convert.ToInt16(div_code);
            param_sfcode.Value = sf_code;
            par_month.Value = cmonth;
            par_year.Value = cyear;
            par_stock_code.Value = stock_code;
            par_prod_code.Value = prod_code;
            param_secsale_code.Value = sec_sale_code;

            strQry = " SELECT tsedv.Sec_Sale_Qty, tsedv.Sec_Sale_Value, tsedv.Sec_Sale_Sub " +
                    " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed, Trans_SS_Entry_Detail_Value tsedv " +
                    " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                    " AND tsed.SS_Det_Sl_No = tsedv.SS_Det_Sl_No " +
                    " AND tseh.SF_Code = @Par_SF_Code " +
                    " AND tseh.Division_Code  = @Par_Division_Code " +
                    " AND tseh.Month = @Par_Month " +
                    " AND tseh.Year = @Par_Year " +
                    " AND tseh.Stockiest_Code = @Par_Stock_Code " +
                    " AND tsed.Product_Detail_Code = @Par_Prod_Code " +
                    " AND tsedv.Sec_Sale_Code = @Sec_Sale_Code " +
                    " ORDER BY 1";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleEnteredQty()");
            }

            return dsSale;
        }

        public DataSet getDtlSNo(string div_code, string sf_code, int cmonth, int cyear, int stock_code, string prod_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sfcode = new SqlParameter();
            param_sfcode.ParameterName = "@Par_SF_Code";    // Defining Name
            param_sfcode.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sfcode.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_month = new SqlParameter();
            par_month.ParameterName = "@Par_Month";    // Defining Name
            par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_year = new SqlParameter();
            par_year.ParameterName = "@Par_Year";    // Defining Name
            par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_year.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_stock_code = new SqlParameter();
            par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
            par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_prod_code = new SqlParameter();
            par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
            par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);
            sCommand.Parameters.Add(param_sfcode);
            sCommand.Parameters.Add(par_month);
            sCommand.Parameters.Add(par_year);
            sCommand.Parameters.Add(par_stock_code);
            sCommand.Parameters.Add(par_prod_code);

            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);
            param_sfcode.Value = sf_code;
            par_month.Value = cmonth;
            par_year.Value = cyear;
            par_stock_code.Value = stock_code;
            par_prod_code.Value = prod_code;

            strQry = " SELECT tsed.SS_Det_Sl_No " +
                    " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed " +
                    " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                    " AND tseh.SF_Code = @Par_SF_Code " +
                    " AND tseh.Division_Code  = @Par_Division_Code " +
                    " AND tseh.Month = @Par_Month " +
                    " AND tseh.Year = @Par_Year " +
                    " AND tseh.Stockiest_Code = @Par_Stock_Code " +
                    " AND tsed.Product_Detail_Code = @Par_Prod_Code ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleEnteredQty()");
            }

            return dsSale;
        }

        public int RecordAdd(int div_code, int Sec_Sale_Code, int Display_Needed, int Value_Needed, int Carry_Fwd_Needed, int Disable_Mode, int Calc_Needed, int Calc_Disable,
            int Sale_Calc, int Carry_Fwd_Field, int Order_by, bool bRecordExist, int sub_needed, string sub_label)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sec_Sale_Code = new SqlParameter();
                param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Display_Needed = new SqlParameter();
                param_Display_Needed.ParameterName = "@Display_Needed";    // Defining Name
                param_Display_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Display_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Value_Needed = new SqlParameter();
                param_Value_Needed.ParameterName = "@Value_Needed";    // Defining Name
                param_Value_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Value_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Needed = new SqlParameter();
                param_Carry_Fwd_Needed.ParameterName = "@Carry_Fwd_Needed";    // Defining Name
                param_Carry_Fwd_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Disable_Mode = new SqlParameter();
                param_Disable_Mode.ParameterName = "@Disable_Mode";    // Defining Name
                param_Disable_Mode.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Disable_Mode.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Needed = new SqlParameter();
                param_Calc_Needed.ParameterName = "@Calc_Needed";    // Defining Name
                param_Calc_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Disable = new SqlParameter();
                param_Calc_Disable.ParameterName = "@Calc_Disable";    // Defining Name
                param_Calc_Disable.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Disable.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sale_Calc = new SqlParameter();
                param_Sale_Calc.ParameterName = "@Sale_Calc";    // Defining Name
                param_Sale_Calc.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sale_Calc.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Field = new SqlParameter();
                param_Carry_Fwd_Field.ParameterName = "@Carry_Fwd_Field";    // Defining Name
                param_Carry_Fwd_Field.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Field.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_needed = new SqlParameter();
                param_sub_needed.ParameterName = "@Sub_Needed";    // Defining Name
                param_sub_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_sub_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_label = new SqlParameter();
                param_sub_label.ParameterName = "@Sub_Label";    // Defining Name
                param_sub_label.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sub_label.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Order_by = new SqlParameter();
                param_Order_by.ParameterName = "@Order_by";    // Defining Name
                param_Order_by.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Order_by.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Created_dt = new SqlParameter();
                param_Created_dt.ParameterName = "@Created_dt";    // Defining Name
                param_Created_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Created_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@Updated_dt";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand

                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_Sec_Sale_Code);
                comm.Parameters.Add(param_Display_Needed);
                comm.Parameters.Add(param_Value_Needed);
                comm.Parameters.Add(param_Carry_Fwd_Needed);
                comm.Parameters.Add(param_Disable_Mode);
                comm.Parameters.Add(param_Calc_Needed);
                comm.Parameters.Add(param_Calc_Disable);
                comm.Parameters.Add(param_Sale_Calc);
                comm.Parameters.Add(param_Carry_Fwd_Field);
                comm.Parameters.Add(param_sub_needed);
                comm.Parameters.Add(param_sub_label);
                comm.Parameters.Add(param_Order_by);
                comm.Parameters.Add(param_Created_dt);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_Sec_Sale_Code.Value = Sec_Sale_Code;
                param_Display_Needed.Value = Display_Needed;
                param_Value_Needed.Value = Value_Needed;
                param_Carry_Fwd_Needed.Value = Carry_Fwd_Needed;
                param_Disable_Mode.Value = Disable_Mode;
                param_Calc_Needed.Value = Calc_Needed;
                param_Calc_Disable.Value = Calc_Disable;
                param_Sale_Calc.Value = Sale_Calc;
                param_Carry_Fwd_Field.Value = Carry_Fwd_Field;
                param_sub_needed.Value = sub_needed;
                param_sub_label.Value = sub_label;
                param_Order_by.Value = Order_by;
                param_Created_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating Setup for the Division
                {
                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code,Sec_Sale_Code,Display_Needed,Value_Needed,Carry_Fwd_Needed,Disable_Mode,"
                        + " Calc_Needed, Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt,Active) VALUES "
                        + " ( @Division_Code, @Sec_Sale_Code, @Display_Needed, @Value_Needed, @Carry_Fwd_Needed, @Disable_Mode, "
                        + " @Calc_Needed , @Calc_Disable, @Sale_Calc, @Carry_Fwd_Field, @Sub_Needed, @Sub_Label, @Order_by, @Created_dt, @Updated_dt,0)";
                }
                else //Update the Setup records for the division
                {
                    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                            " SET Display_Needed = @Display_Needed, " +
                            " Value_Needed = @Value_Needed, " +
                            " Carry_Fwd_Needed = @Carry_Fwd_Needed, " +
                            " Disable_Mode = @Disable_Mode, " +
                            " Calc_Needed = @Calc_Needed, " +
                            " Calc_Disable = @Calc_Disable, " +
                            " Sale_Calc = @Sale_Calc, " +
                            " Carry_Fwd_Field = @Carry_Fwd_Field, " +
                            " Sub_Needed = @Sub_Needed, " +
                            " Sub_Label = @Sub_Label, " +
                            " Order_by = @Order_by, " +
                            " Updated_dt = @Updated_dt " +
                            " WHERE Division_Code = @Division_Code " +
                            " AND Sec_Sale_Code = @Sec_Sale_Code ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(div_code, ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }

        public bool sRecordExist(string div_code, int sec_sale_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;

                strQry = " SELECT count(sl_no) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Code = @Par_Sec_Sale_Code and Active=0 ";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public DataSet getSaleSetup(int div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);

            // Setting values of Parameter
            param_div_code.Value = div_code;

            strQry = " SELECT Sec_Sale_Code, " +
                            " Display_Needed, " +
                            " Value_Needed, " +
                            " Carry_Fwd_Needed, " +
                            " Disable_Mode, " +
                            " Calc_Needed, " +
                            " Calc_Disable, " +
                            " Sale_Calc, " +
                            " Carry_Fwd_Field, " +
                            " Order_by " +
                     " FROM Mas_Sec_Sale_Setup " +
                     " WHERE Division_Code = @Division_Code " +
                     " ORDER BY 1 ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleSetup()");
            }
            return dsSale;
        }

        public DataSet getProduct(string div_code, string state, DateTime cdate, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            if (Sub_Div.Contains(','))
                Sub_Div = Sub_Div.Substring(0, Sub_Div.Length - 1);

            //strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
            //          " b.Product_Description, " +
            //          " b.Product_Detail_Name," +
            //          " b.Product_Sale_Unit,  " +
            //          " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
            //          " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
            //          " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price, " +
            //          " isnull(rtrim(Target_Price),0) Target_Price " +
            //          " From Mas_Product_Detail b" +
            //          " INNER JOIN Trans_SS_Entry_Detail c  " +
            //          " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
            //          " WHERE b.Division_Code= @Division_Code " +
            //          " AND b.Product_Active_Flag = 0 ";

            strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                      " b.Product_Description, " +
                      " b.Product_Detail_Name," +
                      " b.Product_Sale_Unit,  " +
                      " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                      " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                      " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                      " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                      " isnull(rtrim(Target_Price),0) Target_Price " +
                      " From Mas_Product_Detail b" +
                      " INNER JOIN Mas_Product_State_Rates c  " +
                      " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                      " WHERE b.Division_Code= @Division_Code " +
                      " AND b.Product_Active_Flag = 0" +
                      " AND c.state_code = '" + state + "' " +
                      " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                      " AND (b.subdivision_code like '" + Sub_Div + ',' + "%'  or b.subdivision_code like '%" + ',' + Sub_Div + ',' + "%') ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public DataSet getProduct_Total(string div_code, string state, DateTime cdate, string prod_grp, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);

            if ((prod_grp == "C") || (prod_grp == "G"))
            {
                strQry = " EXEC sp_ProdList_SecSales '" + div_code + "', '" + state + "', '" + prod_grp + "','" + subdiv + "' ";
            }
            else
            {
                //strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                //           " b.Product_Description, " +
                //           " b.Product_Detail_Name," +
                //           " b.Product_Sale_Unit,  " +
                //           " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                //           " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                //           " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                //           " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                //           " isnull(rtrim(Target_Price),0) Target_Price " +
                //           " From Mas_Product_Detail b" +
                //           " INNER JOIN Mas_Product_State_Rates c  " +
                //           " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                //           " WHERE b.Division_Code= @Division_Code " +
                //           " AND b.Product_Active_Flag = 0 " +
                //           " AND c.state_code = '" + state + "' " +
                //           " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                //           " AND (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%') " +
                //           " UNION ALL" +
                //           " SELECT 'Tot_Prod' as Product_Detail_Code, '' as Product_Description, '' as Product_Detail_Name, '' as Product_Sale_Unit, " +
                //           " '0' as MRP_Price, '0' as Retailor_Price, '0' as Distributor_Price, '0' as NSR_Price, '0' as Target_Price ";


                strQry = " EXEC SP_Get_ProductTransDetail '" + div_code + "', '" + state + "','" + subdiv + "' ";

            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public bool isValueNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                //if (sec_sale_code == 3.1 || sec_sale_code == 9.1)
                //{
                //    strQry = " SELECT count(Total_Needed) " +
                //             " FROM mas_common_sec_sale_setup " +
                //             " WHERE Division_Code = @Par_Division_Code " +
                //             " AND value_needed = @Par_Val ";
                //}
                //else
                //{
                strQry = " SELECT count(sl_no) " +
                          " FROM Mas_Sec_Sale_Setup " +
                          " WHERE Division_Code = @Par_Division_Code " +
                          " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                          " AND value_needed = @Par_Val and Active=0 ";
                //}
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isValueNeeded()");
            }
            return bValue;
        }

        public bool isTotalValueNeeded(string div_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_val.Value = val;

                strQry = " SELECT count(Total_Needed) " +
                         " FROM mas_common_sec_sale_setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Value_Needed = @Par_Val ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isTotalValueNeeded()");
            }
            return bValue;
        }

        public bool isSubNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                strQry = " SELECT count(sl_no) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                         " AND Sub_needed = @Par_Val and Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isSubNeeded()");
            }
            return bValue;
        }

        public bool isDisableNeeded(string div_code, int sec_sale_code, int val)
        {
            sCommand = new SqlCommand();
            bool bValue = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Code";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val = new SqlParameter();
                par_val.ParameterName = "@Par_Val";    // Defining Name
                par_val.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_val.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);
                sCommand.Parameters.Add(par_val);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_code;
                par_val.Value = val;

                strQry = " SELECT count(sl_no) " +
                        " FROM Mas_Sec_Sale_Setup " +
                        " WHERE Division_Code = @Par_Division_Code " +
                        " AND Sec_Sale_Code = @Par_Sec_Sale_Code " +
                        " AND calc_disable = @Par_Val and Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bValue = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "isDisableNeeded()");
            }
            return bValue;
        }

        public int getmaxrecord(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT COUNT(SS_Head_Sl_No) FROM Trans_SS_Entry_Head";
                strQry = "select MAX(cast(SS_Head_Sl_No as int)) as SS_Head_Sl_No  FROM Trans_SS_Entry_Head";
                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getmaxrecord()");
            }
            return sl_no;
        }

        public int getDetmaxrecord(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "SELECT COUNT(SS_Det_Sl_No) FROM Trans_SS_Entry_Detail";
                strQry = "SELECT ISNULL(MAX(Cast(SS_Det_Sl_No as int)),0) FROM Trans_SS_Entry_Detail";
                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getmaxrecord()");
            }
            return sl_no;
        }

        public int RecordAdd(string div_code, string sf_code, int state_code, int stockiest_code, int iMonth, int iYear, int iStatus, bool bRecordExist, string ReportingTo)
        {
            int iReturn = -1;
            int sl_no = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(SS_Head_Sl_No) + 1 FROM Trans_SS_Entry_Head";
                //sl_no = db.Exec_Scalar(strQry);

                sl_no = getmaxrecord(div_code);
                sl_no += 1;

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_sl_no = new SqlParameter();
                param_sl_no.ParameterName = "@sl_no";    // Defining Name
                param_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_state_code = new SqlParameter();
                param_state_code.ParameterName = "@State_Code";    // Defining Name
                param_state_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_state_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_submitted_dtm = new SqlParameter();
                param_submitted_dtm.ParameterName = "@submitted_dtm";    // Defining Name
                param_submitted_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_submitted_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Status = new SqlParameter();
                param_Status.ParameterName = "@Status";    // Defining Name
                param_Status.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Status.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Reporting_To = new SqlParameter();
                param_Reporting_To.ParameterName = "@ReportingTo";    // Defining Name
                param_Reporting_To.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Reporting_To.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_sl_no);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_state_code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_submitted_dtm);
                comm.Parameters.Add(param_Status);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_Reporting_To);

                // Setting values of Parameter
                param_sl_no.Value = sl_no.ToString();
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code;
                param_state_code.Value = state_code;
                param_stockiest_code.Value = stockiest_code;
                param_month.Value = iMonth;
                param_year.Value = iYear;
                param_submitted_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"); ;
                param_Status.Value = iStatus;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Reporting_To.Value = ReportingTo;

                if (!bRecordExist) //Creating SS Entry Head
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Head (SS_Head_Sl_No,SF_Code,State_Code,Division_Code,Stockiest_Code,Month,"
                          + " Year, submitted_dtm, Status, updated_dtm,Approval_Mgr) VALUES "
                          + " ( @sl_no , @SF_Code, @State_Code, @Division_Code, @Stockiest_Code, @Month, "
                          + "  @Year, @submitted_dtm , @Status, @updated_dtm,@ReportingTo)";
                }
                else //Update the Setup records for the division
                {
                    strQry = "UPDATE Trans_SS_Entry_Head " +
                            " SET Status = @Status, " +
                            " Approval_Mgr=@ReportingTo," +
                            " updated_dtm = @updated_dtm " +
                            " WHERE Division_Code = @Division_Code " +
                            " AND SF_Code = @SF_Code " +
                            " AND Stockiest_Code = @Stockiest_Code " +
                            " AND Month = @Month " +
                            " AND Year = @Year ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }

        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by, int status)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                string sDate = iMonth.ToString() + "-01-" + iYear.ToString();
                DateTime dtEditDate = Convert.ToDateTime(sDate);

                strQry = "EXEC sp_ss_option_edit  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + dtEditDate + "', '" + reject_by + "', 2, 4 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }


        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_by = new SqlParameter();
                param_reject_by.ParameterName = "@reject_by";    // Defining Name
                param_reject_by.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_by.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_reject_by);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code;
                param_stockiest_code.Value = stockiest_code;
                param_month.Value = iMonth;
                param_year.Value = iYear;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_reject_by.Value = reject_by;

                strQry = "UPDATE Trans_SS_Entry_Head " +
                        " SET Approval_Mgr = @reject_by, " +
                        " updated_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND SF_Code = @SF_Code " +
                        " AND Stockiest_Code = @Stockiest_Code " +
                        " AND Month = @Month " +
                        " AND Year = @Year ";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Rejection", "Record Update()");
            }

            return iReturn;
        }


        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by, string reject_reason)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_SF_Code = new SqlParameter();
                param_SF_Code.ParameterName = "@SF_Code";    // Defining Name
                param_SF_Code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_SF_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_stockiest_code = new SqlParameter();
                param_stockiest_code.ParameterName = "@Stockiest_Code";    // Defining Name
                param_stockiest_code.SqlDbType = SqlDbType.BigInt;           // Defining DataType
                param_stockiest_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_month = new SqlParameter();
                param_month.ParameterName = "@Month";    // Defining Name
                param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_year = new SqlParameter();
                param_year.ParameterName = "@Year";    // Defining Name
                param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_by = new SqlParameter();
                param_reject_by.ParameterName = "@reject_by";    // Defining Name
                param_reject_by.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_by.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_reject_reason = new SqlParameter();
                param_reject_reason.ParameterName = "@reject_reason";    // Defining Name
                param_reject_reason.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_reject_reason.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_SF_Code);
                comm.Parameters.Add(param_stockiest_code);
                comm.Parameters.Add(param_month);
                comm.Parameters.Add(param_year);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_reject_by);
                comm.Parameters.Add(param_reject_reason);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_SF_Code.Value = sf_code;
                param_stockiest_code.Value = stockiest_code;
                param_month.Value = iMonth;
                param_year.Value = iYear;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_reject_by.Value = reject_by;
                param_reject_reason.Value = reject_reason;

                strQry = "UPDATE Trans_SS_Entry_Head " +
                        " SET Reject_Reason = @reject_reason, " +
                        " Approval_Mgr = @reject_by, " +
                        " updated_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND SF_Code = @SF_Code " +
                        " AND Stockiest_Code = @Stockiest_Code " +
                        " AND Month = @Month " +
                        " AND Year = @Year ";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Rejection", "Record Update()");
            }

            return iReturn;
        }


        public int DetailRecordAdd(int SS_Head_Sl_No, string div_code, string prod_code, string mrp_price, string ret_price, string dist_price, string target_price, string nsr_price, bool bRecordExist)
        {
            int iReturn = -1;
            int sl_no = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(SS_Det_Sl_No) + 1 FROM Trans_SS_Entry_Detail";

                strQry = "SELECT  MAX(cast(SS_Det_Sl_No as int))+ 1 as SS_Det_Sl_No  FROM Trans_SS_Entry_Detail";
                sl_no = db.Exec_Scalar(strQry);

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_sl_no = new SqlParameter();
                param_sl_no.ParameterName = "@Head_sl_no";    // Defining Name
                param_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_det_sl_no = new SqlParameter();
                param_det_sl_no.ParameterName = "@Det_sl_no";    // Defining Name
                param_det_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_det_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_prod_code = new SqlParameter();
                param_prod_code.ParameterName = "@Prod_Code";    // Defining Name
                param_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_mrp_price = new SqlParameter();
                param_mrp_price.ParameterName = "@MRP_Price";    // Defining Name
                param_mrp_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_mrp_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_ret_price = new SqlParameter();
                param_ret_price.ParameterName = "@Ret_Price";    // Defining Name
                param_ret_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_ret_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_dist_price = new SqlParameter();
                param_dist_price.ParameterName = "@Dist_Price";    // Defining Name
                param_dist_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_dist_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_target_price = new SqlParameter();
                param_target_price.ParameterName = "@Target_Price";    // Defining Name
                param_target_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_target_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_nsr_price = new SqlParameter();
                param_nsr_price.ParameterName = "@NSR_Price";    // Defining Name
                param_nsr_price.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_nsr_price.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Div_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_sl_no);
                comm.Parameters.Add(param_det_sl_no);
                comm.Parameters.Add(param_prod_code);
                comm.Parameters.Add(param_mrp_price);
                comm.Parameters.Add(param_ret_price);
                comm.Parameters.Add(param_dist_price);
                comm.Parameters.Add(param_target_price);
                comm.Parameters.Add(param_nsr_price);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_sl_no.Value = SS_Head_Sl_No.ToString();
                param_det_sl_no.Value = sl_no.ToString();
                param_prod_code.Value = prod_code;
                param_mrp_price.Value = mrp_price;
                param_ret_price.Value = ret_price;
                param_dist_price.Value = dist_price;
                param_target_price.Value = target_price;
                param_nsr_price.Value = nsr_price;
                param_div_code.Value = div_code;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating SS Entry Head
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Detail (SS_Det_Sl_No, SS_Head_Sl_No, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                        " Distributor_Price, Target_Price, NSR_Price, Division_Code, Created_dtm, updated_dtm) VALUES "
                        + " ( @Det_sl_no, @Head_sl_no, @Prod_Code, @MRP_Price, @Ret_Price, @Dist_Price, @Target_Price, "
                        + "  @NSR_Price, @Div_Code, @created_dtm, @updated_dtm)";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }


        public int DetailValueRecordAdd(int SS_Det_Sl_No, string div_code, string sec_sale_code, string sec_sale_qty, string sec_sale_val, string sec_sale_sub, bool bRecordExist)
        {
            int iReturn = -1;
            int sl_no = -1;
            int col_sno = -1;
            DataSet ds;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                // strQry = "SELECT COUNT(SS_Det_Sl_No) + 1 FROM Trans_SS_Entry_Detail_Value";

                strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 FROM Trans_SS_Entry_Detail_Value";
                sl_no = db.Exec_Scalar(strQry);

                if (sl_no > 1)
                {
                    // strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 FROM Trans_SS_Entry_Detail_Value";
                    strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 FROM Trans_SS_Entry_Detail_Value";
                    sl_no = db.Exec_Scalar(strQry);
                }

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_det_sl_no = new SqlParameter();
                param_det_sl_no.ParameterName = "@Det_sl_no";    // Defining Name
                param_det_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_det_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_detval_sl_no = new SqlParameter();
                param_detval_sl_no.ParameterName = "@DetVal_sl_no";    // Defining Name
                param_detval_sl_no.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_detval_sl_no.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_code = new SqlParameter();
                param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_qty = new SqlParameter();
                param_secsale_qty.ParameterName = "@Sec_Sale_Qty";    // Defining Name
                param_secsale_qty.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_qty.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_val = new SqlParameter();
                param_secsale_val.ParameterName = "@Sec_Sale_Val";    // Defining Name
                param_secsale_val.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_val.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_sub = new SqlParameter();
                param_secsale_sub.ParameterName = "@Sec_Sale_Sub";    // Defining Name
                param_secsale_sub.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_sub.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Div_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_det_sl_no);
                comm.Parameters.Add(param_detval_sl_no);
                comm.Parameters.Add(param_secsale_code);
                comm.Parameters.Add(param_secsale_qty);
                comm.Parameters.Add(param_secsale_val);
                comm.Parameters.Add(param_secsale_sub);
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                sec_sale_code = Convert.ToString((int)Convert.ToDouble(sec_sale_code));
                param_det_sl_no.Value = SS_Det_Sl_No.ToString();
                param_detval_sl_no.Value = sl_no.ToString();
                param_secsale_code.Value = sec_sale_code;
                param_secsale_qty.Value = sec_sale_qty;

                if (sec_sale_val.Trim().Length > 0)
                    param_secsale_val.Value = sec_sale_val;
                else
                    param_secsale_val.Value = DBNull.Value;

                if (sec_sale_sub.Trim().Length > 0)
                    param_secsale_sub.Value = sec_sale_sub;
                else
                    param_secsale_sub.Value = DBNull.Value;

                param_div_code.Value = div_code;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");

                if (!bRecordExist) //Creating SS Entry Detail Value
                {
                    strQry = "INSERT INTO Trans_SS_Entry_Detail_Value (SS_Sec_Sl_No, SS_Det_Sl_No, Sec_Sale_Code, Sec_Sale_Qty, Sec_Sale_Value, Sec_Sale_Sub, " +
                        " Division_Code, Created_dtm, updated_dtm) VALUES "
                        + " ( @DetVal_sl_no, @Det_sl_no, @Sec_Sale_Code, @Sec_Sale_Qty, @Sec_Sale_Val, @Sec_Sale_Sub, "
                        + "   @Div_Code, @created_dtm, @updated_dtm)";
                }
                else //Update the SS Entry Detail Value records for the division
                {
                    strQry = "UPDATE Trans_SS_Entry_Detail_Value " +
                            " SET Sec_Sale_Qty = @Sec_Sale_Qty, " +
                            " Sec_Sale_Value = @Sec_Sale_Val, " +
                            " Sec_Sale_Sub = @Sec_Sale_Sub, " +
                            " Updated_dtm = @updated_dtm " +
                            " WHERE Division_Code = @Div_Code " +
                            " AND Sec_Sale_Code = @Sec_Sale_Code " +
                            " AND SS_Det_Sl_No = @Det_sl_no ";
                }

                iReturn = db.ExecQry(strQry, comm);

                ds = getColSNo_Formula(div_code, Convert.ToInt32(sec_sale_code));
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Trim().Length > 0)
                        col_sno = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (col_sno > 0)
                    {
                        strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                                " SET SS_Entry_Done = 1 , " +
                                " updated_dtm = getdate() " +
                                " WHERE Division_Code = '" + div_code + "' " +
                                " AND Col_SNo = '" + col_sno + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                }

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry - Value", "DetailValueRecordAdd()");
            }

            return iReturn;
        }


        public bool sRecordExist(string div_code, string sf_code, int imonth, int iyear, int stockiest_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sf_code = new SqlParameter();
                par_sf_code.ParameterName = "@Par_SF_Code";    // Defining Name
                par_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stockiest = new SqlParameter();
                par_stockiest.ParameterName = "@Par_Stockiest";    // Defining Name
                par_stockiest.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stockiest.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sf_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(par_stockiest);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sf_code.Value = sf_code;
                par_month.Value = imonth;
                par_year.Value = iyear;
                par_stockiest.Value = stockiest_code;

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE Division_Code  = @Par_Division_Code " +
                         " AND SF_Code          = @Par_SF_Code " +
                         " AND Month            = @Par_Month " +
                         " AND Year             = @Par_Year " +
                         " AND Stockiest_Code   = @Par_Stockiest ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public bool sRecordExist(string div_code, string sf_code, int imonth, int iyear, int stockiest_code, int status)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sf_code = new SqlParameter();
                par_sf_code.ParameterName = "@Par_SF_Code";    // Defining Name
                par_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stockiest = new SqlParameter();
                par_stockiest.ParameterName = "@Par_Stockiest";    // Defining Name
                par_stockiest.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stockiest.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_status = new SqlParameter();
                par_status.ParameterName = "@Par_Status";    // Defining Name
                par_status.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_status.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sf_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(par_stockiest);
                sCommand.Parameters.Add(par_status);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sf_code.Value = sf_code;
                par_month.Value = imonth;
                par_year.Value = iyear;
                par_stockiest.Value = stockiest_code;
                par_status.Value = status;

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE Division_Code  = @Par_Division_Code " +
                         " AND SF_Code          = @Par_SF_Code " +
                         " AND Month            = @Par_Month " +
                         " AND Year             = @Par_Year " +
                         " AND Stockiest_Code   = @Par_Stockiest " +
                         " AND Status           = @Par_Status ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public bool sDetailRecordExist(string div_code, string prod_code, string head_sl_no)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_prod_code = new SqlParameter();
                par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
                par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_head_sno = new SqlParameter();
                par_head_sno.ParameterName = "@Par_Head_SNo";    // Defining Name
                par_head_sno.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_head_sno.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_prod_code);
                sCommand.Parameters.Add(par_head_sno);

                // Setting values of Parameter
                par_div_code.Value = div_code;
                par_prod_code.Value = prod_code;
                par_head_sno.Value = head_sl_no;

                strQry = " SELECT COUNT(SS_Det_Sl_No) " +
                         " FROM Trans_SS_Entry_Detail " +
                         " WHERE Division_Code      = @Par_Division_Code " +
                         " AND Product_Detail_Code  = @Par_Prod_Code " +
                         " AND SS_Head_Sl_No        = @Par_Head_SNo ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }


        public bool sDetailValRecordExist(string div_code, int cmon, int cyear, string prod_code, string sec_sale_code, int stock_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_head_div_code = new SqlParameter();
                par_head_div_code.ParameterName = "@Par_Head_Division_Code";    // Defining Name
                par_head_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_head_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_det_div_code = new SqlParameter();
                par_det_div_code.ParameterName = "@Par_Det_Division_Code";    // Defining Name
                par_det_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_det_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_val_div_code = new SqlParameter();
                par_val_div_code.ParameterName = "@Par_Val_Division_Code";    // Defining Name
                par_val_div_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_val_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_month = new SqlParameter();
                par_month.ParameterName = "@Par_Month";    // Defining Name
                par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_month.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_year = new SqlParameter();
                par_year.ParameterName = "@Par_Year";    // Defining Name
                par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_year.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_secsale_code = new SqlParameter();
                param_secsale_code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_secsale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_secsale_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_prod_code = new SqlParameter();
                par_prod_code.ParameterName = "@Par_Prod_Code";    // Defining Name
                par_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_stock_code = new SqlParameter();
                par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
                par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_head_div_code);
                sCommand.Parameters.Add(par_det_div_code);
                sCommand.Parameters.Add(par_val_div_code);
                sCommand.Parameters.Add(par_month);
                sCommand.Parameters.Add(par_year);
                sCommand.Parameters.Add(param_secsale_code);
                sCommand.Parameters.Add(par_prod_code);
                sCommand.Parameters.Add(par_stock_code);

                sec_sale_code = Convert.ToString((int)Convert.ToDouble(sec_sale_code));

                // Setting values of Parameter
                par_head_div_code.Value = div_code;
                par_det_div_code.Value = div_code;
                par_val_div_code.Value = div_code;
                par_month.Value = cmon;
                par_year.Value = cyear;
                param_secsale_code.Value = sec_sale_code;
                par_prod_code.Value = prod_code;
                par_stock_code.Value = stock_code;

                strQry = " SELECT COUNT(SS_Sec_Sl_No) " +
                         " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail_Value tsdv, Trans_SS_Entry_Detail tsed " +
                         " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                         " AND tsed.SS_Det_Sl_No = tsdv.SS_Det_Sl_No " +
                         " AND tseh.Division_Code = @Par_Head_Division_Code " +
                         " AND tsed.Division_Code  = @Par_Det_Division_Code " +
                         " AND tsdv.Division_Code = @Par_Val_Division_Code " +
                         " AND tseh.Month = @Par_Month " +
                         " AND tseh.Year = @Par_Year " +
                         " AND tsdv.Sec_Sale_Code = @Sec_Sale_Code " +
                         " AND tsed.Product_Detail_Code = @Par_Prod_Code " +
                         " AND tseh.Stockiest_Code = @Par_Stock_Code ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry", "sDetailValRecordExist()");
            }
            return bRecordExist;
        }

        public DataSet get_SecSales_Pending_Approval(string sf_code, int istatus, string divCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            if (istatus == 1)
            {
                strQry = " SELECT DISTINCT a.sf_code, " +
                                        " a.sf_name, " +
                                        " a.Sf_HQ, " +
                                        " a.sf_Designation_Short_Name, " +
                                        " b.Month as Cur_Month, " +
                                        " b.Year as Cur_Year, " +
                                        " b.Month + '-' + b.YEAR as Mon, " +
                                        " a.sf_code + '-' + cast(b.Month as varchar) + '-' + cast(b.Year as varchar) + '-' + cast(b.Stockiest_Code as varchar) as key_field, " +
                                        " 'Click here to Approve - ' + cast(DateName( month , DateAdd( month , b.Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Year) as Month " +
                           " FROM Mas_Salesforce a, Trans_SS_Entry_Head b, Mas_Salesforce_AM c,Mas_Common_Sec_Sale_Setup s " +
                           " WHERE a.sf_code = b.sf_code " +
                           " AND a.sf_code=c.sf_code   and S.Approval_Needed=0 and s.Division_Code='" + divCode + "'" +
                           " AND b.Approval_Mgr  = '" + sf_code + "' " +
                           " AND b.Status= " + istatus;
            }
            else if (istatus == 3)
            {
                strQry = " SELECT DISTINCT a.sf_code, " +
                                         " a.sf_name, " +
                                         " a.Sf_HQ, " +
                                         " a.sf_Designation_Short_Name, " +
                                         " b.Month as Cur_Month, " +
                                         " b.Year as Cur_Year, " +
                                         " b.Month + '-' + b.YEAR as Mon, " +
                                         " a.sf_code + '-' + cast(b.Month as varchar) + '-' + cast(b.Year as varchar) + '-' + cast(b.Stockiest_Code as varchar) as key_field, " +
                                         " 'Click here for the month - ' + cast(DateName( month , DateAdd( month , b.Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Year) as Month " +
                            " FROM Mas_Salesforce a, Trans_SS_Entry_Head b, Mas_Salesforce_AM c ,Mas_Common_Sec_Sale_Setup s" +
                            " WHERE a.sf_code = b.sf_code " +
                            " AND a.sf_code = c.sf_code and S.Approval_Needed=0 and s.Division_Code='" + divCode + "' " +
                            " AND b.Approval_Mgr  = '" + sf_code + "' " +
                            " AND b.Status= " + istatus;
            }

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsSecSale;
        }

        public DataSet get_SecSales_Rejection(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Stockiest_Code, Month, Year, Reject_Reason " +
                        " FROM Trans_SS_Entry_Head " +
                        " WHERE division_code = '" + div_code + "' " +
                        " AND sf_code = '" + sf_code + "' " +
                        " AND Status = 3";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry - Rejection", "get_SecSales_Rejection()");

            }

            return dsSecSale;
        }

        public DataSet getsecsalecode(string div_code, string sec_sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Sec_Sale_Code " +
                        " FROM Mas_Sec_Sale_Param " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND Sec_Sale_Sub_Name = '" + sec_sub + "' and Active=0 ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Entry", "getsecsalecode()");

            }

            return dsSecSale;
        }


        public DataSet getClosingBalance(int div_code, string sf_code, int stock_code, int iMonth, int iYear, string prod_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sf_code = new SqlParameter();
            param_sf_code.ParameterName = "@SF_Code";    // Defining Name
            param_sf_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sf_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_stock_code = new SqlParameter();
            param_stock_code.ParameterName = "@Stockiest_Code";    // Defining Name
            param_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_stock_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_month = new SqlParameter();
            param_month.ParameterName = "@Month";    // Defining Name
            param_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_year = new SqlParameter();
            param_year.ParameterName = "@Year";    // Defining Name
            param_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_year.Direction = ParameterDirection.Input;// Setting the direction

            SqlParameter param_prod_code = new SqlParameter();
            param_prod_code.ParameterName = "@Product_Detail_Code";    // Defining Name
            param_prod_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_prod_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_Sec_Sale_Code = new SqlParameter();
            param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_sf_code);
            comm.Parameters.Add(param_stock_code);
            comm.Parameters.Add(param_month);
            comm.Parameters.Add(param_year);
            comm.Parameters.Add(param_prod_code);
            comm.Parameters.Add(param_Sec_Sale_Code);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_sf_code.Value = sf_code;
            param_stock_code.Value = stock_code;
            param_month.Value = iMonth;
            param_year.Value = iYear;
            param_prod_code.Value = prod_code;
            param_Sec_Sale_Code.Value = sec_sale_code;

            strQry = " SELECT tsedv.Sec_Sale_Qty, tsedv.Sec_Sale_Value  " +
                     " FROM Trans_SS_Entry_Detail_Value tsedv, Trans_SS_Entry_Detail tsed, Trans_SS_Entry_Head tseh " +
                     " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                     " and tsed.SS_Det_Sl_No = tsedv.SS_Det_Sl_No " +
                     " and tseh.SF_Code = @SF_Code " +
                     " and tseh.Division_Code = @Division_Code " +
                     " and tseh.Stockiest_Code = @Stockiest_Code" +
                     " and tseh.Month = @Month" +
                     " and tseh.Year = @Year" +
                     " and tsed.Product_Detail_Code = @Product_Detail_Code " +
                     " and Sec_Sale_Code = @Sec_Sale_Code ";

            //" and tseh.Status = 2 " +

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "getClosingBalance()");
            }
            return dsSale;
        }

        public DataSet getStockiest(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name, 1 cind " +
                     " UNION " +
                     " select 999 Stockist_Code, '---All---' Stockist_Name, 2 cind " +
                     " UNION " +
                     " select Stockist_Code,Stockist_Name, 3 cind " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_Code='" + div_code + "' " +
                     " ORDER BY 3";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getStockiestDet(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getrptfield(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Sec_Sale_Code, Sec_Sale_Name, Sec_Sale_Short_Name , Sel_Sale_Operator" +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND is_rpt_field = 0  and Active=0" +
                         " ORDER BY Sel_Sale_Operator ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        public DataSet getrptvalues(string div_code, string sf_code, int stock_code, int fmonth, int fyear, int tmonth, int tyear, string prod_code,
            double rate, int secsalecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "EXEC sp_get_sec_sale_details  '" + div_code + "' , '" + sf_code + "', " + stock_code + ", " + fmonth + ", " + fyear + ", " + tmonth + ", " +
                                                    tyear + " , '" + prod_code + "', " + rate + ", " + secsalecode + " ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        public DataSet getrptvalues_clbal(string div_code, string sf_code, int stock_code, int fmonth, int fyear, string prod_code,
            double rate, int secsalecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "EXEC sp_get_sec_sale_details_clbal  '" + div_code + "' , '" + sf_code + "', " + stock_code + ", " + fmonth + ", " +
                                                    fyear + " , '" + prod_code + "', " + rate + ", " + secsalecode + " ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        public DataSet getAddionalRptSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Total_Needed, " +
                            " Value_Needed, " +
                            " calc_rate, " +
                            " Prod_Grp " +
                     " FROM mas_common_sec_sale_setup " +
                     " WHERE Division_Code = '" + div_code + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getAddionalSaleMaster()");
            }
            return dsSale;
        }

        public DataSet Get_SecSaleCode_TotalField(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT sec_sale_code " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Sub_Name = 'Tot+' ";

                ds = db.Exec_DataSet(strQry, sCommand);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return ds;
        }

        public bool isSaleEntered(string sf_code, string div_code, int imonth, int iyear, int stockiest_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(SS_Head_Sl_No) " +
                         " FROM Trans_SS_Entry_Head " +
                         " WHERE SF_Code = '" + sf_code + "' " +
                         " AND Division_Code  = '" + div_code + "' " +
                         " AND Month = '" + imonth + "' " +
                         " AND Year = '" + iyear + "' " +
                         " AND Stockiest_Code = '" + stockiest_code + "' " +
                         " AND Status = 2";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "isSaleEntered()");
            }
            return bRecordExist;
        }

        public DataSet Get_Div_Year(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public bool getcount_ssentry(string div_code, string sf_code, string stock_code, int imonth, int iyear)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select COUNT(SS_Head_Sl_No) from Trans_SS_Entry_Head where sf_code='" + sf_code + "' and Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "SetupRecordExists()");
            }
            return bRecordExist;
        }

        public DataSet getSubmittedDate(string div_code, string sf_code, string stock_code, int imonth, int iyear)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = " select updated_dtm from Trans_SS_Entry_Head where sf_code='" + sf_code + "' and Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Stockiest Report", "getSubmittedDate()");
            }
            return ds;
        }

        public DataSet Get_SS_Stockiest_Details(string div_code, string sf_code, int imon, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select a.Stockiest_Code, b.Stockist_Name from Trans_SS_Entry_Head a, Mas_Stockist b where a.Stockiest_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and a.MONTH=" + imon + " and a.YEAR=" + iyr + " and a.status=2 ";
                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        public DataSet Get_SS_Option_Edit(string div_code, string sf_code, int istatus, string StockistCode, string Month, string Year)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "select SS_Head_Sl_No, MONTH, YEAR, Approval_Mgr, Stockiest_Code " +
                //            " from Trans_SS_Entry_Head " + 
                //            " WHERE Division_Code = '" + div_code + "' " + 
                //            " AND SF_Code = '" + sf_code + "' "  +
                //            " AND Status = " + istatus + " " +
                //            " AND submitted_dtm in ( " +
                //            " select MIN(submitted_dtm) from Trans_SS_Entry_Head " +
                //            " WHERE Division_Code = '" + div_code + "' " + 
                //            " AND SF_Code = '" + sf_code + "' " + 
                //            " AND Status = " + istatus + ") ";



                strQry = "select SS_Head_Sl_No, MONTH, YEAR, Approval_Mgr, Stockiest_Code " +
                           " from Trans_SS_Entry_Head " +
                           " WHERE Division_Code = '" + div_code + "' " +
                           " AND SF_Code = '" + sf_code + "' " +
                           " AND Status = " + istatus + " and Stockiest_Code='" + StockistCode + "' and Month='" + Month + "' and Year='" + Year + "'";


                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        public DataSet Get_SS_ClBal_Sub(string div_code)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select mssp.Sec_Sale_Sub_Name " +
                            " from Mas_Sec_Sale_Param mssp, Mas_Sec_Sale_Setup msss " +
                            " WHERE mssp.Division_Code = '" + div_code + "' " +
                            " AND msss.Division_Code =  '" + div_code + "' " +
                            " AND mssp.Sec_Sale_Code = msss.Sec_Sale_Code " +
                             "  and mssp.Active=0 and msss.Active=0" +
                            " AND msss.Carry_Fwd_Field = 1 ";

                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry - Closing Balance", "Get_SS_ClBal_Sub()");
            }
            return ds;
        }

        public DataSet getStockiest_Sale(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name, 1 cind " +
                     " UNION " +
                     " select -2 Stockist_Code, '---All---' Stockist_Name, 2 cind " +
                     " UNION " +
                     " select Stockist_Code,Stockist_Name, 3 cind " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_Code='" + div_code + "' " +
                     " ORDER BY 3";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet getProduct(string div_code, string state, DateTime cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            //strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
            //          " b.Product_Description, " +
            //          " b.Product_Detail_Name," +
            //          " b.Product_Sale_Unit,  " +
            //          " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
            //          " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
            //          " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price, " +
            //          " isnull(rtrim(Target_Price),0) Target_Price " +
            //          " From Mas_Product_Detail b" +
            //          " INNER JOIN Trans_SS_Entry_Detail c  " +
            //          " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
            //          " WHERE b.Division_Code= @Division_Code " +
            //          " AND b.Product_Active_Flag = 0 ";

            strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                      " b.Product_Description, " +
                      " b.Product_Detail_Name," +
                      " b.Product_Sale_Unit,  " +
                      " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                      " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                      " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                      " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                      " isnull(rtrim(Target_Price),0) Target_Price " +
                      " From Mas_Product_Detail b" +
                      " INNER JOIN Mas_Product_State_Rates c  " +
                      " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                      " WHERE b.Division_Code= @Division_Code " +
                      " AND b.Product_Active_Flag = 0" +
                      " AND c.state_code = '" + state + "' " +
                      " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public DataSet getStockiest_Mgr(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " EXEC getStockist_Mgr '" + div_code + "', '" + sf_code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getStockiest_Mgr_test(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " EXEC getStockist_Mgr_test '" + div_code + "', '" + sf_code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        /*--------------------------Get Recipt And Sale Qty (04/11/2016) -------------------------------------*/
        public DataSet Get_Sec_Sale_Receipt_Qty(string div_code, string Sf_Code, int StockistCode, int Month, int Year, string Product_Code, string SecSale_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "EXEC SP_Get_SecSale_ReceiptQty  '" + div_code + "' , '" + Sf_Code + "', '" + StockistCode + "', '" + Month + "', '" + Year + "', '" + Product_Code + "', '" + SecSale_Code + "'";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "Get_Sec_Sale_Receipt_Qty()");

            }

            return dsSecSale;
        }

        /*--------------------------Get SecSale Qty (04/11/2016) -------------------------------------*/
        public DataSet Get_Sec_Sale_Qty(string div_code, string Sf_Code, int StockistCode, int Month, int Year, string Product_Code, string SecSale_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "   select Sec_Sale_Code,Sec_Sale_Qty" +
                    " from Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed, Trans_SS_Entry_Detail_Value tsedv" +
                    " where tsedv.Division_Code = '" + div_code + "' " +
                    " and tsed.Division_Code ='" + div_code + "' " +
                    " and tseh.Division_Code = '" + div_code + "'" +
                    "and tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No" +
                    " and tseh.SF_Code = '" + Sf_Code + "' " +
                    " and tseh.Stockiest_Code = '" + StockistCode + "'" +
                    "and tseh.Month= '" + Month + "'" +
                    " and tseh.Year ='" + Year + "'" +
                    "and tsed.SS_Det_Sl_No=tsedv.SS_Det_Sl_No" +
                    " and tsed.Product_Detail_Code = '" + Product_Code + "'" +
                    "and tsedv.Sec_Sale_Code ='" + SecSale_Code + "'";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "Get_Sec_Sale_Receipt_Qty()");

            }

            return dsSecSale;
        }


        public DataSet GetMR_Status(string div_code, string sf_code, int imonth, int iyear, int stockiest_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT Status FROM Trans_SS_Entry_Head WHERE Division_Code  = '" + div_code + "'" +
                     "AND SF_Code = '" + sf_code + "' AND Month = '" + imonth + "' 	AND Year = '" + iyear + "' AND Stockiest_Code = " + stockiest_code + "";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Field_Parameter_Calc()");
            }

            return dsSale;
        }

        /*-------------------------- Get Brand wise Product Detail (25/11/2016) -------------------------------------*/
        public DataSet getProduct_Brand(string div_code, string state, DateTime cdate, string Sub_Div, string Brand)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            if (Sub_Div.Contains(','))
                Sub_Div = Sub_Div.Substring(0, Sub_Div.Length - 1);


            strQry = " SELECT DISTINCT Br.Product_Brd_Name,BR.Product_Brd_Code,b.Product_Detail_Code, " +
                      " b.Product_Description, " +
                      " b.Product_Detail_Name," +
                      " b.Product_Sale_Unit,  " +
                      " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                      " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                      " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                      " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                      " isnull(rtrim(Target_Price),0) Target_Price " +
                      " From Mas_Product_Detail b" +
                      " INNER JOIN Mas_Product_State_Rates c  " +
                      " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                      " inner join Mas_Product_Brand Br on b.Product_Brd_Code=Br.Product_Brd_Code" +
                      " WHERE b.Division_Code= @Division_Code " +
                      " AND b.Product_Active_Flag = 0" +
                      " AND Br.Product_Brd_Code='" + Brand + "'" +
                      " AND c.state_code = '" + state + "' " +
                      " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                      " AND (b.subdivision_code like '" + Sub_Div + ',' + "%'  or b.subdivision_code like '%" + ',' + Sub_Div + ',' + "%') ";


            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct_Brand()");
            }
            return dsProduct;
        }

        /*-------------------------- Get Brand wise Product Detail (28/11/2016) -------------------------------------*/
        public DataSet getProduct_Brand_wise_Detail(string div_code, string state, DateTime cdate, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_date = new SqlParameter();
            param_date.ParameterName = "@Sel_Date";    // Defining Name
            param_date.SqlDbType = SqlDbType.DateTime;           // Defining DataType
            param_date.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_date);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_date.Value = cdate;

            if (Sub_Div.Contains(','))
                Sub_Div = Sub_Div.Substring(0, Sub_Div.Length - 1);

            strQry = " SELECT DISTINCT Br.Product_Brd_Name,BR.Product_Brd_Code " +
                      " From Mas_Product_Detail b" +
                      " INNER JOIN Mas_Product_State_Rates c  " +
                      " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                      " inner join Mas_Product_Brand Br on b.Product_Brd_Code=Br.Product_Brd_Code" +
                      " WHERE b.Division_Code= @Division_Code " +
                      " AND b.Product_Active_Flag = 0" +
                      //" AND Br.Product_Brd_Code='" + Brand + "'" +
                      " AND c.state_code = '" + state + "' " +
                      " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                      " AND (b.subdivision_code like '" + Sub_Div + ',' + "%'  or b.subdivision_code like '%" + ',' + Sub_Div + ',' + "%') ";


            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct_Brand()");
            }
            return dsProduct;
        }

        /*-------------------------- Get Calculate Another Field (28/10/2016) -------------------------------------*/
        public DataSet Get_Another_CalcField_Name(string div_code, string SecSaleCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select case CalcF_Field when 'NULL' then '0' else CalcF_Field end as CalcF_Field  from [dbo].[Mas_Sec_Sale_Setup]" +
                     "where Division_Code='" + div_code + "' and Sec_Sale_Code='" + SecSaleCode + "' and Active='0'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Field_Parameter_Calc()");
            }
            return dsSale;
        }


        /*-------------------------- Get Calculate Another Field Name (28/10/2016) -------------------------------------*/
        public DataSet Get_Calculation_Field(string div_code, string SecSaleCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select Sec_Sale_Name from [dbo].[Mas_Sec_Sale_Param]" +
                     "where Division_Code='" + div_code + "' and Sec_Sale_Code='" + SecSaleCode + "' and Active='0'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Field_Parameter_Calc()");
            }
            return dsSale;
        }


        /*-------------------------- Get All Product Detail ID (09/12/2016) -------------------------------------*/
        public DataSet get_AllProductId(string div_code, string sf_code, int cmonth, int cyear, int stock_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            SqlParameter par_div_code = new SqlParameter();
            par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
            par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_sfcode = new SqlParameter();
            param_sfcode.ParameterName = "@Par_SF_Code";    // Defining Name
            param_sfcode.SqlDbType = SqlDbType.VarChar;           // Defining DataType
            param_sfcode.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_month = new SqlParameter();
            par_month.ParameterName = "@Par_Month";    // Defining Name
            par_month.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_month.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_year = new SqlParameter();
            par_year.ParameterName = "@Par_Year";    // Defining Name
            par_year.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_year.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter par_stock_code = new SqlParameter();
            par_stock_code.ParameterName = "@Par_Stock_Code";    // Defining Name
            par_stock_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            par_stock_code.Direction = ParameterDirection.Input;// Setting the direction 



            // Adding Parameter instances to sqlcommand
            sCommand.Parameters.Add(par_div_code);
            sCommand.Parameters.Add(param_sfcode);
            sCommand.Parameters.Add(par_month);
            sCommand.Parameters.Add(par_year);
            sCommand.Parameters.Add(par_stock_code);


            // Setting values of Parameter
            par_div_code.Value = Convert.ToInt16(div_code);
            param_sfcode.Value = sf_code;
            par_month.Value = cmonth;
            par_year.Value = cyear;
            par_stock_code.Value = stock_code;


            strQry = " SELECT tsed.SS_Det_Sl_No,tsed.Product_Detail_Code " +
                    " FROM Trans_SS_Entry_Head tseh, Trans_SS_Entry_Detail tsed " +
                    " WHERE tseh.SS_Head_Sl_No = tsed.SS_Head_Sl_No " +
                    " AND tseh.SF_Code = @Par_SF_Code " +
                    " AND tseh.Division_Code  = @Par_Division_Code " +
                    " AND tseh.Month = @Par_Month " +
                    " AND tseh.Year = @Par_Year " +
                    " AND tseh.Stockiest_Code = @Par_Stock_Code ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "get_AllProductId()");
            }

            return dsSale;
        }

        /*-------------------------- GetProduct_Detail_XML (27/12/2016) -------------------------------------*/

        public DataSet GetProduct_Detail_XML(string div_code, string state, string prod_grp, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 



            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);


            // Setting values of Parameter
            param_div_code.Value = div_code;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);

            if ((prod_grp == "C") || (prod_grp == "G"))
            {
                strQry = " EXEC sp_ProdList_SecSales '" + div_code + "', '" + state + "', '" + prod_grp + "','" + subdiv + "' ";
            }
            else
            {
                strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                          " b.Product_Description, " +
                          " b.Product_Detail_Name," +
                          " b.Product_Sale_Unit,  " +
                          " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                          " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                          " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                          " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                          " isnull(rtrim(Target_Price),0) Target_Price " +
                          " From Mas_Product_Detail b" +
                          " INNER JOIN Mas_Product_State_Rates c  " +
                          " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                          " WHERE b.Division_Code= @Division_Code " +
                          " AND b.Product_Active_Flag = 0 " +
                          " AND c.state_code = '" + state + "' " +
                          " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " +
                          " AND (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%') " +
                          " UNION ALL" +
                          " SELECT 'Tot_Prod' as Product_Detail_Code, '' as Product_Description, '' as Product_Detail_Name, '' as Product_Sale_Unit, " +
                          " '0' as MRP_Price, '0' as Retailor_Price, '0' as Distributor_Price, '0' as NSR_Price, '0' as Target_Price ";

            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }
        public DataSet Get_Sec_Sale_Code(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = "Exec Get_Sec_Sale_Code_With_Name '" + div_code + "' ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        /*-------------------------- Add TransSSEntry Head Insert Detail (06/03/2017) -------------------------------------*/
        public int Add_TransSSEntry_Head(string Stock_Code, string Month, string Year, string DivisionCode, string SF_Code, string StateCdoe, string Status, string ReportingTo)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "EXEC [dbo].[SP_TransSSEntryHead_Insert]  '" + Stock_Code + "', '" + Month + "', '" + Year + "', '" + DivisionCode + "', '" + SF_Code + "', '" + StateCdoe + "','" + Status + "','" + ReportingTo + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*-------------------------- Get ProductID TransSS Entry Detail (27/02/2017) -------------------------------------*/
        public int Get_Prod_MaxRecordId(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT MAX(cast(SS_Det_Sl_No as int)) as SS_Det_Sl_No  FROM Trans_SS_Entry_Detail";

                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "Get_Prod_MaxRecordId()");
            }
            return sl_no;
        }


        /*-------------------------- Get TransSS Entry Value ID (27/02/2017) -------------------------------------*/
        public int Get_TransVal_ID(string div_code)
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT MAX(cast(SS_Sec_Sl_No as int)) as SS_Det_Sl_No  FROM [Trans_SS_Entry_Detail_Value]";

                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "Get_TransVal_ID()");
            }
            return sl_no;
        }

        /*--------------------------Check If Trans Head Exists Or Not (01/03/2017) -------------------------------------*/
        public bool Check_Trans_Head_Code(string div_code, string imonth, string iyear, string lmonth, string lyear, string stock_code, string Sub_Div)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select SS_Head_Sl_No from Trans_SS_Entry_Head where Division_Code='" + div_code + "' " +
                           "and Month='" + imonth + "' and Year='" + iyear + "' and Stockiest_Code='" + stock_code + "' and Subdiv_Code='" + Sub_Div + "'";


                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_SecSale_Entry_Exists()");
            }
            return bRecordExist;
        }

        /*--------------------------Get All Stockist(04/03/2017) -------------------------------------*/
        public DataSet Get_AllStockist_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Territory,State " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and  Division_code = '" + div_code + "' order by State,Territory,Stockist_Name";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        /*--------------------------Get Count SS Entry(04/03/2017) -------------------------------------*/
        public bool Get_CountEntry(string div_code, string stock_code, int imonth, int iyear)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select COUNT(SS_Head_Sl_No) from Trans_SS_Entry_Head where  Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "SetupRecordExists()");
            }
            return bRecordExist;
        }

        /*--------------------------Get SubmitDate(04/03/2017) -------------------------------------*/
        public DataSet get_Submitted_Date(string div_code, string stock_code, int imonth, int iyear)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = " select updated_dtm from Trans_SS_Entry_Head where  Stockiest_Code = '" + stock_code + "' and Division_Code = '" + div_code + "' and MONTH = " + imonth + " and YEAR = " + iyear + " ";
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Stockiest Report", "getSubmittedDate()");
            }
            return ds;
        }

        public DataSet getrptfield_Excel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSecSale = null;

            strQry = " SELECT Sec_Sale_Code, Sec_Sale_Name, Sec_Sale_Short_Name , Sel_Sale_Operator" +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND is_rpt_field = 0  and Active=0" +
                         " ORDER BY Sec_Sale_Code ";

            try
            {
                dsSecSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sale Report", "getrptfield()");

            }

            return dsSecSale;
        }

        /*--------------- Get Doctor Service RPT (02/06/2017) -----------------------*/
        public DataSet Get_MGRwise_Rpt_Stockist(string div_code, string Month, string Year, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_MGR_Stockist_Detail '" + div_code + "','" + Sf_Code + "','" + Month + "','" + Year + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_MGRwise_Rpt_Stockist()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Statewise Stockist (06/09/2017) -----------------------------------------------------*/
        public DataSet Get_Statewise_StockistDet(string div_code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Proc_GetStatewise_StockistList '" + div_code + "','" + Month + "','" + Year + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Statewise_StockistDet()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get MR wise Stockist (06/09/2017) -----------------------------------------------------*/
        public DataSet Get_Statewise_Stock_MR_Det(string div_code, string StateCode, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_GetStockist_Statewise_MRList '" + div_code + "','" + StateCode + "','" + Month + "','" + Year + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Statewise_Stock_MR_Det()");
            }
            return dsProduct;
        }

        /*-------------------------- Check Total Exist or Not (14-06-2017) -------------------------------------*/
        public bool isParameter_TotalExist(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND  Sec_Sale_Name like'%To%' and Active=0 ";


                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "isParameter_TotalExist()");
            }
            return bRecordExist;
        }
        public DataSet getStockiest_Statewise(string div_code, string sf_code, string state)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " EXEC getStockist_Statewise '" + div_code + "', '" + sf_code + "','" + state + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        #region ServiceCRM

        public DataSet GetStockistDet_DDL(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' and Stockist_Name not like 'Direct%' " +
                     " ORDER BY 2";


            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet User_MRwise_Hierarchy(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC SP_Get_MrandMGR_ListDetailCRRM  '" + sf_code + "','" + divcode + "' ";

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

        /*------------------------------------------- Get Listed Doctor CRM Detail (18/5/2017) -----------------------------------------------------------*/
        public DataSet Get_Listed_Dr_CRM(string sfcode, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC SP_Get_Listed_Doctor_Detail '" + sfcode + "','" + Div_Code + "'";

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

        /*------------------------------------------- Get Listed Doctor CRM Closing Status (18/5/2017) -----------------------------------------------------------*/
        public DataSet GetDoctor_Service_CloseStatus(string div_code, string DoctorCode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select Sl_No,Doctor_Code,case Close_Service_Dr when 'NULL' then '0' " +
                    " else Close_Service_Dr end as Close_Service_Dr, case Total_Business_Expect when '' then '0' else Total_Business_Expect end as Total_Business_Expect  ,Ser_Type  from dbo.Trans_Doctor_Service_Head where " +
                    " Doctor_Code='" + DoctorCode + "' and Sf_Code='" + Sf_Code + "' and  Division_Code='" + div_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDoctor_Service_CloseStatus()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Doctor Service Total Business (18/5/2017) -----------------------------------------------------*/
        public DataSet GetTotal_Business(string div_code, string DoctorCode, string Sf_Code, string ServiceNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select SUM(CAST(Product_Total AS float)) as Total from dbo.Trans_Service_Business_Against_Head_Detail" +
                      " where Service_No='" + ServiceNo + "' and Doctor_Code='" + DoctorCode + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + div_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDoctor_Service_CloseStatus()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get  Total Service Amount (18/5/2017) -----------------------------------------------------*/
        public DataSet Get_Dr_TotalServiceAmt(string Dr_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsDoctor = null;

            strQry = "  select D.Doctor_Code,SUM(CAST(D.Service_Amt AS int))as Service_Amt," +
                        "SUM(CAST(B.Product_Total  as decimal(18,2)))as Product_Total  " +
                        "from  dbo.Trans_Doctor_Service_Head D " +
                        "inner join dbo.Trans_Service_Business_Against_Head_Detail B on " +
                        "D.Sl_No=B.Service_No where D.Doctor_Code='" + Dr_Code + "' and D.Division_Code='" + div_code + "'" +
                        "group by D.Doctor_Code";

            try
            {
                dsDoctor = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Dr_TotalServiceAmt()");
            }
            return dsDoctor;
        }

        /*--------------------------Get MR wise Product Detail (18/5/2017)-------------------------------------*/
        public DataSet Get_MRwise_ProductDetail(string div_code, string state, string prod_grp, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);


            strQry = "SELECT Distinct Product_Detail_Code,Product_Detail_Name,MRP_Price," +
          "Retailor_Price,Distributor_Price,NSR_Price,Target_Price " +
          "  FROM" +
          "(" +
          "SELECT b.Product_Detail_Code,Product_Detail_Name,isnull(rtrim(MRP_Price),0) MRP_Price,   " +
          "isnull(rtrim(Retailor_Price),0) Retailor_Price,  isnull(rtrim(Distributor_Price),0) Distributor_Price,  " +
          "isnull(rtrim(NSR_Price),0) NSR_Price,  isnull(rtrim(Target_Price),0) Target_Price,CAST('<XMLRoot><RowData>' + " +
          "REPLACE(b.subdivision_code,',','</RowData><RowData>') " +
          "+ '</RowData></XMLRoot>' AS XML) AS x," +
          "CAST('<M><RowData>' + " +
          "REPLACE(b.State_Code,',','</RowData><RowData>') " +
          "+ '</RowData></M>' AS XML) AS y" +
          "  FROM  Mas_Product_Detail b   INNER JOIN " +
          "Mas_Product_State_Rates c   ON b.Product_Detail_Code = c.Product_Detail_Code  where b.Division_Code='" + div_code + "'" +
          ")t " +
          "CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)" +
          "cross apply y.nodes('/M/RowData')StateCode(d)" +
          "where " +
          "LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(" + subdiv + ")" +
          "and " +
          "LTRIM(RTRIM(StateCode.d.value('.[1]','varchar(8000)')))='" + state + "' order by Product_Detail_Name";


            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_MRwise_ProductDetail()");
            }
            return dsProduct;
        }

        /*--------------------------Get MR wise Product Price (18/5/2017)-------------------------------------*/
        public DataSet GetProductPrice(string div_code, string state, string Prod_Code, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);


            strQry = "SELECT Distinct Product_Detail_Code,Product_Detail_Name,MRP_Price," +
  "Retailor_Price,Distributor_Price,NSR_Price,Target_Price " +
  "  FROM" +
  "(" +
  "SELECT b.Product_Detail_Code,Product_Detail_Name,isnull(rtrim(MRP_Price),0) MRP_Price,   " +
  "isnull(rtrim(Retailor_Price),0) Retailor_Price,  isnull(rtrim(Distributor_Price),0) Distributor_Price,  " +
  "isnull(rtrim(NSR_Price),0) NSR_Price,  isnull(rtrim(Target_Price),0) Target_Price,CAST('<XMLRoot><RowData>' + " +
  "REPLACE(b.subdivision_code,',','</RowData><RowData>') " +
  "+ '</RowData></XMLRoot>' AS XML) AS x," +
  "CAST('<M><RowData>' + " +
  "REPLACE(b.State_Code,',','</RowData><RowData>') " +
  "+ '</RowData></M>' AS XML) AS y" +
  "  FROM  Mas_Product_Detail b   INNER JOIN " +
  "Mas_Product_State_Rates c   ON b.Product_Detail_Code = c.Product_Detail_Code  where b.Division_Code='" + div_code + "' and b.Product_Detail_Code ='" + Prod_Code + "'" +
  ")t " +
  "CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)" +
  "cross apply y.nodes('/M/RowData')StateCode(d)" +
  "where " +
  "LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(" + subdiv + ")" +
  "and " +
  "LTRIM(RTRIM(StateCode.d.value('.[1]','varchar(8000)')))='" + state + "'";



            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_MRwise_ProductDetail()");
            }
            return dsProduct;
        }

        /*------------------------------------------ Get Trans Service Dr Head ID (18/5/2017) ---------------------------------------*/

        public DataSet Get_Trans_SrveiceDr_Head_ID(string DoctorCode, string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select isnull(MAX(sl_no), 0) as Sl_No from Trans_Doctor_Service_Head ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }

        /*------------------------------------------ Get Trans Service Required ID (18/5/2017) --------------------------------------*/

        public DataSet Get_Trans_Doctor_Service_reqID(string Doc_Code, string Sf_Code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT  Type,STUFF((SELECT ', ' + CAST(SerReq_No AS VARCHAR(10)) [text()] " +
            "FROM Trans_Doctor_Service_Required  WHERE Doctor_Code='" + Doc_Code + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + Div_Code + "' and Type = t.Type  " +
            "FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') SerReq_No " +
            "FROM Trans_Doctor_Service_Required t " +
            "where Doctor_Code='" + Doc_Code + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + Div_Code + "'" +
            "GROUP BY Type order by Type Desc";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }

        /*-------------------------------------------Add Trans Service Dr Head (18/5/2017) -----------------------------------------------------------*/
        public int Add_Trans_Service_DrHead(string DoctorCode, string Sf_Code, string Financial_Year, string Trans_Month,
              string Trans_Year, string Ser_Amt_Till_Date, string Busi_Amt_till_date, string Tot_Busi_Expect,
              string ROI_Month, string Ser_Req, string Ser_Amt, string Spec_Amt_Remark, string Prescr_Chemesit_1,
              string Prescr_Chemesit_2, string Prescr_Chemesit_3, string Stockist_1, string Stockist_2,
              string Stockist_3, string div_Code, string T_Sl_No, string Sf_Name, string Sf_Mgr_1,
              string Sf_Mgr_2, string Sf_Mgr_3, string Sf_Mgr_4, string Sf_Mgr_5, string Ser_Sl_No, string Ser_Type,
              string Level_1, string Level_2, string Level_3, string Level_4, string Level_5, string Level_6, string Level_7,
              string Chemist_Cd, string Mode_Type, string Crm_MGR, string Rpt_To_Cde, string BillType)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "EXEC SP_TransDrService_Head_AddDetail '" + DoctorCode + "','" + Sf_Code + "','" + Financial_Year + "','" + Trans_Month + "'," +
                               "'" + Trans_Year + "','" + Ser_Amt_Till_Date + "','" + Busi_Amt_till_date + "','" + Tot_Busi_Expect + "'," +
                               "'" + ROI_Month + "','" + Ser_Req + "','" + Ser_Amt + "','" + Spec_Amt_Remark + "','" + Prescr_Chemesit_1 + "','" + Prescr_Chemesit_2 + "'," +
                               "'" + Prescr_Chemesit_3 + "','" + Stockist_1 + "','" + Stockist_2 + "','" + Stockist_3 + "','" + div_Code + "','" + T_Sl_No + "'," +
                               "'" + Sf_Name + "','" + Sf_Mgr_1 + "','" + Sf_Mgr_2 + "','" + Sf_Mgr_3 + "','" + Sf_Mgr_4 + "','" + Sf_Mgr_5 + "','" + Ser_Sl_No + "','" + Ser_Type + "'," +
                               "'" + Level_1 + "','" + Level_2 + "','" + Level_3 + "','" + Level_4 + "','" + Level_5 + "','" + Level_6 + "','" + Level_7 + "','" + Chemist_Cd + "'," +
                               "'" + Mode_Type + "','" + Crm_MGR + "','" + Rpt_To_Cde + "','" + BillType + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        /*------------------------------------------ Get Trans Service Dr Product ID (18/5/2017) ---------------------------------------*/

        public DataSet Get_Trans_SrveiceDr_ProductID(string div_code, string T_Sl_No)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;


            strQry = "select  isnull(max(cast(Prd_Sl_No as int)),0) as Prd_Sl_No  from Trans_Doctor_Service_Product ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }


        /*----------------------------------------------- Add Trans Service Doctor Product Detail (18/5/2017) -----------------------------------------------------*/

        public int Add_Trans_Service_Dr_Product(string Sl_No, string Cur_Prod_Code_1, string Cur_Prod_Price_1, string Cur_Prod_Qty_1, string Cur_Prod_Value_1, string Cur_Prod_Code_2, string Cur_Prod_Price_2,
       string Cur_Prod_Qty_2, string Cur_Prod_Value_2, string Cur_Prod_Code_3, string Cur_Prod_Price_3, string Cur_Prod_Qty_3, string Cur_Prod_Value_3,
       string Cur_Prod_Code_4, string Cur_Prod_Price_4, string Cur_Prod_Qty_4, string Cur_Prod_Value_4, string Cur_Prod_Code_5, string Cur_Prod_Price_5, string Cur_Prod_Qty_5, string Cur_Prod_Value_5,
       string Cur_Prod_Code_6, string Cur_Prod_Price_6, string Cur_Prod_Qty_6, string Cur_Prod_Value_6, string Cur_Total,
       string Potl_Prod_Code_1, string Potl_Prod_Price_1, string Potl_Prod_Qty_1, string Potl_Prod_Value_1, string Potl_Prod_Code_2, string Potl_Prod_Price_2, string Potl_Prod_Qty_2, string Potl_Prod_Value_2,
       string Potl_Prod_Code_3, string Potl_Prod_Price_3, string Potl_Prod_Qty_3, string Potl_Prod_Value_3, string Potl_Prod_Code_4, string Potl_Prod_Price_4, string Potl_Prod_Qty_4, string Potl_Prod_Value_4,
       string Potl_Prod_Code_5, string Potl_Prod_Price_5, string Potl_Prod_Qty_5, string Potl_Prod_Value_5, string Potl_Prod_Code_6, string Potl_Prod_Price_6, string Potl_Prod_Qty_6, string Potl_Prod_Value_6,
       string Potential_Total, string Division_Code, string Cur_Crm_Prd_1, string Cur_Crm_Prd_2, string Cur_Crm_Prd_3, string Cur_Crm_Prd_4,
       string Cur_Crm_Prd_5, string Cur_Crm_Prd_6, string Cur_Crm_Tot)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Trans_Service_Dr_Prodcut '" + Sl_No + "','" + Cur_Prod_Code_1 + "','" + Cur_Prod_Price_1 + "','" + Cur_Prod_Qty_1 + "','" + Cur_Prod_Value_1 + "','" + Cur_Prod_Code_2 + "','" + Cur_Prod_Price_2 + "'," +
                         "'" + Cur_Prod_Qty_2 + "','" + Cur_Prod_Value_2 + "','" + Cur_Prod_Code_3 + "','" + Cur_Prod_Price_3 + "','" + Cur_Prod_Qty_3 + "','" + Cur_Prod_Value_3 + "'," +
                         "'" + Cur_Prod_Code_4 + "','" + Cur_Prod_Price_4 + "','" + Cur_Prod_Qty_4 + "','" + Cur_Prod_Value_4 + "','" + Cur_Prod_Code_5 + "','" + Cur_Prod_Price_5 + "','" + Cur_Prod_Qty_5 + "','" + Cur_Prod_Value_5 + "'," +
                         "'" + Cur_Prod_Code_6 + "','" + Cur_Prod_Price_6 + "','" + Cur_Prod_Qty_6 + "','" + Cur_Prod_Value_6 + "','" + Cur_Total + "','" + Potl_Prod_Code_1 + "','" + Potl_Prod_Price_1 + "','" + Potl_Prod_Qty_1 + "'," +
                         "'" + Potl_Prod_Value_1 + "','" + Potl_Prod_Code_2 + "','" + Potl_Prod_Price_2 + "','" + Potl_Prod_Qty_2 + "','" + Potl_Prod_Value_2 + "','" + Potl_Prod_Code_3 + "','" + Potl_Prod_Price_3 + "','" + Potl_Prod_Qty_3 + "'," +
                         "'" + Potl_Prod_Value_3 + "','" + Potl_Prod_Code_4 + "','" + Potl_Prod_Price_4 + "','" + Potl_Prod_Qty_4 + "','" + Potl_Prod_Value_4 + "','" + Potl_Prod_Code_5 + "','" + Potl_Prod_Price_5 + "','" + Potl_Prod_Qty_5 + "','" + Potl_Prod_Value_5 + "','" + Potl_Prod_Code_6 + "','" + Potl_Prod_Price_6 + "','" + Potl_Prod_Qty_6 + "'," +
                         "'" + Potl_Prod_Value_6 + "','" + Potential_Total + "','" + Division_Code + "','" + Cur_Crm_Prd_1 + "','" + Cur_Crm_Prd_2 + "','" + Cur_Crm_Prd_3 + "'," +
                         "'" + Cur_Crm_Prd_4 + "','" + Cur_Crm_Prd_5 + "','" + Cur_Crm_Prd_6 + "','" + Cur_Crm_Tot + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        /*-------------------------- Get Trans Service Req ID (18/5/2017) -------------------------------------*/
        public int Get_Dr_Service_Req_ID()
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select isnull(max(SerReq_No),0)+1 as SerReq_No " +
                         "from dbo.Trans_Doctor_Service_Required";

                sl_no = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {

            }
            return sl_no;
        }

        /*------------------------------------------- Doctor Service Requierd (18/5/2017) -----------------------------------------------------------*/

        public int Doctor_Service_Req(string Doctor_Code, string Sf_Code, string Division_Code,
             string Type, string Location, string Hotel_Type, string No_Of_Person, string From_Date, string To_Date, string Arraival_Time, string Departure_Time, string Remarks,
             string Type_of_Travel, string Date, string From_Travel, string To_Travel, string No_of_Hrs_Km, string Name, string Age, string Sex, string Id_Proof,
             string Name_of_Books, string Author, string Edition, string Approx_Cost, string Name_of_Conference, string Type_Of_Participation, string Cost, string Cheque_Draft,
             string Payable_At, string Item_Descrp, string Rate, string Qty, string Service_Sl_No, string Trans_Sl_No, string Service_Req_No, string Other_II, string Other_Remark,
           string Chemist_Cd, string ModeType)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "EXEC SP_Doctor_ServiceReq_Add_Test '" + Doctor_Code + "','" + Sf_Code + "','" + Division_Code + "','" + Type + "'," +
                  "'" + Location + "','" + Hotel_Type + "','" + No_Of_Person + "','" + From_Date + "'," +
                  "'" + To_Date + "','" + Arraival_Time + "','" + Departure_Time + "','" + Remarks + "','" + Type_of_Travel + "','" + Date + "','" + From_Travel + "'," +
                  "'" + To_Travel + "','" + No_of_Hrs_Km + "','" + Name + "','" + Age + "','" + Sex + "','" + Id_Proof + "'," +
                  "'" + Name_of_Books + "','" + Author + "','" + Edition + "','" + Approx_Cost + "','" + Name_of_Conference + "','" + Type_Of_Participation + "'," +
                  "'" + Cost + "','" + Cheque_Draft + "','" + Payable_At + "','" + Item_Descrp + "','" + Rate + "','" + Qty + "','" + Service_Sl_No + "','" + Other_II + "'," +
                  "'" + Other_Remark + "','" + Chemist_Cd + "','" + ModeType + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        /*------------------------------------------- Get CRM Doctor Detail (18/5/2017)) -----------------------------------------------------*/
        public DataSet Get_Service_CRM_Dr_Detail(string div_code, string DoctorCode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_Listed_DoctorDetail_CRM '" + div_code + "','" + DoctorCode + "','" + Sf_Code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Service_CRM_Dr_Detail()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Doctor Service Date(18/5/2017) -----------------------------------------------------*/
        public DataSet Get_Doctor_ServiceDate(string div_code, string DoctorCode, string ModeType)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (ModeType == "Chemist")
            {

                strQry = "select Sl_No," +
                       "CONVERT(VARCHAR(10),Created_Date,103)+'-'+Service_Req+'-'+Service_Amt as ServiceName" +
                       "   from Trans_Doctor_Service_Head where Chemists_Code='" + DoctorCode + "' and Division_Code='" + div_code + "'";
            }
            else
            {
                strQry = "select Sl_No," +
                         "CONVERT(VARCHAR(10),Created_Date,103)+'-'+Service_Req+'-'+Service_Amt as ServiceName" +
                         "   from Trans_Doctor_Service_Head where Doctor_Code='" + DoctorCode + "' and Division_Code='" + div_code + "'";
            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Doctor_ServiceDate()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Update Doctor Service CRM(18/5/2017) -----------------------------------------------------*/
        public int Doctor_Service_Update(string Sl_No, string Doctor_Code, string Sf_Code, string Division_Code, string Service_Statement)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_Doctor_Service_Head set Last_Updated_Date=GETDATE(),Service_Statement='" + Service_Statement + "'" +
                          "where Sl_No='" + Sl_No + "' and Doctor_Code='" + Doctor_Code + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + Division_Code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*------------------------------------------- Get Doctor Service CRM (18/5/2017) -----------------------------------------------------*/
        public DataSet Get_DoctorService_CRMDetail(string Sl_No, string DoctorCode, string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = " SELECT * " +
            "  FROM  Mas_ListedDr d " +
            " inner join Trans_Doctor_Service_Head S " +
            " on d.ListedDrCode=S.Doctor_Code  inner join Trans_Doctor_Service_Product P on P.Sl_No=S.Sl_No " +
            " WHERE d.ListedDrCode =  '" + DoctorCode + "' " +
            " and d.ListedDr_Active_Flag = 0 and S.Sl_No='" + Sl_No + "' and S.Division_Code='" + Division_Code + "'";


            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(Division_Code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Service_CRM_Dr_Detail()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Update Trans Service Dr Head (18/5/2017) -----------------------------------------------------------*/

        public int Update_Trans_Service_DrHead(string DoctorCode, string Sf_Code, string Financial_Year, string Trans_Month,
             string Trans_Year, string Ser_Amt_Till_Date, string Busi_Amt_till_date, string Tot_Busi_Expect,
             string ROI_Month, string Ser_Req, string Ser_Amt, string Spec_Amt_Remark, string Prescr_Chemesit_1,
             string Prescr_Chemesit_2, string Prescr_Chemesit_3, string Stockist_1, string Stockist_2,
             string Stockist_3, string div_Code, string T_Sl_No, string Sf_Name, string Sf_Mgr_1,
             string Sf_Mgr_2, string Sf_Mgr_3, string Sf_Mgr_4, string Sf_Mgr_5, string Service_Statement, string ApprovalStatus, string SF_ID)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Trans_Dr_Service_Head_Update '" + DoctorCode + "','" + Sf_Code + "','" + Financial_Year + "','" + Trans_Month + "'," +
                         "'" + Trans_Year + "','" + Ser_Amt_Till_Date + "','" + Busi_Amt_till_date + "','" + Tot_Busi_Expect + "'," +
                         "'" + ROI_Month + "','" + Ser_Req + "','" + Ser_Amt + "','" + Spec_Amt_Remark + "','" + Prescr_Chemesit_1 + "','" + Prescr_Chemesit_2 + "'," +
                         "'" + Prescr_Chemesit_3 + "','" + Stockist_1 + "','" + Stockist_2 + "','" + Stockist_3 + "','" + div_Code + "','" + T_Sl_No + "'," +
                         "'" + Sf_Name + "','" + Sf_Mgr_1 + "','" + Sf_Mgr_2 + "','" + Sf_Mgr_3 + "','" + Sf_Mgr_4 + "','" + Sf_Mgr_5 + "','" + Service_Statement + "','" + ApprovalStatus + "','" + SF_ID + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        /*----------------------------------------------- Update Trans Service Doctor Product Detail (18/05/2017) -----------------------------------------------------*/

        public int Update_Trans_Service_Dr_Product(string Prd_Sl_No, string Sl_No, string Cur_Prod_Code_1, string Cur_Prod_Price_1, string Cur_Prod_Qty_1, string Cur_Prod_Value_1, string Cur_Prod_Code_2, string Cur_Prod_Price_2,
       string Cur_Prod_Qty_2, string Cur_Prod_Value_2, string Cur_Prod_Code_3, string Cur_Prod_Price_3, string Cur_Prod_Qty_3, string Cur_Prod_Value_3,
       string Cur_Prod_Code_4, string Cur_Prod_Price_4, string Cur_Prod_Qty_4, string Cur_Prod_Value_4, string Cur_Prod_Code_5, string Cur_Prod_Price_5, string Cur_Prod_Qty_5, string Cur_Prod_Value_5,
       string Cur_Prod_Code_6, string Cur_Prod_Price_6, string Cur_Prod_Qty_6, string Cur_Prod_Value_6, string Cur_Total,
       string Potl_Prod_Code_1, string Potl_Prod_Price_1, string Potl_Prod_Qty_1, string Potl_Prod_Value_1, string Potl_Prod_Code_2, string Potl_Prod_Price_2, string Potl_Prod_Qty_2, string Potl_Prod_Value_2,
       string Potl_Prod_Code_3, string Potl_Prod_Price_3, string Potl_Prod_Qty_3, string Potl_Prod_Value_3, string Potl_Prod_Code_4, string Potl_Prod_Price_4, string Potl_Prod_Qty_4, string Potl_Prod_Value_4,
       string Potl_Prod_Code_5, string Potl_Prod_Price_5, string Potl_Prod_Qty_5, string Potl_Prod_Value_5, string Potl_Prod_Code_6, string Potl_Prod_Price_6, string Potl_Prod_Qty_6, string Potl_Prod_Value_6,
       string Potential_Total, string Division_Code, string Cur_Crm_Prd_1, string Cur_Crm_Prd_2, string Cur_Crm_Prd_3, string Cur_Crm_Prd_4,
       string Cur_Crm_Prd_5, string Cur_Crm_Prd_6, string Cur_Crm_Tot)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Trans_Service_Dr_Prodcut_Update '" + Prd_Sl_No + "','" + Sl_No + "','" + Cur_Prod_Code_1 + "','" + Cur_Prod_Price_1 + "','" + Cur_Prod_Qty_1 + "','" + Cur_Prod_Value_1 + "','" + Cur_Prod_Code_2 + "','" + Cur_Prod_Price_2 + "'," +
                         "'" + Cur_Prod_Qty_2 + "','" + Cur_Prod_Value_2 + "','" + Cur_Prod_Code_3 + "','" + Cur_Prod_Price_3 + "','" + Cur_Prod_Qty_3 + "','" + Cur_Prod_Value_3 + "'," +
                         "'" + Cur_Prod_Code_4 + "','" + Cur_Prod_Price_4 + "','" + Cur_Prod_Qty_4 + "','" + Cur_Prod_Value_4 + "','" + Cur_Prod_Code_5 + "','" + Cur_Prod_Price_5 + "','" + Cur_Prod_Qty_5 + "','" + Cur_Prod_Value_5 + "'," +
                         "'" + Cur_Prod_Code_6 + "','" + Cur_Prod_Price_6 + "','" + Cur_Prod_Qty_6 + "','" + Cur_Prod_Value_6 + "','" + Cur_Total + "','" + Potl_Prod_Code_1 + "','" + Potl_Prod_Price_1 + "','" + Potl_Prod_Qty_1 + "'," +
                         "'" + Potl_Prod_Value_1 + "','" + Potl_Prod_Code_2 + "','" + Potl_Prod_Price_2 + "','" + Potl_Prod_Qty_2 + "','" + Potl_Prod_Value_2 + "','" + Potl_Prod_Code_3 + "','" + Potl_Prod_Price_3 + "','" + Potl_Prod_Qty_3 + "'," +
                         "'" + Potl_Prod_Value_3 + "','" + Potl_Prod_Code_4 + "','" + Potl_Prod_Price_4 + "','" + Potl_Prod_Qty_4 + "','" + Potl_Prod_Value_4 + "','" + Potl_Prod_Code_5 + "','" + Potl_Prod_Price_5 + "','" + Potl_Prod_Qty_5 + "','" + Potl_Prod_Value_5 + "','" + Potl_Prod_Code_6 + "','" + Potl_Prod_Price_6 + "','" + Potl_Prod_Qty_6 + "'," +
                         "'" + Potl_Prod_Value_6 + "','" + Potential_Total + "','" + Division_Code + "','" + Cur_Crm_Prd_1 + "','" + Cur_Crm_Prd_2 + "','" + Cur_Crm_Prd_3 + "'," +
                         "'" + Cur_Crm_Prd_4 + "','" + Cur_Crm_Prd_5 + "','" + Cur_Crm_Prd_6 + "','" + Cur_Crm_Tot + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        /*-------------------------- Get Trans Service Max Record ID (18/05/2017) -------------------------------------*/
        public int Get_Trans_Service_MaxRecordId()
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Trans_Sl_No), 0) as Trans_Sl_No  FROM Trans_Service_Business_Against_Head_Detail";

                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {

            }
            return sl_no;
        }

        /*-------------------------- Insert Trans Service Business Against Head Detail  (18/05/2017) -------------------------------------*/
        public int Add_TransService_Business_Against_Head(int Trans_Sl_No, string Doctor_Code, string Sf_Code,
                   string Trans_Month, string Trans_Year, string Service_No, string Division_Code, string Product_Total)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Exec SP_Proc_TransService_Business_Against_HeadDetail '" + Trans_Sl_No + "','" + Doctor_Code + "','" + Sf_Code + "','" + Trans_Month + "','" + Trans_Year + "','" + Service_No + "','" + Division_Code + "','" + Product_Total + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*-------------------------- Get Trans Service Business Product Max ID (18/05/2017) -------------------------------------*/
        public int Get_Trans_Service_Prodcut_MaxRecordId()
        {
            int sl_no = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT  ISNULL(MAX(Trans_Prd_No), 0) as Trans_Prd_No  FROM Trans_Service_Business_Against_Product_Detail";

                sl_no = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {

            }
            return sl_no;
        }

        /*------------------------------------------- Get Doctor Service Req(18/05/2017) -----------------------------------------------------*/
        public DataSet Get_Doctor_Service_Req(string div_code, string Service_Sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "EXEC SP_Get_Doctor_Service_Req '" + Service_Sl_no + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Doctor_Service_Req()");
            }
            return dsProduct;
        }

        /*---------------------------------------- Get Trans Doctor Service ID (18/05/2017)  --------------------------------------------*/
        public DataSet Get_Trans_Dr_Service_reqID(string Doc_Code, string Sf_Code, string Div_Code, string Type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;


            strQry = "SELECT  Type,STUFF((SELECT ', ' + CAST(SerReq_No AS VARCHAR(10)) [text()] " +
             "FROM Trans_Doctor_Service_Required  WHERE  Doctor_Code='" + Doc_Code + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + Div_Code + "' and Type = t.Type  " +
             "FOR XML PATH(''), TYPE).value('.','NVARCHAR(MAX)'),1,2,' ') SerReq_No " +
             "FROM Trans_Doctor_Service_Required t " +
             "where Doctor_Code='" + Doc_Code + "' and Sf_Code='" + Sf_Code + "' and Division_Code='" + Div_Code + "'" +
             "and Type='" + Type + "'  GROUP BY Type";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }


        /*-------------------------- Update Trans Service Req Head  (18/05/2017) -------------------------------------*/
        public int Get_Doctor_Service_HeadUpdate(string TransSl_No, string Service_Sl_No)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update dbo.Trans_Doctor_Service_Head set Service_Sl_No='" + Service_Sl_No + "', " +
                  "Last_Updated_Date=GETDATE() where Sl_No='" + TransSl_No + "'";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*---------------------------------------- Get Trans Doctor Service Recevied (18/05/2017) ---------------------*/
        public DataSet Get_Dr_Service_CRM_Recevied(string Sf_Code, string div_code, string ModeType, string CrmMgr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;


            strQry = "Exec SP_GetDrService_MgrApproval_CRM '" + Sf_Code + "','" + div_code + "','" + ModeType + "','" + CrmMgr + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Dr_Service_CRM_Recevied()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Close Service Detail(18/05/2017) -----------------------------------------------------*/
        public DataSet Get_Close_Service_Detail(string div_code, string DoctorCode, string Sf_Code, string DrSlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select Sl_No,Created_Date,Service_Req,Service_Amt,Total_Business_Expect,Close_Service_Dr,Ser_Type from dbo.Trans_Doctor_Service_Head " +
                     " where Doctor_Code='" + DoctorCode + "' and Sf_Code='" + Sf_Code + "'" +
                     " and  Division_Code='" + div_code + "' and (Ser_Type=1 or Ser_Type=2 or Ser_Type=3) and Sl_No='" + DrSlNo + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Close_Service_Detail()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Business Given Prodcut Total (18/05/2017) -----------------------------------------------------*/
        public DataSet Get_Business_Given_ProductTotal(string div_code, string DoctorCode, string Sf_Code, string ServiceNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select Trans_Sl_No,Service_No,Product_Total,Close_Service_Business from dbo.Trans_Service_Business_Against_Head_Detail " +
                   " where Doctor_Code='" + DoctorCode + "' and Sf_Code='" + Sf_Code + "'" +
                   " and  Division_Code='" + div_code + "' and Service_No='" + ServiceNo + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Business_Given_ProductTotal()");
            }
            return dsProduct;
        }

        /*----------------------------------------------- Update Trans Service Close (18/05/2017) -----------------------------------------------------*/

        public int Update_Dr_Service_Close(string div_Code, string Doctor_Code, string Sf_Code, string Sl_No,
         string Trans_Sl_No, string Close_Service, string Ser_Type)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Trans_Dr_CloseService_Update '" + div_Code + "','" + Doctor_Code + "','" + Sf_Code + "','" + Sl_No + "','" + Trans_Sl_No + "','" + Close_Service + "','" + Ser_Type + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        /*------------------------------------------- Get Doctor Service Report (18/05/2017) -----------------------------------------------------*/
        public DataSet Get_Doctor_Service_CRM_Rpt(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_Doctor_Service_CRM_Rpt '" + Sf_Code + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDoctor_Service_CloseStatus()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Doctor List Service(18/05/2017) -----------------------------------------------------*/
        public DataSet Get_Dr_CRM_List_Report(string Sf_Code, string div_code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_Listed_Dr_List_Rrt '" + Sf_Code + "','" + div_code + "','" + Month + "','" + Year + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDoctor_Service_CloseStatus()");
            }
            return dsProduct;
        }

        /*---------------------------------------- Get MR Reporting Detail (10/05/2017)  --------------------------------------------*/

        public DataTable Get_MR_Report_Detail(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable DJW = new DataTable();

            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));

                strQry = "Select Sf_Code,'SELF' Sf_Name, Reporting_To_SF, sf_Designation_Short_Name,sf_type from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

                DataSet ds = db_ER.Exec_DataSet(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["Reporting_To_SF"] = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["sf_Designation_Short_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["sf_type"] = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dt.Rows.Add(dr);
                }
            }
            DataSet dsmgr = null;

            strQry = " Select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

            try
            {
                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "admin")
                    {
                        strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF,sf_Designation_Short_Name,sf_type  from Mas_Salesforce " + // SM Level
                                 " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                                 " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                 " and sf_code = '" + dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                        DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsMgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            order_id = order_id + 1;
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dr["Reporting_To_SF"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Designation_Short_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["sf_type"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            dt.Rows.Add(dr);

                            DJW = Get_MR_Report_Detail(div_code, dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        /*---------------------------------------- Get Trans Doctor Service Recevied (13/07/2017) ---------------------*/
        public DataSet Get_Dr_Service_CRM_Recevied_Admin(string Sf_Code, string div_code, string ModeType, string Month, string Year, string M_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_GetDrService_AdminApproval_CRM '" + Sf_Code + "','" + div_code + "','" + ModeType + "','" + Month + "','" + Year + "','" + M_Type + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Dr_Service_CRM_Recevied()");
            }
            return dsProduct;
        }

        /*---------------------------------------- Get Approval Status (17/07/2017) ---------------------*/
        public DataSet GetCRM_Servcie_ApprovalDet(string div_code, string DoctorCode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select H.Doctor_Code,L.ListedDr_Name,CONVERT(VARCHAR(10),Created_Date,103)+'-'+Service_Req+'-'+Service_Amt as ServiceName,Ser_Type " +
                     "Ser_Type,Reporting_To  from dbo.Trans_Doctor_Service_Head H inner join [dbo].[Mas_ListedDr] L on H.Doctor_Code=L.ListedDrCode where " +
                     " Doctor_Code='" + DoctorCode + "' and H.Sf_Code='" + Sf_Code + "' and  H.Division_Code='" + div_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetCRM_Servcie_ApprovalDet()");
            }
            return dsProduct;
        }

        /*---------------------------------------- Get Approval Status (20/07/2017) ---------------------*/
        public DataSet Get_Dr_RemoveNumbers(string div_code, string Service_Sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "SELECT dbo.DistinctList('" + Service_Sl_no + "',',') SerSlNo";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Dr_RemoveNumbers()");
            }
            return dsProduct;
        }


        /*------------------------------------------- Get Doctor Service Date(24/07/2017) -----------------------------------------------------*/
        public DataSet GetCRMDR(string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;


            strQry = "select Sf_Name,Sf_HQ,sf_Designation_Short_Name from [dbo].[Mas_Salesforce] where Sf_Code='" + Sf_Code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetCRMDR()");
            }
            return dsProduct;
        }

        /*---------------------------------------- Get DrService CRM Print (26/07/2017) ---------------------*/
        public DataSet GetDr_ServiceCRM_Print(string Sl_no, string div_code, string Listed_DrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_DrService_CRM_Print '" + Sl_no + "','" + div_code + "','" + Listed_DrCode + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDr_ServiceCRM_Print()");
            }
            return dsProduct;
        }


        public DataSet Get_Dr_Service_CRM_AppoveMRDet(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_CRM_Approve_MRDetail '" + Sf_Code + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Dr_Service_CRM_AppoveMRDet()");
            }
            return dsProduct;
        }


        /*-------------------------- Get TransSS Entry SecSaleValue (04-12-2017) -------------------------------------*/
        public DataSet Trans_SecSale_Value_timeStamp(string div_code, string head_sl_no, string DelNo)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = "select SS_Det_Sl_No,Sec_Sale_Code,CONVERT(bigint, ChgTimeStamp) as 'TimeStamp' from [dbo].[Trans_SS_Entry_Detail_Value] where" + " SS_Det_Sl_No in( select SS_Det_Sl_No from [dbo].[Trans_SS_Entry_Detail] where SS_Head_Sl_no" +
" in( select SS_Head_Sl_no from [dbo].[Trans_SS_Entry_Head] where SS_Head_Sl_no='" + head_sl_no + "')) and SS_Det_Sl_No='" + DelNo + "' order by SS_Sec_Sl_No";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "TransPrdRecodExist()");
            }

            return dsSale;
        }

        /*----------------------Get CRM Manager Status (19/12/2017)---------------------------------*/

        public DataSet Get_CrmMgr_Status(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "select Crm_Mgr,Crm_Approval from Setup_Others where Division_Code='" + div_code + "' and Crm_Mgr!=''  ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*----------------------Get CRM Approval Reporting (19/12/2017)---------------------------------*/

        public DataSet Get_ReportingTo_Status(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "select Reporting_To_SF from [dbo].[Mas_Salesforce] where Sf_Code='" + sf_code + "'";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*------------------------ Search Chemist Name (19/12/2017) --------------------------------------*/
        public DataSet getChemistList_Alphabet(string sfcode, string sAlpha, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, " +
            " t.territory_Name FROM Mas_Chemists d, Mas_Territory_Creation t " +
            " WHERE d.Sf_Code = '" + sfcode + "' and d.Division_Code='" + Div_Code + "'  " +
            " and d.Territory_Code = t.Territory_Code and  LEFT(d.Chemists_Name,1)='" + sAlpha + "' " +
            " and d.Chemists_Active_Flag = 0 order by d.Chemists_Name";
            try
            {
                dsChemist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemist;
        }

        /*------------------------------------------- Get Chemist Service Detail(19/12/2017) -----------------------------------------------------*/
        public DataSet GetChemist_Service_CloseStatus(string div_code, string Chemist_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;


            strQry = "select Sl_No,Chemists_Code,case Close_Service_Dr when 'NULL' then '0' " +
            " else Close_Service_Dr end as Close_Service_Dr, case Total_Business_Expect when '' then '0' " +
            " else Total_Business_Expect end as Total_Business_Expect,Ser_Type  from dbo.Trans_Doctor_Service_Head where " +
            " Chemists_Code='" + Chemist_Code + "' and Sf_Code='" + Sf_Code + "' and  Division_Code='" + div_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetChemist_Service_CloseStatus()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Search Chemist Name (19/12/2017) -----------------------------------------------------*/
        public DataSet Search_ChemistName(string sfcode, string Name, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, " +
                        " t.territory_Name FROM Mas_Chemists d, Mas_Territory_Creation t " +
                        "  Where d.Sf_Code='" + sfcode + "'  and d.Division_Code='" + Div_Code + "'" +
                        "  and d.Territory_Code = t.Territory_Code   and d.Chemists_Name like '" + Name + "%' " +
                        "  and d.Chemists_Active_Flag = 0 order by d.Chemists_Name";
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

        /*------------------------------------------- Get Chemist CRM Detail (19/12/2017)-----------------------------------------------------------*/
        public DataSet Get_Chemist_List_CRM(string sfcode, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone," +
            " t.territory_Name FROM " +
            " Mas_Chemists d, Mas_Territory_Creation t " +
            " WHERE d.Sf_Code = '" + sfcode + "' and d.Division_Code='" + Div_Code + "' and d.Territory_Code = t.Territory_Code" +
            " and d.Chemists_Active_Flag = 0 order by d.Chemists_Name";


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

        /*------------------------------------------- Bind Chemist Detail (19/12/2017) -----------------------------------------------------------*/
        public DataSet Bind_Chemist_Detail(string sfcode, string Div_Code, string Chemist_cd)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;



            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone," +
            " t.territory_Name FROM " +
            " Mas_Chemists d, Mas_Territory_Creation t " +
            " WHERE d.Sf_Code = '" + sfcode + "' and d.Division_Code='" + Div_Code + "' and d.Territory_Code = t.Territory_Code" +
            " and d.Chemists_Active_Flag = 0  and Chemists_Code='" + Chemist_cd + "' order by d.Chemists_Name";

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

        /*------------------------------------------- Trans Service Dr Head (19/12/2017) -----------------------------------------------------------*/

        public int Doctor_Service_Req_Test(string Doctor_Code, string Sf_Code, string Division_Code,
            string Type, string Location, string Hotel_Type, string No_Of_Person, string From_Date, string To_Date, string Arraival_Time, string Departure_Time, string Remarks,
            string Type_of_Travel, string Date, string From_Travel, string To_Travel, string No_of_Hrs_Km, string Name, string Age, string Sex, string Id_Proof,
            string Name_of_Books, string Author, string Edition, string Approx_Cost, string Name_of_Conference, string Type_Of_Participation, string Cost, string Cheque_Draft,
            string Payable_At, string Item_Descrp, string Rate, string Qty, string Service_Sl_No, string Trans_Sl_No, string Service_Req_No, string Other_II, string Other_Remark,
            string Chemist_Cd, string ModeType)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "EXEC SP_Doctor_ServiceReq_Add_Test '" + Doctor_Code + "','" + Sf_Code + "','" + Division_Code + "','" + Type + "'," +
                         "'" + Location + "','" + Hotel_Type + "','" + No_Of_Person + "','" + From_Date + "'," +
                         "'" + To_Date + "','" + Arraival_Time + "','" + Departure_Time + "','" + Remarks + "','" + Type_of_Travel + "','" + Date + "','" + From_Travel + "'," +
                         "'" + To_Travel + "','" + No_of_Hrs_Km + "','" + Name + "','" + Age + "','" + Sex + "','" + Id_Proof + "'," +
                         "'" + Name_of_Books + "','" + Author + "','" + Edition + "','" + Approx_Cost + "','" + Name_of_Conference + "','" + Type_Of_Participation + "'," +
                         "'" + Cost + "','" + Cheque_Draft + "','" + Payable_At + "','" + Item_Descrp + "','" + Rate + "','" + Qty + "','" + Service_Sl_No + "','" + Other_II + "'," +
                         "'" + Other_Remark + "','" + Chemist_Cd + "','" + ModeType + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        /*------------------------------------------- Get Chemist Detail(19/12/2017) -----------------------------------------------------*/
        public DataSet GetChemist_DetailService(string Sl_No, string ChemistCode, string Division_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_CRM_ChemistDetail '" + Division_Code + "','" + ChemistCode + "','" + Sf_Code + "','" + Sl_No + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(Division_Code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetChemist_DetailService()");
            }
            return dsProduct;
        }


        /*------------------------------------------- Trans Service Dr Head (19/12/2017) -----------------------------------------------------------*/

        public int Update_Trans_Service_DrHead_Test(string DoctorCode, string Sf_Code, string Financial_Year, string Trans_Month,
            string Trans_Year, string Ser_Amt_Till_Date, string Busi_Amt_till_date, string Tot_Busi_Expect,
            string ROI_Month, string Ser_Req, string Ser_Amt, string Spec_Amt_Remark, string Prescr_Chemesit_1,
            string Prescr_Chemesit_2, string Prescr_Chemesit_3, string Stockist_1, string Stockist_2,
            string Stockist_3, string div_Code, string T_Sl_No, string Sf_Name, string Sf_Mgr_1,
            string Sf_Mgr_2, string Sf_Mgr_3, string Sf_Mgr_4, string Sf_Mgr_5, string Service_Statement,
            string ApprovalStatus, string SF_ID, string Chemist_Cd, string ModeType, string BillType,
             string Reject_Reason, string Reject_SFCode)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();



                strQry = "EXEC SP_TransDrService_HeadUpdate_DrCRM '" + DoctorCode + "','" + Sf_Code + "','" + Financial_Year + "','" + Trans_Month + "'," +
                         "'" + Trans_Year + "','" + Ser_Amt_Till_Date + "','" + Busi_Amt_till_date + "','" + Tot_Busi_Expect + "'," +
                         "'" + ROI_Month + "','" + Ser_Req + "','" + Ser_Amt + "','" + Spec_Amt_Remark + "','" + Prescr_Chemesit_1 + "','" + Prescr_Chemesit_2 + "'," +
                         "'" + Prescr_Chemesit_3 + "','" + Stockist_1 + "','" + Stockist_2 + "','" + Stockist_3 + "','" + div_Code + "','" + T_Sl_No + "'," +
                         "'" + Sf_Name + "','" + Sf_Mgr_1 + "','" + Sf_Mgr_2 + "','" + Sf_Mgr_3 + "','" + Sf_Mgr_4 + "','" + Sf_Mgr_5 + "','" + Service_Statement + "'," +
                         "'" + ApprovalStatus + "','" + SF_ID + "','" + Chemist_Cd + "','" + ModeType + "','" + BillType + "','" + Reject_Reason + "','" + Reject_SFCode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        /*-------------------------- Get Hospital (20-12-2017) -------------------------------------*/
        public DataSet Get_Hospital(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "Exec SP_Get_Doctor_CRM_MR_List '" + sf_code + "','" + Div_Code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        /*-------------------------- Get Product (20-12-2017) -------------------------------------*/
        public DataSet Get_rpt_Product(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "Exec SP_Get_Doctor_CRM_Product_List '" + sf_code + "','" + Div_Code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        /*-------------------------- Get Doctor List (20-12-2017) -------------------------------------*/
        public DataSet Get_rpt_DoctorList(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "Exec SP_Get_Doctor_CRM_DoctorList_Test '" + sf_code + "','" + Div_Code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        /*------------------------------------------- Bind Chemist DDL (20/12/2017) -----------------------------------------------------------*/
        public DataSet Bind_Chemist_DDL(string sfcode, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC SP_Get_Chemist_CRM_DDL '" + sfcode + "','" + Div_Code + "'";

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

        /*------------------------------------------- Get Designation CRM(25/01/2018) -----------------------------------------------------*/
        public DataSet GetDesignation_Head_CRM(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;
            strQry = "select Designation_Short_Name from [dbo].[Mas_SF_Designation]  where Division_Code='" + div_code + "' and Manager_SNo!=0 order by Manager_SNo";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetDesignation_Head_CRM()");
            }
            return dsProduct;
        }

        #endregion

        #region Sec_Sale_SetUp

        /*-------------------------- Get Parameter Check (14-06-2017) -------------------------------------*/
        public DataSet GetClosingCheckParam(string Div_Code, string SecSaleCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select P.Sec_Sale_Code,P.Sec_Sale_Name,P.Sel_Sale_Operator,S.CalcF_Field  " +
            " from dbo.Mas_Sec_Sale_Setup S inner join [dbo].[Mas_Sec_Sale_Param] P " +
            " on S.Sec_Sale_Code=P.Sec_Sale_Code " +
            " where P.Division_Code = '" + Div_Code + "'  " +
            " AND P.Active=0 and S.CalcF_Field !='' and CalcF_Field is not null and S.Active=0 " +
            "  and S.Sec_Sale_Code='" + SecSaleCode + "' ORDER BY 1";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        /*-------------------------- Get Primary Bill Field (29-08-2017) -------------------------------------*/
        public DataSet Get_PrimaryBill_Field(string Div_Code, string SecSaleCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select P.Sec_Sale_Code,P.Sec_Sale_Name,P.Sel_Sale_Operator,S.Primary_Bill  " +
            " from dbo.Mas_Sec_Sale_Setup S inner join [dbo].[Mas_Sec_Sale_Param] P " +
            " on S.Sec_Sale_Code=P.Sec_Sale_Code " +
            " where P.Division_Code = '" + Div_Code + "'  " +
            " AND P.Active=0 and S.Primary_Bill !='' and Primary_Bill is not null and S.Active=0 " +
            "  and S.Sec_Sale_Code='" + SecSaleCode + "' ORDER BY 1";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_PrimaryBill_Field()");
            }
            return dsSale;
        }

        /*-------------------------- Primary Field  (29/08/2017) -------------------------------------*/
        public int Update_PrimaryBill(int Sec_Sale_Code, string Div_Code, string FieldParam)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.Mas_Sec_Sale_Setup set Primary_Bill='" + FieldParam + "' where Sec_Sale_Code='" + Sec_Sale_Code + "' and Division_Code='" + Div_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*-------------------------- ReActivate SS Parameter  (04/10/2016) -------------------------------------*/
        public DataSet Get_ReActivate_SS_Parameter_Plus(string opr, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select Sec_Sale_Code,Sec_Sale_Name from [dbo].[Mas_Sec_Sale_Param]" +
                             "where Sel_Sale_Operator='" + opr + "' " +
                             " AND Division_Code = '" + div_code + "' " +
                             " AND Active=1" +
                             " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        /*-------------------------- Select Field Parameter (27/10/2016) -------------------------------------*/
        public DataSet Get_Field_Parameter_Calc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select Sec_Sale_Code,Sec_Sale_Name from [dbo].[Mas_Sec_Sale_Param]" +
                              "where " +
                              "  Division_Code = '" + div_code + "' " +
                              " AND Active=0" +
                              " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        /*-------------------------- Get Primary Table Column (21-03-2017) -------------------------------------*/
        public DataSet Get_Primary_Field()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT name as ColumnName FROM sys.columns col WHERE object_id = OBJECT_ID('Trans_Primary_Detail')";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        /*-------------------------- Get Primary Bill Columns (29-08-2017) -------------------------------------*/
        public DataSet Get_Primary_Billwise_Field()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT name as ColumnName FROM sys.columns col WHERE object_id = OBJECT_ID('Primary_Bill') and col.name in ('Sale_Qty','Free_Qty','Return_Qty')";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Primary_Billwise_Field()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster(string opr, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                              " Sec_Sale_Name, " +
                              " Sec_Sale_Short_Name, " +
                              " Sec_Sale_Sub_Name, " +
                              " Sel_Sale_Operator " +
                       " FROM Mas_Sec_Sale_Param " +
                       " WHERE Sel_Sale_Operator = '" + opr + "' " +
                       " AND Division_Code = '" + div_code + "' " +
                       " AND Active=0" +
                       " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public DataSet getSaleSetup(int div_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            SqlParameter param_div_code = new SqlParameter();
            param_div_code.ParameterName = "@Division_Code";    // Defining Name
            param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

            SqlParameter param_Sec_Sale_Code = new SqlParameter();
            param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
            param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
            param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

            // Adding Parameter instances to sqlcommand
            comm.Parameters.Add(param_div_code);
            comm.Parameters.Add(param_Sec_Sale_Code);

            // Setting values of Parameter
            param_div_code.Value = div_code;
            param_Sec_Sale_Code.Value = sec_sale_code;

            strQry = " SELECT Display_Needed, " +
                           " Value_Needed, " +
                           " Carry_Fwd_Needed, " +
                           " Disable_Mode, " +
                           " Calc_Needed, " +
                           " Calc_Disable, " +
                           " Sale_Calc, " +
                           " Carry_Fwd_Field, " +
                           " Sub_Needed, " +
                           " Sub_Label, " +
                           " Order_by, " +
                           " Free_Needed, " +
                           " Sub_Label_1 " +
                    " FROM Mas_Sec_Sale_Setup " +
                    " WHERE Division_Code = @Division_Code " +
                    " AND Sec_Sale_Code = @Sec_Sale_Code and Active=0" +
                    " ORDER BY 1 ";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleSetup()");
            }
            return dsSale;
        }

        /*-------------------------- Add Secondary Sale Setup (29-08-2017) -------------------------------------*/
        public int Add_SecSale_SetUp(int div_code, int Sec_Sale_Code, int Display_Needed, int Value_Needed, int Carry_Fwd_Needed, int Disable_Mode, int Calc_Needed, int Calc_Disable,
        int Sale_Calc, int Carry_Fwd_Field, int Order_by, bool bRecordExist, int sub_needed, string sub_label, int Free_Needed, string Sub_Label1)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sec_Sale_Code = new SqlParameter();
                param_Sec_Sale_Code.ParameterName = "@Sec_Sale_Code";    // Defining Name
                param_Sec_Sale_Code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sec_Sale_Code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Display_Needed = new SqlParameter();
                param_Display_Needed.ParameterName = "@Display_Needed";    // Defining Name
                param_Display_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Display_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Value_Needed = new SqlParameter();
                param_Value_Needed.ParameterName = "@Value_Needed";    // Defining Name
                param_Value_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Value_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Needed = new SqlParameter();
                param_Carry_Fwd_Needed.ParameterName = "@Carry_Fwd_Needed";    // Defining Name
                param_Carry_Fwd_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Disable_Mode = new SqlParameter();
                param_Disable_Mode.ParameterName = "@Disable_Mode";    // Defining Name
                param_Disable_Mode.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Disable_Mode.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Needed = new SqlParameter();
                param_Calc_Needed.ParameterName = "@Calc_Needed";    // Defining Name
                param_Calc_Needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Calc_Disable = new SqlParameter();
                param_Calc_Disable.ParameterName = "@Calc_Disable";    // Defining Name
                param_Calc_Disable.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Calc_Disable.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Sale_Calc = new SqlParameter();
                param_Sale_Calc.ParameterName = "@Sale_Calc";    // Defining Name
                param_Sale_Calc.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Sale_Calc.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Carry_Fwd_Field = new SqlParameter();
                param_Carry_Fwd_Field.ParameterName = "@Carry_Fwd_Field";    // Defining Name
                param_Carry_Fwd_Field.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Carry_Fwd_Field.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_needed = new SqlParameter();
                param_sub_needed.ParameterName = "@Sub_Needed";    // Defining Name
                param_sub_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_sub_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_label = new SqlParameter();
                param_sub_label.ParameterName = "@Sub_Label";    // Defining Name
                param_sub_label.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sub_label.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Order_by = new SqlParameter();
                param_Order_by.ParameterName = "@Order_by";    // Defining Name
                param_Order_by.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Order_by.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Created_dt = new SqlParameter();
                param_Created_dt.ParameterName = "@Created_dt";    // Defining Name
                param_Created_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Created_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@Updated_dt";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Free_needed = new SqlParameter();
                param_Free_needed.ParameterName = "@Free_Needed";    // Defining Name
                param_Free_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_Free_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sub_label1 = new SqlParameter();
                param_sub_label1.ParameterName = "@Sub_Label1";    // Defining Name
                param_sub_label1.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_sub_label1.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand

                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_Sec_Sale_Code);
                comm.Parameters.Add(param_Display_Needed);
                comm.Parameters.Add(param_Value_Needed);
                comm.Parameters.Add(param_Carry_Fwd_Needed);
                comm.Parameters.Add(param_Disable_Mode);
                comm.Parameters.Add(param_Calc_Needed);
                comm.Parameters.Add(param_Calc_Disable);
                comm.Parameters.Add(param_Sale_Calc);
                comm.Parameters.Add(param_Carry_Fwd_Field);
                comm.Parameters.Add(param_sub_needed);
                comm.Parameters.Add(param_sub_label);
                comm.Parameters.Add(param_Order_by);
                comm.Parameters.Add(param_Created_dt);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_Free_needed);
                comm.Parameters.Add(param_sub_label1);

                // Setting values of Parameter
                param_div_code.Value = div_code;
                param_Sec_Sale_Code.Value = Sec_Sale_Code;
                param_Display_Needed.Value = Display_Needed;
                param_Value_Needed.Value = Value_Needed;
                param_Carry_Fwd_Needed.Value = Carry_Fwd_Needed;
                param_Disable_Mode.Value = Disable_Mode;
                param_Calc_Needed.Value = Calc_Needed;
                param_Calc_Disable.Value = Calc_Disable;
                param_Sale_Calc.Value = Sale_Calc;
                param_Carry_Fwd_Field.Value = Carry_Fwd_Field;
                param_sub_needed.Value = sub_needed;
                param_sub_label.Value = sub_label;
                param_Order_by.Value = Order_by;
                param_Created_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Free_needed.Value = Free_Needed;
                param_sub_label1.Value = Sub_Label1;

                if (!bRecordExist) //Creating Setup for the Division
                {
                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code,Sec_Sale_Code,Display_Needed,Value_Needed,Carry_Fwd_Needed,Disable_Mode,"
                        + " Calc_Needed, Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt,Active,Free_Needed,Sub_Label_1) VALUES "
                        + " ( @Division_Code, @Sec_Sale_Code, @Display_Needed, @Value_Needed, @Carry_Fwd_Needed, @Disable_Mode, "
                        + " @Calc_Needed , @Calc_Disable, @Sale_Calc, @Carry_Fwd_Field, @Sub_Needed, @Sub_Label, @Order_by, @Created_dt, @Updated_dt,0,@Free_Needed,@Sub_Label1)";
                }
                else //Update the Setup records for the division
                {
                    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                            " SET Display_Needed = @Display_Needed, " +
                            " Value_Needed = @Value_Needed, " +
                            " Carry_Fwd_Needed = @Carry_Fwd_Needed, " +
                            " Disable_Mode = @Disable_Mode, " +
                            " Calc_Needed = @Calc_Needed, " +
                            " Calc_Disable = @Calc_Disable, " +
                            " Sale_Calc = @Sale_Calc, " +
                            " Carry_Fwd_Field = @Carry_Fwd_Field, " +
                            " Sub_Needed = @Sub_Needed, " +
                            " Sub_Label = @Sub_Label, " +
                            " Order_by = @Order_by, " +
                            " Updated_dt = @Updated_dt, " +
                            " Free_Needed=@Free_Needed, " +
                            " Sub_Label_1=@Sub_Label1 " +
                            " WHERE Division_Code = @Division_Code " +
                            " AND Sec_Sale_Code = @Sec_Sale_Code ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(div_code, ex.Message.ToString().Trim(), "Sec Sales Setup", "Record Add()");
            }

            return iReturn;
        }

        public int ParamRecordUpdate(string div_code, string sale_name, string sale_code, string sale_Short_name)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();

                strQry = "UPDATE Mas_Sec_Sale_Param " +
                         " SET Sec_Sale_Name = '" + sale_name + "' , " +
                         "Sec_Sale_Short_Name = '" + sale_Short_name + "' , " +
                         " Update_dtm = getdate() " +
                         " WHERE Sec_Sale_Code = '" + sale_code + "' ";

                iReturn = db.ExecQry(strQry, comm);
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt16(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "ParamRecordAdd()");
            }

            return iReturn;
        }

        /*-------------------------- Deactivate Secondary Sale Add (29/09/2016) -------------------------------------*/
        public int DeActivate_SecondarySale(int Sec_Sale_Code, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (Get_SecSale_Param_Exists(Div_Code, Sec_Sale_Code))
                {
                    strQry = "UPDATE dbo.Mas_Sec_Sale_Param SET Active=1,Update_dtm = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "'";
                    iReturn = db.ExecQry(strQry);
                }
                if (Get_SecSale_Entry_Exists(Div_Code, Sec_Sale_Code))
                {
                    strQry = "UPDATE dbo.Mas_Sec_Sale_Setup SET Active=1,Updated_dt = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "'";
                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public bool sParamRecordExist(string div_code, string sec_sale_name)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter par_sec_sale_code = new SqlParameter();
                par_sec_sale_code.ParameterName = "@Par_Sec_Sale_Name";    // Defining Name
                par_sec_sale_code.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                par_sec_sale_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);
                sCommand.Parameters.Add(par_sec_sale_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);
                par_sec_sale_code.Value = sec_sale_name;

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Name = @Par_Sec_Sale_Name  and Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sParamRecordExist()");
            }
            return bRecordExist;
        }

        public int ParamRecordAdd(string div_code, string sale_name, string short_name, string opr)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                comm = new SqlCommand();
                strQry = "SELECT isnull(max(Sec_Sale_Code)+1,'1') Sec_Sale_Code from Mas_Sec_Sale_Param ";
                int Sec_Sale_Code = db.Exec_Scalar(strQry);


                strQry = "INSERT INTO Mas_Sec_Sale_Param (Division_Code,Sec_Sale_Name,Sec_Sale_Short_Name, "
                    + " Sec_Sale_Sub_Name,Sel_Sale_Operator, Update_dtm,Active) VALUES "
                    + " ( '" + div_code + "', '" + sale_name + "' , '" + short_name + "' , "
                    + " '" + short_name + "' , '" + opr + "' , getdate(),0)";

                iReturn = db.ExecQry(strQry, comm);
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt16(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "ParamRecordAdd()");
            }

            return iReturn;
        }

        /*-------------------------- Update SS Entry Parameter Plus (04/10/2016) -------------------------------------*/
        public int ReActivate_SSEntry_Parameter(int Sec_Sale_Code, string Div_Code, string Sec_Operator)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (Check_SS_Param_Exists(Div_Code, Sec_Sale_Code, Sec_Operator))
                {
                    strQry = "UPDATE dbo.Mas_Sec_Sale_Param SET Active=0,Update_dtm = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "' and Sel_Sale_Operator='" + Sec_Operator + "'";
                    iReturn = db.ExecQry(strQry);
                }
                if (Check_SS_Sale_Exists(Div_Code, Sec_Sale_Code))
                {
                    strQry = "UPDATE dbo.Mas_Sec_Sale_Setup SET Active=0,Updated_dt = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "'";
                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*-------------------------- Insert Field Parameter (27/10/2016) -------------------------------------*/
        public int FieldAdd_Parameter(int Sec_Sale_Code, string Div_Code, string FieldParam)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.Mas_Sec_Sale_Setup set CalcF_Field='" + FieldParam + "' where Sec_Sale_Code='" + Sec_Sale_Code + "' and Division_Code='" + Div_Code + "' ";

                //if (Check_SS_Param_Exists(Div_Code, Sec_Sale_Code, Sec_Operator))
                //{
                //    strQry = "UPDATE dbo.Mas_Sec_Sale_Param SET Active=0,Update_dtm = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "' and Sel_Sale_Operator='" + Sec_Operator + "'";
                iReturn = db.ExecQry(strQry);
                //}
                //if (Check_SS_Sale_Exists(Div_Code, Sec_Sale_Code))
                //{
                //    strQry = "UPDATE dbo.Mas_Sec_Sale_Setup SET Active=0,Updated_dt = GETDATE() WHERE Sec_Sale_Code = " + Sec_Sale_Code + " and Division_Code='" + Div_Code + "'";
                //    iReturn = db.ExecQry(strQry);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*--------------------------Check ReActivate  SS Entry Parameter Exists (04/10/2016) -------------------------------------*/
        public bool Check_SS_Param_Exists(string div_code, int sec_sale_Code, string Sec_Operator)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code ='" + div_code + "' " +
                         " AND Sec_Sale_Code ='" + sec_sale_Code + "' and Sel_Sale_Operator='" + Sec_Operator + "' and Active=1";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Check_SS_Param_Exists()");
            }
            return bRecordExist;
        }

        /*--------------------------Check ReActivate SS Entry Sale Exists (04/10/2016) -------------------------------------*/
        public bool Check_SS_Sale_Exists(string div_code, int sec_sale_Code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code ='" + div_code + "' " +
                         " AND Sec_Sale_Code ='" + sec_sale_Code + "' and Active=1";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Check_SS_Sale_Exists()");
            }
            return bRecordExist;
        }

        /*--------------------------Check DeActivate SS Entry Parameter Exists (04/10/2016) -------------------------------------*/
        public bool Get_SecSale_Param_Exists(string div_code, int sec_sale_Code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code ='" + div_code + "' " +
                         " AND Sec_Sale_Code ='" + sec_sale_Code + "' and Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_SecSale_Param_Exists()");
            }
            return bRecordExist;
        }

        /*--------------------------Check DeActivate SS Entry Sale Exists (04/10/2016) -------------------------------------*/
        public bool Get_SecSale_Entry_Exists(string div_code, int sec_sale_Code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Setup " +
                         " WHERE Division_Code ='" + div_code + "' " +
                         " AND Sec_Sale_Code ='" + sec_sale_Code + "' and  Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_SecSale_Entry_Exists()");
            }
            return bRecordExist;
        }

        /*-------------------------- Primary Field  (20/03/2017) -------------------------------------*/
        public int BindPrimary_Field(int Sec_Sale_Code, string Div_Code, string FieldParam)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.Mas_Sec_Sale_Setup set Primary_Field_Col='" + FieldParam + "' where Sec_Sale_Code='" + Sec_Sale_Code + "' and Division_Code='" + Div_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Sec_Sale_Code, " +
                            " Sec_Sale_Name, " +
                            " Sec_Sale_Short_Name, " +
                            " Sec_Sale_Sub_Name, " +
                            " Sel_Sale_Operator " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Sec_Sale_Sub_Name != 'Tot+' and Active=0 " +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public bool SetupRecordExist(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT COUNT(Division_Code) " +
                         " FROM mas_common_sec_sale_setup " +
                         " WHERE Division_Code  = @Par_Division_Code ";

                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "SetupRecordExists()");
            }
            return bRecordExist;
        }

        public int RecordAdd(string div_code, int total_needed, int value_needed, string calc_rate, int approval_needed, string Prod_Grp, bool bRecordExist, string Sale_F, string Closing_F)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_total_needed = new SqlParameter();
                param_total_needed.ParameterName = "@Total_Needed";    // Defining Name
                param_total_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_total_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_value_needed = new SqlParameter();
                param_value_needed.ParameterName = "@Value_Needed";    // Defining Name
                param_value_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_value_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_calc_rate = new SqlParameter();
                param_calc_rate.ParameterName = "@Calc_Rate";    // Defining Name
                param_calc_rate.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_calc_rate.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_approval_needed = new SqlParameter();
                param_approval_needed.ParameterName = "@Approval_Needed";    // Defining Name
                param_approval_needed.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_approval_needed.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_prod_grp = new SqlParameter();
                param_prod_grp.ParameterName = "@Prod_Grp";    // Defining Name
                param_prod_grp.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_prod_grp.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_created_dtm = new SqlParameter();
                param_created_dtm.ParameterName = "@created_dtm";    // Defining Name
                param_created_dtm.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_created_dtm.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 


                SqlParameter param_Sale_Field = new SqlParameter();
                param_Sale_Field.ParameterName = "@Sale_F";    // Defining Name
                param_Sale_Field.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Sale_Field.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Closing_Field = new SqlParameter();
                param_Closing_Field.ParameterName = "@Closing_F";    // Defining Name
                param_Closing_Field.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Closing_Field.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_total_needed);
                comm.Parameters.Add(param_value_needed);
                comm.Parameters.Add(param_calc_rate);
                comm.Parameters.Add(param_approval_needed);
                comm.Parameters.Add(param_prod_grp);
                comm.Parameters.Add(param_created_dtm);
                comm.Parameters.Add(param_Updated_dt);
                comm.Parameters.Add(param_Sale_Field);
                comm.Parameters.Add(param_Closing_Field);

                //Commented as the below code is no longer required on 01/31/16
                //if (value_needed == 1)
                //    value_needed = 0;
                //else if (value_needed == 0)
                //    value_needed = 1;

                // Setting values of Parameter
                param_div_code.Value = Convert.ToInt16(div_code);
                param_total_needed.Value = total_needed;
                param_value_needed.Value = value_needed;
                param_calc_rate.Value = calc_rate;
                param_approval_needed.Value = approval_needed;
                param_prod_grp.Value = Prod_Grp;
                param_created_dtm.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"); ;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                param_Sale_Field.Value = Sale_F;
                param_Closing_Field.Value = Closing_F;

                if (!bRecordExist) //Creating common secondary setup
                {
                    strQry = "INSERT INTO mas_common_sec_sale_setup (Division_Code, Total_Needed, Value_Needed, calc_rate, Approval_Needed, Prod_Grp, created_dtm, updated_dtm,Sale_Field,Closing_Field) VALUES "
                        + " ( @Division_Code, @Total_Needed, @Value_Needed, @Calc_Rate, @Approval_Needed, @Prod_Grp, @created_dtm, @updated_dtm,@Sale_F,@Closing_F)";
                }
                else //Update the common secondary setup for the division
                {
                    strQry = "UPDATE mas_common_sec_sale_setup " +
                           " SET Total_Needed = @Total_Needed, " +
                           " Value_Needed = @Value_Needed, " +
                           " calc_rate = @Calc_Rate, " +
                           " Approval_Needed = @Approval_Needed, " +
                           " Prod_Grp = @Prod_Grp, " +
                           " created_dtm = @created_dtm, " +
                           " updated_dtm = @updated_dtm, " +
                           " Sale_Field=@Sale_F, " +
                           " Closing_Field=@Closing_F " +
                           " WHERE Division_Code = @Division_Code ";
                }

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "Record Add()");
            }

            return iReturn;
        }

        public int RecordAdd_TotalValue_Needed(string div_code, int total_needed, int value_needed)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsSale = new DataSet();
                int sec_sale_code = -1;

                //if (total_needed == 1)
                //{
                //strQry = "SELECT MAX(sec_sale_code) + 1 FROM Mas_Sec_Sale_Param";
                //    int sec_sale_code = db.Exec_Scalar(strQry);

                //    strQry = "SELECT MAX(sl_no) + 1 FROM Mas_Sec_Sale_Setup";
                //    int setup_sl_no = db.Exec_Scalar(strQry);

                if (!isParamRecordExist_TotalField(div_code))
                {
                    strQry = "INSERT INTO Mas_Sec_Sale_Param " +
                        " (Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator, Is_Rpt_Field, Division_Code, Update_dtm,Active ) " +
                        " VALUES ('Total +', 'Tot+', 'Tot+', '+', NULL, '" + div_code + "', GETDATE(),0) ";
                    iReturn = db.ExecQry(strQry);
                }
                //else
                //{
                dsSale = Get_SecSaleCode_TotalField(div_code);
                if (dsSale != null)
                    sec_sale_code = Convert.ToInt32(dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                //}

                if (!sRecordExist(div_code, sec_sale_code))
                {
                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code, Sec_Sale_Code, Display_Needed, Value_Needed, Carry_Fwd_Needed, Disable_Mode, Calc_Needed, " +
                    " Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt,Active) " +
                    " VALUES('" + div_code + "', '" + sec_sale_code + "', '" + total_needed + "', '" + value_needed + "', 0, 1, 1, 1, 0, 0, 0, NULL, 9, getdate(), getdate(),0) ";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                                " SET Display_Needed = '" + total_needed + "', " +
                                " Value_Needed = '" + value_needed + "' " +
                                " WHERE division_code = '" + div_code + "' " +
                                " AND Sec_Sale_Code = '" + sec_sale_code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                //}

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "Record Add()");
            }

            return iReturn;
        }

        public int IsReportField(string div_code, int sec_sale_code, int Rpt_Field)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                comm = new SqlCommand();
                comm.Parameters.Clear();

                SqlParameter param_div_code = new SqlParameter();
                param_div_code.ParameterName = "@Division_Code";    // Defining Name
                param_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_sec_sale = new SqlParameter();
                param_sec_sale.ParameterName = "@sec_sale_code";    // Defining Name
                param_sec_sale.SqlDbType = SqlDbType.Int;           // Defining DataType
                param_sec_sale.Direction = ParameterDirection.Input;// Setting the direction 

                SqlParameter param_Updated_dt = new SqlParameter();
                param_Updated_dt.ParameterName = "@updated_dtm";    // Defining Name
                param_Updated_dt.SqlDbType = SqlDbType.VarChar;           // Defining DataType
                param_Updated_dt.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                comm.Parameters.Add(param_div_code);
                comm.Parameters.Add(param_sec_sale);
                comm.Parameters.Add(param_Updated_dt);

                // Setting values of Parameter
                param_div_code.Value = Convert.ToInt16(div_code);
                param_sec_sale.Value = sec_sale_code;
                param_Updated_dt.Value = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");


                strQry = "UPDATE Mas_Sec_Sale_Param " +
                        " SET is_rpt_field =" + Rpt_Field + ", " +
                        " update_dtm = @updated_dtm " +
                        " WHERE Division_Code = @Division_Code " +
                        " AND Sec_Sale_Code = @sec_sale_code ";

                iReturn = db.ExecQry(strQry, comm);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "IsReportField()");
            }

            return iReturn;
        }

        public DataSet getSaleMaster(string div_code, bool bIncludeEmptyRow)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            if (bIncludeEmptyRow)
            {
                strQry = " SELECT  -1 AS Sec_Sale_Code, '' Sec_Sale_Name, ''  Sec_Sale_Short_Name, '' Sec_Sale_Sub_Name, '' Sel_Sale_Operator " +
                         " UNION ALL " +
                         " SELECT  Sec_Sale_Code, " +
                                " Sec_Sale_Name, " +
                                " Sec_Sale_Short_Name, " +
                                " Sec_Sale_Sub_Name, " +
                                " Sel_Sale_Operator " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND Sec_Sale_Sub_Name != 'Tot+' " +
                         " AND Sec_Sale_Sub_Name != 'cust_col'  and Active=0 " +
                         " ORDER BY 1";
            }
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getSaleMaster()");
            }
            return dsSale;
        }

        public bool FormulaRecordExist(string div_code, string col_name)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Col_SNo) " +
                         " FROM Mas_Common_SS_Setup_Formula " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND Col_Name = '" + col_name + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup - Formula", "FormulaRecordExist()");
            }
            return bRecordExist;
        }

        public int Formula_RecordUpdate(string div_code, string col_sno, string col_name, string dis_mode, string order_by, string der_formula, string Calc_Mode)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                        " SET Col_Name = '" + col_name + "', " +
                        " Dis_Mode = '" + dis_mode + "', " +
                        " Order_By = '" + order_by + "', " +
                        " der_formula = '" + der_formula + "', " +
                        " CalculationMode='" + Calc_Mode + "'," +
                        " updated_dtm = getdate() " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND Col_SNo = '" + col_sno + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordUpdate()");
            }

            return iReturn;
        }

        public DataSet getSaleMaster_Det(string div_code, int secsale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator " +
                    " FROM Mas_Sec_Sale_Param " +
                    " WHERE Division_Code = '" + div_code + "' " +
                    " AND Sec_Sale_Code = " + secsale_code +
                     " AND Active = 0 " +
                    " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public DataSet getSaleMaster_Formula(string div_code, int col_sno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Col_Name, " +
                          " Dis_Mode, " +
                          " Order_By, " +
                          " Der_Formula,  " +
                          "CalculationMode," +
                          " SS_Entry_Done " +
                   " FROM Mas_Common_SS_Setup_Formula " +
                   " WHERE Division_Code = '" + div_code + "' " +
                   " AND Col_SNo = " + col_sno +
                   " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public int Formula_RecordDelete(string div_code, string col_sno)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                //if (!FormulaExists_Entry(div_code, Convert.ToInt32(col_sno)))
                //{
                if (FormulaExists_Param(div_code, Convert.ToInt32(col_sno)))
                {
                    strQry = "DELETE Mas_Sec_Sale_Setup " +
                          " WHERE Division_Code = '" + div_code + "' " +
                          " AND Sec_Sale_Code in " +
                          " ( " +
                          " SELECT Sec_Sale_Code FROM Mas_Sec_Sale_Param " +
                          " WHERE Division_Code = '" + div_code + "' " +
                          " AND Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                          " AND Cust_Col_SNo = '" + col_sno + "' and Active=0 " +
                          " ) ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "DELETE Mas_Sec_Sale_Param " +
                           " WHERE Division_Code = '" + div_code + "' " +
                           " AND Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                           " AND Cust_Col_SNo = '" + col_sno + "' and Active=0 ";

                    iReturn = db.ExecQry(strQry);

                }

                strQry = "DELETE Mas_Common_SS_Setup_Formula " +
                        " WHERE Division_Code = '" + div_code + "' " +
                        " AND Col_SNo = '" + col_sno + "' ";

                iReturn = db.ExecQry(strQry);
            }
            //}
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordDelete()");
            }

            return iReturn;
        }

        public DataSet getSaleMaster_Formula(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Col_SNo, " +
                             " Col_Name, " +
                             " Dis_Mode, " +
                             " Order_By, " +
                             " Der_Formula, " +
                             " CalculationMode " +
                      " FROM Mas_Common_SS_Setup_Formula " +
                      " WHERE Division_Code = '" + div_code + "' " +
                      " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getSaleMaster_Formula()");
            }
            return dsSale;
        }

        public int Formula_RecordAdd(string div_code, string col_name, string dis_mode, string order_by, string der_formula, bool bRecordExist, string Calc_Mode)
        {
            int iReturn = -1;
            DataSet dsSale = null;
            int sec_sale_code = -1;
            int dismode = 0;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                int col_sno = 0;

                if (!bRecordExist) //Creating common secondary setup
                {
                    strQry = "SELECT COUNT(Col_SNo) + 1 FROM Mas_Common_SS_Setup_Formula";
                    col_sno = db.Exec_Scalar(strQry);

                    if (col_sno > 1)
                    {
                        strQry = "SELECT MAX(Col_SNo) + 1 FROM Mas_Common_SS_Setup_Formula";
                        col_sno = db.Exec_Scalar(strQry);
                    }

                    strQry = "INSERT INTO Mas_Common_SS_Setup_Formula (Col_SNo, Col_Name, Division_Code, Dis_Mode, Order_By, Der_Formula, created_dtm, updated_dtm,CalculationMode) VALUES "
                       + " ( '" + col_sno + "', '" + col_name + "', '" + div_code + "', '" + dis_mode + "', '" + order_by + "', '" + der_formula + "', getdate(), getdate(),'" + Calc_Mode + "')";
                }
                //else //Update the common secondary setup for the division
                //{
                //    strQry = "UPDATE Mas_Common_SS_Setup_Formula " +
                //            " SET Col_Name = '" + col_name + "', " +
                //            " Dis_Mode = '" + dis_mode + "', " +
                //            " Order_By = '" + order_by + "', " +
                //            " der_formula = '" + der_formula + "', " +
                //            " updated_dtm = @updated_dtm " +
                //            " WHERE Division_Code = '" + div_code + "' " +
                //            " AND Col_SNo = '" + col_sno + "' ";
                //}

                iReturn = db.ExecQry(strQry);

                strQry = "INSERT INTO Mas_Sec_Sale_Param " +
                  " (Sec_Sale_Name, Sec_Sale_Short_Name, Sec_Sale_Sub_Name, Sel_Sale_Operator, Is_Rpt_Field, Cust_Col_SNo, Division_Code, Update_dtm,Active ) " +
                  " VALUES ('" + col_name + "', '" + col_name + "', 'cust_col', 'D', NULL, " + col_sno + ", '" + div_code + "', GETDATE(),0) ";

                iReturn = db.ExecQry(strQry);

                dsSale = Get_SecSaleCode_CustCol(div_code, col_sno);
                if (dsSale != null)
                    sec_sale_code = Convert.ToInt32(dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                if (!sRecordExist(div_code, sec_sale_code))
                {
                    if (dis_mode == "Y")
                        dismode = 1;

                    strQry = "INSERT INTO Mas_Sec_Sale_Setup (Division_Code, Sec_Sale_Code, Display_Needed, Value_Needed, Carry_Fwd_Needed, Disable_Mode, Calc_Needed, " +
                    " Calc_Disable, Sale_Calc, Carry_Fwd_Field, Sub_Needed, Sub_Label, Order_by, Created_dt, Updated_dt,Active) " +
                    " VALUES('" + div_code + "', '" + sec_sale_code + "', 1, 0, 0, '" + dismode + "', 1, 1, 0, 0, 0, NULL, '" + order_by + "', getdate(), getdate(),0) ";

                    iReturn = db.ExecQry(strQry);
                }
                //else
                //{
                //    strQry = "UPDATE Mas_Sec_Sale_Setup " +
                //                " SET Display_Needed = '" + total_needed + "', " +
                //                " Value_Needed = '" + value_needed + "' " +
                //                " WHERE division_code = '" + div_code + "' " +
                //                " AND Sec_Sale_Code = '" + sec_sale_code + "' ";

                //    iReturn = db.ExecQry(strQry);
                //}


            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Formula Setup", "Formula_RecordAdd()");
            }

            return iReturn;
        }

        public bool isParamRecordExist_TotalField(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                SqlParameter par_div_code = new SqlParameter();
                par_div_code.ParameterName = "@Par_Division_Code";    // Defining Name
                par_div_code.SqlDbType = SqlDbType.Int;           // Defining DataType
                par_div_code.Direction = ParameterDirection.Input;// Setting the direction 

                // Adding Parameter instances to sqlcommand
                sCommand.Parameters.Add(par_div_code);

                // Setting values of Parameter
                par_div_code.Value = Convert.ToInt16(div_code);

                strQry = " SELECT count(sec_sale_code) " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = @Par_Division_Code " +
                         " AND Sec_Sale_Sub_Name = 'Tot+' and Active=0";
                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return bRecordExist;
        }

        public bool OrderByExists(string div_code, string order_by)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Sl_No) from Mas_Sec_Sale_Setup where Order_by = '" + order_by + "' and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "OrderByExists()");
            }
            return bRecordExist;
        }

        public bool OrderByExists_Formula(string div_code, string order_by)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Col_SNo) from Mas_Common_SS_Setup_Formula where Order_by = '" + order_by + "' and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "OrderByExists_Formula()");
            }
            return bRecordExist;
        }

        public DataSet Get_SecSaleCode_CustCol(string div_code, int col_sno)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT sec_sale_code " +
                         " FROM Mas_Sec_Sale_Param " +
                         " WHERE Division_Code = '" + div_code + "' " +
                         " AND Cust_Col_SNo = '" + col_sno + "' " +
                         " AND Sec_Sale_Sub_Name = 'cust_col' and Active=0 ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExists()");
            }
            return ds;
        }

        public bool FormulaExists_Entry(string div_code, int Cust_Col_SNo)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Col_SNo) from Mas_Common_SS_Setup_Formula where SS_Entry_Done = 1 and Col_SNo = " + Cust_Col_SNo + " and Division_Code = '" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "FormulaExists_Entry()");
            }
            return bRecordExist;
        }

        public bool FormulaExists_Param(string div_code, int Cust_Col_SNo)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Sec_Sale_Code) from Mas_Sec_Sale_Param " +
                          " where Sec_Sale_Sub_Name = 'cust_col' and Sel_Sale_Operator='D' " +
                          " and Cust_Col_SNo = " + Cust_Col_SNo + " and Division_Code = '" + div_code + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Formula Setup", "FormulaExists_Param()");
            }
            return bRecordExist;
        }

        public DataSet getColSNo_Formula(string div_code, int sec_sale_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Cust_Col_SNo " +
                     " FROM Mas_Sec_Sale_Param " +
                     " WHERE Division_Code = '" + div_code + "' " +
                     " AND Sec_Sale_Code = " + sec_sale_code +
                     " and Active=0" +
                     " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup - Formula", "getColSNo_Formula()");
            }
            return dsSale;
        }

        public DataSet getAddionalSaleMaster(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Total_Needed, " +
                               " Value_Needed, " +
                               " calc_rate, " +
                               " Approval_Needed, " +
                               " Prod_Grp,Sale_Field,Closing_Field " +
                        " FROM mas_common_sec_sale_setup " +
                        " WHERE Division_Code = '" + div_code + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "getAddionalSaleMaster()");
            }
            return dsSale;
        }

        /*--------------------------Get MR_Status(06/03/2017) -------------------------------------*/
        public DataSet Get_Stockiest_MR_Status(string div_code, int imonth, int iyear, int stockiest_code, string Sub_Div, int lmonth, int lyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;


            strQry = "Exec [dbo].[SecSale_Status_Entry] '" + div_code + "','" + stockiest_code + "','" + imonth + "','" + iyear + "','" + lmonth + "','" + lyear + "','" + Sub_Div + "'";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Stockiest_MR_Status()");
            }

            return dsSale;
        }
        /*--------------------------Check TransHead Exists  (06/03/2017) -------------------------------------*/
        public bool sRecordExist_TransHead(string div_code, int imonth, int iyear, int stockiest_code, int status, string Sub_Div)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Exec [dbo].[SS_Entry_Head_Detail_Count] '" + div_code + "','" + imonth + "','" + iyear + "','" + stockiest_code + "','" + Sub_Div + "','" + status + "' ";


                int iRecordExist = db.Exec_Scalar(strQry, sCommand);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "sRecordExist_TransHead()");
            }
            return bRecordExist;
        }

        /*-------------------------- Get SS_Option_Edit_MR_Wise (15/12/2016) -------------------------------------*/
        public DataSet Get_SS_Option_Edit_MR_wise(string div_code, string sf_code, int istatus, string Sub_Div)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Exec [dbo].[SP_SecSale_Stockist_Edit_MR_Entry]  '" + div_code + "','" + sf_code + "','" + Sub_Div + "','" + istatus + "'";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        /*----------------------------------------- Get Parameter Detail (06/03/2017)  -----------------------------------------------------*/

        public DataSet Get_ParameterDetail(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();

            try
            {
                strQry = "EXEC SP_Get_ParameterDetail  '" + div_code + "'";

                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_ParameterDetail()");
            }

            return dsSale;
        }

        /*----------------------------------------- Get Product Detail (06/03/2017)  -----------------------------------------------------*/
        public DataSet GetProduct_Detail(string div_code, string state, string prod_grp, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);

            if ((prod_grp == "C") || (prod_grp == "G"))
            {
                strQry = " EXEC sp_ProdList_SecSales '" + div_code + "', '" + state + "', '" + prod_grp + "','" + subdiv + "' ";
            }
            else
            {
                strQry = " EXEC SP_Get_ProductDetail '" + div_code + "','" + state + "','" + subdiv + "'";
            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public DataSet GetProduct_DetailO(string div_code, string state, string prod_grp, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);

            if ((prod_grp == "C") || (prod_grp == "G"))
            {
                strQry = " EXEC sp_ProdList_SecSalesO '" + div_code + "', '" + state + "', '" + prod_grp + "','" + subdiv + "' ";
            }
            else
            {
                strQry = " EXEC SP_Get_ProductDetailO '" + div_code + "','" + state + "','" + subdiv + "'";
            }
            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }
        /*----------------------------------------- Get Trans SS Entry Detail Values (06/03/2017)  -----------------------------------------------------*/

        public DataSet Get_Trans_SS_EntryDelVal(string div_code, string cmonth, string cyear, string stock_code, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();
            try
            {
                strQry = "Exec [dbo].[SS_Entry_Head_Detail] '" + div_code + "','" + cmonth + "','" + cyear + "','" + stock_code + "','" + Sub_Div + "' ";

                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Trans_SS_EntryDelVal()");
            }

            return dsSale;
        }

        /*-------------------------- Get TransHead TimeStamp (04-12-2017) -------------------------------------*/
        public DataSet Get_TransHead_TimeStamp(string div_Code, string Month, string Year, string Stock_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " select CONVERT(bigint, ChgTimeStamp) as 'TimeStamp' from [dbo].[Trans_SS_Entry_Head] where Division_Code='" + div_Code + "' " +
                " and Month='" + Month + "' and Year='" + Year + "' and Stockiest_Code='" + Stock_Code + "' ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_Code), ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_TransHead_TimeStamp()");
            }
            return dsSale;


        }

        /*--------------------------Check If Trans Head Exists Or Not (06/03/2017) -------------------------------------*/
        public DataSet Get_Trans_Head_Code(string div_code, string imonth, string iyear, string lmonth, string lyear, string stock_code, string Sub_Div)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();
            try
            {
                strQry = "Exec [dbo].[SS_Entry_Head_Detail] '" + div_code + "','" + imonth + "','" + iyear + "','" + stock_code + "','" + Sub_Div + "' ";


                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Trans_Head_Code()");
            }

            return dsSale;


        }

        /*--------------------------Get Current MonthSS Entry (06/03/2017) -------------------------------------*/
        public DataTable Bind_Prd_Curt_Month(string div_code, string imonth, string iyear, string lmonth, string lyear, string stock_code, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSale = null;
            sCommand = new SqlCommand();
            try
            {

                strQry = "EXEC SP_Get_PreMonth_OpeningBal_FreeQty '" + div_code + "','" + imonth + "','" + iyear + "','" + lmonth + "','" + lyear + "','" + stock_code + "','" + Sub_Div + "'";


                dsSale = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Bind_Prd_Curt_Month()");
            }

            return dsSale;
        }

        /*--------------------------Get SS Entry (06/03/2017) -------------------------------------*/
        public DataTable BindPrdDelVal(string div_code, string imonth, string iyear, string lmonth, string lyear, string stock_code, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSale = null;
            sCommand = new SqlCommand();
            try
            {
                strQry = "EXEC SP_Get_Trans_CurrentMonth_Detail_FreeQty  '" + div_code + "','" + imonth + "','" + iyear + "','" + lmonth + "','" + lyear + "','" + stock_code + "','" + Sub_Div + "'";

                dsSale = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "BindPrdDelVal()");
            }

            return dsSale;
        }

        /*----------------------------------------- GetAllProductSecSaleDel Values (06/03/2017)  -----------------------------------------------------*/

        public DataSet GetAllProductSecSaleDel(string div_code, string imonth, string iyear, string lmonth, string lyear, string prod_code, string stock_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            sCommand = new SqlCommand();
            try
            {
                strQry = "EXEC Sp_GetAllProduct_SecSaleQty  '" + div_code + "','" + imonth + "','" + iyear + "','" + lmonth + "','" + lyear + "','" + prod_code + "','" + stock_code + "'";


                dsSale = db_ER.Exec_DataSet(strQry, sCommand);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "GetAllProductSecSaleDel()");
            }

            return dsSale;
        }

        /*-------------------------------------- Check Primary Field (04-12-2017) --------------------------------------------*/
        public bool GetPrimary_SaleField(string div_code)
        {
            sCommand = new SqlCommand();
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select count(*) from [dbo].[Mas_Sec_Sale_Setup] S " +
                           " inner join [dbo].[Mas_Sec_Sale_Param] P on " +
                           " S.Sec_Sale_Code=P.Sec_Sale_Code " +
                           " where S.Division_Code='" + div_code + "' and Primary_Field_Col !='NULL' " +
                           " and S.Active='0'  ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Additional Setup", "GetPrimary_SaleField()");
            }
            return bRecordExist;
        }

        /*-------------------------------------- Get Primary Field (04-12-2017) --------------------------------------------*/
        public DataTable GetPrimary_FieldValue(string div_code, string Tmonth, string Tyear, string stock_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSale = null;
            sCommand = new SqlCommand();
            try
            {
                strQry = "EXEC SP_Get_PrimarySale_Field  '" + div_code + "','" + Tmonth + "','" + Tyear + "','" + stock_code + "'";


                dsSale = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Bind_Prd_Curt_Month()");
            }

            return dsSale;
        }

        /*-------------------------- Get SecSale MGR (25/11/2016) -------------------------------------*/
        public DataSet GetSecSale_MGR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select Reporting_To_SF from Mas_Salesforce where Sf_Code='" + sf_code + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Sec Sales Setup", "Get_Field_Parameter_Calc()");
            }

            return dsSale;
        }

        /*-------------------------- Get TransSS Entry Head Detail ID (06/03/2017) -------------------------------------*/
        public DataSet GetTransHeadID(string div_code, string Stock_Code, string Month, string Year, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = "Exec [dbo].[SS_Entry_Head_Detail] '" + div_code + "','" + Month + "','" + Year + "','" + Stock_Code + "','" + Sub_Div + "' ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }

        /*-------------------------- Get TransSS Entry Detail ID (06/03/2017) -------------------------------------*/
        public DataSet TransPrdRecodExist(string div_code, string head_sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " SELECT SS_Det_Sl_No " +
                          " FROM Trans_SS_Entry_Detail " +
                          " WHERE Division_Code      = '" + div_code + "'" +
                          " AND SS_Head_Sl_No        ='" + head_sl_no + "'";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "TransPrdRecodExist()");
            }
            return dsSale;


        }

        public DataSet Get_SS_Stockiest_Details(string div_code, string sf_code, int imon, int iyr, string Sub_Div)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                // strQry = "select a.Stockiest_Code, b.Stockist_Name from Trans_SS_Entry_Head a, Mas_Stockist b where a.Stockiest_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and a.MONTH=" + imon + " and a.YEAR=" + iyr + " and a.status=2 ";

                // strQry = "EXEC [dbo].[SP_Secondary_Sale_Edit_Detail] '" + div_code + "','" + sf_code + "','" + imon + "','" + iyr + "'";

                strQry = "EXEC [dbo].[SP_Secondary_Sale_Edit_Detail_Test_1] '" + div_code + "','" + sf_code + "','" + imon + "','" + iyr + "','" + Sub_Div + "' ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }

        public int RecordUpdate(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string reject_by, int status, string Sub_Div)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                string sDate = iMonth.ToString() + "-01-" + iYear.ToString();
                DateTime dtEditDate = Convert.ToDateTime(sDate);

                strQry = "EXEC sp_ss_option_edit_Test_1  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + dtEditDate + "', '" + reject_by + "', 2, 4,'" + Sub_Div + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }


        #endregion

        /*------------------------------------------- Update Service Detail (26-10-2017) ---------------*/
        public int Update_Trans_Service_ConfirmAdmin(string Division_Code, string Sf_Type, string Sf_Code, string Dr_Code, string ModeType)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Trans_Dr_Service_Head_Update_Confirm '" + Division_Code + "','" + Sf_Type + "','" + Sf_Code + "','" + Dr_Code + "','" + ModeType + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet Get_top_SSale(string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "exec SecSale_Analysis_Stk_Graph_Least  '" + div_code + "','" + sf_code + "', " + iMonth + ", " + iYear + " ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet Get_tot_SSale(string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "exec Review_SS_Tot  '" + div_code + "','" + sf_code + "', " + iMonth + ", " + iYear + " ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet Primary_SSale_Head(string div_code, string sf_code, int iMonth, int iYear, string stk_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Trans_Sl_No from Trans_Primary_Head where division_code='" + div_code + "' and trans_month=" + iMonth + " and trans_year=" + iYear + " " +

                      "and Stockist_ERP_Code in (" + stk_code + ")";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet Primary_Tot_Amt(string div_code, string Head_Slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select sum(CONVERT(float,Sale_Qty)*CONVERT(float,Rate)) Amount from  Trans_Primary_Detail where " +
                     " Trans_Sl_NO in (" + Head_Slno + ")  and division_code='" + div_code + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet Target_Sale_FF(string Div_code, string sfcode, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " exec Target_Sale_Graph_FF '" + Div_code + "','" + sfcode + "'," + imonth + "," + iyear + " ";
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


        public DataSet GetStock_Detail_Entry(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "SELECT ROW_NUMBER() OVER(ORDER BY Stockist_Name ASC) AS Sl_No,Stockist_Code,Stockist_Name, " +
            "Stockist_Address,Stockist_Designation,Stockist_Mobile, " +
            "case Territory when '--Select--' then '' else Territory end as Territory, " +
            "case State when '---Select---' then '' else State end as State " +
            " FROM mas_stockist " +
            " WHERE stockist_active_flag=0  " +
            " AND Division_Code= '" + div_code + "'  AND Sf_Code like '%" + sf_code + "%' " +
            " order by Stockist_Name";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public int DeActivate_Stockist_Detail(string Division_Code, string Sf_Code, string StockCode)
        {
            int iReturn = 1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SP_Stockist_MR_BulkDeactivation '" + Division_Code + "','" + Sf_Code + "','" + StockCode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        #region SS_Rpt

        /*------------------------------------------- Get Secsale Stockist Entry Status (20/04/2018) ---------------*/
        public DataSet Get_Secsale_Stockist_Entry(string div_code, string Sf_Code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SS_Entry_Status_MRwise_Detail_Rpt] '" + div_code + "','" + Sf_Code + "','" + Month + "','" + Year + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_QuizTimeLimit()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get All HQ (03/05/2018) -----------------------------------------------------*/
        public DataSet GetHQ_AllDetail(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SP_SecSale_All_HQ] '" + div_code + "','" + sf_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetHQ_AllDetail()");
            }
            return dsProduct;
        }


        /*------------------------------------------- Get SS Parameter (24/05/2018) -----------------------------------------------------*/
        public DataSet Get_SS_Parameter(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "select ROW_NUMBER() OVER (ORDER BY Sec_Sale_Code)SNO, Sec_Sale_Code,Sec_Sale_Name from Mas_Sec_Sale_Param " +
            " where division_code='" + div_code + "' and is_Rpt_field=0  and Active=0";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_SS_Parameter()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get sale Closing Field (24/05/2018) -----------------------------------------------------*/
        public DataSet GetSale_Close_Param(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "SELECT  ROW_NUMBER() OVER (ORDER BY Item)SNO,CAST(Item AS VARCHAR(max)) as Sec_Sale_Code FROM dbo.SplitString( " +
    " (select sale_field+closing_field FROM Mas_Common_Sec_Sale_Setup WHERE division_code='" + div_code + "'), ',')where Item !=''";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_SS_Parameter()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Stockist State Detail (31/05/2018) -----------------------------------------------------*/
        public DataSet Get_Stockist_State(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SP_SecSale_All_State] '" + div_code + "','" + sf_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Stockist_State()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get All Stockist (23/06/2018) -----------------------------------------------------*/
        public DataSet GetStockist_AllDetail(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SecSale_All_Stockist] '" + div_code + "','" + sf_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "GetHQ_AllDetail()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get All Stockist (23/07/2018) -----------------------------------------------------*/
        public DataSet Get_All_Stockist_DropDown(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SecSale_All_Stockist_DroDown] '" + div_code + "','" + sf_code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_All_Stockist_DropDown()");
            }
            return dsProduct;
        }

        #endregion

        #region Quiz
        /*------------------------------------------- Get Question Ans Option CRM(05/02/2018) -----------------------------------------------------*/
        public DataSet Get_Question_AnsOption(string div_code, string Survey_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec SP_Get_QuestionAnswer_Option '" + div_code + "'," + Survey_Id + "";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Question_Ans()");
            }
            return dsProduct;
        }

        /*------------------------------------------- Get Quiz TimeLimit (04/04/2018) ---------------*/
        public DataSet Get_QuizTimeLimit(string div_code, string Survey_Id, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SP_Get_Quiz_TimeLimit] '" + div_code + "','" + Survey_Id + "','" + Sf_Code + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_QuizTimeLimit()");
            }
            return dsProduct;
        }
        #endregion

        /*------------------------------------------- Secondary Sales Entry Status (20/11/2018) ---------------*/
        public DataSet SecSaleFilledByStatus(string div_code, string sf_code, int FMonth, int FYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec [dbo].[SecSale_FilledByStatus] '" + sf_code + "', '" + div_code + "','" + FMonth + "','" + FYear + "'";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Secondary Sales Entry Status", "SecSaleFilledByStatus()");
            }
            return dsProduct;
        }

        public DataSet get_product_ddl(string sf_code, string div_code, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;



            strQry = " select row_number() over (order by (select 1)) sno, Product_Code_SlNo,Product_Detail_Name, Product_Sale_Unit as Pack,'' saleqty,'' Foc_qty,'' rate,'' amt from Mas_Product_Detail where Division_Code='" + div_code + "' and subdivision_code like '%" + subdiv + "%' " +
                    " and Product_Active_Flag=0  order by Product_Detail_Name";




            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }


        public DataSet GetStockist_order(string sf_code, string div_code, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (mode == "4")
            {

                strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
                         " from Mas_Stockist " +
                         " where Stockist_Active_Flag=0 " +
                         " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' and Stockist_Name not like 'Direct%' " +
                         " ORDER BY 2";
            }
            else if (mode == "1")
            {
                strQry = "select -1 Stockist_Code, '---Select---' Stockist_Name UNION " +
                        " select chemists_code as Stockist_Code, chemists_name+' - '+b.territory_name as Stockist_Name from mas_chemists a,mas_territory_creation b where a.sf_code='" + sf_code + "' and a.Division_code = '" + div_code + "' and chemists_active_flag=0 and a.territory_code=b.territory_code and a.sf_code=b.sf_code";
            }
            else if (mode == "3")
            {
                strQry = "select -1 Stockist_Code, '---Select---' Stockist_Name UNION " +
                        " select ListedDrCode as Stockist_Code,ListedDr_Name+' - '+b.territory_name as Stockist_Name from mas_listeddr a,mas_territory_creation b where a.sf_code='" + sf_code + "' and a.Division_code = '" + div_code + "' and ListedDr_Active_Flag=0 and a.territory_code=b.territory_code and a.sf_code=b.sf_code ";
            }

            else if (mode == "2")
            {
                strQry = "select -1 Stockist_Code, '---Select---' Stockist_Name UNION " +
                        " select Hospital_Code as Stockist_Code,Hospital_Name+' - '+b.territory_name as Stockist_Name from mas_hospital a,mas_territory_creation b where a.sf_code='" + sf_code + "' and a.Division_code = '" + div_code + "' and Hospital_Active_Flag=0 and a.territory_code=b.territory_code and a.sf_code=b.sf_code";
            }


            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet get_product_list(string sf_code, string div_code, string subdiv, int Trans_Slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;



            //strQry = " select row_number() over (order by (select 1)) sno, Product_Code_SlNo,Product_Detail_Name, Product_Sale_Unit as Pack,'' saleqty,'' Foc_qty,'' rate,'' amt from Mas_Product_Detail where Division_Code='" + div_code + "' and subdivision_code like '%" + subdiv + "%' " +
            //        " and Product_Active_Flag=0  order by Product_Detail_Name";

            //strQry = " select row_number() over (order by (select 1)) sno, Product_Code_SlNo,Product_Detail_Name, " +
            //       " Product_Sale_Unit as Pack,'' saleqty,'' Foc_qty,'' rate,'' amt  from Mas_Product_Detail where Division_Code='0' ";

            strQry = "select row_number() over (order by (select 1)) sno,Product_Code as Product_Code_SlNo,Product_Name as Product_Detail_Name," +
                    " Pack,Order_Sal_Qty as saleqty,Order_Free_Qty as Foc_qty,Order_Rate as rate, Order_Value as amt,NRV_Value,TotNet_Amt  from trans_order_book_detail where Trans_Slno='" + Trans_Slno + "' " +
                    " and sf_code='" + sf_code + "' and division_code='" + div_code + "'";


            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }



        public DataSet Get_Chck_dobl_insrt(string DoctorCode, string div_code, string Sf_Code, string Service_Amt, string FinancialYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select Sl_No from Trans_Doctor_Service_Head  where Sf_Code='" + Sf_Code + "' and Doctor_Code='" + DoctorCode + "'  and Division_Code='" + div_code + "' and Service_Amt='" + Service_Amt + "' and Financial_Year='" + FinancialYear + "' and convert(varchar,Created_Date,103)= convert(varchar,getdate(),103)  ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }




        //------------------------------------------------------



        public DataSet Get_servAgnst_Chck_dobl_insrt(string DoctorCode, string div_code, string Sf_Code, string Trans_Month, string Trans_Year, string Service_No, string PrdTotal)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select Sl_No from  Trans_Service_Business_Against_Head_Detail where Doctor_Code='" + DoctorCode + "' and Sf_Code='" + Sf_Code + "' and Trans_Month='" + Trans_Month + "' and Trans_Year='" + Trans_Year + "' and Service_No='" + Service_No + "' and Division_Code='" + div_code + "' and Product_Total='" + PrdTotal + "' and convert(varchar,Created_Date,103)= convert(varchar,getdate(),103) ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSale;
        }

        #region Stockistwise_Primary_Sales
        public DataSet Get_Stockist_Primay_ProductDetail(string div_code, string state, string subdiv, string sf_code, string stck_code, string mnth, string yr, string in_no, string in_date, string ord_no, string ord_date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            if (subdiv.Contains(','))
                subdiv = subdiv.Substring(0, subdiv.Length - 1);


            strQry = " EXEC sp_Stockist_Primay_ProductDetail '" + div_code + "','" + state + "','" + subdiv + "','" + sf_code + "','" + stck_code + "','" + mnth + "','" + yr + "','" + in_no + "','" + in_date + "','" + ord_no + "','" + ord_date + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "getProduct()");
            }
            return dsProduct;
        }

        public int Stockist_Primary_Sales_Update(string div_code, string sf_Code, string stck_code, string mnth, string yr, string in_no, string in_date, string ord_no, string ord_date, string state_code, string sub_code, string objPS_Detail)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC sp_Trans_Stockist_Primary_Entry '" + div_code + "','" + sf_Code + "', '" + stck_code + "', '" + mnth + "', '" + yr + "', '" + in_no + "', '" + in_date + "', '" + ord_no + "', '" + ord_date + "', '" + state_code + "', '" + sub_code + "',  '" + objPS_Detail + "'";

            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public DataSet Stockist_Primary_Head(string div_code, string sf_code, string stck_code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "SELECT Inv_No,CAST(Inv_Dt as date)Inv_Dt,Ord_No,CAST(Ord_Dt as date)Ord_Dt FROM Trans_Stockist_Inv_Head " +
                        " WHERE Sf_Code='" + sf_code + "' AND Stockist_Code='" + stck_code + "' AND Division_Code='" + div_code + "' " +
                        " AND Month='" + Month + "' AND Year='" + Year + "' Order by Entry_Date desc";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Secondary Sales Entry Status", "SecSaleFilledByStatus()");
            }
            return dsProduct;
        }
        #endregion
        public DataSet GetSLNO_SS(string div_code, string Stock_Code, string Month, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No,Approve_Flag FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            strQry = " SELECT  Sl_No,Approve_Flag from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                    " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                    " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Trans_Month = '" + Month + "' AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                    " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                    " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }

        public DataSet getSS_App_Pending(string div_code, string sf_code, int istatus)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT DISTINCT a.sf_code, a.sf_name, a.Sf_HQ, a.sf_Designation_Short_Name, b.Stockist_Code,s.Stockist_Name, " +
                     " b.Trans_Month , b.Trans_Year , " +
                     "'Click here for the month - ' + cast(DateName( month , DateAdd( month , b.Trans_Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Trans_Year) as Month,Approve_Flag,Stockist_Designation  " +
                     " FROM Mas_Salesforce a, Trans_Secondary_Entry_Head b, Mas_Salesforce_AM c ,mas_stockist s  " +
                     " WHERE a.sf_code = b.sf_code and s.Stockist_Code=b.Stockist_Code  " +
                     " AND a.sf_code =  c.sf_code and  c.Division_Code='" + div_code + "'   AND c.SS_AM in ('" + sf_code + "')    AND b.Approve_Flag= " + istatus + " ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_Prev(string div_code, string Stock_Code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = "select Sl_No FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
                     " Stockist_Code='" + Stock_Code + "' and sf_code='" + sf_code + "' ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet GetSLNO_Reject_Sub(string div_code, string Stock_Code, string Month, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "' and Approve_Flag=2 and sf_code='"+sf_code+"'";

            strQry = " SELECT  Sl_No from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                  " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                  " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Trans_Month = '" + Month + "' AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                  " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                  " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public int GetBillSlNO()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Trans_Secondary_Entry_BillDetails ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet GetSLNO_Bifur(string div_code, string Stock_Code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = "select Trans_Sl_No,Approve_Flag FROM Trans_SS_Bifurcate_Head where Division_Code='" + div_code + "' and  " +
                     " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet GetSLNO_Bifur_Sub(string div_code, string Stock_Code, string Month, string Year, string Sub_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Trans_Sl_No,Approve_Flag FROM Trans_SS_Bifurcate_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";
            strQry = " SELECT  Trans_Sl_No,Approve_Flag from(SELECT  Trans_Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                " FROM Trans_SS_Bifurcate_Head  WHERE  Division_Code='" + div_code + "' AND Trans_Month = '" + Month + "' AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + Sub_Code + "', ',') where Item!='') ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet getBifur_App_Pending(string div_code, string sf_code, int istatus)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT DISTINCT a.sf_code, a.sf_name, a.Sf_HQ, a.sf_Designation_Short_Name, b.Stockist_Code,s.Stockist_Name, " +
                     "  b.Trans_Month , b.Trans_Year , " +
                     " 'Click here for the month - ' + cast(DateName( month , DateAdd( month , b.Trans_Month , 0 ) - 1 ) as varchar) + ' ' + convert(char(4), b.Trans_Year) as Month,Approve_Flag,Stockist_Designation  " +
                     "  FROM Mas_Salesforce a, Trans_SS_Bifurcate_Head b, Mas_Salesforce_AM c ,mas_stockist s " +
                     "  WHERE a.sf_code = b.sf_code and s.Stockist_Code=b.Stockist_Code " +
                     "  AND a.sf_code = c.sf_code and  c.Division_Code='" + div_code + "' " +
                     "  AND b.sf_code in (" + sf_code + ")  " +
                     "  AND b.Approve_Flag= " + istatus + " ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_Qty(string div_code, string Stock_Code, string Month, string Year, string sf_code, string Product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " select Bifurcate_Qty from Trans_SS_Bifurcate_Head D, Trans_SS_Bifurcate_Detail H where product_code='" + Product_code + "' and H.sf_code='" + sf_code + "' " +
                     " and Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "' and D.Trans_Sl_No=H.Trans_Sl_No and D.Division_Code= '" + div_code + "'";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet GetSLNO_Bifur_Reject(string div_code, string Stock_Code, string Month, string Year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = "select Trans_Sl_No FROM Trans_SS_Bifurcate_Head where Division_Code='" + div_code + "' and  " +
                     " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "' and Approve_Flag=2 and sf_code='" + sf_code + "'";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_SS_Edit(string div_code, string sf_code, int imon, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select a.Stockist_Code, b.Stockist_Name from Trans_Secondary_Entry_Head a, Mas_Stockist b where a.Stockist_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and a.Trans_Month=" + imon + " and a.Trans_Year=" + iyr + "  ";
                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public DataSet Get_Bifur_Edit(string div_code, string sf_code, int imon, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select a.Stockist_Code, b.Stockist_Name from Trans_SS_Bifurcate_Head a, Mas_Stockist b where a.Stockist_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and a.Trans_Month=" + imon + " and a.Trans_Year=" + iyr + "  ";
                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public DataSet Get_Edit_SlNo(string div_code, string sf_code, string stk_code, int imon, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select Sl_No from  Trans_Secondary_Entry_Head WHERE Division_Code = '" + div_code + "' AND SF_Code = '" + sf_code + "' AND Stockist_Code = '" + stk_code + "'   " +
                         " AND  Trans_Month >=" + imon + " and Trans_Year >=" + iyr + "  ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public int SS_Delete(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, DataTable dtSlNo, string App_by, string mode)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SS_Entry_Delete  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + iMonth + "','" + iYear + "','" + dtSlNo.Rows[0] + "' ,'" + App_by + "','" + mode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }
        public DataSet Get_Edit_Bifur_SlNo(string div_code, string sf_code, string stk_code, int imon, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select Trans_Sl_No from  Trans_SS_Bifurcate_Head WHERE Division_Code = '" + div_code + "' AND SF_Code = '" + sf_code + "' AND Stockist_Code = '" + stk_code + "'   " +
                         " AND  Trans_Month =" + imon + " and Trans_Year =" + iyr + " ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public int SS_Bifur_Delete(string div_code, string sf_code, int stockiest_code, int iMonth, int iYear, string Trans_SlNo, string App_by, string mode)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SS_Bifur_Entry_Delete  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + iMonth + "','" + iYear + "','" + Trans_SlNo + "' ,'" + App_by + "','" + mode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }

        public DataSet GetSLNO_SS_prev(string div_code, string Stock_Code, string Month, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;



            strQry = " SELECT  Sl_No,Approve_Flag from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                    " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                    " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Trans_Month <= '" + Month + "' AND Trans_Year <= '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "' and Approve_Flag=2  )t " +
                    " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                    " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Reject_Reason(string div_code, string Stock_Code, string Month, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;



            strQry = " SELECT  Stockist_Code,Reject_Reason,Trans_Month,Trans_Year from(SELECT  Stockist_Code,Reject_Reason,Trans_Month,Trans_Year,CAST('<XMLRoot><RowData>' +  " +
                    " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                    " FROM Trans_SSEntry_Reject  WHERE  Division_Code='" + div_code + "' AND Trans_Month = '" + Month + "' AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'   )t " +
                    " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                    " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Month_Record(string div_code, string Stock_Code, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No,Approve_Flag FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            strQry = " SELECT  Sl_No,Approve_Flag from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                    " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                    " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                    " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                    " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }

        public DataSet Get_Month_Max(string div_code, string Stock_Code, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No,Approve_Flag FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            strQry = " SELECT MAX(cast(Trans_Month as int))+ 1  Trans_Month,Trans_Year from(SELECT  Trans_Month,Trans_Year,CAST('<XMLRoot><RowData>' +  " +
                     " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x  " +
                     " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t  " +
                     " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                     " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') " +
                     " where Item!='')  " +
                     " group by Trans_Year ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Base_Entry(string div_code, string Stock_Code, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;


            strQry = " SELECT  Sl_No,Approve_Flag from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                    " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                    " FROM Trans_Base_Entry  WHERE  Division_Code='" + div_code + "' AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                    " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                    " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet getBase_App_Pending(string div_code, string sf_code, int istatus)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT DISTINCT a.sf_code, a.sf_name, a.Sf_HQ, a.sf_Designation_Short_Name, b.Stockist_Code,s.Stockist_Name, " +
                     "  b.Trans_Year , " +
                     " 'Click here for the Year -  ' + convert(char(4), b.Trans_Year) as Month,Approve_Flag,Stockist_Designation  " +
                     "  FROM Mas_Salesforce a, Trans_Base_Entry b, Mas_Salesforce_AM c ,mas_stockist s " +
                     "  WHERE a.sf_code = b.sf_code and s.Stockist_Code=b.Stockist_Code " +
                     "  AND a.sf_code = c.sf_code and  c.Division_Code='" + div_code + "' " +
                     "  AND b.sf_code in (" + sf_code + ")  " +
                     "  AND b.Approve_Flag= " + istatus + " ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet GetBase_Reject(string div_code, string Stock_Code, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "' and Approve_Flag=2 and sf_code='"+sf_code+"'";

            strQry = " SELECT distinct Sl_No from(SELECT  Sl_No,Approve_Flag,CAST('<XMLRoot><RowData>' +  " +
                  " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                  " FROM Trans_Base_Entry  WHERE  Division_Code='" + div_code + "'  AND Trans_Year = '" + Year + "' AND  Stockist_Code  = '" + Stock_Code + "'  )t " +
                  " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                  " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Base_Edit(string div_code, string sf_code, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select distinct a.Stockist_Code, b.Stockist_Name from Trans_Base_Entry a, Mas_Stockist b where a.Stockist_Code = b.Stockist_Code and a.sf_code='" + sf_code + "' and a.Division_Code='" + div_code + "' and  a.Trans_Year=" + iyr + "  ";
                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public DataSet Get_Edit_Base_SlNo(string div_code, string sf_code, string stk_code, int iyr)
        {
            DataSet ds = new DataSet();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " select distinct Sl_No from  Trans_Base_Entry WHERE Division_Code = '" + div_code + "' AND SF_Code = '" + sf_code + "' AND Stockist_Code = '" + stk_code + "'   " +
                         " and Trans_Year =" + iyr + " ";

                ds = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Edit", "Get_SS_Stockiest_Details()");
            }
            return ds;
        }
        public int SS_Base_Delete(string div_code, string sf_code, int stockiest_code, int iYear, string Trans_SlNo, string App_by, string mode)
        {
            int iReturn = -1;

            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC SS_Base_Delete  '" + div_code + "', '" + sf_code + "', " + stockiest_code + ", '" + iYear + "','" + Trans_SlNo + "' ,'" + App_by + "','" + mode + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup - Option Edit", "Record Update()");
            }

            return iReturn;
        }
        public DataSet getStockiest_Primary(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name " +
                     " UNION " +
                     " select Stockist_Code,Stockist_Name " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataSet Get_Statewise_StockistDet_ALL(string div_code, string Month, string Year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec GetStatewise_StockistList_MR '" + div_code + "','" + Month + "','" + Year + "','" + sf_code + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Statewise_StockistDet()");
            }
            return dsProduct;
        }
        public DataSet Get_Statewise_Stock_MR_Det_SS_ALL(string div_code, string StateCode, string Month, string Year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec GetStockist_Statewise_SS_MR '" + div_code + "','" + StateCode + "','" + Month + "','" + Year + "','" + sf_code + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Statewise_Stock_MR_Det()");
            }
            return dsProduct;
        }
        public DataSet Get_Statewise_Stock_MR_Det_ALL(string div_code, string StateCode, string Month, string Year, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsProduct = null;

            strQry = "Exec GetStockist_Statewise_MRList_Pending '" + div_code + "','" + StateCode + "','" + Month + "','" + Year + "','" + sf_code + "' ";

            try
            {
                dsProduct = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Transaction", "Get_Statewise_Stock_MR_Det()");
            }
            return dsProduct;
        }
        public DataSet Get_Prev_St(string div_code, string Stock_Code, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " SELECT  Sl_No from(SELECT  Sl_No,CAST('<XMLRoot><RowData>' +  " +
                  " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                  " FROM Trans_Secondary_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Stockist_Code  = '" + Stock_Code + "'  )t " +
                  " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                  " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_SS_Primary_Bill(string div_code, string Stock_Code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No,Approve_Flag FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            strQry = " SELECT  SS_Head_Sl_No,Status from " +
                    "  Trans_SS_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Month = '" + Month + "' AND Year = '" + Year + "' AND  Stockiest_Code  = '" + Stock_Code + "'   ";



            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Prev_St_Prim(string div_code, string Stock_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " SELECT  SS_Head_Sl_No  " +

                  " FROM Trans_SS_Entry_Head  WHERE  Division_Code='" + div_code + "' AND Stockiest_Code  = '" + Stock_Code + "'  ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet Get_Prev_St_Division(string div_code, string Stock_Code, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            strQry = " SELECT  Sl_No from(SELECT  Sl_No,CAST('<XMLRoot><RowData>' +  " +
                  " REPLACE(Subdivision_code,',','</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                  " FROM  dbo.Trans_Secondary_Entry_Head_" + div_code + "   WHERE  Division_Code='" + div_code + "' AND Stockist_Code  = '" + Stock_Code + "'  )t " +
                  " CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)	 where  " +
                  " LTRIM(RTRIM(SubCode.n.value('.[1]','varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) FROM dbo.SplitString('" + sub_code + "', ',') where Item!='') ";


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
        public DataSet GetSLNO_SS_New(string div_code, string Stock_Code, string Month, string Year, string sub_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            comm = new SqlCommand();
            DataSet dsSale = null;

            //strQry = "select Sl_No,Approve_Flag FROM Trans_Secondary_Entry_Head where Division_Code='" + div_code + "' and  " +
            //         " Stockist_Code='" + Stock_Code + "' and Trans_Month='" + Month + "' and Trans_Year='" + Year + "'";

            

            strQry = "  SELECT  SS_Head_Sl_No from(SELECT  SS_Head_Sl_No, CAST('<XMLRoot><RowData>' + REPLACE(Subdiv_Code, ',', '</RowData><RowData>') + '</RowData></XMLRoot>' AS XML) AS x " +
                      "  FROM Trans_ss_Entry_Head  WHERE  Division_Code = '"+div_code+"' AND Month = '"+Month+"' "+
                      "  AND Year = '"+Year+"' AND  Stockiest_Code = '"+Stock_Code+"')t " +
                     "  CROSS APPLY x.nodes('/XMLRoot/RowData')SubCode(n)   where " +
                     "  LTRIM(RTRIM(SubCode.n.value('.[1]', 'varchar(8000)'))) in(SELECT CAST(Item AS VARCHAR(max)) " +
                     "  FROM dbo.SplitString('" + sub_code + "', ',') where Item!= '')  "; 


            try
            {
                dsSale = db_ER.Exec_DataSet(strQry, comm);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Setup", "GetTransHeadID()");
            }
            return dsSale;
        }
    }
}

