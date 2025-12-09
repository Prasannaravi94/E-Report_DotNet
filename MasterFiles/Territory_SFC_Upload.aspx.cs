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
using System.Drawing.Imaging;
using DBase_EReport;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using ClosedXML;

public partial class MasterFiles_Territory_SFC_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsProduct = null;
    DataSet dslstSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    DataSet dslstCls = new DataSet();
    DataTable dt = new DataTable();
    string sfCode = string.Empty;
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    string Terr_Cat = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;

           
        }
        hHeading.InnerText = this.Page.Title;
    }

   
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();

        SqlCommand cmd = new SqlCommand("SFC_Upload", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Divcode", Convert.ToInt32(div_code));
       
        cmd.CommandTimeout = 150;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
      
        
        dt= dsts.Tables[0];
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(dt, "UPL_SFC_Master");
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"UPload_SFC.xlsx\"");
  
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    
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


            string connectionString = "";
            if (FlUploadcsv.HasFile)
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
                string fileLocation = Server.MapPath("~/Terr_SFC_Uplaod/" + fileName);
                FlUploadcsv.SaveAs(fileLocation);

                //Check whether file extension is xls or xlsx 

                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                //Create OleDB Connection and OleDb Command 

                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                DataSet dtExcelRecords = new DataSet();
                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                Dt = dtExcelRecords.Tables[0];
                con.Close();
              



            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InsertData()
    {
       
        
        DataSet dspsfc = new DataSet();
        DataSet dspterr = new DataSet();
        DataSet dspterr1=new DataSet();
        DataSet dssfcode = new DataSet();
        
        string territory_code = string.Empty;
        string sfcode = string.Empty;
        string terrcodecnt = string.Empty;
        string sfccodecnt = string.Empty;
        string frmhq = string.Empty;
        string tohq = string.Empty;
       
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString();
            }

            int terrcnt = 0;
            int sfccnt = 0;

            Distance_calculation TerrSFC = new Distance_calculation();
            //sf_code
            dssfcode=TerrSFC.GetSFCsfcodeUPL(columns[0].Trim(),div_code);
            if (dssfcode.Tables[0].Rows.Count > 0)
            {
                sfcode = dssfcode.Tables[0].Rows[0][0].ToString();
            }

            //territory_code
            dspterr = TerrSFC.GetCityTerritoryUPL(columns[6].Trim(), sfcode, div_code);
           // dspterr1 = TerrSFC.GetTerritoryUPL(columns[6].Trim(), sfcode, div_code);
            if (dspterr.Tables[0].Rows.Count > 0)
            {
                terrcnt = 1;
                terrcodecnt = dspterr.Tables[0].Rows[0][0].ToString();
            }
            //else
            //{
            //    if (dspterr1.Tables[0].Rows.Count > 0)
            //    {
            //        terrcnt = 1;
            //        terrcodecnt = dspterr1.Tables[0].Rows[0][0].ToString();
            //    }
            //}
            //distance check for city
            dspsfc = TerrSFC.GetSFCchk(columns[6].Trim(), sfcode, div_code);
            //dspsfc = TerrSFC.GetSFCUPL(columns[4].Trim(), columns[6].Trim(), sfcode, div_code);

            if (dspsfc.Tables[0].Rows.Count > 0)
            {
                sfccnt = 1;
               
            }

           
            //int TerrSlNO = TerrSFC.GetTerrCode();

            //territory_code = TerrSlNO.ToString();

            conn.Open();
            if (dssfcode.Tables[0].Rows.Count > 0)
            {
                //if (dssfcode.Tables[0].Rows.Count > 0 && terrcnt == 0 && columns[1].Trim() != "")
                //{

                //    string sql = "INSERT INTO Mas_Territory_Creation(Territory_Code,Territory_Name,Alias_Name,Territory_Cat,Division_Code," +
                //                    " SF_Code,Territory_Active_Flag,Created_date,OS_Distance,OSEX_Distance) " +
                //                    " VALUES('" + territory_code + "', '" + columns[1].Trim() + "', '" + columns[2].Trim() + "', '" + columns[5] + "','" + div_code + "', " +
                //                    " '" + sfcode + "', 0, getdate(),5,'" + columns[7].Trim() + "') ";
                //    SqlCommand cmd = new SqlCommand(sql, conn);
                //    cmd.ExecuteNonQuery();
                //}
                int number = 0;
                if (int.TryParse(columns[8].ToString().Trim(), out number))
                {
                    Terr_Cat = columns[8].ToString().Trim();
                }
                else
                {
                    Terr_Cat = "1";
                }
                string sql2 = "update Mas_Territory_Creation set territory_cat='" + Terr_Cat + "' where territory_name='" + columns[6].Trim() + "' and sf_code='" + sfcode + "' and territory_active_flag=0 ";
                SqlCommand cmd = new SqlCommand(sql2, conn);
                cmd.ExecuteNonQuery();
                if (dssfcode.Tables[0].Rows.Count > 0 && terrcnt != 0 && sfccnt == 0 && columns[7].Trim() != "0" && columns[7].Trim() != "")
                {
                    string sql1 = "INSERT INTO mas_distance_Fixation(sf_code,town_cat,Distance_In_Kms,division_code,from_hq,to_hq,created_Date,uplflg,flag,from_code,to_code,own_HQ,alias_name_one)" +
                    " VALUES('" + sfcode + "', '" + columns[8].Trim() + "', '" + columns[7].Trim() + "', '" + div_code + "', " +
                    " '" + columns[4].Trim() + "', '" + columns[6].Trim() + "', getdate(),1,0,'F','T','" + columns[5].ToString() + "','" + columns[6].ToString() + "') ";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();
                }
                dspsfc = TerrSFC.GetSFCUPL(columns[4].Trim(), columns[6].Trim(), columns[6].Trim(), sfcode, div_code);

                if (dspsfc.Tables[0].Rows.Count > 0)
                {

                    frmhq = dspsfc.Tables[0].Rows[0][0].ToString();
                    tohq = dspsfc.Tables[0].Rows[0][1].ToString();
                    int iReturn = -1;
                    iReturn = TerrSFC.updateFromtoCode(sfcode, div_code, columns[6].Trim());
                    int iReturn1 = -1;
                    iReturn1 = TerrSFC.updateFromtoCodeHQ(sfcode, div_code, columns[6].Trim());
                  
                }
                                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SFC Uploaded Sucessfully');</script>");
                conn.Close();
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Upload');</script>");
                conn.Close();
            }
        }
    }
}