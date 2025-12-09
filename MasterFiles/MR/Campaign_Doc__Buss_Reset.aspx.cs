using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;

public partial class MasterFiles_MR_Campaign_Doc__Buss_Reset : System.Web.UI.Page 
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;  
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            //FillColor();
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            FillManagers();
           // BindCampaign_Linked();

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();
        }
        FillColor();

        hHeading.InnerText = Page.Title;
    }
 

    private void FillDoc_Camp()
    {
            TourPlan tp = new TourPlan();
            SalesForce sf = new SalesForce();

            if (ddlFieldForce.SelectedValue.ToString().Contains("MR"))
            {
                dsTP = tp.Campaign_Doc_Buss_Reset(div_code, ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),"2");
            }
            else if (ddlFieldForce.SelectedValue.ToString().Contains("MGR"))
            {
                dsTP = tp.Campaign_Doc_Buss_Reset(div_code, ddlFieldForce.SelectedValue.ToString(), ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(),"1");
            }
            if (dsTP.Tables[0].Rows.Count > 0) 
            {
                grdCRM.Visible = true;
                grdCRM.DataSource = dsTP;
                grdCRM.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                grdCRM.DataSource = null;
                grdCRM.DataBind();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target and Achievement Not entered for the selected month');</script>");

                grdCRM.Visible = true;
                btnSubmit.Visible = false;
            }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    protected void grdCRM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            Label lblFutureTP = (Label)e.Row.FindControl("lblFutureTP");            
            CheckBox chkTP = (CheckBox)e.Row.FindControl("chkTP");
            HtmlInputCheckBox chkSNo = (HtmlInputCheckBox)e.Row.FindControl("chkSNo");

            TourPlan tp = new TourPlan();
            DataSet dsTp = new DataSet();
        }
    }

    private string getmonthname(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "Jan";
        }
        else if (iMonth == 2)
        {
            sReturn = "Feb";
        }
        else if (iMonth == 3)
        {
            sReturn = "Mar";
        }
        else if (iMonth == 4)
        {
            sReturn = "Apr";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "Jun";
        }
        else if (iMonth == 7)
        {
            sReturn = "Jul";
        }
        else if (iMonth == 8)
        {
            sReturn = "Aug";
        }
        else if (iMonth == 9)
        {
            sReturn = "Sep";
        }
        else if (iMonth == 10)
        {
            sReturn = "Oct";
        }
        else if (iMonth == 11)
        {
            sReturn = "Nov";
        }
        else if (iMonth == 12)
        {
            sReturn = "Dec";
        }
        return sReturn;
    }

    private void FillColor()
    {
        int j = 0;
        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;
        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.AllFieldforce_Novacant_MGROnly(div_code, "admin");
      
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
        FillColor();
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillDoc_Camp();
       // btnSubmit.Visible = true;
        ddlMonth.Enabled = false;
        ddlYear.Enabled = false;
        ddlFieldForce.Enabled = false; 
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdCRM.Rows)
        {
            HtmlInputCheckBox chkSNo = (HtmlInputCheckBox)gridRow.Cells[0].FindControl("chkSNo");
            if (chkSNo.Checked)
            {
                Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                TourPlan tp = new TourPlan();
                //if (lblSFCode.Text.Contains("MR"))
                //{
                //    iReturn = tp.Reset_Campaign_Doc(lblSFCode.Text, ddlLinked.SelectedValue.ToString(), div_code, "0");
                //}
                //else if (lblSFCode.Text.Contains("MGR"))
                //{
                //    iReturn = tp.Reset_Campaign_Doc(lblSFCode.Text, ddlLinked.SelectedValue.ToString(), div_code, "1"); 
                //}
                iReturn = tp.Reset_Campaign_Buss_Doc(lblSFCode.Text, ddlMonth.SelectedValue.ToString(), ddlYear.SelectedValue.ToString(), div_code, "0");
            }
        }

        if (iReturn > 0)
        {
            //Response.Write("TP has been deleted successfully");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reset successfully');</script>");
            FillDoc_Camp();
        }
    }

    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSubmit.Visible = false;
        grdCRM.Visible = false;
      
       // BindCampaign_Linked();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMonth.Enabled = true;
        ddlYear.Enabled = true;
        ddlFieldForce.Enabled = true;

        btnSubmit.Visible = false;
        grdCRM.Visible = false;
    }
    
}