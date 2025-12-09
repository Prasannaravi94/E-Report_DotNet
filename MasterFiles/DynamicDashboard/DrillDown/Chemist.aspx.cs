using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_Chemist : System.Web.UI.Page
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
                Captions.Add(new CaptionData { Value = "Chemists" });
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

                string qrySelects = "Select Row_number() over(order by Mas_Chemists.Chemists_Name) AS No," +
                    "Mas_Chemists.Chemists_Name AS ChemistName," +
                    "Mas_Chemists.Chemists_Contact AS ContactPerson," +
                    "Mas_Chemists.Chemists_Phone AS PhoneNumber," +
                    "Mas_Territory_Creation.Territory_Name AS Territory," +
                    "Mas_Chemist_Class.Chem_Class_Name AS Class," +
                    "Mas_Chemist_Category.Chem_Cat_Name AS Category " +
                    "from Mas_Chemists ";

                string qryJoins = "LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code = Mas_Chemists.Territory_Code " +
                    "LEFT JOIN Mas_Chemist_Class ON   Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code " +
                    "LEFT JOIN Mas_Chemist_Category ON  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code " +
                    "LEFT JOIN Mas_Salesforce ON Mas_Salesforce.sf_code = Mas_Chemists.sf_code ";
                string qryWhere = " WHERE Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' ";





                switch (Request.QueryString["viewby"])
                {
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Cat_Code ='" + Request.QueryString["viewquery"] + "'";
                        break;
                    case "class":
                        Captions.Add(new CaptionData { Label = "Class", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Class_Code ='" + Request.QueryString["viewquery"] + "'";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });

                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["viewquery"] + "' ";

                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });
                        qryJoins += " LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND  Mas_State.State_Code='" + Request.QueryString["viewquery"] + "' ";

                        qryWhere += " AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code ";
                        break;


                    default:
                        break;

                }

                switch (Request.QueryString["splitby"])
                {
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Cat_Code ='" + Request.QueryString["splitquery"] + "'";
                        break;
                    case "class":
                        Captions.Add(new CaptionData { Label = "Class", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Class_Code ='" + Request.QueryString["splitquery"] + "'";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });

                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["splitquery"] + "' ";

                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });
                        qryJoins += " LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND  Mas_State.State_Code='" + Request.QueryString["splitquery"] + "' ";

                        qryWhere += " AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code ";
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