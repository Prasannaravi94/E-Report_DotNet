using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Windows;
using System.IO;
using System.Drawing;


public partial class MasterFiles_Report_rpt_TP_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DataSet dsTourPlan = null;
    DataSet dsTourPlanReport = null;
    DataSet dstpstatus = null;
    string sfCode = string.Empty;
    string sfname = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    int sftype = -1;
    string sTerr = string.Empty;
    DateTime dt_TP_Active_Date;
    string[] sWork;
    int iIndex = -1;
    int iLevel = -1;
    string sLevel = string.Empty;
    string strTPView = string.Empty;
    DateTime dtTourDate;
    string sTerritory = string.Empty;
    DataSet dsWorkType = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            sfCode = Request.QueryString["sf_code"].ToString();
            sfname = Request.QueryString["sf_name"].ToString();
            iMonth = Convert.ToInt32(Request.QueryString["cur_month"].ToString());
            iYear = Convert.ToInt32(Request.QueryString["cur_year"].ToString());
            iLevel = Convert.ToInt32(Request.QueryString["level"].ToString());
            //div_code = Request.QueryString["div_Code"].ToString();           
            string sMonth = getMonthName(iMonth) + " " + iYear.ToString();
            lblHead.Text = lblHead.Text + " For " + sfname + " for the Month Of - " + sMonth;

            FillSF(sfCode);
            //FillTourPlan1();

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_ApprovalTitle(sfCode);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                //lblHead.Text = lblHead.Text + " for " + sfname + " for the Month Of - " + sMonth;
                //+ dsTP.Tables[0].Rows[0]["Sf_Joining_Date"];

                //lblHq.Text = "HQ - " + dsTP.Tables[0].Rows[0]["Sf_HQ"].ToString();
            }
            FillTourPlan();
            TourPlan();
            if (Session["sf_type"].ToString() == "2")
            {
                summ.Visible = false;
            }
            else
            {
                FillSummary();
                summ.Visible = true;

            }


        }
        if (Session["sf_type"].ToString() == "1" || Session["sf_type"].ToString() == "")
        {
            grdTP.Columns[3].Visible = false;

        }
        else
        {
            if (sfCode.StartsWith("MR"))
            {
                grdTP.Columns[3].Visible = false;
            }

        }
    }


       
    protected void TourPlan()
    {
        try
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                strTPView = dsTerritory.Tables[0].Rows[0]["No_of_TP_View"].ToString();
                if (strTPView == "3")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = true;
                    grdTP.Columns[8].Visible = true;

                }
                else if (strTPView == "2")
                {

                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = true;
                    grdTP.Columns[6].Visible = true;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "1")
                {

                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;
                }
                else if (strTPView == "0" || strTPView == "")
                {
                    grdTP.Columns[3].Visible = true;
                    grdTP.Columns[4].Visible = true;
                    grdTP.Columns[5].Visible = false;
                    grdTP.Columns[6].Visible = false;
                    grdTP.Columns[7].Visible = false;
                    grdTP.Columns[8].Visible = false;

                }
            }
            else
            {
                grdTP.Columns[6].Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }


    private void FillSF(string sf_code)
    {
        SalesForce sf = new SalesForce();
        dsTP = sf.getSalesForce(sf_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            sftype = Convert.ToInt32(dsTP.Tables[0].Rows[0].ItemArray.GetValue(28).ToString());
        }
    }

    private void FillTourPlan1()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.get_TP_Status(sfCode, iMonth, iYear);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            tblStatus.Visible = true;
        }
    }

  
    private void FillTourPlan()
    {
        if (iLevel == -1)
        {
            if ((iMonth > 0) && (iYear > 0))
            {
                TourPlan tp = new TourPlan();
                dsTP = tp.get_TP_Draft_Reject(sfCode, iMonth, iYear);

                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    dsTP = tp.get_TP_Entry(sfCode, iMonth, iYear, div_code);


                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        lblFieldForce.Visible = true;
                        lblValHQ.Visible = true;
                        lblDesgn.Visible = true;
                        lblFieldForceValue.Text = ": " + sfname;
                        lbltpfieldforcenames.Text = "(" + dsTP.Tables[0].Rows[0]["TP_Sf_Name"].ToString() + ")";
                        lblHQValue.Text = ": " + dsTP.Tables[0].Rows[0]["Sf_HQ"].ToString();
                        dstpstatus = tp.get_TP_DorR(sfCode, iMonth, iYear, div_code);
                        string status = dstpstatus.Tables[0].Rows[0]["Change_Status"].ToString();
                        if(status=="2")
                        {
                            lblstatusdetail.Text = "Rejected(Reject by Manager)";
                            lblstatusdetail.ForeColor = Color.Red;
                        }
                        else if (status == "0")
                        {
                            lblstatusdetail.Text = "Draft(Not in Manager Approval)";
                            lblstatusdetail.ForeColor = Color.Blue;
                        }
                        else
                        {
                            //lblstatusdetail.Text = "Completed/Confirmed";
                            //lblstatusdetail.ForeColor = Color.Maroon;
                        }
                        lblCompleted.Visible = false;
                        lblConfirmed.Visible = false;
                        //lblConfirmedValue.Text = ": " + dsTP.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                        //lblCompletedValue.Text = ": " + dsTP.Tables[0].Rows[0]["Submission_date"].ToString();
                        lblDesgnValue.Text = ":" + dsTP.Tables[0].Rows[0]["Designation_Short_Name"].ToString();
                    }
                    else
                    {
                        lblFieldForce.Visible = false;
                        lblValHQ.Visible = false;
                        lblConfirmed.Visible = false;
                        lblCompleted.Visible = false;
                        lblDesgn.Visible = false;
                    }
                    //else
                    //{
                    dsTP = tp.get_TP_EntryforMGR(sfCode, iMonth, iYear);
                    //}


                    if (dsTP.Tables[0].Rows.Count > 0)
                    {
                        grdTP.Visible = true;
                        grdTP.DataSource = dsTP;
                        grdTP.DataBind();
                    }
                    else
                    {
                        grdTP.DataSource = dsTP;
                        grdTP.DataBind();
                    }

                }
                else
                {
                   
                        dsTP = tp.get_TP_Entry(sfCode, iMonth, iYear, div_code);


                        if (dsTP.Tables[0].Rows.Count > 0)
                        {
                            lblFieldForce.Visible = true;
                            lblValHQ.Visible = true;
                            lblConfirmed.Visible = true;
                            lblCompleted.Visible = true;
                            lblDesgn.Visible = true;
                            lblFieldForceValue.Text = ": " + sfname;
                            lbltpfieldforcenames.Text = "(" + dsTP.Tables[0].Rows[0]["TP_Sf_Name"].ToString() + ")";
                            lblHQValue.Text = ": " + dsTP.Tables[0].Rows[0]["Sf_HQ"].ToString();
                            dstpstatus = tp.get_TP_DorR(sfCode, iMonth, iYear, div_code);
                            if(dsTP.Tables[0].Rows[0]["Confirmed_Date"].ToString()=="")
                            {
                            lblstatusdetail.Text = "Completed(Approval Pending)";
                            lblstatusdetail.ForeColor = Color.Magenta;
                            }

                        if ((dsTP.Tables[0].Rows[0]["Submission_date"].ToString() != "") && (dsTP.Tables[0].Rows[0]["Confirmed_Date"].ToString() != ""))
                        {
                            lblstatusdetail.Text = "Approved";
                            lblstatusdetail.ForeColor = Color.DarkGreen;
                        }
                        

                        lblConfirmedValue.Text = ": " + dsTP.Tables[0].Rows[0]["Confirmed_Date"].ToString();
                            lblCompletedValue.Text = ": " + dsTP.Tables[0].Rows[0]["Submission_date"].ToString();
                            lblDesgnValue.Text = ": " + dsTP.Tables[0].Rows[0]["Designation_Short_Name"].ToString();
                        }
                        else
                        {
                            lblFieldForce.Visible = false;
                            lblValHQ.Visible = false;
                            lblConfirmed.Visible = false;
                            lblCompleted.Visible = false;
                            lblDesgn.Visible = false;
                        }
                        //else
                        //{
                        dsTP = tp.get_TP_EntryforMGR(sfCode, iMonth, iYear);
                        //}


                        if (dsTP.Tables[0].Rows.Count > 0)
                        {
                            grdTP.Visible = true;
                            grdTP.DataSource = dsTP;
                            grdTP.DataBind();
                        }
                        else
                        {
                            grdTP.DataSource = dsTP;
                            grdTP.DataBind();
                        }

                    
                }
            }
        }
            else
            {
                FillSalesForce();
            }

                
                 
    }
    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[4].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[6].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                e.Row.Cells[8].Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
        }
    }    

    private void FillSalesForce()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.get_UserList_TP_Report_Level(Session["div_code"].ToString(), sfCode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dataRow in dsTP.Tables[0].Rows)
            {
                sLevel = dataRow["cnt_level"].ToString();
            }
        }

        if (sLevel == "1" && iLevel == 4)
        {
            //Rep
            dsTourPlan = tp.get_UserList_TP_Report(Session["div_code"].ToString(), sfCode);
            //ViewState["dsTourPlan"] = dsTourPlan;
        }
        else if (sLevel == "2" && ((iLevel == 3) || (iLevel == 4)))
        {
            //AM & Rep
        }
        else if (sLevel == "3" && ((iLevel == 2) || (iLevel == 3) || (iLevel == 4)))
        {
            //RM, AM & Rep
        }
        else if (sLevel == "4" && ((iLevel == 1) || (iLevel == 2) || (iLevel == 3) || (iLevel == 4)))
        {
            //ZM, RM, AM & Rep
        }

        // Fetch the total columns for the table
        //Doctor dr = new Doctor();
        //if (iLevel == 1)
        //{
        //    dsDoctor = dr.getDocCat(Session["div_code"].ToString());
        //    if (dsDoctor.Tables[0].Rows.Count > 0)
        //    {
        //        tot_cols = dsDoctor.Tables[0].Rows.Count;
        //        ViewState["dsDoctor"] = dsDoctor;
        //    }
        //}
        CreateDynamicTable(dsTourPlan);
    }

    private void CreateDynamicTable(DataSet dsTourPlan)
    {

        //dsDoctor = (DataSet)ViewState["dsDoctor"];
        //TableRow tr_catg = new TableRow();
        ////tr_catg.BackColor = System.Drawing.Color.Pink;

        ////if (type == "0")
        ////{
        ////    dsDoctor = (DataSet)ViewState["dsDoctor"];
        ////}        
        int i = 0;
        if (dsTourPlan.Tables[0].Rows.Count > 0)
        {
            TableRow tr_sf = new TableRow();
            foreach (DataRow dataRow in dsTourPlan.Tables[0].Rows)
            {
                TourPlan tp_report = new TourPlan();
                dsTourPlanReport = tp_report.get_TP_Detail(dataRow["sf_code"].ToString(), iMonth, iYear);
                if (dsTourPlanReport.Tables[0].Rows.Count > 0)
                {
                    tbl.BorderStyle = BorderStyle.None;
                    tbl.CellPadding = 5;
                    tbl.CellSpacing = 5;

                    //Create Dynamic Table
                    TableCell tc_sf = new TableCell();
                    //tc_sf.BorderStyle = BorderStyle.None;
                    tc_sf.VerticalAlign = VerticalAlign.Top;
                    Table tbl_sf = new Table();
                    tbl_sf.CellPadding = 0;
                    tbl_sf.CellSpacing = 0;

                    TableRow tr_tp_sf = new TableRow();
                    TableCell tc_tp_sf = new TableCell();
                    tc_tp_sf.BorderStyle = BorderStyle.Solid;
                    tc_tp_sf.BorderWidth = 1;
                    tc_tp_sf.ColumnSpan = 4;
                    //tc_tp_sf.Width = 10;

                    Literal lit_tp_sf = new Literal();
                    lit_tp_sf.Text = "<center><b>" + dataRow["sf_name"].ToString() + "</b></center>";
                    tc_tp_sf.Controls.Add(lit_tp_sf);
                    tr_tp_sf.Cells.Add(tc_tp_sf);

                    tbl_sf.Rows.Add(tr_tp_sf);

                    TableRow tr_tp_header = new TableRow();
                    //tr_tp_header
                    TableCell tc_tp_date_header = new TableCell();
                    tc_tp_date_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_date_header.BorderWidth = 1;
                    tc_tp_date_header.Width = 10;

                    Literal lit_tp_date_header = new Literal();
                    lit_tp_date_header.Text = "<center><b>Tour Date</b></center>";
                    tc_tp_date_header.Controls.Add(lit_tp_date_header);
                    tr_tp_header.Cells.Add(tc_tp_date_header);

                    TableCell tc_tp_worktype_header = new TableCell();
                    tc_tp_worktype_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_worktype_header.BorderWidth = 1;
                    tc_tp_worktype_header.Width = 150;
                    Literal lit_tp_worktype_header = new Literal();
                    lit_tp_worktype_header.Text = "<center><b>Work Type</b></center>";
                    tc_tp_worktype_header.Controls.Add(lit_tp_worktype_header);
                    tr_tp_header.Cells.Add(tc_tp_worktype_header);

                    TableCell tc_tp_Terr_header = new TableCell();
                    tc_tp_Terr_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_Terr_header.BorderWidth = 1;
                    tc_tp_Terr_header.Width = 300;
                    Literal lit_tp_Terr_header = new Literal();
                    lit_tp_Terr_header.Text = "<center><b>Territory Planned</b></center>";
                    tc_tp_Terr_header.Controls.Add(lit_tp_Terr_header);
                    tr_tp_header.Cells.Add(tc_tp_Terr_header);

                    TableCell tc_tp_obj_header = new TableCell();
                    tc_tp_obj_header.BorderStyle = BorderStyle.Solid;
                    tc_tp_obj_header.BorderWidth = 1;
                    Literal lit_tp_obj_header = new Literal();
                    lit_tp_obj_header.Text = "<center><b>Objective</b></center>";
                    tc_tp_obj_header.Controls.Add(lit_tp_obj_header);
                    tr_tp_header.Cells.Add(tc_tp_obj_header);

                    tbl_sf.Rows.Add(tr_tp_header);

                    foreach (DataRow dr in dsTourPlanReport.Tables[0].Rows)
                    {
                        TableRow tr_tp = new TableRow();

                        TableCell tc_tp_date = new TableCell();
                        tc_tp_date.BorderStyle = BorderStyle.Solid;
                        tc_tp_date.BorderWidth = 1;
                        Literal lit_tp_date = new Literal();
                        dtTourDate = Convert.ToDateTime(dr["tour_date"].ToString());
                        lit_tp_date.Text = dtTourDate.ToString("MM-dd-yyyy");
                        tc_tp_date.Controls.Add(lit_tp_date);
                        tr_tp.Cells.Add(tc_tp_date);

                        TableCell tc_tp_worktype = new TableCell();
                        tc_tp_worktype.BorderStyle = BorderStyle.Solid;
                        tc_tp_worktype.BorderWidth = 1;
                        Literal lit_tp_worktype = new Literal();
                        lit_tp_worktype.Text = dr["WorkType"].ToString();

                        if (dr["WorkType"].ToString().Length > 0)
                        {
                            TourPlan trp = new TourPlan();
                            dsWorkType = trp.FetchWorkType(dr["WorkType"].ToString());
                            if (dsWorkType.Tables[0].Rows.Count > 0)
                            {
                                lit_tp_worktype.Text = Convert.ToString(dsWorkType.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            }
                        }

                        tc_tp_worktype.Controls.Add(lit_tp_worktype);
                        tr_tp.Cells.Add(tc_tp_worktype);

                        //----Territory-----
                        TourPlan tp = new TourPlan();
                        if (iLevel == 4)
                        {
                            dsTP = tp.FetchTerritory(dataRow["sf_code"].ToString(), dr["Worked_With_SF_Code"].ToString());
                            if (dsTP.Tables[0].Rows.Count > 0)
                            {
                                sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            }
                            else
                            {
                                dsTP = tp.FetchWorkType(dr["Worked_With_SF_Code"].ToString());
                                if (dsTP.Tables[0].Rows.Count > 0)
                                {
                                    sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                }
                            }

                        }
                        else
                        {
                            sTerr = "";
                            string sTerrMgr = dr["Worked_With_SF_Code"].ToString();
                            sWork = sTerrMgr.Split(',');
                            foreach (string sw in sWork)
                            {
                                if (sw.Trim().Length > 0)
                                {
                                    dsTP = tp.FetchTerritory_MGR(sw);
                                    if (dsTP.Tables[0].Rows.Count > 0)
                                    {
                                        foreach (DataRow dRow in dsTP.Tables[0].Rows)
                                        {
                                            if (sTerr.Trim().Length > 0)
                                            {
                                                sTerr = dRow["Territory_Name"].ToString();
                                            }
                                            else
                                            {
                                                sTerr = sTerr + "," + dRow["Territory_Name"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dsTP = tp.FetchWorkType(dr["Worked_With_SF_Code"].ToString());
                                        if (dsTP.Tables[0].Rows.Count > 0)
                                        {
                                            sTerritory = Convert.ToString(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                                        }
                                    }
                                }

                            }
                            sTerritory = sTerr;
                        }


                        //----Territory-----
                        TableCell tc_tp_Terr = new TableCell();
                        tc_tp_Terr.BorderStyle = BorderStyle.Solid;
                        tc_tp_Terr.BorderWidth = 1;
                        Literal lit_tp_Terr = new Literal();
                        lit_tp_Terr.Text = "&nbsp;" + sTerritory;
                        tc_tp_Terr.Controls.Add(lit_tp_Terr);
                        tr_tp.Cells.Add(tc_tp_Terr);

                        TableCell tc_tp_obj = new TableCell();
                        tc_tp_obj.BorderStyle = BorderStyle.Solid;
                        tc_tp_obj.BorderWidth = 1;
                        Literal lit_tp_obj = new Literal();
                        lit_tp_obj.Text = dr["objective"].ToString();
                        tc_tp_obj.Controls.Add(lit_tp_obj);
                        tr_tp.Cells.Add(tc_tp_obj);

                        tbl_sf.Rows.Add(tr_tp);
                    }

                    tc_sf.Controls.Add(tbl_sf);
                    tr_sf.Cells.Add(tc_sf);

                }
                tbl.Rows.Add(tr_sf);
            }
        }

        
    }
    

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "Jan";
        }
        else if (iMonth == 2)
        {
            sReturn = "Feb";
        }
        else if (iMonth == 3)
        {
            sReturn = "Mar";
        }
        else if (iMonth == 4)
        {
            sReturn = "Apr";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "Aug";
        }
        else if (iMonth == 9)
        {
            sReturn = "Sep";
        }
        else if (iMonth == 10)
        {
            sReturn = "Oct";
        }
        else if (iMonth == 11)
        {
            sReturn = "Nov";
        }
        else if (iMonth == 12)
        {
            sReturn = "Dec";
        }
        return sReturn;
    }

    private void FillSummary()
    {
        DataSet dsSumm = null;
        string strQry = string.Empty;

        DB_EReporting db_ER = new DB_EReporting();
        if(sfCode.Contains("MR"))
        { strQry = "EXEC TPView_Summary  '" + div_code + "','" + sfCode + "','" + iMonth + "','" + iYear + "'";}
        else { strQry = "EXEC TPView_Summary_test  '" + div_code + "','" + sfCode + "','" + iMonth + "','" + iYear + "'"; }
        dsSumm = db_ER.Exec_DataSet(strQry);

        if (dsSumm.Tables[0].Rows.Count > 0)
        {
            grdSummary.DataSource = dsSumm;
            grdSummary.DataBind();
        }
        else
        {
            grdSummary.DataSource = dsSumm;
            grdSummary.DataBind();
        }


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
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string attachment = "attachment; filename=Export.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);

    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }


}