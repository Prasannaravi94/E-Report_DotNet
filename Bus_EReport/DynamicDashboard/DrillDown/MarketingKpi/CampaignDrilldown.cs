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

    public class CampaignDrilldown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public string StartDate;
        public string EndDate;
        public string ProductIds;
        public CampaignDrilldown()
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
            FileName = "Campaign";
            PreferenceName = "CampaignDrilldown";
        }
        public void setDates()
        {
            string[] monthStart = Filters["dashboardFilters"]["monthStart"].Split('-');
            StartMonth = Convert.ToInt32(monthStart[0]);
            StartYear = Convert.ToInt32(monthStart[1]);

            string[] monthEnd = Filters["dashboardFilters"]["monthEnd"].Split('-');
            EndMonth = Convert.ToInt32(monthEnd[0]);
            EndYear = Convert.ToInt32(monthEnd[1]);

            StartDate = StartYear + "-" + StartMonth + "-01";
            EndDate = EndYear + "-" + EndMonth + "-" + DateTime.DaysInMonth(EndYear, EndMonth);
        }
        public string GetDoctorsQuery()
        {
            var strQry = string.Empty;
            strQry = "Select " + GetSelects() +
                " From DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND ((Mas_Doc_SubCategory.Effective_from >='" + StartDate + "' AND Mas_Doc_SubCategory.Effective_from <='" + EndDate + "')OR(Mas_Doc_SubCategory.Effective_to >='" + StartDate + "' AND Mas_Doc_SubCategory.Effective_from <='" + EndDate + "')) " +
                " ";
            if (VisibleColumns.Contains("SalesforceName") || VisibleColumns.Contains("SalesforceHQ") || VisibleColumns.Contains("SalesforceDesignation") || VisibleColumns.Contains("SalesforceEmployeeId") || VisibleColumns.Contains("SalesforceJoiningDate") || VisibleColumns.Contains("SalesforceState"))
            {
                strQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =DCRMain_Trans.sf_code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                strQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                strQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            strQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear +" AND Mas_Doc_SubCategory.Doc_SubCatCode = "+ Filters["widgetFilters"]["viewquery"]+" ";

            if (Filters["widgetFilters"]["viewby"] == "brand" )
            {
                if (!Filters["widgetFilters"].ContainsKey("brands") || Filters["widgetFilters"]["brands"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.Brand_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignBrands(Filters["widgetFilters"]["brands"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "product")
            {
                if (!Filters["widgetFilters"].ContainsKey("products") || Filters["widgetFilters"]["products"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.product_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignProdcuts(Filters["widgetFilters"]["products"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                if (!Filters["widgetFilters"].ContainsKey("specialities") || Filters["widgetFilters"]["specialities"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.spec_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignSpecialities(Filters["widgetFilters"]["specialities"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                if (!Filters["widgetFilters"].ContainsKey("categories") || Filters["widgetFilters"]["categories"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.cat_code is not null ";
                }
                else
                {
                    strQry += "AND " + LikeQueryForCampaignCategories(Filters["widgetFilters"]["categories"]) + " ";
                }
            }
            return strQry;
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
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND ((Mas_Doc_SubCategory.Effective_from >='" + StartDate + "' AND Mas_Doc_SubCategory.Effective_from <='" + EndDate + "')OR(Mas_Doc_SubCategory.Effective_to >='" + StartDate + "' AND Mas_Doc_SubCategory.Effective_from <='" + EndDate + "')) " +
                "";
            

            strQry += " WHERE DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND Mas_Doc_SubCategory.Doc_SubCatCode="+ Filters["widgetFilters"]["viewquery"]+" ";

            if (Filters["widgetFilters"]["viewby"] == "brand")
            {
                if (!Filters["widgetFilters"].ContainsKey("brands") || Filters["widgetFilters"]["brands"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.Brand_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignBrands(Filters["widgetFilters"]["brands"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "product")
            {
                if (!Filters["widgetFilters"].ContainsKey("products") || Filters["widgetFilters"]["products"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.product_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignProdcuts(Filters["widgetFilters"]["products"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality")
            {
                if (!Filters["widgetFilters"].ContainsKey("specialities") || Filters["widgetFilters"]["specialities"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.spec_code is not null ";
                }
                else
                {
                    strQry += " AND " + LikeQueryForCampaignSpecialities(Filters["widgetFilters"]["specialities"]) + " ";
                }
            }
            else if (Filters["widgetFilters"]["viewby"] == "category")
            {
                if (!Filters["widgetFilters"].ContainsKey("categories") || Filters["widgetFilters"]["categories"] == "")
                {
                    strQry += " AND Mas_Doc_SubCategory.cat_code is not null ";
                }
                else
                {
                    strQry += "AND " + LikeQueryForCampaignCategories(Filters["widgetFilters"]["categories"]) + " ";
                }
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
            setCaption("campaign", Filters["widgetFilters"]["viewquery"], "Campaign");
            if (Filters["widgetFilters"]["viewby"] == "brand" && Filters["widgetFilters"].ContainsKey("brands") && Filters["widgetFilters"]["brands"] !="")
            {
                setCaption("brand", Filters["widgetFilters"]["brands"], "Brands");
            }
            else if (Filters["widgetFilters"]["viewby"] == "product" && Filters["widgetFilters"].ContainsKey("products") && Filters["widgetFilters"]["products"] != "")
            {
                setCaption("product", Filters["widgetFilters"]["products"], "Products");
            }
            else if (Filters["widgetFilters"]["viewby"] == "speciality" && Filters["widgetFilters"].ContainsKey("specialities") && Filters["widgetFilters"]["specialities"] != "")
            {
                setCaption("speciality", Filters["widgetFilters"]["specialities"], "Specialities");
            }
            else if (Filters["widgetFilters"]["viewby"] == "category" && Filters["widgetFilters"].ContainsKey("categories") && Filters["widgetFilters"]["categories"] != "")
            {
                setCaption("category", Filters["widgetFilters"]["categories"], "Categories");
            }

        }

        public string LikeQueryForDCRProducts(string prodcuts)
        {
            string query = "(";
            string[] values = prodcuts.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('#' + '" + value.Trim('\'') + "' + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' ) > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }

        public string LikeQueryForCampaignBrands(string brands)
        {
            string query = "(";
            string[] values = brands.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('," + value.ToString() + ",',',' + Mas_Doc_SubCategory.Brand_code + ',') > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }

        public string LikeQueryForCampaignProdcuts(string products)
        {
            string query = "(";
            string[] values = products.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('," + value.ToString() + ",',',' + Mas_Doc_SubCategory.product_code + ',') > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }
        public string LikeQueryForCampaignSpecialities(string specialities)
        {
            string query = "(";
            string[] values = specialities.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('," + value.ToString() + ",',',' + Mas_Doc_SubCategory.spec_code + ',') > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }
        public string LikeQueryForCampaignCategories(string categories)
        {
            string query = "(";
            string[] values = categories.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('," + value.ToString() + ",',',' + Mas_Doc_SubCategory.cat_code + ',') > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }
    }
}
