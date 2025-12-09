using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.SalesKpi
{

    public class SecondarySalesDrillDown : DrilldownBase
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

        string SecsalesValueColumn = "Distributor_Price";
        private SalesDrillDownHelper _helper;
        public SecondarySalesDrillDown()
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
                {"Month", new DrillDownColumn {Label = "Month", Select = "FORMAT(CAST(CONCAT(CAST(Month AS VARCHAR(2)), '-01-', CAST(Year AS VARCHAR(4))) AS DATE), 'MMM yyyy') as Month"}},
                {"Quantity", new DrillDownColumn {Label = "Quantity", Select = "Trans_SS_Entry_Detail_value.Sec_Sale_Qty as Quantity"}},
                {"Value", new DrillDownColumn {Label = "Value", Select = "CAST(Trans_SS_Entry_Detail_value.Sec_Sale_Qty AS float) * Trans_SS_Entry_Detail." + SecsalesValueColumn+" as Value"}},
            };

            VisibleColumns = new List<string>
            {
                "Stockist",
                "ProductName",
                "Month",
                "Quantity",
                "Value"
            };
        }

        public override void AfterInit()
        {
            FileName = "Secondary Sales";
            PreferenceName = "SecondarySalesDrillDown";

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
            SetValueColumns();
        }

        public void SetValueColumns()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            try
            {
                result = db_ER.Exec_DataSet("select calc_rate from mas_common_sec_sale_setup where Division_Code=" + DivisionCode);

                if (result != null && result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    switch (Row[0].ToString())
                    {
                        default:
                        case "D":
                            SecsalesValueColumn = "Distributor_Price";
                            break;
                        case "T":
                            SecsalesValueColumn = "Target_Price";
                            break;
                        case "R":
                            SecsalesValueColumn = "Retailor_Price";
                            break;
                        case "N":
                            SecsalesValueColumn = "NSR_Price";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public String GetSecondarySalesQuery(bool count)
        {
            var strQry = "    SELECT ";
            if (count)
            {
                strQry += " Count(Trans_SS_Entry_Detail.SS_Head_Sl_No) as totalRecords";
            }
            else if(!IsExport)
            {
                strQry += " Trans_SS_Entry_Detail.SS_Head_Sl_No , ";
                strQry += GetSelects();
                strQry += " Into #DashboardSecsalesTemp ";
            }
            else
            {
                strQry += GetSelects();
            }

            strQry += " FROM " +
                    "        Trans_SS_Entry_Head " +
                    "    INNER JOIN " +
                    "        Stockists " +
                    "    ON " +
                    "        Stockists.Stockist_Code = Trans_SS_Entry_Head.Stockiest_Code ";

            if (VisibleColumns.Contains("StateName"))
            {
                strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
            }
            strQry += "    INNER JOIN " +
            "        Trans_SS_Entry_Detail " +
            "    ON " +
            "        Trans_SS_Entry_Detail.SS_Head_Sl_No = Trans_SS_Entry_Head.SS_Head_Sl_No " +
            "    INNER JOIN " +
            "        Trans_SS_Entry_Detail_value " +
            "    ON " +
            "        Trans_SS_Entry_Detail_value.SS_Det_Sl_No = Trans_SS_Entry_Detail.SS_Det_Sl_No " +
            "    INNER JOIN " +
            "        mas_sec_sale_setup " +
            "    ON " +
            "        mas_sec_sale_setup.Sec_Sale_Code = Trans_SS_Entry_Detail_value.Sec_Sale_Code ";

            strQry += "    INNER JOIN " +
                    "        Mas_Product_Detail " +
                    "    ON " +
                    "        Mas_Product_Detail.Product_Detail_Code = Trans_SS_Entry_Detail.Product_Detail_Code ";
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
            strQry += "    WHERE " +
                    "        Trans_SS_Entry_Head.Division_Code = " + DivisionCode + " " +
                    "        AND Trans_SS_Entry_Head.Month >= " + StartMonth + " " +
                    "        AND Trans_SS_Entry_Head.Year >= " + StartYear + " " +
                    "        AND Trans_SS_Entry_Head.Month <= " + EndMonth + " " +
                    "        AND Trans_SS_Entry_Head.Year <= " + EndYear + " ";
            if (!IsExport && !count)
            {
                strQry += " select "+GetSelectsKey("SecsalesTemp") + " from #DashboardSecsalesTemp as SecsalesTemp  ORDER BY SecsalesTemp.SS_Head_Sl_No  OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY; Drop table #DashboardSecsalesTemp ";
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
            strQry += GetSecondarySalesQuery(count);           
            
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
