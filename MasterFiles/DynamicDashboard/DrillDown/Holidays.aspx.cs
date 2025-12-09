using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_Holidays : System.Web.UI.Page
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
                Captions.Add(new CaptionData { Value = "Holidays" });
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
                    "Row_number() over(order by Mas_Statewise_Holiday_Fixation.Holiday_Name_Sl_No) AS No," +
                    "Mas_Statewise_Holiday_Fixation.Holiday_name As Name," +
                    "CONVERT(VARCHAR, Mas_Statewise_Holiday_Fixation.Holiday_Date, 103) As Date " +
                    "FROM Mas_Statewise_Holiday_Fixation ";
                string qryWhere = " WHERE  Mas_Statewise_Holiday_Fixation.Division_Code='" + DivisionCode + "' AND Mas_Statewise_Holiday_Fixation.Academic_Year=year(getDate()) ";





                switch (Request.QueryString["viewby"])
                {

                    case "state":
                        Captions.Add(new CaptionData { Label = "State", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Statewise_Holiday_Fixation.state_code Like '%"+ Request.QueryString["viewquery"] + ",%'";
                        break;
                    default:
                        break;

                }

                string strQry = qrySelects + qryWhere;

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