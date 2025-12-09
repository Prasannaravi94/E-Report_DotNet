using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_Stockist : System.Web.UI.Page
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
                Captions.Add(new CaptionData { Value = "Stockists" });
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

                string qrySelects = "Select Row_number() over(order by Mas_Stockist.Stockist_Code) AS No," +
                    "Mas_Stockist.Stockist_Name AS Name," +
                    "Mas_Stockist.Territory AS Hq," +
                    "Mas_Stockist.State AS State," +
                    "Mas_Stockist.Stockist_Designation AS ERPCode," +
                    "STRING_AGG(Mas_Salesforce.Sf_Name, ', ') AS AssignedEmpl " +
                    "From Mas_Stockist ";

                string qryJoins = " LEFT JOIN Mas_Salesforce ON CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.Sf_Code + ',') > 0 ";
                string qryWhere = " WHERE Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND Mas_Stockist.Division_Code='" + DivisionCode + "' AND Mas_Stockist.Stockist_Active_Flag='0' ";





                switch (Request.QueryString["viewby"])
                {
                    case "hq":
                        Captions.Add(new CaptionData { Label = "Territory", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Stockist.Territory ='" + Request.QueryString["viewquery"] + "'";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });

                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["viewquery"] + "' ";

                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.Sf_Code + ',') > 0 ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });

                        qryWhere += " AND Mas_Stockist.State ='" + Request.QueryString["viewquery"] + "'";
                        break;


                    default:
                        break;

                }

                switch (Request.QueryString["splitby"])
                {
                    case "hq":
                        Captions.Add(new CaptionData { Label = "Territory", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Stockist.Territory ='" + Request.QueryString["splitquery"] + "'";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });

                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["splitquery"] + "' ";

                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.Sf_Code + ',') > 0 ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });

                        qryWhere += " AND Mas_Stockist.State ='" + Request.QueryString["splitquery"] + "'";
                        break;


                    default:
                        break;

                }

                string strQry = qrySelects + qryJoins + qryWhere+ " GROUP By Mas_Stockist.Stockist_Code,Mas_Stockist.Stockist_Name,Mas_Stockist.Territory,Mas_Stockist.State,Mas_Stockist.Stockist_Designation";

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