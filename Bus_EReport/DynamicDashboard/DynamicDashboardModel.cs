using DBase_EReport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_EReport.DynamicDashboard
{
    public class DynamicDashboardModel
    {
        private String strQry = string.Empty;

        public DataSet GetDashboards(int DivisionCode,int Id =0,string Module ="",bool Single=false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT ";
            if(Single == true)
            {
                strQry += " TOP(1) ";
            }
            strQry +=" Id,Name,Module,Widgets FROM Dynamic_Dashboards WHERE DivisionCode="+ DivisionCode;
            if(Id > 0)
            {
                strQry += "  AND Id=" + Id;
            }
            if (Module !="")
            {
                strQry += "  AND Module='" + Module+"'";
            }
            strQry += " Order by id desc";
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

        public int SaveDashboard(DynamicDashboardFormModel Data, string SFcode,int DivisionCode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int Id = Data.Id;
            if (Id == 0)
            {
                strQry = "INSERT INTO Dynamic_Dashboards (Name, Module, SFCode,DivisionCode) OUTPUT INSERTED.Id VALUES('" + Data.Name + "', '" + Data.Module + "', '" + SFcode + "'," + DivisionCode + "); ";
                try
                {
                    Id = db_ER.Exec_Scalar(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                strQry = "UPDATE Dynamic_Dashboards SET Name ='" + Data.Name + "' WHERE  Id='" + Id + "';";
                try
                {
                    db_ER.Exec_Scalar(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Id;
        }

        public void SaveWidgets(int Id, string Widgets)
        {
            DB_EReporting db_ER = new DB_EReporting();
            strQry = "UPDATE Dynamic_Dashboards SET Widgets ='" + Widgets +"' WHERE  Id='" + Id + "';";
            try
            {
                db_ER.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDashboardNameUnique(DynamicDashboardFormModel FormData,int DivisionCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            if (FormData.Id == 0)
            {
                strQry = "SELECT COUNT(*) FROM Dynamic_Dashboards WHERE Name = '" + FormData.Name + "' AND DivisionCode = " + DivisionCode + " AND Module = '" + FormData.Module + "'";
            }
            else
            {
                strQry = "SELECT COUNT(*) FROM Dynamic_Dashboards WHERE Name = '" + FormData.Name + "' AND DivisionCode = " + DivisionCode + " AND Module = '" + FormData.Module + "' AND Id != " + FormData.Id;
            }

            try
            {
                int count = Convert.ToInt32(db_ER.Exec_Scalar(strQry));
                return count == 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDashboard(int Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            strQry = "Delete Dynamic_Dashboards WHERE  Id='" + Id + "';";
            try
            {
                db_ER.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetBrands(int DivisionCode,bool ActiveOnly=false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Product_Brd_Code AS Id,Product_Brd_Name AS Name,Product_Brd_Active_Flag AS Inactive FROM Mas_Product_Brand Where Division_code =" + DivisionCode + " ";
            if(ActiveOnly == true)
            {
                strQry += " AND Product_Brd_Active_Flag =0 ";
            }
            strQry += " Order by Product_Brd_Name Asc";
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

        public DataSet GetDoctors(int DivisionCode, bool ActiveOnly=false) 
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT ListedDrCode As Id,ListedDr_Name AS Name,listeddr_active_flag AS Inactive FROM Mas_ListedDr Where Division_Code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND listeddr_active_flag =0 ";
            }
            strQry += " Order by ListedDr_Name Asc";

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

        public DataSet GetProducts(int DivisionCode,bool ActiveOnly=false, int top=0)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT ";
            if(top != 0)
            {
                strQry += " top " + top.ToString()+" ";
            }
            strQry += " Product_Code_SlNo AS Id,Product_Detail_Name As Name,Product_Active_Flag As Inactive FROM Mas_Product_Detail Where Division_code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND Product_Active_Flag =0 ";
            }
            strQry += " Order by Product_Detail_Name Asc";
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

        public DataSet GetGroups(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Product_Grp_Code AS Id,Product_Grp_Name AS Name,Product_Grp_Active_Flag AS Inactive FROM Mas_Product_Group Where Division_code='" + DivisionCode + "' ";
            if (ActiveOnly == true)
            {
                strQry += " AND Product_Grp_Active_Flag =0 ";
            }
            strQry += " Order by Product_Grp_Name Asc";
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

        public DataSet GetSubCategories(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Doc_SubCatCode AS Id,Doc_SubCatName AS Name,Doc_SubCat_ActiveFlag AS Inactive FROM Mas_Doc_SubCategory Where Division_code='" + DivisionCode + "' ";
            if (ActiveOnly == true)
            {
                strQry += " AND Doc_SubCat_ActiveFlag =0 ";
            }
            strQry += " Order by Doc_SubCatName Asc";
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

        public DataSet GetSpecialities(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Doc_Special_Code AS Id,Doc_Special_Name AS Name,Doc_Special_Active_Flag AS Inactive FROM Mas_Doctor_Speciality Where Division_code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND Doc_Special_Active_Flag =0 ";
            }
            strQry += " Order by Doc_Special_Name Asc";

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

        public DataSet GetCategories(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Doc_Cat_Code AS Id,Doc_Cat_Name AS Name,Doc_Cat_Active_Flag AS Inactive FROM Mas_Doctor_Category Where Division_code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND Doc_Cat_Active_Flag =0 ";
            }
            strQry += " Order by Doc_Cat_Name Asc";

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
        public DataSet GetProductCategories(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT Product_Cat_Code AS Id,Product_Cat_Name AS Name,Product_Cat_Active_Flag AS Inactive FROM Mas_Product_Category Where Division_code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND Product_Cat_Active_Flag =0 ";
            }
            strQry += " Order by Product_Cat_Name Asc";

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

        public DataSet GetHQs(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT SF_Cat_Code AS Id,Approved_By AS Name,0 AS Inactive FROM mas_salesforce Where CHARINDEX('," + DivisionCode + ",', ',' + division_Code + ',') > 0 AND Approved_By<>'' ";
            strQry += " GROUP BY SF_Cat_Code,Approved_By Order by SF_Cat_Code Asc ";

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


        public DataSet GetStates(int DivisionCode, bool ActiveOnly = false)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet result = null;

            strQry = "SELECT mas_state.State_Code AS Id,mas_state.StateName AS Name,State_Active_Flag AS Inactive FROM mas_division INNER JOIN mas_state ON CHARINDEX(',' + CAST(Mas_State.State_Code AS VARCHAR) + ',', ',' + mas_division.State_Code + ',') > 0  Where mas_division.Division_code='" + DivisionCode + "' ";

            if (ActiveOnly == true)
            {
                strQry += " AND State_Active_Flag =0 ";
            }
            strQry += " Order by mas_state.StateName Asc";

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

        public List<DynamicDashboardOptionsViewModal> DataSetToOptionsView(DataSet Data)
        {
            List<DynamicDashboardOptionsViewModal> dynamicDashboardOptionsViewModals = new List<DynamicDashboardOptionsViewModal>();
            if (Data.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow Row in Data.Tables[0].Rows)
                {
                    DynamicDashboardOptionsViewModal dynamicDashboardOptionsViewModal = new DynamicDashboardOptionsViewModal();
                    dynamicDashboardOptionsViewModal.Id = Row["Id"].ToString();
                    dynamicDashboardOptionsViewModal.Name = Row["Name"].ToString();
                    dynamicDashboardOptionsViewModal.Inactive = Row["Inactive"].ToString();

                    dynamicDashboardOptionsViewModals.Add(dynamicDashboardOptionsViewModal);
                }
            }
            return dynamicDashboardOptionsViewModals;
        }

    }


    public class DynamicDashboardFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string Widgets { get; set; }
        public Dictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();
    }

    public class DynamicDashboardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public string Widgets { get; set; }
    }

    public class WidgetDataInputModal
    {
        public string Module { get; set; }
        public string MeasureBy { get; set; }
        public string ViewBy { get; set; }
        public string SplitBy { get; set; }
        public string SFCode { get; set; }
        public Dictionary<string, string> Filters { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> WidgetFiters { get; set; } = new Dictionary<string, string>();
    }


    public class WidgetDataOutputModal
    {
        public List<string> Labels { get; set; }
        public List<string> LabelIds { get; set; }
        public List<WidgetSeriesModal> Series { get; set; }
        public List<string> SeriesIds { get; set; }
        public string MeasureByTitle { get; set; }
        public string ViewByTitle { get; set; }
    }

    public class WidgetSeriesModal
    {
        public string name { get; set; }
        public List<int> data { get; set; }
    }

    public class DynamicDashboardOptionsViewModal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Inactive { get; set; }

    }


}
