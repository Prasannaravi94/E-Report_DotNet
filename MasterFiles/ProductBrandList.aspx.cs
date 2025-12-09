using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductBrandList : System.Web.UI.Page
{
    #region Declaration
    DataSet dsProBrd = null;
    int ProBrdCode = 0;
    string dive_code = string.Empty;
    string Product_Brd_SName = string.Empty;
    string Product_Brd_Name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        dive_code = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillProBrd();
            btnNew.Focus();
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;
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

    private void FillProBrd()
    {
        Product dv = new Product();
        dsProBrd = dv.getProBrd(dive_code);

        if (dsProBrd.Tables[0].Rows.Count > 0)
        {
            grdProBra.Visible = true;
            grdProBra.DataSource = dsProBrd;
            grdProBra.DataBind();

            foreach (GridViewRow row in grdProBra.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                LinkButton lnkcount = (LinkButton)row.FindControl("lnkcount");
                Label lblslide = (Label)row.FindControl("lblslide");
                // if (Convert.ToInt32(dsProBrd.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                if (lnkcount.Text != "0" || lblslide.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdProBra.DataSource = dsProBrd;
            grdProBra.DataBind();
        }      
    }

    //sorting

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
        Product dv = new Product();
        dtGrid = dv.getProductBrandlist_DataTable(dive_code);
        return dtGrid;
    }

    protected void grdProBra_Sorting(object sender, GridViewSortEventArgs e)
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
        grdProBra.DataSource = dtGrid;
        grdProBra.DataBind();

        Product dv = new Product();

        foreach (GridViewRow row in grdProBra.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            LinkButton lnkcount = (LinkButton)row.FindControl("lnkcount");
            Label lblslide = (Label)row.FindControl("lblslide");
            //if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][3].ToString()) > 0)
            if (lnkcount.Text != "0" || lblslide.Text != "0")
            {
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }

    protected void grdProBra_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdProBra.EditIndex = -1;
        //Fill the State Grid
        FillProBrd();
    }
    protected void grdProBra_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void grdProBra_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdProBra.EditIndex = e.NewEditIndex;
        FillProBrd();
        TextBox ctrl = (TextBox)grdProBra.Rows[e.NewEditIndex].Cells[2].FindControl("txtProduct_Bra_SName");
        ctrl.Focus();
    }
    protected void grdProBra_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblProBraCode = (Label)grdProBra.Rows[e.RowIndex].Cells[1].FindControl("lblProBraCode");
        ProBrdCode = Convert.ToInt16(lblProBraCode.Text);

        //Delete Product Brand
        Product dv = new Product();
        int iReturn = dv.Brd_RecordDelete(ProBrdCode);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }

        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillProBrd();
    }

    protected void grdProBra_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ProBrdCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate

            Product dv = new Product();
            int iReturn = dv.Brd_DeActivate(ProBrdCode);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillProBrd();
        }
    }

    protected void grdProBra_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdProBra.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillProBrd();
    }

    protected void grdProBra_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdProBra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProBra.PageIndex = e.NewPageIndex;
        FillProBrd();
    }
    private void Update(int eIndex)
    {
        Label lblProBraCode = (Label)grdProBra.Rows[eIndex].Cells[1].FindControl("lblProBraCode");
        ProBrdCode = Convert.ToInt16(lblProBraCode.Text);
        TextBox txtProduct_Bra_SName = (TextBox)grdProBra.Rows[eIndex].Cells[2].FindControl("txtProduct_Bra_SName");
        Product_Brd_SName = txtProduct_Bra_SName.Text;
        TextBox txtProBraName = (TextBox)grdProBra.Rows[eIndex].Cells[3].FindControl("txtProBraName");
        Product_Brd_Name = txtProBraName.Text;


        //Upadte Product Brand
        Product dv = new Product();
        int iReturn = dv.Brd_RecordUpdate(ProBrdCode, Product_Brd_SName, Product_Brd_Name,dive_code);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
            txtProBraName.Focus();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Short Name Already Exist');</script>");
            txtProduct_Bra_SName.Focus();
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductBrand.aspx");
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditProdBrd.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdBrd_SlNo_Gen.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Pro_Brd_React.aspx");
    }

}