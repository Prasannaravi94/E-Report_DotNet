using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Net.Mail;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using Bus_EReport;

public partial class MasterFiles_Options_Quiz_Process : System.Web.UI.Page
{
    static string div_code = string.Empty;
    static int surveyID;
    static string sf_code = string.Empty;
    static string[] SfName_Arr;
    static string Month;
    static string Year;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    DataSet dsDesig = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = this.Page.Title;

        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();

        Session["backurl"] = "Quiz_List.aspx";

        // string Month = Session["Month"].ToString();
        //string Year = Session["Year"].ToString();

        menu1.Title = this.Page.Title;
        if (Request.QueryString["Survey_Id"] != null)
        {
            BindDate();
            Get_State();
            FillDesig();
            FillSub_Division();
            // hidSurveyID.Value = Request.QueryString["Survey_Id"];
            surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);
            Month = Request.QueryString["Month"];
            Year = Request.QueryString["Year"];
            Session["SurveyID"] = surveyID.ToString();
            Session["Month"] = Month.ToString();
            Session["Year"] = Year.ToString();
        }
    }
    private void FillSub_Division()
    {
        SalesForce sf = new SalesForce();
        DataSet dsDiv = new DataSet();
        dsDiv = sf.getsubdiv_userlist(div_code);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {
            ddlsubdiv.DataTextField = "subdivision_name";
            ddlsubdiv.DataValueField = "subdivision_code";
            ddlsubdiv.DataSource = dsDiv;
            ddlsubdiv.DataBind();
        }
    }
    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
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
            DataSet dsState;
            // dsState = st.getStateChkBox(state_cd);

            dsState = st.getSt(state_cd);

            ddlst.DataTextField = "statename";
            ddlst.DataValueField = "state_code";
            ddlst.DataSource = dsState;
            ddlst.DataBind();
            ddlst.Items.Insert(0, new ListItem("ALL", "0"));

        }
    }
    private void FillDesig()
    {
        Designation des = new Designation();
        dsDesig = des.getDesignation_count(div_code);
        if (dsDesig.Tables[0].Rows.Count > 0)
        {
            ddlDesig.DataTextField = "Designation_Short_Name";
            ddlDesig.DataValueField = "Designation_Code";
            ddlDesig.DataSource = dsDesig;
            ddlDesig.DataBind();
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
                ddlTo.Items.Add(k.ToString());

            }

            ddlTo.Text = DateTime.Now.Year.ToString();

            //   ddlFrom.SelectedValue = DateTime.Now.Month.ToString();

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quiz_List.aspx");
    }
}

