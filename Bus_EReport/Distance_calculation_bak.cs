using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace Bus_EReport
{
    public class Distance_calculation
    {
        public Distance_calculation()
        {
        }
        private string strQry = string.Empty;
        public DataSet getFieldForce(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT sf_code,sf_name,sf_hq,sf_designation_short_name " +
                     " FROM mas_salesforce WHERE sf_status=0 and sf_type=1 and Division_Code ='" + divcode + ',' + "'" +
                     " ORDER BY sf_name";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataSet getMR(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec sp_SalesForceGet_MR '" + divcode + "', '" + sf_code + "'";
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
        public DataSet getRegion(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = " select sf_code,sf_name from mas_salesforce S inner join Mas_SF_Designation D on S.Designation_Code=D.Designation_Code where sf_type=2 and (S.Division_Code like '" + divcode + ',' + "%'  or  S.Division_Code like '%" + ',' + divcode + ',' + "%' )" +
                     " ORDER BY sf_name";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getAllowFare(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select sum(expense_allowance)allw,SUM(Expense_Fare) fare,sndhqfl Status,SF_Code,submission_date,isnull(SUM(rw_amount),0) rw_amount from Trans_Expense_Head H inner join Trans_Expense_Detail D on H.Sl_No=D.sl_no where Expense_Month=" + month + " and Expense_Year=" + year + " group by SF_Code,sndhqfl,submission_date";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getmis(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select SF_Code,isnull(SUM(Amt),0)mis_Amt from Exp_Others O inner join Trans_Expense_Head H on O.sl_No=H.Sl_No where Expense_Month=" + month + " and Expense_Year=" + year + " group by SF_Code";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getHeadRecord(string month, string year, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable headR = null;
            strQry = "select sndhqfl Status from Trans_Expense_Head where Expense_Month=" + month + " and Expense_Year=" + year + " and sf_code='" + sfCode + "'";
            try
            {
                headR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return headR;
        }
        public DataTable getFixedClmnName(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name from Fixed_Variable_Expense_Setup where division_code='" + divcode + "' and Param_type='F' order by Expense_Parameter_Code";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getOtherExpDetails(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select D.Expense_Parameter_Code,Expense_Parameter_Name,H.SF_Code,Amt from exp_fixed D inner join trans_expense_head H on D.sl_no=H.sl_no inner join Fixed_Variable_Expense_Setup F on F.expense_parameter_code=D.Expense_Parameter_Code where Expense_Month=" + month + " and Expense_Year=" + year;
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getSavedFixedExp(string month, string year, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select F.Expense_Parameter_Code,Expense_Parameter_Name,Amt amount from exp_fixed D inner join Fixed_Variable_Expense_Setup F on D.expense_parameter_code=f.expense_parameter_code inner join trans_expense_head H on H.sl_no=D.sl_no  where H.sf_code='" +
                sfCode + "' and Expense_Month=" + month + " and Expense_Year=" + year;
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable TWD(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();
            DataTable dsDivision = null;
            strQry = "select count(Activity_Date)cnt from DCRMain_Trans where sf_Code='" + sf_code + "'  and month(activity_date)=" + month + " and year(activity_date)=" + year + " and fieldwork_indicator in('F','N')";
            try
            {


                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable FW(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "select count(Activity_Date)cnt from DCRMain_Trans where sf_Code='" + sf_code + "'  and month(activity_date)=" + month + " and year(activity_date)=" + year + " and fieldwork_indicator in('F')";
            DataTable dsDivision = null;
            try
            {

                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }

        public int updateApprHeaderData(string expType, string sf_code, string month, string year, string rmks, string sesscode, string sessname)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set sndhqfl=" + expType + ",Remarks='" + rmks + "',First_level_Mgr='" + sesscode + "',First_level_Mgr_Name='" + sessname + "',Approval_Datea=getdate() WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataTable THQ(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "select count(expense_all_type)cnt from Trans_Expense_Detail where expense_all_type='HQ' and sl_no in(select sl_no from trans_expense_head where sf_code='" + sf_code + "' and expense_month=" + month + " and expense_year=" + year + ")";
            DataTable dsDivision = null;

            try
            {

                dsDivision = db.Exec_DataTable(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable TEX(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "select count(expense_all_type)cnt from Trans_Expense_Detail where expense_all_type='EX' and sl_no in(select sl_no from trans_expense_head where sf_code='" + sf_code + "' and expense_month=" + month + " and expense_year=" + year + ")";
            DataTable dsDivision = null;

            try
            {

                dsDivision = db.Exec_DataTable(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable TOS(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "select count(expense_all_type)cnt from Trans_Expense_Detail where expense_all_type='OS' and sl_no in(select sl_no from trans_expense_head where sf_code='" + sf_code + "' and expense_month=" + month + " and expense_year=" + year + ")";
            DataTable dsDivision = null;

            try
            {

                dsDivision = db.Exec_DataTable(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable Drsmet(string sf_code, string month, string year)
        {
            DB_EReporting db = new DB_EReporting();

            strQry = " select metcnt,madecnt,case when fw=0 then 0 else round(cast(madecnt as float)/cast(fw as float),2) end calavg  from (select isnull(COUNT(distinct Trans_Detail_Info_Code),0)metcnt,isnull(COUNT(Trans_Detail_Info_Code),0)madecnt,(select isnull(count(activity_date),0) from DCRMain_Trans where Sf_Code='" + sf_code + "' and MONTH(Activity_Date)=" + month + " and YEAR(Activity_Date)=" + year + " and fieldwork_indicator in('F'))fw from DCRDetail_Lst_Trans D " +
"inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo " +
"inner join Mas_ListedDr L on L.Listeddrcode=D.Trans_Detail_Info_Code " +
"inner join Mas_Territory_Creation T on CAST(ltrim(T.Territory_Code) as varchar)=L.Territory_Code " +
"inner join  mas_salesforce sf on sf.sf_code=T.sf_code " +
"where H.Sf_Code='" + sf_code + "' and MONTH(Activity_Date)=" + month + " and YEAR(Activity_Date)=" + year + ")as DD";
            DataTable dsDivision = null;
            try
            {

                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataSet getFilterRgn(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "exec sp_SalesForceMgrGet_WoVacant '" + divcode + "','" + sf_code + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }

        public DataSet getFilterRgn_Vacant(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "exec sp_SalesForceGet_Vacant '" + divcode + "','" + sf_code + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }

        public DataTable getFieldForce(string divcode, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT sf_code,sf_name,sf_hq,sf_type,sf_emp_id as Employee_Id,sf_designation_short_name " +
                     " FROM mas_salesforce WHERE sf_code='" + sfcode + "' and sf_status=0 and Division_Code like '%" + divcode + "%'" +
                     " ORDER BY sf_name";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDist(string divCode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dist = null;
            strQry = "Select From_Code FrmTown,To_Code ToTown,Distance_In_Kms Distance from Mas_Distance_Fixation where sf_Code='" + sf_code + "' and division_code='" + divCode + "'";
            try
            {
                dist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dist;


        }
        public DataTable getotherWorkType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT Work_Type_Name,Work_Type_Code Allow_type " +
                     " FROM Worktype_Allowance_Setup WHERE type=1 and Division_Code='" + divcode + "'" +
                     " ORDER BY Work_Type_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getotherFieldWorkType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT Work_Type_Name,Work_Type_Code Allow_type " +
                     " FROM Worktype_Allowance_Setup WHERE type=1 and allow_type!='NA' and Work_Type_name='Field Work' and Division_Code='" + divcode + "'" +
                     " ORDER BY Work_Type_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getotherFieldWorkTypeEXOS(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "SELECT Work_Type_Name,Work_Type_Code Allow_type " +
                     " FROM Worktype_Allowance_Setup WHERE type=1 and Work_Type_Code='EXOS' and Work_Type_name='Field Work' and Division_Code='" + divcode + "'" +
                     " ORDER BY Work_Type_Name";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }

        public DataTable getFrmandTo(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select territory_code,Territory_Name,Territory_Cat, " +
            " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Town_Cat " +
                    "from Mas_Territory_Creation T where Territory_Cat in(2,3,4) and  T.Sf_Code='" + sf_code + "' AND T.Division_Code like '" + divcode + "'";


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

        public DataSet getDistance(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select From_Code,To_Code,Sf_HQ,Territory_Name,Distance_In_Kms, " +
            " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Town_Cat, " +
                       " (select territory_name from mas_territory_creation where cast(territory_code as varchar)=From_Code)  as From_T " +
            " from Mas_Distance_Fixation D inner join Mas_Salesforce S on D.SF_Code=S.Sf_Code " +
        "inner join Mas_Territory_Creation T on T.Territory_Code=D.To_Code where S.Sf_Code='" + sf_code + "' AND T.Division_Code like '" + divcode + "'" +
            " and  T.territory_active_flag=0 ";


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
        public DataTable getTerritory(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select territory_code,Territory_Name, " +
            " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Town_Cat " +
                    "from Mas_Territory_Creation T where T.Sf_Code='" + sf_code + "' AND T.Division_Code like '" + divcode + "'";


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
        public DataSet getPlace(string divcode, string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "select T.Territory_Code,Territory_Name,sf_hq, " +
             " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Territory_Cat, " +
            "Activity_Date,COUNT(distinct Trans_Detail_Info_Code)cnt from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo inner join Mas_ListedDr L on L.Listeddrcode=D.Trans_Detail_Info_Code inner join Mas_Territory_Creation T on CAST(ltrim(T.Territory_Code) as varchar)=L.Territory_Code inner join  mas_salesforce sf on sf.sf_code=T.sf_code " +
        "where H.Sf_Code='" + sf_code + "' and D.Division_Code='" + divcode + "' and MONTH(Activity_Date)='" + mnth + "' and YEAR(Activity_Date)='" + yr + "' group by T.Territory_Code,Territory_Name,Territory_Cat,sf_hq,Activity_Date order by Activity_Date";


            try
            {
                dsPlace = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsPlace;
        }
        public DataSet getExpense(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpense '" + sf_code + "', '" + mnth + "','" + yr + "'";
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
        public DataSet getAllow(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select HQ_Allowance hq_allowance,EX_HQ_Allowance ex_allowance,OS_Allowance os_allowance,FareKm_Allowance fare,Range_of_Fare1,Range_of_Fare2 from Mas_Allowance_Fixation where SF_Code='" + sf_code + "' ";
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
        public DataTable getOthrExpold(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,Fixed_Amount from Fixed_Variable_Expense_Setup where Division_code='" + divcode + "' and Param_type<>'F' ";

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
        public DataTable getOthrExp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,Fixed_Amount from Fixed_Variable_Expense_Setup where Division_code='" + divcode + "' and base_level=1 and Param_type<>'F' ";

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

        public DataTable getOthrExpMGR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,Fixed_Amount1 from Fixed_Variable_Expense_Setup where Division_code='" + divcode + "' and MGR_Lines=1 and Param_type<>'F' ";

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
        public DataTable getExpParam(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select M.Expense_Parameter_Code,M.Expense_Parameter_Name from Mas_Expense_Parameter M inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "'  order by M.Expense_Parameter_Name";
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
        public DataTable getExpParamAmt(string sf_code, String divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            //strQry = "select sf_code,M.expense_parameter_code,M.Expense_Parameter_Name ,amount from Trans_Fixed_Expense_Detail D inner join Trans_Fixed_Expense_Head H on D.sl_no=H.sl_no left outer join Mas_Expense_Parameter M on M.Expense_Parameter_Code=H.expense_parameter_code inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where sf_code='" + sf_code + "' and F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "' ";
            strQry = "select * from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and F.division_code='" + divcode + "' and base_level=1 and Param_type='F')as dd";
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
        public DataTable getExpParamAmtMGR(string sf_code, String divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            //strQry = "select sf_code,M.expense_parameter_code,M.Expense_Parameter_Name ,amount from Trans_Fixed_Expense_Detail D inner join Trans_Fixed_Expense_Head H on D.sl_no=H.sl_no left outer join Mas_Expense_Parameter M on M.Expense_Parameter_Code=H.expense_parameter_code inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where sf_code='" + sf_code + "' and F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "' ";
            strQry = "select * from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and F.division_code='" + divcode + "' and MGR_Lines=1 and Param_type='F')as dd";
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
        public DataTable getVA_FA_Details(string sf_code, String divcode, string allwTyp)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            if (allwTyp == "VA")
            {
                //strQry = "select sf_code,M.expense_parameter_code,M.Expense_Parameter_Name ,amount from Trans_Fixed_Expense_Detail D inner join Trans_Fixed_Expense_Head H on D.sl_no=H.sl_no left outer join Mas_Expense_Parameter M on M.Expense_Parameter_Code=H.expense_parameter_code inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where sf_code='" + sf_code + "' and F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "' ";
                strQry = "select * from (select  ROW_NUMBER() over (order by WorkType_Code_B) as row,WorkType_Code_B,Work_Type_Name,isnull(Wtype_Fixed_Column1,0)Wtype_Fixed_Column1,isnull(Wtype_Fixed_Column2,0)Wtype_Fixed_Column2,isnull(Wtype_Fixed_Column3,0)Wtype_Fixed_Column3,isnull(Wtype_Fixed_Column4,0)Wtype_Fixed_Column4,isnull(Wtype_Fixed_Column5,0)Wtype_Fixed_Column5 from Worktype_Allowance_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and F.division_code='" + divcode + "' and Work_Type_Code='VA' and Type=1)as dd";
            }
            else
            {
                strQry = "select Work_Type_Name,fixed_amount from Worktype_Allowance_Setup where Division_code='" + divcode + "' and Work_Type_Code='FA' and TYPE=1";
            }
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
        public DataSet getOsDistance(string sf_code, string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 Territory_Code,Territory_Name,distance_in_kms*2 distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and Territory_name in(" + places + ") order by distance_in_kms desc";
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
        public DataSet getSingleOsDistance(string sf_code, string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 Territory_Code,Territory_Name,distance_in_kms*2 distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "'  and Territory_name in(" + places + ") order by distance_in_kms desc";
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
        public DataSet getOsExDistance(string sf_code, string places, string frCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select isnull(max(Distance_in_kms*2),0) distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code and From_Code='" + frCode + "' where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + places + ")";
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
        public DataSet getExDistance(string divcode, string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDivision = null;
            strQry = "select max(Distance_in_kms*2)dist,activity_date adate from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo " +
            "inner join Mas_ListedDr L on L.listeddrcode=D.Trans_Detail_Info_Code " +
           " inner join Mas_Territory_Creation T on CAST(ltrim(T.Territory_Code) as varchar)=L.Territory_Code " +
            "inner join  mas_salesforce sf on sf.sf_code=T.sf_code " +
            "inner join Mas_Distance_Fixation DS on DS.To_Code=CAST(ltrim(T.Territory_Code) as varchar) " +
        "where H.Sf_Code='" + sf_code + "' and D.Division_Code='" + divcode + "' and MONTH(Activity_Date)='" + mnth + "' " +
       " and YEAR(Activity_Date)='" + yr + "' " +
       " group by activity_date order by Activity_Date";
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
        public DataTable getSavedRecord(string month, string year, string sf_code)
        {

            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT catTemp,Expense_Date Adate, adate1, Expense_Day theDayName ,Expense_wtype Worktype_Name_B,Place_of_Work Territory_Name,Expense_All_Type Territory_Cat,Expense_Allowance Allowance,Expense_Distance Distance,Expense_Fare Fare,Expense_Total Total,Division_Code,from_place,to_place,exp_remarks,isnull(rw_amount,0)rw_amount,rw_rmks from Trans_Expense_detail " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") order by adate1 asc";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public DataTable getSavedHeadRecord(string month, string year, string sf_code)
        {

            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT sl_No,Admin_Remarks,Remarks,sndhqfl FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public int deleteAllExpenseSavedRecord(string month, string year, string sf_code)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "delete from Trans_Expense_detail " +
                //          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
                //strQry = "delete from exp_fixed " +
                //          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
                //strQry = "delete from exp_others " +
                //         "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
                //strQry = "delete from Exp_AccInf " +
                //         "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
                //strQry = "delete FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                //db.ExecQry(strQry);
                strQry = "exec sp_Delete_Expense '" + sf_code + "'," + month + "," + year + "";
                db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getSavedOtheExpRecord(string month, string year, string sf_code)
        {

            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT Paritulars,amt,other_total,remarks,expval from Exp_others " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public DataTable getSavedAdminExpRecord(string month, string year, string sf_code)
        {
            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT Paritulars,Typ,Amt,grand_total from Exp_AccInf " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public bool RecordExist(string frm_code, string to_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(To_Code) FROM mas_Distance_Fixation WHERE From_Code='" + frm_code + "' and To_Code='" + to_code + "' and sf_code = '" + sf_code + "'";
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

        public bool headRecordExist(string month, string year, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(sl_No) FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
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


        public bool fixedRecordExist(string month, string year, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(sl_No) FROM Exp_fixed WHERE sl_no in(select sl_no from trans_expense_head where SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ")";
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
        public bool adminAdjustExpExist(string month, string year, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(sl_No) FROM Exp_AccInf WHERE sl_no in(select sl_no from trans_expense_head where SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ")";
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
        public bool otherExpRecordExist(string month, string year, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(sl_No) FROM Exp_others WHERE sl_no in(select sl_no from trans_expense_head where SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ")";
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
        public int deleteSfDistance(string sf_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            strQry = "delete from mas_Distance_Fixation where Sf_Code = '" + sf_code + "'";
            iReturn = db.ExecQry(strQry);
            return iReturn;
        }
        public int addOrUpdate(string sf_code, string distance, string Terr_To, string Terr_From, string Terr_cat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                {

                    strQry = "insert into mas_Distance_Fixation (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,division_code, " +
                       " Flag,Created_Date) " +
                       " VALUES('" + sf_code + "', '" + Terr_From + "', '" + Terr_To + "', '" + Terr_cat + "', '" + distance + "','" + Division_Code + "',  " +
                       " 0, getdate())";
                    iReturn = db.ExecQry(strQry);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int addHeadRecord(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (headRecordExist(values["month"], values["year"], values["sf_code"]))
                {
                    deleteOtheExp(values["sf_code"], values["month"], values["year"]);
                    deleteFixed(values);
                    strQry = "delete from exp_accinf WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "";
                    iReturn = db.ExecQry(strQry);
                    strQry = "delete from Trans_Expense_detail " +
                      "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + ") ";
                    iReturn = db.ExecQry(strQry);
                    strQry = "delete from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "";
                    iReturn = db.ExecQry(strQry);

                    iReturn = insertHeadRecord(values, iReturn, db);
                }
                else
                {
                    iReturn = insertHeadRecord(values, iReturn, db);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int deleteFixed(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (fixedRecordExist(values["month"], values["year"], values["sf_code"]))
                {
                    strQry = "delete from Exp_fixed " +
                      "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + ") ";
                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int addFixedRecord(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "INSERT INTO Exp_Fixed " +
                    "(sl_No,Expense_Parameter_Code,Amt,SF_Code)" +
                    "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "),'" +
                    values["Expense_Parameter_Code"] + "','" + values["Amt"] + "','" + values["sf_code"] + "')";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int deleteOtheExp(string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (otherExpRecordExist(month, year, sf_code))
                {
                    strQry = "delete from Exp_others " +
                      "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataTable getAdmnAdjustExp(string sf_code, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select * from Exp_AccInf " +
              "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";

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
        public DataTable getApproveamnt(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select distinct grand_total,sf_code,typ,amt from Exp_AccInf " +
              "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE Expense_Month=" + month + " and Expense_Year=" + year + ") ";

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

        public int deleteAdminAdjustExp(string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (adminAdjustExpExist(month, year, sf_code))
                {
                    strQry = "delete from Exp_AccInf " +
                      "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        public int addOthExpRecord(string expType, string expValue, string amnt, string remarks, string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                if (!expValue.Contains("Select"))
                {


                    DB_EReporting db = new DB_EReporting();
                    strQry = "INSERT INTO Exp_others " +
                        "(sl_No,expval,Paritulars,Amt,Remarks)" +
                        "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "),'" +
                        expType + "','" + expValue + "'," + amnt + ",'" + remarks + "')";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    iReturn = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int addAdminAdjustmentExpRecord(string date, string expCode, string expName, string type, string expValue, string amnt, string gt, string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "INSERT INTO Exp_AccInf " +
                    "(sl_No,Paritulars,typ,Amt,grand_total,sf_code,Expense_Month,Expense_year,adminAdjDate,Expense_Parameter_Name,Expense_Parameter_Code)" +
                    "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "),'" +
                    expValue + "'," + type + "," + amnt + "," + gt + ",'" + sf_code + "'," + month + "," + year + "," + date + ",'" + expName + "','" + expCode + "')";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        private int insertHeadRecord(Dictionary<String, String> values, int iReturn, DB_EReporting db)
        {
            strQry = "INSERT INTO Trans_Expense_Head " +
                "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,File_Path)" +
                "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",'" + values["File_Path"] + "')";
            iReturn = db.ExecQry(strQry);
            return iReturn;
        }
        public int updateHeadFlg(string expType, string sf_code, string month, string year, string rmks)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set sndhqfl=" + expType + ",Admin_Remarks='" + rmks + "',admin_approval_date=getdate() WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int addDetailRecord(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                if (headRecordExist(values["month"], values["year"], values["sf_code"]))
                {
                    strQry = "INSERT INTO Trans_Expense_Detail " +
                        "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place)" +
                        "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "),'" +
                        values["adate"].Replace("'", "\"") + "','" + values["dayName"].Replace("'", "\"") + "','" + values["workType"] + "','" + values["terrName"] + "','" + values["cat"]
                        + "','" + values["allowance"] + "','" + values["distance"] + "','" + values["fare"] + "','" + values["catTemp"] + "','" + values["total"] + "','" + values["div_code"] + "','" + values["from"] + "','" + values["to"] + "')";
                    iReturn = db.ExecQry(strQry);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int addAllDetailRecord(Dictionary<int, Dictionary<String, String>> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
                {
                    for (int i = 0; i < values.Count(); i++)
                    {
                        strQry = "INSERT INTO Trans_Expense_Detail " +
                            "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,rw_amount,rw_rmks)" +
                            "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                            values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                            + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "','" + values[i]["rw_amount"] + "','" + values[i]["rw_rmks"] + "')";
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

        public DataSet DCR_get_Expense_Approval(string divcode, string sf_code, string str, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_get_Expense_Approval '" + sf_code + "', '" + divcode + ',' + "','" + Month + "','" + Year + "','" + str + "' ";

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
        public DataSet DCR_get_Expense_Approval_Vacant(string divcode, string sf_code, string str, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_get_Expense_Approval_Vacant '" + sf_code + "', '" + divcode + ',' + "','" + Month + "','" + Year + "','" + str + "' ";

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

        public DataTable getAdmnAdjust(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;

            strQry = "select * from Exp_AccInf " +
            "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE Expense_Month=" + month + " and Expense_Year=" + year + ") ";

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
        public DataTable getMgrAppr(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select MgrAppr_Remark,Last_Os_Wrkconsider,MgrAppr_Sameadmin,Row_wise_textbox from Expense_Setup where Division_Code='" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDCR_descnt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec DCR_descnt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDCRdesdt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwdesdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }

        public DataTable getDCRmobdt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwmobdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDCRappdt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwappdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDCRothdt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwothdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getDCRedtdt(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwedtdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getAllowFare_View(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select case isnull(sum(mgr_total),0) when 0 then sum(expense_total) " +
                     "else sum(mgr_total) end level1,sum(mgr_total2)level2,sum(expense_allowance)allw,SUM(Expense_Fare) fare,isnull(sum(rw_amount),0)rw_amt,sum(confirm_Total)confirm_Total,sum(confirm_Allow)confirm_Allow,sum(confirm_Amount)confirm_Amount,sndhqfl Status,Mgr_flag,SF_Code,second_level_mgr level2_code from Trans_Expense_Head H inner join Trans_Expense_Detail D on H.Sl_No=D.sl_no where Expense_Month=" + month + " and Expense_Year=" + year + " group by SF_Code,sndhqfl,Mgr_flag,second_level_mgr";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
        public DataTable getSavedAdminExpRecord1(string month, string year, string sf_code)
        {
            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT Paritulars,case when Typ=1 then '+' else '-' end Typ,Amt,grand_total,adminAdjDate,Expense_Parameter_Name,Expense_Parameter_Code from Exp_AccInf " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public int addOthMgrExpSetupRecord(string expType, string expValue, string desCode, string desName, string div_code)
        {
            int iReturn = -1;
            try
            {
                if (!desName.Contains("Select"))
                {



                    DB_EReporting db = new DB_EReporting();
                    strQry = "INSERT INTO Mgr_Exp_setup_Designation " +
                        "(sl_No,Designation_Mode,Designation_Name,Designation_Code,Designation_Short_Name,division_code)" +
                        "VALUES ((select isnull(max(sl_no)+1,0) from Mgr_Exp_setup_Designation),'" + expType + "','" + expValue + "'," + desCode + ",'" + desName + "','" + div_code + "')";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    iReturn = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int deleteOthMgrExpSetupRecord(string div_code)
        {
            int iReturn = -1;
            try
            {


                DB_EReporting db = new DB_EReporting();
                strQry = "delete from Mgr_Exp_setup_Designation where division_code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataTable MGRstpCnt(string div_code, string desig)
        {
            DB_EReporting db = new DB_EReporting();
            DataTable dsDivision = null;
            strQry = "select count(*)cnt from Mgr_Exp_setup_Designation where division_code='" + div_code + "' and Designation_Short_Name='" + desig + "' and Designation_mode='A'";
            try
            {


                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable osCalcRwwisestpCnt(string div_code)
        {
            DB_EReporting db = new DB_EReporting();
            DataTable dsDivision = null;
            strQry = "select count(*)rwwise_calc from expense_Setup where division_code='" + div_code + "' and OS_Package='R'";
            try
            {


                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public DataTable getMgrExpsetupDesignation(string div_code)
        {
            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT Designation_Code,Designation_Short_Name,Designation_Mode,Designation_Name from Mgr_Exp_setup_Designation " +
                          "where Division_Code='" + div_code + "'";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public DataTable getDesigExp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select designation_code,Designation_Short_Name from mas_sf_designation where Division_code='" + divcode + "' and type=2 and Designation_Active_Flag=0 ";

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

        public DataSet getOsEXFrmCode(string sf_code, string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 from_Code,(select (distance_in_kms*2)distance_in_kms from Mas_Distance_Fixation where to_code=D.from_code)distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + places + ")";
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

        public bool isRelationExist(string currPlace, string nextPlace, string currCatType, string nextCatType, string sf_code, string fromCode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(territory_code)cnt from(select cast(ltrim(Territory_Code) as varchar) Territory_Code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' " +
 "and Territory_Cat='4' and Territory_name in(" + currPlace + ") " +
 "union " +
 "select(select from_code from Mas_Distance_Fixation DS inner join mas_territory_creation TR on DS.to_code=cast(ltrim(TR.Territory_Code)as varchar) where DS.sf_code='" + sf_code + "' and territory_name in(" + currPlace + ") and from_code=cast(ltrim(T.Territory_Code)as varchar)) from  mas_territory_creation T where sf_code='" + sf_code + "' and Territory_Cat='3' and Territory_name in(" + nextPlace + ") " +
                    //"select Territory_Cat Territory_Code from Mas_Distance_Fixation D " +
                    //"inner join mas_territory_creation T on D.From_code=cast(ltrim(T.Territory_Code)as varchar) where D.sf_code='" + sf_code + "' " +
                    //"and Territory_Cat='3' and Territory_name in(" + nextPlace + ") " +
 "union " +
 "select (select from_code from Mas_Distance_Fixation DS inner join mas_territory_creation TR on DS.to_code=TR.territory_code where DS.sf_code='" + sf_code + "' and Territory_Cat='4' and territory_name in(" + nextPlace + ") and from_code=D.from_code) Territory_code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='4' " +
 "and Territory_name in(" + currPlace + ") " +
 "union " +
 "select Territory_Cat Territory_Code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code " +
 "where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + nextPlace + ") and Territory_name in(" + currPlace + "))as d";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 1)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool isRelationExistPrev(string currPlace, string nextPlace, string currCatType, string nextCatType, string sf_code, string fromCode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(territory_code)cnt from(select cast(ltrim(Territory_Code) as varchar) Territory_Code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' " +
 "and Territory_Cat='4' and Territory_name in(" + currPlace + ") " +
 "union " +
 "select(select from_code from Mas_Distance_Fixation DS inner join mas_territory_creation TR on DS.to_code=cast(ltrim(TR.Territory_Code)as varchar) where DS.sf_code='" + sf_code + "' and territory_name in(" + currPlace + ") and from_code=cast(ltrim(T.Territory_Code)as varchar)) from  mas_territory_creation T where sf_code='" + sf_code + "' and Territory_Cat='3' and Territory_name in(" + nextPlace + ") " +
                    //"select Territory_Cat Territory_Code from Mas_Distance_Fixation D " +
                    //"inner join mas_territory_creation T on D.From_code=cast(ltrim(T.Territory_Code)as varchar) where D.sf_code='" + sf_code + "' " +
                    //"and Territory_Cat='3' and Territory_name in(" + nextPlace + ") " +
 "union " +
 "select (select distinct from_code from Mas_Distance_Fixation DS inner join mas_territory_creation TR on DS.to_code=TR.territory_code where DS.sf_code='" + sf_code + "' and Territory_Cat='4' and territory_name in(" + nextPlace + ") and from_code=D.from_code) Territory_code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='4' " +
 "and Territory_name in(" + currPlace + ") " +
 "union " +
 "select Territory_Cat Territory_Code from Mas_Distance_Fixation D " +
 "inner join mas_territory_creation T on D.to_code=T.Territory_Code " +
 "where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + nextPlace + ") and Territory_name in(" + currPlace + "))as d";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 1)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool isRelationosExist(string currPlace, string nextPlace, string currCatType, string nextCatType, string sf_code, string fromCode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(territory_code)cnt from(select cast(ltrim(Territory_Code) as varchar) Territory_Code from Mas_Distance_Fixation D " +
               "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' " +
               "and Territory_Cat='3' and Territory_name in(" + currPlace + ") " +
               "union " +
               "select cast(ltrim(Territory_Code) as varchar) territory_code from Mas_Distance_Fixation D " +
               "inner join mas_territory_creation T on D.to_code=T.Territory_Code and From_Code='" + fromCode + "' " +
               "where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + nextPlace + ") " +
               "union " +
               "select territory_name territory_code from mas_territory_creation T where cast(ltrim(territory_code)as varchar)='" + fromCode + "' " +
               "and T.sf_code='" + sf_code + "' and Territory_Cat='3' and T.Territory_name in(" + nextPlace + ") " +
               "union " +
               "select Territory_Cat territory_code from mas_territory_creation T where cast(ltrim(territory_code)as varchar)='" + fromCode + "' " +
               "and T.sf_code='" + sf_code + "' and Territory_Cat='3' and T.Territory_name in(" + nextPlace + ") " +
                   ")as d";
                int iRecordExist = db.Exec_Scalar(strQry);
                if (iRecordExist > 1)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool isRelationosExistPrev(string currPlace, string nextPlace, string currCatType, string nextCatType, string sf_code, string fromCode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(territory_code)cnt from(select cast(ltrim(Territory_Code) as varchar) Territory_Code from Mas_Distance_Fixation D " +
               "inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' " +
               "and Territory_Cat='3' and Territory_name in(" + currPlace + ") " +
               "union " +
               "select cast(ltrim(Territory_Code) as varchar) territory_code from Mas_Distance_Fixation D " +
               "inner join mas_territory_creation T on D.to_code=T.Territory_Code and From_Code='" + fromCode + "' " +
               "where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + nextPlace + ") " +
               "union " +
               "select territory_name territory_code from mas_territory_creation T where cast(ltrim(Territory_Code) as varchar)='" + fromCode + "' " +
               "and T.sf_code='" + sf_code + "' and Territory_Cat='3' and T.Territory_name in(" + nextPlace + ") " +
               "union " +
               "select Territory_Cat territory_code from mas_territory_creation T where cast(ltrim(Territory_Code) as varchar)='" + fromCode + "' " +
               "and T.sf_code='" + sf_code + "' and Territory_Cat='3' and T.Territory_name in(" + nextPlace + ") " +
                   ")as d";
                int iRecordExist = db.Exec_Scalar(strQry);
                if (iRecordExist > 1)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public DataSet getOsEXFrmCoderws(string sf_code, string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 from_Code,(select (distance_in_kms)distance_in_kms from Mas_Distance_Fixation where to_code=D.from_code and sf_code='" + sf_code + "')distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + places + ")";
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
        public DataSet getOsDistancenext(string sf_code, string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 Territory_Code,Territory_Name,distance_in_kms distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and Territory_name in(" + places + ") order by distance_in_kms desc";
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
        //public DataTable getOtherExpDetails(string month, string year)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataTable dsSubDiv = null;
        //    strQry = "select D.Expense_Parameter_Code,Expense_Parameter_Name,H.SF_Code,Amt,H.Approval_date2 from exp_fixed D inner join trans_expense_head H on D.sl_no=H.sl_no inner join Fixed_Variable_Expense_Setup F on F.expense_parameter_code=D.Expense_Parameter_Code where Expense_Month=" + month + " and Expense_Year=" + year;
        //    try
        //    {
        //        dsSubDiv = db_ER.Exec_DataTable(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsSubDiv;
        //}

        public DataSet getFileNamePath(string sf_code, string iMonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select file_path from trans_expense_head where sf_code='" + sf_code + "' and expense_month=" + iMonth + " and expense_year=" + iYear + "";
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
        public DataTable OSCondnMGR(string div_code, string mgrcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataTable dsDivision = null;
            strQry = "select * from Mgr_Allowance_Setup where division_code='" + div_code + "' and mgr_code='" + mgrcode + "' and type_code='OS'";
            try
            {


                dsDivision = db.Exec_DataTable(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDivision;
        }
        public bool isRelationostoosExist(string sf_code)
        {

            bool bRecordExist = false;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select count(distinct ret_dist)ret_dist from mas_distance_Fixation where sf_Code='" + sf_code + "' and ret_dist is not null and Distance_In_Kms>0";
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
        public int getOSDistanceBySP_bak(string callType, string places, string prevPlace, string sfcode, string mgrcode, string prevSf, string nextSf)
        {

            int iReturn = -1;
            try
            {
                //DB_EReporting db = new DB_EReporting();
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ExpenseAllos_condition_bak", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@callType", callType);
                cmd.Parameters.AddWithValue("@places", places);
                cmd.Parameters.AddWithValue("@pOS", prevPlace);
                cmd.Parameters.AddWithValue("@sf_code", sfcode);
                cmd.Parameters.AddWithValue("@MGR_code", mgrcode);
                cmd.Parameters.AddWithValue("@psf_code", prevSf);
                cmd.Parameters.AddWithValue("@nsf_code", nextSf);

                //  SqlParameter count = new SqlParameter("@return_value", DbType.Int32);

                SqlParameter returnParameter = cmd.Parameters.Add("@distance", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();

                iReturn = (int)returnParameter.Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getDCRiOS(string month, string year, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "exec vwiOSdt '" + month + "','" + year + "','" + divcode + "'";
            try
            {
                dsSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDiv;
        }
    }
}