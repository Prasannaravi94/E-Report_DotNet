using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
using Newtonsoft.Json;
using DBase_EReport;

/// <summary>
/// Summary description for Order
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Order : System.Web.Services.WebService
{

    #region order
    public Order()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public static string getstock(string mode)
    {


        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        // string query = "select ROW_NUMBER() over(order by(select 1)) Sl_no, District_Sl_No,District_Name,StateName from Mas_District where Active_Flag=0 and division_code='" + div_code + "' ";

        using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        using (var cmd = new SqlCommand("select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) from Mas_Stockist where Stockist_Active_Flag=0 and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' and Stockist_Name not like 'Direct%' ", con))
        {
            con.Open();
            //object val = cmd.ExecuteScalar();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);


            return JsonConvert.SerializeObject(dt);
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stock_Detail2> Get_prd(string sf_code, string div_code, string subdiv_code)
    {
        List<Stock_Detail2> objStockData = new List<Stock_Detail2>();
        try
        {


            SecSale ss = new SecSale();
            DataSet dsSale = ss.get_product_ddl(sf_code, div_code, subdiv_code);

            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Stock_Detail2 objch = new Stock_Detail2();
                    objch.Product_Code_SlNo = dr["Product_Code_SlNo"].ToString();
                    objch.Product_Detail_Name = dr["Product_Detail_Name"].ToString();
                    objch.Pack = dr["Pack"].ToString();

                    objStockData.Add(objch);
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objStockData;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stock_Detail2> GetStockist(string mode, string sf_code, string div_code)
    {
        List<Stock_Detail2> objStockData = new List<Stock_Detail2>();
        try
        {

            SecSale ss = new SecSale();
            DataSet dsSale = ss.GetStockist_order(sf_code, div_code, mode);

            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Stock_Detail2 objch = new Stock_Detail2();
                    objch.Stockist_Code = dr["Stockist_Code"].ToString();
                    objch.Stockist_Name = dr["Stockist_Name"].ToString();
                    objStockData.Add(objch);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objStockData;
    }



    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stock_Detail2> Get_prd_table(string sf_code, string div_code, string subdiv_code, string mode, string Stockist_Code, string Order_Date, string DHP_Code)
    {
        List<Stock_Detail2> objStockData = new List<Stock_Detail2>();
        try
        {


            string Mode_of_Order = string.Empty;

            if (mode == "1")
            {
                Mode_of_Order = "P";
            }
            else if (mode == "2")
            {
                Mode_of_Order = "H";
            }

            else if (mode == "3")
            {
                Mode_of_Order = "D";
            }

            DateTime Order_Date2 = Convert.ToDateTime(Order_Date);

            string strQry = string.Empty;
            int Trans_SlNo;
            DB_EReporting db = new DB_EReporting();

            strQry = "Select Trans_SlNo from trans_order_book_head WHERE Sf_Code = '" + sf_code + "' AND Division_Code='" + div_code + "' AND Order_Month='" + Order_Date2.Month + "' AND Order_Year ='" + Order_Date2.Year + "' and Order_Date='" + Order_Date2.ToString("MM/dd/yyyy") + "' and Stockist_Code='" + Stockist_Code + "' and Mode_of_Order='" + Mode_of_Order + "' and DHP_Code='" + DHP_Code + "'";
            Trans_SlNo = db.Exec_Scalar(strQry);

            SecSale ss = new SecSale();
            DataSet dsSale = ss.get_product_list(sf_code, div_code, subdiv_code, Trans_SlNo);

            // DataSet dsSale = ss.get_product_ddl(sf_code, div_code, subdiv_code);

            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Stock_Detail2 objch = new Stock_Detail2();
                    objch.sno = dr["sno"].ToString();
                    objch.Product_Code_SlNo = dr["Product_Code_SlNo"].ToString();
                    objch.Product_Detail_Name = dr["Product_Detail_Name"].ToString();
                    objch.Pack = dr["Pack"].ToString();
                    objch.saleqty = dr["saleqty"].ToString();
                    objch.Foc_qty = dr["Foc_qty"].ToString();
                    objch.rate = dr["rate"].ToString();
                    objch.amt = dr["amt"].ToString();
                    objch.NRV_Value = dr["NRV_Value"].ToString();
                    objch.TotNet_Amt = dr["TotNet_Amt"].ToString();
                    objStockData.Add(objch);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objStockData;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stock_Detail2> SaveingOrderbooking(string mode, string sf_code, string div_code, string hdntot_values, string Stockist_Code, string Order_Date, string sf_name, string Stockist_Name, string subdiv_code, string DHP_Code, string DHP_Name)
    {
        List<Stock_Detail2> objStockData = new List<Stock_Detail2>();



        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[12]
        {
            new DataColumn("Trans_SlNo", typeof(string)),
            new DataColumn("Sf_Code", typeof(string)),
            new DataColumn("Procuct_Code",typeof(string)) ,

            new DataColumn("Product_Name",typeof(string)) ,
            new DataColumn("Pack", typeof(string)),
            new DataColumn("Order_Sal_Qty", typeof(decimal)),
            new DataColumn("Order_Free_Qty",typeof(decimal)) ,

            new DataColumn("Order_Rate",typeof(decimal)) ,
            new DataColumn("Order_Value", typeof(decimal)),
             new DataColumn("NRV_Value", typeof(decimal)),
              new DataColumn("TotNet_Amt", typeof(decimal)),
            new DataColumn("Division_Code", typeof(string))



        }
        );
        try
        {

            DateTime Order_Date2 = Convert.ToDateTime(Order_Date);


            subdiv_code = subdiv_code.TrimEnd(',');

            string Mode_of_Order = string.Empty;

            if (mode == "1")
            {
                Mode_of_Order = "P";
            }
            else if (mode == "2")
            {
                Mode_of_Order = "H";
            }

            else if (mode == "3")
            {
                Mode_of_Order = "D";
            }


            string strQry = string.Empty;
            int Trans_SlNo = 0;
            DB_EReporting db = new DB_EReporting();


            string[] splitVal = hdntot_values.Split('~');


            string[] Product_Code = splitVal[0].Split(',');
            string[] Product_Name = splitVal[1].Split(',');
            string[] Pack_Name = splitVal[2].Split(',');
            string[] Order_Sal_Qty = splitVal[3].Split(',');
            string[] Order_Free_Qty = splitVal[4].Split(',');
            string[] Order_Rate = splitVal[5].Split(',');
            string[] Order_Value = splitVal[6].Split(',');
            string[] NRV_Value = splitVal[7].Split(',');
            string[] TotNet_Amt = splitVal[8].Split(',');


            string constr = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                con.Open();
                SqlTransaction transaction;

                transaction = con.BeginTransaction();


                strQry = "Select Trans_SlNo from trans_order_book_head WHERE Sf_Code = '" + sf_code + "' AND Division_Code='" + div_code + "' AND Order_Month='" + Order_Date2.Month + "' AND Order_Year ='" + Order_Date2.Year + "' and Order_Date='" + Order_Date2.ToString("MM/dd/yyyy") + "' and Stockist_Code='" + Stockist_Code + "' and Mode_of_Order='" + Mode_of_Order + "' and DHP_Code='" + DHP_Code + "'";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo == 0)
                {


                    strQry = " INSERT INTO trans_order_book_head(Sf_Code, Sf_Name, Division_Code, Stockist_Code,Stockist_Name,Mode_of_Order,DHP_Code,DHP_Name,Sub_Div_Code,Order_Date,Order_Month,Order_Year,Entry_Mode,Created_Date,Updated_Date) " +
                                 " VALUES ( '" + sf_code + "' , '" + sf_name + "', '" + div_code + "', '" + Stockist_Code + "','" + Stockist_Name + "','" + Mode_of_Order + "','" + DHP_Code + "','" + DHP_Name + "','" + subdiv_code + "','" + Order_Date2.ToString("MM/dd/yyyy") + "','" + Order_Date2.Month + "','" + Order_Date2.Year + "','WebEntry',getdate(), getdate())SELECT SCOPE_IDENTITY()";

                    Trans_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    //strQry = "Update trans_order_book_head set " +
                    //  "DHP_Code='" + DHP_Code + "'," +
                    //  "DHP_Name='" + DHP_Name + "' where sf_code='" + sf_name + "' and Division_Code='" + div_code + "' and Order_Month='" + Order_Date2.Month + "' " +
                    //  " and Order_Year ='" + Order_Date2.Year + "' and Order_Date='" + Order_Date2.ToString("MM/dd/yyyy") + "' and Stockist_Code='" + Stockist_Code + "' and Mode_of_Order='" + Mode_of_Order + "' and Trans_SlNo='" + Trans_SlNo + "' ";
                    strQry = "Delete from trans_order_book_detail where Trans_SlNo='" + Trans_SlNo + "'";
                    int iReturn = db.ExecQry(strQry);
                }






                for (int i = 0; i < Product_Code.Length; i++)
                {

                    string prd_code = Product_Code[i];
                    string Prd_Name = Product_Name[i];
                    string Pck_Name = Pack_Name[i];
                    string Sal_Qty = Order_Sal_Qty[i];
                    string FOC_Qty = Order_Free_Qty[i];
                    string rate = Order_Rate[i];
                    string amt = Order_Value[i];
                    string NRV = NRV_Value[i];
                    string net_amt = TotNet_Amt[i];



                    dt.Rows.Add(Trans_SlNo, sf_code, prd_code, Prd_Name, Pck_Name, Sal_Qty, FOC_Qty, rate, amt, NRV, net_amt, div_code
                    );



                }


                using (SqlCommand cmd = new SqlCommand("Trans_Order_Book"))
                {
                    cmd.Connection = con;

                    cmd.Transaction = transaction;

                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Connection = con;



                    try
                    {
                        cmd.Parameters.AddWithValue("@Order_INSERT", dt);

                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                    }
                    catch
                    {
                        transaction.Rollback();
                    }






                }
                con.Close();

            }




        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objStockData;
    }

}
#endregion



public class Stock_Detail2
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
    public string Product_Code_SlNo { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Pack { get; set; }

    public string saleqty { get; set; }
    public string Foc_qty { get; set; }
    public string rate { get; set; }
    public string amt { get; set; }
    public string sno { get; set; }
    public string NRV_Value { get; set; }
    public string TotNet_Amt { get; set; }
}
