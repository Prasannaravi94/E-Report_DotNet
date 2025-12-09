//
#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;
#endregion

public partial class MasterFiles_MR_ListedDoctor_Listeddr_Chemists_Map : System.Web.UI.Page
{
    #region Variables
    DataSet dsListedDR = null;
    DataSet dsProdDR = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsDocSubCat = null;
    string state_code = string.Empty;
    DataSet dsCatgType = null, dsChemists;
    string Listed_DR_Code = string.Empty;
    string doctype = string.Empty;
    string chkCampaign = string.Empty;
    string Doc_SubCatCode = string.Empty;
    int iIndex = -1;
    string sCmd = string.Empty;
    int iReturn = -1;
    int time;
    #endregion
    //
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = Request.QueryString["sf_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                DataList1.BackColor = System.Drawing.Color.White;

                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                FillDoctor();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                DataList1.BackColor = System.Drawing.Color.White;

                UserControl_MGR_Menu Usc_MR =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                FillDoctor();
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                if (Session["sf_code_Temp"].ToString() != "")
                {
                    sf_code = Session["sf_code_Temp"].ToString();
                }
                DataList1.BackColor = System.Drawing.Color.White;
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);

                Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                FillDoctor();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR1 =
                        (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu Usc_MR1 =
                        (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(Usc_MR1);
                Usc_MR1.Title = this.Page.Title;
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
                if (Session["sf_code_Temp"].ToString() != "")
                {
                    sf_code = Session["sf_code_Temp"].ToString();
                }
                UserControl_MenuUserControl Usc_Menu =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Session["backurl"] = "LstDoctorList.aspx";
            }
        }
    }
    #endregion
    //
    #region Fill Doctor
    private void FillDoctor()
    {
        lblSelect.Visible = true;
        lblSelect.Text = "Select Listed Chemists Name";
        ListedDR LstDoc = new ListedDR();
        dsListedDR = LstDoc.getListedDr_for_Mapp(sf_code, div_code);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            ddldr.DataTextField = "ListedDr_Name";
            ddldr.DataValueField = "ListedDrCode";
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();
        }
        else
        {
            ddldr.DataSource = dsListedDR;
            ddldr.DataBind();
        }
    }
    #endregion
    //
    #region Button Go Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        lblSelect.Visible = true;
        DataList1.Visible = true;
        FillChemists();
    }

    private void FillChemists()
    {
        lblSelect.Text = "Select the Chemists Name";
        btnSubmit.Visible = true;
        Chemist chem = new Chemist();
        dsChemists = chem.getChemists(sf_code);
        if (dsChemists.Tables[0].Rows.Count > 0)
        {
            DataList1.Visible = true;
            DataList1.DataSource = dsChemists;
            DataList1.DataBind();
        }
        else
        {
            DataList1.DataSource = dsChemists;
            DataList1.DataBind();
        }

        string str_CateCode = "";
        Chemist chems = new Chemist();
        dsProdDR = chems.getChemistsfor_Mappdr(ddldr.SelectedValue);

        if (dsProdDR.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsProdDR.Tables[0].Rows.Count; i++)
            {
                str_CateCode = dsProdDR.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                foreach (DataListItem grid in DataList1.Items)
                {
                    Label chk = (Label)grid.FindControl("lblChemistsCode");
                    string[] Salesforce;
                    if (str_CateCode != "")
                    {
                        iIndex = -1;
                        Salesforce = str_CateCode.Split(',');
                        foreach (string sf in Salesforce)
                        {
                            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
                            Label hf = (Label)grid.FindControl("lblChemistsCode");

                            if (sf == hf.Text)
                            {
                                chkCatName.Checked = true;
                                chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:16px; ");
                            }
                        }
                    }
                }
            }
        }     
    }
    #endregion
    //
    #region Button Submit Click
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string strPrd = "";
        string srtpd = "";

        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblChemistsCode");
            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");
            if (chkCatName.Checked == true)
            {
                strPrd += chk.Text + ",";
            }
        }

        if (strPrd != "")
        {
            strPrd = strPrd.Remove(strPrd.Length - 1);
            string[] Chemists_code = strPrd.Split(',');

            foreach (string Chemist_Cod in Chemists_code)    
            {
                Chemist lst = new Chemist();
                int iReturn = lst.RecordAdd_ChemistsMap_New(ddldr.SelectedValue, Chemist_Cod, ddldr.SelectedItem.Text, sf_code, div_code);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Text = "Select Listed Chemists Name";
                }
            }
        }
        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblChemistsCode");
            CheckBox chkCatName = (CheckBox)grid.FindControl("chkCatName");

            if (!chkCatName.Checked)
            {
                if (chkCatName.Checked == false)
                {
                    srtpd += chk.Text + ",";
                }
            }
        }

        if (srtpd != "")
        {
            srtpd = srtpd.Remove(srtpd.Length - 1);
            string[] Chemists_cod = srtpd.Split(',');

            foreach (string Chemist_Co in Chemists_cod)
            {
                Chemist lstdr = new Chemist();
                int iReturn = lstdr.Delete_ChemistsMap(ddldr.SelectedValue, Chemist_Co, sf_code, div_code);
                if (iReturn == -1)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Text = "Select Listed Chemists Name";
                }
            }
        }
    }
    #endregion
    //
    #region Button Clear Click
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddldr.SelectedValue = "0";
        FillDoctor();
        DataList1.Visible = false;
        btnSubmit.Visible = false;
        lblSelect.Text = "Select Listed Chemists Name";
    }
    #endregion
    //

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("LstDoctorList.aspx");
    }
}

