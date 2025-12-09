using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Chem_Cat_React : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsChemClass = null;
    int ChemClassCode = 0;
    string divcode = string.Empty;
    string Chem_Class_SName = string.Empty;
    string ChemClassName = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "ChemistClassList.aspx";
        heading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            FillChemClass();
            menu1.Title = this.Page.Title;

        }
    }
    private void FillChemClass()
    {
        Chemist chem = new Chemist();
        dsChemClass = chem.getChemClass_Re(divcode);
        if (dsChemClass.Tables[0].Rows.Count > 0)
        {
            grdChemClass.Visible = true;
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();
        }
        else
        {
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();
        }
    }
    protected void grdChemClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            ChemClassCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Chemist chem = new Chemist();
            int iReturn = chem.ReActivate_Chemclass(ChemClassCode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillChemClass();
        }
    }
    protected void grdChemClass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemClass.PageIndex = e.NewPageIndex;
        FillChemClass();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChemistClassList.aspx");

    }
}