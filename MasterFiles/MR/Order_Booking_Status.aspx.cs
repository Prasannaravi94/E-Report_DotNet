using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_MR_Order_Booking_Status : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    DataSet dsSalesForce = null;
    DataSet dsSalesForcecamp = null;
    DataSet dsSalesForcecore = null;
    DataSet dsSalesForcecore_camp = null;
    DataSet dsSalesForcecrm = null;
    DataSet dsadm = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Activity_Date = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    int iCnt = -1;
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int i = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            div_code = Session["div_code"].ToString();
            sfCode = Session["sf_code"].ToString();
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR = (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                Usc_MR.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu Usc_MGR = (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MGR);
                Usc_MGR.Title = this.Page.Title;
                Usc_MGR.FindControl("btnBack").Visible = false;
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MenuUserControl c1 = (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //Divid.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
            }
            if (!Page.IsPostBack)
            {
                FillMRfor_mr();
                FillYear();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect(ex.Message);
        }
    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void FillMRfor_mr()
    {
        Territory terr = new Territory();
        DataSet dsSalesforce = terr.getSFCode(div_code);
        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();

        }
        // FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillDoc()
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;

        strQry = " EXEC Rpt_Order_Booking_Status '" + div_code + "', '" + ddlFieldForce.SelectedValue.ToString() + "', '" + ddlMonth.SelectedValue.ToString() + "', '" + ddlYear.SelectedValue.ToString() + "' ";
        dsDoc = db_ER.Exec_DataSet(strQry);

        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            btnSave.Visible = true;
            btnReject.Visible = true;
            imgCrosslbl.Visible = true;
            lblvisit.Visible = true;
            imgCross2lbl.Visible = true;
            lblcamp.Visible = true;
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
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


    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        FillDoc();
    }

    protected void grdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.ClassName='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.ClassName='Normal'");
            // e.Row.Cells[1].Attributes.Add("colspan", "1");
        }
    }


    protected void grdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            i = 0;
            CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkListedDR");
            CheckBox chkAll = (CheckBox)e.Row.FindControl("chkAll");
            Image imgCross = (Image)e.Row.FindControl("imgCross");
            Image imgCross1 = (Image)e.Row.FindControl("imgCross1");
            Image imgCross2 = (Image)e.Row.FindControl("imgCross2");
            Image imgCross3 = (Image)e.Row.FindControl("imgCross3");

            Label lblDocCode = (Label)e.Row.FindControl("lblDocCode");
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            Label lblVisitdate = (Label)e.Row.FindControl("lblMode");
            Label lblFlag = (Label)e.Row.FindControl("lblFlag");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");

            if (lblFlag.Text == "1")
            {
                imgCross2.Visible = false;
                chkSelect.Visible = false;
                imgCross3.Visible = false;
                imgCross1.Visible = false;
                imgCross.Visible = true;
                lblStatus.Text = "Processed";
                lblStatus.ForeColor = System.Drawing.Color.ForestGreen;
            }
            else if (lblFlag.Text == "2")
            {
                imgCross2.Visible = true;
                chkSelect.Visible = false;
                imgCross.Visible = false;
                imgCross1.Visible = false;
                imgCross3.Visible = false;
                lblStatus.Text = "Rejected";
                lblStatus.ForeColor = System.Drawing.Color.IndianRed;
            }
            else if (lblFlag.Text == "0")
            {
                lblStatus.Text = "Pending";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
            else if (lblFlag.Text == "3")
            {
                lblStatus.Text = "invoiced";
                lblStatus.ForeColor = System.Drawing.Color.DeepPink;
            }
            else if (lblFlag.Text == "4")
            {
                lblStatus.Text = "despatched";
                lblStatus.ForeColor = System.Drawing.Color.Purple;
            }
            else if (lblFlag.Text == "5")
            {
                lblStatus.Text = "delivered";
                lblStatus.ForeColor = System.Drawing.Color.Gray;
            }
            else if (lblFlag.Text == "6")
            {
                lblStatus.Text = "Order&nbsp;Recieved";
                lblStatus.ForeColor = System.Drawing.Color.Orange;
            }
        }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();

            Label lblTrans_SlNo = (Label)gridRow.Cells[3].FindControl("lblTrans_SlNo");
            string Trans_SlNo = lblTrans_SlNo.Text.ToString();

            if ((ListedDR.Trim().Length > 0) && (Trans_SlNo.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Listed Doctor
                ListedDR lstDR = new ListedDR();
                iReturn = lstDR.Update_Order_Booking_Status(ListedDR, 2, div_code, Trans_SlNo);
            }
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Listed Doctor De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Rejected Successfully');</script>");
            FillDoc();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdDoctor.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkListedDR");
            bool bCheck = chkDR.Checked;
            Label lblDR = (Label)gridRow.Cells[1].FindControl("lblDocCode");
            string ListedDR = lblDR.Text.ToString();

            Label lblTrans_SlNo = (Label)gridRow.Cells[3].FindControl("lblTrans_SlNo");
            string Trans_SlNo = lblTrans_SlNo.Text.ToString();

            if ((ListedDR.Trim().Length > 0) && (Trans_SlNo.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Listed Doctor
                ListedDR lstDR = new ListedDR();
                iReturn = lstDR.Update_Order_Booking_Status(ListedDR, 1, div_code, Trans_SlNo);
            }
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Listed Doctor De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Successfully');</script>");
            FillDoc();
        }

    }
}