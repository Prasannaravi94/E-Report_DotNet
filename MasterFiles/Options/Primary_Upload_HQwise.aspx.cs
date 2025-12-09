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

public partial class MasterFiles_Options_Primary_Upload_HQwise : System.Web.UI.Page
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

        string StrSf_Code = string.Empty;
        string Stockist_Code = string.Empty;
        string Stockist_Designation = string.Empty;
        Stockist st = new Stockist();
        dtcopy = Dt.Clone();
        dsbill = st.Primary_Bill_Exist(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsbill.Tables[0].Rows.Count > 0)
        {
            //dsSec = st.getPrimary_Exist(div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
            //if (dsSec.Tables[0].Rows.Count > 0)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Secondary Sale has been Entered.');</script>");
            //}
            //else
            //{
            conn.Open();
            string sql2 = "delete from  Primary_Bill where  Division_Code = '" + div_code + "' and Invoice_Month = '" + ddlMonth.SelectedValue + "' and Invoice_Year = '" + ddlYear.SelectedValue + "' ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.ExecuteNonQuery();
            conn.Close();
            //for (int i = 0; i < Dt.Rows.Count; i++)
            //{
            //    DataRow row = Dt.Rows[i];
            //    int columnCount = Dt.Columns.Count;
            //    string[] columns = new string[columnCount];
            //    for (int j = 0; j < columnCount; j++)
            //    {
            //        columns[j] = row[j].ToString().Trim();
            //    }


            //    Stockist sf2 = new Stockist();
            //    DateTime dtInv_dt = new DateTime();
            //    string dtIn = "";

            //    if (columns[2] != "")
            //    {
            //        dtInv_dt = Convert.ToDateTime(columns[2].Trim());
            //        dtIn = dtInv_dt.Month + "-" + dtInv_dt.Day + "-" + dtInv_dt.Year;
            //    }
            //    div_code = Session["div_code"].ToString();

            //    ListedDR lstDR = new ListedDR();
            //    DataSet dsHQ = lstDR.get_SFHQCode(columns[3].Trim());



            //    //DataSet dsStk = lstDR.get_Stk_SFHQCode(StrSf_Code, div_code);
            //    if (dsHQ.Tables[0].Rows.Count > 0)
            //    {
            //        StrSf_Code = dsHQ.Tables[0].Rows[0][0].ToString();
            //        Stockist_Code = dsHQ.Tables[0].Rows[0][2].ToString();
            //        Stockist_Designation = dsHQ.Tables[0].Rows[0][4].ToString();
            //    }

            //    //dsSalesForce = sf2.Get_Stockist_Code_ERP(div_code, columns[3].ToString().Trim());
            //    //if (dsSalesForce.Tables[0].Rows.Count > 0)
            //    //{
            //    //    str_StockCode = dsSalesForce.Tables[0].Rows[0][0].ToString();
            //    //}
            //    else
            //    {
            //        foreach (DataRow dr in Dt.Rows)
            //        {
            //            if (Dt.Rows[i] == dr)
            //            {

            //                dtcopy.Rows.Add(dr.ItemArray);
            //            }
            //        }
            //    }
            //    ds = sf2.Get_Product_Code_ERP(div_code, columns[7].ToString().Trim());
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        str_ProdCode = ds.Tables[0].Rows[0][0].ToString();
            //    }
            //    else
            //    {
            //        foreach (DataRow dr in Dt.Rows)
            //        {
            //            if (Dt.Rows[i] == dr)
            //            {
            //                dtcopy.Rows.Add(dr.ItemArray);
            //            }
            //        }
            //    }

            //    Stockist objStock = new Stockist();
            //    int SlNo = objStock.GetPrimary_Bill();

            //    if (columns[1] != "")
            //    {
            //        if (ds.Tables[0].Rows.Count > 0 && dsHQ.Tables[0].Rows.Count > 0)
            //        //     if (dsSalesForce.Tables[0].Rows.Count > 0 )
            //        {
            //            string sql = "insert into primary_bill (SL_No,Inv_No,Inv_Date,sf_cat_code,Stockist_Code,Stockist_ERP_Code,Product_ERP_Code,Product_Name,Pack,Batch_No, " +
            //           " Expiry_Month_Year, Sale_Qty,Free_Qty,Sale_Rate,Sale_Value,Discount,Net_Amount,Completed_Flag, " +
            //           " Return_Qty,Value,Free_Val,Free_Replace_Qty,Free_Replace_Val,Invoice_Month,Invoice_Year,Division_Code,Created_Date,Product_Code)  " +
            //           " VALUES('" + SlNo + "','" + columns[1].Trim() + "','" + dtIn + "','" + columns[3].Trim() + "','" + Stockist_Code + "','" + Stockist_Designation + "','" + columns[7].Trim() + "','" + columns[8].Trim().Replace("'", " ") + "','" + columns[9].Trim().Replace("'", " ") + "','" + columns[10].Trim() + "', " +
            //           " '" + columns[11].Trim() + "','" + columns[12].Trim() + "','" + columns[13].Trim() + "','" + columns[14].Trim() + "','" + columns[15].Trim() + "','" + columns[16].Trim() + "','" + columns[17].Trim() + "',0, " +
            //           "  '" + columns[18].Trim() + "','" + columns[19].Trim() + "','" + columns[20].Trim() + "','" + columns[21].Trim() + "','" + columns[22].Trim() + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + str_ProdCode + "'  )";
            //            SqlCommand cmd = new SqlCommand(sql, conn);
            //            cmd.ExecuteNonQuery();
            //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
            //        }
            //        else
            //        {
            //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
            //        }
            //    }

            //}
            ////ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
            //conn.Close();
        }
        // }
        // else
        {
            conn.Open();
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


                Stockist objStock = new Stockist();
                int SlNo = objStock.GetPrimary_Bill();
                DateTime dtInv_dt = new DateTime();

                string dtIn = "";
                if (columns[0] != "" && columns[1] != "" && columns[2] != "" && columns[3] != "" && columns[4] != "" && columns[7] != "")
                {

                    if (columns[2] != "")
                    {
                        dtInv_dt = Convert.ToDateTime(columns[2].Trim());
                        dtIn = dtInv_dt.Month + "-" + dtInv_dt.Day + "-" + dtInv_dt.Year;
                    }
                    div_code = Session["div_code"].ToString();

                    ListedDR lstDR = new ListedDR();
                    DataSet dsHQ = lstDR.get_SFHQCode(columns[3].Trim());

                    //DataSet dsStk = lstDR.get_Stk_SFHQCode(StrSf_Code, div_code);
                    if (dsHQ.Tables[0].Rows.Count > 0)
                    {
                        StrSf_Code = dsHQ.Tables[0].Rows[0][0].ToString();
                        Stockist_Code = dsHQ.Tables[0].Rows[0][2].ToString();
                        Stockist_Designation = dsHQ.Tables[0].Rows[0][4].ToString();
                    }
                    //dsSalesForce = sf2.Get_Stockist_Code_ERP(div_code, columns[3].ToString().Trim());
                    //if (dsSalesForce.Tables[0].Rows.Count > 0)
                    //{
                    //    str_StockCode = dsSalesForce.Tables[0].Rows[0][0].ToString();
                    //}
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
                    ds = sf2.Get_Product_Code_ERP(div_code, columns[7].ToString().Trim());
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

                    if (columns[1].Trim() != "")
                    {
                        if (ds.Tables[0].Rows.Count > 0 && dsHQ.Tables[0].Rows.Count > 0)
                        {
                            string sql = "insert into primary_bill (SL_No,Inv_No,Inv_Date,sf_cat_code,Stockist_Code,Stockist_ERP_Code,Product_ERP_Code,Product_Name,Pack,Batch_No, " +
                            " Expiry_Month_Year, Sale_Qty,Free_Qty,Sale_Rate,Sale_Value,Discount,Net_Amount,Completed_Flag, " +
                            " Return_Qty,Value,Free_Val,Free_Replace_Qty,Free_Replace_Val,Invoice_Month,Invoice_Year,Division_Code,Created_Date,Product_Code)  " +
                            " VALUES('" + SlNo + "','" + columns[1].Trim() + "','" + dtIn + "','" + columns[3].Trim() + "','" + Stockist_Code + "','" + Stockist_Designation + "','" + columns[7].Trim() + "','" + columns[8].Trim().Replace("'", " ") + "','" + columns[9].Trim().Replace("'", " ") + "','" + columns[10].Trim() + "', " +
                            " '" + columns[11].Trim() + "','" + columns[12].Trim() + "','" + columns[13].Trim() + "','" + columns[14].Trim() + "','" + columns[15].Trim() + "','" + columns[16].Trim() + "','" + columns[17].Trim() + "',0, " +
                            "  '" + columns[18].Trim() + "','" + columns[19].Trim() + "','" + columns[20].Trim() + "','" + columns[21].Trim() + "','" + columns[22].Trim() + "','" + ddlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "','" + div_code + "',getdate(),'" + str_ProdCode + "'  )";
                            SqlCommand cmd = new SqlCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid Details');</script>");
                        }
                    }
                }
                else
                {
                    i = Dt.Rows.Count;
                }
            }


            conn.Close();
            // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Primary Bill Uploaded Sucessfully');</script>");
        }
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
            string filePath = Server.MapPath("~/Not_Upl_Document_HQ/");
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
            string filePath = Server.MapPath("~/Not_Upl_Document_HQ/");
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
        try
        {

            if (FlUploadcsv.HasFile)
            {

                string excelPath = Server.MapPath("~/Upload_Document_HQ/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);
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
            string fileName = Server.MapPath("~\\Document\\UPL_Primary_Bill_HQwise.xls");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Primary_Bill_HQwise.xls");
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
            string fileName = Server.MapPath("~\\Not_Upl_Document_HQ\\" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue + "_" + div_code + ".xls");
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