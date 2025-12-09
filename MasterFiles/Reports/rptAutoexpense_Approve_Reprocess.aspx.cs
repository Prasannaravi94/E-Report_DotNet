using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Reports_rptAutoexpense_Approve_Reprocess : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable table = null;
    int slno = 0;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            // FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
            FillFieldForcediv(divcode);
            FillColor();
        }
        if (Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
        }
        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
        }


    }

    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlSubdiv.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
        var today = DateTime.Today;
        var month1 = new DateTime(today.Year, today.Month, 1);
        var first = month1.AddMonths(-1);
        var last = month1.AddDays(-1);
        monthId.SelectedValue = first.Month.ToString();
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2017; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
        //yearID.SelectedValue = DateTime.Now.Year.ToString();
        var today = DateTime.Today;
        var month1 = new DateTime(today.Year, today.Month, 1);
        var first = month1.AddMonths(0);
        var yr = month1.AddYears(-1);
        if (first.Month.ToString() == "1")
            yearID.SelectedValue = yr.Year.ToString();
        else
            yearID.SelectedValue = DateTime.Now.Year.ToString();
    }
    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();
        //if (Session["sf_type"].ToString() == "2")
        //{
        //    sfcode=
        //}
        //if (Session["sf_type"].ToString() == "2")
        //{
        //    dsSubDivision = dv.SalesForce_Reporting(sfcode);
        //}
        //else
        //{
        dsSubDivision = dv.SalesForceListMgrGet(divcode, sfcode);
        // }
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();

        }
    }
    protected void linkcheck_Click(object sender, EventArgs e)
    {
        FillFieldForcediv(divcode);
        ddlSubdiv.Visible = true;
        //txtNew.Visible = true;
        btnSF.Visible = true;
        FillColor();
    }
    private string getDisplayPlaceOfWork(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                int startInx = p.LastIndexOf('(');
                int endInx = p.LastIndexOf(')') + 1;
                string type = p.Substring(startInx, endInx - startInx);
                string p1 = p.Substring(0, p.LastIndexOf('(')) + "<span style=\"background-color:yellow\">" + type + "</span>";

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        //distinctValues = distinctValues + p1;
                        distinctValues = distinctValues + "," + p1;
                    else
                        distinctValues = distinctValues + "," + p1;

                }
                else
                    distinctValues = p1;

                counter++;
            }
        }
        return distinctValues;
    }

    protected void btnSF_Click(object sender, EventArgs e)
    {

        Distance_calculation dv = new Distance_calculation();
        //SalesForce S = new SalesForce();
        //dsSubDivision = S.Hierarchy_Team(divcode, ddlSubdiv.SelectedValue.ToString());
        //if (chkVacant.Checked == true)
        //{
        //    dsSubDivision = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());
        //    //string filter="Active".Equals(dsSubDivision.Rows["sf_Tp_Active_Flag"].ToString());	
        //}
        //else
        //{
        // dsSubDivision = dv.getFilterRgn_Vacant(divcode, ddlSubdiv.SelectedValue.ToString());
        dsSubDivision = dv.getsfExp_approval_Active_reprocess(divcode, ddlSubdiv.SelectedValue.ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        //}

        DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetails(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt1 = dv.getFixedClmnName(divcode);
        DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt3 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable = dsSubDivision.Tables[0];
        table = dv.getMgrAppr(divcode);
        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("Fixed_Column1");
        mainTable.Columns.Add("Fixed_Column2");
        mainTable.Columns.Add("Fixed_Column3");
        mainTable.Columns.Add("Fixed_Column4");
        mainTable.Columns.Add("Fixed_Column5");
        mainTable.Columns.Add("Fixed_Column6");
        mainTable.Columns.Add("Fixed_Column7");
        mainTable.Columns.Add("Fixed_Column8");
        mainTable.Columns.Add("Fixed_Column9");
        mainTable.Columns.Add("Fixed_Column10");
        mainTable.Columns.Add("mis_Amt");
        mainTable.Columns.Add("rw_amount");
        mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        mainTable.Columns.Add("appAmnt");
        mainTable.Columns.Add("Decrement");
        mainTable.Columns.Add("Increment");
        mainTable.Columns.Add("Date");
        mainTable.Columns.Add("Approval_Datea");
        mainTable.Columns.Add("admin_approval_date");
        mainTable.Columns.Add("IP_Address");
        mainTable.Columns.Add("session_name");
        if (mainTable.Rows.Count > 0)
        {
            //            double totClaimedAmnt = 0;
            foreach (DataRow row in mainTable.Rows)
            {
                double totClaimedAmnt = 0;
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
                DataRow[] rows = dtAllFare.Select(filter);
                DataRow[] othRows = dt.Select(filter);
                DataRow[] misRows = dt2.Select(filter);
                DataRow[] appRows = dt3.Select(filter);
                if (appRows.Count() > 0)
                {
                    row["appAmnt"] = appRows[0]["grand_total"];
                    if (appRows[0]["typ"].ToString() == "0")
                    {
                        row["Decrement"] = appRows[0]["amt"];
                    }
                    if (appRows[0]["typ"].ToString() == "1")
                    {
                        row["Increment"] = appRows[0]["amt"];
                    }
                }
                string st = "";
                if (divcode == "104" && row["SF_Code"].ToString().Contains("MR"))
                {
                    st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                }
                else
                {
                    st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                }
                if (rows.Count() > 0)
                {
                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    row["rw_amount"] = rows[0]["rw_amount"];

                    row["IP_Address"] = rows[0]["IP_Address"];
                    row["session_name"] = rows[0]["session_name"];
                    row["Date"] = rows[0]["submission_date"];
                    row["Approval_Datea"] = rows[0]["Approval_Datea"];
                    row["admin_approval_date"] = rows[0]["admin_approval_date"];
                    st = rows[0]["Status"].ToString();

                    if (st == "1" || st == "8")
                    {
                        if (divcode == "104" && row["SF_Code"].ToString().Contains("MR"))
                        {
                            st = "<span style='color:green;font-weight:bold'>Approved</span>";
                        }
                        else
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                        }
                    }
                    else if (st == "2" || st == "6")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                    else if (st == "3")
                    {
                        st = "<span style='background-color:brown;font-weight:bold'>Approval Pending</span>";
                    }
                    else if (st == "7")
                    {

                        if (table.Rows.Count > 0)
                        {
                            if ("1".Equals(table.Rows[0]["MgrAppr_Sameadmin"].ToString()))
                            {
                                st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                            }
                            else
                            {
                                st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";

                            }
                        }
                        else
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";
                        }


                        //st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";
                    }
                    else
                    {
                        if (divcode == "104" && row["SF_Code"].ToString().Contains("MR"))
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                        }
                        else
                        {
                            st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                        }
                    }

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString()) + Convert.ToDouble(row["rw_amount"].ToString());
                }
                row["Status"] = st;
                if (misRows.Count() > 0)
                {
                    row["mis_Amt"] = misRows[0]["mis_Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
                }
                for (int i = 0; i < othRows.Count(); i++)
                {
                    row["Fixed_Column" + (i + 1)] = othRows[i]["Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
                }
                row["tot"] = totClaimedAmnt;
            }
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                grdSalesForce.Columns[13 + i].Visible = true;
                grdSalesForce.Columns[13 + i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

            }

            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = mainTable;
            grdSalesForce.DataBind();
            foreach (GridViewRow gridRow in grdSalesForce.Rows)
            {

                //TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                HyperLink link1 = new HyperLink();
                Label lbl = new Label();
                Label lbl1 = new Label();
                HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
                HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");
                HiddenField hold = (HiddenField)gridRow.FindControl("Hiddenhold");
                HiddenField viewHidden = (HiddenField)gridRow.FindControl("viewHidden");
                Label lblSNo = (Label)gridRow.FindControl("lblSNo");
                if (hold.Value == "H")
                {
                    gridRow.Visible = false;
                }
                else
                {
                    slno += 1;

                    lblSNo.Text = slno.ToString();

                }


                lbl.Text = name.Value;
                lbl1.Text = viewHidden.Value;
                link1.Text = "<span>" + lbl1.Text + "</span>";


                link.Text = "<span>" + lbl.Text + "</span>";
                //lbl.Text = name.Value;
                string sURL = "";
                string sURL1 = "";
                // string AdminEnd = "AdmnEnd";
                string sfcd = code.Value;
                //if (divcode == "104")
                //{
                //    if (sfcd.Contains("MR"))
                //    {
                //        sURL = "../MR/RptAutoExpense_rowwise_Cibeles.aspx?AdminEnd=" + AdminEnd + "&sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                //    }
                //}
                //else
                {
                    sURL = "RptAutoExpense_view2.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                }
                sURL1 = "RptAutoExpense_Zoom.aspx?sf_code=" + code.Value + "&month=" + monthId.SelectedValue.ToString() + "&year=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                //link.Attributes["onclick"] = "javascript:window.open('" + sURL + "','ModalPopUp');";
                //link.NavigateUrl = "#";

                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=600,height=600,left=0,top=0');";
                link.NavigateUrl = "#";

                //link1.Attributes["onclick"] = "javascript:window.open('" + sURL1 + "','ModalPopUp');";
                //link1.NavigateUrl = "#";

                link1.Attributes["onclick"] = "javascript:window.open('" + sURL1 + "','_blank','resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=600,height=600,left=0,top=0');";
                link1.NavigateUrl = "#";
                //cell.Controls.Add(link);
                Label label = (Label)gridRow.FindControl("lblstatus");
                if (label.Text.Contains("Not Prepared") || label.Text.Contains("Mgr Approval Pending"))
                {
                    gridRow.Cells[2].Controls.Add(lbl);
                    gridRow.Cells[7].Controls.Add(lbl1);
                }
                else
                {
                    gridRow.Cells[2].Controls.Add(link);
                    gridRow.Cells[7].Controls.Add(link1);

                }



            }

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
    }

    //protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // Get the data bound to the current row
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;

    //        string name = rowView["DESIGNATION"].ToString();
    //      //  int age = Convert.ToInt32(rowView["Age"]);

    //        // Example: Hide the row based on the column value
    //        if (name == "ADMIN")
    //        {
    //            e.Row.Visible = false;
    //        }
    //        // Check a condition to hide the row
         
    //    }
    //}

    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    protected void grdSalesForce_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getsfExp_approval_Active(divcode, ddlSubdiv.SelectedValue.ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());

        DataView sortedView = new DataView(dsSubDivision.Tables[0]);
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdSalesForce.DataSource = sortedView;
        grdSalesForce.DataBind();
    }
}