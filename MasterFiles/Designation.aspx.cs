using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Designation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDesignation = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    int Designation_Code = 0;
    string Designation_Short_Name = string.Empty;
    string Designation_Name = string.Empty;
    string Desig_Color = string.Empty;
    string type = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }
      
        if (!Page.IsPostBack)
        {
            Filldiv();
            ddlDivision.SelectedIndex = 0;
            fillDesignation();
            menu1.Title = this.Page.Title;
          //  //// menu1.FindControl("btnBack").Visible = false;
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
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    private void fillDesignation()
    {
     
        Designation des = new Designation();
        dsDesignation = des.getDesignation_count(ddlDivision.SelectedValue.ToString());
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdDesignation.Visible = true;
            grdDesignation.DataSource = dsDesignation;
            grdDesignation.DataBind();
            foreach (GridViewRow row in grdDesignation.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                if (Convert.ToInt32(dsDesignation.Tables[0].Rows[row.RowIndex][6].ToString()) > 0)
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
            
        }
        else
        {
            divid.Visible = true;
            grdDesignation.DataSource = dsDesignation;
            grdDesignation.DataBind();
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
        Designation des = new Designation();
        dtGrid = des.getDesignation_DataTable(ddlDivision.SelectedValue.ToString());
        return dtGrid;
    }

    protected void grdDesignation_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDesignation.DataSource = dtGrid;
        grdDesignation.DataBind();

        foreach (GridViewRow row in grdDesignation.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][6].ToString()) > 0)
            {
                // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }

    protected void grdDesignation_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {        
        grdDesignation.EditIndex = -1;       
        fillDesignation();
    }

    protected void grdDesignation_RowEditing(object sender, GridViewEditEventArgs e)
    {     
        grdDesignation.EditIndex = e.NewEditIndex;
        fillDesignation();     
        TextBox ctrl = (TextBox)grdDesignation.Rows[e.NewEditIndex].Cells[2].FindControl("txtShortName");
        ctrl.Focus();
    }
    protected void grdDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            //Label lblDesignationCode = (Label)grdDesignation.Rows[e.RowIndex].Cells[1].FindControl("lblDesignationCode");
            //Designation_Code = Convert.ToInt16(lblDesignationCode.Text);

            Designation_Code = Convert.ToInt16(e.CommandArgument);
            Designation des = new Designation();
            int iReturn = des.DeActivate(Designation_Code, division_code);
            if (iReturn > 0)
            {
                // menu1.Status = "Designation deleted Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
          //  else
          //  {
                // menu1.Status ="Unable to Deactivate";
              //  ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
          //  }

            fillDesignation();
        }
    }
    protected void grdDesignation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDesignation.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateDesignation(iIndex);
        fillDesignation();
    }
    protected void grdDesignation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
        //    e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        //}
    }

    protected void grdDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDesignation.PageIndex = e.NewPageIndex;
        fillDesignation();
    }
    private void UpdateDesignation(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblDesignationCode = (Label)grdDesignation.Rows[eIndex].Cells[1].FindControl("lblDesignationCode");
        Designation_Code = Convert.ToInt16(lblDesignationCode.Text);
        TextBox txtShortName = (TextBox)grdDesignation.Rows[eIndex].Cells[2].FindControl("txtShortName");
        Designation_Short_Name = txtShortName.Text;
        TextBox txtDesignationName = (TextBox)grdDesignation.Rows[eIndex].Cells[3].FindControl("txtDesignationName");
        Designation_Name = txtDesignationName.Text;
        Label lbldivi = (Label)grdDesignation.Rows[eIndex].Cells[8].FindControl("lbldivi");
        division_code = lbldivi.Text;
       
        Designation des = new Designation();
        int iReturn = des.RecordUpdate_Inline(Designation_Code, Designation_Short_Name, Designation_Name, division_code);
        if (iReturn > 0)
        {
            //menu1.Status = "State/Location Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "State/Location already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Designation Short Name Already Exist');</script>");
        }
    }


    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("DesignationCreation.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Designation_SlNo.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Designation_React.aspx");
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillDesignation();
    }
    protected void btnDes_level_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Designation_level.aspx?div_code=" + ddlDivision.SelectedValue + "");
    }
}