using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Options_DCREdit : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsDCR = null;
    DataSet  dsSalesForce = null;
    string sfCode = string.Empty;
    DataSet dsTP = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
                DateTime FromMonth = DateTime.Now;
                txtMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
            }


            //GenerateMonth_year();
            FillSalesForce();
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            //hHeading.InnerText = Page.Title;
            
        }

    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_New(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }

    private void FillTourPlan()
    {
        //if ((ddlMonth.SelectedIndex > 0) && (ddlYear.SelectedIndex > 0))
        //{
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            DCR dc = new DCR();
            dsDCR = dc.getDCREdit(ddlFieldForce.SelectedValue.ToString(), MonthVal.ToString(), YearVal.ToString());
            if (dsDCR.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = true;
                grdTP.Visible = true;
                grdTP.DataSource = dsDCR;
                grdTP.DataBind();
            }
            else
            {
                btnSubmit.Visible = false;
                grdTP.DataSource = null;
                grdTP.DataBind();
            }
       // }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillTourPlan();
       // btnSubmit.Visible = true;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        foreach (GridViewRow gridRow in grdTP.Rows)
        {
            Label lblTrans_SlNo = (Label)gridRow.Cells[0].FindControl("lblTrans_SlNo");
            CheckBox chkDate = (CheckBox)gridRow.Cells[1].FindControl("chkDate");
            //Label lblDate = (Label)gridRow.Cells[2].FindControl("lblDate");
            
            DateTime dtDCR = Convert.ToDateTime(chkDate.Text);
            string editdate = dtDCR.ToString("MM/dd/yyyy");
            if (chkDate.Checked)
            {
                DCR dcr = new DCR();
                //iReturn = dcr.Option_EditDCRDates(ddlFieldForce.SelectedValue.ToString(), Convert.ToInt32(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()), lblTrans_SlNo.Text, editdate);
                iReturn = dcr.Option_EditDCRDates_New(ddlFieldForce.SelectedValue.ToString(), Convert.ToInt32(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()), lblTrans_SlNo.Text, editdate);
            }
        }

        if (iReturn > 0)
        {
            //Response.Write("DCR Edit Dates have been created successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Edit Dates have been created successfully');</script>");
            FillTourPlan();
        }
    }

    protected void lnk_Click(object sender, EventArgs e)
    {
        //lblfrom.Visible = true;
        //txtFromdte.Visible = true;
        //lblto.Visible = true;
        //txtTodte.Visible = true;
        //btnSub.Visible = true;

        //lblMonth.Visible = false;
        //Span1.Visible = false;
        //ddlMonth.Visible = false;
        //lblYear.Visible = false;
        //ddlYear.Visible = false;
        //btnGo.Visible = false;
        //grdTP.Visible = false;
        //btnSubmit.Visible = false;
        fill_data();
        Session["lnk_Click"] = "lnk_Click";
    }

    protected void btnSub_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        string strQry = string.Empty;

        DB_EReporting db = new DB_EReporting();
        DB_EReporting db_ER = new DB_EReporting();
        DataTable dsDate = new DataTable();

        if (rdodate.SelectedValue == "1")
        {
            for (DateTime date = Convert.ToDateTime(txtFromdte.Text); date.Date <= Convert.ToDateTime(txtTodte.Text); date = date.AddDays(1))
            {
                DateTime dd = date;


                strQry = "Insert into DCR_MissedDates (Sf_Code,Month,Year,Dcr_Missed_Date,Status,Missed_Created_Date,Missed_Release_Date,Finished_Date,Released_by_Whom,Division_Code) values " +
                         " ('" + ddlFieldForce.SelectedValue + "','" + dd.Month + "','" + dd.Year + "','" + dd.ToString("MM/dd/yyyy") + "',1,getdate(),getdate(),null,'admin','" + div_code + "' )";

                iReturn = db.ExecQry(strQry);
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Inserted Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }
        }

        else if (rdodate.SelectedValue == "2")
        {

            strQry = "update mas_salesforce set Sf_TP_DCR_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "',sf_TP_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "' where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);


            for (DateTime date = Convert.ToDateTime(txtFromdte.Text); date.Date <= Convert.ToDateTime(txtTodte.Text); date = date.AddDays(1))
            {
                DateTime dd = date;


                strQry = "Insert into DCR_MissedDates (Sf_Code,Month,Year,Dcr_Missed_Date,Status,Missed_Created_Date,Missed_Release_Date,Finished_Date,Released_by_Whom,Division_Code) values " +
                         " ('" + ddlFieldForce.SelectedValue + "','" + dd.Month + "','" + dd.Year + "','" + dd.ToString("MM/dd/yyyy") + "',1,getdate(),getdate(),null,'admin','" + div_code + "' )";

                iReturn = db.ExecQry(strQry);
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Inserted Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }
        }

        else if (rdodate.SelectedValue == "3")
        {
            strQry = "update mas_salesforce set Sf_TP_DCR_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "',sf_TP_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "' where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);

            strQry = "update mas_salesforce set last_tp_date='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "'  where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);


            for (DateTime date = Convert.ToDateTime(txtFromdte.Text); date.Date <= Convert.ToDateTime(txtTodte.Text); date = date.AddDays(1))
            {
                DateTime dd = date;


                strQry = "Insert into DCR_MissedDates (Sf_Code,Month,Year,Dcr_Missed_Date,Status,Missed_Created_Date,Missed_Release_Date,Finished_Date,Released_by_Whom,Division_Code) values " +
                         " ('" + ddlFieldForce.SelectedValue + "','" + dd.Month + "','" + dd.Year + "','" + dd.ToString("MM/dd/yyyy") + "',1,getdate(),getdate(),null,'admin','" + div_code + "' )";

                iReturn = db.ExecQry(strQry);
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Inserted Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }
        }

        else if (rdodate.SelectedValue == "4")
        {
            strQry = "update mas_salesforce set Sf_TP_DCR_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "',sf_TP_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "' where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }

        }

        else if (rdodate.SelectedValue == "5")
        {
            strQry = "update mas_salesforce set last_tp_date='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "'  where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }
        }
        else if (rdodate.SelectedValue == "6")
        {

            strQry = "update mas_salesforce set Sf_TP_DCR_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "',sf_TP_Active_Dt='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "' where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);

            strQry = "update mas_salesforce set last_tp_date='" + Convert.ToDateTime(txtFromdte.Text).ToString("MM/dd/yyyy") + "'  where sf_code='" + ddlFieldForce.SelectedValue + "' ";
            iReturn = db.ExecQry(strQry);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                txtFromdte.Text = "";
                txtTodte.Text = "";
                rdodate.ClearSelection();
            }
        }

    }

    //private void GenerateMonth_year()
    //{
    //    ddlMonth.Items.Clear();
    //    ddlYear.Items.Clear();

    //    int months = 12;

    //    for (int i = 1; i <= months; i++)
    //    {
    //        if (i == DateTime.Now.Month)
    //        {
    //            if (i == 1)
    //            {
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(12).Substring(0, 3), (12).ToString()));
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //                ddlYear.Items.Add(DateTime.Now.AddYears(-1).Year.ToString());
    //                ddlYear.Items.Add(DateTime.Now.Year.ToString());

    //            }
    //            else
    //            {
    //                //ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i - 2).Substring(0, 3), (i - 2).ToString()));
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i - 1).Substring(0, 3), (i - 1).ToString()));
    //                ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //                ddlYear.Items.Add(DateTime.Now.Year.ToString());
    //            }

    //        }
    //    }
    //    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //    ddlYear.SelectedValue = DateTime.Now.Year.ToString();


    //}
    //protected void lblMonth_Click(object sender, EventArgs e)
    //{
    //    fill_monthyear();
    //}

    //private void fill_monthyear()
    //{

    //    ddlMonth.Items.Clear();
    //    ddlYear.Items.Clear();

    //    TourPlan tp = new TourPlan();
    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlYear.Items.Add(k.ToString());

    //        }

    //        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //    }

    //    int months = 12;

    //    for (int i = 1; i <= months; i++)
    //    {
    //        ddlMonth.Items.Add(new ListItem(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i).Substring(0, 3), i.ToString()));
    //    }

    //    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //}

    protected void ddlFieldForce_indexchanged(object sender, EventArgs e)
    {
        if (Session["lnk_Click"] != null && Session["lnk_Click"] == null)
        {
            fill_data();
        }

    }

    private void fill_data()
    {
        rdodate.Visible = true;
        lblmode.Visible = true;

        //lblMonth.Visible = false;
        //ddlMonth.Visible = false;
        //lblYear.Visible = false;
        //ddlYear.Visible = false;
        btnGo.Visible = false;
        grdTP.Visible = false;
        btnSubmit.Visible = false;

        lbljoing.Visible = true;
        txtjoing.Visible = true;
        lblreport_start.Visible = true;
        txtreport_start.Visible = true;
        lbltp.Visible = true;
        txtTP.Visible = true;

        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;

        strQry = "select convert(varchar,Sf_Joining_Date,103) as Sf_Joining_Date,convert(varchar,Sf_TP_DCR_Active_Dt,103) as Sf_TP_DCR_Active_Dt from mas_salesforce where sf_code='" + ddlFieldForce.SelectedValue + "'";
        DataTable dsDate = db_ER.Exec_DataTable(strQry);

        if (dsDate.Rows.Count > 0)
        {
            txtjoing.Text = dsDate.Rows[0]["Sf_Joining_Date"].ToString();
            txtreport_start.Text = dsDate.Rows[0]["Sf_TP_DCR_Active_Dt"].ToString();
        }


        strQry = "select convert(varchar,last_tp_date,103) as last_tp_date from mas_salesforce where sf_code='" + ddlFieldForce.SelectedValue + "'";
        DataTable dsDate2 = db_ER.Exec_DataTable(strQry);
        if (dsDate2.Rows.Count > 0)
        {
            txtTP.Text = dsDate2.Rows[0]["last_tp_date"].ToString();
        }
    }
    protected void img_dcr_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("DCR_Delete_New.aspx");
    }



}