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

    public class DigitalDetailingDrilldown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public string ProductIds;
        public DigitalDetailingDrilldown()
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
                {"ActivityDate", new DrillDownColumn {Label = "Activity Date", Select = "DCRMain_Trans.Activity_Date as ActivityDate",Format="Date"}},
                {"TimeSpent", new DrillDownColumn {Label = "Time Spent (minutes)", Select = "CAST(ROUND(DATEDIFF(SECOND, tbDigitalDetailing_Head.StartTime, tbDigitalDetailing_Head.EndTime)/60.0,1) as DECIMAL(5,1)) as TimeSpent"}},
            };

            VisibleColumns = new List<string>
            {
                "SalesforceName",
                "SalesforceHQ",
                "DoctorName",
                "DoctorSpeciality",
                 "ActivityDate",
                "TimeSpent",
            };
        }
        public override void AfterInit()
        {
            setDates();
            FileName = "Digital Detailing";
            PreferenceName = "DigitalDetailingDrilldown";
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
        public string GetBrandDetailingQuery()
        {
            string sqlQry = string.Empty;
            sqlQry = "with DDetailing as (Select " + GetSelects() +
                ",ROW_NUMBER() over (order by DDSl_No) as Rownum FROM DCRMain_Trans " +
                "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
                "INNER JOIN Mas_ListedDr ON cast (Mas_ListedDr.ListedDrCode as varchar ) = tbDigitalDetailing_Head.MSL_code ";

            sqlQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =DCRMain_Trans.sf_code ";

            if (Filters["widgetFilters"]["viewby"] == "brand")
            {
                sqlQry += "INNER JOIN Mas_Product_Brand ON cast(Mas_Product_Brand.Product_Brd_Code as varchar) =tbDigitalDetailing_Head.Product_Code AND Mas_Product_Brand.Product_Brd_Code = " + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                sqlQry += "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                    "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_Doctor_Speciality.Doc_Special_Code = " + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "priority")
            {
                sqlQry += "INNER JOIN mas_product_detail ON mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
                    "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0  " +
                    "INNER JOIN Map_LstDrs_Product ON  CAST(Map_LstDrs_Product.Listeddr_Code AS VARCHAR) =tbDigitalDetailing_Head.MSL_code AND Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code AND Map_LstDrs_Product.Product_Priority=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "product")
            {
                sqlQry += "INNER JOIN mas_product_detail ON mas_product_detail.Product_code_SlNo =" + Filters["widgetFilters"]["viewquery"] + " AND  mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
                    "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 ";
            }

                if (VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " LEFT JOIN Mas_Territory_Creation ON cast(Mas_Territory_Creation.Territory_Code as int) = Mas_ListedDr.Territory_Code ";
            }
            sqlQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " ";
            if (Filters["widgetFilters"]["viewby"] == "user")
            {
                sqlQry += "AND DCRMain_Trans.Sf_Code ='" + Filters["widgetFilters"]["viewquery"] + "' ";

            }
            
            sqlQry += " )";
            return sqlQry;
        }
        public string getQuery()
        {
            return GetBrandDetailingQuery();
        }

        public override string GetRecords()
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet result;
            var strQry = getQuery()+" select * from DDetailing   where Rownum >= "+ StartRow + " AND Rownum <= "+ EndRow;
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
            var strQry = getQuery()+ " select * from DDetailing";
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

        public string GetBrandDetailingTotalQuery()
        {
            var strQry = string.Empty;
            strQry = "Select COUNT(DCRMain_Trans.trans_slno) as totalRecords " +
                "FROM DCRMain_Trans " +
                "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno ";

            strQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =DCRMain_Trans.sf_code " +
                "INNER JOIN Mas_ListedDr ON cast (Mas_ListedDr.ListedDrCode as varchar ) = tbDigitalDetailing_Head.MSL_code ";

            if (Filters["widgetFilters"]["viewby"] == "brand")
            {
                strQry += "INNER JOIN Mas_Product_Brand ON cast(Mas_Product_Brand.Product_Brd_Code as varchar) =tbDigitalDetailing_Head.Product_Code AND Mas_Product_Brand.Product_Brd_Code = " + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                strQry += "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                    "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_Doctor_Speciality.Doc_Special_Code = " + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "priority")
            {
                strQry += "INNER JOIN mas_product_detail ON mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
                    "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0  " +
                    "INNER JOIN Map_LstDrs_Product ON  CAST(Map_LstDrs_Product.Listeddr_Code AS VARCHAR) =tbDigitalDetailing_Head.MSL_code AND Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code AND Map_LstDrs_Product.Product_Priority=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            else if (Filters["widgetFilters"]["viewby"] == "product")
            {
                strQry += "INNER JOIN mas_product_detail ON mas_product_detail.Product_code_SlNo ="+ Filters["widgetFilters"]["viewquery"] + " AND  mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
                    "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 ";
            }

            strQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " ";
            if (Filters["widgetFilters"]["viewby"] == "user")
            {
                strQry += "AND DCRMain_Trans.Sf_Code ='" + Filters["widgetFilters"]["viewquery"]+"'";

            }
            return strQry;
        }
        public override int GetTotalRecords()
        {
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;
            var strQry = string.Empty;
            strQry = GetBrandDetailingTotalQuery();


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

            if (Filters["widgetFilters"]["viewby"] == "brand")
            {
                setCaption("brand", Filters["widgetFilters"]["viewquery"], "Brand");
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                setCaption("speciality", Filters["widgetFilters"]["viewquery"], "Speciality");
            }
            else if (Filters["widgetFilters"]["viewby"] == "product")
            {
                setCaption("product", Filters["widgetFilters"]["viewquery"], "Product");
            }
            else if (Filters["widgetFilters"]["viewby"] == "user")
            {
                setCaption("fieldforce", Filters["widgetFilters"]["viewquery"], "User");
            }
        }

    }
}
