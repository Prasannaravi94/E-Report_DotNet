using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Dashboard_Missed_Call : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string sCurrentDate = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        //Session["div_code"] = Request.QueryString["div"].ToString();
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();
        }
        catch
        {
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
           // sf_type = Request.QueryString["sf_type"].ToString();
            cFMnth = Request.QueryString["cMnth"].ToString();
            cFYear = Request.QueryString["cYr"].ToString();
            cTMnth = Request.QueryString["cMnth"].ToString();
            cTYear = Request.QueryString["cYr"].ToString();
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
                ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlFYear.Items.Add(k.ToString());
                    ddlFYear.SelectedValue = cFYear;

                    ddlTYear.Items.Add(k.ToString());
                    ddlTYear.SelectedValue = cTYear;
                }
            }
            ddlFMonth.SelectedValue = cFMnth;
            ddlTMonth.SelectedValue = cTMnth;

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

        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

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
        cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
        ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

        //cfmonth = Convert.ToInt32(02);
        //cfyear = Convert.ToInt32(2020);
        //ctmonth = Convert.ToInt32(02);
        //ctyear = Convert.ToInt32(2020);

        int months = (ctyear - cfyear) * 12 + ctmonth - cfmonth; //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = cfmonth;
        int cyear = ctyear;

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
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
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "MissedCall";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.ToString());
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    #endregion

    #region GrdFixation
    protected void GrdFixation_DataBound(object sender, EventArgs e)
    {
        //if (GrdFixation.Rows.Count > 0)
        //{
        //    GrdFixation.HeaderRow.TableSection = TableRowSection.TableHeader;
        //}
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
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);

            int months1 = (ctyear - cfyear) * 12 + ctmonth - cfmonth; //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = cfmonth;
            int cyear1 = ctyear;

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);

            for (int j = 0; j <= months1; j++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 3, sf.getMonthName(cmonth1.ToString()) + " - " + cyear1, "#0097AC", true);

                TableCell objtablecell2 = new TableCell();
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed DR", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed", "#0097AC", false);
                iLstVstmnt.Add(cmonth1);
                iLstVstyr.Add(cyear1);

                cmonth1 = cmonth1 + 1;
                if (cmonth1 == 13)
                {
                    cmonth1 = 1;
                    cyear1 = cyear1 + 1;
                }
            }

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {
            }

            for (int l = 4, j = 0; l < e.Row.Cells.Count; l++)
            {

                int iTtl_Drs = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int iDrs_Mt = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);

                int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                e.Row.Cells[l + 2].Text = iDrs_Msd.ToString();
                if (e.Row.Cells[l].Text != "0")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[l + 2].Text;
                    string sSf_code = dtrowClr.Rows[indx][1].ToString();

                    int cMnth = 0;
                    int cYr = 0;
                    if (iLstVstmnt[j] == 0)
                    {
                        cMnth = iLstVstmnt[j + 1];
                        cYr = iLstVstyr[j + 1];
                    }
                    else
                    {
                        cMnth = iLstVstmnt[j];
                        cYr = iLstVstyr[j];
                    }

                    if (cMnth == 12)
                    {
                        sCurrentDate = "01-01-" + (cYr + 1).ToString();
                    }
                    else
                    {
                        sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                    }
                    hLink.Attributes.Add("href", "javascript:showMissedDR('" + sSf_code + "',  '" + cMnth + "', '" + cYr + "',1,'','" + iDrs_Msd.ToString() + "', '"+ div_code +"')");
                    //hLink.ToolTip = "Click here";
                    //hLink.ForeColor = System.Drawing.Color.Blue;
                    hLink.Style.Add("text-decoration", "none");
                    hLink.Style.Add("color", "black");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[l + 2].Controls.Add(hLink);
                }

                l += 2;

                j++;
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
                    e.Row.Cells[i].Controls.Add(hLnk);*/
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
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
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
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
            && ddlFieldForce.SelectedValue != ""
            && ddlFMonth.SelectedValue != "0"
            && ddlTMonth.SelectedValue != "0")
        {
            FillReport();
        }
    }
    #endregion
}