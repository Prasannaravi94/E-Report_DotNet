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
using System.Text.RegularExpressions;

public partial class MasterFiles_Options_Target_Upload : System.Web.UI.Page
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
    string str_ProdName = string.Empty;
    string str_Prod_SlNO = string.Empty;
    int year;
    DataTable dtcopy = new DataTable();
    DataTable dtc = new DataTable();
    SampleDespatch objSample = new SampleDespatch();
    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds;
     DataTable Dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            GetMyMonthList();
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;

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
    private void InsertData1()
    {
        // string sf_code = string.Empty;
        string sf_Username = string.Empty;
        string strsfcode = string.Empty;
        string Strtype = string.Empty;
        string Pool_Name = string.Empty;
        string Pool_NameNew = string.Empty;
        string slNO = string.Empty;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        string ChkReord = "select * from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
        SqlCommand cmd;
        cmd = new SqlCommand(ChkReord, con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtR = new DataTable();
        da.Fill(dtR);

        if (dtR.Rows.Count > 0)
        {
            //foreach (DataRow dtRow in dtR.Rows)
            //{
            //    slNO += dtRow["Trans_sl_No"].ToString() + ",";
            //}
            //slNO = slNO.Remove(slNO.Length - 1);
            string Qry = "delete from Trans_TargetFixation_Product_Details where month='" + monthId.SelectedValue + "' and trans_sl_no in(select trans_sl_no from Trans_TargetFixation_Product_Head where division_code='" + div_code + "' and Financial_Year='" + ddlYear.SelectedValue + "' ) ";
            cmd = new SqlCommand(Qry, con);
            // con.Open();
            int _res1 = cmd.ExecuteNonQuery();


            //string Qry2 = "delete from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
            //cmd = new SqlCommand(Qry2, con);
            //// con.Open();
            //int _res2 = cmd.ExecuteNonQuery();
        }
        for (int i = 0; i < Dt.Rows.Count; i++)
        {
            DataRow row = Dt.Rows[i];
            int columnCount = Dt.Columns.Count;
            string[] columns = new string[columnCount];
            for (int j = 0; j < columnCount; j++)
            {
                columns[j] = row[j].ToString().Trim();
            }

            SalesForce sf = new SalesForce();
            TargetFixation tar = new TargetFixation();
            dsSalesForce = sf.GetHq_Code_Target(div_code, columns[1].Trim());
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {

                string str = columns[1].Trim();
                if (str != "")
                {
                    String stok = (String)Dt.Rows[i][1];

                    if (str == stok)
                    {
                        int output1 = tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue);
                        if (output1 == 0)
                        {
                            output = tar.RecordHeadAdd_Target_UPL(div_code, ddlYear.SelectedValue, str);
                        }
                        else
                        {
                            output = Convert.ToString(tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue));
                        }

                    }
                    if (output != "0")
                    {
                        Stockist prod = new Stockist();
                        ds = prod.Get_Product_Code_Primary(div_code, columns[3].Trim());
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            str_Prod_SlNO = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            str_ProdName = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            str_ProdCode = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                            if (columns[5].Trim() == "1" || columns[5].Trim() == "2" || columns[5].Trim() == "3")
                            {
                                year = Convert.ToInt32(ddlYear.SelectedValue) + 1;
                            }
                            else
                            {
                                year = Convert.ToInt32(ddlYear.SelectedValue);
                            }
                            tar.RecordDetailsAdd_Target_UPL(output, columns[1].Trim(), columns[2].Trim(), str_ProdCode, columns[5].Trim(), columns[6].Trim(), columns[7].Trim(), columns[8].Trim(), div_code, str_Prod_SlNO, str_ProdName, year);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Target Uploaded Successfully!');", true);
                        }
                    }
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Unable to Upload!');", true);
            }
        }
    }







    private void InsertData()
    {
       


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
                        //string floatnumber = @"^[0-9]*(?:\.[0-9]*)?$";
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
                                        statecode = r["state_code"].ToString();
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

                        //if (saleerpCode == "" || SFHQcode == "" || isnum2 == false || isnum1 == false || columns[1].Trim() == "" || columns[3].Trim() == "" || columns[5].Trim() == "" || columns[7].Trim() == "" || mnthno > 12 || mnthno == 0 || isnum3 == false || columns[6].Trim() == "" || isnum4 == false || columns[8].Trim() == "")
                            if (saleerpCode == "" || SFHQcode == "" || isnum2 == false  || columns[1].Trim() == "" || columns[3].Trim() == "" || columns[5].Trim() == "" || columns[7].Trim() == "" || mnthno > 12 || mnthno == 0  || columns[6].Trim() == "" || columns[8].Trim() == "")
                            {
                            //if (isnum1 == false)
                            //{
                            //    desdty = "Target Qty Allowed Only Number" + '/';
                            //}
                            //else 
                            if (columns[7].Trim() == "")
                            {
                                desdty = " Target Qty empty" + '/';
                            }
                            //if (isnum3 == false)
                            //{
                            //    desdtyrate = "Target Rate Allowed Only Number" + '/';
                            //}
                            //else 
                            if (columns[6].Trim() == "")
                            {
                                desdtyrate = " Target Rate empty" + '/';
                            }
                            //if (isnum4 == false)
                            //{
                            //    desdtyvalue = "Target Value Allowed Only Number" + '/';
                            //}
                            //else
                            if (columns[8].Trim() == "")
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
                            if (columns[5].Trim() == "")
                            {
                                monthvalidate = "Month is Empty" + '/';
                            }
                            else if (isnum1 == false)
                            {
                                monthvalidate = "Month Allowed Only Number" + '/';
                            }
                            else if (mnthno > 12 || mnthno == 0)
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
                            remarks = sfempid + serpcode + monthvalidate + desdtyrate + desdty + desdtyvalue;
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



                SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

                string ChkReord = "select * from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
                SqlCommand cmd;
                cmd = new SqlCommand(ChkReord, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtR = new DataTable();
                da.Fill(dtR);

                if (dtR.Rows.Count > 0)
                {
                    //foreach (DataRow dtRow in dtR.Rows)
                    //{
                    //    slNO += dtRow["Trans_sl_No"].ToString() + ",";
                    //}
                    //slNO = slNO.Remove(slNO.Length - 1);
                    string Qry = "delete from Trans_TargetFixation_Product_Details where month='" + monthId.SelectedValue + "' and trans_sl_no in(select trans_sl_no from Trans_TargetFixation_Product_Head where division_code='" + div_code + "' and Financial_Year='" + ddlYear.SelectedValue + "' ) ";
                    cmd = new SqlCommand(Qry, con);
                    // con.Open();
                    int _res1 = cmd.ExecuteNonQuery();


                    //string Qry2 = "delete from Trans_TargetFixation_Product_Head where Financial_Year='" + ddlYear.SelectedValue + "' AND Division_code='" + div_code + "' ";
                    //cmd = new SqlCommand(Qry2, con);
                    //// con.Open();
                    //int _res2 = cmd.ExecuteNonQuery();
                }


                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    DataRow row = Dt.Rows[i];
                    int columnCount = Dt.Columns.Count;
                    string[] columns = new string[columnCount];
                    for (int j = 0; j < columnCount; j++)
                    {
                        columns[j] = row[j].ToString().Trim();
                    }

                    SalesForce sf2 = new SalesForce();
                    TargetFixation tar = new TargetFixation();
                    dsSalesForce = sf2.GetHq_Code_Target(div_code, columns[1].Trim());
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {

                        string str = columns[1].Trim();
                        if (str != "")
                        {
                            String stok = (String)Dt.Rows[i][1];

                            if (str == stok)
                            {
                                int output1 = tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue);
                                if (output1 == 0)
                                {
                                    output = tar.RecordHeadAdd_Target_UPL(div_code, ddlYear.SelectedValue, str);
                                }
                                else
                                {
                                    output = Convert.ToString(tar.RecordExistTargetHead(str, div_code, ddlYear.SelectedValue));
                                }

                            }
                            if (output != "0")
                            {
                                Stockist prod = new Stockist();
                                ds = prod.Get_Product_Code_Primary(div_code, columns[3].Trim());
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    str_Prod_SlNO = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    str_ProdName = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                    str_ProdCode = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                                    if (columns[5].Trim() == "1" || columns[5].Trim() == "2" || columns[5].Trim() == "3")
                                    {
                                        year = Convert.ToInt32(ddlYear.SelectedValue) + 1;
                                    }
                                    else
                                    {
                                        year = Convert.ToInt32(ddlYear.SelectedValue);
                                    }
                                    tar.RecordDetailsAdd_Target_UPL(output, columns[1].Trim(), columns[2].Trim(), str_ProdCode, columns[5].Trim(), columns[6].Trim(), columns[7].Trim(), columns[8].Trim(), div_code, str_Prod_SlNO, str_ProdName, year);
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Target Uploaded Successfully!');", true);
                                }
                            }
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SuccessMsg", "alert('Unable to Upload!');", true);
                    }
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
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
        monthId.SelectedValue = DateTime.Now.Month.ToString();
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
}