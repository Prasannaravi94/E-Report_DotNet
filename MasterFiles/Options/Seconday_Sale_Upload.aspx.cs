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


public partial class MasterFiles_Options_Seconday_Sale_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();

    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;

            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();

            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }

                ddlYear.Text = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

            }

        }

    }
    private decimal TryParseDecimal(string val)
    {
        decimal d;
        return decimal.TryParse(val, out d) ? d : 0;
    }
    private void InsertData()
    {
        conn.Open();
        if (chkDelete.Checked == true)
        {
            string updateQuery = "delete Secondary_Sale_Upload_Excel WHERE Division_Code = @DivisionCode AND Month = @imonth AND Year = @iyear";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {

                updateCmd.Parameters.AddWithValue("@DivisionCode", div_code);
                updateCmd.Parameters.AddWithValue("@imonth", ddlMonth.SelectedValue);
                updateCmd.Parameters.AddWithValue("@iyear", ddlYear.SelectedValue);
                updateCmd.ExecuteNonQuery();
            }
        }


        // string sf_code = string.Empty;
        string sf_Username = string.Empty;
        string strsfcode = string.Empty;
        string Strtype = string.Empty;
        string Pool_Name = string.Empty;
        string Pool_NameNew = string.Empty;
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();

            }
            Stockist objStock = new Stockist();
            int SlNo = objStock.GetSecondary();

            string stockistCode = columns[1]; // assuming column 1 = Stockist_Code
            string divisionCode = Session["div_code"].ToString(); // from session
            string ststatus = string.Empty;
            // 🔍 Check if stockist is valid
            string validationQuery = @"SELECT COUNT(*) FROM Mas_Stockist 
                               WHERE Stockist_Active_Flag = 0 
                               AND Stockist_Designation = @StockistCode 
                               AND Division_Code = @DivisionCode";

            SqlCommand validateCmd = new SqlCommand(validationQuery, conn);
            validateCmd.Parameters.AddWithValue("@StockistCode", stockistCode);
            validateCmd.Parameters.AddWithValue("@DivisionCode", divisionCode);

            string prodCode = columns[5]; // assuming column 1 = Stockist_Code

            string prstatus = string.Empty;
            // 🔍 Check if stockist is valid
            string Query = @"SELECT COUNT(*) FROM mas_product_detail 
                               WHERE Product_Active_Flag = 0 
                               AND sale_Erp_Code = @ProductCode 
                               AND Division_Code = @DivisionCode";

            SqlCommand Querycmd = new SqlCommand(Query, conn);
            Querycmd.Parameters.AddWithValue("@ProductCode", prodCode);
            Querycmd.Parameters.AddWithValue("@DivisionCode", divisionCode);


            int count = (int)validateCmd.ExecuteScalar();
            int count2 = (int)Querycmd.ExecuteScalar();
            if (count == 0 && count2 == 0)
            {
                ststatus = "Stockist ERP Code/Product ERP Code Not Matched";
            }
            else if (count == 0)
            {
                ststatus = "Stockist ERP Code Not Matched";
            }
            else if (count2 == 0)
            {
                ststatus = "Product ERP Code Not Matched";
            }
            else
            {
                ststatus = "";
            }


            div_code = Session["div_code"].ToString();

            bool[] isdecimal = new bool[]
{
    false, // 0 - Skip or Sl_No is handled separately
    false, // 1 - Stockist_Code
    false, // 2 - Stockist_Name
    false, // 3 - Territory_Code
    false, // 4 - Territory_Name
    false, // 5 - Prod_Code
    false, // 6 - Prod_Name
    false, // 7 - Unit
    true,  // 8 - Opening_Balance_Qty
    true,  // 9 - Opening_Balance_Value
    true,  // 10 - Primary_Sale_Qty
    true,  // 11 - Primary_Sale_Value
    true,  // 12 - Receipt_Qty
    true,  // 13 - Receipt_Value
    true,  // 14 - Transit_Qty
    true,  // 15 - Transit_Value
    true,  // 16 - Sale_Qty
    true,  // 17 - Sale_Value
    true,  // 18 - Free_Qty
    true,  // 19 - Free_Value
    true,  // 20 - Closing_Balance_Qty
    true   // 21 - Closing_Balance_Value
     
};

            // Build the value list
            List<string> valueList = new List<string>();
            valueList.Add(SlNo.ToString()); // Sl_No

            for (int j = 1; j <= 21; j++)
            {
                string val = columns[j].Trim();

                if (string.IsNullOrEmpty(val))
                {
                    valueList.Add("0");
                }
                else
                {
                    if (isdecimal[j])
                    {
                        valueList.Add(val);
                    }
                    else
                    {
                        valueList.Add("'" + val.Replace("'", "''") + "'");
                    }
                }
            }
            valueList.Add("'" + div_code.Replace("'", "''") + "'");
            valueList.Add(ddlMonth.SelectedValue);
            valueList.Add(ddlYear.SelectedValue);
            valueList.Add("GETDATE()");
            if (ststatus == "")
            {
                valueList.Add("0");
            }
            else
            {
                valueList.Add("2");
            }
            valueList.Add("'" + ststatus + "'");


            string sql = @"
INSERT INTO Secondary_Sale_Upload_Excel
(
    Sl_No, Stockist_Code, Stockist_Name, Territory_Code, Territory_Name,
    Prod_Code, Prod_Name, Unit,
    Opening_Balance_Qty, Opening_Balance_Value,
    Primary_Sale_Qty, Primary_Sale_Value,
    Receipt_Qty, Receipt_Value,
    Transit_Qty, Transit_Value,
    Sale_Qty, Sale_Value,
    Free_Qty, Free_Value,
    Closing_Balance_Qty, Closing_Balance_Value,
    Division_Code, Month, Year, Created_Date, Status,Row_Status
)
VALUES (" + string.Join(",", valueList) + ")";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Uploaded Sucessfully');</script>");
            // }

        }
        conn.Close();
    }
    private void ImporttoDatatable()
    {
        //try
        //{

        if (FlUploadcsv.HasFile)
        {

            string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);
            FlUploadcsv.SaveAs(excelPath);

            string conString = string.Empty;
            string extension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
            switch (extension)
            {
                case ".xls": //Excel 97-03
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 or higher
                    conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                    break;

            }
            conString = string.Format(conString, excelPath);
            using (OleDbConnection excel_con = new OleDbConnection(conString))
            {
                excel_con.Open();
                string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtExcelData = new DataTable();

                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                {
                    ds = new DataSet();
                    oda.Fill(ds);
                    Dt = ds.Tables[0];
                    //objAdapter1.Fill(ds);
                    //Dt = ds.Tables[0];
                }
                excel_con.Close();
            }
        }
        else
        {

        }
        //}
        //catch (Exception ex)
        //{

        //}
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        ImporttoDatatable();
        // InsertData();
        bulk();
        //  InsertDataBULK();
    }
    //protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Secondary_Sale_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Secondary_Sale_Upload.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
    protected void lnkUploded_Click(object sender, EventArgs e)
    {
        string query = @"SELECT * FROM Secondary_Sale_Upload_Excel 
                         WHERE Division_Code = @DivisionCode 
                         AND Month = @imonth AND Year = @iyear";

        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@DivisionCode", div_code);
        cmd.Parameters.AddWithValue("@imonth", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@iyear", ddlYear.SelectedValue);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Sheet1");

                using (MemoryStream ms = new MemoryStream())
                {
                    wb.SaveAs(ms);
                    ms.Position = 0;

                    // Trigger download
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=SecondarySaleUpload_" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + ".xlsx");
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
            }
        }
        else
        {
            // Optionally show a message if no data
            lblMessage.Text = "No data found for selected month and year.";
        }

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

    private void InsertDataBULK()
    {
        conn.Open();
        if (chkDelete.Checked == true)
        {
            string updateQuery = "delete Secondary_Sale_Upload_Excel WHERE Division_Code = @DivisionCode AND Month = @imonth AND Year = @iyear";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {

                updateCmd.Parameters.AddWithValue("@DivisionCode", div_code);
                updateCmd.Parameters.AddWithValue("@imonth", ddlMonth.SelectedValue);
                updateCmd.Parameters.AddWithValue("@iyear", ddlYear.SelectedValue);
                updateCmd.ExecuteNonQuery();
            }
        }


        DataTable bulkTable = new DataTable();

        bulkTable.Columns.Add("Sl_No", typeof(int));
        bulkTable.Columns.Add("Stockist_Code", typeof(string));
        bulkTable.Columns.Add("Stockist_Name", typeof(string));
        bulkTable.Columns.Add("Territory_Code", typeof(string));
        bulkTable.Columns.Add("Territory_Name", typeof(string));
        bulkTable.Columns.Add("Prod_Code", typeof(string));
        bulkTable.Columns.Add("Prod_Name", typeof(string));
        bulkTable.Columns.Add("Unit", typeof(string));
        bulkTable.Columns.Add("Opening_Balance_Qty", typeof(decimal));
        bulkTable.Columns.Add("Opening_Balance_Value", typeof(decimal));
        bulkTable.Columns.Add("Primary_Sale_Qty", typeof(decimal));
        bulkTable.Columns.Add("Primary_Sale_Value", typeof(decimal));
        bulkTable.Columns.Add("Receipt_Qty", typeof(decimal));
        bulkTable.Columns.Add("Receipt_Value", typeof(decimal));
        bulkTable.Columns.Add("Transit_Qty", typeof(decimal));
        bulkTable.Columns.Add("Transit_Value", typeof(decimal));
        bulkTable.Columns.Add("Sale_Qty", typeof(decimal));
        bulkTable.Columns.Add("Sale_Value", typeof(decimal));
        bulkTable.Columns.Add("Free_Qty", typeof(decimal));
        bulkTable.Columns.Add("Free_Value", typeof(decimal));
        bulkTable.Columns.Add("Closing_Balance_Qty", typeof(decimal));
        bulkTable.Columns.Add("Closing_Balance_Value", typeof(decimal));
        bulkTable.Columns.Add("Division_Code", typeof(string));
        bulkTable.Columns.Add("Month", typeof(string));
        bulkTable.Columns.Add("Year", typeof(string));
        bulkTable.Columns.Add("Created_Date", typeof(DateTime));
        bulkTable.Columns.Add("Status", typeof(int));
        bulkTable.Columns.Add("Row_Status", typeof(string));

        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow sourceRow = Dt.Rows[i];

            string stockistCode = sourceRow["Stockists Code"].ToString().Trim();
            string prodCode = sourceRow["Prod Code"].ToString().Trim();
            string divisionCode = Session["div_code"].ToString();
            string validationQuery = @"SELECT COUNT(*) FROM Mas_Stockist 
                           WHERE Stockist_Active_Flag = 0 
                           AND Stockist_Designation = @StockistCode 
                           AND Division_Code = @DivisionCode";
            bool isStockistValid;
            using (SqlCommand validateCmd = new SqlCommand(validationQuery, conn))
            {
                validateCmd.Parameters.AddWithValue("@StockistCode", stockistCode);
                validateCmd.Parameters.AddWithValue("@DivisionCode", divisionCode);

                // Make sure the connection is open
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                int count = (int)validateCmd.ExecuteScalar();
                isStockistValid = count > 0;
            }

            bool isProductValid;
            string productQuery = @"SELECT COUNT(*) FROM mas_product_detail 
                        WHERE Product_Active_Flag = 0 
                        AND sale_Erp_Code = @ProductCode 
                        AND Division_Code = @DivisionCode";

            using (SqlCommand productCmd = new SqlCommand(productQuery, conn))
            {
                productCmd.Parameters.AddWithValue("@ProductCode", prodCode);  // e.g., from Excel or UI
                productCmd.Parameters.AddWithValue("@DivisionCode", divisionCode);

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                int productCount = (int)productCmd.ExecuteScalar();
                isProductValid = productCount > 0;
            }

            string statusText = "";
            int statusCode = 0;

            if (!isStockistValid && !isProductValid)
            {
                statusText = "Stockist ERP Code/Product ERP Code Not Matched";
                statusCode = 2;
            }
            else if (!isStockistValid)
            {
                statusText = "Stockist ERP Code Not Matched";
                statusCode = 2;
            }
            else if (!isProductValid)
            {
                statusText = "Product ERP Code Not Matched";
                statusCode = 2;
            }

            DataRow newRow = bulkTable.NewRow();

            newRow["Sl_No"] = new Stockist().GetSecondary();
            newRow["Stockist_Code"] = stockistCode;
            newRow["Stockist_Name"] = sourceRow["Stockist Name"].ToString().Trim();
            newRow["Territory_Code"] = sourceRow["Territory Code"].ToString().Trim();
            newRow["Territory_Name"] = sourceRow["Territory Name"].ToString().Trim();
            newRow["Prod_Code"] = prodCode;
            newRow["Prod_Name"] = sourceRow["Prod Name"].ToString().Trim();
            newRow["Unit"] = sourceRow["Unit"].ToString().Trim();


            newRow["Opening_Balance_Qty"] = TryParseDecimal(sourceRow["Opening Balance Qty"].ToString());
            newRow["Opening_Balance_Value"] = TryParseDecimal(sourceRow["Opening Balance Value"].ToString());
            newRow["Primary_Sale_Qty"] = TryParseDecimal(sourceRow["Primary Sale Qty"].ToString());
            newRow["Primary_Sale_Value"] = TryParseDecimal(sourceRow["Primary Sale Value"].ToString());
            newRow["Receipt_Qty"] = TryParseDecimal(sourceRow["Receipt Qty"].ToString());
            newRow["Receipt_Value"] = TryParseDecimal(sourceRow["Receipt Value"].ToString());
            newRow["Transit_Qty"] = TryParseDecimal(sourceRow["Transit Qty"].ToString());
            newRow["Transit_Value"] = TryParseDecimal(sourceRow["Transit Value"].ToString());
            newRow["Sale_Qty"] = TryParseDecimal(sourceRow["Sale Qty"].ToString());
            newRow["Sale_Value"] = TryParseDecimal(sourceRow["Sale Value"].ToString());
            newRow["Free_Qty"] = TryParseDecimal(sourceRow["Free Qty Qty"].ToString());
            newRow["Free_Value"] = TryParseDecimal(sourceRow["Free Qty Value"].ToString());
            newRow["Closing_Balance_Qty"] = TryParseDecimal(sourceRow["Closing Balance Qty"].ToString());
            newRow["Closing_Balance_Value"] = TryParseDecimal(sourceRow["Closing Balance Value"].ToString());

            newRow["Division_Code"] = divisionCode;
            newRow["Month"] = ddlMonth.SelectedValue;
            newRow["Year"] = ddlYear.SelectedValue;
            newRow["Created_Date"] = DateTime.Now;
            newRow["Status"] = statusCode;
            newRow["Row_Status"] = statusText;

            bulkTable.Rows.Add(newRow);
        }

        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
        {
            bulkCopy.DestinationTableName = "Secondary_Sale_Upload_Excel";

            foreach (DataColumn col in bulkTable.Columns)
            {
                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
            }

            bulkCopy.WriteToServer(bulkTable);
        }

        conn.Close();
        //  }
    }

    private void bulk()
    {
        conn.Open();

        if (chkDelete.Checked)
        {
            string deleteQuery = @"DELETE FROM Secondary_Sale_Upload_Excel
                                   WHERE Division_Code = @DivisionCode
                                   AND Month = @Month
                                   AND Year = @Year";
            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
            {
                cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar, 50).Value = div_code;
                cmd.Parameters.Add("@Month", SqlDbType.VarChar, 10).Value = ddlMonth.SelectedValue;
                cmd.Parameters.Add("@Year", SqlDbType.VarChar, 10).Value = ddlYear.SelectedValue;
                cmd.ExecuteNonQuery();
            }
        }

        HashSet<string> validStockists = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        HashSet<string> validProducts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        using (SqlCommand cmd = new SqlCommand(@"
            SELECT Stockist_Designation
            FROM Mas_Stockist
            WHERE Stockist_Active_Flag = 0 AND Division_Code = @DivisionCode", conn))
        {
            cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar, 50).Value = div_code;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                    validStockists.Add(rdr.GetString(0).Trim());
            }
        }

        using (SqlCommand cmd = new SqlCommand(@"
            SELECT sale_Erp_Code
            FROM mas_product_detail
            WHERE Product_Active_Flag = 0 AND Division_Code = @DivisionCode", conn))
        {
            cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar, 50).Value = div_code;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                    validProducts.Add(rdr.GetString(0).Trim());
            }
        }

        DataTable uploadTable = new DataTable();
        uploadTable.Columns.Add("Sl_No", typeof(int));
        uploadTable.Columns.Add("Stockist_Code", typeof(string));
        uploadTable.Columns.Add("Stockist_Name", typeof(string));
        uploadTable.Columns.Add("Territory_Code", typeof(string));
        uploadTable.Columns.Add("Territory_Name", typeof(string));
        uploadTable.Columns.Add("Prod_Code", typeof(string));
        uploadTable.Columns.Add("Prod_Name", typeof(string));
        uploadTable.Columns.Add("Unit", typeof(string));
        uploadTable.Columns.Add("Opening_Balance_Qty", typeof(decimal));
        uploadTable.Columns.Add("Opening_Balance_Value", typeof(decimal));
        uploadTable.Columns.Add("Primary_Sale_Qty", typeof(decimal));
        uploadTable.Columns.Add("Primary_Sale_Value", typeof(decimal));
        uploadTable.Columns.Add("Receipt_Qty", typeof(decimal));
        uploadTable.Columns.Add("Receipt_Value", typeof(decimal));
        uploadTable.Columns.Add("Transit_Qty", typeof(decimal));
        uploadTable.Columns.Add("Transit_Value", typeof(decimal));
        uploadTable.Columns.Add("Sale_Qty", typeof(decimal));
        uploadTable.Columns.Add("Sale_Value", typeof(decimal));
        uploadTable.Columns.Add("Free_Qty", typeof(decimal));
        uploadTable.Columns.Add("Free_Value", typeof(decimal));
        uploadTable.Columns.Add("Closing_Balance_Qty", typeof(decimal));
        uploadTable.Columns.Add("Closing_Balance_Value", typeof(decimal));
        uploadTable.Columns.Add("Division_Code", typeof(string));
        uploadTable.Columns.Add("Month", typeof(string));
        uploadTable.Columns.Add("Year", typeof(string));
        uploadTable.Columns.Add("Created_Date", typeof(DateTime));
        uploadTable.Columns.Add("Status", typeof(int));
        uploadTable.Columns.Add("Row_Status", typeof(string));

        Stockist objStock = new Stockist();
        int slNoCounter = objStock.GetSecondary();

        foreach (DataRow row in Dt.Rows)
        {
            bool isEmpty = true;
            foreach (var item in row.ItemArray)
            {
                if (item != null && !string.IsNullOrWhiteSpace(item.ToString()))
                {
                    isEmpty = false;
                    break;
                }
            }
            if (isEmpty) continue;
            string stockistCode = row[1].ToString().Trim();
            string prodCode = row[5].ToString().Trim();
            string ststatus;

            bool stockistValid = validStockists.Contains(stockistCode);
            bool productValid = validProducts.Contains(prodCode);

            if (!stockistValid && !productValid)
                ststatus = "Stockist ERP Code/Product ERP Code Not Matched";
            else if (!stockistValid)
                ststatus = "Stockist ERP Code Not Matched";
            else if (!productValid)
                ststatus = "Product ERP Code Not Matched";
            else
                ststatus = "";

            DataRow newRow = uploadTable.NewRow();

            newRow["Sl_No"] = slNoCounter++;
            newRow["Stockist_Code"] = stockistCode;
            newRow["Stockist_Name"] = row[2].ToString().Trim();
            newRow["Territory_Code"] = row[3].ToString().Trim();
            newRow["Territory_Name"] = row[4].ToString().Trim();
            newRow["Prod_Code"] = prodCode;
            newRow["Prod_Name"] = row[6].ToString().Trim();
            newRow["Unit"] = row[7].ToString().Trim();

            for (int j = 8; j <= 21; j++)
            {
                decimal val = 0;
                decimal.TryParse(row[j].ToString(), out val);
                newRow[j] = val;
            }

            newRow["Division_Code"] = div_code;
            newRow["Month"] = ddlMonth.SelectedValue;
            newRow["Year"] = ddlYear.SelectedValue;
            newRow["Created_Date"] = DateTime.Now;
            newRow["Status"] = (ststatus == "") ? 0 : 2;
            newRow["Row_Status"] = ststatus;

            uploadTable.Rows.Add(newRow);
        }

        using (SqlBulkCopy bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.TableLock, null))
        {
            bulk.DestinationTableName = "Secondary_Sale_Upload_Excel";
            bulk.BatchSize = 5000;
            bulk.BulkCopyTimeout = 0;

            foreach (DataColumn col in uploadTable.Columns)
                bulk.ColumnMappings.Add(col.ColumnName, col.ColumnName);

            bulk.WriteToServer(uploadTable);
        }
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Secondary Sale Uploaded Sucessfully');</script>");


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        conn.Open();
        string query = @"SELECT top(2) * FROM Secondary_Sale_Upload_Excel WHERE Status = 0 AND Division_Code = @DivisionCode";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@DivisionCode", div_code);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        foreach (DataRow row in dt.Rows)
        {
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
                    insertCmd.Parameters.AddWithValue("@Sec_Sale_Value", secSaleVal);
                    insertCmd.Parameters.AddWithValue("@Division_Code", div_code);
                    insertCmd.Parameters.AddWithValue("@ChgUserID", sfCode);

                    insertCmd.ExecuteNonQuery();

                }

            }
            string updateQuery = "UPDATE Secondary_Sale_Upload_Excel SET Status = 1 WHERE Sl_No = @SlNo AND Division_Code = @DivisionCode";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
            {
                updateCmd.Parameters.AddWithValue("@SlNo", Sl_No);
                updateCmd.Parameters.AddWithValue("@DivisionCode", div_code);
                updateCmd.ExecuteNonQuery();
            }
        }
        conn.Close();
    }
}