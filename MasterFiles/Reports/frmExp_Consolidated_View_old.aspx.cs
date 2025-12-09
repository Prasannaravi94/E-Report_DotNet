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
using System.Text;
using Bus_EReport;
using System.Net;

public partial class MasterFiles_frmExp_Consolidated_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = new DataSet();
    string divcode = string.Empty;
    string sfcode = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);

        if (!Page.IsPostBack)
        {
            FillFieldForcediv(divcode);

            FillYear();
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
        }
        

        FillColor();
        
    }
    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();
        dsSubDivision = dv.UserList_Hierarchy(divcode, "admin");
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSubDivision;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();

        }

       
    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        dsTP = tp.Get_TP_Edit_Year(divcode);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    //protected void btnSubmit_Click1(object sender, EventArgs e)
    //{
    //    Session["Value"] = "Value";
    //    Distance_calculation dv = new Distance_calculation();
    //    string str = "";
    //    if (ddlDesign.SelectedValue == "0")
    //    {
    //        str = "";
    //    }
    //    else
    //    {
    //        str = ddlDesign.SelectedValue;
    //    }

    //    dsSubDivision = dv.DCR_get_Expense_Approval(divcode, ddlSubdiv.SelectedValue.ToString(), str, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable dt = dv.getOtherExpDetails(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable dt1 = dv.getFixedClmnName(divcode);
    //    DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable dt3 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable dt4 = dv.getAdmnAdjust(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
    //    DataTable mainTable = dsSubDivision.Tables[0];


    //    string strRbnValue = string.Empty;
    //    string strRbnText = string.Empty;

    //    for (int i = 0; i < rbnStatus.Items.Count; i++)
    //    {
    //        if (rbnStatus.Items[i].Selected)
    //        {
    //            if (rbnStatus.Items[i].Value != "-1")
    //            {
    //                strRbnValue += rbnStatus.Items[i].Value + ",";
    //                strRbnText += rbnStatus.Items[i].Text + ",";
    //            }
    //            else
    //            {
    //                break;
    //            }
    //        }
    //    }

    //    if (strRbnValue.Contains(","))
    //    {
    //        strRbnValue = strRbnValue.Remove(strRbnValue.Length - 1);
    //        strRbnText = strRbnText.Remove(strRbnText.Length - 1);
    //    }


    //    mainTable.Columns.Add("allowance");
    //    mainTable.Columns.Add("fare");
    //    mainTable.Columns.Add("Fixed_Column1");
    //    mainTable.Columns.Add("Fixed_Column2");
    //    mainTable.Columns.Add("Fixed_Column3");
    //    mainTable.Columns.Add("Fixed_Column4");
    //    mainTable.Columns.Add("Fixed_Column5");
    //    mainTable.Columns.Add("mis_Amt");
    //    mainTable.Columns.Add("tot");
    //    mainTable.Columns.Add("Status");
    //    mainTable.Columns.Add("appAmnt");
    //    mainTable.Columns.Add("mgr_flag");
    //    mainTable.Columns.Add("confirmAmnt");
    //    if (mainTable.Rows.Count > 0)
    //    {

    //        //            double totClaimedAmnt = 0;
    //        foreach (DataRow row in mainTable.Rows)
    //        {

    //            double totClaimedAmnt = 0;
    //            double totApprovedAmnt = 0;
    //            double totConfirmedAmnt = 0;
    //            String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
    //            DataRow[] rows = dtAllFare.Select(filter);
    //            DataRow[] othRows = dt.Select(filter);
    //            DataRow[] misRows = dt2.Select(filter);
    //            DataRow[] appRows = dt3.Select(filter);
    //            DataRow[] admRows = dt4.Select(filter);
    //            string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
    //            int status = 0;
    //            if (rows.Count() > 0)
    //            {
    //                row["mgr_flag"] = rows[0]["mgr_flag"];
    //                row["allowance"] = rows[0]["allw"];
    //                row["fare"] = rows[0]["fare"];
    //                st = rows[0]["Status"].ToString();
    //                status = Convert.ToInt32(st);
    //                if (st == "5")
    //                {
    //                    st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
    //                }
    //                else if (st == "6")
    //                {
    //                    st = "<span style='color:green;font-weight:bold'>Approved</span>";
    //                }
    //                else if (st == "4")
    //                {
    //                    st = "<span style='background-color:yellow;font-weight:bold'>Approval Pending</span>";
    //                }
    //                else if (st == "1" || st == "2")
    //                {
    //                    st = "<span style='color:blue;font-weight:bold'>Mgr Approval Pending</span>";
    //                }
    //                else
    //                {
    //                    st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
    //                }

    //                totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString());
    //                totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString());

    //                if (rows[0]["level2_code"].ToString() == "")
    //                {
    //                    //row["appAmnt"] = Convert.ToDouble(rows[0]["level1"].ToString());
    //                    //totApprovedAmnt = Convert.ToDouble(rows[0]["level1"].ToString());
    //                    totApprovedAmnt = Convert.ToDouble(rows[0]["level1"].ToString() == "" ? "0" : rows[0]["level1"].ToString());
    //                }
    //                else
    //                {
    //                    // row["appAmnt"] = Convert.ToDouble(rows[0]["level2"].ToString());
    //                    //totApprovedAmnt = Convert.ToDouble(rows[0]["level2"].ToString());
    //                    totApprovedAmnt = Convert.ToDouble(rows[0]["level2"].ToString() == "" ? "0" : rows[0]["level2"].ToString());

    //                }
    //                totConfirmedAmnt = Convert.ToDouble(rows[0]["confirm_Total"].ToString() == "" ? "0" : rows[0]["confirm_Total"].ToString());
    //            }
    //            row["Status"] = st;
    //            if (misRows.Count() > 0)
    //            {
    //                row["mis_Amt"] = misRows[0]["mis_Amt"];

    //                totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
    //                totApprovedAmnt = totApprovedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
    //                totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
    //            }
    //            for (int i = 0; i < othRows.Count(); i++)
    //            {
    //                row["Fixed_Column" + (i + 1)] = othRows[i]["Amt"];

    //                totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
    //                totApprovedAmnt = totApprovedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
    //                totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
    //            }
    //            for (int i = 0; i < admRows.Count(); i++)
    //            {
    //                if (admRows[i]["typ"].ToString() == "0")
    //                {
    //                    totConfirmedAmnt = totConfirmedAmnt - Convert.ToDouble(admRows[i]["amt"].ToString());
    //                }
    //                else if (admRows[i]["typ"].ToString() == "1")
    //                {
    //                    totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(admRows[i]["amt"].ToString());
    //                }
    //            }
    //            //if (appRows.Count() > 0)
    //            //{
    //            if (status == 6)
    //            {
    //                //row["confirmAmnt"] = appRows[0]["grand_total"];
    //                row["confirmAmnt"] = totConfirmedAmnt;
    //            }
    //            //}
    //            if (status > 0)
    //                row["tot"] = totClaimedAmnt;
    //            if (status > 1)
    //                row["appAmnt"] = totApprovedAmnt;



    //        }
    //        for (int i = 0; i < dt1.Rows.Count; i++)
    //        {
    //            grdSalesForce.Columns[8 + i].Visible = true;
    //            grdSalesForce.Columns[8 + i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

    //        }

    //        if (strRbnText != "")
    //        {
    //            DataSet ds1 = new DataSet();

    //            mainTable.DefaultView.RowFilter = " status in ('" + strRbnText + "') ";

    //            //break;
    //        }

    //        pnlExcel.Visible = true;
    //        //pnlprint.Visible = true;
    //        grdSalesForce.Visible = true;
    //        grdSalesForce.DataSource = mainTable;
    //        grdSalesForce.DataBind();
    //        GrdViewExpense.DataSource = mainTable;
    //        GrdViewExpense.DataBind();

    //        int K = 0;
    //        foreach (GridViewRow gridRow in grdSalesForce.Rows)
    //        {
    //            K += 1;
    //            //TableCell cell = new TableCell();
    //            HyperLink link = new HyperLink();
    //            Label lbl = new Label();
    //            Label lblSNo = (Label)gridRow.FindControl("lblSNo");
    //            HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
    //            HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");
    //            HiddenField mgrCode = (HiddenField)gridRow.FindControl("mgrFlagHidden");

    //            DataSet dsSf = new DataSet();
    //            SalesForce sf1 = new SalesForce();
    //            dsSf = sf1.CheckSFNameVacant(code.Value, Convert.ToInt16(monthId.SelectedValue), Convert.ToInt16(yearID.SelectedValue));

    //            if (dsSf.Tables[0].Rows.Count > 0)
    //            {
    //                string[] strVacant = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

    //                if (strVacant.Count() >= 2)
    //                {
    //                    if ("( " + strVacant[0].Trim() + " )" != strVacant[1].Trim())
    //                    {
    //                        lbl.Text = strVacant[0] + "<span style='color: red;'>" + strVacant[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
    //                    }
    //                    else
    //                    {
    //                        lbl.Text = strVacant[0];
    //                    }
    //                }
    //                else
    //                {
    //                    lbl.Text = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
    //                }

    //                if (dsSf.Tables[0].Rows[0][2].ToString() != "")
    //                {
    //                    lbl.Text = "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";
    //                    if (chkVacant.Checked == true)
    //                    {
    //                        gridRow.Visible = false;
    //                        K = K - 1;
    //                    }
    //                }



    //            }
    //            else
    //            {
    //                //dsSf = sf1.CheckSFName_DCREntry_Check(strSF_Code, Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
    //                lbl.Text = name.Value;
    //            }

    //            link.Text = "<span>" + lbl.Text + "</span>";
    //            //link.Text = "<span>" + name.Value + "</span>";
    //            //lbl.Text = name.Value;
    //            string sURL;
    //            if (mgrCode.Value == "1")
    //            {
    //                sURL = "RptAutoExpenseApprove_View_Mgr.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
    //            }
    //            else
    //            {
    //                sURL = "RptAutoExpense_view.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
    //            }
    //            link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
    //            link.NavigateUrl = "#";
    //            //cell.Controls.Add(link);
    //            Label label = (Label)gridRow.FindControl("lblstatus");
    //            if (label.Text.Contains("Not Prepared") || label.Text.Contains("Mgr Approval Pending"))
    //            {
    //                gridRow.Cells[1].Controls.Add(lbl);
    //            }
    //            else
    //            {
    //                gridRow.Cells[1].Controls.Add(link);

    //            }
    //            lblSNo.Text = K.ToString();
    //        }




    //    }
    //    else
    //    {
    //        grdSalesForce.DataSource = dsSubDivision;
    //        grdSalesForce.DataBind();
    //        GrdViewExpense.DataSource = dsSubDivision;
    //        GrdViewExpense.DataBind();
    //    }
    //}
}