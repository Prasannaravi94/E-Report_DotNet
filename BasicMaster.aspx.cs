using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class BasicMaster : System.Web.UI.Page
{
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsTP = new DataSet();
    DataSet dsDcr = new DataSet();
    DataSet dsAdminSetup = null;
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        menumas.FindControl("pnlHeader").Visible = false;
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Session["backurl"] = "~/BasicMaster.aspx";
            if (Session["Sub_HO_ID"].ToString() == "NULL" || Session["Sub_HO_ID"].ToString() == "0")
            {
                //FillDoc();
                //FillDcr();
                //FillDoc_Deact();
                //FillTourPlan();
                //FillLeave();
            }
            else
            {
                pnlApproval.Visible = false;
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void FillDoc()
    {
        grdListedDR.DataSource = null;
        grdListedDR.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_MGR(sfCode, 2, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            if (sfCode == "admin")
            {
                grdListedDR.Visible = true;
                grdListedDR.DataSource = dsDoc;
                grdListedDR.DataBind();
            }

        }
        else
        {
            grdListedDR.DataSource = dsDoc;
            grdListedDR.DataBind();
        }
    }

    private void FillDcr()
    {
        grdDCR.DataSource = null;
        grdDCR.DataBind();

        DCR dr = new DCR();
        if (div_code.Contains(','))
            div_code = div_code.Substring(0, div_code.Length - 1);
        dsDcr = dr.get_DCR_Pending_Approval(sfCode, div_code);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            grdDCR.Visible = true;
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
        else
        {
            grdDCR.DataSource = dsDcr;
            grdDCR.DataBind();
        }
    }
    private void FillDoc_Deact()
    {
        grdListedDR_Dea.DataSource = null;
        grdListedDR_Dea.DataBind();

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_MGR(sfCode, 3, div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            if (sfCode == "admin")
            {
                grdListedDR_Dea.Visible = true;
                grdListedDR_Dea.DataSource = dsDoc;
                grdListedDR_Dea.DataBind();
            }
        }
        else
        {
            grdListedDR_Dea.DataSource = dsDoc;
            grdListedDR_Dea.DataBind();
        }
    }

    //private void FillDoc_AddDeactivate()
    //{
    //    grdadddeactivate.DataSource = null;
    //    grdadddeactivate.DataBind();

    //    ListedDR lstAdd = new ListedDR();
    //    dsDoc = lstAdd.getListedDr_adddeact(sfCode, 2, 3, div_code);
    //    if (dsDoc.Tables[0].Rows.Count > 0)
    //    {
    //        grdadddeactivate.Visible = true;
    //        grdadddeactivate.DataSource = dsDoc;
    //        grdadddeactivate.DataBind();
    //    }
    //    else
    //    {
    //        grdadddeactivate.DataSource = dsDoc;
    //        grdadddeactivate.DataBind();
    //    }

    //}
    private void FillLeave()
    {
        grdLeave.DataSource = null;
        grdLeave.DataBind();


        AdminSetup adm = new AdminSetup();
        dsAdminSetup = adm.getLeave_approve(sfCode, 2, div_code);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            grdLeave.Visible = true;
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
        else
        {
            grdLeave.DataSource = dsAdminSetup;
            grdLeave.DataBind();
        }
    }

    private void FillTourPlan()
    {
        //TourPlan tp = new TourPlan();
        TP_New tp = new TP_New();

        dsTP = tp.get_TP_Pending_Approval(sfCode, div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            //string strGetMR = dsTP.Tables[0].Rows[0]["sf_code"].ToString();
            //if (strGetMR.Substring(0, 2) != "MR")
            //{
            grdTP_Calander.Visible = true;
            grdTP_Calander.DataSource = dsTP;
            grdTP_Calander.DataBind();

            //    }
            //    else
            //    {
            //        btnHome.Visible = true;
            //        grdTP.Visible = true;
            //        grdTP.DataSource = dsTP;
            //        grdTP.DataBind();
            //    }
        }
        else
        {

            grdTP.Visible = true;
            grdTP.DataSource = dsTP;
            grdTP.DataBind();
        }


    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ActTerrtotal = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = (Label)e.Row.FindControl("lblMonth");
            lblMonth.Text = getMonthName(lblMonth.Text);
            // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
            ActTerrtotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "sf_code"));
            if (ActTerrtotal.Contains("MR"))
            {
                e.Row.Cells[8].Visible = false;
            }
            else
            {
                e.Row.Cells[7].Visible = false;
            }
        }

    }

    //protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblMonth = (Label)e.Row.FindControl("lblMonth");
    //        lblMonth.Text = getMonthName(lblMonth.Text);
    //        // e.Row.Cells[5].Text = "Click here to Approve " + lblMonth.Text + " "+ dsTP.Tables[0].Rows[0]["Tour_Year"].ToString();
    //    }

    //}
    private string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "January";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "2")
        {
            sReturn = "February";
        }
        else if (sMonth == "3")
        {
            sReturn = "March";
        }
        else if (sMonth == "4")
        {
            sReturn = "April";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "June";
        }
        else if (sMonth == "7")
        {
            sReturn = "July";
        }
        else if (sMonth == "8")
        {
            sReturn = "August";
        }
        else if (sMonth == "9")
        {
            sReturn = "September";
        }
        else if (sMonth == "10")
        {
            sReturn = "October";
        }
        else if (sMonth == "11")
        {
            sReturn = "November";
        }
        else if (sMonth == "12")
        {
            sReturn = "December";
        }

        return sReturn;
    }
}