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
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MIS_Reports_rptTerrTypeWise_DrVisit : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsSales = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    string strDayName = string.Empty;
    int count = 0;
    string type = string.Empty;
    int iIndex;
    string test = string.Empty;
    int iCount;
    int intPreviousRowID = 0;

    int intSubTotalIndex = 1;


    double totdrs = 0;
    double totcore = 0;
    double noncore = 0;
    double supercore = 0;
    double totdrvisit = 0;
    double DrVstcore = 0;
    double DrVstnoncore = 0;
    double DrVstsupercore = 0;
    double MissedDr = 0;
    double MissedDrcore = 0;
    double MissedDrnoncore = 0;
    double MissedDrsupercore = 0;
    double dblSubTotalTotalRevenue = 0;
    int tot_terr = 0;
    string mode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["SF_code"].ToString();
        FMonth = Request.QueryString["cmon"].ToString();
        FYear = Request.QueryString["cyear"].ToString();
        sfname = Request.QueryString["SName"].ToString();
        mode = Request.QueryString["mode"];
        lblRegionName.Text = sfname;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);


        if (mode == "1")
        {
            lblHead.Text = "Territory wise - Listed Doctor Visit Details for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";
        }
        else if (mode == "2")
        {
            lblHead.Text = "Speciality wise - Listed Doctor Visit Details for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";
        }

        else if (mode == "3")
        {
            lblHead.Text = "Category wise - Listed Doctor Visit Details for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";
        }
        else if (mode == "4")
        {
            lblHead.Text = "Class wise - Listed Doctor Visit Details for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";
        }
        else if (mode == "5")
        {
            lblHead.Text = "Campaign wise - Listed Doctor Visit Details for the Month of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>";
        }


        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;


        type = sfCode;

        if (type.Contains("MR"))
        {
            type = "MR";
        }
        else if (type.Contains("MGR"))
        {
            type = "MGR";
        }

        FilldrCnt();
    }

    private void FilldrCnt()
    {

        SalesForce sal = new SalesForce();
        dsSalesForce = sal.getTerr_TypeDrCnt(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), type, mode);

        DataSet dsSale = sal.getTerr_TypeDrCnt_new(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), type);

        if (mode == "1")
        {

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdspec.Visible = false;
                grdTerr.Visible = true;
                grdTerr.DataSource = dsSalesForce;
                grdTerr.DataBind();
            }
            else
            {
                grdTerr.DataSource = dsSalesForce;
                grdTerr.DataBind();
            }
        }
        else
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdTerr.Visible = false;
                grdspec.Visible = true;
                grdspec.DataSource = dsSalesForce;
                grdspec.DataBind();
            }
            else
            {
                grdspec.DataSource = dsSalesForce;
                grdspec.DataBind();
            }
        }


        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    grdTerr.Visible = true;
        //    grdTerr.DataSource = dsSalesForce;
        //    grdTerr.DataBind();
        //}
        //else
        //{
        //    grdTerr.DataSource = dsSalesForce;
        //    grdTerr.DataBind();
        //}


        //foreach (DataRow Dr in dsSale.Tables[0].Rows)
        //{

        //    dsSalesForce.Tables[0].DefaultView.RowFilter = "  sf_code= '" + Dr["sf_code"].ToString() + "' ";
        //    DataTable dt = dsSalesForce.Tables[0].DefaultView.ToTable("table1");
        //    DataSet ds1 = new DataSet();
        //    DataSet dsMgr = new DataSet();
        //    dsSalesForce.Merge(dt);
        //    ds1.Merge(dt);
        //   // dsMgr.Merge(dt);



        //    Table tbl = new Table();
        //    tbl.Width = 100;
        //    tbl.Style.Add("align", "Center");
        //    tbl.Style.Add("margin-left", "300px");

        //    Table tblGrdHeader = new Table();
        //    tblGrdHeader.Width = 100;
        //    // tblGrd.Style.Add("Align", "Center");

        //    TableRow tr_tblGrdHeader = new TableRow();
        //    tr_tblGrdHeader.Width = 100;



        //    TableCell tc_tblGrdHeader = new TableCell();
        //    tc_tblGrdHeader.Width = 100;
        //    tc_tblGrdHeader.BorderStyle = BorderStyle.Solid;
        //    tc_tblGrdHeader.BorderWidth = 1;
        //    tc_tblGrdHeader.ColumnSpan = 1;
        //    tc_tblGrdHeader.HorizontalAlign = HorizontalAlign.Center;
        //    tc_tblGrdHeader.Width = 100;
        //    Literal lit_Header = new Literal();
        //    lit_Header.Text = Dr["sf_name"].ToString() + " - " + Dr["sf_designation_short_name"].ToString() + " - "  + Dr["sf_hq"].ToString();
        //    tc_tblGrdHeader.Controls.Add(lit_Header);
        //    tr_tblGrdHeader.Cells.Add(tc_tblGrdHeader);


        //    tbl.Rows.Add(tr_tblGrdHeader);

        //    pnlbutton.Controls.Add(tbl);


        //    Table tblGrd = new Table();
        //    tblGrd.Width = 100;
        //     tblGrd.Style.Add("align", "Center");

        //    TableRow tr_tblGrd = new TableRow();


        //    TableCell tc_tblGrd = new TableCell();
        //    tc_tblGrd.Width = 100;




        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {


        //        TableRow trGrdLst = new TableRow();
        //        trGrdLst.BorderStyle = BorderStyle.Solid;
        //        trGrdLst.BorderWidth = 1;
        //        trGrdLst.Width = 100;

        //        TableCell tc_GrdLst = new TableCell();
        //        tc_GrdLst.BorderStyle = BorderStyle.Solid;
        //        tc_GrdLst.BorderWidth = 1;



        //        tc_GrdLst.HorizontalAlign = HorizontalAlign.Left;
        //        tc_GrdLst.Width = 100;

        //        //trGrdLst.Cells.Add(tc_Terr);

        //        // lit_Terr.Text = "<span style='margin-left:20px'><b> Territory Worked </b></span>" + "<span style='margin-left:15px'></span>:" + "<span style='margin-left:21px;text-align:right'>" + ds1.Tables[0].Rows[0]["che_POB_Name"].ToString() + "</span>";


               
        //        ds1.Tables[0].Columns.RemoveAt(11);
        //        ds1.Tables[0].Columns.RemoveAt(10);
        //        ds1.Tables[0].Columns.RemoveAt(9);
        //        ds1.Tables[0].Columns.RemoveAt(3);
        //        ds1.Tables[0].Columns.RemoveAt(1);                
        //        ds1.Tables[0].Columns.RemoveAt(0);
               
                

        //        GridView grd = new GridView();

        //        grd.Width = 100;
        //        //grd.AutoGenerateColumns=false;
        //        grd.ID = "GridView" + iCount.ToString();
        //        grd.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#336277");
        //        grd.HeaderStyle.Height = 20;
        //        grd.HeaderStyle.ForeColor = System.Drawing.Color.White;
        //        grd.CellPadding = 10;
        //        grd.CellSpacing = 10;
        //        grd.Style.Add("align", "center");
        //        grd.DataSource = ds1; // some data source
        //        grd.DataBind();
        //        tc_GrdLst.Controls.Add(grd);
        //        trGrdLst.Cells.Add(tc_GrdLst);
        //        //PnlLst.Controls.Add(trGrdLst);

        //        tbl.Rows.Add(trGrdLst);
        //        tc_tblGrd.Controls.Add(tbl);
        //        tr_tblGrd.Cells.Add(tc_tblGrd);
        //        tblGrd.Rows.Add(tr_tblGrd);
        //        pnlbutton.Controls.Add(tblGrd);
        //    }
        //}


    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
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
    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);
    //}

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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void grdTerr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int index = e.Row.RowIndex;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsf_name = (Label)e.Row.FindControl("lblsf_name");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lbldesig = (Label)e.Row.FindControl("lbldesig");
            Label lblsf_hq = (Label)e.Row.FindControl("lblsf_hq");
            

            if (test == lblsf_name.Text && index != 0)
            {
                lblsf_name.Text = "";
                lblSNo.Text = "";
                lbldesig.Text = "";
                lblsf_hq.Text = "";
            }
            else
            {
                count += 1;
                lblSNo.Text = Convert.ToString(count);
                test = lblsf_name.Text;
                //lblsf_name.Text = test;
            }


        }
        if (mode == "1")
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                intPreviousRowID = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id").ToString());
                double Total_Dr = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total_Dr").ToString());
                double Core_dr = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Core_dr").ToString());
                double nonCore_dr = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "non_coredr").ToString());
                double super_coredr = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "super_coredr").ToString());
                double Dr_Visited = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Dr_Visited").ToString());
                double Dr_Vst_core = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Dr_Vst_core").ToString());
                double Dr_Vst_noncore = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Dr_Vst_noncore").ToString());
                double Dr_Vst_supercore = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Dr_Vst_supercore").ToString());

                double Missed_Dr = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Missed_Dr").ToString());
                double Missed_Dr_core = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Missed_Dr_core").ToString());
                double Missed_Dr_noncore = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Missed_Dr_noncore").ToString());
                double Missed_Dr_supercore = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Missed_Dr_supercore").ToString());


                tot_terr += 1;

                totdrs += Total_Dr;
                totcore += Core_dr;
                noncore += nonCore_dr;
                supercore += super_coredr;
                totdrvisit += Dr_Visited;
                DrVstcore += Dr_Vst_core;
                DrVstnoncore += Dr_Vst_noncore;
                DrVstsupercore += Dr_Vst_supercore;
                MissedDr += Missed_Dr;
                MissedDrcore += Missed_Dr_core;
                MissedDrnoncore += Missed_Dr_noncore;
                MissedDrsupercore += Missed_Dr_supercore;
                // dblSubTotalTotalRevenue += (Total_Dr + Core_dr);
            }
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (mode == "2")
            {
                e.Row.Cells[4].Text = "Speciality";
            }
            else if (mode == "3")
            {
                e.Row.Cells[4].Text = "Category";
            }
            else if (mode == "4")
            {
                e.Row.Cells[4].Text = "Class";
            }
            else if (mode == "5")
            {
                e.Row.Cells[4].Text = "Campaign";
            }
        }
    }

    protected void grdTerr_onrowCreated(object sender, GridViewRowEventArgs e)
    {
        bool IsTotalRowNeedToAdd = false;
        if ((intPreviousRowID > 0) && (DataBinder.Eval(e.Row.DataItem, "id") != null))
            if (intPreviousRowID != Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "id").ToString()))
                IsTotalRowNeedToAdd = true;

        if ((intPreviousRowID > 0) && (DataBinder.Eval(e.Row.DataItem, "id") == null))
        {
            IsTotalRowNeedToAdd = true;
            intSubTotalIndex = 0;
        }

        if (IsTotalRowNeedToAdd)
        {
            GridView grdViewProducts = (GridView)sender;

            // Creating a Row
            GridViewRow SubTotalRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

            //Adding Total Cell 
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Summary";
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.ColumnSpan = 4; // For merging first, second row cells to one
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = tot_terr.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            HeaderCell.Style.Add("background-color", "#81BEF7");
            SubTotalRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            HeaderCell.Style.Add("background-color", "#81BEF7");
            SubTotalRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = totdrs.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            HeaderCell.Style.Add("background-color", "#81BEF7");
            SubTotalRow.Cells.Add(HeaderCell);



            //Adding Referral Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = totcore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            //Adding Referral Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = noncore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            //Adding Referral Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = supercore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);



            //Adding Referral Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = totdrvisit.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);



            //Adding Referral Revenue Column
            HeaderCell = new TableCell();
            HeaderCell.Text = DrVstcore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            // 

            HeaderCell = new TableCell();
            HeaderCell.Text = DrVstnoncore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);


            //

            HeaderCell = new TableCell();
            HeaderCell.Text = DrVstsupercore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = MissedDr.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = MissedDrcore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = MissedDrnoncore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = MissedDrsupercore.ToString();
            HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            HeaderCell.CssClass = "SubTotalRowStyle";
            SubTotalRow.Cells.Add(HeaderCell);

            ////Adding Total Revenue Column
            //HeaderCell = new TableCell();
            //HeaderCell.Text = string.Format("{0:0.00}", dblSubTotalTotalRevenue);
            //HeaderCell.HorizontalAlign = HorizontalAlign.Right;
            //HeaderCell.CssClass = "SubTotalRowStyle";
            //SubTotalRow.Cells.Add(HeaderCell);

            //Adding the Row at the RowIndex position in the Grid
            grdViewProducts.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, SubTotalRow);
            intSubTotalIndex++;
            totdrs = 0;
            totcore = 0;
            noncore = 0;
            supercore = 0;
            totdrvisit = 0;
            DrVstcore = 0;
            DrVstnoncore = 0;
            DrVstsupercore = 0;
            MissedDr = 0;
            MissedDrcore = 0;
            MissedDrnoncore = 0;
            MissedDrsupercore = 0;
            dblSubTotalTotalRevenue = 0;
            tot_terr = 0;
        }
    }
       
}
