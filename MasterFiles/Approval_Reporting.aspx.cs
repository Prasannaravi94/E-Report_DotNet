using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Approval_Reporting : System.Web.UI.Page
{
        #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = null;
    DataSet dsSF = null;
    DataSet dsReport = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string Reporting_To_SF = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    string sCmd = string.Empty;
    string DCR = string.Empty;
    string TP = string.Empty;
    string Lst_Dr = string.Empty;
    string leave = string.Empty;
    string SS_AM = string.Empty;
    string Expense = string.Empty;
    string Otr_AM = string.Empty;
    DataSet dsState = null;
    int time;
    string sf_name = string.Empty;
    string designation_short_name = string.Empty;
    string sf_hq = string.Empty;
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
            //div_code = Session["div_code"].ToString();

            sf_code = Request.QueryString["sf_code"];

            sf_name = Request.QueryString["sf_name"];
            designation_short_name = Request.QueryString["designation_short_name"];
            sf_hq = Request.QueryString["sf_hq"];
            Reporting_To_SF = Request.QueryString["Reporting_To_SF"];


        if (!Page.IsPostBack)
        {
            lblRegionName.Text = sf_name + " - " + designation_short_name + " - " + sf_hq;
            lblRept_name.Text = Reporting_To_SF;

            //FillSalesForce_Reporting();
           FillSalesForce();
            //FillReporting();
            //menu1.Title = this.Page.Title;
           // // menu1.FindControl("btnBack").Visible = true;
            //ddlFields.SelectedValue = "Sf_Name";
          //  txtsearch.Visible = true;
            //btnSearch.Visible = true;
            //ill_sales2();
        }
    }

    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#666699", true);
           // AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting To", "#666699", true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Approved By Manager", "#666699", true);
           // AddMergedCells(objgridviewrow, objtablecell, 0, "Inline Edit", "#666699", true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Vacant", System.Drawing.Color.LightSkyBlue.Name, true);
            //AddMergedCells(objgridviewrow, objtablecell, 2, "Blocked", System.Drawing.Color.LightSkyBlue.Name, true);

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "DCR", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "TP", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed Dr", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Leave", "#666699", false);
           // AddMergedCells(objgridviewrow2, objtablecell2, 0, "Secondary Sales", "#666699", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Expense", "#666699", false);
           // AddMergedCells(objgridviewrow2, objtablecell2, 0, "Other", "#666699", false);
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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "White");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getApproval_Reporting(div_code,sf_code);

        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();

        strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=a.Reporting_To) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code WHERE" +
              " c.sf_TP_Active_Flag=0 and c.SF_Status=0 AND " +
                "  a.Reporting_To = '" + sf_code + "' " +
                   // " and a.Division_Code = '" + div_code + "' " +
                    " ORDER BY 2";

        dsSalesForce = db_ER.Exec_DataSet(strQry);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }



}