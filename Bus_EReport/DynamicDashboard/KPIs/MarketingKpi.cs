using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Bus_EReport.DynamicDashboard.KPIs
{
    public class MarketingKpiModel : KpiModal
    {
        private String strQry = string.Empty;
        private int StartMonth = 0;
        private int StartYear = 0;
        private int endMonth = 0;
        private int endYear = 0;
        private string startDate = "";
        private string endDate = "";
        public WidgetDataOutputModal getData()
        {
            if (WidgetDataInput.Filters.ContainsKey("monthStart"))
            {
                string[] monthStart = WidgetDataInput.Filters["monthStart"].Split('-');
                StartMonth = Convert.ToInt32(monthStart[0]);
                StartYear = Convert.ToInt32(monthStart[1]);

                string[] monthEnd = WidgetDataInput.Filters["monthEnd"].Split('-');
                endMonth = Convert.ToInt32(monthEnd[0]);
                endYear = Convert.ToInt32(monthEnd[1]);


                startDate = StartYear + "-" + StartMonth + "-01";
                endDate = endYear + "-" + endMonth + "-" + DateTime.DaysInMonth(endYear, endMonth);
            }



            DataSet result = null;
            if (WidgetDataInput.MeasureBy == "brand_visited")
            {
                if (WidgetDataInput.ViewBy == "potential_yield")
                {
                    MeasureByTitle = "Value";
                    SeriresTitles.Add("Brands");
                }
                else
                {
                    MeasureByTitle = "Listed Doctors";
                    SeriresTitles.Add("Total Listed Doctors");

                }
                result = getBrandVisits();
            }
            else if (WidgetDataInput.MeasureBy == "product_visited")
            {
                if (WidgetDataInput.ViewBy == "potential_yield")
                {
                    MeasureByTitle = "Value";
                    SeriresTitles.Add("Brands");
                }
                else
                {
                    MeasureByTitle = "Listed Doctors";
                    SeriresTitles.Add("Total Listed Doctors");
                }
                result = getProductVisits();
            }
            else if (WidgetDataInput.MeasureBy == "group_visited")
            {
                if (WidgetDataInput.ViewBy == "potential_yield")
                {
                    MeasureByTitle = "Value";
                    SeriresTitles.Add("Groups");
                }
                else
                {
                    MeasureByTitle = "Listed Doctors";
                    SeriresTitles.Add("Total Listed Doctors");
                }
                result = getGroupVisits();
            }
            else if (WidgetDataInput.MeasureBy == "exposure")
            {
                MeasureByTitle = "Listed Doctors";
                SeriresTitles.Add("Total Listed Doctors");
                result = getExposures();
            }
            else if (WidgetDataInput.MeasureBy == "campaign")
            {
                MeasureByTitle = "Listed Doctors";
                SeriresTitles.Add("Total Listed Doctors");
                result = getCampaigns();
            }
            else if (WidgetDataInput.MeasureBy == "doctor_business")
            {
                MeasureByTitle = "Listed Doctors";
                SeriresTitles.Add("Total Listed Doctors");
                result = getDoctorBusiness();
            }
            else if (WidgetDataInput.MeasureBy == "sample_issued")
            {
                MeasureByTitle = "Value";
                SeriresTitles.Add("Total Value");
                result = getSampleIssued();
            }
            else if (WidgetDataInput.MeasureBy == "digital_detailing")
            {
                MeasureByTitle = "Value";
                SeriresTitles.Add("Total Value");
                result = getDigitalDetailing();
            }



            WidgetDataOutputModal Output = new WidgetDataOutputModal();

            if (WidgetDataInput.ViewBy == "potential_yield")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Potential", "Yield", "Competitor" };
                Output.Series = new List<WidgetSeriesModal> {
                    new WidgetSeriesModal{
                        name="Potential",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Yield",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Competitor",
                        data =new List<int>()
                    }
                };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());

                    if (Row["Potential"] != DBNull.Value)
                    {
                        Output.Series[0].data.Add(Convert.ToInt32(Row["Potential"]));
                    }
                    else
                    {
                        Output.Series[0].data.Add(0);
                    }

                    if (Row["Yield"] != DBNull.Value)
                    {
                        Output.Series[1].data.Add(Convert.ToInt32(Row["Yield"]));
                    }
                    else
                    {
                        Output.Series[1].data.Add(0);
                    }

                    if (Row["Competitor"] != DBNull.Value)
                    {
                        Output.Series[2].data.Add(Convert.ToInt32(Row["Competitor"]));
                    }
                    else
                    {
                        Output.Series[2].data.Add(0);
                    }

                }
            }
            else if (false && WidgetDataInput.MeasureBy == "exposure")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                //   Output.SeriesIds = new List<string> { "Tagged Doctors", "Dcr Doctors", "Dcr Tagged Doctors" };
                Output.SeriesIds = new List<string> { "Tagged Doctors", "Dcr Doctors", "Dcr Tagged Doctors" };
                Output.Series = new List<WidgetSeriesModal> {
                    new WidgetSeriesModal{
                        name="TaggedDoctors",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="DcrDoctors",
                        data =new List<int>()
                    }
                    ,
                    new WidgetSeriesModal{
                        name="DcrTaggedDoctors",
                        data =new List<int>()
                    }
                };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());

                    if (Row["TaggedDoctors"] != DBNull.Value)
                    {
                        Output.Series[0].data.Add(Convert.ToInt32(Row["TaggedDoctors"]));
                    }
                    else
                    {
                        Output.Series[0].data.Add(0);
                    }

                    if (Row["DcrDoctors"] != DBNull.Value)
                    {
                        Output.Series[1].data.Add(Convert.ToInt32(Row["DcrDoctors"]));
                    }
                    else
                    {
                        Output.Series[1].data.Add(0);
                    }

                    if (Row["DcrTaggedDoctors"] != DBNull.Value)
                    {
                        Output.Series[2].data.Add(Convert.ToInt32(Row["DcrTaggedDoctors"]));
                    }
                    else
                    {
                        Output.Series[2].data.Add(0);
                    }

                }
            }
            else if (WidgetDataInput.MeasureBy == "campaign")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Doctors", "Visited Doctors", "Percentage" };
                Output.Series = new List<WidgetSeriesModal> {
                    new WidgetSeriesModal{
                        name="Doctors",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Visited Doctors",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Percentage",
                        data =new List<int>()
                    }
                };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());

                    if (Row["Doctors"] != DBNull.Value)
                    {
                        Output.Series[0].data.Add(Convert.ToInt32(Row["Doctors"]));
                    }
                    else
                    {
                        Row["Doctors"] = 0;
                        Output.Series[0].data.Add(0);
                    }

                    if (Row["VisitedDoctors"] != DBNull.Value)
                    {

                        Output.Series[1].data.Add(Convert.ToInt32(Row["VisitedDoctors"]));
                    }
                    else
                    {
                        Row["VisitedDoctors"] = 0;
                        Output.Series[1].data.Add(0);
                    }

                    double percent = 0.0;

                    if (Convert.ToInt32(Row["VisitedDoctors"]) != 0)
                    {
                        percent = ((double)Convert.ToInt32(Row["VisitedDoctors"]) / Convert.ToInt32(Row["Doctors"])) * 100;
                    }

                    if (percent < Int32.MinValue || percent > Int32.MaxValue)
                    {
                        Output.Series[2].data.Add(0);
                    }
                    else
                    {
                        Output.Series[2].data.Add(Convert.ToInt32(percent));
                    }


                }
            }
            else if (WidgetDataInput.MeasureBy == "sample_issued")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Sample Quantity", "Sample Value", "Gift Quantity", "Gift Value" };
                Output.Series = new List<WidgetSeriesModal> {
                    new WidgetSeriesModal{
                        name="Sample Quantity",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Sample Value",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Gift Quantity",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Gift Value",
                        data =new List<int>()
                    },
                };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());

                    if (Row["SampleQuantity"] != DBNull.Value)
                    {
                        Output.Series[0].data.Add(Convert.ToInt32(Row["SampleQuantity"]));
                    }
                    else
                    {
                        Output.Series[0].data.Add(0);
                    }

                    if (Row["SampleValue"] != DBNull.Value)
                    {
                        Output.Series[1].data.Add(Convert.ToInt32(Row["SampleValue"]));
                    }
                    else
                    {
                        Output.Series[1].data.Add(0);
                    }

                    if (Row["GiftQuantity"] != DBNull.Value)
                    {
                        Output.Series[2].data.Add(Convert.ToInt32(Row["GiftQuantity"]));
                    }
                    else
                    {
                        Output.Series[2].data.Add(0);
                    }

                    if (Row["GiftValue"] != DBNull.Value)
                    {
                        Output.Series[3].data.Add(Convert.ToInt32(Row["GiftValue"]));
                    }
                    else
                    {
                        Output.Series[3].data.Add(0);
                    }

                }
            }
            else if (WidgetDataInput.MeasureBy == "digital_detailing")
            {
                Output.LabelIds = new List<string>();
                Output.Labels = new List<string>();
                Output.SeriesIds = new List<string> { "Visited Doctors", "Time Spent in minutes" };
                Output.Series = new List<WidgetSeriesModal> {
                    new WidgetSeriesModal{
                        name="Visited Doctors",
                        data =new List<int>()
                    },
                    new WidgetSeriesModal{
                        name="Time Spent in minutes",
                        data =new List<int>()
                    },
                };
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    Output.LabelIds.Add(Row["LabelId"].ToString());
                    Output.Labels.Add(Row["Label"].ToString());

                    if (Row["VisitedDoctors"] != DBNull.Value)
                    {
                        Output.Series[0].data.Add(Convert.ToInt32(Row["VisitedDoctors"]));
                    }
                    else
                    {
                        Output.Series[0].data.Add(0);
                    }

                    if (Row["TimeSpent"] != DBNull.Value)
                    {
                        Output.Series[1].data.Add(Convert.ToInt32(Row["TimeSpent"]));
                    }
                    else
                    {
                        Output.Series[1].data.Add(0);
                    }

                }
            }
            else
            {
                Output = ProcessDataSet(result);
            }

            if ((WidgetDataInput.MeasureBy == "brand_visited" || WidgetDataInput.MeasureBy == "product_visited" || WidgetDataInput.MeasureBy == "group_visited") && WidgetDataInput.ViewBy == "priority" && Output != null && Output.Series != null)
            {
                if (Output.Series.Count == 1)
                {
                    List<int> visits = Enumerable.Repeat(0, Output.Series[0].data.Count).ToList();
                    Output.Series.Add(new WidgetSeriesModal { data = visits, name = "Visits" });
                }
                List<int> percentageSeries = new List<int>();
                int index = 0;
                foreach (int Series in Output.Series[0].data)
                {
                    int value = Output.Series[1].data[index];
                    double percent = ((double)value / Series) * 100;
                    percentageSeries.Add(Convert.ToInt32(percent));
                    index++;
                }
                Output.Series.Add(new WidgetSeriesModal { data = percentageSeries, name = "Percentage" });
            }
            else if (WidgetDataInput.MeasureBy == "doctor_business" && Output.SeriesIds != null)
            {
                if (Output.SeriesIds.Contains("Visited Doctors") != true)
                {
                    Output.SeriesIds.Add("Visited Doctors");
                    WidgetSeriesModal vistiedSeries = new WidgetSeriesModal
                    {
                        name = "Visited Doctors",
                        data = new List<int>()
                    };
                    foreach (string Label in Output.LabelIds)
                    {
                        vistiedSeries.data.Add(0);
                    }

                    Output.Series.Add(vistiedSeries);
                }
                if (Output.SeriesIds.Contains("Value") != true)
                {
                    Output.SeriesIds.Add("Value");
                    WidgetSeriesModal valueSeries = new WidgetSeriesModal
                    {
                        name = "Value",
                        data = new List<int>()
                    };
                    foreach (string Label in Output.LabelIds)
                    {
                        valueSeries.data.Add(0);
                    }

                    Output.Series.Add(valueSeries);
                }


                Output.SeriesIds.Add("Visit Percentage");
                WidgetSeriesModal percentageSeries = new WidgetSeriesModal
                {
                    name = "Visit Percentage",
                    data = new List<int>()
                };
                int labelKey = 0;
                foreach (string Label in Output.LabelIds)
                {
                    double percent = 0;
                    if (Output.Series[1].data[labelKey] != 0)
                    {
                        percent = ((double)Output.Series[1].data[labelKey] / Output.Series[0].data[labelKey]) * 100;
                    }

                    percentageSeries.data.Add(Convert.ToInt32(percent));
                    labelKey++;
                }

                Output.Series.Add(percentageSeries);
            }

            Output.MeasureByTitle = MeasureByTitle;
            Output.ViewByTitle = ViewByTitle;

            return Output;
        }

        public DataSet getBrandVisits()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            string brandIds = "";
            if (WidgetDataInput.WidgetFiters.ContainsKey("brands"))
            {
                brandIds = WidgetDataInput.WidgetFiters["brands"].ToString();
            }
            switch (WidgetDataInput.ViewBy)
            {
                case "priority":
                    ViewByTitle = "Priorities";
                    strQry = "select " +
                        "Map_LstDrs_Product.Product_Priority AS LabelId," +
                        "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                        "'Target' AS SplitById," +
                        "'Target' AS SplitByLabel," +
                        "COUNT(distinct(Map_LstDrs_Product.Listeddr_Code)) AS Value " +
                        "from Map_LstDrs_Product " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                        "Where Map_LstDrs_Product.Product_Priority >0 AND Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' ";
                    if (brandIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Brd_Code IN (" + brandIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority " +
                    "UNION ALL " +
                    "select " +
                    "Map_LstDrs_Product.Product_Priority AS LabelId," +
                    "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                    "'Visits' AS SplitById," +
                    "'Visits' AS SplitByLabel," +
                    "COUNT(distinct(DCRDetail_Lst_Trans.Trans_Detail_Info_Code)) AS Value " +
                    "from Map_LstDrs_Product " +
                    "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                    "INNER JOIN DCRMain_Trans ON DCRMain_Trans.sf_code = Map_LstDrs_Product.Sf_Code " +
                    "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.Trans_Detail_Info_Type=1 AND  DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND  Map_LstDrs_Product.Listeddr_Code=DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND (" +
                    "(Map_LstDrs_Product.Product_Priority =1 AND (CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code) = 1))" +
                    "OR (Map_LstDrs_Product.Product_Priority =2 AND (CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code, CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) + 1) = CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =3 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =5 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1)+1) )" +
                    ") " +
                    "AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "Where (MONTH(DCRMain_Trans.Activity_Date) >=" + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >=" + StartYear + ") AND (MONTH(DCRMain_Trans.Activity_Date) <=" + endMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <=" + endYear + ") AND  Map_LstDrs_Product.Product_Priority >0 AND Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' AND DCRMain_Trans.FieldWork_Indicator='F' ";
                    if (brandIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Brd_Code IN (" + brandIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "potential_yield":
                    ViewByTitle = "Brands";

                    strQry = "With Yeild as (" +
                        "SELECT Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "SUM(Trans_RCPA_Head.OPValue) AS Yield " +
                        "FROM Mas_Product_Brand " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Brd_Code = Mas_Product_Brand.Product_Brd_Code " +
                        "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo AND Trans_RCPA_Head.sf_code IN (" + Sfcodes + ") AND ((MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ")AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                        "WHERE   Mas_Product_Brand.Division_code = " + DivisionCode + "    AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP BY Mas_Product_Brand.Product_Brd_Code, Mas_Product_Brand.Product_Brd_Name )," +
                        " Competitor as (" +
                        "SELECT Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "SUM(Trans_RCPA_Detail.CPValue) AS Competitor " +
                        "FROM  Mas_Product_Brand " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Brd_Code = Mas_Product_Brand.Product_Brd_Code " +
                        "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo AND Trans_RCPA_Head.sf_code IN (" + Sfcodes + ") AND ((MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ")AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                        "LEFT JOIN Trans_RCPA_Detail ON Trans_RCPA_Detail.FK_PK_ID = Trans_RCPA_Head.PK_ID " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + "    AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP BY Mas_Product_Brand.Product_Brd_Code, Mas_Product_Brand.Product_Brd_Name ) " +
                        "Select Yeild.LabelId,Yeild.Label,SUM(Yeild.Yield+Competitor.Competitor) as potential,Yeild.Yield,Competitor.Competitor from Yeild " +
                        "LEFT JOIN Competitor ON Competitor.LabelId = Yeild.LabelId GROUP BY Yeild.LabelId,Yeild.Label,Yeild.Yield,Competitor.Competitor;";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                default:
                    break;

            }

            return result;
        }

        public DataSet getProductVisits()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            string productIds = "";
            if (WidgetDataInput.WidgetFiters.ContainsKey("products"))
            {
                productIds = WidgetDataInput.WidgetFiters["products"].ToString();
            }
            switch (WidgetDataInput.ViewBy)
            {
                case "priority":
                    ViewByTitle = "Priorities";
                    strQry = "select " +
                        "Map_LstDrs_Product.Product_Priority AS LabelId," +
                        "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                        "'Target' AS SplitById," +
                        "'Target' AS SplitByLabel," +
                        "COUNT(distinct(Map_LstDrs_Product.Listeddr_Code)) AS Value " +
                        "from Map_LstDrs_Product " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                        "Where Map_LstDrs_Product.Product_Priority >0 AND Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' ";
                    if (productIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority " +
                    "UNION ALL " +
                    "select " +
                    "Map_LstDrs_Product.Product_Priority AS LabelId," +
                    "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                    "'Visits' AS SplitById," +
                    "'Visits' AS SplitByLabel," +
                    "COUNT(distinct(DCRDetail_Lst_Trans.Trans_Detail_Info_Code)) AS Value " +
                    "from Map_LstDrs_Product " +
                    "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                    "LEFT JOIN DCRMain_Trans ON DCRMain_Trans.sf_code = Map_LstDrs_Product.Sf_Code " +
                    "LEFT JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND  Map_LstDrs_Product.Listeddr_Code=DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND (" +
                    "(Map_LstDrs_Product.Product_Priority =1 AND (CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code) = 1))" +
                    "OR (Map_LstDrs_Product.Product_Priority =2 AND (CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code, CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) + 1) = CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =3 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1)+1) )" +
                    ") " +
                    "AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "Where (MONTH(DCRMain_Trans.Activity_Date) >=" + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >=" + StartYear + ") AND (MONTH(DCRMain_Trans.Activity_Date) <=" + endMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <=" + endYear + ") AND  Map_LstDrs_Product.Product_Priority >0 AND Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' AND DCRMain_Trans.FieldWork_Indicator='F' ";
                    if (productIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "potential_yield":
                    ViewByTitle = "Products";

                    strQry = "With Yeild as (SELECT ";
                    if (productIds == "")
                    {
                        strQry += " TOP 5 ";
                    }

                    strQry +=
                        "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                        "Mas_Product_Detail.Product_Detail_Name AS Label, " +
                        "SUM(Trans_RCPA_Head.OPValue) AS Yield " +
                    "FROM Mas_Product_Detail " +
                    "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = cast(Mas_Product_Detail.Product_Code_SlNo as varchar) AND Trans_RCPA_Head.sf_code IN(" + Sfcodes + ") AND ( (MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ") AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                    "WHERE Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Active_Flag = 0 ";
                    if (productIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                    }
                    strQry += " GROUP BY Mas_Product_Detail.Product_Code_SlNo, Mas_Product_Detail.Product_Detail_Name),";

                    strQry += "Competitor as (SELECT ";
                    if (productIds == "")
                    {
                        strQry += " ";
                    }

                    strQry +=
                        "Mas_Product_Detail.Product_Code_SlNo AS LabelId, " +
                        "Mas_Product_Detail.Product_Detail_Name AS Label, " +
                        "SUM(Trans_RCPA_Detail.CPValue) AS Competitor " +
                    "FROM Mas_Product_Detail " +
                    "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = cast(Mas_Product_Detail.Product_Code_SlNo as varchar) AND Trans_RCPA_Head.sf_code IN(" + Sfcodes + ") AND ( (MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ") AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                    "LEFT JOIN Trans_RCPA_Detail ON Trans_RCPA_Detail.FK_PK_ID = Trans_RCPA_Head.PK_ID " +
                    "WHERE Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Active_Flag = 0 ";
                    if (productIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Code_SlNo IN (" + productIds + ") ";
                    }
                    strQry += " GROUP BY Mas_Product_Detail.Product_Code_SlNo, Mas_Product_Detail.Product_Detail_Name )";
                    strQry += "Select Yeild.LabelId,Yeild.Label,SUM(Yeild.Yield+Competitor.Competitor) as potential,Yeild.Yield,Competitor.Competitor from Yeild " +
                        "LEFT JOIN Competitor ON Competitor.LabelId = Yeild.LabelId GROUP BY Yeild.LabelId,Yeild.Label,Yeild.Yield,Competitor.Competitor;";



                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                default:
                    break;

            }

            return result;
        }

        public DataSet getGroupVisits()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            string groupIds = "";
            if (WidgetDataInput.WidgetFiters.ContainsKey("groups"))
            {
                groupIds = WidgetDataInput.WidgetFiters["groups"].ToString();
            }
            switch (WidgetDataInput.ViewBy)
            {
                case "priority":
                    ViewByTitle = "Priorities";
                    strQry = "select " +
                        "Map_LstDrs_Product.Product_Priority AS LabelId," +
                        "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                        "'Target' AS SplitById," +
                        "'Target' AS SplitByLabel," +
                        "COUNT(distinct(Map_LstDrs_Product.Listeddr_Code)) AS Value " +
                        "from Map_LstDrs_Product " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                        "Where Map_LstDrs_Product.Product_Priority >0 AND  Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' ";
                    if (groupIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Grp_Code IN (" + groupIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority " +
                    "UNION ALL " +
                    "select " +
                    "Map_LstDrs_Product.Product_Priority AS LabelId," +
                    "Concat('Priority ',Map_LstDrs_Product.Product_Priority) AS Label," +
                    "'Visits' AS SplitById," +
                    "'Visits' AS SplitByLabel," +
                    "COUNT(distinct(DCRDetail_Lst_Trans.Trans_Detail_Info_Code)) AS Value " +
                    "from Map_LstDrs_Product " +
                    "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                    "LEFT JOIN DCRMain_Trans ON DCRMain_Trans.sf_code = Map_LstDrs_Product.Sf_Code " +
                    "LEFT JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND  Map_LstDrs_Product.Listeddr_Code=DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND (" +
                    "(Map_LstDrs_Product.Product_Priority =1 AND (CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code) = 1))" +
                    "OR (Map_LstDrs_Product.Product_Priority =2 AND (CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', DCRDetail_Lst_Trans.Product_Code, CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code) + 1) = CHARINDEX('#', DCRDetail_Lst_Trans.Product_Code)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =3 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1)+1) ) " +
                    "OR (Map_LstDrs_Product.Product_Priority =4 AND ( CHARINDEX('#', Map_LstDrs_Product.Product_Code) >0 AND CHARINDEX(CAST(Map_LstDrs_Product.Product_Code AS VARCHAR) + '~', Map_LstDrs_Product.Product_Code, CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1) + 1) = CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code,CHARINDEX('#', Map_LstDrs_Product.Product_Code) + 1) + 1) + 1)+1) )" +
                    ") " +
                    "AND DCRDetail_Lst_Trans.Trans_SlNo IS NOT NULL " +
                    "Where (MONTH(DCRMain_Trans.Activity_Date) >=" + StartMonth + " AND YEAR(DCRMain_Trans.Activity_Date) >=" + StartYear + ") AND (MONTH(DCRMain_Trans.Activity_Date) <=" + endMonth + " AND YEAR(DCRMain_Trans.Activity_Date) <=" + endYear + ") AND  Map_LstDrs_Product.Product_Priority >0 AND  Map_LstDrs_Product.Product_Priority <6 AND Map_LstDrs_Product.Sf_Code IN(" + Sfcodes + ") AND Map_LstDrs_Product.Division_Code='" + DivisionCode + "' AND DCRMain_Trans.FieldWork_Indicator='F' ";
                    if (groupIds != "")
                    {
                        strQry += " AND Mas_Product_Detail.Product_Grp_Code IN (" + groupIds + ") ";
                    }
                    strQry += "GROUP BY Map_LstDrs_Product.Product_Priority";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "potential_yield":
                    ViewByTitle = "Groups";

                    strQry = "With Yeild as (SELECT ";

                    strQry +=
                        "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                        "Mas_Product_Group.Product_Grp_Name AS Label, " +
                        "SUM(Trans_RCPA_Head.OPValue) AS Yield " +
                    "FROM Mas_Product_Group " +
                    "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Grp_Code = Mas_Product_Group.Product_Grp_Code " +
                    "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo AND Trans_RCPA_Head.sf_code IN(" + Sfcodes + ") AND ( (MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ") AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                    "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 ";
                    if (groupIds != "")
                    {
                        strQry += " AND Mas_Product_Group.Product_Grp_Code IN (" + groupIds + ") ";
                    }
                    strQry += " GROUP BY Mas_Product_Group.Product_Grp_Code, Mas_Product_Group.Product_Grp_Name ),";
                    strQry += "Competitor as (SELECT ";

                    strQry +=
                        "Mas_Product_Group.Product_Grp_Code AS LabelId, " +
                        "Mas_Product_Group.Product_Grp_Name AS Label, " +
                        "SUM(Trans_RCPA_Detail.CPValue) AS Competitor " +
                    "FROM Mas_Product_Group " +
                    "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Product_Grp_Code = Mas_Product_Group.Product_Grp_Code " +
                    "LEFT JOIN Trans_RCPA_Head ON Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo AND Trans_RCPA_Head.sf_code IN(" + Sfcodes + ") AND ( (MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ") AND (MONTH(Trans_RCPA_Head.RCPA_Date) <= " + endMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + endYear + ")) " +
                    "LEFT JOIN Trans_RCPA_Detail ON Trans_RCPA_Detail.FK_PK_ID = Trans_RCPA_Head.PK_ID " +
                    "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 ";
                    if (groupIds != "")
                    {
                        strQry += " AND Mas_Product_Group.Product_Grp_Code IN (" + groupIds + ") ";
                    }
                    strQry += " GROUP BY Mas_Product_Group.Product_Grp_Code, Mas_Product_Group.Product_Grp_Name )";
                    strQry += "Select Yeild.LabelId,Yeild.Label,SUM(Yeild.Yield+Competitor.Competitor) as potential,Yeild.Yield,Competitor.Competitor from Yeild " +
                        "LEFT JOIN Competitor ON Competitor.LabelId = Yeild.LabelId GROUP BY Yeild.LabelId,Yeild.Label,Yeild.Yield,Competitor.Competitor;";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                default:
                    break;

            }

            return result;
        }

        public DataSet getExposures()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "product":
                    string productIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("products"))
                    {
                        productIds = WidgetDataInput.WidgetFiters["products"].ToString();
                    }
                    if (productIds == "")
                    {
                        DynamicDashboardModel dynamicDashboardModel = new DynamicDashboardModel();
                        DataSet products = dynamicDashboardModel.GetProducts(DivisionCode, true, 5);
                        if (products.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow Row in products.Tables[0].Rows)
                            {
                                productIds += "'" + Row["Id"].ToString() + "',";
                            }
                        }
                        if (!string.IsNullOrEmpty(productIds))
                        {
                            productIds = productIds.TrimEnd(',');
                        }
                        else
                        {
                            productIds += "0"; //to prevent sql query error
                        }
                    }
                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        ViewByTitle = "Specialities";

                        strQry = "WITH DoctorCTE AS ( " +
                            "Select " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS ProductCode , " +
                            "DCRDetail_Lst_Trans.Product_Code " +
                            "FROM " +
                            "DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(productIds) + " " +
                            "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                            "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                            "WHERE " +
                            "DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";
                        strQry += "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Map_LstDrs_Product.Listeddr_Code) AS Value " +
                            "FROM Mas_ListedDr  " +
                            "INNER JOIN Map_LstDrs_Product ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") AND Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                            "WHERE Map_LstDrs_Product.Product_Code IN (" + productIds + ")  AND Map_LstDrs_Product.Division_Code = '" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code, Mas_Doctor_Speciality.Doc_Special_Name ";
                        strQry += " UNION ALL ";
                        strQry += " Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label ";
                        strQry += " UNION ALL ";
                        strQry += "Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode =Map_LstDrs_Product.Listeddr_Code WHERE Map_LstDrs_Product.Division_Code =" + DivisionCode + " AND Map_LstDrs_Product.Product_Code IN (" + productIds + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {
                        ViewByTitle = "Categories";

                        strQry = "WITH DoctorCTE AS ( " +
                            "Select " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code " +
                            "FROM " +
                            "DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(productIds) + " " +
                            "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                            "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                            "WHERE " +
                            "DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") )";

                        strQry += "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Map_LstDrs_Product.Listeddr_Code) AS Value " +
                            "FROM Mas_ListedDr  " +
                            "INNER JOIN  Map_LstDrs_Product ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") AND Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                            "WHERE Map_LstDrs_Product.Product_Code IN (" + productIds + ")  AND Map_LstDrs_Product.Division_Code = '" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name ";
                        strQry += " UNION ALL ";
                        strQry += " Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label ";
                        strQry += " UNION ALL ";
                        strQry += "Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode =Map_LstDrs_Product.Listeddr_Code WHERE Map_LstDrs_Product.Division_Code =" + DivisionCode + " AND Map_LstDrs_Product.Product_Code IN (" + productIds + ")  AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        ViewByTitle = "Classes";

                        strQry = "WITH DoctorCTE AS ( " +
                            "Select " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code " +
                            "FROM " +
                            "DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(productIds) + " " +
                            "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                            "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                            "WHERE " +
                            "DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";

                        strQry += "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Map_LstDrs_Product.Listeddr_Code) AS Value " +
                            "FROM Mas_ListedDr  " +
                            "INNER JOIN Map_LstDrs_Product ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") AND Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                            "WHERE Map_LstDrs_Product.Product_Code IN (" + productIds + ")  AND Map_LstDrs_Product.Division_Code = '" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName ";
                        strQry += " UNION ALL ";
                        strQry += " Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label ";
                        strQry += " UNION ALL ";
                        strQry += "Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode =Map_LstDrs_Product.Listeddr_Code WHERE Map_LstDrs_Product.Division_Code =" + DivisionCode + " AND Map_LstDrs_Product.Product_Code IN (" + productIds + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0  " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label";

                    }
                    else
                    {
                        ViewByTitle = "Products";

                        strQry = "WITH DoctorCTE AS ( " +
                            "Select " +
                            "Mas_Product_Detail.Product_Code_SlNo AS LabelId," +
                            "Mas_Product_Detail.Product_Detail_Name AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS ProductCode, " +
                            "DCRDetail_Lst_Trans.Product_Code " +
                            "FROM DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND " + LikeQueryForDCRProducts(productIds) + " " +
                            "INNER JOIN Mas_Product_Detail ON cast(Mas_Product_Detail.Product_Code_SlNo as varchar) IN (" + productIds + ") AND CHARINDEX('#' + cast(Mas_Product_Detail.Product_Code_SlNo as varchar) + '~','#' + DCRDetail_Lst_Trans.Product_Code + '#' ) > 0 " +
                            "WHERE DCRMain_Trans.Division_Code =" + DivisionCode + " AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ")) ";


                        strQry += "SELECT " +
                            "Mas_Product_Detail.Product_Code_SlNo AS LabelId," +
                            "Mas_Product_Detail.Product_Detail_Name AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Map_LstDrs_Product.Listeddr_Code) AS Value " +
                            "FROM Mas_ListedDr   " +
                            "LEFT JOIN Map_LstDrs_Product ON Mas_ListedDr.ListedDrCode = Map_LstDrs_Product.Listeddr_Code and (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                            "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Product_Code_SlNo=Map_LstDrs_Product.Product_Code " +
                            "WHERE Map_LstDrs_Product.division_code=" + DivisionCode + " AND  Map_LstDrs_Product.Product_Code IN (" + productIds + ") " +
                            "GROUP BY Mas_Product_Detail.Product_Code_SlNo, Mas_Product_Detail.Product_Detail_Name ";


                        strQry += " UNION ALL ";
                        strQry += "Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId," +
                            "DoctorCTE.Label ";

                        strQry += " UNION ALL ";
                        strQry += "Select " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode =Map_LstDrs_Product.Listeddr_Code AND Map_LstDrs_Product.Product_Code =DoctorCTE.LabelId " +
                            "WHERE Map_LstDrs_Product.Division_Code =" + DivisionCode + " AND Map_LstDrs_Product.Product_Code IN (" + productIds + ") AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId,DoctorCTE.Label ";

                    }


                    try
                    {

                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;

                case "campaign":
                    string subcategoryIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("subcategories"))
                    {
                        subcategoryIds = WidgetDataInput.WidgetFiters["subcategories"].ToString();
                    }


                    if (WidgetDataInput.SplitBy == "speciality")
                    {


                        ViewByTitle = "Specialities";


                        strQry = "WITH DoctorCTE AS ( " +
                            "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS Product_Code " +
                            "FROM DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                            "INNER JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "INNER JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Code =Mas_ListedDr.Doc_Special_Code ";

                        strQry += "WHERE DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";



                        strQry += "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Mas_ListedDr.ListedDrCode) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_Doctor_Speciality.Doc_Special_Code =Mas_ListedDr.Doc_Special_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                             "LEFT JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "WHERE Mas_Doctor_Speciality.Division_Code = '" + DivisionCode + "'   AND Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' ";

                        strQry += "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code, Mas_Doctor_Speciality.Doc_Special_Name ";

                        strQry += " UNION ALL ";
                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label ";
                        strQry += " UNION ALL ";

                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode = Map_LstDrs_Product.Listeddr_Code " +
                            "WHERE Map_LstDrs_Product.Division_Code = " + DivisionCode + " AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {
                        ViewByTitle = "Categories";


                        strQry = "WITH DoctorCTE AS ( " +
                            "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS Product_Code " +
                            "FROM DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                            "INNER JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "INNER JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code ";

                        strQry += "WHERE DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";



                        strQry += "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Mas_ListedDr.ListedDrCode) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                            "LEFT JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "WHERE Mas_Doctor_Category.Division_Code = '" + DivisionCode + "'   AND Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' ";

                        strQry += "GROUP BY Mas_Doctor_Category.Doc_Cat_Code, Mas_Doctor_Category.Doc_Cat_Name ";

                        strQry += " UNION ALL ";
                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label ";
                        strQry += " UNION ALL ";

                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode = Map_LstDrs_Product.Listeddr_Code " +
                            "WHERE Map_LstDrs_Product.Division_Code = " + DivisionCode + " AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        ViewByTitle = "Classes";


                        strQry = "WITH DoctorCTE AS ( " +
                            "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS Product_Code " +
                            "FROM DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                            "INNER JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "INNER JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode ";

                        strQry += "WHERE DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";



                        strQry += "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Mas_ListedDr.ListedDrCode) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                              "LEFT JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "WHERE Mas_Doc_Class.Division_Code = '" + DivisionCode + "'   AND Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' ";

                        strQry += "GROUP BY Mas_Doc_Class.Doc_ClsCode, Mas_Doc_Class.Doc_ClsName ";

                        strQry += " UNION ALL ";
                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label ";
                        strQry += " UNION ALL ";

                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode = Map_LstDrs_Product.Listeddr_Code " +
                            "WHERE Map_LstDrs_Product.Division_Code = " + DivisionCode + " AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label";
                    }
                    else
                    {
                        ViewByTitle = "Campaigns";

                        strQry = "WITH DoctorCTE AS ( " +
                            "SELECT " +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                            "DCRDetail_Lst_Trans.Trans_Detail_Info_Code AS DoctorCode," +
                            "DCRDetail_Lst_Trans.Product_Code AS Product_Code " +
                            "FROM DCRMain_Trans " +
                            "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                            "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                            "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "WHERE DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") ) ";



                        strQry += "SELECT " +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                            "'Mapped Doctors' AS SplitById," +
                            "'Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT Mas_ListedDr.ListedDrCode) AS Value " +
                            "FROM Mas_Doc_SubCategory " +
                            "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                               "LEFT JOIN Map_LstDrs_Product ON Map_LstDrs_Product.Listeddr_Code = Mas_ListedDr.ListedDrCode " +
                            "WHERE Mas_Doc_SubCategory.Division_Code = '" + DivisionCode + "'   AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' ";
                        if (subcategoryIds != "")
                        {
                            strQry += " AND Mas_Doc_SubCategory.Doc_SubCatCode IN (" + subcategoryIds + ") ";
                        }
                        strQry += "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode, Mas_Doc_SubCategory.Doc_SubCatName ";

                        strQry += " UNION ALL ";
                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Doctors' AS SplitById," +
                            "'DCR Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM DoctorCTE " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label ";
                        strQry += " UNION ALL ";

                        strQry += "SELECT " +
                            "DoctorCTE.LabelId," +
                            "DoctorCTE.Label," +
                            "'DCR Mapped Doctors' AS SplitById," +
                            "'DCR Mapped Doctors' AS SplitByLabel," +
                            "COUNT(DISTINCT DoctorCTE.DoctorCode) AS Value " +
                            "FROM Map_LstDrs_Product " +
                            "INNER JOIN DoctorCTE ON DoctorCTE.DoctorCode = Map_LstDrs_Product.Listeddr_Code " +
                            "WHERE Map_LstDrs_Product.Division_Code = " + DivisionCode + " AND CHARINDEX('#' + cast(Map_LstDrs_Product.Product_Code as varchar) + '~','#' + DoctorCTE.Product_Code + '#' )>0 " +
                            "GROUP BY DoctorCTE.LabelId, DoctorCTE.Label";
                    }

                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                default:
                    break;

            }

            return result;
        }

        public DataSet getCampaigns()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "COUNT(DISTINCT Mas_ListedDr.ListedDrCode) AS Doctors," +
                        "COUNT(DISTINCT DCRDetail_Lst_Trans.Trans_Detail_Info_Code) AS VisitedDoctors " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',',',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code IN (" + Sfcodes + ") " +
                        "LEFT JOIN DCRMain_Trans ON DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") " +
                        "LEFT JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 AND DCRDetail_Lst_Trans.Trans_Detail_Info_Code =Mas_ListedDr.ListedDrCode " +
                        "WHERE Mas_Doc_SubCategory.Division_Code = '" + DivisionCode + "' " +
                        //"AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' " +
                        "AND ((Mas_Doc_SubCategory.Effective_from <='" + startDate + "' AND Mas_Doc_SubCategory.Effective_to >='" + startDate + "')OR(Mas_Doc_SubCategory.Effective_from <='" + endDate + "' AND Mas_Doc_SubCategory.Effective_to >='" + endDate + "')) ";
            switch (WidgetDataInput.ViewBy)
            {
                case "brand":
                    ViewByTitle = "Campaigns";
                    string brandIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("brands"))
                    {
                        brandIds = WidgetDataInput.WidgetFiters["brands"].ToString();
                    }

                    if (brandIds == "")
                    {
                        strQry += "AND Mas_Doc_SubCategory.Brand_code is not null ";
                    }
                    else
                    {
                        strQry += "AND " + LikeQueryForCampaignBrands(brandIds) + " ";
                    }

                    break;
                case "product":
                    ViewByTitle = "Campaigns";
                    string productIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("products"))
                    {
                        productIds = WidgetDataInput.WidgetFiters["products"].ToString();
                    }

                    if (productIds == "")
                    {
                        strQry += "AND Mas_Doc_SubCategory.product_code is not null ";
                    }
                    else
                    {
                        strQry += "AND " + LikeQueryForCampaignProdcuts(productIds) + " ";
                    }

                    break;
                case "speciality":
                    ViewByTitle = "Campaigns";
                    string specialityIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("specialities"))
                    {
                        specialityIds = WidgetDataInput.WidgetFiters["specialities"].ToString();
                    }

                    if (specialityIds == "")
                    {
                        strQry += "AND Mas_Doc_SubCategory.spec_code is not null ";
                    }
                    else
                    {
                        strQry += "AND " + LikeQueryForCampaignSpecialities(specialityIds) + " ";
                    }

                    break;
                case "categroy":
                    ViewByTitle = "Campaigns";
                    string categoryIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("categories"))
                    {
                        categoryIds = WidgetDataInput.WidgetFiters["categories"].ToString();
                    }

                    if (categoryIds == "")
                    {
                        strQry += "AND Mas_Doc_SubCategory.cat_code is not null ";
                    }
                    else
                    {
                        strQry += "AND " + LikeQueryForCampaignCategories(categoryIds) + " ";
                    }

                    break;

                default:
                    break;


            }
            strQry += "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode, Mas_Doc_SubCategory.Doc_SubCatName";
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

        public DataSet getDoctorBusiness()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            switch (WidgetDataInput.ViewBy)
            {
                case "campaign":
                    ViewByTitle = "Campaigns";

                    strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "'Mapped Doctors' AS SplitById," +
                        "'Mapped Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE Mas_Doc_SubCategory.Division_Code ='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "'Visited Doctors' AS SplitById," +
                        "'Visited Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode)AS Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "'Value' AS SplitById," +
                        "'Value' AS SplitByLabel," +
                        "sum(Trans_Dr_Business_Valuewise_Head.Business_Value) AS Value " +
                        "FROM Trans_Dr_Business_Valuewise_Head " +
                        //  "INNER JOIN Trans_Dr_Business_Valuewise_Detail ON Trans_Dr_Business_Valuewise_Detail.trans_sl_no = Trans_Dr_Business_Valuewise_Head.trans_sl_no " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = Trans_Dr_Business_Valuewise_Head.ListedDr_Code " +
                        "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE Trans_Dr_Business_Valuewise_Head.Sf_Code IN (" + Sfcodes + ") AND  Trans_Dr_Business_Valuewise_Head.Division_Code ='" + DivisionCode + "' AND (Trans_Dr_Business_Valuewise_Head.Trans_Month >= '" + StartMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year >= '" + StartYear + "') AND (Trans_Dr_Business_Valuewise_Head.Trans_Month <= '" + endMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year <= '" + endYear + "') " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName";
                    break;

                case "speciality":
                    ViewByTitle = "Specialities";

                    strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "'Mapped Doctors' AS SplitById," +
                        "'Mapped Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Mas_Doctor_Speciality.Division_Code ='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "'Visited Doctors' AS SplitById," +
                        "'Visited Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode)AS Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "'Value' AS SplitById," +
                        "'Value' AS SplitByLabel," +
                        "sum(Trans_Dr_Business_Valuewise_Head.Business_Value) AS Value " +
                        "FROM Trans_Dr_Business_Valuewise_Head " +
                        // "INNER JOIN Trans_Dr_Business_Valuewise_Detail ON Trans_Dr_Business_Valuewise_Detail.trans_sl_no = Trans_Dr_Business_Valuewise_Head.trans_sl_no " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = Trans_Dr_Business_Valuewise_Head.ListedDr_Code " +
                        "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Trans_Dr_Business_Valuewise_Head.Sf_Code IN (" + Sfcodes + ") AND  Trans_Dr_Business_Valuewise_Head.Division_Code ='" + DivisionCode + "' AND (Trans_Dr_Business_Valuewise_Head.Trans_Month >= '" + StartMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year >= '" + StartYear + "') AND (Trans_Dr_Business_Valuewise_Head.Trans_Month <= '" + endMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year <= '" + endYear + "') " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name";
                    break;
                case "category":
                    ViewByTitle = "Categories";

                    strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "'Mapped Doctors' AS SplitById," +
                        "'Mapped Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Mas_Doctor_Category.Division_Code ='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "'Visited Doctors' AS SplitById," +
                        "'Visited Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode)AS Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "'Value' AS SplitById," +
                        "'Value' AS SplitByLabel," +
                        "sum(Trans_Dr_Business_Valuewise_Head.Business_Value) AS Value " +
                        "FROM Trans_Dr_Business_Valuewise_Head " +
                        //   "INNER JOIN Trans_Dr_Business_Valuewise_Detail ON Trans_Dr_Business_Valuewise_Detail.trans_sl_no = Trans_Dr_Business_Valuewise_Head.trans_sl_no " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = Trans_Dr_Business_Valuewise_Head.ListedDr_Code " +
                        "INNER JOIN Mas_Doctor_Category ON Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Trans_Dr_Business_Valuewise_Head.Sf_Code IN (" + Sfcodes + ") AND  Trans_Dr_Business_Valuewise_Head.Division_Code ='" + DivisionCode + "' AND (Trans_Dr_Business_Valuewise_Head.Trans_Month >= '" + StartMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year >= '" + StartYear + "') AND (Trans_Dr_Business_Valuewise_Head.Trans_Month <= '" + endMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year <= '" + endYear + "') " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name";
                    break;
                case "class":
                    ViewByTitle = "Classes";

                    strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "'Mapped Doctors' AS SplitById," +
                        "'Mapped Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN Mas_ListedDr ON (Mas_ListedDr.ListedDr_Deactivate_Date Is Null OR Mas_ListedDr.ListedDr_Active_Flag =0 OR (Mas_ListedDr.ListedDr_Deactivate_Date <='" + endDate + "') ) AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Mas_Doc_Class.Division_Code ='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "'Visited Doctors' AS SplitById," +
                        "'Visited Doctors' AS SplitByLabel," +
                        "count(distinct Mas_ListedDr.ListedDrCode)AS Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code ='" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                        "UNION ALL " +
                        "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "'Value' AS SplitById," +
                        "'Value' AS SplitByLabel," +
                        "sum(Trans_Dr_Business_Valuewise_Head.Business_Value) AS Value " +
                        "FROM Trans_Dr_Business_Valuewise_Head " +
                        //   "INNER JOIN Trans_Dr_Business_Valuewise_Detail ON Trans_Dr_Business_Valuewise_Detail.trans_sl_no = Trans_Dr_Business_Valuewise_Head.trans_sl_no " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = Trans_Dr_Business_Valuewise_Head.ListedDr_Code " +
                        "INNER JOIN Mas_Doc_Class ON Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Trans_Dr_Business_Valuewise_Head.Sf_Code IN (" + Sfcodes + ") AND  Trans_Dr_Business_Valuewise_Head.Division_Code ='" + DivisionCode + "' AND (Trans_Dr_Business_Valuewise_Head.Trans_Month >= '" + StartMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year >= '" + StartYear + "') AND (Trans_Dr_Business_Valuewise_Head.Trans_Month <= '" + endMonth + "' AND  Trans_Dr_Business_Valuewise_Head.Trans_Year <= '" + endYear + "') " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName";
                    break;
                default:
                    break;


            }
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
        public DataSet getSampleIssued()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            switch (WidgetDataInput.ViewBy)
            {
                case "hq":

                    ViewByTitle = "HQs";

                    strQry = "WITH SampleProducts AS ( " +
                        "SELECT " +
                        "Mas_Salesforce.Sf_Code," +
                        "Mas_Salesforce.Sf_HQ," +
                        "Mas_Salesforce.state_code," +
                        "DCRDetail_Lst_Trans.Product_Code," +
                        "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS decimal) AS Quantity," +
                        "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS float) * (Select top 1 cast(Mas_Product_State_Rates.sample_price as float) from Mas_Product_State_Rates WHERE Mas_Product_State_Rates.division_code ='" + DivisionCode + "' " +
                        "AND Mas_Product_State_Rates.State_code =Mas_Salesforce.State_code " +
                        "AND Mas_Product_State_Rates.product_detail_code =mas_product_detail.Product_Detail_code AND Mas_Product_State_Rates.Effective_From_Date <=CAST(DCRMain_Trans.Activity_Date AS DATE) order by Mas_Product_State_Rates.Effective_From_Date DESC) ) as Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno " +
                        "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code = DCRMain_Trans.Sf_Code " +
                        "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s " +
                        "LEFT JOIN mas_product_detail ON s.Item <>'' AND mas_product_detail.Product_code_SlNo =cast(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "'         AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "'         AND DCRDetail_Lst_Trans.Product_Code <> '' AND Item <>'' )" +
                        ",GiftProducts AS (" +
                        "SELECT " +
                        "Mas_Salesforce.Sf_Code," +
                        "Mas_Salesforce.Sf_HQ," +
                        "Mas_Salesforce.state_code," +
                        "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS decimal) AS Quantity," +
                        "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS decimal) *Mas_gift.Gift_value) as Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno " +
                        "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code = DCRMain_Trans.Sf_Code " +
                        "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                        "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRDetail_Lst_Trans.Gift_Code <> '' AND DCRDetail_Lst_Trans.Gift_Code is not null AND Item <> '' )" +
                        ",RecursiveHierarchy AS (" +
                        "SELECT " +
                        "Sf_Code AS firstLevel," +
                        "Sf_Code, Sf_Name," +
                        "Sf_HQ," +
                        "Reporting_To_SF, 0 AS Level " +
                        "FROM Mas_Salesforce ";
                    string[] sfcodesArray = Sfcodes.Split(',').Select(value => value.Trim()).ToArray();
                    if (sfcodesArray.Length == 1)
                    {
                        strQry += "WHERE Sf_Code = '" + WidgetDataInput.SFCode + "' ";
                    }
                    else
                    {
                        strQry += "WHERE Reporting_To_SF = '" + WidgetDataInput.SFCode + "' ";
                    }
                    strQry += "AND CHARINDEX(',' + '" + DivisionCode + "' + ',',',' + Division_Code + ',' ) > 0  " +
                        "UNION ALL " +
                        "SELECT " +
                        "R.firstLevel AS firstLevel," +
                        "M.Sf_Code," +
                        "M.Sf_Name," +
                        "M.Sf_HQ," +
                        "M.Reporting_To_SF," +
                        "R.Level + 1 " +
                        "FROM Mas_Salesforce M " +
                        "INNER JOIN RecursiveHierarchy R ON M.Reporting_To_SF = R.Sf_Code)" +
                        ",SampleHQ As ( " +
                        "SELECT " +
                        "R.Sf_HQ AS LabelId," +
                        "R.Sf_HQ AS Label," +
                        "SUM(SampleProducts.Quantity) AS SampleQuantity," +
                        "SUM(SampleProducts.Value) AS SampleValue " +
                        "FROM RecursiveHierarchy R " +
                        "LEFT JOIN SampleProducts ON SampleProducts.Sf_Code = R.Sf_Code " +
                        "GROUP BY R.Sf_HQ )" +
                        ",GiftHQ As ( " +
                        "SELECT " +
                        "R.Sf_HQ AS LabelId," +
                        "R.Sf_HQ AS Label," +
                        "SUM(GiftProducts.Quantity) AS GiftQuantity," +
                        "SUM(GiftProducts.Value) AS GiftValue " +
                        "FROM RecursiveHierarchy R " +
                        "LEFT JOIN GiftProducts ON GiftProducts.Sf_Code = R.Sf_Code " +
                        "GROUP BY R.Sf_HQ ) " +
                        "SELECT " +
                        "COALESCE(SH.LabelId, GH.LabelId) AS LabelId," +
                        "COALESCE(SH.Label, GH.Label) AS Label," +
                        "SH.SampleQuantity," +
                        "SH.SampleValue," +
                        "GH.GiftQuantity," +
                        "GH.GiftValue " +
                        "FROM SampleHQ SH " +
                        "FULL JOIN GiftHQ GH ON SH.LabelId = GH.LabelId";
                    break;
                case "campaign":

                    ViewByTitle = "Campaign";

                    strQry = "WITH SampleProducts AS ( " +
                        "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode," +
                        "Mas_Doc_SubCategory.Doc_SubCatName," +
                        "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS decimal) AS Quantity," +
                        "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, CHARINDEX('$', s.Item) - CHARINDEX('~', s.Item) - 1) AS float) *(SELECT TOP 1 cast(Mas_Product_State_Rates.sample_price as float) FROM Mas_Product_State_Rates WHERE Mas_Product_State_Rates.division_code = '" + DivisionCode + "' AND Mas_Product_State_Rates.State_code = Mas_Salesforce.State_code AND Mas_Product_State_Rates.product_detail_code = mas_product_detail.Product_Detail_code AND Mas_Product_State_Rates.Effective_From_Date <= CAST(DCRMain_Trans.Activity_Date AS DATE) ORDER BY Mas_Product_State_Rates.Effective_From_Date DESC)) as Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',',',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code = DCRMain_Trans.Sf_Code " +
                        "CROSS APPLY SplitString(DCRDetail_Lst_Trans.Product_Code, '#') AS s " +
                        "LEFT JOIN mas_product_detail ON s.Item <> '' AND mas_product_detail.Product_code_SlNo = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRDetail_Lst_Trans.Product_Code <> '' AND Item <> '' )" +
                        ", GiftProducts AS (" +
                        "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode," +
                        "Mas_Doc_SubCategory.Doc_SubCatName," +
                        "CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS decimal) AS Quantity," +
                        "(CAST(SUBSTRING(s.Item, CHARINDEX('~', s.Item) + 1, LEN(s.Item) - CHARINDEX('~', s.Item)) AS decimal) *Mas_gift.Gift_value) as Value " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN DCRDetail_Lst_Trans ON DCRDetail_Lst_Trans.trans_slno = DCRMain_Trans.trans_slno AND DCRDetail_Lst_Trans.trans_detail_info_type = 1 " +
                        "INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode = DCRDetail_Lst_Trans.Trans_Detail_Info_Code " +
                        "INNER JOIN Mas_Doc_SubCategory ON CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',',',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "CROSS APPLY SplitString(cast(DCRDetail_Lst_Trans.Gift_Code as varchar)+'~'+cast(DCRDetail_Lst_Trans.Gift_Qty as varchar)+'#'+DCRDetail_Lst_Trans.Additional_Gift_Code, '#') AS s " +
                        "LEFT JOIN Mas_gift ON s.Item <> '' AND Mas_gift.Gift_code = CAST(SUBSTRING(s.Item, 1, CHARINDEX('~', s.Item) - 1) AS INT) " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' AND DCRDetail_Lst_Trans.Gift_Code <> '' AND DCRDetail_Lst_Trans.Gift_Code is not null AND Item <> '' )" +
                        ",SampleCamp As ( " +
                        "SELECT " +
                        "SampleProducts.Doc_SubCatCode AS LabelId," +
                        "SampleProducts.Doc_SubCatName AS Label," +
                        "SUM(SampleProducts.Quantity) AS SampleQuantity," +
                        "SUM(SampleProducts.Value) AS SampleValue " +
                        "FROM SampleProducts  " +
                        "GROUP BY SampleProducts.Doc_SubCatCode,SampleProducts.Doc_SubCatName )" +
                        ",GiftCamp As ( " +
                        "SELECT " +
                        "GiftProducts.Doc_SubCatCode AS LabelId," +
                        "GiftProducts.Doc_SubCatName AS Label," +
                        "SUM(GiftProducts.Quantity) AS GiftQuantity," +
                        "SUM(GiftProducts.Value) AS GiftValue " +
                        "FROM GiftProducts  " +
                        "GROUP BY GiftProducts.Doc_SubCatCode,GiftProducts.Doc_SubCatName )" +
                        "SELECT " +
                        "COALESCE(SampleCamp.LabelId, GiftCamp.LabelId) AS LabelId," +
                        "COALESCE(SampleCamp.Label, GiftCamp.Label) AS Label," +
                        "SampleCamp.SampleQuantity," +
                        "SampleCamp.SampleValue," +
                        "GiftCamp.GiftQuantity," +
                        "GiftCamp.GiftValue " +
                        "FROM SampleCamp " +
                        "FULL JOIN GiftCamp ON SampleCamp.LabelId = GiftCamp.LabelId";
                    break;

                default:
                    break;


            }
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
        public DataSet getDigitalDetailing()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;
            switch (WidgetDataInput.ViewBy)
            {
                case "brand":

                    ViewByTitle = "Brands";

                    strQry = "SELECT " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "COUNT(DISTINCT tbDigitalDetailing_Head.MSL_code) AS VisitedDoctors," +
                        "CAST(ROUND(SUM(DATEDIFF(SECOND, tbDigitalDetailing_Head.StartTime, tbDigitalDetailing_Head.EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
                        "FROM tbDigitalDetailing_Head " +
                        "INNER JOIN DCRMain_Trans ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
                        "INNER JOIN Mas_Product_Brand ON cast(Mas_Product_Brand.Product_Brd_Code as varchar) =tbDigitalDetailing_Head.Product_Code " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Mas_Product_Brand.Product_Brd_Code, Mas_Product_Brand.Product_Brd_Name";
                    break;
                case "speciality":

                    ViewByTitle = "Specialities";
                    strQry = "WITH Detailing AS  (" +
                        "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "tbDigitalDetailing_Head.MSL_code AS DoctorCode," +
                        "tbDigitalDetailing_Head.DDSl_No AS DDSl_No," +
                        "tbDigitalDetailing_Head.StartTime AS StartTime," +
                        "tbDigitalDetailing_Head.EndTime AS EndTime " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
                        // "INNER JOIN tbDgDetailing_SlideDetails ON tbDgDetailing_SlideDetails.DDSl_No = tbDigitalDetailing_Head.DDSl_No " +
                        "INNER JOIN Mas_ListedDr ON CAST(Mas_ListedDr.ListedDrCode as varchar) = tbDigitalDetailing_Head.MSL_code " +
                        "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code =DCRMain_Trans.Sf_Code " +
                        "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        // "INNER JOIN Product_Image_List ON Product_Image_List.Img_Name =tbDgDetailing_SlideDetails.SlideName AND Product_Image_List.Division_Code=DCRMain_Trans.Division_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Product_Image_List.subdivision_code + ',') > 0  AND CHARINDEX(',' + CAST(Mas_ListedDr.Doc_Special_Code AS VARCHAR) + ',', ',' + Product_Image_List.Doc_Special_Code + ',') > 0 " +
                        "INNER JOIN Mas_Doctor_Speciality ON Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "'  AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                          // "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code, Mas_Doctor_Speciality.Doc_Special_Name, tbDgDetailing_SlideDetails.DDSl_No, tbDigitalDetailing_Head.MSL_code,  tbDgDetailing_SlideDetails.StartTime, tbDgDetailing_SlideDetails.EndTime ) " +
                          "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code, Mas_Doctor_Speciality.Doc_Special_Name, tbDigitalDetailing_Head.DDSl_No, tbDigitalDetailing_Head.MSL_code,  tbDigitalDetailing_Head.StartTime, tbDigitalDetailing_Head.EndTime ) " +
                        "SELECT " +
                        "LabelId," +
                        "Label," +
                        "COUNT(DISTINCT DoctorCode) AS VisitedDoctors," +
                        "CAST(ROUND(SUM(DATEDIFF(SECOND, StartTime, EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
                        "FROM Detailing " +
                        "GROUP BY LabelId, Label";
                    break;
                case "product":
                    string productIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("products"))
                    {
                        productIds = WidgetDataInput.WidgetFiters["products"].ToString();
                    }
                    if (productIds == "")
                    {
                        DynamicDashboardModel dynamicDashboardModel = new DynamicDashboardModel();
                        DataSet products = dynamicDashboardModel.GetProducts(DivisionCode, true, 5);
                        if (products.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow Row in products.Tables[0].Rows)
                            {
                                productIds += "'" + Row["Id"].ToString() + "',";
                            }
                        }
                        if (!string.IsNullOrEmpty(productIds))
                        {
                            productIds = productIds.TrimEnd(',');
                        }
                        else
                        {
                            productIds += "0";
                        }
                    }
                    ViewByTitle = "Products";

                    strQry = "WITH Detailing AS  (" +
    "SELECT " +
    "mas_product_detail.Product_code_SlNo AS LabelId," +
    "mas_product_detail.product_detail_name AS Label," +
    "tbDigitalDetailing_Head.MSL_code AS DoctorCode," +
    //"tbDgDetailing_SlideDetails.DDSl_No AS DDSl_No," +
    //"tbDgDetailing_SlideDetails.StartTime AS StartTime," +
    //"tbDgDetailing_SlideDetails.EndTime AS EndTime " +
    "tbDigitalDetailing_Head.DDSl_No AS DDSl_No," +
    "tbDigitalDetailing_Head.StartTime AS StartTime," +
    "tbDigitalDetailing_Head.EndTime AS EndTime " +
    "FROM DCRMain_Trans " +
    "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
    //  "INNER JOIN tbDgDetailing_SlideDetails ON tbDgDetailing_SlideDetails.DDSl_No = tbDigitalDetailing_Head.DDSl_No " +
    "INNER JOIN mas_product_detail ON mas_product_detail.Product_code_SlNo IN(" + productIds + ") AND  mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
    "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code =DCRMain_Trans.Sf_Code " +
    "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0  " +
    //  "INNER JOIN Product_Image_List ON Product_Image_List.Img_Name =tbDgDetailing_SlideDetails.SlideName AND Product_Image_List.Division_Code=DCRMain_Trans.Division_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Product_Image_List.subdivision_code + ',') > 0  AND CHARINDEX(',' + CAST(mas_product_detail.Product_code_SlNo AS VARCHAR) + ',', ',' + Product_Image_List.Product_Detail_Code + ',') > 0 " +
    "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
      // "GROUP BY mas_product_detail.Product_code_SlNo,mas_product_detail.product_detail_name,tbDgDetailing_SlideDetails.DDSl_No,tbDigitalDetailing_Head.MSL_code,tbDgDetailing_SlideDetails.slideName,tbDgDetailing_SlideDetails.StartTime,tbDgDetailing_SlideDetails.EndTime) " +
      "GROUP BY mas_product_detail.Product_code_SlNo,mas_product_detail.product_detail_name,tbDigitalDetailing_Head.DDSl_No,tbDigitalDetailing_Head.MSL_code,tbDigitalDetailing_Head.StartTime,tbDigitalDetailing_Head.EndTime) " +
    "SELECT " +
    "LabelId," +
    "Label," +
    "COUNT(DISTINCT DoctorCode) AS VisitedDoctors," +
    "CAST(ROUND(SUM(DATEDIFF(SECOND, StartTime, EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
    "FROM Detailing " +
    "GROUP BY LabelId, Label";
                    break;

                case "user":
                    string userIds = "";
                    if (WidgetDataInput.WidgetFiters.ContainsKey("users"))
                    {
                        userIds = convertSfcodeWithQuotes(WidgetDataInput.WidgetFiters["users"].ToString());
                    }

                    ViewByTitle = "Users";

                    strQry = "SELECT " +
     "Mas_Salesforce.sf_code AS LabelId," +
     "Mas_Salesforce.sf_name AS Label," +
     "COUNT(DISTINCT tbDigitalDetailing_Head.MSL_code) AS VisitedDoctors," +
      // "CAST(ROUND(SUM(DATEDIFF(SECOND, tbDgDetailing_SlideDetails.StartTime, tbDgDetailing_SlideDetails.EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
      "CAST(ROUND(SUM(DATEDIFF(SECOND, tbDigitalDetailing_Head.StartTime, tbDigitalDetailing_Head.EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
     "FROM DCRMain_Trans " +
     "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
     //  "INNER JOIN tbDgDetailing_SlideDetails ON tbDgDetailing_SlideDetails.DDSl_No = tbDigitalDetailing_Head.DDSl_No " +
     "INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code = DCRMain_Trans.Sf_Code " +
     "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Sf_Code IN (" + userIds + ")  AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
     "GROUP BY Mas_Salesforce.sf_code,Mas_Salesforce.sf_name";

                    break;
                case "priority":

                    ViewByTitle = "Priorities";
                    strQry = "WITH Detailing AS ( " +
                        "SELECT " +
                        "Map_LstDrs_Product.Product_Priority AS LabelId," +
                        "CONCAT('Priority ', Map_LstDrs_Product.Product_Priority) AS Label," +
                        "tbDigitalDetailing_Head.MSL_code AS DoctorCode," +
                        //"tbDgDetailing_SlideDetails.DDSl_No AS DDSl_No," +
                        //"tbDgDetailing_SlideDetails.StartTime AS StartTime," +
                        //"tbDgDetailing_SlideDetails.EndTime AS EndTime " +
                        "tbDigitalDetailing_Head.DDSl_No AS DDSl_No," +
                        "tbDigitalDetailing_Head.StartTime AS StartTime," +
                        "tbDigitalDetailing_Head.EndTime AS EndTime " +
                        "FROM DCRMain_Trans " +
                        "INNER JOIN tbDigitalDetailing_Head ON tbDigitalDetailing_Head.Activity_Report_code = DCRMain_Trans.trans_slno " +
                        // "INNER JOIN tbDgDetailing_SlideDetails ON tbDgDetailing_SlideDetails.DDSl_No = tbDigitalDetailing_Head.DDSl_No " +
                        "INNER JOIN mas_product_detail ON mas_product_detail.Product_Brd_Code = tbDigitalDetailing_Head.Product_Code " +
                        "INNER JOIN Mas_Salesforce ON Mas_Salesforce.Sf_Code = DCRMain_Trans.Sf_Code " +
                        "INNER JOIN mas_subdivision ON CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0  " +
                        //"INNER JOIN Product_Image_List ON Product_Image_List.Img_Name = tbDgDetailing_SlideDetails.SlideName AND Product_Image_List.Division_Code = DCRMain_Trans.Division_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Product_Image_List.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_product_detail.Product_code_SlNo AS VARCHAR) + ',', ',' + Product_Image_List.Product_Detail_Code + ',') > 0 " +
                        "INNER JOIN Map_LstDrs_Product ON  CAST(Map_LstDrs_Product.Listeddr_Code AS VARCHAR) =tbDigitalDetailing_Head.MSL_code AND Mas_Product_Detail.Product_Code_SlNo = Map_LstDrs_Product.Product_Code " +
                        "WHERE DCRMain_Trans.Sf_Code IN (" + Sfcodes + ") AND DCRMain_Trans.Division_Code = '" + DivisionCode + "' AND DCRMain_Trans.Activity_Date >= '" + startDate + "' AND DCRMain_Trans.Activity_Date <= '" + endDate + "' " +
                        "GROUP BY Map_LstDrs_Product.Product_Priority, tbDigitalDetailing_Head.DDSl_No, tbDigitalDetailing_Head.MSL_code,  tbDigitalDetailing_Head.StartTime, tbDigitalDetailing_Head.EndTime ) " +
                        "SELECT " +
                        "LabelId," +
                        "Label," +
                        "COUNT(DISTINCT DoctorCode) AS VisitedDoctors, CAST(ROUND(SUM(DATEDIFF(SECOND, StartTime, EndTime))/ 60.0, 1) AS DECIMAL(5,1)) AS TimeSpent " +
                        "FROM Detailing " +
                        "GROUP BY LabelId, Label;";
                    break;

                default:
                    break;


            }
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

        public string convertSfcodeWithQuotes(string sfcodes)
        {
            string sfcodesWithQuotes = "";
            string[] values = sfcodes.Split(',').Select(value => value.Trim()).ToArray();

            foreach (string value in values)
            {
                sfcodesWithQuotes += " '" + value + "',";
            }
            sfcodesWithQuotes = sfcodesWithQuotes.TrimEnd(',');
            return sfcodesWithQuotes;
        }
    }
}
