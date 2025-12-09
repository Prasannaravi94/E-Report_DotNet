using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;

namespace Bus_EReport.DynamicDashboard.KPIs
{

    public class SalesKpiModel : KpiModal
    {
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

        string productIds = "";
        string brandIds = "";
        string categoryIds = "";
        string groupIds = "";
        string stateIds = "";
        string hqNames = "";

        string TargetValueColumn = "Target_Price";
        string SecsalesValueColumn = "Distributor_Price";

        public WidgetDataOutputModal getData()
        {

            SetDates();
            SetValueColumns();
            if (WidgetDataInput.WidgetFiters.ContainsKey("products"))
            {
                productIds = WidgetDataInput.WidgetFiters["products"].ToString();
            }
            if (WidgetDataInput.WidgetFiters.ContainsKey("brands"))
            {
                brandIds = WidgetDataInput.WidgetFiters["brands"].ToString();
            }
            if (WidgetDataInput.WidgetFiters.ContainsKey("categories"))
            {
                categoryIds = WidgetDataInput.WidgetFiters["categories"].ToString();
            }
            if (WidgetDataInput.WidgetFiters.ContainsKey("groups"))
            {
                groupIds = WidgetDataInput.WidgetFiters["groups"].ToString();
            }
            if (WidgetDataInput.WidgetFiters.ContainsKey("states") && WidgetDataInput.WidgetFiters["states"].ToString() !="")
            {
                stateIds = IdsToQuotedIds(WidgetDataInput.WidgetFiters["states"].ToString());
            }
            if (WidgetDataInput.WidgetFiters.ContainsKey("hqs") && WidgetDataInput.WidgetFiters["hqs"].ToString() != "")
            {
                hqNames = IdsToQuotedIds(WidgetDataInput.WidgetFiters["hqs"].ToString());
            }

            if (WidgetDataInput.ViewBy == "product")
            {
                ViewByTitle = "Products";

            }
            else if (WidgetDataInput.ViewBy == "group")
            {
                ViewByTitle = "Groups";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                ViewByTitle = "Brands";
            }
            else if (WidgetDataInput.ViewBy == "category")
            {
                ViewByTitle = "Categories";
            }
            else if (WidgetDataInput.ViewBy == "group_wise_product")
            {
                ViewByTitle = "Products";
            }
            else if (WidgetDataInput.ViewBy == "brand_wise_product")
            {
                ViewByTitle = "Products";            
            }
            else if (WidgetDataInput.ViewBy == "category_wise_product")
            {
                ViewByTitle = "Products";
            }
            else if (WidgetDataInput.ViewBy == "hq_wise_brand")
            {
                ViewByTitle = "Brands";
            }
            else if (WidgetDataInput.ViewBy == "hq_wise_group")
            {
                ViewByTitle = "Groups";
            }
            else if (WidgetDataInput.ViewBy == "state_wise_brand")
            {
                ViewByTitle = "Brands";
            }
            else if (WidgetDataInput.ViewBy == "state_wise_group")
            {
                ViewByTitle = "Groups";
            }

            DataSet result = null;
            WidgetDataOutputModal Output = new WidgetDataOutputModal();
            if (WidgetDataInput.MeasureBy == "target_primary_sales")
            {
                MeasureByTitle = "Target Vs Primary Sales";
                SeriresTitles.Add("Primary Sales");
                result = GetTargetVsSales();
            }else if (WidgetDataInput.MeasureBy == "target_secondary_sales")
            {
                MeasureByTitle = "Target Vs Secondary Sales";
                SeriresTitles.Add("Secondary Sales");
                result = GetTargetVsSales();
            }
            else if (WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                MeasureByTitle = "Target Vs P/S Sales";
                SeriresTitles.Add("Primary/Secondary Sales");
                result = GetTargetVsSales();
            }
            else if (WidgetDataInput.MeasureBy == "primary_sales")
            {
                MeasureByTitle = "Primary Sales";
                SeriresTitles.Add("Primary Sales");
                result = GetTargetVsSales();
            }
            else if (WidgetDataInput.MeasureBy == "secondary_sales")
            {
                MeasureByTitle = "Seconadary Sales";
                SeriresTitles.Add("Secondary Sales");
                result = GetTargetVsSales();
            }
            else if (WidgetDataInput.MeasureBy == "primary_secondary_sales")
            {
                MeasureByTitle = "Primary/Seconadary Sales";
                SeriresTitles.Add("Priamary/Secondary Sales");
                result = GetTargetVsSales();
            }
            string FilterMeasureBy = "value";
            if (WidgetDataInput.WidgetFiters.ContainsKey("measure_by"))
            {
                FilterMeasureBy = WidgetDataInput.WidgetFiters["measure_by"];
            }
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Target", "Sales", "Growth" };
                Output.Series = new List<WidgetSeriesModal> {
                        new WidgetSeriesModal{
                            name="Target",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Growth",
                            data =new List<int>()
                        }
                    };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());
                    if (FilterMeasureBy == "quantity")
                    {
                        if (Row["Target_Quantity"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Target_Quantity"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["Quantity"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["Quantity"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }

                        if (Row["QuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["QuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }
                    }
                    else
                    {
                        if (Row["Target"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Target"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["Sales"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["Sales"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }

                        if (Row["SaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["SaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }
                    }
                }
            }
            else if (WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Target", "Primary Sales", "Secondary Sales", "Primary Growth", "Secondary Growth" };
                Output.Series = new List<WidgetSeriesModal> {
                        new WidgetSeriesModal{
                            name="Target",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Primary Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Secondary Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Primary Growth",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Secondary Growth",
                            data =new List<int>()
                        }
                    };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());
                    if (FilterMeasureBy == "quantity")
                    {
                        if (Row["Target_Quantity"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Target_Quantity"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["PrimaryQuantity"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["PrimaryQuantity"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                        if (Row["SecondaryQuantity"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["SecondaryQuantity"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }
                        if (Row["PrimaryQuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[3].data.Add(Convert.ToInt32(Row["PrimaryQuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[3].data.Add(0);
                        }



                        if (Row["SecondaryQuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[4].data.Add(Convert.ToInt32(Row["SecondaryQuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[4].data.Add(0);
                        }
                    }
                    else
                    {
                        if (Row["Target"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Target"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["PrimarySales"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["PrimarySales"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                        if (Row["SecondarySales"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["SecondarySales"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }
                        if (Row["PrimarySaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[3].data.Add(Convert.ToInt32(Row["PrimarySaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[3].data.Add(0);
                        }



                        if (Row["SecondarySaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[4].data.Add(Convert.ToInt32(Row["SecondarySaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[4].data.Add(0);
                        }
                    }
                }
            }
            else if (WidgetDataInput.MeasureBy == "primary_sales" || WidgetDataInput.MeasureBy == "secondary_sales")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Sales", "Growth" };
                Output.Series = new List<WidgetSeriesModal> {
                        new WidgetSeriesModal{
                            name="Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Growth",
                            data =new List<int>()
                        }
                    };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());
                    if (FilterMeasureBy == "quantity")
                    {
                        if (Row["Quantity"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Quantity"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["QuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["QuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                    }
                    else
                    {
                        if (Row["Sales"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["Sales"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }

                        if (Row["SaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["SaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                    }

                }
            }
            else if (WidgetDataInput.MeasureBy == "primary_secondary_sales")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> {"Primary Sales", "Secondary Sales", "Primary Growth", "Secondary Growth" };
                Output.Series = new List<WidgetSeriesModal> {
                        new WidgetSeriesModal{
                            name="Primary Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Secondary Sales",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Primary Growth",
                            data =new List<int>()
                        },
                        new WidgetSeriesModal{
                            name="Secondary Growth",
                            data =new List<int>()
                        }
                    };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());
                    if (FilterMeasureBy == "quantity")
                    {
                        if (Row["PrimaryQuantity"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["PrimaryQuantity"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }
                        if (Row["SecondaryQuantity"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["SecondaryQuantity"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                        if (Row["PrimaryQuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["PrimaryQuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }



                        if (Row["SecondaryQuantityGrowth"] != DBNull.Value)
                        {
                            Output.Series[3].data.Add(Convert.ToInt32(Row["SecondaryQuantityGrowth"]));
                        }
                        else
                        {
                            Output.Series[3].data.Add(0);
                        }
                    }
                    else
                    {
                        if (Row["PrimarySales"] != DBNull.Value)
                        {
                            Output.Series[0].data.Add(Convert.ToInt32(Row["PrimarySales"]));
                        }
                        else
                        {
                            Output.Series[0].data.Add(0);
                        }
                        if (Row["SecondarySales"] != DBNull.Value)
                        {
                            Output.Series[1].data.Add(Convert.ToInt32(Row["SecondarySales"]));
                        }
                        else
                        {
                            Output.Series[1].data.Add(0);
                        }
                        if (Row["PrimarySaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[2].data.Add(Convert.ToInt32(Row["PrimarySaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[2].data.Add(0);
                        }



                        if (Row["SecondarySaleGrowth"] != DBNull.Value)
                        {
                            Output.Series[3].data.Add(Convert.ToInt32(Row["SecondarySaleGrowth"]));
                        }
                        else
                        {
                            Output.Series[3].data.Add(0);
                        }
                    }

                }
            }
            Output.MeasureByTitle = MeasureByTitle;
            Output.ViewByTitle = ViewByTitle;
            return Output;
        }

        public void SetDates()
        {
            if (WidgetDataInput.WidgetFiters.ContainsKey("periodtype"))
            {
                if (WidgetDataInput.WidgetFiters["periodtype"] == "MTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["period"].Split('-');
                    StartMonth = Convert.ToInt32(monthStart[0]);
                    StartYear = Convert.ToInt32(monthStart[1]);
                    EndMonth = Convert.ToInt32(monthStart[0]);
                    EndYear = Convert.ToInt32(monthStart[1]);
                }
                else if (WidgetDataInput.WidgetFiters["periodtype"] == "QTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["period"].Split('-');
                    switch (Convert.ToInt32(monthStart[0]))
                    {
                        case 1:
                            StartMonth = 1;
                            EndMonth = 3;
                            break;
                        case 2:
                            StartMonth = 4;
                            EndMonth = 6;
                            break;
                        case 3:
                            StartMonth = 7;
                            EndMonth = 9;
                            break;
                        case 4:
                            StartMonth = 10;
                            EndMonth = 12;
                            break;
                        default:
                            break;

                    }

                    StartYear = Convert.ToInt32(monthStart[1]);

                    EndYear = Convert.ToInt32(monthStart[1]);
                }
                else if (WidgetDataInput.WidgetFiters["periodtype"] == "YTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["period"].Split('-');
                    StartMonth = 1;
                    StartYear = Convert.ToInt32(monthStart[0]);
                    EndMonth = 12;
                    EndYear = Convert.ToInt32(monthStart[0]);
                }

                startDate = StartYear + "-" + StartMonth + "-01";
                endDate = EndYear + "-" + EndMonth + "-" + DateTime.DaysInMonth(EndYear, EndMonth);
                laststartDate = (StartYear-1) + "-" + StartMonth + "-01";
                lastendDate = (EndYear-1) + "-" + EndMonth + "-" + DateTime.DaysInMonth(EndYear-1, EndMonth);

                LastStartMonth = StartMonth;
                LastStartYear = StartYear - 1;
                LastEndMonth = EndMonth;
                LastEndYear = EndYear - 1;
                SetComparisonDate();
            }
        }
        public void SetComparisonDate()
        {
            if (WidgetDataInput.WidgetFiters.ContainsKey("comparison_period"))
            {
                if (WidgetDataInput.WidgetFiters["periodtype"] == "MTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["comparison_period"].Split('-');
                    LastStartMonth = Convert.ToInt32(monthStart[0]);
                    LastStartYear = Convert.ToInt32(monthStart[1]);
                    LastEndMonth = Convert.ToInt32(monthStart[0]);
                    LastEndYear = Convert.ToInt32(monthStart[1]);
                }
                else if (WidgetDataInput.WidgetFiters["periodtype"] == "QTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["comparison_period"].Split('-');
                    switch (Convert.ToInt32(monthStart[0]))
                    {
                        case 1:
                            LastStartMonth = 1;
                            LastEndMonth = 3;
                            break;
                        case 2:
                            LastStartMonth = 4;
                            LastEndMonth = 6;
                            break;
                        case 3:
                            LastStartMonth = 7;
                            LastEndMonth = 9;
                            break;
                        case 4:
                            LastStartMonth = 10;
                            LastEndMonth = 12;
                            break;
                        default:
                            break;

                    }

                    LastStartYear = Convert.ToInt32(monthStart[1]);

                    LastEndYear = Convert.ToInt32(monthStart[1]);
                }
                else if (WidgetDataInput.WidgetFiters["periodtype"] == "YTD")
                {
                    string[] monthStart = WidgetDataInput.WidgetFiters["comparison_period"].Split('-');
                    StartMonth = 1;
                    LastStartYear = Convert.ToInt32(monthStart[0]);
                    LastEndMonth = 12;
                    LastEndYear = Convert.ToInt32(monthStart[0]);
                }

                laststartDate = LastStartYear + "-" + LastStartMonth + "-01";
                lastendDate = LastEndYear + "-" + LastEndMonth + "-" + DateTime.DaysInMonth(LastEndYear, LastEndMonth);

            }
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
        public DataSet GetTargetVsSales()
        {
            string strQry =
                        "WITH Stockists as (" +
                        "SELECT " +
                            "mas_stockist.Stockist_Code ";
                        if (WidgetDataInput.ViewBy == "state")
                        {
                            strQry += ",Mas_Salesforce.State_Code ";
                        }
                        else if (WidgetDataInput.ViewBy == "hq")
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
                            "Mas_Salesforce.sf_code IN (" + Sfcodes + ") " +
                            "AND CHARINDEX('," + DivisionCode + ",', ',' + Mas_Salesforce.division_Code + ',') > 0 ";
            if (hqNames !="")
            {
                strQry += "AND Mas_Salesforce.sf_cat_code IN(" + hqNames + ") ";
            }
            if (stateIds !="")
            {
                strQry += "AND Mas_Salesforce.State_Code IN (" + stateIds + ") ";
            }
            strQry += "GROUP BY " +
                        "mas_stockist.Stockist_Code ";
            if (WidgetDataInput.ViewBy == "state")
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += ",Mas_Salesforce.sf_cat_code,Mas_Salesforce.Approved_By ";
            }
            strQry += "), Headquarters as (" +
                    "SELECT " +
                        "Mas_Salesforce.sf_cat_code ";
                        if (WidgetDataInput.ViewBy == "state")
                        {
                            strQry += ",Mas_Salesforce.State_Code ";
                        }
                        else if (WidgetDataInput.ViewBy == "hq")
                        {
                            strQry += ",Mas_Salesforce.Approved_By ";
                        }
            strQry += "FROM " +
                        "Mas_Salesforce " +
                    "WHERE " +
                        "Mas_Salesforce.sf_code IN (" + Sfcodes + ") " +
                        "AND CHARINDEX('," + DivisionCode + ",', ',' + Mas_Salesforce.division_Code + ',') > 0 ";
            if (hqNames !="")
            {
                strQry += "AND Mas_Salesforce.sf_cat_code IN(" + hqNames + ") ";
            }
            if (stateIds != "")
            {
                strQry += "AND Mas_Salesforce.State_Code IN (" + stateIds + ") ";
            }
            strQry += "GROUP BY " +
                        "Mas_Salesforce.sf_cat_code ";
            if (WidgetDataInput.ViewBy == "state")
            {
                strQry += ",Mas_Salesforce.State_Code ";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += ",Mas_Salesforce.Approved_By ";
            }
            strQry += "), ";
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales" || WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                strQry += "FilteredTargets AS (" +
                "    SELECT ";

                if (WidgetDataInput.ViewBy == "group")
                {
                    strQry += "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                        "Mas_Product_Group.Product_Grp_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "brand")
                {
                    strQry += "Mas_Product_Brand.Product_Brd_Code AS LabelId, " +
                        "Mas_Product_Brand.Product_Brd_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "product")
                {
                    strQry += "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                         "Mas_Product_Detail.Product_Detail_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "category")
                {
                    strQry += "Mas_Product_Category.Product_Cat_Code AS LabelId, " +
                         "Mas_Product_Category.Product_Cat_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "state")
                {
                    strQry += "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "hq")
                {
                    strQry += "Headquarters.SF_Cat_Code AS LabelId," +
                        "Headquarters.Approved_By AS Label, ";
                }
                strQry += "Trans_TargetFixation_Product_details.Quantity AS Target_Quantity," +
                    "Trans_TargetFixation_Product_details.Quantity * Trans_TargetFixation_Product_details." + TargetValueColumn + " As Target_Value," +
                        "Trans_TargetFixation_Product_details.Product_Code as product_Code " +
                        "    FROM Trans_TargetFixation_Product_Head " +
                        "    INNER JOIN Headquarters ON Headquarters.sf_cat_code = Trans_TargetFixation_Product_Head.Sf_HQ_Code ";
                if (WidgetDataInput.ViewBy == "state")
                {
                    strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Headquarters.State_Code ";
                }
                strQry += " INNER JOIN Trans_TargetFixation_Product_details ON Trans_TargetFixation_Product_details.Trans_sl_No = Trans_TargetFixation_Product_Head.Trans_sl_No " +
                        "    INNER JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Detail_Code = Trans_TargetFixation_Product_details.Product_Code ";
                if (WidgetDataInput.ViewBy == "group")
                {
                    strQry += " INNER JOIN Mas_Product_Group " +
                        "ON " +
                        "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
                }
                else if (WidgetDataInput.ViewBy == "brand")
                {
                    strQry += "INNER JOIN Mas_Product_Brand " +
                        "ON " +
                        "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
                }
                else if (WidgetDataInput.ViewBy == "category")
                {
                    strQry += "INNER JOIN Mas_Product_Category " +
                        "ON " +
                        "Mas_Product_Category.Product_Cat_Code = Mas_Product_Detail.Product_Cat_Code ";
                }

                if (productIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                }
                
                if (groupIds !="")
                {
                    strQry += "AND Mas_Product_Detail.Product_Grp_Code IN( " + groupIds + ") ";
                }
                if (brandIds !="")
                {
                    strQry += "AND Mas_Product_Detail.Product_Brd_Code IN( " + brandIds + ") ";
                }
                if (categoryIds !="")
                {
                    strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
                }
                strQry += "    WHERE Trans_TargetFixation_Product_Head.Division_Code = " + DivisionCode + " " +
                        "    AND Trans_TargetFixation_Product_details.Month >= '" + StartMonth + "' " +
                        "    AND Trans_TargetFixation_Product_details.Year >= '" + StartYear + "' " +
                        "    AND Trans_TargetFixation_Product_details.Month <= '" + EndMonth + "' " +
                        "    AND Trans_TargetFixation_Product_details.Year <= '" + EndYear + "' ";
                strQry += "), Targets AS (" +
                    "SELECT " +
                    "LabelId," +
                    "Label," +
                    "SUM(Target_Quantity) AS Target_Quantity,"+
                    "SUM(Target_Value) AS Target_Value " +
                    "FROM FilteredTargets " +
                    "GROUP BY LabelId, Label)," +
                    "TargetProducts as(" +
                    "SELECT " +
                    "product_Code " +
                    "FROM FilteredTargets " +
                    "GROUP BY product_Code), ";

            }

            if (WidgetDataInput.MeasureBy == "target_primary_sales")
            {
            
                strQry += GetPrimarySalesQuery();
                strQry += "SELECT " +
                        "COALESCE(PrimaryCurrentSales.LabelId, PrimaryPreviousSales.LabelId, Targets.LabelId) AS LabelId, " +
                        "COALESCE(PrimaryCurrentSales.Label, PrimaryPreviousSales.Label, Targets.Label) AS Label, " +
                        "Targets.Target_Value AS Target, " +
                        "Targets.Target_Quantity AS Target_Quantity, " +
                        "PrimaryCurrentSales.Sale_Value AS Sales, " +
                        "Round((COALESCE((PrimaryCurrentSales.Sale_Value - PrimaryPreviousSales.Sale_Value) / NULLIF(PrimaryPreviousSales.Sale_Value, 0), 0) * 100),2) AS SaleGrowth, " +
                        "PrimaryCurrentSales.Sale_Quantity AS Quantity, " +
                        "Round((COALESCE((PrimaryCurrentSales.Sale_Quantity - PrimaryPreviousSales.Sale_Quantity) / NULLIF(PrimaryPreviousSales.Sale_Quantity, 0), 0) * 100),2) AS QuantityGrowth " +
                    "FROM " +
                        "Targets " +
                    "FULL JOIN " +
                        "PrimaryCurrentSales ON Targets.LabelId = PrimaryCurrentSales.LabelId " +
                    "FULL JOIN " +
                        "PrimaryPreviousSales ON Targets.LabelId = PrimaryPreviousSales.LabelId ";
            }
            else if (WidgetDataInput.MeasureBy == "target_secondary_sales")
            {
                strQry += GetSecondarySalesQuery();
                strQry += "SELECT " +
                        "COALESCE(SecondaryCurrentSales.LabelId, SecondaryPreviousSales.LabelId, Targets.LabelId) AS LabelId, " +
                        "COALESCE(SecondaryCurrentSales.Label, SecondaryPreviousSales.Label, Targets.Label) AS Label, " +
                        "Targets.Target_Value AS Target, " +
                        "Targets.Target_Quantity AS Target_Quantity, " +
                        "SecondaryCurrentSales.Sale_Value AS Sales, " +
                        "Round((COALESCE((SecondaryCurrentSales.Sale_Value - SecondaryPreviousSales.Sale_Value) / NULLIF(SecondaryPreviousSales.Sale_Value, 0), 0) * 100),2) AS SaleGrowth, " +
                        "SecondaryCurrentSales.Sale_Quantity AS Quantity, " +
                        "Round((COALESCE((SecondaryCurrentSales.Sale_Quantity - SecondaryPreviousSales.Sale_Quantity) / NULLIF(SecondaryPreviousSales.Sale_Quantity, 0), 0) * 100),2) AS QuantityGrowth " +
                    "FROM " +
                        "Targets " +
                    "FULL JOIN " +
                        "SecondaryCurrentSales ON Targets.LabelId = SecondaryCurrentSales.LabelId " +
                    "FULL JOIN " +
                        "SecondaryPreviousSales ON Targets.LabelId = SecondaryPreviousSales.LabelId ";
            }
            else if (WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                strQry += GetPrimarySalesQuery();
                strQry += ",";
                strQry += GetSecondarySalesQuery();
                strQry +=
                ",PrimaryFinal as ( " +
                "    SELECT " +
                "        COALESCE(PrimaryCurrentSales.LabelId, PrimaryPreviousSales.LabelId) AS LabelId, " +
                "        COALESCE(PrimaryCurrentSales.Label, PrimaryPreviousSales.Label) AS Label, " +
                "        PrimaryCurrentSales.Sale_Value AS PrimarySales, " +
                "        Round((COALESCE((PrimaryCurrentSales.Sale_Value - PrimaryPreviousSales.Sale_Value) / NULLIF(PrimaryPreviousSales.Sale_Value, 0), 0) * 100), 2) AS PrimarySaleGrowth, " +
                 "        PrimaryCurrentSales.Sale_Quantity AS PrimaryQuantity, " +
                "        Round((COALESCE((PrimaryCurrentSales.Sale_Quantity - PrimaryPreviousSales.Sale_Quantity) / NULLIF(PrimaryPreviousSales.Sale_Quantity, 0), 0) * 100), 2) AS PrimaryQuantityGrowth " +
                "    FROM " +
                "        PrimaryCurrentSales " +
                "    LEFT JOIN " +
                "        PrimaryPreviousSales ON PrimaryCurrentSales.LabelId = PrimaryPreviousSales.LabelId " +
                ") " +
                ",SecondaryFinal as ( " +
                "    SELECT " +
                "        COALESCE(SecondaryCurrentSales.LabelId, SecondaryPreviousSales.LabelId) AS LabelId, " +
                "        COALESCE(SecondaryCurrentSales.Label, SecondaryPreviousSales.Label) AS Label, " +
                "        SecondaryCurrentSales.Sale_Value AS SecondarySales, " +
                "        Round((COALESCE((SecondaryCurrentSales.Sale_Value - SecondaryPreviousSales.Sale_Value) / NULLIF(SecondaryPreviousSales.Sale_Value, 0), 0) * 100), 2) AS SecondarySaleGrowth, " +
                "        SecondaryCurrentSales.Sale_Quantity AS SecondaryQuantity, " +
                "        Round((COALESCE((SecondaryCurrentSales.Sale_Quantity - SecondaryPreviousSales.Sale_Quantity) / NULLIF(SecondaryPreviousSales.Sale_Quantity, 0), 0) * 100), 2) AS SecondaryQuantityGrowth " +
                "    FROM " +
                "        SecondaryCurrentSales " +
                "    LEFT JOIN " +
                "        SecondaryPreviousSales ON SecondaryCurrentSales.LabelId = SecondaryPreviousSales.LabelId " +
                ") " +
                "SELECT " +
                "    COALESCE(PrimaryFinal.LabelId, SecondaryFinal.LabelId, Targets.LabelId) AS LabelId, " +
                "    COALESCE(PrimaryFinal.Label, SecondaryFinal.Label, Targets.Label) AS Label, " +
                "    Targets.Target_Value AS Target, " +
                "    Targets.Target_Quantity AS Target_Quantity, " +
                "    PrimarySales, " +
                "    SecondarySales, " +
                "    PrimarySaleGrowth, " +
                "    SecondarySaleGrowth, " +
                "    PrimaryQuantity, " +
                "    SecondaryQuantity, " +
                "    PrimaryQuantityGrowth, " +
                "    SecondaryQuantityGrowth " +
                "FROM " +
                "    Targets " +
                "    FULL JOIN " +
                "    PrimaryFinal ON PrimaryFinal.LabelId = Targets.LabelId " +
                "    FULL JOIN " +
                "    SecondaryFinal ON SecondaryFinal.LabelId = Targets.LabelId ";

            }
            else if (WidgetDataInput.MeasureBy == "primary_sales")
            {
                strQry += GetPrimarySalesQuery();
                strQry += "SELECT " +
                        "COALESCE(PrimaryCurrentSales.LabelId, PrimaryPreviousSales.LabelId) AS LabelId, " +
                        "COALESCE(PrimaryCurrentSales.Label, PrimaryPreviousSales.Label) AS Label, " +
                        "PrimaryCurrentSales.Sale_Value AS Sales, " +
                        "Round((COALESCE((PrimaryCurrentSales.Sale_Value - PrimaryPreviousSales.Sale_Value) / NULLIF(PrimaryPreviousSales.Sale_Value, 0), 0) * 100),2) AS SaleGrowth, " +
                        "PrimaryCurrentSales.Sale_Quantity AS Quantity, " +
                        "Round((COALESCE((PrimaryCurrentSales.Sale_Quantity - PrimaryPreviousSales.Sale_Quantity) / NULLIF(PrimaryPreviousSales.Sale_Quantity, 0), 0) * 100),2) AS QuantityGrowth " +
                    "FROM " +
                        "PrimaryCurrentSales " +
                    "FULL JOIN " +
                        "PrimaryPreviousSales ON PrimaryPreviousSales.LabelId = PrimaryCurrentSales.LabelId ";
            }
            else if (WidgetDataInput.MeasureBy == "secondary_sales")
            {
                strQry += GetSecondarySalesQuery();
                strQry += "SELECT " +
                            "COALESCE(SecondaryCurrentSales.LabelId, SecondaryPreviousSales.LabelId) AS LabelId, " +
                            "COALESCE(SecondaryCurrentSales.Label, SecondaryPreviousSales.Label) AS Label, " +
                            "SecondaryCurrentSales.Sale_Value AS Sales, " +
                            "Round((COALESCE((SecondaryCurrentSales.Sale_Value - SecondaryPreviousSales.Sale_Value) / NULLIF(SecondaryPreviousSales.Sale_Value, 0), 0) * 100),2) AS SaleGrowth, " +
                            "SecondaryCurrentSales.Sale_Quantity AS Quantity, " +
                            "Round((COALESCE((SecondaryCurrentSales.Sale_Quantity - SecondaryPreviousSales.Sale_Quantity) / NULLIF(SecondaryPreviousSales.Sale_Quantity, 0), 0) * 100),2) AS QuantityGrowth " +
                        "FROM " +
                            "SecondaryCurrentSales " +
                        "FULL JOIN " +
                            "SecondaryPreviousSales ON SecondaryCurrentSales.LabelId = SecondaryPreviousSales.LabelId ";
            }
            else if (WidgetDataInput.MeasureBy == "primary_secondary_sales")
            {
                strQry += GetPrimarySalesQuery();
                strQry += ",";
                strQry += GetSecondarySalesQuery();
                strQry +=
                ",PrimaryFinal as ( " +
                "    SELECT " +
                "        COALESCE(PrimaryCurrentSales.LabelId, PrimaryPreviousSales.LabelId) AS LabelId, " +
                "        COALESCE(PrimaryCurrentSales.Label, PrimaryPreviousSales.Label) AS Label, " +
                "        PrimaryCurrentSales.Sale_Value AS PrimarySales, " +
                "        Round((COALESCE((PrimaryCurrentSales.Sale_Value - PrimaryPreviousSales.Sale_Value) / NULLIF(PrimaryPreviousSales.Sale_Value, 0), 0) * 100), 2) AS PrimarySaleGrowth, " +
                "        PrimaryCurrentSales.Sale_Quantity AS PrimaryQuantity, " +
                "        Round((COALESCE((PrimaryCurrentSales.Sale_Quantity - PrimaryPreviousSales.Sale_Quantity) / NULLIF(PrimaryPreviousSales.Sale_Quantity, 0), 0) * 100), 2) AS PrimaryQuantityGrowth " +
                "    FROM " +
                "        PrimaryCurrentSales " +
                "    LEFT JOIN " +
                "        PrimaryPreviousSales ON PrimaryCurrentSales.LabelId = PrimaryPreviousSales.LabelId " +
                ") " +
                ",SecondaryFinal as ( " +
                "    SELECT " +
                "        COALESCE(SecondaryCurrentSales.LabelId, SecondaryPreviousSales.LabelId) AS LabelId, " +
                "        COALESCE(SecondaryCurrentSales.Label, SecondaryPreviousSales.Label) AS Label, " +
                "        SecondaryCurrentSales.Sale_Value AS SecondarySales, " +
                "        Round((COALESCE((SecondaryCurrentSales.Sale_Value - SecondaryPreviousSales.Sale_Value) / NULLIF(SecondaryPreviousSales.Sale_Value, 0), 0) * 100), 2) AS SecondarySaleGrowth, " +
                "        SecondaryCurrentSales.Sale_Quantity AS SecondaryQuantity, " +
                "        Round((COALESCE((SecondaryCurrentSales.Sale_Quantity - SecondaryPreviousSales.Sale_Quantity) / NULLIF(SecondaryPreviousSales.Sale_Quantity, 0), 0) * 100), 2) AS SecondaryQuantityGrowth " +
                "    FROM " +
                "        SecondaryCurrentSales " +
                "    LEFT JOIN " +
                "        SecondaryPreviousSales ON SecondaryCurrentSales.LabelId = SecondaryPreviousSales.LabelId " +
                ") " +
                "SELECT " +
                "    COALESCE(PrimaryFinal.LabelId, SecondaryFinal.LabelId) AS LabelId, " +
                "    COALESCE(PrimaryFinal.Label, SecondaryFinal.Label) AS Label, " +
                "    PrimarySales, " +
                "    SecondarySales, " +
                "    PrimarySaleGrowth, " +
                "    SecondarySaleGrowth, " +
                "    PrimaryQuantity, " +
                "    SecondaryQuantity, " +
                "    PrimaryQuantityGrowth, " +
                "    SecondaryQuantityGrowth " +
                "FROM " +
                "    PrimaryFinal " +
                "    FULL JOIN " +
                "    SecondaryFinal ON SecondaryFinal.LabelId = PrimaryFinal.LabelId ";
            }

            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
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
        public String GetPrimarySalesQuery()
        {
            string strQry ="PrimaryCurrentSales as (" +
                        "SELECT ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                    "Mas_Product_Group.Product_Grp_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "Mas_Product_Brand.Product_Brd_Code AS LabelId, " +
                    "Mas_Product_Brand.Product_Brd_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "product")
            {
                strQry += "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                     "Mas_Product_Detail.Product_Detail_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "category")
            {
                strQry += "Mas_Product_Category.Product_Cat_Code AS LabelId, " +
                     "Mas_Product_Category.Product_Cat_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "Mas_State.State_Code AS LabelId," +
                    "Mas_State.StateName AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += "Stockists.SF_Cat_Code AS LabelId," +
                    "Stockists.Approved_By AS Label, ";
            }
            strQry += "SUM(Primary_Bill.Sale_Qty) AS Sale_Quantity," +
                "SUM(Primary_Bill.Sale_Value) AS Sale_Value " +
                "FROM " +
                    "Primary_Bill " +
                    "INNER JOIN " +
                    "Stockists " +
                    "ON " +
                    "Stockists.Stockist_Code = Primary_Bill.Stockist_Code ";

            if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
            }
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales" || WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                strQry += "INNER JOIN " +
                    "TargetProducts " +
                    "ON " +
                    "TargetProducts.Product_Code = Primary_Bill.Product_Code ";
            }
            
            strQry +="INNER JOIN " +
                    "Mas_Product_Detail " +
                    "ON " +
                    "Mas_Product_Detail.Product_Detail_Code = Primary_Bill.Product_Code ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += "INNER JOIN Mas_Product_Group " +
                    "ON " +
                    "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "INNER JOIN Mas_Product_Brand " +
                    "ON " +
                    "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
            }
            else if (WidgetDataInput.ViewBy == "category")
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
                    "Primary_Bill.Division_Code = " + DivisionCode + " " +
                    "AND Primary_Bill.Inv_Date >= '" + startDate + "' " +
                    "AND Primary_Bill.Inv_Date <= '" + endDate + "' " +
                "GROUP BY ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += "Mas_Product_Group.Product_Grp_Code, " +
                    "Mas_Product_Group.Product_Grp_Name ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "Mas_Product_Brand.Product_Brd_Code, " +
                    "Mas_Product_Brand.Product_Brd_Name ";
            }
            else if (WidgetDataInput.ViewBy == "product")
            {
                strQry += "Mas_Product_Detail.Product_Code_SlNo, " +
                    "Mas_Product_Detail.Product_Detail_Name ";
            }
            else if (WidgetDataInput.ViewBy == "category")
            {
                strQry += "Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name ";
            }
            else if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "Mas_State.State_Code ," +
                    "Mas_State.StateName ";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += "Stockists.SF_Cat_Code," +
                    "Stockists.Approved_By ";
            }

            strQry += "),PrimaryPreviousSales as (" +
                    "SELECT ";
                if (WidgetDataInput.ViewBy == "group")
                {
                    strQry += "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                        "Mas_Product_Group.Product_Grp_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "brand")
                {
                    strQry += "Mas_Product_Brand.Product_Brd_Code AS LabelId, " +
                        "Mas_Product_Brand.Product_Brd_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "product")
                {
                    strQry += "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                         "Mas_Product_Detail.Product_Detail_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "category")
                {
                    strQry += "Mas_Product_Category.Product_Cat_Code AS LabelId, " +
                         "Mas_Product_Category.Product_Cat_Name AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "state")
                {
                    strQry += "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label, ";
                }
                else if (WidgetDataInput.ViewBy == "hq")
                {
                    strQry += "Stockists.SF_Cat_Code AS LabelId," +
                        "Stockists.Approved_By AS Label, ";
                }
            strQry += "SUM(Primary_Bill.Sale_Qty) AS Sale_Quantity," +
                "SUM(Primary_Bill.Sale_Value) AS Sale_Value " +
                "FROM " +
                    "Primary_Bill " +
                    "INNER JOIN " +
                    "Stockists " +
                    "ON " +
                    "Stockists.Stockist_Code = Primary_Bill.Stockist_Code ";

            if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
            }
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales" || WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
                {
                    strQry += "INNER JOIN " +
                        "TargetProducts " +
                        "ON " +
                        "TargetProducts.Product_Code = Primary_Bill.Product_Code ";
                }
                strQry += "INNER JOIN " +
                        "Mas_Product_Detail " +
                        "ON " +
                        "Mas_Product_Detail.Product_Detail_Code = Primary_Bill.Product_Code ";
                if (WidgetDataInput.ViewBy == "group")
                {
                    strQry += " INNER JOIN Mas_Product_Group " +
                        "ON " +
                        "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
                }
                else if (WidgetDataInput.ViewBy == "brand")
                {
                    strQry += "INNER JOIN Mas_Product_Brand " +
                        "ON " +
                        "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
                }
                else if (WidgetDataInput.ViewBy == "category")
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
                else if (brandIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Brd_Code IN( " + brandIds + " ) ";
                }
                if (categoryIds != "")
                {
                    strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
                }
                strQry += "WHERE " +
                        "Primary_Bill.Division_Code = " + DivisionCode + " " +
                        "AND Primary_Bill.Inv_Date >= '" + laststartDate + "' " +
                        "AND Primary_Bill.Inv_Date <= '" + lastendDate + "' " +
                    "GROUP BY ";
                if (WidgetDataInput.ViewBy == "group")
                {
                    strQry += "Mas_Product_Group.Product_Grp_Code, " +
                        "Mas_Product_Group.Product_Grp_Name ";
                }
                else if (WidgetDataInput.ViewBy == "brand")
                {
                    strQry += "Mas_Product_Brand.Product_Brd_Code, " +
                        "Mas_Product_Brand.Product_Brd_Name ";
                }
                else if (WidgetDataInput.ViewBy == "product")
                {
                    strQry += "Mas_Product_Detail.Product_Code_SlNo, " +
                        "Mas_Product_Detail.Product_Detail_Name ";
                }
                else if (WidgetDataInput.ViewBy == "category")
                {
                    strQry += "Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name ";
                }
                else if (WidgetDataInput.ViewBy == "state")
                {
                    strQry += "Mas_State.State_Code ," +
                        "Mas_State.StateName ";
                }
                else if (WidgetDataInput.ViewBy == "hq")
                {
                    strQry += "Stockists.SF_Cat_Code," +
                        "Stockists.Approved_By ";
                }
            strQry += ")";
            return strQry;
        }
        public String GetSecondarySalesQuery()
        {
            string strQry ="SecSalesAll as (" +
                    "    SELECT ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                    "Mas_Product_Group.Product_Grp_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "Mas_Product_Brand.Product_Brd_Code AS LabelId, " +
                    "Mas_Product_Brand.Product_Brd_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "product")
            {
                strQry += "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                     "Mas_Product_Detail.Product_Detail_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "category")
            {
                strQry += "Mas_Product_Category.Product_Cat_Code AS LabelId, " +
                     "Mas_Product_Category.Product_Cat_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "Mas_State.State_Code AS LabelId," +
                    "Mas_State.StateName AS Label,";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += "Stockists.SF_Cat_Code AS LabelId," +
                    "Stockists.Approved_By AS Label, ";
            }
            strQry += "Trans_SS_Entry_Detail_value.Sec_Sale_Qty AS SalesQty," +
                "CAST(Trans_SS_Entry_Detail_value.Sec_Sale_Qty AS float) * Trans_SS_Entry_Detail." + SecsalesValueColumn+" AS SalesValue " +
                    "    FROM " +
                    "        Trans_SS_Entry_Head " +
                    "    INNER JOIN " +
                    "        Stockists " +
                    "    ON " +
                    "        Stockists.Stockist_Code = Trans_SS_Entry_Head.Stockiest_Code ";

                    if (WidgetDataInput.ViewBy == "state")
                    {
                        strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
                    }
                    strQry +="    INNER JOIN " +
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
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales" || WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                strQry += "INNER JOIN " +
                    "TargetProducts " +
                    "ON " +
                    "TargetProducts.Product_Code = Trans_SS_Entry_Detail.Product_Detail_Code ";
            }

            strQry += "    INNER JOIN " +
                    "        Mas_Product_Detail " +
                    "    ON " +
                    "        Mas_Product_Detail.Product_Detail_Code = Trans_SS_Entry_Detail.Product_Detail_Code ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += " INNER JOIN Mas_Product_Group " +
                    "ON " +
                    "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "INNER JOIN Mas_Product_Brand " +
                    "ON " +
                    "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
            }
            else if (WidgetDataInput.ViewBy == "category")
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
                    "        AND Trans_SS_Entry_Head.Year <= " + EndYear + " " +
                    "), " +
                    "SecondaryCurrentSales as (" +
                    "    SELECT " +
                    "        LabelId, " +
                    "        Label, " +
                    "        SUM(SalesQty) AS Sale_Quantity," +
                    "        SUM(SalesValue) AS Sale_Value " +
                    "    FROM " +
                    "        SecSalesAll " +
                    "    GROUP BY " +
                    "        LabelId, " +
                    "        Label " +
                    "), " +
                    "PreviousSecSalesAll as (" +
                    "    SELECT ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                    "Mas_Product_Group.Product_Grp_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "Mas_Product_Brand.Product_Brd_Code AS LabelId, " +
                    "Mas_Product_Brand.Product_Brd_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "product")
            {
                strQry += "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                     "Mas_Product_Detail.Product_Detail_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "category")
            {
                strQry += "Mas_Product_Category.Product_Cat_Code AS LabelId, " +
                     "Mas_Product_Category.Product_Cat_Name AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "state")
            {
                strQry += "Mas_State.State_Code AS LabelId ," +
                    "Mas_State.StateName AS Label, ";
            }
            else if (WidgetDataInput.ViewBy == "hq")
            {
                strQry += "Stockists.SF_Cat_Code AS LabelId," +
                    "Stockists.Approved_By AS Label, ";
            }
            strQry += " Trans_SS_Entry_Detail_value.Sec_Sale_Qty AS SalesQty," +
                "CAST(Trans_SS_Entry_Detail_value.Sec_Sale_Qty AS float) * Trans_SS_Entry_Detail." + SecsalesValueColumn+" AS SalesValue " +
                    "    FROM " +
                    "        Trans_SS_Entry_Head " +
                    "    INNER JOIN " +
                    "        Stockists " +
                    "    ON " +
                    "        Stockists.Stockist_Code = Trans_SS_Entry_Head.Stockiest_Code ";

                    if (WidgetDataInput.ViewBy == "state")
                    {
                        strQry += "INNER JOIN Mas_State ON Mas_State.State_Code = Stockists.State_Code ";
                    }
                    strQry +="    INNER JOIN " +
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
                    "        mas_sec_sale_setup.Sec_Sale_Code = Trans_SS_Entry_Detail_value.Sec_Sale_Code AND mas_sec_sale_setup.Sale_Calc = 1 ";
            if (WidgetDataInput.MeasureBy == "target_primary_sales" || WidgetDataInput.MeasureBy == "target_secondary_sales" || WidgetDataInput.MeasureBy == "target_primary_secondary_sales")
            {
                strQry += "INNER JOIN " +
                    "TargetProducts " +
                    "ON " +
                    "TargetProducts.Product_Code = Trans_SS_Entry_Detail.Product_Detail_Code ";
            }
            strQry += "    INNER JOIN " +
                    "        Mas_Product_Detail " +
                    "    ON " +
                    "        Mas_Product_Detail.Product_Detail_Code = Trans_SS_Entry_Detail.Product_Detail_Code ";
            if (WidgetDataInput.ViewBy == "group")
            {
                strQry += " INNER JOIN Mas_Product_Group " +
                    "ON " +
                    "Mas_Product_Group.Product_Grp_Code = Mas_Product_Detail.Product_Grp_Code ";
            }
            else if (WidgetDataInput.ViewBy == "brand")
            {
                strQry += "INNER JOIN Mas_Product_Brand " +
                    "ON " +
                    "Mas_Product_Brand.Product_Brd_Code = Mas_Product_Detail.Product_Brd_Code ";
            }
            else if (WidgetDataInput.ViewBy == "category")
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
                strQry += "AND Mas_Product_Detail.Product_Brd_Code IN ( " + brandIds + ") ";
            }
            if (categoryIds != "")
            {
                strQry += "AND Mas_Product_Detail.Product_Cat_Code IN( " + categoryIds + ") ";
            }

            strQry += "    WHERE " +
                    "        Trans_SS_Entry_Head.Division_Code = " + DivisionCode + " " +
                    "        AND Trans_SS_Entry_Head.Month >= " + LastStartMonth + " " +
                    "        AND Trans_SS_Entry_Head.Year >= " + LastStartYear + " " +
                    "        AND Trans_SS_Entry_Head.Month <= " + LastEndMonth + " " +
                    "        AND Trans_SS_Entry_Head.Year <= " + LastEndYear + " " +
                    "), " +
                    "SecondaryPreviousSales as (" +
                    "    SELECT " +
                    "        LabelId, " +
                    "        Label, " +
                    "        SUM (SalesQty) AS Sale_Quantity," +
                    "        SUM(SalesValue) AS Sale_Value " +
                    "    FROM " +
                    "        PreviousSecSalesAll " +
                    "    GROUP BY " +
                    "        LabelId, " +
                    "        Label " +
                    ") ";
            return strQry;
        }

    }
}
