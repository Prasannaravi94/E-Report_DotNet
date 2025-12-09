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
using System.Net;
using System.Web.UI.DataVisualization.Charting;
using DBase_EReport;
using System.Data.SqlClient;
#endregion
//
#region Questionnaire View
public partial class MasterFiles_Quesionaire_rpt_Questionnaire_View : System.Web.UI.Page
{
    //
    #region variables
    DataSet dsDoctor = null;
    DataSet dsDCR = null;
    DataSet dsmgrsf = new DataSet();
    DataTable dtMnYr = new DataTable();
    SalesForce sf = new SalesForce();

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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataTable dtrowClr = new DataTable();
    List<int> iLstColCnt = new List<int>();

    int tot_miss = 0, TempValue = 0, iTtlMnth = 0, iTtl_Visit = 0;
    int fldwrk_total = 0;
    int doctor_total = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    double dblCoverage = 0.00;
    double dblCatg = 0.00;
    double dblaverage = 0.00;
    DateTime dtCurrent;
    string tot_fldwrk = string.Empty;
    string tot_dr = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string sCurrentDate = string.Empty;
    string sType = string.Empty, sLvl="";
    string smode = string.Empty;
    #endregion
    //
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        string sf_name = Request.QueryString["sf_name"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        sType = Request.QueryString["cbVal"].ToString();
        smode = Request.QueryString["mode"].ToString();
                
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        string sHeading = "";
        if (smode == "1")
        {
            lblHead.Text = "Questionnaire View  Product Based - " + strFrmMonth.Substring(0, 3) + " " + FYear + "  To  " + strToMonth.Substring(0, 3) + " " + TYear;
        }
        else if (smode == "2")
        {
            lblHead.Text = "Questionnaire View Competitor Brand Based - " + strFrmMonth.Substring(0, 3) + " " + FYear + "  To  " + strToMonth.Substring(0, 3) + " " + TYear;
        }
        LblForceName.Text = "Field Force Name : " + sf_name;
        FillCatg();
    }
    //
    #endregion
    //
    #region getMonthName
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
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
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
    #endregion
    //
   
    //
   
    #endregion
    //
    #region FillCatg
    private void FillCatg()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);
        iTtlMnth = months;

        int iMn = 0, iYr = 0;        
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
        DataTable dtProd = new DataTable();
        dtProd.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtProd.Columns["INX"].AutoIncrementSeed = 1;
        dtProd.Columns["INX"].AutoIncrementStep = 1;
        dtProd.Columns.Add("CODE", typeof(int));

        string Prod = Request.QueryString["cbVal"].ToString();
        if (Prod != "")
        {
            Prod = Prod.Remove(Prod.LastIndexOf(','));
            string[] ttlProd = Prod.Split(',');

            foreach (string sProd in ttlProd)
            {
                if (sProd != "")
                    dtProd.Rows.Add(null, Convert.ToInt32(sProd));
            }
        }
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        string sProc_Name = "";

        if (smode == "1")
        {
            sProc_Name = "Questionnaire_View_Product";
        }
        else if (smode == "2")
        {
            sProc_Name = "Questionnaire_View_Comp";
        }
        con.Open();
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Prod", dtProd);
        cmd.CommandTimeout = 650;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
       // dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixationMode.DataSource = dsts;
        GrdFixationMode.DataBind();
    }
    #endregion
    //
    #region GrdFixationMode_RowCreated
    protected void GrdFixationMode_RowCreated(object sender, GridViewRowEventArgs e)
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

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #endregion
            //
            #region Merge cells
            //
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "S.No", "#008080", true);            
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "FieldForce Name", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Designation", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "HQ", "#008080", true);           
            
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Month", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "DR Cnt", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Chem Cnt", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Hos Cnt", "#008080", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, 0, "Stok Cnt", "#008080", true);            

            
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            
            //
            #endregion
            //
        }
    }
    //
    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Height = 30;
        objtablecell.Font.Size = 12;
        objtablecell.Font.Bold = true;
        objtablecell.Style.Add("border-color", "#000000");
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "#FFFFFF");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion
    //
    #endregion
    //    
    #region GrdFixationMode_RowDataBound
    protected void GrdFixationMode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
         
            //
            LinkButton lnk_btn = new LinkButton();
            for (int i = 5 ; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "";
                }
               
            }
           
                //if (e.Row.Cells[1].Text != "")
                //{
                //    lnk_btn = new LinkButton();
                //    lnk_btn.Text = e.Row.Cells[1].Text;
                //    lnk_btn.Attributes.Add("class", "btnSf");
                //    lnk_btn.Attributes.Add("onclick", "javascript:window.open('rpt_Questionnaire_View_Zoom.aspx?sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + FMonth + "&cYr=" + FYear + "&type=" + "DO" + "&Prod=" + sType + "&sf=" + "ALL" + "&TMnth=" + TMonth + "&TYr=" + TYear + "&smode=" + smode +
                //        "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                //    lnk_btn.Font.Underline = false;
                //    // lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                //    lnk_btn.ForeColor = System.Drawing.Color.Black;
                //    e.Row.Cells[1].Controls.Add(lnk_btn);
                //}
            
            if (e.Row.Cells[5].Text != "&nbsp;" && e.Row.Cells[5].Text != "" && e.Row.Cells[5].Text != "0" && e.Row.Cells[5].Text != "-")
            {
                lnk_btn = new LinkButton();
                lnk_btn.Text = e.Row.Cells[5].Text;
                lnk_btn.Attributes.Add("class", "btnLstDr");
                lnk_btn.Attributes.Add("onclick", "javascript:window.open('rpt_Questionnaire_View_Zoom.aspx?sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString() + "&cYr=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "&type=" + "DO" + "&Prod=" + sType + "&sf=" + "" + "&smode=" + smode +
                    "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                lnk_btn.Font.Underline = false;
                // lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                lnk_btn.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].Controls.Add(lnk_btn);
            }
            if (e.Row.Cells[6].Text != "&nbsp;" && e.Row.Cells[6].Text != "" && e.Row.Cells[6].Text != "0" && e.Row.Cells[6].Text != "-")
            {
                lnk_btn = new LinkButton();
                lnk_btn.Text = e.Row.Cells[6].Text;
                lnk_btn.Attributes.Add("class", "btnChem");
                lnk_btn.Attributes.Add("onclick", "javascript:window.open('rpt_Questionnaire_View_Zoom.aspx?sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString() + "&cYr=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "&type=" + "CH" + "&Prod=" + sType + "&sf=" + "" + "&smode=" + smode +
                    "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                lnk_btn.Font.Underline = false;
                // lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                lnk_btn.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[6].Controls.Add(lnk_btn);
            }
            if (e.Row.Cells[7].Text != "&nbsp;" && e.Row.Cells[7].Text != "" && e.Row.Cells[7].Text != "0" && e.Row.Cells[7].Text != "-")
            {
                lnk_btn = new LinkButton();
                lnk_btn.Text = e.Row.Cells[7].Text;
                lnk_btn.Attributes.Add("class", "btnChem");
                lnk_btn.Attributes.Add("onclick", "javascript:window.open('rpt_Questionnaire_View_Zoom.aspx?sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString() + "&cYr=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "&type=" + "HO" + "&Prod=" + sType + "&sf=" + "" + "&smode=" + smode +
                    "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                lnk_btn.Font.Underline = false;
                // lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                lnk_btn.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[7].Controls.Add(lnk_btn);
            }
            if (e.Row.Cells[8].Text != "&nbsp;" && e.Row.Cells[8].Text != "" && e.Row.Cells[8].Text != "0" && e.Row.Cells[8].Text != "-")
            {
                lnk_btn = new LinkButton();
                lnk_btn.Text = e.Row.Cells[8].Text;
                lnk_btn.Attributes.Add("class", "btnStock");
                lnk_btn.Attributes.Add("onclick", "javascript:window.open('rpt_Questionnaire_View_Zoom.aspx?sf_code=" + dtrowClr.Rows[indx][1].ToString() + "&sf_name=" + dtrowClr.Rows[indx][2].ToString() + "&cMnth=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString() + "&cYr=" + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString() + "&type=" + "ST" + "&Prod=" + sType + "&sf=" + "" + "&smode=" + smode +
                    "' ,null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=650,height=450,left=0,top=0');");
                lnk_btn.Font.Underline = false;
                // lnk_btn.ToolTip = AssignToolTip(iLstMonth[inxMnth].ToString(), iLstYear[inxYr].ToString(), sLstCat_Spclty_Cls[inxCode].ToString(), "List");
                lnk_btn.ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[8].Controls.Add(lnk_btn);
            }
            
            var result = e.Row.Cells[4].Text.Aggregate("", (res, c) => res + ((byte)c).ToString("X"));
                if (Convert.ToInt32(result) > 40)
                {
                    e.Row.Cells[4].Text = (Convert.ToInt32(result) - 31).ToString();
                }
                e.Row.Cells[4].Text = sf.getMonthName(dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["MNTH"].ToString()).Substring(0, 3) +
                    " - " + dtMnYr.Rows[Convert.ToInt32(e.Row.Cells[4].Text) - 1]["YR"].ToString();
                e.Row.Cells[1].Attributes.Add("align", "left");
                e.Row.Cells[2].Attributes.Add("align", "left");
                e.Row.Cells[3].Attributes.Add("align", "left");
                e.Row.Cells[4].Attributes.Add("align", "left");
                e.Row.Cells[4].Attributes.Add("style", "color:#0000CD;");
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][5].ToString()));                    
            }
        
        //
        #region merge columns
        int RowSpan = 2;
        for (int i = GrdFixationMode.Rows.Count - 2; i >= 0; i--)
        {
            GridViewRow currRow = GrdFixationMode.Rows[i];
            GridViewRow prevRow = GrdFixationMode.Rows[i + 1];
            if (currRow.Cells[0].Text == prevRow.Cells[0].Text)
            {
                currRow.Cells[0].RowSpan = RowSpan;
                prevRow.Cells[0].Visible = false;
                currRow.Cells[1].RowSpan = RowSpan;
                prevRow.Cells[1].Visible = false;
                currRow.Cells[2].RowSpan = RowSpan;
                prevRow.Cells[2].Visible = false;
                currRow.Cells[3].RowSpan = RowSpan;
                prevRow.Cells[3].Visible = false;
               
      
                RowSpan += 1;
            }
            else
            {
                RowSpan = 2;
            }
        }
        #endregion
        //
    }
    #endregion
    //
}
//
