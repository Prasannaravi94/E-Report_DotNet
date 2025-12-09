using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Division
    {
        private string strQry = string.Empty;

        public DataSet getLocation()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsLocation = null;
            strQry = "SELECT state_code,statename,shortname FROM mas_state WHERE state_active_flag=0" +
                " order by 2";
            try
            {
                dsLocation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLocation;
        }
        public DataSet getmenutype(string div_code, string menuType)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsdiv = null;


            string strQry = " SELECT   PK_Id,Division_Code,  CASE WHEN Menu_Type = 'R' THEN 'Report' WHEN Menu_Type = 'M' THEN 'Menu' ELSE Menu_Type  " +
             " END AS Menu_Type, Menu_Name  FROM Tbl_DynamicMenuCreation WHERE Is_Active = 0 AND Division_Code = '" + div_code + "'";


            if (!string.IsNullOrEmpty(menuType))
            {
                strQry += " AND Menu_Type = '" + menuType + "'";
            }

            try
            {

                dsdiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dsdiv;
        }
        public DataSet getDivisionHO_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " SELECT Division_Code,Division_Name,Division_SName,Division_Add2" +
                     " FROM mas_division " +
                     " WHERE Division_Code= '" + div_code + "' and division_active_flag=0 ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }


        public DataSet getStatePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getModePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT '---Select---' as Product_Mode " +
                         " UNION " +
                "select distinct Product_Mode from Mas_Product_Detail where division_code='" + div_code + "'" +
                       "and Product_Active_Flag = 0";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getDivision_Report()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT 0 Division_Code, '---All Division---' Division_Name UNION ALL " +
                     " SELECT Division_Code,Division_Name" +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY 2";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public int MenuDeActivate(string PK_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Tbl_DynamicMenuCreation " +
                            " SET Is_Active=1  " +
                          " WHERE PK_Id = '" + PK_Id + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getmenu(string div_code, string menutype, string menuName)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsdiv = null;


            string strQry = "SELECT PK_Id " +
                "FROM Tbl_DynamicMenuCreation " +
                "WHERE Division_Code = '" + div_code + "' " +
                "AND Menu_Type = '" + menutype + "' " +
                "AND Menu_Name = '" + menuName + "';";
            try
            {
                dsdiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dsdiv;
        }
        public DataSet getmenuFkpkid(string div_code, string PK_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsdiv1 = null;

            string strQry = "SELECT OptionMenu_Id " +
                            "FROM Tbl_DynamicOptionMenuCreation " +
                            "WHERE FK_PK_Id = '" + PK_Id + "'";

            try
            {
                dsdiv1 = db_ER.Exec_DataSet(strQry);


                if (dsdiv1 == null)
                {
                    Console.WriteLine("Error: DataSet is null.");
                }
                else if (dsdiv1.Tables.Count == 0 || dsdiv1.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("Error: No rows returned.");
                }
                else
                {
                    Console.WriteLine("DataSet successfully retrieved.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dsdiv1;
        }
        public int GetOptionUpdateMenuData(string div_code, string PK_Id, string optionmenuid, string Menu_Name, string path, string orderBy, string menuIcon)
        {
            int irReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                string checkQuery = "SELECT COUNT(*) FROM Tbl_DynamicOptionMenuCreation WHERE OptionMenu_Id = '" + optionmenuid + "' AND FK_PK_Id = '" + PK_Id + "';";
                int recordExists = db.Exec_Scalar(checkQuery);


                if (recordExists > 0)
                {
                    string updateQry = "UPDATE Tbl_DynamicOptionMenuCreation SET " +
                                       "OptionMenu_Name = '" + Menu_Name + "', " +
                                       "OptionMenu_Page = '" + path + "', " +
                                       "OptionMenu_Position = '" + orderBy + "', " +
                                       "OptionMenu_Icon = '" + menuIcon + "' " +
                                       "WHERE FK_PK_Id = '" + PK_Id + "' AND OptionMenu_Id = '" + optionmenuid + "';";

                    irReturn = db.ExecQry(updateQry);
                }
                else
                {

                    string sQry1 = "SELECT ISNULL(MAX(OptionMenu_Id), 0) + 1 FROM Tbl_DynamicOptionMenuCreation WHERE FK_PK_Id = '" + PK_Id + "';";
                    int nextOptionMenuId = db.Exec_Scalar(sQry1);


                    string strQry = "INSERT INTO Tbl_DynamicOptionMenuCreation (FK_PK_Id, OptionMenu_Id, OptionMenu_Name, OptionMenu_Page, OptionMenu_Icon, OptionMenu_Position, Is_Active, Creation_Date) " +
                                    "VALUES ('" + PK_Id + "', '" + nextOptionMenuId + "', '" + Menu_Name + "', '" + path + "', '" + menuIcon + "', '" + orderBy + "', '0', GETDATE());";

                    irReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return irReturn;
        }
        public string GetNextOptionMenuId(string pkId)
        {
            DB_EReporting db = new DB_EReporting();
            string query = "SELECT ISNULL(MAX(OptionMenu_Id), 0) + 1 FROM Tbl_DynamicOptionMenuCreation WHERE FK_PK_Id = '" + pkId + "' ;";
            int nextOptionMenuId = db.Exec_Scalar(query);
            return nextOptionMenuId.ToString();
        }
        public bool CheckMenuNameExistsmenu(string menuName)
        {
            bool exists = false;
            DB_EReporting db = new DB_EReporting();

            try
            {

                string sCheckQry = "SELECT COUNT(1) FROM Tbl_DynamicMenuCreation WHERE Menu_Name = '" + menuName + "'AND Is_Active = '0'";
                int count = db.Exec_Scalar(sCheckQry);
                exists = count > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return exists;
        }
        public int GetOptionMenuData(string div_code, string PK_Id, string menuName1, string path, string orderBy, string menuIcon)
        {
            int irReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                string sQry1 = "SELECT ISNULL(MAX(OptionMenu_Id), 0) + 1 FROM Tbl_DynamicOptionMenuCreation WHERE FK_PK_Id = '" + PK_Id + "';";
                int slNo1 = db.Exec_Scalar(sQry1);


                string strQry = "INSERT INTO Tbl_DynamicOptionMenuCreation (FK_PK_Id, OptionMenu_Id, OptionMenu_Name, OptionMenu_Page, OptionMenu_Icon, OptionMenu_Position, Is_Active, Creation_Date) " +
                 "VALUES ('" + PK_Id + "', '" + slNo1 + "', '" + menuName1 + "', '" + path + "', '" + menuIcon + "', '" + orderBy + "', '0', GETDATE());";


                irReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return irReturn;
        }

        public DataSet getDivision()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT  convert(char(2),ROW_NUMBER() over(ORDER BY Division_City)) +' '+ Division_Name Division_Name,Division_Code,Division_City,Division_SName,Alias_Name " +
            //        " FROM mas_division " +
            //        " WHERE division_active_flag=0 " +
            //        " ORDER BY Division_City asc";
            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        // Sorting
        public DataTable getDivisionlist_DataTable()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDivision = null;
            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY 2";
            try
            {
                dtDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDivision;
        }
        public DataSet getDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT Division_SName, Division_Name, Division_Add1,  " +
                     " isnull(Division_Add2,'') Division_Add2, Division_City, Division_Pincode, " +
                     " isnull(Alias_Name,'') Alias_Name, State_Code, Year, WeekOff,convert(varchar(10),Bulk_Date,103)Bulk_Date " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " AND division_code = '" + div_code + "' ";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public bool RecordExist(string div_sname)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(division_code) FROM mas_division WHERE division_sname='" + div_sname + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool RecordExist(string div_code, string div_sname)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(division_code) FROM mas_division WHERE division_code != '" + div_code + "' AND division_sname='" + div_sname + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }


        public int RecordAdd(string div_name, string div_addr1, string div_addr2, string div_city, string div_pin, string div_state, string div_sname, string div_alias, string div_year, string div_weekoff, string Imple_Date)
        {
            int iReturn = -1;
            if (!RecordExist(div_sname))
            {
                try
                {

                    int div_code = -1;
                    int Div_Sl_No = -1;
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT CASE WHEN COUNT(division_code)>0 THEN MAX(division_code) ELSE 0 END FROM mas_division";
                    div_code = db.Exec_Scalar(strQry);
                    div_code += 1;
                    strQry = "SELECT CASE WHEN COUNT(Div_Sl_No)>0 THEN MAX(Div_Sl_No) ELSE 0 END FROM mas_division";
                    Div_Sl_No = db.Exec_Scalar(strQry);
                    Div_Sl_No += 1;
                    strQry = "INSERT INTO mas_division(Division_Code, Division_Name,Division_Add1,Division_Add2,Division_City," +
                                " Division_Pincode, State_Code,Division_SName,Division_Active_Flag,Alias_Name,Year,WeekOff,Created_Date,LastUpdt_Date,Div_Sl_No,Bulk_Date) " +
                                " values(" + div_code + ", '" + div_name + "', '" + div_addr1 + "', '" + div_addr2 + "', '" + div_city + "', '" + div_pin + "', " +
                                " '" + div_state + "', '" + div_sname + "', 0, '" + div_alias + "', '" + div_year + "', '" + div_weekoff + "',getdate(),getdate(),'" + Div_Sl_No + "','" + Imple_Date + "') ";


                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }

        public DataSet get_App_versionNew_Add(string div_code, string sf_code)
        {
            
                DB_EReporting db_ER = new DB_EReporting();

                DataSet dsDivision = null;
                strQry = "EXEC get_App_versionNew_Add '" + div_code + "','" + sf_code + "'";
                try
                {
                    dsDivision = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsDivision;
            
        }

        public int RecordUpdate(string div_code, string div_name, string div_city, string div_sname, string div_alias)
        {
            int iReturn = -1;
            //if (!RecordExist(div_code, div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                         " SET division_name = '" + div_name + "', " +
                         " division_city = '" + div_city + "', " +
                         //" division_sname = '" + div_sname + "', " +
                         " Alias_Name = '" + div_alias + "', " +
                         //  " Div_Sl_No =" + txtNewSlNo + ", " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }


        public int RecordUpdate(string div_code, string div_name, string div_add1, string div_add2, string div_city, string div_pin, string div_state, string div_sname, string div_alias, string div_year, string div_weekoff, string Imple_Date)
        {
            int iReturn = -1;
            if (!RecordExist(div_code, div_sname))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE mas_division " +
                             " SET division_name = '" + div_name + "', " +
                             " division_add1 = '" + div_add1 + "', " +
                             " division_add2 = '" + div_add2 + "', " +
                             " division_city = '" + div_city + "', " +
                             " division_pincode = '" + div_pin + "', " +
                             " state_code = '" + div_state + "', " +
                             " division_sname = '" + div_sname + "', " +
                             " alias_name = '" + div_alias + "',  " +
                             " Year = '" + div_year + "',  " +
                             " WeekOff = '" + div_weekoff + "' , " +
                             " Bulk_Date = '" + Imple_Date + "' , " +
                             " LastUpdt_Date = getdate() " +
                             " WHERE division_code = '" + div_code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        public int DeActivate(string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                            " SET division_active_flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int DeActivate_New(string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                            " SET division_active_flag=1 , Standby=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getDivision_list()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT  convert(char(2),ROW_NUMBER() over(ORDER BY Division_Name)) +' '+ Division_Name Division_Name,Division_Code,Division_City,Division_SName,Alias_Name " +
            //        " FROM mas_division " +
            //        " WHERE division_active_flag=0 " +
            //        " ORDER BY Division_Name asc";
            strQry = "SELECT convert(varchar(10),Div_Sl_No)+'.' +' '+ Division_Name +' (' + Division_SName +') '  as Division_Name, Division_Code,Division_City,Division_SName,Alias_Name " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet Division_State(int div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDivision = null;

            strQry = "EXEC [DivisionState] '" + div_code + "'";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        //saravana changes
        public DataSet getTPviewDivision()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " Select '0' as Division_Code,'---Select---' as Division_Name,'' as Division_City,'' as Division_SName,'' as Alias_Name " +
                     " Union all " +
                     " SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY 2";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        //changes done by Priya
        public DataSet getDivision_Name()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "Select '0' as Division_Code,'--Select--' as Division_Name,'' as Div_Sl_No " +
            //             " Union all" +
            strQry = " SELECT Division_Code,Division_Name,Div_Sl_No " +
              " FROM mas_division " +
              " WHERE division_active_flag=0 " +
              " order by Div_Sl_No";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        //Created a new function to fetch the divisions for the fieldforce by Devi on 02/06/15
        public DataSet getDivision_List(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Division_Code from Mas_Salesforce " +
                     " WHERE sf_code = '" + sf_code + "' ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getDivision_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT Division_Name,division_code " +
                     " FROM mas_division " +
                     " WHERE division_code = '" + div_code + "' ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getMailDivision(string Division_Code) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (Division_Code.Contains(','))
                Division_Code = Division_Code.Substring(0, Division_Code.Length - 1);

            strQry = "Select '0' as sf_code,'---Select Clear---' as sf_name " +
                     "Union All " +
                     "SELECT Division_Code as sf_code,Division_Name as sf_name" +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 and Division_Code ='" + Division_Code + "' " +
                     " ORDER BY 2";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        // Changes done by Saravanan
        public DataSet getMultiDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "Select '0' as Division_Code,'--Select--' as Division_Name,'' as Div_Sl_No " +
                         " Union all" +
                         " SELECT Division_Code,Division_Name,Div_Sl_No " +
                         " FROM mas_division " +
                         " WHERE division_active_flag=0 and Division_Code in (" + div_code + ") " +
                         " order by Div_Sl_No";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        //Changes done by Priya
        public int getProCat_newdivision(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDivision(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(Product_Cat_Code)+1,'1') Product_Cat_Code from Mas_Product_Category ";
                    int Product_Cat_Code = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Product_Category(Product_Cat_Code,Division_Code,Product_Cat_SName,Product_Cat_Name,Created_Date,Product_Cat_Active_Flag) " +
                             " select '" + Product_Cat_Code + "' - 1 + row_number() over (order by (select NULL)) as Product_Cat_Code,'" + divcode + "' Division_Code,Product_Cat_SName,Product_Cat_Name,getdate() as Created_Date,Product_Cat_Active_Flag from Mas_Product_Category where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }

        public bool sRecordExistDivision(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Product_Category where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int getProCat_newGroup(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistGroup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(Product_Grp_Code)+1,'1') Product_Grp_Code from Mas_Product_Group ";
                    int Product_Grp_Code = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Product_Group(Product_Grp_Code,Division_Code,Product_Grp_SName,Product_Grp_Name,Created_Date,Product_Grp_Active_Flag) " +
                             " select '" + Product_Grp_Code + "' - 1 + row_number() over (order by (select NULL)) as Product_Grp_Code, '" + divcode + "' Division_Code,Product_Grp_SName,Product_Grp_Name,getdate() as Created_Date,Product_Grp_Active_Flag from Mas_Product_Group where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }

        public bool sRecordExistGroup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Product_Group where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int getNew_Doc_Category(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDocCat(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(Doc_Cat_Code)+1,'1') Doc_Cat_Code from Mas_Doctor_Category ";
                    int Doc_Cat_Code = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Doctor_Category(Doc_Cat_Code,Division_Code,Doc_Cat_SName,Doc_Cat_Name,Created_Date,Doc_Cat_Active_Flag,No_of_visit) " +
                             " select '" + Doc_Cat_Code + "' - 1 + row_number() over (order by (select NULL)) as Doc_Cat_Code,'" + divcode + "' Division_Code,Doc_Cat_SName,Doc_Cat_Name,getdate() as Created_Date,Doc_Cat_Active_Flag,No_of_visit from Mas_Doctor_Category where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistDocCat(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Doctor_Category where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int getNew_DocSpec(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDocSpec(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(Doc_Special_Code)+1,'1') Doc_Special_Code from Mas_Doctor_Speciality ";
                    int Doc_Special_Code = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Doctor_Speciality(Doc_Special_Code,Division_Code,Doc_Special_SName,Doc_Special_Name,Created_Date,Doc_Special_Active_Flag,No_of_visit) " +
                             " select '" + Doc_Special_Code + "' - 1 + row_number() over (order by (select NULL)) as Doc_Special_Code ,'" + divcode + "' Division_Code,Doc_Special_SName,Doc_Special_Name,getdate() as Created_Date,Doc_Special_Active_Flag,No_of_visit from Mas_Doctor_Speciality where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistDocSpec(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Doctor_Speciality where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int getNew_DocQua(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDocQua(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();


                    strQry = "SELECT isnull(max(Doc_QuaCode)+1,'1') Doc_QuaCode from Mas_Doc_Qualification ";
                    int Doc_QuaCode = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Created_Date,Doc_Qua_ActiveFlag) " +
                             " select '" + Doc_QuaCode + "' - 1 + row_number() over (order by (select NULL)) as  Doc_QuaCode,'" + divcode + "' Division_Code,Doc_QuaSName,Doc_QuaName,getdate() as Created_Date,Doc_Qua_ActiveFlag from Mas_Doc_Qualification where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistDocQua(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Doc_Qualification where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int getNew_Doc_Cls(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDocCls(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(Doc_ClsCode)+1,'1') Doc_ClsCode from Mas_Doc_Class ";
                    int Doc_ClsCode = db.Exec_Scalar(strQry);

                    strQry = " insert into Mas_Doc_Class(Doc_ClsCode,Division_Code,Doc_ClsSName,Doc_ClsName,Created_Date,Doc_Cls_ActiveFlag,No_of_visit) " +
                             " select  '" + Doc_ClsCode + "' - 1 + row_number() over (order by (select NULL)) as  Doc_ClsCode, '" + divcode + "' Division_Code,Doc_ClsSName,Doc_ClsName,getdate() as Created_Date,Doc_Cls_ActiveFlag,No_of_visit from Mas_Doc_Class where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistDocCls(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Doc_Class where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int getNew_ProductDet(string divcode)
        {
            int iReturn = -1;
            DataSet dsDivision = null;
            int icodeSlNo = -1;
            if (!sRecordExistProdDet(divcode))
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = " insert into Mas_Product_Detail(Division_Code,Product_Detail_Name,Product_Sale_Unit,Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Code,Product_Type_Code,Product_Description,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,State_Code,subdivision_code,) " +
                             " select '" + divcode + "' Division_Code,Product_Detail_Name,Product_Sale_Unit,Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Code,Product_Type_Code,Product_Description,getdate() as Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,State_Code,subdivision_code from Mas_Product_Detail where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);

                    // Added by Sridevi - TO update unique prod code slno number
                    strQry = "select Product_Detail_Code from Mas_Product_Detail where division_code = '" + divcode + "' order by Prod_Detail_Sl_No";

                    dsDivision = db.Exec_DataSet(strQry);

                    if (dsDivision.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                        {
                            strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_Product_Detail";
                            icodeSlNo = db.Exec_Scalar(strQry);

                            strQry = "UPDATE Mas_Product_Detail SET product_code_slno = " + icodeSlNo + " where Product_Detail_Code = '" + drFF["Product_Detail_Code"].ToString() + "'";

                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            return iReturn;

        }
        public bool sRecordExistProdDet(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Product_Detail where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int getNew_adm_Setup(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistadm_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    //strQry = " insert into Mas_Doctor_Category(Division_Code,Doc_Cat_SName,Doc_Cat_Name,Created_Date,Doc_Cat_Active_Flag,No_of_visit) " +
                    //         " select '" + divcode + "' Division_Code,Doc_Cat_SName,Doc_Cat_Name,getdate() as Created_Date,Doc_Cat_Active_Flag,No_of_visit from Mas_Doctor_Category where Division_Code=1 " +
                    //         " ORDER BY 2";

                    strQry = "insert into Admin_Setups(Division_Code,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,wrk_area_Name,wrk_area_SName, DCRLDR_Remarks, DCRNew_Chem, DCRNew_ULDr, LastUpdt_DCRStp, Doc_App_Needed, Doc_Deact_Needed, Add_Deact_Needed) " +
             " select '" + divcode + "' Division_Code,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,1,wrk_area_Name,wrk_area_SName, DCRLDR_Remarks, DCRNew_Chem, DCRNew_ULDr, getdate(),1, 1, 1 from Admin_Setups where Division_Code=1";


                    iReturn = db.ExecQry(strQry);

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (!sRecordExistadmMgr_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    //strQry = " insert into Mas_Doctor_Category(Division_Code,Doc_Cat_SName,Doc_Cat_Name,Created_Date,Doc_Cat_Active_Flag,No_of_visit) " +
                    //         " select '" + divcode + "' Division_Code,Doc_Cat_SName,Doc_Cat_Name,getdate() as Created_Date,Doc_Cat_Active_Flag,No_of_visit from Mas_Doctor_Category where Division_Code=1 " +
                    //         " ORDER BY 2";

                    strQry = "insert into Admin_Setups_MGR(Division_Code,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,wrk_area_Name,DCRLDR_Remarks, DCRNew_Chem, DCRNew_ULDr, LastUpdt_DCRStp) " +
             " select '" + divcode + "' Division_Code,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,1,wrk_area_Name, DCRLDR_Remarks, DCRNew_Chem, DCRNew_ULDr, getdate() from Admin_Setups_MGR where Division_Code=1";


                    iReturn = db.ExecQry(strQry);

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (!sRecordExistLeave_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "insert into mas_Leave_Type(Division_Code, Leave_SName, Leave_Name, Active_Flag,Created_Date)" +
                              " select '" + divcode + "' Division_Code, Leave_SName, Leave_Name, Active_Flag,getdate() as Created_Date from mas_Leave_Type where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            if (!sRecordExistWork_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(WorkType_Code_B)+1,'1') WorkType_Code_B from Mas_WorkType_BaseLevel ";
                    int WorkType_Code_B = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_WorkType_BaseLevel(WorkType_Code_B,Division_Code,Worktype_Name_B,RmksNeed,ExpNeed,active_flag,WType_Flag,WorkType_Orderly,WType_SName,TP_Flag,TP_DCR,Place_Involved,Expense_Type,Button_Access,FieldWork_Indicator)" +
                             " select '" + WorkType_Code_B + "' - 1 + row_number() over (order by (select NULL)) as WorkType_Code_B, '" + divcode + "' Division_Code, Worktype_Name_B,RmksNeed,ExpNeed,active_flag,WType_Flag,WorkType_Orderly,WType_SName,TP_Flag,TP_DCR,Place_Involved,Expense_Type,Button_Access,FieldWork_Indicator from Mas_WorkType_BaseLevel where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistWorkMgr_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(WorkType_Code_M)+1,'1') WorkType_Code_M from Mas_WorkType_Mgr ";
                    int WorkType_Code_M = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_WorkType_Mgr(WorkType_Code_M,Division_Code,Worktype_Name_M,RmksNeed,ExpNeed,active_flag,WType_Flag,WorkType_Orderly,WType_SName,TP_DCR,Place_Involved,Expense_Type,Button_Access,FieldWork_Indicator)" +
                             " select '" + WorkType_Code_M + "' - 1 + row_number() over (order by (select NULL)) as WorkType_Code_M, '" + divcode + "' Division_Code, Worktype_Name_M,RmksNeed,ExpNeed,active_flag,WType_Flag,WorkType_Orderly,WType_SName,TP_DCR,Place_Involved,Expense_Type,Button_Access,FieldWork_Indicator from Mas_WorkType_Mgr where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistMailFolder_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Mail_Folder_Name ";
                    int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Mail_Folder_Name(Move_MailFolder_Id,Division_Code,Move_MailFolder_Name)" +
                             " select '" + Move_MailFolder_Id + "' - 1 + row_number() over (order by (select NULL)) as Move_MailFolder_Id, '" + divcode + "' Division_Code, Move_MailFolder_Name from Mas_Mail_Folder_Name where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistProduct_Feed(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT isnull(max(FeedBack_Id)+1,'1') FeedBack_Id from Mas_Product_Feedback ";
                    int FeedBack_Id = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Product_Feedback(FeedBack_Id,FeedBack_Name,Division_Code)" +
                             " select '" + FeedBack_Id + "' - 1 + row_number() over (order by (select NULL)) as  FeedBack_Id, FeedBack_Name,'" + divcode + "' Division_Code from Mas_Product_Feedback where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistAccess_master(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();


                    strQry = "insert into Access_Master(company_code,mobile_access,computer_access ,TBase,GeoChk " +
                             ",UNLNeed,DrCap,ChmCap,StkCap,NLCap,ChmNeed ,StkNeed ,DPNeed,DINeed,CPNeed,CINeed,SPNeed " +
                             ",SINeed,NPNeed,NINeed,division_code,DrRxQCap,DrSmpQCap ,ChmQCap,StkQCap,NLRxQCap,NLSmpQCap,GEOTagNeed ,DisRad)" +
                             " select company_code,mobile_access,computer_access ,TBase,GeoChk " +
                             ",UNLNeed,DrCap,ChmCap,StkCap,NLCap,ChmNeed ,StkNeed ,DPNeed,DINeed,CPNeed,CINeed,SPNeed " +
                             ",SINeed,NPNeed,NINeed,'" + divcode + "' division_code,DrRxQCap,DrSmpQCap ,ChmQCap,StkQCap,NLRxQCap,NLSmpQCap,GEOTagNeed ,DisRad from Access_Master where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistDCRSetups(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();


                    strQry = "insert into DCRSetups(typ,Msl,Chm,Stk,Unl,Hos,Msl_v,Chm_v,Stk_v,Unl_v,Hos_v,Division_code,desig,desig_v)" +
                             " select typ,Msl,Chm,Stk,Unl,Hos,Msl_v,Chm_v,Stk_v,Unl_v,Hos_v,'" + divcode + "' Division_Code,desig,desig_v from DCRSetups where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            if (!sRecordExistHome_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "insert into Mas_Home_Page_Restrict(Division_Code,DCR_Home, TP_Home, Leave_Home, Expense_Home, Listeddr_Add_Home,Listeddr_Deact_Home, Listeddr_Add_Deact_Home,SS_Entry_Home, Doctor_Ser_Home, Created_Date)" +
                             " select '" + divcode + "' Division_Code, DCR_Home, TP_Home, Leave_Home, Expense_Home, Listeddr_Add_Home,Listeddr_Deact_Home, Listeddr_Add_Deact_Home,SS_Entry_Home, Doctor_Ser_Home, getdate() Created_Date from Mas_Home_Page_Restrict where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }

            if (!sRecordExistChem_Setup(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(Cat_Code)+1,'1') Cat_Code from Mas_Chemist_Category ";
                    int Cat_Code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Chemist_Category(Division_Code,Cat_Code, Chem_Cat_SName, Chem_Cat_Name, Chem_Cat_Active_Flag, Created_Date,LastUpdt_Date, Chem_Cat_Sl_No)" +
                             " select '" + divcode + "' Division_Code, '" + Cat_Code + "' - 1 + row_number() over (order by (select NULL)) as Cat_Code, Chem_Cat_SName, Chem_Cat_Name, Chem_Cat_Active_Flag, getdate() Created_Date,getdate() LastUpdt_Date, Chem_Cat_Sl_No from Mas_Chemist_Category where Division_Code = 1";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return iReturn;

        }
        public bool sRecordExistChem_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Chemist_Category where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistHome_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Home_Page_Restrict where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistMailFolder_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Mail_Folder_Name where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistAccess_master(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Access_Master where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistProduct_Feed(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Product_Feedback where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistDCRSetups(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from DCRSetups where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistadm_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Admin_Setups where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistadmMgr_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Admin_Setups_MGR where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistLeave_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from mas_Leave_Type where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistWork_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_WorkType_BaseLevel where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExistWorkMgr_Setup(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_WorkType_Mgr where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int getNew_Flash(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistFlash(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    int sl_no = -1;
                    strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Flash_News";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Mas_Flash_News(Division_Code,FN_Cont1,Created_Date,FN_Active_Flag,FNHome_Page_Flag) " +
                             " select '" + divcode + "' Division_Code,FN_Cont1,Created_Date,FN_Active_Flag,FNHome_Page_Flag from Mas_Flash_News where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistFlash(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_Flash_News where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        //changes done by RESHMI
        public int Update_DivisionSno(string Div_Sl_No, string div_code)
        {
            int iReturn = -1;
            //if (!sRecordExist(Div_Sl_No))
            //{

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Division " +
                         " SET Div_Sl_No = '" + Div_Sl_No + "', " +
                         " LastUpdt_Date = getdate()  WHERE division_code = '" + div_code + "'  ";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}


            return iReturn;


        }
        public bool sRecordExist(string Div_Sl_No)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Div_Sl_No) FROM Mas_Division WHERE Div_Sl_No='" + Div_Sl_No + "'";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        //Changes done by Reshmi

        public DataSet getDivision_React()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=1  " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public int Reactivate_Divi(int Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                            " SET division_active_flag=0, Standby=0 ," +
                            " LastUpdt_Date= getdate() " +
                            " WHERE Division_Code= '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getDiv()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division WHERE Division_Active_Flag=0 " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }

        //Added by Sri - 25-Aug-15
        public DataSet getHODivision(string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            //strQry = "SELECT a.Division_Code,a.Division_Name FROM Mas_Division a,Mas_HO_ID_Creation b" +
            //          " WHERE a.Division_Active_Flag=0 and  " +
            //          " ((b.Division_Code not like cast(a.Division_Code as varchar) +','+'%') and (b.Division_Code not like '%'+','+ cast(a.Division_Code as varchar) +','+'%'))";


            //strQry = "SELECT a.Division_Code,a.Division_Name FROM Mas_Division a,Mas_HO_ID_Creation b" +
            //          " WHERE a.Division_Active_Flag=0 and  " +
            //          " (cast(a.Division_Code as varchar) +','+'%' not like (select b.Division_Code from Mas_HO_ID_Creation b)) and " +
            //          " ('%'+','+ cast(a.Division_Code as varchar) +','+'%') not like (select c.Division_Code from Mas_HO_ID_Creation c)";
            strQry = "Select Division_Code " +
                        "From Mas_HO_ID_Creation WHERE HO_Active_Flag =0 and HO_ID ='" + HO_ID + "' ";



            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }


        //Added by Sri - 25-Aug-15
        public DataSet getDivisionHO(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " SELECT Division_Code,Division_Name,Division_SName,Alias_Name" +
                     " FROM mas_division " +
                     " WHERE Division_Code= '" + div_code + "' and division_active_flag=0 ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getDiv(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division " +
                     " WHERE Division_Active_Flag=0 " +
                     " AND Division_Code NOT IN (" + div_code + ") " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }

        public DataSet getDivEdit(string div_code, string Ho_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division " +
                     " WHERE Division_Active_Flag=0 " +
                     " AND Division_Code IN (" + div_code + ") " +
                     //    " AND Division_Code IN (" + Ho_Div + ") " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }

        public bool isHOExist()
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(HO_ID) FROM Mas_HO_ID_Creation";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }


        public DataSet getDivision_Hoadmin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT  convert(char(2),ROW_NUMBER() over(ORDER BY Division_City)) +' '+ Division_Name Division_Name,Division_Code,Division_City,Division_SName,Alias_Name " +
            //        " FROM mas_division " +
            //        " WHERE division_active_flag=0 " +
            //        " ORDER BY Division_City asc";
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 and  Division_Code in (" + div_code + ") " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        //Added by Sri - 14Sep15

        public DataSet getHODivision()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            //strQry = "SELECT a.Division_Code,a.Division_Name FROM Mas_Division a,Mas_HO_ID_Creation b" +
            //          " WHERE a.Division_Active_Flag=0 and  " +
            //          " ((b.Division_Code not like cast(a.Division_Code as varchar) +','+'%') and (b.Division_Code not like '%'+','+ cast(a.Division_Code as varchar) +','+'%'))";

            //strQry = "SELECT a.Division_Code,a.Division_Name FROM Mas_Division a" +
            //          " WHERE a.Division_Active_Flag=0 and  " +
            //          " (cast(a.Division_Code as varchar) +','+'%' not like (select b.Division_Code from Mas_HO_ID_Creation b)) and " +
            //          " ('%'+','+ cast(a.Division_Code as varchar) +','+'%') not like (select c.Division_Code from Mas_HO_ID_Creation c))";

            strQry = "SELECT Division_Code FROM Mas_HO_ID_Creation " +
                      " WHERE HO_Active_Flag=0  ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        //Added by Sri - 14Sep15
        public DataSet getDiv(string div_code, string HoDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division " +
                     " WHERE Division_Active_Flag=0 " +
                     " AND Division_Code NOT IN (" + div_code + ") " +
                     " OR Division_Code IN (" + HoDiv + ") " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }

        public DataSet getHODivisionEdit(string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT Division_Code FROM Mas_HO_ID_Creation " +
                      " WHERE HO_Active_Flag=0  and HO_ID <>'" + HO_ID + "' ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        //14-Sep-15

        public DataSet getLocationHO(string StateCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsLocation = null;
            strQry = "SELECT state_code,statename,shortname FROM mas_state WHERE state_active_flag=0 and state_code in (" + StateCode + ") " +
                " order by 2";
            try
            {
                dsLocation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLocation;
        }

        public DataSet getDivEdit(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDiv = null;
            strQry = "SELECT Division_Code,Division_Name " +
                     " FROM Mas_Division " +
                     " WHERE Division_Active_Flag=0 " +
                     " AND Division_Code IN (" + div_code + ") " +
                     " ORDER BY 2";
            try
            {
                dsDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDiv;
        }


        public DataTable getDivision_DataTable(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;

            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 and  Division_Code in (" + div_code + ") " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        //done by Reshmi

        public DataSet getSubHODivision(string HO_ID, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT a.Division_Code,a.Division_Name " +
                     " FROM Mas_Division a ,Mas_HO_ID_Creation b " +
                     " WHERE a.Division_Active_Flag=0 and b.HO_ID='" + HO_ID + "' and a.Division_Code in (" + div_code + ") " +
                     " ORDER BY 2";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getSubHODiv(string HO_ID, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT a.Division_Code,a.Division_Name " +
                     " FROM Mas_Division a ,Mas_HO_ID_Creation b " +
                     " WHERE a.Division_Active_Flag=0 and b.HO_ID='" + HO_ID + "' and a.Division_Code in (" + div_code + ") " +
                     //" WHERE a.Division_Active_Flag=0 and b.HO_ID='" + HO_ID + "' " +
                     " ORDER BY 2";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public int getNew_Desig(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDesig(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    int sl_no = -1;


                    //  strQry = "SELECT ISNULL(MAX(Designation_Code),0)+1 FROM Mas_SF_Designation";

                    strQry = "SELECT isnull(max(Designation_Code)+1,'1') Designation_Code from Mas_SF_Designation ";
                    int Designation_Code = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Mas_SF_Designation(Designation_Code,Division_Code,Designation_Short_Name,Designation_Name,Type,Desig_Color,tp_approval_Sys,Designation_Active_Flag,Created_Date) " +
                             " select '" + Designation_Code + "' - 1 + row_number() over (order by (select NULL)) as Designation_Code,'" + divcode + "' Division_Code,Designation_Short_Name,Designation_Name,Type,Desig_Color,tp_approval_Sys,Designation_Active_Flag, getdate() as Created_Date from Mas_SF_Designation where Division_Code=1 " +
                             " ORDER BY 2";

                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistDesig(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Mas_SF_Designation where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int getNew_Holi(string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistHoli(divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    int Holiday_Id = -1;
                    strQry = "SELECT isnull(max(Holiday_Id)+1,'1') Holiday_Id from Holidaylist ";
                    Holiday_Id = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Holidaylist(Holiday_Id,Division_Code,Holiday_Name,Multiple_Date,Month,Holiday_Active_Flag,Fixed_date,Created_Date,Holiday_SlNo) " +
                             " select '" + Holiday_Id + "' - 1 + row_number() over (order by (select NULL)) as Holiday_Id,'" + divcode + "' Division_Code,Holiday_Name,Multiple_Date,Month,Holiday_Active_Flag,Fixed_date, getdate() as Created_Date,Holiday_SlNo from Holidaylist where Division_Code=1 ";


                    iReturn = db.ExecQry(strQry);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iReturn;

        }
        public bool sRecordExistHoli(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Holidaylist where Division_Code='" + div_code + "' ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public DataSet getDivision_Name_Queries()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "Select '0' as Division_Code,'--Select--' as Division_Name,'' as Div_Sl_No " +
            //             " Union all" +
            strQry = " SELECT distinct d.Division_Code,d.Division_Name,d.Div_Sl_No " +
                     " FROM mas_division d inner join  Mas_Query_Box a on  a.Division_Code= d.Division_Code " +
                     " WHERE a.Query_Active_Flag=0 and d.Division_Active_Flag=0 " +
                     " order by Div_Sl_No";



            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public int Standby(string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                            " SET Standby=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getDivision_stand()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT  convert(char(2),ROW_NUMBER() over(ORDER BY Division_City)) +' '+ Division_Name Division_Name,Division_Code,Division_City,Division_SName,Alias_Name " +
            //        " FROM mas_division " +
            //        " WHERE division_active_flag=0 " +
            //        " ORDER BY Division_City asc";
            strQry = "SELECT Division_Code,Division_Name,Division_City,Division_SName,Alias_Name,Div_Sl_No, standby " +
                     " FROM mas_division " +
                     " WHERE division_active_flag=0 " +
                     " ORDER BY Div_Sl_No";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public int Standby_activate(string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_division " +
                            " SET Standby=0 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getMultiDiv_For_salesforce(string Ho_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select LEN(Division_Code)Div_CodeLen,Division_Code from Mas_HO_ID_Creation where HO_ID='" + Ho_id + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet getMultiDivision_forTransfer(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " SELECT Division_Code,Division_Name,Div_Sl_No " +
                         " FROM mas_division " +
                         " WHERE division_active_flag=0 and Division_Code in (" + div_code + ") " +
                         " order by Div_Sl_No";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getLogo(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select div_logo from mas_division where division_code in(" + div_code + ") AND div_logo IS NOT NULL";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataSet getLocation_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            div_code = div_code + ',';
            DataSet dsLocation = null;
            //strQry = "select COUNT(sf_code),  s.state_code,statename + ' (' + cast(COUNT(sf_code)as varchar) + ')' as statename ,shortname from Mas_Salesforce sa, Mas_State s " +
            //          " where sa.State_Code = s.State_Code  " +
            //          " group by s.state_code,statename,shortname ";

            strQry = " select s.state_code,s.shortname,s.statename + ' ('+ cast((select  count(sf_code) from Mas_Salesforce sf where sf.State_Code=s.state_code  " +
                     " and Division_Code = '" + div_code + "')  as varchar)+ ')' as statename " +
                     " from Mas_State s  where s.State_Active_Flag=0 ";
            try
            {
                dsLocation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLocation;
        }

        public DataSet getStateMailDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select state_code from mas_division where division_code in(" + div_code + ")";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getProdSt(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " declare @state varchar(max) " +
                     " SET @state = (SELECT  distinct REPLACE(STUFF((SELECT ',' + State_Code " +
                     " FROM Mas_Product_Detail d  where division_code='" + div_code + "' and Product_Active_Flag=0  " +
                     " FOR XML PATH('')) ,1,1,''),',,',',') AS st from Mas_Product_Detail h   ) " +
                     " select  dbo.RemoveDups(@state) as  State";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }



        public DataSet get_App_version(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "EXEC get_App_version '" + div_code + "','" + sf_code + "'";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet FetchProduct(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = //"select 0 as Product_Code_SlNo, '--Select--' as Product_Detail_Name  Union All  " +
                 "select Product_Code_SlNo,Product_Detail_Name from mas_Product_Detail where Product_Active_Flag=0 and Division_Code='" + div_code + "' order by Product_Code_SlNo";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTerr;
        }
    }
}
