using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_FieldForces : System.Web.UI.Page
{
    String SFcode = String.Empty;
    int DivisionCode = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["div_code"] == null || Session["sf_code"] == null)
        {
            Response.Redirect("~/");
            return;
        }
        DivisionCode = Convert.ToInt32(Session["div_code"]);
        SFcode = Session["sf_code"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["measureby"] != null && Request.QueryString["viewby"] != null && Request.QueryString["splitby"] != null && Request.QueryString["sfcode"] != null && Request.QueryString["sfname"] != null && Request.QueryString["viewquery"] != null && Request.QueryString["splitquery"] != null && Request.QueryString["viewbyname"] != null)
            {

                List<CaptionData> Captions = new List<CaptionData>();
                Captions.Add(new CaptionData { Value = "Field Forces" });
                Captions.Add(new CaptionData { Label = "Field Force", Value = Request.QueryString["sfname"].ToString() });

                string Sfcodes = "";
                SalesForce salesForce = new SalesForce();
                DataSet salesForceData = salesForce.AllFieldforce_MR_Novacant(DivisionCode.ToString(), Request.QueryString["sfcode"]);

                if (salesForceData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in salesForceData.Tables[0].Rows)
                    {
                        Sfcodes += "'" + row["Sf_Code"].ToString() + "',";
                    }
                }
                if (!string.IsNullOrEmpty(Sfcodes))
                {
                    Sfcodes = Sfcodes.TrimEnd(',');
                }

                DB_EReporting db_ER = new DB_EReporting();

                DataSet records = null;

                string qrySelects = "Select " +
                    "Row_number() over(order by Mas_Salesforce.sf_code) AS No," +
                    "Mas_Salesforce.Sf_Name As Name," +
                    "Mas_Salesforce.Sf_HQ As Hq," +
                    "Mas_SF_Designation.Designation_Name As Designation," +
                    "Reporting_manager.Sf_Name AS ReportingManger," +
                    "Reporting_manager.Sf_HQ AS ReportingMangerHQ," +
                    "Reporting_manager_two.Sf_Name AS ReportingMangerTwo," +
                    "Reporting_manager_two.Sf_HQ AS ReportingMangerTwoHQ," +
                    "Mas_Salesforce.Sf_HQ AS ReportingMangerTwoHQ," +
                    "Mas_State.StateName AS State," +
                    "Mas_Salesforce.sf_emp_id AS EmplCode," +
                    "Mas_Salesforce.Sf_Joining_Date AS JoiningDate " +
                    "FROM Mas_Salesforce ";

                string qryJoins = " LEFT JOIN Mas_SF_Designation ON Mas_SF_Designation.Designation_Code =Mas_Salesforce.Designation_Code " +
                    "LEFT JOIN Mas_Salesforce AS Reporting_manager ON Reporting_manager.Sf_Code = Mas_Salesforce.Sf_Code " +
                    "LEFT JOIN Mas_Salesforce AS Reporting_manager_two ON Reporting_manager_two.Sf_Code = Reporting_manager.Sf_Code " +
                    "LEFT JOIN Mas_State ON Mas_Salesforce.State_Code = Mas_State.State_Code ";
                string qryWhere = " WHERE Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag='0' ";





                switch (Request.QueryString["viewby"])
                {
                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });

                        qryWhere += " AND Mas_Salesforce.subdivision_code like '%"+ Request.QueryString["viewquery"] + ",%' ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "State", Value = Request.QueryString["viewbyname"] });

                        qryWhere += " AND Mas_Salesforce.State_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;


                    default:
                        break;

                }
                switch (Request.QueryString["splitby"])
                {
                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });

                        qryWhere += " AND Mas_Salesforce.subdivision_code like '%" + Request.QueryString["splitquery"] + ",%' ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "State", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Salesforce.State_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;
                    default:
                        break;

                }

                string strQry = qrySelects + qryJoins + qryWhere;

                records = db_ER.Exec_DataSet(strQry);
                DataTable table = records.Tables[0];
                DrillDownRecords.DataSource = table;
                DrillDownRecords.DataBind();

                TableCaptions.DataSource = Captions;
                TableCaptions.DataBind();
            }
            else
            {
                Response.Redirect("~/");
                return;
            }
        }
    }
    public class CaptionData
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}