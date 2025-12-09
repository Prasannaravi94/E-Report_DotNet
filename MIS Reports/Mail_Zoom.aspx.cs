using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Bus_EReport;

public partial class MasterFiles_Mails_Mail_Zoom : System.Web.UI.Page
{
    DataSet dsMail = null;
    DataSet dsFrom = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_Type = string.Empty;
    string HO_ID = string.Empty;
    DataSet dsUserList = new DataSet();
    string sLevel = string.Empty;
    string temp_code = string.Empty;
    string mail_to_sf_code = string.Empty;
    string temp_Name = string.Empty;
    string mail_to_sf_Name = string.Empty;
    string mail_cc_sf_code = string.Empty;
    string strSF_Name = string.Empty;
    string mail_bcc_sf_code = string.Empty;
    string sf_HO_ID = string.Empty;
    SalesForce sf = new SalesForce();
    DataSet dsSalesForce = null;
    string strMail_CC = string.Empty;
    string sf_Name = string.Empty;
    string strMail_To = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strFileDateTime = string.Empty;
    string div_Name = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_Type = Session["sf_type"].ToString();

        if (Session["Sub_HO_ID"] != null)
        {
            sf_HO_ID = Session["Sub_HO_ID"].ToString();
        }

        // Modified bySridevi  - 08Oct
        if (Session["div_code"] != null)
        {
            if (Session["div_code"].ToString() != "")
            {
                div_code = Session["div_code"].ToString();
                div_Name = Session["sf_name"].ToString();
            }
        }
        else if (sf_Type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_Name = Session["div_Name"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }



        HO_ID = Session["HO_ID"].ToString();

        string productName = Request.QueryString["ProductName"];
        // Changes done by Priya --Start


        if (!Page.IsPostBack)
        {
            //  Session["WinPostBack"] = "1";
            Session["Inbox"] = "Inbox";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "ShowInbox();", true);

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            OpenPopup();


        }
    }
       
    private void OpenPopup()
    {
       
        AdminSetup ast = new AdminSetup();
        if (Request.QueryString["slno"] != null)
        {
            dsFrom = ast.ViewMail(Convert.ToInt32(Request.QueryString["slno"].ToString()));
        }
        else
        {
            dsFrom = ast.ViewMail(Convert.ToInt32(Request.QueryString["slno"].ToString()));
        }
        if (dsFrom.Tables[0].Rows.Count > 0)
        {
            //DivView.Visible = true;
            lblViewFrom.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            lblViewTo.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            lblViewCC.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            lblViewSub.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            //New chanages added by saravanan start 

            lblViewSub.Text = lblViewSub.Text.Replace("asdf", "'");
            lblViewSent.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            lblReadDate.Text = dsFrom.Tables[0].Rows[0]["Mail_Read_date"].ToString();
            lblMailBody.Text = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            lblMailBody.Text = lblMailBody.Text.Replace("asdf", "'");

            //New chanages added by saravanan End 
            ViewState["mail_sf_from"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            ViewState["strMail_To"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            ViewState["strMail_CC"] = dsFrom.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
            //if (lblViewCC.Text != "")
            //{
            //    ViewState["mail_to_sf_NameCC"] = lblViewCC.Text;
            //}
            //ViewState["mail_to_sf_Name"] = lblViewFrom.Text;
            //ViewState["mail_to_sf_Name"] = lblViewTo.Text;
            strSF_Name = Session["sf_name"].ToString();

            AdminSetup adm = new AdminSetup();
            ViewState["inbox_id"] = Convert.ToInt32(Request.QueryString["slno"].ToString());
            DataSet dsMailAttach = adm.getMailAttach(Request.QueryString["slno"].ToString());
            if (dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString() != "")
            {
                aTagAttach.HRef = "~/" + dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString();
                string[] str = dsMailAttach.Tables[0].Rows[0]["Mail_Attachement"].ToString().Split('/');
                lblViewAttach.Text = str[3];
                imgViewAttach.Visible = true;
            }
            else
            {
                lblViewAttach.Text = "";
                imgViewAttach.Visible = false;
            }
        }

        //txtboxSearch.Visible = false;

    }

      
}