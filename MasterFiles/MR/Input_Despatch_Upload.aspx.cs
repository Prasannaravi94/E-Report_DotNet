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

public partial class MasterFiles_Options_Input_Despatch_Upload : System.Web.UI.Page
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
    InputDespatch objInput = new InputDespatch();
    DataSet dsProd=new DataSet();
    DataSet dsProddeact = new DataSet();
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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Records are not available');window.location='Input_Despatch_Upload.aspx'</script>");
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
            InputDespatch sf1 = new InputDespatch();
            dsproduct = sf1.Get_gift_SlNo(div_code);
            DataTable procode = dsproduct.Tables[0];
            DataTable DT_tbl_FirstTable = new DataTable();
            DT_tbl_FirstTable.Columns.AddRange(new DataColumn[6]
        {
            //new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("Trans_Month", typeof(string)),
            new DataColumn("Trans_Year",typeof(int)) ,
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("Created_Date", typeof(DateTime)),
            new DataColumn("Trans_month_year", typeof(DateTime)),
            //new DataColumn("Updated_Date", typeof(DateTime))
            
           
           
          
        }
            );

            DataTable DT_tbl_SecondTable = new DataTable();
            DT_tbl_SecondTable.Columns.AddRange(new DataColumn[15]
        {
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("Trans_Month", typeof(string)),
            new DataColumn("Trans_Year", typeof(int)),
            new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("gift_code", typeof(string)),
            new DataColumn("gift_name", typeof(string)),
            new DataColumn("pack", typeof(string)),
            new DataColumn("gift_rate",typeof(double)) ,
            new DataColumn("Despatch_Qty",typeof(double)) ,
            new DataColumn("giftslno",typeof(int)),
              new DataColumn("InvNo",typeof(string)) ,
             new DataColumn("InvDate",typeof(string)),

              new DataColumn("LrNo",typeof(string)) ,
              new DataColumn("LrDate",typeof(string))





        }
            );


            DataTable DT_tbl_FirstUpdateTable = new DataTable();
            DT_tbl_FirstUpdateTable.Columns.AddRange(new DataColumn[7]
        {
            new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("Trans_Month", typeof(string)),
            new DataColumn("Trans_Year",typeof(int)) ,
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("Created_Date", typeof(DateTime)),
            new DataColumn("Trans_month_year", typeof(DateTime)),
            //new DataColumn("Updated_Date", typeof(DateTime))
            
           
           
          
        }
            );

            DataTable masterDt = null;

            masterDt = dcmas.getinputHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
            int countRow = Dt.Rows.Count;
            int countCol = Dt.Columns.Count;
            bool success = true;
            for (int iCol = 0; iCol < countCol; iCol++)
            {

                DataColumn col = Dt.Columns[iCol];
                if (col.ColumnName != "Sl_No" && iCol == 0)
                {

                    success = false;

                }
                else if (col.ColumnName != "Division_Name" && iCol == 1)
                {

                    success = false;

                }
                else if (col.ColumnName != "Employee_ID" && iCol == 2)
                {

                    success = false;

                }
                else if (col.ColumnName != "Input_Code" && iCol == 3)
                {

                    success = false;

                }
                else if (col.ColumnName != "Input_Name" && iCol == 4)
                {

                    success = false;

                }
                else if (col.ColumnName != "InvNo" && iCol == 5)
                {

                    success = false;

                }
                else if (col.ColumnName != "InvDate" && iCol == 6)
                {

                    success = false;

                }
                else if (col.ColumnName != "LrNo" && iCol == 7)
                {

                    success = false;

                }
                else if (col.ColumnName != "LrDate" && iCol == 8)
                {

                    success = false;

                }
                else if (col.ColumnName != "Pack" && iCol == 9)
                {

                    success = false;

                }
                else if (col.ColumnName != "Input_Rate" && iCol == 10)
                {

                    success = false;

                }
                else if (col.ColumnName != "Input_Qty" && iCol == 11)
                {
                    success = false;

                }
            }
            if (success == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('column Names are Not Matched');window.location='Input_Despatch_Upload.aspx'</script>");
            }
            else
            {

                int columnCount = Dt.Columns.Count;


                if (columnCount > 12)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Column Not Matched.check the column heading');window.location='Input_Despatch_Upload.aspx'</script>");
                }
                else if (columnCount < 12)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format Not Matched.Check the column heading');window.location='Input_Despatch_Upload.aspx'</script>");
                }
                else
                {


                    string[] columnNames = Dt.Columns.Cast<DataColumn>()
                          .Select(x => x.ColumnName)
                          .ToArray();

                    var ValuetoReturn = (from Rows in Dt.AsEnumerable()
                                         select Rows[columnNames[3]]).Distinct().ToList();
                    foreach (var gift_code in ValuetoReturn)
                    {
                       
                        dsProd = sf1.Get_Input_SName_ALL(div_code,  gift_code.ToString());
                        if (dsProd.Tables[0].Rows.Count > 0)
                        {

                            if (dsProd.Tables[0].Rows[0]["Gift_Active_Flag"].ToString() == "0")
                            {
                                conn.Open();

                                string strQry1 = "UPDATE Mas_Gift set Gift_Effective_To=cast(dateadd(m, 3, getdate()) as date) WHERE Gift_SName = '" + gift_code.ToString() + "'  AND  Division_Code='" + div_code + "' and Gift_Active_Flag  =0 ";
                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                cmd2.ExecuteNonQuery();
                                conn.Close();
                            }
                            else
                            {
                                conn.Open();

                                string strQry1 = "UPDATE Mas_Gift set Gift_Effective_To=cast(dateadd(m, 3, getdate()) as date),Gift_Active_Flag=1,LastUpdt_Date=getdate() WHERE Gift_SName = '" + gift_code.ToString() + "'  AND  Division_Code='" + div_code + "'";
                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                cmd2.ExecuteNonQuery();
                                conn.Close();
                            }

                        }
                        else
                        {
                            string Sub_Div = string.Empty;
                            string Prod_Brand = string.Empty;
                            DataSet dsSub_Divi = sf1.Get_Subdivision(div_code);
                            if (dsSub_Divi.Tables[0].Rows.Count > 0)
                            {
                                Sub_Div = dsSub_Divi.Tables[0].Rows[0][0].ToString();
                            }

                            DataSet dsProd_Brand = sf1.Get_Prod_Brand(div_code);
                            if (dsProd_Brand.Tables[0].Rows.Count > 0)
                            {
                                Prod_Brand = dsProd_Brand.Tables[0].Rows[0][0].ToString();
                            }
                            string State_code = string.Empty;
                            Division sf2 = new Division();
                            ds = sf2.getStatePerDivision(div_code);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                               
                                State_code = ds.Tables[0].Rows[0][0].ToString();
                            }

                            if (dsProd.Tables[0].Rows.Count == 0)
                            {
                                ListedDR objQua = new ListedDR();
                                int Gift_Code = objQua.GetInput_code();
                                conn.Open();
                                string strQry1 = "INSERT INTO Mas_Gift(Gift_Code,Gift_Name,Gift_SName,Gift_Effective_From,Gift_Effective_To,Division_Code,Created_Date,Gift_Active_Flag,Gift_Type,Gift_Value,State_Code,subdivision_code,Product_Brd_Code)" +
                                         "values('" + Gift_Code + "','','" + gift_code.ToString() + "', cast(dateadd(m, 0, getdate()) as date), cast(dateadd(m, 3, getdate()) as date) ,'" + div_code + "',getdate(),0,2,0,'" + State_code + "','" + Sub_Div + "','" + Prod_Brand + "')";
                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                cmd2.ExecuteNonQuery();
                                conn.Close();
                            }

                            dsProd = sf1.Get_Input_SName(div_code, gift_code.ToString());
                            if (dsProd.Tables[0].Rows.Count > 0)
                            {
                              //  str_ProdCode = dsProd.Tables[0].Rows[0][0].ToString();
                              //  Input_Name = dsProd.Tables[0].Rows[0][1].ToString();
                            }
                        }

                    }
                    Dt.Columns.Add("remarks");


                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        DataRow ro = dtc.Rows[i];
                        string number = @"^[0-9]+$";
                        string floatnumber = @"^[0-9]*(?:\.[0-9]*)?$";
                        string serpcode = string.Empty;
                        string sfempid = string.Empty;
                        string remarks = string.Empty;
                        bool isnum1 = true;
                        bool isnum2 = true;
                        string SFcode = "";
                        string giftcode = "";
                        string giftslno = "";
                        string desdty = "";
                        string desdtyrate = "";
                        DataRow row = Dt.Rows[i];
                        int rowno = i + 2;
                        int columnCount1 = Dt.Columns.Count;
                        string[] columns = new string[columnCount];
                        DataRow[] rows1;
                        DataRow[] rows;
                        for (int j = 0; j < columnCount; j++)
                        {

                            columns[j] = row[j].ToString().Trim();
                            if (Dt.Columns[j].ColumnName == "Employee_ID")
                            {
                                String filter = "sf_emp_id='" + columns[2] + "'";
                                rows = sfemp.Select(filter);
                                if (rows.Count() > 0)
                                {
                                    foreach (DataRow r in rows)
                                    {
                                        SFcode = r["Sf_Code"].ToString();
                                    }
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Input_Code")
                            {
                                String filter1 = "Gift_SName='" + columns[3] + "'";
                                rows1 = procode.Select(filter1);
                                if (rows1.Count() > 0)
                                {
                                    foreach (DataRow r in rows1)
                                    {

                                        giftcode = r["Gift_SName"].ToString();
                                        giftslno = r["Gift_code"].ToString();
                                    }
                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Input_Qty")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum1 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }
                            else if (Dt.Columns[j].ColumnName == "Input_Rate")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum2 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }

                        }

                        if ( SFcode == "" || isnum1 == false || isnum2 == false || columns[11].Trim() == "" || columns[3].Trim() == "" || columns[2].Trim() == "" || columns[10].Trim() == "")
                        {
                            if (isnum1 == false)
                            {
                                desdty = "Input Qty Allowed Only Number" + '/';
                            }
                            else if (columns[11].Trim() == "")
                            {
                                desdty = " Input Qty Empty" + '/';
                            }
                            if (isnum2 == false)
                            {
                                desdtyrate = "Input Rate Allowed Only Number" + '/';
                            }
                            else if (columns[10].Trim() == "")
                            {
                                desdtyrate = " Input Rate Empty" + '/';
                            }
                            if (columns[3].Trim() == "")
                            {
                                serpcode = "Input Code empty" + '/';
                            }

                           
                            if (columns[2].Trim() == "")
                            {
                                sfempid = "Employee ID empty" + '/';
                            }
                            else if (SFcode == "")
                            {
                                sfempid = "Employee ID Not Matched" + '/';
                            }
                            //if (sfempid == "" && serpcode == "")
                            //{
                                remarks = sfempid + serpcode + desdtyrate + desdty;
                                ro[dtc.Columns.Count - 2] = rowno;
                                ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                            //}
                            //else
                            //{
                            //    remarks = sfempid + serpcode + desdtyrate + desdty;
                            //    ro[dtc.Columns.Count - 2] = rowno;
                            //    ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                            //}

                            foreach (DataRow dr in dtc.Rows)
                            {

                                if (dtc.Rows[i] == dr)
                                {

                                    dtcopy.Rows.Add(dr.ItemArray);

                                }
                            }

                        }






                        string transslno = "";
                        string filtermsdt = "sf_code='" + SFcode + "' ";
                        DataRow[] masdt = masterDt.Select(filtermsdt);
                        Dictionary<String, String> values = new Dictionary<String, String>();
                        DateTime dtlr = new DateTime();

                        string strlr = "";
                        DateTime dtinv = new DateTime();

                        string strinv = "";

                        //if (columns[6].Trim() != "")
                        //{
                            
                        //}
                        //else
                        //{
                        //    columns[6] = "1900-01-01 00:00:00.000";
                        //}
                        //if (columns[8].Trim() != "")
                        //{
                            
                        //}
                        //else
                        //{
                        //    columns[8] = "1900-01-01 00:00:00.000";
                        //}

                        if (dtcopy.Rows.Count == 0)
                        {

                            if (masdt.Count() > 0)
                            {
                                string prod_name = string.Empty;
                                dsProd = sf1.Get_Input_SName(div_code, columns[3].ToString());
                                dsProddeact = sf1.Get_Input_SName_DeactiveCheck(div_code, columns[3].ToString());
                                if (dsProd.Tables[0].Rows.Count > 0)
                                {
                                    giftslno = dsProd.Tables[0].Rows[0]["Gift_code"].ToString();
                                    if (dsProd.Tables[0].Rows[0]["Gift_Name"].ToString() == "")
                                    {
                                        conn.Open();

                                        string strQry1 = "UPDATE Mas_Gift set Gift_Name='"+ columns[4].ToString().Replace("'", " ") + "' WHERE Gift_SName = '" + giftcode + "'  AND  Division_Code='" + div_code + "' and Gift_Active_Flag  =0 ";
                                        SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                        cmd2.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                                else if (dsProddeact.Tables[0].Rows.Count > 0)
                                    {
                                   
                                    conn.Open();
                                    string Effromdate = "01" + "-" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedValue;
                                    DateTime frm_Date = Convert.ToDateTime(Effromdate.ToString());
                                    string[] frm_Date1 = frm_Date.GetDateTimeFormats();
                                    var todate = frm_Date.AddMonths(3);
                                    DateTime lastDate = new DateTime(todate.Year, todate.Month, 1).AddMonths(1).AddDays(-1);
                                    string[] lastDate1 = lastDate.GetDateTimeFormats();
                                    string strQry1 = "UPDATE Mas_Gift set Gift_Active_Flag=0,Gift_Effective_To = '" + lastDate1[4] + "' WHERE Gift_SName = '" + giftcode + "'  AND  Division_Code='" + div_code + "' and Gift_Active_Flag =1";
                                    SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                    cmd2.ExecuteNonQuery();
                                    conn.Close();

                                }

                                    DT_tbl_FirstUpdateTable.Rows.Add(masdt[0]["Trans_sl_No"], SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, System.DateTime.Now, System.DateTime.Now);
                                    DT_tbl_SecondTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, masdt[0]["Trans_sl_No"], div_code, columns[3].ToString(), columns[4].ToString().Replace("'", " "), columns[9].ToString().Replace("'", " "), Convert.ToDouble(columns[10].Replace("'", " ")), Convert.ToDouble(columns[11].Replace("'", " ")), Convert.ToInt32(giftslno), columns[5].Replace("'", " "), columns[6].Trim(), columns[7].Replace("'", " "), columns[8].Trim());

                            }
                            else
                            {
                                string prod_name = string.Empty;
                                dsProd = sf1.Get_Input_SName(div_code, columns[3].ToString());
                                dsProddeact = sf1.Get_Input_SName_DeactiveCheck(div_code, columns[3].ToString());
                                if (dsProd.Tables[0].Rows.Count > 0)
                                {
                                    giftslno = dsProd.Tables[0].Rows[0]["Gift_code"].ToString();
                                    if (dsProd.Tables[0].Rows[0]["Gift_Name"].ToString() == "")
                                    {
                                        conn.Open();

                                        string strQry1 = "UPDATE Mas_Gift set Gift_Name='" + columns[4].ToString().Replace("'", " ") + "' WHERE Gift_SName = '" + columns[3].ToString() + "'  AND  Division_Code='" + div_code + "' and Gift_Active_Flag  =0 ";
                                        SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                        cmd2.ExecuteNonQuery();
                                        conn.Close();
                                    }
                                }
                                else if (dsProddeact.Tables[0].Rows.Count > 0)
                                {

                                    conn.Open();
                                    string Effromdate = "01" + "-" + ddlMonth.SelectedValue + "-" + ddlYear.SelectedValue;
                                    DateTime frm_Date = Convert.ToDateTime(Effromdate.ToString());
                                    string[] frm_Date1 = frm_Date.GetDateTimeFormats();
                                    var todate = frm_Date.AddMonths(3);
                                    DateTime lastDate = new DateTime(todate.Year, todate.Month, 1).AddMonths(1).AddDays(-1);
                                    string[] lastDate1 = lastDate.GetDateTimeFormats();
                                    string strQry1 = "UPDATE Mas_Gift set Gift_Active_Flag=0,Gift_Effective_To = '" + lastDate1[4] + "' WHERE Gift_SName = '" + columns[3].ToString() + "'   AND  Division_Code='" + div_code + "' and Gift_Active_Flag =1";
                                    SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                    cmd2.ExecuteNonQuery();
                                    conn.Close();

                                }
                                DT_tbl_FirstTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, System.DateTime.Now, System.DateTime.Now);
                                DT_tbl_SecondTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, 0, div_code, columns[3].ToString(), columns[4].ToString().Replace("'", " "), columns[9].ToString().Replace("'", " "), Convert.ToDouble(columns[10].Replace("'", " ")), Convert.ToDouble(columns[11].Replace("'", " ")), Convert.ToInt32(giftslno), columns[5].Replace("'", " "), columns[6].Trim(), columns[7].Replace("'", " "), columns[8].Trim());
                            }
                        }
                    }

                }
            }
            //DataTable master1=all data
            //DataTable master1=update
            //DataTable master2=DT_tbl_FirstTable
            //DT_tbl_FirstTable.Rows.Add("MR1286");
            //DT_tbl_FirstTable.Rows.Add("MR1287");
            //for (int i = 0; i < 1000; i++)
            //{
            //    if (i % 2 == 0)
            //    {
            //        DT_tbl_SecondTable.Rows.Add("MR1287", 0, "102", "1s");
            //    }
            //    else
            //    {
            //        DT_tbl_SecondTable.Rows.Add("MR1286", 0, "1012", "15s");
            //    }
            //}


            if (dtcopy.Rows.Count > 0)
            {
                pnlprimary.Visible = true;
                lnlnot.Focus();
                grdPrimary.DataSource = dtcopy;
                grdPrimary.DataBind();
                System.IO.StringWriter sw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                // Render grid view control.
                grdPrimary.RenderControl(htw);
                //User objUser = new User();
                string filePath = Server.MapPath("~/Not_upl_Input/");
                //   string fileName = ("PrimaryBill_" + (System.DateTime.Now.Date).ToString() + "_" + div_code + ".xls");
                string fileName = ("Inputupld_" + div_code + ".xls");

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
                bool IsSuccessSave = false;
                SqlTransaction transaction = null;
                String transslno = "";


                try
                {
                    string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(constr))
                    {
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            using (transaction = connection.BeginTransaction())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;

                                //removeDuplicatesRows(DT_tbl_FirstTable);
                                RemoveDuplicateRowsFirtstable(DT_tbl_FirstTable, "SF_Code");
                                transslno = "";
                                RemoveDuplicateRows(DT_tbl_FirstUpdateTable, "SF_Code", ref transslno);


                                using (SqlBulkCopy bulkCopy_tbl_FirstTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                                {
                                    bulkCopy_tbl_FirstTable.BatchSize = 5000;
                                    bulkCopy_tbl_FirstTable.DestinationTableName = "dbo.Trans_Input_Despatch_head";
                                    // bulkCopy_tbl_FirstTable.ColumnMappings.Add("Trans_sl_No", "Trans_sl_No");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Sf_Code", "Sf_Code");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Trans_Month", "Trans_Month");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Trans_Year", "Trans_Year");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Division_Code", "Division_Code");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Created_Date", "Created_Date");
                                    // bulkCopy_tbl_FirstTable.ColumnMappings.Add("Updated_Date", "getdate()");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Trans_month_year", "Trans_month_year");
                                    bulkCopy_tbl_FirstTable.WriteToServer(DT_tbl_FirstTable);
                                }


                                transaction.Commit();
                                //IsSuccessSave = true;
                            }
                            connection.Close();

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

                        }
                    }
                }
                catch (Exception ex)
                {


                    try
                    {

                        transaction.Rollback();

                    }

                    catch (Exception ex2)
                    {

                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);

                    }

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists. in saving header');window.location='Input_Despatch_Upload.aspx'</script>");



                }




                try
                {
                    string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(constr))
                    {
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            using (transaction = connection.BeginTransaction())
                            {
                                command.Connection = connection;
                                command.Transaction = transaction;



                                //if (rdochksave.SelectedValue == "1")
                                //{
                                //    //overwite with exist(deleta and insert)
                                //    if (transslno != "")
                                //    {
                                //        command.CommandText = "delete from Trans_Input_Despatch_details where trans_Sl_no in(" + transslno + ") ";
                                //        command.ExecuteNonQuery();
                                //    }
                                //}
                                //else
                                //{
                                //    //overwite with exist(only insert)
                                //    if (transslno != "")
                                //    {
                                //        command.CommandText = "update Trans_Input_Despatch_head set Updated_Date=getdate() where trans_Sl_no in(" + transslno + ") ";
                                //        command.ExecuteNonQuery();
                                //    }
                                //}

                                //masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
                                //DataRow[] filterdr = null;
                                //command.CommandText = "select * from Trans_Input_Despatch_head where division_code='" + div_code + "' and trans_month='" + ddlMonth.SelectedValue + "' and trans_year='" + ddlYear.SelectedValue + "'";
                                //SqlDataAdapter da2 = new SqlDataAdapter(command);
                                //DataTable dt2 = new DataTable();
                                //da2.Fill(dt2);
                                //foreach (DataRow mr in dt2.Rows)
                                //{
                                //    string filter = "";
                                //    filter = "sf_code='" + mr["sf_code"] + "'";

                                //    filterdr = DT_tbl_SecondTable.Select(filter);
                                //    double smpledd = 0;
                                    //foreach (DataRow r in filterdr)
                                    //{
                                    //    r["Trans_sl_No"] = mr["Trans_sl_No"].ToString();
                                    //    smpledd = smpledd + Convert.ToDouble(r["Despatch_Qty"].ToString());

                                    //    Int32 count1 = 0;
                                    //    //if (rdochksave.SelectedValue == "1")
                                    //    //{
                                    //    //    command.CommandText = "select count(sl_no) from Trans_Input_Stock_FFWise_AsonDate where sf_code='" + r["sf_code"] + "' and gift_code='" + r["Gift_Code"] + "'";
                                    //    //    count1 = (Int32)command.ExecuteScalar();
                                    //    //    if (count1 > 0)
                                    //    //    {

                                    //    //        command.CommandText = "insert into Bkp_Trans_Input_Stock_FFWise_AsonDate select sf_code,Gift_Code,Gift_SName,InputQty_AsonDate,division_code,getdate() from Trans_Input_Stock_FFWise_AsonDate";
                                    //    //        command.ExecuteNonQuery();

                                    //    //        string sfcode = r["sf_code"].ToString();
                                    //    //        string prdcode = r["Gift_Code"].ToString();
                                    //    //        double minusqty = 0;
                                    //    //        DataTable smpprd = dcmas.getinputdetailsproduct(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, sfcode, prdcode);
                                    //    //        if (smpprd.Rows.Count > 0)
                                    //    //        {
                                    //    //            minusqty = Convert.ToDouble(smpprd.Rows[0]["Despatch_Qty"].ToString());
                                    //    //        }
                                    //    //        command.CommandText = "update Trans_Input_Stock_FFWise_AsonDate set InputQty_AsonDate=isnull(InputQty_AsonDate,0) - " + minusqty + " + " + r["Despatch_Qty"] + " where sf_code='" + r["sf_code"] + "' and Gift_Code='" + r["Gift_Code"] + "' ";
                                    //    //        command.ExecuteNonQuery();

                                    //    //    }
                                    //    //    else
                                    //    //    {
                                    //    //        //backup
                                    //    //        command.CommandText = "insert into Bkp_Trans_Input_Stock_FFWise_AsonDate select sf_code,Gift_Code,Gift_SName,InputQty_AsonDate,division_code,getdate() from Trans_Input_Stock_FFWise_AsonDate";
                                    //    //        command.ExecuteNonQuery();

                                    //    //        command.CommandText = "insert into Trans_Input_Stock_FFWise_AsonDate values ('" + r["sf_code"] + "','" + r["Gift_Code"] + "','" + r["Gift_Name"] + "','" + r["Despatch_Qty"] + "','" + div_code + "') ";
                                    //    //        command.ExecuteNonQuery();
                                    //    //    }
                                    //    //}
                                    //    //else
                                    //    //{
                                    //    //    command.CommandText = "select count(sl_no) from Trans_Input_Stock_FFWise_AsonDate where sf_code='" + r["sf_code"] + "' and Gift_Code='" + r["Gift_Code"] + "'";
                                    //    //    count1 = (Int32)command.ExecuteScalar();
                                    //    //    if (count1 > 0)
                                    //    //    {
                                    //    //        //backup
                                    //    //        command.CommandText = "insert into bkp_Trans_Input_Stock_FFWise_AsonDate select sf_code,Gift_Code,Gift_SName,InputQty_AsonDate,division_code,getdate() from Trans_Input_Stock_FFWise_AsonDate";
                                    //    //        command.ExecuteNonQuery();

                                    //    //        command.CommandText = "update Trans_Input_Stock_FFWise_AsonDate set InputQty_AsonDate=isnull(InputQty_AsonDate,0)+'" + r["Despatch_Qty"] + "' where sf_code='" + r["sf_code"] + "' and Gift_Code='" + r["Gift_Code"] + "' ";
                                    //    //        command.ExecuteNonQuery();
                                    //    //    }
                                    //    //    else
                                    //    //    {
                                    //    //        //backup
                                    //    //        command.CommandText = "insert into bkp_Trans_Input_Stock_FFWise_AsonDate select sf_code,Gift_Code,Gift_SName,InputQty_AsonDate,division_code,getdate() from Trans_Input_Stock_FFWise_AsonDate";
                                    //    //        command.ExecuteNonQuery();

                                    //    //        command.CommandText = "insert into Trans_Input_Stock_FFWise_AsonDate values ('" + r["sf_code"] + "','" + r["Gift_Code"] + "','" + r["Gift_Name"] + "','" + r["Despatch_Qty"] + "','" + div_code + "') ";
                                    //    //        command.ExecuteNonQuery();
                                    //    //    }
                                    //    //}
                                    //}
                               // }


                                if (rdochksave.SelectedValue == "1")
                                {
                                    //overwite with exist(deleta and insert)
                                    if (transslno != "")
                                    {
                                        command.CommandText = "delete from Trans_Input_Despatch_details where trans_Sl_no in(" + transslno + ") ";
                                        command.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    //overwite with exist(only insert)
                                    if (transslno != "")
                                    {
                                        command.CommandText = "update Trans_Input_Despatch_head set Updated_Date=getdate() where trans_Sl_no in(" + transslno + ") ";
                                        command.ExecuteNonQuery();
                                    }
                                }



                                //masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
                                DataRow[] filterdr = null;
                                command.CommandText = "select * from Trans_Input_Despatch_head where division_code='" + div_code + "' and trans_month='" + ddlMonth.SelectedValue + "' and trans_year='" + ddlYear.SelectedValue + "'";
                                SqlDataAdapter da2 = new SqlDataAdapter(command);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                foreach (DataRow mr in dt2.Rows)
                                {
                                    string filter = "";
                                    filter = "sf_code='" + mr["sf_code"] + "'";

                                    filterdr = DT_tbl_SecondTable.Select(filter);

                                    foreach (DataRow r in filterdr)
                                    {
                                        r["Trans_sl_No"] = mr["Trans_sl_No"].ToString();
                                    }
                                }


                                using (SqlBulkCopy bulkCopy_tbl_SecondTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                                {


                                    bulkCopy_tbl_SecondTable.BatchSize = 5000;
                                    bulkCopy_tbl_SecondTable.DestinationTableName = "dbo.Trans_Input_Despatch_details";
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Trans_sl_No", "Trans_sl_No");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Division_Code", "Division_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Gift_Code", "Gift_Name_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Gift_Name", "Gift_Name");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("pack", "Pack");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("gift_rate", "Sample_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Despatch_Qty", "Despatch_Actual_qty");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Despatch_Qty", "Despatch_Qty_Bk");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("giftslno", "productc");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("InvNo", "InvNo");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("InvDate", "InvDate");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("LrNo", "LrNo");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("LrDate", "LrDate");

                                    bulkCopy_tbl_SecondTable.WriteToServer(DT_tbl_SecondTable);
                                }


                                transaction.Commit();
                                //IsSuccessSave = true;
                            }
                            connection.Close();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Input Uploaded Successfully');window.location='Input_Despatch_Upload.aspx'</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    //DataTable dtsamplehead = null;
                    //dcmas.deleteinputHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);


                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists in saving Details');</script>");




                }

                // return IsSuccessSave;

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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format not Matched');window.location='Input_Despatch_Upload.aspx'</script>");
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
            string fileName = Server.MapPath("~\\Document\\Input_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Input_Upload.xlsx");
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
            string fileName = Server.MapPath("~/Not_upl_Input/" + "Inputupld_" + div_code + ".xls");
            //Server.MapPath("");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Inputupld_" + div_code + ".xls");
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

    public DataTable removeDuplicatesRows(DataTable dt)
    {
        DataTable uniqueCols = dt.DefaultView.ToTable(true, "sf_code");
        return uniqueCols;
    }
    public DataTable RemoveDuplicateRows(DataTable dTable, string colName, ref string existsslno)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
        //And add duplicate item value in arraylist.
        foreach (DataRow drow in dTable.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
            {
                hTable.Add(drow[colName], string.Empty);

                if (existsslno == "")
                    existsslno = existsslno + drow["Trans_sl_No"];
                else
                    existsslno = existsslno + "," + drow["Trans_sl_No"];


            }
        }

        //Removing a list of duplicate items from datatable.
        foreach (DataRow dRow in duplicateList)
            dTable.Rows.Remove(dRow);

        //Datatable which contains unique records will be return as output.
        return dTable;
    }

    public DataTable RemoveDuplicateRowsFirtstable(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
        //And add duplicate item value in arraylist.
        foreach (DataRow drow in dTable.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
            {
                hTable.Add(drow[colName], string.Empty);




            }
        }

        //Removing a list of duplicate items from datatable.
        foreach (DataRow dRow in duplicateList)
            dTable.Rows.Remove(dRow);

        //Datatable which contains unique records will be return as output.
        return dTable;
    }


}