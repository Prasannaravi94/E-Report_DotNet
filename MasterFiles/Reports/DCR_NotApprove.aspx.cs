using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class Reports_DCR_NotApprove : System.Web.UI.Page
{
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            btnSubmit.Focus();
            //menu1.FindControl("btnBack").Visible = false;
            menu1.Title = this.Page.Title;
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    //ddlYear.Items.Add(k.ToString());
                    //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                }
            }
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);
        //string sURL = "rptDCRNotApprove.aspx?cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim();
        string sURL = "rptDCRNotApprove.aspx?cmon=" + MonthVal.ToString() + "&cyear=" + YearVal.ToString();
        string newWin = "window.open('" + sURL + "',null,'');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }
}