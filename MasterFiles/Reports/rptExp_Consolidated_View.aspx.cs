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

public partial class MasterFiles_Reports_rptExp_Consolidated_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = new DataSet();
    string divcode = string.Empty;
    string sfcode = string.Empty;

    double DA = 0;
    double Fare = 0;
    double Miscellaneous = 0;
    double Applied = 0;
    double AddDet = 0;
    double Confirm = 0;

    string strMonth = string.Empty;
    string strYear = string.Empty;
    string strFieldForce = string.Empty;
	int StrVac = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        strFieldForce = Request.QueryString["sf_name"].ToString();
        strMonth = Request.QueryString["cur_month"].ToString();
        strYear = Request.QueryString["cur_year"].ToString();
        sfcode = Request.QueryString["sf_code"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
		StrVac = Convert.ToInt16(Request.QueryString["StrVac"].ToString());
        Distance_calculation dv = new Distance_calculation();
        DataTable ds = dv.getFieldForce(divcode, sfcode);

        lblHead.Text = "Expense Consolidated View for the Month of " + getMonthName(Convert.ToInt16(strMonth)) + " - " + strYear.ToString();

        lblFieldForceName.Text = "Field Force Name : <span style='color:#696d6e'>" + strFieldForce + "</span>";
        lblHQ.Text = "HQ : <span style='color:#696d6e'>" + ds.Rows[0]["sf_hq"].ToString() + "</span>";
        lblDesig.Text = "Designation : <span style='color:#696d6e'>" + ds.Rows[0]["sf_designation_short_name"].ToString() + "</span>";
        if (StrVac == 0)
        {
            dsSubDivision = dv.DCR_get_Expense_Approval_Vacant(divcode, sfcode, "", strMonth, strYear);
        }
        else
        {
            dsSubDivision = dv.DCR_get_Expense_Approval(divcode, sfcode, "", strMonth, strYear);
        }
        DataTable dtAllFare = dv.getAllowFare_View(strMonth, strYear);
        DataTable dt = dv.getOtherExpDetails(strMonth, strYear);
        DataTable dt1 = dv.getFixedClmnName(divcode);
        DataTable dt2 = dv.getmis(strMonth, strYear);
        DataTable dt3 = dv.getApproveamnt(strMonth, strYear);
        DataTable dt4 = dv.getAdmnAdjust(strMonth, strYear);
        DataTable dtAllHQ = dv.getAllowDaysHQ(strMonth, strYear);
        DataTable dtAllEX = dv.getAllowDaysEX(strMonth, strYear);
        DataTable dtAllOS = dv.getAllowDaysOS(strMonth, strYear);
        DataTable mainTable = dsSubDivision.Tables[0];


        string strRbnValue = string.Empty;
        string strRbnText = string.Empty;

        //for (int i = 0; i < rbnStatus.Items.Count; i++)
        //{
        //    if (rbnStatus.Items[i].Selected)
        //    {
        //        if (rbnStatus.Items[i].Value != "-1")
        //        {
        //            strRbnValue += rbnStatus.Items[i].Value + ",";
        //            strRbnText += rbnStatus.Items[i].Text + ",";
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}

        if (strRbnValue.Contains(","))
        {
            strRbnValue = strRbnValue.Remove(strRbnValue.Length - 1);
            strRbnText = strRbnText.Remove(strRbnText.Length - 1);
        }


        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("Fixed_Column1");
        mainTable.Columns.Add("Fixed_Column2");
        mainTable.Columns.Add("Fixed_Column3");
        mainTable.Columns.Add("Fixed_Column4");
        mainTable.Columns.Add("Fixed_Column5");
        mainTable.Columns.Add("Fixed_Column6");
        mainTable.Columns.Add("mis_Amt");
        mainTable.Columns.Add("rw_amt");
        mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        mainTable.Columns.Add("HQ_Days");
        mainTable.Columns.Add("EX_Days");
        mainTable.Columns.Add("OS_Days");
        mainTable.Columns.Add("appAmnt");
        mainTable.Columns.Add("mgr_flag");
        mainTable.Columns.Add("confirmAmnt");
        mainTable.Columns.Add("Expense_Distance");
        if (mainTable.Rows.Count > 0)
        {

            //            double totClaimedAmnt = 0;
            foreach (DataRow row in mainTable.Rows)
            {

                double totClaimedAmnt = 0;
                double totApprovedAmnt = 0;
                double totConfirmedAmnt = 0;
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
                DataRow[] rows = dtAllFare.Select(filter);
                DataRow[] othRows = dt.Select(filter);
                DataRow[] misRows = dt2.Select(filter);
                DataRow[] appRows = dt3.Select(filter);
                DataRow[] admRows = dt4.Select(filter);
                DataRow[] row11 = dtAllHQ.Select(filter);
                DataRow[] row22 = dtAllEX.Select(filter);
                DataRow[] row33 = dtAllOS.Select(filter);
                string st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                int status = 0;
                if (rows.Count() > 0)
                {
                    if (row11.Count() > 0)
                    {
                        row["HQ_Days"] = row11[0]["allw"];
                    }
                    if (row22.Count() > 0)
                    {
                        row["EX_Days"] = row22[0]["allw"];
                    }
                    if (row33.Count() > 0)
                    {
                        row["OS_Days"] = row33[0]["allw"];
                    }
                    // row["mgr_flag"] = rows[0]["mgr_flag"];
                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    row["rw_amt"] = rows[0]["rw_amt"];
                    row["Expense_Distance"] = rows[0]["Expense_Distance"];
                    st = rows[0]["Status"].ToString();
                    status = Convert.ToInt32(st);
                    //if (st == "5")
                    //{
                    //    st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                    //}
                    //else if (st == "6")
                    //{
                    //    st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    //}
                    //else if (st == "4")
                    //{
                    //    st = "<span style='background-color:yellow;font-weight:bold'>Approval Pending</span>";
                    //}
                    //else if (st == "1" || st == "2")
                    //{
                    //    st = "<span style='color:blue;font-weight:bold'>Mgr Approval Pending</span>";
                    //}
                    //else
                    //{
                    //    st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                    //}

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString());
                    totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString());

                    if (rows[0]["level2_code"].ToString() == "")
                    {                        
                        totApprovedAmnt = Convert.ToDouble(rows[0]["level1"].ToString() == "" ? "0" : rows[0]["level1"].ToString());
                    }
                    else
                    {                        
                        totApprovedAmnt = Convert.ToDouble(rows[0]["level2"].ToString() == "" ? "0" : rows[0]["level2"].ToString());

                    }
                   // totConfirmedAmnt = Convert.ToDouble(rows[0]["confirm_Total"].ToString() == "" ? "0" : rows[0]["confirm_Total"].ToString());
                }
                row["Status"] = st;
                if (misRows.Count() > 0)
                {
                    row["mis_Amt"] = misRows[0]["mis_Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
                    totApprovedAmnt = totApprovedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
                    totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
                }
                for (int i = 0; i < othRows.Count(); i++)
                {
                    row["Fixed_Column" + (i + 1)] = othRows[i]["Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
                    totApprovedAmnt = totApprovedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
                    totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
                }

                double iAdd = 0;
                double iDet = 0;
                for (int i = 0; i < admRows.Count(); i++)
                {
                    if (admRows[i]["typ"].ToString() == "0")
                    {
                        totConfirmedAmnt = totConfirmedAmnt - Convert.ToDouble(admRows[i]["amt"].ToString());
                        iDet = iDet + Convert.ToDouble(admRows[i]["amt"].ToString());
                    }
                    else if (admRows[i]["typ"].ToString() == "1")
                    {
                        totConfirmedAmnt = totConfirmedAmnt + Convert.ToDouble(admRows[i]["amt"].ToString());
                        iAdd = iAdd + Convert.ToDouble(admRows[i]["amt"].ToString());
                    }
                }

              double Final= iAdd - iDet;
              if (Final == 0)
              {
                  row["appAmnt"] = "";
              }
              else
              {
                  row["appAmnt"] = Final;
              }
                //if (appRows.Count() > 0)
                //{
                if (status == 2)
                {
                    //row["confirmAmnt"] = appRows[0]["grand_total"];
                    row["confirmAmnt"] = totConfirmedAmnt;
                }
                //}
                if (status > 0)
                    row["tot"] = totClaimedAmnt;
               // if (status > 1)
                    //row["appAmnt"] = totApprovedAmnt;



            }
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                grdExpense.Columns[12 + i].Visible = true;
                grdExpense.Columns[12 + i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

            }

            if (strRbnText != "")
            {
                DataSet ds1 = new DataSet();

                mainTable.DefaultView.RowFilter = " status in ('" + strRbnText + "') ";

                //break;
            }

            
            //pnlprint.Visible = true;
            grdExpense.Visible = true;
            grdExpense.DataSource = mainTable;
            grdExpense.DataBind();
            //GrdViewExpense.DataSource = mainTable;
            //GrdViewExpense.DataBind();

            int K = 0;
            foreach (GridViewRow gridRow in grdExpense.Rows)
            {
                K += 1;
                //TableCell cell = new TableCell();
                HyperLink link = new HyperLink();
                Label lbl = new Label();
                Label lblSNo = (Label)gridRow.FindControl("lblSNo");
                Label name = (Label)gridRow.FindControl("lblSF_Name");
                Label code = (Label)gridRow.FindControl("lblSF_Code");
                //HiddenField mgrCode = (HiddenField)gridRow.FindControl("mgrFlagHidden");

                DataSet dsSf = new DataSet();
                SalesForce sf1 = new SalesForce();
                dsSf = sf1.CheckSFNameVacant(code.Text, Convert.ToInt16(strMonth), Convert.ToInt16(strYear));

                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    string[] strVacant = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

                    if (strVacant.Count() >= 2)
                    {
                        if ("( " + strVacant[0].Trim() + " )" != strVacant[1].Trim())
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
                        //if (chkVacant.Checked == true)
                        //{
                        //    gridRow.Visible = false;
                        //    K = K - 1;
                        //}
                    }



                }
                else
                {
                    //dsSf = sf1.CheckSFName_DCREntry_Check(strSF_Code, Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
                    lbl.Text = name.Text;
                }

                link.Text = "<span>" + lbl.Text + "</span>";

                //link.Text = "<span>" + name.Value + "</span>";
                //lbl.Text = name.Value;
                //string sURL;
                //if (mgrCode.Value == "1")
                //{
                //    sURL = "RptAutoExpenseApprove_View_Mgr.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                //}
                //else
                //{
                //    sURL = "RptAutoExpense_view.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                //}
                //link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                //link.NavigateUrl = "#";
                ////cell.Controls.Add(link);
                //Label label = (Label)gridRow.FindControl("lblstatus");
                //if (label.Text.Contains("Not Prepared") || label.Text.Contains("Mgr Approval Pending"))
                //{
                //    gridRow.Cells[1].Controls.Add(lbl);
                //}
                //else
                //{
                //    gridRow.Cells[1].Controls.Add(link);

                //}
                //lblSNo.Text = K.ToString();
            }




        }
        else
        {
            grdExpense.DataSource = dsSubDivision;
            grdExpense.DataBind();
            //GrdViewExpense.DataSource = dsSubDivision;
            //GrdViewExpense.DataBind();
        }
    }

    protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string str = string.Empty;
            string con_Fare = string.Empty;
            string con_Mis = string.Empty;

            Label lblsfAll = (Label)e.Row.FindControl("lblsfAll");
            if (lblsfAll.Text != "")
            {
                DA += Convert.ToDouble(lblsfAll.Text);
            }

            Label lblFare = (Label)e.Row.FindControl("lblsfFare");
            if (lblFare.Text != "")
            {
                Fare += Convert.ToDouble(lblFare.Text);
            }

            Label lblmisamt = (Label)e.Row.FindControl("lblmisamt");
            if (lblmisamt.Text != "")
            {
                Miscellaneous += Convert.ToDouble(lblmisamt.Text);
            }

            Label lblAppliedAmnt = (Label)e.Row.FindControl("lblAppliedAmnt");
            if (lblAppliedAmnt.Text != "")
            {
                Applied += Convert.ToDouble(lblAppliedAmnt.Text);
            }

            Label lblAddDetAmnt = (Label)e.Row.FindControl("lblAddDetAmnt");
            if (lblAddDetAmnt.Text != "")
            {
                AddDet += Convert.ToDouble(lblAddDetAmnt.Text);
            }

            Label lblConfirmAmnt = (Label)e.Row.FindControl("lblConfirmAmnt");
            if (lblConfirmAmnt.Text != "")
            {
                Confirm += Convert.ToDouble(lblConfirmAmnt.Text);
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label ftlblAll = (Label)e.Row.FindControl("ftlblAll");
            if (DA != 0)
            {
                ftlblAll.Text = DA.ToString();
            }

            Label ftlblFare = (Label)e.Row.FindControl("ftlblFare");
            if (Fare != 0)
            {
                ftlblFare.Text = Fare.ToString();
            }

            Label ftlblmisamt = (Label)e.Row.FindControl("ftlblmisamt");
            if (Miscellaneous != 0)
            {
                ftlblmisamt.Text = Miscellaneous.ToString();
            }

            Label ftlblAppliedAmt = (Label)e.Row.FindControl("ftlblAppliedAmt");
            if (Applied != 0)
            {
                ftlblAppliedAmt.Text = Applied.ToString();
            }

            Label ftlblAddDetAmnt = (Label)e.Row.FindControl("ftlblAddDetAmnt");
            if (AddDet != 0)
            {
                ftlblAddDetAmnt.Text = AddDet.ToString();
            }

            Label ftlblConfirmAmnt = (Label)e.Row.FindControl("ftlblConfirmAmnt");
            if (Confirm != 0)
            {
                ftlblConfirmAmnt.Text = Confirm.ToString();
            }
        }



    }

    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
}