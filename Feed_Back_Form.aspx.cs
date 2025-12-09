using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Feed_Back_Form : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsadm = new DataSet();
    string div_name = string.Empty;
    string strRec = string.Empty;
    string feed = string.Empty;
    int status;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["division_code"].ToString();
        div_name = Session["div_Name"].ToString();
        DataSet ds = new DataSet();
        feed = Request.QueryString["Feed_ID"];
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Feed_ID"] != null)
            {
                div_code = Request.QueryString["div_code"];
                AdminSetup admin = new AdminSetup();
                ds = admin.GetFeedback(div_code,feed);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rdoproduct.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    rdoService.SelectedValue = ds.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    ddlarea.SelectedItem.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtCon.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtcom.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtRem.Text = ds.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    if (ds.Tables[0].Rows[0]["Recommend"].ToString() == "Yes")
                    {
                        rdoYes.Checked = true;
                        rdoNo.Checked = false;
                    }
                    else
                    {
                        rdoYes.Checked = false;
                        rdoNo.Checked = true;
                    }

                }
            }
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        if (rdoYes.Checked)
        {
            strRec = "Yes";
        }
        else
        {
            strRec = "No";
        }
        status=1;
        int iReturn = adm.FeedBack_RecordAdd(Session["div_Name"].ToString(), div_code, rdoproduct.SelectedItem.Text, rdoService.SelectedItem.Text, ddlarea.SelectedItem.Text, txtCon.Text, txtcom.Text, txtRem.Text, strRec,status);
        if (iReturn > 0)
        {
            //menu1.Status = "State/Location Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');window.location='Index.aspx'</script>");
        }
    }
}