using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi
{

    public class InputOutputDrillDown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public bool IsExport = false;
        public InputOutputDrillDown()
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
                {"ProductName", new DrillDownColumn {Label = "Product Name", Select = "mas_product_detail.Product_Detail_Name as ProductName"}},
                {"ProductQty", new DrillDownColumn {Label = "Product Quantity", Select = "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS INT) as ProductQty"}},
               {"ProductValue", new DrillDownColumn {Label = "Product Value", Select = ""}}, //Select Query will be added in after init because of divisioncode
            };

            VisibleColumns = new List<string>
            {
                "SalesforceName",
                "SalesforceHQ",
                //"DoctorName",
                //"DoctorSpeciality",
                "ActivityDate"
            };
        }
        public override void AfterInit()
        {
            setDates();
            FileName = "InputOutput";
            PreferenceName = "InputOutputDrillDown";
            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {

                Columns["ProductName"].Select = "Mas_gift.Gift_Name as ProductName";
                Columns["ProductQty"].Select = "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS INT) as ProductQty";
                Columns["ProductValue"].Select = "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS INT) *Mas_gift.Gift_value) as ProductValue";
            }
            else
            {
                Columns["ProductValue"].Select = "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS float) * (SELECT TOP 1 cast(Mas_Product_State_Rates.sample_price as float) FROM Mas_Product_State_Rates WHERE Mas_Product_State_Rates.division_code = '" + DivisionCode + "' AND Mas_Product_State_Rates.State_code = Mas_Salesforce.State_code AND Mas_Product_State_Rates.product_detail_code = mas_product_detail.Product_Detail_code AND Mas_Product_State_Rates.Effective_From_Date <= CAST(DCRMain_Trans.Activity_Date AS DATE) ORDER BY Mas_Product_State_Rates.Effective_From_Date DESC)) as ProductValue";

            }
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

        public string getRecursiveHierarchy()
        {
            return "SELECT " +
                "Sf_Code AS firstLevel," +
                "Sf_Code," +
                "Sf_Name," +
                "Sf_HQ," +
                "sf_emp_id," +
                "sf_joining_date," +
                "State_code," +
                "sf_desgn," +
                "Reporting_To_SF," +
                "0 AS Level " +
                "FROM Mas_Salesforce " +
                "WHERE  "+
                //"Reporting_To_SF = '" + Filters["dashboardFilters"]["sfcode"] +"'   AND "+
                "CHARINDEX(',' + '" + DivisionCode + "' + ',',',' + Division_Code + ',' ) > 0  AND Sf_HQ ='" + Filters["widgetFilters"]["viewquery"] +"' " +
                "UNION ALL " +
                "SELECT  R.firstLevel AS firstLevel," +
                "M.Sf_Code," +
                "M.Sf_Name," +
                "M.Sf_HQ," +
                "M.sf_emp_id," +
                "M.sf_joining_date," +
                "M.State_code," +
                "M.sf_desgn," +
                "M.Reporting_To_SF," +
                "R.Level + 1 " +
                "FROM  Mas_Salesforce M  INNER JOIN RecursiveHierarchy R ON M.Reporting_To_SF = R.Sf_Code";
        }
        public string getHqQuery()
        {
            var sqlQry = "WITH RecursiveHierarchy AS (" + getRecursiveHierarchy() + ")," +
                "SampleProducts AS (" +
                "SELECT " +
                 GetSelects() +
                " FROM  DCRMain_Trans  " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno " +
                "INNER JOIN RecursiveHierarchy as Mas_Salesforce ON Mas_Salesforce.Sf_Code=DCRMain_Trans.Sf_Code  ";
                if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
                {
                    sqlQry += "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                    "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) ";
                }
                else
                {
                    sqlQry += "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s  " +
                    "LEFT JOIN mas_product_detail ON s.Item <> '' AND mas_product_detail.Product_code_SlNo = CAST(      SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1      ) AS INT  ) ";
                }
                if (VisibleColumns.Contains("DoctorName") || VisibleColumns.Contains("DoctorSpeciality") || VisibleColumns.Contains("DoctorCategory") || VisibleColumns.Contains("DoctorClass") || VisibleColumns.Contains("DoctorTerritory"))
                {
                    sqlQry += " INNER JOIN Mas_ListedDr ON DCRDetail_Lst_Trans.Trans_Detail_Info_type=1 AND Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code ";
                }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            sqlQry += "WHERE  DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'  AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + "  AND Item <> '' ";

            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Gift_Code<> '' AND DCRDetail_Lst_Trans.Gift_Code is not null ";
            }
            else
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Product_Code<> '' ";
            }

            if (!IsExport)
            {
                sqlQry += " ORDER BY DCRDetail_Lst_Trans.Trans_SlNo  OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY ";
            }

            sqlQry += " ) select * from SampleProducts";
            return sqlQry;
        }

        public string getCampaignQuery()
        {
            var sqlQry =
            "SELECT " +
                GetSelects() +
            " FROM  DCRMain_Trans  " +
            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno ";
            sqlQry += "INNER JOIN Mas_Salesforce as Mas_Salesforce ON Mas_Salesforce.Sf_Code=DCRMain_Trans.Sf_Code  ";
            sqlQry += " INNER JOIN Mas_ListedDr ON DCRDetail_Lst_Trans.Trans_Detail_Info_type=1 AND Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
            "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0   ";
            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) ";
            }
            else
            {
                sqlQry += "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s  " +
                "LEFT JOIN mas_product_detail ON s.Item <> '' AND mas_product_detail.Product_code_SlNo = CAST(      SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1      ) AS INT  ) ";
            }



            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                sqlQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                sqlQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            sqlQry += "WHERE  DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'  AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + "  AND Item <> '' AND  Mas_Doc_SubCategory.Doc_SubCatCode =" + Filters["widgetFilters"]["viewquery"];

            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Gift_Code<> '' AND DCRDetail_Lst_Trans.Gift_Code is not null ";
            }
            else
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Product_Code<> '' ";
            }

            if (!IsExport)
            {
                sqlQry += " ORDER BY DCRDetail_Lst_Trans.Trans_SlNo  OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY ";
            }

            sqlQry += " ";
            return sqlQry;
            
        }

        public string getQuery()
        {
            if (Filters["widgetFilters"].ContainsKey("viewby") && Filters["widgetFilters"]["viewby"] == "hq")
            {
                return getHqQuery();
            }
            else
            {
                return getCampaignQuery();
            }
            
        }
        public override string GetRecords()
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet result;
            var strQry = getQuery();
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
            IsExport = true;
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

        public string GetHqTotalRecords()
        {
            var sqlQry = "WITH RecursiveHierarchy AS (" + getRecursiveHierarchy() + ")," +
                "SampleProducts AS (" +
                "SELECT " +
                "DCRDetail_Lst_Trans.Trans_Detail_Slno " +
                "FROM  DCRMain_Trans  " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno " +
                "INNER JOIN RecursiveHierarchy ON RecursiveHierarchy.Sf_Code=DCRMain_Trans.Sf_Code  ";
                if(Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
                {
                    sqlQry += "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                    "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) ";
                }
                else
                {
                    sqlQry += "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s  " +
                    "LEFT JOIN mas_product_detail ON s.Item <> '' AND mas_product_detail.Product_code_SlNo = CAST(      SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1      ) AS INT  ) ";
                }

            sqlQry += "WHERE  DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'  AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND Item <> '' ";
            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Gift_Code<> '' AND DCRDetail_Lst_Trans.Gift_Code is not null ";
            }
            else
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Product_Code<> '' ";
            }
            sqlQry +=")select count(Trans_Detail_Slno) as totalRecords from SampleProducts";
            return sqlQry;
        }
        public string GetCampaignTotalRecords()
        {
            var sqlQry =
                "SELECT " +
                "COUNT(DCRDetail_Lst_Trans.Trans_Detail_Slno) as totalRecords " +
                "FROM  DCRMain_Trans  " +
                "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno " +
                "INNER JOIN Mas_ListedDr ON DCRDetail_Lst_Trans.Trans_Detail_Info_type=1 AND Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) ";
            }
            else
            {
                sqlQry += "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s  " +
                "LEFT JOIN mas_product_detail ON s.Item <> '' AND mas_product_detail.Product_code_SlNo = CAST(      SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1      ) AS INT  ) ";
            }

            sqlQry += "WHERE  DCRMain_Trans.Sf_Code IN (" + getSfCodes() + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'  AND MONTH(DCRMain_Trans.Activity_Date) >= " + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >= " + StartYear + " AND MONTH(DCRMain_Trans.Activity_Date) <= " + EndMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <= " + EndYear + " AND Item <> '' AND  Mas_Doc_SubCategory.Doc_SubCatCode =" + Filters["widgetFilters"]["viewquery"];
            if (Filters["widgetFilters"]["splitquery"] == "Gift Quantity")
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Gift_Code<> '' AND DCRDetail_Lst_Trans.Gift_Code is not null ";
            }
            else
            {
                sqlQry += " AND DCRDetail_Lst_Trans.Product_Code<> '' ";
            }
            return sqlQry;
        }
        public override int GetTotalRecords()
        {
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;
            var strQry =string.Empty;

            if (Filters["widgetFilters"].ContainsKey("viewby") && Filters["widgetFilters"]["viewby"] == "hq")
            {
                strQry = GetHqTotalRecords();
            }
            else
            {
                strQry = GetCampaignTotalRecords();
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
            if (Filters["widgetFilters"].ContainsKey("viewby") && Filters["widgetFilters"]["viewby"] == "hq")
            {
                Captions["Hq"] = Filters["widgetFilters"]["viewquery"];
            }
            if(Filters["widgetFilters"].ContainsKey("splitquery"))
            {
                Captions["Product Type"] = Filters["widgetFilters"]["splitquery"];
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
