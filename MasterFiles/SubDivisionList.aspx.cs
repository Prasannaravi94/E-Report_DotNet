using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_SubDivisionList : System.Web.UI.Page
{
#region "Declaration"
DataSet dsSubDiv = null;
int subdivcode = 0;
int subdivision_code = 0;
string divcode = string.Empty;
string subdiv_sname = string.Empty;
string subdiv_name = string.Empty;
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
            FillSubdiv();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    } 
    private void FillSubdiv()
    {
        SubDivision dv = new SubDivision();
        dsSubDiv = dv.getSubDiv(divcode);
        if (dsSubDiv.Tables[0].Rows.Count > 0)
        {
            grdSubDiv.Visible = true;
            grdSubDiv.DataSource = dsSubDiv;
            grdSubDiv.DataBind();

            foreach (GridViewRow row in grdSubDiv.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblSubDiv_count = (Label)row.FindControl("lblSubDiv_count");
                Label lblSubfield_count = (Label)row.FindControl("lblSubfield_count");
                if ((lblSubDiv_count.Text != "0") || (lblSubfield_count.Text != "0"))
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdSubDiv.DataSource = dsSubDiv;
            grdSubDiv.DataBind();
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
        dtGrid = dv.getSubDivisionlist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdSubDiv_Sorting(object sender, GridViewSortEventArgs e)
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
        grdSubDiv.DataSource = sortedView;
        grdSubDiv.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SubDivisionCreation.aspx");
    }
    protected void grdSubDiv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdSubDiv.EditIndex = -1;
        //Fill the Grid
        FillSubdiv();
    }

    protected void grdSubDiv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdSubDiv.EditIndex = e.NewEditIndex;
        //Fill the  Grid
        FillSubdiv();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdSubDiv.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
    //protected void grdSubDiv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    Label lblsubdivCode = (Label)grdSubDiv.Rows[e.RowIndex].Cells[1].FindControl("lblsubdivCode");
    //    subdivcode = Convert.ToInt16(lblsubdivCode.Text);

    //    // Delete SubDivision
    //    SubDivision dv = new SubDivision();
    //    int iReturn = dv.RecordDelete(subdivcode);
    //     if (iReturn > 0 )
    //    {
    //        //menu1.Status = "Record Deleted Successfully ";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Deleted Successfully');</script>");
    //    }
    //    else if (iReturn == -2)
    //    {
    //        //menu1.Status = "Record already Exist";
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record already Exist');</script>");
    //    }
    //    FillSubdiv();
    //}

    //Changes done by priya
    //begin
    protected void grdSubDiv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
           // subdivcode = Convert.ToString(e.CommandArgument);
            subdivision_code = Convert.ToInt16(e.CommandArgument);
            //Deactivate
            SubDivision dv = new SubDivision();
            int iReturn = dv.DeActivate(subdivision_code);
            if (iReturn > 0)
            {                
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deactivated Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillSubdiv();
        }
    }
    //end
    protected void grdSubDiv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdSubDiv.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillSubdiv();
    }
    protected void grdSubDiv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdSubDiv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSubDiv.PageIndex = e.NewPageIndex;
        FillSubdiv();
    }
    private void Update(int eIndex)
    {
        Label lblsubdivCode = (Label)grdSubDiv.Rows[eIndex].Cells[1].FindControl("lblsubdivCode");
        subdivcode = Convert.ToInt16(lblsubdivCode.Text);
        TextBox txtShortName = (TextBox)grdSubDiv.Rows[eIndex].Cells[2].FindControl("txtShortName");
        subdiv_sname = txtShortName.Text;
        TextBox txtSubDivName = (TextBox)grdSubDiv.Rows[eIndex].Cells[3].FindControl("txtSubDivName");
        subdiv_name = txtSubDivName.Text;

        // Update Sub Division
        SubDivision dv = new SubDivision();
        int iReturn = dv.RecordUpdate(subdivcode, subdiv_sname, subdiv_name,divcode);
         if (iReturn > 0 )
        {
            //menu1.Status = "Sub Division Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {

             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Name Already Exist');</script>");
             txtShortName.Focus();
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Short Name Already Exist');</script>");
             txtSubDivName.Focus();
         }
    }
}