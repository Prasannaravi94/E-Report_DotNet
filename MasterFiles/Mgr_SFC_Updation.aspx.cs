using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Mgr_SFC_Updation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            // usc_MR.FindControl("btnBack").Visible = false;
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";
            //btnBack.Visible = true;

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sfcode = Session["sf_code"].ToString();
            UserControl_MGR_Menu c1 =
             (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            //btnBack.Visible = true;
            c1.Title = this.Page.Title;
            //   Session["backurl"] = "LstDoctorList.aspx";
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //                   "<span style='font-weight: bold;color:Red'>  " + Session["sf_HQ"] + "</span>";

        }
        else
        {
            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;


        }
        if (Request.QueryString["sfCode"]!=null)
        {
            Distance_calculation_001 Exp = new Distance_calculation_001();
            sfcode = Request.QueryString["sfCode"].ToString();
            divcode = Request.QueryString["divCode"].ToString();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            

            populateGriddata(false);
            mainDiv.Visible = false;
            
        }
        else
        {
            divcode = Convert.ToString(Session["div_code"]);
            sfcode = Convert.ToString(Session["Sf_Code"]);
            if (!Page.IsPostBack)
            {
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
                //Divid.Title = this.Page.Title;
                //Divid.FindControl("btnBack").Visible = false;
                //FillFieldForcediv(divcode);
                ddlSubdiv.Focus();
            }

        }
        FillColor();
    }

     
    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();

        dsSubDivision = dv.UserList_MGR_SFC(divcode, sfcode);
       
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            if (Session["sf_type"].ToString() == "3")
            {
                //dsSubDivision.Tables[0].Rows[0].Delete();
                dsSubDivision.Tables[0].Rows[1].Delete();
            }

            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();
        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlSubdiv.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
    private void FillColorMR()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF1.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlMR.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
        foreach (ListItem item in ddlMR.Items)
        {
            if (item.Value == "-1")
            {
                item.Attributes.Add("style", "color:red");
            }

        }

    }

	protected void linkcheck_Click(object sender, EventArgs e)
    {

        
		FillFieldForcediv(divcode);
        ddlSubdiv.Visible = true;
        linkcheck.Visible = false;
        txtNew.Visible = true;
        //btnGo.Visible = true;
        FillColor();
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        populateGriddata(true);
        btnSave.Visible = true;

        foreach (GridViewRow gridRow in grdDist.Rows)
         {
             DropDownList frmBox = ((DropDownList)gridRow.FindControl("Territory_Type"));
             TextBox dist = (TextBox)gridRow.FindControl("txtdist");
             TextBox rtndist = (TextBox)gridRow.FindControl("txtdistRtn");
             HiddenField hidtocat = (HiddenField)gridRow.FindControl("tocat");
            DropDownList terr = (DropDownList)gridRow.FindControl("Territory_Type");
            Label lblterr = (Label)gridRow.FindControl("lblSFTypeMGR");

            //if (hidtocat.Value== "HQ")
            //{
            //    dist.Visible = false;
            //    rtndist.Visible = false;
            //}


        }
    }

    private void populateGriddata(bool flag)
    {
        string sf_code = "";
        if (flag)
        {
            sf_code = ddlMR.SelectedValue.ToString();
        }
        else
        {
            sf_code = sfcode;
        }
        Distance_calculation_001 dv = new Distance_calculation_001();
        dsSubDivision = dv.Expense_SF_View(divcode, sf_code,ddlSubdiv.SelectedValue.ToString());
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {

            grdDist.Visible = true;
            grdDist.DataSource = dsSubDivision;
            grdDist.DataBind();
            lblSelect.Visible = false;

        }
        else
        {
            grdDist.DataSource = dsSubDivision;
            grdDist.DataBind();
            lblSelect.Visible = true;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;
            Territory terr = new Territory();
            iReturn = terr.GetdeleteMGRDist(ddlMR.SelectedValue.ToString(), ddlSubdiv.SelectedValue.ToString(), divcode);
            foreach (GridViewRow gridRow in grdDist.Rows)
                {
                    
                   HiddenField hidsfcode=(HiddenField)gridRow.FindControl("hidsfcode");
                HiddenField tocode=(HiddenField)gridRow.FindControl("tocode");
                HiddenField mrcat=(HiddenField)gridRow.FindControl("tocat");
                HiddenField hidmgrcode=(HiddenField)gridRow.FindControl("hidmgrcode");
                TextBox dist=(TextBox)gridRow.FindControl("txtdist");
                TextBox rtnHQ=(TextBox)gridRow.FindControl("txtdistRtn");
                             
                iReturn = terr.GetinsertMGRDist(ddlMR.SelectedValue.ToString(), ddlSubdiv.SelectedValue.ToString(), hidsfcode.Value, tocode.Value, mrcat.Value, hidmgrcode.Value, dist.Text, rtnHQ.Text, divcode);
                }
           
           
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        string sReport = ddlSubdiv.SelectedValue.ToString();
        DataSet dsSubDivision = sf.SalesForceList_MR_ALL_Sfc(divcode, sReport);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            lblMR.Visible = true;
            ddlMR.Visible = true;
            btnSF.Visible = true;
            lblmap.Visible = false;
            grdDist.Visible = false;
            btnSave.Visible = false;
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSubDivision;
            ddlMR.DataBind();

        }
        else
        {
            lblMR.Visible = false;
            ddlMR.Visible = false;
            btnSF.Visible = false;
            lblmap.Visible = true;
            lblSelect.Visible = false;
            grdDist.Visible = false;
            btnSave.Visible = false;
        }

        FillColorMR();

    }
 protected void txtNew_TextChanged(object sender, EventArgs e)
    {
        ddlFieldForce_SelectedIndexChanged(sender, e);
    }
   
}