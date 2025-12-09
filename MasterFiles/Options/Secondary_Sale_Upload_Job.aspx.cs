using Bus_EReport;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
public partial class MasterFiles_Options_Secondary_Sale_Upload_Job : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        UploadSS();
    }
    public int GetNextHeadSlNo(SqlConnection con)
    {
        string query = "SELECT MAX(cast(SS_Head_Sl_No as int)) + 1 as SS_Head_Sl_No FROM Trans_SS_Entry_Head";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
    public int GetNextDetSlNo(SqlConnection con)
    {
        string query = "SELECT MAX(cast(SS_Det_Sl_No as int)) + 1 as SS_Det_Sl_No FROM Trans_SS_Entry_Detail";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }

    public int GetNextSecSlNo(SqlConnection con)
    {
        string query = "SELECT MAX(cast(SS_Sec_Sl_No as int)) + 1 as SS_Sec_Sl_No FROM Trans_SS_Entry_Detail_Value";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
    private void UploadSS()
    {

        conn.Open();
        List<string> divisionCodes = new List<string> { "2", "3", "4","5" };
        foreach (string divCode in divisionCodes)
        {
            string query = @"SELECT * FROM Secondary_Sale_Upload_Excel WHERE Status = 0 AND Division_Code = @DivisionCode order by Sl_No";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@DivisionCode", divCode);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string div_code = row["Division_Code"].ToString().Trim();

                string Sl_No = row["Sl_No"].ToString().Trim();
                string stockistCode = row["Stockist_Code"].ToString().Trim();
                string productCode = row["Prod_Code"].ToString().Trim();
                string territoryCode = row["Territory_Code"].ToString().Trim();
                string month = row["Month"].ToString();
                string year = row["Year"].ToString();
                string stockist_Code = string.Empty;
                string stockistName = string.Empty;

                string validationQuery = @"
    SELECT stockist_code, stockist_name 
    FROM Mas_Stockist  
    WHERE Stockist_Active_Flag = 0 
      AND Stockist_Designation = @StockistDesignation 
      AND Division_Code = @DivisionCode";

                using (SqlCommand validateCmd = new SqlCommand(validationQuery, conn))
                {
                    validateCmd.Parameters.AddWithValue("@StockistDesignation", stockistCode);
                    validateCmd.Parameters.AddWithValue("@DivisionCode", div_code);

                    using (SqlDataReader reader = validateCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stockist_Code = reader["stockist_code"].ToString();
                            stockistName = reader["stockist_name"].ToString();


                        }
                    }
                }
                string sfCode = string.Empty;

                string query2 = @"
    SELECT 
        SUBSTRING(SF_Code, 0, CHARINDEX(',', SF_Code)) AS SF_Code
    FROM mas_stockist 
    WHERE stockist_code = @StockistCode";

                using (SqlCommand cmd2 = new SqlCommand(query2, conn))
                {
                    cmd2.Parameters.AddWithValue("@StockistCode", stockist_Code);

                    object result = cmd2.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        sfCode = result.ToString();
                    else
                        sfCode = "";
                }

                SalesForce sf = new SalesForce();
                DataSet dsDet = sf.getSfCode_Verify(sfCode, div_code);
                string state = string.Empty;
                string subdiv = string.Empty;
                if (dsDet.Tables[0].Rows.Count > 0)
                {
                    state = dsDet.Tables[0].Rows[0]["State_Code"].ToString();
                    subdiv = dsDet.Tables[0].Rows[0]["subdivision_code"].ToString();
                }
                int ssHeadSlNo = GetNextHeadSlNo(conn);
                SecSale sa = new SecSale();
                DataSet dsHead = sa.Get_SS_Primary_Bill(div_code, stockist_Code, month, year);
                if (dsHead.Tables[0].Rows.Count > 0)
                {
                    ssHeadSlNo = Convert.ToInt32(dsHead.Tables[0].Rows[0]["SS_Head_Sl_No"].ToString());
                }
                else
                {
                    string headQuery = @"INSERT INTO Trans_SS_Entry_Head 
(SS_Head_Sl_No, SF_Code, State_Code, Division_Code, Stockiest_Code, 
 Month, Year, submitted_dtm, Status, updated_dtm, ChgUserID, Subdiv_Code) 
VALUES 
(@SS_Head_Sl_No, @SF_Code, @State_Code, @Division_Code, @Stockiest_Code, 
 @Month, @Year, GETDATE(), 0, GETDATE(), @ChgUserID, @Subdiv_Code)";

                    SqlCommand headCmd = new SqlCommand(headQuery, conn);
                    headCmd.Parameters.AddWithValue("@SS_Head_Sl_No", ssHeadSlNo);
                    headCmd.Parameters.AddWithValue("@SF_Code", sfCode);
                    headCmd.Parameters.AddWithValue("@State_Code", state);
                    headCmd.Parameters.AddWithValue("@Division_Code", div_code);
                    headCmd.Parameters.AddWithValue("@Stockiest_Code", stockist_Code);
                    headCmd.Parameters.AddWithValue("@Month", month);
                    headCmd.Parameters.AddWithValue("@Year", year);
                    headCmd.Parameters.AddWithValue("@ChgUserID", sfCode);
                    headCmd.Parameters.AddWithValue("@Subdiv_Code", subdiv);

                    headCmd.ExecuteNonQuery();
                }
                string Product_Detail_Code = string.Empty;
                string Product_Detail_Name = string.Empty;

                string PQuery = @"SELECT Product_Detail_Code,Product_Detail_Name FROM mas_product_detail 
                               WHERE Product_Active_Flag = 0 
                               AND sale_Erp_Code = @ProductCode 
                               AND Division_Code = @DivisionCode";

                using (SqlCommand pCmd = new SqlCommand(PQuery, conn))
                {
                    pCmd.Parameters.AddWithValue("@ProductCode", productCode);
                    pCmd.Parameters.AddWithValue("@DivisionCode", div_code);

                    using (SqlDataReader reader = pCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product_Detail_Code = reader["Product_Detail_Code"].ToString();
                            Product_Detail_Name = reader["Product_Detail_Name"].ToString();

                        }
                    }
                }

                string priceQuery = @"SELECT TOP 1 MRP_Price, Retailor_Price, Distributor_Price, Target_Price, NSR_Price
FROM Mas_Product_State_Rates
WHERE Product_Detail_Code = @ProductCode AND State_Code = @StateCode";

                SqlCommand priceCmd = new SqlCommand(priceQuery, conn);
                priceCmd.Parameters.AddWithValue("@ProductCode", Product_Detail_Code);
                priceCmd.Parameters.AddWithValue("@StateCode", state);

                decimal distributorPrice = 0;
                decimal mrp = 0, retailor = 0, target = 0, nsr = 0;

                using (SqlDataReader rdr = priceCmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        mrp = rdr["MRP_Price"] != DBNull.Value ? Convert.ToDecimal(rdr["MRP_Price"]) : 0;
                        retailor = rdr["Retailor_Price"] != DBNull.Value ? Convert.ToDecimal(rdr["Retailor_Price"]) : 0;
                        distributorPrice = rdr["Distributor_Price"] != DBNull.Value ? Convert.ToDecimal(rdr["Distributor_Price"]) : 0;
                        target = rdr["Target_Price"] != DBNull.Value ? Convert.ToDecimal(rdr["Target_Price"]) : 0;
                        nsr = rdr["NSR_Price"] != DBNull.Value ? Convert.ToDecimal(rdr["NSR_Price"]) : 0;
                    }
                    rdr.Close();
                }
                int ssDetSlNo = GetNextDetSlNo(conn);
                string detailQuery = @"INSERT INTO Trans_SS_Entry_Detail 
(SS_Det_Sl_No, SS_Head_Sl_No, Product_Detail_Code,
 MRP_Price, Retailor_Price, Distributor_Price, 
 Target_Price, NSR_Price, Division_Code,
 Created_dtm, Updated_dtm, ChgUserID)
VALUES 
(@SS_Det_Sl_No, @SS_Head_Sl_No, @Product_Detail_Code,
 @MRP_Price, @Retailor_Price, @Distributor_Price, 
 @Target_Price, @NSR_Price, @Division_Code,
 GETDATE(), GETDATE(), @ChgUserID)";

                SqlCommand detailCmd = new SqlCommand(detailQuery, conn);
                detailCmd.Parameters.AddWithValue("@SS_Det_Sl_No", ssDetSlNo);
                detailCmd.Parameters.AddWithValue("@SS_Head_Sl_No", ssHeadSlNo);
                detailCmd.Parameters.AddWithValue("@Product_Detail_Code", Product_Detail_Code);
                detailCmd.Parameters.AddWithValue("@MRP_Price", mrp);
                detailCmd.Parameters.AddWithValue("@Retailor_Price", retailor);
                detailCmd.Parameters.AddWithValue("@Distributor_Price", distributorPrice);
                detailCmd.Parameters.AddWithValue("@Target_Price", target);
                detailCmd.Parameters.AddWithValue("@NSR_Price", nsr);
                detailCmd.Parameters.AddWithValue("@Division_Code", div_code);
                detailCmd.Parameters.AddWithValue("@ChgUserID", sfCode);

                detailCmd.ExecuteNonQuery();

                string paramQuery = @"SELECT Pri_Sec_SName, Sec_Sale_Code FROM Mas_Sec_Sale_Param WHERE Division_Code = @Div AND Active = 0";
                SqlCommand paramCmd = new SqlCommand(paramQuery, conn);
                paramCmd.Parameters.AddWithValue("@Div", div_code);

                Dictionary<string, string> secSaleParamMap = new Dictionary<string, string>();
                using (SqlDataReader rdr = paramCmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string priSecName = rdr["Pri_Sec_SName"].ToString().Trim();
                        string secSaleCode = rdr["Sec_Sale_Code"].ToString().Trim();
                        secSaleParamMap[priSecName] = secSaleCode;
                    }
                }
                paramCmd.Dispose();


                decimal openingBalanceQty = Convert.ToDecimal(row["Opening_Balance_Qty"]);
                decimal openingBalanceValue = Convert.ToDecimal(row["Opening_Balance_Value"]);
                decimal primarySaleQty = Convert.ToDecimal(row["Primary_Sale_Qty"]);
                decimal primarySaleValue = Convert.ToDecimal(row["Primary_Sale_Value"]);
                decimal receiptQty = Convert.ToDecimal(row["Receipt_Qty"]);
                decimal receiptValue = Convert.ToDecimal(row["Receipt_Value"]);
                decimal transitQty = Convert.ToDecimal(row["Transit_Qty"]);
                decimal transitValue = Convert.ToDecimal(row["Transit_Value"]);
                decimal saleQty = Convert.ToDecimal(row["Sale_Qty"]);
                decimal saleValue = Convert.ToDecimal(row["Sale_Value"]);
                decimal freeQty = Convert.ToDecimal(row["Free_Qty"]);
                decimal freeValue = Convert.ToDecimal(row["Free_Value"]);
                decimal closingBalanceQty = Convert.ToDecimal(row["Closing_Balance_Qty"]);
                decimal closingBalanceValue = Convert.ToDecimal(row["Closing_Balance_Value"]);

                var salesData = new Dictionary<string, Tuple<decimal, decimal>>()
    {
        { "OB", Tuple.Create(openingBalanceQty, openingBalanceValue) },
        { "PS", Tuple.Create(primarySaleQty, primarySaleValue) },
        { "RC", Tuple.Create(receiptQty, receiptValue) },
        { "TR", Tuple.Create(transitQty, transitValue) },
        { "SA", Tuple.Create(saleQty, saleValue) },
        { "FR", Tuple.Create(freeQty, freeValue) },
        { "CB", Tuple.Create(closingBalanceQty, closingBalanceValue) }
    };

                foreach (var item in salesData)
                {
                    string priSecName = item.Key;
                    decimal secSaleQty = item.Value.Item1;
                    decimal secSaleVal = item.Value.Item2;

                    string secSaleCode;
                    if (!secSaleParamMap.TryGetValue(priSecName, out secSaleCode))
                        continue;

                    int ssSecSlNo = GetNextSecSlNo(conn);

                    // Insert into Trans_SS_Entry_Detail_Value
                    string insertQuery = @"
            INSERT INTO Trans_SS_Entry_Detail_Value
            (SS_Sec_Sl_No, SS_Det_Sl_No, SS_Head_Sl_No, Sec_Sale_Code, Sec_Sale_Qty, Sec_Sale_Value, Division_Code, Created_dtm, Updated_dtm, ChgUserID)
            VALUES
            (@SS_Sec_Sl_No, @SS_Det_Sl_No, @SS_Head_Sl_No, @Sec_Sale_Code, @Sec_Sale_Qty, @Sec_Sale_Value, @Division_Code, GETDATE(), GETDATE(), @ChgUserID)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@SS_Sec_Sl_No", ssSecSlNo);
                        insertCmd.Parameters.AddWithValue("@SS_Det_Sl_No", ssDetSlNo);
                        insertCmd.Parameters.AddWithValue("@SS_Head_Sl_No", ssHeadSlNo);
                        insertCmd.Parameters.AddWithValue("@Sec_Sale_Code", secSaleCode);
                        insertCmd.Parameters.AddWithValue("@Sec_Sale_Qty", secSaleQty);
                        // insertCmd.Parameters.AddWithValue("@Sec_Sale_Value", secSaleVal);
                        //  insertCmd.Parameters.AddWithValue("@Sec_Sale_Value", Convert.ToDecimal(secSaleVal));
                        var param = insertCmd.Parameters.Add("@Sec_Sale_Value", SqlDbType.Decimal);
                        param.Precision = 18;
                        param.Scale = 2;
                        param.Value = secSaleVal;
                     //  insertCmd.Parameters.Add("@Sec_Sale_Value", SqlDbType.Decimal).Value = secSaleVal;

                        insertCmd.Parameters.AddWithValue("@Division_Code", div_code);
                        insertCmd.Parameters.AddWithValue("@ChgUserID", sfCode);

                        insertCmd.ExecuteNonQuery();

                    }

                }
                string updateQuery = "UPDATE Secondary_Sale_Upload_Excel SET Status = 1,Row_Status='Processed' WHERE Sl_No = @SlNo AND Division_Code = @DivisionCode";
                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@SlNo", Sl_No);
                    updateCmd.Parameters.AddWithValue("@DivisionCode", div_code);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }
        conn.Close();
    }
}