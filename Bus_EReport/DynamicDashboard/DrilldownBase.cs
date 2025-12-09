using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bus_EReport.DynamicDashboard
{

    public abstract class DrilldownBase
    {
        public string FileName ="DrillDown";    
        public Dictionary<String,DrillDownColumn> Columns { get; set; }
        public List<string> VisibleColumns { get; set; }
        public int DivisionCode { get; set; }
        public string SfCode { get; set; }
        public string SfCodes { get; set; }
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public Dictionary<string, Dictionary<string, string>> Filters { get; set; }
        public Dictionary<string,string> Captions { get; set; }
        public string PreferenceName { get; set; }

        public abstract int GetTotalRecords();
        public abstract string GetRecords();
        public abstract DataSet Export();
        public abstract void SetCaptions();
        public abstract void AfterInit();

        public DrilldownBase()
        {
            Captions = new Dictionary<string, string>();
        }
        public void Init(int divisionCode, string sfCode, HttpRequest Request=null,bool captions=false)
        {
            DivisionCode = divisionCode;
            SfCode = sfCode;
            if(Request != null)
            {
                setFilters(Request);
            }

            AfterInit();
            if (captions)
            {
                SetCaptions();
            }
            SetVisibleColumns();
        }
        public void SetPagination(int startRow = 0, int endRow = 0)
        {
            StartRow = startRow;
            EndRow = endRow;
        }
        public string GetTableHeaders()
        {
            StringBuilder tableHeaderBuilder = new StringBuilder();

            tableHeaderBuilder.Append("<thead class=\"bg-dark text-light\">");
            tableHeaderBuilder.Append("<th>Sl No</th>");
            foreach (var column in VisibleColumns)
            {
                if (Columns.ContainsKey(column))
                {
                    tableHeaderBuilder.Append("<th>").Append(Columns[column].Label).Append("</th>");
                }
            }
            tableHeaderBuilder.Append("</thead>");

            return tableHeaderBuilder.ToString();
        }

        public string GetTableBody(DataSet result)
        {
            string rows =string.Empty;
            if (result != null && result.Tables[0].Rows.Count > 0)
            {
                int slNo = StartRow;
                foreach (DataRow Row in result.Tables[0].Rows)
                {
                    rows += "<tr>";
                    rows += "<td>" + slNo + "</td>";
                    foreach (var column in VisibleColumns)
                    {
                        if (Columns.ContainsKey(column))
                        {
                            if (Columns[column].Format == "Date")
                            {
                                DateTime dateValue = (DateTime)Row[column];
                                rows += "<td>" + dateValue.ToString("dd/MM/yyyy") + "</td>";
                            }
                            else
                            {
                                rows += "<td>" + Row[column].ToString() + "</td>";
                            }
                            
                        }
                        
                    }
                    rows += "</tr>";
                    slNo++;
                }
            }
            return rows;
        }

        public string getSfCodes()
        {
            if(SfCodes == null)
            {
                SfCodes += "'" + Filters["dashboardFilters"]["sfcode"] + "',";
                SalesForce salesForce = new SalesForce();
                DataSet salesForceData = salesForce.AllFieldforce_MR_Novacant(DivisionCode.ToString(), Filters["dashboardFilters"]["sfcode"]);

                if (salesForceData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in salesForceData.Tables[0].Rows)
                    {
                        if (Filters["dashboardFilters"]["sfcode"] != row["Sf_Code"].ToString())
                        {
                            SfCodes += "'" + row["Sf_Code"].ToString() + "',";
                        }

                    }
                }
                if (!string.IsNullOrEmpty(SfCodes))
                {
                    SfCodes = SfCodes.TrimEnd(',');
                }
            }
            return SfCodes;
        }

        public string getFiltersJs()
        {
            return JsonConvert.SerializeObject(Filters);
        }

        public void setFilters(HttpRequest Request)
        {
            Filters = new Dictionary<string, Dictionary<string, string>>();
           
            Dictionary<string, string> dashboardFilters = new Dictionary<string, string>();
            Dictionary<string, string> widgetFilters = new Dictionary<string, string>();

            foreach (string key in Request.QueryString.AllKeys)
            {
                // Check if the key starts with "dashboardFilters["
                if (key.StartsWith("dashboardFilters["))
                {
                    // Extract the parameter name from the key
                    int startIndex = key.IndexOf('[') + 1;
                    int endIndex = key.IndexOf(']');
                    string parameterName = key.Substring(startIndex, endIndex - startIndex);

                    // Add the parameter to the dictionary
                    dashboardFilters[parameterName] = Request.QueryString[key];
                }
                if (key.StartsWith("widgetFilters["))
                {
                    // Extract the parameter name from the key
                    int startIndex = key.IndexOf('[') + 1;
                    int endIndex = key.IndexOf(']');
                    string parameterName = key.Substring(startIndex, endIndex - startIndex);

                    // Add the parameter to the dictionary
                    widgetFilters[parameterName] = Request.QueryString[key];
                }
            }

            Filters ["dashboardFilters"]= dashboardFilters;
            Filters ["widgetFilters"] = widgetFilters;
        }

        public string GetCaptionsTable()
        {
            string table = "<div><p class=\"text-muted\">Applied Filters</p></div><div class=\"table-responsive\"><table class=\"table captions-table table-bordered\" style=\"width:auto\">";
            table += "<tbody>";
            foreach (var caption in Captions)
            {
                table += "<tr><td>" + caption.Key + "</td><td><span class=\"fw-bold\">" + caption.Value + "</span></td></tr>";
            }
            table += "</tbody>";
            table += "</table></div>";
            return table;
        }

        public string GetSelects()
        {
            string selects = string.Empty;
            foreach (var column in VisibleColumns)
            {
                if (Columns.ContainsKey(column))
                {
                    selects += Columns[column].Select + ",";
                }
                
            }
            return selects.TrimEnd(',');
        }

        public string GetSelectsKey(string prefix)
        {
            string selects = string.Empty;
            foreach (var column in VisibleColumns)
            {
                if (Columns.ContainsKey(column))
                {
                    selects += prefix + "." + column + ",";
                }
                
            }
            return selects.TrimEnd(',');
        }

        public void setCaption(string type , string id,string label)
        {
            string name = "";
            string[] items = id.Split(',');

            // Iterate through each item in the array
            foreach (string item in items)
            {
                switch (type) {
                    case "fieldforce":
                        name += GetFieldForceName(item) + ",";
                        break;
                    case "brand":
                        name += GetBrandName(item) + ",";
                        break;
                    case "group":
                        name += GetGroupName(item) + ",";
                        break;
                    case "product":
                        name += GetProductName(item) + ",";
                        break;

                    case "speciality":
                        name += GetSpecialityName(item) + ",";
                        break;
                    case "category":
                        name += GetCategoryName(item) + ",";
                        break;
                    case "productCategory":
                        name += GetProductCategoryName(item) + ",";
                        break;
                    case "class":
                        name += GetClassName(item) + ",";
                        break;
                    case "campaign":
                        name += GetCampaignName(item) + ",";
                        break;
                    case "state":
                        name += GetStateName(item) + ",";
                        break;
                    case "hq":
                        name += GetHqName(item) + ",";
                        break;
                    default:
                        break;
                }

            }
            if(name != "")
            {
                Captions[label] = name.TrimEnd(',');
            }
        }

        public string GetHqName(string hqId)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Approved_By From mas_salesforce where SF_Cat_Code='" + hqId + "'");
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Approved_By"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetFieldForceName(string sfcode)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Sf_Name From mas_salesforce where sf_code='"+ sfcode+"'");
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Sf_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetBrandName(string brandId)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Product_Brd_Name From Mas_Product_Brand where Product_Brd_Code=" + brandId);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Product_Brd_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetGroupName(string groupId)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Product_Grp_Name From Mas_Product_Group where Product_Grp_Code=" + groupId);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Product_Grp_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetStateName(string StateCode)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select StateName From mas_state where mas_state.State_Code=" + StateCode);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["StateName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetProductName(string productId)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Product_Detail_Name From Mas_Product_Detail where Product_Code_SlNo=" + productId);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Product_Detail_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetSpecialityName(string id)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Doc_Special_Name From Mas_Doctor_Speciality where Doc_Special_Code=" + id);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Doc_Special_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        
        public string GetProductCategoryName(string id)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Product_Cat_Name From Mas_Product_Category where Product_Cat_Code=" + id);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Product_Cat_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetCategoryName(string id)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Doc_Cat_Name From Mas_Doctor_Category where Doc_Cat_Code=" + id);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Doc_Cat_Name"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetClassName(string id)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Doc_ClsName From Mas_Doc_Class where Doc_ClsCode=" + id);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Doc_ClsName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }
        public string GetCampaignName(string id)
        {
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet result = db_ER.Exec_DataSet("Select Doc_SubCatName From Mas_Doc_SubCategory where Doc_SubCatCode=" + id);
                if (result.Tables[0].Rows.Count > 0)
                {
                    DataRow Row = result.Tables[0].Rows[0];
                    return Row["Doc_SubCatName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        public string monthFormatter(string month)
        {
            //if (DateTime.TryParseExact(month, "MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime result))
            //{
            //    return result.ToString("MMMM yyyy");
            //}
            //else
            //{
            //    return "";
            //}
            DateTime dt;
            if (DateTime.TryParseExact(month, "MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                //     return result.ToString("MMMM yyyy");
                return dt.ToString("MMMM yyyy");
            }
            else
            {
                return "";
            }
        }

        public string GetColumnsJs()
        {
            Dictionary<string,string> keyValuePairs = new Dictionary<string,string>();
            foreach (var column in Columns)
            {
                keyValuePairs[column.Key] = column.Value.Label;
            }
            return JsonConvert.SerializeObject(keyValuePairs);
        }
        public string GetVisibleColumnsJs()
        {
            return JsonConvert.SerializeObject(VisibleColumns);
        }
        public void SetVisibleColumns()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet result = db_ER.Exec_DataSet("Select Value From Dynamic_Dashboard_Preferences WHERE DivisionCode=" + DivisionCode + " AND Sfcode='" + SfCode + "' AND Type='column' AND Name='" + PreferenceName + "'");
            if (result.Tables[0].Rows.Count > 0)
            {
                DataRow Row = result.Tables[0].Rows[0];
                if(Row["Value"].ToString() != null && Row["Value"].ToString().Length > 0 && Row["Value"].ToString() !="[]" && JsonConvert.DeserializeObject<List<string>>(Row["Value"].ToString()) != null)
                {
                    VisibleColumns = JsonConvert.DeserializeObject<List<string>>(Row["Value"].ToString());
                }
                
            }
        }

        public void SetSalesCaptions(Dictionary<string, Dictionary<string, string>> filters,string PeriodValue, string ComparisionPeriodValue)
        {
            setCaption("fieldforce", Filters["dashboardFilters"]["sfcode"], "Field Force");
            Captions["Period Type"] = Filters["widgetFilters"]["periodtype"];
            Captions["Period"] = PeriodValue;
            //Captions["Comparision Period"] = ComparisionPeriodValue;

            if (Filters["widgetFilters"].ContainsKey("products") && Filters["widgetFilters"]["products"] != "")
            {
                setCaption("product", Filters["widgetFilters"]["products"], "Products");
            }
            if (Filters["widgetFilters"].ContainsKey("brands") && Filters["widgetFilters"]["brands"] != "")
            {
                setCaption("brand", Filters["widgetFilters"]["brands"], "Brands");
            }
            if (Filters["widgetFilters"].ContainsKey("categories") && Filters["widgetFilters"]["categories"] != "")
            {
                setCaption("productCategory", Filters["widgetFilters"]["categories"], "Categories");
            }
            if (Filters["widgetFilters"].ContainsKey("groups") && Filters["widgetFilters"]["groups"] != "")
            {
                setCaption("group", Filters["widgetFilters"]["groups"], "Groups");
            }
            if (Filters["widgetFilters"].ContainsKey("states") && Filters["widgetFilters"]["states"] != "")
            {
                setCaption("state", Filters["widgetFilters"]["states"], "States");
            }
            if (Filters["widgetFilters"].ContainsKey("hqs") && Filters["widgetFilters"]["hqs"] != "")
            {
                setCaption("hq", Filters["widgetFilters"]["hqs"], "HQs");
            }
        }
    }

    public class DrillDownColumn {
        public string Label { get;set; }
        public string Select { get;set; }
        public string Format { get;set; }
    }




}
