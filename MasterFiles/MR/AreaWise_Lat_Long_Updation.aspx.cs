using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_MR_AreaWise_Lat_Long_Updation : System.Web.UI.Page
{
    DataSet dsTerritory = null;
    string div_code;
    string terr_code = string.Empty;
    string lat = string.Empty;
    string Long = string.Empty;
    string sf_code = string.Empty;
    string terr_name = string.Empty;
    //string lat1 = string.Empty;
    //string long1 = string.Empty;
    int iReturn = -1;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            getddlSF_Code();
            ViewTerritory();
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Session["sf_code"] = ddlSFCode.SelectedValue.ToString();
            ViewTerritory();
        }
        catch (Exception ex)
        {

        }
    }
    private void ViewTerritory()
    {
        Territory terr = new Territory();
        //for (int i = 1; i < ddlSFCode.Items.Count; i++)
        //{
        //    string str = Session["sf_code"].ToString();
        //    if (ddlSFCode.Items[i].Value == str)
        //    {
        //        ddlSFCode.SelectedIndex = i;

        //    }
        //}

        dsTerritory = terr.getTerritory_lat_long(ddlSFCode.SelectedValue);

        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            //foreach (GridViewRow row in grdTerritory.Rows)
            //{
            //    // Label lblimg = (Label)row.FindControl("lblimg");
            //    Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
            //    TextBox txtdcrlat = (TextBox)row.FindControl("txtdcrlat");
            //    TextBox txtdcrlong = (TextBox)row.FindControl("txtdcrlong");
            //    //  if (Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][4].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][5].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][7].ToString()) > 0)
            //}
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
    }
    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();
            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                sf_code = ddlSFCode.SelectedValue.ToString();
                Session["sf_code_Temp"] = sf_code;
            }           
            

        }

    }
    protected void grdTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritory.PageIndex = e.NewPageIndex;

        ViewTerritory();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Territory terr = new Territory();
        //try
        //{
            int iReturn = -1;

            foreach (GridViewRow row in grdTerritory.Rows)
            {
                Label lblTerritory_Code = (Label)row.FindControl("lblTerritory_Code");
                TextBox txtdcrlat = (TextBox)row.FindControl("txtdcrlat");
                TextBox txtdcrlong = (TextBox)row.FindControl("txtdcrlong");
                Label lblTerritory_Name = (Label)row.FindControl("lblTerritory_Name");
                //TextBox txtdcrlat2 = (TextBox)row.FindControl("txtdcrlat2");
                //TextBox txtdcrlong2 = (TextBox)row.FindControl("txtdcrlong2");

                terr_code = lblTerritory_Code.Text;
                lat = txtdcrlat.Text;
                Long = txtdcrlong.Text;
                terr_name = lblTerritory_Name.Text;
                //lat1 = txtdcrlat.Text;
                //lat1 = txtdcrlat2.Text;
                //long1 = txtdcrlong2.Text;
                Territory terr = new Territory();
                iReturn = terr.delete_lat_long(lat, Long, ddlSFCode.SelectedValue, terr_code);
                iReturn = terr.Add_lat_long(lat, Long, terr_code, ddlSFCode.SelectedValue,terr_name);
            }
           
            if (iReturn > 0)
            {
               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                ViewTerritory();
            }
            
        }

        //catch(Exception ex)
        //{
        //    throw ex;
       // }
        
    //}
  
}
