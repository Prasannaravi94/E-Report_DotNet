using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Common_Doctor_List : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDoc = null;
    DataSet dsListedDR = null;
    DataSet dsSalesForce = null;
    DataSet dsadm = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string Listed_DR_Code = string.Empty;
    string Listed_DR_Name = string.Empty;
    string Listed_DR_Address = string.Empty;
    string Listed_DR_Catg = string.Empty;
    string Listed_DR_Spec = string.Empty;
    string Listed_DR_Class = string.Empty;
    string Listed_DR_Qual = string.Empty;
    string Listed_DR_Terr = string.Empty;
    string Territory = string.Empty;
    string Activity_Date = string.Empty;
    string Category = string.Empty;
    string Spec = string.Empty;
    string Qual = string.Empty;
    string Class = string.Empty;
    string sf_type = string.Empty;
    int iCnt = -1;
    string sf_code = string.Empty;
    string Find = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int i = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        //// menu1.FindControl("btnBack").Visible = false;
        menu1.Title = Page.Title;
        if (!Page.IsPostBack)
        {
            FillDoc();
        }
    }
    private void FillDoc()
    {
        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getCommonDr_List(div_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;         
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsDoc;
            grdDoctor.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Common_Doctor_Updation.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {       
        Find = ddlFields.SelectedValue.ToString();
        if (Find == "C_Doctor_Name" || Find == "C_Doctor_HQ")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, div_code);
        }
        else
        {
            FillDoc();
        }
    }
    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = "  (" + sSearchBy + " like '" + sSearchText + "%' or " + sSearchBy + " like '% " + sSearchText + "%') AND Division_Code ='" + div_code + "' ";
                 
        ListedDR sf = new ListedDR();
        dsSalesForce = sf.FindCommonDr(sFind);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdDoctor.Visible = true;
            grdDoctor.DataSource = dsSalesForce;
            grdDoctor.DataBind();
        }
        else
        {
            grdDoctor.DataSource = dsSalesForce;
            grdDoctor.DataBind();
        }

    }
}