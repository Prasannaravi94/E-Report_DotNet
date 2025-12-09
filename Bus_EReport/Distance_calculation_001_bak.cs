using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Configuration;
namespace Bus_EReport
{
    public class Distance_calculation_001
    {
        public Distance_calculation_001()
        {
        }
        private string strQry = string.Empty;
        public DataSet getFieldForce(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT sf_code,sf_name,sf_hq " +
                     " FROM mas_salesforce WHERE sf_status=0 and sf_type=1 and Division_Code like '%" + divcode + "%'" +
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
        public DataTable getDivision()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select Division_Code,Division_Name from Mas_Division where Division_Active_Flag=0";
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
        public int saveExpenseLock(string div, string mnth, string yr)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int sNo = 0;
                strQry = "select isnull(max(Sl_No)+1,1) id from Option_Expense_Lock";
                sNo = db.Exec_Scalar(strQry);
                strQry = "insert into Option_Expense_Lock values(" + sNo + ",'" + div + "','" + mnth + "','" + yr + "',getdate())";
                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "update trans_expense_head set Lock_flg=1,lock_date=getdate() where sndhqfl=6 and (lock_flg<>1 or Lock_flg is null) and expense_month='" + mnth + "' and expense_year='" + yr + "'";
                    db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataTable getAllowFare(string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select sum(expense_allowance)allw,SUM(Expense_Fare) fare,sndhqfl Status,SF_Code from Trans_Expense_Head H inner join Trans_Expense_Detail D on H.Sl_No=D.sl_no where Expense_Month=" + month + " and Expense_Year=" + year + " group by SF_Code,sndhqfl";
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
            strQry = "select sndhqfl Status, second_level_mgr level2_code, First_level_mgr level1_code from Trans_Expense_Head where Expense_Month=" + month + " and Expense_Year=" + year + " and sf_code='" + sfCode + "'";
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

        public DataTable getSavedFixedExpMR(string month, string year, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSubDiv = null;
            strQry = "select F.Expense_Parameter_Code,Expense_Parameter_Name,Amt amount from exp_fixed D inner join Fixed_Variable_Expense_Setup F on D.expense_parameter_code=f.expense_parameter_code inner join trans_expense_head H on H.sl_no=D.sl_no  where f.expense_parameter_code not in(12,13,14,15) and H.sf_code='" +
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

        public DataSet getFilterRgn(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "exec sp_SalesForceGet '" + divcode + "','" + sf_code + "'";
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
            strQry = "select sf_code,sf_name+' - '+sf_Designation_Short_Name+' - '+sf_hq sf_name,sf_hq,sf_type,sf_emp_id Employee_Id,Employee_Id SF_Emp_Id,Division_Name " +
                " FROM mas_salesforce S inner join Mas_SF_Designation D on D.Designation_Code=S.Designation_Code inner join Mas_Division MD on D.Division_Code=MD.Division_Code WHERE sf_code='" + sfcode + "' and sf_status=0" +
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
            strQry = "Select From_Code FrmTown,To_Code ToTown,Distance_In_Kms Distance,amount from Mas_Distance_Fixation where sf_Code='" + sf_code + "' and division_code='" + divCode + "'";
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
            strQry = "SELECT Work_Type_Name,Allow_type,Work_Type_Code " +
                     " FROM Worktype_Allowance_Setup WHERE Type='1' and Division_Code like '%" + divcode + "%'" +
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

        public DataSet getDistance(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
                 strQry = "select From_Code,To_Code,Sf_HQ,T.Territory_Name,Distance_In_Kms,Amount, " +
                     " case when T.Territory_Cat=1 then 'HQ' " +
                                " else case when T.Territory_Cat=2 then 'EX' " +
                                " else case when T.Territory_Cat=3 then 'OS' " +
                                " else 'OS-EX' " +
                                " end end end as Town_Cat " +
                     " from Mas_Distance_Fixation D inner join Mas_Salesforce S on D.SF_Code=S.Sf_Code " +
                 "inner join Mas_Territory_Creation T on T.Territory_Code=D.To_Code and T.Territory_Cat=D.Town_Cat where Territory_Active_Flag=0 and S.Sf_Code='" + sf_code + "' and T.Territory_Cat<>4 AND T.Division_Code like '" + divcode + "' " +
                 "Union select From_Code,To_Code,(select Territory_Name from Mas_Territory_Creation where convert(varchar(10),Territory_Code)=From_Code)Sf_HQ,T.Territory_Name,Distance_In_Kms,Amount, " +
                     " case when T.Territory_Cat=1 then 'HQ' " +
                                " else case when T.Territory_Cat=2 then 'EX' " +
                                " else case when T.Territory_Cat=3 then 'OS' " +
                                " else 'OS-EX' " +
                                " end end end as Town_Cat " +
                     " from Mas_Distance_Fixation D inner join Mas_Salesforce S on D.SF_Code=S.Sf_Code " +
                 "inner join Mas_Territory_Creation T on T.Territory_Code=D.To_Code and T.Territory_Cat=D.Town_Cat where Territory_Active_Flag=0 and S.Sf_Code='" + sf_code + "' and T.Territory_Cat=4 AND T.Division_Code like '" + divcode + "' order by Town_Cat,From_Code";
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
        public DataSet getPlace_MGR(string divcode, string sf_code, string mnth, string yr)
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
        public DataSet getExpense_MGR(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpense_MGR '" + sf_code + "', '" + mnth + "','" + yr + "'";
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
            strQry = "select HQ_Allowance hq_allowance,EX_HQ_Allowance ex_allowance,OS_Allowance os_allowance,FareKm_Allowance fare,Hill_Allowance from Mas_Allowance_Fixation where SF_Code='" + sf_code + "' ";
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
        public DataTable getExpParamAmtMR(string sf_code, String divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            //strQry = "select sf_code,M.expense_parameter_code,M.Expense_Parameter_Name ,amount from Trans_Fixed_Expense_Detail D inner join Trans_Fixed_Expense_Head H on D.sl_no=H.sl_no left outer join Mas_Expense_Parameter M on M.Expense_Parameter_Code=H.expense_parameter_code inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where sf_code='" + sf_code + "' and F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "' ";
            strQry = "select * from (select  ROW_NUMBER() over (order by Expense_Parameter_Code) as row,Expense_Parameter_Code,Expense_Parameter_Name,isnull(Fixed_Column1,0)Fixed_Column1,isnull(Fixed_Column2,0)Fixed_Column2,isnull(Fixed_Column3,0)Fixed_Column3,isnull(Fixed_Column4,0)Fixed_Column4,isnull(Fixed_Column5,0)Fixed_Column5,M.Sf_Code,F.Division_code from Fixed_Variable_Expense_Setup F inner join Mas_Division S on F.Division_code=S.Division_Code inner join Mas_Allowance_Fixation M on M.SF_Code='" + sf_code + "' and F.division_code='" + divcode + "' and Param_type='F' and Expense_Parameter_Code not in(12,13,14,15))as dd";
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
        public DataSet getOsDistance(string sf_code, string places, string osexPlaces)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (places == "''")
            {
                strQry = " select top 1 Territory_Code,T.Territory_Name,distance_in_kms*2 distance_in_kms,isnull(amount*2,0) amount from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and T.Territory_name in(select distinct(select Territory_Name from Mas_Territory_Creation where Territory_Code=From_Code)territory_name from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code and D.Town_Cat=T.Territory_Cat where D.sf_code='" + sf_code + "' and Territory_Cat='4' and T.Territory_name in(" + osexPlaces + ") and distance_in_kms>0) order by distance_in_kms desc";
            }
            else
            {
                strQry = "select top 1 Territory_Code,T.Territory_Name,distance_in_kms*2 distance_in_kms,isnull(amount*2,0) amount from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and T.Territory_name in(" + places + ") order by distance_in_kms desc";
                //strQry = "select isnull(sum(distance_in_kms*2),0) distance_in_kms,isnull(sum(amount*2),0) amount from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and T.Territory_name in(" + places + ")";
            }
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
            strQry = "select top 1 Territory_Code,T.Territory_Name,distance_in_kms*2 distance_in_kms,isnull(amount*2,0) amount from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "'  and T.Territory_name in(" + places + ") order by distance_in_kms desc";
            
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
            if (frCode == "")
            {
                strQry = "select isnull(sum(distinct Distance_in_kms*2),0) distance_in_kms,isnull(sum(distinct amount*2),0) amount  from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code and D.Town_Cat=T.Territory_Cat where D.sf_code='" + sf_code + "' and Territory_Cat='4' and T.Territory_name in(" + places + ") group by amount";
            }
            else
            {
                strQry = "select isnull(sum(distinct Distance_in_kms*2),0) distance_in_kms,isnull(sum(distinct amount*2),0) amount  from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code and From_Code='" + frCode + "' where D.sf_code='" + sf_code + "' and Territory_Cat='4' and T.Territory_name in(" + places + ") group by amount";
            }
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
            strQry = "select isnull(sum(Distance_in_kms*2),0)dist, isnull(sum(amount*2),0) amount,adate from (select distinct Distance_in_kms,amount,activity_date adate,L.Territory_Code from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo " +
            "inner join Mas_ListedDr L on L.listeddrcode=D.Trans_Detail_Info_Code " +
           " inner join Mas_Territory_Creation T on CAST(ltrim(T.Territory_Code) as varchar)=L.Territory_Code " +
            "inner join  mas_salesforce sf on sf.sf_code=T.sf_code " +
            "inner join Mas_Distance_Fixation DS on DS.To_Code=T.Territory_Code " +
        "where H.Sf_Code='" + sf_code + "' and D.Division_Code='" + divcode + "' and MONTH(Activity_Date)='" + mnth + "' " +
       " and YEAR(Activity_Date)='" + yr + "') as dd" +
       " group by adate order by adate";
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
                strQry = "SELECT catTemp,Expense_Date Adate, adate1, Expense_Day theDayName ,Expense_wtype Worktype_Name_B,Expense_wtype Worktype_Name_M,Place_of_Work Territory_Name,Place_of_Work TerrPlaces,Expense_All_Type Territory_Cat,Expense_All_Type Type_Code,Expense_Allowance Allowance,Expense_Distance Distance,Expense_Fare Fare,Expense_Total Total,Division_Code,from_place,to_place,mgr_amount,mgr_remarks,mgr_total,isnull(mgr_amount2,'') mgr_amount2,mgr_remarks2,isnull(mgr_total2,'') mgr_total2,confirm_amount,confirm_remarks,confirm_total,Mgr_Allow,isnull(Mgr_Allow2,'') Mgr_Allow2,confirm_allow,calc_flag,calc_flag2,exp_remarks from Trans_Expense_detail " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") order by adate1 asc";
                data = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
         public DataTable getSaveDraft(string month, string year, string sf_code)
        {

            DataTable data = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "exec sp_mgr_Draft_Record '" + sf_code + "'," + month + "," + year + "";
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
                strQry = "SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
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
               // strQry = "delete from Trans_Expense_detail " +
                //          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
                //strQry = "delete from exp_fixed " +
                //          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
               // db.ExecQry(strQry);
               // strQry = "delete from exp_others " +
               //           "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                //db.ExecQry(strQry);
              //  strQry = "delete from Exp_AccInf " +
               //           "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
               // db.ExecQry(strQry);
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

        public int deleteAllExpenseSavedRecord_Mgr(string month, string year, string sf_code)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                
                //strQry = "delete FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                strQry = "update Trans_Expense_Head set sndhqfl=0,first_level_mgr=NULL,First_level_Mgr_Name=NULL,Same_report=NULL,Second_level_Mgr=NULL,Second_level_Mgr_Name=NULL WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RejectAllExpenseSavedRecord(string month, string year, string sf_code, int flg, string mgr_code, string mgr_name,string rejectedReason)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set sndhqfl=" + flg + ",Rejected_by='" + mgr_code + "',Reject_name='" + mgr_name + "',Reject_reason='" + rejectedReason + "',first_level_mgr=NULL,First_level_Mgr_Name=NULL,Same_report=NULL,Second_level_Mgr=NULL,Second_level_Mgr_Name=NULL,reject_date=getdate() WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                db.ExecQry(strQry);
                strQry = "delete from Trans_Expense_detail " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                db.ExecQry(strQry);
                strQry = "delete from exp_fixed " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                db.ExecQry(strQry);
                strQry = "delete from exp_others " +
                          "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + ") ";
                db.ExecQry(strQry);
                iReturn = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RejectAllExpenseSavedRecord_Mgr(string month, string year, string sf_code, int flg, string mgr_code, string mgr_name, string rejectedReason)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set sndhqfl=" + flg + ",Rejected_by='" + mgr_code + "',Reject_name='" + mgr_name + "',Reject_reason='" + rejectedReason + "',first_level_mgr=NULL,First_level_Mgr_Name=NULL,Same_report=NULL,Second_level_Mgr=NULL,Second_level_Mgr_Name=NULL,reject_date=getdate() WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                db.ExecQry(strQry);
                
                iReturn = 1;
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

                strQry = "SELECT COUNT(To_Code) FROM Mas_Distance_Fixation WHERE From_Code='" + frm_code + "' and To_Code='" + to_code + "' and sf_code = '" + sf_code + "'";
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

        public int getValidheadRecordNo(string month, string year, string sf_code)
        {

            int sNo = 0;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT sl_No ,sndhqfl status FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                DataTable dt = db.Exec_DataTable(strQry);

                if (dt.Rows.Count > 0)
                {
                        sNo = Convert.ToInt32(dt.Rows[0]["sl_No"].ToString());
                     
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sNo;
        }
        public DataTable getValidheadRecordStaus(string month, string year, string sf_code)
        {

            DataTable dt = null;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT sl_No ,sndhqfl status,reject_name rejectedBy,reject_reason rejectedReason FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                dt = db.Exec_DataTable(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
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
            strQry = "delete from Mas_Distance_Fixation where Sf_Code = '" + sf_code + "'";
            iReturn = db.ExecQry(strQry);
            return iReturn;
        }
        public bool distExist(string frmCode, string toCode, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(*) FROM Mas_Distance_Fixation WHERE SF_Code='" + sf_code + "' and From_Code='" + frmCode + "' and To_Code='" + toCode+"'";
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
        public int addOrUpdate(int level,string sf_code, string distance, string amt, string Terr_To, string Terr_From, string Terr_cat, string Terr_name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int Division_Code = -1;
                string flgName = "approve_flg";
                if (level == 1)
                {
                    flgName = "level1_flg";
                }
                else if (level == 2)
                {
                    flgName = "level2_flg";
                }
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                if (distExist(Terr_From, Terr_To, sf_code))
                {
                    strQry = "update Mas_Distance_Fixation set " + flgName + "='" + level + "',amount='" + amt + "',Distance_In_Kms='" + distance + "',town_cat='" + Terr_cat + "' where SF_Code='" + sf_code + "' and From_Code='" + Terr_From + "' and To_Code='" + Terr_To + "'";
                }
                else
                {
                    strQry = "insert into Mas_Distance_Fixation (SF_Code," + flgName + ",From_Code,To_Code,Town_Cat,Distance_In_Kms,Amount,division_code, " +
                       " Flag,Created_Date,Territory_Name) " +
                       " VALUES('" + sf_code + "', '" + level + "', '" + Terr_From + "', '" + Terr_To + "', '" + Terr_cat + "', '" + distance + "', '" + amt + "','" + Division_Code + "',  " +
                       " 0, getdate(),'" + Terr_name + "')";
                }
                    iReturn = db.ExecQry(strQry);


                
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
                   // strQry = "delete from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "";
                    //iReturn = db.ExecQry(strQry);

                    if (values["sNo"] == "0")
                    {
                        iReturn = insertHeadRecord(values, iReturn, db,0);
                    }
                    else
                    {
                        iReturn = updateHeadRecord(values,0);
                    }
                }
                else
                {
                    iReturn = insertHeadRecord(values, iReturn, db,0);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int addHeadRecordMgr(Dictionary<String, String> values)
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
                    // strQry = "delete from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "";
                    //iReturn = db.ExecQry(strQry);

                    if (values["sNo"] == "0")
                    {
                        iReturn = insertHeadRecord(values, iReturn, db, 1);
                    }
                    else
                    {
                        iReturn = updateHeadRecord(values, 1);
                    }
                }
                else
                {
                    iReturn = insertHeadRecord(values, iReturn, db, 1);

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
            strQry = "select distinct grand_total,sf_code from Exp_AccInf " +
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

        public int addAdminAdjustmentExpRecord(string type, string expValue, string amnt, string gt, string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "INSERT INTO Exp_AccInf " +
                    "(sl_No,Paritulars,typ,Amt,grand_total,sf_code,Expense_Month,Expense_year)" +
                    "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "),'" +
                    expValue + "'," + type + "," + amnt + "," + gt + ",'" + sf_code + "'," + month + "," + year + ")";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        private int insertHeadRecord(Dictionary<String, String> values, int iReturn, DB_EReporting db, int mgrFlg)
        {
            int sNo = 0;
            strQry="select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head";
            sNo=db.Exec_Scalar(strQry);
            strQry = "INSERT INTO Trans_Expense_Head " +
                "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,mgr_flag,Status_flg,Employee_Id,sf_emp_id)" +
                "VALUES ("+sNo+",'" +
                values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + "," + mgrFlg + ",0,'" + values["EmpNo"] + "','" + values["SfEmpNo"] + "')";
            iReturn = db.ExecQry(strQry);
            return sNo;
        }
        public int updateHeadRecord(Dictionary<String, String> values, int mgrFlg)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set mgr_flag=" + mgrFlg + ",sndhqfl=" + values["flag"] + " WHERE sl_no=" + values["sNo"];
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int updateHeadFlg(string expType, string sf_code, string month, string year)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Trans_Expense_Head set Status_flg=1,admin_approval_date=getdate(),sndhqfl=" + expType + " WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
         public int updateHeadFlgMgrLevel1(string expType, string sf_code, string month, string year,string mgr_code,string mgr_name,bool isApproval)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if(isApproval)
                    strQry = "update Trans_Expense_Head set Status_flg=0,Approval_Datea=getdate(),sndhqfl=" + expType + ",First_level_Mgr='" + mgr_code + "',First_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                else
                    strQry = "update Trans_Expense_Head set sndhqfl=" + expType + ",First_level_Mgr='" + mgr_code + "',First_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
         public int updateHeadFlgMgrLevel2(string expType, string sf_code, string month, string year, string mgr_code, string mgr_name,bool isApproval)
         {
             int iReturn = -1;
             try
             {
                 DB_EReporting db = new DB_EReporting();
                 if (isApproval)
                 {
                     strQry = "update Trans_Expense_Head set Status_flg=0,Approval_Date2=getdate(),sndhqfl=" + expType + ",Second_level_Mgr='" + mgr_code + "',Second_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                 }
                 else
                 {
                     strQry = "update Trans_Expense_Head set sndhqfl=" + expType + ",Second_level_Mgr='" + mgr_code + "',Second_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                 }
                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return iReturn;
         }

         public int updateHeadFlgMgrLevel22(string expType, string sf_code, string month, string year, string mgr_code, string mgr_name)
         {
             int iReturn = -1;
             try
             {
                 DB_EReporting db = new DB_EReporting();

                 strQry = "SELECT same_report FROM Trans_Expense_Head WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year;
                 DataTable dt = db.Exec_DataTable(strQry);
                 int rpt = -1;

                 if (dt.Rows.Count > 0)
                 {
                     rpt = Convert.ToInt32(dt.Rows[0]["same_report"].ToString() == "0" ? "0" : "2");
                 }

                 if (expType == "2" && rpt!=0)
                 {
                     strQry = "update Trans_Expense_Head set Status_flg=0,same_report=0,Approval_Datea=getdate(),sndhqfl=" + expType + ",first_level_Mgr='" + mgr_code + "',first_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                 }
                 else
                 {
                     if (expType=="2" && rpt == 0)
                     {
                         expType = "4";
                     }
                     strQry = "update Trans_Expense_Head set Status_flg=0,Approval_Date2=getdate(),sndhqfl=" + expType + ",Second_level_Mgr='" + mgr_code + "',Second_level_Mgr_Name='" + mgr_name + "' WHERE SF_Code='" + sf_code + "' and Expense_Month=" + month + " and Expense_Year=" + year + "";
                 }
                 iReturn = db.ExecQry(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }

             return iReturn;
         }


         public int getExp_MgrAmtLevel22Update(string expType, Dictionary<int, Dictionary<String, String>> values)
         {
             DB_EReporting db_ER = new DB_EReporting();
             int iReturn = -1;

             if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
             {
                 for (int i = 0; i < values.Count(); i++)
                 {
                     values[i]["calc_flag"] = values[i]["calc_flag"] == "" ? "0" : values[i]["calc_flag"];
                     values[i]["calc_flag2"] = values[i]["calc_flag2"] == "" ? "0" : values[i]["calc_flag2"];

                     if (expType == "2")
                     {
                         strQry = " update trans_expense_detail set Mgr_Allow=" + values[i]["allow"] + ",calc_flag=" + values[i]["calc_flag"] + ",calc_flag2=" + values[i]["calc_flag2"] + ",Mgr_Amount=" + values[i]["amount"] + ",Mgr_Remarks='" + values[i]["remarks"] + "',Mgr_total=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                     }
                     else
                     {
                         strQry = " update trans_expense_detail set Mgr_Allow2=" + values[i]["allow"] + ",calc_flag=" + values[i]["calc_flag"] + ",calc_flag2=" + values[i]["calc_flag2"] + ",Mgr_Amount2=" + values[i]["amount"] + ",Mgr_Remarks2='" + values[i]["remarks"] + "',Mgr_total2=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                     }
                     //strQry = " update trans_expense_detail set Mgr_Amount2=" + values[i]["amount"] + ",Mgr_Remarks2='" + values[i]["remarks"] + "',Mgr_total2=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                     try
                     {
                         iReturn = db_ER.ExecQry(strQry);
                     }
                     catch (Exception ex)
                     {
                         throw ex;
                     }

                 }
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
        public int getExp_MgrAmtUpdate(Dictionary<int, Dictionary<String, String>> values)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;

            if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    values[i]["calc_flag"] = values[i]["calc_flag"] == "" ? "0" : values[i]["calc_flag"];
                    values[i]["calc_flag2"] = values[i]["calc_flag2"] == "" ? "0" : values[i]["calc_flag2"];
                    strQry = " update trans_expense_detail set Mgr_Allow=" + values[i]["allow"] + ",calc_flag=" + values[i]["calc_flag"] + ",calc_flag2=" + values[i]["calc_flag2"] + ",Mgr_Amount=" + values[i]["amount"] + ",Mgr_Remarks='" + values[i]["remarks"] + "',Mgr_total=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                    try
                    {
                       iReturn= db_ER.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return iReturn;
        }
        public int getExp_MgrAmtLevel2Update(Dictionary<int, Dictionary<String, String>> values)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;

            if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    values[i]["calc_flag"] = values[i]["calc_flag"] == "" ? "0" : values[i]["calc_flag"];
                    values[i]["calc_flag2"] = values[i]["calc_flag2"] == "" ? "0" : values[i]["calc_flag2"];
                    strQry = " update trans_expense_detail set Mgr_Allow2=" + values[i]["allow"] + ",calc_flag=" + values[i]["calc_flag"] + ",calc_flag2=" + values[i]["calc_flag2"] + ",Mgr_Amount2=" + values[i]["amount"] + ",Mgr_Remarks2='" + values[i]["remarks"] + "',Mgr_total2=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                    //strQry = " update trans_expense_detail set Mgr_Amount2=" + values[i]["amount"] + ",Mgr_Remarks2='" + values[i]["remarks"] + "',Mgr_total2=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                    try
                    {
                        iReturn = db_ER.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
            return iReturn;
        }
        public int getExp_AdminAmntUpdate(Dictionary<int, Dictionary<String, String>> values)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;

            if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
            {
                for (int i = 0; i < values.Count(); i++)
                {
                    values[i]["calc_flag"] = values[i]["calc_flag"] == "" ? "0" : values[i]["calc_flag"];
                    values[i]["calc_flag2"] = values[i]["calc_flag2"] == "" ? "0" : values[i]["calc_flag2"];
                    //strQry = " update trans_expense_detail set Confirm_Amount=" + values[i]["amount"] + ",Confirm_Remarks='" + values[i]["remarks"] + "',Confirm_total=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                    strQry = " update trans_expense_detail set Confirm_Allow=" + values[i]["allow"] + ",calc_flag=" + values[i]["calc_flag"] + ",calc_flag2=" + values[i]["calc_flag2"] + ",Confirm_Amount=" + values[i]["amount"] + ",Confirm_Remarks='" + values[i]["remarks"] + "',Confirm_total=" + values[i]["adminTot"] + " where adate1='" + values[i]["adate1"] + "' and sl_no in(select sl_no from trans_expense_head where sf_code='" + values[0]["sf_code"] + "' and expense_month='" + values[0]["month"] + "' and expense_year='" + values[0]["year"] + "')";
                    try
                    {
                        iReturn = db_ER.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
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
                            "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place)" +
                            "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                            values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                            + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "')";
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

        public int addAllDetailRecordMgr(Dictionary<int, Dictionary<String, String>> values, int flg)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (headRecordExist(values[0]["month"], values[0]["year"], values[0]["sf_code"]))
                {

                    for (int i = 0; i < values.Count(); i++)
                    {
                        if (flg == 1)
                        {
                            strQry = "INSERT INTO Trans_Expense_Detail " +
                                "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow,Mgr_amount,Mgr_Remarks,Mgr_total,exp_remarks)" +
                                "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                                values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                                + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "','" + values[i]["allowance"] + "','" + values[i]["fare"] + "','" + values[i]["remarks"] + "','" + values[i]["total"] + "','" + values[i]["remarks"] + "')";
                        }
                        else if (flg == 2)
                        {
                            strQry = "INSERT INTO Trans_Expense_Detail " +
                                "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow2,Mgr_amount2,Mgr_Remarks2,Mgr_total2,exp_remarks)" +
                                "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                                values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                                + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "','" + values[i]["allowance"] + "','" + values[i]["fare"] + "','" + values[i]["remarks"] + "','" + values[i]["total"] + "','" + values[i]["remarks"] + "')";
                        }
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

        public int addAllDetailRecordMgr(Dictionary<int, Dictionary<String, String>> values)
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
                            "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow,Mgr_amount,Mgr_Remarks,Mgr_total,exp_remarks)" +
                            "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                            values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                            + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "','" + values[i]["allowance"] + "','" + values[i]["fare"] + "','" + values[i]["remarks"] + "','" + values[i]["total"] + "','" + values[i]["remarks"] + "')";

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

        public DataSet Expense_SF_View(string divcode, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC Mgr_distance_Fixation '" + sf_code + "', '" + divcode + "','" + mgr_code + "' ";

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
        public DataSet Expense_SF_View1(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC Expense_SF_View '" + sf_code + "', '" + divcode + "' ";

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
        public DataSet Expense_Fixed_MR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_Expense_Fixed_MR '" + sf_code + "' ";

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
        public DataSet Expense_Fixed_Variable(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_Expense_Fixed_Variable '" + Div_Code + "' ";

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
        public DataSet getPlaceMGR(string sf_code, string mnth, string yr, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec MGR_Auto_Allow '" + sf_code + "' ,'" + mnth + "','" + yr + "','" + divcode + "'";
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

        public DataSet getPlsMR(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec MGR_mrterr '" + sf_code + "' ,'" + mnth + "','" + yr + "'";
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
        public DataSet getPlsMRdist(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec MGR_mrdist '" + sf_code + "' ,'" + mnth + "','" + yr + "'";
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
        public DataSet getDistMGR(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
//            strQry = "select activity_date,max(Dist*2)dist from DCRDetail_Lst_Trans D " +
//"inner join DCRMain_Trans H on D.trans_slno=H.trans_slno " +
//"inner join mas_listeddr L on D.Trans_Detail_Info_Code=L.ListedDrCode " +
//"inner join mas_territory_creation T on T.territory_code=L.territory_Code " +
//"inner join mas_salesforce S on S.sf_code=T.sf_code " +
//"inner join Mgr_Dist_Fixation MGR on MGR.To_code=T.territory_code and Mgr.sf_code=S.sf_code and mgr_sf_Code='" + sf_code + "' " +
//"where H.sf_Code='" + sf_code + "' and month(activity_date)='" + mnth + "' and year(activity_date)='" + yr + "' " +
//"group by activity_date order by activity_date";
            strQry = "select activity_date,max(dist*2)dist from( " +
"select activity_date,case mgr_dist when 0 then mr_dist else mgr_dist end dist,territory_name,MR_Code,MGR_Code from " +
"(select distinct activity_date,L.territory_code,MG.to_code MGR_Code,M.to_code MR_code,territory_name,isnull(MG.dist,0) MGR_Dist,isnull(M.Distance_In_Kms,0) MR_Dist,rtn_hq " +
"from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.trans_slno=H.trans_slno " +
"inner join Mas_ListedDr L on L.ListedDrCode=D.Trans_Detail_Info_Code " +
"inner join mas_territory_creation T on Cast(ltrim(T.territory_code)as varchar)=L.territory_code " +
"inner join mas_salesforce S on S.sf_Code=T.sf_Code and reporting_to_sf='" + sf_code + "' " +
"left outer join mas_distance_fixation M on M.to_code=cast(ltrim(T.territory_code)as varchar) " +
"left outer join Mgr_Dist_Fixation MG on MG.To_Code=cast(ltrim(T.territory_code)as varchar) and MGR_sf_code='"+ sf_code +"' " +
"where month(activity_date)='" + mnth + "' and year(activity_date)='" + yr + "' and H.sf_code='" + sf_code + "')as d)as terr group by activity_date order by activity_date";


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
        public DataSet getDistPrevMGR(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "select activity_date,MG.sf_code,max(MG.Dist)dist from DCRDetail_Lst_Trans D " +
            "inner join DCRMain_Trans H on D.trans_slno=H.trans_slno " +
            "inner join Mas_ListedDr L on L.listeddrcode=D.Trans_Detail_Info_Code " +
            "inner join mas_territory_creation T on cast(ltrim(T.territory_code)as varchar)=L.territory_code " +
            "inner join mas_salesforce S on S.sf_Code=T.sf_Code and reporting_to_sf='" + sf_code + "' " +
            "inner join Mgr_Allowance_Setup MG on MG.sf_code=S.sf_code and mgr_Code='" + sf_code + "' " +
            "where H.sf_Code='" + sf_code + "' and month(activity_date)='" + mnth + "' and year(activity_date)='" + yr + "' " +
            "group by activity_date,MG.sf_code order by activity_date";


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
        public DataSet getDistNextMGR(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "select lstdrs,DM.activity_date,L.territory_code,T.territory_name,Rtn_HQ from(select activity_date,max(trans_detail_slno)lstdrs from DCRDetail_Lst_Trans D " +
            "inner join DCRMain_Trans H on D.trans_slno=H.trans_slno " +
             "where H.sf_Code='" + sf_code + "' and month(activity_date)='" + mnth + "' and year(activity_date)='" + yr + "' " +
            "group by activity_date)as d " +
            "inner join DCRDetail_Lst_Trans DE on d.lstdrs=DE.trans_detail_slno " +
            "inner join DCRMain_Trans DM on DM.Trans_SlNo=DE.Trans_SlNo " +
            "inner join Mas_ListedDr L on L.listeddrcode=DE.Trans_Detail_Info_Code " +
            "inner join mas_territory_creation T on cast(ltrim(T.territory_code)as varchar)=L.territory_code " +
            "inner join mas_salesforce S on S.sf_Code=T.sf_Code and reporting_to_sf='" + sf_code + "' " +
            "inner join Mgr_Dist_Fixation MGR on MGR.To_code=T.territory_code and Mgr.sf_code=S.sf_code and MGR_sf_code='" + sf_code + "' " +
            "where DM.sf_Code='" + sf_code + "' and month(DM.activity_date)='" + mnth + "' and year(DM.activity_date)='" + yr + "' order by DM.activity_date";


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
        public int addAllDetailRecordMgrAutomatic(Dictionary<int, Dictionary<String, String>> values)
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
                            "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow,Mgr_amount,Mgr_Remarks,Mgr_total)" +
                            "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values[i]["sf_code"] + "' and Expense_Month=" + values[i]["month"] + " and Expense_Year=" + values[i]["year"] + "),'" +
                            values[i]["adate"].Replace("'", "\"") + "','" + values[i]["dayName"].Replace("'", "\"") + "','" + values[i]["workType"] + "','" + values[i]["adate1"] + "','" + values[i]["terrName"] + "','" + values[i]["cat"]
                            + "','" + values[i]["allowance"] + "','" + values[i]["distance"] + "','" + values[i]["fare"] + "','" + values[i]["catTemp"] + "','" + values[i]["total"] + "','" + values[i]["div_code"] + "','" + values[i]["from"] + "','" + values[i]["to"] + "','" + values[i]["allowance"] + "','" + values[i]["fare"] + "','" + values[i]["remarks"] + "','" + values[i]["total"] + "')";

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
        public DataSet getExpense_MGR_prevnextrelation(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpense_MGR_prevnextrelation '" + sf_code + "', '" + mnth + "','" + yr + "'";
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
        public DataSet getMGRAllowTyp(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec mgrallowtype '" + sf_code + "' ,'" + mnth + "','" + yr + "'";
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
        public DataSet getPlaceMGRNEW(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec getExpense_MGR_Places '" + sf_code + "' ,'" + mnth + "','" + yr + "'";
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
        public DataSet getPlaceMGRActual(string sf_code, string mnth, string yr, string divcode, string dt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "exec getAllowActual '" + sf_code + "' ,'" + mnth + "','" + yr + "','" + divcode + "','" + dt + "'";


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
    }
}
