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
#endregion
public partial class MasterFiles_AnalysisReports_rptCallfeedback : System.Web.UI.Page
{
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
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();

        //  sReportType = Convert.ToInt32(Request.QueryString["cMode"].ToString());
        //  strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        //if (sReportType == 1)
        //{
        //    sHeading = " Category ";
        //}
        //else if (sReportType == 2)
        //{
        //    sHeading = " Speciality ";
        //}
        //else if (sReportType == 3)
        //{
        //    sHeading = " Class ";
        //}
        lblHead.Text = "ListedDr - Call Feedbackwise for the month of  " + strFrmMonth + " " + FYear + "  To  " + strToMonth + " " + TYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        FillReport();
    }
    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        sProc_Name = "Callfeedbackwise_Dr";

        //else if (sReportType == 2)
        //{
        //    sProc_Name = "visit_fixation_Spclty";
        //}
        //else if (sReportType == 3)
        //{
        //    sProc_Name = "visit_fixation_Class";
        //}
        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 500;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        //grdDoctor.Attributes["runat"] = "server";
        //grdDoctor.RenderControl(hw);
        HtmlForm frm = new HtmlForm();
        GrdFixation.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
        //grdDoctor.AllowPaging = true;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
            

            int months = (Convert.ToInt32(Request.QueryString["To_year"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_year"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["To_Month"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_Month"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());
            //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(FMonth);
            //int cyear = Convert.ToInt32(FYear);

            //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());
            string strQry = "";

            strQry = "select Feedback_Id,Feedback_Content,Division_Code,Act_Flag from Mas_App_CallFeedback where Division_Code = '" + div_code + "' and Act_Flag=0 ";


            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                //string strMonthName = Convert.ToDateTime("01-" + i.ToString() + "-2016").ToString("MMM-yy"); 
                int iColSpan = 0;
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "TDrs", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met", "#0097AC", false);
                foreach (DataRow dtRow in dsDoctor.Tables[0].Rows)
                {
                    int iCnt = 0;
                    iColSpan = ((dsDoctor.Tables[0].Rows.Count));
                    //iCnt = Convert.ToInt32(dtRow["No_of_visit"].ToString()) + 2;
                    iLstVstCnt.Add(1);

                    AddMergedCells(objgridviewrow2, objtablecell2, 0, iCnt, dtRow["Feedback_Content"].ToString(), "#0097AC", false);


                    //for (int j = 0; j < iCnt - 2; j++)
                    //{
                    //    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, (j + 1).ToString() + " V", "#4A9586", false);
                    //}
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Miss", "#4A9586", false);
                }
                //CBP
                iColSpan = iColSpan + 2;
                //CBP
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, iColSpan, sTxt, "#0097AC", true);
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
            //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.   
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
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
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations


            //ABP
            int cmonth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            int cyear = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());

            int iColSpan = 0;

             iColSpan = iColSpan+(Convert.ToInt32((dsDoctor.Tables[0].Rows.Count)));
            for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + cmonth + "', '" + cyear + "', '" + TMonth + "', '" + TYear + "','" + "1" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Font.Underline = true;
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[i].Controls.Add(hLink);
                }
                cmonth = cmonth + 1;
                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
                i = i + iColSpan + 1;
            }
            //ABP

            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {/*
                int months = (Convert.ToInt32(ddlTYear.SelectedValue.ToString()) - Convert.ToInt32(ddlFYear.SelectedValue.ToString())) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue.ToString()) - Convert.ToInt32(ddlFMonth.SelectedValue.ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue.ToString());
                int cyear = Convert.ToInt32(ddlFYear.SelectedValue.ToString());
                string dttme = "";
                if (months == 12)
                    dttme = "01-01-" + (cyear + 1).ToString();
                else
                    dttme = (months + 1).ToString() + "-01-" + (cyear).ToString();

                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);

                SqlCommand cmd = new SqlCommand("VisitDetail_LstDr_Count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@div_code", div_code);
                cmd.Parameters.AddWithValue("@sf_code", dtrowClr.Rows[indx][1].ToString());
                cmd.Parameters.AddWithValue("@cdate", dttme);
                cmd.CommandTimeout = 100;
                string txtc = dtrowClr.Rows[indx][1].ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dst = new DataTable();
                da.Fill(dst);
                e.Row.Cells[4].Text = dst.Rows[0][0].ToString();
              */
            }

            for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {/*
                    HyperLink hLnk = new HyperLink();
                    hLnk.Text = e.Row.Cells[i].Text;
                    hLnk.NavigateUrl = "#";
                    hLnk.ForeColor = System.Drawing.Color.Black;
                    hLnk.Font.Underline = false;
                    hLnk.ToolTip = "Click to View Details";
                    e.Row.Cells[i].Controls.Add(hLnk);
                    */
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    int ist = iLstVstCnt[j];
                    int iMax = (e.Row.Cells[i - ist].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - ist].Text);
                    int iMin = (e.Row.Cells[i - 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[i - 1].Text);
                    e.Row.Cells[i].Text = (iMax - iMin).ToString();
                    e.Row.Cells[i].Attributes.Add("style", "color:red");
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black");
                    }
                    j++;
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
        }
    }

    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#EAF2D9";
                break;
            case "2":
                strCase = "#DBECF0";
                break;
            case "3":
                strCase = "#F0E2DB";
                break;
            case "4":
                strCase = "#E9E0EB";
                break;
            case "5":
                strCase = "#CCFFCC";
                break;
            case "6":
                strCase = "#F7819F";
                break;
            case "7":
                strCase = "#0B610B";
                break;
            case "8":
                strCase = "#FF4000";
                break;
            case "9":
                strCase = "#FE2E64";
                break;
            case "10":
                strCase = "#9AFE2E";
                break;
            case "11":
                strCase = "#F2F5A9";
                break;
            case "12":
                strCase = "#F5A9D0";
                break;
            default:
                strCase = "#F2F5A9";
                break;
        }
        return strCase;
    }

}