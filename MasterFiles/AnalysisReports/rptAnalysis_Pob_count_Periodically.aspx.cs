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


public partial class MasterFiles_AnalysisReports_rptAnalysis_Pob_count_Periodically : System.Web.UI.Page
{
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
  
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
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string prdvalue = string.Empty;
    string prdname = string.Empty;
    string txtEffFrom = string.Empty;
    string txtEffTo = string.Empty;
    DateTime dtDCRfrom;
    DateTime dtDCRto;
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        txtEffFrom = Request.QueryString["txtEffFrom"].ToString();
        txtEffTo = Request.QueryString["txtEffTo"].ToString();
       
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        prdvalue = Request.QueryString["prdvalue"];
        prdname = Request.QueryString["prdname"];
        ViewState["prdname"] = prdname;
        ViewState["prdvalue"] = prdvalue;


        dtDCRfrom = Convert.ToDateTime(txtEffFrom);
        dtDCRto = Convert.ToDateTime(txtEffTo);

        SalesForce sf = new SalesForce();


        lblHead.Text = "Product wise Rx Report between  " + "<span style='color:#007bff'>" + dtDCRfrom.ToString("MMM") + " " + dtDCRfrom.Day + " " + dtDCRfrom.Year + "</span>" + " To " + " " + "<span style='color:#007bff'>" + dtDCRto.ToString("MMM") + " " + dtDCRto.Day + " " + dtDCRto.Year + "</span>";
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        screen_name = "rptAnalysis_Pob_count_Periodically";
        if (!Page.IsPostBack)
        {
            FillReport();
        }
    }
    private void FillReport()
    {
        //int months = (Convert.ToInt32(dtDCRto.Year) - Convert.ToInt32(dtDCRfrom.Year)) * 12 + Convert.ToInt32(dtDCRto.Month) - Convert.ToInt32(dtDCRfrom.Month); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth = Convert.ToInt32(dtDCRfrom.Month);
        //int cyear = Convert.ToInt32(dtDCRto.Year);


        //int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        //dtMnYr.Columns["INX"].AutoIncrementStep = 1;
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


        int withprd = 0;

        DataTable dtprd = new DataTable();
        dtprd.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtprd.Columns["INX"].AutoIncrementSeed = 1;
        dtprd.Columns["INX"].AutoIncrementStep = 1;
        dtprd.Columns.Add("prdcode", typeof(int));

        if (prdvalue != "0")
        {
            withprd = 1;

            string prdcode = prdvalue.Remove(prdvalue.Length - 1);

            string[] code;
            code = prdcode.Split(',');

            foreach (string cod in code)
            {
                dtprd.Rows.Add(null, cod);
            }
        }


        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        SqlCommand cmd = new SqlCommand("Dr_PobRx_Periodically_new", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 3000;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
      //  cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Fdate", dtDCRfrom.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@Tdate", dtDCRto.ToString("MM/dd/yyyy"));
        cmd.Parameters.AddWithValue("@withprd", withprd);
        cmd.Parameters.AddWithValue("@dtprd", dtprd);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
       
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(10);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(8);
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(1);
        dsts.Tables[0].Columns["sf_name"].SetOrdinal(1);
        dsts.Tables[0].Columns["sf_hq"].SetOrdinal(2);
        dsts.Tables[0].Columns["sf_Designation_Short_Name"].SetOrdinal(3);
        GrdFixation.DataSource = dsts;
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
        string attachment = "attachment; filename=Visit_Details_Class_Wise.xls";
        Response.ClearContent();
        Response.AddHeader("Content-Disposition", attachment);
        Response.ContentType = "application/x-msexcel";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Design Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Joining Date", "#0097AC", true);
            //int months = (Convert.ToInt32(Request.QueryString["To_year"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_year"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["To_Month"].ToString()) - Convert.ToInt32(Request.QueryString["Frm_Month"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            //int cmonth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            //int cyear = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());
            //ViewState["months"] = months1;
            //ViewState["cmonth"] = cmonth1;
            //ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //if (months >= 0)
            //{

            //for (int j = 1; j <= months + 1; j++)
            //{
            //    iLstMonth.Add(cmonth);
            //    iLstYear.Add(cyear);
            prdvalue = ViewState["prdvalue"].ToString();
            if (prdvalue != "0")
                    {

                        var count = prdvalue.Count(c => c == ',');

                        AddMergedCells(objgridviewrow, objtablecell, 7 + count, txtEffFrom + " To " + " " + txtEffTo , "#0097AC", true);

                        TableCell objtablecell2 = new TableCell();
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "FWD", "#0097AC", false);
                      //  AddMergedCells(objgridviewrow2, objtablecell2, 0, "Morning Calls", "#0097AC", false);
                       // AddMergedCells(objgridviewrow2, objtablecell2, 0, "Evening Calls", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Drs Visited", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Avg", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Drs POB", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chem POB", "#0097AC", false);

                prdname = ViewState["prdname"].ToString();
                string prdnamee = prdname.Remove(prdname.Length - 1);

                        string[] name;

                        name = prdnamee.Split(',');

                        foreach (string nn in name)
                        {
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, nn, "#0097AC", false);
                        }


                    }
                    else
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 3, txtEffFrom + " To " + " " + txtEffTo, "#0097AC", true);

                        TableCell objtablecell2 = new TableCell();
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Avg", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Drs POB", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chem POB", "#0097AC", false);
                    }

                   
              //  }

           // }


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


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
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if (objtablecell.Text == "Emp Code" || objtablecell.Text == "Joining Date")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true, 2);
        }

        dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, objtablecell.Text, sf_code);
        if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
        {
            if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
            {
                //objtablecell.Visible = false;
                objtablecell.Style.Add("display", "none");
            }
        }
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;

            for (int i = 6, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.Cells[i].Text != "0")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i].Text;
                    string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                    string sSf_name = dtrowClr.Rows[iInx][4].ToString();

                    //if (i == 7)
                    //{

                    //    hLink.Text = e.Row.Cells[i].Text;
                    //    string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                    //    string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                    //    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "','" + "M" + "','" + dtDCRfrom.ToString("MM/dd/yyyy") + "','" + dtDCRto.ToString("MM/dd/yyyy") + "')");

                    //    hLink.ToolTip = "Click here";
                    //    hLink.ForeColor = System.Drawing.Color.Blue;
                    //    e.Row.Cells[i].Controls.Add(hLink);
                    //}
                    //if (i == 8)
                    //{

                    //    hLink.Text = e.Row.Cells[i].Text;
                    //    string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                    //    string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                    //    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "','" + "E" + "','" + dtDCRfrom.ToString("MM/dd/yyyy") + "','" + dtDCRto.ToString("MM/dd/yyyy") + "')");
                    //    hLink.ToolTip = "Click here";
                    //    hLink.ForeColor = System.Drawing.Color.Blue;
                    //    e.Row.Cells[i].Controls.Add(hLink);
                    //}
                    if (e.Row.RowIndex != dtrowClr.Rows.Count - 1)
                    {
                        if (i == 7)
                        {

                            hLink.Text = e.Row.Cells[i].Text;
                            string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "','" + "All" + "','" + dtDCRfrom.ToString("MM/dd/yyyy") + "','" + dtDCRto.ToString("MM/dd/yyyy") + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                    }
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
                    // hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                    //hLink.NavigateUrl = "#";
                    //hLink.ToolTip = "Click here";
                  //  hLink.ForeColor = System.Drawing.Color.Black;
                   // e.Row.Cells[i].Controls.Add(hLink);

                    int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][8].ToString()));
                    j++;
                }
                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                {
                    e.Row.Cells[i].Text = "";
                    //e.Row.Cells[i].Attributes.Add("align", "center");
                }
                e.Row.Cells[i].Attributes.Add("align", "right");
            }
        }
        Chemist chem = new Chemist();

        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
        if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("column_name"),
                             Ch_Code = data.Field<string>("column_name")
                         };
            var listOfGrades = result.ToList();
            cblGridColumnList.Visible = true;
            cblGridColumnList.DataSource = listOfGrades;
            cblGridColumnList.DataTextField = "Ch_Name";
            cblGridColumnList.DataValueField = "Ch_Code";
            cblGridColumnList.DataBind();

            string headerText = string.Empty;

            for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
            {
                headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

                System.Web.UI.WebControls.ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                if (ddl != null)
                {
                    if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                    }
                    else
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                    }
                }

                if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                {

                    int j = i + 4;

                    e.Row.Cells[j].Visible = false;
                }
            }
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

    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && sf_code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, sf_code);
        }

        Response.Redirect(Request.RawUrl);
    }

}