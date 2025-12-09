using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.UI.HtmlControls;


//Done By Preethi 
public partial class MasterFiles_MR_Campaign_Doc_Reset : System.Web.UI.Page 
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
            BindCampaign_Linked();
            FillManagers();
            
        }
        FillColor();
        hHeading.InnerText = Page.Title;
    }

    private void BindCampaign_Linked()
    {
        TourPlan Tp = new TourPlan();
        DataSet dsCamp_Linkd = new DataSet();
        dsCamp_Linkd = Tp.Fill_Campaign_Doc(div_code, ddlFieldForce.SelectedValue.ToString());
        if (dsCamp_Linkd.Tables[1].Rows.Count > 1)
        {
            ddlLinked.DataTextField = "Doc_SubCatName";
            ddlLinked.DataValueField = "Doc_SubCatCode";
            ddlLinked.DataSource = dsCamp_Linkd.Tables[1];
            ddlLinked.DataBind();
            ddlLinked.SelectedIndex = -1;

            btnGo.Visible = true;
        }
        else if (dsCamp_Linkd.Tables[0].Rows.Count == 1)
        {
            ddlLinked.DataTextField = "Doc_SubCatName";
            ddlLinked.DataValueField = "Doc_SubCatCode";
            ddlLinked.DataSource = dsCamp_Linkd.Tables[0];
            ddlLinked.DataBind();

            btnGo.Visible = false;
        }
    }


    private void FillDoc_Camp()
    {
            TourPlan tp = new TourPlan();
            SalesForce sf = new SalesForce();
            dsTP = tp.Campaign_Doc_Reset(div_code, ddlFieldForce.SelectedValue.ToString(), ddlLinked.SelectedValue.ToString());
             
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                grdCRM.Visible = true;
                grdCRM.DataSource = dsTP;
                grdCRM.DataBind();
                btnSubmit.Visible = true;
            }
            else
            {
                grdCRM.DataSource = dsTP;
                grdCRM.DataBind();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Target and Achievement Not entered for the selected month');</script>");
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
           // dsSalesForce.Tables[0].Rows[0].Delete();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
        FillColor();
        BindCampaign_Linked();
    }



    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillDoc_Camp();
       // btnSubmit.Visible = true;
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
                iReturn = tp.Reset_Campaign_Doc(lblSFCode.Text, ddlLinked.SelectedValue.ToString(), div_code, "0");
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
        BindCampaign_Linked();

        grdCRM.Visible = false;
        btnSubmit.Visible = false;
    }
    protected void ddlLinked_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdCRM.Visible = false;
        btnSubmit.Visible = false;
    }
}