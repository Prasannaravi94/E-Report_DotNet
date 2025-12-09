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

public partial class MasterFiles_Options_Primary_Upload_Screen : System.Web.UI.Page
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
    string str_StockCode = string.Empty;
    string str_StockName = string.Empty;
    string str_ProdName = string.Empty;
    Stockist objInput = new Stockist();
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
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
        string slNO = string.Empty;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        string ChkReord = "select * from Trans_Primary_Head where Trans_Month='" + ddlMonth.SelectedValue + "' AND Trans_Year ='" + ddlYear.SelectedValue + "' and Division_Code='" + div_code + "'";
        SqlCommand cmd;
        cmd = new SqlCommand(ChkReord, con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtR = new DataTable();
        da.Fill(dtR);

        if (dtR.Rows.Count > 0)
        {
            foreach (DataRow dtRow in dtR.Rows)
            {
                slNO += dtRow["Trans_sl_No"].ToString() + ",";
            }
            slNO = slNO.Remove(slNO.Length - 1);
            string Qry = "delete from Trans_Primary_Detail where Trans_sl_No in (" + slNO + ") and Division_Code='" + div_code + "'";
            cmd = new SqlCommand(Qry, con);
            // con.Open();
            int _res1 = cmd.ExecuteNonQuery();


            string Qry2 = "delete from Trans_Primary_Head where Trans_Month='" + ddlMonth.SelectedValue + "' AND Trans_Year ='" + ddlYear.SelectedValue + "' and Division_Code='" + div_code + "'";
            cmd = new SqlCommand(Qry2, con);
            // con.Open();
            int _res2 = cmd.ExecuteNonQuery();
        }
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();

            }

            SalesForce sf = new SalesForce();
            Username = "";

            string str = columns[3].ToString().Trim();

            String stok = (String)Dt.Rows[i][3].ToString().Trim();


            if (str == stok)
            {
                if (str != "")
                {
                    int output1 = objInput.RecordExistPrimaryHead(columns[3].Trim(), div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
                    if (output1 == 0)
                    {
                        Stockist sf1 = new Stockist();
                        dsSalesForce = sf1.Get_Stockist_Code_Primary(div_code, columns[3].ToString().Trim());
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            str_StockCode = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            str_StockName = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                            output = objInput.RecordHeadAdd_Primary_Kpi(columns[3].Trim(), str_StockCode, str_StockName.Trim(), div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, columns[1].Trim(), columns[2].Trim());
                        }
                    }
                    else
                    {
                        output = Convert.ToString(objInput.RecordExistPrimaryHead(columns[3].ToString().Trim(), div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue));
                    }
                    if (output != "0")
                    {
                        Stockist sf1 = new Stockist();
                        dsSalesForce = sf1.Get_Stockist_Code_Primary(div_code, columns[3].ToString().Trim());
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            str_StockCode = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            str_StockName = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            //}
                            ds = sf1.Get_Product_Code_Primary(div_code, columns[5].Trim());
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                str_ProdCode = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                str_ProdName = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();


                                //  objInput.RecordDetailsAddPrimary(output, columns[3].Trim(), str_ProdCode, str_ProdName, columns[5], columns[7], columns[6], columns[8], div_code, str_StockCode, columns[2]);
                                objInput.RecordDetailsAddPrimary_Kpi(output, columns[5].Trim(), str_ProdCode, str_ProdName, columns[7].Trim(), columns[9].Trim(), columns[8].Trim(), columns[10].Trim(), div_code, str_StockCode, str_StockName, columns[11].Trim(), columns[12].Trim(), columns[13].Trim(), columns[14].Trim(), columns[15].Trim(), columns[16].Trim(), columns[17].Trim(), columns[18].Trim());
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Primary Uploaded Successfully!');", true);
                            }
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
            string fileName = Server.MapPath("~\\Document\\Upl_Primary_Upload_Screen.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Upl_Primary_Upload_Screen.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}