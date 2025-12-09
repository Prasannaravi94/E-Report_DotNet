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

public partial class MasterFiles_Dashboard_TargetvsSales : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sfcode = string.Empty;
    string sfname = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string sCurrentDate = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
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
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
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
                FillMasterList();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillMasterList();
                ddlFieldForce.SelectedIndex = 1;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                Fillteam();
                ddlFieldForce.SelectedIndex = 1;
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

        FillReport();
    }
    #endregion

    #region Fillteam
    private void Fillteam()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.Hierarchy_Team(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    #endregion

    #region FillMasterList
    private void FillMasterList()
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

    public static Array GetMonths(DateTime date1, DateTime date2)
    {
        //Note - You may change the format of date as required.  
        return GetDates(date1, date2).Select(x => x.ToString("MMMM yyyy")).ToArray();
    }

    public static IEnumerable<DateTime> GetDates(DateTime date1, DateTime date2)
    {
        while (date1 <= date2)
        {
            yield return date1;
            date1 = date1.AddMonths(1);
        }
        if (date1 > date2 && date1.Month == date2.Month)
        {
            // Include the last month  
            yield return date1;
        }
    }

    #region FillReport
    private void FillReport()
    {
        //cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        //cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
        //ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
        //ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

        //DateTime fDate = DateTime.ParseExact(cfyear + "-" + cfmonth.ToString("D2") + "-01", "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);
        //DateTime tDate = DateTime.ParseExact(ctyear + "-" + ctmonth.ToString("D2") + "-01", "yyyy-MM-dd",
        //                               System.Globalization.CultureInfo.InvariantCulture);

        //Array dates = GetMonths(fDate, tDate);
        //string[] strDate = dates.OfType<object>().Select(o => o.ToString()).ToArray();
        //string strDates = (new JavaScriptSerializer()).Serialize(strDate);

        //string monthName = new DateTime(cfyear, cfmonth, 1).ToString("MMM", CultureInfo.InvariantCulture);

        int months = (Convert.ToInt32(ddlTYear.SelectedValue) - Convert.ToInt32(ddlFYear.SelectedValue)) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue) - Convert.ToInt32(ddlFMonth.SelectedValue); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
        int cyear = Convert.ToInt32(ddlFYear.SelectedValue);

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
        con.Open();
        SqlCommand cmd = new SqlCommand("AllProduct_Exp_Count", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@mode", "1");
        cmd.CommandTimeout = 300;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(3);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdPrdExp.DataSource = dsts;
        GrdPrdExp.DataBind();
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
            cfmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
            cfyear = Convert.ToInt32(ddlFYear.SelectedValue);
            ctmonth = Convert.ToInt32(ddlTMonth.SelectedValue);
            ctyear = Convert.ToInt32(ddlTYear.SelectedValue);

            FillReport();
        }
    }
    #endregion

    //#region GrdPrdExp_DataBound
    //protected void GrdPrdExp_DataBound(object sender, EventArgs e)
    //{
    //    if (GrdPrdExp.Rows.Count > 0)
    //    {
    //        GrdPrdExp.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //}
    //#endregion

    #region GrdPrdExp_RowCreated
    protected void GrdPrdExp_RowCreated(object sender, GridViewRowEventArgs e)
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
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Product Name / Month", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "No of Doctors - Tagged", "#0097AC", true);

            int months = (Convert.ToInt32(ddlTYear.SelectedValue) - Convert.ToInt32(ddlFYear.SelectedValue)) * 12 + Convert.ToInt32(ddlTMonth.SelectedValue) - Convert.ToInt32(ddlFMonth.SelectedValue); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(ddlFMonth.SelectedValue);
            int cyear = Convert.ToInt32(ddlFYear.SelectedValue);

            SalesForce sf = new SalesForce();

            for (int i = 0; i <= months; i++)
            {
                if (cmonth != 0)
                {
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                }
                string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                AddMergedCells(objgridviewrow, objtablecell, 0, 0, sTxt, "#0097AC", true);
                AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "No. of Drs (As Per DCR)", "#0097AC", true);
                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
            //
            #endregion
            //
        }
    }
    #endregion

    #region AddMergedCells
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    #endregion

    #region GrdPrdExp_RowDataBound
    protected void GrdPrdExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;
            for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
            {
                if (i == 2)
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i].Text;
                    string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                    string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                    hLink.Attributes.Add("href", "javascript:showModal('" + ddlFieldForce.SelectedValue + "', '" + ddlFieldForce.SelectedItem.Text + "','" + Pro_Name + "','" + Pro_code + "', '"+ div_code +"')");
                    hLink.ToolTip = "Click here";
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[i].Controls.Add(hLink);
                }
                else
                {

                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string Prd_code = dtrowClr.Rows[iInx][1].ToString();
                        string Prd_Name = dtrowClr.Rows[iInx][2].ToString();
                        int cMnth = iLstMonth[j];
                        int cYr = iLstYear[j];

                        if (cMnth == 12)
                        {
                            sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        }
                        else
                        {
                            sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        }

                        //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "')");
                        hLink.Attributes.Add("href", "javascript:show('" + ddlFieldForce.SelectedValue + "', '" + ddlFieldForce.SelectedItem.Text + "', '" + cYr + "', '" + cMnth + "','" + Prd_Name + "','" + Prd_code + "', '" + sCurrentDate + "','1', '" + div_code + "')");
                        hLink.ToolTip = "Click here";
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        //int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                        j++;
                    }
                }

                if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                {
                    e.Row.Cells[i].Text = "-";
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
        }
    }
    #endregion
}