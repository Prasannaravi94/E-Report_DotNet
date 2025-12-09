using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;

public partial class MasterFiles_AnalysisReports_Payslip : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  // menu1.FindControl("btnBack").Visible = false;
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                FillManagers();
                chckvacant.Visible = true;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                FillManagers1();
            }
            else  
            {
                FillManagers1();
            }

            FillColor();
            BindDate();


        }

        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
            //FillManagers();
            //FillColor();
            //BindDate();
            ddlFFType.Visible = false;
            ddlFieldForce.SelectedItem.Value = sf_code;
            //ddlFieldForce.Enabled = false;
            //lblFilter.Enabled = false;
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            //FillColor();
            //BindDate();

            ddlFieldForce.SelectedItem.Value = sf_code;
            ddlFFType.Visible = false;
            ddlFieldForce.Enabled = false;
            lblFilter.Enabled = false;

        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            chckvacant.Visible = true;
            //   FillManagers();
            //FillColor();
            //BindDate();
            // c1.FindControl("btnBack").Visible = false;
            // sf_code = ddlFieldForce.SelectedItem.Value;
        }

        FillColor();
    }
    private void FillManagers1()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSfCode_mr(sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.Hierarchy_Team(div_code, "admin");
            //dsSalesForce = sf.UserListTP_Hierarchy_Sale(div_code, sf_code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, sf_code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }
    private void FillMRManagers3()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSfCode_mrVacant(ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "Employee_Name";
            ddlFieldForce.DataValueField = "Employee_Id";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();


        }
    }
    private void FillColor()
    {
        //int j = 0;


        //foreach (ListItem ColorItems in ddlSF.Items)
        //{
        //    //ddlFieldForce.Items[j].Selected = true;
        //    string bcolor = "#" + ColorItems.Text;
        //    ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

        //    j = j + 1;

        //}
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }
    protected void OnCheckBox_Changed(object sender, EventArgs e)
    {
        if (chckvacant.Checked)
        {
            lblFilter.Visible = false;
            ddlFFType.Visible = false;
            ddlFieldForce.Visible = false;
            btnSubmit.Visible = false;
            btnSubmit.Visible = false;
            btngo.Visible = true;
            FillColor();
            ViewState["temp"] = ddlFieldForce.SelectedValue;

        }
        else
        {
            lblFilter.Visible = true;
            ddlFFType.Visible = true;
            ddlFieldForce.Visible = true;
            btnSubmit.Visible = true;
            btnSubmit1.Visible = false;
            //FillManagers();
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                FillManagers();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                FillManagers1();
            }
            else
            {
                FillManagers1();
            }
            //btnSubmit.Visible = true;
        }


    }
    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    string sURL = "";

    //    sURL = "rptCoverage_Analysis.aspx?sf_code=" + ddlFieldForce.SelectedValue.ToString() +
    //           "&FMonth=" + ddlMonth.SelectedValue.ToString() +
    //           "&Fyear=" + ddlYear.SelectedValue.ToString() + 
    //           "&sf_name=" + ddlFieldForce.SelectedItem.Text.ToString() +              
    //           "&div_Code=" + div_code + "";

    //    string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
    //    ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    //}
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {

            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();

    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{

    //    string Sf_Code = HttpUtility.UrlEncode(Encrypt(ddlFieldForce.SelectedValue));
    //    string Month = (ddlMonth.SelectedValue);
    //    string Year = (ddlYear.SelectedItem.Text);

    //    //PaySlip_View

    //    //Response.Redirect("Frm_PaySlip_View.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "");


    //    if (div_code == "43")
    //    {
    //        string pageurl = "Frm_PaySlip_View.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "');", true);
    //    }
    //    else if (div_code == "3")
    //    {
    //        string pageurl = "Frm_AstraeaLife_PaySlip_View.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "');", true);
    //    }
    //    else if (div_code == "64" || div_code == "79")
    //    {
    //        string pageurl = "Frm_Bal_Pharma_PayslipView.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "');", true);
    //    }
    //    else if (div_code == "24")
    //    {
    //        string pageurl = "Frm_InjectCare_PayslipView.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "');", true);
    //    }
    //    else if (div_code == "12")
    //    {
    //        string pageurl = "Frm_Goddress_PaySlipView.aspx?SF_Code=" + Sf_Code + "&Month=" + Month + "&Year=" + Year + "";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "');", true);
    //    }   

    //    //Response.Write( "<script>window.open( '"+pageurl+"','_blank' ); </script>");
    //    //Response.End();

    //}

    public static string Encrypt(string inputText)
    {
        string encryptionkey = "SAUW193BX628TD57";
        byte[] keybytes = Encoding.ASCII.GetBytes(encryptionkey.Length.ToString());
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes pwdbytes = new PasswordDeriveBytes(encryptionkey, keybytes);
        using (ICryptoTransform encryptrans = rijndaelCipher.CreateEncryptor(pwdbytes.GetBytes(32), pwdbytes.GetBytes(16)))
        {
            using (MemoryStream mstrm = new MemoryStream())
            {
                using (CryptoStream cryptstm = new CryptoStream(mstrm, encryptrans, CryptoStreamMode.Write))
                {
                    cryptstm.Write(plainText, 0, plainText.Length);
                    cryptstm.Close();
                    return Convert.ToBase64String(mstrm.ToArray());
                }
            }
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static string GetDivision_Code()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        return div_code;
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        FillMRManagers3();
        lblFilter.Visible = true;
        ddlFieldForce.Visible = true;
        btnSubmit1.Visible = true;
        btngo.Visible = false;
        ViewState["temp"] = ddlFieldForce.SelectedValue;
    }

}