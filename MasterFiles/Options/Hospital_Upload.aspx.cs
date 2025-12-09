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

public partial class MasterFiles_Options_Hospital_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsListedDR = null;
    string sfCode = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
        }
        hHeading.InnerText = Page.Title;
    }

    private void ImportData()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {
                string FileName = FlUploadcsv.FileName;
                string path = string.Concat(Server.MapPath("~/Upload_Document/" + FlUploadcsv.FileName));
                OleDbConnection OleDbcon = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");

                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", OleDbcon);
                OleDbDataAdapter objAdapter1 = new OleDbDataAdapter(cmd);
                ds = new DataSet();
                objAdapter1.Fill(ds);
                Dt = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {

        }
    }
        //private void Deactivate()
        //{
        //    if (chkDeact.Checked == true)
        //    {
        //        conn.Open();
        //        string sql = "update Mas_Chemists set Chemists_Active_Flag = 1 where Chemists_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + ddlFilter.SelectedValue + "'";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.ExecuteNonQuery();
        //        conn.Close();
        //    }

        //}
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
            catch (Exception ex)
            {

            }
        }

        private void InsertData()
        {
            string StrSpec_Code = string.Empty;
            string StrTerritory_Code = string.Empty;
            string StrCat_Code = string.Empty;
            string StrCls_Code = string.Empty;
            string strUsername = string.Empty;
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow row = Dt.Rows[i];
                int columnCount = Dt.Columns.Count;
                string[] columns = new string[columnCount];
                for (int j = 0; j < columnCount; j++)
                {
                    columns[j] = row[j].ToString();
                }
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getSfCode_Upload(columns[1].Trim(),div_code);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strUsername = dsSalesForce.Tables[0].Rows[0][0].ToString();
                }
                Territory terr = new Territory();
                int terrcode = terr.GetterrCode();
                Hospital Hos = new Hospital();
                int Hospital_Code = Hos.GetHospitalCode();
                ListedDR lstDR = new ListedDR();
                //DataSet dslstDR = new DataSet();
                //dslstDR = lstDR.GetTerritory_Code(columns[8]);
                //if (dslstDR.Tables[0].Rows.Count > 0)
                //{
                //    StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                //}
                DataSet dslstDR = new DataSet();
                dslstDR = lstDR.GetTerritory_Upload(columns[12].Trim(), strUsername, div_code);
                if (dslstDR.Tables[0].Rows.Count > 0)
                {
                    StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                }
                conn.Open();
                if ((dsSalesForce.Tables[0].Rows.Count > 0))
                {
                    if (dslstDR.Tables[0].Rows.Count == 0)
                    {
                        string strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                      " SF_Code,Territory_Active_Flag,Created_date) " +
                      " VALUES('" + terrcode + "', '" + columns[12].Trim() + "' ,'1', null, " +
                      " '" + div_code + "', '" + strUsername + "', 0, getdate())";
                        SqlCommand cmd1 = new SqlCommand(strQry, conn);
                        cmd1.ExecuteNonQuery();
                    }

                    dslstDR = lstDR.GetTerritory_Upload(columns[12].Trim(), strUsername, div_code);
                    if (dslstDR.Tables[0].Rows.Count > 0)
                    {
                        StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                    }
                    string sql = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Address2,Hospital_Phone,Hospital_Mobile,Hospital_Contact,Hospital_PinCode,Hospital_EMail,Hospital_Address3,Hospital_Contact_Desgn, Territory_Code, " +
                         " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                         " VALUES('" + Hospital_Code + "', '" + columns[2].Trim() + "', '" + columns[3].Trim() + "', '" + columns[4].Trim() + "', '" + columns[5].Trim() + "', " +
                         " '" + columns[6].Trim() + "', '" + columns[7].Trim() + "', '" + columns[8].Trim() + "', '" + columns[9].Trim() + "', '" + columns[10].Trim() + "', '" + columns[11].Trim() + "',  '" + StrTerritory_Code + "', '" + div_code + "','" + strUsername + "', 0, getdate())";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Hospital Uploaded Sucessfully');</script>");
                  
                }
                conn.Close();
            }
        }
        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {

                Response.ContentType = "application/vnd.ms-excel";
                string fileName = Server.MapPath("~\\Document\\UPL_Hospital_Bulk.xlsx");
                Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Hospital_Bulk.xlsx");
                Response.TransmitFile(fileName);
                Response.End();

            }

            catch (Exception ex)
            {

                // lblMessage.Text = ex.Message;

            }

        }
        //private void FillTerr()
        //{
        //    ListedDR lstDR = new ListedDR();
        //    dsListedDR = lstDR.Terr_doc(ddlFilter.SelectedValue.ToString(), div_code);
        //    if (dsListedDR.Tables[0].Rows.Count > 0)
        //    {
        //        rptTerr.DataSource = dsListedDR;
        //        rptTerr.DataBind();

        //    }

        //    else
        //    {
        //        rptTerr.DataSource = dsListedDR;
        //        rptTerr.DataBind();

        //    }
        //}
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           
        }
       
    }
