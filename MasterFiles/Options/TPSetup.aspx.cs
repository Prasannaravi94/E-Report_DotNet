using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using System.Data;

public partial class MasterFiles_Options_TPSetup : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsTPSetup = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                GetTPSetup();
            }
        }
        else {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                UserControl_MenuUserControl c1 =
                  (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
            }
        }
    }
    private void GetTPSetup()
    {
        TourPlan TP = new TourPlan();
        dsTPSetup = TP.GetTPSetup(div_code);
        rdoAddsessionNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["AddsessionNeed"].ToString();
        txtAddsessionCount.Text = dsTPSetup.Tables[0].Rows[0]["AddsessionCount"].ToString();
        rdoClusterNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["ClusterNeed"].ToString();
        rdoDrNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["DrNeed"].ToString();
        rdoChmNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["ChmNeed"].ToString();
        rdoStkNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["StkNeed"].ToString();
        rdoCip_Need.SelectedValue = dsTPSetup.Tables[0].Rows[0]["Cip_Need"].ToString();
        rdoHospNeed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["HospNeed"].ToString();
        rdoJWneed.SelectedValue = dsTPSetup.Tables[0].Rows[0]["JWneed"].ToString();
        rdotp_objective.SelectedValue = dsTPSetup.Tables[0].Rows[0]["tp_objective"].ToString();
        txtmax_doc.Text = dsTPSetup.Tables[0].Rows[0]["max_doc"].ToString();
        rdoFW_meetup_mandatory.SelectedValue = dsTPSetup.Tables[0].Rows[0]["FW_meetup_mandatory"].ToString();
        rdoclustertype.SelectedValue = dsTPSetup.Tables[0].Rows[0]["clustertype"].ToString();
        rdoHoliday_Editable.SelectedValue = dsTPSetup.Tables[0].Rows[0]["Holiday_Editable"].ToString();
        rdoWeeklyoff_Editable.SelectedValue = dsTPSetup.Tables[0].Rows[0]["Weeklyoff_Editable"].ToString();
    }
    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {
        TourPlan TP = new TourPlan();
        int iReturn = TP.RecordUpdate_TPSetupScreen(
            Convert.ToInt16(rdoAddsessionNeed.SelectedValue), txtAddsessionCount.Text.ToString(),
            Convert.ToInt16(rdoClusterNeed.SelectedValue), Convert.ToInt16(rdoDrNeed.SelectedValue),
            Convert.ToInt16(rdoChmNeed.SelectedValue), Convert.ToInt16(rdoStkNeed.SelectedValue),
            Convert.ToInt16(rdoCip_Need.SelectedValue), Convert.ToInt16(rdoHospNeed.SelectedValue),
            Convert.ToInt16(rdoJWneed.SelectedValue), Convert.ToInt16(rdotp_objective.SelectedValue),
            txtmax_doc.Text.ToString(),
            Convert.ToInt16(rdoFW_meetup_mandatory.SelectedValue), Convert.ToInt16(rdoclustertype.SelectedValue),
            Convert.ToInt16(rdoHoliday_Editable.SelectedValue), Convert.ToInt16(rdoWeeklyoff_Editable.SelectedValue),
            div_code
            );

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('TP Setup has been updated Successfully');</script>");
            GetTPSetup();
        }
    }
}