using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using DBase_EReport;

//Added by Preethi
public partial class MasterFiles_Dashboard_Product_prclst : System.Web.UI.Page 
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
        //Session["div_code"] = Request.QueryString["div"].ToString();
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
            ddlState.Visible = false;
            if (sf_type == "1" || sf_type == "MR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;

                ddlState.Visible = true;

                FillState(div_code);
            }

            

            //TourPlan tp = new TourPlan();
            //dsTP = tp.Get_TP_Edit_Year(div_code);
            //if (dsTP.Tables[0].Rows.Count > 0)
            //{
            //    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            //    {
            //        ddlYear.Items.Add(k.ToString());
            //        ddlYear.SelectedValue = cYear;
            //    }
            //}
            //ddlMonth.SelectedValue = cMnth;

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


    private void FillState(string div_code)
    {
        Division dv = new Division();
      DataSet dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
           string state_cd = string.Empty;
           string sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            string[] statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
           DataSet dsState = st.getState_new(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
    }

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
            //dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sf_code);
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
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
        string host = Request.Url.Host;
        int port = Request.Url.Port;

        ifmReviewRep.Attributes.Add("src", "http://" + host + ":" + port + "/MasterFiles/MR/ProductRate.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&monthId=" + "" + "&yearId=" + "" + "&divCode=" + div_code + "&sf_type=" + sf_type + "&sfname=" + ddlFieldForce.SelectedItem.Text);
        //ifmReviewRep.Attributes.Add("src", "http://" + host + "/MasterFiles/Reports/RptAutoExpense_view1.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&monthId=" + cmonth + "&yearId=" + cyear + "&divCode=" + div_code + "&sfname=" + ddlFieldForce.SelectedItem.Text);
    }
    #endregion

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
            && ddlFieldForce.SelectedValue != "")
        {
            FillReport();
        }
    }
    #endregion
}