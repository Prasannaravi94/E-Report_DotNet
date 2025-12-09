using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;

using System.Web.UI.WebControls.WebParts;
using System.Windows;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MIS_Reports_rpt_gift_issued : System.Web.UI.Page
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
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
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
    DataTable dtrowClr1 = new System.Data.DataTable();
    DataTable dtrowClr = new System.Data.DataTable();
    string sCurrentDate = string.Empty;
    string strQry = string.Empty;



    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Gift Issued Region wise for the Month of  " + strFMonthName + " " + FYear;

        strQry = "select sf_hq + '(' + sf_name + ')' sf from Mas_Salesforce where sf_code = '" + sfCode + "' ";

        dsSalesForce = db_ER.Exec_DataSet(strQry);

        LblForceName.Text = dsSalesForce.Tables[0].Rows[0]["sf"].ToString();

        fillreport();

    }

    private void fillreport()
    {
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        //sProc_Name = "gift_issued_regionwise";
        sProc_Name = "gift_issued_regionwise_New";//by Vasanthi.P on 2-Sep-24
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@divcode", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@sfcode", sfCode);

        cmd.Parameters.AddWithValue("@month", cmonth);
        cmd.Parameters.AddWithValue("@year", cyear);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr1 = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.RemoveAt(4);
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(1);
        //dsts.Tables[0].Columns.RemoveAt(0);

        dtrowClr = dsts.Tables[0].Copy();

        DataRow drRow = dtrowClr.NewRow();
        dtrowClr.Rows.Add(drRow);

        GrdFixation.DataSource = dtrowClr; //dsts.Tables[0];

        GrdFixation.DataBind();



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

            AddMergedCells(objgridviewrow, objtablecell, 2, 1, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 1, "Gift Name", "#0097AC", true);


            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);


            dsprd = sf.salesforce_name_hq_header(divcode, sfCode);



            TableCell objtablecell2 = new TableCell();
            TableCell objtablecell3 = new TableCell();

            if (dsprd.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < dsprd.Tables[0].Rows.Count; k++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, 4, dsprd.Tables[0].Rows[k]["sf"].ToString(), "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Opening", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Despatch", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Issued", "0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Closing", "0097AC", false);
                }
            }




            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowspan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowspan;

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        int k = e.Row.RowIndex;

        if (k == dtrowClr.Rows.Count - 1)
        {
            e.Row.Cells[1].Text = "Total";
            e.Row.Cells[1].Attributes.Add("style", "color:red;font-weight:bold;");
        }
        //if(k==2)
        //e.Row.Cells[2].Text = "sujee";

        //Grand Total Calculation 
        for (int i = 2; i <= e.Row.Cells.Count - 1; i++)
        {
            if (e.Row.Cells[i].Text == "0")
            {
                e.Row.Cells[i].Text = "";
            }
        }
        if (k > -1)
        {
            for (int l = 2, j = 0; l <= e.Row.Cells.Count - 1; l++)
            {
                if (e.Row.RowIndex == dtrowClr.Rows.Count - 1)
                {
                    e.Row.Cells[l].Text = dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString();
                    e.Row.Cells[l].Attributes.Add("style", "color:red;font-weight:bold;");


                }

                int appl = (e.Row.Cells[l].Text == null) || (e.Row.Cells[l].Text == "") || (e.Row.Cells[l].Text == "&nbsp;") ? 0 : Convert.ToInt16(e.Row.Cells[l].Text);
                int pApplT = (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] == null) || (dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString() == "") ? 0 : Convert.ToInt16(dtrowClr.Rows[dtrowClr.Rows.Count - 1][l].ToString());
                dtrowClr.Rows[dtrowClr.Rows.Count - 1][l] = (pApplT + appl).ToString();


                j++;
            }
            //if (k < dtrowClr.Rows.Count - 1)
            //{
            //    for (int l = 4, j = 0; l <= e.Row.Cells.Count - 1; l += 4)
            //    {
            //        HyperLink issued = new HyperLink();
            //        if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != null && e.Row.Cells[l].Text != "&nbsp;")
            //        {
            //            issued = new HyperLink();
            //            issued.Text = e.Row.Cells[l].Text;
            //            //issued.Attributes.Add("style", "color:red;font-weight:bold;");
            //            issued.Attributes.Add("href", "javascript:showModalPopUp('" + dsprd.Tables[0].Rows[j]["sf_code"].ToString() + "','" + dsprd.Tables[0].Rows[j]["sf_name"].ToString() + "', '" + FYear + "', '" + FMonth + "','" + dtrowClr1.Rows[k]["Gift_Code"].ToString() + "')");
            //            issued.Font.Underline = true;
            //            issued.ForeColor = System.Drawing.Color.Black;
            //            e.Row.Cells[l].Controls.Add(issued);

            //        }
            //        j++;
            //    }
            //}
        }
       
        e.Row.Cells[0].Wrap = false;
        e.Row.Cells[1].Wrap = false;






    }

}