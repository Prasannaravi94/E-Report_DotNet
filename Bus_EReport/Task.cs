using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class Task
    {
        #region "Variable Declarations"
        private string strQry = string.Empty;
        SqlCommand comm;
        SqlCommand sCommand;
        string sError = string.Empty;
        int iErrReturn = -1;
        #endregion

        public DataSet getTaskType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Task_Id, " +
                            " Task_ShortName, " +
                            " Task_Name " +
                     " FROM Mas_Task_Dtls " +
                     " WHERE Task_Flag = 0 " +
                     " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getTaskType()");
            }
            return dsSale;
        }

        public DataSet getTaskType(bool bInclude)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            if (bInclude)
            {
                strQry = "SELECT -1 as Task_Id, '' as Task_ShortName, '---Select---' as Task_Name " +
                         " UNION " +
                         " SELECT  Task_Id, " +
                                " Task_ShortName, " +
                                " Task_Name " +
                         " FROM Mas_Task_Dtls " +
                         " WHERE Task_Flag = 0 " +
                         " ORDER BY 2";
                try
                {
                    dsSale = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    ErrorLog err = new ErrorLog();
                    iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getTaskType()");
                }
            }
            return dsSale;
        }

        public DataSet getFieldForce(string div_code,string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT '-1' as Sf_Code,  '---Select---' as Sf_Name " +
                        " UNION " +
                        " SELECT Sf_Code, Sf_Name  " +
                        " FROM Mas_Salesforce " +
                        " WHERE SF_Status = 0 " +
                        " AND sf_Designation_Short_Name != 'DIR' " +
                        " AND sf_Designation_Short_Name !='Admin' " +
                        " AND  Division_code = '" + div_code + "' " +
                        " AND sf_code != '" + sf_code + "' " +
                        " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getFieldForce()");
            }
            return dsSale;
        }
        public DataSet getFieldForce(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "SELECT '-1' as Sf_Code,  '---Select---' as Sf_Name " +
                        " UNION " +
                        " SELECT Sf_Code, Sf_Name  " +
                        " FROM Mas_Salesforce " +
                        " WHERE SF_Status = 0 " +
                        " AND sf_Designation_Short_Name != 'DIR' " +
                        " AND sf_Designation_Short_Name !='Admin' " +
                        " AND  Division_code = '" + div_code + "' " +
                        " ORDER BY 1";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getFieldForce()");
            }
            return dsSale;
        }
        public DataSet getTaskMode()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Mode_ID, " +
                            " Short_Name, " +
                            " Mode_Name " +
                     " FROM Mas_Task_Mode " +
                     " WHERE Mode_Flag = 0 " +
                     " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Mode - Maintenance", "getTaskMode()");
            }
            return dsSale;
        }

        public DataSet getTaskClient()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Client_ID, " +
                            " Short_Name, " +
                            " Client_Name " +
                     " FROM Mas_Task_Client " +
                     " WHERE Client_Flag = 0 " +
                     " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Client - Maintenance", "getTaskClient()");
            }
            return dsSale;
        }

        public DataSet getTaskStatus()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Status_ID, " +
                            " Short_Name, " +
                            " Status_Name " +
                     " FROM Mas_Task_Status " +
                     " WHERE Status_Flag = 0 " +
                     " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Status - Maintenance", "getTaskStatus()");
            }
            return dsSale;
        }

        public DataSet getTaskClient(bool includeEmpty)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            if (includeEmpty)
            {
                strQry = "SELECT -1 as Client_ID, '' as Short_Name, '---Select---' as Client_Name " +
                         " UNION " +
                         " SELECT Client_ID, " +
                                " Short_Name, " +
                                " Client_Name " +
                         " FROM Mas_Task_Client " +
                         " WHERE Client_Flag = 0 " +
                         " ORDER BY 2";
                try
                {
                    dsSale = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    ErrorLog err = new ErrorLog();
                    iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Client - Maintenance", "getTaskClient()");
                }
            }
            return dsSale;
        }

        public DataSet getTaskStatus(bool includeEmpty)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            if (includeEmpty)
            {
                strQry = "SELECT -1 as Status_ID, '' as Short_Name, '---Select---' as Status_Name " +
                         " UNION " +
                         " SELECT Status_ID, " +
                                " Short_Name, " +
                                " Status_Name " +
                         " FROM Mas_Task_Status " +
                         " WHERE Status_Flag = 0 " +
                         " ORDER BY 3";
                try
                {
                    dsSale = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    ErrorLog err = new ErrorLog();
                    iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Status - Maintenance", "getTaskStatus()");
                }
            }
            return dsSale;
        }
        public DataSet getTaskStatusView(bool includeEmpty)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            if (includeEmpty)
            {
                strQry = "SELECT -1 as Status_ID, '' as Short_Name, 'All' as Status_Name " +
                         " UNION " +
                         " SELECT Status_ID, " +
                                " Short_Name, " +
                                " Status_Name " +
                         " FROM Mas_Task_Status " +
                         " WHERE Status_Flag = 0 " +
                         " ORDER BY 3";
                try
                {
                    dsSale = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    ErrorLog err = new ErrorLog();
                    iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Status - Maintenance", "getTaskStatus()");
                }
            }
            return dsSale;
        }
        public DataSet getTaskMode(bool includeEmpty,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            if (includeEmpty)
            {
                strQry = "SELECT -1 as Mode_ID, '' as Short_Name, '---Select---' as Mode_Name " +
                         " UNION " +
                         " SELECT Mode_ID, " +
                                " Short_Name, " +
                                " Mode_Name " +
                         " FROM Mas_Task_Mode " +
                         " WHERE Mode_Flag = 0 and Division_code='"+div_code+"'" +
                         " ORDER BY 3";
                try
                {
                    dsSale = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    ErrorLog err = new ErrorLog();
                    iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Status - Maintenance", "getTaskMode()");
                }
            }
            return dsSale;
        }

        public DataSet getTaskType(int task_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT  Task_ShortName, " +
                            " Task_Name " +
                     " FROM Mas_Task_Dtls " +
                     " WHERE Task_Id = " + task_id;
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getTaskType()");
            }
            return dsSale;
        }

        public DataSet getTaskMode(int task_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Short_Name, " +
                            " Mode_Name " +
                     " FROM Mas_Task_Mode " +
                     " WHERE Mode_Id = " + task_id;
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Mode - Maintenance", "getTaskMode()");
            }
            return dsSale;
        }

        public DataSet getTaskClient(int task_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Short_Name, " +
                            " Client_Name " +
                     " FROM Mas_Task_Client " +
                     " WHERE Client_Id = " + task_id;
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Client - Maintenance", "getTaskClient()");
            }
            return dsSale;
        }

        public DataSet getTaskStatus(int task_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Short_Name, " +
                            " Status_Name " +
                     " FROM Mas_Task_Status " +
                     " WHERE Status_Id = " + task_id;
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Status - Maintenance", "getTaskStatus()");
            }
            return dsSale;
        }

        public bool TaskExist(string short_name, string task_name)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Task_Id) " +
                         " FROM Mas_Task_Dtls " +
                         " WHERE Task_ShortName = '" + short_name + "' " +
                         " AND Task_Name = '" + task_name + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Maintenance", "TaskExist()");
            }
            return bRecordExist;
        }

        public bool ModeExist(string short_name, string task_name,string Div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Mode_Id) " +
                         " FROM Mas_Task_Mode " +
                         " WHERE Short_Name = '" + short_name + "' " +
                         " AND Mode_Name = '" + task_name + "' and Division_code ='"+Div_code+"' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Mode - Maintenance", "ModeExist()");
            }
            return bRecordExist;
        }

        public bool ClientExist(string short_name, string task_name)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Client_Id) " +
                         " FROM Mas_Task_client " +
                         " WHERE Short_Name = '" + short_name + "' " +
                         " AND Client_Name = '" + task_name + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Client - Maintenance", "ClientExist()");
            }
            return bRecordExist;
        }

        public bool StatusExist(string short_name, string task_name, string Div_code)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Status_Id) " +
                         " FROM Mas_Task_Status " +
                         " WHERE Short_Name = '" + short_name + "' " +
                         " AND Status_Name = '" + task_name + "' and Division_code='"+Div_code+"' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Client - Maintenance", "StatusExist()");
            }
            return bRecordExist;
        }

        public int RecordAdd(string task_name, string short_name)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "INSERT INTO Mas_Task_Dtls(Task_ShortName, Task_Name, Task_Flag) VALUES ( '" + short_name.Trim() + "', '" + task_name.Trim() + "', 0)";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Add", "TaskType_RecordAdd()");
            }

            return iReturn;
        }


        public int Mode_RecordAdd(string task_name, string short_name, string div_code)
        {
            int iReturn = -1;
            int Mode_ID = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(COUNT(Mode_ID),0)+1 FROM Mas_Task_Mode ";
                Mode_ID = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Task_Mode(Mode_ID,Mode_Name, Short_Name, Mode_Flag,Created_Date,Division_Code) VALUES ('" + Mode_ID + "','" + task_name.Trim() + "', '" + short_name.Trim() + "',  0, getdate(),'"+div_code+"')";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Mode - Record Add", "Mode_RecordAdd()");
            }

            return iReturn;
        }

        public int Client_RecordAdd(string task_name, string short_name)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "INSERT INTO Mas_Task_client(Client_Name, Short_Name, Client_Flag) VALUES ( '" + task_name.Trim() + "', '" + short_name.Trim() + "',  0)";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Client - Record Add", "Client_RecordAdd()");
            }

            return iReturn;
        }

        public int Status_RecordAdd(string task_name, string short_name,string Div_Code)
        {
            int iReturn = -1;
            int Status_ID = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "SELECT ISNULL(COUNT(Status_ID),0)+1 FROM Mas_Task_Status ";
                Status_ID = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Task_Status(Status_ID,Status_Name, Short_Name, Status_Flag,Created_Date,Division_Code) VALUES ('" + Status_ID + "', '" + task_name.Trim() + "', '" + short_name.Trim() + "',  0,getdate(),'"+Div_Code+"')";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Status - Record Add", "Status_RecordAdd()");
            }

            return iReturn;
        }

        public int RecordUpdate(int task_id, string task_name, string short_name)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Task_Dtls " +
                         " SET Task_ShortName = '" + short_name.Trim() + "', " +
                         " Task_Name = '" + task_name.Trim() + "' " +
                         " WHERE Task_Id = " + task_id;

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Update", "TaskType_RecordUpdate()");
            }

            return iReturn;
        }


        public int Mode_RecordUpdate(int task_id, string task_name, string short_name, string div_code)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Task_Mode " +
                         " SET Short_Name = '" + short_name.Trim() + "', " +
                         " Mode_Name = '" + task_name.Trim() + "' " +
                         " WHERE Division_code ='"+div_code+"' and Mode_Id = " + task_id;

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Mode - Record Update", "Mode_RecordUpdate()");
            }

            return iReturn;
        }

        public int Client_RecordUpdate(int task_id, string task_name, string short_name)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Task_client " +
                         " SET Short_Name = '" + short_name.Trim() + "', " +
                         " Client_Name = '" + task_name.Trim() + "' " +
                         " WHERE Client_Id = " + task_id;

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Client - Record Update", "Client_RecordUpdate()");
            }

            return iReturn;
        }

        public int Status_RecordUpdate(int task_id, string task_name, string short_name,string div_code)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Task_Status " +
                         " SET Short_Name = '" + short_name.Trim() + "', " +
                         " Status_Name = '" + task_name.Trim() + "' " +
                         " WHERE Division_code='"+div_code+"' and Status_Id = " + task_id;

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Status - Record Update", "Status_RecordUpdate()");
            }

            return iReturn;
        }

        //public int RecordAdd(string task_name, string short_name)
        //{
        //    int iReturn = -1;
        //    ErrorLog err = new ErrorLog();
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();
        //        strQry = "INSERT INTO Mas_Task_Dtls(Task_ShortName, Task_Name, Task_Flag) VALUES ( '" + short_name.Trim() + "', '" + task_name.Trim() + "', 0)";
        //        iReturn = db.ExecQry(strQry);

        //    }
        //    catch (Exception ex)
        //    {
        //        iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Add", "TaskType_RecordAdd()");
        //    }

        //    return iReturn;
        //}

        //public int RecordAdd_Details(int task_client, int task_type, string task_desc, string task_to, DateTime comp_date, int is_flex, int task_status, int task_mode, string task_sev)
        public int RecordAdd_Details(int task_client, int task_type, string task_desc, string task_to, string comp_date, int is_flex, int task_status, int task_mode, string task_sev, string task_by,string commit_date)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(Task_Det_ID),0)+1 FROM trans_task_details ";
                int Task_Det_ID = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO trans_task_details VALUES ( " + Task_Det_ID  + " " +
                        " , '" + task_client + "', '" + task_type + "', '" + task_desc + "', '" + task_to + "', '" + comp_date + "', '" + is_flex + "', " +
                        " '" + task_status + "', '" + task_mode + "', '" + task_sev + "', getdate(), getdate(),'" + task_by + "' ,'','','"+ commit_date + "')";
                iReturn = db.ExecQry(strQry);

                strQry = "INSERT INTO trans_task_details_History Select * from trans_task_details where Task_Det_ID = '" + Task_Det_ID + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Add - Details", "RecordAdd_Details()");
            }

            return iReturn;
        }
        public int RecordUpdate(int task_client, int task_type, string task_desc, string task_to, string comp_date, int is_flex, int task_status, int task_mode, string task_sev, string task_by, string Task_Det_ID,string commit_date)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();

                

                strQry = "UPDATE trans_task_details " +
                        " SET Task_Client = '" + task_client + "', " +
                        " Task_Type = " + task_type + ", " +
                        " Task_Desc = '" + task_desc + "'," +
                        " Task_To = '" + task_to + "'," +
                        " Task_Status = '" + task_status + "'," +
                        " Task_mode = '" + task_mode + "'," +
                        " Task_Sev = '" + task_sev + "'," +
                        " Completion_Date =  '" + comp_date + "', " +
                        " Updated_dtm = getdate(), " +
                        " Commitment_date =  '" + commit_date + "' " +
                        " WHERE Task_Det_ID = '" + Task_Det_ID + "' ";

                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    strQry = "INSERT INTO trans_task_details_History Select * from trans_task_details where Task_Det_ID = '" + Task_Det_ID + "'";
                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Add - Details", "RecordAdd_Details()");
            }

            return iReturn;
        }
        public bool TaskEntry_Exist(int Task_Client, string Task_To, int Task_Type, string comp_date, int Task_Mode)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(task_det_id) " +
                         " FROM Trans_Task_Details " +
                         " WHERE Task_Client = " + Task_Client +
                         " AND Task_To = '" + Task_To + "' " +
                         " AND Task_Type = " + Task_Type +
                         " AND Completion_Date = '" + comp_date + "' " +
                         " AND Task_Mode = " + Task_Mode;

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Entry - Maintenance", "TaskEntry_Exist()");
            }
            return bRecordExist;
        }


        public DataSet getTasks(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select a.Task_Det_ID,(select sf_name from mas_salesforce where a.Task_by = sf_code) as Task_By_Name," +
                        "(select sf_name from mas_salesforce where a.Task_to = sf_code) as Task_to_Name, " +
                        " a.Task_Desc, a.Is_Flexible , b.Client_Name, c.Task_Name, d.Status_Name, e.Mode_Name, a.Task_Sev, a.Task_To, a.Completion_Date, a.Commitment_date, f.Sf_Name " +
                        " from Trans_Task_Details a,Mas_Task_Client b, Mas_Task_Dtls c, Mas_Task_Status d, Mas_Task_Mode e, Mas_Salesforce f " +
                        " where a.Task_Client = b.Client_ID and a.Task_Type = c.Task_Id and a.Task_Status = d.Status_ID and a.Task_Mode = e.Mode_ID "+
                        " and ((a.Task_To = '" + sf_code + "' and a.Task_To = f.sf_code )  or (a.Task_by = '" + sf_code + "' and a.Task_by = f.sf_Code ) ) "+
                        " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task View", "getTasks()");
            }

            return dsSale;
        }

        public DataSet getTasks_Dtls(string Task_Det_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select a.Task_Det_ID, " +
                        " a.Task_Desc, a.Is_Flexible , a.Task_Client, a.Task_Type, a.Task_Status, a.Task_mode, a.Task_Sev, a.Task_To, convert(varchar(10),a.Completion_Date,103)Completion_Date, replace(convert(varchar(10),a.Commitment_date,103),'01/01/1900','') Commitment_date" +
                        " from Trans_Task_Details a " +
                        " where a.Task_Det_ID = '" + Task_Det_ID  +"' ";
                        
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task View", "getTasks()");
            }

            return dsSale;
        }
        public DataSet getTasks_DtlsHis(string sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "select a.Task_Det_ID, " +
                        " a.Task_Desc, a.Is_Flexible , a.Task_Client, a.Task_Type, a.Task_Status, a.Task_mode, a.Task_Sev, a.Task_To, a.Completion_Date,a.Commitment_date " +
                        " from Trans_Task_Details_History a " +
                        " where a.sl_no = '" + sl_no + "' ";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task View", "getTasks()");
            }

            return dsSale;
        }
        public DataSet getTasksHis(string Task_Det_ID,string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "select a.sl_no,a.Task_Det_ID,(select sf_name from mas_salesforce where a.Task_by = sf_code) as Task_By_Name," +
                       "(select sf_name from mas_salesforce where a.Task_to = sf_code) as Task_to_Name, " +
                       " a.Task_Desc, a.Is_Flexible , b.Client_Name, c.Task_Name, d.Status_Name, e.Mode_Name, a.Task_Sev, a.Task_To, a.Completion_Date, a.Commitment_date, f.Sf_Name,a.Created_dtm,a.Updated_dtm " +
                       " from Trans_Task_Details_History a,Mas_Task_Client b, Mas_Task_Dtls c, Mas_Task_Status d, Mas_Task_Mode e, Mas_Salesforce f " +
                       " where a.Task_Client = b.Client_ID and a.Task_Type = c.Task_Id and a.Task_Status = d.Status_ID and a.Task_Mode = e.Mode_ID " +
                       " and ((a.Task_To = '" + sf_code + "' and a.Task_To = f.sf_code )  or (a.Task_by = '" + sf_code + "' and a.Task_by = f.sf_Code ) ) " +
                       " and a.Task_Det_ID =  '" + Task_Det_ID + "' " +
                       " ORDER BY 2";

            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task View", "getTasks()");
            }

            return dsSale;
        }
        public DataSet sp_SalesForceMgrGet_Reporting(string div_code,string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;

            strQry = "exec sp_SalesForceMgrGet_Reporting '"+ div_code + "','"+ sfcode + "'";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Type - Maintenance", "getFieldForce()");
            }
            return dsSale;
        }
        public DataSet getTasks_All(string divcode,string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "exec sp_SalesForceMgrGet_All '"+divcode+"','"+sf_code+"' ";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task View", "getTasks_All()");
            }

            return dsSale;
        }
        public DataSet Get_Year()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division";
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
        public DataSet task_View(string divcode,string sfcode,string mnth, string yr,string mnth1,string yr1,string sts)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sts == "-1")
            {
                strQry = "exec sp_task_All_betweendates '" + divcode + "','" + sfcode + "','" + mnth + "','" + yr + "','" + mnth1 + "','" + yr1 + "','' ";
            }
            else
            {
                strQry = "exec sp_task_All_betweendates_Withsts '" + divcode + "','" + sfcode + "','" + mnth + "','" + yr + "','" + mnth1 + "','" + yr1 + "','" + sts + "' ";
            }
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


        public int RecordAdd_Details_Task(string mode_id, string mode_Name, string task_from_code, string task_from_name, string Desc,string Prior, string From_date, string To_date, string task_to_code, string task_to_name, string div_code)
        {
            int iReturn = -1;
            ErrorLog err = new ErrorLog();
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(Task_ID),0)+1 FROM Trans_Task_Details ";
                int Task_Det_ID = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Trans_Task_Details(Task_ID,Mode_Id,Mode_Name,Task_From_Code,Task_From_Name,Task_Desc, " +
                         " Priority,DeadLine_From,DeadLine_To,Task_To_Code,Task_To_Name,Created_Date,Updated_Date,Task_Status_Id, " +
                         " Task_Status_Name,Completed_Date,Division_Code) " +
                         " VALUES('" + Task_Det_ID + "','"+mode_id+"','"+mode_Name+"','"+task_from_code+"','"+task_from_name+"', " +
                         " '" + Desc + "','" + Prior + "','" + From_date + "','" + To_date + "','" + task_to_code + "','" + task_to_name + "',getdate(),NULL,1,'New',Null,'"+div_code+"'  )";
                iReturn = db.ExecQry(strQry);

                strQry = "INSERT INTO Trans_Task_Details_History Select * from Trans_Task_Details where Task_ID = '" + Task_Det_ID + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                iErrReturn = err.LogError(-1, ex.Message.ToString().Trim(), "Task Type - Record Add - Details", "RecordAdd_Details()");
            }

            return iReturn;
        }
        public DataSet getTaskMode_DV(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSale = null;
            strQry = "SELECT Mode_ID, " +
                            " Short_Name, " +
                            " Mode_Name " +
                     " FROM Mas_Task_Mode " +
                     " WHERE Mode_Flag = 0 and Division_Code='" + div_code + "'" +
                     " ORDER BY 2";
            try
            {
                dsSale = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                ErrorLog err = new ErrorLog();
                iErrReturn = err.LogError(0, ex.Message.ToString().Trim(), "Task Mode - Maintenance", "getTaskMode()");
            }
            return dsSale;
        }
    }
}
