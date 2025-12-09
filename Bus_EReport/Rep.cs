using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DBase_EReport;
using System.Text;
using System.Configuration;


namespace Bus_EReport
{
    public class Rep
    {
        private string strQry = string.Empty;

        public int AddJoinee(string sf_code, string sf_name, string sf_Hq, string sf_Desig, string name, string Desig, string hq, string doj, string wrkbag, string samples, string stationary, string visualaid, string productglossary, string readyreckoner, string address, string replacename, string propertycollected, string div_code)
        {
            int iReturn = -1;

            try
            {
                int Sl_No = -1;
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Trans_NewJoinee_Kit ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "insert into Trans_NewJoinee_Kit(Sl_No,From_Sf_Code,From_Sf_Name,From_Hq,From_Designation,N_C,N_C_D, " +
                "N_C_H,DOJ,Work_Bag,Samples,Stationary,Visual_Aid,Product_Glossary,Ready_Reckoner,Add_sample_request,Replacement_nme, " +
                "C_P_C,Division_Code,Created_Date)VALUES('" + Sl_No + "','" + sf_code + "','" + sf_name + "-" + sf_Hq + "-" + sf_Desig + " ','" + sf_Hq + "','" + sf_Desig + "','" + name + "','" + Desig + "','" + hq + "','" + doj + "','" + wrkbag + "', " +
                "'" + samples + "','" + stationary + "','" + visualaid + "','" + productglossary + "','" + readyreckoner + "','" + address + "','" + replacename + "','" + propertycollected + "','" + div_code + "',getdate())";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet gettraineekit(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select Sl_No,From_Sf_Name,From_Hq,From_Designation,(select sf_emp_id from mas_salesforce b where a.From_Sf_Code=b.sf_code)Emp_id,convert(varchar, created_date, 105)dt  " +
                     "from Trans_NewJoinee_Kit a where Division_Code='"+div_code+ "' order by Created_Date DESC ";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }
        public DataSet gettraineekitview(string id,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select Sl_No,From_Sf_Code,From_Sf_Name,From_Hq,From_Designation,N_C,N_C_D,N_C_H,convert(varchar, DOJ, 105)DOJ,Work_Bag,Samples,Stationary,Visual_Aid,Product_Glossary,Ready_Reckoner,Add_sample_request,Replacement_nme,C_P_C,convert(varchar,Created_Date,105)Created_Date  " +
                     "from Trans_NewJoinee_Kit  where Sl_No='" + id + "' and Division_Code='" + div_code + "'";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }
        public DataSet gettraineekitcnt(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select count(Sl_No)cnt  " +
                     "from Trans_NewJoinee_Kit  where Division_Code='" + div_code + "'";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }
        public DataSet getRcmdview(string id, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select Sl_No,Sf_Code,Sf_name,Sf_Hq,Sf_Desig,Reporting_Name,Salary,Detailing_Gud,Detailing_Avg,Detailing_Poor,Detailing_Comm_FB,Inchamber_Gud, " +
                     "Inchamber_Avg,Inchamber_Poor,Inchamber_Comm_FB,Work_Punct_Gud,Work_Punct_Avg,Work_Punct_Poor,Work_Punct_Comm_FB,Report_Punct_Gud,Report_Punct_Avg, " +
                     "Report_Punct_poor,Report_Punct_Commen_FB,Perf_Exist_Target,Perf_Fmnth_Target,Perf_Smnth_Target,Perf_Tmnth_Target,Perf_Frthmnth_Target,Perf_Fivthmnth_Target, " +
                     "Perf_sixmnth_Target,Per_Achve_Fmnth,Per_Achve_Smnth,Per_Achve_Tmnth,Per_Achve_Frthmnth,Per_Achve_Fivthmnth,Per_Achve_sixthmnth,Per_Achve_ext,Per_Achve_ext_sec, " +
                     "Per_Achve_Fmnth_sec,Per_Achve_Smnth_sec,Per_Achve_Tmnth_sec,Per_Achve_Frthmnth_sec,Per_Achve_Fivthmnth_sec,Per_Achve_sixthmnth_sec,Performance_Fmnth,Performance_Smnth, " +
                     "Performance_Tmnth,Performance_Frthmnth,Performance_Fivthmnth,Performance_Sxthmnth,Last_Three_Callavg_F,Last_Three_Callavg_S,Last_Three_Callavg_T,Last_Three_MarCov_F, " +
                     "Last_Three_MarCov_S,Last_Three_MarCov_T,recmd_name,convert(varchar,recmd_doj, 105)recmd_doj,recmd_Hq,recmd_rptname,div_code,convert(varchar,created_date,105)created_date,recode,convert(varchar,Confirm_Date,105)Confirm_Date from Trans_Recommendation_Confirm where Sl_No='" + id + "' and div_code='" + div_code + "'";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }
        public int AddRecommendation(string sf_code, string sf_name, string sf_Hq, string sf_Desig, string labeladm, string rename, string recode, string redoj, string rehq, string rerptname,
            string salary, string Detailgud, string DetailAvg, string DetailPoor, string Detailcomment, string Inchambergud, string Inchamberavg, string Inchamberpoor,
            string Inchamvercomment, string wrkgud, string wrkavg, string wrkpoor, string wrkcomment, string reportgud, string reportavg, string reportpoor, string reportcomment,
            string pertarext, string pertar1mnth, string pertar2mnth, string pertar3mnth, string pertar4mnth, string pertar5mnth, string pertar6mnth, string achipriext, string achipri1mnth,
            string achivepri2mnth, string achivepri3mnth, string achievepri4mnth, string achivepri5mnht, string achive6mnth, string achvesecext, string achesec1mnht, string achesec2mnth,
            string achvesec3mnth, string achvesec4mnth, string achvesec5mnth, string achvesec6mnht, string per1mnht, string per2mnth, string per3mnth, string per4mnth, string per5mnth, string per6mnth,
            string lc1mnth, string lc2mnth, string lc3mnht, string lmc1mnth, string lmc2mnth, string lmc3mnht, string div_code,string confirm_dte)
        {
            int iReturn = -1;

            try
            {
                int Sl_No = -1;
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Trans_Recommendation_Confirm  ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "insert into Trans_Recommendation_Confirm(Sl_No,Sf_Code,Sf_name,Sf_Hq,Sf_Desig,Reporting_Name,Salary,Detailing_Gud,Detailing_Avg, " +
                         "Detailing_Poor,Detailing_Comm_FB,Inchamber_Gud,Inchamber_Avg,Inchamber_Poor,Inchamber_Comm_FB,Work_Punct_Gud,Work_Punct_Avg,Work_Punct_Poor, " +
                         "Work_Punct_Comm_FB,Report_Punct_Gud,Report_Punct_Avg,Report_Punct_poor,Report_Punct_Commen_FB,Perf_Exist_Target,Perf_Fmnth_Target,Perf_Smnth_Target, " +
                         "Perf_Tmnth_Target,Perf_Frthmnth_Target,Perf_Fivthmnth_Target,Perf_sixmnth_Target,Per_Achve_Fmnth,Per_Achve_Smnth,Per_Achve_Tmnth,Per_Achve_Frthmnth, " +
                         "Per_Achve_Fivthmnth,Per_Achve_sixthmnth,Per_Achve_ext,Per_Achve_ext_sec,Per_Achve_Fmnth_sec,Per_Achve_Smnth_sec,Per_Achve_Tmnth_sec,Per_Achve_Frthmnth_sec, " +
                         "Per_Achve_Fivthmnth_sec,Per_Achve_sixthmnth_sec,Performance_Fmnth,Performance_Smnth,Performance_Tmnth,Performance_Frthmnth,Performance_Fivthmnth,Performance_Sxthmnth, " +
                         "Last_Three_Callavg_F,Last_Three_Callavg_S,Last_Three_Callavg_T,Last_Three_MarCov_F,Last_Three_MarCov_S,Last_Three_MarCov_T,div_code,created_date,recmd_name,recmd_doj,recmd_Hq,recmd_rptname,recode,Confirm_Date)VALUES('" + Sl_No + "','" + sf_code + "','" + sf_name + "-" + sf_Hq + "-" + sf_Desig + " ','" + sf_Hq + "','" + sf_Desig + "','" + labeladm + "','" + salary + "','" + Detailgud + "','" + DetailAvg + "','" + DetailPoor + "','" + Detailcomment + "', " +
                "'" + Inchambergud + "','" + Inchamberavg + "','" + Inchamberpoor + "','" + Inchamvercomment + "','" + wrkgud + "','" + wrkavg + "','" + wrkpoor + "','" + wrkcomment + "', " +
                "'" + reportgud + "','" + reportavg + "','" + reportpoor + "','" + reportcomment + "','" + pertarext + "','" + pertar1mnth + "','" + pertar2mnth + "','" + pertar3mnth + "', " +
                "'" + pertar4mnth + "','" + pertar5mnth + "','" + pertar6mnth + "','" + achipriext + "','" + achipri1mnth + "','" + achivepri2mnth + "','" + achivepri3mnth + "','" + achievepri4mnth + "', " +
                 "'" + achivepri5mnht + "','" + achive6mnth + "','" + achvesecext + "','" + achesec1mnht + "','" + achesec2mnth + "','" + achvesec3mnth + "','" + achvesec4mnth + "','" + achvesec5mnth + "', " +
                 "'" + achvesec6mnht + "','" + per1mnht + "','" + per2mnth + "','" + per3mnth + "','" + per4mnth + "','" + per5mnth + "','" + per6mnth + "','" + lc1mnth + "', " +
                 "'" + lc2mnth + "','" + lc3mnht + "','" + lmc1mnth + "','" + lmc2mnth + "','" + lmc3mnht + "', " +
                 "'" + div_code + "',getdate(),'" + rename + "','" + redoj + "','" + rehq + "','" + rerptname + "','" + recode + "','"+ confirm_dte + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getSFCode(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC Hierarchy_Team_mgr '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getfieldforce(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "select Sf_Name,convert(varchar, Sf_Joining_Date, 101)Sf_Joining_Date,Reporting_To_SF,Sf_HQ from mas_salesforce where sf_code='" + sf_code + "' and division_code='" + divcode + ",'";

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
        public DataSet getrecmdcnt(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select count(Sl_No)cnt  " +
                     "from Trans_Recommendation_Confirm  where div_code='" + div_code + "'";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }
        public DataSet getRecommendview(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsrep = null;

            strQry = "select Sl_No,Sf_name,Sf_Hq,convert(varchar, created_date, 105)dt,(select sf_emp_id from mas_salesforce b where a.Sf_Code=b.sf_code)Emp_Id,(select sf_Designation_Short_Name from mas_salesforce c where a.Sf_Code=c.sf_code)Desig from Trans_Recommendation_Confirm a where a.div_code='" + div_code + "' order by Created_Date DESC";
            try
            {
                dsrep = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsrep;
        }

    }
}
