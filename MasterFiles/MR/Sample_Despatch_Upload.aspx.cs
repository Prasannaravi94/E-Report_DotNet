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

public partial class MasterFiles_Options_Sample_Despatch_Upload : System.Web.UI.Page
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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Records are not available');window.location='Sample_Despatch_Upload.aspx'</script>");
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
            dsproduct = sf1.Get_product_SlNo(div_code);
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
            DT_tbl_SecondTable.Columns.AddRange(new DataColumn[14]
        {
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("Trans_Month", typeof(string)),
            new DataColumn("Trans_Year", typeof(int)),
            new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("product_name", typeof(string)),
            new DataColumn("Product_Sale_Unit", typeof(string)),
            new DataColumn("Sample_rate",typeof(double)) ,
            new DataColumn("Despatch_Qty",typeof(double)) ,
            new DataColumn("productslno",typeof(int)),
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

            masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
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
                else if (col.ColumnName != "Sample_ERP_Code" && iCol == 3)
                {

                    success = false;

                }
                else if (col.ColumnName != "Product_Name" && iCol == 4)
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
                else if (col.ColumnName != "Sample_Rate" && iCol == 10)
                {

                    success = false;

                }
                else if (col.ColumnName != "Despatch_Qty" && iCol == 11)
                {
                    success = false;

                }
            }
            if (success == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('column Names are Not Matched');window.location='Sample_Despatch_Upload.aspx'</script>");
            }
            else
            {

                int columnCount = Dt.Columns.Count;


                if (columnCount > 12)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Column Not Matched.check the column heading');window.location='Sample_Despatch_Upload.aspx'</script>");
                }
                else if (columnCount < 12)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format Not Matched.Check the column heading');window.location='Sample_Despatch_Upload.aspx'</script>");
                }
                else
                {
                    //for (int j = 0; j < columnCount; j++)
                    //{
                    ////    DataColumn col = row.Columns[j];
                    ////    columns[j] = row[j].ToString().Trim();
                    ////    if (j.ColumnName != "div_name" && col.ColumnName != "sf_name" && col.ColumnName != "statename" && col.ColumnName != "sf_joining_date" && col.ColumnName != "hq" && col.ColumnName != "ASM" && col.ColumnName != "ASM HQ" && col.ColumnName != "ASM desig" && col.ColumnName != "RSM" && col.ColumnName != "RSM HQ" && col.ColumnName != "RSM desig" && col.ColumnName != "ZSM" && col.ColumnName != "ZSM HQ" && col.ColumnName != "ZSM desig" && col.ColumnName != "desg" && col.ColumnName != "empid" && col.ColumnName != "mnth" && col.ColumnName != "yr" && col.ColumnName != "Brand_Name" && col.ColumnName != "Prod_Name" && col.ColumnName != "Sale_ERP_Code" && col.ColumnName != "Pack" && col.ColumnName != "rate" && col.ColumnName != "CogCost" && col.ColumnName != "sales_rtn")
                    ////{
                    ////    }

                    //}

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
                        string prodcodeslno = "";
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
                            else if (Dt.Columns[j].ColumnName == "Sample_ERP_Code")
                            {
                                String filter1 = "sample_erp_code='" + columns[3] + "'";
                                rows1 = procode.Select(filter1);
                                if (rows1.Count() > 0)
                                {
                                    foreach (DataRow r in rows1)
                                    {

                                        prodcodeslno = r["Product_Code_SlNo"].ToString();
                                    }
                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Despatch_Qty")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum1 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }
                            else if (Dt.Columns[j].ColumnName == "Sample_Rate")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum2 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }

                        }

                        if (prodcodeslno == "" || SFcode == "" || isnum1 == false || isnum2 == false || columns[11].Trim() == "" || columns[3].Trim() == "" || columns[2].Trim() == "" || columns[10].Trim() == "")
                        {
                            if (isnum1 == false)
                            {
                                desdty = "Despatch Qty Allowed Only Number" + '/';
                            }
                            else if (columns[11].Trim() == "")
                            {
                                desdty = " Despatch Qty empty"+'/';
                            }
                            if (isnum1 == false)
                            {
                                desdtyrate = "Sample rate Allowed Only Number" + '/';
                            }
                            else if (columns[10].Trim() == "")
                            {
                                desdtyrate = " Sample rate empty" + '/';
                            }
                            if(columns[3].Trim() == "")
                            {
                                serpcode = "Sample ERP Code Empty" + '/';
                            }
                            else if (prodcodeslno == "")
                            {
                                serpcode = "Sample ERP Code Not Matched" + '/';
                            }
                            if (columns[2].Trim() == "")
                            {
                                sfempid = "Employee code Empty" + '/';
                            }
                            else if (SFcode == "")
                            {
                                sfempid = "Employee code Not Matched" + '/';
                            }
                            //if (sfempid == "" && serpcode == "")
                            //{
                            //    remarks = desdty + sfempid + serpcode;
                            //    ro[dtc.Columns.Count - 2] = rowno;
                            //    ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                            //}
                            //else
                            //{
                                remarks = sfempid + serpcode+ desdtyrate + desdty;
                                ro[dtc.Columns.Count - 2] = rowno;
                                ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1) ;
                          //  }

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
                        //    dtinv = Convert.ToDateTime(columns[6].Trim());
                        //    strinv = dtinv.Month + "-" + dtinv.Day + "-" + dtinv.Year;
                        //}
                        //else
                        //{
                        //    strinv = null;
                        //}
                        //if (columns[8].Trim() != "")
                        //{
                        //    dtlr = Convert.ToDateTime(columns[8].Trim());
                        //    strlr = dtlr.Month + "-" + dtlr.Day + "-" + dtlr.Year;
                        //}
                        //else
                        //{
                        //    strlr = null;
                        //}
                        if (dtcopy.Rows.Count == 0)
                        {

                            if (masdt.Count() > 0)
                            {

                                DT_tbl_FirstUpdateTable.Rows.Add(masdt[0]["Trans_sl_No"], SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, System.DateTime.Now, System.DateTime.Now);
                                DT_tbl_SecondTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, masdt[0]["Trans_sl_No"], div_code, columns[4].ToString().Replace("'", " "), columns[9].ToString().Replace("'", " "), Convert.ToDouble(columns[10].Replace("'", " ")), Convert.ToDouble(columns[11].Replace("'", " ")), Convert.ToInt32(prodcodeslno), columns[5].Replace("'", " "), columns[6].Trim(), columns[7].Replace("'", " "), columns[8].Trim());
                                //foreach (string strslno in DT_tbl_FirstUpdateTable.Rows)
                                //{
                                //    if (strslno != "")
                                //    {

                                //      //  transslno += strslno[0]["Trans_sl_No"] + ',';
                                //    }
                                //}
                            }
                            else
                            {
                                DT_tbl_FirstTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, System.DateTime.Now, System.DateTime.Now);
                                DT_tbl_SecondTable.Rows.Add(SFcode, ddlMonth.SelectedValue, ddlYear.SelectedValue, 0, div_code, columns[4].ToString().Replace("'", " "), columns[9].ToString().Replace("'", " "), Convert.ToDouble(columns[10].Replace("'", " ")), Convert.ToDouble(columns[11].Replace("'", " ")), Convert.ToInt32(prodcodeslno), columns[5].Replace("'", " "), columns[6].Trim(), columns[7].Replace("'", " "), columns[8].Trim());
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
                string filePath = Server.MapPath("~/Not_upl_sample/");
                //   string fileName = ("PrimaryBill_" + (System.DateTime.Now.Date).ToString() + "_" + div_code + ".xls");
                string fileName = ("Sampleupld_" + div_code + ".xls");

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
                                    bulkCopy_tbl_FirstTable.DestinationTableName = "dbo.Trans_Sample_Despatch_head";
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

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists. in saving header');window.location='Sample_Despatch_Upload.aspx'</script>");



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
                                //        command.CommandText = "delete from Trans_Sample_Despatch_details where trans_Sl_no in(" + transslno + ") ";
                                //        command.ExecuteNonQuery();
                                //    }
                                //}
                                //else
                                //{
                                //    //overwite with exist(only insert)
                                //    if (transslno != "")
                                //    {
                                //        command.CommandText = "update Trans_Sample_Despatch_head set Updated_Date=getdate() where trans_Sl_no in(" + transslno + ") ";
                                //        command.ExecuteNonQuery();
                                //    }
                                //}

                                //masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
                                //DataRow[] filterdr = null;
                                //command.CommandText = "select * from Trans_Sample_Despatch_head where division_code='" + div_code + "' and trans_month='" + ddlMonth.SelectedValue + "' and trans_year='" + ddlYear.SelectedValue + "'";
                                //SqlDataAdapter da2 = new SqlDataAdapter(command);
                                //DataTable dt2 = new DataTable();
                                //da2.Fill(dt2);
                               // foreach (DataRow mr in dt2.Rows)
                                //{
                                //    string filter = "";
                                //    filter = "sf_code='" + mr["sf_code"] + "'";

                                //    filterdr = DT_tbl_SecondTable.Select(filter);
                                //    double smpledd = 0;
                                //    foreach (DataRow r in filterdr)
                                //    {
                                //        r["Trans_sl_No"] = mr["Trans_sl_No"].ToString();

                                //        smpledd = smpledd + Convert.ToDouble(r["Despatch_Qty"].ToString());

                                //        Int32 count1 = 0;
                                //        if (rdochksave.SelectedValue == "1")
                                //        {
                                //            command.CommandText = "select count(sl_no) from Trans_Sample_Stock_FFWise_AsonDate where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "'";
                                //            count1 = (Int32)command.ExecuteScalar();
                                //            if (count1 > 0)
                                //            {
                                //                //command.CommandText = "update Trans_Sample_Stock_FFWise_AsonDate set Sample_AsonDate=sum(isnull(Sample_AsonDate,0)-'" + r["Despatch_Qty"] + "'+'" + r["Despatch_Qty"] + "') where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "' ";
                                //                //command.ExecuteNonQuery();
                                //                command.CommandText = "insert into bkp_Trans_Sample_Stock_FFWise_AsonDate select sf_code,prod_detail_sl_no,sample_asondate,division_code,getdate() from Trans_Sample_Stock_FFWise_AsonDate";
                                //                command.ExecuteNonQuery();
                                //                //command.CommandText = "delete Trans_Sample_Stock_FFWise_AsonDate  where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "' ";
                                //                //command.ExecuteNonQuery();
                                //                string sfcode = r["sf_code"].ToString();
                                //                string prdcode = r["productslno"].ToString();
                                //                double minusqty = 0;
                                //                DataTable smpprd = dcmas.getsampledetailsproduct(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code, sfcode, prdcode);
                                //                if (smpprd.Rows.Count > 0)
                                //                {
                                //                    minusqty = Convert.ToDouble(smpprd.Rows[0]["Despatch_Qty"].ToString());
                                //                }
                                //                command.CommandText = "update Trans_Sample_Stock_FFWise_AsonDate set Sample_AsonDate=isnull(Sample_AsonDate,0) - " + minusqty + " + " + r["Despatch_Qty"] + " where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "' ";
                                //                command.ExecuteNonQuery();
                                //                //command.CommandText = "insert into Trans_Sample_Stock_FFWise_AsonDate value() ('" + r["sf_code"] + "','" + r["productslno"] + "','" + r["Despatch_Qty"] + "','" + div_code + "') ";
                                //                //command.ExecuteNonQuery();
                                //            }
                                //            else
                                //            {
                                //                //backup
                                //                command.CommandText = "insert into bkp_Trans_Sample_Stock_FFWise_AsonDate select sf_code,prod_detail_sl_no,sample_asondate,division_code,getdate() from Trans_Sample_Stock_FFWise_AsonDate";
                                //                command.ExecuteNonQuery();

                                //                command.CommandText = "insert into Trans_Sample_Stock_FFWise_AsonDate values ('" + r["sf_code"] + "','" + r["productslno"] + "','" + r["Despatch_Qty"] + "','" + div_code + "') ";
                                //                command.ExecuteNonQuery();
                                //            }
                                //        }
                                //        else
                                //        {
                                //            command.CommandText = "select count(sl_no) from Trans_Sample_Stock_FFWise_AsonDate where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "'";
                                //            count1 = (Int32)command.ExecuteScalar();
                                //            if (count1 > 0)
                                //            {
                                //                //backup
                                //                command.CommandText = "insert into bkp_Trans_Sample_Stock_FFWise_AsonDate select sf_code,prod_detail_sl_no,sample_asondate,division_code,getdate() from Trans_Sample_Stock_FFWise_AsonDate";
                                //                command.ExecuteNonQuery();

                                //                command.CommandText = "update Trans_Sample_Stock_FFWise_AsonDate set Sample_AsonDate=isnull(Sample_AsonDate,0)+'" + r["Despatch_Qty"] + "' where sf_code='" + r["sf_code"] + "' and Prod_Detail_Sl_No='" + r["productslno"] + "' ";
                                //                command.ExecuteNonQuery();
                                //            }
                                //            else
                                //            {
                                //                //backup
                                //                command.CommandText = "insert into bkp_Trans_Sample_Stock_FFWise_AsonDate select sf_code,prod_detail_sl_no,sample_asondate,division_code,getdate() from Trans_Sample_Stock_FFWise_AsonDate";
                                //                command.ExecuteNonQuery();

                                //                command.CommandText = "insert into Trans_Sample_Stock_FFWise_AsonDate values ('" + r["sf_code"] + "','" + r["productslno"] + "','" + r["Despatch_Qty"] + "','" + div_code + "') ";
                                //                command.ExecuteNonQuery();
                                //            }
                                //        }
                                //    }
                                //}


                                if (rdochksave.SelectedValue == "1")
                                {
                                    //overwite with exist(deleta and insert)
                                    if (transslno != "")
                                    {
                                        command.CommandText = "delete from Trans_Sample_Despatch_details where trans_Sl_no in(" + transslno + ") ";
                                        command.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    //overwite with exist(only insert)
                                    if (transslno != "")
                                    {
                                        command.CommandText = "update Trans_Sample_Despatch_head set Updated_Date=getdate() where trans_Sl_no in(" + transslno + ") ";
                                        command.ExecuteNonQuery();
                                    }
                                }

                                //masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
                                  DataRow[] filterdr = null;
                                  command.CommandText = "select * from Trans_Sample_Despatch_head where division_code='" + div_code + "' and trans_month='" + ddlMonth.SelectedValue + "' and trans_year='" + ddlYear.SelectedValue + "'";
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
                                    bulkCopy_tbl_SecondTable.DestinationTableName = "dbo.Trans_Sample_Despatch_details";
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Trans_sl_No", "Trans_sl_No");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Division_Code", "Division_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("product_name", "Product_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Product_Sale_Unit", "Product_Sale_Unit");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Sample_rate", "Sample_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Despatch_Qty", "Despatch_Actual_qty");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Despatch_Qty", "Despatch_Qty_Bk");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("productslno", "productc");
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
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sample Uploaded Successfully');window.location='Sample_Despatch_Upload.aspx'</script>");
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                        transaction.Rollback();
                    //DataTable dtsamplehead = null;
                    dcmas.deletesampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);


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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format not Matched');window.location='Sample_Despatch_Upload.aspx'</script>");
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
            string fileName = Server.MapPath("~\\Document\\Sample_Upload.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Sample_Upload.xlsx");
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
            string fileName = Server.MapPath("~/Not_upl_sample/" + "Sampleupld_" + div_code + ".xls");
            //Server.MapPath("");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Sampleupld_" + div_code + ".xls");
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