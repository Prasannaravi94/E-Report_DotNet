using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
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
        FillColor();

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
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2017; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
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
        linkcheck.Visible = false;
        txtNew.Visible = true;
        btnSF.Visible = true;
        FillColor();
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
        dsSubDivision = dv.getsfExp_approval_Active(divcode, ddlSubdiv.SelectedValue.ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        //}

        DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetails(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt1 = dv.getFixedClmnName(divcode);
        DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt3 = dv.getApproveamntPlus(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt4 = dv.getApproveamntMinus(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable =dsSubDivision.Tables[0];
        table = dv.getMgrAppr(divcode);
        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("Fixed_Column1");
        mainTable.Columns.Add("Fixed_Column2");
        mainTable.Columns.Add("Fixed_Column3");
        mainTable.Columns.Add("Fixed_Column4");
        mainTable.Columns.Add("Fixed_Column5");
        mainTable.Columns.Add("Fixed_Column6");
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

        if (mainTable.Rows.Count > 0)
        {
//            double totClaimedAmnt = 0;
            foreach(DataRow row in mainTable.Rows)
            {
                double totClaimedAmnt = 0;
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
                DataRow[] rows = dtAllFare.Select(filter);
                DataRow[] othRows = dt.Select(filter);
                DataRow[] misRows = dt2.Select(filter);
                DataRow[] appRows = dt3.Select(filter);
                DataRow[] appRows1 = dt4.Select(filter);
                if (appRows.Count()>0)
                {
                    row["appAmnt"] = appRows[0]["grand_total"];
                    //if (appRows[0]["typ"].ToString() == "0")
                    //{
                    //    row["Decrement"] = appRows[0]["amt"];
                    //}
                    if (appRows[0]["typ"].ToString() == "1")
                    {
                        row["Increment"] = appRows[0]["amt"];
                    }
                }
                if (appRows1.Count() > 0)
                {
                    row["appAmnt"] = appRows1[0]["grand_total"];
                    if (appRows1[0]["typ"].ToString() == "0")
                    {
                        row["Decrement"] = appRows1[0]["amt"];
                    }
                    //if (appRows[0]["typ"].ToString() == "1")
                    //{
                    //    row["Increment"] = appRows[0]["amt"];
                    //}
                }
                //string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                string st = "<span style='color:blue;font-weight:bold'>Approval Pending</span>";
                if (rows.Count() > 0)
                {
                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    row["rw_amount"] = rows[0]["rw_amount"];
                    row["Date"] = rows[0]["submission_date"];
                    row["Approval_Datea"] = rows[0]["Approval_Datea"];
                    row["admin_approval_date"] = rows[0]["admin_approval_date"];
                    st =rows[0]["Status"].ToString();
                    if (st=="1"||st=="8")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                    else if (st=="2"||st=="6")
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
                        //st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                        st = "<span style='color:blue;font-weight:bold'>Approval Pending</span>";
                    }
                    
                    totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(row["allowance"].ToString())+Convert.ToDouble(row["fare"].ToString())+Convert.ToDouble(row["rw_amount"].ToString());
                }
                row["Status"] = st;
                if (misRows.Count() > 0)
                {
                    row["mis_Amt"] = misRows[0]["mis_Amt"];
                    
                    totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(row["mis_Amt"].ToString());
                }
                for (int i = 0; i < othRows.Count(); i++)
                {
                    row["Fixed_Column" + (i + 1)] = othRows[i]["Amt"];

                    totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(othRows[i]["Amt"].ToString());
                }
            row["tot"]=totClaimedAmnt;
            }
            for(int i=0;i<dt1.Rows.Count;i++)
            {
                grdSalesForce.Columns[12+i].Visible = true;
                grdSalesForce.Columns[12+i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

            }

            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = mainTable;
            grdSalesForce.DataBind();
            foreach (GridViewRow gridRow in grdSalesForce.Rows)
             {
            
            //TableCell cell = new TableCell();
            HyperLink link = new HyperLink();
            Label lbl = new Label();
            HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
            HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");
            HiddenField hold = (HiddenField)gridRow.FindControl("Hiddenhold");
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
           

            link.Text = "<span>" + lbl.Text + "</span>";
            //lbl.Text = name.Value;
            string AdminEnd = "AdmnEnd";
                string sfcd=code.Value;
                string sURL = "";
                if (sfcd.Contains("MR"))
                {
                     sURL = "../MR/RptAutoExpense_rowwise_Icarus.aspx?AdminEnd=" + AdminEnd + "&sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                }
                else
                {
                     sURL = "../MGR/RptAutoExpense_MGR_Icarus.aspx?AdminEnd=" + AdminEnd + "&sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                
                }
               
            //Response.Redirect("MR/RptAutoExpense_rowwise.aspx?type=" + type + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                link.NavigateUrl = "#";
                //cell.Controls.Add(link);
              Label label=  (Label)gridRow.FindControl("lblstatus");
              if (label.Text.Contains("Not Prepared") || label.Text.Contains("Mgr Approval Pending"))
                {
                    gridRow.Cells[2].Controls.Add(lbl);
                }
             else
                {
                    gridRow.Cells[2].Controls.Add(link);

                }

             }

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
    }

}