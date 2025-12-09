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
using System.Text;
using System.Data.OleDb;
using ClosedXML.Excel;

public partial class MasterFiles_Options_Payslip_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    string div_Name = string.Empty;
    DataTable ds = new DataTable();
    DataSet dsdiv = null;
    string sf_type = string.Empty;
    DataSet dsDivision = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        div_Name = Session["div_name"].ToString();

        if (!Page.IsPostBack)
        {
            Filldiv();

            BindListColumn();
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
            //  menu1.Title = this.Page.Title;
            //   // menu1.FindControl("btnBack").Visible = false;
            if (ViewState["AddValues"] == null)
            {

                DataTable dtSlip = new DataTable();
                dtSlip.Columns.AddRange(new DataColumn[2] { new DataColumn("SlipId", typeof(int)), 
                                    new DataColumn("SlipName", typeof(string)) });
                dtSlip.Columns["SlipId"].AutoIncrement = true;
                dtSlip.Columns["SlipId"].AutoIncrementSeed = 1;
                dtSlip.Columns["SlipId"].AutoIncrementStep = 1;
                ViewState["AddValues"] = dtSlip;
            }


        }

    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    protected void AddValues(object sender, EventArgs e)
    {

        //DataTable dtSlip = (DataTable)ViewState["AddValues"];
        //dtSlip.Rows.Add();
        ////lstAddColumns.Items.Add(new ListItem("SlNo", "0"));
        ////lstAddColumns.Items.Add(new ListItem("EmpCode", "1"));
        //dtSlip.Rows[dtSlip.Rows.Count - 1]["SlipName"] = txtadd.Text.Trim();
        //txtadd.Text = "TT";
        //txtadd.Text = string.Empty;
        //ViewState["AddValues"] = dtSlip;
        //lstAddColumns.DataSource = dtSlip;
        //lstAddColumns.DataTextField = "SlipName";
        //lstAddColumns.DataValueField = "SlipId";
        //lstAddColumns.DataBind();


        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT column_name FROM information_schema.columns WHERE table_name = 'PaySlip_" + div_code + "'", con))
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string TxtVal = txtadd.Text;

                    lstAddColumns.Items.Add(TxtVal);
                    txtadd.Text = "";

                }

                else
                {
                    DataTable dtSlip = (DataTable)ViewState["AddValues"];
                    dtSlip.Rows.Add();
                    //lstAddColumns.Items.Add(new ListItem("SlNo", "0"));
                    //lstAddColumns.Items.Add(new ListItem("EmpCode", "1"));
                    dtSlip.Rows[dtSlip.Rows.Count - 1]["SlipName"] = txtadd.Text.Trim();
                    txtadd.Text = "TT";
                    txtadd.Text = string.Empty;
                    ViewState["AddValues"] = dtSlip;
                    lstAddColumns.DataSource = dtSlip;
                    lstAddColumns.DataTextField = "SlipName";
                    lstAddColumns.DataValueField = "SlipId";
                    lstAddColumns.DataBind();
                    txtadd.Text = "";

                }

            }
        }

    }


    private void BindListColumn()
    {
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT column_name,IS_NULLABLE,DATA_TYPE FROM information_schema.columns WHERE table_name = 'PaySlip_" + div_code + "'", con))
            {
                con.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Col = dt.Rows[i]["column_name"].ToString();
                        string data = dt.Rows[i]["DATA_TYPE"].ToString();

                        //foreach (ListItem item1 in lstAddColumns.Items)
                        //{
                        //    if (d == "NO")
                        //    {
                        //        item1.Selected = true;
                        //    }
                        //}
                      


                        //  lstAddColumns.Items.FindByValue(d).Selected = true;
                        //    lstAddColumns.Items[i].Selected = true;
                        if (data != "int")
                        {

                            lstAddColumns.Items.Add(Col);
                        }
                      
                    }
                }

            }
        }
    }

    protected void DeleteValues(object sender, EventArgs e)
    {
        DataTable dtSlip = (DataTable)ViewState["AddValues"];
        foreach (ListItem item in lstAddColumns.Items)
        {
            if (item.Selected)
            {
                DataRow[] rows = dtSlip.Select("SlipId = " + item.Value);
                dtSlip.Rows.Remove(rows[0]);
            }
        }
        dtSlip.AcceptChanges();
        ViewState["AddValues"] = dtSlip;

        lstAddColumns.DataSource = dtSlip;
        lstAddColumns.DataTextField = "SlipName";
        lstAddColumns.DataValueField = "SlipId";
        lstAddColumns.DataBind();
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        int number = lstAddColumns.Items.Count;
        StringBuilder sw = new StringBuilder();

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        for (int i = 0; i < number; i++)
        {
            ds.Columns.Add(lstAddColumns.Items[i].ToString());

        }

        SqlCommand sqlCom = new SqlCommand("SELECT count(*) FROM sysobjects WHERE xtype = 'U' and name = 'PaySlip_" + div_code + "'", con);
        int j = 0;
        //   con.Open();
        string drop;
        j = int.Parse(sqlCom.ExecuteScalar().ToString());
        if (j > 0)
        {

            foreach (ListItem item1 in lstAddColumns.Items)
            {
                SqlCommand sqlCom1 = new SqlCommand("Select object_name(object_id) as PaySlip_" + div_code + ",* from  SYS.columns where name in ('" + item1.Text + "')", con);

                sqlCom1.ExecuteNonQuery();
                //   drop += "[" + item1.Text + "] " + "varchar(150)" + ",";



                //   drop = drop.TrimEnd(new char[] { ',' }) + ")";
                // SqlCommand cmd1 = new SqlCommand(drop, con);
                SqlDataAdapter da = new SqlDataAdapter(sqlCom1);
                DataSet ds1 = new DataSet();
                da.Fill(ds1);
                sqlCom1.ExecuteNonQuery();

                if (ds1.Tables[0].Rows.Count == 0)
                {             
                
                     drop = "alter table PaySlip_" + div_code + " ADD [" + item1.Text + "] " + "varchar(150)" ;
                     SqlCommand cmd = new SqlCommand(drop, con);
                     SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                     cmd.ExecuteNonQuery();
                }
                //Exists

            }

          
        }
        else
        {
            string sql = "Create Table PaySlip_" + div_code + " (Paymonth " + "int" + ", Payyear " + "int " + ",";
            //for (int i = 0; i < lstAddColumns.Items.Count; i++)
            //{
            foreach (ListItem item1 in lstAddColumns.Items)
            {

                sql += "[" + item1.Text + "] " + "varchar(150)" + ",";


            }
            sql = sql.TrimEnd(new char[] { ',' }) + ")";

            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.ExecuteNonQuery();
        }
        con.Close();
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

        dgGrid.DataSource = ds;

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
        //string FileName = Server.MapPath("~/Upload_Document/PaySlip_" + div_code + ".xlsx");
        //Request.Files["xlsFile"].SaveAs(FileName);
       // string path = Server.MapPath("~/Document/");
       // System.IO.FileInfo objFileInfo = new System.IO.FileInfo(path);
      //  objFileInfo.IsReadOnly = false;
        //if (!Directory.Exists(path))

        // {           
        //    Directory.CreateDirectory(path);       
        //}
        //if (File.Exists(@"~/Document/PaySlip_" + div_code + ".xlsx"))
        //{
        //    string file_Path = @"~/Document/PaySlip_" + div_code + ".xlsx";
        //    File.Delete(file_Path);
        //}
        //using (StringWriter sw1 = new StringWriter(sb))       
        //{           
        //    using (HtmlTextWriter hw1 = new HtmlTextWriter(sw1))           
        //    {
        //        StreamWriter writer = File.AppendText(path + "PaySlip_" + div_code + ".xlsx");
                
        //        hw1.BeginRender();                             
        //        string html = sb.ToString();           
        //        writer.WriteLine(html);
        //    }     
        //}
      

      //  Response.ContentType = "application/vnd.ms-excel";
      //Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

      //  Response.AppendHeader("Content-Disposition",
      //                        "attachment; filename= PaySlip_" + div_code + ".xls");
      //  System.IO.FileInfo objFileInfo1 = new System.IO.FileInfo(path);
      //  objFileInfo1.IsReadOnly = false;
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ds, "Customers");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=PaySlip_" + div_code + ".xlsx");
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
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        string fname = "PaySlip_" + div_code + ".xlsx";
        string path = "~/Document/PaySlip_" + div_code + ".xlsx";
        string name = Path.GetFileName(path);
        string ext = Path.GetExtension(path);
        Response.AppendHeader("content-disposition", "attachment; filename=" + name);
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.WriteFile(path);
        Response.End();    

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable_New();
    }

    private void ImporttoDatatable()
    {
        try
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


                //  ValidateExcelColumns(excelPath);


                conString = string.Format(conString, excelPath);
                using (OleDbConnection excel_con = new OleDbConnection(conString))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        DataTable dt = new DataTable();
                        oda.Fill(dt);

                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dt.Rows[i][1] == DBNull.Value)
                                dt.Rows[i].Delete();
                        }
                        dt.AcceptChanges();


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                                {
                                   
                                    dt.Rows[i][j] = "0";
                                }
                            }
                        }

                        dt.AcceptChanges();

                        DataTable dbset = new DataTable();
                        oda.Fill(dbset);

                        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                        string ChkReord = "select * from dbo.PaySlip_" + div_code + " where Paymonth=" + ddlMonth.SelectedValue + " and Payyear=" + ddlYear.SelectedValue + "";
                        SqlCommand cmd;
                        cmd = new SqlCommand(ChkReord, con);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtR = new DataTable();
                        da.Fill(dtR);



                        if (dtR.Rows.Count > 0)
                        {
                            string Qry = "delete from dbo.PaySlip_" + div_code + " where Paymonth=" + ddlMonth.SelectedValue + " and Payyear=" + ddlYear.SelectedValue + "";
                            cmd = new SqlCommand(Qry, con);
                            // con.Open();
                            int _res1 = cmd.ExecuteNonQuery();
                            //  con.Close();

                            if (_res1 > 0)
                            {
                                foreach (DataRow dsrc in dt.Rows)
                                {

                                    string insertcommand = "insert into dbo.PaySlip_" + div_code + "" + dbset.TableName + " ";
                                    string cols = "";
                                    string vals = "";

                                    DataRow dr = dbset.NewRow();
                                    foreach (DataColumn clm in dt.Columns)
                                    {
                                        dr[clm.ColumnName] = dsrc[clm.ColumnName].ToString();
                                        if (cols.Length > 0)
                                        {
                                            cols += ",[" + clm.ColumnName + "]";
                                        }
                                        else
                                        {
                                            cols = "[" + clm.ColumnName + "]";
                                        }
                                        if (vals.Length > 0)
                                        {
                                            vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";
                                        }
                                        else
                                        {

                                            vals = "'" + dsrc[clm.ColumnName].ToString() + "'";

                                            //vals = "'" + dsrc[clm.ColumnName].ToString() + "'";
                                        }

                                    }


                                    insertcommand += "(Paymonth,Payyear," + cols + ") values(" + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue + "," + vals + ")";
                                    // SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                    cmd = new SqlCommand(insertcommand, con);

                                    // con.Open();
                                    int _res = cmd.ExecuteNonQuery();
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                                    //  con.Close();
                                    //cmdinsert.CommandText = insertcommand;
                                    //cmdinsert.ExecuteNonQuery();
                                    insertcommand = "";

                                }
                            }

                        }

                        else
                        {
                            foreach (DataRow dsrc in dt.Rows)
                            {

                                string insertcommand = "insert into dbo.PaySlip_" + div_code + "" + dbset.TableName + " ";
                                string cols = "";
                                string vals = "";

                                DataRow dr = dbset.NewRow();
                                foreach (DataColumn clm in dt.Columns)
                                {
                                    dr[clm.ColumnName] = dsrc[clm.ColumnName].ToString();
                                    if (cols.Length > 0)
                                    {
                                        cols += ",[" + clm.ColumnName + "]";
                                    }
                                    else
                                    {
                                        cols = "[" + clm.ColumnName + "]";
                                    }
                                    if (vals.Length > 0)
                                    {

                                        vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";

                                        // vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";
                                    }
                                    else
                                    {
                                        vals = "'" + dsrc[clm.ColumnName].ToString() + "'";

                                        //  vals += "," + "'" + Value + "'";

                                        //vals = "'" + dsrc[clm.ColumnName].ToString() + "'";
                                    }

                                }


                                insertcommand += "(Paymonth,Payyear," + cols + ") values(" + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue + "," + vals + ")";
                                // SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                cmd = new SqlCommand(insertcommand, con);

                                // con.Open();
                                int _res = cmd.ExecuteNonQuery();
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                                // con.Close();
                                //cmdinsert.CommandText = insertcommand;
                                //cmdinsert.ExecuteNonQuery();
                                insertcommand = "";

                            }
                        }

                        con.Close();

                    }
                    excel_con.Close();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    private void ImporttoDatatable_New()
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
                        DataTable dt = new DataTable();
                        oda.Fill(dt);

                        for (int i = dt.Rows.Count - 1; i >= 0; i--)
                        {
                            if (dt.Rows[i][1] == DBNull.Value)
                                dt.Rows[i].Delete();
                        }
                        dt.AcceptChanges();


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[i][j].ToString()))
                                {

                                    dt.Rows[i][j] = "0";
                                }
                            }
                        }

                        dt.AcceptChanges();

                        DataTable dbset = new DataTable();
                        oda.Fill(dbset);

                        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                        string ChkReord = "select * from dbo.PaySlip_" + div_code + " where Paymonth=" + ddlMonth.SelectedValue + " and Payyear=" + ddlYear.SelectedValue + "";
                        SqlCommand cmd;
                        cmd = new SqlCommand(ChkReord, con);
                        con.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dtR = new DataTable();
                        da.Fill(dtR);


                        if (rdolstrpt.SelectedValue == "1")
                        {
                            //if (dtR.Rows.Count > 0)
                            //{
                            string Qry = "delete from dbo.PaySlip_" + div_code + " where Paymonth=" + ddlMonth.SelectedValue + " and Payyear=" + ddlYear.SelectedValue + "";
                            cmd = new SqlCommand(Qry, con);

                            int _res1 = cmd.ExecuteNonQuery();


                            if (_res1 > 0)
                            {
                                foreach (DataRow dsrc in dt.Rows)
                                {

                                    string insertcommand = "insert into dbo.PaySlip_" + div_code + "" + dbset.TableName + " ";
                                    string cols = "";
                                    string vals = "";

                                    DataRow dr = dbset.NewRow();
                                    foreach (DataColumn clm in dt.Columns)
                                    {
                                        dr[clm.ColumnName] = dsrc[clm.ColumnName].ToString();
                                        if (cols.Length > 0)
                                        {
                                            cols += ",[" + clm.ColumnName + "]";
                                        }
                                        else
                                        {
                                            cols = "[" + clm.ColumnName + "]";
                                        }
                                        if (vals.Length > 0)
                                        {
                                            vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";
                                        }
                                        else
                                        {

                                            vals = "'" + dsrc[clm.ColumnName].ToString() + "'";


                                        }

                                    }


                                    insertcommand += "(Paymonth,Payyear," + cols + ") values(" + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue + "," + vals + ")";

                                    cmd = new SqlCommand(insertcommand, con);


                                    int _res = cmd.ExecuteNonQuery();
                                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");

                                    insertcommand = "";

                                }
                            }

                            //}
                            // else
                            // {

                            // }

                        }


                        else
                        {
                            foreach (DataRow dsrc in dt.Rows)
                            {

                                string insertcommand = "insert into dbo.PaySlip_" + div_code + "" + dbset.TableName + " ";
                                string cols = "";
                                string vals = "";

                                DataRow dr = dbset.NewRow();
                                foreach (DataColumn clm in dt.Columns)
                                {
                                    dr[clm.ColumnName] = dsrc[clm.ColumnName].ToString();
                                    if (cols.Length > 0)
                                    {
                                        cols += ",[" + clm.ColumnName + "]";
                                    }
                                    else
                                    {
                                        cols = "[" + clm.ColumnName + "]";
                                    }
                                    if (vals.Length > 0)
                                    {

                                        vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";


                                    }
                                    else
                                    {
                                        vals = "'" + dsrc[clm.ColumnName].ToString() + "'";


                                    }

                                }


                                insertcommand += "(Paymonth,Payyear," + cols + ") values(" + ddlMonth.SelectedValue + "," + ddlYear.SelectedValue + "," + vals + ")";

                                cmd = new SqlCommand(insertcommand, con);


                                int _res = cmd.ExecuteNonQuery();
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");

                                insertcommand = "";

                            }
                        }

                        con.Close();


                    }

                    excel_con.Close();
                }

            }
        }

        catch (Exception ex)
        {
            throw (ex);
        }
    }
    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        lstAddColumns.Items.Clear();
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/BasicMaster.aspx");
    }
}