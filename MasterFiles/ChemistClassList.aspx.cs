using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ChemistCategoryList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsChemClass = null;
    int ChemClassCode = 0;
    string divcode = string.Empty;
    string Chem_Class_SName = string.Empty;
    string ChemClassName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        heading.InnerText = this.Page.Title;

        if (!Page.IsPostBack)
        {
            FillChemClass();
            btnNew.Focus();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            // menu1.FindControl("btnBack").Visible = false;
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
    private void FillChemClass()
    {
        Chemist chem = new Chemist();
        dsChemClass = chem.getChemClass(divcode);
        if (dsChemClass.Tables[0].Rows.Count > 0)
        {
            grdChemClass.Visible = true;
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();

            foreach (GridViewRow row in grdChemClass.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                if (lblCount.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdChemClass.DataSource = dsChemClass;
            grdChemClass.DataBind();
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
        Chemist chem = new Chemist();
        dtGrid = chem.getChemistClasslist_DataTable(divcode);
        return dtGrid;
    }

    protected void grdChemClass_Sorting(object sender, GridViewSortEventArgs e)
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
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        grdChemClass.DataSource = dtGrid;
        grdChemClass.DataBind();

        foreach (GridViewRow row in grdChemClass.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][3].ToString()) > 0)
            {
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }

    protected void grdChemClass_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdChemClass.EditIndex = -1;
        FillChemClass();
    }
    protected void grdChemClass_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdChemClass.EditIndex = e.NewEditIndex;
        FillChemClass();
        TextBox ctrl = (TextBox)grdChemClass.Rows[e.NewEditIndex].Cells[2].FindControl("txtChem_Class_SName");
        ctrl.Focus();
    }
    protected void grdChemClass_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblChemClassCode = (Label)grdChemClass.Rows[e.RowIndex].Cells[1].FindControl("lblChemClassCode");
        ChemClassCode = Convert.ToInt16(lblChemClassCode.Text);

        Chemist chem = new Chemist();
        int iReturn = chem.RecordDeleteChemClass(ChemClassCode);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillChemClass();
    }
    protected void grdChemClass_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ChemClassCode = Convert.ToInt16(e.CommandArgument);

            Chemist chem = new Chemist();
            int iReturn = chem.DeActivateChemClass(ChemClassCode);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillChemClass();
        }

    }
    protected void grdChemClass_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdChemClass.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillChemClass();
    }
    protected void grdChemClass_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdChemClass_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemClass.PageIndex = e.NewPageIndex;
        FillChemClass();
    }
    private void Update(int eIndex)
    {
        Label lblChemClassCode = (Label)grdChemClass.Rows[eIndex].Cells[1].FindControl("lblChemClassCode");
        ChemClassCode = Convert.ToInt16(lblChemClassCode.Text);
        TextBox txtChem_Class_SName = (TextBox)grdChemClass.Rows[eIndex].Cells[1].FindControl("txtChem_Class_SName");
        Chem_Class_SName = txtChem_Class_SName.Text;
        TextBox txtChemClassName = (TextBox)grdChemClass.Rows[eIndex].Cells[1].FindControl("txtChemClassName");
        ChemClassName = txtChemClassName.Text;

        Chemist chem = new Chemist();
        int iReturn = chem.RecordUpdate_ChemClass_code(ChemClassCode, Chem_Class_SName, ChemClassName, divcode);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            txtChemClassName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            txtChemClassName.Focus();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["backurl"] = "ChemistClassList.aspx";
        Response.Redirect("ChemistClass.aspx");
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditChemClass.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ChemClass_SlNo_Gen.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Chem_Class_React.aspx");
    }
    protected void btnTransfer_Class_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Chem_Class_Trans.aspx");
    }
}