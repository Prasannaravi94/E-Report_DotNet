using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_ActivityUploadReact : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsActivityUpload = null;
    int Activity_ID = 0;
    string divcode = string.Empty;
    string Activity_S_Name = string.Empty;
    string Activity_Name = string.Empty;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "ActivityMasterList.aspx";
        if (!Page.IsPostBack)
        {
            FillActivity();
            menu1.Title = this.Page.Title;

        }
    }
    private void FillActivity()
    {
        SubDivision dv = new SubDivision();
        dsActivityUpload = dv.getActivityUpload(divcode);
        if (dsActivityUpload.Tables[0].Rows.Count > 0)
        {
            grdActUpload.Visible = true;
            grdActUpload.DataSource = dsActivityUpload;
            grdActUpload.DataBind();
        }
        else
        {

            grdActUpload.DataSource = null;
            grdActUpload.DataBind();
        }
    }
    protected void grdActUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reactivate")
        {
            Activity_ID = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            SubDivision dv = new SubDivision();
            int iReturn = dv.ActivityUploadReActivate(Activity_ID);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Category has been Reactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Reactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
            }
            FillActivity();
        }
    }
    protected void grdActUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActUpload.PageIndex = e.NewPageIndex;
        FillActivity();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ActivityMasterList.aspx");
    }
}