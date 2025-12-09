using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_ActivityMasterList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsActMaster = null;
    int subdivcode = 0;
    int Activity_ID = 0;
    string divcode = string.Empty;
    string ShortName = string.Empty;
    string Activity_Name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillActMaster();
            btnNew.Focus();
        }
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

    private void FillActMaster()
    {
        SubDivision dv = new SubDivision();
        dsActMaster = dv.getActUpload(divcode);
        if (dsActMaster.Tables[0].Rows.Count > 0)
        {
            grdActUpload.Visible = true;
            grdActUpload.DataSource = dsActMaster;
            grdActUpload.DataBind();

            foreach (GridViewRow row in grdActUpload.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            }
        }
        else
        {
            grdActUpload.DataSource = dsActMaster;
            grdActUpload.DataBind();
        }

    }
    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        SubDivision dv = new SubDivision();
        dtGrid = dv.getActMasterUploadlist_DataTable(divcode);
        return dtGrid;
    }

    protected void grdActUpload_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdActUpload.DataSource = sortedView;
        grdActUpload.DataBind();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ActivityMasterCreation.aspx");
    }

    protected void grdActUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdActUpload.EditIndex = -1;
        //Fill the Grid
        FillActMaster();
    }

    protected void grdActUpload_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdActUpload.EditIndex = e.NewEditIndex;
        //Fill the  Grid
        FillActMaster();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdActUpload.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
    
    //begin
    protected void grdActUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            // subdivcode = Convert.ToString(e.CommandArgument);
            Activity_ID = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SubDivision dv = new SubDivision();
            int iReturn = dv.ActUploadDeActivate(Activity_ID);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillActMaster();
        }
    }
    //end
    protected void grdActUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdActUpload.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillActMaster();
    }
    protected void grdActUpload_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdActUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdActUpload.PageIndex = e.NewPageIndex;
        FillActMaster();
    }
    private void Update(int eIndex)
    {
        Label lblActivity_ID = (Label)grdActUpload.Rows[eIndex].Cells[1].FindControl("lblActivity_ID");
        Activity_ID = Convert.ToInt16(lblActivity_ID.Text);
        TextBox txtShortName = (TextBox)grdActUpload.Rows[eIndex].Cells[2].FindControl("txtShortName");
        ShortName = txtShortName.Text;
        TextBox txtActivity_Name = (TextBox)grdActUpload.Rows[eIndex].Cells[3].FindControl("txtActivity_Name");
        Activity_Name = txtActivity_Name.Text;

        // Update Sub Division
        SubDivision dv = new SubDivision();
        int iReturn = dv.MasActivityRecordUpdate(Activity_ID, ShortName, Activity_Name, divcode);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Master Name Already Exist');</script>");
            txtShortName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activity Master Short Name Already Exist');</script>");
            txtActivity_Name.Focus();
        }
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ActivityUploadReact.aspx");
    }
}