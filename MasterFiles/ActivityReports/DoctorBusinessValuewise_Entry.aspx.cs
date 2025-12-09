using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Globalization;

public partial class DoctorBusinessEntry : System.Web.UI.Page
{
    string sfCode = string.Empty;
    DCRBusinessEntry objDCRBusiness = new DCRBusinessEntry();

    Territory objTerritory = new Territory();
    DataSet dsDoc = null;
    DataSet dsTrans_Bus = null;
    ListedDR lstDR = new ListedDR();
    DataSet dsTP = null;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    string div_code = string.Empty;

    protected void Page_Init(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sfCode = Session["sf_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //linkcheck.Visible = false;
                //lblFF.Visible = false;
                ddlFieldForce.Visible = false;
                lblFieldForceName.Visible = false;
            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                //this.FillMasterList_adm();
                this.FillMasterList_adm();
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }

            //ddlMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Value = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }

        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                sfCode = ddlFieldForce.SelectedValue;
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
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
    private void FillMasterList_adm()
    {
        SalesForce sf = new SalesForce();
        Doctor objDoctor = new Doctor();
        DataSet dsSalesForce = new DataSet();
        if (Session["sf_type"].ToString() == "2")
        {
            sfCode = Session["sf_code"].ToString();
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sfCode);
        }
        else { dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin"); }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            if (Session["sf_type"].ToString() != "2")
            {
                ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }
    }
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();

        dsDoc = LstDoc.getListedDr_new(sfCode);

        DataTable dt = new DataTable();
        dt = dsDoc.Tables[0].AsEnumerable().OrderBy(r => (r.Field<string>("Doc_Cat_ShortName")))
                 .CopyToDataTable();
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

        if (dt.Rows.Count > 0)
        {
            gvDoctorBusiness.DataSource = dt;
            gvDoctorBusiness.DataBind();

            foreach (GridViewRow row in gvDoctorBusiness.Rows)
            {
                HiddenField hdnDoctor;
                Label lblDoctor;
                TextBox txtDocBusVal;
                int iReturn = 0;

                hdnDoctor = (HiddenField)row.FindControl("hdnDoctor");
                lblDoctor = (Label)row.FindControl("lblDoctor");
                txtDocBusVal = (TextBox)row.FindControl("txtDocBusVal");

                ListedDR lst = new ListedDR();
                dsTrans_Bus = lst.Trans_DrBus_Valuewise_RecordExistNew(hdnDoctor.Value, sfCode, div_code, MonthVal, YearVal);

                if (dsTrans_Bus.Tables[0].Rows.Count > 0)
                {
                    txtDocBusVal.Text = dsTrans_Bus.Tables[0].Rows[0]["Business_Value"].ToString();
                }
            }
        }
        else
        {
            gvDoctorBusiness.DataSource = dt;
            gvDoctorBusiness.DataBind();
        }
    }
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);

        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsTerritory;
            ddlFieldForce.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlFieldForce.SelectedIndex = 1;
                sfCode = ddlFieldForce.SelectedValue.ToString();
                Session["sf_code"] = sfCode;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            //
            #region Variable
            HiddenField hdnDoctor;
            Label lblDoctor;
            TextBox txtDocBusVal;
            int iReturn = 0;
            #endregion
            //
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Value).Year);

            foreach (GridViewRow gridrow in gvDoctorBusiness.Rows)
            {
                #region Variables
                hdnDoctor = (HiddenField)gridrow.FindControl("hdnDoctor");
                lblDoctor = (Label)gridrow.FindControl("lblDoctor");
                txtDocBusVal = (TextBox)gridrow.FindControl("txtDocBusVal");
                #endregion

                if (hdnDoctor.Value != "" && lblDoctor.Text != "")
                {
                    if (Session["sf_type"].ToString() == "1")
                    {
                        sfCode = Session["sf_code"].ToString();
                    }

                    iReturn = InsertDocBusEntry(sfCode, MonthVal.ToString(), YearVal.ToString(), hdnDoctor, lblDoctor, txtDocBusVal);
                }
            }

            if (iReturn < 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Business Valuewise Entry Updated Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    //
    #region InsertDocBusEntry
    //private int InsertDocBusEntry(string sfCode, DropDownList ddlMonth, DropDownList ddlYear, HiddenField hdnDoctor, Label lblDoctor, TextBox txtDocBusVal)
    //{
    private int InsertDocBusEntry(string sfCode, string ddlMonth, string ddlYear, HiddenField hdnDoctor, Label lblDoctor, TextBox txtDocBusVal)
    {
        int iReturn = -1;

        ListedDR lst = new ListedDR();

        if (txtDocBusVal.Text != string.Empty)
        {
            dsTrans_Bus = lst.Trans_DrBus_Valuewise_RecordExistNew(hdnDoctor.Value, sfCode, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));

            if (dsTrans_Bus.Tables[0].Rows.Count == 0)
            {
                iReturn = lst.RecordAddTrans_DrBus_Valuewise(sfCode, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), Convert.ToInt32(hdnDoctor.Value), lblDoctor.Text, Convert.ToSingle(txtDocBusVal.Text));
            }
            else
            {
                iReturn = lst.RecordUpdateTrans_DrBus_Valuewise(sfCode, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), Convert.ToInt32(hdnDoctor.Value), Convert.ToSingle(txtDocBusVal.Text));
            }
        }
        else if (txtDocBusVal.Text == string.Empty)
        {
            dsTrans_Bus = lst.Trans_DrBus_Valuewise_RecordExistNew(hdnDoctor.Value, sfCode, div_code, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear));

            if (dsTrans_Bus.Tables[0].Rows.Count > 0)
            {
                iReturn = lst.RecordDelTrans_DrBus_Valuewise(sfCode, Convert.ToInt32(ddlMonth), Convert.ToInt32(ddlYear), Convert.ToInt32(div_code), Convert.ToInt32(hdnDoctor.Value));
            }
        }
        return iReturn;
    }
    #endregion

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            btnSave.Visible = true;
            FillDoc();
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}