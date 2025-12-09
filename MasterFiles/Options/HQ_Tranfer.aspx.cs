using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Configuration;
using DBase_EReport;

public partial class MasterFiles_Options_HQ_Tranfer : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsSalesForce2 = null;
    DataSet dsSalesForce3 = null;
    DataSet dsChemists = null;
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    DataTable dtListedDR = null;
    DataTable dtChemists = null;
    DataTable dtChem = null;
    DataTable dt;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        hHeading.InnerText = this.Page.Title;

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            FillManagers1();
            FillManagers2();
        }
        FillColor1();
        FillColor2();
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
    protected void ddlFromFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsub1.Visible = true;
        grdField1.Visible = true;
        lblsub1.Visible = true;
        fillbaselevel(grdField1,ddlFromFieldForce.SelectedValue);
    }
    protected void ddlToFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblsub2.Visible = true;
        grdField2.Visible = true;
        lblsub2.Visible = true;
        fillbaselevel(grdField2,ddlToFieldForce.SelectedValue);
    }

    private void FillManagers1()
    {
        SalesForce sf = new SalesForce();

       
        dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
 

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFromFieldForce.DataTextField = "sf_name";
            ddlFromFieldForce.DataValueField = "sf_code";
            ddlFromFieldForce.DataSource = dsSalesForce;
            ddlFromFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

        FillColor1();
    }

    private void FillManagers2()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce2 = sf.UserList_Hierarchy(div_code, sf_code);


        if (dsSalesForce2.Tables[0].Rows.Count > 0)
        {
            dsSalesForce2.Tables[0].Rows[1].Delete();
            ddlToFieldForce.DataTextField = "sf_name";
            ddlToFieldForce.DataValueField = "sf_code";
            ddlToFieldForce.DataSource = dsSalesForce2;
            ddlToFieldForce.DataBind();

            ddlSF2.DataTextField = "des_color";
            ddlSF2.DataValueField = "sf_code";
            ddlSF2.DataSource = dsSalesForce2;
            ddlSF2.DataBind();

        }
        FillColor2();
    }


    
    private void FillColor1()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFromFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillColor2()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF2.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlToFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void fillbaselevel(GridView grdField,string mgr_code)
    {
        
        SalesForce sf = new SalesForce();

        dsSalesForce3 = sf.getReporting_sf(div_code, mgr_code);


        if (dsSalesForce3.Tables[0].Rows.Count > 0)
        {

            grdField.DataSource = dsSalesForce3;
            grdField.DataBind();
        }
        else
        {
            grdField.DataSource = dsSalesForce3;
            grdField.DataBind();
        }
    }

    protected void btnTransfer_Click(object sender, EventArgs e)
    {

        int iReturn = -1;
        string strQry = string.Empty;
        DB_EReporting db = new DB_EReporting();
        strQry = "EXEC HQ_Tranfer '" + ddlFromFieldForce.SelectedValue + "','" + ddlToFieldForce.SelectedValue + "' ";
        iReturn = db.ExecQry(strQry);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('HQ Transfered successfully');</script>");
            grdField2.Visible = false;
            grdField1.Visible = false;
            lblsub2.Visible = false;
            lblsub1.Visible = false;
            ddlFromFieldForce.SelectedIndex = 0;
            ddlToFieldForce.SelectedIndex = 0;
            FillManagers1();
            FillManagers2();
        }
    }

}