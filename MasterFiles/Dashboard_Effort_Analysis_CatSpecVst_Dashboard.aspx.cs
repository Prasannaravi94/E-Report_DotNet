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
public partial class MasterFiles_Dashboard_Effort_Analysis_CatSpecVst_Dashboard : System.Web.UI.Page
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


    #region FillReport
    private void FillReport()
    {
        DB_EReporting db = new DB_EReporting();
        string strQry = "";
        strQry = " select HO_ID from mas_ho_id_creation b where charindex(','+cast('" + div_code + "' as varchar)+',',','+ b.Division_Code) >0 and HO_Active_Flag=0 ";
        DataSet dsHoid = db.Exec_DataSet(strQry);
        string HO_ID = dsHoid.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        string host = Request.Url.Host;
        int port = Request.Url.Port;
        if (sf_type == "1" || sf_type == "MR" || sf_type == "2" || sf_type == "MGR")
        {
            Session["div_code"] = div_code.ToString();
        }
        else
        {
            Session["division_code"] = div_code.ToString();
        }
        Session["sf_code"] = sf_code.ToString();
        Session["HO_ID"] = HO_ID.ToString();
        Session["sf_type"] = sf_type;
        
        Session["Dashboard"] = HO_ID.ToString();

        ifmReviewRep.Attributes.Add("src", "http://" + host + ":" + port + "/Effort_Analysis_CatSpecVst_Dashboard.aspx?AppDashboard=" + "Yes");
        //ifmReviewRep.Attributes.Add("src", "http://" + host + "/MasterFiles/Reports/RptAutoExpense_view1.aspx?sf_code=" + ddlFieldForce.SelectedValue + "&monthId=" + cmonth + "&yearId=" + cyear + "&divCode=" + div_code + "&sfname=" + ddlFieldForce.SelectedItem.Text);
    }
    #endregion

    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion
}


