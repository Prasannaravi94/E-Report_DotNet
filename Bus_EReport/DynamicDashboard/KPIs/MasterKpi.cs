using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_EReport.DynamicDashboard.KPIs
{

    public class MasterKpiModel : KpiModal
    {
        private String strQry = string.Empty;

        public WidgetDataOutputModal getData()
        {
            DataSet result = null;
            if (WidgetDataInput.MeasureBy == "listed_doctor")
            {
                MeasureByTitle = "Listed Doctors";
                SeriresTitles.Add("Total Listed Doctors");
                result = getListedDoctors();
            }
            else if (WidgetDataInput.MeasureBy == "un_listed_doctor")
            {
                MeasureByTitle = "Unlisted Doctors";
                SeriresTitles.Add("Total UnListed Doctors");
                result = getUnListedDoctors();
            }
            else if (WidgetDataInput.MeasureBy == "chemist")
            {
                MeasureByTitle = "Chemists";
                SeriresTitles.Add("Total Chemists");
                result = getChemists();
            }
            else if (WidgetDataInput.MeasureBy == "stockist")
            {
                MeasureByTitle = "Stockists";
                SeriresTitles.Add("Total Stockists");
                result = getStockists();
            }
            else if (WidgetDataInput.MeasureBy == "speciality")
            {
                MeasureByTitle = "Specialities";
                SeriresTitles.Add("Total Specialities");
                result = getSpecialities();
            }
            else if (WidgetDataInput.MeasureBy == "fieldforce")
            {
                MeasureByTitle = "Field Forces";
                SeriresTitles.Add("Total Field Forces");
                result = getFieldForces();
            }
            else if (WidgetDataInput.MeasureBy == "product")
            {
                MeasureByTitle = "Products";
                SeriresTitles.Add("Total Products");
                result = getProducts();
            }
            else if (WidgetDataInput.MeasureBy == "holiday")
            {
                MeasureByTitle = "Holidays";
                SeriresTitles.Add("Total Holidays");
                result = getHolidays();
            }


            WidgetDataOutputModal Output = ProcessDataSet(result);
            Output.MeasureByTitle = MeasureByTitle;
            Output.ViewByTitle = ViewByTitle;

            return Output;
        }

        public DataSet getListedDoctors()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "speciality":
                    ViewByTitle = "Specialities";

                    if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0'  " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                            "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Division_Code='" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag='0'  " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "campaign")
                    {

                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS SplitById," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag='0'  " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "mas_subdivision.subdivision_code AS SplitById," +
                            "mas_subdivision.subdivision_name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "Mas_State.State_Code AS SplitById," +
                            "Mas_State.StateName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_State.State_Code,Mas_State.StateName " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                            "'' AS SplitById," +
                            "'' AS SplitByLabel,Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Speciality " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                            "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                            "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";

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
                case "category":
                    ViewByTitle = "Categories";
                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code AND Mas_Doctor_Speciality.Doc_Special_Code=Mas_ListedDr.Doc_Special_Code " +
                            "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                            "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                            "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Division_Code='" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag='0' " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code  AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode " +
                            "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                            "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "campaign")
                    {

                        strQry = "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS SplitById," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag='0'  " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code  AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName " +
                            "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "mas_subdivision.subdivision_code AS SplitById," +
                            "mas_subdivision.subdivision_name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code  AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                            "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code  AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_State.State_Code,Mas_State.StateName " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

                    }
                    else
                    {
                        strQry = "SELECT " +
                            "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                            "'' AS SplitById," +
                            "'' AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doctor_Category " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                            "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                            "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

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
                case "class":
                    ViewByTitle = "Classes";
                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_Doctor_Speciality.Doc_Special_Code=Mas_ListedDr.Doc_Special_Code " +
                            "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                            "ORDER BY Mas_Doc_Class.Doc_ClsName";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0' " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code " +
                            "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                            "ORDER BY Mas_Doc_Class.Doc_ClsName";
                    }
                    else if (WidgetDataInput.SplitBy == "campaign")
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS SplitById," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag='0'  " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName " +
                        "ORDER BY Mas_Doc_Class.Doc_ClsName";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "mas_subdivision.subdivision_code AS SplitById," +
                            "mas_subdivision.subdivision_name AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                            "ORDER BY Mas_Doc_Class.Doc_ClsName";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "Mas_State.State_Code AS SplitById," +
                            "Mas_State.StateName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_State.State_Code,Mas_State.StateName " +
                            "ORDER BY Mas_Doc_Class.Doc_ClsName";
                    }
                    else
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                            "Mas_Doc_Class.Doc_ClsName AS Label," +
                            "'' AS SplitById," +
                            "'' AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_Class " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND  Mas_ListedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                            "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                            "ORDER BY Mas_Doc_Class.Doc_ClsName";
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
                    ViewByTitle = "Campaigns";
                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_Doctor_Speciality.Doc_Special_Code=Mas_ListedDr.Doc_Special_Code " +
                        "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name  " +
                        "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                        "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Division_Code='" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag='0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode " +
                        "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName  " +
                        "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code " +
                        "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";
                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "SELECT " +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                            "Mas_State.State_Code AS SplitById," +
                            "Mas_State.StateName AS SplitByLabel," +
                            "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "FROM Mas_Doc_SubCategory " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                            "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName,Mas_State.State_Code,Mas_State.StateName  " +
                            "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";

                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_SubCategory.Doc_SubCatCode AS LabelId," +
                        "Mas_Doc_SubCategory.Doc_SubCatName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "FROM Mas_Doc_SubCategory " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.listeddr_active_flag='0' AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag = '0' AND Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName  " +
                        "ORDER BY Mas_Doc_SubCategory.Doc_SubCatName";

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
                case "subdivision":
                    ViewByTitle = "Sub Divisions";

                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doctor_Speciality.Doc_Special_Code=Mas_ListedDr.Doc_Special_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name ";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                            "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Division_Code = '" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName ";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name ";
                    }
                    else if (WidgetDataInput.SplitBy == "campaign")
                    {

                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS SplitById," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag='0'  " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_State.State_Code AS SplitById," +
                            "Mas_State.StateName AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_name AS Label,'' AS SplitById,'' AS SplitByLabel, Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name";

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
                case "state":
                    ViewByTitle = "States";

                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                            "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doctor_Speciality.Doc_Special_Code=Mas_ListedDr.Doc_Special_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                            "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Division_Code = '" + DivisionCode + "' AND Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doc_Class.Doc_ClsCode=Mas_ListedDr.Doc_ClsCode " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName ";
                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                            "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0' " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_Doctor_Category.Doc_Cat_Code=Mas_ListedDr.Doc_Cat_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name ";
                    }
                    else if (WidgetDataInput.SplitBy == "campaign")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "Mas_Doc_SubCategory.Doc_SubCatCode AS SplitById," +
                            "Mas_Doc_SubCategory.Doc_SubCatName AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Doc_SubCategory ON Mas_Doc_SubCategory.Division_Code='" + DivisionCode + "' AND Mas_Doc_SubCategory.Doc_SubCat_ActiveFlag='0'  " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code AND CHARINDEX(',' + CAST(Mas_Doc_SubCategory.Doc_SubCatCode AS VARCHAR) + ',', ',' + Mas_ListedDr.Doc_SubCatCode + ',') > 0 " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doc_SubCategory.Doc_SubCatCode,Mas_Doc_SubCategory.Doc_SubCatName ";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "mas_subdivision.subdivision_code AS SplitById," +
                            "mas_subdivision.subdivision_name AS SplitByLabel," +
                            " Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name ";

                    }
                    else
                    {
                        strQry = "select Mas_State.State_Code AS LabelId,Mas_State.StateName AS Label,'' AS SplitById,'' AS SplitByLabel, Count(distinct(Mas_ListedDr.ListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Division_Code = " + DivisionCode + " AND Mas_ListedDr.listeddr_active_flag = 0 AND Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName";

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

        public DataSet getUnListedDoctors()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "speciality":
                    ViewByTitle = "Specialities";

                    if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND  Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                        "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND  Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                        "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND  Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND  Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name,Mas_State.State_Code,Mas_State.StateName " +
                        "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";
                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS LabelId," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Speciality " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND  Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                        "ORDER BY Mas_Doctor_Speciality.Doc_Special_Name";

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
                case "category":
                    ViewByTitle = "Categories";

                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                        "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code AND Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name,Mas_State.State_Code,Mas_State.StateName " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";
                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Doctor_Category.Doc_Cat_Code AS LabelId," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doctor_Category " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "ORDER BY Mas_Doctor_Category.Doc_Cat_Name";

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
                case "class":
                    ViewByTitle = "Classes";
                    if (WidgetDataInput.SplitBy == "speciality")
                    {

                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name " +
                        "ORDER BY Mas_Doc_Class.Doc_ClsName";

                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND  Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name " +
                        "ORDER BY Mas_Doc_Class.Doc_ClsName";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code  " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Doc_Class.Doc_ClsName";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code  " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName,Mas_State.State_Code,Mas_State.StateName " +
                        "ORDER BY Mas_Doc_Class.Doc_ClsName";
                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Doc_Class.Doc_ClsCode AS LabelId," +
                        "Mas_Doc_Class.Doc_ClsName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "FROM Mas_Doc_Class " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.UnListedDr_Active_Flag='0' AND  Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName  ORDER BY Mas_Doc_Class.Doc_ClsName";

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
                case "subdivision":
                    ViewByTitle = "Sub Divisions";

                    if (WidgetDataInput.SplitBy == "speciality")
                    {
                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId, " +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                        " Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name ";


                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId, " +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                        " Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId, " +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                        "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                        " Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId, " +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        " Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId, " +
                        "mas_subdivision.subdivision_name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        " Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name";

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
                case "state":
                    ViewByTitle = "States";

                    if (WidgetDataInput.SplitBy == "speciality")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "Mas_Doctor_Speciality.Doc_Special_Code AS SplitById," +
                        "Mas_Doctor_Speciality.Doc_Special_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Doctor_Speciality ON Mas_Doctor_Speciality.Doc_Special_Active_Flag = '0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doctor_Speciality.Doc_Special_Code,Mas_Doctor_Speciality.Doc_Special_Name ";


                    }
                    else if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "Mas_Doctor_Category.Doc_Cat_Code AS SplitById," +
                        "Mas_Doctor_Category.Doc_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Doctor_Category ON Mas_Doctor_Category.Doc_Cat_Active_Flag = '0' AND Mas_Doctor_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_Cat_Code = Mas_Doctor_Category.Doc_Cat_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doctor_Category.Doc_Cat_Code,Mas_Doctor_Category.Doc_Cat_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "Mas_Doc_Class.Doc_ClsCode AS SplitById," +
                        "Mas_Doc_Class.Doc_ClsName AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Doc_Class ON Mas_Doc_Class.Doc_Cls_ActiveFlag = '0' AND Mas_Doc_Class.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code AND Mas_UnlistedDr.Doc_ClsCode = Mas_Doc_Class.Doc_ClsCode " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Doc_Class.Doc_ClsCode,Mas_Doc_Class.Doc_ClsName ";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_UnlistedDr.UnListedDrCode)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_UnlistedDr ON Mas_UnlistedDr.UnListedDr_Active_Flag = 0 AND Mas_UnlistedDr.Division_Code='" + DivisionCode + "' AND Mas_UnlistedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_UnlistedDr.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName";

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

        public DataSet getChemists()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "category":
                    ViewByTitle = "Categories";

                    if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "SELECT " +
                        "Mas_Chemist_Category.Cat_Code AS LabelId," +
                        "Mas_Chemist_Category.Chem_Cat_Name AS Label," +
                        "Mas_Chemist_Class.Class_Code AS SplitById," +
                        "Mas_Chemist_Class.Chem_Class_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Category " +
                        "LEFT JOIN Mas_Chemist_Class ON Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code " +
                        "WHERE Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name,Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name " +
                        "ORDER BY Mas_Chemist_Category.Chem_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_Chemist_Category.Cat_Code AS LabelId," +
                        "Mas_Chemist_Category.Chem_Cat_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Category " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Chemist_Category.Chem_Cat_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "SELECT " +
                       "Mas_Chemist_Category.Cat_Code AS LabelId," +
                       "Mas_Chemist_Category.Chem_Cat_Name AS Label," +
                       "Mas_State.State_Code AS SplitById," +
                       "Mas_State.StateName AS SplitByLabel," +
                       "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                       "FROM Mas_Chemist_Category " +
                       "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                       "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                       "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                       "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                       "WHERE Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                       "GROUP BY Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name,Mas_State.State_Code,Mas_State.StateName " +
                       "ORDER BY Mas_Chemist_Category.Chem_Cat_Name";

                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Chemist_Category.Cat_Code AS LabelId," +
                        "Mas_Chemist_Category.Chem_Cat_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Category " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code " +
                        "WHERE Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name " +
                        "ORDER BY Mas_Chemist_Category.Chem_Cat_Name";

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
                case "class":
                    ViewByTitle = "Classes";

                    if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "SELECT " +
                        "Mas_Chemist_Class.Class_Code AS LabelId," +
                        "Mas_Chemist_Class.Chem_Class_Name AS Label," +
                        "Mas_Chemist_Category.Cat_Code AS SplitById," +
                        "Mas_Chemist_Category.Chem_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Class " +
                        "LEFT JOIN Mas_Chemist_Category ON  Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code " +
                        "WHERE Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name,Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name " +
                        "ORDER BY Mas_Chemist_Class.Chem_Class_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_Chemist_Class.Class_Code AS LabelId," +
                        "Mas_Chemist_Class.Chem_Class_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Class " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code AND  Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_Chemist_Class.Chem_Class_Name";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "SELECT " +
                        "Mas_Chemist_Class.Class_Code AS LabelId," +
                        "Mas_Chemist_Class.Chem_Class_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Class " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code AND  Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name,Mas_State.State_Code,Mas_State.StateName " +
                        "ORDER BY Mas_Chemist_Class.Chem_Class_Name";

                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_Chemist_Class.Class_Code AS LabelId," +
                        "Mas_Chemist_Class.Chem_Class_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "FROM Mas_Chemist_Class " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Chemists_Active_Flag='0' AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code " +
                        "WHERE Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name " +
                        "ORDER BY Mas_Chemist_Class.Chem_Class_Name";

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
                case "subdivision":
                    ViewByTitle = "Sub Divisions";
                    if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            " mas_subdivision.subdivision_name AS Label," +
                            "Mas_Chemist_Category.Cat_Code AS SplitById," +
                            "Mas_Chemist_Category.Chem_Cat_Name AS SplitByLabel," +
                            "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Chemist_Category ON  Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name ";


                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            " mas_subdivision.subdivision_name AS Label," +
                            "Mas_Chemist_Class.Class_Code AS SplitById," +
                            "Mas_Chemist_Class.Chem_Class_Name AS SplitByLabel," +
                            "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from mas_subdivision " +
                        "LEFT JOIN Mas_Chemist_Class ON Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            "mas_subdivision.subdivision_name AS Label," +
                            "Mas_State.State_Code AS SplitById," +
                            "Mas_State.StateName AS SplitByLabel," +
                            "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select " +
                            "mas_subdivision.subdivision_code AS LabelId," +
                            " mas_subdivision.subdivision_name AS Label," +
                            "'' AS SplitById," +
                            "'' AS SplitByLabel," +
                            " Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from mas_subdivision " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name";

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
                case "state":
                    ViewByTitle = "States";

                    if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "Mas_Chemist_Category.Cat_Code AS SplitById," +
                        "Mas_Chemist_Category.Chem_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Chemist_Category ON  Mas_Chemist_Category.Chem_Cat_Active_Flag = '0' AND Mas_Chemist_Category.Division_Code='" + DivisionCode + "' " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code AND  Mas_Chemists.Cat_Code = Mas_Chemist_Category.Cat_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Chemist_Category.Cat_Code,Mas_Chemist_Category.Chem_Cat_Name ";


                    }
                    else if (WidgetDataInput.SplitBy == "class")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "Mas_Chemist_Class.Class_Code AS SplitById," +
                            "Mas_Chemist_Class.Chem_Class_Name AS SplitByLabel," +
                            "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from Mas_State " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN Mas_Chemist_Class ON Mas_Chemist_Class.Chem_Class_Active_Flag = '0' AND Mas_Chemist_Class.Division_Code='" + DivisionCode + "' " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code AND  Mas_Chemists.Class_Code = Mas_Chemist_Class.Class_Code " +
                            "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Chemist_Class.Class_Code,Mas_Chemist_Class.Chem_Class_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "select " +
                            "Mas_State.State_Code AS LabelId," +
                            "Mas_State.StateName AS Label," +
                            "mas_subdivision.subdivision_code AS SplitById," +
                            "mas_subdivision.subdivision_name AS SplitByLabel," +
                            "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                            "from Mas_State " +
                            "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                            "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                            "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                            "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code AND  Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                            "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                            "GROUP by Mas_State.State_Code,Mas_State.StateName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Chemists.Chemists_Code)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.State_Code=Mas_State.State_Code " +
                        "LEFT JOIN Mas_Chemists ON Mas_Chemists.Chemists_Active_Flag = 0 AND Mas_Chemists.Division_Code='" + DivisionCode + "' AND Mas_Chemists.Sf_Code IN(" + Sfcodes + ") AND Mas_Chemists.Sf_Code = Mas_Salesforce.Sf_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName ";

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

        public DataSet getStockists()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "hq":
                    ViewByTitle = "HQs";

                    if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_stockist.territory AS LabelId," +
                        "Mas_stockist.territory AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.SF_Code + ',') > 0 " +
                        "GROUP BY Mas_stockist.territory,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_stockist.territory";
                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "SELECT " +
                        "Mas_stockist.territory AS LabelId," +
                        "Mas_stockist.territory AS Label," +
                        "Mas_stockist.State AS SplitById," +
                        "Mas_stockist.State AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_stockist.territory,Mas_stockist.State " +
                        "ORDER BY Mas_stockist.territory";
                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_stockist.territory AS LabelId," +
                        "Mas_stockist.territory AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_stockist.territory " +
                        "ORDER BY Mas_stockist.territory";
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
                case "subdivision":
                    ViewByTitle = "Sub Divisions";
                    if (WidgetDataInput.SplitBy == "hq")
                    {

                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId," +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_stockist.territory AS SplitById," +
                        "Mas_stockist.territory AS SplitByLabel," +
                        " Count(distinct(Mas_Stockist.Stockist_Code)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Stockist ON Mas_Stockist.Stockist_Active_Flag = 0  AND Mas_stockist.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.SF_Code + ',') > 0 " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND Mas_stockist.territory IS NOT NULL " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_stockist.territory ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId," +
                        "mas_subdivision.subdivision_name AS Label," +
                        "Mas_stockist.State AS SplitById," +
                        "Mas_stockist.State AS SplitByLabel," +
                        " Count(distinct(Mas_Stockist.Stockist_Code)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Stockist ON Mas_Stockist.Stockist_Active_Flag = 0  AND Mas_stockist.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.SF_Code + ',') > 0 " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND Mas_stockist.State IS NOT NULL " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_stockist.State,Mas_stockist.State  ";
                    }
                    else
                    {
                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId," +
                        " mas_subdivision.subdivision_name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        " Count(distinct(Mas_Stockist.Stockist_Code)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Stockist ON Mas_Stockist.Stockist_Active_Flag = 0  AND Mas_stockist.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.SF_Code + ',') > 0 " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name";
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
                case "state":
                    ViewByTitle = "States";
                    if (WidgetDataInput.SplitBy == "hq")
                    {

                        strQry = "SELECT " +
                        "Mas_stockist.State AS LabelId," +
                        "Mas_stockist.State AS Label," +
                        "Mas_stockist.territory AS SplitById," +
                        "Mas_stockist.territory AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_stockist.State,Mas_stockist.territory  " +
                        "ORDER BY Mas_stockist.State";


                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "SELECT " +
                        "Mas_stockist.State AS LabelId," +
                        "Mas_stockist.State AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_Salesforce.Sf_Code AS VARCHAR) + ',', ',' + Mas_Stockist.SF_Code + ',') > 0 " +
                        "GROUP BY Mas_stockist.State,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name " +
                        "ORDER BY Mas_stockist.State";
                    }
                    else
                    {
                        strQry = "SELECT " +
                        "Mas_stockist.State AS LabelId," +
                        "Mas_stockist.State AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_stockist.Stockist_Code)) AS Value " +
                        "FROM Mas_stockist " +
                        "WHERE Mas_Stockist.Stockist_Active_Flag = '0' AND Mas_stockist.Division_Code='" + DivisionCode + "' " +
                        "GROUP BY Mas_stockist.State  " +
                        "ORDER BY Mas_stockist.State";
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

        public DataSet getFieldForces()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "subdivision":
                    ViewByTitle = "Sub Divisions";
                    if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_name AS Label,Mas_State.State_code AS SplitById,Mas_State.Statename AS SplitByLabel, Count(distinct(Mas_Salesforce.Sf_Code)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.state_active_flag=0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.State_code =Mas_State.State_code AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0  " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name,Mas_State.State_code,Mas_State.Statename";
                    }
                    else
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_name AS Label,'' AS SplitById,'' AS SplitByLabel, Count(distinct(Mas_Salesforce.Sf_Code)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "WHERE mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_name";
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
                case "state":
                    ViewByTitle = "States";
                    if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_name AS SplitByLabel," +
                        " Count(distinct(Mas_Salesforce.Sf_Code)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode +" "+
                        "LEFT JOIN mas_subdivision ON mas_subdivision.Div_Code = " + DivisionCode + " AND mas_subdivision.SubDivision_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_name ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        " Count(distinct(Mas_Salesforce.Sf_Code)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.State_Code=Mas_State.State_Code AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName";
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

        public DataSet getProducts()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "category":
                    ViewByTitle = "Categories";

                    if (WidgetDataInput.SplitBy == "brand")
                    {

                        strQry = "select " +
                        "Mas_Product_Category.Product_Cat_Code AS LabelId," +
                        " Mas_Product_Category.Product_Cat_Name AS Label," +
                        "Mas_Product_Brand.Product_Brd_Code AS SplitById," +
                        "Mas_Product_Brand.Product_Brd_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Category " +
                        "LEFT JOIN Mas_Product_Brand ON Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code " +
                        "WHERE Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name,Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name ";
                    }
                    else if (WidgetDataInput.SplitBy == "group")
                    {

                        strQry = "select " +
                        "Mas_Product_Category.Product_Cat_Code AS LabelId," +
                        " Mas_Product_Category.Product_Cat_Name AS Label," +
                        "Mas_Product_Group.Product_Grp_Code AS SplitById," +
                        "Mas_Product_Group.Product_Grp_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Category " +
                        "LEFT JOIN Mas_Product_Group ON Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code " +
                        "WHERE Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name,Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name  ";
                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "select " +
                        "Mas_Product_Category.Product_Cat_Code AS LabelId," +
                        " Mas_Product_Category.Product_Cat_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_sname AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Category " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 " +
                        "WHERE Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname   ";
                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "select " +
                        "Mas_Product_Category.Product_Cat_Code AS LabelId," +
                        " Mas_Product_Category.Product_Cat_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Category " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 " +
                        "WHERE Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name,Mas_State.State_Code,Mas_State.StateName ";
                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_Product_Category.Product_Cat_Code AS LabelId," +
                        " Mas_Product_Category.Product_Cat_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Category " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON  Mas_Product_Detail.Division_code = " + DivisionCode + " AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code AND Mas_Product_Detail.Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name";
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

                case "brand":
                    ViewByTitle = "Brands";

                    if (WidgetDataInput.SplitBy == "category")
                    {

                        strQry = "select " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "Mas_Product_Category.Product_Cat_Code AS SplitById," +
                        "Mas_Product_Category.Product_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Brand " +
                        "LEFT JOIN Mas_Product_Category ON Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0" +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name,Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name ";
                    }
                    else if (WidgetDataInput.SplitBy == "group")
                    {
                        strQry = "select " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "Mas_Product_Group.Product_Grp_Code AS SplitById," +
                        "Mas_Product_Group.Product_Grp_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Brand " +
                        "LEFT JOIN Mas_Product_Group ON Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name,Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "select " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_sname AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Brand " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "select " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Brand " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                         "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                         "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_Product_Brand.Product_Brd_Code AS LabelId," +
                        "Mas_Product_Brand.Product_Brd_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Brand " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name";
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
                case "group":
                    ViewByTitle = "Groups";
                    if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "select " +
                        "Mas_Product_Group.Product_Grp_Code AS LabelId," +
                        "Mas_Product_Group.Product_Grp_Name AS Label," +
                        "Mas_Product_Category.Product_Cat_Code AS SplitById," +
                        "Mas_Product_Category.Product_Cat_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Group " +
                        "LEFT JOIN Mas_Product_Category ON Mas_Product_Category.Division_code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag = 0" +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code " +
                        "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "GROUP by  Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name,Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "brand")
                    {

                        strQry = "select " +
                        "Mas_Product_Group.Product_Grp_Code AS LabelId," +
                        "Mas_Product_Group.Product_Grp_Name AS Label," +
                        "Mas_Product_Brand.Product_Brd_Code AS SplitById," +
                        "Mas_Product_Brand.Product_Brd_Name AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Group " +
                        "LEFT JOIN Mas_Product_Brand ON Mas_Product_Brand.Division_code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag = 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code " +
                        "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "GROUP by  Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name,Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name ";

                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {
                        strQry = "select " +
                        "Mas_Product_Group.Product_Grp_Code AS LabelId," +
                        "Mas_Product_Group.Product_Grp_Name AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_sname AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Group " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 " +
                        "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "GROUP by  Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name,mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname ";

                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {

                        strQry = "select " +
                        "Mas_Product_Group.Product_Grp_Code AS LabelId," +
                        "Mas_Product_Group.Product_Grp_Name AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Group " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 " +
                        "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "GROUP by  Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_Product_Group.Product_Grp_Code AS LabelId," +
                        "Mas_Product_Group.Product_Grp_Name AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_Product_Group " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND  Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE Mas_Product_Group.Division_code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag = 0 " +
                        "GROUP by Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name";
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
                case "subdivision":
                    ViewByTitle = "Sub Divisions";
                    if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_sname AS Label,Mas_Product_Category.Product_Cat_Code AS SplitById,Mas_Product_Category.Product_Cat_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Product_Category ON Mas_Product_Category.Division_Code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag=0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code " +
                        "WHERE mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname,Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "brand")
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_sname AS Label,Mas_Product_Brand.Product_Brd_Code AS SplitById,Mas_Product_Brand.Product_Brd_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Product_Brand ON Mas_Product_Brand.Division_Code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag=0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code " +
                        "WHERE mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname,Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "group")
                    {
                        strQry = "select mas_subdivision.subdivision_code AS LabelId, mas_subdivision.subdivision_sname AS Label,Mas_Product_Group.Product_Grp_Code AS SplitById,Mas_Product_Group.Product_Grp_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Product_Group ON Mas_Product_Group.Division_Code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag=0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0 AND Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code " +
                        "WHERE mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname,Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "state")
                    {
                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId," +
                        " mas_subdivision.subdivision_sname AS Label," +
                        "Mas_State.State_Code AS SplitById," +
                        "Mas_State.StateName AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_State ON Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 " +
                        "WHERE mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname,Mas_State.State_Code,Mas_State.StateName ";

                    }
                    else
                    {
                        strQry = "select " +
                        "mas_subdivision.subdivision_code AS LabelId," +
                        " mas_subdivision.subdivision_sname AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from mas_subdivision " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' " +
                        "GROUP by mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname";
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
                case "state":
                    ViewByTitle = "States";

                    if (WidgetDataInput.SplitBy == "category")
                    {
                        strQry = "select Mas_State.State_Code AS LabelId, Mas_State.StateName AS Label,Mas_Product_Category.Product_Cat_Code AS SplitById,Mas_Product_Category.Product_Cat_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Product_Category ON Mas_Product_Category.Division_Code = " + DivisionCode + " AND Mas_Product_Category.Product_Cat_Active_Flag=0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND Mas_Product_Detail.Product_Cat_Code =Mas_Product_Category.Product_Cat_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Product_Category.Product_Cat_Code,Mas_Product_Category.Product_Cat_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "brand")
                    {
                        strQry = "select Mas_State.State_Code AS LabelId, Mas_State.StateName AS Label,Mas_Product_Brand.Product_Brd_Code AS SplitById,Mas_Product_Brand.Product_Brd_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Product_Brand ON Mas_Product_Brand.Division_Code = " + DivisionCode + " AND Mas_Product_Brand.Product_Brd_Active_Flag=0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND Mas_Product_Detail.Product_Brd_Code =Mas_Product_Brand.Product_Brd_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Product_Brand.Product_Brd_Code,Mas_Product_Brand.Product_Brd_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "group")
                    {
                        strQry = "select Mas_State.State_Code AS LabelId, Mas_State.StateName AS Label,Mas_Product_Group.Product_Grp_Code AS SplitById,Mas_Product_Group.Product_Grp_Name AS SplitByLabel, Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Product_Group ON Mas_Product_Group.Division_Code = " + DivisionCode + " AND Mas_Product_Group.Product_Grp_Active_Flag=0 " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  AND Mas_Product_Detail.Product_Grp_Code =Mas_Product_Group.Product_Grp_Code " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,Mas_Product_Group.Product_Grp_Code,Mas_Product_Group.Product_Grp_Name";
                    }
                    else if (WidgetDataInput.SplitBy == "subdivision")
                    {

                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "mas_subdivision.subdivision_code AS SplitById," +
                        "mas_subdivision.subdivision_sname AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName,mas_subdivision.subdivision_code,mas_subdivision.subdivision_sname ";

                    }
                    else
                    {
                        strQry = "select " +
                        "Mas_State.State_Code AS LabelId," +
                        "Mas_State.StateName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        "Count(distinct(Mas_Product_Detail.Prod_Detail_Sl_No)) AS Value " +
                        "from Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Salesforce ON  Mas_Salesforce.Division_Code like '%" + DivisionCode + ",%' AND Mas_Salesforce.sf_TP_Active_Flag = '0' AND Mas_Salesforce.Sf_Code IN(" + Sfcodes + ") " +
                        "LEFT JOIN mas_subdivision ON  mas_subdivision.Div_Code ='" + DivisionCode + "' AND mas_subdivision.SubDivision_Active_Flag = '0' AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Salesforce.subdivision_code + ',') > 0 " +
                        "LEFT JOIN Mas_Product_Detail ON Mas_Product_Detail.Division_Code =" + DivisionCode + " AND Product_Active_Flag ='0'  AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Product_Detail.state_code + ',') > 0 AND CHARINDEX(',' + CAST(mas_subdivision.subdivision_code AS VARCHAR) + ',', ',' + Mas_Product_Detail.subdivision_code + ',') > 0  " +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP by Mas_State.State_Code,Mas_State.StateName";
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

        public DataSet getHolidays()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "state":
                    ViewByTitle = "States";
                    strQry = "SELECT " +
                        "Mas_State.State_Code AS LabelId," +
                        " Mas_State.StateName AS Label," +
                        "'' AS SplitById," +
                        "'' AS SplitByLabel," +
                        " Count(Mas_Statewise_Holiday_Fixation.Sl_No) AS Value " +
                        "FROM Mas_State " +
                        "LEFT JOIN mas_division ON mas_division.Division_Code = " + DivisionCode + " " +
                        "LEFT JOIN Mas_Statewise_Holiday_Fixation ON Mas_Statewise_Holiday_Fixation.Division_Code='" + DivisionCode + "' AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + Mas_Statewise_Holiday_Fixation.state_code + ',') >0 AND Mas_Statewise_Holiday_Fixation.Academic_Year=year(getDate())" +
                        "WHERE Mas_State.State_Active_Flag = 0 AND CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0 " +
                        "GROUP BY Mas_State.State_Code,Mas_State.StateName  " +
                        "ORDER BY Mas_State.StateName";
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

        public DataSet getSpecialities()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            switch (WidgetDataInput.ViewBy)
            {
                case "category":
                    ViewByTitle = "Categories";
                    strQry = "SELECT Mas_Doctor_Category.Doc_Cat_Name AS Label,'' AS SplitById,'' AS SplitByLabel,  COUNT(DISTINCT Mas_Doctor_Speciality.Doc_Special_Code) AS Value FROM Mas_Doctor_Category  LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "'  AND Mas_ListedDr.listeddr_active_flag='0' AND Mas_Doctor_Category.Doc_Cat_Code = Mas_ListedDr.Doc_Cat_Code LEFT JOIN Mas_Doctor_Speciality  ON Mas_Doctor_Speciality.Doc_Special_Active_Flag='0' AND Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code WHERE Mas_Doctor_Category.Division_Code='" + DivisionCode + "' AND Mas_Doctor_Category.Doc_Cat_Active_Flag='0'  GROUP BY Mas_Doctor_Category.Doc_Cat_Name ORDER BY Mas_Doctor_Category.Doc_Cat_Name";
                    try
                    {
                        result = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case "class":
                    ViewByTitle = "Classes";
                    strQry = "SELECT Mas_Doc_Class.Doc_ClsName AS Label,'' AS SplitById,'' AS SplitByLabel,  COUNT(DISTINCT Mas_Doctor_Speciality.Doc_Special_Code) AS Value FROM Mas_Doc_Class  LEFT JOIN Mas_ListedDr ON Mas_ListedDr.Sf_Code IN(" + Sfcodes + ") AND Mas_ListedDr.Division_Code='" + DivisionCode + "' AND Mas_Doc_Class.Doc_ClsCode = Mas_ListedDr.Doc_ClsCode LEFT JOIN Mas_Doctor_Speciality  ON Mas_Doctor_Speciality.Division_Code='" + DivisionCode + "' AND Mas_ListedDr.Doc_Special_Code = Mas_Doctor_Speciality.Doc_Special_Code GROUP BY Mas_Doc_Class.Doc_ClsName ORDER BY Mas_Doc_Class.Doc_ClsName";
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
    }
}
