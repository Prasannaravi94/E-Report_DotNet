using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BulkEditProdBrd : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsProd = null;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataTable dtrowClr = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "ProductBrandList.aspx";
            menu1.Title = this.Page.Title;
            FillProdBrd();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    private void FillProdBrd()
    {
        Product dv = new Product();
        dsProd = dv.getProBrd(div_code);
        dtrowClr = dsProd.Tables[0].Copy();
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            grdProdBrd.Visible = true;
            grdProdBrd.DataSource = dsProd;
            grdProdBrd.DataBind();
        }
        else
        {
            grdProdBrd.DataSource = dsProd;
            grdProdBrd.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string Product_Brd_Code = string.Empty;
        string Product_Brd_SName = string.Empty;
        string Product_Brd_Name = string.Empty;
        string rdoTypee = string.Empty;
        Product dv = new Product();
        int iReturn = -1;
        bool err = false;
        foreach (GridViewRow gridRow in grdProdBrd.Rows)
        {
            Label lblProductBrdCode = (Label)gridRow.Cells[1].FindControl("lblProductBrdCode");
            Product_Brd_Code = lblProductBrdCode.Text.ToString();
            TextBox txtProductBrdSName = (TextBox)gridRow.Cells[1].FindControl("txtProductBrdSName");
            Product_Brd_SName = txtProductBrdSName.Text.ToString();
            TextBox txtProductBrdName = (TextBox)gridRow.Cells[1].FindControl("txtProductBrdName");
            Product_Brd_Name = txtProductBrdName.Text.ToString();
            RadioButtonList rdotype = (RadioButtonList)gridRow.Cells[1].FindControl("rdotype");
            rdoTypee = rdotype.Text.ToString();
            iReturn = dv.Brd_RecordUpdate_new(Convert.ToInt16(Product_Brd_Code), Product_Brd_SName, Product_Brd_Name, div_code, rdoTypee);
            if (iReturn > 0)
                err = false;

            if ((iReturn == -2))
            {
                txtProductBrdName.Focus();
                err = true;
                break;
            }

            if((iReturn == -3))
            {
                txtProductBrdSName.Focus();
                err = true ;
                break;
            }          

        }
        if (err == false )
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='ProductBrandList.aspx';</script>");
        }
        else if (err == true)
        {
            if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Name Already Exist');</script>");
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Brand Short Name Already Exist');</script>");
            }
        }
    }

    protected void grdProdBrd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            int iInx = e.Row.RowIndex;
            RadioButtonList rdotype = (RadioButtonList)e.Row.FindControl("rdotype");
            DataRowView row = (DataRowView)e.Row.DataItem;
            rdotype.SelectedValue = dtrowClr.Rows[iInx][4].ToString();
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductBrandList.aspx");
    }
}