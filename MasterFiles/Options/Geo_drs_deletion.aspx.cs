using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Options_Geo_drs_deletion : System.Web.UI.Page
{
   

    DataSet dsGeodrs = null;
    
    string div_code = string.Empty;
    string Sf_Code = string.Empty;
    private DataSet dsGeochem;
    private DataSet dsGeostock;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Sf_Code = Session["sf_code"].ToString();
        hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
           
           
            Fillcombo();
        
        }

        if (Session["sf_type"].ToString() != "1")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.Title = this.Page.Title;
            //c1.FindControl("btnBack").Visible = false;
        }
        else
        {
            UserControl_MR_Menu c2 =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c2);
            c2.Title = this.Page.Title;
            c2.FindControl("btnBack").Visible = false;
        }
      
    }

    
  
    private void Fillcombo()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getSecSales_MR(div_code, Sf_Code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataValueField = "Sf_Code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }
    
    private void FillGeodrs()
    {
        SalesForce sf = new SalesForce();
        
        dsGeodrs = sf.untagGeodrs(div_code, ddlFieldForce.SelectedValue);

        if (dsGeodrs.Tables[0].Rows.Count > 0)
        {
            Geodrs.Visible = true;
            Geodrs.DataSource = dsGeodrs;
            Geodrs.DataBind();
        }
        else
        {
            Geodrs.DataSource = dsGeodrs;
            Geodrs.DataBind();
        }

    }
    private void FillGeochemist()
    {
        SalesForce sf = new SalesForce();
        dsGeochem= sf.untagGeochemist(div_code, ddlFieldForce.SelectedValue);
        if (dsGeochem.Tables[0].Rows.Count > 0)
        {
            GrdChemist.Visible = true;
            GrdChemist.DataSource = dsGeochem;
            GrdChemist.DataBind();
        }
        else
        {
            GrdChemist.DataSource = dsGeochem;
            GrdChemist.DataBind();
        }

    }
    private void FillGeoStock()
    {
        SalesForce sf = new SalesForce();
        dsGeostock = sf.untagGeoStockist(div_code, ddlFieldForce.SelectedValue);
        if (dsGeostock.Tables[0].Rows.Count > 0)
        {
            GrdStockist.Visible = true;
            GrdStockist.DataSource = dsGeostock;
            GrdStockist.DataBind();
        }
        else
        {
            GrdStockist.DataSource = dsGeostock;
            GrdStockist.DataBind();
        }
    }
   
   
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rdoFenceDel.SelectedValue == "1")
        {
            PnlDrs.Visible = true;
            PnlChemist.Visible = false;
            PnlStockist.Visible = false;
            FillGeodrs();
            btnSubmit.Visible = true;
            btnsubmitchemist.Visible = false;
            btnSubmit1Stockist.Visible = false;
        }
        else if (rdoFenceDel.SelectedValue == "2")
        {
            PnlChemist.Visible = true;
            PnlDrs.Visible = false;
            PnlStockist.Visible = false;
            FillGeochemist();
            btnsubmitchemist.Visible = true;
            btnSubmit.Visible = false;
            btnSubmit1Stockist.Visible = false;
        }
        else  if (rdoFenceDel.SelectedValue == "3")
        {
            PnlStockist.Visible = true;
            PnlDrs.Visible = false;
            PnlChemist.Visible = false;
            FillGeoStock();
            btnSubmit1Stockist.Visible = true;
            btnsubmitchemist.Visible = false;
            btnSubmit.Visible = false;

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in Geodrs.Rows)
        {
            Label lblDR_Code = (Label)gridRow.Cells[0].FindControl("lblDR_Code");
            string lblDRCode = lblDR_Code.Text.ToString();
            Label lbl_lat = (Label)gridRow.Cells[0].FindControl("lbl_lat");
            string lbllat = lbl_lat.Text.ToString();
            Label lbl_long = (Label)gridRow.Cells[0].FindControl("lbl_long");
            string lbllong = lbl_long.Text.ToString();
            Label mapid = (Label)gridRow.Cells[0].FindControl("mapid");
            string map_id = mapid.Text.ToString();
           
            CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
            bool bCheck = chkRelease.Checked;


            if ((lblDRCode.Trim().Length > 0) && (bCheck == true))
            {
                AdminSetup dcr = new AdminSetup();
                iReturn = dcr.Recorddelete_Untagdrs(lblDRCode, div_code,  map_id);
            }
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Geo Drs Deleted successfully');window.location='Geo_drs_deletion.aspx';</script>");

            }
        }
    }

   
    protected void btnsubmitchemist_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in GrdChemist.Rows)
        {
            Label lblChem_Code = (Label)gridRow.Cells[0].FindControl("lblChem_Code");
            string lblChemCode = lblChem_Code.Text.ToString();
            Label lbl_lat = (Label)gridRow.Cells[0].FindControl("lbl_lat");
            string lbllat = lbl_lat.Text.ToString();
            Label lbl_long = (Label)gridRow.Cells[0].FindControl("lbl_long");
            string lbllong = lbl_long.Text.ToString();
            Label mapid = (Label)gridRow.Cells[0].FindControl("mapid");
            string map_id = mapid.Text.ToString();
            CheckBox chkReleasech = (CheckBox)gridRow.Cells[1].FindControl("chkReleasech");
            bool bCheck = chkReleasech.Checked;
            if ((lblChemCode.Trim().Length > 0) && (bCheck == true))
            {
                AdminSetup dcr = new AdminSetup();
                iReturn = dcr.Recorddelete_UntagdrsChem(lblChemCode, div_code, map_id);
            }
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Geo Chemists Deleted successfully');window.location='Geo_drs_deletion.aspx';</script>");
            }
        }
    }
    protected void btnSubmit1Stockist_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in GrdStockist.Rows)
        {
            Label lblStockist_Code = (Label)gridRow.Cells[0].FindControl("lblStockist_Code");
            string lblStockistCode = lblStockist_Code.Text.ToString();
            Label lbl_lat = (Label)gridRow.Cells[0].FindControl("lbl_lat");
            string lbllat = lbl_lat.Text.ToString();
            Label lbl_long = (Label)gridRow.Cells[0].FindControl("lbl_long");
            string lbllong = lbl_long.Text.ToString();
            Label mapid = (Label)gridRow.Cells[0].FindControl("mapid");
            string map_id = mapid.Text.ToString();
            CheckBox chkReleasestk = (CheckBox)gridRow.Cells[1].FindControl("chkReleasestk");
            bool bCheck = chkReleasestk.Checked;

            if ((lblStockistCode.Trim().Length > 0) && (bCheck == true))
            {
                AdminSetup dcr = new AdminSetup();
                iReturn = dcr.Recorddelete_UntagdrsStock(lblStockistCode, div_code, map_id);
            }
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Geo Stockists Deleted successfully');window.location='Geo_drs_deletion.aspx';</script>");
            }
        }
    }
}