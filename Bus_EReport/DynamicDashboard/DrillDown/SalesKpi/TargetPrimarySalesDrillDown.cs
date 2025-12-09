using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.SalesKpi
{

    public class TargetPrimarySalesDrillDown : DrilldownBase
    {

        string productIds = "";
        string brandIds = "";
        string categoryIds = "";
        string groupIds = "";
        string stateIds = "";
        string hqNames = "";
        public bool IsExport = false;

        private int StartMonth = 0;
        private int StartYear = 0;
        private int EndMonth = 0;
        private int EndYear = 0;
        private string startDate = "";
        private string endDate = "";
        private string laststartDate = "";
        private string lastendDate = "";

        private int LastStartMonth = 0;
        private int LastStartYear = 0;
        private int LastEndMonth = 0;
        private int LastEndYear = 0;

        private string PeriodValue = "";
        private string ComparisionPeriodValue = "";
        private SalesDrillDownHelper _helper;
        string TargetValueColumn = "Target_Price";
        public TargetPrimarySalesDrillDown()
        {
            _helper = new SalesDrillDownHelper();
            SetValueColumns();
            Columns = new Dictionary<string, DrillDownColumn>
            {
                {"ProductName", new DrillDownColumn {Label = "Product Name",Select="Mas_Product_Detail.Product_Detail_Name as ProductName"}},
                {"BrandName", new DrillDownColumn {Label = "Brand Name",Select="Mas_Product_Brand.Product_Brd_Name as BrandName"}},
                {"CategoryName", new DrillDownColumn {Label = "Category Name",Select="Mas_Product_Category.Product_Cat_Name as CategoryName"}},
                {"GroupName", new DrillDownColumn {Label = "Group Name",Select="Mas_Product_Group.Product_Grp_Name as GroupName"}},
                {"StateName", new DrillDownColumn {Label = "State Name", Select = "Mas_State.StateName as StateName"}},
                {"HQName", new DrillDownColumn {Label = "HQ Name",Select="Headquarters.Approved_By as HQName"}},
                {"Month", new DrillDownColumn {Label = "Month", Select = "FORMAT(Cast(Trans_TargetFixation_Product_details.Month+'-01-'+Trans_TargetFixation_Product_details.Year AS DATE), 'MMM yyyy') as Month"}},
                {"TargetQuantity", new DrillDownColumn {Label = "Target Quantity", Select = "Trans_TargetFixation_Product_details.Quantity as TargetQuantity"}},
                {"SalesQuantity", new DrillDownColumn {Label = "Sale Quantity", Select = "0 as SalesQuantity"}},
                {"TargetValue", new DrillDownColumn {Label = "Target Value", Select = "Trans_TargetFixation_Product_details." + TargetValueColumn +" as TargetValue"}},
                {"SalesValue", new DrillDownColumn {Label = "Sale Value", Select = "0 as SalesValue"}},
            };

            VisibleColumns = new List<string>
            {
                "HQName",
                "ProductName",
                "Month",
                "TargetQuantity",
                "SalesQuantity",
                "TargetValue",
                "SalesValue",
            };
        }

        public override void AfterInit()
        {
            FileName = "Target vs Primary Sales";
            PreferenceName = "TargetPrimarySalesDrillDown";

            if (Filters["widgetFilters"].ContainsKey("products"))
            {
                productIds = Filters["widgetFilters"]["products"].ToString();
            }
            if (Filters["widgetFilters"].ContainsKey("brands"))
            {
                brandIds = Filters["widgetFilters"]["brands"].ToString();
            }
            if (Filters["widgetFilters"].ContainsKey("categories"))
            {
                categoryIds = Filters["widgetFilters"]["categories"].ToString();
            }
            if (Filters["widgetFilters"].ContainsKey("groups"))
            {
                groupIds = Filters["widgetFilters"]["groups"].ToString();
            }
            if (Filters["widgetFilters"].ContainsKey("states") && Filters["widgetFilters"]["states"].ToString() != "")
            {
                stateIds = IdsToQuotedIds(Filters["widgetFilters"]["states"].ToString());
            }
            if (Filters["widgetFilters"].ContainsKey("hqs") && Filters["widgetFilters"]["hqs"].ToString() != "")
            {
                hqNames = IdsToQuotedIds(Filters["widgetFilters"]["hqs"].ToString());
            }
            SetDates();
        }

        public void SetDates()
        {
            SetDatesModel values = _helper.SetDates(Filters);
            StartMonth = values.StartMonth;
            StartYear = values.StartYear;
            EndMonth = values.EndMonth;
            EndYear = values.EndYear;
            startDate = values.startDate;
            endDate = values.endDate;
            laststartDate = values.laststartDate;
            lastendDate = values.lastendDate;

            LastStartMonth = values.LastStartMonth;
            LastStartYear = values.LastStartYear;
            LastEndMonth = values.LastEndMonth;
            LastEndYear = values.LastEndYear;

            PeriodValue = values.PeriodValue;
            ComparisionPeriodValue = values.ComparisionPeriodValue;
        }
        public void SetValueColumns()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            try
            {

                result = db_ER.Exec_DataSet("select Targer_Cal_Based from Setup_Others where Division_Code=" + DivisionCode);

                if (result != null && result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    switch (Row[0].ToString())
                    {
                        case "1":
                            TargetValueColumn = "MRP_Price";
                            break;
                        default:
                        case "2":
                            TargetValueColumn = "Target_Price";
                            break;
                        case "3":
                            TargetValueColumn = "Retailor_Price";
                            break;
                        case "4":
                            TargetValueColumn = "NSR_Price";
                            break;
                        case "5":
                            TargetValueColumn = "Distributor_Price";
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string getStockistQuery()
        {
            var strQry = "SELECT " +
                            "mas_stockist.Stockist_Code,mas_stockist.Stockist_Name ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }

            strQry += ",Mas_Salesforce.sf_cat_code ";
            strQry += ",Mas_Salesforce.Approved_By ";
            strQry += "FROM " +
                            "Mas_Salesforce " +
                            "INNER JOIN " +
                            "mas_stockist " +
                            "ON " +
                            "CHARINDEX(',' + Mas_Salesforce.sf_code + ',', ',' + mas_stockist.SF_Code + ',') > 0 " +
                        "WHERE " +
                            "Mas_Salesforce.sf_code IN (" + getSfCodes() + ") " +
                            "AND CHARINDEX('," + DivisionCode + ",', ',' + Mas_Salesforce.division_Code + ',') > 0 ";
            if (hqNames != "")
            {
                strQry += "AND Mas_Salesforce.sf_cat_code IN(" + hqNames + ") ";
            }
            if (stateIds != "")
            {
                strQry += "AND Mas_Salesforce.State_Code IN (" + stateIds + ") ";
            }
            strQry += "GROUP BY " +
                        "mas_stockist.Stockist_Code,mas_stockist.Stockist_Name ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            strQry += ",Mas_Salesforce.sf_cat_code,Mas_Salesforce.Approved_By ";
            return strQry;
        }

        public string GetHeadQuartersQuery()
        {
            string strQry ="SELECT " +
                        "Mas_Salesforce.sf_cat_code ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            if (VisibleColumns.Contains("HQName"))
            {
                strQry += ",Mas_Salesforce.Approved_By ";
            }
            strQry += "FROM " +
                        "Mas_Salesforce " +
                    "WHERE " +
                        "Mas_Salesforce.sf_code IN (" + getSfCodes() + ") " +
                        "AND CHARINDEX('," + DivisionCode + ",', ',' + Mas_Salesforce.division_Code + ',') > 0 ";
            if (hqNames != "")
            {
                strQry += "AND Mas_Salesforce.sf_cat_code IN(" + hqNames + ") ";
            }
            if (stateIds != "")
            {
                strQry += "AND Mas_Salesforce.State_Code IN (" + stateIds + ") ";
            }
            strQry += "GROUP BY " +
                        "Mas_Salesforce.sf_cat_code ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            if (VisibleColumns.Contains("HQName"))
            {
                strQry += ",Mas_Salesforce.Approved_By ";
            }
            return strQry;

        }

        public string GetTargetQuery()
        {
            string strQry = "    SELECT ";

            strQry += GetSelects();

            strQry += " ,Trans_TargetFixation_Product_details.Product_Code as ProductId," +
                "Headquarters.SF_Cat_Code  AS HQCode " +
                    "    FROM Trans_TargetFixation_Product_Head " +
                    "    INNER JOIN Headquarters ON Headquarters.sf_cat_code = Trans_TargetFixation_Product_Head.Sf_HQ_Code ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Headquarters.State_Code ";
            }
            strQry += " INNER JOIN Trans_TargetFixation_Product_details ON Trans_TargetFixation_Product_details.Trans_sl_No = Trans_TargetFixation_Product_Head.Trans_sl_No " +
                    "    INNER JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Detail_Code = Trans_TargetFixation_Product_details.Product_Code ";
            if (VisibleColumns.Contains("GroupName"))
            {
                strQry += " INNER JOIN Mas_Product_Group " +
                    "ON " +
                    "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
            }
            if (VisibleColumns.Contains("BrandName"))
            {
                strQry += "INNER JOIN Mas_Product_Brand " +
                    "ON " +
                    "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
            }
            if (VisibleColumns.Contains("CategoryName"))
            {
                strQry += "INNER JOIN Mas_Product_Category " +
                    "ON " +
                    "Mas_Product_Category.Product_Cat_Code = Mas_Product_Detail.Product_Cat_Code ";
            }

            if (productIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
            }

            if (groupIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Grp_Code IN( " + groupIds + ") ";
            }
            if (brandIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Brd_Code IN( " + brandIds + ") ";
            }
            if (categoryIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
            }
            strQry += "    WHERE Trans_TargetFixation_Product_Head.Division_Code = " + DivisionCode + " " +
                    "    AND Trans_TargetFixation_Product_details.Month >= '" + StartMonth + "' " +
                    "    AND Trans_TargetFixation_Product_details.Year >= '" + StartYear + "' " +
                    "    AND Trans_TargetFixation_Product_details.Month <= '" + EndMonth + "' " +
                    "    AND Trans_TargetFixation_Product_details.Year <= '" + EndYear + "' ";
            return strQry;
        }

        public string GetPriamrySalesQuery()
        {
            string strQry = "SELECT " +
                "Primary_Bill.Product_Code AS ProductId," +
                "Primary_Bill.Sale_Qty AS Sale_Quantity," +
                "Primary_Bill.Sale_Value AS Sale_Value," +
                "FORMAT(Primary_Bill.Inv_Date, 'MMM yyyy') as Month," +
                "Stockists.SF_Cat_Code AS HQ_Code " +
                "FROM Primary_Bill " +
                "INNER JOIN Stockists ON Stockists.Stockist_Code = Primary_Bill.Stockist_Code " +
                "INNER JOIN TargetProducts ON TargetProducts.ProductId = Primary_Bill.Product_Code " +
                "INNER JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Detail_Code = Primary_Bill.Product_Code ";
                if (productIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                }
                if (groupIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Grp_Code IN( " + groupIds + ") ";
                }
                if (brandIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Brd_Code IN( " + brandIds + ") ";
                }
                if (categoryIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
                }
            strQry +="WHERE Primary_Bill.Division_Code = " +DivisionCode+ " "+
                "AND Primary_Bill.Inv_Date >= '"+ startDate + "' " +
                "AND Primary_Bill.Inv_Date <= '"+ endDate + "' ";

            return strQry;
        }
        public string getQuery(bool count =false)
        {

            var strQry = "WITH Stockists AS ("+getStockistQuery()+")";         
            strQry += ",Headquarters AS ("+GetHeadQuartersQuery()+")";         
            strQry += ",Targets AS ("+GetTargetQuery()+")";
            strQry += ",TargetProducts AS (" +
                "SELECT ProductId " +
                "FROM Targets " +
                "GROUP BY ProductId)";
            strQry += ",PrimaryCurrentSales AS ("+GetPriamrySalesQuery()+ ")," +
                "PirmaryBill as ( " +
                "Select " +
                "ProductId,HQ_Code ,Month,sum(Sale_Quantity) as Sale_Quantity,sum(Sale_Value) as Sale_Value " +
                "From PrimaryCurrentSales " +
                "Group By ProductId,HQ_Code ,Month)";
            if (count)
            {
                strQry += "select count(*) AS totalRecords from ( ";
            }
            strQry += "SELECT ";
            if (count)
            {
                strQry += " count(*) AS totalRecords ";
            }
            else
            {
                strQry += "Targets.ProductId AS ProductId";
                if (VisibleColumns.Contains("ProductName"))
                {
                    strQry += ",Targets.ProductName";
                }
                if (VisibleColumns.Contains("BrandName"))
                {
                    strQry += ",Targets.BrandName";
                }
                if (VisibleColumns.Contains("CategoryName"))
                {
                    strQry += ",Targets.CategoryName";
                }
                if (VisibleColumns.Contains("GroupName"))
                {
                    strQry += ",Targets.GroupName";
                }
                if (VisibleColumns.Contains("StateName"))
                {
                    strQry += ",Targets.StateName";
                }
                if (VisibleColumns.Contains("HQName"))
                {
                    strQry += ",Targets.HQName";
                }
                if (VisibleColumns.Contains("Month"))
                {
                    strQry += ",Targets.Month";
                }
                if (VisibleColumns.Contains("TargetQuantity"))
                {
                    strQry += ",SUM(Targets.TargetQuantity) As TargetQuantity";
                }
                if (VisibleColumns.Contains("SalesQuantity"))
                {
                    strQry += ",SUM(PirmaryBill.Sale_Quantity) AS SalesQuantity";
                }
                if (VisibleColumns.Contains("TargetValue"))
                {
                    strQry += ",SUM(Targets.TargetQuantity)*SUM(Targets.TargetValue) AS TargetValue";
                }
                if (VisibleColumns.Contains("SalesValue"))
                {
                    strQry += ",SUM(PirmaryBill.Sale_Value) AS SalesValue";
                }
            }
            strQry += " FROM Targets " +
                "LEFT JOIN PirmaryBill ON Targets.ProductId = PirmaryBill.ProductId AND PirmaryBill.HQ_Code=Targets.HQCode AND PirmaryBill.Month=Targets.Month ";

                strQry += "Group by  " +
                "Targets.ProductId";
                if (VisibleColumns.Contains("ProductName"))
                {
                    strQry += ",Targets.ProductName";
                }
                if (VisibleColumns.Contains("BrandName"))
                {
                    strQry += ",Targets.BrandName";
                }
                if (VisibleColumns.Contains("CategoryName"))
                {
                    strQry += ",Targets.CategoryName";
                }
                if (VisibleColumns.Contains("GroupName"))
                {
                    strQry += ",Targets.GroupName";
                }
                if (VisibleColumns.Contains("StateName"))
                {
                    strQry += ",Targets.StateName";
                }
                if (VisibleColumns.Contains("HQName"))
                {
                    strQry += ",Targets.HQName";
                }
                if (VisibleColumns.Contains("Month"))
                {
                    strQry += ",Targets.Month";
                }

            if (!IsExport && !count)
            {
                strQry += " ORDER BY Targets.ProductId  OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY ";
            }
            if (count)
            {
                strQry += ") AS Subquery ";
            }
            return strQry;
        }
        public override string GetRecords()
        {
            
            DB_EReporting db_ER = new DB_EReporting();
            DataSet result =null;
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
            DataSet result =null;
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
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();
            var strQry = getQuery(true);
            DataSet result;
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
            SetSalesCaptions(Filters, PeriodValue, ComparisionPeriodValue);
        }
        public string IdsToQuotedIds(string Ids)
        {
            string[] elements = Ids.Split(',');

            // Add single quotes around each element
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = "'" + elements[i] + "'";
            }

            // Join the elements back together with commas
            return string.Join(",", elements);
        }
    }
}
