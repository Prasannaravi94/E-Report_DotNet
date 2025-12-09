using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Stockist
    {
        private string strQry = string.Empty;

        public DataSet getSalesforce(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSalesforce = null;
            //strQry = " SELECT sf_code,Sf_Name " + 
            //         " FROM mas_salesforce  " +                                        
            //         " WHERE Division_Code='" + divcode + "'  AND SF_Status = 0 AND sf_type = 1   "; 

            //strQry = "SELECT sf_code,sf_Name,sf_hq,Reporting_To_SF, " +
            //        " (Select Sf_HQ from mas_salesforce where sf_code = a.Reporting_To_SF)HQ " +
            //        " FROM Mas_Salesforce a " +
            //        " WHERE (Division_Code like '" + divcode + ',' + "%'  or " +
            //         " Division_Code like '%" + ',' + divcode + ',' + "%') AND SF_Status = 0 AND sf_type = 1  ";

            strQry = "SELECT sf_code,sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' +Sf_HQ as sf_Name,sf_hq,Reporting_To_SF, " +
                    " (Select Sf_HQ from mas_salesforce where sf_code = a.Reporting_To_SF)HQ " +
                    " FROM Mas_Salesforce a " +
                    " WHERE (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') AND SF_Status = 0 AND sf_TP_Active_Flag=0 and  sf_type = 1  ";

             

            //SELECT sf_code,Sf_Name,Reporting_To_SF, (select Sf_HQ from mas_salesforce
            //    where sf_code = a.Reporting_To_SF)HQ  FROM mas_salesforce a WHERE Division_Code='28'
            //AND SF_Status = 0 AND sf_type = 1    order by HQ



                  //   " order by sf_code";
            try
            {
                dsSalesforce = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSalesforce;
        }

        public int GetSecondarysale()
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Trans_Secondary_Bill";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // SS sale Entry 
        public DataSet getStockist_Name_SE()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as stockist_code, ' --- Select --- ' as Stockist_Name " +
                      " UNION " +
                " select Stockist_Code,Stockist_Name  from Mas_Stockist_FM where Sale_Entry=1   ";
            //  " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
            //  " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            // " order by Sf_Name ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        public DataSet Get_Empcode_sale(string empcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sf_emp_id,sf_name,SF_Cat_Code,Sf_HQ,Sf_Code  from mas_salesforce where  sf_emp_id = '" + empcode + "' and sf_TP_Active_Flag=0";

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

        public DataSet Get_chemist_Code_sale(string Product_chem_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Chemist_ERP_Code,Chemists_Name,Chemists_Code from Mas_Chemists where  Chemist_ERP_Code = '" + Product_chem_code + "' and Chemists_Active_Flag=0";

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
        public DataSet Get_Doctor_Code_sale(string Product_doctor_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select ListedDrCode,ListedDr_Name,Doctor_ERP_Code from Mas_ListedDr where  ListedDrCode = '" + Product_doctor_code + "' and ListedDr_Active_Flag=0 ";

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
        public DataSet Get_Product_Code_sale(string Product_detail_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Code_SlNo,Product_Detail_Name,Product_Detail_Code,sale_Erp_Code from mas_product_detail where Product_Detail_Code = '" + Product_detail_code + "' and Product_Active_Flag=0 ";

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
        public DataSet getStockistCreate_StockistName_SE()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = " SELECT '' as stockist_code, ' --- Select --- ' as Stockist_Name " +
                      " UNION " + 
                "SELECT stockist_code,Stockist_Name " +
                   " FROM mas_Stockist_FM " +
                   " WHERE Sale_Entry=1 ";
            //  " AND Division_Code= '" + divcode + "' " +
            //  " AND stockist_code = '" + stockist_code + "'  ";
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
        public DataSet getStockist_Reporting(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSalesforce = null;
            strQry = " SELECT '' as sf_code, ' --- Select --- ' as Sf_Name " +
                      " UNION " +
                      " select Sf_Code,Sf_Name  from mas_salesforce " +
                      " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
                      " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%')) "; 
                        //" (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
                        //" order by Sf_Name ";

            try
            {
                dsSalesforce = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSalesforce ;
        }
        // Fieldforce stockist entry Map
        public DataSet getStockist_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
            //          " UNION " +
            //          " select Stockist_Code,Stockist_Name  from mas_Stockist where stockist_active_flag=0 AND Division_Code = '" + div_code + "'  ";


            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                     " UNION " +
                     " select Stockist_Code,case Territory when '--Select--' then Stockist_Name else Stockist_Name+' - '+Territory end as Stockist_Name  from mas_Stockist where stockist_active_flag=0 AND Division_Code = '" + div_code + "'  ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistCreate_StockistName(string divcode,string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile, SF_Code,state " +
                   " FROM mas_Stockist " +
                   " WHERE stockist_active_flag=0 " +
                   " AND Division_Code= '" + divcode + "' " +
                   " AND stockist_code = '" + stockist_code + "'  ";
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
        // Alphabet order
        public DataSet getS_C_A(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            /*strQry = " SELECT stockist_code,Stockist_Name FROM mas_stockist a " +
                     " WHERE a.Division_Code='" + divcode + "' AND Stockist_Code='" + stockist_code + "'  " +
                     " order by 2"; */

            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile, SF_Code " +
                   " FROM mas_Stockist " +
                   " WHERE stockist_active_flag=0 " +
                   " AND Division_Code= '" + divcode + "' " +
                   " AND LEFT(Stockist_Name,1) = '" + sAlpha + "' ";
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
        public DataSet getSalesForcelist_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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
        public DataSet getStockist_N(string ddltext)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "SELECT stockist_code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile,Territory, SF_Code " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 " +
                //  " AND Division_Code= '" + divcode + "' " + 
                    " AND Stockist_Code = '" + ddltext + "'  ";


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
        // alphbet
        public DataSet getStockist_Alphabet(string ddlvar)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "select '1' val,'All' stockist_name " +
            //         " union " +
            //         " select distinct LEFT(Stockist_Code,stockist_name,1) val, LEFT(Stockist_Code,stockist_name,1) stockist_name" +
            //       //  "SELECT Stockist_Code,Stockist_Name " +
            //         " FROM mas_Stockist " +
            //       " WHERE stockist_active_flag=0 " +
            //    //  " AND Division_Code= '" + divcode + "' " +
            //       " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";


            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                      " UNION " +
                     "SELECT Stockist_Code,Stockist_Name " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 " +
                //  " AND Division_Code= '" + divcode + "' " +
                    " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";
            //" AND Stockist_Name Like % '" + ddlvar + "' %  ";


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
        public DataSet getStockist_Alphabet_N()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "select '1' val,'All' stockist_name " +
            //         " union " +
            //         " select distinct LEFT(Stockist_Code,stockist_name,1) val, LEFT(Stockist_Code,stockist_name,1) stockist_name" +
            //       //  "SELECT Stockist_Code,Stockist_Name " +
            //         " FROM mas_Stockist " +
            //       " WHERE stockist_active_flag=0 " +
            //    //  " AND Division_Code= '" + divcode + "' " +
            //       " AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";


            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                      " UNION " + 
                    "SELECT Stockist_Code,Stockist_Name " +
                    " FROM mas_Stockist " +
                    " WHERE stockist_active_flag=0 ";
                //  " AND Division_Code= '" + divcode + "' " +
                    //" AND LEFT(Stockist_Name,1) = '" + ddlvar + "' ";
            //" AND Stockist_Name Like % '" + ddlvar + "' %  ";


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

       public DataSet getStockist_View(string divcode)
       {
           DB_EReporting db_ER = new DB_EReporting();

           DataSet dsDivision = null;

            strQry = " SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_ContactPerson,a.Stockist_Designation," +
                   " a.Stockist_Mobile,case a.Territory when '--Select--' then '' else a.Territory end as Territory,case a.State when '---Select---' then '' else a.State end as State,a.SF_Code, " +
                   " stuff((select ', '+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName " +
                    " FROM mas_stockist a where  stockist_active_flag=0 AND a.Division_Code= '" + divcode + "' ";

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

        public DataSet getStockistCreate_Reporting(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (sf_code != "admin")
            {
                //strQry = " SELECT sf_code,Sf_Name,Sf_Hq FROM mas_salesforce a " +
                //         " WHERE (a.Division_Code like '" + divcode + ',' + "%'  or " +
                //         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  AND SF_Status = 0 AND lower(sf_code) != 'admin' AND sf_type = 1 AND a.TP_Reporting_SF = '" + sf_code + "'" +
                //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                //         " order by Sf_Hq";

                strQry = " SELECT sf_code,Sf_Name,Sf_Hq , " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         "(select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as HQ FROM mas_salesforce a " +
                         " WHERE (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  AND SF_Status = 0 AND lower(sf_code) != 'admin' AND sf_type = 1 AND a.TP_Reporting_SF = '" + sf_code + "'" +
                         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                         " order by Sf_Hq";
            }
            else
            {
                //strQry = " Select Sf_code,Sf_Name,Sf_Hq From mas_salesforce a" +
                //         " where (a.Division_Code like '" + divcode + ',' + "%'  or " +
                //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') and SF_Status = 0 And lower(sf_code) != 'admin' And sf_type =1 " +
                //         " And a.Sf_status = 0 and a.sf_Tp_Active_flag = 0" +
                //         " order by Sf_Hq";

                strQry = " Select Sf_code,Sf_Name,Sf_Hq, " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as HQ From mas_salesforce a" +
                         " where (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%') and SF_Status = 0 And lower(sf_code) != 'admin' And sf_type =1 " +
                         " And a.Sf_status = 0 and a.sf_Tp_Active_flag = 0" +
                         " order by Sf_Hq";
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
        // I want to use list screen Gridview Option using select from Database           
        public DataSet getStockist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=0 AND Division_Code='" + divcode + "' ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        // Alphabat Order in list screen
        public DataSet getStockist_Alphabat(string divcode,string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
                     " AND LEFT(stockist_name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        //alphabetical order
        public DataSet getStockistview_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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

        public DataSet getStockistview_Alphabat(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_Mobile,Sf_Name as SfName,Stockist_Designation, " +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State" +
                     " FROM mas_Stockist a " +
                     " inner join Mas_Stockist_FM b On a.Stockist_Code=b.Stockist_Code inner join Mas_Salesforce sfc on b.SF_Code=sfc.Sf_Code" +
                     " WHERE a.stockist_active_flag=0 " +
                     " AND a.Division_Code= '" + divcode + "' " +
                     " AND LEFT(b.stockist_name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        // Search by Text
        public DataSet FindStockistlist(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                       "case Territory when '--Select--' then '' else Territory end as Territory," +
                       "case State when '---Select---' then '' else State end as State" +
                       " FROM mas_stockist " +
                       " WHERE stockist_active_flag=0  " +
                        sFindQry +
                       " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        // Sorting For StockistList 
        public DataTable getStockistList_DataTable(string divcode) 
        {
            DB_EReporting db_ER = new DB_EReporting(); 

            DataTable dtStockist = null;
            //strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory,State " +
            //           " FROM mas_stockist " +
            //           " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
            //           " ORDER BY 2";

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                  "case Territory when '--Select--' then '' else Territory end as Territory," +
                  "case State when '---Select---' then '' else State end as State" +
                  " FROM mas_stockist " +
                  " WHERE stockist_active_flag=0  " +
                  "  AND Division_Code= '" + divcode + "'" +
                  " ORDER BY 2";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }
         public DataTable getStockistFilter_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            //strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory,State " +
            //        " FROM mas_stockist " +
            //        " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' AND LEFT(Stockist_Name,1) = '" + sAlpha + "' " +
            //        " ORDER BY 2";

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
               "case Territory when '--Select--' then '' else Territory end as Territory," +
               "case State when '---Select---' then '' else State end as State" +
               " FROM mas_stockist " +
               " WHERE stockist_active_flag=0  " +
               "  AND Division_Code= '" + divcode + "'  AND LEFT(Stockist_Name,1) = '" + sAlpha + "' " +
               " ORDER BY 2";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        public DataTable getStockistSearch_DataTable(string divcode, string sAlpha,string SearchBy)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory,State " +
                      " FROM mas_stockist " +
                      " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' AND LEFT(" + SearchBy + ",1) = '" + sAlpha + "' " +
                      " ORDER BY 2";
            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        // I want to Create Stockist Details using select from Database
        public DataSet getStockist_Create(string divcode, string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "SELECT Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile," +
     "case Territory when '--Select--' then '' else Territory end as Territory,SF_Code, PoolStatus," +
     "case State when '---Select---' then '' else State end as State" +
     " FROM mas_stockist " +
     " WHERE stockist_active_flag=0  " +
     "  AND Division_Code= '" + divcode + "'  AND stockist_code = '" + stockist_code + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        // Same Stockist_name could not be Created return will be Exit
        public bool RecordExist(string stockist_name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(stockist_code) FROM mas_stockist WHERE stockist_name ='" + stockist_name + "'  ";
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
        public bool RecordExist(string stockist_code, string stockist_name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(stockist_code) FROM mas_stockist WHERE stockist_code != '" + stockist_code + "' AND stockist_name ='" + stockist_name + "' and Stockist_Active_Flag =0   ";

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
        // Insert the Values in Stockist Details
        public int RecordAdd(string divcode, string SF_Name, string stockist_name, string stockist_address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobile, string Territory, string PoolStatus, string State)
        {
            int iReturn = -1;
            // if (!RecordExist(stockist_name))
            //  {
            try
            {
                int stockist_code = -1;

                DB_EReporting db = new DB_EReporting();


                strQry = "SELECT CASE WHEN COUNT(stockist_code)>0 THEN MAX(stockist_code) ELSE 0 END FROM mas_stockist";
                stockist_code = db.Exec_Scalar(strQry);
                stockist_code += 1;


                strQry = " INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory,PoolStatus, Created_Date,State) " +
                         " values('" + divcode + "', '" + stockist_code + "', '" + SF_Name + "','" + stockist_name + "', '" + stockist_address + "', '" + stockist_ContactPerson + "', '" + stockist_Designation + "', 0 ,'" + stockist_mobile + "','" + Territory + "','" + PoolStatus + "',getdate(),'" + State + "' )";

                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
                }
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
        
        // Insert into Another Table
        public int RecordAdd_FM(string divcode,int stockist_code, string stockist_name,string SF_Code,string Sale_Entry)
        {
            int iReturn = -1;
            // if (!RecordExist(stockist_name))
            //  {
            try
            {
                //int SF_Code = -1;

                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT CASE WHEN COUNT(SF_Code)>0 THEN MAX(SF_Code) ELSE 0 END FROM mas_stockist_FM";
                //SF_Code = db.Exec_Scalar(strQry);
                //SF_Code += 1;

                strQry = " INSERT INTO mas_stockist_FM(Stockist_Code,Stockist_Name ,SF_Code ,Sale_Entry,Division_Code) " +
                         " values( '" + stockist_code + "', '" + stockist_name + "','" + SF_Code + "', 0 ,'" + divcode + "')";

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
       
        // InlineEdit Option Update the Stockist Details
        public int RecordUpdate(string divcode, string stockist_code, string stockist_name, string stockist_ContactPerson, string stockist_mobile, string Territory, string State)
        {
            int iReturn = -1;
            //  if (!RecordExist(stockist_code, stockist_name))
            // {
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_stockist " +
                          " SET stockist_name = '" + stockist_name + "' , " +
                          " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                          " stockist_mobile = '" + stockist_mobile + "' , " +
                          " Territory = '" + Territory + "', " +
                          " LastUpdt_Date = getdate(), " +
                          " State = '" + State + "' " +
                          " WHERE stockist_code = '" + stockist_code + "' AND  Division_Code = '" + divcode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            // else
            //  {
            //     iReturn = -3;
            // }
            return iReturn;
        }

        public int RecordUpdate(string divcode, string stockist_code, string SF_Name, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobile, string Territory, string PoolStatus, string State)
        {
            int iReturn = -1;
            // if (!RecordExist(stockist_code, stockist_name))
            // {
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_stockist " +
                            " SET stockist_name = '" + stockist_name + "' , " +
                            " sf_code = '" + SF_Name + "' , " +
                            " stockist_Address = '" + stockist_Address + "', " +
                            " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                            " stockist_Designation = '" + stockist_Designation + "' , " +
                            " stockist_mobile = '" + stockist_mobile + "' , " +
                            " Territory = '" + Territory + "', PoolStatus = '" + PoolStatus + "', " +
                            " LastUpdt_Date = getdate() ," +
                            " State = '" + State + "' " +
                            " WHERE stockist_code = '" + stockist_code + "' AND Division_Code = '" + divcode + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            // else
            // {
            //     iReturn = -3;
            //  }
            return iReturn;
        }
       public int RecordUpdate_Sale_Entry(string divcode, string sChkSalesforce)
       {
           int iReturn = -1;

           try
           {
               DB_EReporting db = new DB_EReporting();

               strQry = " UPDATE mas_stockist_FM " +
                        " SET Sale_Entry= 1 " +
                        " WHERE SF_Code = '" + sChkSalesforce + "' AND Division_Code = '" + divcode + "' ";
               iReturn = db.ExecQry(strQry);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return iReturn;
       }
       // DeActivate Option Deactivate in Stockist Details
        public int DeActivate(string stockist_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE mas_stockist " +
                         " SET stockist_active_flag=1 , " +
                         " LastUpdt_Date = getdate(), " +
                         " DeActivate_Date=getdate(),Activate_Date=Null" +
                         " WHERE stockist_code = '" + stockist_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAdd_Pool(string divcode, string Pool_SName, string Pool_Name, string Sf_Code)
        {
            int iReturn = -1;
          
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Pool_Id)+1,'1') Pool_Id from Mas_Pool_Area_Name ";
                        int Pool_Id = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Pool_Area_Name(Pool_Id,Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date,Active,Sf_Code)" +
                                 "values('" + Pool_Id + "','" + divcode + "','" + Pool_SName + "', '" + Pool_Name + "',getdate(),getdate(),0,'" + Sf_Code + "') ";

                        iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

            return iReturn;
        }
        public DataSet getPoolName(string divcode, string Pool_Id, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_SName,Pool_Name " +
                     " FROM Mas_Pool_Area_Name WHERE Pool_Id= '" + Pool_Id + "' AND Division_Code= '" + divcode + "' and Active=0  and Sf_Code='" + Sf_Code + "'" +
                     " ORDER BY Pool_Name";
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

        public DataSet GetPool(string Pool_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Pool_Name from Mas_Pool_Area_Name where Pool_Name='" + Pool_Name + "'  and Division_Code = '" + div_code + "' and Active=0  ";

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
        public DataSet getPoolName_List(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name,State " +
                     " FROM Mas_Pool_Area_Name WHERE Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "' " +
                     " ORDER BY Pool_Name";
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
        public DataSet getPool_Name(string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Pool_Id, '--Select--' as Pool_Name " +
                      " UNION " +
                      " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0 and Sf_Code='" + Sf_Code + "'  ";           

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockistCheck(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = "EXEC sp_UserList_MR_Stockist '" + divcode + "', 'admin' ";

            //strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
            //         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
            //          " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
            //          " a.sf_hq, a.sf_password FROM mas_salesforce a " +
            //          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";

            strQry = " SELECT a.sf_Code, a.sf_Name +' - ' + a.sf_Designation_Short_Name +' - '+ a.Sf_HQ as sf_Name, a.Sf_UserName, a.sf_Type, " +
                      " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                       " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                       " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                       " WHERE a.SF_Status!=2 and sf_type=1 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                      " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";


            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getStockist_Filter(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            //strQry = "EXEC sp_UserList_MR_Stockist '" + divcode + "', 'admin' ";
            if (sf_code != "admin")
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
                         " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                          " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                          " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                          " a.Division_Code like '%" + ',' + divcode + ',' + "%') and a.Reporting_To_SF ='" + sf_code + "' ";
            }
            else
            {
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF," +
                         " (select sf_hq from mas_salesforce where sf_code=a.Reporting_To_SF) as rep_hq, " +
                         " a.sf_hq, a.sf_password FROM mas_salesforce a " +
                         " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and sf_type=1 AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%')  ";
            }
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        //changes done By Reshmi
        public DataSet getStockist_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "Select No_Of_Sl_StockistAllowed from Admin_Setups where Division_Code='" + div_code + "'";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        public DataSet getStockist_Count(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsStockist = null;

            strQry = "SELECT COUNT(Stockist_Code) from mas_stockist WHERE Division_Code='" + div_code + "' and Stockist_Active_Flag= 0";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;

        }
        public int GetStockistCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Stockist_Code)+1,'1') Stockist_Code from Mas_Stockist";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetPool(string Pool_Name, string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Pool_Name from Mas_Pool_Area_Name where Pool_Name='" + Pool_Name + "'  and Division_Code = '" + div_code + "' and Active=0 and Sf_Code='" + Sf_Code + "' ";

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

        public int GetPrimary()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(SNo)+1,'1') SNo from JS_Primary_Upload";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int GetSecondary()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Sl_No)+1,'1') SNo from Secondary_Sale_Upload_Excel";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int GetPoolid()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
            
                strQry = "SELECT isnull(Max(Pool_Id)+1,'1') Pool_Id from Mas_Pool_Area_Name ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetSuper_stk(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "  SELECT '' as SS_Code, '---Select---' as SS_Name,'' as Division_Code, '' as State_Name " +
                     " UNION " +
                     " select SS_Code,SS_Name,Division_Code,State_Name from Mas_SuperStockiest where  Division_Code = '" + div_code + "' ";

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
        public int Add_Super_Stockist(string divcode, string SS_Name, string state_Name, string state_code, string Email)
        {


            int iReturn = -1;

            if (!Record_SStockist_Name(SS_Name, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    int SS_Code = -1;
                    strQry = "SELECT ISNULL(MAX(SS_Code),0)+1 FROM Mas_SuperStockiest";
                    SS_Code = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO Mas_SuperStockiest(SS_Code,Division_Code,SS_Name,State_code,State_Name,Email_ID)" +
                     "values(" + SS_Code + ",'" + divcode + "','" + SS_Name + "', '" + state_code + "', '" + state_Name + "', '" + Email + "') ";

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
        public DataSet getStockist_List(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;




            strQry = " SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_ContactPerson,a.Stockist_Designation," +
                        " a.Stockist_Mobile,case a.Territory when '--Select--' then '' else a.Territory end as Territory,case a.State when '---Select---' then '' else a.State end as State,a.SF_Code " +

                         " FROM mas_stockist a where  stockist_active_flag=0 AND a.Division_Code= '" + divcode + "' order by  State";

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
        public DataSet getStk_MapSS(string ss_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Stockist_Code,Stockist_Name from Map_Super_Stockist where SS_Code='" + ss_code + "' and Division_Code='" + div_code + "' ";

            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }
        public int RecordAdd_SStockist_Map(string st_code, string st_Name, string ss_code, string ss_Name, string Division_Code)
        {
            int iReturn = -1;


            try
            {
                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_Super_Stockist ";
                Sl_No = db.Exec_Scalar(strQry);



                if (sstockist_RecordExist(ss_code, Division_Code, st_code))
                {
                    strQry = "update Map_Super_Stockist set Stockist_Code ='" + st_code + "',Stockist_Name='" + st_Name + "'  " +
                        " where SS_Code = '" + ss_code + "' and  Stockist_Code ='" + st_code + "' and Division_Code='" + Division_Code + "' ";
                }
                else
                {
                    strQry = " insert into Map_Super_Stockist (Sl_No,Stockist_Code,Stockist_Name,SS_Code,SS_Name, " +
                       " Division_Code,Created_Date) " +
                       " VALUES('" + Sl_No + "','" + st_code + "', '" + st_Name + "', '" + ss_code + "', '" + ss_Name + "',  " +
                       " '" + Division_Code + "',  getdate() )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                //throw ex;
            }

            return iReturn;
        }
        public bool Record_SStockist_Name(string SS_Name, string divcode)
        {

            bool bRecordExist = false;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT COUNT(SS_Name) FROM dbo.Mas_SuperStockiest WHERE SS_Name='" + SS_Name + "' and Division_Code= '" + divcode + "' ";

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


        private bool sstockist_RecordExist(string ss_code, string div_code, string st_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "Select count(*) from Map_Super_Stockist " +

                          " where SS_Code = '" + ss_code + "' and  Stockist_Code ='" + st_code + "' and Division_Code='" + div_code + "' ";
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
        public int Delete_StockistMap(string st_code, string st_Name, string ss_code, string ss_Name, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                if (sstockist_RecordExist(ss_code, Division_Code, st_code))
                {
                    strQry = "delete from Map_Super_Stockist  " +
                        " where SS_Code = '" + ss_code + "' and  Stockist_Code ='" + st_code + "' and Division_Code='" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet Get_stk(string div_code, string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Stockist_Code,s.Stockist_Name,Stockist_Designation,Product_ERP_Code,Product_ERP_Name, Pack,Qty,Rate, Value,Free,Replacement from Mas_Stockist s, JS_Primary_Upload j where s.Stockist_Designation = j.Stockist_ERP_Code " +
                     " and Stockist_Code='" + Stockist_Code + "' and s.Division_code='" + div_code + "' ";

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

        public DataSet Get_stk_View(string div_code, string Stockist_Code, int cmonth, int cyear, string Prod_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Stockist_Code,s.Stockist_Name,Stockist_Designation,Product_ERP_Code,Product_ERP_Name, Pack,Qty,Rate, Value,Free,Replacement from Mas_Stockist s, JS_Primary_Upload j where s.Stockist_Designation = j.Stockist_ERP_Code " +
                     " and Stockist_Code='" + Stockist_Code + "' and s.Division_code='" + div_code + "' and Pmonth='" + cmonth + "' and Pyear = '" + cyear + "' and Product_ERP_Code='" + Prod_code + "'";

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
        /*-------------------------- Add Stockist- HQ- Creation (30/08/2016) -------------------------------------*/

        public int Add_Stockist_HQ(string divcode, string Pool_SName, string Pool_Name, string Sf_code)
        {


            int iReturn = -1;
            if (!S_Record_StockistHQ_SubName(Pool_SName, divcode, Sf_code))
            {
                if (!Record_StockistHQ_Name(Pool_SName, divcode, Sf_code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        int PoolID = -1;
                        strQry = "SELECT ISNULL(MAX(Pool_Id),0)+1 FROM Mas_Pool_Area_Name";
                        PoolID = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Pool_Area_Name(Pool_Id,Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date,Active,Sf_Code)" +
                           "values(" + PoolID + ",'" + divcode + "','" + Pool_SName + "', '" + Pool_Name + "',getdate(),getdate(),0,'" + Sf_code + "') ";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        /*--------------------------Check Stockist HQ Short Name Exist(30/08/2016) -------------------------------------*/

        public bool S_Record_StockistHQ_SubName(string Pool_SName, string divcode, string Sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_SName) FROM dbo.Mas_Pool_Area_Name WHERE Pool_SName='" + Pool_SName + "' and Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_code + "'";
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


        /*-------------------------- Check Stockist HQ Name Exist(30/08/2016) -------------------------------------*/

        public bool Record_StockistHQ_Name(string Pool_Name, string divcode, string Sf_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_Name) FROM dbo.Mas_Pool_Area_Name WHERE Pool_Name='" + Pool_Name + "' and Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "'";
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

        /*---------------------------------------- Get Stockist Detail (30/08/2016) -------------------------------------------------*/
        public DataSet GetStockist_Detail(string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[SP_Get_Stockist_Detail] '" + div_code + "', '" + Sf_Code + "' ";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }

        /*---------------------------------------- Update Stockist Detail (1/09/2016) -------------------------------------------------*/
        public int Update_Stockist_Detail(string divcode, int Stock_Code, string ERPCode, string HQName, string State)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Stockist " +
                         " SET Stockist_Designation='" + ERPCode + "',Territory='" + HQName + "',State='" + State + "'" +
                         "   WHERE Stockist_Code = '" + Stock_Code + "' AND Division_Code = '" + divcode + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        /*---------------------------------------- Update HQ Name (08/09/2016) -------------------------------------------------*/
        public int RecordUpdateHq(int Pool_code, string Pool_sName, string Pool_Name, string divcode, string Sf_Code)
        {
            int iReturn = -1;
            if (!RecordExistHq(Pool_code, Pool_sName, divcode, Sf_Code))
            {
                if (!RecordExistName(Pool_code, Pool_Name, divcode, Sf_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();


                        strQry = "UPDATE Mas_Pool_Area_Name " +
                                 " SET Pool_SName = '" + Pool_sName + "', " +
                                 " Pool_Name = '" + Pool_Name + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Pool_Id = '" + Pool_code + "' ";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        /*----------------------------------------Check Short HQ Name Exist (08/09/2016) -------------------------------------------------*/
        public bool RecordExistHq(int Pool_code, string Pool_SName, string divcode, string Sf_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_SName) FROM Mas_Pool_Area_Name WHERE Pool_SName= '" + Pool_SName + "' AND Pool_Id!='" + Pool_code + "' AND Division_Code='" + divcode + "' and Sf_Code='" + Sf_Code + "' ";

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

        /*---------------------------------------- Check HQ Name Exist (08/09/2016) -------------------------------------------------*/

        public bool RecordExistName(int Pool_code, string Pool_Name, string divcode, string Sf_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_Name) FROM Mas_Pool_Area_Name WHERE Pool_Name= '" + Pool_Name + "' AND Pool_Id!='" + Pool_code + "' AND Division_Code='" + divcode + "' and Sf_Code='" + Sf_Code + "' ";

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

        /*---------------------------------------- Search Stock Name (13/09/2016) -------------------------------------------------*/
        public DataSet Search_Stock_Name(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Mobile,SF_Code,Stockist_Designation, " +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State," +
                     " stuff((select ', '+cast((ROW_NUMBER() OVER ( ORDER BY SF_Name ))as varchar(10))+'.'+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName " +
                     " FROM mas_stockist a " +
                     " WHERE stockist_active_flag=0  " +
                      sFindQry +
                     " ORDER BY Stockist_Name,State,Territory";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*---------------------------------------- Get ReActive Stockist Detail (28/09/2016) -------------------------------------------------*/
        public DataSet Get_Active_Stockist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
           
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*-----------------------  Sorting For StockistList  (28/09/2016) ---------------------*/
        public DataTable getStockistList_Sorting(string div_code, string Stock_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            strQry = " EXEC [dbo].[SP_Get_StockistList_Filter] '" + div_code + "', '" + Stock_Name + "' ";


            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Search Statewise Stock (28/09/2016) -------------------------------------------------*/
        public DataTable Search_State_StockName(string divcode, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=0 AND Division_Code='" + divcode + "' and State='" + State + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Search HQ Stock (28/09/2016) -------------------------------------------------*/
        public DataTable Search_HQ_Stockist(string divcode, string HQName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=0 AND Division_Code='" + divcode + "' and Territory='" + HQName + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*---------------------------------------- Get ReActive Stockist Detail (28/09/2016) -------------------------------------------------*/
        public int Activate_Stockist(string stockist_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE mas_stockist " +
                         " SET stockist_active_flag=0 , " +
                         " LastUpdt_Date = getdate(), " +
                          " DeActivate_Date=Null,Activate_Date=getdate()" +
                         " WHERE stockist_code = '" + stockist_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        /*-----------------------  Sorting For StockistList Empty  (28/09/2016) ---------------------*/
        public DataTable getStockistList_sort_Empty(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;


            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                  "case Territory when '--Select--' then '' else Territory end as Territory," +
                  "case State when '---Select---' then '' else State end as State" +
                  " FROM mas_stockist " +
                  " WHERE stockist_active_flag=0  " +
                  "  and Division_Code='" + div_code + "'" +
                  " ORDER BY 2";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Search by  ReActivate Stockist List (28/09/2016) -------------------------------------------------*/

        public DataTable Find_ReActive_Stocklist(string div_code, string Stock_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;
            strQry = " EXEC [dbo].[SP_Get_ReActive_StockistList_Filter] '" + div_code + "', '" + Stock_Name + "' ";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Search by ReActive HQ Stockist (28/09/2016) -------------------------------------------------*/
        public DataTable Search_ReActivate_HQ_Stockist(string divcode, string HQName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "' and Territory='" + HQName + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Search by ReActivate Statewise Stock (28/09/2016) -------------------------------------------------*/
        public DataTable Search_ReActivate_State_Stockist(string divcode, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                    "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "' and State='" + State + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Alphabat Order in ReActivate Stockist List (28/09/2016) -------------------------------------------------*/
        public DataSet get_ReActive_Stockist_Alphabat(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=1 AND Division_Code= '" + divcode + "' " +
                     " AND LEFT(stockist_name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*---------------------------------------- ReActivate Sorting For StockistList  (28/09/2016) -------------------------------------------------*/
        public DataTable get_ReActivate_StockistList(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                 "case Territory when '--Select--' then '' else Territory end as Territory," +
                 "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                 "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "'  ORDER BY 2";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Sorting For  ReActivate StockistList (28/09/2016) -------------------------------------------------*/

        public DataTable get_ReActive_StockistList_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
          
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                  "case Territory when '--Select--' then '' else Territory end as Territory," +
                  "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                  "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "' ORDER BY 2";


            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Filter ReActivate StockistList (28/09/2016) -------------------------------------------------*/
        public DataTable get_ReActive_StockistFilter_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;
         
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile," +
                  "case Territory when '--Select--' then '' else Territory end as Territory," +
                  "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                  "   FROM mas_stockist  WHERE stockist_active_flag=1 AND Division_Code='" + divcode + "' AND LEFT(Stockist_Name,1) = '" + sAlpha + "' ORDER BY 2";


            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }
        

        /*---------------------------------------- FillSF Reactivate Stockist Alpha (28/09/2016) -------------------------------------------------*/
        public DataSet get_ReActive_Stockist_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=1 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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


        /*---------------------------------------- DeActivate Pool Name List (28/09/2016) -------------------------------------------------*/
        public int DeActivate_PoolName_List(int PoolId)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Pool_Area_Name " +
                         " SET Active=1 , " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Pool_Id = " + PoolId + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetStockist_Detail_Hq(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "EXEC Stockist_Detail_Hq '" + divcode + "', '" + sf_code + "' ";


            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet GetStockist_Detail_New(string div_code, string Sf_Code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[Sec_Product_Stkwise] '" + div_code + "', '" + Sf_Code + "', " + imonth + "," + iyear + " ";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }
        public DataSet GetSale_HQ(string div_code, string Sf_Code, int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = " EXEC [dbo].[SecSale_Analysis_Hqwise_Temp] '" + div_code + "', '" + Sf_Code + "', " + imonth + "," + iyear + " ";

            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }

        /*-----------------------  Search by  ReActivate Stockist List Empty  (27/09/2016) ---------------------*/
        public DataTable getStockistList_ReActive_Empty(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;


            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                  "case Territory when '--Select--' then '' else Territory end as Territory," +
                  "case State when '---Select---' then '' else State end as State,CONVERT(VARCHAR(10),LastUpdt_Date,103)as DeActivateDate" +
                  " FROM mas_stockist " +
                  " WHERE stockist_active_flag=1  " +
                  "  and Division_Code='" + div_code + "'" +
                  " ORDER BY 2";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }



        /*-----------------------  Get MR wise Stockist Detail  (11/11/2016) ---------------------*/

        public DataSet Get_Stockist_MR_Wise(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " EXEC [dbo].[SP_Get_MR_wise_StockistDetail] '" + divcode + "', '" + Sf_Code + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Get MR  Stockist Alphabet  (11/11/2016) ---------------------*/
        public DataSet get_MR_Stockist_Alphabet(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " EXEC [dbo].[SP_Get_MR_Stockist_Alpha] '" + divcode + "', '" + Sf_Code + "' ";

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


        // Alphabat Order in list screen

        /*-----------------------  Get MR Stockist List Alphabat   (11/11/2016) ---------------------*/

        public DataSet getStockist_Alphabat_OrderList_MR(string divcode, string sAlpha, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = " EXEC [dbo].[SP_Get_Stockist_Alphabat_Order] '" + divcode + "', '" + Sf_Code + "','" + sAlpha + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Sorting for MR Stockist List (11/11/2016) ---------------------*/

        public DataTable get_MR_StockistList_DataTable(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            strQry = " EXEC [dbo].[SP_Get_MR_wise_StockistDetail] '" + divcode + "', '" + Sf_Code + "' ";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*-----------------------  Sorting MR Stockist List Alphabat Order   (11/11/2016) ---------------------*/

        public DataTable get_MR_Stockist_Alphabat_Filter(string divcode, string sAlpha, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = " EXEC [dbo].[SP_Get_Stockist_Alphabat_Order] '" + divcode + "', '" + Sf_Code + "','" + sAlpha + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*-----------------------  Sorting For MR StockistList  (11/11/2016) ---------------------*/
        public DataTable get_MR_StockistList_Sorting(string div_code, string Stock_Name, string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            strQry = " EXEC [dbo].[SP_Get_MR_StockistList_Filter] '" + div_code + "', '" + Stock_Name + "','" + sf_Code + "' ";


            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Search MR Statewise StockList (11/11/2016) -------------------------------------------------*/
        public DataTable Search_MR_Statewise_StockName(string divcode, string SfCode, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = " EXEC [dbo].[SP_Get_MR_StateWise_StockistList] '" + divcode + "', '" + SfCode + "','" + State + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Search MR HQ wise StockistList (11/11/2016) -------------------------------------------------*/
        public DataTable Search_MR_HQwise_StockistList(string divcode, string SfCode, string HQName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = " EXEC [dbo].[SP_Get_MR_HQ_StockistList] '" + divcode + "', '" + SfCode + "','" + HQName + "' ";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Sorting For MR wise StockistList Empty  (11/11/2016) ---------------------*/
        public DataTable getStockistList_MR_sort_Empty(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            strQry = " EXEC [dbo].[SP_Get_MR_wise_StockistDetail] '" + divcode + "', '" + Sf_Code + "' ";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }
        public int RecordExistPrimaryHead(string Stockist_ERP_Code, string divCode, string strmonth, string stryear)
        {

            int iTransSlNo = 0;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select Trans_sl_No from Trans_Primary_Head WHERE  Division_Code='" + divCode + "' AND Trans_Month='" + strmonth + "' AND Trans_Year ='" + stryear + "' and Stockist_ERP_Code='" + Stockist_ERP_Code + "' ";
                iTransSlNo = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iTransSlNo;
        }

        public DataSet Get_Stockist_Code_Primary(string div_code, string Stock_erp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code='" + div_code + "' and stockist_designation ='" + Stock_erp_code + "'  and Stockist_Active_Flag=0 ";

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
        public DataSet Get_Product_Code_Primary(string div_code, string Product_erp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Code_SlNo,Product_Detail_Name,product_detail_code from mas_product_detail where Division_Code='" + div_code + "' and sale_Erp_Code = '" + Product_erp_code + "' and Product_Active_Flag=0 ";

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
        public string RecordHeadAdd_Primary(string Stockist_ERP_Code, string stockist_code, string Stockist_Name, string division_code, string transMonth, string transYear)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            int Trans_sl_No = -1;
            strQry = "SELECT isnull(Count(Trans_sl_No)+1,'1') Trans_sl_No  FROM Trans_Primary_Head";
            Trans_sl_No = db.Exec_Scalar(strQry);

            try
            {

                strQry = " INSERT INTO [Trans_Primary_Head]([Trans_sl_No],[Stockist_ERP_Code] ,[Stockist_Code],[Stockist_Name],[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Uploaded_Date]) " +
                       " VALUES ( " + Trans_sl_No + ",'" + Stockist_ERP_Code + "','" + stockist_code + "','" + Stockist_Name + "' , '" + division_code + "', '" + transMonth + "', '" + transYear + "', getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Primary_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();


        }

        //public string RecordDetailsAddPrimary(string TransSlNo, string Product_ERP_Code, string Product_code, string Product_Name, string Pack, string Qty, string Rate, string amount, string division_code, string Stock_Code, string Stock_Name)
        public string RecordDetailsAddPrimary(string TransSlNo, string Product_ERP_Code, string Product_code, string Product_Name, string Pack, string Qty, string Rate, string amount, string division_code, string Stock_Code, string Stock_Name, string free, string ret, string repl, string type, string others, string depo_code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Trans_Detail_No = -1;
                strQry = "SELECT isnull(Count(Trans_Detail_No)+1,'1') Trans_Detail_No  FROM Trans_Primary_Detail";
                Trans_Detail_No = db.Exec_Scalar(strQry);

                //strQry = " INSERT INTO [Trans_Primary_Detail]([Trans_Detail_No],[Trans_sl_No],[Product_ERP_Code],[Product_Code],[Product_Name],[Pack],[Sale_Qty],[Rate],[Amount],[Division_code],[Stockist_Code],[Stockist_Name],[Created_Date]) " +
                //       " VALUES ( '" + Trans_Detail_No + "','" + TransSlNo + "' , '" + Product_ERP_Code + "', '" + Product_code + "','" + Product_Name + "','" + Pack + "','" + Qty + "','" + Rate + "','" + amount + "','" + division_code + "', '" + Stock_Code + "','" + Stock_Name + "',getdate()) ";

                strQry = " INSERT INTO [Trans_Primary_Detail]([Trans_Detail_No],[Trans_sl_No],[Product_ERP_Code],[Product_Code],[Product_Name],[Pack],[Sale_Qty],[Rate],[Amount],[Division_code],[Stockist_Code],[Stockist_Name],Free_Qty,Return_Qty,Repl_Qty,Type,Others,Depot_Code) " +
                       " VALUES ( '" + Trans_Detail_No + "','" + TransSlNo + "' , '" + Product_ERP_Code + "', '" + Product_code + "','" + Product_Name + "','" + Pack + "','" + Qty + "','" + Rate + "','" + amount + "','" + division_code + "', '" + Stock_Code + "','" + Stock_Name + "','" + free + "','" + ret + "','" + repl + "','" + type + "','" + others + "','" + depo_code + "') ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }

        public DataSet Search_Stock_Name_Alpha(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name " +
                     " FROM mas_stockist a " +
                     " WHERE stockist_active_flag=0  " +
                      sFindQry +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Deactivate All Stockist  (08/03/2017) ---------------------*/
        public int DeActivate_Stockist(string DivCode, string Stock_code, int iflag)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE [dbo].[Mas_Stockist] " +
                            " SET Stockist_Active_Flag = '" + iflag + "' , " +
                            " LastUpdt_Date = getdate(), " +
                            " DeActivate_Date=getdate(),Activate_Date=Null" +
                            " WHERE Division_Code = '" + DivCode + "' and Stockist_Code='" + Stock_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }


        /*-----------------------  StateWise HQ Create (08/03/2017) ---------------------*/
        public DataSet Get_StateWise_HQ(string StateName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Distinct Territory from [dbo].[Mas_Stockist]  where State='" + StateName + "'  and Division_Code = '" + div_code + "' and Stockist_Active_Flag=0 ";

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


        /*---------------------------------------- Search HQ Name (23/03/2017) -------------------------------------------------*/
        public DataTable Search_Hq_Name(string divcode, string HqId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsStockist = null;

            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name " +
                     " FROM Mas_Pool_Area_Name WHERE Division_Code= '" + divcode + "' and Active=0 and Pool_Name like'" + HqId + "%'" +
                     " ORDER BY Pool_Name";

            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }       

        /*---------------------------------------- Bind State wise Stockist Name (23/03/2017) -------------------------------------------------*/
        public DataSet Get_Statewise_Stockist(string div_code, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = " SELECT '' as stockist_code, ' --- Select the Stockist --- ' as Stockist_Name " +
                      " UNION " +
                      " select Stockist_Code,case Territory when '--Select--' then Stockist_Name else Stockist_Name+' - '+Territory end as Stockist_Name  from mas_Stockist where stockist_active_flag=0 AND Division_Code = '" + div_code + "' and State='" + State + "'  ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }


        /*---------------------------------------- Bind Stockist Detail (23/03/2017) -------------------------------------------------*/
        public DataSet GetStockist_Sample()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;

            strQry = "select '' as Stockist_Code,'Select Stockist' as Stockist_Name,'Select ERP Code' as Stockist_Designation,'' as Stockist_ContactPerson,'' as Stockist_Mobile,'Select HQ' as Territory,'Select State' as State ";

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

        public DataSet Get_HQ_Stockist(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " select Distinct Territory from dbo.Mas_Stockist where Division_Code='" + div_code + "' " +
            " and Stockist_Active_Flag=0 order by Territory ";
	
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet getPoolName_List_New(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            //strQry = "SELECT Pool_Id,Pool_SName,Pool_Name " +
            //         " FROM Mas_Pool_Area_Name WHERE Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "' " +
            //         " ORDER BY Pool_Name";

            strQry = " SELECT max(Pool_Id) Pool_Id,Pool_Name,Pool_SName " +
                   " FROM Mas_Pool_Area_Name WHERE Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "' " +
                   " group by Pool_SName,Pool_Name " +
                   " ORDER BY Pool_Name ";
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


        /*-------------------------- Add Stockist- HQ- Creation (26/05/2017) -------------------------------------*/

        public int Create_Stockist_HQ(string divcode, string Pool_SName, string Pool_Name, string Sf_code, string State)
        {


            int iReturn = -1;
            if (!S_Record_StockistHQ_SubName(Pool_SName, divcode, Sf_code))
            {
                if (!Record_StockistHQ_Name(Pool_Name, divcode, Sf_code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        int PoolID = -1;
                        strQry = "SELECT ISNULL(MAX(Pool_Id),0)+1 FROM Mas_Pool_Area_Name";
                        PoolID = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Pool_Area_Name(Pool_Id,Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date,Active,Sf_Code,State)" +
                           "values(" + PoolID + ",'" + divcode + "','" + Pool_SName + "', '" + Pool_Name + "',getdate(),getdate(),0,'" + Sf_code + "','" + State + "') ";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        /*-------------------------- Get Statewise HQ Detail (26/05/2017) -------------------------------------*/
        public DataSet Get_HQ_Detail(string div_code, string Sf_Code, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Pool_Id, '--Select--' as Pool_Name " +
                      " UNION " +
                      " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0 and Sf_Code='" + Sf_Code + "' and State='" + State + "' order by Pool_Name ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  StateWise HQ Detail (26/05/2017) ---------------------*/
        public DataSet Get_Statewise_HQ_Det(string StateName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //            strQry = "select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0 " +
            //"and Sf_Code='admin' and State='" + StateName + "' order by Pool_Name";

            strQry = " SELECT '' as Pool_Id, '--Select--' as Pool_Name " +
            " UNION " +
            " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0 and Sf_Code='admin' and State='" + StateName + "' order by Pool_Name";

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

        /*---------------------------------------- Update HQ Name (26/05/2017) -------------------------------------------------*/
        public int Update_Stockist_HQ(int Pool_code, string Pool_sName, string Pool_Name, string divcode, string Sf_Code, string State)
        {
            int iReturn = -1;
            if (!RecordExistHq(Pool_code, Pool_sName, divcode, Sf_Code))
            {
                if (!RecordExistName(Pool_code, Pool_Name, divcode, Sf_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();


                        strQry = "UPDATE Mas_Pool_Area_Name " +
                                 " SET Pool_SName = '" + Pool_sName + "', " +
                                 " Pool_Name = '" + Pool_Name + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Pool_Id = '" + Pool_code + "' and State='" + State + "' ";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        /*-----------------------  Sorting For HQ Listt Empty  (26/05/2017) ---------------------*/
        public DataTable get_HQ_List_sort_Empty(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;


            strQry = "select Pool_Id,Pool_SName,Pool_Name from Mas_Pool_Area_Name " +
                      " where Division_Code =" + div_code + " and Active=0 and Sf_Code='admin' order by Pool_Name";

            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*-----------------------  Sorting For HqList  (26/05/2017) ---------------------*/
        public DataTable get_HQ_List_Sorting(string div_code, string Pool_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtStockist = null;

            //strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile,Territory,State " +
            //         " FROM mas_stockist " +
            //         " WHERE stockist_active_flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";

            strQry = "EXEC [dbo].[SP_Get_StockistList_Filter] '" + div_code + "', '" + Pool_Name + "' ";


            try
            {
                dtStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtStockist;
        }

        /*---------------------------------------- Search Stock Name (30/05/2016) -------------------------------------------------*/
        public DataSet Search_Alphabet_StockistName(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Mobile,SF_Code,Stockist_Designation," +
                    "case Territory when '--Select--' then '' else Territory end as Territory," +
                    "case State when '---Select---' then '' else State end as State," +
                    " stuff((select ', '+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName " +
                    " FROM mas_stockist a " +
                    " WHERE stockist_active_flag=0  " +
                     sFindQry +
                    " ORDER BY Stockist_Name,State,Territory";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*---------------------------------------- Search Deactivate Stock Name (30/05/2016) -------------------------------------------------*/

        public DataSet Search_Reactivate_Stock_Name(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State," +
                     " stuff((select ', '+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName,CONVERT(varchar(200),LastUpdt_Date,103)as DeActivateDate " +
                     " FROM mas_stockist a " +
                     " WHERE stockist_active_flag=1  " +
                      sFindQry +
                     " ORDER BY Stockist_Name,State,Territory";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Search Stockist ReActivate  (31/05/2017) ---------------------*/
        public DataSet Search_ReActivate_Stock_Name_Alpha(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name " +
                     " FROM mas_stockist a " +
                     " WHERE stockist_active_flag=1  " +
                      sFindQry +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------  Search Stockist ReActivate  Alphabet order (31/05/2017) ---------------------*/
        public DataSet Search_Alphabet__ReActivateStockistName(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Mobile,SF_Code," +
                     "case Territory when '--Select--' then '' else Territory end as Territory," +
                     "case State when '---Select---' then '' else State end as State," +
                     " stuff((select ', '+SF_Name from Mas_Salesforce b where charindex(b.Sf_Code+',',a.SF_Code)>0 for XML path('')),1,2,'') sfName,CONVERT(varchar(200),LastUpdt_Date,103)as DeActivateDate " +
                     " FROM mas_stockist a " +
                     " WHERE stockist_active_flag=1  " +
                      sFindQry +
                     " ORDER BY Stockist_Name,State,Territory";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*----------------- Bind Stockist Detail (31/05/2017) -----------------------*/
        public DataSet Get_HQ_Sample()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;

            strQry = "select '' as Pool_Id,'' as Pool_SName,'Select HQ' as Pool_Name,'Select State' as State ";

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

        /*----------------- Bind HQ Detail (31/05/2017) -----------------------*/
        public DataSet Get_HQDetail_List(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name,State " +
                     " FROM Mas_Pool_Area_Name WHERE Active=0  " + sFindQry + "" +
                     " ORDER BY State,Pool_Name";
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

        /*----------------- Search HQ Alpha (31/05/2017) -----------------------*/
        public DataSet Search_Hq_Name_Alpha(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT distinct LEFT(Pool_Name,1) val, LEFT(Pool_Name,1) Pool_Name " +
                     " FROM Mas_Pool_Area_Name a " +
                     " WHERE Active=0  " +
                      sFindQry +
                     " ORDER BY Pool_Name";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*----------------- Hq Detail (31/05/2017) -----------------------*/
        public DataSet Get_HQ_NameDetail(string divcode, string Pool_Id, string Sf_Code, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_SName,Pool_Name,State " +
                   " FROM Mas_Pool_Area_Name WHERE Pool_Id= '" + Pool_Id + "' AND Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "' " +
                   " ORDER BY Pool_Name";
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

        /*----------------------- Update HQ Name (31/05/2017) --------------------*/
        public int Stockist_HQ_List_Update(int Pool_code, string Pool_sName, string Pool_Name, string divcode, string Sf_Code, string State, string hdnPool)
        {
            int iReturn = -1;
            if (!HQ_ShortName_Exsits(Pool_code, Pool_sName, divcode, Sf_Code, State))
            {
                if (!HQ_Name_Exsist(Pool_code, Pool_Name, divcode, Sf_Code, State))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();


                        //strQry = "UPDATE Mas_Pool_Area_Name " +
                        //         " SET Pool_SName = '" + Pool_sName + "', " +
                        //         " Pool_Name = '" + Pool_Name + "'," +
                        //         " LastUpdt_Date = getdate() " +
                        //         " WHERE Pool_Id = '" + Pool_code + "' and State='" + State + "'";

                        //iReturn = db.ExecQry(strQry);

                        //strQry = "UPDATE mas_stockist " +
                        //   " SET Territory = '" + Pool_Name + "', " +
                        //   " LastUpdt_Date = getdate() " +
                        //   " WHERE stockist_active_flag=0 " +
                        //   "  AND State ='" + State + "' AND Division_Code = '" + divcode + "' and Territory='" + Pool_Name + "'";

                        strQry = "UPDATE Mas_Pool_Area_Name " +
                              " SET Pool_SName = '" + Pool_sName + "', " +
                              " Pool_Name = '" + Pool_Name + "'," +
                              " LastUpdt_Date = getdate() , State='" + State + "'" +
                              " WHERE Pool_Id = '" + Pool_code + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE mas_stockist " +
                                " SET Territory = '" + Pool_Name + "', " +
                                " LastUpdt_Date = getdate() " +
                                " WHERE stockist_active_flag=0 " +
                                " AND Division_Code = '" + divcode + "' and Territory='" + hdnPool + "'";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }

        /*---------------------Check Short HQ Name Exist (31/05/2017) --------------*/
        public bool HQ_ShortName_Exsits(int Pool_code, string Pool_SName, string divcode, string Sf_Code, string State)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_SName) FROM Mas_Pool_Area_Name WHERE Pool_SName= '" + Pool_SName + "' AND Pool_Id!='" + Pool_code + "' AND Division_Code='" + divcode + "' and Sf_Code='" + Sf_Code + "' and State='" + State + "'";

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

        /*------------------- Check HQ Name Exist (31/05/2016) -----------------*/

        public bool HQ_Name_Exsist(int Pool_code, string Pool_Name, string divcode, string Sf_Code, string State)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_Name) FROM Mas_Pool_Area_Name WHERE Pool_Name= '" + Pool_Name + "' AND Pool_Id!='" + Pool_code + "' AND Division_Code='" + divcode + "' and Sf_Code='" + Sf_Code + "' and State='" + State + "'";

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

        public DataSet getStockist_View_Statwise(string divcode, string state)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;




            strQry = " SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_ContactPerson,a.Stockist_Designation," +
                        " a.Stockist_Mobile,case a.Territory when '--Select--' then '' else a.Territory end as Territory,State,a.SF_Code " +

                         " FROM mas_stockist a where  stockist_active_flag=0 AND a.Division_Code= '" + divcode + "' and State='" + state + "' ";

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

        public int Update_Stockist_Detail_Edit(string divcode, int Stock_Code, string ERPCode, string HQName, string State)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (!RecordExistHQ_state(HQName, divcode,State))
                {

                    strQry = "SELECT isnull(max(Pool_Id)+1,'1') Pool_Id from Mas_Pool_Area_Name ";
                    int Pool_Id = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO Mas_Pool_Area_Name(Pool_Id,Division_Code,Pool_SName,Pool_Name,created_Date,LastUpdt_Date,Active,state,sf_code)" +
                             "values('" + Pool_Id + "','" + divcode + "','" + HQName + "', '" + HQName + "',getdate(),getdate(),0,'" + State + "','admin') ";

                    iReturn = db.ExecQry(strQry);

                    strQry = " UPDATE Mas_Stockist " +
                           " SET Stockist_Designation='" + ERPCode + "',Territory='" + HQName + "',State='" + State + "'" +
                           "   WHERE Stockist_Code = '" + Stock_Code + "' AND Division_Code = '" + divcode + "' ";
                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = " UPDATE Mas_Stockist " +
                        " SET Stockist_Designation='" + ERPCode + "',Territory='" + HQName + "',State='" + State + "'" +
                        "   WHERE Stockist_Code = '" + Stock_Code + "' AND Division_Code = '" + divcode + "' ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public bool RecordExistHQ(string HQName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_Name) FROM Mas_Pool_Area_Name WHERE Pool_Name= '" + HQName + "' AND Division_Code='" + divcode + "' ";

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

        /*-------------------  HQ Name DDL (03/06/2017) -----------------*/
        public DataSet Get_Territory_Detail(string territory, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name,State, " +
                 "(select count(d.Territory)  from dbo.Mas_Stockist d WHERE Division_Code= '" + div_code + "' " +
                  "  and Stockist_Active_flag=0   and d.Territory = c.Pool_Name) as Pool_Count " +
                     " FROM Mas_Pool_Area_Name c WHERE Active=0 and Sf_Code='admin' and Division_code='" + div_code + "' and Pool_Name Like '%" + territory + "%'" +
                     " ORDER BY State,Pool_Name";
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
        /*------------------- State Name DDL (03/06/2017) -----------------*/
        public DataSet Get_HQDetail_State(string State, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name,State, " +
       "(select count(d.Territory)  from dbo.Mas_Stockist d WHERE Division_Code= '" + div_code + "' " +
        "  and Stockist_Active_flag=0   and d.Territory = c.Pool_Name) as Pool_Count " +
           " FROM Mas_Pool_Area_Name c WHERE Active=0  and Sf_Code='admin' and Division_code='" + div_code + "' and state='" + State + "' " +
           " ORDER BY State,Pool_Name";
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


        /*------------------- HQ List (03/06/2017) -----------------*/
        public DataSet GetHQ_List_Det(string divcode, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Pool_Id,Pool_SName,Pool_Name,State, " +
                "(select count(d.Territory)  from dbo.Mas_Stockist d WHERE Division_Code= '" + divcode + "' " +
                  "  and Stockist_Active_flag=0   and d.Territory = c.Pool_Name) as Pool_Count " +
                     "  FROM Mas_Pool_Area_Name c WHERE Division_Code= '" + divcode + "' and Active=0 and Sf_Code='" + Sf_Code + "'" +
                     "ORDER BY State,Pool_Name";
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
        public bool RecordExistHQ_state(string HQName, string divcode, string state)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Pool_Name) FROM Mas_Pool_Area_Name WHERE Pool_Name= '" + HQName + "' AND Division_Code='" + divcode + "' and state='" + state + "' ";

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

        /*----- Get Stockist Detail Statewise (07/06/2017)----------------*/
        public DataSet Get_Statewise_Stockist_Detail(string div_code, string State)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockDet = null;
            strQry = "  SELECT  Stockist_Name,Stockist_Code,Territory,SF_Code,Stockist_Designation,  " +
 " State   FROM   Mas_Stockist where Division_Code='" + div_code + "' and Stockist_Active_Flag=0" +
 " and State='" + State + "' order by Stockist_Name";


            try
            {
                dsStockDet = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockDet;

        }

        public DataSet getStockist_View_Statwise_select(string divcode, string state)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;




            strQry = " SELECT a.stockist_code,a.Stockist_Name,a.Stockist_Address,a.Stockist_ContactPerson,a.Stockist_Designation," +
                        " a.Stockist_Mobile,case a.Territory when '--Select--' then '' else a.Territory end as Territory,State,a.SF_Code " +

                         " FROM mas_stockist a where  stockist_active_flag=0 AND a.Division_Code= '" + divcode + "' and State in (" + state + ") ";

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

        public DataSet Get_StateWise_HQ_AllState(string StateName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Exec Get_All_state_HQ '" + StateName + "', '" + div_code + "'";

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


        public DataSet GetHQ_StockistView(string div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Pool_Id, '--All--' as Pool_Name " +
                      " UNION " +
                      " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0 and Sf_Code='" + Sf_Code + "'  ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_Stockist_Code_ERP(string div_code, string ERP_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Stockist_Code from Mas_Stockist where Division_Code='" + div_code + "' and Stockist_Designation='" + ERP_Code + "' and Stockist_Active_Flag=0 ";

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

        public DataSet Get_Product_Code_ERP(string div_code, string Product_ERP)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Detail_Code from mas_product_detail where Division_Code='" + div_code + "' and Sale_Erp_Code = '" + Product_ERP + "' and Product_Active_Flag=0 ";
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
        public DataSet getPrimary_Exist(string div_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select SS_Head_Sl_No from Trans_SS_Entry_Head where  Division_Code = '" + div_code + "' and MONTH = '" + imonth + "' and YEAR = '" + iyear + "' ";

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

        public DataSet Primary_Bill_Exist(string div_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Primary_Bill where  Division_Code = '" + div_code + "' and Invoice_Month = '" + imonth + "' and Invoice_Year = '" + iyear + "' ";

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
        public int GetPrimary_Bill()
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(SL_No)+1,'1') SL_No from Primary_Bill";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*------------- Get Stockist MR Contribution (12/01/2018) --------------*/
        public DataSet GetStock_MR_Contribution(string divcode, string stockist_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = " select Sf_HQ_Cont from  [dbo].[Map_Stk_MR_Contribution] where Stockist_Code='" + stockist_code + "'" +
           "  and Division_Code='" + divcode + "' and Sf_Code='" + Sf_Code + "' and Active_Mr='0' ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        /*--------------------Get Stockist View Detail with Contribution (12/01/2018)-------------------------*/
        public DataSet GetStockist_HQwise_Contribution(string divcode, string SearchText, string ModeType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "Exec SP_Get_Stockist_ContributionView '" + divcode + "','" + SearchText + "','" + ModeType + "'";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        /*-----------------------------  Stockist View (24_07_2018) -----------------*/

        public DataSet Bind_Stockist_View(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select Row_Number() over(order by S.State) slno,C.Stockist_Code,C.Stockist_Name,C.Stockist_Designation as ERP_Code, " +
            " S.State,C.HQ_Name,b.Sf_Name,b.Sf_HQ ,C.SF_HQ_Code,C.SF_HQ_Cont " +
            " from [dbo].[Mas_Stockist] S inner join [dbo].[Map_Stk_MR_Contribution] C    " +
            " on S.Stockist_Code=C.Stockist_Code inner join Mas_Salesforce b " +
            " on C.Sf_Code=b.Sf_Code " +
            " where C.Division_Code='" + divcode + "'  " +
            " and C.Active_Mr=0 and S.stockist_active_flag=0 " +
            " order by State,HQ_Name,Stockist_Name,Sf_Name";

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

        public DataSet Sort_Stockist_View(string sFindQry, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "   select Row_Number() over(order by S.State) slno,C.Stockist_Code,C.Stockist_Name,C.Stockist_Designation as ERP_Code, " +
          " S.State,C.HQ_Name,b.Sf_Name,b.Sf_HQ ,C.SF_HQ_Code,C.SF_HQ_Cont " +
          " from [dbo].[Mas_Stockist] S inner join [dbo].[Map_Stk_MR_Contribution] C  " +
          " on S.Stockist_Code=C.Stockist_Code inner join Mas_Salesforce b " +
          " on C.Sf_Code=b.Sf_Code " +
         "  where C.Active_Mr=0 and S.stockist_active_flag=0  and S.Division_Code='" + Div_Code + "'" +
          sFindQry +
         "  order by State,HQ_Name,S.Stockist_Name,Sf_Name";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        public DataSet Sort_StockistView_Name_Alpha(string sFindQry, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "SELECT distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name " +
                     " FROM mas_stockist S " +
                     " WHERE stockist_active_flag=0  and Division_Code='" + Div_Code + "'" +
                      sFindQry +
                     " ORDER BY 2";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        public DataSet Sort_Alphabet_StockistView(string sFindQry, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "   select Row_Number() over(order by S.State) slno,C.Stockist_Code,C.Stockist_Name,C.Stockist_Designation as ERP_Code, " +
          " S.State,C.HQ_Name,b.Sf_Name,b.Sf_HQ ,C.SF_HQ_Code,C.SF_HQ_Cont " +
          " from [dbo].[Mas_Stockist] S inner join [dbo].[Map_Stk_MR_Contribution] C  " +
          " on S.Stockist_Code=C.Stockist_Code inner join Mas_Salesforce b " +
          " on C.Sf_Code=b.Sf_Code " +
         "  where C.Active_Mr=0 and S.stockist_active_flag=0  and S.Division_Code='" + Div_Code + "'" +
          sFindQry +
         "  order by State,HQ_Name,S.Stockist_Name,Sf_Name";
            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }

        public DataSet Sort_Stockist_view_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' stockist_name " +
                     " union " +
                     " select distinct LEFT(stockist_name,1) val, LEFT(stockist_name,1) stockist_name" +
                     " FROM mas_stockist " +
                     " WHERE stockist_active_flag=0 " +
                     " AND Division_Code = '" + divcode + "' " +
                     " ORDER BY 1";
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
        public string RecordHeadAdd_Primary_Kpi(string Stockist_ERP_Code, string stockist_code, string Stockist_Name, string division_code, string transMonth, string transYear,string Zone, string HQ)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            int Trans_sl_No = -1;
            strQry = "SELECT isnull(Count(Trans_sl_No)+1,'1') Trans_sl_No  FROM Trans_Primary_Head";
            Trans_sl_No = db.Exec_Scalar(strQry);

            try
            {

                strQry = " INSERT INTO [Trans_Primary_Head]([Trans_sl_No],[Stockist_ERP_Code] ,[Stockist_Code],[Stockist_Name],[Division_Code] ,[Trans_Month] ,[Trans_Year] ,[Uploaded_Date],Zone,HQ) " +
                       " VALUES ( " + Trans_sl_No + ",'" + Stockist_ERP_Code + "','" + stockist_code + "','" + Stockist_Name + "' , '" + division_code + "', '" + transMonth + "', '" + transYear + "', getdate(),'"+Zone+"','"+HQ+"') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (iReturn > 0)
            {
                strQry = "SELECT MAX(Trans_sl_No) FROM Trans_Primary_Head";
                iReturn = db.Exec_Scalar(strQry);
            }
            return iReturn.ToString();


        }
        public string RecordDetailsAddPrimary_Kpi(string TransSlNo, string Product_ERP_Code, string Product_code, string Product_Name, string Pack, string Qty, string Rate, string amount, string division_code, string Stock_Code, string Stock_Name, string free, string ret_Qty, string ret_val, string break_qty,string break_val, string Voucher, string others, string depo_code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                int Trans_Detail_No = -1;
                strQry = "SELECT isnull(Count(Trans_Detail_No)+1,'1') Trans_Detail_No  FROM Trans_Primary_Detail";
                Trans_Detail_No = db.Exec_Scalar(strQry);

                //strQry = " INSERT INTO [Trans_Primary_Detail]([Trans_Detail_No],[Trans_sl_No],[Product_ERP_Code],[Product_Code],[Product_Name],[Pack],[Sale_Qty],[Rate],[Amount],[Division_code],[Stockist_Code],[Stockist_Name],[Created_Date]) " +
                //       " VALUES ( '" + Trans_Detail_No + "','" + TransSlNo + "' , '" + Product_ERP_Code + "', '" + Product_code + "','" + Product_Name + "','" + Pack + "','" + Qty + "','" + Rate + "','" + amount + "','" + division_code + "', '" + Stock_Code + "','" + Stock_Name + "',getdate()) ";

                strQry = " INSERT INTO [Trans_Primary_Detail]([Trans_Detail_No],[Trans_sl_No],[Product_ERP_Code],[Product_Code],[Product_Name],[Pack],[Sale_Qty],[Rate],[Amount],[Division_code],[Stockist_Code],[Stockist_Name],Free_Qty,Return_Qty,Return_Value,Breakage_Qty,Breakage_Val,Voucher_type,Others,Depot_Code) " +
                       " VALUES ( '" + Trans_Detail_No + "','" + TransSlNo + "' , '" + Product_ERP_Code + "', '" + Product_code + "','" + Product_Name + "','" + Pack + "','" + Qty + "','" + Rate + "','" + amount + "','" + division_code + "', '" + Stock_Code + "','" + Stock_Name + "','" + free + "','" + ret_Qty + "','" + ret_val + "','" + break_qty + "','"+break_val+"','"+Voucher+"','" + others + "','" + depo_code + "') ";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public DataSet Get_TransitDt_Prev(string stk_code, string imonth, string iyear, string div_code, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct Transit_Bill_No_Date from Trans_Secondary_Entry_BillDetails " +
                     " where Trans_Month ='" + imonth + "' and Trans_Year='" + iyear + "' and Stockist_Code='" + stk_code + "' and  Sub_div ='" + sub_div + "'";


            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_Primary_bill_Sale(string stk_code, string imonth, string iyear, string div_code, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct convert(varchar(10),Inv_Date,103) Inv_Date,sum(Sale_Value) Svalue from primary_bill " +
                     " where Invoice_Month ='" + imonth + "' and Invoice_Year='" + iyear + "' and Division_code='" + div_code + "' and Stockist_Code='" + stk_code + "' and mode=1 " +
                     " group by Inv_Date";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_Primary_bill_Ret(string stk_code, string imonth, string iyear, string div_code, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct convert(varchar(10),Inv_Date,103) Inv_Date,sum(Return_Value) Rvalue from primary_bill " +
                   " where Invoice_Month ='" + imonth + "' and Invoice_Year='" + iyear + "' and Division_code='" + div_code + "' and Stockist_Code='" + stk_code + "'  and mode=2 " +
                   " group by Inv_Date";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet GetHQ_Stockist(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0   ";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet GetHQ_Stockist_List(string divcode, string HQName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;

            strQry = "SELECT Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile" +
           
                    "   FROM mas_stockist  WHERE stockist_active_flag=0 AND Division_Code='" + divcode + "' and Territory='" + HQName + "' ORDER BY 2";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_Primary_bill_Sale_Tab(string stk_code, string imonth, string iyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct convert(varchar(10),Inv_Date,103) Inv_Date,Inv_No,sum(Sale_Value) Svalue into #prim from primary_bill " +
                     " where Invoice_Month ='" + imonth + "' and Invoice_Year='" + iyear + "' and Division_code='" + div_code + "' and Stockist_Code='" + stk_code + "' " +
                     " group by Inv_Date,Inv_No" +
                     "  select Inv_Date,Inv_No,Svalue from  #prim where Svalue !='0' " +
                     " drop table #prim";


            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_Primary_bill_Ret_P(string stk_code, string imonth, string iyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct convert(varchar(10),Inv_Date,103) Inv_Date,sum(Return_Value) Rvalue from primary_bill " +
                   " where Invoice_Month ='" + imonth + "' and Invoice_Year='" + iyear + "' and Division_code='" + div_code + "' and Stockist_Code='" + stk_code + "'  and mode=2 " +
                   " group by Inv_Date";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_TransitDt_Prev_P(string stk_code, string imonth, string iyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;


            strQry = " select distinct Transit_Bill_No_Date,Transit_bill_Dt,Transit_bill_val from Trans_Secondary_Entry_BillDetails " +
                     " where Trans_Month ='" + imonth + "' and Trans_Year='" + iyear + "' and Stockist_Code='" + stk_code + "' ";


            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_st_HQ(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Pool_Id,Pool_Name from Mas_Pool_Area_Name where Division_Code = '" + div_code + "' and Active=0  order by Pool_Name";

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

    }
}