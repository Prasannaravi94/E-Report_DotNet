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
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rptManagerCovrg_Analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
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
    DataSet dsmgrsf = new DataSet();
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;
    string MultiSf_Code = string.Empty;
    DataSet dsprd = new DataSet();
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string tot = string.Empty;
    int Total;

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Manager wise Coverage Analysis for the Period of " + "<span style='color:#0077FF'>" + strFMonthName + " " + FYear + "</span>" + " " + "To" + " " + "<span style='color:#0077FF'>" + strTMonthName + " " + TYear +"</span>";


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF();
        FillSample_Prd();
    }
    private void FillSample_Prd()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
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

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("MGR_Covg_Analysis", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 150;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
        GrdInput.DataSource = dsts;
        GrdInput.DataBind();

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

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void GrdInput_RowCreated(object sender, GridViewRowEventArgs e)
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
            GridViewRow objgridviewrow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();

            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
          

            int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                iLstMonth.Add(cmonth);
                iLstYear.Add(cyear);
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, 4, sTxt, "#0097AC", true);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "EX", "#0097AC", true);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "OS", "#0097AC", true);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "OS-EX", "#0097AC", true);
                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Total", "#0097AC", true);
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            //
            #endregion
            //
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        if (rowSpan == 0 && colspan == 0)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdInput_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;
            for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
            {
                //if (e.Row.Cells[i].Text != "0")
                //{
                HyperLink hLink = new HyperLink();
                hLink.Text = e.Row.Cells[i].Text;
                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                //int cMnth = iLstMonth[j];
                //int cYr = iLstYear[j];

                //if (cMnth == 12)
                //{
                //    sCurrentDate = "01-01-" + (cYr + 1).ToString();
                //}
                //else
                //{
                //    sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                //}

                //if (dtrowClr.Rows[iInx][1].ToString() != "Total")
                //{
                //    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                //}
                //else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                //{
                //    hLink.Attributes.Add("href", "javascript:showModal('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "', '" + cMnth + "','" + sCurrentDate + "', '" + cMnth + "')");
                //    Session["Sf_Code_multiple"] = sfCode;
                //}


                //hLink.ToolTip = "Click here";
                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                {
                    hLink.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    hLink.ForeColor = System.Drawing.Color.Blue;
                }


                e.Row.Cells[i].Controls.Add(hLink);
                tot = hLink.Text;
                if (tot != "-" && tot != "&nbsp;")
                {
                    Total +=Convert.ToInt16(tot);
                }

                int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                j++;
                //}

                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");

            }

            if (dtrowClr.Rows[iInx][1].ToString() == "Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true;font-size:14px;Color:Red;border-color:Black");
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Attributes.Add("align", "right");
            }

            TableCell tott = new TableCell();
            tott.Text = Total.ToString();

            if (tott.Text == "0")
            {
                tott.Text = "";
            }
            e.Row.Cells.Add(tott);
            tott.Attributes.Add("align", "right");
            Total = 0;

            //Add S By Rp
            int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cellcount = ((months + 1) * 4) + 4;

            if (dtrowClr.Rows.Count - 1 != iInx)
            {
                if (e.Row.Cells[cellcount].Text != "&nbsp;" && e.Row.Cells[cellcount].Text != "0" && e.Row.Cells[cellcount].Text != "-")
                {
                    HyperLink hLink1 = new HyperLink();    // Request.QueryString["sf_name"].ToString();
                    hLink1.Text = e.Row.Cells[cellcount].Text;
                    hLink1.Attributes.Add("class", "btnDrSn");
                    hLink1.Attributes.Add("onclick", "javascript:showModalPopUp('" + sfCode + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[iInx][1].ToString()) + "')");
                    hLink1.ToolTip = "Click here";
                    hLink1.Font.Underline = true;
                    hLink1.Attributes.Add("style", "cursor:pointer");
                    hLink1.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[cellcount].Controls.Add(hLink1);
                }
            }
            e.Row.Cells[cellcount].Attributes.Add("align", "center");

            //e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            //Add E By Rp
        }
    }
}

   

