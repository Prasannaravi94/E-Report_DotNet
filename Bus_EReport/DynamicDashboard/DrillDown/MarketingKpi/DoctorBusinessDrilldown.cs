using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi
{

    public class DoctorBusinessDrilldown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public string ProductIds;
        public DoctorBusinessDrilldown()
        {

            Columns = new Dictionary<string, DrillDownColumn>
            {
                {"SalesforceName", new DrillDownColumn {Label = "FF Name",Select="Mas_Salesforce.sf_name as SalesforceName"}},
                {"SalesforceHQ", new DrillDownColumn {Label = "FF HQ",Select="Mas_Salesforce.sf_hq as SalesforceHQ"}},
                {"SalesforceDesignation", new DrillDownColumn {Label = "FF Designation",Select="Mas_Salesforce.sf_desgn as SalesforceDesignation"}},
                {"SalesforceEmployeeId", new DrillDownColumn {Label = "Empl Id",Select="Mas_Salesforce.sf_emp_id as SalesforceEmployeeId"}},
                {"SalesforceJoiningDate", new DrillDownColumn {Label = "FF Joining Date", Select = "Mas_Salesforce.sf_joining_date as SalesforceJoiningDate",Format="Date"}},
                {"SalesforceState", new DrillDownColumn {Label = "FF State",Select="Mas_State.StateName as SalesforceState"}},
                {"DoctorName", new DrillDownColumn {Label = "Doctor Name", Select = "Mas_ListedDr.ListedDr_Name as DoctorName"}},
                {"DoctorSpeciality", new DrillDownColumn {Label = "Doctor Speciality", Select = "Mas_ListedDr.Doc_Spec_ShortName as DoctorSpeciality"}},
                {"DoctorCategory", new DrillDownColumn {Label = "Doctor Category",Select="Mas_ListedDr.Doc_Cat_ShortName as DoctorCategory"}},
                {"DoctorClass", new DrillDownColumn {Label = "Doctor Class", Select = "Mas_ListedDr.Doc_Class_ShortName as DoctorClass"}},
                {"DoctorTerritory", new DrillDownColumn {Label = "Doctor Territory", Select = "Mas_Territory_Creation.Territory_Name as DoctorTerritory"}},
                {"ActivityDate", new DrillDownColumn {Label = "Activity Date", Select = "DCRMain_Trans.Activity_Date as ActivityDate", Format = "Date"}},
            };

            VisibleColumns = new List<string>
            {
                "SalesforceName",
                "SalesforceHQ",
                "DoctorName",
                "DoctorSpeciality",
            };
        }
        public override void AfterInit()
        {
            setDates();
            FileName = "Doctor Business";
            PreferenceName = "DoctorBusinessDrilldown";
        }
        public void setDates()
        {
            string[] monthStart = Filters["dashboardFilters"]["monthStart"].Split('-');
            StartMonth = Convert.ToInt32(monthStart[0]);
            StartYear = Convert.ToInt32(monthStart[1]);

            string[] monthEnd = Filters["dashboardFilters"]["monthEnd"].Split('-');
            EndMonth = Convert.ToInt32(monthEnd[0]);
            EndYear = Convert.ToInt32(monthEnd[1]);
        }
        public string GetDoctorsQuery()
        {
            string sqlQry = string.Empty;
            sqlQry = "Select " + GetSelects() +
                " From DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";
            if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                sqlQry += "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                sqlQry += "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                sqlQry += "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "class")
            {
                sqlQry += "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode ";
            }
            if (VisibleColumns.Contains("SalesforceName") || VisibleColumns.Contains("SalesforceHQ") || VisibleColumns.Contains("SalesforceDesignation") || VisibleColumns.Contains("SalesforceEmployeeId") || VisibleColumns.Contains("SalesforceJoiningDate") || VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =DCRMain_Trans.sf_code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            sqlQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " ";
            if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                sqlQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                sqlQry += " AND Mas_Doctor_Speciality.Doc_Special_Code = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                sqlQry += " AND Mas_Doctor_Category.Doc_Cat_Code = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "class")
            {
                sqlQry += " AND Mas_Doc_Class.Doc_ClsCode = " + Filters["widgetFilters"]["viewquery"];
            }
            return sqlQry;
        }
        public string getQuery()
        {
            return GetDoctorsQuery();
        }

        public override string GetRecords()
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet result;
            var strQry = getQuery() + " ORDER BY DCRDetail_Lst_Trans.trans_slno OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY";
            try
            {
                result = db_ER.Exec_DataSet(strQry);
                return GetTableBody(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override DataSet Export()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet result;
            var strQry = getQuery();
            try
            {
                result = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string GetDoctorsTotalQuery()
        {
            var strQry = string.Empty;
            strQry = "Select COUNT(DCRDetail_Lst_Trans.Trans_Detail_Slno) as totalRecords " +
                " From DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";
            if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                strQry += "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                strQry += "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                strQry += "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "class")
            {
                strQry += "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode ";
            }

            strQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " ";

            if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                strQry += " AND Mas_Doctor_Speciality.Doc_Special_Code = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                strQry += " AND Mas_Doctor_Category.Doc_Cat_Code = " + Filters["widgetFilters"]["viewquery"];
            }
            else if (Filters["widgetFilters"]["viewby"] == "class")
            {
                strQry += " AND Mas_Doc_Class.Doc_ClsCode = " + Filters["widgetFilters"]["viewquery"];
            }

            return strQry;
        }
        public override int GetTotalRecords()
        {
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;
            var strQry = string.Empty;
            strQry = GetDoctorsTotalQuery();


            try
            {
                result = db_ER.Exec_DataSet(strQry);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    TotalRecords = Convert.ToInt32(Row["totalRecords"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TotalRecords;
        }

        public override void SetCaptions()
        {
            if (Filters["widgetFilters"]["splitquery"] == "Mapped Doctors")
            {
                Filters["widgetFilters"]["splitquery"] = "DCR Doctors";
            }

            setCaption("fieldforce", Filters["dashboardFilters"]["sfcode"], "Field Force");
            Captions["Month"] = monthFormatter(Filters["dashboardFilters"]["monthStart"]) + " - " + monthFormatter(Filters["dashboardFilters"]["monthEnd"]);

            if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                setCaption("campaign", Filters["widgetFilters"]["viewquery"], "Campaign");
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                setCaption("speciality", Filters["widgetFilters"]["viewquery"], "Speciality");
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                setCaption("category", Filters["widgetFilters"]["viewquery"], "Category");
            }
            else if (Filters["widgetFilters"]["viewby"] == "class")
            {
                setCaption("class", Filters["widgetFilters"]["viewquery"], "Class");
            }
        }
    }
}
