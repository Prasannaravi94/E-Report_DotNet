//
#region Assembly
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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
#endregion
//
//
public partial class MasterFiles_AnalysisReports_Coverage_New : System.Web.UI.Page
{
    //
    #region Variables
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
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    List<int> iLstMnth = new List<int>();
    List<string> sLstMnth = new List<string>();
    List<int> iLstYr = new List<int>();
    string sMr_Code, sSpeciality;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {       
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["Tyear"].ToString();
        sSpeciality = Request.QueryString["cbVal"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);        
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillSalesForce();
    }
    
    #endregion
    //
    #region FillSalesForce
    private void FillSalesForce()
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
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("visit_details_DrLst", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
        //
        GrdDoctor.DataSource = dsts;
        GrdDoctor.DataBind();
        //
    }
    #endregion    
    // 
    #region GridView Header
    protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#DDEECC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#DDEECC", true);
          
            //AddMergedCells(objgridviewrow, objtablecell, 0, "Total Listed Drs", "#DDEECC", true);

            string spclty = Request.QueryString["cbVal"].ToString();

            spclty = spclty.Remove(spclty.LastIndexOf(','));
            string[] ttlSpc = spclty.Split(',');

            for (int i = 0; i < ttlSpc.Length; i++)
            {                
                string strMonthName = Convert.ToDateTime("01-" + ttlSpc[i].Substring(0, 2).ToString() + "-" + ttlSpc[i].Substring(3, 4).ToString()).ToString("MMM-yy");
                AddMergedCells(objgridviewrow, objtablecell, 4, strMonthName, "#DDEECC", true);
                iLstMnth.Add(Convert.ToInt32(ttlSpc[i].Substring(0, 2).ToString()));
                iLstYr.Add(Convert.ToInt32(ttlSpc[i].Substring(3, 4).ToString()));
                sLstMnth.Add(strMonthName);
            }
            lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
                +" To " +Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length-1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length-1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");
            //AddMergedCells(objgridviewrow, objtablecell, 4, "Total", "#33FF66", true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            for (int i = 0; i < ttlSpc.Length; i++)
            {
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "List", "#DDEECC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#DDEECC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Seen", "#DDEECC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed", "#DDEECC", false);
            }
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //if (celltext == "FieldForce Name")
        //{
        //    objtablecell.Wrap = false;
        //}
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //    
    #region grid doctor rowdatabound
    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iLstTmp = 0, iMtTmp = 0, iSnTmp = 0, iMsdTmp = 0;
            int iLst = 0, iMt = 0, iSn = 0, iMsd = 0;
            int k = e.Row.Cells.Count - 5, tmp = 0, indx = e.Row.RowIndex;

            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][5].ToString()));

            for (int j = 4, inxMnth = 0, inxYr = 0; j < e.Row.Cells.Count; j += 4, inxMnth++, inxYr++)
            {
                iLstTmp = Convert.ToInt32((e.Row.Cells[j].Text == "&nbsp;") || (e.Row.Cells[j].Text == "-") ? "0" : e.Row.Cells[j].Text);
                iLst += iLstTmp;
                iMtTmp = Convert.ToInt32((e.Row.Cells[j + 1].Text == "&nbsp;") || (e.Row.Cells[j + 1].Text == "-") ? "0" : e.Row.Cells[j + 1].Text);
                iMt += iMtTmp;
                iSnTmp = Convert.ToInt32((e.Row.Cells[j + 2].Text == "&nbsp;") || (e.Row.Cells[j + 2].Text == "-") ? "0" : e.Row.Cells[j + 2].Text);
                iSn += iSnTmp;
                //
                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
                {
                    iMsdTmp = 0;
                }
                else
                {
                    if (iLstTmp != 0)
                        iMsdTmp = iLstTmp - iMtTmp;
                    else
                        iMsdTmp = 0;
                }
                //
                e.Row.Cells[j + 3].Text = iMsdTmp.ToString();
                iMsd += iMsdTmp;
                //
                System.Drawing.Color clr;
                if (tmp % 2 == 0)
                {
                    clr = System.Drawing.Color.LightPink;
                    tmp++;
                }
                else
                {
                    clr = System.Drawing.Color.LightGray;
                    tmp++;
                }
                e.Row.Cells[j].BackColor = clr;
                e.Row.Cells[j + 1].BackColor = clr;
                e.Row.Cells[j + 2].BackColor = clr;
                e.Row.Cells[j + 3].BackColor = clr;
                //
                LinkButton lnk_btn = new LinkButton();
                if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR" || dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR")
                {
                    if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                    {
                        if (e.Row.Cells[j].Text != "&nbsp;" && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                        {
                            lnk_btn = new LinkButton();
                            lnk_btn.Text = e.Row.Cells[j].Text;
                            lnk_btn.Attributes.Add("class", "btnLstDr");
                            lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + iLstMnth[inxMnth] + "&cYr=" + iLstYr[inxYr] + "&cTyp_cd=-1" +
                                "&typ=1&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                            lnk_btn.Font.Underline = false;
                            lnk_btn.ToolTip = AssignToolTip(iLstMnth[inxMnth].ToString(), iLstYr[inxYr].ToString(), "List");
                            lnk_btn.ForeColor = System.Drawing.Color.Black;
                            e.Row.Cells[j].Controls.Add(lnk_btn);
                        }
                    }
                    //
                    if (e.Row.Cells[j + 1].Text != "&nbsp;" && e.Row.Cells[j + 1].Text != "0" && e.Row.Cells[j + 1].Text != "-")
                    {
                        lnk_btn = new LinkButton();
                        lnk_btn.Text = e.Row.Cells[j + 1].Text;
                        lnk_btn.Attributes.Add("class", "btnDrMt");
                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + iLstMnth[inxMnth] + "&cYr=" + iLstYr[inxYr] + "&cTyp_cd=-1" +
                            "&typ=3&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                        lnk_btn.Font.Underline = false;
                        lnk_btn.ToolTip = AssignToolTip(iLstMnth[inxMnth].ToString(), iLstYr[inxYr].ToString(), "Met");
                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[j + 1].Controls.Add(lnk_btn);
                    }
                    //
                    if (e.Row.Cells[j + 2].Text != "&nbsp;" && e.Row.Cells[j + 2].Text != "0" && e.Row.Cells[j + 2].Text != "-")
                    {
                        lnk_btn = new LinkButton();
                        lnk_btn.Text = e.Row.Cells[j + 2].Text;
                        lnk_btn.Attributes.Add("class", "btnDrSn");
                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + iLstMnth[inxMnth] + "&cYr=" + iLstYr[inxYr] + "&cTyp_cd=-1" +
                            "&typ=4&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                        lnk_btn.Font.Underline = false;
                        lnk_btn.ToolTip = AssignToolTip(iLstMnth[inxMnth].ToString(), iLstYr[inxYr].ToString(), "Seen");
                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[j + 2].Controls.Add(lnk_btn);
                    }
                    //
                    if (e.Row.Cells[j + 3].Text != "&nbsp;" && e.Row.Cells[j + 3].Text != "0" && e.Row.Cells[j + 3].Text != "-")
                    {
                        lnk_btn = new LinkButton();
                        lnk_btn.Text = e.Row.Cells[j + 3].Text;
                        lnk_btn.Attributes.Add("class", "btnDrMsd");
                        lnk_btn.Attributes.Add("onclick", "javascript:window.open('ViewDetails_DrWise_Report.aspx?div_code=" + div_code + "&sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + iLstMnth[inxMnth] + "&cYr=" + iLstYr[inxYr] + "&cTyp_cd=-1" +
                            "&typ=2&cMode=" + Request.QueryString["cMode"].ToString() + "' ,null,'');");
                        lnk_btn.Font.Underline = false;
                        lnk_btn.ToolTip = AssignToolTip(iLstMnth[inxMnth].ToString(), iLstYr[inxYr].ToString(), "Missed");
                        lnk_btn.ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[j + 3].Controls.Add(lnk_btn);
                    }
                }
            }/*
            for (int iTtl = e.Row.Cells.Count - 4; iTtl < e.Row.Cells.Count; iTtl += 4)
            {
                e.Row.Cells[iTtl].Text = iLst.ToString();
                e.Row.Cells[iTtl + 1].Text = iMt.ToString();
                e.Row.Cells[iTtl + 2].Text = iSn.ToString();
                e.Row.Cells[iTtl + 3].Text = iMsd.ToString();
                break;
            }
            */
            for (int i = 4; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
            //
        }
    }
    //
    private string AssignToolTip(string sMnth, string sYr, string sType)
    {
        SalesForce sf = new SalesForce();
        string sTxt = sf.getMonthName(sMnth.ToString()).Substring(0, 3) + "/" + sYr.Substring(2, 2);
        return sTxt += " (" + sType + ")";
    }
    //
    #endregion
    //
}