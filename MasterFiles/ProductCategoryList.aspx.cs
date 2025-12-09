using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductCategoryList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProCat = null;
    int ProCatCode = 0;
    string divcode = string.Empty;
    string Product_Cat_SName = string.Empty;
    string ProCatName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {
            FillProCat();
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
    private void FillProCat()
    {
        
        Product dv = new Product();
        dsProCat = dv.getProCat(divcode);
        
        if (dsProCat.Tables[0].Rows.Count > 0)
        {
            grdProCat.Visible = true;
            grdProCat.DataSource = dsProCat;
            grdProCat.DataBind();            
            //for (int i = 0; i < dsProCat.Tables[0].Rows.Count; i++)
            //{                
              foreach (GridViewRow row in grdProCat.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                  Label lblimg = (Label)row.FindControl("lblimg");
                  LinkButton lnkcount = (LinkButton)row.FindControl("lnkcount");
               // if (Convert.ToInt32(dsProCat.Tables[0].Rows[row.RowIndex][3].ToString()) > 0)
                  if(lnkcount.Text != "0")
                {
                   // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible =true;
                }
            }
        }
        else
        {
            grdProCat.DataSource = dsProCat;
            grdProCat.DataBind();
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
        Product dv = new Product();
        dtGrid = dv.getProductCategorylist_DataTable(divcode);
        return dtGrid;
    }
  
    protected void grdProCat_Sorting(object sender, GridViewSortEventArgs e)
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
        grdProCat.DataSource = dtGrid;
        grdProCat.DataBind();
     
        Product dv = new Product();
      //  dtGrid = dv.getProductCategorylist_DataTable(divcode);
        foreach (GridViewRow row in grdProCat.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][3].ToString()) > 0)
            {
                // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                lnkdeact.Visible = false;
               lblimg.Visible = true;
            }
        }
    }
    protected void grdProCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdProCat.EditIndex = -1;
        //Fill the State Grid
        FillProCat();
    }
    protected void grdProCat_RowDataBound(object sender, GridViewRowEventArgs e)
    {     

    }
    protected void grdProCat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdProCat.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillProCat();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdProCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtProduct_Cat_SName");
        ctrl.Focus();
    }
    protected void grdProCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblProCatCode = (Label)grdProCat.Rows[e.RowIndex].Cells[1].FindControl("lblProCatCode");
        ProCatCode = Convert.ToInt16(lblProCatCode.Text);

        // Delete Product Category
        Product dv = new Product();
        int iReturn = dv.RecordDelete(ProCatCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Product Category Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
           // menu1.Status = "Product Category cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillProCat();
    }
    protected void grdProCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ProCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            int iReturn = dv.DeActivate(ProCatCode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Product Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
              //  menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillProCat();
        }
    }

    protected void grdProCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdProCat.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillProCat();
    }
    protected void grdProCat_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdProCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProCat.PageIndex = e.NewPageIndex;
        FillProCat();
    }
    private void Update(int eIndex)
    {
        Label lblProCatCode = (Label)grdProCat.Rows[eIndex].Cells[1].FindControl("lblProCatCode");
        ProCatCode = Convert.ToInt16(lblProCatCode.Text);
        TextBox txtProduct_Cat_SName = (TextBox)grdProCat.Rows[eIndex].Cells[2].FindControl("txtProduct_Cat_SName");
        Product_Cat_SName = txtProduct_Cat_SName.Text;
        TextBox txtProCatName = (TextBox)grdProCat.Rows[eIndex].Cells[3].FindControl("txtProCatName");
        ProCatName = txtProCatName.Text;

        // Update Product Category
        Product dv = new Product();
        int iReturn = dv.RecordUpdate(ProCatCode, Product_Cat_SName, ProCatName, divcode);
         if (iReturn > 0 )
        {
            //menu1.Status = "Product Category Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
             txtProCatName.Focus();
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Short Name Already Exist');</script>");
             txtProduct_Cat_SName.Focus();
         }
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductCategory.aspx");
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditProdCat.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdCat_SlNo_Gen.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Pro_Cat_React.aspx");
    }
}