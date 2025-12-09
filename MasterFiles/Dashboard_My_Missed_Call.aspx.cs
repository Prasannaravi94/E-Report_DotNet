using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MasterFiles_Dashboard_My_Missed_Call : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    DataSet dsListedDR = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cMnth = string.Empty;
    string cYear = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string sCurrentDate = string.Empty;
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();
        }
        catch
        {
            sf_code = Request.QueryString["sfcode"].ToString();
            div_code = Request.QueryString["div_Code"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
            cMnth = Request.QueryString["cMnth"].ToString();
            cYear = Request.QueryString["cYr"].ToString();
        }

        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedIndex = 1;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 1;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = cYear;
                }
            }
            ddlMonth.SelectedValue = cMnth;

            FillReport();
        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (sf_type == "1" || sf_type == "MR")
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    #endregion

    #region FillReport
    private void FillReport()
    {
        ListedDR lstDR = new ListedDR();
        dsListedDR = lstDR.FetchTerritory(ddlFieldForce.SelectedValue);

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            dsListedDR.Tables[0].Rows.RemoveAt(0);
            dsListedDR.AcceptChanges();
        }

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsListedDR;
            GrdFixation.DataBind();

            Doctor dc = new Doctor();
            DataSet dsDoctor = null;
            DateTime dtCurrent;
            string sCurrentDate = string.Empty;

            if (DateTime.Now.Month == 12)
            {
                sCurrentDate = "01-01-" + (DateTime.Now.Year + 1);
            }
            else
            {
                sCurrentDate = (DateTime.Now.Month + 1) + "-01-" + DateTime.Now.Year;
            }

            dtCurrent = Convert.ToDateTime(sCurrentDate);

            foreach (GridViewRow row in GrdFixation.Rows)
            {
                HiddenField hdnTerrCode = (HiddenField)row.FindControl("hdnTerrCode");
                Label lblTerrName = (Label)row.FindControl("lblTerrName");
                HyperLink hyLnkMsdCallCount = (HyperLink)row.FindControl("hyLnkMsdCallCount");

                dsDoctor = dc.Missed_Doc_cnt(div_code, ddlFieldForce.SelectedValue, DateTime.Now.Month, DateTime.Now.Year, dtCurrent, "1");

                if (dsDoctor.Tables[0].Rows.Count > 0)
                {
                    var rsLstdDr = from dr in dsDoctor.Tables[0].AsEnumerable()
                                   where dr.Field<string>("territory_Name") == lblTerrName.Text
                                   select new
                                   {
                                       territory_Name = dr.Field<string>("territory_Name")
                                   };

                    if (rsLstdDr.Any())
                    {
                        hyLnkMsdCallCount.Text = rsLstdDr.Count().ToString();
                        hyLnkMsdCallCount.Attributes.Add("href", "javascript:showModalPopUp('" + ddlFieldForce.SelectedValue + "', '" + ddlFieldForce.SelectedItem.Text + "','" + lblTerrName.Text + "', '" + div_code + "')");
                    }
                    else
                    {
                        hyLnkMsdCallCount.Text = "0";
                        hyLnkMsdCallCount.Enabled = false;
                    }
                }
            }
        }
        else
        {
            GrdFixation.DataSource = dsListedDR;
            GrdFixation.DataBind();
        }
    }
    #endregion

    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0"
            && ddlFieldForce.SelectedValue != "")
        {
            FillReport();
        }
    }
    #endregion

    //#region GrdFixation_RowCreated
    //protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    cfmonth = Convert.ToInt32(ddlMonth.SelectedValue);
    //    cfyear = Convert.ToInt32(ddlYear.SelectedValue);
    //    ctmonth = Convert.ToInt32(ddlMonth.SelectedValue);
    //    ctyear = Convert.ToInt32(ddlYear.SelectedValue);

    //    SalesForce sf = new SalesForce();
    //    if (e.Row.RowType == DataControlRowType.Header)
    //    {
    //        //Creating a gridview object            
    //        GridView objGridView = (GridView)sender;

    //        //Creating a gridview row object
    //        GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

    //        //Creating a table cell object
    //        TableCell objtablecell = new TableCell();

    //        #region Merge cells

    //        AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#337AB7", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#337AB7", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#337AB7", true);
    //        AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#337AB7", true);

    //        int months1 = (ctyear - cfyear) * 12 + ctmonth - cfmonth; //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //        int cmonth1 = cfmonth;
    //        int cyear1 = cfyear;

    //        ViewState["months"] = months1;
    //        ViewState["cmonth"] = cmonth1;
    //        ViewState["cyear"] = cyear1;

    //        GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        if (months1 >= 0)
    //        {
    //            for (int j = 1; j <= months1 + 1; j++)
    //            {
    //                if (cmonth1 > 0)
    //                AddMergedCells(objgridviewrow, objtablecell, 3, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#337AB7", true);

    //                TableCell objtablecell2 = new TableCell();
    //                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed DR", "#337AB7", false);
    //                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#337AB7", false);
    //                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed", "#337AB7", false);
    //                iLstVstmnt.Add(cmonth1);
    //                iLstVstyr.Add(cyear1);

    //                cmonth1 = cmonth1 + 1;
    //                if (cmonth1 == 13)
    //                {
    //                    cmonth1 = 1;
    //                    cyear1 = cyear1 + 1;
    //                }
    //            }
    //        }
    //        //Lastly add the gridrow object to the gridview object at the 0th position
    //        //Because, the header row position is 0.
    //        objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
    //        objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
    //        #endregion
    //    }
    //}
    //#endregion

    //#region AddMergedCells
    //protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    //{
    //    objtablecell = new TableCell();
    //    objtablecell.Text = celltext;
    //    objtablecell.ColumnSpan = colspan;
    //    if ((colspan == 0) && bRowspan)
    //    {
    //        objtablecell.RowSpan = 2;
    //    }
    //    objtablecell.Style.Add("background-color", backcolor);
    //    objtablecell.Style.Add("color", "white");
    //    objtablecell.Style.Add("border-color", "black");
    //    objtablecell.HorizontalAlign = HorizontalAlign.Center;
    //    objgridviewrow.Cells.Add(objtablecell);
    //}
    //#endregion

    //#region GrdFixation_RowDataBound
    //protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int indx = e.Row.RowIndex;
    //        int k = e.Row.Cells.Count - 5;
    //        //
    //        #region Calculations

    //        if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
    //        {

    //        }

    //        for (int l = 4, j = 0; l < e.Row.Cells.Count; l++)
    //        {

    //            int iTtl_Drs = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
    //            int iDrs_Mt = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);

    //            int iDrs_Msd = iTtl_Drs - iDrs_Mt;
    //            e.Row.Cells[l + 2].Text = iDrs_Msd.ToString();
    //            //HyperLink hLnk = new HyperLink();
    //            //hLnk.Text = e.Row.Cells[l + 2].Text;
    //            //if (iDrs_Msd > 0)
    //            //    hLnk.Attributes.Add("href", "javascript:showMissedDR('" + drFF["sf_code"].ToString() + "', '" + cmonthdoc.ToString() + "', '" + cyeardoc.ToString() + "', 1,'')");
    //            //hLnk.Style.Add("text-decoration", "none");
    //            //hLnk.Style.Add("color", "black");
    //            //hLnk.Style.Add("cursor", "hand");
    //            if (e.Row.Cells[l].Text != "0")
    //            {
    //                HyperLink hLink = new HyperLink();
    //                hLink.Text = e.Row.Cells[l + 2].Text;
    //                string sSf_code = dtrowClr.Rows[indx][1].ToString();

    //                int cMnth = iLstVstmnt[j];
    //                int cYr = iLstVstyr[j];

    //                if (cMnth == 12)
    //                {
    //                    sCurrentDate = "01-01-" + (cYr + 1).ToString();
    //                }
    //                else
    //                {
    //                    sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
    //                }
    //                //hLink.Attributes.Add("href", "javascript:showMissedDR('" + sSf_code + "',  '" + cMnth + "', '" + cYr + "',1,'','" + iDrs_Msd.ToString() + "')");
    //                //hLink.ToolTip = "Click here";
    //                //hLink.ForeColor = System.Drawing.Color.Blue;
    //                hLink.Style.Add("text-decoration", "none");
    //                hLink.Style.Add("color", "black");
    //                hLink.Style.Add("cursor", "hand");
    //                e.Row.Cells[l + 2].Controls.Add(hLink);
    //            }
    //            l += 2;
    //            j++;
    //        }

    //        for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
    //        {

    //            if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
    //            {/*
    //                HyperLink hLnk = new HyperLink();
    //                hLnk.Text = e.Row.Cells[i].Text;
    //                hLnk.NavigateUrl = "#";
    //                hLnk.ForeColor = System.Drawing.Color.Black;
    //                hLnk.Font.Underline = false;
    //                hLnk.ToolTip = "Click to View Details";
    //                e.Row.Cells[i].Controls.Add(hLnk);*/
    //            }
    //            else if (e.Row.Cells[i].Text == "0")
    //            {
    //                if (e.Row.Cells[i].Text == "0")
    //                {
    //                    e.Row.Cells[i].Text = "-";
    //                    e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
    //                }
    //            }
    //            e.Row.Cells[i].Attributes.Add("align", "center");
    //        }
    //        try
    //        {
    //            int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
    //            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
    //        }
    //        catch
    //        {
    //            e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
    //        }

    //        #endregion
    //        //
    //        e.Row.Cells[1].Wrap = false;
    //        e.Row.Cells[2].Wrap = false;
    //        e.Row.Cells[3].Wrap = false;
    //    }
    //}
    //#endregion
}