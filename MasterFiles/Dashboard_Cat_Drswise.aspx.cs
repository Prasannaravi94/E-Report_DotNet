using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MasterFiles_Dashboard_Cat_Drswise : System.Web.UI.Page
{

    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cMnth = string.Empty;
    string cYear = string.Empty;
    #endregion

    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();

        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            //sf_type = Request.QueryString["SFTyp"].ToString();
        }
        catch
        {
            sf_code = Request.QueryString["sfcode"].ToString();
            div_code = Request.QueryString["div_Code"].ToString();
            //sf_type = Request.QueryString["sf_type"].ToString();
            cMnth = Request.QueryString["cMnth"].ToString();
            cYear = Request.QueryString["cYr"].ToString();
        }

        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedIndex = 1;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 1;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = cYear;
                }
            }
            ddlMonth.SelectedValue = cMnth;
            FillCategory();
            FillReport();
        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (sf_type == "1" || sf_type == "MR")
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, "admin");
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    #endregion

    #region FillReport
    private void FillReport()
    {
        int cmonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int cyear = Convert.ToInt32(ddlYear.SelectedValue);
       
        string host = Request.Url.Host;
        int port = Request.Url.Port;
        string chkval = string.Empty;
        string chktxt = string.Empty;
        for (int i = 0; i < cbSpeciality.Items.Count; i++)
        {

            cbSpeciality.Items[i].Selected = true;
        }
        for (int i = 0; i < cbSpeciality.Items.Count; i++)
        {
            if (cbSpeciality.Items[i].Selected)
            {
                chkval = chkval + cbSpeciality.Items[i].Value + ",";
                chktxt = chktxt + cbSpeciality.Items[i].Text + ",";
            }
        }
        ifmReviewRep.Attributes.Add("src", "http://" + host + ":" + port + "/MasterFiles/AnalysisReports/Visit_Details_Cat_Cls_Spclty_Report.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&FMonth=" + cmonth + "&Fyear=" + cyear + "&TMonth=" + cmonth + "&Tyear=" + cyear + "&sf_name=" + ddlFieldForce.SelectedItem.Text +  "&div_code=" + div_code+ "&cbVal=" + chkval + "&cbTxt=" + chktxt + "&cMode=" + 1);
        //ifmReviewRep.Attributes.Add("src", "http://" + host + "/MIS%20Reports/rptsamplestatus_New_New.aspx?sfcode=" + ddlFieldForce.SelectedValue + "&FMonth=" + cmonth + "&Fyear=" + cyear + "&TMonth=" + cmonth + "&Tyear=" + cyear + "&sf_name=" + ddlFieldForce.SelectedItem.Text + "&div_code=" + div_code);
    }
    #endregion
    private void FillCategory()
    {
        string sQry = "SELECT Distinct Doc_Cat_Code as Code, Doc_Cat_SName as Short_Name FROM Mas_Doctor_Category " +
            "WHERE Division_Code='" + div_code + "' AND Doc_Cat_Active_Flag='0' ORDER BY 1";
        DB_EReporting db = new DB_EReporting();
        DataTable dt = db.Exec_DataTable(sQry);
        cbSpeciality.DataSource = dt;
        cbSpeciality.DataTextField = "Short_Name";
        cbSpeciality.DataValueField = "Code";
        cbSpeciality.DataBind();
        setValueToChkBoxList();
    }
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in cbSpeciality.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }
    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (ddlFieldForce.SelectedValue != "0"
            && ddlFieldForce.SelectedValue != ""
            && ddlMonth.SelectedValue != "0")
        {
            FillReport();
        }
    }
    #endregion
}