using System;
using System.Collections.Generic;
using System.Collections;
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
using ClosedXML.Excel;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Text.RegularExpressions;
using ClosedXML;
using System.Text.RegularExpressions;
using ClosedXML;
using System.Web.Script.Serialization;
using System.Drawing;
public partial class MasterFiles_Options_Activity_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsproduct = new DataSet();
    string output = string.Empty;
    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
    string str_ProdCode = string.Empty;
    DataTable dtcopy = new DataTable();
    DataTable dtc = new DataTable();
    DataTable actcode = new DataTable();
    SampleDespatch objSample = new SampleDespatch();
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            menu1.FindControl("btnBack").Visible = false;
        }
    }
    private void InsertData()
    {
        // string sf_code = string.Empty;
        int dtcnt = 0;

        if (Dt == null)
        {
            dtcnt = 0;
        }
        else
        {
            dtcnt = Dt.Rows.Count;
        }
        if (dtcnt == 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Records are not available');window.location='Activity_Upload.aspx'</script>");
            FlUploadcsv.Focus();
        }
        else
        {
            string sf_Username = string.Empty;
            string strsfcode = string.Empty;
            string Strtype = string.Empty;
            string Pool_Name = string.Empty;
            string Pool_NameNew = string.Empty;
            Distance_calculation dcmas = new Distance_calculation();
            //Dt = dcmas.CHKEXCELDT();
            dtc = Dt.Copy();
            dtc.Columns.Add("Excel_Row_No", typeof(int));
            dtc.Columns.Add("Remarks", typeof(String));
            dtcopy = Dt.Clone();
            dtcopy.Columns.Add("Excel_Row_No", typeof(int));
            dtcopy.Columns.Add("Remarks");
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getEmp_code(div_code);
            DataTable sfemp = dsSalesForce.Tables[0];
            SampleDespatch sf1 = new SampleDespatch();

            string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd1 = new SqlCommand("select Activity_ID,Activity_S_Name,Activity_Name from Mas_Upload_activity " +
                            "  where Division_code = '" + div_code + "' and  Active_Flag=0 ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    actcode = ds.Tables[0];
                }
            }
            Dt.Columns.Add("remarks");


            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                DataRow ro = dtc.Rows[i];

                string sactcode = string.Empty;
                string sfempid = string.Empty;
                string adt = string.Empty;
                string remarks = string.Empty;
                string act_id = string.Empty;
                string act_sname = string.Empty;
                string act_name = string.Empty;
                string date = @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$";
                string SFcode = "";
                DataRow row = Dt.Rows[i];
                int rowno = i + 2;
                int columnCount = Dt.Columns.Count;
                int columnCount1 = Dt.Columns.Count;
                string[] columns = new string[columnCount];
                DataRow[] rows1;
                DataRow[] rows;
                bool isValid = true;
                for (int j = 0; j < columnCount; j++)
                {
                    columns[j] = row[j].ToString().Trim();
                    DataColumn col = Dt.Columns[j];
                    if (Dt.Columns[j].ColumnName == "Employee_Id")
                    {
                        dsSalesForce = sf.getEmp_code(columns[j].Trim().ToString(), div_code);
                        if (dsSalesForce.Tables[0].Rows.Count > 0)
                        {
                            SFcode = dsSalesForce.Tables[0].Rows[0][0].ToString();
                        }

                    }
                    else if (Dt.Columns[j].ColumnName == "Activity id")
                    {
                        string constr2 = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(constr2))
                        {
                            SqlCommand cmd1 = new SqlCommand("select Activity_ID,Activity_S_Name,Activity_Name from Mas_Upload_activity " +
                                        "  where Division_Code = '" + div_code + "' and  Active_Flag=0 and Activity_S_Name='" + columns[j].Trim() + "' ", con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd1);
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                act_sname = ds.Tables[0].Rows[0]["Activity_S_Name"].ToString();

                            }
                        }
                    }
                    else if (Dt.Columns[j].ColumnName == "Date of Activity Approval")
                    {
                        if (columns[j].Trim().ToString() != "")
                        {
                            string dd = columns[j].Trim().ToString().Replace("00:00:00", "");
                            isValid = Regex.IsMatch(dd.Trim(), date);
                        }

                    }

                }
                if (SFcode == "" || act_sname == "" || (isValid == false))
                {
                    if (act_sname == "")
                    {
                        sactcode = "Activity Id Not Matched" + '/';
                    }
                    //if (columns[2].Trim() == "")
                    //{
                    //    sfempid = "Employee code Empty" + '/';
                    //}
                    if (SFcode == "")
                    {
                        sfempid = "Employee code Not Matched" + '/';
                    }
                    if (isValid == false)
                    {
                        adt = "Date of Activity Approval Format Wrong" + '/';
                    }
                    remarks = sfempid + sactcode;
                    ro[dtc.Columns.Count - 2] = rowno;
                    ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                    foreach (DataRow dr in dtc.Rows)
                    {

                        if (dtc.Rows[i] == dr)
                        {

                            dtcopy.Rows.Add(dr.ItemArray);

                        }
                    }
                }


            }
            if (dtcopy.Rows.Count > 0)
            {
                pnlprimary.Visible = true;
                lnlnot.Focus();
                grdActivity.DataSource = dtcopy;
                grdActivity.DataBind();
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                // Render grid view control.
                grdActivity.RenderControl(htw);
                //User objUser = new User();
                string filePath = Server.MapPath("~/Not_Upl_Activity/");
                //   string fileName = ("PrimaryBill_" + (System.DateTime.Now.Date).ToString() + "_" + div_code + ".xls");
                string fileName = ("Activityupld_" + div_code + ".xls");

                // Write the rendered content to a file.
                string renderedGridView = sw.ToString();
                System.IO.File.WriteAllText(filePath + fileName, renderedGridView);
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload Not Successful. Kindly download the below link for Error Records.');</script>");
                int countCol1 = Dt.Columns.Count;
                // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Uploaded.Check the Error list');</script>");
            }
            else
            {
                pnlprimary.Visible = false;

                using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;

                    // Start a local transaction.
                    transaction = connection.BeginTransaction("SampleTransaction");

                    // Must assign both transaction object and connection
                    // to Command object for a pending local transaction
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {
                        conn.Open();
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            DataRow row = Dt.Rows[i];
                            int columnCount = Dt.Columns.Count;
                            string[] columns = new string[columnCount];
                            //   strSf_Code=columns[2].Trim();
                            for (int j = 0; j < columnCount; j++)
                            {
                                columns[j] = row[j].ToString().Trim();
                            }
                            string act_no = string.Empty;
                            string act_name = string.Empty;
                            string act_sname = string.Empty;
                            string constr2 = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                            using (SqlConnection con = new SqlConnection(constr2))
                            {
                                SqlCommand cmd1 = new SqlCommand("select Activity_ID,Activity_S_Name,Activity_Name from Mas_Upload_activity " +
                                            "  where Division_Code = '" + div_code + "' and  Active_Flag=0 and Activity_S_Name='" + columns[1].Trim() + "' ", con);
                                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                                DataSet ds = new DataSet();
                                da.Fill(ds);

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    act_no = ds.Tables[0].Rows[0][0].ToString();
                                    act_sname = ds.Tables[0].Rows[0][1].ToString();

                                    act_name = ds.Tables[0].Rows[0][2].ToString();

                                }
                            }
                            string Sfcode = string.Empty;
                            dsSalesForce = sf.getEmp_code(columns[2].Trim(), div_code);
                            if (dsSalesForce.Tables[0].Rows.Count > 0)
                            {
                                Sfcode = dsSalesForce.Tables[0].Rows[0][0].ToString();
                            }

                            command.CommandText = "SELECT isnull(Max(SL_No)+1,'1') SL_No from Trans_Upload_Activity";

                            SqlDataAdapter daEx = new SqlDataAdapter(command);
                            DataSet dsact = new DataSet();
                            daEx.Fill(dsact);
                            string SL_No = dsact.Tables[0].Rows[0]["SL_No"].ToString();
                            DateTime dtapp = new DateTime();

                            string dtappdate = "";
                            dtapp = Convert.ToDateTime(columns[0].Replace("00:00:00", ""));
                            dtappdate = dtapp.Month + "-" + dtapp.Day + "-" + dtapp.Year;

                            DateTime dtact = new DateTime();
                            string dtactdate = "";
                            dtact = Convert.ToDateTime(columns[4].Replace("00:00:00", ""));
                            dtactdate = dtact.Month + "-" + dtact.Day + "-" + dtact.Year;

                            command.CommandText = " insert into Trans_Upload_Activity ([SL_No],[Activity_ID],[Activity_S_Name],[Activity_Name],[SF_Code],[SF_EMP_ID]" +
                                         " ,[Employee_Name],[Date_Activity_Approval],[Date_Activity],[Month_Activity] ,[Year_Activity] " +
                                         ",[Activity_Description],[Activity_Advance],[Activity_Approved_Bill_Amount],[Activity_Addition_Deletion] " +
                                        " ,[Activity_Dateon_Which_Incurred],[Activity_Reason],[Division_Code],[Uploaded_Date],[Process_Flag]) " +
                             " VALUES(" + SL_No + ", " + act_no + ", '" + act_sname + "', '" + act_name + "','" + Sfcode + "','" + columns[2].Trim() + "','" + columns[3].Trim() + "','" + dtappdate + "','" + dtactdate + "','" + columns[5].Trim() + "','" + columns[6].Trim() + "','" + columns[7].Trim() + "','" + columns[8].Trim() + "', " +
                                 "'" + columns[9].Trim() + "', '" + columns[10].Trim() + "','" + columns[11].Trim() + "', '" + columns[12].Trim() + "', '" + div_code + "',getdate(), 0)";

                            //  SqlCommand cmd = new SqlCommand(sql, conn);
                            command.ExecuteNonQuery();
                            
                        }
                        transaction.Commit();
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                        conn.Close();

                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);

                        // Attempt to roll back the transaction.
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                        var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
                        var script = string.Format("{0}", message);
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + script + "');</script>");
                    }
                    connection.Close();
                    // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");

                }
            }
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void ImporttoDatatable()
    {
        try
        {

            if (FlUploadcsv.HasFile)
            {

                string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                if (!excelPath.Contains(".xls"))
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format not Matched');window.location='Activity_Upload.aspx'</script>");
                }
                else
                {
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

                    //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0;";

                    string connStr = "";

                    if (Path.GetExtension(excelPath) == ".xls")
                    {
                        connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                    }
                    else if (Path.GetExtension(excelPath) == ".xlsx")
                    {
                        connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                    }

                    DataTable dtCloned = new DataTable();
                    using (OleDbConnection excel_con = new OleDbConnection(connStr))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();

                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                        {
                            //ds = new DataSet();
                            //oda.Fill(ds);
                            //Dt = ds.Tables[0];

                            ds = new DataSet();
                            oda.Fill(ds);
                           
                            //  Dt = ds.Tables[0];
                            dtCloned = ds.Tables[0];

                            //  oda.Fill(dbset);
                            // Dt.AcceptChanges();

                            //objAdapter1.Fill(ds);
                            //Dt = ds.Tables[0];
                        }
                        //excel_con.Close();
                        excel_con.Close();

                        Dt = dtCloned.Clone();
                        Dt.Columns[1].DataType = typeof(string);
                        Dt.Columns[2].DataType = typeof(string);
                        Dt.Columns[3].DataType = typeof(string);
                        Dt.AcceptChanges();
                        foreach (DataRow r in dtCloned.Rows)
                        {
                            Dt.ImportRow(r);
                        }
                        for (int i = Dt.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow row = Dt.Rows[i];

                            bool col1Empty = row.IsNull(1) || string.IsNullOrWhiteSpace(row[1].ToString());
                            bool col2Empty = row.IsNull(2) || string.IsNullOrWhiteSpace(row[2].ToString());
                            bool col3Empty = row.IsNull(3) || string.IsNullOrWhiteSpace(row[3].ToString());

                            if (col1Empty && col2Empty && col3Empty)
                            {
                                Dt.Rows.RemoveAt(i);
                            }
                        }
                    }
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Activity_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Activity_Upload.xlsx");
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
            string fileName = Server.MapPath("~/Not_Upl_Activity/" + "Activityupld_" + div_code + ".xls");
            //Server.MapPath("");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Activityupld_" + div_code + ".xls");
                Response.TransmitFile(fileName);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
            //
        }
    }

}