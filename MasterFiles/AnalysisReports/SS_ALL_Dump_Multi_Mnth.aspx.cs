using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using System.IO;
using System.Data.Sql;
using DBase_EReport;
using ClosedXML;

public partial class MasterFiles_AnalysisReports_SS_ALL_Dump_Multi_Mnth : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsDes = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                //FillColor();
                // FillMRManagers1();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

                FillColor();
                FillMRManagers();
                BindDate();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

        FillColor();
    }
    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }

    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }
        FillColor();


    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlFrmYear.Items.Add(k.ToString());
                //ddlToYear.Items.Add(k.ToString());
            }
            //ddlFrmYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();

            //ddlToYear.Text = DateTime.Now.Year.ToString();
            //ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();

            DateTime FromMonth = DateTime.Now;
            DateTime ToMonth = DateTime.Now;
            txtFromMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Value = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int FromMonth = Convert.ToInt32(Convert.ToDateTime(txtFromMonthYear.Text).Month);
        //int FromYear = Convert.ToInt32(Convert.ToDateTime(txtFromMonthYear.Text).Year);
        //int ToMonth = Convert.ToInt32(Convert.ToDateTime(txtToMonthYear.Text).Month);
        //int ToYear = Convert.ToInt32(Convert.ToDateTime(txtToMonthYear.Text).Year);


        int FromMonth = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Month);
        int FromYear = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Year);
        int ToMonth = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Month);
        int ToYear = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Year);

        int months = (ToYear - FromYear) * 12 + ToMonth - FromMonth; 

        //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = FromMonth;
        int cyear = FromYear;

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("SecSale_Analysis_All_MR_Multi_Mnth_Dump", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        //cmd.Parameters.AddWithValue("@FMonth", Convert.ToInt32(ddlFrmMonth.Text));
        //cmd.Parameters.AddWithValue("@FYear", Convert.ToInt32(ddlFrmYear.Text)); 
        //cmd.Parameters.AddWithValue("@dtdes", dtSpec);

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        ds.Tables[0].Columns.Remove("sf_code");
        ds.Tables[0].Columns.Remove("sf_type");
        ds.Tables[0].Columns.Remove("div_code");
        ds.Tables[0].Columns.Remove("sf_code1");
        ds.Tables[0].Columns.Remove("stok_code");
        ds.Tables[0].Columns.Remove("Product_code");

        dt = ds.Tables[0];
        int countCol = dt.Columns.Count;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(ddlFrmMonth.Text)).ToString().Substring(0, 3);
        //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(ddlToMonth.Text)).ToString().Substring(0, 3);

        //int months1 = (Convert.ToInt32(ddlToYear.Text) - Convert.ToInt32(ddlFrmYear.Text)) * 12 + Convert.ToInt32(ddlToMonth.Text) - Convert.ToInt32(ddlFrmMonth.Text);
        int months1 = (ToYear - FromYear) * 12 + ToMonth - FromMonth; 
        //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth1 = Convert.ToInt32(ddlFrmMonth.Text);
        //int cyear1 = Convert.ToInt32(ddlFrmYear.Text);

        int cmonth1 = FromMonth;
        int cyear1 = FromYear;

        string ss_code = string.Empty;
        if (months1 >= 0)
        {
            int Codeno = 1;
            for (int j = 1; j <= months1 + 1; j++)
            {
                for (int iCol = 0; iCol < countCol; iCol++)
                {
                    DataColumn col = dt.Columns[iCol];
                    if (col.ColumnName != "SNO" && col.ColumnName != "Fieldforce Name" && col.ColumnName != "Designation" && col.ColumnName != "HQ" && col.ColumnName != "Reporting To" &&  col.ColumnName != "stateName" && col.ColumnName != "subdiv" && col.ColumnName != "stk_Name" && col.ColumnName != "stk_Hq" && col.ColumnName != "Stk_Erp_code" && col.ColumnName != "Prod_Name" && col.ColumnName != "Pack" && col.ColumnName != "Prd_Erp_Code")
                    {
                        if (col.ColumnName.Contains("_ABT") || col.ColumnName.Contains("_ACT"))
                        {
                            string[] ss = col.ColumnName.Split('_');
                            ss_code = ss[2].Trim().ToString();
                            SecSale sec = new SecSale();
                            DataSet dssec = new DataSet();
                            dssec = sec.getSaleMaster_Det(div_code, Convert.ToInt32(ss_code));

                            if (col.ColumnName.Contains(j + "_" + Codeno + "_" + ss_code + "_ABT"))
                            {
                                dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " (" + dssec.Tables[0].Rows[0]["Sec_Sale_Name"].ToString() + " " + " - Qty )";
                                dt.AcceptChanges();
                            }
                            if (col.ColumnName.Contains(j + "_" + Codeno + "_" + ss_code + "_ACT"))
                            {
                                dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " (" + dssec.Tables[0].Rows[0]["Sec_Sale_Name"].ToString() + " " + " - Value )";
                                dt.AcceptChanges();

                                if (Codeno == 2)
                                {
                                    Codeno = 1;
                                }
                                else
                                {
                                    Codeno = Codeno + 1;
                                }
                            }
                        }
                        //if (col.ColumnName.Contains("DET"))
                        //{
                        //    dt.Columns[iCol].ColumnName = "Total";
                        //    dt.AcceptChanges();
                        //}
                        //if (col.ColumnName.Contains("EET"))
                        //{
                        //    dt.Columns[iCol].ColumnName = "AVERAGE/MONTH";
                        //    dt.AcceptChanges();
                        //}
                    }
                }
                cmonth1 = cmonth1 + 1;
                if (cmonth1 == 13)
                {
                    cmonth1 = 1;
                    cyear1 = cyear1 + 1;
                }
            }
        }


        //for (int i = 5; i < dt.Columns.Count; i++)
        //{
        //    foreach (DataColumn c in dt.Columns)
        //    {

        //        if (c.ColumnName != "sno" && c.ColumnName != "sf_name" && c.ColumnName != "ListedDr_Name" && c.ColumnName != "Cat" && c.ColumnName != "Spec")
        //        {
        //            if (c.ColumnName == "0_0_AAT" || c.ColumnName == "0_0_ABT")
        //            {
        //                dt.Columns[i].ColumnName = "MR";
        //                dt.AcceptChanges();
        //            }
        //            else
        //            {
        //                des_code = c.ColumnName.Split('_').First();
        //                Designation des = new Designation();
        //                dsdes = des.getDesig_graph(des_code, div_code);
        //                dt.Columns[i].ColumnName = dsdes.Tables[0].Rows[0]["Designation_Short_Name"].ToString();
        //                dt.AcceptChanges();
        //            }
        //            break;
        //        }

        //    }

        //}
        //

        // ExportDataSetToExcel(ds);      
        /**
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"Dump\"");

        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.Font.Name = "Verdana";
                dg.Font.Size = 10;
                dg.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                dg.HeaderStyle.BackColor = System.Drawing.Color.Blue;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();
            }

        }**/
        ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
        wbook.Worksheets.Add(dt, "tab1");
        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //Provide you file name here
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"SecSale_Consolidate_Dump.xlsx\"");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            wbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    }


}