using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_EReport.DynamicDashboard
{

    public class KpiModal
    {
        public int DivisionCode = 0;
        public WidgetDataInputModal WidgetDataInput = null;
        public string MeasureByTitle = "";
        public string ViewByTitle = "";
        public List<string> SeriresTitles = new List<string>();
        public string Sfcodes = "";
        public string SfcodesNoQuote = "";
        public void SetDivisionCode(int DivCode)
        {
            DivisionCode = DivCode;
        }

        public void SetWidgetDataInput(WidgetDataInputModal Data)
        {
            WidgetDataInput = Data;
            setSfCodes();
        }

        public WidgetDataOutputModal ProcessDataSet(DataSet Records)
        {
            WidgetDataOutputModal WidgetData = new WidgetDataOutputModal();
            if (Records != null && Records.Tables[0].Rows.Count > 0)
            {
                List<WidgetSeriesModal> SeriesListAll = new List<WidgetSeriesModal>();

                // Create a dictionary to store data by SplitByLabel
                Dictionary<string, Dictionary<string,int>> SplitByData = new Dictionary<string, Dictionary<string,int>>();
                Dictionary<string, string> SplitByNames = new Dictionary<string, string>();
                List<string> Labels = new List<string>();
                List<string> LabelIds = new List<string>();
                List<string> SeriesIds = new List<string>();
                foreach (DataRow Row in Records.Tables[0].Rows)
                {
                    string splitByLabel = Row["SplitByLabel"].ToString();
                    string splitById = Row["SplitById"].ToString();
                    string LabelId = Row["LabelId"].ToString();
                    int value = Convert.ToInt32(Row["Value"]);

                    if (SplitByData.ContainsKey(splitById))
                    {
                        SplitByData[splitById][LabelId] = value;
                    }
                    else
                    {
                        SplitByData[splitById] = new Dictionary<string, int>();
                        SplitByData[splitById][LabelId] = value;
                        SplitByNames[splitById] = splitByLabel;
                        SeriesIds.Add(splitById);
                    }
                    if (!LabelIds.Contains(LabelId))
                    {
                        Labels.Add(Row["Label"].ToString());
                        LabelIds.Add(LabelId);
                    }
                }

                // Create WidgetSeriesModal for each SplitByLabel
                foreach (var kvp in SplitByData)
                {
                    string name = "";
                    if (kvp.Key == "")
                    {
                        name = MeasureByTitle;
                    }
                    else
                    {
                        name = SplitByNames[kvp.Key];
                    }

                    List<int> seriresValues = new List<int>();
                    foreach (var labelId in LabelIds)
                    {
                        if (kvp.Value.ContainsKey(labelId))
                        {
                            seriresValues.Add(kvp.Value[labelId]);
                        }
                        else
                        {
                            seriresValues.Add(0);
                        }
                    }

                    WidgetSeriesModal series = new WidgetSeriesModal
                    {
                        name = name,
                        data = seriresValues
                    };
                    SeriesListAll.Add(series);
                }

                WidgetData.Series = SeriesListAll;
                WidgetData.Labels = Labels;
                WidgetData.LabelIds = LabelIds;
                WidgetData.SeriesIds = SeriesIds;
            }

            return WidgetData;
        }



        public void setSfCodes()
        {
            Sfcodes += "'" + WidgetDataInput.SFCode+ "',";
            SfcodesNoQuote += WidgetDataInput.SFCode+ ",";
            SalesForce salesForce = new SalesForce();
            DataSet salesForceData = salesForce.AllFieldforce_MR_Novacant(DivisionCode.ToString(), WidgetDataInput.SFCode);

            if (salesForceData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in salesForceData.Tables[0].Rows)
                {
                    if(WidgetDataInput.SFCode != row["Sf_Code"].ToString())
                    {
                        Sfcodes += "'" + row["Sf_Code"].ToString() + "',";
                        SfcodesNoQuote += row["Sf_Code"].ToString() + ",";
                    }
                    
                }
            }
            if (!string.IsNullOrEmpty(Sfcodes))
            {
                Sfcodes = Sfcodes.TrimEnd(',');
                SfcodesNoQuote = SfcodesNoQuote.TrimEnd(',');
            }
        }

        public string IdsToQuotedIds(string Ids)
        {
            string[] elements = Ids.Split(',');

            // Add single quotes around each element
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = "'"+elements[i]+"'";
            }

            // Join the elements back together with commas
            return  string.Join(",", elements);
        }

    }

}
