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

/// <summary>
/// Summary description for SecSaleWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class SecSaleWebService : System.Web.Services.WebService
{

    #region SSEntry
    public SecSaleWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Field_Name> GetFieldForceName()
    {
        List<Field_Name> objField = new List<Field_Name>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();

            SalesForce sf = new SalesForce();
            DataSet dsSalesforce = sf.getSecSales_MR(div_code, sf_code);
            if (dsSalesforce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSalesforce.Tables[0].Rows)
                {
                    Field_Name objFFDet = new Field_Name();
                    objFFDet.Field_Sf_Code = dr["SF_Code"].ToString();
                    objFFDet.Field_Sf_Name = dr["Sf_Name"].ToString();
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

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stock_Detail> GetStockist(string objSfCode)
    {
        List<Stock_Detail> objStockData = new List<Stock_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objSfCode;

            //DCR dc = new DCR();
            //DataSet dsSale = dc.getStockiest_SSentry(sf_code, div_code);

            SecSale ss = new SecSale();
            DataSet dsSale = ss.GetStockistDet_DDL(sf_code, div_code);

            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Stock_Detail objch = new Stock_Detail();
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
    public List<Y_Det> FillYear()
    {
        List<Y_Det> objYearDel = new List<Y_Det>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            Y_Det objYear = new Y_Det();

            if (dsYear.Tables[0].Rows.Count > 0)
            {
                //for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                //{
                //    objYear.Year = k.ToString();
                //    objYearDel.Add(objYear);
                //}

                foreach (DataRow dr in dsYear.Tables[0].Rows)
                {
                    objYear.Year = dr["Year"].ToString();
                    objYearDel.Add(objYear);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objYearDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string ButtonVal()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        string btnSubmit = string.Empty;

        int approval_needed = -1;
        SecSale sa = new SecSale();
        DataSet dsSale = sa.getAddionalSaleMaster(div_code);
        if (dsSale.Tables[0].Rows.Count > 0)
        {
            if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(3) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim().Length > 0))
            {
                approval_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                HttpContext.Current.Session["approval_needed"] = approval_needed.ToString();
            }
        }

        if (approval_needed == 0)
        {
            btnSubmit = "Send to Manager";
        }
        else
        {
            btnSubmit = "Send to Admin";
        }

        string btnVal = btnSubmit + "^" + approval_needed;

        return btnVal;
    }

    [WebMethod(EnableSession = true)]
    public string GetRecordStaus(string objMonthDel)
    {
        bool bRecordExists;

        string Val = "";

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        SecSale ss = new SecSale();
        DataSet dsStatus;

        try
        {

            int approval_needed = -1;
            int Status = -1;

            // string sf_code = "";

            string[] values = objMonthDel.Split('^');
            string Month = values[0];
            string Year = values[1];
            string Stockiest = values[2];
            string S_Code = values[3];


            int lmonth = -1;
            int lyear = -1;
            //calculating opening balance from last month opening balance
            lmonth = GetLastMonth(Convert.ToString(Month));

            if (lmonth == 12)
                lyear = Convert.ToInt32(Year) - 1;
            else
                lyear = Convert.ToInt32(Year);

            sf_code = S_Code;

            string sub_code = string.Empty;
            SubDivision sb = new SubDivision();
            DataSet dsSub = sb.getSub_sf(sf_code);
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }

            DisplayContactUI(Month, Year, Stockiest);

            DataSet dsSale = ss.getAddionalSaleMaster(div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                if ((dsSale.Tables[0].Rows[0].ItemArray.GetValue(3) != null) && (dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim().Length > 0))
                {
                    approval_needed = Convert.ToInt16(dsSale.Tables[0].Rows[0].ItemArray.GetValue(3).ToString());
                    HttpContext.Current.Session["approval_needed"] = approval_needed.ToString();
                }
            }
            if (approval_needed == 0)
            {
                if (HttpContext.Current.Session["refer"] != null)
                {
                    sf_code = HttpContext.Current.Session["SF_Code_Id"].ToString();
                }
                else
                {

                }

                dsStatus = ss.Get_Stockiest_MR_Status(div_code, Convert.ToInt16(Month), Convert.ToInt16(Year), Convert.ToInt16(Stockiest), sub_code, lmonth, lyear);

                if (dsStatus.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(dsStatus.Tables[0].Rows[0]["Status"].ToString());
                }

                bRecordExists = ss.sRecordExist_TransHead(div_code, Convert.ToInt16(Month), Convert.ToInt16(Year), Convert.ToInt16(Stockiest), Status, sub_code);
            }
            else
            {
                dsStatus = ss.Get_Stockiest_MR_Status(div_code, Convert.ToInt16(Month), Convert.ToInt16(Year), Convert.ToInt16(Stockiest), sub_code, lmonth, lyear);

                if (dsStatus.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToInt32(dsStatus.Tables[0].Rows[0]["Status"].ToString());
                }

                bRecordExists = ss.sRecordExist_TransHead(div_code, Convert.ToInt16(Month), Convert.ToInt16(Year), Convert.ToInt16(Stockiest), 2, sub_code);
            }

            string MgrVal = HttpContext.Current.Session["sf_type"].ToString();

            Val = bRecordExists + "^" + approval_needed + "^" + Status + "^" + MgrVal;
        }
        catch (Exception ex)
        {
        }
        return Val;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_OB_Free> Option_Edit()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string sMgr = "";

        Get_OB_Free objEdit = new Get_OB_Free();

        List<Get_OB_Free> objOpening = new List<Get_OB_Free>();

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        //Option Edit
        SecSale ss = new SecSale();
        DataSet dsOption = ss.Get_SS_Option_Edit_MR_wise(div_code, sf_code, 4, sub_code);

        if (dsOption.Tables[0].Rows.Count > 0)
        {
            objEdit.SS_Head_Sl_No = dsOption.Tables[0].Rows[0]["SS_Head_Sl_No"].ToString();
            objEdit.MONTH = dsOption.Tables[0].Rows[0]["MONTH"].ToString();
            objEdit.YEAR = dsOption.Tables[0].Rows[0]["YEAR"].ToString();
            objEdit.Approval_Mgr = dsOption.Tables[0].Rows[0]["Approval_Mgr"].ToString();
            objEdit.Stockiest_Code = dsOption.Tables[0].Rows[0]["Stockiest_Code"].ToString();

            SalesForce sf = new SalesForce();
            DataSet dsSF = sf.getSfName(objEdit.Approval_Mgr);
            if (dsSF.Tables[0].Rows.Count > 0)
                sMgr = dsSF.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            objOpening.Add(objEdit);
        }

        return objOpening;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<SS_AllParameter_Free> GetParameter()
    {
        List<SS_AllParameter_Free> objSecSale_param = new System.Collections.Generic.List<SS_AllParameter_Free>();

        SS_AllParameter_Free objParam = new SS_AllParameter_Free();
        string divCode = HttpContext.Current.Session["div_code"].ToString();

        SecSale ss = new SecSale();
        DataSet dsSecSales = ss.Get_ParameterDetail(divCode);

        foreach (DataRow dr in dsSecSales.Tables[0].Rows)
        {
            SS_AllParameter_Free objSS = new SS_AllParameter_Free();

            objSS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
            objSS.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
            objSS.Sec_Sale_Short_Name = dr["Sec_Sale_Short_Name"].ToString();
            objSS.Sel_Sale_Operator = dr["Sel_Sale_Operator"].ToString();
            objSS.CalculationMode = dr["CalculationMode"].ToString();

            //// Change Formula Based Operator 
            //if (objSS.Sel_Sale_Operator == "D")
            //{
            //    if (objSS.CalculationMode.Trim() == "D(+)")
            //    {
            //        string D_opr = "+";
            //        objSS.Sel_Sale_Operator = D_opr;
            //    }
            //    else if (objSS.CalculationMode.Trim() == "D(-)")
            //    {
            //        string D_opr = "-";
            //        objSS.Sel_Sale_Operator = D_opr;
            //    }
            //    else if (objSS.CalculationMode.Trim() == "N")
            //    {

            //    }
            //}

            objSS.value_needed = dr["value_needed"].ToString();
            objSS.calc_needed = dr["calc_needed"].ToString();
            objSS.Sub_Needed = dr["Sub_Needed"].ToString();
            objSS.Sub_Label = dr["Sub_Label"].ToString();
            objSS.Order_by = dr["Order_by"].ToString();
            objSS.Carry_Fwd_Needed = dr["Carry_Fwd_Needed"].ToString();
            objSS.Carry_Fwd_Field = dr["Carry_Fwd_Field"].ToString();
            objSS.Der_Formula = dr["Der_Formula"].ToString();
            objSS.CalcF_Field = dr["CalcF_Field"].ToString();
            objSS.Calc_Disable = dr["Calc_Disable"].ToString();
            objSS.Sec_Sale_Sub_Name = dr["Sec_Sale_Sub_Name"].ToString();
            objSS.DivCode = divCode;
            objSS.Primary_Bill = dr["Primary_Bill"].ToString();
            objSS.Free_Needed = dr["Free_Needed"].ToString();
            objSS.Sub_Label_1 = dr["Sub_Label_1"].ToString();

            objSecSale_param.Add(objSS);
        }

        return objSecSale_param;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ProductDet_Free> GetProductDetail(string objPrd_SfCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        sf_code = objPrd_SfCode;

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDet_Free objProd = new ProductDet_Free();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        DataSet dsRate = ss.getAddionalRptSaleMaster(div_code);
        if (dsRate != null)
        {
            if (dsRate.Tables[0].Rows.Count > 0)

                objProd.Calc_Rate = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();
            Prod_Grp = dsRate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();

        }

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        DataSet dsProd = new DataSet();
        if (div_code == "44")    // Tidal Healthcare Pvt Ltd
        {
            dsProd = ss.GetProduct_DetailO(div_code, state_code, Prod_Grp, sub_code);
        }
        else
        {
            dsProd = ss.GetProduct_Detail(div_code, state_code, Prod_Grp, sub_code);
        }


        List<ProductDet_Free> objProdDel = new List<ProductDet_Free>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ProductDet_Free objPrd = new ProductDet_Free();
            objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
            objPrd.Product_Description = drow["Product_Description"].ToString();

            if (objPrd.Product_Detail_Code == "Tot_Prod")
            {
                objPrd.Product_Detail_Name = "Total";
            }
            else
            {
                objPrd.Product_Detail_Name = drow["Product_Detail_Name"].ToString();
            }

            if (objPrd.Product_Description == "Catg_Code" || objPrd.Product_Description == "Grp_Code")
            {
                Prd_grp = "Catg_Code";
            }
            else
            {
                Prd_grp = "1";
            }

            objPrd.Product_Sale_Unit = drow["Product_Sale_Unit"].ToString();
            objPrd.MRP_Price = drow["MRP_Price"].ToString();
            objPrd.Retailor_Price = drow["Retailor_Price"].ToString();
            objPrd.Distributor_Price = drow["Distributor_Price"].ToString();
            objPrd.NSR_Price = drow["NSR_Price"].ToString();
            objPrd.Target_Price = drow["Target_Price"].ToString();
            objPrd.Calc_Rate = objProd.Calc_Rate;
            objPrd.Prd_grp = Prd_grp;

            objProdDel.Add(objPrd);

        }

        return objProdDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Get_Trans(string objStocKProd)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objStocKProd.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];


        string sub_code = "";
        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        //   ProdDel = objTransDel[0].ToString();


        DisplayContactUI(objQty.iMonth, objQty.iYear, objQty.Par_Stock_Code);
        int lmonth = -1;
        int lyear = -1;

        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        int Cnt = 0;
        SecSale ss = new SecSale();
        DataSet ds = ss.Get_Trans_SS_EntryDelVal(div_code, Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Stock_Code, sub_code);

        DataSet dsPrev = ss.Get_Trans_SS_EntryDelVal(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), objQty.Par_Stock_Code, sub_code);

        if (ds.Tables[0].Rows.Count > 0 || dsPrev.Tables[0].Rows.Count > 0)
        {
            Cnt = 1;
        }
        else
        {
            Cnt = 0;
        }

        return Cnt;
    }

    private int GetLastMonth(string sMonth)
    {
        int iMonth = 0;

        if (sMonth == "1")
            iMonth = 12;
        else if (sMonth == "2")
            iMonth = 1;
        else if (sMonth == "3")
            iMonth = 2;
        else if (sMonth == "4")
            iMonth = 3;
        else if (sMonth == "5")
            iMonth = 4;
        else if (sMonth == "6")
            iMonth = 5;
        else if (sMonth == "7")
            iMonth = 6;
        else if (sMonth == "8")
            iMonth = 7;
        else if (sMonth == "9")
            iMonth = 8;
        else if (sMonth == "10")
            iMonth = 9;
        else if (sMonth == "11")
            iMonth = 10;
        else if (sMonth == "12")
            iMonth = 11;

        return iMonth;

    }

    public string TimeStamp
    {
        get
        {
            return (HttpContext.Current.Session["TimeStamp"] !=
            null ? HttpContext.Current.Session["TimeStamp"].ToString() : "0");
        }
        set
        {
            HttpContext.Current.Session["TimeStamp"] = value;
        }
    }

    public string TimeStamp_Product
    {
        get
        {
            return (HttpContext.Current.Session["TimpStamp_Product"] !=
            null ? HttpContext.Current.Session["TimpStamp_Product"].ToString() : "0");
        }
        set
        {
            HttpContext.Current.Session["TimpStamp_Product"] = value;
        }
    }

    public string TimeStamp_Value
    {
        get
        {
            return (HttpContext.Current.Session["TimeStamp_Value"] !=
            null ? HttpContext.Current.Session["TimeStamp_Value"].ToString() : "0");
        }
        set
        {
            HttpContext.Current.Session["TimeStamp_Value"] = value;
        }
    }

    public void DisplayContactUI(string Month, string Year, string StockistCode)
    {
        //Contact Display code here

        string div_code = HttpContext.Current.Session["div_code"].ToString();

        SecSale ss = new SecSale();
        DataSet ds = ss.Get_TransHead_TimeStamp(div_code, Month, Year, StockistCode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            TimeStamp = ds.Tables[0].Rows[0]["TimeStamp"].ToString();
        }
        else
        {
            TimeStamp = null;
        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Get_Trans_Head_Cnt(string objCnt)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string ProdDel;
        int Count = 0;



        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objCnt.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        int lmonth = -1;
        int lyear = -1;
        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        SecSale ss = new SecSale();

        DataSet dsCnt = ss.Get_Trans_Head_Code(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Stock_Code, sub_code);

        if (dsCnt.Tables[0].Rows.Count > 0)
        {
            Count = 1;
        }

        return Count;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_OB_Free> GetPreviousMonthOpeing(string objPrevious)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string sub_code = "";

        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objPrevious.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        string ProdDel;



        DisplayContactUI(objQty.iMonth, objQty.iYear, objQty.Par_Stock_Code);

        int lmonth = -1;
        int lyear = -1;
        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        List<Get_OB_Free> objOpening = new List<Get_OB_Free>();

        SecSale ss = new SecSale();

        DataSet dsSecSales = ss.Get_ParameterDetail(div_code);

        int ParamCnt = dsSecSales.Tables[0].Rows.Count;

        DataTable dsQty = ss.Bind_Prd_Curt_Month(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Stock_Code, sub_code);

        foreach (DataRow dr in dsQty.Rows)
        {
            Get_OB_Free objQtyDel = new Get_OB_Free();
            objQtyDel.Product_Detail_Code = dr["Product_Detail_Code"].ToString();
            objQtyDel.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
            objQtyDel.Sec_Sale_Qty = dr["Sec_Sale_Qty"].ToString();
            objQtyDel.Sec_Sale_Value = dr["Sec_Sale_Value"].ToString();
            objQtyDel.Sec_Sale_Sub = dr["Sec_Sale_Sub"].ToString();
            objQtyDel.Sec_Sale_Free = dr["Sec_Sale_Free"].ToString();
            objQtyDel.Carry_Field_Code = dr["Carry_Field_Code"].ToString();
            objQtyDel.Carry_FieldName = dr["Carry_FieldName"].ToString();
            objQtyDel.CalcF_Field = dr["CalcF_Field"].ToString();
            objQtyDel.CalcF_FieldName = dr["CalcF_FieldName"].ToString();
            objQtyDel.Sub_Needed = dr["Sub_Needed"].ToString();
            objQtyDel.Free_Needed = dr["Free_Needed"].ToString();
            objOpening.Add(objQtyDel);
        }

        return objOpening;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_OB_Free> GetCurrentMonth(string objCurrent)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string ProdDel;

        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objCurrent.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        //objQty.Par_Prod_Code = values[3];
        DisplayContactUI(objQty.iMonth, objQty.iYear, objQty.Par_Stock_Code);

        int lmonth = -1;
        int lyear = -1;
        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        List<Get_OB_Free> objOpening = new List<Get_OB_Free>();

        SecSale ss = new SecSale();

        DataTable dsQty = ss.BindPrdDelVal(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Stock_Code, sub_code);

        foreach (DataRow dr in dsQty.Rows)
        {
            Get_OB_Free objQtyDel = new Get_OB_Free();
            objQtyDel.Product_Detail_Code = dr["Product_Detail_Code"].ToString();

            objQtyDel.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
            objQtyDel.Sec_Sale_Qty = dr["Sec_Sale_Qty"].ToString();
            objQtyDel.Sec_Sale_Value = dr["Sec_Sale_Value"].ToString();
            objQtyDel.TimeStamp_Head = dr["TimeStamp_Head"].ToString();
            objQtyDel.TimeStamp_Prd = dr["TimeStamp_Prd"].ToString();
            objQtyDel.TimeStamp_Value = dr["TimeStamp_Value"].ToString();
            objQtyDel.SS_Det_Sl_No = dr["SS_Det_Sl_No"].ToString();
            objQtyDel.Sec_Sale_Sub = dr["Sec_Sale_Sub"].ToString();
            objQtyDel.Sec_Sale_Free = dr["Sec_Sale_Free"].ToString();

            string[] Sec_Sale_Code = objQtyDel.Sec_Sale_Code.Split(',');
            string[] Sec_Sale_Qty = objQtyDel.Sec_Sale_Qty.Split(',');
            string[] Sec_Sale_Value = objQtyDel.Sec_Sale_Value.Split(',');
            string[] Sec_Sale_Sub = objQtyDel.Sec_Sale_Sub.Split(',');
            string[] Sec_Sale_Free = objQtyDel.Sec_Sale_Free.Split(',');
            string[] TimeStamp_Head = objQtyDel.TimeStamp_Head.Split(',');
            string[] TimeStamp_Prd = objQtyDel.TimeStamp_Prd.Split(',');
            string[] TimeStamp_Value = objQtyDel.TimeStamp_Value.Split(',');

            objQtyDel.ArrSec_Sale_Code = Sec_Sale_Code;
            objQtyDel.ArrSec_Sale_Qty = Sec_Sale_Qty;
            objQtyDel.ArrSec_Sale_Value = Sec_Sale_Value;
            objQtyDel.ArrSec_Sale_Sub = Sec_Sale_Sub;
            objQtyDel.ArrSec_Sale_Free = Sec_Sale_Free;
            objQtyDel.ArrTimeStamp_Head = TimeStamp_Head;
            objQtyDel.ArrTimeStamp_Prd = TimeStamp_Prd;
            objQtyDel.ArrTimeStamp_Value = TimeStamp_Value;

            objOpening.Add(objQtyDel);
        }

        return objOpening;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<TransSSDet_Free> GetTransSSQty(string objTransDel)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();


        List<TransSSDet_Free> objTransSS = new List<TransSSDet_Free>();

        string ProdDel;

        TransSSDet_Free objQty = new TransSSDet_Free();

        //   ProdDel = objTransDel[0].ToString();
        string[] values = objTransDel.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];
        objQty.Par_Prod_Code = values[3];

        sf_code = values[4];

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        SecSale ss = new SecSale();
        string sop = string.Empty;
        int lmonth = -1;
        int lyear = -1;
        int secsalecd = -1;
        int plus_qty = 0;
        int minus_qty = 0;
        int C_Qty = 0;
        double C_val;
        double plus_val;
        double minus_val;
        int totpls_qty = 0;
        int cls_qty = 0;
        double totpls_val;
        double cls_bal;

        string CalcName = string.Empty;
        int Calc_Field = 0;

        string cl_bal_sub = string.Empty;

        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        DataSet dsQty = ss.GetAllProductSecSaleDel(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Prod_Code, objQty.Par_Stock_Code);

        foreach (DataRow dr in dsQty.Tables[0].Rows)
        {
            TransSSDet_Free objQtyDel = new TransSSDet_Free();
            objQtyDel.Sec_Sale_Qty = dr["txtSecSale"].ToString();
            objQtyDel.Sec_Sale_Value = dr["txtVal"].ToString();
            objTransSS.Add(objQtyDel);
        }
        return objTransSS;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public bool GetPrimaryField()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        SecSale ss = new SecSale();
        bool Count = ss.GetPrimary_SaleField(div_code);
        return Count;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Primary_Field_Free> BindPrimaryField(string objPrimary)
    {
        List<Primary_Field_Free> objPrimaryData = new List<Primary_Field_Free>();

        SecSale ss = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string ProdDel;

        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objPrimary.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        DataTable dtPrime = ss.GetPrimary_FieldValue(objQty.Division_Code, objQty.iMonth, objQty.iYear, objQty.Par_Stock_Code);

        if (dtPrime != null)
        {
            foreach (DataRow dr in dtPrime.Rows)
            {
                Primary_Field_Free objData = new Primary_Field_Free();
                objData.S_Id = dr["S_Id"].ToString();
                objData.Product_Name = dr["Product_Name"].ToString();
                objData.Product_ERP_Code = dr["Product_ERP_Code"].ToString();
                objData.Sale_Qty = dr["Sale_Qty"].ToString();
                objData.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objData.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objPrimaryData.Add(objData);
            }
        }

        return objPrimaryData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Get_Previous_Month_Cnt(string objStocKProd)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        TransSSDet_Free objQty = new TransSSDet_Free();

        //   ProdDel = objTransDel[0].ToString();
        string[] values = objStocKProd.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        int lmonth = -1;
        int lyear = -1;

        //calculating opening balance from last month opening balance
        lmonth = GetLastMonth(Convert.ToString(objQty.iMonth));

        if (lmonth == 12)
            lyear = Convert.ToInt32(objQty.iYear) - 1;
        else
            lyear = Convert.ToInt32(objQty.iYear);

        int Cnt = 0;
        SecSale ss = new SecSale();
        DataSet ds = ss.Get_Trans_SS_EntryDelVal(div_code, Convert.ToString(lmonth), Convert.ToString(lyear), objQty.Par_Stock_Code, sub_code);

        //DataSet dsPrev = ss.Get_Trans_SS_EntryDelVal(div_code, Convert.ToString(objQty.iMonth), Convert.ToString(objQty.iYear), objQty.Par_Stock_Code);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Cnt = 1;
        }
        else
        {
            Cnt = 0;
        }

        return Cnt;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Add_Secondary_Sale_Detail_Test(List<string> objProdData, ParamDet_Free objParam)
    {
        int iReturn = 0;
        try
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();

            string SecSaleData;
            string[] ProdDetail = new string[] { };
            string U_Data;
            List<string> myList = new List<string>();

            string[] ParamDel = new string[] { };

            int ParamCnt;
            string txtSecSale;
            string txtVal;
            string txtSub;
            string txtFree;
            string hidSecSaleCode;
            string state_code = "";

            int head_sl_no = 0;

            int Prod_Id;

            int SSValID = 0;

            ParamDet_Free objParamDel = new ParamDet_Free();
            objParamDel.Stockist_Code = objParam.Stockist_Code;
            objParamDel.Month = objParam.Month;
            objParamDel.Year = objParam.Year;
            objParamDel.Status = objParam.Status;
            objParamDel.SfCode = objParam.SfCode;

            sf_code = objParam.SfCode;

            int iStatus = Convert.ToInt32(objParamDel.Status);

            string sub_code = "";

            SubDivision sb = new SubDivision();
            DataSet dsSub = sb.getSub_sf(sf_code);
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            }

            //Get state_code for sf_code
            UnListedDR LstDR = new UnListedDR();
            DataSet dsSale = LstDR.getState(sf_code);
            if (dsSale.Tables[0].Rows.Count > 0)
                state_code = dsSale.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            string ReportingTo = string.Empty;

            SecSale ss = new SecSale();

            if (iStatus == 1)
            {
                DataSet dsMGR = ss.GetSecSale_MGR(sf_code);

                if (dsMGR.Tables[0].Rows.Count > 0)
                {
                    ReportingTo = dsMGR.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    //iReturn = ss.Add_TransSSEntry_Head(objParamDel.Stockist_Code, objParamDel.Month, objParamDel.Year, div_code, sf_code, state_code, objParamDel.Status, ReportingTo);
                }
            }
            else
            {
                //iReturn = ss.Add_TransSSEntry_Head(objParamDel.Stockist_Code, objParamDel.Month, objParamDel.Year, div_code, sf_code, state_code, objParamDel.Status, ReportingTo);
            }

            DataSet dsHead = ss.GetTransHeadID(div_code, objParamDel.Stockist_Code, objParamDel.Month, objParamDel.Year, sub_code);

            if (dsHead.Tables[0].Rows.Count > 0)
            {
                head_sl_no = Convert.ToInt32(dsHead.Tables[0].Rows[0]["SS_Head_Sl_No"].ToString());
            }

            // Prod_Id = ss.Get_Prod_MaxRecordId(div_code);

            //  SSValID = ss.Get_TransVal_ID(div_code);



            DataSet RecExistsPrd = ss.TransPrdRecodExist(div_code, head_sl_no.ToString());

            // if (RecExistsPrd.Tables[0].Rows.Count > 0)
            //  {

            //if (RecExistsPrd.Tables[0].Rows.Count > 0 && (objProdData.Count != RecExistsPrd.Tables[0].Rows.Count))
            //{
            //    conn.Open();
            //    // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);
            //    SqlCommand cmd = new SqlCommand("SP_TransSSDetailandVal_Delete", conn);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@Division_Code", div_code);
            //    cmd.Parameters.AddWithValue("@Head_Sl_No", head_sl_no);
            //    cmd.Parameters.AddWithValue("@retValue", SqlDbType.Int);
            //    cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
            //    cmd.ExecuteNonQuery();
            //    iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
            //    conn.Close();
            //}

            StringBuilder Sb_Product = new StringBuilder();
            Sb_Product.Append("<root>");

            StringBuilder Sb_Val_Update = new StringBuilder();
            Sb_Val_Update.Append("<ParamUpdate>");

            for (int i = 0; i < objProdData.Count; i++)
            {
                U_Data = objProdData[i].ToString();

                string[] values = U_Data.Split('^');

                ParamCnt = Convert.ToInt32(values[0]);

                ProductDet_Free objProduct = new ProductDet_Free();
                objProduct.Product_Detail_Code = values[1];
                objProduct.Retailor_Price = values[2];
                objProduct.MRP_Price = values[3];
                objProduct.Distributor_Price = values[4];
                objProduct.NSR_Price = values[5];
                objProduct.Target_Price = values[6];
                objProduct.hdnPrdTimeStm = values[7];

                if (objProduct.hdnPrdTimeStm == "undefined" || objProduct.hdnPrdTimeStm == "")
                {
                    objProduct.hdnPrdTimeStm = "0";
                }

                if (objProduct.Retailor_Price == "undefined" && objProduct.MRP_Price == "undefined"
                    && objProduct.Distributor_Price == "undefined" && objProduct.NSR_Price == "undefined"
                    && objProduct.Target_Price == "undefined"
                    )
                {
                    objProduct.Retailor_Price = "0.00";
                    objProduct.MRP_Price = "0.00";
                    objProduct.Distributor_Price = "0.00";
                    objProduct.NSR_Price = "0.00";
                    objProduct.Target_Price = "0.00";
                }

                // Prod_Id = Prod_Id + 1;

                if (RecExistsPrd.Tables[0].Rows.Count > 0)
                {
                    TimeStamp_Product = objProduct.hdnPrdTimeStm;
                }
                else
                {
                    TimeStamp_Product = null;
                }

                Decimal Time_Product = decimal.Parse(TimeStamp_Product);

                Sb_Product.Append("<Product  Prod_Code='" + objProduct.Product_Detail_Code + "' MRP_Price='" + objProduct.MRP_Price + "' Ret_Price='" + objProduct.Retailor_Price + "' Dist_Price='" + objProduct.Distributor_Price + "' Target_Price='" + objProduct.Target_Price + "' NSR_Price='" + objProduct.NSR_Price + "' Div_Code='" + div_code + "'  TimeStamp_Prod='" + Time_Product + "'  />");

                //DataSet dsTime = new DataSet();

                //if (RecExistsPrd.Tables[0].Rows.Count > 0)
                //{

                //    string Del = RecExistsPrd.Tables[0].Rows[i]["SS_Det_Sl_No"].ToString();
                //    // dsTime
             //    dsTime = ss.Trans_SecSale_Value_timeStamp(div_code, head_sl_no.ToString(), Del);
                //}


                string[] param = values[8].Split(',');

                for (int j = 0; j < param.Length; j++)
                {
                    // SSValID = SSValID + 1;

                    string[] PrmDel = param[j].Split('/');

                    objParamDel.SecSaleCode = PrmDel[0];
                    objParamDel.txtSecSale = PrmDel[1];
                    objParamDel.txtVal = PrmDel[2];
                    objParamDel.hdnSSvalueTimeStm = PrmDel[4];

                    string SS_Det_SlNo = PrmDel[5];

                    objParamDel.txtFree = PrmDel[6];

                    //  objParamDel.txtSub = PrmDel[3];
                    //  objParamDel.txtFree = PrmDel[4];

                    if (PrmDel[3] == "undefined")
                    {
                        objParamDel.txtSub = "0";
                    }
                    else
                    {
                        objParamDel.txtSub = PrmDel[3];

                        if (objParamDel.txtSub == "")
                        {
                            objParamDel.txtSub = "0";
                        }
                    }

                    if (PrmDel[6] == "undefined")
                    {
                        objParamDel.txtFree = "0";
                    }
                    else
                    {
                        objParamDel.txtFree = PrmDel[6];

                        if (objParamDel.txtFree == "")
                        {
                            objParamDel.txtFree = "0";
                        }
                    }

                    if (PrmDel[2] == "undefined")
                    {
                        objParamDel.txtVal = "0";
                    }
                    else
                    {
                        objParamDel.txtVal = PrmDel[2];

                        if (objParamDel.txtVal == "")
                        {
                            objParamDel.txtVal = "0";
                        }
                    }

                    if (PrmDel[1] == "undefined")
                    {
                        objParamDel.txtSecSale = "0";
                    }
                    else
                    {
                        objParamDel.txtSecSale = PrmDel[1];

                        if (objParamDel.txtSecSale == "")
                        {
                            objParamDel.txtSecSale = "0";
                        }
                    }

                    if (objParamDel.hdnSSvalueTimeStm == "undefined" || objParamDel.hdnSSvalueTimeStm == "")
                    {
                        objParamDel.hdnSSvalueTimeStm = "0";
                    }

                    int Cnt = RecExistsPrd.Tables[0].Rows.Count;

                    if (Cnt == 0)
                    {
                        Sb_Product.Append("<Parameter Prod_Code_Param='" + objProduct.Product_Detail_Code + "' Sec_Sale_Code='" + objParamDel.SecSaleCode + "' Sec_Sale_Qty='" + objParamDel.txtSecSale + "' Sec_Sale_Val='" + objParamDel.txtVal + "'  Sec_Sale_Sub='" + objParamDel.txtSub + "'  Sec_Sale_Free='" + objParamDel.txtFree + "'  Div_Code='" + div_code + "'   />");
                    }
                    else if (Cnt > 0 && objProdData.Count != Cnt)
                    {
                        Sb_Product.Append("<Parameter Prod_Code_Param='" + objProduct.Product_Detail_Code + "' Sec_Sale_Code='" + objParamDel.SecSaleCode + "' Sec_Sale_Qty='" + objParamDel.txtSecSale + "' Sec_Sale_Val='" + objParamDel.txtVal + "' Sec_Sale_Sub='" + objParamDel.txtSub + "'  Sec_Sale_Free='" + objParamDel.txtFree + "'  Div_Code='" + div_code + "'   />");
                    }
                    else
                    {

                        string Del1 = RecExistsPrd.Tables[0].Rows[i]["SS_Det_Sl_No"].ToString();

                        // dsTime
                        //  DataSet dsTime = ss.Trans_SecSale_Value_timeStamp(div_code, head_sl_no.ToString(),Del);

                        if (objParamDel.hdnSSvalueTimeStm != "0")
                        {
                            TimeStamp_Value = objParamDel.hdnSSvalueTimeStm;
                        }
                        else
                        {
                            TimeStamp_Value = null;
                        }

                        Decimal Time_Value = decimal.Parse(TimeStamp_Value);

                        Sb_Val_Update.Append("<row  Det_sl_no='" + SS_Det_SlNo + "' Sec_Sale_Code='" + objParamDel.SecSaleCode + "' Sec_Sale_Qty='" + objParamDel.txtSecSale + "' Sec_Sale_Val='" + objParamDel.txtVal + "'  Sec_Sale_Sub='" + objParamDel.txtSub + "'  Sec_Sale_Free='" + objParamDel.txtFree + "'    Div_Code='" + div_code + "'  Head_sl_no='" + Convert.ToInt32(head_sl_no) + "'  TimeStamp_Value='" + Time_Value + "'  />");
                    }
                }

            }

            Sb_Val_Update.Append("</ParamUpdate>");
            Sb_Product.Append("</root>");


            if (RecExistsPrd.Tables[0].Rows.Count == 0)
            {

                conn.Open();
                // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);

                //SP_BulkInsert_Trans_SSDetailandVal_Test

                SqlCommand cmd = new SqlCommand("SP_BulkInsert_Trans_SSDetailandVal_FreeQty", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 240;
                cmd.Parameters.Add("@StockistCode", SqlDbType.VarChar);
                cmd.Parameters[0].Value = objParamDel.Stockist_Code;
                cmd.Parameters.Add("@iMonth", SqlDbType.VarChar);
                cmd.Parameters[1].Value = objParamDel.Month;
                cmd.Parameters.Add("@iYear", SqlDbType.VarChar);
                cmd.Parameters[2].Value = objParamDel.Year;
                cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar);
                cmd.Parameters[3].Value = div_code;
                cmd.Parameters.Add("@SF_Code", SqlDbType.VarChar);
                cmd.Parameters[4].Value = sf_code;
                cmd.Parameters.Add("@State_Code", SqlDbType.VarChar);
                cmd.Parameters[5].Value = state_code;
                cmd.Parameters.Add("@Status", SqlDbType.VarChar);
                cmd.Parameters[6].Value = objParamDel.Status;
                cmd.Parameters.Add("@ReportingTo", SqlDbType.VarChar);
                cmd.Parameters[7].Value = ReportingTo;
                cmd.Parameters.Add("@XMLProduct_Det", SqlDbType.VarChar);
                cmd.Parameters[8].Value = Sb_Product.ToString();
                cmd.Parameters.Add("@XMLTransUpdate_Val", SqlDbType.VarChar);
                cmd.Parameters[9].Value = Sb_Product.ToString();

                cmd.Parameters.Add("@inChgUserID", SqlDbType.VarChar);
                cmd.Parameters[10].Value = sf_code;
                cmd.Parameters.Add("@inChgTimeStamp", SqlDbType.BigInt);
                // cmd.Parameters[10].Value = System.Text.ASCIIEncoding.ASCII.GetBytes(TimeStamp); 
                cmd.Parameters[11].Value = decimal.Parse(TimeStamp);
                cmd.Parameters.Add("@Subdiv_Code", SqlDbType.VarChar);
                cmd.Parameters[12].Value = sub_code;

                cmd.Parameters.Add("@retValue", SqlDbType.Int);
                cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
                conn.Close();
            }
            else
            {
                if (RecExistsPrd.Tables[0].Rows.Count > 0 && (objProdData.Count != RecExistsPrd.Tables[0].Rows.Count))
                {
                    conn.Open();
                    // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);
                    SqlCommand cmd = new SqlCommand("SP_BulkInsert_Trans_SSDetailandVal_FreeQty", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 240;
                    cmd.Parameters.Add("@StockistCode", SqlDbType.VarChar);
                    cmd.Parameters[0].Value = objParamDel.Stockist_Code;
                    cmd.Parameters.Add("@iMonth", SqlDbType.VarChar);
                    cmd.Parameters[1].Value = objParamDel.Month;
                    cmd.Parameters.Add("@iYear", SqlDbType.VarChar);
                    cmd.Parameters[2].Value = objParamDel.Year;
                    cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar);
                    cmd.Parameters[3].Value = div_code;
                    cmd.Parameters.Add("@SF_Code", SqlDbType.VarChar);
                    cmd.Parameters[4].Value = sf_code;
                    cmd.Parameters.Add("@State_Code", SqlDbType.VarChar);
                    cmd.Parameters[5].Value = state_code;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar);
                    cmd.Parameters[6].Value = objParamDel.Status;
                    cmd.Parameters.Add("@ReportingTo", SqlDbType.VarChar);
                    cmd.Parameters[7].Value = ReportingTo;
                    cmd.Parameters.Add("@XMLProduct_Det", SqlDbType.VarChar);
                    cmd.Parameters[8].Value = Sb_Product.ToString();
                    cmd.Parameters.Add("@XMLTransUpdate_Val", SqlDbType.VarChar);

                    cmd.Parameters[9].Value = Sb_Product.ToString();
                    cmd.Parameters.Add("@inChgUserID", SqlDbType.VarChar);
                    cmd.Parameters[10].Value = sf_code;
                    cmd.Parameters.Add("@inChgTimeStamp", SqlDbType.BigInt);
                    //cmd.Parameters[10].Value = System.Text.ASCIIEncoding.ASCII.GetBytes(TimeStamp); 
                    cmd.Parameters[11].Value = decimal.Parse(TimeStamp);
                    cmd.Parameters.Add("@Subdiv_Code", SqlDbType.VarChar);
                    cmd.Parameters[12].Value = sub_code;

                    cmd.Parameters.Add("@retValue", SqlDbType.Int);
                    cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    // SqlCommand cmd = new SqlCommand("SP_BulkUpdate_Trans_Det_Value", conn);SP_BulkInsert_TransSSDetailandVal
                    SqlCommand cmd = new SqlCommand("SP_BulkInsert_Trans_SSDetailandVal_FreeQty", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 240;
                    cmd.Parameters.Add("@StockistCode", SqlDbType.VarChar);
                    cmd.Parameters[0].Value = objParamDel.Stockist_Code;
                    cmd.Parameters.Add("@iMonth", SqlDbType.VarChar);
                    cmd.Parameters[1].Value = objParamDel.Month;
                    cmd.Parameters.Add("@iYear", SqlDbType.VarChar);
                    cmd.Parameters[2].Value = objParamDel.Year;
                    cmd.Parameters.Add("@DivisionCode", SqlDbType.VarChar);
                    cmd.Parameters[3].Value = div_code;
                    cmd.Parameters.Add("@SF_Code", SqlDbType.VarChar);
                    cmd.Parameters[4].Value = sf_code;
                    cmd.Parameters.Add("@State_Code", SqlDbType.VarChar);
                    cmd.Parameters[5].Value = state_code;
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar);
                    cmd.Parameters[6].Value = objParamDel.Status;
                    cmd.Parameters.Add("@ReportingTo", SqlDbType.VarChar);
                    cmd.Parameters[7].Value = ReportingTo;
                    cmd.Parameters.Add("@XMLProduct_Det", SqlDbType.VarChar);
                    cmd.Parameters[8].Value = Sb_Product.ToString();
                    cmd.Parameters.Add("@XMLTransUpdate_Val", SqlDbType.VarChar);
                    cmd.Parameters[9].Value = Sb_Val_Update.ToString();
                    cmd.Parameters.Add("@inChgUserID", SqlDbType.VarChar);
                    cmd.Parameters[10].Value = sf_code;
                    cmd.Parameters.Add("@inChgTimeStamp", SqlDbType.BigInt);
                    cmd.Parameters[11].Value = decimal.Parse(TimeStamp);

                    cmd.Parameters.Add("@Subdiv_Code", SqlDbType.VarChar);
                    cmd.Parameters[12].Value = sub_code;
                    cmd.Parameters.Add("@retValue", SqlDbType.Int);
                    cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
                    conn.Close();
                }
            }

            // }

        }
        catch (Exception ex)
        {
            throw;
            // ErrorLog err = new ErrorLog();
            //// iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Entry", "FillStockiest()");
            //HttpResponse.Equals("Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
        finally
        {
        }
        return iReturn;
    }

    /*--------------Delete------------------*/
    [WebMethod(EnableSession = true)]
    public int SecSale_Entry_Delete(string objDelete)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string ProdDel;
        int Count = 0;
        TransSSDet_Free objQty = new TransSSDet_Free();

        string[] values = objDelete.Split('^');
        objQty.Division_Code = div_code;
        objQty.iMonth = values[0];
        objQty.iYear = values[1];
        objQty.Par_Stock_Code = values[2];

        sf_code = values[3];

        string sub_code = "";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        conn.Open();
        // SqlCommand cmd = new SqlCommand("SP_BulkUpdate_Trans_Det_Value", conn);SP_BulkInsert_TransSSDetailandVal
        SqlCommand cmd = new SqlCommand("SP_SecSale_Entry_Delete", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Stock_Code", objQty.Par_Stock_Code);
        cmd.Parameters.AddWithValue("@Month", objQty.iMonth);
        cmd.Parameters.AddWithValue("@Year", objQty.iYear);
        cmd.Parameters.AddWithValue("@Sub_DivCode", sub_code);
        int iReturn = cmd.ExecuteNonQuery();
        Count = 1;
        conn.Close();

        return Count;
    }
    #endregion

}
#region SSParam

public class Stock_Detail
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}
public class Field_Name
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}
public class Y_Det
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}
public class Get_OB_Free
{
    public string Product_Detail_Code { get; set; }
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Qty { get; set; }
    public string Sec_Sale_Value { get; set; }
    public string Sec_Sale_Sub { get; set; }
    public string Sec_Sale_Free { get; set; }
    public string Carry_Field_Code { get; set; }
    public string Carry_FieldName { get; set; }
    public string CalcF_Field { get; set; }
    public string CalcF_FieldName { get; set; }

    public string SS_Head_Sl_No { get; set; }
    public string MONTH { get; set; }
    public string YEAR { get; set; }
    public string Approval_Mgr { get; set; }
    public string Stockiest_Code { get; set; }

    public string TimeStamp_Head { get; set; }
    public string TimeStamp_Prd { get; set; }
    public string TimeStamp_Value { get; set; }

    public string SS_Det_Sl_No { get; set; }
    public string Sub_Needed { get; set; }
    public string Free_Needed { get; set; }

    public string[] ArrSec_Sale_Code { get; set; }
    public string[] ArrSec_Sale_Qty { get; set; }
    public string[] ArrSec_Sale_Value { get; set; }
    public string[] ArrSec_Sale_Sub { get; set; }
    public string[] ArrSec_Sale_Free { get; set; }

    public string[] ArrTimeStamp_Head { get; set; }
    public string[] ArrTimeStamp_Prd { get; set; }
    public string[] ArrTimeStamp_Value { get; set; }

}
public class SS_AllParameter_Free
{
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }
    public string Sec_Sale_Short_Name { get; set; }
    public string Sec_Sale_Sub_Name { get; set; }
    public string Sel_Sale_Operator { get; set; }
    public string value_needed { get; set; }
    public string calc_needed { get; set; }
    public string Sub_Needed { get; set; }
    public string Sub_Label { get; set; }
    public string Sub_Label_1 { get; set; }
    public string Order_by { get; set; }
    public string Carry_Fwd_Needed { get; set; }
    public string Carry_Fwd_Field { get; set; }
    public string Der_Formula { get; set; }
    public string CalculationMode { get; set; }
    public string CalcF_Field { get; set; }
    public string Calc_Disable { get; set; }
    public string Free_Needed { get; set; }

    public string tSNo { get; set; }
    public string tPName { get; set; }
    public string tPack { get; set; }
    public string tRate { get; set; }
    public string hdrcol { get; set; }

    public string DivCode { get; set; }

    public string Primary_Bill { get; set; }
}
public class ProductDet_Free
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
    public string Calc_Rate { get; set; }
    public string Prd_grp { get; set; }
    public string txtSecSale { get; set; }
    public Array ArrSec { get; set; }
    public string hdnPrdTimeStm { get; set; }
}
public class TransSSDet_Free
{
    public string Division_Code { get; set; }
    public string iMonth { get; set; }
    public string iYear { get; set; }
    public string Par_Prod_Code { get; set; }
    public string Par_Stock_Code { get; set; }
    public string Sec_Sale_Code { get; set; }

    public string Sec_Sale_Qty { get; set; }
    public string Sec_Sale_Value { get; set; }
    public string Sec_Sale_Sub { get; set; }
}
public class Primary_Field_Free
{

    public string S_Id { get; set; }
    public string Product_Name { get; set; }
    public string Product_ERP_Code { get; set; }
    public string Sale_Qty { get; set; }
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }
}
public class ParamDet_Free
{
    public string Stockist_Code { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string Status { get; set; }

    public string SecSaleCode { get; set; }
    public string txtSecSale { get; set; }
    public string txtVal { get; set; }
    public string txtSub { get; set; }
    public string txtFree { get; set; }

    public string hdnSSvalueTimeStm { get; set; }
    public string SfCode { get; set; }
}
#endregion