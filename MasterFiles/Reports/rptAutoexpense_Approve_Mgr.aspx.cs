using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Reports_rptAutoexpense_Approve_Mgr : System.Web.UI.Page
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
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);

        Distance_calculation dist = new Distance_calculation();
        table = dist.getMgrAppr(divcode);

        if (table.Rows.Count > 0)
        {
            if ("0".Equals(table.Rows[0]["MgrAppr_Remark"].ToString()))
            {
                msgId.Visible = true;
                MainId.Visible = false;
                return;
            }
            else
            {
                msgId.Visible = false;
                MainId.Visible = true;

            }
        }
        else
        {
            msgId.Visible = true;
            return;
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            // usc_MR.FindControl("btnBack").Visible = false;
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
              //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";
            //btnBack.Visible = true;

        }
        else
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MGR_Menu c1 =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            //btnBack.Visible = true;
            c1.Title = this.Page.Title;
            //   Session["backurl"] = "LstDoctorList.aspx";
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
              //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";

        }

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;
            FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
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
        for (int intCount = 2015; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private void FillFieldForcediv(string divcode)
    {
        //Distance_calculation dv = new Distance_calculation();
        SalesForce dv = new SalesForce();
        //dsSubDivision = dv.getRegion(divcode);
        dsSubDivision = dv.getSfName(sfcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();
        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        Distance_calculation dv = new Distance_calculation();
        SalesForce sf = new SalesForce();
        dsSubDivision = sf.SalesForceList_New(divcode, ddlSubdiv.SelectedValue.ToString());
        DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetails(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt1 = dv.getFixedClmnName(divcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt3 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable = dsSubDivision.Tables[0];

        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("Fixed_Column1");
        mainTable.Columns.Add("Fixed_Column2");
        mainTable.Columns.Add("Fixed_Column3");
        mainTable.Columns.Add("Fixed_Column4");
        mainTable.Columns.Add("Fixed_Column5");
        mainTable.Columns.Add("mis_Amt");
        mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        mainTable.Columns.Add("appAmnt");
        mainTable.Columns.Add("mgr_flag");
        mainTable.Columns.Add("submission_date");
        mainTable.Columns.Add("Approval_Datea");
        mainTable.Columns.Add("admin_approval_date");
        mainTable.Columns.Add("reject_date");
        mainTable.Columns.Add("resibmit_date");
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
                }
                string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                if (rows.Count() > 0)
                {
                    //row["mgr_flag"] = rows[0]["mgr_flag"];
                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    row["submission_date"] = rows[0]["submission_date"];
                    row["Approval_Datea"] = rows[0]["Approval_Datea"];
                    row["admin_approval_date"] = rows[0]["admin_approval_date"];
                    row["reject_date"] = rows[0]["reject_date"];
                    row["resibmit_date"] = rows[0]["resibmit_date"];
                    st = rows[0]["Status"].ToString();
                     if (st == "6" || st=="2")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                     else if (st == "8" || st == "3")
                     {
                         if (table.Rows.Count > 0)
                         {
                             if ("1".Equals(table.Rows[0]["MgrAppr_Sameadmin"].ToString()))
                             {
                                 st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                             }
                             else
                             {
                                 st = "<span style='color:green;font-weight:bold'>Admin Approval Pending</span>";

                             }
                         }
                         else
                         {
                             st = "<span style='color:green;font-weight:bold'>Admin Approval Pending</span>";
                         }
                        // st = "<span style='color:green;font-weight:bold'>Admin Approval Pending</span>";
                     }
                     else if (st == "10")
                     {
                         st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Rejected</span>";
                     }
                    else if (st == "7")
                    {
                        st = "<span style='background-color:Yellow;font-weight:bold'>Approval Pending</span>";
                    }
                    else
                    {
                        st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                    }

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString());
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
                grdSalesForce.Columns[12 + i].Visible = true;
                grdSalesForce.Columns[12 + i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

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
                HiddenField mgrCode = (HiddenField)gridRow.FindControl("mgrFlagHidden");

                DataSet dsSf = new DataSet();
                SalesForce sf1 = new SalesForce();
                dsSf = sf1.CheckSFNameVacant_Temp(code.Value, Convert.ToInt16(monthId.SelectedValue), Convert.ToInt16(yearID.SelectedValue));

                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    string[] strVacant = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

                    if (strVacant.Count() >= 2)
                    {
                        if (strVacant[0] != strVacant[1])
                        {
                            lbl.Text = strVacant[0] + "<span style='color: red;'>" + strVacant[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                        }
                        else
                        {
                            lbl.Text = strVacant[0];
                        }
                    }
                    else
                    {

                        lbl.Text = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                    }

                    if (dsSf.Tables[0].Rows[0][2].ToString() != "")
                    {
                        lbl.Text = "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";
                    }

                }
                else
                {
                    //dsSf = sf1.CheckSFName_DCREntry_Check(strSF_Code, Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
                    lbl.Text = name.Value;
                }

                link.Text = "<span>" + lbl.Text + "</span>";

                //string sURL = "RptAutoExpense_view.aspx?sf_code=" + code.Value+ "&monthId="+monthId.SelectedValue.ToString()+"&yearId="+yearID.SelectedValue.ToString()+"&divCode="+divcode+"";
                string sURL;
                if (table.Rows.Count > 0)
                {
                    if ("1".Equals(table.Rows[0]["MgrAppr_Sameadmin"].ToString()))
                    {
                        sURL = "RptAutoExpense_view.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";

                    }
                    else
                    {
                        sURL = "RptAutoExpense_view_Mgr.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";

                    }
                }
                else
                {
                    sURL = "RptAutoExpense_view_Mgr.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";

                }
                    //sURL = "RptAutoExpense_view_Mgr.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                link.NavigateUrl = "#";
                //cell.Controls.Add(link);
                Label label = (Label)gridRow.FindControl("lblstatus");
                if (label.Text.Contains("Not Prepared"))
                {
                    gridRow.Cells[1].Controls.Add(lbl);
                }
                else
                {
                    gridRow.Cells[1].Controls.Add(link);

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