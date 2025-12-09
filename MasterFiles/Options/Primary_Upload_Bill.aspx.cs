using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;


public partial class MasterFiles_Options_Primary_Upload_Bill : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();

    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
    string str_StockCode = string.Empty;
    string str_ProdCode = string.Empty;
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    DataSet dsSec = new DataSet();
    DataSet dsbill = new DataSet();
    DataTable dtcopy = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //  menu1.FindControl("btnBack").Visible = false;

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
            hHeading.InnerText = Page.Title;
        }
    }
    private void InsertData()
    {
        // string sf_code = string.Empty;
        string sf_Username = string.Empty;
        string strsfcode = string.Empty;
        string Strtype = string.Empty;
        string Pool_Name = string.Empty;
        string Pool_NameNew = string.Empty;
        Stockist st = new Stockist();
        dtcopy = Dt.Clone();
        // dsbill = st.Primary_Bill_Exist(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        // if (dsbill.Tables[0].Rows.Count > 0)
        // {
        //dsSec = st.getPrimary_Exist(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        //if (dsSec.Tables[0].Rows.Count > 0)
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Secondary Sale has been Entered.');</script>");
        //}
        //else
        //{
        conn.Open();
        if (rdolstrpt.SelectedValue == "1")
        {
            string sql2 = "delete from  Primary_Bill where  Division_Code = '" + div_code + "' and Invoice_Month = '" + ddlMonth.SelectedValue + "' and Invoice_Year = '" + ddlYear.SelectedValue + "' ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();
        }

        for (int i = Dt.Rows.Count - 1; i >= 0; i--)
        {
            if (Dt.Rows[i][1] == DBNull.Value)
            {
                Dt.Rows[i].Delete();
            }
        }
        Dt.AcceptChanges();


        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();
            }

            Stockist sf2 = new Stockist();
            DateTime dtInv_dt = new DateTime();

            string dtIn = "";
            if (columns[2] != "")
            {
                dtInv_dt = Convert.ToDateTime(columns[2].Trim());
                dtIn = dtInv_dt.Month + "-" + dtInv_dt.Day + "-" + dtInv_dt.Year;
            }

            dsSalesForce = sf2.Get_Stockist_Code_ERP(div_code, columns[3].ToString().Trim());
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                str_StockCode = dsSalesForce.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                foreach (DataRow dr in Dt.Rows)
                {
                    if (Dt.Rows[i] == dr)
                    {
                        dtcopy.Rows.Add(dr.ItemArray);
                    }
                }
            }
            ds = sf2.Get_Product_Code_ERP(div_code, columns[5].ToString().Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                str_ProdCode = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                foreach (DataRow dr in Dt.Rows)
                {
                    if (Dt.Rows[i] == dr)
                    {
                        dtcopy.Rows.Add(dr.ItemArray);
                    }
                }
            }
            div_code = Session["div_code"].ToString();

            Stockist objStock = new Stockist();
            int SlNo = objStock.GetPrimary_Bill();

            if (columns[1] != "")
            {
                if (dsSalesForce.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //     if (dsSalesForce.Tables[0].Rows.Count > 0 )
                {
                    string sql = "INSERT INTO [dbo].[Primary_Bill](SL_No,Inv_No,Inv_Date,Stockist_Code,Stockist_ERP_Code,Product_ERP_Code,Product_Name,Batch_No " +
                                 ",Expiry_Month_Year,Sale_Qty,Free_Qty,Sale_Rate,Sale_Value,Discount,Net_Amount,Completed_Flag " +
                                 ",Pack,Invoice_Month,Invoice_Year,Division_Code,Created_Date,Stockist_Name,Product_Code,Return_Qty,Value,Free_Val,Free_Replace_Qty,Free_Replace_Val) VALUES('" + SlNo + "','" + columns[1].Trim() + "','" + dtIn + "','" + str_StockCode + "','" + columns[3].Trim() + "', " +
                                 " '" + columns[5] + "','" + columns[6] + "','" + columns[8] + "','" + columns[9] + "','" + columns[10] + "' " +
                                 ",'" + columns[11] + "','" + columns[12] + "'  ,'" + columns[13] + "' ,'" + columns[14] + "','" + columns[15] + "' ,0,'" + columns[7] + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + columns[4] + "','" + str_ProdCode + "','" + columns[16] + "' ,'" + columns[17] + "','" + columns[18] + "','" + columns[19] + "','" + columns[20] + "'   )";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
                }

            }
            //if(columns[1] == "")
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
            //    break;
            //}

        }
        // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
        conn.Close();


        if (dtcopy.Rows.Count > 0)
        {
            pnlprimary.Visible = true;
            grdPrimary.DataSource = dtcopy;
            grdPrimary.DataBind();
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            // Render grid view control.
            grdPrimary.RenderControl(htw);
            //User objUser = new User();
            string filePath = Server.MapPath("~/Not_Upl_Document/");
            string fileName = "" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + "_" + div_code + ".xls";

            // Write the rendered content to a file.
            string renderedGridView = sw.ToString();
            System.IO.File.WriteAllText(filePath + fileName, renderedGridView);
        }
        else
        {

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            grdPrimary.RenderControl(htw);
            //User objUser = new User();
            string filePath = Server.MapPath("~/Not_Upl_Document/");
            string fileName = "" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + "_" + div_code + ".xls";

            // Write the rendered content to a file.
            string renderedGridView = sw.ToString();
            System.IO.File.WriteAllText(filePath + fileName, renderedGridView);
            //  File.Delete(filePath);
            pnlprimary.Visible = false;

        }
    }
    private void ImporttoDatatable()
    {
        if (FlUploadcsv.HasFile)
        {

            string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);

            // if (!System.IO.File.Exists(excelPath))
            //  {
            FlUploadcsv.SaveAs(excelPath);
            //  }
            //  else
            //  {
            // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Selected File is Already Exists')</script>");
            // }

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

            //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0;";

            string connStr = "";

            if (Path.GetExtension(excelPath) == ".xls")
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (Path.GetExtension(excelPath) == ".xlsx")
            {
                connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
            }


            using (OleDbConnection excel_con = new OleDbConnection(connStr))
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

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        ImporttoDatatable();
        InsertData();
    }
    //protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    //{

    //}
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Upl_Primary_Bill.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Upl_Primary_Bill.xls");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
    protected void lnlnot_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Not_Upl_Document\\" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + "_" + div_code + ".xls");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + "_" + div_code + ".xls");
                Response.TransmitFile(fileName);
                Response.End();
            }

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}