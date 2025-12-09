using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi
{

    public class ExposureDrillDown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public string ProductIds;
        public ExposureDrillDown()
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
            FileName = "Exposure";
            PreferenceName = "ExposureDrillDown";
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
        
        public void setProductIds()
        {
            if (Filters["widgetFilters"].ContainsKey("products"))
            {
                ProductIds = Filters["widgetFilters"]["products"];
            }
            if (ProductIds == "")
            {
                DynamicDashboardModel dynamicDashboardModel = new DynamicDashboardModel();
                DataSet products = dynamicDashboardModel.GetProducts(DivisionCode, true, 5);
                if (products.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow Row in products.Tables[0].Rows)
                    {
                        ProductIds += "'" + Row["Id"].ToString() + "',";
                    }
                }
                if (!string.IsNullOrEmpty(ProductIds))
                {
                    ProductIds = ProductIds.TrimEnd(',');
                }
                else
                {
                    ProductIds += "0"; //to prevent sql query error
                }
            }
        }

        public string productGetQuery()
        {
            if(Filters["widgetFilters"]["splitby"] == "speciality" || Filters["widgetFilters"]["splitby"] == "category" || Filters["widgetFilters"]["splitby"] == "class")
            {
                var sqlQry = "SELECT " + GetSelects() +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(ProductIds) + " " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";

                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    sqlQry += " INNER JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Product_Code IN (" + ProductIds + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
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
                sqlQry += " WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' ";
                if(Filters["widgetFilters"]["splitby"] == "speciality")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_Special_Code =" + Filters["widgetFilters"]["viewquery"];
                }else if (Filters["widgetFilters"]["splitby"] == "category")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_Cat_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "class")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_ClsCode =" + Filters["widgetFilters"]["viewquery"];
                }

                return sqlQry;
            }
            else
            {
                var sqlQry = "SELECT " + GetSelects() +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(Filters["widgetFilters"]["viewquery"]) + " ";

                sqlQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL ";

                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    sqlQry += " INNER JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Product_Code IN (" + Filters["widgetFilters"]["viewquery"] + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
                }
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
                if (VisibleColumns.Contains("DoctorTerritory"))
                {
                    sqlQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
                }
                sqlQry += " WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' ";
                return sqlQry;
            }
            
        }

        public string campaignGetQuery()
        {
            if (Filters["widgetFilters"]["splitby"] == "speciality" || Filters["widgetFilters"]["splitby"] == "category" || Filters["widgetFilters"]["splitby"] == "class")
            {
                var sqlQry = "SELECT " + GetSelects() +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";
              //  "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    sqlQry += " INNER JOIN Map_LstDrs_Product ON CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
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
                sqlQry += " WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' ";
                if (Filters["widgetFilters"]["splitby"] == "speciality")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_Special_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "category")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_Cat_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "class")
                {
                    sqlQry += "AND Mas_ListedDr.Doc_ClsCode =" + Filters["widgetFilters"]["viewquery"];
                }

                if (Filters["widgetFilters"].ContainsKey("subcategories") && Filters["widgetFilters"]["subcategories"].Trim().Length > 0)
                {
                    sqlQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN ( " + Filters["widgetFilters"]["subcategories"] + ") ";
                }

                return sqlQry;
            }
            else
            {
                var sqlQry = "SELECT " + GetSelects() +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";

                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    sqlQry += " INNER JOIN Map_LstDrs_Product ON CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
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
                sqlQry += " WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCatCode = " + Filters["widgetFilters"]["viewquery"] + " ";
                    
                return sqlQry;
            }

        }

        public string getQuery()
        {
            setProductIds();
            if (Filters["widgetFilters"]["viewby"] == "product")
            {
                return productGetQuery();
            }
            else if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                return campaignGetQuery();
            }
            return "";
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

        public string GetProductTotalRecords()
        {
            var strQry = string.Empty;
            if (Filters["widgetFilters"]["splitby"] == "speciality" || Filters["widgetFilters"]["splitby"] == "category" || Filters["widgetFilters"]["splitby"] == "class")
            {
                strQry = "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.trans_slno) as totalRecords " +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(ProductIds) + " " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";
                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    strQry += " INNER JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Product_Code IN (" + ProductIds + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
                }

                strQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' ";
                if (Filters["widgetFilters"]["splitby"] == "speciality")
                {
                    strQry += "AND Mas_ListedDr.Doc_Special_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "category")
                {
                    strQry += "AND Mas_ListedDr.Doc_Cat_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "class")
                {
                    strQry += "AND Mas_ListedDr.Doc_ClsCode =" + Filters["widgetFilters"]["viewquery"];
                }
            }
            else
            {
                strQry = "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.trans_slno) as totalRecords " +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(Filters["widgetFilters"]["viewquery"]) + " ";
                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    strQry += " INNER JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Product_Code IN (" + Filters["widgetFilters"]["viewquery"] + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
                }

                strQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'";
            }
            
            return strQry;
        }

        public string GetCampaignTotalRecords()
        {
            var strQry = string.Empty;
            if (Filters["widgetFilters"]["splitby"] == "speciality" || Filters["widgetFilters"]["splitby"] == "category" || Filters["widgetFilters"]["splitby"] == "class")
            {
                strQry = "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.trans_slno) as totalRecords " +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    strQry += " INNER JOIN Map_LstDrs_Product ON CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
                }

                strQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' ";
                if (Filters["widgetFilters"]["splitby"] == "speciality")
                {
                    strQry += "AND Mas_ListedDr.Doc_Special_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "category")
                {
                    strQry += "AND Mas_ListedDr.Doc_Cat_Code =" + Filters["widgetFilters"]["viewquery"];
                }
                else if (Filters["widgetFilters"]["splitby"] == "class")
                {
                    strQry += "AND Mas_ListedDr.Doc_ClsCode =" + Filters["widgetFilters"]["viewquery"];
                }
                if (Filters["widgetFilters"].ContainsKey("subcategories") && Filters["widgetFilters"]["subcategories"].Trim().Length > 0)
                {
                    strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN ( " + Filters["widgetFilters"]["subcategories"] + ") ";
                }
            }
            else
            {
                strQry += "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.trans_slno) as totalRecords " +
                " FROM DCRMain_Trans " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
                if (Filters["widgetFilters"]["splitquery"] == "DCR Mapped Doctors")
                {
                    strQry += " INNER JOIN Map_LstDrs_Product ON CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' )>0 AND Map_LstDrs_Product.Listeddr_code = DCRDetail_Lst_Trans.trans_detail_info_code ";
                }

                strQry += " AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "WHERE MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCatCode = " + Filters["widgetFilters"]["viewquery"] + " ";
            }

            return strQry;
        }
        public override int GetTotalRecords()
        {
            setProductIds();
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;
            var strQry =string.Empty;
            if (Filters["widgetFilters"]["viewby"] == "product")
            {
                strQry = GetProductTotalRecords();
            }else if (Filters["widgetFilters"]["viewby"] == "campaign")
            {
                strQry = GetCampaignTotalRecords();
            }
            else
            {
                return 0;
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
            if (Filters["widgetFilters"]["splitquery"] == "Mapped Doctors")
            {
                Filters["widgetFilters"]["splitquery"] = "DCR Doctors";
            }

            setCaption("fieldforce", Filters["dashboardFilters"]["sfcode"], "Field Force");
            Captions["Month"] = monthFormatter(Filters["dashboardFilters"]["monthStart"]) + " - " + monthFormatter(Filters["dashboardFilters"]["monthEnd"]);
            if (Filters["widgetFilters"].ContainsKey("splitquery") && Filters["widgetFilters"]["splitquery"] != null && Filters["widgetFilters"]["splitquery"] != "")
            {
                Captions["Split By"] = Filters["widgetFilters"]["splitquery"];
            }

            if (Filters["widgetFilters"].ContainsKey("splitby") && Filters["widgetFilters"]["splitby"] != null && Filters["widgetFilters"]["splitby"] != "")
            {
                if (Filters["widgetFilters"]["splitby"] == "speciality")
                {
                    setCaption("speciality", Filters["widgetFilters"]["viewquery"], "Speciality");
                }
                else if (Filters["widgetFilters"]["splitby"] == "category")
                {
                    setCaption("category", Filters["widgetFilters"]["viewquery"], "Category");
                }
                else if (Filters["widgetFilters"]["splitby"] == "class")
                {
                    setCaption("class", Filters["widgetFilters"]["viewquery"], "Class");
                }

                if (Filters["widgetFilters"].ContainsKey("products") && Filters["widgetFilters"]["products"] != null && Filters["widgetFilters"]["products"] != "")
                {
                    setCaption("product", Filters["widgetFilters"]["products"], "Products");
                }
                if (Filters["widgetFilters"].ContainsKey("subcategories") && Filters["widgetFilters"]["subcategories"] != null && Filters["widgetFilters"]["subcategories"] != "")
                {
                    setCaption("campaign", Filters["widgetFilters"]["subcategories"], "Campaigns");
                }

            }
                else if (Filters["widgetFilters"].ContainsKey("viewby") && Filters["widgetFilters"]["viewby"] != null && Filters["widgetFilters"]["viewby"] != "")
            {
                
                if (Filters["widgetFilters"]["viewby"] == "product")
                {
                    setCaption("product", Filters["widgetFilters"]["viewquery"], "Product");
                }
                else if (Filters["widgetFilters"]["viewby"] == "campaign")
                {
                    setCaption("campaign", Filters["widgetFilters"]["viewquery"], "Campaign");
                } 
            }
        }

        public string LikeQueryForDCRProducts(string prodcuts)
        {
            string query = "(";
            string[] values = prodcuts.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                query += " CHARINDEX('#' + '"+ value.Trim('\'') + "' + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' ) > 0 OR";
            }
            query = query.TrimEnd('R');
            query = query.TrimEnd('O');
            query += ")";
            return query;
        }
    }
}
