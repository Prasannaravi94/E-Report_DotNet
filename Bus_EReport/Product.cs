using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Product
    {
        private string strQry = string.Empty;

        public DataSet getProd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three, Product_Code_SlNo " +
                     " FROM  Mas_Product_Detail " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getProd(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three, " +
                     "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " AND LEFT(Product_Detail_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";

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
        public DataSet getProd_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select '1' val,'All' Product_Detail_Name " +
                     " union " +
                     " select distinct LEFT(Product_Detail_Name,1) val, LEFT(Product_Detail_Name,1) Product_Detail_Name" +
                     " FROM mas_Product_Detail " +
                     // " WHERE SF_Status=0 " +
                     // " AND lower(sf_code) != 'admin' " +
                     " WHERE Division_Code = '" + divcode + "' " +
                     " AND Product_Active_flag = 0 " +
                     " ORDER BY 1";
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
        public DataSet getEmptyProd()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 10 '' code,'' name,'' descr,'' sale_unit, '' sample_unit1, '' sample_unit2, '' sample_unit3 " +
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
        // Sorting For ProductList
        public DataTable getProductlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three, " +
                      "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +

                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataTable getProductlist_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three, " +
                      "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a " +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     "AND LEFT(Product_Detail_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }



        ///*-------------------------- Process Quiz User List(29/01/2018) -------------------------------------*/
        //public DataSet Process_UserList(string div_Code, string Sf_Code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsUserList = null;
        //    strQry = " EXEC [dbo].[sp_UserList_Process_Quiz] '" + div_Code + "', '" + Sf_Code + "'";

        //    try
        //    {
        //        dsUserList = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsUserList;
        //}




        public DataSet getProdall_sfcode(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c, Mas_Product_State_Rates d, Mas_Salesforce e" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Active_Flag=0 AND a.Product_Detail_Code = d.Product_Detail_Code AND " +
                     " d.State_Code = e.State_Code AND a.Division_Code= '" + divcode + "'  AND " +
                     " e.Sf_Code = '" + sf_code + "' " +
                     " ORDER BY 2";
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





        public DataSet getEffDate(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                       " ORDER BY 1 DESC";

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

        public DataSet getEffDate_deact(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'All' Effective_From_To UNION SELECT DISTINCT (convert(varchar(12),gift_Effective_from,103)+' To '+convert(varchar(12),gift_effective_to,103)) Effective_From_To FROM mas_Gift " +
                       " WHERE gift_active_flag=0 AND division_code=  '" + divcode + "' " +
                       " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                       " order by 1 desc";
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

        // Changes done by Saravanan 05/08/2014
        public DataSet getGift(string divcode, string giftcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "  SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,Gift_Effective_From,Gift_Effective_To,State_Code" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Code = '"+ giftcode + "' " +
            //            " ORDER BY 2";

            strQry = " SELECT Gift_SName,Gift_Name,Gift_Value,Gift_Type,convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From," +
                     " convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,State_Code,subdivision_code" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     " '" + divcode + "' AND Gift_Code = '" + giftcode + "' ORDER BY 2";
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


        public DataSet getProductRate(string state_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC Product_stateWise_Rate_View '" + state_code + "', '" + div_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getProductRate_all(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            //strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(rtrim(MRP_Price),0) MRP_Price, " +     
            //         " isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +     
            //          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price  From Mas_Product_Detail p left outer " +       
            //          " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +       
            //           " Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
            //            " where  Division_Code = '" + div_code + "') where p.Product_Active_Flag=0 and p.Division_Code = '" + div_code + "' ";

            //strQry = " select distinct p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(rtrim(MRP_Price),0) MRP_Price,  " +
            //          " isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price  From Mas_Product_Detail p left outer " +
            //          " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code    " +
            //          " where Product_Active_Flag=0 and p.Division_code='" + div_code + "' and (p.state_code like '" + state_code + ',' + "%'  or p.state_code like '%" + ',' + state_code + ',' + "%' or p.state_code like '%" + ',' + state_code + "' or p.state_code = '"+state_code+"') ";
            strQry = " select p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(rtrim(MRP_Price),0) MRP_Price,  " +
          " isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price,isnull(rtrim(Sample_Price),0) Sample_Price  From Mas_Product_Detail p left outer " +
          " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code    " +
          " where Product_Active_Flag=0 and p.Division_code='" + div_code + "'   and  r.state_code=  '" + state_code + "' ORDER BY Prod_Detail_Sl_No ";
            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet getProdRate(string state_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(MRP_Price,0) MRP_Price, " +
                     " isnull(Retailor_Price,0) Retailor_Price,isnull(Distributor_Price,0) Distributor_Price, " +
                     " isnull(NSR_Price,0) NSR_Price,isnull(Target_Price,0) Target_Price " +
                     " From Mas_Product_Detail p left outer join Mas_Product_State_Rates R " +
                     " on R.product_Detail_code=p.Product_Detail_code and " +
                     " Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                     " where state_code='" + state_code + "' and division_code= '" + div_code + "') " +
                     " where Product_Active_Flag=0 and p.Division_code= '" + div_code + "' and p.state_code  like '%" + state_code + "%'" +
                     " ORDER BY Prod_Detail_Sl_No";
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
        // Sorting For ProductRate 
        public DataTable getProductRatelist_DataTable(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,isnull(MRP_Price,0) MRP_Price, " +
                     " isnull(Retailor_Price,0) Retailor_Price,isnull(Distributor_Price,0) Distributor_Price, " +
                     " isnull(NSR_Price,0) NSR_Price,isnull(Target_Price,0) Target_Price,isnull(Sample_Price,0) Sample_Price " +
                     " From Mas_Product_Detail p left outer join Mas_Product_State_Rates R " +
                     " on R.product_Detail_code=p.Product_Detail_code and " +
                     " Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                     " where division_code= '" + div_code + "') " +
                     " where Product_Active_Flag=0 and p.Division_code= '" + div_code + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getProCate(string divcode, string ProdCatcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_SName,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Code= '" + ProdCatcode + "'AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet getProCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                     " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdCat_SNo";
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
        // Sorting For ProductCategoryList 
        public DataTable getProductCategorylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            //strQry = " SELECT Product_Cat_Code,Product_Cat_SName,Product_Cat_Name FROM  Mas_Product_Category " +
            //         " WHERE Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";

            strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name, " +
                   " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                   " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                   " ORDER BY c.ProdCat_SNo";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //..test
        public DataSet getProductCategory(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Cat_Code, '---Select---' as Product_Cat_Name " +
                     " UNION " +
                     " SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet FillProductGroup(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Grp_Code, '---Select---' as Product_Grp_Name " +
                     " UNION " +
                     " SELECT Product_Grp_Code,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";

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




        public DataSet getProductForCategory(string divcode, string cat_code, string nil_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT Product_Detail_Code, Product_Cat_Code, Product_Detail_Name " +
            //         " FROM  Mas_Product_Detail " +
            //         " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " AND Product_Cat_Code IN ('" + cat_code + "','" + nil_code + "') " +
            //         " ORDER BY 2";

            strQry = "SELECT a.Product_Detail_Code, a.Product_Cat_Code, a.Product_Detail_Name + '-' + b.Product_Cat_Name as Product_Detail_Name " +
                     " FROM  Mas_Product_Detail a, Mas_Product_Category b " +
                     " WHERE a.Product_Cat_Code = b.Product_Cat_Code AND a.Product_Active_Flag=0  " +
                     " AND a.Product_Cat_Code IN ('" + cat_code + "','" + nil_code + "') " +
                     " AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 3";

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

        public DataSet getProGroup(string divcode, string prodgrpcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Code=  '" + prodgrpcode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        // Sorting For ProductGroupList 
        public DataTable getProductGrouplist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            //strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
            //         " WHERE  Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT c.Product_Grp_Code,c.Product_Grp_SName,c.Product_Grp_Name, " +
                 " (select COUNT(p.Product_Grp_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Grp_Code = c.Product_Grp_Code ) as Grp_count,"+
                  "   (select COUNT(d.Product_Grp_Code) from Product_Image_List d where Flag=0 and ',' + d.Product_Grp_Code + ',' LIKE '%,' + CAST(c.Product_Grp_Code as VARCHAR(2000)) + ',%')   as slide_count " +
                 " FROM  Mas_Product_Group c" +
                 " WHERE c.Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                 " ORDER BY c.ProdGrp_Sl_No";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //...
        public DataSet getProductGroup(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Grp_Code, '---Select---' as Product_Grp_Name " +
                     " UNION " +
                     " SELECT Product_Grp_Code,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet getProductRate_sf(string sf_code, string div_code, string state, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name,b.Product_Sale_Unit, " +
            //         " c.MRP_Price,c.Retailor_Price,c.Distributor_Price,c.Target_Price " +
            //         " from Mas_Salesforce a, Mas_Product_Detail b, Mas_Product_State_Rates c " +
            //         " where a.Sf_Code = '" + sf_code + "' and b.Division_Code= '" + div_code + "'  " +
            //         " and b.Product_Active_Flag=0 and b.Product_Detail_Code = c.Product_Detail_Code " +
            //         " and b.Division_Code = c.Division_Code and a.State_Code = c.State_Code " +
            //         " ORDER BY 2";
            //strQry =   " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
            //          " b.Product_Sale_Unit,  isnull(rtrim(MRP_Price),0) MRP_Price,  isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
            //          " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price  From Mas_Product_Detail b" +
            //          " left outer join Mas_Product_State_Rates c  on b.Product_Detail_Code = c.Product_Detail_Code  " +
            //      //    " and  Sl_No  in (select max(Max_State_Sl_No) from mas_Product_State_Rates) " +
            //          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
            //          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%') " ;
            //strQry = " select * " +
            //            " from Mas_Product_State_Rates where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "') ";


            strQry = " select * from Mas_Product_Detail where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "' or state_code like '" + state + "') ";

            dsProCat = db_ER.Exec_DataSet(strQry);

            if (dsProCat.Tables[0].Rows.Count > 0)
            {
                if (subdiv.Contains(','))
                    subdiv = subdiv.Substring(0, subdiv.Length - 1);
                strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                    " b.Product_Description, " +
                    " b.Product_Detail_Name," +
                    " b.Product_Sale_Unit,  " +
                    " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                    " isnull(rtrim(Retailor_Price),0) Retailor_Price, " +
                    " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                    " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                    " isnull(rtrim(Target_Price),0) Target_Price, " +
                    " isnull(rtrim(Sample_Price),0) Sample_Price " +
                    " From Mas_Product_Detail b" +
                    " INNER JOIN Mas_Product_State_Rates c  " +
                    " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                    " WHERE b.Division_Code= '" + div_code + "' " +
                    " AND b.Product_Active_Flag = 0" +
                    " AND c.state_code = '" + state + "' " +
                    " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%' or b.state_code like '%" + ',' + state + "') " +
                     " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "')";
            }

            else
            {
                strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
                          " b.Product_Sale_Unit,  '0' as MRP_Price,  '0'as Retailor_Price,'0' as Distributor_Price, " +
                          " '0' as NSR_Price,'0' as Target_Price,'0' as Sample_Price  From Mas_Product_Detail b" +
                          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
                          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%'  or b.state_code like '%" + ',' + state + "') " +
                            " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "')";

            }

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

        public DataTable getProductRate_DataTable(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name,b.Product_Sale_Unit, " +
                     " c.MRP_Price,c.Retailor_Price,c.Distributor_Price,c.Target_Price,c.Sample_Price " +
                     " from Mas_Salesforce a, Mas_Product_Detail b, Mas_Product_State_Rates c " +
                     " where a.Sf_Code = '" + sf_code + "' and b.Division_Code = '" + div_code + "' " +
                     " and b.Product_Active_Flag=0 and b.Product_Detail_Code = c.Product_Detail_Code " +
                     " and b.Division_Code = c.Division_Code and a.State_Code = c.State_Code " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public bool RecordExist(string Product_Cat_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName='" + Product_Cat_SName + "' and Division_Code = '" + div_code + "' ";
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
        //changes done by Priya
        //public bool sRecordExist(string Product_Cat_Name, string div_code)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name='" + Product_Cat_Name + "' and Division_Code = '" + div_code + "' ";
        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}
        public bool sRecordExist(int Product_Cat_Code, string Product_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' and Division_Code = '" + divcode + "'";

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
        public bool sRecordExist(int Product_Cat_Code, string Product_Cat_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' ";

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


        //end

        //public bool RecordExist(int Product_Cat_Code, string Product_Cat_SName)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName = '" + Product_Cat_SName + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' ";

        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}
        public bool RecordExist(int Product_Cat_Code, string Product_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Product_Category WHERE Product_Cat_SName = '" + Product_Cat_SName + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' AND Division_Code= '" + divcode + "' ";

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

        public bool RecordExistdet(string Product_Detail_Code, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Code) FROM Mas_Product_Detail WHERE Product_Detail_Code='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "' ";
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
        public bool sRecordExistdet(string Product_Detail_Name, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_Product_Detail WHERE Product_Detail_Name='" + Product_Detail_Name + "' AND Division_Code= '" + divcode + "' ";
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


        //Record Exist

        public bool RecordExistGiftSN(string GiftSName, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_SName) FROM Mas_Gift WHERE Gift_SName='" + GiftSName + "' and Division_code=" + div_code + " and Gift_Active_Flag=0";
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
        public bool RecordExistGiftN(string GiftName, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE Gift_Name='" + GiftName + "' and Division_code=" + div_code + " and Gift_Active_Flag=0";
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

        public bool snRecordExist(string Gift_Code, string Gift_SName, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_SName='" + Gift_SName + "' and Division_Code=" + div_code + "  and Gift_Active_Flag=0";

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
        public bool nRecordExist(string Gift_Code, string Gift_Name, int div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Code) FROM mas_Gift WHERE Gift_Code != '" + Gift_Code + "' AND Gift_Name='" + Gift_Name + "' and  Division_Code=" + div_code + "  and Gift_Active_Flag=0 ";

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
        //end
        // Insert Product Reminder 


        public int RecordAddGift(string GiftSName, string GiftName, int GiftType, string Giftvalue, DateTime EffFrom, DateTime EffTo, int Division_Code, string State_Code, string subdivision_code)
        {
            int iReturn = -1;
            //    if (!RecordExist(GiftName,GiftType,EffFrom,EffTo,Division_Code, state))
            //   {

            if (!RecordExistGiftSN(GiftSName, Division_Code))
            {
                if (!RecordExistGiftN(GiftName, Division_Code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Gift_Code)+1,'1') Gift_Code from Mas_Gift ";
                        int Gift_Code = db.Exec_Scalar(strQry);

                        string EffFromdate = EffFrom.Month.ToString() + "-" + EffFrom.Day + "-" + EffFrom.Year;
                        string EffTodate = EffTo.Month.ToString() + "-" + EffTo.Day + "-" + EffTo.Year;

                        strQry = "INSERT INTO Mas_Gift(Gift_Code,Gift_SName,Gift_Name,Gift_Type,Gift_Value,Gift_Effective_From,Gift_Effective_To,Division_Code,State_Code,subdivision_code, Created_Date,Gift_Active_flag,LastUpdt_Date)" +
                                 "values('" + Gift_Code + "','" + GiftSName + "','" + GiftName + "', '" + GiftType + "' , '" + Giftvalue + "' , " +
                                 " '" + EffFromdate + "' ,'" + EffTodate + "', " + Division_Code + "," +
                                 " '" + State_Code + "','" + subdivision_code + "', getdate(), '0',getdate()) ";

                        // ",getdate(),getdate()," + Division_Code + ", getdate(), '0')";


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





        public bool RecordExist(string GiftName, int GiftType, DateTime EffFrom, DateTime EffTo, int divcode, string state)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE " +
                            " Gift_Name = '" + GiftName + "' AND Gift_Type =" + GiftType + "  " +
                            " AND Gift_Effective_From ='" + EffFrom + "' " +
                            " AND Gift_Effective_To = '" + EffTo + "' AND Division_Code =" + divcode + " AND state_code = '" + state + "' ";

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
        public bool RecordExistgift(string GiftName, int GiftType, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Gift_Name) FROM Mas_Gift WHERE " +
                            " Gift_Name = '" + GiftName + "' AND Gift_Type =" + GiftType + " " +
                            " AND Division_Code =" + divcode + " ";

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


        public int RecordAdd(string divcode, string Product_Cat_SName, string Product_Cat_Name)
        {
            int iReturn = -1;
            if (!RecordExist(Product_Cat_SName, divcode))
            {
                if (!sRecordExist(Product_Cat_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Cat_Code)+1,'1') Product_Cat_Code from Mas_Product_Category ";
                        int Product_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Category(Product_Cat_Code,Division_Code,Product_Cat_SName,Product_Cat_Name,Product_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Product_Cat_Code + "','" + divcode + "','" + Product_Cat_SName + "', '" + Product_Cat_Name + "',0,getdate(),getdate())";


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
        public bool sRecordExist(string Product_Cat_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Product_Category WHERE Product_Cat_Name='" + Product_Cat_Name + "' and Division_Code = '" + div_code + "' ";
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
        //public int RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name)
        //{
        //    int iReturn = -1;
        //    if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
        //    {
        //        if (!sRecordExist(Product_Cat_Code, Product_Cat_Name))
        //        {
        //        try
        //        {

        //            DB_EReporting db = new DB_EReporting();

        //            strQry = "UPDATE Mas_Product_Category " +
        //                     " SET Product_Cat_SName = '" + Product_Cat_SName + "', " +
        //                     " Product_Cat_Name = '" + Product_Cat_Name + "' ," +
        //                     " LastUpdt_Date = getdate() " +
        //                     " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //     }
        //        else
        //        {
        //            iReturn = -2;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -3;
        //    }
        //    return iReturn;
        //}
        //....re
        public int RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExist(Product_Cat_Code, Product_Cat_SName, divcode))
            {
                if (!sRecordExist(Product_Cat_Code, Product_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Category " +
                                 " SET Product_Cat_SName = '" + Product_Cat_SName + "', " +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' and Product_Cat_Active_Flag = 0 ";

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
        public int RecordUpdateGift(string GiftCode, string GiftSName, string GiftName, int GiftType, string GiftVal, DateTime efffrom, DateTime effto, int divcode, string State_Code, string subdivision_code)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType, divcode))
            // {
            if (!snRecordExist(GiftCode, GiftSName, divcode))
            {
                if (!nRecordExist(GiftCode, GiftName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Value = '" + GiftVal + "', " +
                                 " Gift_Type = " + GiftType + " ," +
                                 //" StateCode  = " +state +"," +
                                 " Gift_Effective_From = '" + efffrom.Month + '-' + efffrom.Day + '-' + efffrom.Year + "'," +
                                 " Gift_Effective_To = '" + effto.Month + '-' + effto.Day + '-' + effto.Year + "' , " +
                                 " State_Code = '" + State_Code + "', subdivision_code='" + subdivision_code + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "'  AND  Division_Code=" + divcode + " ";

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
        public int RecordUpdateGift(string GiftCode, string GiftName, string GiftSName, string GiftVal, int GiftType, int divcode)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType,divcode))
            //  {
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                         " SET Gift_Name = '" + GiftName + "', " +
                         " Gift_SName = '" + GiftSName + "', " +
                         " Gift_Value = '" + GiftVal + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Gift_Code = '" + GiftCode + "' AND Gift_Type = '" + GiftType + "' AND  Division_Code= " + divcode + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // }
            //   else
            //  {
            // iReturn = -2;
            //  }
            return iReturn;

        }

        public int RecordUpdateProd(string Product_Detail_Code, string Product_Detail_Name, string ProdSaleUnit, string ProdDescr, string divcode)
        {
            int iReturn = -1;
            if (!sRecordExistDetail(Product_Detail_Name, Product_Detail_Code, divcode))

            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Product_Detail " +
                             " SET Product_Detail_Name = '" + Product_Detail_Name + "', " +
                             " Product_Sale_Unit = '" + ProdSaleUnit + "' ," +
                             " Product_Description ='" + ProdDescr + "'," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Product_Detail_Code = '" + Product_Detail_Code + "' ";

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

        public int getMaxStateSlNo(string state_code, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Max_State_Sl_No),0)+1 FROM mas_Product_State_Rates WHERE state_code = '" + state_code + "' AND Division_Code='" + div_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int UpdateProductRate(string prod_code, string state_code, string effective_from, decimal mrp_amt, decimal ret_amt, decimal dist_amt, decimal nsr_amt, decimal target_amt, string div_code, int iStateSlNo, decimal sample_amt)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date,Sample_Price) VALUES " +
                         " ( '" + iSlNo + "', '" + iStateSlNo + "', '" + state_code + "', '" + prod_code + "', '" + mrp_amt + "', '" + ret_amt + "', '" + dist_amt + "', " +
                         " '" + target_amt + "', '" + nsr_amt + "', '" + effective_from.Substring(6, 4) + "-" + effective_from.Substring(3, 2) + "-" + effective_from.Substring(0, 2) + "', '" + div_code + "', getdate(),getdate(),'" + sample_amt + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int RecordUpdate_NilCode(string Product_Detail_Code, string Nil_Code, string div_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Cat_Code = '" + Nil_Code + "' ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + Product_Detail_Code + "' " +
                            " AND Division_Code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }


        public int UpdateProdSno(string ProdCode, string ProdSno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                         " SET Prod_Detail_Sl_No = '" + ProdSno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Detail_Code = '" + ProdCode + "' ";

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

        public int DeActivate(String ProdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int DeActivateGift(String GiftCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Gift " +
                            " SET Gift_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Gift_Code = '" + GiftCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Brd_RecordDelete(int ProBrdCode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Brand WHERE Product_Brd_Code = '" + ProBrdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int BulkEdit(string str, string Product_Code, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //strQry = "UPDATE Mas_Product_Detail " +
                //         " SET Product_Detail_Name = '" + ProductName + "', " +
                //         " Product_Sale_Unit = '" + Product_Sale_Unit + "', " +
                //           " Product_Sample_Unit_One = '" + Product_Sample_Unit_one + "', " +
                //             " Product_Sample_Unit_Two = '" + Product_Sample_Unit_Two + "', " +
                //               " Product_Sample_Unit_Three = '" + Product_Sample_Unit_Three + "', " +
                //                 " Product_Cat_Code = " + ProductCatCode + " ," +                               
                //             " Product_Type_Code = '" + Product_Type_Code + "', " +
                //             " Product_Description = '" + ProductDescr + "'," +
                //             " LastUpdt_Date = getdate() , " +
                //             " State_Code = '" + strstate + "',"+
                //             " subdivision_code = '" + strSubState + "' " +
                //         " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                strQry = "UPDATE Mas_Product_Detail SET " + str + "  Where Product_Detail_Code='" + Product_Code + "' and Division_code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int DeActivate(int Product_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                            " SET Product_Cat_Active_Flag=1 ," +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getProGrp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
            //         " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY ProdGrp_Sl_No";

            strQry = " SELECT c.Product_Grp_Code,c.Product_Grp_SName,c.Product_Grp_Name, " +
                     " (select COUNT(p.Product_Grp_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and  p.Product_Grp_Code = c.Product_Grp_Code ) as Grp_count, "+
                      " (select COUNT(d.Product_Grp_Code) from Product_Image_List d where Flag=0 and ',' + d.Product_Grp_Code + ',' LIKE '%,' + CAST(c.Product_Grp_Code as VARCHAR(2000)) + ',%')   as slide_count " +
                     " FROM  Mas_Product_Group c" +
                     " WHERE c.Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdGrp_Sl_No";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }

        public bool RecordExistGrp(string Product_Grp_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName='" + Product_Grp_SName + "' AND Division_Code= '" + divcode + "' ";
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
        public bool RecordExistGrp(int Product_Grp_Code, string Product_Grp_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName = '" + Product_Grp_SName + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' AND Division_Code= '" + divcode + "'";

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
        public bool sRecordExistGrp(string Product_Grp_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name='" + Product_Grp_Name + "' AND Division_Code= '" + divcode + "' ";
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
        public bool sRecordExistGrp(int Product_Grp_Code, string Product_Grp_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name = '" + Product_Grp_Name + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' AND Division_Code= '" + divcode + "' ";

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
        public int getNilCode(string div_code)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT Product_Cat_Code FROM Mas_Product_Category WHERE Division_Code='" + div_code + "' and Product_Cat_Active_Flag=2 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public bool RecordExistGrp(int Product_Grp_Code, string Product_Grp_SName)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_SName) FROM Mas_Product_Group WHERE Product_Grp_SName = '" + Product_Grp_SName + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' ";

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
        public bool sRecordExistGrp(int Product_Grp_Code, string Product_Grp_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Grp_Name) FROM Mas_Product_Group WHERE Product_Grp_Name = '" + Product_Grp_Name + "' AND Product_Grp_Code!='" + Product_Grp_Code + "' ";

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

        public int RecordAddGrp(string divcode, string Product_Grp_SName, string Product_Grp_Name)
        {
            int iReturn = -1;
            if (!RecordExistGrp(Product_Grp_SName, divcode))
            {
                if (!sRecordExistGrp(Product_Grp_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Product_Grp_Code)+1,'1') Product_Grp_Code from Mas_Product_Group ";
                        int Product_Grp_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Group(Product_Grp_Code,Division_Code,Product_Grp_SName,Product_Grp_Name,Product_Grp_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Product_Grp_Code + "','" + divcode + "','" + Product_Grp_SName + "', '" + Product_Grp_Name + "',0,getdate(),getdate())";


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
        //public int RecordUpdateGrp(int Product_Grp_Code, string Product_Grp_SName, string Product_Grp_Name)
        //{
        //    int iReturn = -1;
        //   if (!RecordExistGrp(Product_Grp_Code,Product_Grp_SName))
        //    {
        //        if (!sRecordExistGrp(Product_Grp_Code,Product_Grp_Name))
        //        {
        //        try
        //        {

        //            DB_EReporting db = new DB_EReporting();

        //            strQry = "UPDATE Mas_Product_Group " +
        //                     " SET Product_Grp_SName = '" + Product_Grp_SName + "', " +
        //                     " Product_Grp_Name = '" + Product_Grp_Name + "', " +
        //                     " LastUpdt_Date = getdate() " +
        //                     " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //        else
        //        {
        //            iReturn = -2;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -3;
        //    }
        //    return iReturn;
        //}

        public int RecordUpdateGrp(int Product_Grp_Code, string Product_Grp_SName, string Product_Grp_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistGrp(Product_Grp_Code, Product_Grp_SName, divcode))
            {
                if (!sRecordExistGrp(Product_Grp_Code, Product_Grp_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Group " +
                                 " SET Product_Grp_SName = '" + Product_Grp_SName + "', " +
                                 " Product_Grp_Name = '" + Product_Grp_Name + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

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
        public int RecordDeleteGrp(int Product_Grp_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Group WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getProdgift(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Gift_Code,Gift_SName,Gift_Name,Gift_Value,case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       " else 'Ordinary Gift' " +
                       " end end end as Gift_Type," +
                       " Gift_Effective_From,Gift_Effective_To,State_Code" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND LEFT(Gift_Name,1) = '" + sAlpha + "' AND Gift_Active_Flag='0'  " +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                        " ORDER BY 2";
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


        public DataSet getProdgift_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select '1' val,'All' Gift_Name " +
                     " union " +
                     " select distinct LEFT(Gift_Name,1) val, LEFT(Gift_Name,1) Gift_Name" +
                     " FROM Mas_Gift " +
                     " WHERE Division_Code = '" + divcode + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " AND Gift_Active_flag = 0 " +
                     " ORDER BY 1";
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
        public int DeActivateGrp(int Product_Grp_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                            " SET Product_Grp_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Changes done by Priya -15jul // 
        // Begin 

        public int RecordUpdateGift_new(string GiftCode, string GiftName, string GiftSName, string GiftVal, int GiftType, int divcode)
        {
            int iReturn = -1;
            // if (!RecordExistgift(GiftName, GiftType,divcode))
            //  {
            if (!snRecordExist(GiftCode, GiftSName, divcode))
            {
                if (!nRecordExist(GiftCode, GiftName, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Gift " +
                                 " SET Gift_Name = '" + GiftName + "', " +
                                 " Gift_SName = '" + GiftSName + "', " +
                                 " Gift_Value = '" + GiftVal + "', " +
                                " Gift_Type = '" + GiftType + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Gift_Code = '" + GiftCode + "' AND  Division_Code= " + divcode + " ";

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
        //end

        //Changes done by Priya
        //begin
        public DataSet getGift(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                       " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
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

        public DataSet getGift_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                      " case when Gift_Type = '1' then 'Literature/Lable' " +
                      " else case when Gift_Type = '2' then 'Special Gift'" +
                      " else case when Gift_Type = '3' then 'Doctor Kit'" +
                      "else 'Ordinary Gift' " +
                      "end end end as Gift_Type" +
                      " FROM Mas_Gift  WHERE Division_Code=" +
                      "'" + divcode + "' AND Gift_Active_Flag=0" +
                    //" ORDER BY 2";
                    " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                    " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                    " order by 1 desc";


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
        //Changes done by Priya
        //begin
        // Sorting For ProductReminderList(i.e)-GiftList
        //public DataTable getGiftlist_DataTable(string divcode)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataTable dtProCat = null;

        //    //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
        //    //            " case when Gift_Type = '1' then 'Literature/Lable' " +
        //    //            " else case when Gift_Type = '2' then 'Special Gift' else 'Doctor Kit' end end Gift_Type" +
        //    //            " FROM Mas_Gift  WHERE Division_Code=" +
        //    //            "'" + divcode + "' AND Gift_Active_Flag='0'" +
        //    //         " ORDER BY 2";
        //    strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
        //                " case when Gift_Type = '1' then 'Literature/Lable' " +
        //                " else case when Gift_Type = '2' then 'Special Gift'" +
        //                " else case when Gift_Type = '3' then 'Doctor Kit'" +
        //                "else 'Ordinary Gift' " +
        //                "end end end as Gift_Type" +
        //                " FROM Mas_Gift  WHERE Division_Code=" +
        //                "'" + divcode + "' AND Gift_Active_Flag='0'" +
        //             " ORDER BY 2";
        //    try
        //    {
        //        dtProCat = db_ER.Exec_DataTable(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dtProCat;
        //}
        public DataTable getGiftlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                        " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public DataTable getGiftlist_DataTable(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift'" +
            //            " else case when Gift_Type = '3' then 'Doctor Kit'" +
            //            "else 'Ordinary Gift' " +
            //            "end end end as Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND LEFT(Gift_Name,1) = '" + sAlpha + "' AND Gift_Active_Flag='0'" +
            //         " ORDER BY 2";
            strQry = "SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                    " case when Gift_Type = '1' then 'Literature/Lable' " +
                    " else case when Gift_Type = '2' then 'Special Gift'" +
                    " else case when Gift_Type = '3' then 'Doctor Kit'" +
                    "else 'Ordinary Gift' " +
                    "end end end as Gift_Type" +
                    " FROM Mas_Gift  WHERE Division_Code=" +
                    "'" + divcode + "' AND  Gift_Name like '" + sAlpha + "%'" +
                    "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                 " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //end


        //Reactivate sorting

        public DataTable getGiftlist_DataTable_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        //changes done by Saravanan
        public DataTable getGiftDateDiff(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = " SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                     //" Gift_Effective_From >= '" + EffFromdate + "' AND Gift_Effective_To <= '" + EffTodate + "'" +
                     " (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        //end
        //Changes done by Priya
        //begin
        public DataSet getGift(string divcode, DateTime efffrom, DateTime effto)
        //  public DataSet getGift(string divcode, string efffrom, string effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift' else 'Doctor Kit' end end Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
            //            " Gift_Effective_From = '" + efffrom + "' AND Gift_Effective_To = '" + effto + "'" +
            //         " ORDER BY 2";

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                     " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
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

        public DataSet getGift_React(string divcode, DateTime efffrom, DateTime effto)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            string EffFromdate = efffrom.Month.ToString() + "-" + efffrom.Day + "-" + efffrom.Year;
            string EffTodate = effto.Month.ToString() + "-" + effto.Day + "-" + effto.Year;

            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type" +
                     " FROM Mas_Gift  WHERE Division_Code=" +
                     "'" + divcode + "' AND Gift_Active_Flag='0' AND" +
                " Gift_Effective_From = '" + EffFromdate + "' AND Gift_Effective_To = '" + EffTodate + "'" +
                     " ORDER BY 2";
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
        //end



        //Changes done by Priya
        //begin
        public int ReActivate(String ProdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail " +
                            " SET Product_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Detail_Code = '" + ProdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //end

        //changes done by Priya//19 jul

        public DataSet getProdforstat(string sf_code, string val, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.product_cat_code = '" + val + "' AND " +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                    " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name" +
                    " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c, Mas_Product_State_Rates d, Mas_Salesforce e" +
                    " WHERE a.product_cat_code = b.product_cat_code AND " +
                    " a.Product_Grp_Code = c.product_grp_code AND " +
                    " a.Product_Active_Flag=0 AND a.Product_Detail_Code = d.Product_Detail_Code AND " +
                    " d.State_Code = e.State_Code AND a.Division_Code= '" + div_code + "' AND " +
                    " d.State_Code= '" + val + "' AND " +
                    " e.Sf_Code = '" + sf_code + "' " +
                    " ORDER BY 2";
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
        //end

        //Changes done by Priya 

        public int Update_ProdCatSno(string Product_Cat_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                         " SET ProdCat_SNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end

        //Changes done by Priya--jul21
        public int Update_ProdGrpSno(string Product_Grp_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                         " SET ProdGrp_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Grp_Code = '" + Product_Grp_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //Changes done by Priya--jul24
        public DataSet getGiftName(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                         " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) " +
                    " ORDER BY 2";
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
        }

        //end
        //Changes done by Priya --jul24
        public DataSet FetchState(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsProCat = null;

            strQry = "select state_code from Mas_State where state_code = '" + state_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as State_Code,'---Select---' as State_Name " +
                         " UNION " +
                     " SELECT State_Code,State_Name " +
                     " FROM  Mas_State where division_Code = '" + div_code + "' AND state_active_flag=0 ";
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
        //End
        public DataSet getGifttype(string div_code, string Gift_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value" +

                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + div_code + "' AND  Gift_Type='" + Gift_Type + "'" +
                       "AND Gift_Active_Flag='0'" +
                    " ORDER BY 2";
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
        }
        //Changes done by Priya---jul 24 and aug 6
        public DataSet getStategift(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "  SELECT Gift_Code,Gift_SName,Gift_Name,Gift_Value,Gift_Type,Gift_Effective_From,Gift_Effective_To" +
            //           " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' And State_Code='" + State_Code + "' " +
            //            " ORDER BY 2";

            //strQry = " SELECT a.Gift_SName,a.Gift_Name,a.Gift_Value,a.Gift_Type,a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code,c.statename" +
            //               " FROM Mas_Gift a, mas_state c" +
            //               " WHERE a.state_code=c.state_code And c.state_code='" + State_Code + "' AND a.Division_Code ='" + div_code + "' and a.Gift_Code='" + Gift_Code + "'";

            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) ";

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
        //end
        //Changes done by Priya--jul 26
        public DataSet getLatest_date(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift" +
                " where Gift_Active_Flag='0'" +
                " And LastUpdt_Date IN (SELECT max(LastUpdt_Date) FROM Mas_Gift)";
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
        //end  

        //Changes done by Priya

        public DataSet Fillsub_div(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";

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
        //End
        //Changes done by Priya
        public DataSet getProd_Edit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,State_Code,subdivision_code, Sample_Erp_Code, Sale_Erp_Code, " +
                      " (select Product_Grp_Name from mas_product_group where cast((Product_Grp_Code)as varchar(20))=cast((a.Product_Grp_Code)as varchar(20)))Product_Grp_Name, " +
                     " (select Product_Brd_Name from mas_product_brand where cast((Product_Brd_Code)as varchar(20))=cast((a.Product_Brd_Code)as varchar(20)))Product_Brd_Name, " +
                     " (select Product_Cat_Name from mas_product_Category where cast((Product_Cat_Code)as varchar(20))=cast((a.Product_Cat_Code)as varchar(20)))Product_Cat_Name " +
                     " FROM  Mas_Product_Detail a" +
                     " WHERE Product_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        //
        //Changes done by Saravanan
        public DataSet ViewGift(string div_code, int State_Code, int iFrom, int iTo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select Gift_code,Gift_Name,Gift_Type,Gift_Value,Gift_SName,Division_Code,Gift_Effective_From,Gift_Effective_To,Created_Date,State_Code from Mas_Gift"+
            //         " where Gift_Active_Flag='0'" +            
            //         " And Division_Code = '" + div_code + "' " +
            //         " and year(Gift_Effective_From) = " + iFrom + " and year(Gift_Effective_To) = " + iTo + " order by 1";

            strQry = " SELECT b.Gift_Code,b.Gift_SName,b.Gift_Name,b.Gift_Value," +
                     " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     " else 'Ordinary Gift' " +
                     " end end end as Gift_Type," +
                     " convert(varchar(10),Gift_Effective_From,103) Gift_Effective_From,convert(varchar(10),Gift_Effective_To,103) Gift_Effective_To,b.State_Code" +
                     " FROM mas_state a join Mas_Gift b" +
                     " on" +
                     " b.state_code like '" + State_Code + ',' + "%'  or " +
                     " b.state_code like '%" + ',' + State_Code + "' or" +
                     " b.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " b.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,a.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " b.Division_Code ='" + div_code + "'" +
                     " and year(Gift_Effective_From) >= " + iFrom + " and year(Gift_Effective_To) <= " + iTo + " order by 1";
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
        //end

        //End
        public int RecordCount(string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "select ROW_NUMBER() OVER(ORDER BY Product_Detail_Code DESC) AS Row,* from Mas_Product_Detail WHERE Division_Code = '" + div_code + "' ";
                strQry = "SELECT count(product_detail_code) FROM Mas_Product_Detail WHERE Division_Code = '" + div_code + "' and Product_Active_Flag=0";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getSubdiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as subdivision_code, '---Select---' as subdivision_name " +
                 " UNION " +
                     " SELECT subdivision_code,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";

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


        //Changes done by Sarvanan
        public DataSet getMultiDivsf_Name(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select len(Division_Code) Div_CodeLen,Division_Code,IsMultiDivision from Mas_Salesforce where Sf_Code='" + sf_code + "'";
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

        //Changes done by Priya

        public DataTable getGiftlist_React_Sort(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                        " case when Gift_Type = '1' then 'Literature/Lable' " +
                        " else case when Gift_Type = '2' then 'Special Gift'" +
                        " else case when Gift_Type = '3' then 'Doctor Kit'" +
                        "else 'Ordinary Gift' " +
                        "end end end as Gift_Type" +
                        " FROM Mas_Gift  WHERE Division_Code=" +
                        "'" + divcode + "' AND Gift_Active_Flag=0" +
                        " AND  Gift_Name like '" + sAlpha + "%'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getGiftName_React(string divcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND  Gift_Name like '" + Name + "%'" +
                       "AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                       " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                    " ORDER BY 2";
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
        }
        public DataSet getStategift_React(string div_code, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "SELECT a.Gift_Code,a.Gift_SName,a.Gift_Name,a.Gift_Value," +
                 " case when Gift_Type = '1' then 'Literature/Lable' " +
                     " else case when Gift_Type = '2' then 'Special Gift'" +
                     " else case when Gift_Type = '3' then 'Doctor Kit'" +
                     "else 'Ordinary Gift' " +
                     "end end end as Gift_Type," +
                    " a.Gift_Effective_From,a.Gift_Effective_To,a.State_Code" +
                    " FROM mas_state c join Mas_Gift a" +
                     " on" +
                     " a.state_code like '" + State_Code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + State_Code + "' or" +
                     " a.state_code like '%" + ',' + State_Code + ',' + "%' or" +
                      " a.state_code like '%" + State_Code + "%'" +
                     " WHERE convert(varchar,c.state_code)='" + State_Code + "' and Gift_Active_Flag=0 and " +
                     " a.Division_Code ='" + div_code + "' " +
                     " and (Gift_Effective_From < DATEADD(day, DATEDIFF(day, 0, getDate()) - 1, 0)" +
                     " and Gift_Effective_To <  DATEADD(day, DATEDIFF(day, 0, getDate()),  0))" +
                     " ORDER BY 2";
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


        //Changes done by Reshmi
        public bool sRecordExistDetail(string Product_Detail_Name, string Product_Detail_Code, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_Product_Detail WHERE Product_Detail_Name = '" + Product_Detail_Name + "' AND Product_Detail_Code!='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "'";

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
        public DataSet getProCat_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Cat_SName,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY ProdCat_SNo";
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
        public int ReActivate(int Prod_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Category " +
                            " SET Product_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Cat_Code = '" + Prod_Cat_Code + "' and  Product_Cat_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getProGrp_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            strQry = " SELECT Product_Grp_Code,Product_Grp_SName,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY ProdGrp_Sl_No";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public int ReActivateGrp(int Prod_Grp_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Group " +
                            " SET Product_Grp_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Grp_Code = '" + Prod_Grp_Code + "' and  Product_Grp_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Done By Reshmi

        public DataSet getProBrd(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name, " +
                     " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count,Type,"+
                     " (select COUNT(d.product_brand_code) from Product_Image_List d where Flag = 0 and d.product_brand_code = b.Product_Brd_Code ) as slide_count " +
                     " FROM  Mas_Product_Brand b" +
                     " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY b.Product_Brd_SNO";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataTable getProductBrandlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProBrd = null;

            strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name, " +
                   " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag =0 and  p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count ,"+
                  " (select COUNT(d.product_brand_code) from Product_Image_List d where Flag = 0 and d.product_brand_code = b.Product_Brd_Code ) as slide_count " +
                   " FROM  Mas_Product_Brand b" +
                   " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                   " ORDER BY b.Product_Brd_SNO";
            try
            {
                dtProBrd = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProBrd;
        }

        public int RecordDelete(int Product_Cat_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Product_Category WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Brd_DeActivate(int ProBrdCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                            " SET Product_Brd_Active_Flag=1 ," +
                           " LastUpdt_Date = getdate() " +
                            " WHERE Product_Brd_Code = '" + ProBrdCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //..
        public int Brd_RecordUpdate(int ProBrdCode, string Product_Brd_SName, string Product_Brd_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistbrd(ProBrdCode, Product_Brd_SName, divcode))
            {
                if (!nRecordExist(ProBrdCode, Product_Brd_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Brand " +
                                 " SET Product_Brd_SName = '" + Product_Brd_SName + "', " +
                                 " Product_Brd_Name = '" + Product_Brd_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Brd_Code = '" + ProBrdCode + "' and Product_Brd_Active_Flag = 0 ";

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

        public bool RecordExistbrd(int ProBrdCode, string Product_Brd_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Product_Brand WHERE Product_Brd_SName = '" + Product_Brd_SName + "' AND Product_Brd_Code!='" + ProBrdCode + "' AND Division_Code= '" + divcode + "' ";

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
        public bool nRecordExist(int ProBrdCode, string Product_Brd_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Product_Brand WHERE Product_Brd_Name = '" + Product_Brd_Name + "' AND Product_Brd_Code!='" + ProBrdCode + "' and Division_Code = '" + divcode + "'";

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
        public DataSet getProdBrd(string divcode, string ProdBrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_SName,Product_Brd_Name,Type FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Code= '" + ProdBrdCode + "'AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public int Brd_RecordAdd(string divcode, string Product_Brd_SName, string Product_Brd_Name, string Type)
        {
            int iReturn = -1;
            if (!RecordExist_Brd(Product_Brd_SName, divcode))
            {
                if (!sRecordExist_Brd(Product_Brd_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Brd_Code)+1,'1') Product_Brd_Code from Mas_Product_Brand ";
                        int Product_Brd_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Brand(Product_Brd_Code,Division_Code,Product_Brd_SName,Product_Brd_Name,Product_Brd_Active_Flag,Created_Date,LastUpdt_Date,Type)" +
                                 "values('" + Product_Brd_Code + "','" + divcode + "','" + Product_Brd_SName + "', '" + Product_Brd_Name + "',0,getdate(),getdate(),'" + Type + "')";


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
        public bool RecordExist_Brd(string Product_Brd_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Product_Brand WHERE Product_Brd_SName='" + Product_Brd_SName + "' and Division_Code = '" + div_code + "' ";
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
        public bool sRecordExist_Brd(string Product_Brd_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Product_Brand WHERE Product_Brd_Name='" + Product_Brd_Name + "' and Division_Code = '" + div_code + "' ";
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
        public int Update_ProdBrdSno(string Product_Brd_Code, string Sno)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                         " SET Product_Brd_SNO = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Product_Brd_Code = '" + Product_Brd_Code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getProBrd_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_Code,Product_Brd_SName,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Product_Brd_SNO";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public int Brd_ReActivate(int Product_Brd_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Brand " +
                            " SET Product_Brd_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Product_Brd_Code = '" + Product_Brd_Code + "' and  Product_Brd_Active_Flag =1";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getProdforbrd(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code," +
                        " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                         "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Brd_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdforcat(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code," +
                        " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                         "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.product_cat_code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getProdforgrp(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code," +
                        " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                         "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Grp_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public int ReActivate_Brd(string Product_Brd_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Product_Brand" +
                         " SET Product_Brd_Active_Flag=0, " +
                         " LastUpdt_Date = getdate() " +
                         " where Product_Brd_Code = '" + Product_Brd_Code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataTable getDTProduct_Brd(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProBrd = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name," +
                      "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Brd_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdforCode(string divcode, string prodcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Detail_Name,Product_Sale_Unit,Product_Sample_Unit_One," +
                     " Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Code,Product_Type_Code,Product_Description,Product_Grp_Code,Product_Detail_Code,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code,Product_Brd_Code" +
                     " FROM Mas_Product_Detail WHERE Product_Detail_Code= '" + prodcode + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        //...
        #region Quiz

        /*-------------------------- Process Quiz User List(29/01/2018) -------------------------------------*/
        public DataSet Process_UserList(string div_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[sp_UserList_Process_Quiz] '" + div_Code + "', '" + Sf_Code + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }

        /*-------------------------- Add Processing  User Details(29/01/2018) -------------------------------------*/

        public int AddProcessing_Details(string Sf_Code, string Sf_Name, string HQ, string Desig, string State, string Time, string P_Date, string Type, string No_Attempt, int Sf_UID, string Div_Code, string Survey_Id, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn;

            int ProcessID = -1;

            strQry = "SELECT ISNULL(MAX(ProcessId),0)+1 FROM dbo.Processing_UserList ";
            ProcessID = db_ER.Exec_Scalar(strQry);

            strQry = " EXEC [dbo].[SP_Add_UserListProcessing] " + ProcessID + ", '" + Sf_Code + "','" + Sf_Name + "','" + HQ + "','" + Desig + "','" + State + "','" + Time + "','" + P_Date + "','" + Type + "','" + No_Attempt + "','0'," + Sf_UID + ",'" + Div_Code + "','" + Survey_Id + "','" + Month + "','" + Year + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        /*-------------------------- Process Zone Selected UserList (29/01/2018) -------------------------------------*/
        public DataSet Process_SelectedUser_List(string div_Code, string Survey_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProcess = null;
            strQry = "select ProcessId,Sf_Code,Sf_Name from dbo.Processing_UserList where SurveyId='" + Survey_ID + "' and Div_Code='" + div_Code + "'";

            try
            {
                dsProcess = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProcess;
        }

        /*-------------------------- Add Quiz Category Details(29/01/2018) -------------------------------------*/

        public int AddQuiz_Category_Details(string Category_SName, string Category_Name, string Div_Code)
        {


            int iReturn = -1;
            if (!S_RecordQuiz_SubName(Category_SName, Div_Code))
            {
                if (!Record_Category_Name(Category_Name, Div_Code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        int CategoryId = -1;
                        strQry = "SELECT ISNULL(MAX(Category_Id),0)+1 FROM dbo.QuizCategory_Creation ";
                        CategoryId = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO dbo.QuizCategory_Creation(Category_Id,Category_ShortName,Category_Name,Created_Date,Division_Code,Category_Active)" +
                                 "values(" + CategoryId + ",'" + Category_SName + "', '" + Category_Name + "',GETDATE(),'" + Div_Code + "',0) ";

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

        /*-------------------------- Quiz Category SubName Exist(29/01/2018) -------------------------------------*/

        public bool S_RecordQuiz_SubName(string Category_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT * FROM dbo.QuizCategory_Creation WHERE Category_ShortName='" + Category_SName + "' and Division_Code= '" + divcode + "' and Category_Active=0 ";
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

        /*-------------------------- Quiz Category Name Exist(29/01/2018) -------------------------------------*/

        public bool Record_Category_Name(string Category_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT * FROM dbo.QuizCategory_Creation WHERE Category_Name='" + Category_Name + "' and Division_Code= '" + divcode + "' and Category_Active=0 ";
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

        /*-------------------------- Quiz Category List (29/01/2018) -------------------------------------*/

        public DataSet Quiz_Category_List(string divCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCategory = null;
            strQry = "select Category_Id,Category_ShortName,Category_Name from dbo.QuizCategory_Creation where Division_Code='" + divCode + "' and Category_Active=0 ORDER BY Category_Name";

            try
            {
                dsCategory = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCategory;
        }



        // Sorting
        public DataTable Quiz_Category_List_Sorting(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtCategory = null;

            strQry = "select Category_Id,Category_ShortName,Category_Name from dbo.QuizCategory_Creation where Division_Code='" + divcode + "' and Category_Active=0 ORDER BY Category_ShortName,Category_Name";

            try
            {
                dtCategory = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtCategory;
        }


        /*-------------------------- Update Quiz Category Details(29/01/2018) -------------------------------------*/

        public int Update_Quiz_Category(string Category_SName, string Category_Name, string Div_Code, int CategoryId)
        {
            int iReturn = -1;
            if (!S_RecordQuiz_SubName(Category_SName, Div_Code))
            {
                if (!Record_Category_Name(Category_Name, Div_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE dbo.QuizCategory_Creation SET Category_ShortName = '" + Category_SName + "',Category_Name = '" + Category_Name + "',Last_Update_Date = GETDATE(),Division_Code='" + Div_Code + "' WHERE Category_Id = " + CategoryId + "";


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

        /*-------------------------- Edit Quiz Category Details(29/01/2018) -------------------------------------*/

        public DataSet Get_Quiz_Category(string divcode, int Category_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsCategory = null;

            strQry = "SELECT Category_ShortName,Category_Name " +
                     " FROM dbo.QuizCategory_Creation WHERE Category_Id= '" + Category_Id + "' AND Division_Code= '" + divcode + "' and Category_Active=0 " +
                     " ORDER BY 2";
            try
            {
                dsCategory = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCategory;
        }

        /*-------------------------- Deactivate Quiz Category Details(29/01/2018) -------------------------------------*/
        public int DeActivate_Category(int CategoryId)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.QuizCategory_Creation SET Category_Active=1,Last_Update_Date = GETDATE() WHERE Category_Id = " + CategoryId + "";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }


        /*-------------------------- Deactivate Quiz Title Details(29/01/2018) -------------------------------------*/
        public int DeActivate_QuizTitle(int SurveyID)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE dbo.QuizTitleCreation SET Active=1,LastUpdate_Date=getdate() WHERE Survey_Id = " + SurveyID + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        /*-------------------------- Get Quiz Input Option (29/01/2018) -------------------------------------*/
        public DataSet GetQuizInputOption(int QueId, string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsOption = null;

            strQry = "select Input_Id,Correct_Ans from dbo.AddInputOptions where Question_Id=" + QueId + " and Division_Code='" + DivCode + "'";
            try
            {
                dsOption = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsOption;

        }

        /*-------------------------- Update Quiz Input Option (29/01/2018) -------------------------------------*/
        public int QuizOption_Update(int QusId, int InputOpt_Id, string AnsOpt, string CorAns)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " update dbo.AddInputOptions set Input_Text='" + AnsOpt + "', Correct_Ans='" + CorAns + "' where Input_Id=" + InputOpt_Id + " and Question_Id=" + QusId + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        /*-------------------------- Update Quiz Question (29/01/2018) -------------------------------------*/
        public int Quiz_Update_Question(int QusId, string Ques_text, string DivCode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update dbo.AddQuestions set Question_Text='" + Ques_text + "', Division_Code='" + DivCode + "' where Question_Id=" + QusId + "";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        #endregion
        public int RecordAdd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, string Product_Sample_Unit_One, string Product_Sample_Unit_Two, string Product_Sample_Unit_Three, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, int Division_Code, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code)
        {
            int iReturn = -1;
            int iSlNo = -1;
            int icodeSlNo = -1;
            if (!RecordExistdet(Product_Detail_Code, Division_Code))
            {
                if (!sRecordExistdet(Product_Detail_Name, Division_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(MAX(Prod_Detail_Sl_No),0)+1 FROM Mas_Product_Detail";
                        iSlNo = db.Exec_Scalar(strQry);


                        strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_Product_Detail";
                        icodeSlNo = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Product_Detail(Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Sample_Unit_One," +
                                    " Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Code,Product_Type_Code,Product_Description, " +
                                    " Division_Code,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,LastUpdt_Date,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code,Product_Brd_Code,product_code_slno) " +
                                    " VALUES('" + Product_Detail_Code + "', '" + Product_Detail_Name + "', '" + Product_Sale_Unit + "', '" + Product_Sample_Unit_One + "', " +
                                    " '" + Product_Sample_Unit_Two + "', '" + Product_Sample_Unit_Three + "', " + Product_Cat_Code + ", " +
                                    " '" + Product_Type_Code + "', '" + Product_Description + "', " + Division_Code + ", getdate(), 0, " + iSlNo + ", " + Product_Grp_Code + ",getdate(),'" + state + "','" + sub_division + "','" + mode + "','" + sample + "','" + sale + "','" + Product_Brd_Code + "', " + icodeSlNo + ") ";


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
        public int RecordUpdateProd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, string Product_Sample_Unit_One, string Product_Sample_Unit_Two, string Product_Sample_Unit_Three, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, int Division_Code, string State_Code, string sub_division, string mode, string sample, string sale, string divcode, int Product_Brd_Code)
        {
            int iReturn = -1;
            if (!sRecordExistDetail(Product_Detail_Name, Product_Detail_Code, divcode))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Product_Detail " +
                             " SET Product_Detail_Name = '" + Product_Detail_Name + "', " +
                             " Product_Sale_Unit = '" + Product_Sale_Unit + "', " +
                               " Product_Sample_Unit_One = '" + Product_Sample_Unit_One + "', " +
                                 " Product_Sample_Unit_Two = '" + Product_Sample_Unit_Two + "', " +
                                   " Product_Sample_Unit_Three = '" + Product_Sample_Unit_Three + "', " +
                                     " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                    " Product_Grp_Code = " + Product_Grp_Code + "," +
                                 " Product_Type_Code = '" + Product_Type_Code + "', " +
                                 " Product_Description = '" + Product_Description + "'," +
                                 " LastUpdt_Date = getdate() , " +
                                 " State_Code = '" + State_Code + "',subdivision_code = '" + sub_division + "', Product_Mode ='" + mode + "', Sample_Erp_Code = '" + sample + "', Sale_Erp_Code ='" + sale + "' ,Product_Brd_Code ='" + Product_Brd_Code + "' " +
                             " WHERE  Division_Code = " + Division_Code + " AND Product_Detail_Code = '" + Product_Detail_Code + "' ";

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
        public DataSet getProdall(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Code_SlNo,a.Product_Sale_Unit,a.Product_Sample_Unit_One,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code," +
                        " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                         "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
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
        //
        public DataSet getProdforname(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code," +
                        " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                         "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                      " a.Product_Detail_Name like '" + val + "%' and " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";

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
        public DataSet getProdforSubdiv(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.subdivision_name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,mas_subdivision d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.subdivision_code = '" + val + "' AND " +                     
            //         " a.subdivision_code = convert(varchar(10),d.subdivision_code) and" +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                      " c.Product_Grp_Name, e.Product_Brd_Name,d.subdivision_code,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code,"+
                         " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                      "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                      " FROM  mas_subdivision d,Mas_Product_Category b, " +
                      " Mas_Product_Group c ,Mas_Product_Brand e join Mas_Product_Detail a on a.subdivision_code like '" + val + ',' + "%'  or " +
                       " a.subdivision_code like '%" + ',' + val + "' or a.subdivision_code like '%" + ',' + val + ',' + "%' " +
                       " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                       " AND d.SubDivision_Active_Flag=0 AND d.Div_Code= '" + divcode + "' and d.subdivision_code ='" + val + "'" +
                       "order by Product_Detail_Name";
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
        //Changes done by priya And Reshmi

        public DataSet getProdforState(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                      " c.Product_Grp_Name,e.Product_Brd_Name, d.State_Code,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code,"+
                         " (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                      "(select COUNT(g.Product_Detail_Code) from Product_Image_List g where Flag=0 and g.Division_Code= '" + divcode + "' and  	" +
                     " ',' + g.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                      " FROM  Mas_State d,Mas_Product_Category b, " +
                      " Mas_Product_Group c ,Mas_Product_Brand e join Mas_Product_Detail a on a.State_Code like '" + val + ',' + "%'  or " +
                       " a.State_Code like '%" + ',' + val + "' or a.State_Code like '%" + ',' + val + ',' + "%' " +
                       " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                       " AND a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' and d.State_Code ='" + val + "'" +
                       "order by Product_Detail_Name";
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
        public DataSet getProdforMode(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Code_SlNo,a.Product_Sale_Unit,a.Product_Sample_Unit_One," +
                "a.Product_Description,  a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name," +
                "d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code, (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE " +
                "(a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                " FOR XML PATH(''))  , 1, 1, ''))  subdivision_name,Product_Mode  FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d WHERE " +
                "a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = d.Product_Brd_Code AND a.Product_Active_Flag=0 " +
                "AND a.Division_Code= '" + divcode + "' and Product_Mode='" + val + "'  ORDER BY Prod_Detail_Sl_No";
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
        public DataTable getDTProduct_Cat(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name," +
                      "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.product_cat_code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Nam(string search, string searchname, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Name,Product_Grp_Name,Product_Brd_Name," +
                      "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + div_code + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 " +
                     " and a.Product_Detail_Name like '" + searchname + "%'  and  a.Division_Code= '" + div_code + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Grp(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name," +
                     "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Grp_Code = c.product_grp_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Grp_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Sbdiv(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            //strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
            //         " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.subdivision_name" +
            //         " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,mas_subdivision d" +
            //         " WHERE a.product_cat_code = b.product_cat_code AND " +
            //         " a.Product_Grp_Code = c.product_grp_code AND " +
            //         " a.subdivision_code = '" + val + "' AND " +
            //         " a.subdivision_code = convert(varchar(10),d.subdivision_code) and" +
            //         " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                   " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                    " c.Product_Grp_Name,e.Product_Brd_Name, d.subdivision_code, "+
                     "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                    " FROM  mas_subdivision d,Mas_Product_Category b, " +
                    " Mas_Product_Group c , Mas_Product_Brand e join Mas_Product_Detail a on a.subdivision_code like '" + val + ',' + "%'  or " +
                     " a.subdivision_code like '%" + ',' + val + "' or a.subdivision_code like '%" + ',' + val + ',' + "%' " +
                     " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                     " AND a.Product_Active_Flag=0 AND  a.Division_Code= '" + divcode + "' and d.subdivision_code ='" + val + "'" +
                     "order by Product_Detail_Name";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_State(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                      " c.Product_Grp_Name,e.Product_Brd_Name, d.State_Code,"+
                       "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + divcode + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                      " FROM  Mas_State d,Mas_Product_Category b, " +
                      " Mas_Product_Group c ,Mas_Product_Brand e join Mas_Product_Detail a on a.State_Code like '" + val + ',' + "%'  or " +
                       " a.State_Code like '%" + ',' + val + "' or a.State_Code like '%" + ',' + val + ',' + "%' " +
                       " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = e.Product_Brd_Code" +
                       " AND a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' and d.State_Code ='" + val + "'" +
                       "order by Product_Detail_Name";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public DataTable getDTProduct_Mode(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = "  SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Code_SlNo,a.Product_Sale_Unit,a.Product_Sample_Unit_One,a.Product_Description, " +
                " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode," +
                "a.Sale_Erp_Code,a.Sample_Erp_Code, (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE " +
                "(a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%')  " +
                "FOR XML PATH(''))  , 1, 1, ''))  subdivision_name,Product_Mode " +
                " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d WHERE a.product_cat_code = b.product_cat_code AND " +
                "a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = d.Product_Brd_Code AND a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "'" +
                " and Product_Mode='" + val + "'  ORDER BY Prod_Detail_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //changes done by Reshmi
        public DataSet FindProduct(string search, string searchname, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Description, " +
                     " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three,Product_Cat_Name,Product_Grp_Name,Product_Brd_Name," +
                        " Sale_Erp_Code,Sample_Erp_Code, (SELECT STUFF((SELECT '/ ' +  sub.subdivision_name FROM mas_subdivision Sub WHERE (a.subdivision_code like  Convert(varchar, sub.subdivision_code) + ',' + '%' or a.subdivision_code like '%,' + Convert(varchar, sub.subdivision_code) + ',%') " +
                     " FOR XML PATH('')) " +
                     " , 1, 1, ''))  subdivision_name, " +
                        "(select COUNT(d.Product_Detail_Code) from Product_Image_List d where Flag=0 and d.Division_Code= '" + div_code + "' and  	" +
                     " ',' + d.Product_Detail_Code + ',' LIKE '%,' + CAST(a.Product_Code_SlNo as VARCHAR(2000)) + ',%')  as slide_count " +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 " +
                     " and a.Product_Detail_Name like '" + searchname + "%'  and  a.Division_Code= '" + div_code + "' " +
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
        //Changes done by Priya And Reshmi
        //begin
        public DataSet getProd_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Product_Type_Code,Product_Description, " +
            //         " Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three " +
            //         " FROM  Mas_Product_Detail " +
            //         " WHERE Product_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY 2";
            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                " WHERE a.product_cat_code = b.product_cat_code AND " +
                "a.Product_Grp_Code = c.product_grp_code AND " +
                "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                "a.Product_Active_Flag=1 AND a.Division_Code= '" + divcode + "' " +
                " ORDER BY Prod_Detail_Sl_No";
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
        //end

        // Sorting For ProductViewList 
        public DataTable getProductallList_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public DataSet getProdSlNo(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Description,P.Product_Sale_Unit," +
                     " case when P.Product_Type_Code = 'R' then 'Regular' " +
                     " else case when P.Product_Type_Code = 'N' then 'New' else 'Others' end end Product_Type_Name, " +
                     " C.Product_Cat_Name, G.Product_Grp_Name, B.Product_Brd_Name" +
                     " FROM  Mas_Product_Detail P,Mas_Product_Category C,Mas_Product_Group G,Mas_Product_Brand B" +
                     " WHERE P.Product_Cat_Code = C.Product_Cat_Code AND " +
                     " P.Product_Grp_Code = G.Product_Grp_Code AND " +
                     " P.Product_Brd_Code = B.Product_Brd_Code AND " +
                     " P.Product_Active_Flag=0 AND P.Division_Code= '" + divcode + "' " +
                     " ORDER BY Prod_Detail_Sl_No";
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
        //Changes done by Priya  AND Reshmi
        public DataTable getProductdet_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtProCat = null;

            strQry = " SELECT P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Description,P.Product_Sale_Unit," +
                    " case when P.Product_Type_Code = 'R' then 'Regular' " +
                    " else case when P.Product_Type_Code = 'N' then 'New' else 'Others' end end Product_Type_Name, " +
                    " C.Product_Cat_Name, G.Product_Grp_Name,B.Product_Brd_Name" +
                    " FROM  Mas_Product_Detail P,Mas_Product_Category C,Mas_Product_Group G,Mas_Product_Brand B" +
                    " WHERE P.Product_Cat_Code = C.Product_Cat_Code AND " +
                    " P.Product_Grp_Code = G.Product_Grp_Code AND " +
                    " P.Product_Brd_Code = B.Product_Brd_Code AND " +
                    " P.Product_Active_Flag=0 AND P.Division_Code= '" + divcode + "' " +
                    " ORDER BY 2";
            try
            {
                dtProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }

        public int RecordBulkAdd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, int Product_Cat_Code, int Product_Grp_Code, string Product_Description, string State_Code, string subdivision_code, int Division_Code, int Product_Brd_Code)
        {
            int iReturn = -1;
            int iSlNo = -1;
            int icodeSlNo = -1;
            if (!RecordExistdet(Product_Detail_Code, Division_Code))
            {
                if (!sRecordExistdet(Product_Detail_Name, Division_Code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(MAX(Prod_Detail_Sl_No),0)+1 FROM Mas_Product_Detail WHERE Division_Code = '" + Division_Code + "' ";
                        iSlNo = db.Exec_Scalar(strQry);


                        strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_Product_Detail";
                        icodeSlNo = db.Exec_Scalar(strQry);


                        strQry = "insert into Mas_Product_Detail (Product_Detail_Code,Product_Detail_Name,Product_Description,Product_Sale_Unit, " +
                                 " Product_Cat_Code,Product_Grp_Code,Product_Sample_Unit_One,Product_Sample_Unit_Two,Product_Sample_Unit_Three, " +
                                 " Product_Type_Code,State_Code,subdivision_code,Division_Code,Created_Date,LastUpdt_Date,Product_Active_Flag ,Prod_Detail_Sl_No ,Product_Brd_Code,product_code_slno ) " +
                                 " VALUES('" + Product_Detail_Code + "', '" + Product_Detail_Name + "', '" + Product_Description + "', '" + Product_Sale_Unit + "', " +
                                 " '" + Product_Cat_Code + "', '" + Product_Grp_Code + "', 1,1,1,'N','" + State_Code + "','" + subdivision_code + "','" + Division_Code + "',getdate(),getdate(),0," + iSlNo + ",'" + Product_Brd_Code + "'," + icodeSlNo + ")";


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
        public DataSet FillProductBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                    " UNION " +
                    " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                    " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                    " ORDER BY 2";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet getProdCatgType(string prod_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Type_Code,State_Code,subdivision_code,Product_Brd_Code FROM  Mas_Product_Detail " +
                     " WHERE Product_Detail_Code='" + prod_code + "' AND Division_Code= '" + divcode + "' ";
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
        public int DeleteProductRate(string state_code, string div_code)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Delete from Mas_Product_State_Rates where State_Code='" + state_code + "' and Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteProductRate(string div_code)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Delete from Mas_Product_State_Rates where Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getProduct_State(string Division_Code, string product_Detail_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT State_Code  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and product_Detail_Code='" + product_Detail_Code + "'";

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

        public DataSet getProduct_Exp(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT '0' as Product_Code_SlNo,'---Select the Product---' as Product_Detail_Name union all " +
                     "SELECT '-1' as Product_Code_SlNo,'All Product' as Product_Detail_Name union all " +
                     "SELECT Product_Code_SlNo,Product_Detail_Name  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";

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

        //Prod Code Change


        public int RecordUpdateProductCode(string ProdCode, string NewCode, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistcode(NewCode, divcode))
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();



                    strQry = "UPDATE Mas_Product_State_Rates " +
                          " SET Product_Detail_Code = '" + NewCode + "' " +
                          " WHERE Product_Detail_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_SS_Entry_Detail " +
                      " SET Product_Detail_Code = '" + NewCode + "' " +
                      " WHERE Product_Detail_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_TargetFixation_Product_Details " +
                      " SET Product_Code = '" + NewCode + "' " +
                      " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_Sample_Despatch_Details " +
                   " SET Product_Code = '" + NewCode + "' " +
                   " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Trans_DCR_BusinessEntry_Details " +
                   " SET Product_Code = '" + NewCode + "' " +
                   " WHERE Product_Code='" + ProdCode + "' and Division_Code = '" + divcode + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_Product_Detail " +
                                                   "SET Product_Detail_Code='" + NewCode + "' " +
                                                   "WHERE Product_Detail_Code='" + ProdCode + "' AND Division_Code='" + divcode + "' and Product_Active_Flag=0";


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
        public bool RecordExistcode(string NewCode, string divcode)
        {
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Code) FROM Mas_Product_Detail WHERE Product_Detail_Code='" + NewCode + "'AND Division_Code='" + divcode + "' ";
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

        // product Upload

        public DataSet getProductGroup_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Grp_Code,Product_Grp_Name FROM  Mas_Product_Group " +
                     " WHERE Product_Grp_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getProductCategory_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet getProductBrand_UP(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public int GetProductCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Product_Code_SlNo),0)+1 FROM Mas_Product_Detail";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetProd_Cat_Code(string Cat_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Cat_Code,Product_Cat_Name from Mas_Product_Category where Product_Cat_Name='" + Cat_Name + "' and Division_Code = '" + div_code + "' and Product_Cat_Active_Flag=0";

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
        public DataSet GetProd_Grp_Code(string Qrp_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Grp_Code,Product_Grp_Name from Mas_Product_Group where Product_Grp_Name='" + Qrp_Name + "' and Division_Code = '" + div_code + "' and Product_Grp_Active_Flag=0";

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

        public DataSet GetProd_Brd_Code(string Brd_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand where Product_Brd_Name='" + Brd_Name + "' and Division_Code = '" + div_code + "' and Product_Brd_Active_Flag=0";

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
        public DataSet Visit_Doc_Prd(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_Product '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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
        public DataSet getPrd_For_Mapp(string div_code, string sf_code,string sale)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            //strQry = " SELECT Product_Code_SlNo,Product_Detail_Name,Product_Sale_Unit  FROM  Mas_Product_Detail " +
            //          " WHERE Product_Active_Flag=0 AND Division_Code= '" + div_code + "'  ORDER BY 2 ";

            //strQry = "select Product_Brd_Code as Product_Code_SlNo,Product_Brd_Name as Product_Detail_Name, '' as Product_Sale_Unit " +
            //     " from mas_product_brand where division_code='" + div_code + "' and Product_Brd_Active_Flag=0 ";


            strQry = "SELECT Product_Code_SlNo,Product_Detail_Name,Product_Sale_Unit FROM  Mas_Product_Detail a,mas_salesforce b " +
                   " WHERE Product_Active_Flag=0  AND Product_Mode='"+ sale + "' AND a.Division_Code= '" + div_code + "'  and b.sf_code='" + sf_code + "' " +
                   " and charindex(cast(b.subdivision_code as varchar),a.subdivision_code) > 0  ORDER BY 2 ";

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

        public DataSet getprdfor_Mappdr(string Listeddr_Code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Code as Product_Code_SlNo ,Product_Name,Product_Priority  from Map_LstDrs_Product " +
                      " where Listeddr_Code ='" + Listeddr_Code + "' and sf_code='" + sf_code + "' ";

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

        public DataSet getPr(string sf_code, string divcode, string transMonth, string transYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry =
                " select  b.Product_Code,b.Product_Sale_Unit,b.productc, a.Trans_sl_No,a.Sf_Code,a.Division_Code,a.Trans_Month,a.Trans_Year,b.Trans_sl_No,b.Division_Code,b.Despatch_Qty,b.Remarks FROM  Trans_Sample_Despatch_Head a inner join Trans_Sample_Despatch_Details b on a.Trans_sl_No=b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "'and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "'";
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

        public DataSet getGiftedit(string sf_code, string divcode, string transMonth, string transYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select  b.Gift_Name,b.productc, a.Trans_sl_No,a.Sf_Code,a.Division_Code,a.Trans_Month,a.Trans_Year,b.Trans_sl_No,b.Division_Code,b.Despatch_Qty,b.Remarks FROM  Trans_Input_Despatch_Head a inner join Trans_Input_Despatch_Details b on a.Trans_sl_No=b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "'and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "'";

            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift'" +
            //            " else case when Gift_Type = '3' then 'Doctor Kit'" +
            //            "else 'Ordinary Gift' " +
            //            "end end end as Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Active_Flag='0'";
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
        //Changes done by Vidya

        /*-------------Product List Details-----------------*/

        public DataSet getProductStatewise(int div_code, int sub_Division)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProductList = null;


            strQry = "EXEC [SP_Proc_Product_SubState_Detail] '" + div_code + "','" + sub_Division + "'";

            try
            {
                dsProductList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProductList;
        }



        /*-----------Product Statewise Select------------*/
        public DataSet getProduct_Selected_State(int div_code, int sub_Division, int State_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProductState = null;

            strQry = "EXEC [SP_Proc_Product_Statewise_Detail] '" + div_code + "','" + sub_Division + "'," + State_Code + "";

            try
            {
                dsProductState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProductState;
        }


        /*------------- State Code ProductCode wise --------------*/
        public DataSet getProduct_StateSplit(int div_code, int sub_Division, int Product_SLNO)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStateSplit = null;
            //strQry = "select State_Code from Mas_Product_Detail where Product_Active_Flag=0 and Division_Code= '" + div_code + "' and subdivision_code='" + sub_Division + "'  and Product_Code_SlNo in (" + Product_SLNO + ")";

            strQry = "EXEC [SP_Proc_Product_Statewise_Split] '" + div_code + "','" + sub_Division + "'," + Product_SLNO + "";

            try
            {
                dsStateSplit = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStateSplit;
        }



        /*----------------ProductDetail Statewise Update----------------------*/
        public int RecordProduct_Update(string Product_SLNO, string State_Cod)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail set State_Code='" + State_Cod + "' where Product_Code_SlNo='" + Product_SLNO + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }



        /*------------------SubDivision----------------*/
        public DataSet getSubDivisionDDL(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDivision = null;
            strQry = "select subdivision_code,subdivision_name from  dbo.mas_subdivision where Div_Code='" + div_code + "'";

            try
            {
                dsSubDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSubDivision;
        }


        /*----------------Product Detail SubDivision wise------------------*/

        public DataSet getProduct_SubDivision_Select(string div_code, string sub_Division)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProductState = null;

            strQry = "EXEC [SP_Proc_Product_SubDivision_Detail] '" + div_code + "','" + sub_Division + "'";

            try
            {
                dsProductState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProductState;
        }



        /*---------------------SubDivision Wise StateCode and SubDivision--------------------- */

        public DataSet getSubDiv_StateSplit(string div_code, string sub_Division, string Product_SLNO)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState_SubDivSplit = null;
            //  strQry = "select P.subdivision_code,P.State_Code from Mas_Product_Detail P inner join mas_subdivision S on LEFT(p.subdivision_code,CHARINDEX(',', p.subdivision_code + ',') - 1)=S.subdivision_code where  Division_Code= '" + div_code + "' and s.subdivision_name='" + sub_Division + "' and P.Product_Code_SlNo='" + Product_SLNO + "'";


            strQry = "select P.subdivision_code,P.State_Code from Mas_Product_Detail P inner join mas_subdivision S on LEFT(p.subdivision_code,CHARINDEX(',', p.subdivision_code + ',') - 1)=S.subdivision_code where  Division_Code= '" + div_code + "' and P.subdivision_code like '%" + sub_Division + "%' and P.Product_Code_SlNo='" + Product_SLNO + "'";


            try
            {
                dsState_SubDivSplit = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState_SubDivSplit;
        }


        /*----------------ProductDetail SubDivision Update----------------------*/
        public int Rec_ProductSubDiv_Update(string Product_SLNO, string SubDiv_Code, string State_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail set subdivision_code='" + SubDiv_Code + "' where Product_Code_SlNo='" + Product_SLNO + "'";

                //strQry = "UPDATE Mas_Product_Detail set subdivision_code='" + SubDiv_Code + "',State_Code='" + State_Code + "' where Product_Code_SlNo='" + Product_SLNO + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public DataSet getProduct_State_Code(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT top(1) State_Code  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "'";

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

        // end

        public DataSet getProdstatedoctor(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description,d.Retailor_Price, " +
                     "a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name" +
                    " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c, Mas_Product_State_Rates d, Mas_Salesforce e" +
                    " WHERE a.product_cat_code = b.product_cat_code AND " +
                    " a.Product_Grp_Code = c.product_grp_code AND " +
                    " a.Product_Active_Flag=0 AND a.Product_Detail_Code = d.Product_Detail_Code AND " +
                    " d.State_Code = e.State_Code AND a.Division_Code= '" + divcode + "'  AND " +
                    "  e.Sf_Code = '" + sf_code + "'" +
                    " ORDER BY Prod_Detail_Sl_No";
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

        public DataSet getProdstadoctor(string divcode, string st_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            //strQry = "EXEC  SP_Proc_Product_Statewise_Detail '" + divcode + "', '" + st_code + "' ";

            //'7', '8'
            strQry = "select  p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,  isnull(rtrim(Retailor_Price),0) Retailor_Price " +
                     "From Mas_Product_Detail p left outer   " +
                    " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +
                    "Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                    "where state_code='" + st_code + "' and division_code='" + divcode + "')" +
                    "and R.state_code = '" + st_code + "' where Product_Active_Flag=0 and p.Division_code='" + divcode + "'  ";
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

        public DataSet getProdstadoctorBasedOn(string divcode, string st_code, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (basedOn == "M")
            {
                basedOn = "MRP_Price";
            }
            else if (basedOn == "T")
            {
                basedOn = "Target_Price";
            }
            else if (basedOn == "R")
            {
                basedOn = "Retailor_Price";
            }
            else if (basedOn == "N")
            {
                basedOn = "NSR_Price";
            }
            else if (basedOn == "D")
            {
                basedOn = "Distributor_Price";
            }

            strQry = "select isnull(rtrim(MRP_Price),0) as MRP_Price, isnull(rtrim(Retailor_Price),0) as R_Price, isnull(rtrim(Distributor_Price),0) as Distributor_Price, isnull(rtrim(Target_Price),0) as Target_Price, isnull(rtrim(NSR_Price),0) as NSR_Price, p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,  isnull(rtrim(" + basedOn + "),0) Retailor_Price " +
                     "From Mas_Product_Detail p left outer   " +
                    " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +
                    "Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                    "where state_code='" + st_code + "' and division_code='" + divcode + "')" +
                    "and R.state_code = '" + st_code + "' where Product_Active_Flag=0 and p.Division_code='" + divcode + "' and Product_Mode='Sale'";
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

        public DataSet uu(string sf_code, string divcode, string dc, string transYear, string transMonth, string rt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  select Product_Quantity  from Trans_DCR_BusinessEntry_Details a inner join Trans_DCR_BusinessEntry_Head b  on a.Division_Code=b.Division_Code and a.Trans_sl_No=b.Trans_sl_No where  b.Sf_Code='" + sf_code + "'  and a.Division_Code ='" + divcode + "' and a.ListedDrCode='" + dc + "' and b.Trans_Month='" + transMonth + "' and b.Trans_Year='" + transYear + " 'and a.Product_Code='" + rt + "'";

            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift'" +
            //            " else case when Gift_Type = '3' then 'Doctor Kit'" +
            //            "else 'Ordinary Gift' " +
            //            "end end end as Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Active_Flag='0'";
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

        public DataSet uuu(string sf_code, string divcode, string dc, string transYear, string transMonth, string rt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  select value  from Trans_DCR_BusinessEntry_Details a inner join Trans_DCR_BusinessEntry_Head b  on a.Division_Code=b.Division_Code and a.Trans_sl_No=b.Trans_sl_No where  b.Sf_Code='" + sf_code + "'  and a.Division_Code ='" + divcode + "' and a.ListedDrCode='" + dc + "' and b.Trans_Month='" + transMonth + "' and b.Trans_Year='" + transYear + " 'and a.Product_Code='" + rt + "'";

            //strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
            //            " case when Gift_Type = '1' then 'Literature/Lable' " +
            //            " else case when Gift_Type = '2' then 'Special Gift'" +
            //            " else case when Gift_Type = '3' then 'Doctor Kit'" +
            //            "else 'Ordinary Gift' " +
            //            "end end end as Gift_Type" +
            //            " FROM Mas_Gift  WHERE Division_Code=" +
            //            "'" + divcode + "' AND Gift_Active_Flag='0'";
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
        public DataSet getProduct_Sale(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT '0' as Product_Detail_Code,'---Select the Product---' as Product_Detail_Name union all " +
                     "SELECT '-1' as Product_Detail_Code,'All Product' as Product_Detail_Name union all " +
                     "SELECT Product_Detail_Code,Product_Detail_Name  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";

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

        public int Brd_RecordUpdate_new(int ProBrdCode, string Product_Brd_SName, string Product_Brd_Name, string divcode, string Type)
        {
            int iReturn = -1;
            if (!RecordExistbrd(ProBrdCode, Product_Brd_SName, divcode))
            {
                if (!nRecordExist(ProBrdCode, Product_Brd_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Product_Brand " +
                                 " SET Product_Brd_SName = '" + Product_Brd_SName + "', " +
                                 " Product_Brd_Name = '" + Product_Brd_Name + "' ," +
                                 " Type ='" + Type + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Brd_Code = '" + ProBrdCode + "' and Product_Brd_Active_Flag = 0 ";

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
        public DataSet FetchProduct(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsField = null;

            strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            dsField = db_ER.Exec_DataSet(strQry);

            string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsPro = null;

            strQry = " SELECT '0' as ourBrndCode,'---Select---' as Product_Detail_Name, '---Select---' as Product_Description " +
                     " UNION " +
                     "SELECT Product_Code_SlNo AS ourBrndCode, Product_Detail_Name, Product_Description FROM " +
                    " Mas_Product_Detail WHERE Division_Code= '" + div_code + "' AND " +
                    " subdivision_code like '%" + subdivision_code + "'+'%' AND State_Code like '%" + State_Code + "'+','+'%' AND " +
                    " Product_Active_Flag=0 order by Product_Detail_Name";
            try
            {
                dsPro = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsPro;
        }
        public DataSet getProdByDivSubDState(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsField = null;

            strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            dsField = db_ER.Exec_DataSet(strQry);

            string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsProCat = null;

            strQry = "SELECT Product_Code_SlNo, Product_Detail_Name FROM " +
                    " Mas_Product_Detail WHERE Division_Code= '" + div_code + "' AND " +
                    " subdivision_code like '%" + subdivision_code + "'+'%' AND State_Code like '%" + State_Code + "'+','+'%' AND " +
                    " Product_Active_Flag=0 order by Product_Detail_Name";
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

        public DataSet Get_Business(string div_Code, string dr_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = "  select distinct product_code from Trans_DCR_BusinessEntry_Details d, " +
                     "  Trans_DCR_BusinessEntry_head h  where h.division_code='" + div_Code + "' and " +
                     "  h.listeddrcode='" + dr_code + "'  and sf_code='" + sf_code + "' and d.Trans_sl_No = h.Trans_sl_No ";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet Get_Business_Det(string div_code, string ListedDrCode, int cmonth, int cyear, string prodcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "  select product_code,Product_Quantity,Value from Trans_DCR_BusinessEntry_Details d, " +
                     "  Trans_DCR_BusinessEntry_head h  where h.division_code='" + div_code + "' and " +
                     "  h.listeddrcode='" + ListedDrCode + "'  and sf_code='" + sf_code + "' and d.Trans_sl_No = h.Trans_sl_No and trans_month=" + cmonth + " and trans_year=" + cyear + " and product_code='" + prodcode + "' ";



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
        public int UpdateProductRate_NewProduct(string prod_code, string state_code, string div_code)
        {
            int iReturn = -1;

            int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                iSlNo = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO mas_Product_State_Rates (Sl_No, Max_State_Sl_No, State_Code, Product_Detail_Code, MRP_Price, Retailor_Price, " +
                         " Distributor_Price, Target_Price, NSR_Price, Effective_From_Date, Division_Code, Created_Date,LastUpdt_Date) VALUES " +
                         " ( '" + iSlNo + "', 1, '" + state_code + "', '" + prod_code + "', 0, 0, 0, " +
                         " 0, 0, getdate(), '" + div_code + "', getdate(),getdate() ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int GetProductCode_Rate()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataTable getProductRate_dt(string state_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsRate = null;
            strQry = "EXEC Product_stateWise_Rate_View_Upl '" + state_code + "', '" + div_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }
        public DataSet GetProdRate_mntwise(string div_code, string state_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = " select distinct d.product_detail_code,p.Product_Detail_Name,p.Product_Description,p.Product_Sale_Unit,max(MRP_Price) as MRP_Price, " +
                         " max(Retailor_Price)  as Retailor_Price,max(Distributor_Price) as Distributor_Price,max(Target_Price) as Target_Price ,max(NSR_Price) as NSR_Price  from Trans_SS_Entry_Detail d,Trans_SS_Entry_Head s, Trans_SS_Entry_Detail_Value v," +
                         " Mas_Stockist st ,Mas_Product_Detail p      where s.division_code='" + div_code + "' and s.state_code='" + state_code + "' " +
                         " and s.Month= '" + imonth + "' and s.Year = '" + iyear + "'  and d.SS_Head_Sl_No = s.SS_Head_Sl_No " +
                         " and d.SS_Det_Sl_No=v.SS_Det_Sl_No " +
                         //  " and v.Sec_Sale_Code in(select Sec_Sale_Code from Mas_Sec_Sale_Setup where  Division_Code='"+div_code+"' and Sale_Calc='1' ) " +
                         " and s.Stockiest_Code=st.Stockist_Code and d.product_detail_code !='Tot_Prod' " +
                         " and d.product_detail_code=p.product_detail_code " +
                         " group by d.product_detail_code,p.Product_Detail_Name,p.Product_Description,p.Product_Sale_Unit " +
                         " order by d.product_detail_code ";
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
        }
        public DataSet getDocPrdDistPrice(string prd_code, string divcode, string st_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            //strQry = "EXEC  SP_Proc_Product_Statewise_Detail '" + divcode + "', '" + st_code + "' ";

            //'7', '8'
            strQry = "select  p.Product_Code_SlNo,product_Detail_Name,Product_Sale_Unit,  isnull(rtrim(Distributor_Price),0) Distributor_Price " +
                     "From Mas_Product_Detail p left outer   " +
                    " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +
                    "Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                    "where state_code='" + st_code + "' and division_code='" + divcode + "')" +
                    "and R.state_code = '" + st_code + "' where p.Product_Code_SlNo='" + prd_code + "' AND Product_Active_Flag=0 and p.Division_code='" + divcode + "'  ";
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

        public DataSet getProduct_Exp_new(string Division_Code, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            if (mode == "1")
            {

                strQry = "SELECT '0' as Product_Code_SlNo,'---Select the Product---' as Product_Detail_Name union all " +
                         "SELECT '-1' as Product_Code_SlNo,'All Product' as Product_Detail_Name union all " +
                         "SELECT Product_Code_SlNo,Product_Detail_Name  " +
                         " FROM Mas_Product_Detail " +
                         " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";
            }
            else if (mode == "2")
            {
                strQry = "SELECT '0' as Product_Code_SlNo,'---Select the Campaign---' as Product_Detail_Name union all " +
                         "SELECT '-1' as Product_Code_SlNo,'All Product' as Product_Detail_Name union all " +
                        " select distinct Product_Code_SlNo,Product_Detail_Name from mas_product_detail a,Mas_Doc_SubCategory b " +
                         " where a.division_code='" + Division_Code + "' and  charindex(cast(Product_Code_SlNo as varchar),Product_Code) > 0";
            }

            else if (mode == "3")
            {
                strQry = "SELECT '0' as Product_Code_SlNo,'---Select the Brand---' as Product_Detail_Name union all " +
                         " SELECT '-1' as Product_Code_SlNo,'All Brand' as Product_Detail_Name union all " +
                         " SELECT Product_Brd_Code as Product_Code_SlNo,Product_Brd_Name as Product_Detail_Name FROM Mas_Product_Brand " +
                         " where Division_Code = '" + Division_Code + "' and Product_Brd_Active_Flag=0 ";
            }

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

        #region RCPA
        public DataSet getEmptyRCPA()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT TOP 5 '' CompCode,'' CompName,'' CompPCode, '' CompPName, '' CPQty, '' CPRate, '' CPValue " +
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
        public DataSet getRCPADoc(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtProCat = null;

            strQry = "SELECT DISTINCT(DrCode) FROM Trans_RCPA_Head where SF_Code = '" + SF_Code + "' ";

            try
            {
                dtProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public DataSet getRCPAEntryView(string Sf_Code, string ListedDrCode, string PrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtProCat = null;

            strQry = "SELECT h.*,d.* FROM Trans_RCPA_Head h inner join Trans_RCPA_Detail D on d.FK_PK_ID = h.PK_ID " +
                    " where h.SF_Code='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode='" + PrdCode + "'";

            try
            {
                dtProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public DataSet getRCPAPrdComp(string Sf_Code, string ListedDrCode, string PrdCode, string CompCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtProCat = null;

            strQry = "SELECT h.*,d.* FROM Trans_RCPA_Head h inner join Trans_RCPA_Detail D on d.FK_PK_ID = h.PK_ID " +
                    " where h.SF_Code='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode='" + PrdCode + "' AND  " +
                    " d.CompCode='" + CompCode + "' ";

            try
            {
                dtProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public DataSet Trans_RCPA_HeadExist(string Sf_Code, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT * FROM Trans_RCPA_Head where SF_Code='" + Sf_Code + "' AND DrCode='" + ListedDrCode + "'";

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
        public DataSet Trans_RCPA_Product(string Sf_Code, string ListedDrCode, string PrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT * FROM Trans_RCPA_Head where SF_Code='" + Sf_Code + "' AND DrCode='" + ListedDrCode + "' AND OPCode='" + PrdCode + "' ";

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
        public DataSet Trans_RCPA_DetailExist(string CompCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT * FROM Trans_RCPA_Detail where CompCode = '" + CompCode + "'";

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
        public DataSet Trans_RCPA_PrdComp(string Sf_Code, string ListedDrCode, string PrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT h.*,d.* FROM Trans_RCPA_Head h inner join Trans_RCPA_Detail D on d.FK_PK_ID = h.PK_ID " +
                    " where h.SF_Code='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode='" + PrdCode + "'";

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

        public int RecordAddRCPA_Head(string Sf_Code, string sf_Name, string DrCode, string DrName, string ChmCode, string ChmName,
            string OPCode, string OPName, decimal OPQty, decimal OPRate, decimal OPValue)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                decimal PK_ID;

                strQry = "SELECT ISNULL(MAX(PK_ID),0)+1 FROM Trans_RCPA_Head";
                PK_ID = db.Exec_Scalar(strQry);

                strQry = "";
                strQry = "INSERT INTO Trans_RCPA_Head(PK_ID,SF_Code,SF_Name,RCPA_Date,DrCode,DrName,ChmCode,ChmName, " +
                        " OPCode,OPName,OPQty,OPRate,OPValue) " +
                        " VALUES ('" + PK_ID + "','" + Sf_Code + "','" + sf_Name + "', getDate(), '" + DrCode + "', '" + DrName + "', " +
                        " '" + ChmCode + "', '" + ChmName + "', '" + OPCode + "', '" + OPName + "', '" + OPQty + "', '" + OPRate + "', '" + OPValue + "')";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAddRCPA_Details(string Sf_Code, string ListedDrCode, string PrdCode, string CompCode, string CompName, string CompPName,
                decimal CPQty, decimal CPRate, decimal CPValue)
        {
            int iReturn = -1;
            try
            {
                decimal FK_PK_ID = -1;

                DB_EReporting db = new DB_EReporting();

                DataSet dsFK_PK_ID = Trans_RCPA_Product(Sf_Code, ListedDrCode, PrdCode);

                if (dsFK_PK_ID.Tables[0].Rows.Count > 0)
                {
                    FK_PK_ID = Convert.ToInt32(dsFK_PK_ID.Tables[0].Rows[0]["PK_ID"]);
                }

                strQry = "";
                strQry = "INSERT INTO Trans_RCPA_Detail(FK_PK_ID,CompCode, CompName,CompPName,CPQty,CPRate,CPValue) " +
                        " VALUES ('" + FK_PK_ID + "','" + CompCode + "','" + CompName + "','" + CompPName + "', '" + CPQty + "', '" + CPRate + "', '" + CPValue + "')";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordUpdRCPA_Head(string Sf_Code, string DrCode, string OPCode, string ChmCode, string ChmName,
            decimal OPQty, decimal OPValue)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                DateTime Updated_Date = DateTime.Now;
                string sqlUpdated_Date = Updated_Date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                strQry = "";
                strQry = "UPDATE Trans_RCPA_Head SET ChmCode='" + ChmCode + "', ChmName = '" + ChmName + "', OPQty='" + OPQty + "', " +
                        " OPValue='" + OPValue + "', UpdatedOn='" + sqlUpdated_Date + "' WHERE " +
                        " Sf_Code ='" + Sf_Code + "' AND DrCode='" + DrCode + "' AND OPCode ='" + OPCode + "'";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordUpdRCPADetails(string SF_Code, string DrCode, string OPCode, string CompCode, string CompName, string hdnCompName, string CompPName,
                decimal CPQty, decimal CPRate, decimal CPValue)
        {
            int iReturn = -1;
            try
            {
                decimal FK_PK_ID = -1;

                DB_EReporting db = new DB_EReporting();
                DataSet dsFK_PK_ID = Trans_RCPA_Product(SF_Code, DrCode, OPCode);

                if (dsFK_PK_ID.Tables[0].Rows.Count > 0)
                {
                    FK_PK_ID = Convert.ToInt32(dsFK_PK_ID.Tables[0].Rows[0]["PK_ID"]);
                }

                strQry = "";
                strQry = "UPDATE d SET d.CompCode='" + CompCode + "', d.CompName='" + CompName + "',d.CompPName='" + CompPName + "', CPQty='" + CPQty + "', " +
                    " CPRate='" + CPRate + "',CPValue='" + CPValue + "' FROM " +
                    " Trans_RCPA_Detail d INNER JOIN Trans_RCPA_Head h ON d.FK_PK_ID = h.PK_ID " +
                    " WHERE h.Sf_Code ='" + SF_Code + "' AND h.DrCode='" + DrCode + "' AND h.OPCode='" + OPCode + "' AND " +
                    " d.FK_PK_ID='" + FK_PK_ID + "' AND d.CompCode='" + hdnCompName + "' ";

                if (strQry != "")
                    iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Delete_RCPAEntry(string Sf_Code, string ListedDrCode, string OPCode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "  DELETE d FROM Trans_RCPA_Detail d LEFT JOIN Trans_RCPA_Head h " +
                         " ON d.FK_PK_ID = h.PK_ID WHERE h.Sf_Code ='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode='" + OPCode + "' " +
                         " DELETE h FROM Trans_RCPA_Head h LEFT JOIN Trans_RCPA_Detail d ON h.PK_ID = d.FK_PK_ID " +
                         " WHERE h.Sf_Code ='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode='" + OPCode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int Delete_RCPACompDetails(string Sf_Code, string ListedDrCode, string OPCode, string CompName)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE d FROM Trans_RCPA_Detail d INNER JOIN Trans_RCPA_Head h " +
                    " ON d.FK_PK_ID = h.PK_ID WHERE h.Sf_Code ='" + Sf_Code + "' AND h.DrCode='" + ListedDrCode + "' AND h.OPCode= '" + OPCode + "' " +
                    " AND d.CompName='" + CompName + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getRCPA(string drNameRCPA, string fieldForce, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtProCat = null;

            strQry = "SELECT SF_code,  EntryDt, RCPADt, Chemists_Code, ListedDrCode, CmptrName, CmptrBrnd, " +
                        " CmptrPriz, SUBSTRING(ourbrnd,0,PATINDEX('%(%', ourbrnd)) AS ourBrndCode, SUBSTRING(ourBrndNm,0,PATINDEX('%(%', ourBrndNm)) AS ourBrndNm, SUBSTRING(ourbrnd, CHARINDEX('(', ourbrnd), CHARINDEX(')', ourbrnd)) AS OurQty, " +
                        " Division_Code, CmptrQty, CmptrPOB, ChmName, DrName FROM TbRCPADetails " +
                        " where DrName = '" + drNameRCPA + "' AND SF_code = '" + fieldForce + "' AND Division_Code = '" + div_code + "'";

            try
            {
                dtProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public DataSet getRCPAView(string ourBrndCode, string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtProCat = null;

            strQry = "SELECT SF_code,CmptrName,CmptrBrnd,CmptrPriz," +
                " SUBSTRING(ourBrndNm,0,PATINDEX('%(%', ourBrndNm)) AS ourBrndNm, " +
                " SUBSTRING(ourBrnd, CHARINDEX('(', ourBrnd), CHARINDEX(')', ourBrnd)) AS OurQty," +
                " CmptrQty,CmptrPOB,ChmName,DrName FROM TbRCPADetails  where SUBSTRING(ourBrnd,0,PATINDEX('%(%', ourBrnd)) in (" + ourBrndCode + ") and SF_code='" + sf_Code + "' " +
                " order by SF_code, ourBrndNm ";

            try
            {
                dtProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtProCat;
        }
        public int RecordAddRCPA(string sf_Code, string Chemists_Code, string ListedDrCode, string CmptrName, string CmptrBrnd, string CmptrPriz, string OurQty, string OurPrice, string ourBrndCode, string ourBrndNm, string CmptrQty, string ChmName, string DrName)
        {
            int iReturn = -1;

            if (sf_Code != string.Empty)
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;

                    //DrName = DrName.Split('-')[0];

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_Code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);
                    Division_Code.ToString().Replace(",", "");

                    strQry = "insert into TbRCPADetails (SF_code, EntryDt, RCPADt, Chemists_Code, ListedDrCode, CmptrName, " +
                             "CmptrBrnd, CmptrPriz, ourBrnd, ourBrndNm, Remark, Division_Code, CmptrQty, " +
                             "CmptrPOB, ChmName, DrName) " +
                             " VALUES('" + sf_Code + "', getdate(),  getdate(), '" + Chemists_Code + "', '" + ListedDrCode + "', " +
                             " '" + CmptrName + "', '" + CmptrBrnd + "','" + CmptrPriz + "', '" + ourBrndCode + "' + ' ' + '(' + '" + OurQty + "' + ')', '" + ourBrndNm + "' + ' ' + '(' + '" + OurQty + "' + ')', '', " +
                             " '" + Division_Code + "', '" + CmptrQty + "', Convert(FLOAT, '" + OurPrice + "'), '" + ChmName + "', '" + DrName + "')";


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
        public int RecordUpdateRCPA(string sf_Code, string DrName)
        {
            int iReturn = -1;

            if (sf_Code != string.Empty)
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_Code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    strQry = "DELETE FROM TbRCPADetails WHERE DrName = '" + DrName + "' AND SF_code = '" + sf_Code + "' AND Division_Code = '" + Division_Code + "'";

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

        public DataSet getMapCompetitorProduct(string CompSlNo, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC spMapCompetitorProduct '" + CompSlNo + "','" + divcode + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        #endregion
        //
        public DataSet Product_Det_drs(string div_code, string sf_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = " select sum(cntT)cnt from ( select  Product_Detail_Name, COUNT(distinct c.Trans_Detail_Info_Code) cntT   from dcrmain_trans b, " +
                         " DCRDetail_Lst_Trans c , Mas_ListedDr e,mas_product_detail p where  b.Trans_SlNo = c.Trans_SlNo  " +
                         " and charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+c.Product_Code) > 0 and c.Trans_Detail_Info_Type=1 " +
                         " and c.Trans_Detail_Info_Code =e.ListedDrCode  and month(b.Activity_Date)='" + imonth + "' and e.Sf_Code=b.Sf_Code and " +
                         " p.division_code='" + div_code + "' and Product_Active_Flag=0   and b.division_code='" + div_code + "' and " +
                         " YEAR(b.Activity_Date)= '" + iyear + "'  and b.sf_code IN('" + sf_code + "') group by Product_Detail_Name  ) a ";
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
        }

        public DataSet top_Product_Det_drs(string div_code, string sf_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = " select distinct Product_Detail_Name, COUNT(distinct c.Trans_Detail_Info_Code) cntT into #tt  from dcrmain_trans b, " +
                         " DCRDetail_Lst_Trans c , Mas_ListedDr e,mas_product_detail p where  b.Trans_SlNo = c.Trans_SlNo " +
                         " and charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+c.Product_Code) > 0 and c.Trans_Detail_Info_Type=1  " +
                         " and c.Trans_Detail_Info_Code =e.ListedDrCode  and month(b.Activity_Date)='" + imonth + "' and e.Sf_Code=b.Sf_Code and " +
                         " p.division_code='" + div_code + "' and Product_Active_Flag=0   and b.division_code='" + div_code + "' and " +
                         " YEAR(b.Activity_Date)= '" + iyear + "'   and b.sf_code IN('" + sf_code + "') group by Product_Detail_Name   " +
                         " select  top 5 cntT,Product_Detail_Name from #tt " +
                         " order by cntT desc " +
                         "  drop table  #tt";
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
        }
        public DataSet Rx_drs(string div_code, string sf_code, string imonth, string iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProCat = null;
            {
                strQry = " select B.Sf_Code AS sf_code, count(distinct Trans_Detail_Info_Code ) cnt from dcrmain_trans b,DCRDetail_Lst_Trans c , " +
                         " mas_product_detail e  where b.Trans_SlNo = c.Trans_SlNo and " +
                         " charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ c.Product_Code) > 0  " +
                         " and  charindex('#'+cast(Product_Code_SlNo as varchar)+'~','#'+ Product_Code) > 0 " +
                         " and (charindex('$$','#'+ Product_Code+'#') <= 0 and charindex('$0$','#'+ Product_Code+'#') <= 0) " +
                         " and (charindex('$#','#'+ Product_Code+'#') <= 0 and charindex('$0#','#'+ Product_Code+'#') <= 0) " +
                         " and month(b.Activity_Date)='" + imonth + "' and YEAR(b.Activity_Date)= '" + iyear + "' and e.division_code='" + div_code + "'  and b.division_code='" + div_code + "' " +
                         " and Product_Active_Flag=0  and b.sf_code in ('" + sf_code + "') group by b.Sf_Code";

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
        }
        public DataSet Get_Rcpa_Products(string div_Code, string dr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = "select distinct ourbrnd,ourBrndNm,CmptrPOB from TbRCPADetails where division_code='" + div_Code + "' and listeddrcode='" + dr_code + "' ";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet Get_Rcpa_Products_Det(string div_Code, string dr_code, string prod_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = "select CmptrName,CmptrBrnd,CmptrPriz,ourBrnd,ourBrndNm,CmptrQty,CmptrPOB,ChmName from TbRCPADetails where division_code='" + div_Code + "' and listeddrcode='" + dr_code + "' and sf_code='" + sf_code + "' and ourbrnd='" + prod_code + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet getProduct_Trans(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT '0' as Product_Code_SlNo,'--Select--' as Product_Detail_Name union all " +
                     "SELECT Product_Code_SlNo,Product_Detail_Name  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";

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
        public int RecordAdd_Prod_Auto_Code(string Product_Detail_Name, string Product_Sale_Unit, string Product_Sample_Unit_One, string Product_Sample_Unit_Two, string Product_Sample_Unit_Three, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, int Division_Code, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code)
        {
            int iReturn = -1;

            if (!sRecordExistdet(Product_Detail_Name, Division_Code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "EXEC Product_Add_Rollback '" + @Product_Detail_Name + "', '" + @Product_Sale_Unit + "', '" + @Product_Sample_Unit_One + "', '" + @Product_Sample_Unit_Two + "','" + @Product_Sample_Unit_Three + "'," + @Product_Cat_Code + " ," + @Product_Grp_Code + ",'" + @Product_Type_Code + "','" + @Product_Description + "'," + @Division_Code + " ,'" + @state + "','" + @sub_division + "','" + @mode + "','" + @sample + "','" + @sale + "'," + @Product_Brd_Code + " ";

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
        public DataSet getProdBrd_Name(string divcode, string Product_Brd_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_Code,Product_Brd_SName,Product_Brd_Name FROM  Mas_Product_Brand " +
                     " WHERE Product_Brd_Name= '" + Product_Brd_Name + "'AND Division_Code= '" + divcode + "' and Product_Brd_Active_Flag=0 " +
                     " ORDER BY 2";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet GetMnthPonCont(string sf_code, string iMonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC GetMnthPonCont '" + sf_code + "','" + iMonth + "','" + iYear + "'";

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
        public DataSet getProdRate_all(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT inn.product_Detail_Code, inn.product_Detail_Name,inn.Product_Sale_Unit, inn.MRP_Price, inn.Retailor_Price, inn.Distributor_Price, " +
                     " inn.NSR_Price, inn.Target_Price , inn.Sample_Price,inn.Division_code,inn.Product_Active_Flag FROM " +
                     " (SELECT t.product_Detail_Code, t.product_Detail_Name,t.Product_Sale_Unit,isnull(rtrim(MRP_Price),0) MRP_Price, " +
                     " isnull(rtrim(Retailor_Price),0) Retailor_Price,isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                     " isnull(rtrim(NSR_Price),0) NSR_Price,isnull(rtrim(Target_Price),0) Target_Price,isnull(rtrim(Sample_Price),0) Sample_Price," +
                     " t.Division_code,Product_Active_Flag, ROW_NUMBER() OVER(PARTITION BY t.product_Detail_Code  ORDER BY Distributor_Price) num " +
                     " FROM  Mas_Product_Detail t left outer join    Mas_Product_State_Rates R  on  R.product_Detail_code=t.Product_Detail_code " +
                     " ) inn WHERE Division_code='" + div_code + "' and Product_Active_Flag=0   and inn.num=1;";
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

        public DataSet GetBrandProduct(string divcode, string BrdValue, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " EXEC GetBrandProduct '" + divcode + "','" + BrdValue + "','" + Sub_Div + "' ";
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
        public DataSet Get_Goddress_PaySlip(string sf_Code, string div_Code, string Month, string Year, string tblDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserData = null;


            strQry = "select * from dbo.PaySlip_" + tblDivCode + " P inner join dbo.Mas_Salesforce S on P.Employee_Code=S.sf_emp_id " +
                     " inner join dbo.mas_subdivision D on LEFT(S.subdivision_code, CHARINDEX(',', S.subdivision_code + ',') - 1)=D.subdivision_code " +
                     " where Paymonth='" + Month + "' and Payyear='" + Year + "' and Division_Code='" + div_Code + "' and Sf_Code='" + sf_Code + "'";

            try
            {
                dsUserData = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserData;

        }

        public int Save_Slide(string Brand_Name, string Img_Name, string Priority, string Div_Code)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC Save_Slide_Main '" + Brand_Name + "','" + Img_Name + "','" + Priority + "','" + Div_Code + "'";

            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public DataSet GetSelect(string division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC GetSelect '" + division_code + "'";

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

        public DataSet GetProductSlidePriorty(string division_code, string Doc_Special_Code, string Mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC GetProductSlidePriorty '" + division_code + "','" + Doc_Special_Code + "','" + Mode + "'";

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
        public DataSet getProdRate_all_Latest(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT  t.product_Detail_Code, t.product_Detail_Name,t.Product_Sale_Unit,max(isnull(rtrim(MRP_Price),0)) MRP_Price " +
                     " ,max(isnull(rtrim(Retailor_Price),0)) Retailor_Price " +
                     " ,max(isnull(rtrim(Distributor_Price),0)) Distributor_Price,  " +
                     " max(isnull(rtrim(NSR_Price),0)) NSR_Price,max(isnull(rtrim(Target_Price),0)) Target_Price,max(isnull(rtrim(Sample_Price),0)) Sample_Price, " +
                     " t.Division_code,Product_Active_Flag,Sale_Erp_Code,Sample_Erp_Code " +
                     " FROM  Mas_Product_Detail t left join  Mas_Product_State_Rates R  on  R.product_Detail_code=t.Product_Detail_code " +
                     " WHERE t.Division_code='" + div_code + "' and Product_Active_Flag=0  " +
                     " group by t.product_Detail_Code, t.product_Detail_Name,t.Product_Sale_Unit ,t.Division_code,Product_Active_Flag,t.Prod_Detail_Sl_No,Sale_Erp_Code,Sample_Erp_Code " +
                     " order by Prod_Detail_Sl_No Asc ";
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
        public DataSet Process_UserList_jod(string div_Code, string Sf_Code, string imonth, string iyear, string st)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;


            //sp_UserList_Process

            //sp_SalesForceMgrGet

            strQry = " EXEC [dbo].[UserList_Process_Quiz_Jod] '" + div_Code + "', '" + Sf_Code + "', '" + imonth + "','" + iyear + "','" + st + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }

        #region Slide_Upload
        public DataSet RecordExists(string divcode, string brandCode, string subCode, string filename)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select Product_Detail_Code, Product_Detail_Name, Doc_Special_Code, " +
                        " Doc_Special_Name, Product_Grp_Code, Product_Grp_Name, Subdivision_code  " +
                        " from Product_Image_List " +
                        " where Division_code='" + divcode + "'" +
                        " AND Product_Brand_Code = '" + brandCode + "' " +
                        " AND Img_Name='" + filename + "' AND ',' + Subdivision_code like '%," + subCode + ",%'";

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
        public DataSet GetProductBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_GetProductBrand '" + divcode + "' ";

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

        public DataSet GetImageFile(string divcode, string strBrand, string SubDiv, string Product, string Spec, string Therapy)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "Exec sp_GetImages '" + divcode + "','" + strBrand + "','" + SubDiv + "', " + Product + ", " + Spec + ", " + Therapy + " ";

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

        public DataSet GetPriorityView(string divcode, string mode, string strBrand, string SubDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "Exec sp_GetPriorityList '" + divcode + "', '" + mode + "','" + strBrand + "','" + SubDiv + "' ";

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

        public DataSet GetSlidePriority(string divcode, string strBrand, string SubDiv, string modeVal, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "Exec sp_GetImgSlidePriority '" + divcode + "','" + strBrand + "'," + SubDiv + ", " + modeVal + "," + mode + "  ";

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

        public int Delete_Img(string SI_No)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC sp_Upd_Prd_Del '" + SI_No + "'";
            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public int Update_Common(string SLNO, string Cmn)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC sp_Update_Common '" + SLNO + "', '" + Cmn + "'";
            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public int Update_Mas_Priority(string div_code, string code, int Priority, string mode, string subDiv, string modeVal)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC sp_Upd_Mas_Priority '" + div_code + "', '" + code + "', " + Priority + ", '" + mode + "', '" + modeVal + "', '" + subDiv + "'";
            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public int Update_Slide_Priority(string div_code, string imgSlno, int Priority, string mode, string subDiv, string modeVal, string BrndVal)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC sp_Upd_Slide_Priority '" + div_code + "', '" + imgSlno + "', " + Priority + ", '" + mode + "', '" + modeVal + "', '" + subDiv + "', '" + BrndVal + "'";
            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public DataSet GetMultiBrandProduct(string divcode, string BrdValue, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " EXEC GetMultiBrandProduct '" + divcode + "','" + BrdValue + "','" + Sub_Div + "' ";
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

        public DataSet GetFileFolderPath(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC GetFileFolderPath '" + Div_Code + "'";

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

        public int Insert_Image(string Image_Name, string Product_Brand_Name, string Product_Brand_Code, string divcode, string SubDivision, string lPrdValue, string lPrdText, string lSpecValue, string lSpecText, string lTheraValue, string lTheraText)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC sp_Insert_Prd_Image '" + Image_Name + "','" + Product_Brand_Name + "','" + Product_Brand_Code + "','" + divcode + "','" + SubDivision + "','" + lPrdValue + "','" + lPrdText + "','" + lSpecValue + "','" + lSpecText + "','" + lTheraValue + "','" + lTheraText + "' ";

            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public int Update_Image(string Image_Sl_No, string Image_Name, string Product_Brand_Name, string Product_Brand_Code, string divcode, string SubDivision, string lPrdValue, string lPrdText, string lSpecValue, string lSpecText, string lTheraValue, string lTheraText, string Common)
        {
            int iReturn = -1;

            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC sp_Update_Prd_Image '" + Image_Sl_No + "', '" + Image_Name + "','" + Product_Brand_Name + "','" + Product_Brand_Code + "','" + divcode + "','" + SubDivision + "','" + lPrdValue + "','" + lPrdText + "','" + lSpecValue + "','" + lSpecText + "','" + lTheraValue + "','" + lTheraText + "', '" + Common + "' ";

            iReturn = db.ExecQry(strQry);

            return iReturn;
        }

        public DataSet getProd_Subdiv(string divcode, string subdiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Code_SlNo,a.Product_Sale_Unit,a.Product_Sample_Unit_One,a.Product_Description, " +
                     " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name,c.Product_Grp_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     "a.Product_Grp_Code = c.product_grp_code AND " +
                     "a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     "a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' and charindex(cast('" + subdiv + "' as varchar), a.subdivision_code)>0 " +
                     " ORDER BY Prod_Detail_Sl_No";
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

        public DataSet getGift_Subdiv(string divcode, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;


            strQry = "  SELECT Gift_Code,Gift_Name,Gift_SName,Gift_Value," +
                       " case when Gift_Type = '1' then 'Literature/Lable' " +
                       " else case when Gift_Type = '2' then 'Special Gift'" +
                       " else case when Gift_Type = '3' then 'Doctor Kit'" +
                       "else 'Ordinary Gift' " +
                       "end end end as Gift_Type" +
                       " FROM Mas_Gift  WHERE Division_Code=" +
                       "'" + divcode + "' AND Gift_Active_Flag='0'" +
                      " and (Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) and charindex(cast('" + sub_div + "' as varchar), subdivision_code)>0  " +
                    " ORDER BY 2";
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
        #endregion


        public DataSet ProductList_Detail(int div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProductList = null;

            strQry = "EXEC [dbo].[SP_ProductDetail_List_SubDivision] '" + div_code + "'";

            try
            {
                dsProductList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProductList;
        }

        public DataSet getProduct_SubDivisionwise_Det(int div_code, int sub_Division)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsProductState = null;

            strQry = "EXEC [dbo].[SP_Proc_Product_SubDivisionwise_Det] '" + div_code + "','" + sub_Division + "'";

            try
            {
                dsProductState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProductState;
        }

        public DataSet getProduct_SubDivisionSplit(int div_code, int sub_Division, int Product_SLNO)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStateSplit = null;
            //strQry = "select State_Code from Mas_Product_Detail where Product_Active_Flag=0 and Division_Code= '" + div_code + "' and subdivision_code='" + sub_Division + "'  and Product_Code_SlNo in (" + Product_SLNO + ")";

            strQry = "select subdivision_code from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Code_SlNo='" + Product_SLNO + "'";

            // strQry = "EXEC [dbo].[SP_Proc_Product_SubDivision_Split] '" + div_code + "','" + sub_Division + "'," + Product_SLNO + "";

            try
            {
                dsStateSplit = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStateSplit;
        }
        #region Dashboard_Slides_Analysis
        public DataSet getSlides(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlides '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", " +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideListedDr(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlideListedDr '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", " +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideTime(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlideTime '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", " +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideUsrLike(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlideUsrLike '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", " +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlidesListedDrs(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode, string slide)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesListedDrs '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", '" + slide + "', " +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlidesListedDrsUsrLike(string sf_code, string fDate, string tDate, string Doc_Special_Code,
            string Doc_Cat_Code, string Doc_QuaCode, string Doc_ClsCode, string Territory_Code, string drPrdBrd, string Div_Code, int mode, string slide, int usrLike)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesListedDrsUsrLike '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", '" + slide + "', '" + usrLike + "'," +
                    " " + Doc_Special_Code + "," + Doc_Cat_Code + "," + Doc_QuaCode + ", " + Doc_ClsCode + ", " + Territory_Code + ", " + drPrdBrd + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        #endregion
        public int Rec_ProductSubDivision_Update(string Product_SLNO, string SubDiv_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Product_Detail set subdivision_code='" + SubDiv_Code + "' where Product_Code_SlNo='" + Product_SLNO + "'";

                //strQry = "UPDATE Mas_Product_Detail set subdivision_code='" + SubDiv_Code + "',State_Code='" + State_Code + "' where Product_Code_SlNo='" + Product_SLNO + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public DataSet Process_UserList_Desig(string div_Code, string Sf_Code, string desig)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[UserList_Process_Quiz_Desig] '" + div_Code + "', '" + Sf_Code + "', '" + desig + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }
        public DataSet Process_UserList_Subdiv(string div_Code, string Sf_Code, string sub_div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUserList = null;
            strQry = " EXEC [dbo].[UserList_Process_Quiz_Subdiv] '" + div_Code + "', '" + Sf_Code + "', '" + sub_div + "'";

            try
            {
                dsUserList = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUserList;
        }

        //gowsi
        public DataSet getBrdBySelectedState(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            //DataSet dsField = null;

            //strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            //dsField = db_ER.Exec_DataSet(strQry);

            //string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            //string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsProCat = null;

            strQry = "select convert(decimal,Product_Brd_Code) as Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand where Product_Brd_Active_Flag=0 and Division_Code='" + div_code + "' ";
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
        public DataSet getCatBySelectedState(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            //DataSet dsField = null;

            //strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            //dsField = db_ER.Exec_DataSet(strQry);

            //string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            //string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsProCat = null;

            strQry = "select convert(decimal,Product_Cat_Code) as Product_Cat_Code,Product_Cat_Name from Mas_Product_Category where Product_Cat_Active_Flag=0 and Division_Code='" + div_code + "' ";
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

        public DataSet getGrpBySelectedState(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            //DataSet dsField = null;

            //strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            //dsField = db_ER.Exec_DataSet(strQry);

            //string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            //string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsProCat = null;

            strQry = "select convert(decimal,Product_Grp_Code) as Product_Grp_Code,Product_Grp_Name from Mas_Product_Group where Product_grp_Active_Flag=0 and Division_Code='" + div_code + "' ";
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
        public DataSet getSpecBySelectedState(string sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            //DataSet dsField = null;

            //strQry = "SELECT State_Code, subdivision_code FROM Mas_Salesforce where Sf_Code = '" + sf_Code + "' ";
            //dsField = db_ER.Exec_DataSet(strQry);

            //string State_Code = dsField.Tables[0].Rows[0]["State_Code"].ToString();
            //string subdivision_code = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();

            DataSet dsProCat = null;

            strQry = "select convert(decimal,Doc_Special_Code) as Doc_Special_Code,Doc_Special_Name from Mas_Doctor_Speciality where Doc_Special_Active_Flag=0 and Division_Code='" + div_code + "' ";
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


        #region Stk_Prod_ERP_Update

        public DataSet getStk_Edit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                    " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                    " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                    " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                    " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                    " ORDER BY Stockist_Name,State,Territory";
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

        public DataSet getStkall(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                     " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                     " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                     " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                     " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                     " ORDER BY Stockist_Name,State,Territory";
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

        public DataSet getStkforname(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                    " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                    " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                    " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                    " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                      " and d.Stockist_Name like '" + val + "%'  " +
                    " ORDER BY Stockist_Name,State,Territory";
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

        public DataSet getStkforHQ(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = "select * from ( select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
            //      " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
            //      " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
            //      " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
            //      " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
            //        "  )tt where SF_Cat_Code='" + val + "' " +
            //      " ORDER BY Stockist_Name,State,Territory";

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                   " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                   " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                   " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                   " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                     " and d.Territory like '" + val + "%'  " +
                   " ORDER BY Stockist_Name,State,Territory";

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


        public DataSet getStkforERP(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                    " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                    " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                    " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                    " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                      " and d.Stockist_Designation like '" + val + "%'  " +
                    " ORDER BY Stockist_Name,State,Territory";
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

        public DataSet getStkforState(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_Designation,case Territory when '--Select--' then '' else Territory end as Territory, " +
                    " case State when '---Select---' then '' else State end as State, Stockist_Address,Stockist_ContactPerson,Stockist_Mobile," +
                    " ( select distinct top 1 Approved_by from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) Approved_by, " +
                    " ( select distinct top 1 SF_Cat_Code from mas_salesforce b where charindex(','+cast(b.sf_code as varchar)+',',','+ d.sf_code) >0) SF_Cat_Code " +
                    " from Mas_Stockist d where Division_Code='" + divcode + "' and Stockist_Active_Flag=0 " +
                      " and d.State like '" + val + "%'  " +
                    " ORDER BY Stockist_Name,State,Territory";
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

        public int BulkEdit_Stk(string str, string Stockist_Code, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Stockist SET " + str + "  Where Stockist_Code='" + Stockist_Code + "' and Division_code='" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        #endregion

        //dr Potienal By Ferooz
        public DataSet getPotProdstadoctorBasedOn(string divcode, string st_code, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (basedOn == "M")
            {
                basedOn = "MRP_Price";
            }
            else if (basedOn == "T")
            {
                basedOn = "Target_Price";
            }
            else if (basedOn == "R")
            {
                basedOn = "Retailor_Price";
            }
            else if (basedOn == "N")
            {
                basedOn = "NSR_Price";
            }
            else if (basedOn == "D")
            {
                basedOn = "Distributor_Price";
            }

            strQry = "select isnull(rtrim(MRP_Price),0) as MRP_Price, isnull(rtrim(Retailor_Price),0) as R_Price, isnull(rtrim(Distributor_Price),0) as Distributor_Price, isnull(rtrim(Target_Price),0) as Target_Price, isnull(rtrim(NSR_Price),0) as NSR_Price, p.product_Detail_Code,product_Detail_Name,Product_Sale_Unit,  isnull(rtrim(" + basedOn + "),0) Retailor_Price " +
                     "From Mas_Product_Detail p left outer   " +
                    " join Mas_Product_State_Rates R on R.product_Detail_code=P.Product_Detail_code and " +
                    "Max_State_Sl_No in (select max(Max_State_Sl_No) from mas_Product_State_Rates " +
                    "where state_code='" + st_code + "' and division_code='" + divcode + "')" +
                    "and R.state_code = '" + st_code + "' where Product_Active_Flag=0 and p.Division_code='" + divcode + "'  ";
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
        public DataSet GetProductBrandSlide(string divcode, string SubDiv)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_GetProductBrandSlide '" + divcode + "','" + SubDiv + "' ";

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

        public DataSet RecordFileNameExists(string divcode, string filename)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "Select Img_Name,Flag " +
                        " from Product_Image_List " +
                        " where Division_code='" + divcode + "'" +
                        " AND Img_Name='" + filename + "' ";

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
        public DataSet SlideExistProduct(string ProdCode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            strQry = " SlideExistProduct '" + ProdCode + "','" + divcode + "' ";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }

        public DataSet SlideExistProGrp(string ProGrpCode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            strQry = " SlideExistProGrp '" + ProGrpCode + "','" + divcode + "' ";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }

        //
        // sample delete by Ferooz
        //
        public int RecordDeleteParticularSampleMR(string baseitem, string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Sample_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "'  AND Sf_Code='" + baseitem + "') ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' AND Sf_Code='" + baseitem + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordDeleteParticularSampleAdmin(string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Sample_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "') ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordDeleteParticularSample(string MultipleSf_code, string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Sample_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "'  AND Sf_Code in (" + MultipleSf_code + ")) ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Sample_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' AND Sf_Code in (" + MultipleSf_code + ") ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordDeleteParticularInputMR(string baseitem, string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Input_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "'  AND Sf_Code='" + baseitem + "') ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' AND Sf_Code='" + baseitem + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordDeleteParticularInputAdmin(string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Input_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "') ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int RecordDeleteParticularInput(string MultipleSf_code, string div_code, string ddlMonth, string ddlYear)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = " delete from Trans_Input_Despatch_Details WHERE Trans_sl_No in (select Trans_sl_No from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "'  AND Sf_Code in (" + MultipleSf_code + ")) ";
                iReturn = db.ExecQry(strQry);

                strQry = string.Empty;
                strQry = " delete from Trans_Input_Despatch_Head WHERE Division_Code='" + div_code + "' AND Trans_Month='" + ddlMonth + "' AND Trans_Year='" + ddlYear + "' AND Sf_Code in (" + MultipleSf_code + ") ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public DataSet getSlidesNew(string sf_code, string fDate, string tDate, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideListedDrNew(string sf_code, string fDate, string tDate, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlideListedDrNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideTimeNew(string sf_code, string fDate, string tDate, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesTimeNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlideUsrLikeNew(string sf_code, string fDate, string tDate, string Div_Code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlideUsrLikeNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlidesListedDrsNew(string sf_code, string fDate, string tDate, string Div_Code, int mode, string slide)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesListedDrsNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", '" + slide + "' " +
                    " ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getSlidesListedDrsUsrLikeNew(string sf_code, string fDate, string tDate, string Div_Code, int mode, string slide, int usrLike)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC getSlidesListedDrsUsrLikeNew '" + sf_code + "','" + fDate + "','" + tDate + "','" + Div_Code + "'," + mode + ", '" + slide + "', '" + usrLike + "'";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        #region Sum previous + current qty  by FErooz
        public DataSet getPrNew(string sf_code, string divcode, string transMonth, string transYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry =
                " select  b.Product_Code,b.Product_Sale_Unit,b.productc, a.Trans_sl_No,a.Sf_Code,a.Division_Code,a.Trans_Month,a.Trans_Year,b.Trans_sl_No,b.Division_Code,b.Despatch_Qty,b.Remarks FROM  Trans_Sample_Despatch_Head a inner join Trans_Sample_Despatch_Details b on a.Trans_sl_No=b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "'and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "'" +
                " UNION ALL" +
                "  SELECT a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Code_SlNo,'0' Trans_sl_No,''	Sf_Code,a.Division_Code,'0'	Trans_Month,'0'	Trans_Year,	'0'Trans_sl_No,a.Division_Code,''	Despatch_Qty,'' Remarks FROM  Mas_Product_Detail a,Mas_Product_Category b,Mas_Product_Group c,Mas_Product_Brand d WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code AND a.Product_Brd_Code = d.Product_Brd_Code AND a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                " and a.Product_Code_SlNo not in(select  b.productc FROM  Trans_Sample_Despatch_Head a inner join Trans_Sample_Despatch_Details b on a.Trans_sl_No=b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "'and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "')";

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

        public DataSet getGifteditNew(string sf_code, string divcode, string transMonth, string transYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "  select  b.Gift_Name,b.productc, a.Trans_sl_No,a.Sf_Code,a.Division_Code,a.Trans_Month,a.Trans_Year,b.Trans_sl_No,b.Division_Code,b.Despatch_Qty,b.Remarks FROM  Trans_Input_Despatch_Head a inner join Trans_Input_Despatch_Details b on a.Trans_sl_No=b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "' and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "' " +
              " UNION ALL " +

            "SELECT Gift_Name, Gift_Code,'0'Trans_sl_No,'' Sf_Code,Division_Code,'0'Trans_Month,'0'Trans_Year,'0'Trans_sl_No,Division_Code,''Despatch_Qty,'' Remarks FROM Mas_Gift WHERE Division_Code = '" + divcode + "' AND Gift_Active_Flag = '0' and(Gift_Effective_From >= Convert(Date, GetDate(), 101) or Gift_Effective_To >= Convert(Date, GetDate(), 101)) and Gift_Code not in(select b.productc FROM  Trans_Input_Despatch_Head a inner join Trans_Input_Despatch_Details b on a.Trans_sl_No = b.Trans_sl_No where  a.Division_Code='" + divcode + "'and  a.Trans_Month='" + transMonth + "' and a.Trans_Year='" + transYear + "'and a.Sf_Code='" + sf_code + "')";

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
        #endregion


        public DataSet getProductRatePriceBusiness(string sf_code, string div_code, string state, string subdiv, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (basedOn == "M")
            {
                basedOn = "MRP_Price";
            }
            else if (basedOn == "T")
            {
                basedOn = "Target_Price";
            }
            else if (basedOn == "R")
            {
                basedOn = "Retailor_Price";
            }
            else if (basedOn == "N")
            {
                basedOn = "NSR_Price";
            }
            else if (basedOn == "D")
            {
                basedOn = "Distributor_Price";
            }

            strQry = " select * from Mas_Product_Detail where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "' or state_code like '" + state + "') ";

            dsProCat = db_ER.Exec_DataSet(strQry);

            if (dsProCat.Tables[0].Rows.Count > 0)
            {
                if (subdiv.Contains(','))
                    subdiv = subdiv.Substring(0, subdiv.Length - 1);
                strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                    " b.Product_Description, " +
                    " b.Product_Detail_Name," +
                    " b.Product_Sale_Unit,  " +
                    " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                    " isnull(rtrim(Retailor_Price),0) R_Price, " +
                    " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                    " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                    " isnull(rtrim(Target_Price),0) Target_Price, " +
                    " isnull(rtrim(Sample_Price),0) Sample_Price, " +
                    " isnull(rtrim(" + basedOn + "),0) Retailor_Price " +
                    " From Mas_Product_Detail b" +
                    " INNER JOIN Mas_Product_State_Rates c  " +
                    " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                    " WHERE b.Division_Code= '" + div_code + "' " +
                    " AND b.Product_Active_Flag = 0" +
                    " AND c.state_code = '" + state + "' " +
                    " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%' or b.state_code like '%" + ',' + state + "') " +
                     " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "') " +
                    
                 "and b.Product_Mode='Sale' ";
            }

            else
            {
                strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
                          " b.Product_Sale_Unit,  '0' as MRP_Price,  '0'as Retailor_Price,'0' as R_Price,'0' as Distributor_Price, " +
                          " '0' as NSR_Price,'0' as Target_Price,'0' as Sample_Price  From Mas_Product_Detail b" +
                          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
                          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%'  or b.state_code like '%" + ',' + state + "') " +
                            " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "') " +
                         
               "and b.Product_Mode='Sale' ";

            }

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
        public DataSet getProductRatePriceBusiness_sampl_sale(string sf_code, string div_code, string state, string subdiv, string basedOn)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (basedOn == "M")
            {
                basedOn = "MRP_Price";
            }
            else if (basedOn == "T")
            {
                basedOn = "Target_Price";
            }
            else if (basedOn == "R")
            {
                basedOn = "Retailor_Price";
            }
            else if (basedOn == "N")
            {
                basedOn = "NSR_Price";
            }
            else if (basedOn == "D")
            {
                basedOn = "Distributor_Price";
            }

            strQry = " select * from Mas_Product_Detail where Division_Code = '" + div_code + "' and (state_code like '" + state + ',' + "%'  or state_code like '%" + ',' + state + ',' + "%' or state_code like '%" + ',' + state + "' or state_code like '" + state + "') ";

            dsProCat = db_ER.Exec_DataSet(strQry);

            if (dsProCat.Tables[0].Rows.Count > 0)
            {
                if (subdiv.Contains(','))
                    subdiv = subdiv.Substring(0, subdiv.Length - 1);
                strQry = " SELECT DISTINCT b.Product_Detail_Code, " +
                    " b.Product_Description, " +
                    " b.Product_Detail_Name," +
                    " b.Product_Sale_Unit,  " +
                    " isnull(rtrim(MRP_Price),0) MRP_Price,  " +
                    " isnull(rtrim(Retailor_Price),0) R_Price, " +
                    " isnull(rtrim(Distributor_Price),0) Distributor_Price, " +
                    " isnull(rtrim(NSR_Price),0) NSR_Price, " +
                    " isnull(rtrim(Target_Price),0) Target_Price, " +
                    " isnull(rtrim(Sample_Price),0) Sample_Price, " +
                    " isnull(rtrim(" + basedOn + "),0) Retailor_Price " +
                    " From Mas_Product_Detail b" +
                    " INNER JOIN Mas_Product_State_Rates c  " +
                    " ON b.Product_Detail_Code = c.Product_Detail_Code  " +
                    " WHERE b.Division_Code= '" + div_code + "' " +
                    " AND b.Product_Active_Flag = 0" +
                    " AND c.state_code = '" + state + "' " +
                    " AND (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%' or b.state_code like '%" + ',' + state + "') " +
                     " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "') " +
                     "and b.Product_Mode in( 'Sale/Sample', 'sale') ";
                    // "and b.Product_Mode='Sale' ";
            }

            else
            {
                strQry = " select distinct b.Product_Detail_Code,b.Product_Description,b.Product_Detail_Name," +
                          " b.Product_Sale_Unit,  '0' as MRP_Price,  '0'as Retailor_Price,'0' as R_Price,'0' as Distributor_Price, " +
                          " '0' as NSR_Price,'0' as Target_Price,'0' as Sample_Price  From Mas_Product_Detail b" +
                          " where  b.Division_Code= '" + div_code + "' and b.Product_Active_Flag = 0" +
                          " and  (b.state_code like '" + state + ',' + "%'  or b.state_code like '%" + ',' + state + ',' + "%'  or b.state_code like '%" + ',' + state + "') " +
                            " And (b.subdivision_code like '" + subdiv + ',' + "%'  or b.subdivision_code like '%" + ',' + subdiv + ',' + "%' or b.subdivision_code like '%" + ',' + subdiv + "') " +
                             "and b.Product_Mode in( 'Sale/Sample', 'sale') ";
                //"and b.Product_Mode='Sale' ";

            }

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
        public DataSet GetSampled_Product_Temp(string Div_Code, string Sf_Code, string Trans_Month, string Trans_Year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = "Exec Sample_Stock_Transfer_View '"+ Div_Code + "','"+ Sf_Code + "','"+ Trans_Month + "','"+ Trans_Year + "'";
            strQry = "EXEC sample_Des_Stock_Transfer '" + Div_Code + "', '" + Sf_Code + "', " + Trans_Month + "," + Trans_Year + "," + Trans_Month + "," + Trans_Year + " ";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public DataSet GetSampled_Product(string sf_Code, string Month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            strQry = "select b.Sl_No,b.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty,Sale_Erp_Code from [dbo].[Trans_Sample_Despatch_Head] a, " +
                                                      "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
                                                      "where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_Code + "'" +
                                                      "and b.productc=c.Product_Code_SlNo and a.Trans_Month='" + Month + "' and a.Trans_Year='" + year + "'";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public int Insert_Existing_Product_New(string Sf_Code, string div_Code, string month, string year)
        {
            int iReturn = -1;

            //int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                //iSlNo = db.Exec_Scalar(strQry);

                strQry = "insert into Trans_Sample_Despatch_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date,Updated_Date,Trans_month_year) " +
                    "values('" + Sf_Code + "','" + div_Code + "','" + month + "','" + year + "',getdate(),getdate(),getdate())";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getprdfor_Mappdr_Prioity(string Listeddr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select distinct Listeddr_Code,stuff((select  ',' + t1.Product_Code from Map_LstDrs_Product t1 " +
                    " where t1.Listeddr_Code = t.Listeddr_Code order by t1.Product_Priority " +
                    "for xml path('')),1,1,'') Product_priority_code FROM Map_LstDrs_Product t where Listeddr_Code = '" + Listeddr_Code + "' ";


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
        public DataSet getsampleErpcode(string samplecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select count(Sample_Erp_Code) cnt from Mas_Product_Detail where Sample_Erp_code='" + samplecode + "' ";


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
        public DataSet getsaleErpcode(string salecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;


            strQry = " select count(Sale_Erp_Code) cnt from Mas_Product_Detail where Sale_Erp_code='" + salecode + "' ";


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
        public DataSet GetInput_Product_Temp(string Div_Code, string Sf_Code)
        {
            DB_EReporting db_Er = new DB_EReporting();
            DataSet dsProGrp = null;
            strQry = "select SF_Code,a.Gift_Code,Gift_Name,b.Gift_SName,InputQty_AsonDate from Trans_Input_Stock_FFWise_AsonDate a,Mas_Gift b where a.division_Code='" + Div_Code + "' and sf_Code='" + Sf_Code + "' and a.Gift_Code=b.Gift_Code";
            try
            {
                dsProGrp = db_Er.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public DataSet GetAdjusmentBal_Input(string Div_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = "Exec Sample_Stock_Transfer_View '"+ Div_Code + "','"+ Sf_Code + "','"+ Trans_Month + "','"+ Trans_Year + "'";
            strQry = "select a.Gift_Code,b.Gift_SName,b.Gift_Name,InputQty_AsonDate from Trans_Input_Stock_FFWise_AsonDate a,Mas_Gift b " +
                "where sf_Code='" + Sf_Code + "' and a.Division_Code='" + Div_Code + "' and a.gift_Code=b.gift_Code and b.Division_Code='" + Div_Code + "'";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public DataSet GetInput_Product(string sf_Code, string Month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = "select b.Sl_No,b.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty,Sale_Erp_Code from [dbo].[Trans_Sample_Despatch_Head] a, " +
            //                                          "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
            //                                          "where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_Code + "'" +
            //                                          "and b.productc=c.Product_Code_SlNo and a.Trans_Month='" + Month + "' and a.Trans_Year='" + year + "'";
            strQry = "select a.Trans_sl_No,b.Gift_Name,b.Despatch_Qty,b.Despatch_Actual_qty,a.Trans_month_year," +
         " b.Received_Flag,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Input_Despatch_Head] a,[dbo].[Trans_Input_Despatch_Details] b " +
         " where a.Trans_sl_No=b.Trans_sl_No and  a.sf_code='" + sf_Code + "'" +
         "and a.Trans_Year='" + year + "'  and a.Trans_Month='" + Month + "'";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public int Insert_Existing_Product_New_Input(string Sf_Code, string div_Code, string month, string year)
        {
            int iReturn = -1;

            //int iSlNo = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM mas_Product_State_Rates ";
                //iSlNo = db.Exec_Scalar(strQry);

                strQry = "insert into Trans_Input_Despatch_Head(Sf_Code,Division_Code,Trans_Month,Trans_Year,Created_Date,Updated_Date,Trans_month_year) " +
                    "values('" + Sf_Code + "','" + div_Code + "','" + month + "','" + year + "',getdate(),getdate(),getdate())";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetAdjusmentBal(string Div_Code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProGrp = null;

            //strQry = "Exec Sample_Stock_Transfer_View '"+ Div_Code + "','"+ Sf_Code + "','"+ Trans_Month + "','"+ Trans_Year + "'";
            strQry = "select Product_Code_SlNo,Sample_Erp_Code,Product_Detail_Name,Sample_AsonDate from Trans_Sample_Stock_FFWise_AsonDate a,Mas_Product_Detail b " +
                "where sf_Code='" + Sf_Code + "' and a.Division_Code='" + Div_Code + "' and a.Prod_Detail_Sl_No=b.Product_Code_SlNo and b.Division_Code='" + Div_Code + "'";
            try
            {
                dsProGrp = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public DataSet getmotnh_Dcr_sample(string Div_Code, string Sf_Code, string month, string year)
        {
            DB_EReporting db_Er = new DB_EReporting();
            DataSet dsProGrp = null;
            strQry = "select a.trans_SlNo,Trans_Detail_Info_Code,listeddr_Name,Convert(varchar,activity_Date,103)activity_Date,Doc_Qua_Name,Doc_Cat_ShortName,Doc_Cat_ShortName,Doc_Class_ShortName,stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = c.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',c.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name from dcrmain_Trans a,dcrdetail_lst_Trans b,mas_Listeddr c where a.Trans_SlNo=b.Trans_SlNo and a.sf_Code='" + Sf_Code + "' and month(activity_Date)='" + month + "' and year(activity_Date)='" + year + "' and b.Trans_Detail_Info_Code=c.listeddrcode and a.division_Code='" + Div_Code + "' order by activity_Date";
            try
            {
                dsProGrp = db_Er.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProGrp;
        }
        public DataSet getPrd_For_Mapp_brand(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            //strQry = " SELECT Product_Code_SlNo,Product_Detail_Name,Product_Sale_Unit  FROM  Mas_Product_Detail " +
            //          " WHERE Product_Active_Flag=0 AND Division_Code= '" + div_code + "'  ORDER BY 2 ";

            strQry = "select Product_Brd_Code as Product_Code_SlNo,Product_Brd_Name as Product_Detail_Name, '' as Product_Sale_Unit " +
                 " from mas_product_brand where division_code='" + div_code + "' and Product_Brd_Active_Flag=0 ";

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

        public DataSet getprdfor_Mappdr_brand(string Listeddr_Code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select Product_Code as Product_Code_SlNo ,Product_Name,Product_Priority  from Map_LstDrs_Product_Brand " +
                      " where Listeddr_Code ='" + Listeddr_Code + "' and sf_code='" + sf_code + "' ";

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
    }
}
