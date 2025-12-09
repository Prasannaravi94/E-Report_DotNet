using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;

using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_AnalysisReports_rptMgr_Coverage : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsdcr = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string Join_Date = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    string tot_docSeen = "";
    string tot_Mor = "";
    string tot_Eve = "";
    string tot_Both = "";
    string tot_fldwrkDays = "";
    string strFieledForceName = string.Empty;
    string mode = string.Empty;
    DataTable dtrowClr = new DataTable();

    DataTable dtrowClr2 = new DataTable();
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int seen = 0;
    int Fw = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //div_code = Request.QueryString["div_Code"].ToString();
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();
            TMonth = Request.QueryString["To_Month"].ToString();
            TYear = Request.QueryString["To_year"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            mode = Request.QueryString["mode"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Manager - HQ - Coverage From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            dsSalesForce = sf.GetJoiningdate(sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Join_Date = dsSalesForce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            LblForceName.Text = "Field Force Name : " + strFieledForceName + "<span style='font-weight: bold;color:#0077FF;'> " + " (DOJ: " + Join_Date + ") " + "</span>";

            FillReport();
            FillMgr();
          //  CreateDynamicTable();
        }
       
     
    }
    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

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
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        if (mode == "1")
        {
            sProc_Name = "HQ_Coverage_Analysis_II"; 
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.CommandTimeout = 600;
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("hq");
            //dsts.Tables[0].Columns.Remove("sf_code1");
            //dsts.Tables[0].Columns.Remove("sf_type");
            //dsts.Tables[0].Columns.Remove("sf_Designation_Short_Name");
            //dsts.Tables[0].Columns.Remove("Sf_HQ");
            //dsts.Tables[0].Columns.Remove("sf");
            GrdFixation.DataSource = dsts.Tables[0];
            GrdFixation.DataBind();
        }
        else if (mode == "2")
        {
            sProc_Name = "HQ_Coverage_Analysis_Det"; 
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);

            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.CommandTimeout = 600;
            // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.Remove("sf_code");
            dsts.Tables[0].Columns.Remove("sf_code1");
            dsts.Tables[0].Columns.Remove("sf_type");
            dsts.Tables[0].Columns.Remove("sf_Designation_Short_Name");
            dsts.Tables[0].Columns.Remove("Sf_HQ");
          //  dsts.Tables[0].Columns.Remove("sf");
            GrdFixation.DataSource = dsts.Tables[0];
            GrdFixation.DataBind();
        }
      

    }

    private void FillMgr()
    {
        //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth = Convert.ToInt32(FMonth);
        //int cyear = Convert.ToInt32(FYear);

        //int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        ////
        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}
        int iMn = 0, iYr = 0, iTtlMnth;
        DataTable dtMnYr = new DataTable();
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        iTtlMnth = months;
        //DataTable dtMnYr = new DataTable();
        //int iMn = 0, iYr = 0;
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
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Manager_Hq_Coverage_new", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts2 = new DataSet();
        da.Fill(dsts2);
        dtrowClr2 = dsts2.Tables[0].Copy();

        dsts2.Tables[0].Columns.RemoveAt(3);
        dsts2.Tables[0].Columns.RemoveAt(2);
        dsts2.Tables[0].Columns.RemoveAt(0);
        grdMgr.DataSource = dsts2;
        grdMgr.DataBind();



    }




    protected void grdMgr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            if (dtrowClr2.Rows[indx][1].ToString() == "Day Wise Detail" || dtrowClr2.Rows[indx][1].ToString() == "Doctor Details")
            {
                //e.Row.Cells[0].Attributes.Add("style", "color:red;font-weight:bold;fon-size:12pt;font-name:Calibri;back-ground:Lightblue");
                e.Row.Cells[0].Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:15px; Color:Red; border-color:Black; font-names:Calibri");
            }
            int approved = 0;
            double applied = 0.0;
            for (int l = 1, j = 0; l < e.Row.Cells.Count - 1; l++)
            {


                double appl = (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToDouble(e.Row.Cells[l].Text);


                applied += appl;
                //  approved = approved + appr;


                j++;
            }


            e.Row.Cells[e.Row.Cells.Count - 1].Text = applied.ToString();

            if (dtrowClr2.Rows[indx][1].ToString() == "No of Listed Drs Seen")
            {
                seen = Convert.ToInt32(applied.ToString());
            }
            if (dtrowClr2.Rows[indx][1].ToString() == "Fieldwork days")
            {
                Fw = Convert.ToInt32(applied.ToString());
            }

            if (dtrowClr2.Rows[indx][1].ToString() == "Day Wise Detail" || dtrowClr2.Rows[indx][1].ToString() == "Doctor Details" || dtrowClr2.Rows[indx][1].ToString() == "TP Deviation Days")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "";
            }

            if (dtrowClr2.Rows[indx][1].ToString() == "Call Average")
            {
                if (Fw != 0)
                {
                    e.Row.Cells[e.Row.Cells.Count - 1].Text = (Decimal.Divide(seen, Fw)).ToString("0.##");
                }
            }

            //if (dtrowClr2.Rows[indx][1].ToString() == "No of Listed Drs Seen")
            //{
            // HyperLink hLink = new HyperLink();    // Request.QueryString["sf_name"].ToString();
            // hLink.Text = e.Row.Cells[e.Row.Cells.Count - 1].Text;
            //  hLink.Attributes.Add("class", "btnDrMt");
            // hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "3" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr2.Rows[indx][0].ToString()) + "')");
            // hLink.ToolTip = "Click here";
            //hLink.Font.Underline = true;
            // hLink.Attributes.Add("style", "cursor:pointer");
            //   hLink.ForeColor = System.Drawing.Color.Fuchsia;
            // e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(hLink);
            // }
        }
    }
    //private void CreateDynamicTable()
    //{
    //    int iCount = 0;

    //    SalesForce sf = new SalesForce();

    //    dsSalesForce = sf.getmode(div_code);

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        TableRow tr_header = new TableRow();
    //        //tr_header.BorderStyle = BorderStyle.Solid;
    //        //tr_header.BorderWidth = 1;
    //        //tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        //tr_header.Attributes.Add("Class", "table");


    //        TableCell tc_DR_Name = new TableCell();
    //        //tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //        //tc_DR_Name.BorderWidth = 1;
    //        //tc_DR_Name.Width = 500;
    //        tc_DR_Name.Style.Add("min-width","250px");

    //        Literal lit_DR_Name = new Literal();
    //        lit_DR_Name.Text = "<center>Parameter</center>";
    //        //tc_DR_Name.Style.Add("font-family", "Calibri");
    //        //tc_DR_Name.Style.Add("font-size", "12pt");
    //        //tc_DR_Name.Style.Add("Color", "White");
    //        //tc_DR_Name.Style.Add("border-color", "Black");
    //        //tc_DR_Name.Attributes.Add("Class", "tr_det_head");
    //        tc_DR_Name.Controls.Add(lit_DR_Name);
    //        tr_header.Cells.Add(tc_DR_Name);


    //        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //        int cmonth = Convert.ToInt32(FMonth);
    //        int cyear = Convert.ToInt32(FYear);

    //        ViewState["months"] = months;
    //        ViewState["cmonth"] = cmonth;
    //        ViewState["cyear"] = cyear;

    //        //    tbl.Rows.Add(tr_header);

    //        TableRow tr_catg1 = new TableRow();
    //        if (months >= 0)
    //        {

    //            for (int j = 1; j <= months + 1; j++)
    //            {
    //                TableCell tc_month = new TableCell();
    //                // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //                //tc_month.RowSpan = 2;
    //                Literal lit_month = new Literal();
    //                lit_month.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
    //                //tc_month.Style.Add("font-family", "Calibri");
    //                //tc_month.Style.Add("font-size", "12pt");
    //                //tc_month.Style.Add("Color", "White");
    //                //tc_month.Style.Add("border-color", "Black");
    //                //tc_month.Attributes.Add("Class", "tr_det_head");

    //                //tc_month.BorderStyle = BorderStyle.Solid;
    //                //tc_month.BorderWidth = 1;

    //                tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                tc_month.Width = 200;
    //                tc_month.Controls.Add(lit_month);
    //                tr_header.Cells.Add(tc_month);
    //                // tr_catg1.Cells.Add(tc_month);
    //                cmonth = cmonth + 1;
    //                if (cmonth == 13)
    //                {
    //                    cmonth = 1;
    //                    cyear = cyear + 1;
    //                }
    //            }
    //        }

    //        //   tbl.Rows.Add(tr_catg1);

    //        TableRow tr_lst_det = new TableRow();
    //        TableCell tc_DR_Total = new TableCell();
    //        //tc_DR_Total.BorderStyle = BorderStyle.Solid;
    //        //tc_DR_Total.BorderWidth = 1;
    //        //tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //        tc_DR_Total.Width = 50;
    //        Literal lit_DR_Total = new Literal();
    //        lit_DR_Total.Text = "<center>Total</center>";
    //        //tc_DR_Total.Style.Add("Color", "White");
    //        //tc_DR_Total.Style.Add("font-family", "Calibri");
    //        //tc_DR_Total.Style.Add("font-size", "12pt");

    //        //tc_DR_Total.Style.Add("border-color", "Black");
    //        //tc_DR_Total.Attributes.Add("Class", "tr_det_head");
    //        tc_DR_Total.Controls.Add(lit_DR_Total);
    //        //  tr_lst_det.Cells.Add(tc_DR_Total);


    //        tr_header.Cells.Add(tc_DR_Total);
    //        tbl.Rows.Add(tr_header);
    //        int cmonthact = Convert.ToInt32(FMonth);
    //        int tmonthact = Convert.ToInt32(TMonth);
    //        int cyearact = Convert.ToInt32(FYear);
    //        int tyearact = Convert.ToInt32(TYear);

    //        //TableRow tr_catg3 = new TableRow();
    //        int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //        int cmonth1 = Convert.ToInt32(FMonth);
    //        int cyear1 = Convert.ToInt32(FYear);

    //        ViewState["months"] = months1;
    //        ViewState["cmonth"] = cmonth1;
    //        ViewState["cyear"] = cyear1;

    //        int Final_Fw = 0;
    //        int Final_NFw = 0;
    //        int Final_DC = 0;
    //        int Final_Mor = 0;
    //        int Final_Eve = 0;
    //        int Final_Both = 0;
    //          TableRow tr_emp = new TableRow();

    //            tr_emp.BackColor = System.Drawing.Color.White;
    //            TableCell tc_det_emp = new TableCell();
    //            Literal lit_det_emp = new Literal();
    //            lit_det_emp.Text = "Day Wise Detail";
    //            //tc_det_emp.BorderStyle = BorderStyle.Solid;
    //            //tc_det_emp.BorderWidth = 1;
    //            tc_det_emp.Controls.Add(lit_det_emp);

    //        tc_det_emp.Style.Add("text-align", "left");
    //        tc_det_emp.Style.Add("font-weight", "bold");
    //        //tc_det_emp.Style.Add("font-family", "Calibri");
    //        //tc_det_emp.BorderStyle = BorderStyle.Solid;

    //            tc_det_emp.BackColor = System.Drawing.ColorTranslator.FromHtml("LightBlue");
    //            tc_det_emp.Style.Add("font-size", "12pt");
    //            tr_emp.Cells.Add(tc_det_emp);
    //            tbl.Rows.Add(tr_emp);
    //        foreach (DataRow drow in dsSalesForce.Tables[0].Rows)
    //        {
    //            DataSet dsCall = new DataSet();
    //            DataSet dsField = new DataSet();
    //            double dblaverage = 0.00;

    //            TableRow tr_det = new TableRow();
    //            if (drow["Doc_Cat_SName"].ToString() == "Calendar Days")
    //            {



    //                tr_det.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det.Cells.Add(tc_det_FF1);

    //                if (months1 >= 0)
    //                {
    //                    string tot_fldwrk = string.Empty;
    //                    DCR dcr1 = new DCR();
    //                    DataSet ds = new DataSet();
    //                    tot_fldwrk = "";
    //                    int itotWorkType = 0;
    //                    int fldwrk_total = 0;

    //                    for (int j = 1; j <= months1 + 1; j++)
    //                    {

    //                        ds = dcr1.getMonth_Count(cmonth1, cyear1);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
    //                        //itotWorkType += fldwrk_total;

    //                        tr_det.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + tot_fldwrk;
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det.Cells.Add(tc_det_FF);


    //                        cmonth1 = cmonth1 + 1;
    //                        if (cmonth1 == 13)
    //                        {
    //                            cmonth1 = 1;
    //                            cyear1 = cyear1 + 1;
    //                        }

    //                    }

    //                    //int[] arrWorkType = new int[] { itotWorkType };

    //                    //for (int W = 0; W < arrWorkType.Length; W++)
    //                    //{
    //                    //    iSumLeave += arrWorkType[W];
    //                    //}

    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det.Cells.Add(tc_det_sf_Tot);
    //                }

    //                tbl.Rows.Add(tr_det);


    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "Sundays & Holidays")
    //            {
    //                int monthsl = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonthl = Convert.ToInt32(FMonth);
    //                int cyearl = Convert.ToInt32(FYear);

    //                ViewState["months"] = monthsl;
    //                ViewState["cmonth"] = cmonthl;
    //                ViewState["cyear"] = cyearl;
    //                TableRow tr_detl = new TableRow();
    //                tr_detl.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_L = new TableCell();
    //                Literal lit_det_L = new Literal();
    //                lit_det_L.Text = "" + drow[0];
    //                //tc_det_L.BorderStyle = BorderStyle.Solid;
    //                //tc_det_L.BorderWidth = 1;
    //                tc_det_L.Controls.Add(lit_det_L);
    //                tc_det_L.Style.Add("text-align", "left");
    //                //tc_det_L.Style.Add("font-family", "Calibri");
    //                //tc_det_L.Style.Add("font-size", "10pt");
    //                tr_detl.Cells.Add(tc_det_L);

    //                DCR dcr1 = new DCR();
    //                string tot_fldwrk = "";
    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                // int iSumLeave = 0;
    //                if (monthsl >= 0)
    //                {
    //                    for (int j = 1; j <= monthsl + 1; j++)
    //                    {
    //                        DataSet ds = new DataSet();
    //                        ds = dcr1.getWorking_Days_W_H(sf_code, div_code, cmonthl, cyearl);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //                        tr_detl.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_H = new TableCell();
    //                        Literal lit_det_H = new Literal();
    //                        lit_det_H.Text = "" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_H.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_H.BorderWidth = 1;
    //                        tc_det_H.Controls.Add(lit_det_H);
    //                        tc_det_H.Style.Add("text-align", "left");
    //                        //tc_det_H.Style.Add("font-family", "Calibri");
    //                        //tc_det_H.Style.Add("font-size", "10pt");
    //                        tr_detl.Cells.Add(tc_det_H);


    //                        cmonthl = cmonthl + 1;
    //                        if (cmonthl == 13)
    //                        {
    //                            cmonthl = 1;
    //                            cyearl = cyearl + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_L = new TableCell();
    //                    Literal lit_det_sf_L = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_L.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_L.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_L.BorderStyle = BorderStyle.Solid;
    //                    tc_det_sf_L.Style.Add("text-align", "left");
    //                    //tc_det_sf_L.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_L.Style.Add("font-size", "10pt");
    //                    //tc_det_sf_L.BorderWidth = 1;
    //                    tc_det_sf_L.Width = 50;
    //                    tc_det_sf_L.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_L.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_L.Controls.Add(lit_det_sf_L);
    //                    tr_detl.Cells.Add(tc_det_sf_L);
    //                }

    //                tbl.Rows.Add(tr_detl);
    //            }

    //            else if (drow["Doc_Cat_SName"].ToString() == "Working Days (Excl/Holidays & Sundays )")
    //            {
    //                int months2 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth2 = Convert.ToInt32(FMonth);
    //                int cyear2 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months2;
    //                ViewState["cmonth"] = cmonth2;
    //                ViewState["cyear"] = cyear2;
    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();
    //                string tot_fldwrk = "";
    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                // int iSumLeave = 0;
    //                if (months2 >= 0)
    //                {
    //                    for (int j = 1; j <= months2 + 1; j++)
    //                    {
    //                        DataSet ds = new DataSet();
    //                        ds = dcr1.getWorking_Days(sf_code, div_code, cmonth2, cyear2);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);


    //                        cmonth2 = cmonth2 + 1;
    //                        if (cmonth2 == 13)
    //                        {
    //                            cmonth2 = 1;
    //                            cyear2 = cyear2 + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);
    //                }

    //                tbl.Rows.Add(tr_det1);

    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "Fieldwork Days")
    //            {
    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;

    //                if (months >= 0)
    //                {
    //                    for (int j = 1; j <= months + 1; j++)
    //                    {
    //                        // DataSet ds = new DataSet();
    //                        dsField = dcr1.getFieldwork_Days(sf_code, div_code, cmonthact, cyearact);

    //                        if (dsField.Tables[0].Rows.Count > 0)
    //                            tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_Fw += fldwrk_total + Convert.ToInt16(tot_fldwrkDays);
    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);

    //                        cmonthact = cmonthact + 1;
    //                        if (cmonthact == 13)
    //                        {
    //                            cmonthact = 1;
    //                            cyearact = cyearact + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (Final_Fw == 0)
    //                    {
    //                        lit_det_sf_Tot.Text =  " - ";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + Final_Fw;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);

    //                }

    //                tbl.Rows.Add(tr_det1);
    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "No Fieldwork Days")
    //            {
    //                int monthsN = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonthN = Convert.ToInt32(FMonth);
    //                int cyearN = Convert.ToInt32(FYear);

    //                ViewState["months"] = monthsN;
    //                ViewState["cmonth"] = cmonthN;
    //                ViewState["cyear"] = cyearN;
    //                TableRow tr_detN = new TableRow();
    //                tr_detN.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_detN.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;

    //                if (monthsN >= 0)
    //                {
    //                    for (int j = 1; j <= monthsN + 1; j++)
    //                    {
    //                        // DataSet ds = new DataSet();
    //                        dsField = dcr1.getNonFieldwork_Days(sf_code, div_code, cmonthN, cyearact);

    //                        if (dsField.Tables[0].Rows.Count > 0)
    //                            tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_NFw += fldwrk_total + Convert.ToInt16(tot_fldwrkDays);
    //                        tr_detN.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_N = new TableCell();
    //                        Literal lit_det_N = new Literal();
    //                        lit_det_N.Text = "" + dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_N.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_N.BorderWidth = 1;
    //                        tc_det_N.Controls.Add(lit_det_N);
    //                        tc_det_N.Style.Add("text-align", "left");
    //                        //tc_det_N.Style.Add("font-family", "Calibri");
    //                        //tc_det_N.Style.Add("font-size", "10pt");
    //                        tr_detN.Cells.Add(tc_det_N);

    //                        cmonthN = cmonthN + 1;
    //                        if (cmonthN == 13)
    //                        {
    //                            cmonthN = 1;
    //                            cyearN = cyearN + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (Final_Fw == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = " - ";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + Final_NFw;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_detN.Cells.Add(tc_det_sf_Tot);

    //                }

    //                tbl.Rows.Add(tr_detN);
    //            }


    //            else if (drow["Doc_Cat_SName"].ToString() == "Leave")
    //            {
    //                int months3 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth3 = Convert.ToInt32(FMonth);
    //                int cyear3 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months3;
    //                ViewState["cmonth"] = cmonth3;
    //                ViewState["cyear"] = cyear3;

    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();
    //                string tot_fldwrk = "";
    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (months3 >= 0)
    //                {
    //                    for (int j = 1; j <= months3 + 1; j++)
    //                    {
    //                        DataSet ds = new DataSet();
    //                        ds = dcr1.getLeave_Days(sf_code, div_code, cmonth3, cyear3);

    //                        if (ds.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
    //                        //  string leave = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();

    //                        if (tot_fldwrk == "0")
    //                        {
    //                            lit_det_FF.Text = " - ";
    //                        }
    //                        else
    //                        {
    //                            lit_det_FF.Text = "" + tot_fldwrk;
    //                        }
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);

    //                        cmonth3 = cmonth3 + 1;
    //                        if (cmonth3 == 13)
    //                        {
    //                            cmonth3 = 1;
    //                            cyear3 = cyear3 + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_Tot.Text =  " - ";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);

    //                }
    //                tbl.Rows.Add(tr_det1);
    //            }


    //            else if (drow["Doc_Cat_SName"].ToString() == "TP Deviation Days")
    //            {
    //                int months4 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth4 = Convert.ToInt32(FMonth);
    //                int cyear4 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months4;
    //                ViewState["cmonth"] = cmonth4;
    //                ViewState["cyear"] = cyear4;
    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();

    //                //DataSet ds = new DataSet();
    //                //ds = dcr1.getLeave_Days(sf_code, div_code, cmonthact, cyearact);
    //                if (months4 >= 0)
    //                {
    //                    for (int j = 1; j <= months4 + 1; j++)
    //                    {

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = " - ";
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);

    //                        cmonth4 = cmonth4 + 1;
    //                        if (cmonth4 == 13)
    //                        {
    //                            cmonth4 = 1;
    //                            cyear4 = cyear4 + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();


    //                        lit_det_sf_Tot.Text = " - ";


    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);

    //                }
    //                tbl.Rows.Add(tr_det1);
    //                TableRow tr_DD = new TableRow();

    //                tr_DD.BackColor = System.Drawing.Color.White;
    //                TableCell tc_DD = new TableCell();
    //                Literal lit_DD = new Literal();
    //                lit_DD.Text = "Doctor Details";
    //                //tc_DD.BorderStyle = BorderStyle.Solid;
    //                //tc_DD.BorderWidth = 1;

    //                tc_DD.Controls.Add(lit_DD);
    //                tc_DD.Style.Add("text-align", "left");
    //                tc_DD.Style.Add("font-weight", "bold");
    //                //tc_DD.Style.Add("font-family", "Calibri");
    //                //tc_DD.BorderStyle = BorderStyle.Solid;

    //                tc_DD.BackColor = System.Drawing.ColorTranslator.FromHtml("LightBlue");
    //                tc_DD.Style.Add("font-size", "12pt");
    //                tr_DD.Cells.Add(tc_DD);
    //                tbl.Rows.Add(tr_DD);
    //            }

    //            else if (drow["Doc_Cat_SName"].ToString() == "No of Listed Drs Met")
    //            {
    //                int months5 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth5 = Convert.ToInt32(FMonth);
    //                int cyear5 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months5;
    //                ViewState["cmonth"] = cmonth5;
    //                ViewState["cyear"] = cyear5;
    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();
    //                string tot_fldwrk = "";
    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (months5 >= 0)
    //                {
    //                    for (int j = 1; j <= months5 + 1; j++)
    //                    {
    //                        DataSet ds = new DataSet();
    //                        ds = dcr1.DCR_Doc_Met_Self(sf_code, div_code, cmonth5, cyear5);
    //                        if (ds.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);

    //                        cmonth5 = cmonth5 + 1;
    //                        if (cmonth5 == 13)
    //                        {
    //                            cmonth5 = 1;
    //                            cyear5 = cyear5 + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_Tot.Text =  " - ";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_det1);
    //            }

    //            else if (drow["Doc_Cat_SName"].ToString() == "No of Listed Drs Seen")
    //            {
    //                int months6 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth6 = Convert.ToInt32(FMonth);
    //                int cyear6 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months6;
    //                ViewState["cmonth"] = cmonth6;
    //                ViewState["cyear"] = cyear6;
    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (months >= 0)
    //                {
    //                    for (int j = 1; j <= months + 1; j++)
    //                    {

    //                        dsCall = dcr1.DCR_Doc_Seen_Self(sf_code, div_code, cmonth6, cyear6);
    //                        if (dsCall.Tables[0].Rows.Count > 0)
    //                            tot_docSeen = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_DC += fldwrk_total + Convert.ToInt16(tot_docSeen);

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det1.Cells.Add(tc_det_FF);

    //                        cmonth6 = cmonth6 + 1;
    //                        if (cmonth6 == 13)
    //                        {
    //                            cmonth6 = 1;
    //                            cyear6 = cyear6 + 1;
    //                        }

    //                    }
    //                    //Changes S by RP 
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                   // Literal lit_det_sf_Tot = new Literal();
    //                    HyperLink hyp_lst_month = new HyperLink();
    //                    if (Final_DC == 0)
    //                    {
    //                        hyp_lst_month.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        hyp_lst_month.Text = "" + Final_DC;

    //                        hyp_lst_month.Attributes.Add("class", "btnDrMt");
    //                        hyp_lst_month.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "3" + "','" + Request.QueryString["sf_name"].ToString() + "','')");
    //                        hyp_lst_month.ToolTip = "Click here";
    //                        hyp_lst_month.Font.Underline = true;
    //                        hyp_lst_month.Attributes.Add("style", "cursor:pointer");
    //                        //hyp_lst_month.ForeColor = System.Drawing.Color.Fuchsia;
    //                        hyp_lst_month.ForeColor = System.Drawing.ColorTranslator.FromHtml("#007bff");
    //                    }
    //                    //Changes E by RP 
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(hyp_lst_month);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_det1);
    //            }



    //            else if (drow["Doc_Cat_SName"].ToString() == "Call Average")
    //            {
    //                int months7 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth7 = Convert.ToInt32(FMonth);
    //                int cyear7 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months7;
    //                ViewState["cmonth"] = cmonth7;
    //                ViewState["cyear"] = cyear7;

    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tr_det1.Cells.Add(tc_det_FF1);

    //                double itotWorkType = 0.0;
    //                double fldwrk_total = 0.0;

    //                if (months7 >= 0)
    //                {
    //                    for (int j = 1; j <= months7 + 1; j++)
    //                    {
    //                        decimal RoundLstCallAvg = new decimal();
    //                        dblaverage = 0.0;
    //                        DCR dcr_seen = new DCR();
    //                        dsCall = dcr_seen.DCR_Doc_Seen_Self(sf_code, div_code, cmonth7, cyear7);
    //                        if (dsCall.Tables[0].Rows.Count > 0)
    //                            tot_docSeen = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                    //    itotWorkType += fldwrk_total + Convert.ToInt16(tot_docSeen);

    //                        dsField = dcr_seen.getFieldwork_Days(sf_code, div_code, cmonth7, cyear7);

    //                        if (dsField.Tables[0].Rows.Count > 0)
    //                            tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //                       //     tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                   //     itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //                        if (Convert.ToDecimal(tot_fldwrkDays) != 0)

    //                            dblaverage = Convert.ToDouble((Convert.ToDecimal(tot_docSeen) / Convert.ToDecimal(tot_fldwrkDays)));
    //                        RoundLstCallAvg = Math.Round((decimal)dblaverage, 2);

    //                        itotWorkType += fldwrk_total + Convert.ToDouble(RoundLstCallAvg);
    //                        TableCell tc_det_average = new TableCell();
    //                        Literal lit_det_average = new Literal();
    //                        if (RoundLstCallAvg == 0)
    //                        {
    //                            lit_det_average.Text =  " - ";
    //                        }
    //                        else
    //                        {

    //                            lit_det_average.Text = "" + RoundLstCallAvg;
    //                        }
    //                        //tc_det_average.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_average.BorderWidth = 1;
    //                        //tc_det_average.Style.Add("font-family", "Calibri");
    //                        //tc_det_average.Style.Add("font-size", "10pt");
    //                        //  tc_det_average.HorizontalAlign = HorizontalAlign.Center;
    //                        tc_det_average.VerticalAlign = VerticalAlign.Middle;
    //                        tc_det_average.Controls.Add(lit_det_average);
    //                        tr_det1.Cells.Add(tc_det_average);

    //                        cmonth7 = cmonth7 + 1;
    //                        if (cmonth7 == 13)
    //                        {
    //                            cmonth7 = 1;
    //                            cyear7 = cyear7 + 1;
    //                        }
    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();
    //                    decimal RoundLstCallAvg_Final = new decimal();
    //                    double dblaverage1 = 0.0;
    //                    if (Convert.ToDecimal(Final_Fw) != 0)
    //                        dblaverage1 = Convert.ToDouble((Convert.ToDecimal(Final_DC) / Convert.ToDecimal(Final_Fw)));
    //                    RoundLstCallAvg_Final = Math.Round((decimal)dblaverage1, 2);
    //                    if (RoundLstCallAvg_Final == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = " - ";
    //                    }
    //                    else
    //                    {
    //                        // lit_det_sf_Tot.Text = "" + itotWorkType;

    //                        lit_det_sf_Tot.Text = "" + RoundLstCallAvg_Final;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det1.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_det1);
    //                //}
    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "Morning Calls")
    //            {
    //                int monthsM = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonthM = Convert.ToInt32(FMonth);
    //                int cyearM = Convert.ToInt32(FYear);

    //                ViewState["months"] = monthsM;
    //                ViewState["cmonth"] = cmonthM;
    //                ViewState["cyear"] = cyearM;
    //                TableRow tr_detM = new TableRow();
    //                tr_detM.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_M = new TableCell();
    //                Literal lit_det_M = new Literal();
    //                lit_det_M.Text = "" + drow[0];
    //                //tc_det_M.BorderStyle = BorderStyle.Solid;
    //                //tc_det_M.BorderWidth = 1;
    //                tc_det_M.Controls.Add(lit_det_M);
    //                tc_det_M.Style.Add("text-align", "left");
    //                //tc_det_M.Style.Add("font-family", "Calibri");
    //                //tc_det_M.Style.Add("font-size", "10pt");
    //                tr_detM.Cells.Add(tc_det_M);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (monthsM >= 0)
    //                {
    //                    for (int j = 1; j <= monthsM + 1; j++)
    //                    {

    //                        dsCall = dcr1.Get_Mor_Calls(sf_code, div_code, cmonthM, cyearM);
    //                        if (dsCall.Tables[0].Rows.Count > 0)
    //                            tot_Mor = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_Mor += fldwrk_total + Convert.ToInt16(tot_Mor);

    //                        tr_detM.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + tot_Mor;
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_detM.Cells.Add(tc_det_FF);

    //                        cmonthM = cmonthM + 1;
    //                        if (cmonthM == 13)
    //                        {
    //                            cmonthM = 1;
    //                            cyearM = cyearM + 1;
    //                        }

    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (Final_DC == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + Final_Mor;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_detM.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_detM);
    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "Evening Calls")
    //            {
    //                int monthsE = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonthE = Convert.ToInt32(FMonth);
    //                int cyearE = Convert.ToInt32(FYear);

    //                ViewState["months"] = monthsE;
    //                ViewState["cmonth"] = cmonthE;
    //                ViewState["cyear"] = cyearE;
    //                TableRow tr_detE = new TableRow();
    //                tr_detE.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_M = new TableCell();
    //                Literal lit_det_M = new Literal();
    //                lit_det_M.Text = "" + drow[0];
    //                //tc_det_M.BorderStyle = BorderStyle.Solid;
    //                //tc_det_M.BorderWidth = 1;
    //                tc_det_M.Controls.Add(lit_det_M);
    //                tc_det_M.Style.Add("text-align", "left");
    //                //tc_det_M.Style.Add("font-family", "Calibri");
    //                //tc_det_M.Style.Add("font-size", "10pt");
    //                tr_detE.Cells.Add(tc_det_M);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (monthsE >= 0)
    //                {
    //                    for (int j = 1; j <= monthsE + 1; j++)
    //                    {

    //                        dsCall = dcr1.Get_Eve_Calls(sf_code, div_code, cmonthE, cyearE);
    //                        if (dsCall.Tables[0].Rows.Count > 0)
    //                            tot_Eve= dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_Eve += fldwrk_total + Convert.ToInt16(tot_Eve);

    //                        tr_detE.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + tot_Eve;
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_detE.Cells.Add(tc_det_FF);

    //                        cmonthE = cmonthE + 1;
    //                        if (cmonthE == 13)
    //                        {
    //                            cmonthE = 1;
    //                            cyearE = cyearE + 1;
    //                        }

    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (Final_DC == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + Final_Eve;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_detE.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_detE);
    //            }
    //            else if (drow["Doc_Cat_SName"].ToString() == "Both Calls")
    //            {
    //                int monthsB = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonthB = Convert.ToInt32(FMonth);
    //                int cyearB = Convert.ToInt32(FYear);

    //                ViewState["months"] = monthsB;
    //                ViewState["cmonth"] = cmonthB;
    //                ViewState["cyear"] = cyearB;
    //                TableRow tr_detB = new TableRow();
    //                tr_detB.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_B = new TableCell();
    //                Literal lit_det_B= new Literal();
    //                lit_det_B.Text = "" + drow[0];
    //                //tc_det_B.BorderStyle = BorderStyle.Solid;
    //                //tc_det_B.BorderWidth = 1;
    //                tc_det_B.Controls.Add(lit_det_B);
    //                tc_det_B.Style.Add("text-align", "left");
    //                //tc_det_B.Style.Add("font-family", "Calibri");
    //                //tc_det_B.Style.Add("font-size", "10pt");
    //                tr_detB.Cells.Add(tc_det_B);

    //                DCR dcr1 = new DCR();

    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (monthsB >= 0)
    //                {
    //                    for (int j = 1; j <= monthsB + 1; j++)
    //                    {

    //                        dsCall = dcr1.Get_Both_Calls(sf_code, div_code, cmonthB, cyearB);
    //                        if (dsCall.Tables[0].Rows.Count > 0)
    //                            tot_Both = dsCall.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        Final_Both += fldwrk_total + Convert.ToInt16(tot_Both);

    //                        tr_detB.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + tot_Both;
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_detB.Cells.Add(tc_det_FF);

    //                        cmonthB = cmonthB + 1;
    //                        if (cmonthB == 13)
    //                        {
    //                            cmonthB = 1;
    //                            cyearB = cyearB + 1;
    //                        }

    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (Final_DC == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = "" + "-";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + Final_Both;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_detB.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_detB);
    //            }
    //            else
    //            {

    //                int months8 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //                int cmonth8 = Convert.ToInt32(FMonth);
    //                int cyear8 = Convert.ToInt32(FYear);

    //                ViewState["months"] = months8;
    //                ViewState["cmonth"] = cmonth8;
    //                ViewState["cyear"] = cyear8;

    //                TableRow tr_det1 = new TableRow();
    //                tr_det1.BackColor = System.Drawing.Color.White;
    //                TableCell tc_det_FF1 = new TableCell();
    //                Literal lit_det_FF1 = new Literal();
    //                lit_det_FF1.Text = "" + drow[0];
    //                //tc_det_FF1.BorderStyle = BorderStyle.Solid;
    //                //tc_det_FF1.BorderWidth = 1;
    //                tc_det_FF1.Controls.Add(lit_det_FF1);
    //                tc_det_FF1.Style.Add("text-align", "left");
    //                //tc_det_FF1.Style.Add("font-family", "Calibri");
    //                //tc_det_FF1.Style.Add("font-size", "10pt");
    //                tc_det_FF1.Style.Add("color","Red");
    //                //tc_det_FF1.Style.Add("border-color", "Black");
    //                tr_det.Cells.Add(tc_det_FF1);
    //                string tot_dcr_dr = string.Empty;
    //                DCR dcr1 = new DCR();

    //                string tot_fldwrk = "";
    //                int itotWorkType = 0;
    //                int fldwrk_total = 0;
    //                if (months8 >= 0)
    //                {
    //                    for (int j = 1; j <= months8 + 1; j++)
    //                    {
    //                        DataSet dsDCR = new DataSet();

    //                        DCR dc = new DCR();
    //                        dsDCR = dc.Catg_Visit_Report1(sf_code, div_code, cmonth8, cyear8, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()), 1);
    //                        //  if (dsDCR.Tables[0].Rows.Count > 0)
    //                        //  tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        if (dsDCR.Tables[0].Rows.Count > 0)
    //                            tot_fldwrk = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                        itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);

    //                        tr_det1.BackColor = System.Drawing.Color.White;
    //                        TableCell tc_det_FF = new TableCell();
    //                        Literal lit_det_FF = new Literal();
    //                        lit_det_FF.Text = "" + tot_fldwrk;
    //                        //tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                        //tc_det_FF.BorderWidth = 1;
    //                        tc_det_FF.Controls.Add(lit_det_FF);
    //                        tc_det_FF.Style.Add("text-align", "left");
    //                        //tc_det_FF.Style.Add("font-family", "Calibri");
    //                        //tc_det_FF.Style.Add("font-size", "10pt");
    //                        tr_det.Cells.Add(tc_det_FF);

    //                        cmonth8 = cmonth8 + 1;
    //                        if (cmonth8 == 13)
    //                        {
    //                            cmonth8 = 1;
    //                            cyear8 = cyear8 + 1;
    //                        }

    //                    }
    //                    TableCell tc_det_sf_Tot = new TableCell();
    //                    Literal lit_det_sf_Tot = new Literal();

    //                    if (itotWorkType == 0)
    //                    {
    //                        lit_det_sf_Tot.Text = " - ";
    //                    }
    //                    else
    //                    {
    //                        lit_det_sf_Tot.Text = "" + itotWorkType;
    //                    }
    //                    //tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //                    //tc_det_sf_Tot.BorderWidth = 1;
    //                    tc_det_sf_Tot.Style.Add("text-align", "left");
    //                    //tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //                    //tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //                    tc_det_sf_Tot.Width = 50;
    //                    tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //                    tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //                    tr_det.Cells.Add(tc_det_sf_Tot);
    //                }
    //                tbl.Rows.Add(tr_det);
    //            }

    //       // }

    //   // }
    //    //DataSet dsDCR1 = new DataSet();
    //    //DCR dc1 = new DCR();
    //    //dsDCR1 = dc1.getHQ_Mgr(sf_code);
    //    //if (dsDCR1.Tables[0].Rows.Count > 0)
    //    //{

    //    //    TableRow tr_header_hq = new TableRow();
    //    //    tr_header_hq.BorderStyle = BorderStyle.Solid;
    //    //    tr_header_hq.BorderWidth = 1;
    //    //    tr_header_hq.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //    tr_header_hq.Attributes.Add("Class", "tblCellFont");


    //    //    TableCell tc_Hq_Name = new TableCell();
    //    //    tc_Hq_Name.BorderStyle = BorderStyle.Solid;
    //    //    tc_Hq_Name.BorderWidth = 1;
    //    //    tc_Hq_Name.RowSpan = 3;
    //    //    tc_Hq_Name.Width = 500;

    //    //    Literal lit_DR_Name = new Literal();
    //    //    lit_DR_Name.Text = "<center>HQ Name</center>";
    //    //    tc_Hq_Name.Style.Add("font-weight", "bold");
    //    //    tc_Hq_Name.Attributes.Add("Class", "tr_det_head");
    //    //    tc_Hq_Name.Style.Add("Color", "White");
    //    //    tc_Hq_Name.Style.Add("border-color", "Black");
    //    //    tc_Hq_Name.Controls.Add(lit_DR_Name);
    //    //    tr_header_hq.Cells.Add(tc_Hq_Name);


    //    //    int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    //    int cmonth = Convert.ToInt32(FMonth);
    //    //    int cyear = Convert.ToInt32(FYear);

    //    //    //    tbl.Rows.Add(tr_header);

    //    //    TableRow tr_catg1 = new TableRow();
    //    //    if (months >= 0)
    //    //    {

    //    //        for (int j = 1; j <= months + 1; j++)
    //    //        {
    //    //            TableCell tc_month_hq = new TableCell();
    //    //            // tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //    //            tc_month_hq.ColumnSpan = 9;

    //    //            Literal lit_month_hq = new Literal();
    //    //            lit_month_hq.Text = "" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
    //    //            tc_month_hq.Style.Add("font-family", "Calibri");
    //    //            tc_month_hq.Style.Add("font-size", "10pt");
    //    //            tc_month_hq.Attributes.Add("Class", "tr_det_head");
    //    //            tc_month_hq.Style.Add("Color", "White");
    //    //            tc_month_hq.Style.Add("border-color", "Black");
    //    //            tc_month_hq.BorderStyle = BorderStyle.Solid;
    //    //            tc_month_hq.BorderWidth = 1;


    //    //            tc_month_hq.HorizontalAlign = HorizontalAlign.Center;
    //    //            //tc_month.Width = 200;
    //    //            tc_month_hq.Controls.Add(lit_month_hq);
    //    //            tr_header_hq.Cells.Add(tc_month_hq);
    //    //            // tr_catg1.Cells.Add(tc_month);
    //    //            cmonth = cmonth + 1;
    //    //            if (cmonth == 13)
    //    //            {
    //    //                cmonth = 1;
    //    //                cyear = cyear + 1;
    //    //            }
    //    //        }
    //    //    }
    //    //    tblhq.Rows.Add(tr_header_hq);
    //    //    TableRow tr_lst_det = new TableRow();
    //    //    if (months >= 0)
    //    //    {

    //    //        for (int j = 1; j <= months + 1; j++)
    //    //        {
    //    //            TableCell tc_lst_month = new TableCell();
    //    //            HyperLink lit_lst_month = new HyperLink();
    //    //            lit_lst_month.Text = "No of Days Worked";

    //    //            tc_lst_month.BorderStyle = BorderStyle.Solid;
    //    //            tc_lst_month.ColumnSpan = 4;
    //    //            //  tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_lst_month.BorderWidth = 1;

    //    //            tc_lst_month.Style.Add("font-family", "Calibri");
    //    //            tc_lst_month.Style.Add("font-size", "10pt");
    //    //            tc_lst_month.Style.Add("Color", "White");
    //    //            tc_lst_month.Style.Add("border-color", "Black");
    //    //            tc_lst_month.Controls.Add(lit_lst_month);
    //    //            tr_lst_det.Cells.Add(tc_lst_month);




    //    //            TableCell tc_msd_month = new TableCell();
    //    //            HyperLink lit_msd_month = new HyperLink();
    //    //            lit_msd_month.Text = "Total Doctor Calls";
    //    //            tc_msd_month.BorderStyle = BorderStyle.Solid;
    //    //            tc_msd_month.ColumnSpan = 5;
    //    //            tc_msd_month.HorizontalAlign = HorizontalAlign.Center;
    //    //            //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_msd_month.BorderWidth = 1;
    //    //            tc_msd_month.Style.Add("Color", "White");
    //    //            tc_msd_month.Style.Add("border-color", "Black");
    //    //            tc_msd_month.Style.Add("font-family", "Calibri");
    //    //            tc_msd_month.Style.Add("font-size", "10pt");
    //    //            tc_msd_month.Controls.Add(lit_msd_month);
    //    //            tr_lst_det.Cells.Add(tc_msd_month);

    //    //            cmonth = cmonth + 1;
    //    //            if (cmonth == 13)
    //    //            {
    //    //                cmonth = 1;
    //    //                cyear = cyear + 1;
    //    //            }
    //    //            tblhq.Rows.Add(tr_lst_det);
    //    //        }




    //    //    }
    //    //    int months8 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;         
    //    //    int cmonth8 = Convert.ToInt32(FMonth);
    //    //    int cyear8 = Convert.ToInt32(FYear);

    //    //    ViewState["months"] = months8;
    //    //    ViewState["cmonth"] = cmonth8;
    //    //    ViewState["cyear"] = cyear8;
    //    //    TableRow tr_cov_hq = new TableRow();
    //    //    if (months8 >= 0)
    //    //    {

    //    //        for (int j = 1; j <= months8 + 1; j++)
    //    //        {


    //    //            TableCell tc_cov_hq = new TableCell();
    //    //            Literal lit_cov_hq = new Literal();
    //    //            lit_cov_hq.Text = "HQ";
    //    //            tc_cov_hq.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_hq.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_hq.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_hq.BorderWidth = 1;

    //    //            tc_cov_hq.Style.Add("font-family", "Calibri");
    //    //            tc_cov_hq.Style.Add("font-size", "10pt");
    //    //            tc_cov_hq.Style.Add("Color", "White");
    //    //            tc_cov_hq.Style.Add("border-color", "Black");
    //    //            tc_cov_hq.Controls.Add(lit_cov_hq);
    //    //            tr_cov_hq.Cells.Add(tc_cov_hq);

    //    //            TableCell tc_cov_ex = new TableCell();
    //    //            Literal lit_cov_ex = new Literal();
    //    //            lit_cov_ex.Text = "EX";
    //    //            tc_cov_ex.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_ex.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_ex.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_ex.BorderWidth = 1;

    //    //            tc_cov_ex.Style.Add("font-family", "Calibri");
    //    //            tc_cov_ex.Style.Add("font-size", "10pt");
    //    //            tc_cov_ex.Style.Add("Color", "White");
    //    //            tc_cov_ex.Style.Add("border-color", "Black");
    //    //            tc_cov_ex.Controls.Add(lit_cov_ex);
    //    //            tr_cov_hq.Cells.Add(tc_cov_ex);

    //    //            TableCell tc_cov_os = new TableCell();
    //    //            Literal lit_cov_os = new Literal();
    //    //            lit_cov_os.Text = "OS";
    //    //            tc_cov_os.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_os.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_os.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_os.BorderWidth = 1;

    //    //            tc_cov_os.Style.Add("font-family", "Calibri");
    //    //            tc_cov_os.Style.Add("font-size", "10pt");
    //    //            tc_cov_os.Style.Add("Color", "White");
    //    //            tc_cov_os.Style.Add("border-color", "Black");
    //    //            tc_cov_os.Controls.Add(lit_cov_os);
    //    //            tr_cov_hq.Cells.Add(tc_cov_os);


    //    //            TableCell tc_cov_tot = new TableCell();
    //    //            Literal lit_cov_tot = new Literal();
    //    //            lit_cov_tot.Text = "Total";
    //    //            tc_cov_tot.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_tot.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_tot.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_tot.BorderWidth = 1;

    //    //            tc_cov_tot.Style.Add("font-family", "Calibri");
    //    //            tc_cov_tot.Style.Add("font-size", "10pt");
    //    //            tc_cov_tot.Style.Add("Color", "White");
    //    //            tc_cov_tot.Style.Add("border-color", "Black");
    //    //            tc_cov_tot.Controls.Add(lit_cov_tot);
    //    //            tr_cov_hq.Cells.Add(tc_cov_tot);

    //    //            //TableRow tr_cov_hq1 = new TableRow();
    //    //            TableCell tc_cov_hq1 = new TableCell();
    //    //            Literal lit_cov_hq1 = new Literal();
    //    //            lit_cov_hq1.Text = "HQ";
    //    //            tc_cov_hq1.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_hq1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_hq1.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_hq1.BorderWidth = 1;

    //    //            tc_cov_hq1.Style.Add("font-family", "Calibri");
    //    //            tc_cov_hq1.Style.Add("font-size", "10pt");
    //    //            tc_cov_hq1.Style.Add("Color", "White");
    //    //            tc_cov_hq1.Style.Add("border-color", "Black");
    //    //            tc_cov_hq1.Controls.Add(lit_cov_hq1);
    //    //            tr_cov_hq.Cells.Add(tc_cov_hq1);

    //    //            TableCell tc_cov_ex1 = new TableCell();
    //    //            Literal lit_cov_ex1 = new Literal();
    //    //            lit_cov_ex1.Text = "EX";
    //    //            tc_cov_ex1.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_ex1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_ex1.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_ex1.BorderWidth = 1;

    //    //            tc_cov_ex1.Style.Add("font-family", "Calibri");
    //    //            tc_cov_ex1.Style.Add("font-size", "10pt");
    //    //            tc_cov_ex1.Style.Add("Color", "White");
    //    //            tc_cov_ex1.Style.Add("border-color", "Black");
    //    //            tc_cov_ex1.Controls.Add(lit_cov_ex1);
    //    //            tr_cov_hq.Cells.Add(tc_cov_ex1);

    //    //            TableCell tc_cov_os1 = new TableCell();
    //    //            Literal lit_cov_os1 = new Literal();
    //    //            lit_cov_os1.Text = "OS";
    //    //            tc_cov_os1.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_os1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_os1.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_os1.BorderWidth = 1;

    //    //            tc_cov_os1.Style.Add("font-family", "Calibri");
    //    //            tc_cov_os1.Style.Add("font-size", "10pt");
    //    //            tc_cov_os1.Style.Add("Color", "White");
    //    //            tc_cov_os1.Style.Add("border-color", "Black");
    //    //            tc_cov_os1.Controls.Add(lit_cov_os1);
    //    //            tr_cov_hq.Cells.Add(tc_cov_os1);


    //    //            TableCell tc_cov_tot1 = new TableCell();
    //    //            Literal lit_cov_tot1 = new Literal();
    //    //            lit_cov_tot1.Text = "Total Seen";
    //    //            tc_cov_tot1.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_tot1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_tot1.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_tot1.BorderWidth = 1;

    //    //            tc_cov_tot1.Style.Add("font-family", "Calibri");
    //    //            tc_cov_tot1.Style.Add("font-size", "10pt");
    //    //            tc_cov_tot1.Style.Add("Color", "White");
    //    //            tc_cov_tot1.Style.Add("border-color", "Black");
    //    //            tc_cov_tot1.Controls.Add(lit_cov_tot1);
    //    //            tr_cov_hq.Cells.Add(tc_cov_tot1);

    //    //            TableCell tc_cov_totmet = new TableCell();
    //    //            Literal lit_cov_totmet = new Literal();
    //    //            lit_cov_totmet.Text = "Total Met";
    //    //            tc_cov_totmet.BorderStyle = BorderStyle.Solid;

    //    //            tc_cov_totmet.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //    //            tc_cov_totmet.HorizontalAlign = HorizontalAlign.Center;
    //    //            tc_cov_totmet.BorderWidth = 1;

    //    //            tc_cov_totmet.Style.Add("font-family", "Calibri");
    //    //            tc_cov_totmet.Style.Add("font-size", "10pt");
    //    //            tc_cov_totmet.Style.Add("Color", "White");
    //    //            tc_cov_totmet.Style.Add("border-color", "Black");
    //    //            tc_cov_totmet.Controls.Add(lit_cov_totmet);
    //    //            tr_cov_hq.Cells.Add(tc_cov_totmet);




    //    //            cmonth8 = cmonth8 + 1;
    //    //            if (cmonth8 == 13)
    //    //            {
    //    //                cmonth8 = 1;
    //    //                cyear8 = cyear8 + 1;
    //    //            }

    //    //        }
    //    //        tblhq.Rows.Add(tr_cov_hq);



    //        }

    //     //   TableRow tr_lst_det = new TableRow();
    //       // TableCell tc_DR_Total = new TableCell();
    //       // tc_DR_Total.BorderStyle = BorderStyle.Solid;
    //       // tc_DR_Total.BorderWidth = 1;
    //       // tc_DR_Total.ColumnSpan = 2;
    //       // tc_DR_Total.Style.Add("Color", "White");
    //       // tc_DR_Total.Style.Add("border-color", "Black");
    //       // tc_DR_Total.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //       // //tc_DR_Total.Width = 50;
    //       // Literal lit_DR_Total = new Literal();
    //       // lit_DR_Total.Text = "<center>Total</center>";
    //       // tc_DR_Total.Attributes.Add("Class", "tr_det_head");
    //       // tc_DR_Total.Controls.Add(lit_DR_Total);
    //       // //  tr_lst_det.Cells.Add(tc_DR_Total);


    //       // tr_header_hq.Cells.Add(tc_DR_Total);

    //       // //  tblhq.Rows.Add(tr_header_hq);
    //       //// TableRow tr_lstWorkdr = new TableRow();
    //       // TableCell tc_lstWork = new TableCell();

    //       // HyperLink lit_lstWork = new HyperLink();
    //       // lit_lstWork.Text = "No of Days Worked";
    //       // tc_lstWork.BorderStyle = BorderStyle.Solid;
    //       // tc_lstWork.RowSpan = 2;
    //       // //  tc_lst_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //       // tc_lstWork.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //       // tc_lstWork.HorizontalAlign = HorizontalAlign.Center;
    //       // tc_lstWork.BorderWidth = 1;
    //       // tc_lstWork.Style.Add("Color", "White");
    //       // tc_lstWork.Style.Add("border-color", "Black");
    //       // tc_lstWork.Style.Add("font-family", "Calibri");
    //       // tc_lstWork.Style.Add("font-size", "10pt");
    //       // tc_lstWork.Controls.Add(lit_lstWork);
    //       // tr_lst_det.Cells.Add(tc_lstWork);

    //       // TableCell tc_lstWork1 = new TableCell();
    //       // HyperLink lit_Work1 = new HyperLink();
    //       // lit_Work1.Text = "Total Doctor Calls";
    //       // tc_lstWork1.BorderStyle = BorderStyle.Solid;
    //       // tc_lstWork1.RowSpan = 2;
    //       // tc_lstWork1.HorizontalAlign = HorizontalAlign.Center;
    //       // //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //       // tc_lstWork1.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
    //       // tc_lstWork1.BorderWidth = 1;
    //       // tc_lstWork1.Style.Add("Color", "White");
    //       // tc_lstWork1.Style.Add("border-color", "Black");
    //       // tc_lstWork1.Style.Add("font-family", "Calibri");
    //       // tc_lstWork1.Style.Add("font-size", "10pt");
    //       // tc_lstWork1.Controls.Add(lit_Work1);
    //       // tr_lst_det.Cells.Add(tc_lstWork1);

    //       // tblhq.Rows.Add(tr_catg1);

    //       // foreach (DataRow drFF1 in dsDCR1.Tables[0].Rows)
    //       // {

    //       //     TableRow tr_det_hq = new TableRow();

    //       //     //tc_det_SNo.Height = 10;

    //       //     //tr_det.Height = 10;
    //       //     tr_det_hq.BackColor = System.Drawing.Color.White;
    //       //     TableCell tc_det_FF = new TableCell();
    //       //     Literal lit_det_FF = new Literal();
    //       //     lit_det_FF.Text = "" + drFF1["sf_Name"];
    //       //     tc_det_FF.BorderStyle = BorderStyle.Solid;
    //       //     tc_det_FF.BorderWidth = 1;
    //       //     tc_det_FF.Controls.Add(lit_det_FF);
    //       //     tc_det_FF.Style.Add("text-align", "left");
    //       //     tc_det_FF.Style.Add("font-family", "Calibri");

    //       //     tc_det_FF.Style.Add("font-size", "10pt");
    //       //     tr_det_hq.Cells.Add(tc_det_FF);

    //       //     tblhq.Rows.Add(tr_det_hq);

    //       //  //   int itotWorkType = 0;
    //       //  //   
    //       //     string tot_fldwrk = "";
    //       //     int months5 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //       //     int cmonth5 = Convert.ToInt32(FMonth);
    //       //     int cyear5 = Convert.ToInt32(FYear);
    //       //   //  string tot_fldwrk = "";
    //       //     int itotWorkType1 = 0;
    //       //     int fldwrk_total = 0;
    //       //     int fldwrk_totalDay = 0;
    //       //     int itotWorkTypeDay = 0;
    //       //     TableRow tr_det1 = new TableRow();
    //       //     if (months5 >= 0)
    //       //     {
    //       //         for (int j = 1; j <= months5 + 1; j++)
    //       //         {
    //       //             int totdayhq = 0;
    //       //             DataSet dsdayhq = new DataSet();
    //       //             DCR dcr2 = new DCR();
    //       //             Literal lit_dethq = new Literal();
    //       //             int totdayex = 0;
    //       //             int totdayos = 0;
    //       //             dsdayhq = dcr2.getHQDay_HQ_Cnt(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             for (int i = 0; i < dsdayhq.Tables[0].Rows.Count; i++)
    //       //             {
    //       //                 if (dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1")
    //       //                 {
    //       //                     totdayhq += 1;
    //       //                 }
    //       //                 if (dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "2" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1,2")
    //       //                 {
    //       //                     totdayex += 1;
    //       //                 }
    //       //                 if (dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "3" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "4" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1,2,3" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "2,3" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "3,4" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString() == "1,2,4" || dsdayhq.Tables[0].Rows[i].ItemArray.GetValue(1).ToString().Contains("3"))
    //       //                 {
    //       //                     totdayos += 1;
    //       //                 }
    //       //             }

    //       //             if (dsdayhq.Tables[0].Rows.Count > 0)
    //       //             {//  totdayhq =  Convert.ToInt16(dsdayhq.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //       //                 lit_dethq.Text = "" + totdayhq;

    //       //             }
    //       //             else
    //       //             {
    //       //                 lit_dethq.Text = " - ";
    //       //             }
    //       //             if (totdayhq == 0)
    //       //             {
    //       //                 lit_dethq.Text = " - ";
    //       //             }

    //       //                 TableCell tc_dethq = new TableCell();



    //       //                 tc_dethq.BorderStyle = BorderStyle.Solid;
    //       //                 tc_dethq.BorderWidth = 1;
    //       //                 tc_dethq.Style.Add("text-align", "left");
    //       //                 tc_dethq.Style.Add("font-family", "Calibri");
    //       //                 tc_dethq.Style.Add("font-size", "10pt");
    //       //                 //tc_det_sf_Tot.Width = 50;
    //       //                 tc_dethq.HorizontalAlign = HorizontalAlign.Center;
    //       //                 tc_dethq.VerticalAlign = VerticalAlign.Middle;
    //       //                 tc_dethq.Controls.Add(lit_dethq);
    //       //                 tr_det_hq.Cells.Add(tc_dethq);


    //       //                 DataSet dsdayex = new DataSet();
    //       //                 //dsdayex = dcr2.getHQDay_EX(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //                 //if (dsdayex.Tables[0].Rows.Count > 0)
    //       //                // totdayex = Convert.ToInt16(dsdayex.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
    //       //                 Literal lit_detex = new Literal();
    //       //                 if (dsdayhq.Tables[0].Rows.Count > 0)
    //       //                 {//  totdayhq =  Convert.ToInt16(dsdayhq.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //       //                     lit_detex.Text = "" + totdayex;

    //       //                 }
    //       //                 else
    //       //                 {
    //       //                     lit_detex.Text = " - ";
    //       //                 }
    //       //                 if (totdayex == 0)
    //       //                 {
    //       //                     lit_detex.Text = " - ";
    //       //                 }
    //       //                 TableCell tc_detex = new TableCell();


    //       //               //  lit_detex.Text = "" + totdayex;

    //       //                 tc_detex.BorderStyle = BorderStyle.Solid;
    //       //                 tc_detex.BorderWidth = 1;
    //       //                 tc_detex.Style.Add("text-align", "left");
    //       //                 tc_detex.Style.Add("font-family", "Calibri");
    //       //                 tc_detex.Style.Add("font-size", "10pt");
    //       //                 //tc_det_sf_Tot.Width = 50;
    //       //                 tc_detex.HorizontalAlign = HorizontalAlign.Center;
    //       //                 tc_detex.VerticalAlign = VerticalAlign.Middle;
    //       //                 tc_detex.Controls.Add(lit_detex);
    //       //                 tr_det_hq.Cells.Add(tc_detex);



    //       //                 DataSet dsdayos = new DataSet();
    //       //                 //dsdayos = dcr2.getHQDay_OS(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //                 //if (dsdayos.Tables[0].Rows.Count > 0)
    //       //                 //    totdayos =  Convert.ToInt16(dsdayos.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
    //       //                 Literal lit_detos = new Literal();
    //       //                 if (dsdayhq.Tables[0].Rows.Count > 0)
    //       //                 {

    //       //                     lit_detos.Text = "" + totdayos;

    //       //                 }
    //       //                 else
    //       //                 {
    //       //                     lit_detos.Text = " - ";
    //       //                 }
    //       //                 if (totdayos == 0)
    //       //                 {
    //       //                     lit_detos.Text = " - ";
    //       //                 }
    //       //                 TableCell tc_detos = new TableCell();


    //       //               //  lit_detos.Text = "" + totdayos;

    //       //                 tc_detos.BorderStyle = BorderStyle.Solid;
    //       //                 tc_detos.BorderWidth = 1;
    //       //                 tc_detos.Style.Add("text-align", "left");
    //       //                 tc_detos.Style.Add("font-family", "Calibri");
    //       //                 tc_detos.Style.Add("font-size", "10pt");
    //       //                 //tc_det_sf_Tot.Width = 50;
    //       //                 tc_detos.HorizontalAlign = HorizontalAlign.Center;
    //       //                 tc_detos.VerticalAlign = VerticalAlign.Middle;
    //       //                 tc_detos.Controls.Add(lit_detos);
    //       //                 tr_det_hq.Cells.Add(tc_detos);


    //       //             fldwrk_total = 0;
    //       //             DataSet ds = new DataSet();
    //       //             DCR dcr1 = new DCR();

    //       //             ds = dcr1.getDaysWorked(sf_code, cmonth5, cyear5, drFF1["sf_code"].ToString());
    //       //             if (ds.Tables[0].Rows.Count > 0)
    //       //                 tot_fldwrk = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //       //             itotWorkType1 += fldwrk_totalDay + Convert.ToInt16(tot_fldwrk);

    //       //             tr_det1.BackColor = System.Drawing.Color.White;



    //       //             TableCell tc_det_work = new TableCell();
    //       //             Literal lit_det_work = new Literal();
    //       //             lit_det_work.Text = "" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //       //             if (tot_fldwrk == "0")
    //       //             {
    //       //                 lit_det_work.Text = " - ";
    //       //             }
    //       //             tc_det_work.BorderStyle = BorderStyle.Solid;
    //       //             tc_det_work.BorderWidth = 1;
    //       //             tc_det_work.Controls.Add(lit_det_work);
    //       //             tc_det_work.Style.Add("text-align", "left");
    //       //             tc_det_work.Style.Add("font-family", "Calibri");
    //       //             tc_det_work.Style.Add("font-size", "10pt");
    //       //             tr_det_hq.Cells.Add(tc_det_work);

    //       //             int docCallsHQ = 0;

    //       //             DataSet dsCallsHQ = new DataSet();
    //       //             DCR dcrnew = new DCR();
    //       //             //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
    //       //             dsCallsHQ = dcrnew.getHQCalls_HQ(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             docCallsHQ = Convert.ToInt16(dsCallsHQ.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
    //       //          //   fldwrk_total = docCalls;

    //       //             TableCell tc_dethq1 = new TableCell();
    //       //             Literal lit_dethq1 = new Literal();

    //       //             lit_dethq1.Text = " " + docCallsHQ;
    //       //             if (docCallsHQ == 0)
    //       //             {
    //       //                 lit_dethq1.Text = " - ";
    //       //             }

    //       //             tc_dethq1.BorderStyle = BorderStyle.Solid;
    //       //             tc_dethq1.BorderWidth = 1;
    //       //             tc_dethq1.Style.Add("text-align", "left");
    //       //             tc_dethq1.Style.Add("font-family", "Calibri");
    //       //             tc_dethq1.Style.Add("font-size", "10pt");
    //       //             //tc_det_sf_Tot.Width = 50;
    //       //             tc_dethq1.HorizontalAlign = HorizontalAlign.Center;
    //       //             tc_dethq1.VerticalAlign = VerticalAlign.Middle;
    //       //             tc_dethq1.Controls.Add(lit_dethq1);
    //       //             tr_det_hq.Cells.Add(tc_dethq1);

    //       //             int docCallsEX = 0;
    //       //             DataSet dsCallsEx = new DataSet();

    //       //             //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
    //       //             dsCallsEx = dcrnew.getHQCalls_EX(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             docCallsEX = Convert.ToInt16(dsCallsEx.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //       //             TableCell tc_detex1 = new TableCell();
    //       //             Literal lit_detex1 = new Literal();

    //       //             lit_detex1.Text = "" + docCallsEX;
    //       //             if (docCallsEX == 0)
    //       //             {
    //       //                 lit_detex1.Text = " - ";
    //       //             }

    //       //             tc_detex1.BorderStyle = BorderStyle.Solid;
    //       //             tc_detex1.BorderWidth = 1;
    //       //             tc_detex1.Style.Add("text-align", "left");
    //       //             tc_detex1.Style.Add("font-family", "Calibri");
    //       //             tc_detex1.Style.Add("font-size", "10pt");
    //       //             //tc_det_sf_Tot.Width = 50;
    //       //             tc_detex1.HorizontalAlign = HorizontalAlign.Center;
    //       //             tc_detex1.VerticalAlign = VerticalAlign.Middle;
    //       //             tc_detex1.Controls.Add(lit_detex1);
    //       //             tr_det_hq.Cells.Add(tc_detex1);


    //       //             int docCallsOS = 0;
    //       //             DataSet dsCallsOS = new DataSet();

    //       //             //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
    //       //             dsCallsOS = dcrnew.getHQCalls_OS(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             docCallsOS = Convert.ToInt16(dsCallsOS.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

    //       //             TableCell tc_detos1 = new TableCell();
    //       //             Literal lit_detos1 = new Literal();

    //       //             lit_detos1.Text = "" + docCallsOS;
    //       //             if (docCallsOS == 0)
    //       //             {
    //       //                 lit_detos1.Text = " - ";
    //       //             }

    //       //             tc_detos1.BorderStyle = BorderStyle.Solid;
    //       //             tc_detos1.BorderWidth = 1;
    //       //             tc_detos1.Style.Add("text-align", "left");
    //       //             tc_detos1.Style.Add("font-family", "Calibri");
    //       //             tc_detos1.Style.Add("font-size", "10pt");
    //       //             //tc_det_sf_Tot.Width = 50;
    //       //             tc_detos1.HorizontalAlign = HorizontalAlign.Center;
    //       //             tc_detos1.VerticalAlign = VerticalAlign.Middle;
    //       //             tc_detos1.Controls.Add(lit_detos1);
    //       //             tr_det_hq.Cells.Add(tc_detos1);




    //       //             TableCell tc_doc_call = new TableCell();
    //       //             Literal lit_doc_call = new Literal();
    //       //             DataSet dsCalls = new DataSet();
    //       //             int fldwrk_totalDay1 = 0;

    //       //          //   DCR dcr2 = new DCR();
    //       //             //dsCalls = dcr2.getHQCalls(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             //if (dsCalls.Tables[0].Rows.Count > 0)
    //       //             int docCalls = 0;
    //       //             //foreach (DataRow drcalls in dsCalls.Tables[0].Rows)
    //       //             //{

    //       //                 DataSet dsCallsdoc = new DataSet();
    //       //                 DCR dcrnew1 = new DCR();
    //       //               //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
    //       //                 dsCallsdoc = dcrnew1.getHQCalls_Doc(drFF1["sf_code"].ToString(),cmonth5, cyear5, sf_code);
    //       //                 docCalls =  Convert.ToInt16(dsCallsdoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
    //       //                fldwrk_total = docCalls;

    //       //               //  itotWorkTypeDay += fldwrk_totalDay1 + fldwrk_total;
    //       //            // }
    //       //             itotWorkTypeDay += fldwrk_totalDay1 + fldwrk_total;
    //       //             lit_doc_call.Text = "" + fldwrk_total;
    //       //             if (fldwrk_total == 0)
    //       //             {
    //       //                 lit_doc_call.Text = " - ";
    //       //             }
    //       //             tc_doc_call.BorderStyle = BorderStyle.Solid;
    //       //             tc_doc_call.HorizontalAlign = HorizontalAlign.Left;
    //       //             //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");

    //       //             tc_doc_call.BorderWidth = 1;
    //       //             tc_doc_call.Style.Add("font-family", "Calibri");
    //       //             tc_doc_call.Style.Add("font-size", "10pt");
    //       //             tc_doc_call.Controls.Add(lit_doc_call);
    //       //             tr_det_hq.Cells.Add(tc_doc_call);


    //       //             TableCell tc_doc_call_Met = new TableCell();
    //       //             Literal lit_doc_call_Met = new Literal();
    //       //             DataSet dsCal = new DataSet();


    //       //             int docCal = 0;

    //       //             //    DataSet dsCallsdoc = new DataSet();
    //       //             DCR dcr = new DCR();
    //       //             //  dsCallsdoc = dcrnew.getHQCalls_Doc(drFF1["sf_code"].ToString(), drcalls["dcrdate"].ToString(), cmonth5, cyear5);
    //       //             dsCal = dcr.getHQCalls_met(drFF1["sf_code"].ToString(), cmonth5, cyear5, sf_code);
    //       //             docCal = Convert.ToInt16(dsCal.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
    //       //             //  fldwrk_total = docCalls;

    //       //             //   itotWorkTypeDay += fldwrk_totalDay1 + fldwrk_total;
    //       //             lit_doc_call_Met.Text = "" + docCal;
    //       //             if (docCal == 0)
    //       //             {
    //       //                 lit_doc_call_Met.Text = " - ";
    //       //             }
    //       //             tc_doc_call_Met.BorderStyle = BorderStyle.Solid;
    //       //             tc_doc_call_Met.HorizontalAlign = HorizontalAlign.Left;
    //       //             //  tc_msd_month.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");

    //       //             tc_doc_call_Met.BorderWidth = 1;
    //       //             tc_doc_call_Met.Style.Add("font-family", "Calibri");
    //       //             tc_doc_call_Met.Style.Add("font-size", "10pt");
    //       //             tc_doc_call_Met.Controls.Add(lit_doc_call_Met);
    //       //             tr_det_hq.Cells.Add(tc_doc_call_Met);

    //       //             cmonth5 = cmonth5 + 1;
    //       //             if (cmonth5 == 13)
    //       //             {
    //       //                 cmonth5 = 1;
    //       //                 cyear5 = cyear5 + 1;
    //       //             }
    //       //         }

    //       //         TableCell tc_det_sf_Tot = new TableCell();
    //       //         Literal lit_det_sf_Tot = new Literal();

    //       //         if (itotWorkType1 == 0)
    //       //         {
    //       //             lit_det_sf_Tot.Text = " - ";
    //       //         }
    //       //         else
    //       //         {
    //       //             lit_det_sf_Tot.Text = "" + itotWorkType1;
    //       //         }
    //       //         tc_det_sf_Tot.BorderStyle = BorderStyle.Solid;
    //       //         tc_det_sf_Tot.BorderWidth = 1;
    //       //         tc_det_sf_Tot.Style.Add("text-align", "left");
    //       //         tc_det_sf_Tot.Style.Add("font-family", "Calibri");
    //       //         tc_det_sf_Tot.Style.Add("font-size", "10pt");
    //       //         //tc_det_sf_Tot.Width = 50;
    //       //         tc_det_sf_Tot.HorizontalAlign = HorizontalAlign.Center;
    //       //         tc_det_sf_Tot.VerticalAlign = VerticalAlign.Middle;
    //       //         tc_det_sf_Tot.Controls.Add(lit_det_sf_Tot);
    //       //         tr_det_hq.Cells.Add(tc_det_sf_Tot);

    //       //         TableCell tc_det_sf_Tot1 = new TableCell();
    //       //         Literal lit_det_sf_Tot1 = new Literal();

    //       //         if (itotWorkType1 == 0)
    //       //         {
    //       //             lit_det_sf_Tot1.Text =  " - ";
    //       //         }
    //       //         else
    //       //         {
    //       //             lit_det_sf_Tot1.Text = "" + itotWorkTypeDay;
    //       //         }
    //       //         tc_det_sf_Tot1.BorderStyle = BorderStyle.Solid;
    //       //         tc_det_sf_Tot1.BorderWidth = 1;
    //       //         tc_det_sf_Tot1.Style.Add("text-align", "left");
    //       //         tc_det_sf_Tot1.Style.Add("font-family", "Calibri");
    //       //         tc_det_sf_Tot1.Style.Add("font-size", "10pt");
    //       //         //tc_det_sf_Tot.Width = 50;
    //       //         tc_det_sf_Tot1.HorizontalAlign = HorizontalAlign.Center;
    //       //         tc_det_sf_Tot1.VerticalAlign = VerticalAlign.Middle;
    //       //         tc_det_sf_Tot1.Controls.Add(lit_det_sf_Tot1);
    //       //         tr_det_hq.Cells.Add(tc_det_sf_Tot1);

    //       //         tblhq.Rows.Add(tr_det_hq);



    //       //     }

    //       // }
    //    }
    //}




    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 28;
        else if (imonth == 3)
            idays = 31;
        else if (imonth == 4)
            idays = 30;
        else if (imonth == 5)
            idays = 31;
        else if (imonth == 6)
            idays = 30;
        else if (imonth == 7)
            idays = 31;
        else if (imonth == 8)
            idays = 31;
        else if (imonth == 9)
            idays = 30;
        else if (imonth == 10)
            idays = 31;
        else if (imonth == 11)
            idays = 30;
        else if (imonth == 12)
            idays = 31;

        return idays;
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string strFileName = "rptTPView";
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);
        iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();


    }

    public override void VerifyRenderingInServerForm(Control txt_salutaion)
    {
        /* Verifies that the control is rendered */
    }

    //public static void PrintWebControl(Control ControlToPrint)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ControlToPrint is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage);
    //        ((WebControl)ControlToPrint).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ControlToPrint);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();

    //}
    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    string Export = this.Page.Title;
    //    string attachment = "attachment; filename=" + Export + ".xls";
    //    Response.ClearContent();
    //    Response.AddHeader("content-disposition", attachment);
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    HtmlForm frm = new HtmlForm();
    //    pnlContents.Parent.Controls.Add(frm);
    //    frm.Attributes["runat"] = "server";
    //    frm.Controls.Add(pnlContents);
    //    frm.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();


    //}
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
       
        SalesForce sf = new SalesForce();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells
            if (mode == "1")
            {
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "No of Reps", "#0097AC", true);
            }
            else if (mode == "2")
            {
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ Name", "#0097AC", true);
            }

            //AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            //  AddMergedCells(objgridviewrow, objtablecell, 0, "Rate", "#0097AC", true);
            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();
            TableCell objtablecell2 = new TableCell();
            TableCell objtablecell3 = new TableCell();
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    if (mode == "1")
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 0, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "No of Days Worked", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Doctor Calls", "#0097AC", false);
                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }

                    }
                    else if (mode == "2")
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 0, 10, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 2, "Covered Clusters", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell2, 0, 0, "Yes", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell2, 0, 0, "No", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "No of Days Worked", "#0097AC", false);

                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "HQ", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "EX", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "OS", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Total", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 4, "Total Doctor Calls", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "HQ", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "EX", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "OS", "#0097AC", false);
                        AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Total", "#0097AC", false);



                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                }
                if (mode == "1")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, 2, "Total", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "No of Days Worked", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Doctor Calls", "#0097AC", false);
                }
                else if (mode == "2")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 2, 2, "Total", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "No of Days Worked", "#0097AC", false);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Total Doctor Calls", "#0097AC", false);
                }
            }


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            if (mode == "1")
            {
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            }
            else if (mode == "2")
            {
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            }
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        if (mode == "2")
        {
            if (objgridviewrow.RowIndex == 1)
            {
                objtablecell.Attributes.Add("class", "stickyFirstRow");
            }
            else if (objgridviewrow.RowIndex == 2)
            {
                objtablecell.Attributes.Add("class", "stickySecondRow");
            }
            else
            {
                objtablecell.Attributes.Add("class", "stickyThirdRow");
            }
        }
        else
        {
            if (objgridviewrow.RowIndex == 1)
            {
                objtablecell.Attributes.Add("class", "stickyFirstRow");
            }
            else
            {
                objtablecell.Attributes.Add("class", "stickySecondRow");
            }
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;
            //
            #region Calculations



            //for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
            //{


            //}
            //try
            //{
            //    int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
            //    //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            //}
            //catch
            //{
            //    e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            //}

            #endregion
            if (mode == "1")
            {
                int approved = 0;
                int applied = 0;
                for (int l = 2, j = 0; l < e.Row.Cells.Count - 2; l ++)
                {
                    //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                    //{
                    //    e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();
                    //    e.Row.Cells[l + 4].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 4].ToString();
                    //    e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:normal;");
                    //    e.Row.Cells[l + 4].Attributes.Add("style", "color:red;font-weight:normal;");


                    //}

                    int appl = (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") || (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                    int appr = (e.Row.Cells[l + 1].Text == "") || (e.Row.Cells[l + 1].Text == "&nbsp;") || (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);




                    //   dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();
                    //   dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 4] = (pApprT + appr).ToString();
                    applied = applied + appl;
                    approved = approved + appr;
                    l++;

                    j++;
                }


                e.Row.Cells[e.Row.Cells.Count - 2].Text = applied.ToString();
                e.Row.Cells[e.Row.Cells.Count - 1].Text = approved.ToString();
                 //Changes S by Rp 
                if (e.Row.Cells[e.Row.Cells.Count - 1].Text != "&nbsp;" && e.Row.Cells[e.Row.Cells.Count - 1].Text != "0" && e.Row.Cells[e.Row.Cells.Count - 1].Text != "-")
                {
                    HyperLink hLink = new HyperLink();    // Request.QueryString["sf_name"].ToString();
                    hLink.Text = e.Row.Cells[e.Row.Cells.Count - 1].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + mode + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(hLink);
                }
                //Changes E by Rp 

                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
            }
            else if (mode == "2")
            {
                int approved = 0;
                int applied = 0;
                for (int l = 4, j = 0; l < e.Row.Cells.Count - 2; l += 10)
                {
                    //if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                    //{
                    //    e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();
                    //    e.Row.Cells[l + 4].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 4].ToString();
                    //    e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:normal;");
                    //    e.Row.Cells[l + 4].Attributes.Add("style", "color:red;font-weight:normal;");


                    //}

                    int appl = (e.Row.Cells[l + 2].Text == "") || (e.Row.Cells[l + 2].Text == "&nbsp;") || (e.Row.Cells[l + 2].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 2].Text);
                    int appr = (e.Row.Cells[l + 6].Text == "") || (e.Row.Cells[l + 6].Text == "&nbsp;") || (e.Row.Cells[l + 6].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 6].Text);

                    //  int pApplT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 5] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 5].ToString() == "") || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 5].ToString() == "-") ? 0 : Convert.ToInt32(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 5].ToString());
                    // int pApprT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 9] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 9].ToString() == "") || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 9].ToString() == "-") ? 0 : Convert.ToInt32(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 9].ToString());




                    //   dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();
                    //   dtrowClr.Rows[dtrowClr.Rows.Count - 1][l + 4] = (pApprT + appr).ToString();
                    applied = applied + appl;
                    approved = approved + appr;
                    // l++;

                    j++;
                }

                e.Row.Cells[e.Row.Cells.Count - 2].Text = applied.ToString();
                e.Row.Cells[e.Row.Cells.Count - 1].Text = approved.ToString();


                //Changes S by Rp 
                if (e.Row.Cells[e.Row.Cells.Count - 1].Text != "&nbsp;" && e.Row.Cells[e.Row.Cells.Count - 1].Text != "0" && e.Row.Cells[e.Row.Cells.Count - 1].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[e.Row.Cells.Count - 1].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + mode + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][5].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[e.Row.Cells.Count - 1].Controls.Add(hLink);
                }
                //Changes E by Rp 
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
              
                //  e.Row.Cells[3].Text = string.Join(Environment.NewLine, e.Row.Cells[3].Text.Split(','));
            }
        }

    }

    protected void grdMgr_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            #endregion
            //
            #region Merge cells


            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Parameters", "#0097AC", true);

            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);
            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                iLstMonth.Add(cmonth);
                iLstYear.Add(cyear);
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()).Substring(0, 3) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 1, 0, sTxt, "#0097AC", true);
                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Total", "#0097AC", true);
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            //
            #endregion
            //
        }
    }
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);
    //}


}