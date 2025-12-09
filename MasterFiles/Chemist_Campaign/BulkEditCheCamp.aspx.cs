using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Chemist_Campaign_BulkEditCheCamp : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsCheCat = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ChemistCampaignList.aspx";
            menu1.Title = this.Page.Title;
            FillCheSubCat();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    private void FillCheSubCat()
    {
        ChemistCampaign dv = new ChemistCampaign();
        dsCheCat = dv.getCheSubCat(div_code);
        if (dsCheCat.Tables[0].Rows.Count > 0)
        {
            grdCheCat.Visible = true;
            grdCheCat.DataSource = dsCheCat;
            grdCheCat.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            grdCheCat.DataSource = dsCheCat;
            grdCheCat.DataBind();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChemistCampaignList.aspx");

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Che_cat_code = string.Empty;
        string Che_cat_sname = string.Empty;
        string Che_cat_name = string.Empty;
        ChemistCampaign dv = new ChemistCampaign();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in grdCheCat.Rows)
        {
            Label lblCheCatCode = (Label)gridRow.Cells[1].FindControl("lblCheCatCode");
            Che_cat_code = lblCheCatCode.Text.ToString();
            TextBox txtChe_Cat_SName = (TextBox)gridRow.Cells[1].FindControl("txtChe_Cat_SName");
            Che_cat_sname = txtChe_Cat_SName.Text.ToString();
            TextBox txtCheCatName = (TextBox)gridRow.Cells[1].FindControl("txtCheCatName");
            Che_cat_name = txtCheCatName.Text.ToString();
            iReturn = dv.RecordUpdateSubCatChem(Convert.ToInt16(Che_cat_code), Che_cat_sname, Che_cat_name, div_code);

            if (iReturn > 0)
                err = false;

            if ((iReturn == -2))
            {
                txtCheCatName.Focus();
                err = true;
                break;
            }
            if ((iReturn == -3))
            {
                txtChe_Cat_SName.Focus();
                err = true;
                break;
            }

        }

        if (err == false)
        {
            //menu1.Status = "Doctor Campaign(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ChemistCampaignList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
            }
            else if (iReturn == -3)
            {
                // menu1.Status = "Doctor Sub-Category already Exist";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            }
        }
    }

}