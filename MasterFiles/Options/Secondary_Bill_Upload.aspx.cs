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

public partial class MasterFiles_Options_Secondary_Bill_Upload : System.Web.UI.Page
{

    string div_code = string.Empty;
    string div_name = string.Empty;
    string sf_Emp_id= string.Empty;
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    DataSet dsSalesForce = new DataSet();
    DataTable dtcopy = new DataTable();
    string str_EmpCode = string.Empty;
    string str_EmpName = string.Empty;
    string str_ProdCode =string.Empty;
    string str_ProdName = string.Empty;
    DataSet ds1 = new DataSet();
    DataSet ds2 = new DataSet();
     string str_chemName=string.Empty;
     string str_chem_Erp_code = string.Empty;
    string str_HQ_Code=string.Empty;
    string str_HQName = string.Empty;
    string str_ProdERPcode = string.Empty;
    string str_doc_code=string.Empty;
     string str_docName=string.Empty;
     string str_docERPcode = string.Empty;
     string str_Sfcode = string.Empty;
     string str_chemcode = string.Empty;
                

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        div_name = Session["div_name"].ToString();
        SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        DataSet ds = new DataSet();
        DataTable Dt = new DataTable();
         if (!Page.IsPostBack)
         {
             menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;

         }
    }
   
    protected void btnUpload_Click(object sender, EventArgs e)
    {

       ImporttoDatatable();
       InsertData();
    }

    private void InsertData()
    {
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();

            }
            Stockist sf1 = new Stockist();
            dsSalesForce = sf1.Get_Empcode_sale(columns[3].ToString().Trim());
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                str_EmpCode = dsSalesForce.Tables[0].Rows[0]["sf_emp_id"].ToString();
                str_EmpName = dsSalesForce.Tables[0].Rows[0]["sf_name"].ToString();
                str_HQ_Code = dsSalesForce.Tables[0].Rows[0]["SF_Cat_Code"].ToString();
                str_HQName = dsSalesForce.Tables[0].Rows[0]["Sf_HQ"].ToString();
                str_Sfcode = dsSalesForce.Tables[0].Rows[0]["Sf_Code"].ToString();


            }
           
            Stockist sf2 = new Stockist();
            ds = sf2.Get_Product_Code_sale(columns[14].ToString().Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                str_ProdCode = ds.Tables[0].Rows[0]["Product_Detail_Code"].ToString();
                str_ProdName = ds.Tables[0].Rows[0]["Product_Detail_Name"].ToString();
                str_ProdERPcode = ds.Tables[0].Rows[0]["sale_Erp_Code"].ToString();
            }
           
            Stockist sf3 = new Stockist();
            ds1 = sf3.Get_chemist_Code_sale(columns[7].ToString().Trim());
            if (ds1.Tables[0].Rows.Count > 0)
            {
                str_chem_Erp_code = ds1.Tables[0].Rows[0]["Chemist_ERP_Code"].ToString();
                str_chemName = ds1.Tables[0].Rows[0]["Chemists_Name"].ToString();
                str_chemcode = ds1.Tables[0].Rows[0]["Chemists_Code"].ToString();
                
            }
           

            //Stockist sf4 = new Stockist();
            //ds2 = sf4.Get_Doctor_Code_sale(columns[10].ToString().Trim());
            //if (ds2.Tables[0].Rows.Count > 0)
            //{
            //    str_doc_code = ds2.Tables[0].Rows[0]["ListedDrCode"].ToString();
            //    str_docName = ds2.Tables[0].Rows[0]["ListedDr_Name"].ToString();
            //    str_docERPcode= ds2.Tables[0].Rows[0]["Doctor_ERP_Code"].ToString();

               
            //}
           

            Stockist objStock = new Stockist();
            int SlNo = objStock.GetSecondarysale();

            SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            conn.Open();
            //if (dsSalesForce.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            //{
                if (columns[3] != "" && columns[14] != "" && columns[7] != "" )
                {
                //string sql = "INSERT INTO Trans_Secondary_Bill(Sl_No,Division_Name,Division_code,Sale_Date,Emp_Code," +
                //              "Emp_Name,SF_Code,HQ_Code,HQ_Name,Chemist_ERP_Code,Chemist_Name,Chemist_Code,Doctor_Name," +
                //              "Doctor_ERP_Code,Doctor_Code,Sale_Qty,Sale_Value,Product_Name,Product_Code,Product_ERP_Code)  VALUES('" + SlNo + "'," +
                //              "'" + div_name + "','" + div_code + "','" + columns[2].Trim() + "','" + str_EmpCode + "', " +
                //              "'" + str_EmpName + "', '" + str_Sfcode + "','" + str_HQ_Code + "','" + str_HQName + "','" + str_chem_Erp_code + "' " +
                //              ",'" + str_chemName + "','" + str_chemcode + "'  ,'" + str_docName + "' ,'" + str_docERPcode + "', " +
                //              "'" + str_doc_code + "','" + columns[11].Trim() + "','" + columns[12].Trim() + "','" + str_ProdName + "','" + str_ProdCode + "', " +
                //              "'" + str_ProdERPcode + "')";
                //SqlCommand cmd = new SqlCommand(sql, conn);

                string sql = "INSERT INTO Trans_Secondary_Bill(Sl_No,Division_Name,Division_code, " +
                                "Sale_Date,Emp_Code,Emp_Name," +
                                "SF_Code,HQ_Code,HQ_Name," +
                                "Chemist_ERP_Code,Chemist_Name,Chemist_Code," +
                                "Doctor_Name,Doctor_ERP_Code," +
                                "Sale_Qty,Sale_Value,Product_Name," +
                                "Product_Code,Product_ERP_Code)  VALUES('" + columns[0].Trim() + "','" + div_name + "','" + div_code + "'," +
                                "'" + columns[2].Trim() + "','" + columns[3].Trim() + "','" + columns[4].Trim() + "', " +
                                " '" + str_Sfcode + "','" + columns[5].Trim() + "','" + columns[6].Trim() + "', " +
                                " '" + columns[7].Trim() + "','" + columns[8].Trim() + "','" + str_chemcode + "', " +
                                "'" + columns[9].Trim() + "' ,'" + columns[10].Trim() + "', " +
                                "'" + columns[11].Trim() + "','" + columns[12].Trim() + "','" + columns[13].Trim() + "','" + columns[14].Trim() + "', " +
                                "'" + str_ProdERPcode + "')";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                    conn.Close();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sales Bill Uploaded Sucessfully');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
                }
                
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
            //}
           
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Sales_Bill.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Sales_Bill.xls");
            Response.TransmitFile(fileName);
            Response.End();

        }
        catch (Exception ex)
        {
        }
    }
}