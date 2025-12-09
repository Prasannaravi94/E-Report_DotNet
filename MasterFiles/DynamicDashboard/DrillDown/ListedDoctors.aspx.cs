using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_DynamicDashboard_DrillDown_ListedDoctors : System.Web.UI.Page
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
                Captions.Add(new CaptionData { Value = "Listed Doctors" });
                Captions.Add(new CaptionData { Label="Field Force",Value = Request.QueryString["sfname"].ToString() });

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

                string qrySelects = "SELECT " +
                    "Row_number() over(order by Mas_Salesforce.sf_name) AS No," +
                    "Mas_Salesforce.sf_name + ' - ' + Mas_Salesforce.sf_Designation_Short_Name + ' - ' + Mas_Salesforce.sf_hq AS FieldForceName," +
                    "Mas_ListedDr.ListedDr_Name AS DoctorName," +
                    "Mas_Doctor_Speciality.Doc_Special_Name AS DoctorSpeciality," +
                    "Mas_Doctor_Category.Doc_Cat_Name AS DoctorCategory," +
                    "Mas_Doc_Class.Doc_ClsName AS DoctorClass," +
                    "Mas_Territory_Creation.Territory_Name AS DoctorTerritory" +
                    " FROM Mas_ListedDr ";

                string qryJoins ="LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Code = Mas_ListedDr.Doc_Special_Code " +
                    " LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Code = Mas_ListedDr.Doc_Cat_Code " +
                    " LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_ClsCode = Mas_ListedDr.Doc_ClsCode " +
                    " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code = Mas_ListedDr.Territory_Code " +
                    " LEFT JOIN Mas_Salesforce ON Mas_Salesforce.sf_code = Mas_ListedDr.sf_code ";
                string qryWhere =" WHERE Mas_ListedDr.listeddr_active_flag = '0' AND Mas_ListedDr.sf_code IN(" + Sfcodes + ") AND Mas_ListedDr.division_code = '"+ DivisionCode + "'";



                

                switch (Request.QueryString["viewby"])
                {
                    case "speciality":
                        Captions.Add(new CaptionData { Label = "Speciality", Value= Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Doc_Special_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Doc_Cat_Code ='" + Request.QueryString["viewquery"] + "' ";
                        break;
                    case "class":
                        Captions.Add(new CaptionData { Label = "Class", Value = Request.QueryString["viewbyname"] });
                        qryWhere += " AND Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Doc_ClsCode ='" + Request.QueryString["viewquery"] + "' ";
                        break;
                    case "campaign":
                        Captions.Add(new CaptionData { Label = "Campaign", Value = Request.QueryString["viewbyname"] });
                        qryJoins += " LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND  Mas_Doc_SubCategory.Doc_SubCatCode='" + Request.QueryString["viewquery"] + "' ";
                        qryWhere += " AND CHARINDEX(cast(Mas_Doc_SubCategory.Doc_SubCatCode as varchar),Mas_ListedDr.Doc_SubCatCode)>0 ";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });

                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["viewquery"] + "' ";

                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code ";

                        break;

                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["viewbyname"] });
                        qryJoins += " LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND  Mas_State.State_Code='" + Request.QueryString["viewquery"] + "' ";

                        qryWhere += " AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code ";
                        break;


                    default:
                        break;

                }

                switch (Request.QueryString["splitby"])
                {
                    case "speciality":
                        Captions.Add(new CaptionData { Label = "Speciality", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Doctor_Speciality.Doc_Special_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;
                    case "category":
                        Captions.Add(new CaptionData { Label = "Category", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Doctor_Category.Doc_Cat_Code ='" + Request.QueryString["splitquery"] + "' ";
                        break;
                    case "class":
                        Captions.Add(new CaptionData { Label = "Class", Value = Request.QueryString["splitbyname"] });
                        qryWhere += " AND Mas_Doc_Class.Doc_ClsCode ='" + Request.QueryString["splitquery"] + "' ";
                        break;

                    case "campaign":
                        Captions.Add(new CaptionData { Label = "Campaign", Value = Request.QueryString["splitbyname"] });
                        qryJoins += " LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND  Mas_Doc_SubCategory.Doc_SubCatCode='" + Request.QueryString["splitquery"] + "' ";
                        qryWhere += " AND CHARINDEX(cast(Mas_Doc_SubCategory.Doc_SubCatCode as varchar),Mas_ListedDr.Doc_SubCatCode)>0 ";
                        break;

                    case "subdivision":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });
                        qryJoins += " LEFT JOIN mas_subdivision ON mas_subdivision.SubDivision_Active_Flag = '0' AND mas_subdivision.Div_Code='" + DivisionCode + "' AND  mas_subdivision.subdivision_code='" + Request.QueryString["splitquery"] + "' ";
                        qryWhere += " AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code ";
                        break;
                    case "state":
                        Captions.Add(new CaptionData { Label = "Subdivision", Value = Request.QueryString["splitbyname"] });
                        qryJoins += " LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND  Mas_State.State_Code='" + Request.QueryString["splitquery"] + "' ";

                        qryWhere += " AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code ";
                        break;
                    default:
                        break;

                }

                string strQry = qrySelects+qryJoins+qryWhere;

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