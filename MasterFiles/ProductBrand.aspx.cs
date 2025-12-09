using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;


public partial class MasterFiles_ProductBrand : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsPBrd = null;
    string ProdBrdCode = string.Empty;
    string divcode = string.Empty;
    string Product_Brd_SName = string.Empty;
    string ProBrdName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductBrandList.aspx";
        divcode =Convert.ToString(Session["div_code"]);
        ProdBrdCode = Request.QueryString["Product_Brd_Code"];
        txtProduct_Brd_SName.Focus();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (ProdBrdCode != "" && ProdBrdCode != null)
            {
                Product dv = new Product();
                dsPBrd = dv.getProdBrd(divcode, ProdBrdCode);

                if (dsPBrd.Tables[0].Rows.Count > 0)
                {
                    txtProduct_Brd_SName.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtProBrdName.Text = dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                    if (dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "S")
                    {
                        rdotype.SelectedValue = "S";
                    }
                    else if (dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "R")
                    {
                        rdotype.SelectedValue = "R";
                    }
                    else if (dsPBrd.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "N")
                    {
                        rdotype.SelectedValue = "N";
                    }
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
        Product_Brd_SName = txtProduct_Brd_SName.Text;
        ProBrdName = txtProBrdName.Text;

        if (ProdBrdCode == null)
        {
            //add new brand
            Product dv = new Product();
            int iReturn = dv.Brd_RecordAdd(divcode, Product_Brd_SName, ProBrdName, rdotype.SelectedValue);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
                txtProBrdName.Focus();
            }

            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Short Name Already Exist');</script>");
                txtProduct_Brd_SName.Focus();
            }
        }
        else
        {
            //Update product Brand
            Product dv = new Product();
            int iReturn = dv.Brd_RecordUpdate_new(Convert.ToInt16(ProdBrdCode), Product_Brd_SName, ProBrdName, divcode, rdotype.SelectedValue);
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductBrandList.aspx';</script>");
            }

            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
                txtProBrdName.Focus();
            }

            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Short NameAlready Exist');</script>");
                txtProduct_Brd_SName.Focus();
            }
        }
    }
    private void Resetall()
    {
        txtProduct_Brd_SName.Text = "";
        txtProBrdName.Text = "";
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductBrandList.aspx");
    }
}