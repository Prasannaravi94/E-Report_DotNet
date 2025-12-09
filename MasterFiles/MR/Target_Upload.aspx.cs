
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

public partial class MasterFiles_Options_Target_Upload : System.Web.UI.Page
{
    string div_code = string.Empty;
    
    DataSet dsproduct = new DataSet();
    DataSet dsSalesForce = new DataSet();
    string output = string.Empty;
    string sfCode = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Designation = string.Empty;
    string sf_type = string.Empty;
   
    int year;
    DataTable dtcopy = new DataTable();
    DataTable dtc = new DataTable();
    SampleDespatch objSample = new SampleDespatch();
    
    DataSet ds;
    DataTable Dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //hHeading.InnerText = Page.Title;
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
           menu1.FindControl("btnBack").Visible = false;

            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Value", typeof(int));
                dt.Columns.Add("Text", typeof(string));

                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    int m = k;
                    m++;
                    // ddlYear.Items.Add(k.ToString() + " - " + m.ToString());
                    //ddlTYear.Items.Add(k.ToString());

                    dt.Rows.Add(k, k.ToString() + " - " + m.ToString());
                    // ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
                }
                ddlYear.DataValueField = "Value";
                ddlYear.DataTextField = "Text";
                ddlYear.DataSource = dt;
                ddlYear.DataBind();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
    }
    private void InsertData()
    {
        // string sf_code = string.Empty;
        //string sf_Username = string.Empty;
        //string strsfcode = string.Empty;
        //string Strtype = string.Empty;
        //string Pool_Name = string.Empty;
        //string Pool_NameNew = string.Empty;
        //string slNO = string.Empty;
        //SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        //string ChkReord = "select * from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
        //SqlCommand cmd;
        //cmd = new SqlCommand(ChkReord, con);
        //con.Open();
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dtR = new DataTable();
        //da.Fill(dtR);

        //if (dtR.Rows.Count > 0)
        //{
        //    foreach (DataRow dtRow in dtR.Rows)
        //    {
        //        slNO += dtRow["Trans_sl_No"].ToString() + ",";
        //    }
        //    slNO = slNO.Remove(slNO.Length - 1);
        //    string Qry = "delete from Trans_TargetFixation_Product_Details where Trans_sl_No in (" + slNO + ") AND Division_code='" + div_code + "' ";
        //    cmd = new SqlCommand(Qry, con);
        //    // con.Open();
        //    int _res1 = cmd.ExecuteNonQuery();


        //    string Qry2 = "delete from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
        //    cmd = new SqlCommand(Qry2, con);
        //    // con.Open();
        //    int _res2 = cmd.ExecuteNonQuery();
        //}
        //for (int i = 0; i < Dt.Rows.Count; i++)
        //{
        //    DataRow row = Dt.Rows[i];
        //    int columnCount = Dt.Columns.Count;
        //    string[] columns = new string[columnCount];
        //    for (int j = 0; j < columnCount; j++)
        //    {
        //        columns[j] = row[j].ToString().Trim();

        //    }

        //    SalesForce sf = new SalesForce();
        //    TargetFixation tar = new TargetFixation();
        //    dsSalesForce = sf.GetHq_Code_Target(div_code, columns[1].Trim());
        //    if (dsSalesForce.Tables[0].Rows.Count > 0)
        //    {

        //        string str = columns[1].Trim();
        //        if (str != "")
        //        {
        //            String stok = (String)Dt.Rows[i][1];

        //            if (str == stok)
        //            {
        //                int output1 = tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue);
        //                if (output1 == 0)
        //                {
        //                    output = tar.RecordHeadAdd_Target_UPL(div_code, ddlYear.SelectedValue, str);
        //                }
        //                else
        //                {
        //                    output = Convert.ToString(tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue));
        //                }

        //            }
        //            if (output != "0" || output != "")
        //            {
        //                Stockist prod = new Stockist();
        //                ds = prod.Get_Product_Code_Primary(div_code, columns[3].Trim());
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    str_Prod_SlNO = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //                    str_ProdName = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
        //                    str_ProdCode = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

        //                    if (columns[5].Trim() == "1" || columns[5].Trim() == "2" || columns[5].Trim() == "3")
        //                    {
        //                        year = Convert.ToInt32(ddlYear.SelectedValue) + 1;
        //                    }
        //                    else
        //                    {
        //                        year = Convert.ToInt32(ddlYear.SelectedValue);
        //                    }
        //                    tar.RecordDetailsAdd_Target_UPL(output, columns[1].Trim(), columns[2].Trim(), str_ProdCode, columns[5].Trim(), columns[6].Trim(), columns[7].Trim(), columns[8].Trim(), div_code, str_Prod_SlNO, str_ProdName, year);
        //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Target Uploaded Successfully!');", true);
        //                }
        //            }
        //        }

        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Unable to Upload!');", true);
        //    }
        //}




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
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Records are not available');window.location='Target_Upload.aspx'</script>");
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
            TargetFixation sf1 = new TargetFixation();
            dsproduct = sf1.Get_product_SlNo(div_code);
            DataTable procode = dsproduct.Tables[0];
            DataTable ststeRate = sf1.Get_product_state_rate(div_code);

            DataTable DT_tbl_FirstTable = new DataTable();
             DT_tbl_FirstTable.Columns.AddRange(new DataColumn[5]
            {
            //new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("Financial_Year",typeof(int)) ,
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("Created_Date", typeof(DateTime)),
            new DataColumn("Sf_HQ_Code", typeof(string))
            //new DataColumn("Updated_Date", typeof(DateTime))
            
           
           
          
          }
            );

            DataTable DT_tbl_SecondTable = new DataTable();
            DT_tbl_SecondTable.Columns.AddRange(new DataColumn[17]
        {
            new DataColumn("SF_Code", typeof(string)),
            new DataColumn("SF_hq_Code", typeof(string)),
            new DataColumn("Financial_Year", typeof(int)),
            new DataColumn("Trans_sl_No", typeof(int)),
            new DataColumn("Division_Code", typeof(int)),
            new DataColumn("Product_Code", typeof(string)),
            new DataColumn("Month", typeof(string)),
            new DataColumn("Quantity", typeof(double)),
            new DataColumn("MRP_Price",typeof(double)) ,
            new DataColumn("Retailor_Price",typeof(double)) ,
            new DataColumn("Distributor_Price",typeof(double)) ,
            new DataColumn("NSR_Price",typeof(double)) ,
            new DataColumn("Target_Price",typeof(double)) ,
            new DataColumn("Target_Value",typeof(double)) ,
            new DataColumn("Product_Name", typeof(string)),
            new DataColumn("Product_Sl_No", typeof(string)),
            new DataColumn("Sf_HQ_Name", typeof(string))
            
        
        
        }
            );


     

            DataTable masterDt = null;

          //  masterDt = dcmas.getinputHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
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
                else if (col.ColumnName != "HQ_Code" && iCol == 1)
                {

                    success = false;

                }
                else if (col.ColumnName != "HQ_Name" && iCol == 2)
                {

                    success = false;

                }
                else if (col.ColumnName != "Sale_ERP_Code" && iCol == 3)
                {

                    success = false;

                }
                else if (col.ColumnName != "Product_Name" && iCol == 4)
                {

                    success = false;

                }
                else if (col.ColumnName != "Month" && iCol == 5)
                {

                    success = false;

                }
                else if (col.ColumnName != "Target_Rate" && iCol == 6)
                {

                    success = false;

                }
                else if (col.ColumnName != "Target_Qty" && iCol == 7)
                {
                    success = false;

                }
                else if (col.ColumnName != "Target_Value" && iCol == 8)
                {
                    success = false;

                }
            }
            if (success == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('column Names are Not Matched');window.location='Target_Upload.aspx'</script>");
            }
            else
            {

                int columnCount = Dt.Columns.Count;


                if (columnCount > 9)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Column Not Matched.check the column heading');window.location='Target_Upload.aspx'</script>");
                }
                else if (columnCount < 9)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format Not Matched.Check the column heading');window.location='Target_Upload.aspx'</script>");
                }
                else
                {



                    Dt.Columns.Add("remarks");


                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        DataRow ro = dtc.Rows[i];
                        string number = @"^[0-9]+$";
                       // string floatnumber = @"^[0-9]*(?:\.[0-9]*)?$";
                        string floatnumber = @"^-?[0-9]\d*(\.\d+)?$";
                        string serpcode = string.Empty;
                        string sfempid = string.Empty;
                        string remarks = string.Empty;
                        bool isnum1 = true;
                        bool isnum2 = true;
                        bool isnum3 = true;
                        bool isnum4 = true;
                        string SFcode = "";
                        string SFHQcode = "";
                        string statecode = "";
                        string str_ProdCode = string.Empty;
                        string saleerpCode = "";
                        string str_ProdName = string.Empty;
                        string str_Prod_SlNO = string.Empty;
                        string desdty = "";
                        string monthvalidate = "";
                        string mrpprice = "";
                        string retailorprice = "";
                        string desdtyrate = "";
                        string desdtyvalue = "";
                        string DistPrice = "";
                        string NSRPrice = "";
                        string TargetPrice = "";
                        DataRow row = Dt.Rows[i];
                        int rowno = i + 2;
                        int mnthno = 0;
                        int columnCount1 = Dt.Columns.Count;
                        string[] columns = new string[columnCount];
                        DataRow[] rows1;
                        DataRow[] rows;
                        DataRow[] rows3;
                        for (int j = 0; j < columnCount; j++)
                        {

                            columns[j] = row[j].ToString().Trim();
                            if (Dt.Columns[j].ColumnName == "HQ_Code")
                            {
                                String filter = "sf_cat_code='" + columns[1] + "'";
                                rows = sfemp.Select(filter);
                                if (rows.Count() > 0)
                                {
                                    foreach (DataRow r in rows)
                                    {
                                        SFHQcode = r["sf_cat_code"].ToString();
                                        SFcode = r["Sf_Code"].ToString();
                                        statecode=r["state_code"].ToString();
                                    }
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Sale_ERP_Code")
                            {
                                String filter1 = "Sale_ERP_Code='" + columns[3] + "'";
                                rows1 = procode.Select(filter1);
                                if (rows1.Count() > 0)
                                {
                                    foreach (DataRow r in rows1)
                                    {

                                        str_Prod_SlNO = r["Product_Code_SlNo"].ToString();
                                        str_ProdName = r["Product_Detail_Name"].ToString();
                                        str_ProdCode = r["product_detail_code"].ToString();
                                        saleerpCode = r["sale_erp_code"].ToString();
                                    }
                                    if (statecode != "" && str_ProdCode != "")
                                    {
                                        string filterrate = "State_Code='" + statecode + "' and Product_Detail_Code='" + str_ProdCode + "'";
                                        rows3 = ststeRate.Select(filterrate);
                                        if (rows3.Count() > 0)
                                        {
                                            foreach (DataRow r3 in rows3)
                                            {
                                                mrpprice = r3["MRP_Price"].ToString();
                                                retailorprice = r3["Retailor_Price"].ToString();
                                                DistPrice = r3["Distributor_Price"].ToString();
                                                NSRPrice = r3["NSR_Price"].ToString();
                                                TargetPrice = r3["Target_Price"].ToString();
                                            }
                                        }
                                    }

                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Target_Qty")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum1 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }
                            else if (Dt.Columns[j].ColumnName == "Target_Rate")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum3 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }
                            else if (Dt.Columns[j].ColumnName == "Target_Value")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum4 = Regex.IsMatch(columns[j].Trim(), floatnumber);
                                //}

                            }
                            else if (Dt.Columns[j].ColumnName == "Month")
                            {
                                //if (columns[j].Trim() != "")
                                //{
                                isnum2 = Regex.IsMatch(columns[j].Trim(), number);
                                //}
                                if (isnum2 == true)
                                {
                                    string mnth = columns[5].ToString();
                                    mnthno = Convert.ToInt32(mnth);
                                }
                            }
                           

                        }

                        if (saleerpCode == "" || SFHQcode == "" || isnum2 == false || isnum1 == false || columns[1].Trim() == "" || columns[3].Trim() == "" || columns[5].Trim() == "" || columns[7].Trim() == "" || mnthno > 12 || mnthno == 0 || isnum3 == false || columns[6].Trim() == "" || isnum4 == false || columns[8].Trim() == "")
                          {
                            if (isnum1 == false)
                            {
                                desdty = "Target Qty Allowed Only Number" + '/';
                            }
                            else if (columns[7].Trim() == "")
                            {
                                desdty = " Target Qty empty" + '/';
                            }
                            if (isnum3 == false)
                            {
                                desdtyrate = "Target Rate Allowed Only Number" + '/';
                            }
                            else if (columns[6].Trim() == "")
                            {
                                desdtyrate = " Target Rate empty" + '/';
                            }
                            if (isnum4 == false)
                            {
                                desdtyvalue = "Target Value Allowed Only Number" + '/';
                            }
                            else if (columns[8].Trim() == "")
                            {
                                desdtyvalue = " Target Value empty" + '/';
                            }
                            if (columns[3].Trim() == "")
                            {
                                serpcode = "Sale ERP Code Empty" + '/';
                            }
                            else if (saleerpCode == "")
                            {
                                serpcode = "Sale ERP Code Not Matched" + '/';
                            }
                            if(columns[5].Trim() == "")
                            {
                                monthvalidate = "Month is Empty" + '/';
                            }
                           else if (isnum1 == false)
                            {
                                monthvalidate = "Month Allowed Only Number" + '/';
                            }
                            else if(mnthno>12 || mnthno==0)
                            {
                                monthvalidate = "Month Allowed Only Number between 1 to 12" + '/';
                            }
                            if (columns[1].Trim() == "")
                            {
                                sfempid = "HQ code Empty" + '/';
                            }
                            else if (SFcode == "")
                            {
                                sfempid = "HQ code Not Matched" + '/';
                            }
                            //if (sfempid == "" && serpcode == "")
                            //{
                                remarks =  sfempid + serpcode+ monthvalidate+ desdtyrate+ desdty+ desdtyvalue;
                                ro[dtc.Columns.Count - 2] = rowno;
                                ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                           // }
                            //else
                            //{
                            //    remarks = monthvalidate+ desdty + sfempid + serpcode;
                            //    ro[dtc.Columns.Count - 2] = rowno;
                            //    ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1) + " Not Matched";
                            //}

                            foreach (DataRow dr in dtc.Rows)
                            {

                                if (dtc.Rows[i] == dr)
                                {

                                    dtcopy.Rows.Add(dr.ItemArray);

                                }
                            }

                        }






                        
                        
                        Dictionary<String, String> values = new Dictionary<String, String>();
                        if (dtcopy.Rows.Count == 0)
                        {
                            if (columns[5].Trim() == "1" || columns[5].Trim() == "2" || columns[5].Trim() == "3")
                            {
                                year = Convert.ToInt32(ddlYear.SelectedValue) + 1;
                            }
                            else
                            {
                                year = Convert.ToInt32(ddlYear.SelectedValue);
                            }

                            DT_tbl_FirstTable.Rows.Add(SFcode, ddlYear.SelectedValue, div_code, System.DateTime.Now, SFHQcode);
                            DT_tbl_SecondTable.Rows.Add(SFcode, SFHQcode, year, 0, div_code, str_ProdCode, columns[5].ToString().Replace("'", " "), columns[7].ToString().Replace("'", " "), mrpprice, retailorprice, DistPrice, NSRPrice, TargetPrice, columns[8].ToString().Replace("'", " "), str_ProdName, str_Prod_SlNO,columns[2].Trim());

                        }
                    }

                }
            }


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
                string filePath = Server.MapPath("~/Not_Upl_Target/");
                //   string fileName = ("PrimaryBill_" + (System.DateTime.Now.Date).ToString() + "_" + div_code + ".xls");
                string fileName = ("Targetupld_" + div_code + ".xls");

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
                                RemoveDuplicateRowsFirtstable(DT_tbl_FirstTable, "Sf_HQ_Code");
                                transslno = "";
                                //RemoveDuplicateRows(DT_tbl_FirstUpdateTable, "SF_Code", ref transslno);

                                command.CommandText = "delete from Trans_TargetFixation_Product_details where trans_Sl_no in(select trans_sl_no from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ) ";
                                command.ExecuteNonQuery();
                                command.CommandText = "delete from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "'";
                                command.ExecuteNonQuery();
                                using (SqlBulkCopy bulkCopy_tbl_FirstTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                                {
                                    bulkCopy_tbl_FirstTable.BatchSize = 5000;
                                    bulkCopy_tbl_FirstTable.DestinationTableName = "dbo.Trans_TargetFixation_Product_Head";
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Sf_Code", "Sf_Code");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Financial_Year", "Financial_Year");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Division_Code", "Division_Code");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Created_Date", "Created_Date");
                                    // bulkCopy_tbl_FirstTable.ColumnMappings.Add("Updated_Date", "getdate()");
                                    bulkCopy_tbl_FirstTable.ColumnMappings.Add("Sf_HQ_Code", "Sf_HQ_Code");
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

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists. in saving header');window.location='Target_Upload.aspx'</script>");



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




                           


                                //masterDt = dcmas.getsampleHead(ddlMonth.SelectedValue, ddlYear.SelectedValue, div_code);
                                DataRow[] filterdr = null;
                                command.CommandText = "select * from Trans_TargetFixation_Product_Head where division_code='" + div_code + "' and  Financial_year='" + ddlYear.SelectedValue + "'";
                                SqlDataAdapter da2 = new SqlDataAdapter(command);
                                DataTable dt2 = new DataTable();
                                da2.Fill(dt2);
                                foreach (DataRow mr in dt2.Rows)
                                {
                                    string filter = "";
                                    filter = "sf_hq_code='" + mr["sf_hq_code"] + "'";

                                    filterdr = DT_tbl_SecondTable.Select(filter);

                                    foreach (DataRow r in filterdr)
                                    {
                                        r["Trans_sl_No"] = mr["Trans_sl_No"].ToString();
                                    }
                                }

                                using (SqlBulkCopy bulkCopy_tbl_SecondTable = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, transaction))
                                {


                                    bulkCopy_tbl_SecondTable.BatchSize = 5000;
                                    bulkCopy_tbl_SecondTable.DestinationTableName = "dbo.Trans_TargetFixation_Product_details";
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Trans_sl_No", "Trans_sl_No");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Product_Code", "Product_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Month", "Month");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Quantity", "Quantity");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Division_Code", "Division_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("MRP_Price", "MRP_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Retailor_Price", "Retailor_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Distributor_Price", "Distributor_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("NSR_Price", "NSR_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Target_Price", "Target_Price");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Target_Value", "Target_Value");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Product_Name", "Product_Name");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("SF_hq_Code", "Sf_HQ_Code");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Financial_Year", "Year");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Product_Sl_No", "Product_Sl_No");
                                    bulkCopy_tbl_SecondTable.ColumnMappings.Add("Sf_HQ_Name", "Sf_HQ_Name");


                                    bulkCopy_tbl_SecondTable.WriteToServer(DT_tbl_SecondTable);
                                }


                                transaction.Commit();
                                //IsSuccessSave = true;
                            }
                            connection.Close();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target Uploaded Successfully');window.location='Target_Upload.aspx'</script>");
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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Format not Matched');window.location='Target_Upload.aspx'</script>");
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
                            //objAdapter1.Fill(ds);
                            //Dt = ds.Tables[0];
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
            string fileName = Server.MapPath("~\\Document\\Target_Upload1.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Target_Upload1.xlsx");
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
            string fileName = Server.MapPath("~/Not_Upl_Target/" + "Targetupld_" + div_code + ".xls");
            //Server.MapPath("");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Targetupld_" + div_code + ".xls");
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