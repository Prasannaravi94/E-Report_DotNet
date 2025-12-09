using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_Options_DeviceIdDeletion : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sf_type = string.Empty;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "Salesforce_Homepage.aspx";
        if (!Page.IsPostBack)
        {

            FillMRManagers();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            //menu1.FindControl("btnBack").Visible = false;
            base.OnPreInit(e);
        }
        FillColor();

        hHeading.InnerText = Page.Title;
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }



    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, "admin");
        if (sf_type == "3")
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();
    }

    private void fillgrid()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.fill_baselevel_forDeviceId(div_code, ddlFieldForce.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdRelease.Visible = true;
            grdRelease.DataSource = dsSalesForce;
            grdRelease.DataBind();
        }
        else
        {
            grdRelease.DataSource = dsSalesForce;
            grdRelease.DataBind();
            btnSubmit.Visible = false;
        }
      
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int iReturn = -1;


       
            foreach (GridViewRow gridRow in grdRelease.Rows)
            {
                Label lblsf_code = (Label)gridRow.Cells[0].FindControl("lblsf_code");
                string lblsfcode = lblsf_code.Text.ToString();
                CheckBox chkRelease = (CheckBox)gridRow.Cells[1].FindControl("chkRelease");
                bool bCheck = chkRelease.Checked;
             

                if ((lblsfcode.Trim().Length > 0) &&(bCheck == true))
                {
                    SalesForce dcr = new SalesForce();
                    iReturn = dcr.Update_deviceid_delete(lblsf_code.Text);
                }


                if (iReturn > 0)
                {
                
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Device Id Deleted successfully');</script>");
                    grdRelease.Visible = false;
                    btnSubmit.Visible = false;
                }
            }
        }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
       
    }
    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

}
        

   


