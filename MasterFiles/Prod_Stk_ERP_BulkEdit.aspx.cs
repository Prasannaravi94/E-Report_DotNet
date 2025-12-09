using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Prod_Stk_ERP_BulkEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsState = null;
    bool bsrch = false;
    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string search = string.Empty;
    string ProdName = string.Empty;
    int i;
    int iReturn = -1;
    string sChkLocation = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string val = string.Empty;
    string Text = string.Empty;

    int iIndex;
    int kIndex;
    string subdivision_code = string.Empty;
    string sub_division = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductList.aspx";
            menu1.Title = this.Page.Title;
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
    private void FillProd()
    {
        Product dv = new Product();
        if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
        {
            dsProd = dv.getStk_Edit(div_code);

            if (dsProd.Tables[0].Rows.Count > 0)
            {
                grdStk.Visible = true;
                grdProduct.Visible = false;
                grdStk.DataSource = dsProd;
                grdStk.DataBind();
            }
        }
        else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
        {
            dsProd = dv.getProd_Edit(div_code);

            if (dsProd.Tables[0].Rows.Count > 0)
            {
                grdProduct.Visible = true;
                grdStk.Visible = false;
                grdProduct.DataSource = dsProd;
                grdProduct.DataBind();
            }
        }
    }

    protected DataSet FillCategory()
    {
        Product prd = new Product();
        dsProd = prd.getProductCategory(div_code);
        return dsProd;
    }

    protected DataSet FillBrand()
    {
        Product prd = new Product();
        dsProd = prd.GetProductBrand(div_code);
        return dsProd;
    }


    protected void grdProduct_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            //e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdStk_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("onmouseover", "this.className='highlight_clr'");
            //e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["Char"] = string.Empty;
        grdProduct.PageIndex = 0;

        //for (i = 0; i < CblProdCode.Items.Count; i++)
        //{
        //    if (CblProdCode.Items[i].Selected == true)
        //    {
        //        bsrch = true;
        //    }
        //}

        //if (bsrch == true)
        //{
        //    tblProduct.Visible = true;
        //    btnUpdate.Visible = true;
        //}

        tblProduct.Visible = true;
        btnUpdate.Visible = true;
        Product dv = new Product();

        if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
        {
            if (ddlstk.SelectedValue == "1")
            {
                dsProd = dv.getStkall(div_code);
            }
            else if (ddlstk.SelectedValue == "2")
            {
                dsProd = dv.getStkforname(div_code, TxtSrch.Text);
            }
            else if (ddlstk.SelectedValue == "3" && val != "")
            {
                dsProd = dv.getStkforHQ(div_code, Text);
            }
            else if (ddlstk.SelectedValue == "7" && val != "")
            {
                dsProd = dv.getStkforState(div_code, Text);
            }
            else if (ddlstk.SelectedValue == "8")
            {
                dsProd = dv.getStkforERP(div_code, TxtSrch.Text);
            }
            else
            {
                dsProd = dv.getStkall(div_code);
            }
        }
        else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
        {
            if (ddlSrch.SelectedValue == "1")
            {
                dsProd = dv.getProdall(div_code);
            }
            else if (ddlSrch.SelectedValue == "2")
            {
                dsProd = dv.getProdforname(div_code, TxtSrch.Text);
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
        }




        if (dsProd.Tables[0].Rows.Count > 0)
        {
            if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
            {
                grdStk.Visible = true;
                grdProduct.Visible = false;
                grdStk.DataSource = dsProd;
                grdStk.DataBind();
            }
            else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
            {
                grdProduct.Visible = true;
                grdStk.Visible = false;
                grdProduct.DataSource = dsProd;
                grdProduct.DataBind();
            }
        }
        else
        {
            if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
            {
                grdStk.DataSource = dsProd;
                grdStk.DataBind();
            }
            else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
            {
                grdProduct.DataSource = dsProd;
                grdProduct.DataBind();
            }
        }

        foreach (GridViewRow gridRow in grdProduct.Rows)
        {
            Label lblProdCode = (Label)gridRow.Cells[1].FindControl("lblProdCode");
            ProdCode = lblProdCode.Text.ToString();
        }
    }


    //protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DropDownList ddlCatg = (DropDownList)e.Row.FindControl("Product_Cat_Code");
    //        if (ddlCatg != null)
    //        {
    //            DataRowView row = (DataRowView)e.Row.DataItem;
    //            ddlCatg.SelectedIndex = ddlCatg.Items.IndexOf(ddlCatg.Items.FindByValue(row["Product_Cat_Code"].ToString()));
    //        }
    //    }
    //}

    protected void btnClr_Click(object sender, EventArgs e)
    {
        for (i = 0; i < rdolstrpt.Items.Count; i++)
        {
            rdolstrpt.Items[i].Enabled = true;
            rdolstrpt.Items[i].Selected = false;
        }

        grdProduct.DataSource = null;
        grdProduct.DataBind();
        tblProduct.Visible = false;
        btnUpdate.Visible = false;
    }
    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduct.PageIndex = e.NewPageIndex;
        FillProd();
    }

    protected void grdStk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStk.PageIndex = e.NewPageIndex;
        FillProd();
    }

    protected void ddlProCatGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        val = ddlProCatGrp.SelectedValue;
        Text = ddlProCatGrp.SelectedItem.ToString();
        FillProd();

    }
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProCatGrp.Visible = true;
        int search = 0;
        if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
        {
            search = Convert.ToInt32(ddlstk.SelectedValue);
        }
        else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
        {
            search = Convert.ToInt32(ddlSrch.SelectedValue);
        }
        TxtSrch.Text = string.Empty;
        if (search == 2 || search == 8)
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
            if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
            {

            }
            else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
            {
                FillProd();
            }
        }
        if (search == 3)
        {
            if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
            {
                FillHQ();
            }
            else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
            {
                FillCategory1();
            }
        }
        if (search == 4)
        {
            FillGroup();
        }
        if (search == 5)
        {
            FillBrand1();
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
        // FillProd();

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
    private void FillCategory1()
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
    private void FillBrand1()
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

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Product_Grp_Name";
            ddlProCatGrp.DataValueField = "Product_Grp_Code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }

    }

    private void FillSubdiv()
    {
        Product prd = new Product();
        dsProduct = prd.getSubdiv(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "subdivision_name";
            ddlProCatGrp.DataValueField = "subdivision_code";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }

    }
    private void FillHQ()
    {
        Stockist stk = new Stockist();
        dsProduct = stk.getPool_Name(div_code, "admin");
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlProCatGrp.DataTextField = "Pool_Name";
            ddlProCatGrp.DataValueField = "Pool_Id";
            ddlProCatGrp.DataSource = dsProduct;
            ddlProCatGrp.DataBind();
        }
    }

    private void Search()
    {
        search = ddlSrch.SelectedValue.ToString();
        if (search == "2")
        {
            Product prd = new Product();
            // FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            dsProduct = prd.FindProduct(ddlSrch.SelectedValue, TxtSrch.Text, Session["div_code"].ToString());
            if (dsProduct.Tables[0].Rows.Count > 0)
            {

                grdProduct.Visible = true;
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();
            }
            else
            {
                grdProduct.DataSource = dsProduct;
                grdProduct.DataBind();

            }
        }
    }
    //change done by saravanan
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string cntrl = string.Empty;
        string prod_code = string.Empty;
        string strTextBox = string.Empty;
        string stxt = string.Empty;
        if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
        {
            for (i = 0; i < grdProduct.Rows.Count; i++)
            {
                Label lblProdCode = (Label)grdProduct.Rows[i].Cells[0].FindControl("lblProdCode");

                TextBox txtSamp_Erp = (TextBox)grdProduct.Rows[i].Cells[6].FindControl("txtSamp_Erp");
                stxt = txtSamp_Erp.Text;
                strTextBox = "Sample_Erp_Code" + "= '" + stxt + "',";

                if (strTextBox.Trim().Length > 0)
                {
                    strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                    Product prd = new Product();
                    iReturn = prd.BulkEdit(strTextBox, lblProdCode.Text, div_code);
                    //iReturn = prd.BulkEdit(lblProdCode.Text, txtProductName.Text, txtproductDesc.Text, ddlProductCatCode.SelectedValue, ddlProduct_Type_Code.SelectedValue, txtProduct_Sale_Unit.Text, txtProduct_Sample_Unit_one.Text, txtProduct_Sample_Unit_Two.Text, txtProduct_Sample_Unit_Three.Text, strstate, strSubstate);
                    strTextBox = "";
                }
            }
        }
        else if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
        {
            for (i = 0; i < grdStk.Rows.Count; i++)
            {
                Label lblProdCode = (Label)grdStk.Rows[i].Cells[0].FindControl("lblStockistCode");

                TextBox txtStk_Erp = (TextBox)grdStk.Rows[i].Cells[6].FindControl("txtstk_Erp");
                stxt = txtStk_Erp.Text;
                strTextBox = "Stockist_Designation" + "= '" + stxt + "',";

                if (strTextBox.Trim().Length > 0)
                {
                    strTextBox = strTextBox + " LastUpdt_Date = getdate() ";
                    Product prd = new Product();
                    iReturn = prd.BulkEdit_Stk(strTextBox, lblProdCode.Text, div_code);
                    //iReturn = prd.BulkEdit(lblProdCode.Text, txtProductName.Text, txtproductDesc.Text, ddlProductCatCode.SelectedValue, ddlProduct_Type_Code.SelectedValue, txtProduct_Sale_Unit.Text, txtProduct_Sample_Unit_one.Text, txtProduct_Sample_Unit_Two.Text, txtProduct_Sample_Unit_Three.Text, strstate, strSubstate);
                    strTextBox = "";
                }
            }
        }

        if (iReturn > 0)
        {
            //menu1.Status = "Product detailhave been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductList.aspx';</script>");
        }

    }
    //Changes done by Priya

    //protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    string name1 = "";
    //    string id1 = "";
    //    GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
    //    CheckBoxList check = (CheckBoxList)gv.FindControl("CheckBoxList1");
    //    TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
    //    HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnSubDivisionId");
    //    txtSubDivision.Text = "";
    //    hdnSubDivisionId.Value = "";
    //    for (int i = 0; i < check.Items.Count; i++)
    //    {
    //        if(check.Items[i].Selected)
    //        {  
    //            name1 += check.Items[i].Text + ",";
    //            id1 += check.Items[i].Value + ",";
    //        } 
    //    }
    //    //if (name1 == "")
    //    //{
    //    // //   name1 = "NIL";
    //    //} 


    //    txtSubDivision.Text = name1.TrimEnd(',');
    //    hdnSubDivisionId.Value = id1.TrimEnd(',');
    //}
    protected void chkstate_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name = "";
        string id = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList checkst = (CheckBoxList)gv.FindControl("chkstate");
        TextBox txtState = (TextBox)gv.FindControl("txtState");
        HiddenField hdnStateId = (HiddenField)gv.FindControl("hdnStateId");
        txtState.Text = "";
        hdnStateId.Value = "";

        if (checkst.Items[0].Text == "ALL" && checkst.Items[0].Selected == true)
        {
            for (int i = 0; i < checkst.Items.Count; i++)
            {

                checkst.Items[i].Selected = true;
                //checkst.Items[i].Selected = true;            

            }
        }
        int countSelected = checkst.Items.Cast<ListItem>().Where(i => i.Selected).Count();
        if (countSelected == checkst.Items.Count - 1)
        {
            for (int i = 0; i < checkst.Items.Count; i++)
            {

                checkst.Items[i].Selected = false;
                //checkst.Items[i].Selected = true; 
            }

        }

        for (int i = 0; i < checkst.Items.Count; i++)
        {
            if (checkst.Items[i].Selected)
            {
                if (checkst.Items[i].Text != "ALL")
                {
                    name += checkst.Items[i].Text + ",";
                    id += checkst.Items[i].Value + ",";
                }

            }
        }
        if (name == "")
        {
            name = "---- Select ----";
        }



        txtState.Text = name.TrimEnd(',');
        hdnStateId.Value = id.TrimEnd(',');
    }
    protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void grdStk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string name1 = "";
        string id1 = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("CheckBoxList1");
        TextBox txtDivision = (TextBox)gv.FindControl("txtDivision");
        HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnDivision");
        txtDivision.Text = "";
        hdnSubDivisionId.Value = "";
        for (int i = 0; i < check.Items.Count; i++)
        {
            if (check.Items[i].Selected)
            {
                name1 += check.Items[i].Text + ",";
                id1 += check.Items[i].Value + ",";
            }
        }
        if (name1 == "")
        {
            name1 = "---- Select ----";
        }

        txtDivision.Text = name1.TrimEnd(',');
        hdnSubDivisionId.Value = id1.TrimEnd(',');
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductList.aspx");
    }


    protected void rdolstrpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdolstrpt.SelectedValue.ToString() == "Stk_Erp_Code")
        {
            ddlstk.Visible = true;
            ddlSrch.Visible = false;
        }
        else if (rdolstrpt.SelectedValue.ToString() == "Sample_Erp_Code")
        {
            ddlstk.Visible = false;
            ddlSrch.Visible = true;
        }
    }
}