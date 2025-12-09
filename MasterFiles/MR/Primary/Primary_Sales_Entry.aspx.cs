using AjaxControlToolkit;
using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_MR_Primary_Primary_Sales_Entry : System.Web.UI.Page
{
    #region Declaration
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Header.DataBind();
        hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/MR.css";
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/MGR.css";
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/Master.css";
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/MR.css";
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/MGR.css";
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                ModalPopupExtender mpe = (ModalPopupExtender)c1.FindControl("mpeTimeout");
                c1.Controls.Remove(mpe);
                ToolkitScriptManager tsm = (ToolkitScriptManager)c1.FindControl("ToolkitScriptManager1");
                c1.Controls.Remove(tsm);
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //cssMenu.Href = "../../../css/Master.css";
            }
        }

        AdminSetup adm = new AdminSetup();
        string Cal_Rate = "D";
        DataSet dsadmin = adm.getSetup_forTargetFix(div_code);
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            if (dsadmin.Tables[0].Rows[0]["Stockist_Primary_Sale_Based_On"] != DBNull.Value)
            {
                Cal_Rate = dsadmin.Tables[0].Rows[0]["Stockist_Primary_Sale_Based_On"].ToString();
            }
            else
            {
                Cal_Rate = "D";
            }
        }

        PS_CalRate.Value = Cal_Rate;
    }
}