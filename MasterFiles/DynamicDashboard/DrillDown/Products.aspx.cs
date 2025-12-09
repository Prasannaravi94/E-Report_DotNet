using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_Products : System.Web.UI.Page
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
                Captions.Add(new CaptionData { Value = "Products" });
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

                string qrySelects = "select " +
                    "Row_number() over(order by Mas_Product_Detail.Prod_Detail_Sl_No) AS No," +
                    "Mas_Product_Detail.Product_Detail_Name AS Name," +
                    "Mas_Product_Detail.Product_Sale_Unit AS Pack," +
                    "Mas_Product_Detail.Sale_Erp_Code AS SalesERPCode," +
                    "Mas_Product_Detail.Sample_Erp_Code AS SampleERPCode," +
                    "Mas_Product_Brand.Product_Brd_Name AS Brand ," +
                    "Mas_Product_Category.Product_Cat_Name AS Category ," +
                    "Mas_Product_Group.Product_Grp_Name AS GroupName " +
                    "FROM Mas_Product_Detail ";

                string qryJoins = " LEFT JOIN Mas_Product_Brand ON Mas_Product_Brand.Product_Brd_Code=Mas_Product_Detail.Product_Brd_Code " +
                    "LEFT JOIN Mas_Product_Category ON Mas_Product_Category.Product_Cat_Code = Mas_Product_Detail.Product_Cat_Code " +
                    "LEFT JOIN Mas_Product_Group ON Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code " +
                    "LEFT JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' " +
                    "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') >0 ";

                string qryWhere = " WHERE Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ")";


                switch (Request.QueryString["viewby"])
                {
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Cat_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;

                    case "brand":
                        Captions.Add(new CaptionData { Label = "Brand", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Brd_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;
                    case "group":
                        Captions.Add(new CaptionData { Label = "Group", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Grp_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Product_Detail.subdivision_code like '%" + Request.QueryString["viewquery"] + ",%' ";
                        break;
                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Product_Detail.state_code like '%" + Request.QueryString["viewquery"] + ",%' ";
                        break;
                    default:
                        break;

                }

                switch (Request.QueryString["splitby"])
                {
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Cat_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;

                    case "brand":
                        Captions.Add(new CaptionData { Label = "Brand", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Brd_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;
                    case "group":
                        Captions.Add(new CaptionData { Label = "Group", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Product_Detail.Product_Grp_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;
                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Product_Detail.subdivision_code like '%" + Request.QueryString["splitquery"] + ",%' ";
                        break;
                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Product_Detail.state_code like '%" + Request.QueryString["splitquery"] + ",%' ";
                        break;

                    default:
                        break;

                }

                string strQry = qrySelects + qryJoins + qryWhere+ " GROUP BY Mas_Product_Detail.Prod_Detail_Sl_No,Mas_Product_Detail.Product_Detail_Name,Mas_Product_Detail.Product_Sale_Unit,Mas_Product_Detail.Sale_Erp_Code,Mas_Product_Detail.Sample_Erp_Code,Mas_Product_Brand.Product_Brd_Name,Mas_Product_Category.Product_Cat_Name,Mas_Product_Group.Product_Grp_Name";

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