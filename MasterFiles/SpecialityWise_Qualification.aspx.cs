using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_SpecialityWise_Qualification : System.Web.UI.Page
{

    DataSet dsspecial = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        heading.InnerHtml = this.Page.Title;
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            fillspeciality();
            if (Session["sf_type"].ToString() == "1")
            {
                sfCode = Session["sf_code"].ToString();
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Usc_MR.Title = this.Page.Title;
                Usc_MR.FindControl("btnBack").Visible = false;
              
            }
            else
            {
               
                UserControl_MenuUserControl Usc_Menu =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
              //  Divid.FindControl("btnBack").Visible = false;
                //Usc_Menu.Title = this.Page.Title;
                Session["backurl"] = "LstDoctorList.aspx";
                //Usc_Menu.FindControl("btnBack").Visible = false;
              
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
                Usc_MR1.FindControl("btnBack").Visible = false;
                //btnBack.Visible = false;
            }
            else
            {
                UserControl_MenuUserControl Usc_Menu =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(Usc_Menu);
                Session["backurl"] = "LstDoctorList.aspx";
                //Usc_Menu.FindControl("btnBack").Visible = false; 
            }
        }
       

    }
    private void fillspeciality()
    {
        ListedDR dr = new ListedDR();
        dsspecial = dr.getspeciality(div_code);
        if (dsspecial.Tables[0].Rows.Count > 0)
        {
            ddlspecial.DataTextField = "Doc_Special_SName";
            ddlspecial.DataValueField = "Doc_Special_Code";
            ddlspecial.DataSource = dsspecial;
            ddlspecial.DataBind();

            ddltospclty.DataTextField = "Doc_Special_SName";
            ddltospclty.DataValueField = "Doc_Special_Code";
            ddltospclty.DataSource = dsspecial;
            ddltospclty.DataBind();
        }

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdQual.Rows)
        {
            CheckBox chkspec = (CheckBox)gridRow.Cells[0].FindControl("chkspecial");
            bool bCheck = chkspec.Checked;
            Label lblqualcode = (Label)gridRow.Cells[2].FindControl("lblqualcode");
            string qual = lblqualcode.Text.ToString();

            if ((bCheck == true))
            {
                AdminSetup adm = new AdminSetup();
                iReturn = adm.updatespeciality(qual, div_code, ddltospclty.SelectedValue, ddlspecial.SelectedValue, ddltospclty.SelectedItem.Text, ddlspecial.SelectedItem.Text);

                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update Successfully');</script>");
                }
                fillspecialqual(); 
            }
            else
            {
              
            }
        }
       

    }
    protected void btngo_click(object sender,EventArgs e)
    {
        tbl1.Visible = true;
        btnupdate.Visible = true;
        fillspecialqual();
    }
    protected void fillspecialqual()
    {
        AdminSetup adm = new AdminSetup();
        dsspecial = adm.getqualcnt(div_code, ddlspecial.SelectedValue);
        if(dsspecial.Tables[0].Rows.Count>0)
        {
            grdQual.DataSource = dsspecial;
            grdQual.DataBind();
        }
        else
        {
            grdQual.DataSource = dsspecial;
            grdQual.DataBind();
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DoctorQualificationList.aspx");
    }
}