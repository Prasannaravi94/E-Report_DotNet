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

/// <summary>
/// Summary description for EDetailingWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PrimarySalesWebService : System.Web.Services.WebService
{

    public PrimarySalesWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<PrimaryProduct> GetProductDetail(string obj_Head)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_Code = "";
        string state_code = "";
        string sub_code = "";
        string stck_code = "";
        string mnth = "";
        string yr = "";
        string in_no = "";
        string in_date = "";
        string ord_no = "";
        string ord_date = "";

        string[] PS_Head = new string[] { };

        PS_Head = obj_Head.Split('^');
        sf_Code = PS_Head[0];
        stck_code = PS_Head[1];
        mnth = PS_Head[2];
        yr = PS_Head[3];
        in_no = PS_Head[4];
        in_date = PS_Head[5];
        ord_no = PS_Head[6];
        ord_date = PS_Head[7];

        string Cal_Rate = "";

        PrimaryProduct objProd = new PrimaryProduct();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_Code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        SubDivision sb = new SubDivision();
        AdminSetup adm = new AdminSetup();
        DataSet dsSub = sb.getSub_sf(sf_Code);

        DataSet dsadmin = adm.getSetup_forTargetFix(div_code);
        if (dsadmin.Tables[0].Rows.Count > 0)
        {
            if (dsadmin.Tables[0].Rows[0]["Stockist_Primary_Sale_Based_On"] != DBNull.Value)
            {
                Cal_Rate = dsadmin.Tables[0].Rows[0]["Stockist_Primary_Sale_Based_On"].ToString();
            }
            else
            {
                Cal_Rate = "D";
            }
        }
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        DataSet dsProd = ss.Get_Stockist_Primay_ProductDetail(div_code, state_code, sub_code, sf_Code, stck_code, mnth, yr, in_no, in_date, ord_no, ord_date);


        List<PrimaryProduct> objProdDel = new List<PrimaryProduct>();
        if (dsProd.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow drow in dsProd.Tables[0].Rows)
            {
                PrimaryProduct objPrd = new PrimaryProduct();
                objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
                objPrd.Product_Description = drow["Product_Description"].ToString();
                objPrd.Product_Sale_Unit = drow["Product_Sale_Unit"].ToString();
                objPrd.MRP_Price = drow["MRP_Price"].ToString();
                objPrd.Retailor_Price = drow["Retailor_Price"].ToString();
                objPrd.Distributor_Price = drow["Distributor_Price"].ToString();
                objPrd.NSR_Price = drow["NSR_Price"].ToString();
                objPrd.Target_Price = drow["Target_Price"].ToString();
                objPrd.Sale_Qty = drow["Sale_Qty"].ToString();
                objPrd.Free_Qty = drow["Free_Qty"].ToString();
                objPrd.Replace_Qty = drow["Replace_Qty"].ToString();
                objPrd.Batch_No = drow["Batch_No"].ToString();
                objPrd.Cal_Rate = Cal_Rate;

                objProdDel.Add(objPrd);
            }
        }

        return objProdDel;
    }

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod]
    //public string GetRCPA(string objPrd_SfCode)
    //{
    //    string div_code = HttpContext.Current.Session["div_code"].ToString();

    //    Product adm = new Product();

    //    DataSet dsadmin = adm.get_RCPA_View(objPrd_SfCode);
    //    DataTable dt = new DataTable();
    //    dt = dsadmin.Tables[0];


    //    string JSONString = string.Empty;
    //    JSONString = JsonConvert.SerializeObject(dt);

    //    return JSONString;
    //}

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Update(string objPS_Head, string objPS_Detail)
    {
        string objField = "false";
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_Code = "";
            string state_code = "";
            string sub_code = "";
            string stck_code = "";
            string mnth = "";
            string yr = "";
            string in_no = "";
            string in_date = "";
            string ord_no = "";
            string ord_date = "";

            string[] PS_Head = new string[] { };

            PS_Head = objPS_Head.Split('^');
            sf_Code = PS_Head[0];
            stck_code = PS_Head[1];
            mnth = PS_Head[2];
            yr = PS_Head[3];
            in_no = PS_Head[4];
            in_date = PS_Head[5];
            ord_no = PS_Head[6];
            ord_date = PS_Head[7];

            Product adm = new Product();
            UnListedDR LstDR = new UnListedDR();
            DataSet dsState = LstDR.getState(PS_Head[0]);
            if (dsState.Tables[0].Rows.Count > 0)
                state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            SubDivision sb = new SubDivision();
            SecSale ss = new SecSale();
            DataSet dsSub = sb.getSub_sf(PS_Head[0]);
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            int iReturn = -1;

            iReturn = ss.Stockist_Primary_Sales_Update(div_code, sf_Code, stck_code, mnth, yr, in_no, in_date, ord_no, ord_date, state_code, sub_code, objPS_Detail);
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<PrimaryProduct> GetPrimaryHead(string objPrimary_Head)
    {
        List<PrimaryProduct> objField = new List<PrimaryProduct>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_Code = "";
        string stck_code = "";
        string mnth = "";
        string yr = "";

        string[] PS_Head = new string[] { };

        PS_Head = objPrimary_Head.Split('^');
        sf_Code = PS_Head[0];
        stck_code = PS_Head[1];
        mnth = PS_Head[2];
        yr = PS_Head[3];

        try
        {
            SecSale dv = new SecSale();
            DataSet ds = new DataSet();

            ds = dv.Stockist_Primary_Head(div_code, sf_Code, stck_code, mnth, yr);

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    PrimaryProduct objFFDet = new PrimaryProduct();
                    objFFDet.Inv_No = dr["Inv_No"].ToString();
                    objFFDet.Inv_Dt = dr["Inv_Dt"].ToString();
                    objFFDet.Ord_No = dr["Ord_No"].ToString();
                    objFFDet.Ord_Dt = dr["Ord_Dt"].ToString();
                    objField.Add(objFFDet);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

}

public class PrimaryProduct
{
    public string Product_Detail_Code { get; set; }
    public string Product_Description { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Product_Sale_Unit { get; set; }
    public string MRP_Price { get; set; }
    public string Retailor_Price { get; set; }
    public string Distributor_Price { get; set; }
    public string NSR_Price { get; set; }
    public string Target_Price { get; set; }
    public string Sale_Qty { get; set; }
    public string Free_Qty { get; set; }
    public string Replace_Qty { get; set; }
    public string Batch_No { get; set; }
    public string Cal_Rate { get; set; }
    public string Inv_No { get; set; }
    public string Inv_Dt { get; set; }
    public string Ord_No { get; set; }
    public string Ord_Dt { get; set; }

}

