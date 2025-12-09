using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;

public partial class MasterFiles_Chemist_Campaign_ChemistCampaign : System.Web.UI.Page
{
    #region "Declaration"
    string Chm_SubCat_SName = string.Empty;
    string ChmSubCatName = string.Empty;
    int All_DrsTagg = 0;
    string divcode = string.Empty;
    string CheSubCatCode = string.Empty;
    DataSet dsChe = null;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ChemistCampaignList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        CheSubCatCode = Request.QueryString["chm_campaign_code"];
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            ChemistCampaign dv = new ChemistCampaign();
            dsChe = dv.getCheSubCat(divcode, CheSubCatCode);

            if (dsChe.Tables[0].Rows.Count > 0)
            {
                txtChm_SubCat_SName.Text = dsChe.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtChmSubCatName.Text = dsChe.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                txtEffFrom.Text = dsChe.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtEffTo.Text = dsChe.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Chm_SubCat_SName = txtChm_SubCat_SName.Text;
        ChmSubCatName = txtChmSubCatName.Text;
        ChemistCampaign Chmst = new ChemistCampaign();
        if (CheSubCatCode == null)
        {
            
            int iReturn = Chmst.RecordAddSubCat(divcode, Chm_SubCat_SName, ChmSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Clear();
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                txtChmSubCatName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChm_SubCat_SName.Focus();
            }
        }
        else
        {
            int ChemSubCatCode = Convert.ToInt16(CheSubCatCode);
            int iReturn = Chmst.RecordAddSubCatUpdate(ChemSubCatCode, divcode, Chm_SubCat_SName, ChmSubCatName, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');  window.location= 'ChemistCampaignList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
                txtChmSubCatName.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
                txtChm_SubCat_SName.Focus();
            }
        }
    }
    private void Clear()
    {
        txtChm_SubCat_SName.Text = "";
        txtChmSubCatName.Text = "";
        txtEffFrom.Text = "";
        txtEffTo.Text = "";
    }
}