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
using System.Drawing.Text;


/// <summary>
/// Summary description for DrService_Print_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DrService_Print_WS : System.Web.Services.WebService
{

    public DrService_Print_WS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region CRMPrint

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DrServiceDetail_Print> Bind_Doctor_Service(DrServiceDetail_Print objDrDetail)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();

        List<DrServiceDetail_Print> objDrService = new List<DrServiceDetail_Print>();

        DrServiceDetail_Print objData = new DrServiceDetail_Print();
        objData.Sl_No = objDrDetail.Sl_No;
        objData.DoctorCode = objDrDetail.DoctorCode;

        SecSale ss = new SecSale();
        DataSet dsDr = ss.GetDr_ServiceCRM_Print(objData.Sl_No, div_code, objData.DoctorCode);

        foreach (DataRow dr in dsDr.Tables[0].Rows)
        {
            DrServiceDetail_Print objDr = new DrServiceDetail_Print();
            objDr.Sl_No = dr["Sl_No"].ToString();
            objDr.DoctorCode = dr["ListedDrCode"].ToString();
            objDr.DoctorName = dr["ListedDr_Name"].ToString();
            objDr.Address = dr["ListedDr_Address1"].ToString();
            objDr.Category = dr["Doc_Cat_Name"].ToString();
            objDr.Speciality = dr["Doc_Special_Name"].ToString();
            objDr.Qualification = dr["Doc_QuaName"].ToString();
            objDr.Class = dr["Doc_ClsName"].ToString();
            objDr.Mobile = dr["ListedDr_Mobile"].ToString();
            objDr.Email = dr["ListedDr_Email"].ToString();
            objDr.TotalBusReturn_Amt = dr["Total_Business_Expect"].ToString();
            objDr.ServiceAmt_tillDate = dr["Sevice_Amt_till_Date"].ToString();
            objDr.ROI_Dur_Month = dr["ROI_Month"].ToString();
            objDr.Service_Req = dr["Service_Req"].ToString();
            objDr.Service_Amt = dr["Service_Amt"].ToString();
            objDr.Specific_Act = dr["Specific_Remark"].ToString();

            objDr.ddlChemist_1 = dr["Prescr_Chemist_1"].ToString();
            objDr.ddlChemist_2 = dr["Prescr_Chemist_2"].ToString();
            objDr.ddlChemist_3 = dr["Prescr_Chemist_3"].ToString();
            objDr.ddlStockist_1 = dr["Stockist_1"].ToString();
            objDr.ddlStockist_2 = dr["Stockist_2"].ToString();
            objDr.ddlStockist_3 = dr["Stockist_3"].ToString();

            objDr.Prd_Sl_No = dr["Prd_Sl_No"].ToString();
            objDr.Cur_Prod_Code_1 = dr["Cur_Prod_Code_1"].ToString();
            objDr.Cur_Prod_Price_1 = dr["Cur_Prod_Price_1"].ToString();
            objDr.Cur_Prod_Qty_1 = dr["Cur_Prod_Qty_1"].ToString();
            objDr.Cur_Prod_Value_1 = dr["Cur_Prod_Value_1"].ToString();
            objDr.Cur_Prod_Code_2 = dr["Cur_Prod_Code_2"].ToString();
            objDr.Cur_Prod_Price_2 = dr["Cur_Prod_Price_2"].ToString();
            objDr.Cur_Prod_Qty_2 = dr["Cur_Prod_Qty_2"].ToString();
            objDr.Cur_Prod_Value_2 = dr["Cur_Prod_Value_2"].ToString();
            objDr.Cur_Prod_Code_3 = dr["Cur_Prod_Code_3"].ToString();
            objDr.Cur_Prod_Price_3 = dr["Cur_Prod_Price_3"].ToString();
            objDr.Cur_Prod_Qty_3 = dr["Cur_Prod_Qty_3"].ToString();
            objDr.Cur_Prod_Value_3 = dr["Cur_Prod_Value_3"].ToString();
            objDr.Cur_Prod_Code_4 = dr["Cur_Prod_Code_4"].ToString();
            objDr.Cur_Prod_Price_4 = dr["Cur_Prod_Price_4"].ToString();
            objDr.Cur_Prod_Qty_4 = dr["Cur_Prod_Qty_4"].ToString();
            objDr.Cur_Prod_Value_4 = dr["Cur_Prod_Value_4"].ToString();
            objDr.Cur_Prod_Code_5 = dr["Cur_Prod_Code_5"].ToString();
            objDr.Cur_Prod_Price_5 = dr["Cur_Prod_Price_5"].ToString();
            objDr.Cur_Prod_Qty_5 = dr["Cur_Prod_Qty_5"].ToString();
            objDr.Cur_Prod_Value_5 = dr["Cur_Prod_Value_5"].ToString();
            objDr.Cur_Prod_Code_6 = dr["Cur_Prod_Code_6"].ToString();
            objDr.Cur_Prod_Price_6 = dr["Cur_Prod_Price_6"].ToString();
            objDr.Cur_Prod_Qty_6 = dr["Cur_Prod_Qty_6"].ToString();
            objDr.Cur_Prod_Value_6 = dr["Cur_Prod_Value_6"].ToString();
            objDr.Cur_Total = dr["Cur_Total"].ToString();

            objDr.Potl_Prod_Code_1 = dr["Potl_Prod_Code_1"].ToString();
            objDr.Potl_Prod_Price_1 = dr["Potl_Prod_Price_1"].ToString();
            objDr.Potl_Prod_Qty_1 = dr["Potl_Prod_Qty_1"].ToString();
            objDr.Potl_Prod_Value_1 = dr["Potl_Prod_Value_1"].ToString();
            objDr.Potl_Prod_Code_2 = dr["Potl_Prod_Code_2"].ToString();
            objDr.Potl_Prod_Price_2 = dr["Potl_Prod_Price_2"].ToString();
            objDr.Potl_Prod_Qty_2 = dr["Potl_Prod_Qty_2"].ToString();
            objDr.Potl_Prod_Value_2 = dr["Potl_Prod_Value_2"].ToString();
            objDr.Potl_Prod_Code_3 = dr["Potl_Prod_Code_3"].ToString();
            objDr.Potl_Prod_Price_3 = dr["Potl_Prod_Price_3"].ToString();
            objDr.Potl_Prod_Qty_3 = dr["Potl_Prod_Qty_3"].ToString();
            objDr.Potl_Prod_Value_3 = dr["Potl_Prod_Value_3"].ToString();
            objDr.Potl_Prod_Code_4 = dr["Potl_Prod_Code_4"].ToString();
            objDr.Potl_Prod_Price_4 = dr["Potl_Prod_Price_4"].ToString();
            objDr.Potl_Prod_Qty_4 = dr["Potl_Prod_Qty_4"].ToString();
            objDr.Potl_Prod_Value_4 = dr["Potl_Prod_Value_4"].ToString();
            objDr.Potl_Prod_Code_5 = dr["Potl_Prod_Code_5"].ToString();
            objDr.Potl_Prod_Price_5 = dr["Potl_Prod_Price_5"].ToString();
            objDr.Potl_Prod_Qty_5 = dr["Potl_Prod_Qty_5"].ToString();
            objDr.Potl_Prod_Value_5 = dr["Potl_Prod_Value_5"].ToString();
            objDr.Potl_Prod_Code_6 = dr["Potl_Prod_Code_6"].ToString();
            objDr.Potl_Prod_Price_6 = dr["Potl_Prod_Price_6"].ToString();
            objDr.Potl_Prod_Qty_6 = dr["Potl_Prod_Qty_6"].ToString();
            objDr.Potl_Prod_Value_6 = dr["Potl_Prod_Value_6"].ToString();
            objDr.Potl_Total = dr["Potential_Total"].ToString();
            //objDr.Service_Sl_No = dr["Service_Sl_No"].ToString();

            objDr.First_Lev_Name = dr["First_Lev_Name"].ToString();
            objDr.Second_Lev_Name = dr["Second_Lev_Name"].ToString();
            objDr.Third_Lev_Name = dr["Third_Lev_Name"].ToString();
            objDr.Four_Lev_Name = dr["Four_Lev_Name"].ToString();
            objDr.Fivth_Lev_Name = dr["Fivth_Lev_Name"].ToString();
            objDr.Six_Lev_Name = dr["Six_Lev_Name"].ToString();
            objDr.Seven_Lev_Name = dr["Seven_Lev_Name"].ToString();
            objDr.Applied_Date = dr["Applied_Date"].ToString();
            objDr.Approved_Date = dr["Approved_Date"].ToString();
            objDr.Confirmed_Date = dr["Confirmed_Date"].ToString();
            objDr.Applied_By = dr["Applied_By"].ToString();
            objDr.Approved_By = dr["Approved_By"].ToString();
            objDr.Confirm_By = dr["Confirm_By"].ToString();

            objDr.Sf_HQ = dr["Sf_HQ"].ToString();
            objDr.Sf_Desig = dr["Sf_Desig"].ToString();
            objDr.Division_Name = dr["Division_Name"].ToString();
            objDr.Sf_Type = Sf_Type;
            objDr.Status = dr["Ser_Type"].ToString();

            objDrService.Add(objDr);

        }

        return objDrService;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ProductDetail_Print> GetProductDetail()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_Print objProd = new ProductDetail_Print();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //DataSet dsRate = ss.getAddionalRptSaleMaster(div_code);
        //if (dsRate != null)
        //{
        //    if (dsRate.Tables[0].Rows.Count > 0)

        //    objProd.Calc_Rate = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();

        //    Prod_Grp = dsRate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();

        //}

        objProd.Calc_Rate = "R";
        Prod_Grp = "0";

        SubDivision sb = new SubDivision();
        DataSet dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }

        DataSet dsProd = ss.Get_MRwise_ProductDetail(div_code, state_code, Prod_Grp, sub_code);

        List<ProductDetail_Print> objProdDel = new List<ProductDetail_Print>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ProductDetail_Print objPrd = new ProductDetail_Print();
            objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
            objPrd.Product_Detail_Name = drow["Product_Detail_Name"].ToString();
            objProd.Calc_Rate = "R";
            if (objProd.Calc_Rate == "R")
            {
                objPrd.Rate = drow["Retailor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "M")
            {
                objPrd.Rate = drow["MRP_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "D")
            {
                objPrd.Rate = drow["Distributor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "N")
            {
                objPrd.Rate = drow["NSR_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "T")
            {
                objPrd.Rate = drow["Target_Price"].ToString();
            }

            objProdDel.Add(objPrd);
        }

        return objProdDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Chemist_Pr> GetChemist(string DoctorCode)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR ch = new ListedDR();
        DataSet dsCh = ch.BingChemistDDL(DoctorCode, sf_code);

        List<Chemist_Pr> objChData = new List<Chemist_Pr>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_Pr objch = new Chemist_Pr();
            objch.Chemists_Code = dr["Chemists_Code"].ToString();
            objch.Chemists_Name = dr["chemists_name"].ToString();
            objChData.Add(objch);
        }

        return objChData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Pr> GetStockist()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        DCR dc = new DCR();
        DataSet dsSale = dc.getStockiest(sf_code, div_code);

        List<Stockist_Pr> objStockData = new List<Stockist_Pr>();

        foreach (DataRow dr in dsSale.Tables[0].Rows)
        {
            Stockist_Pr objch = new Stockist_Pr();
            objch.Stockist_Code = dr["Stockist_Code"].ToString();
            objch.Stockist_Name = dr["Stockist_Name"].ToString();
            objStockData.Add(objch);
        }

        return objStockData;
    }

    #endregion
}

#region Dr_Print

public class DrServiceDetail_Print
{
    public string Sf_Type { get; set; }

    public string DoctorCode { get; set; }
    public string DoctorName { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public string Qualification { get; set; }
    public string Speciality { get; set; }
    public string Class { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string ServiceAmt_tillDate { get; set; }
    public string Business_Date { get; set; }
    public string TotalBusReturn_Amt { get; set; }
    public string ROI_Dur_Month { get; set; }
    public string Service_Req { get; set; }
    public string Service_Amt { get; set; }
    public string Specific_Act { get; set; }
    public string ddlChemist_1 { get; set; }
    public string ddlChemist_2 { get; set; }
    public string ddlChemist_3 { get; set; }
    public string ddlStockist_1 { get; set; }
    public string ddlStockist_2 { get; set; }
    public string ddlStockist_3 { get; set; }

    public string FinancialYear { get; set; }
    public string TransMonth { get; set; }
    public string TransYear { get; set; }

    public string Service_Statement { get; set; }

    public string Sf_Name { get; set; }
    public string Sf_Mgr_1 { get; set; }
    public string Sf_Mgr_2 { get; set; }
    public string Sf_Mgr_3 { get; set; }
    public string Sf_Mgr_4 { get; set; }
    public string Sf_Mgr_5 { get; set; }

    public string Cur_Prod_Code_1 { get; set; }
    public string Cur_Prod_Price_1 { get; set; }
    public string Cur_Prod_Qty_1 { get; set; }
    public string Cur_Prod_Value_1 { get; set; }
    public string Cur_Prod_Code_2 { get; set; }
    public string Cur_Prod_Price_2 { get; set; }
    public string Cur_Prod_Qty_2 { get; set; }
    public string Cur_Prod_Value_2 { get; set; }
    public string Cur_Prod_Code_3 { get; set; }
    public string Cur_Prod_Price_3 { get; set; }
    public string Cur_Prod_Qty_3 { get; set; }
    public string Cur_Prod_Value_3 { get; set; }
    public string Cur_Prod_Code_4 { get; set; }
    public string Cur_Prod_Price_4 { get; set; }
    public string Cur_Prod_Qty_4 { get; set; }
    public string Cur_Prod_Value_4 { get; set; }
    public string Cur_Prod_Code_5 { get; set; }
    public string Cur_Prod_Price_5 { get; set; }
    public string Cur_Prod_Qty_5 { get; set; }
    public string Cur_Prod_Value_5 { get; set; }
    public string Cur_Prod_Code_6 { get; set; }
    public string Cur_Prod_Price_6 { get; set; }
    public string Cur_Prod_Qty_6 { get; set; }
    public string Cur_Prod_Value_6 { get; set; }
    public string Cur_Total { get; set; }

    public string Potl_Prod_Code_1 { get; set; }
    public string Potl_Prod_Price_1 { get; set; }
    public string Potl_Prod_Qty_1 { get; set; }
    public string Potl_Prod_Value_1 { get; set; }
    public string Potl_Prod_Code_2 { get; set; }
    public string Potl_Prod_Price_2 { get; set; }
    public string Potl_Prod_Qty_2 { get; set; }
    public string Potl_Prod_Value_2 { get; set; }
    public string Potl_Prod_Code_3 { get; set; }
    public string Potl_Prod_Price_3 { get; set; }
    public string Potl_Prod_Qty_3 { get; set; }
    public string Potl_Prod_Value_3 { get; set; }
    public string Potl_Prod_Code_4 { get; set; }
    public string Potl_Prod_Price_4 { get; set; }
    public string Potl_Prod_Qty_4 { get; set; }
    public string Potl_Prod_Value_4 { get; set; }
    public string Potl_Prod_Code_5 { get; set; }
    public string Potl_Prod_Price_5 { get; set; }
    public string Potl_Prod_Qty_5 { get; set; }
    public string Potl_Prod_Value_5 { get; set; }
    public string Potl_Prod_Code_6 { get; set; }
    public string Potl_Prod_Price_6 { get; set; }
    public string Potl_Prod_Qty_6 { get; set; }
    public string Potl_Prod_Value_6 { get; set; }
    public string Potl_Total { get; set; }

    public string Sl_No { get; set; }
    public string Prd_Sl_No { get; set; }
    public string Service_Sl_No { get; set; }
    public string ButtonVal { get; set; }
    public string Status { get; set; }

    public string First_Lev_Name { get; set; }
    public string Second_Lev_Name { get; set; }
    public string Third_Lev_Name { get; set; }
    public string Four_Lev_Name { get; set; }
    public string Fivth_Lev_Name { get; set; }
    public string Six_Lev_Name { get; set; }
    public string Seven_Lev_Name { get; set; }
    public string Applied_Date { get; set; }
    public string Approved_Date { get; set; }
    public string Confirmed_Date { get; set; }
    public string Applied_By { get; set; }
    public string Approved_By { get; set; }
    public string Confirm_By { get; set; }
    public string Sf_HQ { get; set; }
    public string Sf_Desig { get; set; }
    public string Division_Name { get; set; }

}

public class ProductDetail_Print
{
    public string Calc_Rate { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Rate { get; set; }
}

public class Chemist_Pr
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
}
public class Stockist_Pr
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}

#endregion