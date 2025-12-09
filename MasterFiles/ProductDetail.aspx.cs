using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_ProductDetail : System.Web.UI.Page
{
    DataSet dsPro = null;
    string ProdCode = string.Empty;
    string div_code = string.Empty;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsSubDivision = null;
    DataSet dsState = null;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string sChkLocation1 = string.Empty;
    int iIndex;
    string subdivision_code = string.Empty;
    string sub_division = string.Empty;
    string Prod_mode = string.Empty;
    string sam_Erp = string.Empty;
    string sale_Erp = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {        
        Session["backurl"] = "ProductList.aspx";
        div_code = Session["div_code"].ToString();
        ProdCode = Request.QueryString["Product_Detail_Code"];
        txtProdDetailCode.Focus();
        if (!Page.IsPostBack)
        {
            FillCategory();
            FillGroup();
            FillBrand();
            menu1.Title = this.Page.Title;            
            Product dv = new Product();
            dsPro = dv.getProdforCode(div_code, ProdCode);
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (dsPro.Tables[0].Rows.Count > 0)
            {
                txtProdDetailName.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                txtProdSUnit.Text =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); 
                txtSamp1.Text =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                txtSamp2.Text =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                txtSamp3.Text =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                ddlCat.SelectedValue  =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                RblType.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                txtProdDesc.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                ddlGroup.SelectedValue  =  dsPro.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                txtProdDetailCode.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                state_code = dsPro.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                subdivision_code = dsPro.Tables[0].Rows[0].ItemArray.GetValue(11).ToString(); // Sub Division
                ddlmode.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                txtsample.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                txtsale.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                ddlBrand.SelectedValue = dsPro.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();//Brand
                txtProdDetailCode.Enabled = false;
            }
            FillCheckBoxList();         
            FillCheckBoxList_New();
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
    //change chkallbox done by saravanan 05-08-14 
    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
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
            dsState = st.getStateChkBox(state_cd);           
            chkboxLocation.DataTextField = "statename";
            chkboxLocation.DataValueField = "state_code";
            chkboxLocation.DataSource = dsState;
            chkboxLocation.DataBind();
        }
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {                        
                    chkboxLocation.Items[iIndex].Selected = true;
                    chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold ");
                    }
                }
            }
            int countSelected = chkboxLocation.Items.Cast<ListItem>().Where(i => i.Selected).Count();
            if (countSelected == chkboxLocation.Items.Count)
            {
                ChkAll.Checked = true;
            }
        }
        else
        {
          ChkAll.Checked = true;
          for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
            {                                           
              chkboxLocation.Items[iIndex].Selected = true;
              //chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: Blue; font-weight:Bold ");                    
            }
            
        }
        
    }
    private void FillCheckBoxList_New()
    {
        //List of Sub division are loaded into the checkbox list from Division Class
        
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        chkSubdiv.DataTextField = "subdivision_name";
        chkSubdiv.DataSource = dsSubDivision;
        chkSubdiv.DataBind();
        string[] subdiv;
        
        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');            
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkSubdiv.Items.Count; iIndex++)
                {
                    if (st == chkSubdiv.Items[iIndex].Value)
                    {
                        chkSubdiv.Items[iIndex].Selected = true;
                        chkSubdiv.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold");
                       // chkNil.Checked = false;

                    }                   
                }               
            }         

        }        
        
    }
    private void FillCategory()
    {
        Product prd = new Product();
        dsProduct = prd.getProductCategory(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlCat.DataTextField = "Product_Cat_Name";
            ddlCat.DataValueField = "Product_Cat_Code";
            ddlCat.DataSource = dsProduct;
            ddlCat.DataBind();
        }
    }

    private void FillGroup()
    {
        Product prd = new Product();
        dsProduct = prd.getProductGroup(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlGroup.DataTextField = "Product_Grp_Name";
            ddlGroup.DataValueField = "Product_Grp_Code";
            ddlGroup.DataSource = dsProduct;
            ddlGroup.DataBind();
        }
    }

    private void FillBrand()
    {
        Product prd = new Product();
        dsProduct = prd.GetProductBrand(div_code);
        if (dsProduct.Tables[0].Rows.Count > 0)
        {
            ddlBrand.DataTextField = "Product_Brd_Name";
            ddlBrand.DataValueField = "Product_Brd_Code";
            ddlBrand.DataSource = dsProduct;
            ddlBrand.DataBind();
        }
    }

    private void ResetAll()
    {
        txtProdDetailCode.Text = "";
        txtProdDetailName.Text = ""; 
        txtProdSUnit.Text = "";  
        txtSamp1.Text = "";
        txtSamp2.Text = "";
        txtSamp3.Text = "";
        ddlCat.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        ddlBrand.SelectedIndex = 0;
        RblType.SelectedIndex=-1;
        txtProdDesc.Text = "";
        txtsample.Text = "";
        txtsale.Text = "";
        ChkAll.Checked = true;
        for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
        {
            chkboxLocation.Items[iIndex].Selected = false;
        }
        //chkNil.Checked = true;
        for (iIndex = 0; iIndex <chkSubdiv.Items.Count; iIndex++)
        {
            chkSubdiv.Items[iIndex].Selected = false;
        }
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        for (int i = 0; i < chkboxLocation.Items.Count; i++)
        {
            if (chkboxLocation.Items[i].Selected)
            {
                sChkLocation = sChkLocation + chkboxLocation.Items[i].Value + ",";
            }
        }
        for (int i = 0; i < chkSubdiv.Items.Count; i++)
        {
            if (chkSubdiv.Items[i].Selected)
            {
                sChkLocation1 = sChkLocation1 + chkSubdiv.Items[i].Value + ",";
            }
        }

        Product dv1 = new Product();
        dsPro = dv1.getProdforCode(div_code, ProdCode);
        if (dsPro.Tables[0].Rows.Count > 0)
        {
            txtsample.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
            txtsale.Text = dsPro.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();

            //Update Product
            txtProdDetailCode.Enabled = true;
            Product dv = new Product();
            int iReturn = dv.RecordUpdateProd(ProdCode, txtProdDetailName.Text.Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", ""), txtProdSUnit.Text, txtSamp1.Text, txtSamp2.Text, txtSamp3.Text, Convert.ToInt32(ddlCat.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToString(RblType.SelectedItem.Value), txtProdDesc.Text, Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, ddlmode.SelectedItem.Text, txtsample.Text, txtsale.Text, div_code, Convert.ToInt32(ddlBrand.SelectedValue));
            if (iReturn > 0)
            {
                // menu1.Status = "Product Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductList.aspx';</script>");
            }
            else if (iReturn == -2)
            {
                // menu1.Status = "Product already exist!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name already exist');</script>");
                txtProdDetailName.Focus();
            }

        }
        else
        {
            Product prod = new Product();
            string samplecode = txtsample.Text;
            string salecode = txtsale.Text;
            dsProduct = prod.getsampleErpcode(samplecode);



            //if (dsProduct.Tables[0].Rows[0]["cnt"].ToString() != "0")
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sample Erp Code Already Exist ');</script>");
            //    txtsample.Focus();
            //}
            //dsProduct = prod.getsaleErpcode(salecode);
            //if (dsProduct.Tables[0].Rows[0]["cnt"].ToString() != "0")
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sale Erp Code Already Exist ');</script>");
            //    txtsale.Focus();
            //}

            //above commented by 23-05-2025



            //if (dsProduct.Tables[0].Rows[0]["cnt"].ToString() == "0" && dsProduct.Tables[0].Rows[0]["cnt"].ToString() == "0")
            //{

                if (ProdCode == null)
                {
                    // Add New Product            
                    Product prd = new Product();
                    //  int iReturn = prd.RecordAdd(txtProdDetailCode.Text, txtProdDetailName.Text, txtProdSUnit.Text, txtSamp1.Text, txtSamp2.Text, txtSamp3.Text, Convert.ToInt32(ddlCat.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToString(RblType.SelectedItem.Value), txtProdDesc.Text, Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, ddlmode.SelectedItem.Text, txtsample.Text, txtsale.Text, Convert.ToInt32(ddlBrand.SelectedValue));
                    int iReturn = prd.RecordAdd_Prod_Auto_Code(txtProdDetailName.Text.Replace("(", "").Replace(")", "").Replace("{", "").Replace("}", ""), txtProdSUnit.Text, txtSamp1.Text, txtSamp2.Text, txtSamp3.Text, Convert.ToInt32(ddlCat.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToString(RblType.SelectedItem.Value), txtProdDesc.Text, Convert.ToInt32(Session["div_code"].ToString()), sChkLocation, sChkLocation1, ddlmode.SelectedItem.Text, txtsample.Text, txtsale.Text, Convert.ToInt32(ddlBrand.SelectedValue));
                    if (iReturn > 0)
                    {
                        // menu1.Status = "Product Details Created Successfully";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                        ResetAll();

                    }

                    if (iReturn == -2)
                    {
                        //  menu1.Status = "Product exist with the same short name!!";
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Name Already Exist ');</script>");
                        txtProdDetailName.Focus();

                    }
                    //if (iReturn == -3)
                    //{
                    //    //  menu1.Status = "Product exist with the same short name!!";
                    //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Product Code Already Exist');</script>");
                    //    txtProdDetailCode.Focus();

                    //}

                }
           // }
        }

    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {        
        chkboxLocation.Attributes.Add("onclick", "checkAll(this);");
    }
    //protected void chkNil_CheckedChanged(object sender, EventArgs e)
    //{
    //    chkboxLocation.Attributes.Add("onclick", "checkNIL(this);");
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductList.aspx");
    }
}