using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    bool isSavedPage = false;
    string sfcode = "";
    string monthId = "";
    string yearID = "";
    string divCode = "";
    string gt = "";
    double amtfix = 0;
    double amtoth = 0;
    double amtadmn = 0;
    double Final = 0;
    Hashtable months = new Hashtable();
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {
        months.Add("1", "January");
        months.Add("2", "Feb");
        months.Add("3", "March");
        months.Add("4", "April");
        months.Add("5", "May");
        months.Add("6", "June");
        months.Add("7", "July");
        months.Add("8", "August");
        months.Add("9", "Sept");
        months.Add("10", "October");
        months.Add("11", "November");
        months.Add("12", "Decemeber");

        gt = grandTotalName.InnerHtml;
        sfcode = Request.QueryString["sf_code"].ToString();
        divCode = Request.QueryString["divCode"].ToString();
        monthId = Request.QueryString["month"].ToString();
        yearID = Request.QueryString["year"].ToString();
        mnthtxtId.InnerHtml = months[monthId.ToString()].ToString();
        yrtxtId.InnerHtml = yearID;
        if (divCode == "19" || divCode == "20")
        {
            grdExpMain.Columns[9].Visible = false;
        }
        DataTable rt = Exp.getMgrAppr(divCode);
        if (rt.Rows.Count > 0)
        {
            if ("Y".Equals(rt.Rows[0]["Row_wise_textbox"].ToString()))
            {
                grdExpMain.Columns[11].Visible = true;
                grdExpMain.Columns[12].Visible = true;

            }
        }
        DataTable table = Exp.getSavedHeadRecord(monthId, yearID, sfcode);

        if (table.Rows.Count > 0)
        {
            if ("2".Equals(table.Rows[0]["sndhqfl"].ToString()))
            {
                msgId.Visible = false;
                MainId.Visible = true;

            }
            else
            {
                msgId.Visible = true;
                MainId.Visible = false;

            }
        }
        else
        {
            msgId.Visible = true;
            return;
        }

        DataTable ds = Exp.getFieldForce(divCode, sfcode);
        if ("2".Equals(ds.Rows[0]["sf_type"].ToString()))
        {
            grdExpMain.Columns[7].Visible = false;
            grdExpMain.Columns[10].Visible = true;
            grdExpMain.Columns[11].Visible = true;


        }
        fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
        hqId.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
        empId.InnerHtml = "Employee_ID :" + ds.Rows[0]["Employee_Id"].ToString();
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        DataTable headerDataSet = Exp.getSavedHeadRecord(monthId, yearID, sfcode);
        if (headerDataSet.Rows.Count > 0)
        {
            string rms = headerDataSet.Rows[0]["Admin_Remarks"].ToString();
            approveTextId.InnerText = rms;
            string rms1 = headerDataSet.Rows[0]["Remarks"].ToString();
            MgrTextId.InnerText = rms1;
            //lblrmks1.InnerText = rms;
        }
        DataTable pm = Exp.getAdmnAdjust(monthId, yearID);
        String filter = "SF_Code='" + sfcode + "'";
        DataRow[] admRows = pm.Select(filter);

        double iAdd = 0;
        double iDet = 0;
        for (int i = 0; i < admRows.Count(); i++)
        {
            if (admRows[i]["typ"].ToString() == "0")
            {

                iDet = iDet + Convert.ToDouble(admRows[i]["amt"].ToString());
            }
            else if (admRows[i]["typ"].ToString() == "1")
            {

                iAdd = iAdd + Convert.ToDouble(admRows[i]["amt"].ToString());
            }
        }

        Final = iAdd - iDet;
        DataTable t1 = Exp.getSavedRecord(monthId, yearID, sfcode, dt3);
        double totalAllowance = 0;
        double totalDistance = 0;
        double totalFare = 0;
        double totaddExp = 0;
        double grandTotal = 0;
        foreach (DataRow row in t1.Rows)
        {
            totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
            totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
            totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
            totaddExp = totaddExp + Convert.ToDouble(row["rw_amount"].ToString());
            grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
        }
        t1.Rows.Add();
        t1.Rows[t1.Rows.Count - 1]["Allowance"] = totalAllowance;
        t1.Rows[t1.Rows.Count - 1]["Distance"] = totalDistance;
        t1.Rows[t1.Rows.Count - 1]["Fare"] = totalFare;
        t1.Rows[t1.Rows.Count - 1]["rw_amount"] = totaddExp;
        t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


        misExp.Visible = true;
        grdExpMain.Visible = true;
        grdExpMain.DataSource = t1;
        grdExpMain.DataBind();
        //generateOtherExpControls(Exp);
        double otherExAmnt = 0;
        DataTable customExpTable = Exp.getSavedFixedExp(monthId, yearID, sfcode);
        otherExpGrid.DataSource = customExpTable;
        otherExpGrid.DataBind();
        foreach (DataRow r in customExpTable.Rows)
        {
            otherExAmnt = otherExAmnt + Convert.ToDouble(r["amount"].ToString());

        }

        DataTable dtExp = Exp.getSavedOtheExpRecord(monthId, yearID, sfcode, dt3);
        expGrid.DataSource = dtExp;
        expGrid.DataBind();
        foreach (DataRow r in dtExp.Rows)
        {
            otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());

        }
        DataTable adminExp = Exp.getSavedAdminExpRecord(monthId, yearID, sfcode);
        adminExpGrid.DataSource = adminExp;
        adminExpGrid.DataBind();
        foreach (DataRow r in adminExp.Rows)
        {
            if (r["typ"].ToString() == "1")
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());
            }
            else if (r["typ"].ToString() == "0")
            {
                otherExAmnt = otherExAmnt - Convert.ToDouble(r["amt"].ToString());
            }

        }
        double tot = otherExAmnt + grandTotal;
        grandTotalName.InnerHtml = tot.ToString();
        DataTable t2 = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);

        if (t2.Rows.Count > 0)
        {
            isSavedPage = true;
            grandTotalName.InnerHtml = t2.Rows[0]["grand_total"].ToString();
        }
        foreach (GridViewRow gridRow in adminExpGrid.Rows)
        {
            Label lblTyp = (Label)gridRow.FindControl("lblTyp");
            if (lblTyp.Text == "0")
            {
                lblTyp.Text = " - ";
            }
            else if (lblTyp.Text == "1")
            {
                lblTyp.Text = " + ";
            }
        }

    }

    protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblSexpAmnt = (Label)e.Row.FindControl("lblSexpAmnt");
            if (lblSexpAmnt.Text != "")
            {
                amtfix += Convert.ToDouble(lblSexpAmnt.Text);
            }




        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label ftlblSexpAmnt = (Label)e.Row.FindControl("ftlblSexpAmnt");
            if (amtfix != 0)
            {
                ftlblSexpAmnt.Text = amtfix.ToString();
            }


        }



    }
    protected void expGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            if (lblAmount.Text != "")
            {
                amtoth += Convert.ToDouble(lblAmount.Text);
            }




        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label ftlblAmount = (Label)e.Row.FindControl("ftlblAmount");
            if (amtoth != 0)
            {
                ftlblAmount.Text = amtoth.ToString();
            }


        }



    }
    protected void adminExpGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblAmount = (Label)e.Row.FindControl("lblAmount");
            if (lblAmount.Text != "")
            {
                amtadmn += Convert.ToDouble(lblAmount.Text);
            }




        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label ftlblAmount = (Label)e.Row.FindControl("ftlblAmount");
            if (amtadmn != 0)
            {
                ftlblAmount.Text = Final.ToString();
            }


        }



    }

    protected void grdExpMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (sfcode.Contains("MGR"))
            {
                grdExpMain.Columns[7].Visible = false;
            }
            else if (sfcode.Contains("MR"))
            {
                grdExpMain.Columns[8].Visible = false;
            }

        }
    }

}