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

public partial class MasterFiles_Options_Gift_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();

    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
    
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //  sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillReporting();           
        }
        hHeading.InnerText = Page.Title;
    }
    private void InsertData()
    {
        // string sf_code = string.Empty;
    
        string StrBrd_Code = string.Empty;
        string strState = string.Empty;
        string stateName = string.Empty;
        string subdivCode = string.Empty;
        string SubName = string.Empty;     
        DataSet dsprodbrd = new DataSet();
        DataSet dsState = new DataSet();
      
        string Product_detail_cd = string.Empty;
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();

            }
          
            conn.Open();
            Input_New inp = new Input_New();
            int input_code = inp.GetInput_Code();
            Product pro = new Product();
            dsprodbrd = pro.getProductBrand_UP(div_code);
            if (dsprodbrd.Tables[0].Rows.Count > 0)
            {
                StrBrd_Code = dsprodbrd.Tables[0].Rows[0][0].ToString();

            }


            State st = new State();

            stateName = "";
            string[] strDivSplit = columns[6].Split('/');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsState = st.getState_Code(strdiv);
                    if (dsState.Tables[0].Rows.Count > 0)
                    {
                        strState = dsState.Tables[0].Rows[0][0].ToString();
                    }
                    stateName += strState + ',';
                }
            }



            SubDivision sub = new SubDivision();
            DataSet dsSubdiv = new DataSet();

            SubName = "";
            string[] strsubSplit = columns[7].Split('/');
            foreach (string strsub in strsubSplit)
            {
                if (strsub != "")
                {

                    dsSubdiv = sub.GetSubdiv_Code(strsub, div_code);
                    if (dsSubdiv.Tables[0].Rows.Count > 0)
                    {
                        subdivCode = dsSubdiv.Tables[0].Rows[0][0].ToString();
                    }
                    SubName += subdivCode + ',';
                }
            }
            if (columns[1].Trim() != "")
            {
                DateTime dteff = new DateTime();
                DateTime dtefft = new DateTime();
                string dtEfffrom = "";
                string dtEffto = "";

                if (columns[4].Trim() != "")
                {
                    dteff = Convert.ToDateTime(columns[4].Trim());
                    dtEfffrom = dteff.Month + "-" + dteff.Day + "-" + dteff.Year;
                }
                if (columns[5].Trim() != "")
                {

                    dtefft = Convert.ToDateTime(columns[5].Trim());
                    dtEffto = dtefft.Month + "-" + dtefft.Day + "-" + dtefft.Year;
                }
                string sql = "INSERT INTO Mas_Gift(Gift_Code,Gift_SName,Gift_Name,Gift_Type,Gift_Value,Gift_Effective_From,Gift_Effective_To,Division_Code,State_Code,subdivision_code,Product_Brd_Code, Created_Date,Gift_Active_flag,LastUpdt_Date)" +
                                 "values('" + input_code + "','" + columns[1].Trim() + "','" + columns[2].Trim() + "', '4' , '" + columns[3].Trim() + "' , " +
                                 " '" + dtEfffrom + "' ,'" + dtEffto + "', " + div_code + "," +
                                 " '" + stateName + "','" + SubName + "','" + StrBrd_Code + "', getdate(), '0',getdate()) ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
         
              
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Gift Uploaded Sucessfully');</script>");
            }
            
         
            conn.Close();
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
    protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Upl_Gift_Master.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Upl_Gift_Master.xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }


        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }
}