using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Options_Holiday_Upload : System.Web.UI.Page
{
    #region Declaration
    string sf_type = string.Empty;
    string div_code = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        hHeading.InnerText = Page.Title;
        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
        }
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\UPL_Holiday_Fixation.xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=UPL_Holiday_Fixation.xlsx");
            Response.TransmitFile(fileName);
            Response.End();
        }

        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
        }
    }
}