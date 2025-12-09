using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class UserLogin
    {
        private string strQry = string.Empty;

        public DataSet Process_Login(string usr_id, string pwd)
        {
            if (usr_id == "admin")
            {
                strQry = "select Sf_Code, sf_name," +
                        " case when sf_code='admin' then '' else  left(Division_Code,len(Division_Code))  end as Division_code, " +
                       " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name, Sf_HQ ,sf_status , '' standby,Designation_Code" +
                       "  from mas_salesforce_Three " +
                       " Where Sf_UserName= '" + usr_id + "' and Sf_Password= '" + pwd + "'  " +
                       " and sf_status=0 and sf_TP_Active_Flag=0";
            }
            else
            {
                //strQry = " select a.Sf_Code, a.sf_name, case when a.sf_code='admin' then '' else  left(b.Division_Code,len(a.Division_Code)) " +
                //        " end as Division_code,  sf_TP_Active_Flag,sf_type,a.sf_Designation_Short_Name, a.Sf_HQ   from Mas_Salesforce a,Mas_Salesforce_AM b  " +
                //         " Where (a.Sf_UserName= '" + usr_id + "' or a.UsrDfd_UserName = '" + usr_id + "') and a.Sf_Password= '" + pwd + "' and a.sf_status=0 and sf_TP_Active_Flag=0 and a.Sf_Code=b.Sf_Code  ";

                strQry = " select a.Sf_Code, a.sf_name, case when a.sf_code='admin' then '' else  left(b.Division_Code,len(a.Division_Code)) " +
                       " end as Division_code,  sf_TP_Active_Flag,sf_type,a.sf_Designation_Short_Name, a.Sf_HQ,a.sf_status, " +
                           " (select Division_Name from Mas_Division d where d.Division_Code= b.Division_Code )  div_name, " +
                         "  (select Standby from Mas_Division d where d.Division_Code= b.Division_Code )  standby,a.Designation_Code,a.sf_vacantblock from mas_salesforce_Three a,Mas_Salesforce_AM b  " +
                        " Where (a.UsrDfd_UserName = '" + usr_id + "') and a.Sf_Password= '" + pwd + "'  and a.Sf_Code=b.Sf_Code  ";
            }

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

          
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

        public int Audit_Report_Details(string sf_code, string sf_name, string div_code, string div_Name, string sRemote,
                                     string To_Code, string To_Name, string From_Month, string From_year, string To_Month, string To_Year, string Mode_Repeat)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;


                strQry = " INSERT INTO Audit_Report_Details(From_SF_Code, From_SF_Name, Division_code, Division_Name,Login,IP_Address,To_Code,To_Name,From_Month,From_Year,To_Month,To_Year,Mode_of_Repeat) " +
                         " VALUES ( '" + sf_code + "', '" + sf_name + "', '" + div_code + "', '" + div_Name + "',getdate(),'" + sRemote + "','" + To_Code + "','" + To_Name + "','" + From_Month + "','" + From_year + "','" + To_Month + "','" + To_Year + "','" + Mode_Repeat + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet HO_Login(string usr_id, string pwd)
        {


            //strQry = " declare @SubDivicode as varchar(100) " +
            //         " set @SubDivicode=(select Division_Name from Mas_Division where Division_Code =(select reverse(stuff(reverse(Division_Code), 1, 1, '')) from Mas_HO_ID_Creation " +
            //         " Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0)) " +
            //         " select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate " +
            //         " from Mas_HO_ID_Creation Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag=0  ";


            //  strQry = " declare @SubDivicode as varchar(100) " +
            //" declare @standby as varchar(100) " +
            //" set @SubDivicode=(select Division_Name from Mas_Division where Division_Code = " +
            //" (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
            //" Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  " +
            //" set @standby=(select standby from Mas_Division where Division_Code = (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
            //" Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag =0))  " +
            //" select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,Sub_HO_ID from Mas_HO_ID_Creation " +
            //"  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag=0  ";

            //strQry = "  declare @SubDivicode as varchar(100)  " +
            //    "  declare @standby as varchar(100)  " +
            //    "  declare @Div_code varchar(100) " +
            //    "  set @Div_code=(select division_code  from Mas_HO_ID_Creation  Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0) " +
            //    "  print @Div_code " +
            //    "  set @SubDivicode=(select  Division_Name from Mas_Division where Division_Code= (SUBSTRING(@Div_code,0,charindex(',',@Div_code)))) " +
            //    "  set @standby=(select standby from Mas_Division where  Division_Code=(SUBSTRING(@Div_code,0,charindex(',',@Div_code))))   " +
            //    "  select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as " +
            //    "  standby,Sub_HO_ID from Mas_HO_ID_Creation   Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag=0";

            strQry = "EXEC GetLoginUsrPwd '" + usr_id + "','" + pwd + "' ";

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


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

        public DataSet Check_Mail(string sf_code, string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsMail = null;

            strQry = "EXEC CheckMail '" + sf_code + "', '" +  Convert.ToInt32(div_code) + "' ";

            try
            {
                dsMail = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsMail;
        }
        public DataSet Process_LoginMr(string Sf_Code, string Sf_Password)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name," +
                    " case when sf_code='admin' then '' else  left(Division_Code,len(Division_Code))  end as Division_code, " +
                   " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ " +
                   "  from Mas_Salesforce " +
                   " Where Sf_Code='" + Sf_Code + "' and sf_status=0  " +
                    " (select Password from Mas_HO_ID_Creation where Password='" + Sf_Password + "')";
               

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
        public DataSet ProcessMgr_LoginMr(string Sf_Code, string Sf_Password, string smgr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ," +
                     " Division_code " +                   
                     " from Mas_Salesforce " +
                     " Where sf_code='" + Sf_Code + "' " +
                     "(select Sf_Password from Mas_Salesforce where sf_code = '" + smgr + "' and Sf_Password='" + Sf_Password + "')";


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
        public DataSet ProcessMgr_Login(string Sf_Code, string Sf_Password, string smgr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ," +
                     " Division_code " +
                     " from Mas_Salesforce " +
                     " Where sf_code='" + Sf_Code + "' " +
                     "(select Sf_Password from Mas_Salesforce where sf_code = '" + smgr + "' and Sf_Password='" + Sf_Password + "')";


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
        public int Login_details(string sf_code, string sf_name, string div_code, string sRemote)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;


                strQry = " INSERT INTO Login_Details(Sf_Code, Sf_Name, Division_Code, Log_In,IP_Address) " +
                         " VALUES ( '" + sf_code + "', '" + sf_name + "', '" + div_code + "', getdate(),'" + sRemote + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Master_Menu_ADD(string Menu_Name, string Ho_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select  isnull(max(Cast((Master_Menu_Id) as int) ),0) + 1 from Master_Menu_Rights  ";
                int Master_Menu_Id = db.Exec_Scalar(strQry);
                if (Menu_RecordExist(Ho_Id))
                {
                    strQry = " update Master_Menu_Rights set Menu_Name='" + Menu_Name + "' " +
                             " where  Ho_Id = '" + Ho_Id + "'";
                }
                else
                {
                    strQry = " INSERT INTO Master_Menu_Rights(Master_Menu_Id, Menu_Name, Ho_Id, Created_Date) " +
                             " VALUES ( '" + Master_Menu_Id + "' ,'" + Menu_Name + "', '" + Ho_Id + "', getdate()) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool Menu_RecordExist(string Ho_Id)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Master_Menu_Rights " +
                         " where Ho_Id = '" + Ho_Id + "'  ";

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
        public DataSet Master_Menu_View(string Ho_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Menu_Name, Ho_Id " +
                   "  from Master_Menu_Rights " +
                   " Where Ho_Id='" + Ho_Id + "'";



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
        public DataSet MGR_Menu_View(string Ho_Id, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "select MGR_Menu_Name, Ho_Id,MGR_Menu_Id " +
                   "  from MGR_Menu_Rights " +
                   " Where Ho_Id ='" + Ho_Id + "' and Division_code='"+div_code+"'";


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
        public DataSet MR_Menu_View(string Ho_Id, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select MR_Menu_Name, Ho_Id,MR_Menu_Id " +
                   "  from MR_Menu_Rights " +
                   " Where Ho_Id = '" + Ho_Id + "' and Division_code='"+div_code+"' ";



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
        public DataSet MGR_Menu_View_Div(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            if (Division_Code.Contains(","))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            strQry = "select MGR_Menu_Name, Ho_Id,MGR_Menu_Id " +
                   "  from MGR_Menu_Rights " +
                    " Where  (Division_Code like '" + Division_Code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + Division_Code + ',' + "%') ";


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
        public DataSet MR_Menu_View_Div(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (Division_Code.Contains(","))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            strQry = "select MR_Menu_Name, Ho_Id,MR_Menu_Id " +
                   "  from MR_Menu_Rights " +
                  " Where  (Division_Code like '" + Division_Code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + Division_Code + ',' + "%') ";



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


        public int MGR_Menu_ADD(string Menu_Name, string Ho_Id)
        {
            int iReturn = -1;
            string Division_Code = string.Empty;
            DataSet div_code = new DataSet();
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select  isnull(max(Cast((MGR_Menu_Id) as int) ),0) + 1 from MGR_Menu_Rights  ";
                int MGR_Menu_Id = db.Exec_Scalar(strQry);

                strQry = "select division_code from mas_ho_Id_creation where Ho_Id = '" + Ho_Id + "' ";
                div_code = db.Exec_DataSet(strQry);
                Division_Code = div_code.Tables[0].Rows[0][0].ToString();
                if (MGR_Menu_RecordExist(Division_Code))
                {
                    strQry = " update MGR_Menu_Rights set MGR_Menu_Name='" + Menu_Name + "' " +
                             " where  Division_Code = '" + Division_Code + "'";
                }
                else
                {
                    strQry = " INSERT INTO MGR_Menu_Rights(MGR_Menu_Id, MGR_Menu_Name, Ho_Id,Division_Code, Created_Date) " +
                             " VALUES ( '" + MGR_Menu_Id + "' ,'" + Menu_Name + "', '" + Ho_Id + "','" + Division_Code + "', getdate()) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool MGR_Menu_RecordExist(string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from MGR_Menu_Rights " +
                         " where Division_Code = '" + Division_Code + "'  ";

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
        public int MR_Menu_ADD(string Menu_Name, string Ho_Id)
        {
            int iReturn = -1;
            string Division_Code = string.Empty;
            DataSet div_code = new DataSet();
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select  isnull(max(Cast((MR_Menu_Id) as int) ),0) + 1 from MR_Menu_Rights  ";
                int MR_Menu_Id = db.Exec_Scalar(strQry);

                strQry = "select division_code from mas_ho_Id_creation where Ho_Id = '" + Ho_Id + "' ";
                div_code = db.Exec_DataSet(strQry);
                Division_Code = div_code.Tables[0].Rows[0][0].ToString();
                if (MR_Menu_RecordExist(Division_Code))
                {
                    strQry = " update MR_Menu_Rights set MR_Menu_Name='" + Menu_Name + "' " +
                             " where  Division_Code = '" + Division_Code + "'";
                }
                else
                {
                    strQry = " INSERT INTO MR_Menu_Rights(MR_Menu_Id, MR_Menu_Name, Ho_Id,Division_Code, Created_Date) " +
                             " VALUES ( '" + MR_Menu_Id + "' ,'" + Menu_Name + "', '" + Ho_Id + "','" + Division_Code + "', getdate()) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool MR_Menu_RecordExist(string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from MR_Menu_Rights " +
                         " where Division_Code = '" + Division_Code + "'  ";

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
        public DataSet Process_LoginAll(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name," +
                    " case when sf_code='admin' then '' else  left(Division_Code,len(Division_Code))  end as Division_code, " +
                   " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ " +
                   "  from Mas_Salesforce " +
                   " Where Sf_Code='" + Sf_Code + "' and sf_status !=2   ";



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
        public DataSet Get_Sub_Id(string Ho_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select Sub_HO_ID " +
                   "  from Mas_HO_ID_Creation " +
                   " Where Ho_Id = '" + Ho_Id + "';";



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

        public DataSet Tp_Auto_Setup_new(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select SingleDr_WithMultiplePlan_Required from admin_setups " +
                   " Where division_code='" + div_code + "' ";



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
    }
}
