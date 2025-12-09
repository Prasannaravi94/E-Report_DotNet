using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace Bus_EReport.DynamicDashboard.DrillDown.MarketingKpi
{

    public class PotentialDrillDown : DrilldownBase
    {
        public int StartMonth;
        public int EndMonth;
        public int StartYear;
        public int EndYear;
        public string ProductIds;
        public PotentialDrillDown()
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
                {"Potential", new DrillDownColumn {Label = "Potential", Select = "Trans_RCPA_Head.OPValue + (select isnull(SUM(Trans_RCPA_Detail.CPValue) ,0) CPValue from Trans_RCPA_Detail where Trans_RCPA_Detail.FK_PK_ID = Trans_RCPA_Head.PK_ID) as Potential"}},
                {"Yield", new DrillDownColumn {Label = "Yield", Select = "Trans_RCPA_Head.OPValue as Yield"}},
                {"Competitor", new DrillDownColumn {Label = "Competitor", Select = "(select SUM(Trans_RCPA_Detail.CPValue) from Trans_RCPA_Detail where Trans_RCPA_Detail.FK_PK_ID = Trans_RCPA_Head.PK_ID) as Competitor"}},
                   {"RCPADate", new DrillDownColumn {Label = "RCPA Date", Select = "Trans_RCPA_Head.RCPA_Date as RCPADate",Format="Date"}},
            };

            VisibleColumns = new List<string>
            {
                "SalesforceName",
                "SalesforceHQ",
                "DoctorName",
                "DoctorSpeciality",
                "Potential",
                "Yield",
                "Competitor",
            };
        }
        public override void AfterInit()
        {
            setDates();
            FileName = "Potential vs Yield";
            PreferenceName = "PotentialDrillDown";
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



        public string getQuery()
        {
            setProductIds();
            var strQry = string.Empty;
            strQry = "SELECT " + GetSelects() + " " +
                "FROM Trans_RCPA_Head " +
                "INNER JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_code = " + DivisionCode + " AND Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo ";
            if (Filters["widgetFilters"]["measureby"] == "product_visited")
            {
                strQry += " AND cast(Mas_Product_Detail.Product_Code_SlNo as varchar)='" + Filters["widgetFilters"]["viewquery"] + "' ";
            }
            if (Filters["widgetFilters"]["measureby"] == "group_visited")
            {
                strQry += " AND Mas_Product_Detail.Product_Grp_Code=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            if (Filters["widgetFilters"]["measureby"] == "brand_visited")
            {
                strQry += "INNER JOIN Mas_Product_Brand ON Mas_Product_Detail.Product_Brd_Code = Mas_Product_Brand.Product_Brd_Code AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 AND Mas_Product_Brand.Product_Brd_Code=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            
            strQry += " INNER JOIN Mas_ListedDr ON Mas_ListedDr.ListedDrCode =Trans_RCPA_Head.Drcode ";
            if (VisibleColumns.Contains("SalesforceName") || VisibleColumns.Contains("SalesforceHQ") || VisibleColumns.Contains("SalesforceDesignation") || VisibleColumns.Contains("SalesforceEmployeeId") || VisibleColumns.Contains("SalesforceJoiningDate") || VisibleColumns.Contains("SalesforceState"))
            {
                strQry += " INNER JOIN Mas_Salesforce ON Mas_Salesforce.sf_code =Trans_RCPA_Head.sf_code ";
            }
            if (VisibleColumns.Contains("DoctorTerritory"))
            {
                strQry += " LEFT JOIN Mas_Territory_Creation ON Mas_Territory_Creation.Territory_Code =Mas_ListedDr.Territory_Code ";
            }
            if (VisibleColumns.Contains("SalesforceState"))
            {
                strQry += " LEFT JOIN Mas_State ON Mas_State.State_code = Mas_Salesforce.State_code ";
            }
            strQry += " WHERE Trans_RCPA_Head.sf_code IN ("+getSfCodes()+")AND((MONTH(Trans_RCPA_Head.RCPA_Date) >= "+StartMonth+" AND YEAR(Trans_RCPA_Head.rcpa_date) >= "+StartYear+")AND(MONTH(Trans_RCPA_Head.RCPA_Date) <= "+EndMonth+" AND YEAR(Trans_RCPA_Head.rcpa_date) <= "+EndYear+"))  ";
            return strQry;
        }
        public override string GetRecords() 
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet result;
            var strQry = getQuery() + " ORDER BY Trans_RCPA_Head.PK_ID OFFSET " + (StartRow - 1) + " ROWS FETCH NEXT " + (EndRow - StartRow + 1) + " ROWS ONLY";
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

        public override int GetTotalRecords()
        {
            setProductIds();
            int TotalRecords = 0;
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result;
            var strQry =string.Empty;
            strQry = "SELECT Count(Trans_RCPA_Head.PK_ID) AS totalRecords " +
                "FROM Trans_RCPA_Head " +
                "INNER JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_code = " + DivisionCode + " AND Trans_RCPA_Head.OPCode = Mas_Product_Detail.Product_Code_SlNo ";
            if (Filters["widgetFilters"]["measureby"] == "product_visited")
            {
                strQry += " AND cast(Mas_Product_Detail.Product_Code_SlNo as varchar) ='" + Filters["widgetFilters"]["viewquery"] + "' ";
            }
            if (Filters["widgetFilters"]["measureby"] == "group_visited")
            {
                strQry += " AND Mas_Product_Detail.Product_Grp_Code=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            if (Filters["widgetFilters"]["measureby"] == "brand_visited")
            {
                strQry += "INNER JOIN Mas_Product_Brand ON Mas_Product_Detail.Product_Brd_Code = Mas_Product_Brand.Product_Brd_Code AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 AND Mas_Product_Brand.Product_Brd_Code=" + Filters["widgetFilters"]["viewquery"] + " ";
            }
            
            strQry += " WHERE Trans_RCPA_Head.sf_code IN (" + getSfCodes() + ")AND((MONTH(Trans_RCPA_Head.RCPA_Date) >= " + StartMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) >= " + StartYear + ")AND(MONTH(Trans_RCPA_Head.RCPA_Date) <= " + EndMonth + " AND YEAR(Trans_RCPA_Head.rcpa_date) <= " + EndYear + "))  ";


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

            if (Filters["widgetFilters"]["measureby"] == "brand_visited")
            {
                setCaption("brand", Filters["widgetFilters"]["viewquery"], "Brand");
            }
            if (Filters["widgetFilters"]["measureby"] == "product_visited")
            {
                setCaption("product", Filters["widgetFilters"]["viewquery"], "Product");
            }
            if (Filters["widgetFilters"]["measureby"] == "group_visited")
            {
                setCaption("group", Filters["widgetFilters"]["viewquery"], "Group");
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
