using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_ProductCategory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsPCat= null;
    string ProdCatCode = string.Empty;
    string divcode = string.Empty;
    string Product_Cat_SName = string.Empty;
    string ProCatName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductCategoryList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        ProdCatCode = Request.QueryString["Product_Cat_Code"];
        txtProduct_Cat_SName.Focus();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (ProdCatCode != "" && ProdCatCode != null)
            {
                Product dv = new Product();
                dsPCat = dv.getProCate(divcode, ProdCatCode);

                if (dsPCat.Tables[0].Rows.Count > 0)
                {
                    txtProduct_Cat_SName.Text = dsPCat.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtProCatName.Text = dsPCat.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

            }
          menu1.Title = this.Page.Title;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Product_Cat_SName = txtProduct_Cat_SName.Text;
        ProCatName = txtProCatName.Text;

        if (ProdCatCode == null)
        {
            // Add New Product Category
            Product dv = new Product();
            int iReturn = dv.RecordAdd(divcode, Product_Cat_SName, ProCatName);

             if (iReturn > 0 )
            {
                //menu1.Status = "Product Category Created Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {                
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
                txtProCatName.Focus();
            }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Short NameAlready Exist');</script>");
                 txtProduct_Cat_SName.Focus();
             }
        }
        else
        {

            // Update Product Category
            Product dv = new Product();
            int iReturn = dv.RecordUpdate(Convert.ToInt16(ProdCatCode), Product_Cat_SName, ProCatName, divcode);
             if (iReturn > 0 )
            {
               // menu1.Status = "Product Category Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductCategoryList.aspx';</script>");
            }
             else if (iReturn == -2)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Name Already Exist');</script>");
                 txtProCatName.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Category Short NameAlready Exist');</script>");
                 txtProduct_Cat_SName.Focus();
             }
        }
    }
    private void Resetall()
    {
        txtProduct_Cat_SName.Text = "";
        txtProCatName.Text = "";
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductCategoryList.aspx");
    }
}