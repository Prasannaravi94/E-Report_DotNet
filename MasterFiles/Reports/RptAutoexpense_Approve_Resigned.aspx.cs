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
   
	
    protected void btnSF_Click(object sender, EventArgs e)
    {
        Distance_calculation dv = new Distance_calculation();

        dsSubDivision = dv.sp_get_Expense_Approval_Resigned(divcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable vacsts = dv.getVacStsMnth(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dtAllFare = dv.getAllowFareResigned(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetailsResigned(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt1 = dv.getFixedClmnName(divcode);
        DataTable dt2 = dv.getmisResigned(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt3 = dv.getApproveamntResigned(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());

        DataTable dt7 = dv.getAllowFareResinedPrev(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt8 = dv.getOtherExpDetailsResinedPrev(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt9 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt10 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable =dsSubDivision.Tables[0];

        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("sl_no");
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
        mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        mainTable.Columns.Add("appAmnt");
        mainTable.Columns.Add("Decrement");
        mainTable.Columns.Add("Increment");
        mainTable.Columns.Add("Date");

        if (mainTable.Rows.Count > 0)
        {
//            double totClaimedAmnt = 0;
            foreach(DataRow row in mainTable.Rows)
            {
                double totClaimedAmnt = 0;
                String filter1 = "SF_Code='" + row["SF_Code"].ToString() + "'";
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "' and Employee_Code='"+ row["Employee_Code"].ToString() + "'";
                DataRow[] vac = vacsts.Select(filter);
                DataRow[] rows;
                DataRow[] othRows;
                DataRow[] misRows;
                DataRow[] appRows;
                if (vac.Count() > 0)
                   // if (vac.Count() > 0 && vac[0]["SF_Code"] == row["SF_Code"].ToString())
                {
                 
                    rows = dtAllFare.Select(filter1);
                    othRows = dt.Select(filter1);
                    misRows = dt2.Select(filter1);
                    appRows = dt3.Select(filter1);
                    
                }
                else
                {

                    rows = dt7.Select(filter1);
                    othRows = dt8.Select(filter1);
                    misRows = dt9.Select(filter1);
                    appRows = dt10.Select(filter1);  
                }
                if (appRows.Count()>0)
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
                string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                if (rows.Count() > 0)
                {
                    row["sl_no"] = rows[0]["sl_no"];

                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    row["Date"] = rows[0]["submission_date"];
                     st=rows[0]["Status"].ToString();
                    
                    if (st=="1")
                    {
                    st="<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                    }
                    else if (st=="2")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                    else if (st == "3")
                    {
                        st = "<span style='background-color:brown;font-weight:bold'>Approval Pending</span>";
                    }
                    else 
                    {
                        st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                    }
                    
                    totClaimedAmnt=totClaimedAmnt+Convert.ToDouble(row["allowance"].ToString())+Convert.ToDouble(row["fare"].ToString());
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
                grdSalesForce.Columns[10+i].Visible = true;
                grdSalesForce.Columns[10+i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

            }

            pnlprint.Visible = true;
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = mainTable;
            grdSalesForce.DataBind();
            int K = 0;
            foreach (GridViewRow gridRow in grdSalesForce.Rows)
             {
                 K += 1;
            //TableCell cell = new TableCell();
            HyperLink link = new HyperLink();
            Label lbl = new Label();
            HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
            HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");
            //HiddenField empid = (HiddenField)gridRow.FindControl("emphidden");
            HiddenField LstDCRdt = (HiddenField)gridRow.FindControl("LstDCRdt");
            Label slno = (Label)gridRow.FindControl("lblslno");
                Label empid = (Label)gridRow.FindControl("lblsfempid");
           
           
                lbl.Text = name.Value;
            

            link.Text = "<span>" + lbl.Text + "</span>";

            string sURL = "RptAutoExpense_view_resigned.aspx?sf_code=" + code.Value + "&slno=" + slno.Text + "&LstDCRdt=" + LstDCRdt.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&sname=" + name.Value + "&divCode=" + divcode + "&empid=" + empid.Text + "";
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "','ModalPopUp');";
                link.NavigateUrl = "#";
                
              Label label=  (Label)gridRow.FindControl("lblstatus");
                if (label.Text.Contains("Not Prepared"))
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
    protected void lnk_Click(object sender, EventArgs e)
    {

        Response.Redirect("rptAutoexpense_Vacant_Entry.aspx");


    }

}