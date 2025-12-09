using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;


public partial class MasterFiles_Division_ListAdmin : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string txtNewSlNo = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iLength = -1;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        
       
            div_code = Session["div_code"].ToString();
            grdDivision.Columns[8].Visible = true;
        

        if (!Page.IsPostBack)
        {

            FillDivision();
            menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    private void FillDivision()
    {
        Division dv = new Division();
      
            dsDivision = dv.getDivision_stand();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                grdDivision.Visible = true;
                grdDivision.DataSource = dsDivision;
                grdDivision.DataBind();
                foreach (GridViewRow row in grdDivision.Rows)
                {
                    if (dsDivision.Tables[0].Rows[row.RowIndex]["standby"].ToString() == "1")
                    {
                        grdDivision.Rows[row.RowIndex].Cells[8].Text = "Activate";
                        LinkButton lb = new LinkButton();
                        lb.Text = "Activate";
                        lb.Style.Add("color", "Red");
                        row.Cells[8].Controls.Add(lb);


                    }
                }
            }
            else
            {
                grdDivision.DataSource = dsDivision;
                grdDivision.DataBind();
            }
        }
    
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
        Division dv = new Division();
        if (sf_type == "3")
        {
            iLength = div_code.Trim().Length;
            div_code = div_code.Substring(0, iLength - 1);
            dtGrid = dv.getDivision_DataTable(div_code);
        }
        else
        {
            dtGrid = dv.getDivisionlist_DataTable();
        }
        return dtGrid;
    }
    protected void grdDivision_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDivision.DataSource = sortedView;
        grdDivision.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("DivisionCreation_admin.aspx");
    }
    protected void grdDivision_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDivision.EditIndex = -1;
        //Fill the Division Grid
        FillDivision();
    }

    protected void grdDivision_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDivision.EditIndex = e.NewEditIndex;
        //Fill the Division Grid
        FillDivision();
        //Setting the focus to the textbox "Division Name"        
        TextBox ctrl = (TextBox)grdDivision.Rows[e.NewEditIndex].Cells[2].FindControl("txtDiv");
        ctrl.Focus();
    }

    protected void grdDivision_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDivision.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateDivision(iIndex);
        FillDivision();
    }

    protected void grdDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            div_code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Division dv = new Division();
            int iReturn = dv.DeActivate_New(div_code);
            if (iReturn > 0)
            {
                // menu1.Status ="Division has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillDivision();
        }
        if (e.CommandName == "Standby")
        {
            div_code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Division dv = new Division();
            int iReturn = dv.Standby(div_code);
            if (iReturn > 0)
            {

                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Stand by Successfully.\');", true);
            }
            else
            {

                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Stand by.\');", true);
            }
            FillDivision();
        }
    }

    protected void grdDivision_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdDivision_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDivision.PageIndex = e.NewPageIndex;
        FillDivision();
    }

    private void UpdateDivision(int eIndex)
    {
        System.Threading.Thread.Sleep(time);

        Label lblDivCode = (Label)grdDivision.Rows[eIndex].Cells[1].FindControl("lblDivCode");
        div_code = lblDivCode.Text;
        TextBox txtDivName = (TextBox)grdDivision.Rows[eIndex].Cells[2].FindControl("txtDiv");
        div_name = txtDivName.Text;
        //TextBox txtDivSName = (TextBox)grdDivision.Rows[eIndex].Cells[3].FindControl("txtSName");
        //div_sname = txtDivSName.Text;
        TextBox txtAlName = (TextBox)grdDivision.Rows[eIndex].Cells[6].FindControl("txtAlName");
        div_alias = txtAlName.Text;
        TextBox txtDivCity = (TextBox)grdDivision.Rows[eIndex].Cells[4].FindControl("txtCity");
        div_city = txtDivCity.Text;
        //TextBox txtSNo = (TextBox)grdDivision.Rows[eIndex].Cells[7].FindControl("txtNewSlNo");
        //txtNewSlNo = txtSNo.Text;
        // Update Division
        Division dv = new Division();
        int iReturn = dv.RecordUpdate(div_code, div_name, div_city, div_sname, div_alias);
        if (iReturn > 0)
        {
            // menu1.Status ="Division Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Updated Successfully.\');", true);
        }
        else if (iReturn == -2)
        {
            // menu1.Status = "Division exist with the same short name!!";
            ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Already exist with the same short name.\');", true);
        }
    }
    protected void btnSlNo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Division_SlNo.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Division_React.aspx");
    }
}