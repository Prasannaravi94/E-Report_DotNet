using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi
{

    public class GroupPriorityDrillDown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public GroupPriorityDrillDown()
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
                {"GroupName", new DrillDownColumn {Label = "Group Name", Select = "Mas_Product_Group.Product_Grp_Name as GroupName"}},
                {"ActivityDate", new DrillDownColumn {Label = "Activity Date", Select = "DCRMain_Trans.Activity_Date as ActivityDate",Format="Date"}},
            };

            VisibleColumns = new List<string>
            {
                "SalesforceName",
                "SalesforceHQ",
                "DoctorName",
                "DoctorSpeciality",
                "GroupName"
            };
        }
        public override void AfterInit()
        {
            FileName = "Priority Group Wise visited";
            PreferenceName = "GroupPriorityDrillDown";
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

        public string getQuery()
        {
            setDates();
            var sqlQry = "SELECT " + GetSelects() +
                " FROM Map_LstDrs_Product " +
                "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                "INNER JOIN DCRMain_Trans ON DCRMain_Trans.sf_code = Map_LstDrs_Product.Sf_Code " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.Trans_Detail_Info_Type=1 AND DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND Map_LstDrs_Product.Listeddr_Code = DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";

            if (Filters["widgetFilters"]["viewquery"] == "1")
            {
                sqlQry += " AND ((Map_LstDrs_Product.Product_Priority = 1 AND (CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code) = 1)) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "2")
            {
                sqlQry += " AND (Map_LstDrs_Product.Product_Priority =2 AND (CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code, CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) + 1) = CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code)+1) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "3")
            {
                sqlQry += " AND (Map_LstDrs_Product.Product_Priority =3 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1)+1) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "4")
            {
                sqlQry += " AND (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1)+1) )";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "5")
            {
                sqlQry += "AND (Map_LstDrs_Product.Product_Priority =5 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1)+1) ) ";
            }


            sqlQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL ";
            if (VisibleColumns.Contains("DoctorName") || VisibleColumns.Contains("DoctorSpeciality") || VisibleColumns.Contains("DoctorCategory") || VisibleColumns.Contains("DoctorClass") || VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code  ";
            }
            if (VisibleColumns.Contains("SalesforceName") || VisibleColumns.Contains("SalesforceHQ") || VisibleColumns.Contains("SalesforceDesignation") || VisibleColumns.Contains("SalesforceEmployeeId") || VisibleColumns.Contains("SalesforceJoiningDate") || VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =DCRMain_Trans.sf_code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            if (VisibleColumns.Contains("GroupName"))
            {
                sqlQry += " LEFT JOIN Mas_Product_Group ON Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code ";
            }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            sqlQry += " WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND Map_LstDrs_Product.Product_Priority =" + Filters["widgetFilters"]["viewquery"] + " AND Map_LstDrs_Product.Sf_Code IN (" + getSfCodes() + ") AND Map_LstDrs_Product.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.FieldWork_Indicator = 'F' ";


            if (Filters["widgetFilters"].ContainsKey("groups") && Filters["widgetFilters"]["groups"] != null && Filters["widgetFilters"]["groups"] != "")
            {
                sqlQry += " AND Mas_Product_Detail.Product_Grp_Code IN (" + Filters["widgetFilters"]["groups"] + ") ";
            }
            return sqlQry;
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

        public override int GetTotalRecords()
        {
            setDates();
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;

            var strQry = "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.trans_slno) as totalRecords " +
                "FROM Map_LstDrs_Product " +
                "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                "INNER JOIN DCRMain_Trans ON DCRMain_Trans.sf_code = Map_LstDrs_Product.Sf_Code " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.Trans_Detail_Info_Type=1 AND DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND Map_LstDrs_Product.Listeddr_Code = DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";

            if (Filters["widgetFilters"]["viewquery"] == "1")
            {
                strQry += " AND ((Map_LstDrs_Product.Product_Priority = 1 AND (CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code) = 1)) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "2")
            {
                strQry += " AND (Map_LstDrs_Product.Product_Priority =2 AND (CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code, CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) + 1) = CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code)+1) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "3")
            {
                strQry += " AND (Map_LstDrs_Product.Product_Priority =3 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1)+1) ) ";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "4")
            {
                strQry += " AND (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1)+1) )";
            }
            else if (Filters["widgetFilters"]["viewquery"] == "5")
            {
                strQry += "AND (Map_LstDrs_Product.Product_Priority =5 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1)+1) ) ";
            }

            strQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                "WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND Map_LstDrs_Product.Product_Priority =" + Filters["widgetFilters"]["viewquery"] + " AND Map_LstDrs_Product.Sf_Code IN (" + getSfCodes() + ") AND Map_LstDrs_Product.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.FieldWork_Indicator = 'F' ";

            if (Filters["widgetFilters"].ContainsKey("groups") && Filters["widgetFilters"]["groups"] != null && Filters["widgetFilters"]["groups"] != "")
            {
                strQry += " AND Mas_Product_Detail.Product_Grp_Code IN (" + Filters["widgetFilters"]["groups"] + ") ";
            }

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
            setCaption("fieldforce", Filters["dashboardFilters"]["sfcode"], "Field Force");
            Captions["Month"] = monthFormatter(Filters["dashboardFilters"]["monthStart"]) + " - " + monthFormatter(Filters["dashboardFilters"]["monthEnd"]);
            Captions["Priority"] = Filters["widgetFilters"]["viewquery"];
            if (Filters["widgetFilters"].ContainsKey("groups") && Filters["widgetFilters"]["groups"] != null && Filters["widgetFilters"]["groups"] != "")
            {
                setCaption("group", Filters["widgetFilters"]["groups"], "Groups");
            }
        }
    }
}
