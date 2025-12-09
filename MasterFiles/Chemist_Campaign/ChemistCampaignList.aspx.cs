using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Chemist_Campaign_ChemistCampaignList : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsCheSubCat = null;
    int CheSubCatCode = 0;
    string divcode = string.Empty;
    string Che_SubCat_SName = string.Empty;
    string CheSubCatName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillChmSubCat();
            btnNew.Focus();
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    
    private void FillChmSubCat()
    {
        ChemistCampaign Chm = new ChemistCampaign();
        dsCheSubCat = Chm.getChemSubCat(divcode);
        if (dsCheSubCat.Tables[0].Rows.Count > 0)
        {
            grdChemistCap.Visible = true;
            grdChemistCap.DataSource = dsCheSubCat;
            grdChemistCap.DataBind();
        }
        else
        {
            grdChemistCap.DataSource = dsCheSubCat;
            grdChemistCap.DataBind();
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
        Doctor dv = new Doctor();
        dtGrid = dv.getDocSubCatlist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdChemistCap_Sorting(object sender, GridViewSortEventArgs e)
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
        grdChemistCap.DataSource = sortedView;
        grdChemistCap.DataBind();
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ChemistCampaign.aspx");
    }
    protected void grdChemistCap_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdChemistCap.EditIndex = -1;
        //Fill the State Grid
        FillChmSubCat();
    }

    protected void grdChemistCap_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdChemistCap.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillChmSubCat();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdChemistCap.Rows[e.NewEditIndex].Cells[2].FindControl("txtChe_SubCat_SName");
        ctrl.Focus();
    }
    protected void grdChemistCap_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblCheSubCatCode = (Label)grdChemistCap.Rows[e.RowIndex].Cells[1].FindControl("lblChe_SubCat_SName");
        CheSubCatCode = Convert.ToInt16(lblCheSubCatCode.Text);

        // Delete Doctor Sub-Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDeleteSubCat(CheSubCatCode);
        if (iReturn > 0)
        {
            //menu1.Status = "Doctor Sub-Category Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            // menu1.Status = "Doctor Sub-Category cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillChmSubCat();
    }
    protected void grdChemistCap_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            CheSubCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            ChemistCampaign dv = new ChemistCampaign();
            int iReturn = dv.DeActivateSubCat(CheSubCatCode);
            if (iReturn > 0)
            {
                //menu1.Status = "Doctor Sub-Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillChmSubCat();
        }
    }

    protected void grdChemistCap_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdChemistCap.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillChmSubCat();
    }
    protected void grdChemistCap_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdChemistCap_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdChemistCap.PageIndex = e.NewPageIndex;
        FillChmSubCat();
    }
    private void Update(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblCheSubCatCode = (Label)grdChemistCap.Rows[eIndex].Cells[1].FindControl("lblCheSubCatCode");
        CheSubCatCode = Convert.ToInt16(lblCheSubCatCode.Text);
        TextBox txtChe_SubCat_SName = (TextBox)grdChemistCap.Rows[eIndex].Cells[2].FindControl("txtChe_SubCat_SName");
        Che_SubCat_SName = txtChe_SubCat_SName.Text;
        TextBox txtCheSubCatName = (TextBox)grdChemistCap.Rows[eIndex].Cells[3].FindControl("txtCheSubCatName");
        CheSubCatName = txtCheSubCatName.Text;

        // Update Doctor Sub-Category
        ChemistCampaign dv = new ChemistCampaign();
        int iReturn = dv.RecordUpdateCheSubCat(CheSubCatCode, Che_SubCat_SName, CheSubCatName, divcode);
        if (iReturn > 0)
        {
            // menu1.Status = "Doctor Sub-Category Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            FillChmSubCat();
        }
        else if (iReturn == -2)
        {
            // menu1.Status = "Doctor Sub-Category already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Campaign Name Already Exist');</script>");
            txtCheSubCatName.Focus();
        }
        else if (iReturn == -3)
        {
            // menu1.Status = "Doctor Sub-Category already Exist";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
            txtChe_SubCat_SName.Focus();
        }
    }

    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditCheCamp.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("CheCamp_SlNo_Gen.aspx");
    }

    protected void btnReact_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("CheCamp_React.aspx");
    }
}