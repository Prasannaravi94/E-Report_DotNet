using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_TourPlan_Datewise : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
             
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //// menu1.FindControl("btnBack").Visible = false;
            //FillManagers();
            FillYear();
            Filldays();
        }
        FillColor();
        setValueToChkBoxList();
        if (Session["sf_type"].ToString() == "2")
        {
            //FillMRManagers1();
        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            FillManagers();
        }
        ddlFieldForce.Visible = true;
        // linkcheck.Visible = false;
        // txtNew.Visible = true;
        btnGo.Enabled = true;

    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
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

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        //if (ddlFFType.SelectedValue.ToString() == "1")
        //{
        //    ddlAlpha.Visible = false;
        dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        //}
        //else if (ddlFFType.SelectedValue.ToString() == "0")
        //{
        //    FillSF_Alpha();
        //    ddlAlpha.Visible = true;
        //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
        //}

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
        FillColor();
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
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
    protected void btnGo_Click(object sender, EventArgs e)
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);
        // FillSalesForce();
        string sURL = "rptDelayed_DCR_Status.aspx?cmon=" + MonthVal.ToString() + "&cyear=" + YearVal.ToString() + "&SF_code=" + ddlFieldForce.SelectedValue;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }



    protected void linkcheck_Click(object sender, EventArgs e)
    {

        

    }

    private void Filldays()
    {
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        //int to_days = DateTime.DaysInMonth(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue));
        int to_days = DateTime.DaysInMonth(Convert.ToInt16(YearVal), Convert.ToInt16(MonthVal));
       
   
            for (int i = 1; i <= to_days; i++)
            {

                chkdate.Items.Add("   " +i.ToString());

            }
        

    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdate.Items.Clear();
        Filldays();
        setValueToChkBoxList();
    }

    private void setValueToChkBoxList()
    {
        try
        {

            foreach (ListItem item in chkdate.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }

        }
        catch (Exception)
        {
        }
    }
}