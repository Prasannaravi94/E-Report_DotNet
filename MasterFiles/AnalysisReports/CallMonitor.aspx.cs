using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;


public partial class MasterFiles_AnalysisReports_CallMonitor : System.Web.UI.Page
{
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string strFieledForceName = string.Empty;
    DataSet dsDoctor = new DataSet();
    string sf_name = string.Empty;
    string sf_hq = string.Empty;
    string sf_desig = string.Empty;
    string sCurrentDate = string.Empty;
    string tot_met = string.Empty;
    string doc_calls = string.Empty;
    int Missed_dr = 0;
    string tot_dcr_dr = string.Empty;
    string tot_flddoc_miss = string.Empty;
    DateTime dtCurrent1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["Frm_Month"].ToString();
            FYear = Request.QueryString["Frm_year"].ToString();

            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
            DataSet dssf = sf.getSfName(sf_code);
            if (dssf.Tables[0].Rows.Count > 0)
                sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            sf_desig = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
            sf_hq = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());

            //      string sMonth = getMonthName(Convert.ToInt16(FMonth)) + " " + FYear.ToString();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = lblHead.Text + strFrmMonth + "<span style='font-weight: bold;color:Black;'> " + " ( " + sf_name + " - " + sf_desig + " - " + sf_hq + " )" + "</span>";
            //  LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillDoctor();
        }
    }

    private void FillDoctor()
    {
        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        Doctor dc = new Doctor();

        dsDoctor = dc.Missed_Cal_Monitor(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear));
        //dsDoctor = dc.Missed_Doc(div_code, sf_code, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, sMode, vMode);



        if (dsDoctor.Tables[1].Rows.Count > 0)
        {
            //grdDoctor.Visible = true;
            dtdoctor.DataSource = dsDoctor.Tables[1];
            dtdoctor.DataBind();
        }
        CreateDynamicTable();

    }
    private void CreateDynamicTable()
    {
        int iCount = 0;

        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();

        dsSalesForce = sf.getmode_monitor(div_code);
        string tot_doc = string.Empty;
        string tot_doctor = string.Empty;
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");
            //tr_header.Attributes.Add("Class", "tblCellFont");


            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 300;
            tc_DR_Name.Height = 35;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Summary</center>";
            tc_DR_Name.ColumnSpan = 2;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("font-size", "12pt");
            tc_DR_Name.Style.Add("Color", "#414d55");
            tc_DR_Name.Style.Add("border-color", "#DCE2E8");
            //tc_DR_Name.Style.Add("font-weight", "bold");
            //tc_DR_Name.Attributes.Add("Class", "tr_det_head");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);



            tbl.Rows.Add(tr_header);
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);


            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            //    tbl.Rows.Add(tr_header);


            int cmonthact = Convert.ToInt32(FMonth);


            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);


            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            Literal lit_det_doct = new Literal();
            Literal lit_det_metdoc = new Literal();
         
            foreach (DataRow drow in dsSalesForce.Tables[0].Rows)
            {
                DataSet dsCall = new DataSet();
                DataSet dsField = new DataSet();
                double dblaverage = 0.00;
                Literal lit_det_t = new Literal();
                Literal lit_docmet = new Literal();
                TableRow tr_det = new TableRow();
                string doc_calls = "Total " + drow["Doc_Cat"].ToString() + " Calls";
                string doc_calls_met = drow["Doc_Cat"].ToString() + " Calls Met";
                if (drow["Doc_Cat_SName"].ToString() == "No.of Listed Drs in List")
                {



                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_doc = new Literal();
                    lit_det_doc.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_doc);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det.Cells.Add(tc_det_FF1);



                    DCR dcr1 = new DCR();
                    DataSet ds = new DataSet();


                    int cmonthd = Convert.ToInt32(FMonth);
                    int cyeard = Convert.ToInt32(FYear);


                    if (cmonthd == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeard + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthd + 1) + "-01-" + cyeard;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }
                    dtCurrent1 = Convert.ToDateTime(sCurrentDate);

                    SalesForce dcrdoc = new SalesForce();
                    DCR dc = new DCR();
                    ds = dc.Call_monitor_totdr(div_code, sf_code, cmonthd, cyeard, dtCurrent1);
                    if (ds.Tables[0].Rows.Count > 0)
                        tot_doctor = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //  itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                    //itotWorkType += fldwrk_total;

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();

                    lit_det_doct.Text = "" + tot_doctor;
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_doct);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tr_det.Cells.Add(tc_det_FF);





                    tbl.Rows.Add(tr_det);


                }

                else if (drow["Doc_Cat_SName"].ToString() == "Total No.of Calls Met")
                {

                    int cmonth2 = Convert.ToInt32(FMonth);
                    int cyear2 = Convert.ToInt32(FYear);


                    ViewState["cmonth"] = cmonth2;
                    ViewState["cyear"] = cyear2;
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_met = new Literal();
                    lit_det_met.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_met);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();



                    DataSet ds = new DataSet();
                    ds = dcr1.Call_monitor_Met(sf_code, div_code, cmonth2, cyear2,dtCurrent1);
                    if (ds.Tables[0].Rows.Count > 0)
                        tot_met = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();

                    lit_det_metdoc.Text = "" + ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_metdoc);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF);





                    tbl.Rows.Add(tr_det1);
                }
                else if (drow["Doc_Cat_SName"].ToString() == "Total No.of Calls Seen")
                {
                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_seen = new Literal();
                    lit_det_seen.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_seen);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();


                    string tot_fldwrkDays = string.Empty;
                    //   int cmonthact = Convert.ToInt32(FMonth);
                    int cyearact = Convert.ToInt32(FYear);
                    dsField = dcr1.Call_monitor_Seen(sf_code, div_code, cmonthact, cyearact,dtCurrent1);

                    if (dsField.Tables[0].Rows.Count > 0)
                        tot_fldwrkDays = dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = "" + dsField.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF);


                    tbl.Rows.Add(tr_det1);
                }


                else if (drow["Doc_Cat_SName"].ToString() == "Total No.of Listed Drs Missed")
                {

                    int cmonth3 = Convert.ToInt32(FMonth);
                    int cyear3 = Convert.ToInt32(FYear);

                    ViewState["cmonth"] = cmonth3;
                    ViewState["cyear"] = cyear3;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_miss = new Literal();
                    lit_det_miss.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_miss);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DCR dcr1 = new DCR();
                    int tot_missed = 0;

                    DataSet ds = new DataSet();
                    if (lit_det_doct.Text != "0")
                        tot_missed = Convert.ToInt32(lit_det_doct.Text) - Convert.ToInt32(lit_det_metdoc.Text);
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();

                    if (tot_missed == 0)
                    {
                        lit_det_FF.Text = " - ";
                    }
                    else
                    {
                        lit_det_FF.Text = "" + tot_missed;
                    }
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF);


                    tbl.Rows.Add(tr_det1);
                }


                else if (drow["Doc_Cat_SName"].ToString() == doc_calls)
                {
                    int cmonthd = Convert.ToInt32(FMonth);
                    int cyeard = Convert.ToInt32(FYear);


                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_m = new Literal();
                    lit_det_m.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_m);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("background", "LightBlue");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF1);

                    DataSet dscat = new DataSet();
                    if (cmonthd == 12)
                    {
                        sCurrentDate = "01-01-" + (cyeard + 1);
                    }
                    else
                    {
                        sCurrentDate = (cmonthd + 1) + "-01-" + cyeard;
                        //sCurrentDate = cmonth  + "-01-" + cyear;
                    }
                    dtCurrent1 = Convert.ToDateTime(sCurrentDate);
                    DCR dcr = new DCR();
                    dscat = dcr.Get_tot_doc_Cat(div_code, sf_code, cmonthd, cyeard, dtCurrent1, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()));
                    if (dscat.Tables[0].Rows.Count > 0)
                        tot_doc = dscat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //  itotWorkType += fldwrk_total + Convert.ToInt16(tot_fldwrk);
                    //itotWorkType += fldwrk_total;

                    tr_det.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                
                    lit_det_t.Text = "" + tot_doc;
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_t);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("background", "LightBlue");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tr_det1.Cells.Add(tc_det_FF);





                    tbl.Rows.Add(tr_det1);
                }

                else if (drow["Doc_Cat_SName"].ToString() == doc_calls_met)
                {

                    int cmonth8 = Convert.ToInt32(FMonth);
                    int cyear8 = Convert.ToInt32(FYear);

                    ViewState["cmonth"] = cmonth8;
                    ViewState["cyear"] = cyear8;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tc_det_FF1.Style.Add("background", "LightPink");
                    tc_det_FF1.Style.Add("color", "#414d55");
                    //tc_det_FF1.Style.Add("border-color", "Black");
                    tr_det1.Cells.Add(tc_det_FF1);
                    
                    DCR dcr1 = new DCR();

                     

                    DataSet dsDCR = new DataSet();

                    DCR dc = new DCR();
                    dsDCR = dc.Call_monitor_Cat_Met(sf_code, div_code, cmonth8, cyear8,dtCurrent1, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()));
                    //  if (dsDCR.Tables[0].Rows.Count > 0)
                    //  tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (dsDCR.Tables[0].Rows.Count > 0)
                        tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();                 

                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                    if (lit_docmet.Text != "0")
                    {
                        lit_docmet.Text = "" + tot_dcr_dr;
                    }
                    else
                    {
                        lit_docmet.Text = " - ";
                    }
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_docmet);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tc_det_FF.Style.Add("background", "LightPink");
                    tr_det1.Cells.Add(tc_det_FF);


                    tbl.Rows.Add(tr_det1);
                }
                else
                {
                    int cmonth8 = Convert.ToInt32(FMonth);
                    int cyear8 = Convert.ToInt32(FYear);

                    ViewState["cmonth"] = cmonth8;
                    ViewState["cyear"] = cyear8;

                    TableRow tr_det1 = new TableRow();
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF1 = new TableCell();
                    Literal lit_det_FF1 = new Literal();
                    lit_det_FF1.Text = "" + drow[0];
                    tc_det_FF1.BorderStyle = BorderStyle.Solid;
                    tc_det_FF1.BorderWidth = 1;
                    tc_det_FF1.Controls.Add(lit_det_FF1);
                    tc_det_FF1.Style.Add("text-align", "left");
                    tc_det_FF1.Style.Add("font-family", "Calibri");
                    tc_det_FF1.Style.Add("font-size", "10pt");
                    tc_det_FF1.Style.Add("color", "#414d55");
                    tc_det_FF1.Style.Add("background", "LightGray");
                    //tc_det_FF1.Style.Add("border-color", "Black");
                    tr_det1.Cells.Add(tc_det_FF1);
                    string tot_dcr_dr = string.Empty;
                    DCR dcr1 = new DCR();



                  
                    DataSet dsDCR = new DataSet();
                    DataSet dsDCR1 = new DataSet();
                    DCR dc = new DCR();
                    //  dsDCR = dc.Catg_Visit_Report1(sf_code, div_code, cmonth8, cyear8, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()), 1);
                    //  if (dsDCR.Tables[0].Rows.Count > 0)
                    //  tot_dcr_dr = dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Missed_dr = 0;
                    tot_flddoc_miss = "";
                    dsDCR = dc.Get_tot_doc_Cat(div_code, sf_code, cmonth8, cyear8, dtCurrent1, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()));
                    dsDCR1 = dc.Call_monitor_Cat_Met(sf_code, div_code, cmonth8, cyear8,dtCurrent1, Convert.ToInt32(drow["Doc_Cat_Code"].ToString()));
                    Missed_dr = Convert.ToInt32(dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) - Convert.ToInt32(dsDCR1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    if (Convert.ToInt32(dsDCR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString()) != 0)
                        tot_flddoc_miss = Convert.ToString(Missed_dr);
              
                    tr_det1.BackColor = System.Drawing.Color.White;
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    if (lit_det_FF.Text != "0")
                    {
                        lit_det_FF.Text = "" + tot_flddoc_miss;
                    }
                    else
                    {
                        lit_det_FF.Text = " - ";
                    }
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Height = 25;
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tc_det_FF.Style.Add("text-align", "left");
                    tc_det_FF.Style.Add("font-family", "Calibri");
                    tc_det_FF.Style.Add("font-size", "10pt");
                    tc_det_FF.Style.Add("background", "LightGray");
                    tr_det1.Cells.Add(tc_det_FF);


                    tbl.Rows.Add(tr_det1);
                }

            }

        }
        TableRow tr_header1 = new TableRow();
        tr_header1.BorderStyle = BorderStyle.Solid;
        tr_header1.BorderWidth = 1;
        tr_header1.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");
        //tr_header1.Attributes.Add("Class", "tblCellFont");


        TableCell tc_DR_Name1 = new TableCell();
        tc_DR_Name1.BorderStyle = BorderStyle.Solid;
        tc_DR_Name1.BorderWidth = 1;
        tc_DR_Name1.Width = 300;
        tc_DR_Name1.Height = 35;
        Literal lit_DR_Name1 = new Literal();
        lit_DR_Name1.Text = "<center>Visit Details</center>";
        tc_DR_Name1.ColumnSpan = 2;
        //tc_DR_Name1.Style.Add("font-family", "Calibri");
        tc_DR_Name1.Style.Add("font-size", "12pt");
        tc_DR_Name1.Style.Add("Color", "#414d55");
        tc_DR_Name1.Style.Add("border-color", "#DCE2E8");
        //tc_DR_Name1.Style.Add("font-weight", "bold");
        //tc_DR_Name1.Attributes.Add("Class", "tr_det_head");
        tc_DR_Name1.Controls.Add(lit_DR_Name1);
        tr_header1.Cells.Add(tc_DR_Name1);

        tblhq.Rows.Add(tr_header1);

        TableRow tr_det_vis = new TableRow();
        tr_det_vis.BackColor = System.Drawing.Color.White;
        TableCell tc_det_vis = new TableCell();
        Literal lit_det_vis = new Literal();
        lit_det_vis.Text = "One Visit drs Count";
        tc_det_vis.BorderStyle = BorderStyle.Solid;
        tc_det_vis.BorderWidth = 1;
        tc_det_vis.Controls.Add(lit_det_vis);
        tc_det_vis.Style.Add("text-align", "left");
        tc_det_vis.Style.Add("font-family", "Calibri");
        tc_det_vis.Style.Add("font-size", "10pt");
        tr_det_vis.Cells.Add(tc_det_vis);

        TableCell tc_det_1 = new TableCell();
        Literal lit_det_1 = new Literal();
        DataSet dsvis1 = new DataSet();
        DCR dcV = new DCR();
        int cmon = Convert.ToInt32(FMonth);
        int cyr = Convert.ToInt32(FYear);
        dsvis1 = dcV.visit_cnt_1_dt(sf_code, div_code, cmon, cyr, dtCurrent1);
        if(dsvis1.Tables[0].Rows.Count > 0)
        lit_det_1.Text = "" + dsvis1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        tc_det_1.BorderStyle = BorderStyle.Solid;
        tc_det_1.BorderWidth = 1;
        tc_det_1.Height = 25;
        tc_det_1.Controls.Add(lit_det_1);
        tc_det_1.Style.Add("text-align", "left");
        tc_det_1.Style.Add("font-family", "Calibri");
        tc_det_1.Style.Add("font-size", "10pt");
        tr_det_vis.Cells.Add(tc_det_1);

        TableRow tr_det_vis1 = new TableRow();
        TableCell tc_det_vis1 = new TableCell();
        Literal lit_det_vis1 = new Literal();
        lit_det_vis1.Text = "Two Visit drs Count";
        tc_det_vis1.BorderStyle = BorderStyle.Solid;
        tc_det_vis1.BorderWidth = 1;
        tc_det_vis1.Controls.Add(lit_det_vis1);
        tc_det_vis1.Style.Add("text-align", "left");
        tc_det_vis1.Style.Add("font-family", "Calibri");
        tc_det_vis1.Style.Add("font-size", "10pt");
        tr_det_vis1.Cells.Add(tc_det_vis1);

        TableCell tc_det_2 = new TableCell();
        Literal lit_det_2 = new Literal();
        DataSet dsvis2 = new DataSet();
       // DCR dcV = new DCR();
        
        int cmon1 = Convert.ToInt32(FMonth);
        int cyr1 = Convert.ToInt32(FYear);
        dsvis2 = dcV.visit_cnt_2_dt(sf_code, div_code, cmon1, cyr1, dtCurrent1);
        if (dsvis2.Tables[0].Rows.Count > 0)
            lit_det_2.Text = "" + dsvis2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        tc_det_2.BorderStyle = BorderStyle.Solid;
        tc_det_2.BorderWidth = 1;
        tc_det_2.Height = 25;
        tc_det_2.Controls.Add(lit_det_2);
        tc_det_2.Style.Add("text-align", "left");
        tc_det_2.Style.Add("font-family", "Calibri");
        tc_det_2.Style.Add("font-size", "10pt");
        tr_det_vis1.Cells.Add(tc_det_2);

        TableRow tr_det_vis2 = new TableRow();
        TableCell tc_det_vis2 = new TableCell();
        Literal lit_det_vis2 = new Literal();
        lit_det_vis2.Text = "Three Visit drs Count";
        tc_det_vis2.BorderStyle = BorderStyle.Solid;
        tc_det_vis2.BorderWidth = 1;
        tc_det_vis2.Controls.Add(lit_det_vis2);
        tc_det_vis2.Style.Add("text-align", "left");
        tc_det_vis2.Style.Add("font-family", "Calibri");
        tc_det_vis2.Style.Add("font-size", "10pt");
        tr_det_vis2.Cells.Add(tc_det_vis2);

        TableCell tc_det_3 = new TableCell();
        Literal lit_det_3 = new Literal();
        DataSet dsvis3 = new DataSet();
        // DCR dcV = new DCR();

        int cmon2 = Convert.ToInt32(FMonth);
        int cyr2 = Convert.ToInt32(FYear);
        dsvis3 = dcV.visit_cnt_3_dt(sf_code, div_code, cmon2, cyr2, dtCurrent1);
        if (dsvis3.Tables[0].Rows.Count > 0)
            lit_det_3.Text = "" + dsvis3.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        tc_det_3.BorderStyle = BorderStyle.Solid;
        tc_det_3.BorderWidth = 1;
        tc_det_3.Height = 25;
        tc_det_3.Controls.Add(lit_det_3);
        tc_det_3.Style.Add("text-align", "left");
        tc_det_3.Style.Add("font-family", "Calibri");
        tc_det_3.Style.Add("font-size", "10pt");
        tr_det_vis2.Cells.Add(tc_det_3);

        TableRow tr_det_vis3 = new TableRow();
        TableCell tc_det_vis3 = new TableCell();
        Literal lit_det_vis3 = new Literal();
        lit_det_vis3.Text = "More than Three Visit drs Count";
        tc_det_vis3.BorderStyle = BorderStyle.Solid;
        tc_det_vis3.BorderWidth = 1;
        tc_det_vis3.Controls.Add(lit_det_vis3);
        tc_det_vis3.Style.Add("text-align", "left");
        tc_det_vis3.Style.Add("font-family", "Calibri");
        tc_det_vis3.Style.Add("font-size", "10pt");
        tr_det_vis3.Cells.Add(tc_det_vis3);

        TableCell tc_det_4 = new TableCell();
        Literal lit_det_4 = new Literal();
        DataSet dsvis4 = new DataSet();
        // DCR dcV = new DCR();

        int cmon3 = Convert.ToInt32(FMonth);
        int cyr3 = Convert.ToInt32(FYear);
        dsvis4 = dcV.visit_cnt_more_dt(sf_code, div_code, cmon3, cyr3, dtCurrent1);
        if (dsvis4.Tables[0].Rows.Count > 0)
            lit_det_4.Text = "" + dsvis4.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        tc_det_4.BorderStyle = BorderStyle.Solid;
        tc_det_4.BorderWidth = 1;
        tc_det_4.Height = 25;
        tc_det_4.Controls.Add(lit_det_4);
        tc_det_4.Style.Add("text-align", "left");
        tc_det_4.Style.Add("font-family", "Calibri");
        tc_det_4.Style.Add("font-size", "10pt");
        tr_det_vis3.Cells.Add(tc_det_4);

        tblhq.Rows.Add(tr_det_vis);
        tblhq.Rows.Add(tr_det_vis1);
        tblhq.Rows.Add(tr_det_vis2);
        tblhq.Rows.Add(tr_det_vis3);
    
    }
}