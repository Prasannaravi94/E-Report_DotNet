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

public partial class MasterFiles_AnalysisReports_PrimarySale_Multi_Mnth_Dump : System.Web.UI.Page
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
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                //FillColor();
                FillMRManagers1();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                //FillColor();
                FillMRManagers() ;
                ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
                ddlFieldForce.Enabled = false;
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
                FillManagers();
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
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
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
       
        DataSet DsAudit = sf.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "Desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();
            }
        }
        else
        {
            // Fetch Managers Audit Team
            DataSet dsmgrsf = new DataSet();
            DataTable dt = sf.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
           DataSet dsTP = dsmgrsf;

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsTP;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsTP;
            ddlSF.DataBind();
        }
        FillColor();
    }
    private void FillManagers()
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

    //private void FillMRManagers1()
    //{
    //    SalesForce sf = new SalesForce();

    //    dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "des_color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();
    //    }
    //    FillColor();


    //}
    private void FillMRManagers1()
    {
        SalesForce sf = new SalesForce();
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {
            dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam_GetMGR(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            //ddlSF.DataTextField = "Desig_Color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

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
            txtFromMonthYear.Value= FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            txtToMonthYear.Value= ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //
        if (rdolstrpt.SelectedValue == "7")
        {
                int FromMonth = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Month);
				int FromYear = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Year);
				int ToMonth = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Month);
				int ToYear = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Year);

            //int months = (Convert.ToInt32(ddlToYear.Text) - Convert.ToInt32(ddlFrmYear.Text)) * 12 + Convert.ToInt32(ddlToMonth.Text) - Convert.ToInt32(ddlFrmMonth.Text); 
            //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int months = (ToYear - FromYear) * 12 + ToMonth - FromMonth;
            int cmonth = FromMonth;
            int cyear = FromYear;

            int iMn = 0, iYr = 0;
            DataTable dtMnYr = new DataTable();
            dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
            dtMnYr.Columns["INX"].AutoIncrementStep = 1;
            dtMnYr.Columns.Add("MNTH", typeof(int));
            dtMnYr.Columns.Add("YR", typeof(int));
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
            string proce_Name = "";
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();
            SqlCommand cmd = new SqlCommand("Primary_bill_HQwise_P", con);
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
            ds.Tables[0].Columns.Remove("Stockist_Code");
            ds.Tables[0].Columns.Remove("Stockist_Code1");

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
                        if (col.ColumnName != "slno" && col.ColumnName != "HQ_Code" && col.ColumnName != "Stockist_Name" && col.ColumnName != "ERP_Code" && col.ColumnName != "HQ_Name")
                        {
                            if (col.ColumnName.Contains("_ACT"))
                            {
                                string[] ss = col.ColumnName.Split('_');
                                ss_code = ss[2].Trim().ToString();
                                SecSale sec = new SecSale();
                                DataSet dssec = new DataSet();
                                //   dssec = sec.getSaleMaster_Det(div_code, Convert.ToInt32(ss_code));

                                if (col.ColumnName.Contains(j + "_" + "0_ACT"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( Value )";
                                    dt.AcceptChanges();
                                }
                                //if (col.ColumnName.Contains(j + "_" + Codeno + "_" + ss_code + "_ACT"))
                                //{
                                //    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " (" + dssec.Tables[0].Rows[0]["Sec_Sale_Name"].ToString() + " " + " - Value )";
                                //    dt.AcceptChanges();

                                //    if (Codeno == 2)
                                //    {
                                //        Codeno = 1;
                                //    }
                                //    else
                                //    {
                                //        Codeno = Codeno + 1;
                                //    }
                                //}
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



            ClosedXML.Excel.XLWorkbook wbook = new ClosedXML.Excel.XLWorkbook();
            wbook.Worksheets.Add(dt, "tab1");
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Provide you file name here
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"SecSale_FF_HQ_Dump.xlsx\"");

            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wbook.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        else
        {

      int FromMonth = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Month);
        int FromYear = Convert.ToInt32(Convert.ToDateTime(this.txtFromMonthYear.Value.ToString()).Year);
        int ToMonth = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Month);
        int ToYear = Convert.ToInt32(Convert.ToDateTime(this.txtToMonthYear.Value.ToString()).Year);           

            //int months = (Convert.ToInt32(ddlToYear.Text) - Convert.ToInt32(ddlFrmYear.Text)) * 12 + Convert.ToInt32(ddlToMonth.Text) - Convert.ToInt32(ddlFrmMonth.Text); 
            //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int months = (ToYear - FromYear) * 12 + ToMonth - FromMonth;
            int cmonth = FromMonth;
            int cyear = FromYear;

            int iMn = 0, iYr = 0;
            DataTable dtMnYr = new DataTable();
            dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
            dtMnYr.Columns["INX"].AutoIncrementStep = 1;
            dtMnYr.Columns.Add("MNTH", typeof(int));
            dtMnYr.Columns.Add("YR", typeof(int));
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
            string proce_Name = "";


            if (rdolstrpt.SelectedValue == "1")
            {
                proce_Name = "PrimarySale_MultiMnth_FF";
            }
            else if (rdolstrpt.SelectedValue == "2")
            {
                proce_Name = "PrimarySale_MultiMnth_Prodwise";
            }
            string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
            SqlConnection con = new SqlConnection(strConn);
            con.Open();

            SqlCommand cmd = new SqlCommand(proce_Name, con);
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
            if (rdolstrpt.SelectedValue == "1")
            {
                ds.Tables[0].Columns.Remove("sf_code");
                ds.Tables[0].Columns.Remove("sf_cat_code");
                ds.Tables[0].Columns.Remove("sf_code1");
            }
            else if (rdolstrpt.SelectedValue == "2")
            {
                ds.Tables[0].Columns.Remove("Product_Code");
                ds.Tables[0].Columns.Remove("Product_Code1");
            }

            dt = ds.Tables[0];
            int countCol = dt.Columns.Count;

            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
            //string strFMonthName = mfi.GetMonthName(Convert.ToInt16(ddlFrmMonth.Text)).ToString().Substring(0, 3);
            //string strTMonthName = mfi.GetMonthName(Convert.ToInt16(ddlToMonth.Text)).ToString().Substring(0, 3);

            //int months1 = (Convert.ToInt32(ddlToYear.Text) - Convert.ToInt32(ddlFrmYear.Text)) * 12 + Convert.ToInt32(ddlToMonth.Text) - Convert.ToInt32(ddlFrmMonth.Text); 
            //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth1 = Convert.ToInt32(ddlFrmMonth.Text);
            //int cyear1 = Convert.ToInt32(ddlFrmYear.Text);
            int months1 = (ToYear - FromYear) * 12 + ToMonth - FromMonth;
            int cmonth1 = FromMonth;
            int cyear1 = FromYear;

            bool Colmn_Cntrol = false;



            string ss_code = string.Empty;
            if (months1 >= 0)
            {
                int Codeno = 1;
                for (int j = 1; j <= months1 + 1; j++)
                {
                    for (int iCol = 0; iCol < countCol; iCol++)
                    {
                        DataColumn col = dt.Columns[iCol];

                        if (rdolstrpt.SelectedValue == "1")
                        {
                            Colmn_Cntrol = (col.ColumnName != "SNO" && col.ColumnName != "Fieldforce Name" && col.ColumnName != "Designation" && col.ColumnName != "HQ" && col.ColumnName != "Reporting_1" && col.ColumnName != "Reporting_1 HQ" && col.ColumnName != "Reporting_1 Desig" && col.ColumnName != "Reporting_2" && col.ColumnName != "Reporting_2 HQ" && col.ColumnName != "Reporting_2 Desig" && col.ColumnName != "HQ Name");
                        }
                        else if (rdolstrpt.SelectedValue == "2")
                        {
                            Colmn_Cntrol = (col.ColumnName != "SNO" && col.ColumnName != "Product Name" && col.ColumnName != "Pack" && col.ColumnName != "Group" && col.ColumnName != "Brand");
                        }

                        //if (col.ColumnName != "SNO" && col.ColumnName != "Fieldforce Name" && col.ColumnName != "Designation" && col.ColumnName != "HQ" && col.ColumnName != "Reporting_1" && col.ColumnName != "Reporting_1 HQ" && col.ColumnName != "Reporting_1 Desig" && col.ColumnName != "Reporting_2" && col.ColumnName != "Reporting_2 HQ" && col.ColumnName != "Reporting_2 Desig" && col.ColumnName != "HQ Name")
                        if (Colmn_Cntrol)
                        {
                            if (col.ColumnName.Contains("D_BAPrimary_Lsale") || col.ColumnName.Contains("D_BBPrimary_CSale") || col.ColumnName.Contains("D_BCPrimary_Targt") || col.ColumnName.Contains("D_BDPrimary_Ach") || col.ColumnName.Contains("D_BEPrimary_Grwth") || col.ColumnName.Contains("D_BFPrimary_PCPM"))
                            {
                                if (col.ColumnName.Contains("D_BAPrimary_Lsale"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1 - 1) + " ( NETLYVAL )";
                                    dt.AcceptChanges();
                                }

                                if (col.ColumnName.Contains("D_BBPrimary_CSale"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1) + " ( NETCYVAL )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains("D_BCPrimary_Targt"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1) + " ( TGT )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains("D_BDPrimary_Ach"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1) + " ( ACHT% )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains("D_BEPrimary_Grwth"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1) + " ( GTH% )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains("D_BFPrimary_PCPM"))
                                {
                                    dt.Columns[iCol].ColumnName = "Upto " + sf.getMonthName(Convert.ToInt32(ToMonth).ToString()) + "-" + (cyear1) + " ( PCP )";
                                    dt.AcceptChanges();
                                }
                            }
                            else if (col.ColumnName.Contains("_BAPrimary_Lsale") || col.ColumnName.Contains("_BBPrimary_CSale") || col.ColumnName.Contains("_BCPrimary_Targt") || col.ColumnName.Contains("_BDPrimary_Ach") || col.ColumnName.Contains("_BEPrimary_Grwth") || col.ColumnName.Contains("_BFPrimary_PCPM"))
                            {
                                if (col.ColumnName.Contains(j + "_BAPrimary_Lsale"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + (cyear1 - 1) + " ( NETLYVAL )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains(j + "_BBPrimary_CSale"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( NETCYVAL )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains(j + "_BCPrimary_Targt"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( TGT )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains(j + "_BDPrimary_Ach"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( ACHT% )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains(j + "_BEPrimary_Grwth"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( GTH% )";
                                    dt.AcceptChanges();
                                }
                                if (col.ColumnName.Contains(j + "_BFPrimary_PCPM"))
                                {
                                    dt.Columns[iCol].ColumnName = sf.getMonthName(cmonth1.ToString()) + "-" + cyear1 + " ( PCP )";
                                    dt.AcceptChanges();
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






}