using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

using System.Collections;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_rptResigned_User_Status1 : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string DCR_StartDate = string.Empty;
    string DCR_EndDate = string.Empty;
    string SF_Name = string.Empty;
    string DCR = string.Empty;
    string tp = string.Empty;
    string tp_DCR = string.Empty;
    string Emp_Code = string.Empty;
    int startmonth;
    int endmonth;
    int startyear;
    int endyear;
    int month;
    int year;
    DateTime ServerStartTime;
    DateTime ServerEndTime;

    DataTable dtMnYr = new DataTable();
    int months = 0;

    DataSet dsts = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        //div_code = Request.QueryString["div_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sfcode"].ToString();
        SF_Name = Request.QueryString["SF_Name"].ToString();
        DCR_StartDate = Request.QueryString["DCR_Start_Date"].ToString();
        DCR_EndDate = Request.QueryString["DCR_End_Date"].ToString();
        tp_DCR = Request.QueryString["tp_DCR"].ToString();
        Emp_Code = Request.QueryString["Employee_Code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        //if (!Page.IsPostBack)
        {
            FillYear();
        }

        if (tp_DCR == "TP")
        {
            lblHead.Text = "TP View for Resigned User";
        }
        else if (tp_DCR == "DCR")
        {
            lblHead.Text = "DCR View for Resigned User";
        }
        else if (tp_DCR == "Leave")
        {
            lblHead.Text = "Leave Status for Resigned User";
        }
        else if (tp_DCR == "InputStatus")
        {
            lblHead.Text = "Input Status for Resigned User";
        }
        else if (tp_DCR == "ExpenseStatus")
        {
            lblHead.Text = "Expense Status for Resigned User";
        }
        else if (tp_DCR == "Paylsip")
        {
            lblHead.Text = "Payslip for Resigned User";
        }

        if (ddlMonth.SelectedValue != "0")
        {

            string[] test4 = ddlMonth.SelectedValue.Split('-');
            month = Convert.ToInt32(test4[0]);
            year = Convert.ToInt32(test4[1]);

            lblsf_Name.Text = SF_Name;
            lblsfcode.Text = sf_code;
            lbltpdcr.Text = tp_DCR;
            lblmonth.Text = month.ToString();
            lblyear.Text = year.ToString();
            lbldcrstart_Date.Text = DCR_StartDate;
            lbldcrend_Date.Text = DCR_EndDate;
            lblemployeecode.Text = Emp_Code;
        }

    }

    private void FillYear()
    {

        string[] test2 = DCR_StartDate.Split('/');
        startmonth = Convert.ToInt32(test2[1]);
        startyear = Convert.ToInt32(test2[2]);

        string[] test3 = DCR_EndDate.Split('/');
        endmonth = Convert.ToInt32(test3[1]);
        endyear = Convert.ToInt32(test3[2]);

        months = (Convert.ToInt32(endyear) - Convert.ToInt32(startyear)) * 12 + Convert.ToInt32(endmonth) - Convert.ToInt32(startmonth);
        int cmonth = Convert.ToInt32(startmonth);
        int cyear = Convert.ToInt32(startyear);


        int iMn = 0; int iYr = 0;

        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH_Numeric", typeof(string));
        dtMnYr.Columns.Add("Month_String", typeof(string));


        List<string> MonthYear = new List<string>();

        SalesForce sf = new SalesForce();

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

            string month_new = sf.getMonthName(iMn.ToString());
            dtMnYr.Rows.Add(null, iMn.ToString() + "-" + iYr.ToString(), month_new.Substring(0, 3) + "-" + iYr.ToString());
            MonthYear.Add(iMn.ToString() + "-" + iYr.ToString());
            months--; cmonth++;
        }

        ddlMonth.DataSource = dtMnYr;
        ddlMonth.DataValueField = "MNTH_Numeric";
        ddlMonth.DataTextField = "Month_String";
        ddlMonth.DataBind();
        ddlMonth.Items.Insert(0, new ListItem("---Select---", "0"));

        if (dtMnYr.Rows.Count > 0)
        {
            ddlMonth.SelectedValue = dtMnYr.Rows[dtMnYr.Rows.Count - 1]["MNTH_Numeric"].ToString();
        }

        #region GrdBind
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Leave_Details_Mnth_wise";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);

        //dsts.Tables[0].Columns.Remove("Ter_Code");
        dsts.Tables[0].Columns.Remove("Sf_Code");
        dsts.Tables[0].Columns.Remove("Sf_Code1");
        //dsts.Tables[0].Columns.Remove("Territory_Name");

        if (dsts.Tables[0].Rows.Count > 0)
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        else
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
        #endregion
    }


    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (dtMnYr.Rows.Count > 0)
            {
                //Creating a gridview object            
                GridView objGridView = (GridView)sender;

                //Creating a gridview row object
                GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

                //Creating a table cell object
                TableCell objtablecell = new TableCell();


                GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell objtablecell2 = new TableCell();
                TableCell objtablecell3 = new TableCell();

                HyperLink hlink = new HyperLink();

                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Fieldforce Name ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Employee Id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
                for (int n = 0; n <= dtMnYr.Rows.Count - 1; n++)
                {
                    string a = dtMnYr.Rows[n]["MNTH_Numeric"].ToString();
                    AddMergedCells(objgridviewrow, objtablecell, 2, 0, dtMnYr.Rows[n]["Month_String"].ToString(), "#0097AC", true);
                    //dtMnYr.Rows[dtMnYr.Rows.Count - 1]["MNTH_Numeric"].ToString()

                }

                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

                #endregion
            }
        }
    }


    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.Font.Bold = true;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Session["div_code"] = div_code;
        /*   string[] test4 = ddlMonth.SelectedValue.Split('-');
           month = Convert.ToInt32(test4[0]);
           year = Convert.ToInt32(test4[1]);
           string sURL = string.Empty;

           if (tp_DCR =="TP")
           {
               Session["div_code"] = div_code;
              // Response.Redirect("~/MasterFiles/Report/rptTPView.aspx?sf_code=" + sf_code + "&cur_month=" + month + "&cur_year=" + year + "&level=-1" + "&sf_name=" + SF_Name);
               sURL = "Report/rptTPView.aspx?sf_code=" + sf_code + "&cur_month=" + month + "&cur_year=" + year + "&level=-1" + "&sf_name=" + SF_Name;
               string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=800,height=600,left=0,top=0');";
               ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);

               lblnorecords.Visible = false;
           }
           else if (tp_DCR =="DCR")
           {

           }*/


    }
}