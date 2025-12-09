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


public partial class MasterFiles_Options_Input_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string output = string.Empty;
    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
    string str_ProdCode = string.Empty;
    InputDespatch objInput = new InputDespatch();
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = Page.Title;

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;

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
    private void InsertData()
    {
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
            //Stockist objStock = new Stockist();
            //int Stockist_Code = objStock.GetStockistCode();
            SalesForce sf = new SalesForce();
            //dsSalesForce = sf.GetSFCode_Upload(columns[1]);
            //if (dsSalesForce.Tables[0].Rows.Count > 0)
            //{
            //    strsfcode = dsSalesForce.Tables[0].Rows[0][0].ToString();
            //}
            Username = "";
            string[] strDivSplit = columns[1].Split('/');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsSalesForce = sf.getEmp_code(strdiv, div_code);
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        sf_Username = dsSalesForce.Tables[0].Rows[0][0].ToString();
                    }
                    Username += sf_Username + ',';
                }
            }
           
            string[] strEmp = Username.Split(',');
            foreach (string str in strEmp)
            {
                if (str != "")
                {
                    int output1 = objInput.RecordExistInputDespatchHead(str, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                    if (output1 == 0)
                    {
                        output = objInput.RecordHeadAdd_Upl(str, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                    }
                    else
                    {
                        output = output1.ToString();
                    }
                    if (output != "0" || output != "")
                    {
                        InputDespatch sf1 = new InputDespatch();
                        dsSalesForce = sf1.Get_Input_Code(div_code, columns[3]);
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            str_ProdCode = dsSalesForce.Tables[0].Rows[0][0].ToString();

                            objInput.RecordDetailsAddinput(output, str, div_code, columns[3], Convert.ToInt32(columns[6]), str_ProdCode);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Input Despatched Successfully!');", true);
                        }
                    }

                }
            }
        }
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
        }
        catch (Exception ex)
        {

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
            string fileName = Server.MapPath("~\\Document\\Upl_Input_Master.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Upl_Input_Master.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}