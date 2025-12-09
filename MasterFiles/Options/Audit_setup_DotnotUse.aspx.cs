using System;
using System.Web.UI;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;
using System.Globalization;

public partial class MasterFiles_Options_Audit_setup : System.Web.UI.Page
{
    public string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            string NowDay= DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
            string NowMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string NowYear = DateTime.Now.Year.ToString().Length == 1 ? "0" + DateTime.Now.Year.ToString() : DateTime.Now.Year.ToString();
            string NowHour = DateTime.Now.Hour.ToString().Length == 1 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string NowMinute = DateTime.Now.Minute.ToString().Length == 1 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            string NowSecond = DateTime.Now.Second.ToString().Length == 1 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();

            txtDateTime.Text = NowDay + "/" + NowMonth + "/" + NowYear + " " + NowHour + ":" + NowMinute + ":" + NowSecond;
           
            Session["Audit_setup"] = null;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        int iReturn= -1;
        DateTime DateAccess= DateTime.ParseExact(txtDateTime.Text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        string DtAc = DateAccess.Month + "-" + DateAccess.Day + "-" + DateAccess.Year + " " + DateAccess.Hour + ":" + DateAccess.Minute + ":" + DateAccess.Second;
        iReturn = adm.InsertAuditSetup(txtSupport.Text, DtAc, txtremark.Text, div_code);
        if (iReturn > 0)
        {
            Session["Audit_setup"] = "1";
            Response.Redirect("~/MasterFiles/Options/Mob_App_Setting.aspx");
        }
    }
}