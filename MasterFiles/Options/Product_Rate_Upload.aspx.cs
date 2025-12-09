using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.Text;

public partial class MasterFiles_Options_Product_Rate_Upload : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dsProd = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string prod_code = string.Empty;
    string prod_name = string.Empty;
    decimal mrp_amt;
    decimal ret_amt;
    decimal dist_amt;
    decimal nsr_amt;
    decimal target_amt;
    decimal sam_amt;
    string effective_from = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iReturn = -1;

    DataTable dts = new DataTable();
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            FillState(div_code);
            // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
         
         
        }
    }
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStateProd(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }
    private void FillRate()
    {
        btnUPload.Visible = true;
        Product dv = new Product();
        DataSet dsst = new DataSet();
        string State_Code = string.Empty;
        btnUPload.Visible = true;

        FlUploadcsv.Visible = true;
        //dsProd = dv.getProdRate(ddlState.SelectedValue.ToString(), div_code);
        if (ddlState.SelectedItem.Text != "ALL")
        {
            dts = dv.getProductRate_dt(ddlState.SelectedValue.ToString(), div_code);



            if (dts.Rows.Count > 0)
            {

                grdRate.Visible = true;
                grdRate.DataSource = dts;
                grdRate.DataBind();
            }
            else
            {

                grdRate.DataSource = dts;
                grdRate.DataBind();
            }
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            dgGrid.BorderWidth = Unit.Pixel(1);
            dgGrid.BorderColor = System.Drawing.Color.Black;
            dgGrid.BackColor = System.Drawing.Color.LightBlue;
            dgGrid.GridLines = GridLines.Both;
            dgGrid.CellPadding = 1;


            //ds.Columns.Add("PayS_Month");
            //ds.Columns.Add("PayS_Year");

            dgGrid.DataSource = dts;

            dgGrid.DataBind();


            hw.Write("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            hw.Write("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
            hw.Write("xmlns=\"http://www.w3.org/TR/REC-html40\"> ");
            hw.Write("<head> ");
            hw.Write("<!--[if gte mso 9]><xml> ");
            hw.Write("<x:ExcelWorkbook> ");
            hw.Write("<x:ExcelWorksheets> ");
            hw.Write("<x:ExcelWorksheet> ");
            hw.Write("<x:Name>Sheet1</x:Name> ");
            hw.Write("<x:WorksheetOptions> ");
            hw.Write("<x:Selected/> ");
            hw.Write("<x:ProtectContents>False</x:ProtectContents> ");
            hw.Write("<x:ProtectObjects>False</x:ProtectObjects> ");
            hw.Write("<x:ProtectScenarios>False</x:ProtectScenarios> ");
            hw.Write("</x:WorksheetOptions> ");
            hw.Write("</x:ExcelWorksheet> ");
            hw.Write("</x:ExcelWorksheets> ");
            hw.Write("</x:ExcelWorkbook> ");
            hw.Write("</xml><![endif]--> ");
            hw.Write("</head>");
            hw.WriteLine("");

            StringBuilder sb = new StringBuilder();
            dgGrid.RenderControl(hw);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dts, "ProductRate");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename="+ddlState.SelectedItem.Text.Trim()+".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
                //System.IO.FileInfo objFileInfo1 = new System.IO.FileInfo(path);
                //objFileInfo1.IsReadOnly = false;
            }


            //      this.EnableViewState = false;

            Response.Write(tw.ToString());
            Response.End();
        }
    }
    protected void lnkcount_Click(object sender, EventArgs e)
    {

        FillRate();

   



        //string url = "Product_Rate_Upload.aspx?state_code=" + ddlState.SelectedValue;
        //string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
        //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);


    }
  
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();
        InsertData();
    }
    private void ImporttoDatatable()
    {
        try
        {
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
                        DataSet ds = new DataSet();
                        oda.Fill(ds);
                        Dt = ds.Tables[0];
                    }
                    excel_con.Close();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void InsertData()
    {
        SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        int k = 0;
        string StrCat_Code = string.Empty;
        string StrGrp_Code = string.Empty;
        string StrBrd_Code = string.Empty;
        string strState = string.Empty;
        string stateName = string.Empty;
        string subdivCode = string.Empty;
        string SubName = string.Empty;
        DataSet dsprodcat = new DataSet(); 
        DataSet dsprodgrp = new DataSet();
        DataSet dsprodbrd = new DataSet();
        DataSet dsState = new DataSet();
        DataSet dsDesignation = new DataSet();

        for (int i = 0; i < Dt.Rows.Count; i++)
        {

            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString();
            }
            conn.Open();
            if (k == 0)
            {
                Product dv = new Product();
                iReturn = dv.DeleteProductRate(ddlState.SelectedValue, div_code);
            }
            Product prod = new Product();
            int ProductSlNO = prod.GetProductCode_Rate();


            //mrp_amt = Convert.ToDecimal(columns[5].Trim());
            //mrp_amt = Math.Round(mrp_amt, 2);

            //ret_amt = Convert.ToDecimal(columns[6].Trim());
            //ret_amt = Math.Round(ret_amt, 2);

            //dist_amt = Convert.ToDecimal(columns[7].Trim());
            //dist_amt = Math.Round(dist_amt, 2);

            //nsr_amt = Convert.ToDecimal(columns[8].Trim());
            //nsr_amt = Math.Round(nsr_amt, 2);

            //target_amt = Convert.ToDecimal(columns[9].Trim());
            //target_amt = Math.Round(target_amt, 2);

            //sam_amt = Convert.ToDecimal(columns[10].Trim());
            //sam_amt = Math.Round(sam_amt, 2);

            if (columns[5] == null || columns[5] == "0" || string.IsNullOrWhiteSpace(columns[5].ToString()))
            {
                mrp_amt = 0;
            }
            else
            {
                mrp_amt = Convert.ToDecimal(columns[5].ToString().Trim());
                mrp_amt = Math.Round(mrp_amt, 2);
            }


            ret_amt = Convert.ToDecimal(columns[6].Trim());
            ret_amt = Math.Round(ret_amt, 2);


            if (columns[7] == null || columns[7] == "0" || string.IsNullOrWhiteSpace(columns[7].ToString()))
            {
                dist_amt = 0;
            }
            else
            {
                dist_amt = Convert.ToDecimal(columns[7].ToString().Trim());
                dist_amt = Math.Round(dist_amt, 2);
            }

            if (columns[8] == null || columns[8] == "0" || string.IsNullOrWhiteSpace(columns[8].ToString()))
            {
                nsr_amt = 0;
            }
            else
            {
                nsr_amt = Convert.ToDecimal(columns[8].ToString().Trim());
                nsr_amt = Math.Round(nsr_amt, 2);
            }



            if (columns[9] == null || columns[9] == "0" || string.IsNullOrWhiteSpace(columns[9].ToString()))
            {
                target_amt = 0;
            }
            else
            {
                target_amt = Convert.ToDecimal(columns[9].ToString().Trim());
                target_amt = Math.Round(target_amt, 2);
            }

            if (columns[10] == null || columns[10] == "0" || string.IsNullOrWhiteSpace(columns[10].ToString()))
            {
                sam_amt = 0;
            }
            else
            {
                sam_amt = Convert.ToDecimal(columns[10].ToString().Trim());
                sam_amt = Math.Round(sam_amt, 2);
            }


            if (ddlState.SelectedValue != "0")
            {
                string sql = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Sample_Price) VALUES " +
                         " ( '" + ProductSlNO + "', 1, '" + ddlState.SelectedValue + "', '" + columns[1].Trim() + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "', " +
                         " '" + target_amt + "', '" + nsr_amt + "', getdate(), '" + div_code + "', getdate(),getdate(),'" + sam_amt + "' ) ";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Rate Uploaded Sucessfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select State');</script>");
            }

            k++;
            conn.Close();
        }
    }
    //protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlState.SelectedValue != "0")
    //    {
    //        lnkcount.Visible = true;
    //        FlUploadcsv.Visible = true;
    //        btnUPload.Visible = true;
    //    }
    //    else
    //    {
    //        lnkcount.Visible = false;
    //        FlUploadcsv.Visible = false;
    //        btnUPload.Visible = false;
    //    }
    //}
}