using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.SalesKpi
{

    public class PrimarySalesDrillDown : DrilldownBase
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
        public PrimarySalesDrillDown()
        {
            _helper = new SalesDrillDownHelper();
            Columns = new Dictionary<string, DrillDownColumn>
            {
                {"Stockist", new DrillDownColumn {Label = "Stockist",Select="Stockists.Stockist_Name as Stockist"}},
                {"ProductName", new DrillDownColumn {Label = "Product Name",Select="Mas_Product_Detail.Product_Detail_Name as ProductName"}},
                {"BrandName", new DrillDownColumn {Label = "Brand Name",Select="Mas_Product_Brand.Product_Brd_Name as BrandName"}},
                {"CategoryName", new DrillDownColumn {Label = "Category Name",Select="Mas_Product_Category.Product_Cat_Name as CategoryName"}},
                {"GroupName", new DrillDownColumn {Label = "Group Name",Select="Mas_Product_Group.Product_Grp_Name as GroupName"}},
                {"StateName", new DrillDownColumn {Label = "State Name", Select = "Mas_State.StateName as StateName"}},
                {"HQName", new DrillDownColumn {Label = "HQ Name",Select="Stockists.Approved_By as HQName"}},
                {"Date", new DrillDownColumn {Label = "Date", Select = "Primary_bill.Inv_Date as Date"}},
                {"Quantity", new DrillDownColumn {Label = "Quantity", Select = "Primary_bill.Sale_Qty as Quantity"}},
                {"Value", new DrillDownColumn {Label = "Value", Select = "Primary_bill.Sale_Value as Value"}},
            };

            VisibleColumns = new List<string>
            {
                "Stockist",
                "ProductName",
                "Date",
                "Quantity",
                "Value"
            };
        }

        public override void AfterInit()
        {
            SetDates();
            FileName = "Primary Sales";
            PreferenceName = "PrimarySalesDrillDown";

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
                stateIds = _helper.IdsToQuotedIds(Filters["widgetFilters"]["states"].ToString());
            }
            if (Filters["widgetFilters"].ContainsKey("hqs") && Filters["widgetFilters"]["hqs"].ToString() != "")
            {
                hqNames = _helper.IdsToQuotedIds(Filters["widgetFilters"]["hqs"].ToString());
            }
            
        }

        public void SetDates()
        {
            SetDatesModel values =_helper.SetDates(Filters);
            StartMonth = values.StartMonth;
            StartYear = values.StartYear;
            EndMonth = values.EndMonth;
            EndYear = values.EndYear;
            startDate = values.startDate;
            endDate = values.endDate;
            laststartDate = values.laststartDate ;
            lastendDate = values.lastendDate;

            LastStartMonth = values.LastStartMonth;
            LastStartYear = values.LastStartYear;
            LastEndMonth = values.LastEndMonth;
            LastEndYear = values.LastEndYear;

            PeriodValue = values.PeriodValue;
            ComparisionPeriodValue = values.ComparisionPeriodValue;
        }
        public String GetPrimarySalesQuery(bool count)
        {
            var strQry = "SELECT ";
               
                if (count)
                {
                strQry += " Count(Primary_Bill.SL_No) as totalRecords";
            }
                else
                {
                    strQry += GetSelects();
                }
                strQry += " FROM " +
                    "Primary_Bill " +
                    "INNER JOIN " +
                    "Stockists " +
                    "ON " +
                    "Stockists.Stockist_Code = Primary_Bill.Stockist_Code ";

            if (VisibleColumns.Contains("StateName"))
            {
                strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
            }

            strQry += "INNER JOIN " +
                    "Mas_Product_Detail " +
                    "ON " +
                    "Mas_Product_Detail.Product_Detail_Code = Primary_Bill.Product_Code ";
            if (VisibleColumns.Contains("GroupName"))
            {
                strQry += "INNER JOIN Mas_Product_Group " +
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
                strQry += "AND Mas_Product_Detail.Product_Grp_Code  IN(" + groupIds + ") ";
            }
            if (brandIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Brd_Code IN( " + brandIds + ") ";
            }
            if (categoryIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
            }
            strQry +=
            "WHERE " +
                    "Primary_Bill.Division_Code = " + DivisionCode + " ";
            strQry += "AND Primary_Bill.Inv_Date >= '" + startDate + "' " +
            "AND Primary_Bill.Inv_Date <= '" + endDate + "' ";
            if (!IsExport  && !count)
            {
                strQry += " ORDER BY Primary_Bill.SL_No  OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY ";
            }
            return strQry;
        }

        public string getStockistQuery()
        {
            var strQry = "SELECT " +
                            "mas_stockist.Stockist_Code,mas_stockist.Stockist_Name ";
            if (VisibleColumns.Contains("StateName"))
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            if (VisibleColumns.Contains("HQName"))
            {
                strQry += ",Mas_Salesforce.sf_cat_code ";
                strQry += ",Mas_Salesforce.Approved_By ";
            }
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
            if (VisibleColumns.Contains("HQName"))
            {
                strQry += ",Mas_Salesforce.sf_cat_code,Mas_Salesforce.Approved_By ";
            }
            return strQry;
        }
        public string getQuery(bool count =false)
        {

            var strQry = "WITH Stockists as ( ";
            strQry += getStockistQuery();
            strQry += " ) ";
            strQry += GetPrimarySalesQuery(count);           
            
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
        
    }
}
