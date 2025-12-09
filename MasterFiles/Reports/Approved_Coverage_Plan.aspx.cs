using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class Reports_Approved_Coverage_Plan : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = new DataSet();
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();        
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Convert.ToString(Session["div_code"]);
        }
        else
        {
            div_code = Convert.ToString(Session["div_code"]);
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
        if (!Page.IsPostBack)
        {
            DataSet dsTP = new DataSet();
            DataSet dsmgrsf = new DataSet();
            SalesForce sf = new SalesForce();
            
           
                FillMRManagers();
            
            



        }

    }

   

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }

        FillColor();
    }

   
    

   

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
      
        string sURL = "";

        sURL = "Approved_Coverage_Plan_View.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() +
               "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() +"";

        string newWin1 = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin1, true);  

    }


    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

}