
using System.Globalization;
using System;
using System.Collections.Generic;

namespace Bus_EReport.DynamicDashboard.DrillDown.SalesKpi
{

    public class SalesDrillDownHelper
    {
        public SetDatesModel SetDates(Dictionary<string, Dictionary<string, string>> Filters)
        {
            SetDatesModel setDatesModel = new SetDatesModel();
            if (Filters["widgetFilters"].ContainsKey("periodtype"))
            {
                if (Filters["widgetFilters"]["periodtype"] == "MTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["period"].Split('-');
                    setDatesModel.StartMonth = Convert.ToInt32(monthStart[0]);
                    setDatesModel.StartYear = Convert.ToInt32(monthStart[1]);
                    setDatesModel.EndMonth = Convert.ToInt32(monthStart[0]);
                    setDatesModel.EndYear = Convert.ToInt32(monthStart[1]);

                    CultureInfo culture = CultureInfo.CurrentCulture;
                    string monthName = culture.DateTimeFormat.GetMonthName(Convert.ToInt32(monthStart[0]));
                    setDatesModel.PeriodValue = monthName + " " + monthStart[1];
                }
                else if (Filters["widgetFilters"]["periodtype"] == "QTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["period"].Split('-');
                    switch (Convert.ToInt32(monthStart[0]))
                    {
                        case 1:
                            setDatesModel.StartMonth = 1;
                            setDatesModel.EndMonth = 3;
                            break;
                        case 2:
                            setDatesModel.StartMonth = 4;
                            setDatesModel.EndMonth = 6;
                            break;
                        case 3:
                            setDatesModel.StartMonth = 7;
                            setDatesModel.EndMonth = 9;
                            break;
                        case 4:
                            setDatesModel.StartMonth = 10;
                            setDatesModel.EndMonth = 12;
                            break;
                        default:
                            break;
                    }

                    setDatesModel.StartYear = Convert.ToInt32(monthStart[1]);

                    setDatesModel.EndYear = Convert.ToInt32(monthStart[1]);

                    setDatesModel.PeriodValue = "Q" + monthStart[0] + " " + monthStart[1];
                }
                else if (Filters["widgetFilters"]["periodtype"] == "YTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["period"].Split('-');
                    setDatesModel.StartMonth = 1;
                    setDatesModel.StartYear = Convert.ToInt32(monthStart[0]);
                    setDatesModel.EndMonth = 12;
                    setDatesModel.EndYear = Convert.ToInt32(monthStart[0]);
                    setDatesModel.PeriodValue = monthStart[0];
                }

                setDatesModel.startDate = setDatesModel.StartYear + "-" + setDatesModel.StartMonth + "-01";
                setDatesModel.endDate = setDatesModel.EndYear + "-" + setDatesModel.EndMonth + "-" + DateTime.DaysInMonth(setDatesModel.EndYear, setDatesModel.EndMonth);
                setDatesModel.laststartDate = (setDatesModel.StartYear - 1) + "-" + setDatesModel.StartMonth + "-01";
                setDatesModel.lastendDate = (setDatesModel.EndYear - 1) + "-" + setDatesModel.EndMonth + "-" + DateTime.DaysInMonth(setDatesModel.EndYear - 1, setDatesModel.EndMonth);

                setDatesModel.LastStartMonth = setDatesModel.StartMonth;
                setDatesModel.LastStartYear = setDatesModel.StartYear - 1;
                setDatesModel.LastEndMonth = setDatesModel.EndMonth;
                setDatesModel.LastEndYear = setDatesModel.EndYear - 1;
            }
            if (Filters["widgetFilters"].ContainsKey("comparison_period"))
            {
                if (Filters["widgetFilters"]["periodtype"] == "MTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["comparison_period"].Split('-');
                    setDatesModel.LastStartMonth = Convert.ToInt32(monthStart[0]);
                    setDatesModel.LastStartYear = Convert.ToInt32(monthStart[1]);
                    setDatesModel.LastEndMonth = Convert.ToInt32(monthStart[0]);
                    setDatesModel.LastEndYear = Convert.ToInt32(monthStart[1]);
                    CultureInfo culture = CultureInfo.CurrentCulture;
                    string monthName = culture.DateTimeFormat.GetMonthName(Convert.ToInt32(monthStart[0]));
                    setDatesModel.ComparisionPeriodValue = monthName + " " + monthStart[1];
                }
                else if (Filters["widgetFilters"]["periodtype"] == "QTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["comparison_period"].Split('-');
                    switch (Convert.ToInt32(monthStart[0]))
                    {
                        case 1:
                            setDatesModel.LastStartMonth = 1;
                            setDatesModel.LastEndMonth = 3;
                            break;
                        case 2:
                            setDatesModel.LastStartMonth = 4;
                            setDatesModel.LastEndMonth = 6;
                            break;
                        case 3:
                            setDatesModel.LastStartMonth = 7;
                            setDatesModel.LastEndMonth = 9;
                            break;
                        case 4:
                            setDatesModel.LastStartMonth = 10;
                            setDatesModel.LastEndMonth = 12;
                            break;
                        default:
                            break;

                    }

                    setDatesModel.LastStartYear = Convert.ToInt32(monthStart[1]);

                    setDatesModel.LastEndYear = Convert.ToInt32(monthStart[1]);
                    setDatesModel.ComparisionPeriodValue = "Q" + monthStart[0] + " " + monthStart[1];
                }
                else if (Filters["widgetFilters"]["periodtype"] == "YTD")
                {
                    string[] monthStart = Filters["widgetFilters"]["comparison_period"].Split('-');
                    setDatesModel.StartMonth = 1;
                    setDatesModel.LastStartYear = Convert.ToInt32(monthStart[0]);
                    setDatesModel.LastEndMonth = 12;
                    setDatesModel.LastEndYear = Convert.ToInt32(monthStart[0]);
                    setDatesModel.ComparisionPeriodValue = monthStart[0];
                }

                setDatesModel.laststartDate = setDatesModel.LastStartYear + "-" + setDatesModel.LastStartMonth + "-01";
                setDatesModel.lastendDate = setDatesModel.LastEndYear + "-" + setDatesModel.LastEndMonth + "-" + DateTime.DaysInMonth(setDatesModel.LastEndYear, setDatesModel.LastEndMonth);

            }
            return setDatesModel;
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

    public class SetDatesModel
    {
        public int StartMonth { get; set; }
        public int StartYear { get; set; }
        public int EndMonth { get; set; }
        public int EndYear { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string laststartDate { get; set; }
        public string lastendDate { get; set; }

        public int LastStartMonth { get; set; }
        public int LastStartYear { get; set; }
        public int LastEndMonth { get; set; }
        public int LastEndYear { get; set; }

        public string PeriodValue { get; set; }
        public string ComparisionPeriodValue { get; set; }
    }
}
