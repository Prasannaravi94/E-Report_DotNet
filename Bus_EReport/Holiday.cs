using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Holiday
    {
        private string strQry = string.Empty;
        private string strNowYear = DateTime.Now.Year.ToString();

        public DataSet getHolidays(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE   a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        // Sorting For HolidayList(Admin)
        //Change getHolidaylist_DataTable parameter done by saravanan 07-08-2014
        public DataTable getHolidaylist_DataTable(string divcode, string strYear, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE   a.Division_Code = '" + divcode + "' AND a.state_code='" + statcode + "' AND a.Academic_Year='" + strYear + "' AND  a.state_code=b.state_code " +
            //         " ORDER BY 1";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, CONVERT(varchar(3), Holiday_Date, 100) as month" +
                " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                " on a.state_code like '" + state_code + ',' + "%'  or " +
                " a.state_code like '%" + ',' + state_code + "' or" +
                " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                " WHERE convert(varchar,b.state_code)='" + state_code + "' AND a.Academic_Year='" + strYear + "' " +
                " and a.Division_Code = '" + divcode + "' " +
                " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dtHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHoliday;
        }
        public DataSet Holi_State(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;


            string strQry =
                "SELECT b.State_Code, b.StateName " +
                "FROM Mas_Division a " +
                "JOIN Mas_State b ON ',' + a.State_Code + ',' LIKE '%,' + CAST(b.State_Code AS VARCHAR) + ',%' " +
                "WHERE a.Division_Code = '" + div_code + "'";

            try
            {
                dsListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsListedDR;
        }
        //Change Strqry done by saravanan 07-08-2014
        public DataSet getHolidays(string state_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " and a.Academic_Year ='" + strNowYear + "' ORDER BY 3";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public bool CheckMenuNameExists(string menuName)
        {
            bool exists = false;
            DB_EReporting db = new DB_EReporting();

            try
            {
                // Correct the query and avoid SQL injection using parameters
                string sCheckQry = "SELECT COUNT(1) FROM Tbl_DynamicMenuCreation WHERE Menu_Name = '" + menuName.Replace("'", "''") + "'";

                // Execute the query to get the count
                int count = db.Exec_Scalar(sCheckQry);

                // If count is greater than 0, it means the menu name already exists
                exists = count > 0;
            }
            catch (Exception ex)
            {
                // Handle exceptions properly
                throw ex;
            }

            return exists;
        }
        public int Add_Menu(string PK_Id, string menuName, string div_code, string Menu_Icon, string menutype, string page)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                // Generate new PK_Id and Menu_Id
                string sQry = "SELECT ISNULL(MAX(PK_Id), 0) + 1 FROM Tbl_DynamicMenuCreation";
                int slNo = db.Exec_Scalar(sQry);

                string sQry1 = "SELECT ISNULL(MAX(Menu_Id), 0) + 1 FROM Tbl_DynamicMenuCreation WHERE PK_Id = '" + slNo + "' ";
                int slNo1 = db.Exec_Scalar(sQry1);

                // Insert new record
                string insertQry = "INSERT INTO Tbl_DynamicMenuCreation (PK_Id, Menu_Id, Menu_Name, Menu_Type, Menu_Icon, Menu_Page, Division_Code, Is_Active, Creation_Date) " +
                                   "VALUES ('" + slNo + "', '" + slNo1 + "', '" + menuName + "', '" + menutype + "', '" + Menu_Icon + "', '" + page + "', '" + div_code + "', '0', GETDATE())";

                iReturn = db.ExecQry(insertQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Add_Menusave(string PK_Id, string Menu_Name, string Division_Code, string Menu_Icon, string Menu_Type, string page)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                // Check if the provided PK_Id already exists in the database for the given Division_Code
                string sCheckQry = "SELECT COUNT(1) FROM Tbl_DynamicMenuCreation WHERE PK_Id = '" + PK_Id + "' AND Division_Code = '" + Division_Code + "'";

                int exists = db.Exec_Scalar(sCheckQry); // Check if the record exists

                if (exists > 0) // If the record exists, perform the update
                {
                    string strUpdateQry = "UPDATE Tbl_DynamicMenuCreation SET " +
                                          "Menu_Name = '" + Menu_Name + "', " +
                                          "Menu_Type = '" + Menu_Type + "', " +
                                          "Menu_Icon = '" + Menu_Icon + "', " +
                                          "Menu_Page = '" + page + "', " +
                                          "Division_Code = '" + Division_Code + "', " +
                                          "Is_Active = '0', " +
                                          "Creation_Date = GETDATE() " +
                                          "WHERE PK_Id = '" + PK_Id + "' AND Division_Code = '" + Division_Code + "'";

                    iReturn = db.ExecQry(strUpdateQry); // Execute the update query
                }
                else // If the record doesn't exist, return an error or handle accordingly
                {
                    iReturn = -1; // Record not found, cannot update
                }
            }
            catch (Exception ex)
            {
                throw ex; // Handle the exception
            }

            return iReturn;
        }
        public int Add_Menuupdate(string PK_Id, string Menu_Name, string Division_Code, string Menu_Icon, string Menu_Type, string page)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {

                string sCheckQry = "SELECT COUNT(1) FROM Tbl_DynamicMenuCreation WHERE PK_Id = '" + PK_Id + "' AND Division_Code = '" + Division_Code + "'";

                int exists = db.Exec_Scalar(sCheckQry);

                if (exists > 0)
                {
                    string strUpdateQry = "UPDATE Tbl_DynamicMenuCreation SET " +
                                          "Menu_Name = '" + Menu_Name + "', " +
                                          "Menu_Type = '" + Menu_Type + "', " +
                                          "Menu_Icon = '" + Menu_Icon + "', " +
                                          "Menu_Page = '" + page + "', " +
                                          "Division_Code = '" + Division_Code + "', " +
                                          "Is_Active = '0', " +
                                          "Creation_Date = GETDATE() " +
                                          "WHERE PK_Id = '" + PK_Id + "' AND Division_Code = '" + Division_Code + "'";

                    iReturn = db.ExecQry(strUpdateQry);
                }
                else
                {
                    iReturn = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet getHolidayYear(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
                     " and a.Academic_Year ='" + ddlYear + "' ORDER BY 3";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        //Change done by saravanan 07-08-2014

        // Sorting For MR_HolidayList 
        public DataTable getHolidayslistMR_DataTable(string state_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " ORDER BY 3";
            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                 " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                 " on a.state_code like '" + state_code + ',' + "%'  or " +
                 " a.state_code like '%" + ',' + state_code + "' or" +
                 " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                 " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                 " and a.Division_Code = '" + divcode + "' " +
                 " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dtHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHoliday;
        }

        public DataSet getHoli(string divcode, string slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT  state_code, Academic_Year, Holiday_Name,convert(varchar,Holiday_Date,105) Holiday_Date " +
                     " FROM Mas_Statewise_Holiday_Fixation " +
                     " WHERE Sl_No = '" + slno + "' AND  Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet getState(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsHoliday = null;
            strQry = "SELECT  distinct a.state_code as statecode,b.statename as statename FROM Mas_Statewise_Holiday_Fixation a,mas_State b" +
                     " WHERE  a.state_code = b.state_code and Division_Code = '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet getYear(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT  max(a.Academic_Year)as Academic_Year, a.state_code as statecode FROM Mas_Statewise_Holiday_Fixation a,mas_State b" +
                     " WHERE  a.state_code = b.state_code and Division_Code = '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        ////public int RecordAdd(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti, string existingState)
        ////{
        ////    int iReturn = -1;

        ////    try
        ////    {
        ////        DB_EReporting db = new DB_EReporting();
        ////        int Sl_No = -1;
        ////        strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
        ////        Sl_No = db.Exec_Scalar(strQry);

        ////        if (lblMulti == "0")
        ////        {
        ////            if (Holiday_RecordExist(div_code, holiday_Name, holiday_date))
        ////            {
        ////                state_code += existingState;

        ////                string[] str = state_code.Split(',');
        ////                string st = string.Empty;
        ////                foreach (string strar in str)
        ////                {
        ////                    if (st.Contains(strar))
        ////                    {

        ////                    }
        ////                    else
        ////                    {
        ////                        st += strar + ",";
        ////                    }
        ////                }

        ////                //if (state_code != "")
        ////                //{
        ////                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
        ////                     " SET Academic_Year = " + year + ", State_code = '" + st + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
        ////                     " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
        ////                     " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'  ";
        ////                //}
        ////                //else
        ////                //{
        ////                //    strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
        ////                //}
        ////                //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";
        ////            }
        ////            else if (state_code != "")
        ////            {

        ////                strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
        ////                         " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
        ////            }
        ////        }
        ////        else
        ////        {
        ////            if (Holiday_SingleRecordExist(div_code, holiday_Name))
        ////            {
        ////                 state_code += existingState;
        ////                 string[] str = state_code.Split(',');
        ////                 string st = string.Empty;
        ////                 foreach (string strar in str)
        ////                 {
        ////                     if (st.Contains(strar))
        ////                     {

        ////                     }
        ////                     else
        ////                     {
        ////                         st += strar + ",";
        ////                     }
        ////                 }
        ////                 //if (state_code != "")
        ////                 //{
        ////                     strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
        ////                          " SET Academic_Year = " + year + ", State_code = '" + st + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
        ////                          " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
        ////                          " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
        ////                 //}
        ////                 //else 
        ////                 //{
        ////                 //    strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
        ////                 //}
        ////                //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

        ////            }
        ////            else if (state_code != "")
        ////            {

        ////                strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
        ////                         " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "',getdate()) ";
        ////            }

        ////        }
        ////        iReturn = db.ExecQry(strQry);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////    //}
        ////    //else
        ////    //{
        ////    //    iReturn = -2;
        ////    //}
        ////    return iReturn;
        ////}

        public int RecordAdd(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti, string existingState)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int Sl_No = -1;
                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                Sl_No = db.Exec_Scalar(strQry);
                string sQryVal = "SELECT State_Code FROM Mas_Statewise_Holiday_Fixation where convert(varchar,Holiday_Date,105) ='" + holiday_date + "' and (State_Code like ('" + state_code.Replace(",", "").Trim() + ",%') or " +
                                "State_Code like('%," + state_code.Replace(",", "").Trim() + "') or State_Code like ('%," + state_code.Replace(",", "").Trim() + ",%')) AND Division_Code='" + div_code + "'";

                DataTable dtRecords = new DataTable();
                try
                {
                    dtRecords = db.Exec_DataTable(sQryVal);
                }
                catch (Exception ex)
                {
                }
                /*
                strQry = "SELECT State_Code, Sl_No FROM Mas_Statewise_Holiday_Fixation WHERE convert(varchar, Holiday_Date,105)='"+holiday_date+"'"+
                    " AND Division_Code='" + div_code + "' AND Academic_Year='"+year+"' AND Holiday_Name_Sl_No='"+hdnHolidayID+"'";
                DataTable dtGetData = db.Exec_DataTable(strQry);
                if (dtGetData.Rows.Count>1)
                {
                    string sStateCode = "";
                    string sSlNo = "";
                    string sTmp = "";
                    foreach (DataRow drRow in dtGetData.Rows)
                    {
                        sStateCode+=drRow[0].ToString()+",";
                        sSlNo+=drRow[1].ToString()+",";
                    }
                    string[] aSt= sStateCode.Split(',');
                    Array.Sort(aSt);
                    sStateCode = "";
                    foreach (string item in aSt)
                    {
                        if (sTmp != item && item!="")
                        {
                            sStateCode += item + ",";
                            sTmp = item;
                        }
                    }
                }*/
                if (dtRecords.Rows.Count < 2)
                {
                    bool stateExist = false;
                    if (dtRecords.Rows.Count > 0)
                    {
                        string[] sAllSt = null;
                        foreach (DataRow dtr in dtRecords.Rows)
                        {
                            sAllSt = dtr[0].ToString().Split(',');
                        }
                        foreach (string item in sAllSt)
                        {
                            if (state_code.Replace(",", "").Trim() == item.Trim() && item.Trim() != "")
                            {
                                stateExist = true;
                            }
                        }
                    }

                    if ((dtRecords.Rows.Count > 0 && stateExist == true) || (dtRecords.Rows.Count < 1 && stateExist == false) || (dtRecords.Rows.Count > 0 && stateExist == false && state_code == ""))
                    {
                        if (Holiday_RecordExist(div_code, holiday_Name, holiday_date, year))
                        {
                            strQry = "";
                            string[] aAllStates = existingState.Split(',');
                            string state_codes = state_code.Replace(",", "");
                            foreach (string item in aAllStates)
                            {
                                if (state_code.Replace(",", "").Trim() != item && item != "")
                                {
                                    state_codes += "," + item;
                                }
                            }
                            if (state_code != "" || state_code == "")
                            {
                                state_codes = remove_extra_commas(state_codes);
                                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                        " SET Academic_Year = " + year + ", State_code = '" + state_codes + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                        " holiday_Name='" + holiday_Name.Replace("'", "''") + "',LastUpdt_Date = getdate() " +
                                        " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' AND Academic_Year=" + year + " ";
                                if (lblMulti == "0")
                                {
                                    strQry += " and convert(char(10),Holiday_Date,105)='" + holiday_date + "' ";
                                }
                            }
                            else
                            {/*
                                strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'";
                                if (lblMulti=="0")
	                            {
		                            strQry+=" and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
	                            }*/
                            }
                        }
                        else if (state_code != "")
                        {
                            strQry = "";
                            state_code = remove_extra_commas(state_code);
                            strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                        " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name.Replace("'", "''") + "', GETDATE(), '" + div_code + "',getdate()) ";
                        }
                        if (strQry != "")
                            iReturn = db.ExecQry(strQry);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Holiday_SingleRecordAdd(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int Sl_No = -1;
                int Holiday = -1;
                int state = -1;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                Sl_No = db.Exec_Scalar(strQry);

                string sQryHoliday = "With result as(SELECT Sl_No,Academic_Year,State_Code as State_Code, ','+State_Code as State_Codes, " +
                    " Holiday_Date,Holiday_Name_Sl_No,Holiday_Name,Division_Code FROM Mas_Statewise_Holiday_Fixation where Academic_Year='" + year + "' AND " +
                    " convert(varchar,Holiday_Date,105)='" + holiday_date + "' AND Holiday_Name_Sl_No = '" + hdnHolidayID + "' AND Division_Code='" + div_code + "') select COUNT(*) from result ";
                Holiday = db.Exec_Scalar(sQryHoliday);

                string sQryState = "With result as(SELECT Sl_No,Academic_Year,State_Code as State_Code, ','+State_Code as State_Codes, " +
                    " Holiday_Date,Holiday_Name_Sl_No,Holiday_Name,Division_Code FROM Mas_Statewise_Holiday_Fixation where Academic_Year='" + year + "' AND " +
                    " convert(varchar,Holiday_Date,105)='" + holiday_date + "' AND Holiday_Name_Sl_No = '" + hdnHolidayID + "' AND Division_Code='" + div_code + "') select COUNT(*) from result where State_Codes like '%," + state_code + ",%'";
                state = db.Exec_Scalar(sQryState);

                if (Holiday == 0 && state == 0)
                {
                    strQry = "";
                    strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code) " +
                                " values( " + Sl_No + ", " + year + ", '" + state_code + ",', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name + "', GETDATE(), '" + div_code + "') ";

                    if (strQry != "")
                        iReturn = db.ExecQry(strQry);
                }
                else if (Holiday != 0 && state == 0)
                {
                    string states = getHolidayState(div_code, Convert.ToString(hdnHolidayID), holiday_date);
                    string nState = states + state_code + ",";

                    strQry = "";
                    strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                        " SET Academic_Year = " + year + ", State_code = '" + nState + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                        " holiday_Name='" + holiday_Name + "',LastUpdt_Date = getdate() " +
                        " WHERE Division_Code = '" + div_code + "' and convert(varchar,Holiday_Date,105)='" + holiday_date + "' AND Holiday_Name_Sl_No='" + hdnHolidayID + "' AND Academic_Year=" + year + " ";

                    if (strQry != "")
                        iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public bool Holiday_SingleRecordExist(string Division_Code, string Holiday_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Statewise_Holiday_Fixation " +
                         " where Division_Code='" + Division_Code + "'  and Holiday_Name='" + Holiday_Name.Replace("'", "''") + "'";

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
        #region RecordUpdate
        public int RecordUpdate(string statecode, string Slno, string HolName, string Holdate, string divcode, string sHolidayNameId, string sYear, string sOldDate)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            //
            #region Get Existing State codes
            strQry = "SELECT Convert(varchar,Holiday_Date,105) as Holiday_Date, State_Code, Holiday_Name FROM Mas_Statewise_Holiday_Fixation WHERE Holiday_Name='" + HolName.Replace("'", "''") + "' " +
                " AND Division_Code='" + divcode + "' AND Holiday_Name_Sl_No='" + sHolidayNameId + "'";

            DataTable dt = db.Exec_DataTable(strQry);
            string sExstOldDate = "", sExstHStCd = "", sRmvExstStCd = "";
            bool bDateExist = false;
            //
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtRow in dt.Rows)
                {
                    string sDate = dtRow[0].ToString();
                    if (Holdate == sDate)
                    {
                        bDateExist = true;
                        sExstHStCd = dtRow[1].ToString();
                    }
                    else if (sOldDate == sDate)
                    {
                        sRmvExstStCd = dtRow[1].ToString();
                        sExstOldDate = sOldDate;
                    }
                }
                if (sExstHStCd.Length > 1)
                {
                    string[] sUpdt = sExstHStCd.Split(',');
                    sExstHStCd = "";
                    foreach (string nwStCd in sUpdt)
                    {
                        if (nwStCd != "" && nwStCd != statecode)
                        {
                            sExstHStCd += nwStCd + ",";
                        }
                    }
                    sExstHStCd = statecode + "," + sExstHStCd;
                }
                if (sRmvExstStCd.Length > 1)
                {
                    string[] sRmv = sRmvExstStCd.Split(',');
                    sRmvExstStCd = "";
                    foreach (string updtStCd in sRmv)
                    {
                        if (updtStCd != "" && updtStCd != statecode)
                        {
                            sRmvExstStCd += updtStCd + ",";
                        }
                    }
                }
            }
            #endregion
            //
            try
            {
                strQry = "";
                #region Removing Current State from Existing Old Date
                if (sExstOldDate != "")
                {
                    sRmvExstStCd = remove_extra_commas(sRmvExstStCd);
                    strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                         " SET State_code='" + sRmvExstStCd.Trim() + "', LastUpdt_Date = getdate() " +
                         " WHERE convert(varchar, Holiday_Date, 105) = '" + sExstOldDate + "' AND Division_Code = '" + divcode + "' AND Holiday_Name_Sl_No='" + sHolidayNameId + "' ";
                    //
                    iReturn = db.ExecQry(strQry);
                }
                #endregion
                //
                #region Inserting New Holiday
                if (!bDateExist)
                {
                    int Sl_No = -1;
                    strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                    Sl_No = db.Exec_Scalar(strQry);
                    statecode = remove_extra_commas(statecode);
                    strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                        " values( " + Sl_No + ", " + sYear + ", '" + statecode.Trim() + "', '" + Holdate.Substring(6, 4) + "-" + Holdate.Substring(3, 2) + "-" + Holdate.Substring(0, 2) + "', " +
                                        "" + sHolidayNameId + ", '" + HolName.Replace("'", "''") + "', GETDATE(), '" + divcode + "',getdate()) ";
                    //
                    iReturn = db.ExecQry(strQry);
                }
                #endregion
                //
                #region Updating State in Existing Date & Holiday
                else
                {
                    sExstHStCd = remove_extra_commas(sExstHStCd);
                    strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                             " SET State_code='" + sExstHStCd.Trim() + "', LastUpdt_Date = getdate() " +
                             " WHERE Holiday_Name = '" + HolName.Replace("'", "''") + "' AND Division_Code = '" + divcode + "'" +
                             " AND Convert(varchar,Holiday_Date,105)='" + Holdate + "' AND Holiday_Name_Sl_No='" + sHolidayNameId + "'";
                    //
                    iReturn = db.ExecQry(strQry);
                }
                #endregion
                //
                #region Removing Empty Holiday
                strQry = "DELETE Mas_Statewise_Holiday_Fixation WHERE State_Code='' OR State_Code=NULL";
                int iRslt = db.ExecQry(strQry);
                #endregion
                //
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        #endregion
        public int RecordEdit(int year, int state, DateTime Holdate, string holiday, string divcode, string Slno)
        {
            int iReturn = -1;
            //if (!RecordExist(statecode, shortname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();
                string sStates = remove_extra_commas(state.ToString());
                strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                         " SET Holiday_Name = '" + holiday.Replace("'", "''") + "', " +
                         " Holiday_Date = '" + Holdate + "', " +
                         " Academic_Year = " + year + " ," +
                         " State_code = " + sStates + "," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Sl_No = '" + Slno + "'  AND  Division_Code = '" + divcode + "' ";

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

        public int RecordDelete(string slno, string Division_Code, string sHly_Date, string sState_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string sQry = "SELECT State_Code FROM Mas_Statewise_Holiday_Fixation WHERE Sl_No = '" + slno + "'" +
                    " AND Division_Code = '" + Division_Code + "' AND convert(varchar,Holiday_Date,105)='" + sHly_Date + "'";
                DataTable dtHly = db.Exec_DataTable(sQry);
                if (dtHly.Rows.Count > 0)
                {
                    string nwState_Code = "", sExistSt = "";
                    sExistSt = dtHly.Rows[0][0].ToString().Trim();
                    string[] aSExist = sExistSt.Split(',');
                    foreach (string item in aSExist)
                    {
                        if (item != sState_Code && item != "")
                        {
                            nwState_Code += item + ",";
                        }
                    }
                    nwState_Code = remove_extra_commas(nwState_Code);
                    strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                         " SET State_code = '" + nwState_Code + "', LastUpdt_Date = getdate() " +
                         " WHERE convert(varchar,Holiday_Date,105) = '" + sHly_Date + "' AND " +
                         " Sl_No = '" + slno + "'  AND  Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);
                    strQry = "DELETE Mas_Statewise_Holiday_Fixation WHERE State_code='' OR State_code=','";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getHoliday_View(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, MONTH(holiday_date) [Month]" +
            //         " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //         " WHERE a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code AND a.state_code = '" + state_code + "' " +
            //         " and a.Academic_Year ='" + ddlYear + "'  ORDER BY 3";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, " +
                    " a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, " +
                    " a.Division_Code, stuff((select ', '+StateName from mas_state b where charindex(cast(b.state_code as varchar)+',',a.state_code+',')>0 for XML path('')),1,2,'') StateName, " +
                    " MONTH(holiday_date) [Month] FROM Mas_Statewise_Holiday_Fixation a   WHERE a.Division_Code = '" + divcode + "' " +
                    " and a.Academic_Year = '" + ddlYear + "'" +
                    " and (a.state_code like '" + state_code + ',' + "%'  or " +
                    " a.state_code like '%" + ',' + state_code + "' or" +
                    " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "' )";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public bool Holiday_RecordExist(string Division_Code, string Holiday_Name, string StrDate, int year)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (Holiday_Name != "")
                {
                    strQry = "Select count(*) from Mas_Statewise_Holiday_Fixation " +
                             " where Division_Code='" + Division_Code + "'  and Holiday_Name='" + Holiday_Name.Replace("'", "''") + "' and convert(char(10),Holiday_Date,105)='" + StrDate + "' AND Academic_Year = " + year + " ";
                }
                else
                {
                    strQry = "Select count(*) from Mas_Statewise_Holiday_Fixation " +
                             " where Division_Code='" + Division_Code + "' and convert(char(10),Holiday_Date,105)='" + StrDate + "' ";
                }
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
        public DataSet getmulti_Days()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date" +
                     " FROM Holidaylist " +
                     " WHERE  Holiday_Active_Flag=0  and Multiple_Date=0";

            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet get_Holidays(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName" +
                     " FROM Holidaylist where Holiday_Active_Flag=0 and Division_Code = '"+div_code+"'" +
                     " order by Holiday_SlNo";


            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        //Changes done by Priya --jan6
        public DataSet getHoliday_List(string state_code, string divcode, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name, CONVERT(varchar(3), Holiday_Date, 100) as month" +
                     " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                     " on a.state_code like '" + state_code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + state_code + "' or" +
                     " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                     " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                     " and a.Division_Code = '" + divcode + "' " +
                     " and a.Academic_Year ='" + ddlYear + "' ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHoldayDate(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHolidayState = null;

            //strQry = " select Sl_No,convert(char(10),Holiday_Date,103) Holiday_Date,Holiday_Name,State_Code from Mas_Statewise_Holiday_Fixation " +
            //         " where State_Code in ('" + state_code + "') " +
            //         " ORDER BY 2";

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                      " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                      " on a.state_code like '" + state_code + ',' + "%'  or " +
                      " a.state_code like '%" + ',' + state_code + "' or" +
                      " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                      " WHERE convert(varchar,b.state_code)='" + state_code + "' ";

            try
            {
                dsHolidayState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHolidayState;
        }
        //public int RecordDelete(string slno)
        //{
        //    int iReturn = -1;
        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "DELETE FROM  Mas_Statewise_Holiday_Fixation WHERE Sl_No = '" + slno + "' ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;

        //}

        public int Add_Holiday(string Holiday_Name, int Multiple_Date, string Fixed_date, string Month, string div_code)
        {
            int iReturn = -1;
            if (!RecordExistAdd(Holiday_Name, div_code))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    string sQry = "SELECT max(Holiday_ID) FROM Holidaylist";
                    int slNo = db.Exec_Scalar(sQry);
                    if (Fixed_date != "")
                    {
                        strQry = "INSERT INTO Holidaylist(Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,Created_Date, Holiday_Active_Flag, Division_Code)" +
                            "values(" + (slNo + 1) + ",'" + Holiday_Name.Replace("'", "''") + "', " + Multiple_Date + ",'" + Fixed_date.Substring(6, 4) + "-" + Fixed_date.Substring(3, 2) + "-" + Fixed_date.Substring(0, 2) + "','" + Month + "', GETDATE(), 0, '" + div_code + "') ";
                    }
                    else
                    {
                        strQry = "INSERT INTO Holidaylist(Holiday_Id,Holiday_Name,Multiple_Date,Month,Created_Date, Holiday_Active_Flag, Division_Code)" +
                            "values(" + (slNo + 1) + ",'" + Holiday_Name.Replace("'", "''") + "', " + Multiple_Date + ",'" + Month + "', GETDATE(), 0,'" + div_code + "') ";
                    }

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

        public bool RecordExist(string Holiday_Name, int hdnHolidayID, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Holiday_Id) FROM Holidaylist WHERE Holiday_Name='" + Holiday_Name.Replace("'", "''") + "'  AND Holiday_Id !='" + hdnHolidayID + "' and Division_Code = '" + div_code + "'";
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
        public int Update_Holiday(int hdnHolidayID, string Holiday_Name, int Multiple_Date, string Fixed_date, string Month, string div_code)
        {
            int iReturn = -1;
            if (!RecordExist(Holiday_Name, hdnHolidayID, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    if (Fixed_date != "")
                    {
                        strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name.Replace("'", "''") + "',Multiple_Date =" + Multiple_Date + ",Fixed_date='" + Fixed_date.Substring(6, 4) + "-" + Fixed_date.Substring(3, 2) + "-" + Fixed_date.Substring(0, 2) + "',Month='" + Month + "',Division_Code = '" + div_code + "' " +
                       " where Holiday_Id=" + hdnHolidayID + " and Holiday_Active_Flag=0 ";
                    }
                    else
                    {
                        strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name.Replace("'", "''") + "',Multiple_Date =" + Multiple_Date + ",Month='" + Month + "', Division_Code = '" + div_code + "' " +
                                 " where Holiday_Id=" + hdnHolidayID + " and Holiday_Active_Flag=0 ";
                    }

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
        public DataSet getHolidayName_Alphabet()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "select '1' val,'All' Holiday_Name " +
                     " union " +
                     " select distinct LEFT(Holiday_Name,1) val, LEFT(Holiday_Name,1) Holiday_Name" +
                     " FROM HolidayList where Holiday_Active_Flag=0 " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHolidayName_Alphabet(string sAlpha, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            strQry = "SELECT Holiday_Id,Holiday_Name,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName " +
                     " FROM HolidayList " +
                     " where LEFT(Holiday_Name,1) = '" + sAlpha + "' and Holiday_Active_Flag=0 and Division_Code = '"+div_code+"' " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHoli_Ed(string Holiday_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT Holiday_Id, Holiday_Name,Multiple_Date,Month, convert(varchar,Fixed_date,105) Fixed_date, Division_Code" +
                     " FROM HolidayList WHERE Holiday_Active_Flag=0 And Holiday_Id= '" + Holiday_Id + "'" +
                     " ORDER BY 2";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public int Delete_Holiday(int Holiday_Id)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Holidaylist WHERE Holiday_Id = '" + Holiday_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int Update_Inline_Holiday(int Holiday_Id, string Holiday_Name, string div_code)
        {
            int iReturn = -1;
           if (!RecordExist(Holiday_Name, Holiday_Id, div_code))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "Update Holidaylist set Holiday_Name = '" + Holiday_Name.Replace("'", "''") + "'" +
               " where Holiday_Id=" + Holiday_Id + " and Holiday_Active_Flag=0 and Division_Code = '"+div_code+"'";



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
        //Changes done by saravana
        public DataSet getHolidaysMGR(string state_code, string divcode, string stateName, string strYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' " +
                   " and a.Division_Code = '" + divcode + "' and a.Academic_Year='" + strYear + "' and b.StateName='" + stateName + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public bool RecordExist(int Holiday_Id)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(state_code) FROM mas_division WHERE state_code = '" + State_Code + "'  ";
                strQry = " SELECT COUNT(Sl_No) FROM Mas_Statewise_Holiday_Fixation WHERE Holiday_Name_Sl_No =   '" + Holiday_Id + "'";


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

        public int DeActivateHoli(int Holiday_Id)
        {
            int iReturn = -1;
            if (!RecordExist(Holiday_Id))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Holidaylist " +
                                 " SET Holiday_Active_Flag=1 , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Holiday_Id = '" + Holiday_Id + "' ";

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
        //Changes done by Priya

        public DataTable get_Holidays_sort(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHoliday = null;

            strQry = "SELECT Holiday_Id,Holiday_Name,Multiple_Date,Fixed_date,Month,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName" +
                     " FROM Holidaylist where Holiday_Active_Flag=0 and Division_Code = '"+div_code+"'" +
                     " order by Month";


            try
            {
                dsHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataTable get_Holidays_sort(string sAlpha, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHoliday = null;
            strQry = "SELECT Holiday_Id,Holiday_Name,datename(month,dateadd(month, [Month] - 1, 0)) as MonthName " +
                     " FROM HolidayList " +
                     " where LEFT(Holiday_Name,1) = '" + sAlpha + "' and Holiday_Active_Flag=0 and Division_Code = '"+div_code+"'" +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public bool RecordExistAdd(string Holiday_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Holiday_Id) FROM Holidaylist WHERE Holiday_Name='" + Holiday_Name.Replace("'", "''") + "' and Division_Code = '" + div_code + "'";
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

        public string getHolidayState(string divcode, string holidayid, string holiday_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            string states = string.Empty;
            strQry = "SELECT  state_code" +
                     " FROM Mas_Statewise_Holiday_Fixation " +
                     " WHERE Holiday_Name_Sl_No= '" + holidayid + "' AND  Division_Code = '" + divcode + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "' ";

            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
                if (dsHoliday.Tables[0].Rows.Count > 0)
                {
                    states = dsHoliday.Tables[0].Rows[0][0].ToString();

                }
                else
                    states = "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return states;
        }

        //Changes done by Priya

        public int Update_HolidaySlNO(string Hol_Sl_No, string div_code, string Holiday_Id)
        {
            int iReturn = -1;
            //if (!sRecordExist(Div_Sl_No))
            //{

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Holidaylist " +
                         " SET Holiday_SlNo = '" + Hol_Sl_No + "', " +
                         " LastUpdt_Date = getdate()  WHERE Division_Code = '" + div_code + "' and Holiday_Id='" + Holiday_Id + "' and Holiday_Active_Flag = 0 ";


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

        //New Holiday Mr

        public DataSet getHolidays_Mr(string state_code, string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;

          

            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,105) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '"+year+"'" +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public int RecordAdd_Date(int year, string state_code, string holiday_date, string holiday_Name, string div_code, int hdnHolidayID, string lblMulti)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                int Sl_No = -1;
                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Statewise_Holiday_Fixation ";
                Sl_No = db.Exec_Scalar(strQry);

                if (lblMulti == "0")
                {
                    if (Holiday_RecordExist(div_code, holiday_Name, holiday_date, year))
                    {
                       // state_code += existingState;
                        if (state_code != "")
                        {
                            state_code = remove_extra_commas(state_code);
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name.Replace("'", "''") + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "' and convert(char(10),Holiday_Date,105)='" + holiday_date + "'";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {
                        state_code = remove_extra_commas(state_code);
                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name.Replace("'", "''") + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }
                }
                else
                {
                    if (Holiday_SingleRecordExist(div_code, holiday_Name))
                    {
                     //   state_code += existingState;
                        if (state_code != "")
                        {
                            state_code = remove_extra_commas(state_code);
                            strQry = "UPDATE Mas_Statewise_Holiday_Fixation " +
                                 " SET Academic_Year = " + year + ", State_code = '" + state_code + "', Holiday_Date = '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "' , " +
                                 " holiday_Name='" + holiday_Name.Replace("'", "''") + "',LastUpdt_Date = getdate() " +
                                 " WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        else
                        {
                            strQry = "Delete Mas_Statewise_Holiday_Fixation WHERE Division_Code = '" + div_code + "' and Holiday_Name_Sl_No='" + hdnHolidayID + "'  ";
                        }
                        //strQry = "EXEC SaveHolidayList '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "','" + hdnHolidayID + "','" + div_code + "' ";

                    }
                    else if (state_code != "")
                    {
                        state_code = remove_extra_commas(state_code);
                        strQry = "INSERT INTO Mas_Statewise_Holiday_Fixation(Sl_No,Academic_Year, State_code, Holiday_Date, Holiday_Name_Sl_No, Holiday_Name, Created_Date, Division_Code,LastUpdt_Date) " +
                                 " values( " + Sl_No + ", " + year + ", '" + state_code + "', '" + holiday_date.Substring(6, 4) + "-" + holiday_date.Substring(3, 2) + "-" + holiday_date.Substring(0, 2) + "', " + hdnHolidayID + ", '" + holiday_Name.Replace("'", "''") + "', GETDATE(), '" + div_code + "',getdate()) ";
                    }

                }
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
        public DataSet getStateCode(string StateName)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT State_Code FROM  Mas_State " +
                     " WHERE StateName= '" + StateName + "' ";

            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataSet getHolidays_Consol(string state_code, string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "SELECT  ROW_NUMBER() OVER (ORDER BY month(Holiday_Date),day(Holiday_Date)) AS [SlNo] ,  " +
                     " convert(varchar,a.Holiday_Date,105) as HolidayDate , LEFT(DATENAME(WEEKDAY,Holiday_Date),3) AS [Day],a.Holiday_Name as HolidayName, b.StateName " +

                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '" + year + "'" +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        //
        private string remove_extra_commas(string sStates)
        {
            try
            {
                if (sStates.TrimStart().TrimEnd().Substring(0, 1) == ",")
                {
                    sStates = sStates.TrimStart().TrimEnd().Remove(0, 1);
                }                
            }
            catch (Exception exs) { }
            try
            {
                if (sStates.TrimStart().TrimEnd().Substring(sStates.TrimStart().TrimEnd().Length - 1, 1) != ",")
                {
                    sStates += ",";
                }
            }
            catch (Exception ex)
            { }
            return sStates;
        }
        public DataSet getEmptyHoliday()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 20 '' Holiday_Name_Sl_No, '' Holiday_Name,'' Holiday_Date, '' oldDate " +
                     " FROM  sys.tables ";
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
        public DataSet getHolidaySlno(string Division_Code, string Holiday_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Holiday_Id, Holiday_Name, Multiple_Date, Created_Date, " +
                    " Month, Holiday_Active_Flag, Fixed_date, LastUpdt_Date, Division_Code, " +
                    " Holiday_SlNo FROM Bluecross_testing.dbo.Holidaylist where Division_Code='" + Division_Code + "' " +
                    " AND Holiday_Name='" + Holiday_Name + "'";
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
        public DataSet getHolidays_Mr_Leave(string state_code, string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code,  Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '" + year + "'" +
                   " and a.Division_Code = '" + divcode + "' " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet getHolidays_Mr_Leave_From(string state_code, string divcode, string year, string fromdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;



            strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code,  Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
                   " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
                   " on a.state_code like '" + state_code + ',' + "%'  or " +
                   " a.state_code like '%" + ',' + state_code + "' or" +
                   " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
                   " WHERE convert(varchar,b.state_code)='" + state_code + "' and a.Academic_Year = '" + year + "'" +
                   " and a.Division_Code = '" + divcode + "' and Holiday_Date='" + fromdate + "'  " +
                   " ORDER BY month(Holiday_Date) ,day(Holiday_Date)";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }
        public DataSet Leave_Entry_EditDefault(string divcode, string leave_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "  select Leave_Entry,Leave_Entitlement from Mas_Leave_Setup where Division_Code='" + divcode + "' and  Leave_Type_Reg='" + leave_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Leave_Entry_Edit(string divcode, string leave_type, string leave_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "  select Leave_Entry,Leave_Entitlement from Mas_Leave_Setup where Division_Code='" + divcode + "' and Leave_Type_Sl_no='" + leave_type + "' and Leave_Type_Code='" + leave_code + "' and Leave_Entry='Y' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public int LeaveInsert(string leave_entitle, string Leave_Code_value, string chkleave, string Leavereg, string Leavetypecode, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Mas_Leave_Setup ";
                int slNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Leave_Setup(Sl_No,Leave_Type_Sl_no,Leave_Entry,Leave_Type_Reg,Leave_Type_Code, Division_Code, Created_Date,Active_Flg,Leave_Entitlement)" +
                         "values(" + slNo + ",'" + Leave_Code_value + "', '" + chkleave + "','" + Leavereg + "','" + Leavetypecode + "','" + div_code + "', GETDATE(), 0,'" + leave_entitle + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }

        public int LeaveInsertDefault(string leave_entitle, int Leave_Code_value, string chkleave, string Leavereg, string Leavetypecode, string div_code)
        {
            int ireturn = -2;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Mas_Leave_Setup ";
                int slNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Leave_Setup(Sl_No,Leave_Type_Sl_no,Leave_Entry,Leave_Type_Reg,Leave_Type_Code, Division_Code, Created_Date,Active_Flg,Leave_Entitlement)" +
                         "values(" + slNo + ",'" + Leave_Code_value + "', '" + chkleave + "','" + Leavereg + "','" + Leavetypecode + "','" + div_code + "', GETDATE(), 0,'" + leave_entitle + "') ";



                ireturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return ireturn;
        }

        #region Holiday_Upload
        public DataSet Holiday_Upload_Process(string div_code, string xml)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet dsHoliday = null;

            strQry = "EXEC sp_Holiday_Upload_Process '" + div_code + "', '" + xml + "'";

            try
            {
                dsHoliday = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public int Holiday_Upload(string div_code, string xml)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC sp_Holiday_Upload '" + div_code + "', '" + xml + "'";

            iReturn = db.ExecQry(strQry);

            return iReturn;
        }
        #endregion
    }
}
