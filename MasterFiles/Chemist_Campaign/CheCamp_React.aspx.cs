using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Chemist_Campaign_CheCamp_React : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsCheSpe = null;
    int CheSpeCode = 0;
    string divcode = string.Empty;
    string CheSpe_SName = string.Empty;
    string CheSpeName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "ChemistCampaignList.aspx";
        if (!Page.IsPostBack)
        {
            FillCheSpe();
            menu1.Title = this.Page.Title;

        }
    }

    private void FillCheSpe()
    {

        ChemistCampaign dv = new ChemistCampaign();
        dsCheSpe = dv.getCheCamp_Re(divcode);
        if (dsCheSpe.Tables[0].Rows.Count > 0)
        {
            grdCheSpe.Visible = true;
            grdCheSpe.DataSource = dsCheSpe;
            grdCheSpe.DataBind();
        }
        else
        {
            grdCheSpe.DataSource = dsCheSpe;
            grdCheSpe.DataBind();
        }
    }

    protected void grdCheSpe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            CheSpeCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            ChemistCampaign dv = new ChemistCampaign();
            int iReturn = dv.RectivateChe_CamReact(CheSpeCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Speciality has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillCheSpe();
        }
    }
    protected void grdCheSpe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCheSpe.PageIndex = e.NewPageIndex;
        FillCheSpe();
    }
//    protected void btnBack_Click(object sender, EventArgs e)
//    {
//        Response.Redirect("ChemistCampaignList.aspx");

//    }
}