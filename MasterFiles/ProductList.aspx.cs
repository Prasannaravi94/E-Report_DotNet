using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProductList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProd= null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdDescr = string.Empty;
    string ProdName = string.Empty;
    string ProdSaleUnit = string.Empty;
    string sCmd = string.Empty;
    string Char = string.Empty;
    string val = string.Empty;
    string search = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       div_code = Session["div_code"].ToString();
        
        if (!Page.IsPostBack)
        {
            Session["Char"] = "All";
            FillProd();
            FillProd_Alpha();
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
   
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductDetail.aspx");
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductView.aspx");
    }
    
    private void FillProd()
    {
        Product dv = new Product();
        if (ddlSrch.SelectedValue == "1")
        {
            dsProd = dv.getProdall(div_code);
        }
        else if (ddlSrch.SelectedValue == "2" && val != "")
        {
            dsProd = dv.getProdforname(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "3" && val != "")
        {
            dsProd = dv.getProdforcat(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "4" && val != "")
        {
            dsProd = dv.getProdforgrp(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "5" && val != "")
        {
            dsProd = dv.getProdforbrd(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "6" && val != "")
        {
            dsProd = dv.getProdforSubdiv(div_code, val);
        }
        else if (ddlSrch.SelectedValue == "7" && val != "")
        {
            dsProd = dv.getProdforState(div_code, val);
        }
       
        else
        {
            dsProd = dv.getProdall(div_code);
        }

        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
            foreach (GridViewRow row in grdProduct.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");

                Label lblslide = (Label)row.FindControl("lblslide");

                if (lblslide.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
        }
    }
    private void FillProd(string sAlpha)
    {
        Product dv = new Product();
        dsProd = dv.getProd(div_code, sAlpha);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
            foreach (GridViewRow row in grdProduct.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");

                Label lblslide = (Label)row.FindControl("lblslide");

                if (lblslide.Text != "0")
                {
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdProduct.DataSource = dsProd;
            grdProduct.DataBind();
        }
    }
    private void FillProd_Alpha()
    {
        Product dv = new Product();
        dsProd = dv.getProd_Alphabet(div_code);
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsProd;
            dlAlpha.DataBind();
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
        dtGrid = dv.getProductlist_DataTable(div_code);
        sCmd = Session["Char"].ToString();
        if (sCmd == "All")
        {
            dtGrid = dv.getProductlist_DataTable(div_code);
        }
        else if (sCmd != "")
        {

            dtGrid = dv.getProductlist_DataTable(div_code, sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dtGrid = dv.getDTProduct_Nam(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
        }
        else if (ddlProCatGrp.SelectedIndex > 0)
        {
            search = ddlSrch.SelectedValue.ToString();

            if (search == "3")
            {
                dtGrid = dv.getDTProduct_Cat(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "4")
            {
                dtGrid = dv.getDTProduct_Grp(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "5")
            {
                dtGrid = dv.getDTProduct_Brd(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "6")
            {
                dtGrid = dv.getDTProduct_Sbdiv(div_code, ddlProCatGrp.SelectedValue);
            }
            else if (search == "7")
            {
                dtGrid = dv.getDTProduct_State(div_code, ddlProCatGrp.SelectedValue);
            }
            
        }
        return dtGrid;
    }


    protected void grdProduct_Sorting(object sender, GridViewSortEventArgs e)
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
        grdProduct.DataSource = sortedView;
        grdProduct.DataBind();
        foreach (GridViewRow row in grdProduct.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");

            Label lblslide = (Label)row.FindControl("lblslide");

            if (lblslide.Text != "0")
            {
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }

    }
    protected void btnBulk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdBulkEdit.aspx");
    }
    protected void grdProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdProduct.EditIndex = -1;
        //Fill the Division Grid
        sCmd = Session["Char"].ToString();
        Product prd = new Product();

        if (sCmd == "All")
        {
            FillProd();
        }
        else if (sCmd != "")
        {
            FillProd(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dsProduct = prd.getProdforname(div_code, TxtSrch.Text);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                dlAlpha.Visible = false;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
        }
        else if (ddlProCatGrp.SelectedIndex != -1)
        {
            Search();
        }
    }

    protected void grdProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdProduct.EditIndex = e.NewEditIndex;
        //Fill the Division Grid

        sCmd = Session["Char"].ToString();
        Product prd = new Product ();

        if (sCmd == "All")
        {
            FillProd();
        }
        else if (sCmd !="")
        {
            FillProd(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dsProduct = prd.getProdforname(div_code, TxtSrch.Text);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                dlAlpha.Visible = false;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
        }
        else if (ddlProCatGrp.SelectedIndex != -1)
        {
            Search();
        }
        
        //Setting the focus to the textbox "Product Name"        
        TextBox ctrl = (TextBox)grdProduct.Rows[e.NewEditIndex].Cells[2].FindControl("txtProName");
        ctrl.Focus();
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        
        string sCmd = e.CommandArgument.ToString();
        Session["Char"] = sCmd;

        if (sCmd == "All")
        {
            grdProduct.PageIndex = 0;
            FillProd();
        }
        else
        {
            grdProduct.PageIndex = 0;            
            FillProd(sCmd);
        }
        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }
    protected void grdProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdProduct.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateProd(iIndex);
        Product prd = new Product();

        sCmd = Session["Char"].ToString();

        if (sCmd == "All")
        {
            FillProd();
        }
        else if (sCmd != "")
        {
            FillProd(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dsProduct = prd.getProdforname(div_code, TxtSrch.Text);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                dlAlpha.Visible = false;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
        }
        else if (ddlProCatGrp.SelectedIndex != -1)
        {
            Search();
        }
    }

    protected void grdProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            ProdCode = Convert.ToString(e.CommandArgument);

            //Deactivate
            Product dv = new Product();
            DataSet dsProduct = dv.SlideExistProduct(ProdCode, div_code);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not possible to deactivate. Single slide upload has tagged');</script>");
            }
            else
            {
                int iReturn = dv.DeActivate(ProdCode);
                if (iReturn > 0)
                {
                    //menu1.Status = "Product has been Deactivated Successfully";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product has been Deactivated Successfully');</script>");
                }
                else
                {
                    // menu1.Status = "Unable to Deactivate";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
                }
                FillProd();
            }
        }
    }

    protected void grdProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");             
        }
    }

    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduct.PageIndex = e.NewPageIndex;

        //string sCmd = null;
        sCmd = Session["Char"].ToString();
        Product prd = new Product();

        if (sCmd == "All")
        {
            FillProd();
        }
        else if (sCmd != "")
        {
            FillProd(sCmd);
        }
        else if (TxtSrch.Text != "")
        {
            dsProduct = prd.getProdforname(div_code, TxtSrch.Text);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                dlAlpha.Visible = false;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
        }
        else if (ddlProCatGrp.SelectedIndex != -1)
        {
            Search();
        }
    }    
    private void UpdateProd(int eIndex)
    {
        System.Threading.Thread.Sleep(time);
        Label lblProdCode = (Label)grdProduct.Rows[eIndex].Cells[1].FindControl("lblProdCode");
        ProdCode = lblProdCode.Text;
        TextBox txtProName = (TextBox)grdProduct.Rows[eIndex].Cells[2].FindControl("txtProName");
        ProdName = txtProName.Text.Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", "");
        TextBox txtProDesc = (TextBox)grdProduct.Rows[eIndex].Cells[3].FindControl("txtProDesc");
        ProdDescr = txtProDesc.Text;       
        TextBox txtSaleUn = (TextBox)grdProduct.Rows[eIndex].Cells[3].FindControl("txtSaleUn");
        ProdSaleUnit = txtSaleUn.Text;
       
         //Update Product
        Product dv = new Product();
        int iReturn = dv.RecordUpdateProd(ProdCode, ProdName, ProdSaleUnit, ProdDescr, div_code);
         if (iReturn > 0 )
        {
            //menu1.Status = "Product Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Product already exist!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name already exist');</script>");
        }
    }

    protected void btnSno_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdSlNo_Gen.aspx");
    }

    protected void btnCatMap_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProdCatgMap.aspx");
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductBulkCreate.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Product_Reactivate.aspx");
    }
       private void FillCategory()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Cat_Name";
            ddlProCatGrp.DataValueField = "Product_Cat_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    
    }

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct =prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField ="Product_Grp_Name";
            ddlProCatGrp.DataValueField = "Product_Grp_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    
    }

    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.GetProductBrand(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Brd_Name";
            ddlProCatGrp.DataValueField = "Product_Brd_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }

    private void FillSubdiv()
    {
        Product prd =new Product ();
        dsProduct = prd.getSubdiv(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "subdivision_name";
            ddlProCatGrp.DataValueField = "subdivision_code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    
    
    }
  

    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProCatGrp.Visible = true;
        int search = Convert.ToInt32(ddlSrch.SelectedValue);
        TxtSrch.Text = string.Empty;
        if (search == 2)
        {
            TxtSrch.Visible = true;
            Btnsrc.Visible = true;
            ddlProCatGrp.Visible = false;
        }
        else
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = true;
            Btnsrc.Visible = true;
        }
        if (search == 1)
        {
            TxtSrch.Visible = false;
            ddlProCatGrp.Visible = false;
            Btnsrc.Visible = false;
            FillProd();
        }
        if (search == 3)
        {
            FillCategory();
        }
        if (search == 4)
        {
            FillGroup();
        }
        if (search == 5)
        {
            FillBrand();
        }
        if (search == 6)
        {
            FillSubdiv();
        }
        if (search == 7)
        {
            FillState(div_code);
        }
        val = "";
        FillProd();

  }
    //Changes done by Priya
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getState(state_cd);
            ddlProCatGrp.DataTextField = "statename";
            ddlProCatGrp.DataValueField = "state_code";
            ddlProCatGrp.DataSource = dsState;
            ddlProCatGrp.DataBind();
        }
    }
    protected void ddlProCatGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        val = ddlProCatGrp.SelectedValue;
        FillProd();

    }
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["Char"] = string.Empty;
        grdProduct.PageIndex = 0;
        Search();
       
   }
    protected void btnProdCodeChg_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Prod_Code_Chg.aspx");
    }
    private void Search()
    {
        search = ddlSrch.SelectedValue.ToString();
        Product prd = new Product();
        if (search == "1")
        {
            FillProd();
        }
        if (search == "2")
        {
            
            // FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            dsProduct = prd.FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == "3")
        {
            dsProduct = prd.getProdforcat(div_code, ddlProCatGrp.SelectedValue);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == "4")
        {
            dsProduct = prd.getProdforgrp(div_code, ddlProCatGrp.SelectedValue);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == "5")
        {
            dsProduct = prd.getProdforbrd(div_code, ddlProCatGrp.SelectedValue);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == "6")
        {
            dsProduct = prd.getProdforSubdiv(div_code, ddlProCatGrp.SelectedValue);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
        if (search == "7")
        {
            dsProduct = prd.getProdforState(div_code, ddlProCatGrp.SelectedValue);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                dlAlpha.Visible = true;
                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                foreach (GridViewRow row in grdProduct.Rows)
                {
                    LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                    Label lblimg = (Label)row.FindControl("lblimg");

                    Label lblslide = (Label)row.FindControl("lblslide");

                    if (lblslide.Text != "0")
                    {
                        lnkdeact.Visible = false;
                        lblimg.Visible = true;
                    }
                }
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
                dlAlpha.Visible = false;
            }
        }
    }

    protected void btnStateWiseProduct_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductMap_Statewise.aspx");
    }
    protected void btnSubDivProductMap_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);

        Response.Redirect("Product_Map_Sub_Divisionwise.aspx");        
    }
}