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
/// Summary description for DrService_Analysis_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DrService_Analysis_WebService : System.Web.Services.WebService
{

    public DrService_Analysis_WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //----- Dr Service Analysis----//

    #region DrAnalysis

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<FieldForce_Name> Get_FieldForce_Name()
    {
        List<FieldForce_Name> objField = new List<FieldForce_Name>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();

            SalesForce sf = new SalesForce();
            DataSet dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSalesForce.Tables[0].Rows)
                {
                    FieldForce_Name objFFDet = new FieldForce_Name();
                    objFFDet.Field_Sf_Code = dr["sf_code"].ToString();
                    objFFDet.Field_Sf_Name = dr["sf_name"].ToString();
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
    public List<Hospital_Detail> GetHospital(string objSfCode)
    {
        List<Hospital_Detail> objStockData = new List<Hospital_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objSfCode;

            SecSale ss = new SecSale();
            DataSet dsSale = ss.Get_Hospital(sf_code, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Hospital_Detail objch = new Hospital_Detail();
                    objch.Hospital_Code = dr["Hospital_Code"].ToString();
                    objch.Hospital_Name = dr["Hospital_Name"].ToString();
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
    public List<Product_Detail> GetProductDel(string objPrdSf)
    {
        List<Product_Detail> objStockData = new List<Product_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objPrdSf;

            SecSale ss = new SecSale();
            DataSet dsSale = ss.Get_rpt_Product(sf_code, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Product_Detail objch = new Product_Detail();
                    objch.Product_Code = dr["Product_Detail_Code"].ToString();
                    objch.Product_Name = dr["Product_Detail_Name"].ToString();
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
    public List<Doctor_Detail> GetDoctorList(string objDrLst)
    {
        List<Doctor_Detail> objStockData = new List<Doctor_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objDrLst;

            SecSale ss = new SecSale();
            DataSet dsSale = ss.Get_rpt_DoctorList(sf_code, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Doctor_Detail objch = new Doctor_Detail();
                    objch.ListedDrCode = dr["ListedDrCode"].ToString();
                    objch.ListedDr_Name = dr["ListedDr_Name"].ToString();
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
    public List<Year_Det> FillYear()
    {
        List<Year_Det> objYearDel = new List<Year_Det>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            Year_Det objYear = new Year_Det();

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
    public List<Chemist_DDL> GetChemist_DDL(string objSfCode)
    {
        List<Chemist_DDL> objChemist = new List<Chemist_DDL>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objSfCode;

            SecSale ss = new SecSale();
            DataSet dsSale = ss.Bind_Chemist_DDL(sf_code, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Chemist_DDL objch = new Chemist_DDL();
                    objch.Chemists_Code = dr["Chemists_Code"].ToString();
                    objch.Chemists_Name = dr["Chemists_Name"].ToString();
                    objChemist.Add(objch);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objChemist;
    }

    //-------------- Dr CRM Admin Appove-------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ReCeived_Dr> GetDrService_CRM_Approval(string objModeType)
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string S_Code = string.Empty;
        string SF_Type = string.Empty;
        string Sf_Name = string.Empty;

        //  DataSet dsFieldForce;

        if (sf_code == "admin")
        {
            SF_Type = "1";
        }
        else
        {
            SF_Type = "0";
        }

        //SalesForce sf = new SalesForce();

        //if (SF_Type == "1")
        //{
        //    dsFieldForce = sf.getSecSales_MR(div_code, "admin");
        //}
        //else
        //{
        //    dsFieldForce = sf.getSecSales_MR(div_code, sf_code);
        //}

        List<ReCeived_Dr> objDrDetail = new List<ReCeived_Dr>();

        //if (dsFieldForce.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dsFieldForce.Tables[0].Rows)
        //    {
        //        string SfCode = dr["SF_Code"].ToString();
        //        S_Code += SfCode + ",";
        //    }

        //    S_Code = S_Code.Substring(0, S_Code.Length - 1);

        string[] Data = objModeType.Split('^');

        DataSet dsListedDR = lst.Get_Dr_Service_CRM_Recevied_Admin(sf_code, div_code, Data[0], Data[1], Data[2], Data[3]);

        string Mode = Data[3];

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            int Cnt = 0;
            int ReceiveCnt = 0;

            foreach (DataRow dr in dsListedDR.Tables[0].Rows)
            {
                Cnt = Cnt + 1;

                ReCeived_Dr objLstDr = new ReCeived_Dr();
                objLstDr.Sl_No = Convert.ToString(Cnt);
                objLstDr.Sf_Code = dr["Sf_Code"].ToString();
                objLstDr.Sf_Name = dr["Sf_Name"].ToString();
                objLstDr.Sf_HQ = dr["Sf_HQ"].ToString();
                objLstDr.Designation = dr["sf_Designation_Short_Name"].ToString();
                if (Mode == "Doctor")
                {
                    objLstDr.Lst_Dr_Code = dr["ListedDrCode"].ToString();
                    objLstDr.Lst_Dr_Name = dr["ListedDr_Name"].ToString();
                    objLstDr.Doc_Cat_ShortName = dr["Doc_Cat_ShortName"].ToString();
                    objLstDr.Doc_Spec_ShortName = dr["Doc_Spec_ShortName"].ToString();
                    objLstDr.Doc_Class_ShortName = dr["Doc_Class_ShortName"].ToString();
                    objLstDr.Doc_Qua_Name = dr["Doc_Qua_Name"].ToString();
                    objLstDr.Approved_Date = dr["Approved_Date"].ToString();
                }
                else
                {
                    objLstDr.Chemists_Code = dr["Chemist_Code"].ToString();
                    objLstDr.Chemists_Name = dr["Chemist_Name"].ToString();
                    objLstDr.Chemists_Contact = dr["Contact"].ToString();
                    objLstDr.Chemists_Address1 = dr["Address"].ToString();
                    objLstDr.Chemists_Phone = dr["Phone"].ToString();
                    objLstDr.Approved_Date = dr["Applied_Date"].ToString();
                }
                objLstDr.territory_Name = dr["territory_Name"].ToString();
                objLstDr.Service_Amount = dr["Service_Amt"].ToString();
                objLstDr.Service_Req = dr["Service_Req"].ToString();
                objLstDr.Dr_Sl_No = dr["DrSl_No"].ToString();
                objLstDr.Cur_Tot = dr["CurSup_Tot"].ToString();
                objLstDr.Pot_Tot = dr["Poten_Tot"].ToString();
                objLstDr.TotExpen_Amt = dr["Total_Bus_Exp"].ToString();

                objLstDr.SCode = sf_code;
                objLstDr.F_Code = dr["Sf_Code"].ToString();

                //objLstDr.Count = Convert.ToString(dsListedDR.Tables[0].Rows.Count);

                int SerType = Convert.ToInt32(dr["Ser_Type"].ToString());

                //if (SerType == 1)
                // {
                ReceiveCnt = ReceiveCnt + 1;
                //}

                objLstDr.Count = Convert.ToString(ReceiveCnt);
                objLstDr.Status = dr["Ser_Type"].ToString();

                objDrDetail.Add(objLstDr);

            }
        }
        //   }

        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int BulkUpdate_DoctorDetailAdmin(string objDr)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string SF_Type = string.Empty;
        string Sf_Name = string.Empty;

        if (sf_code == "admin")
        {
            SF_Type = "2";
        }
        else if (sf_code.Contains("MGR"))
        {
            SF_Type = "1";
        }

        int iRet = 1;

        string[] Data = objDr.Split('^');
        string Type = Data[0];
        string DocCode = Data[1].TrimEnd(',');

        SecSale SS = new SecSale();
        iRet = SS.Update_Trans_Service_ConfirmAdmin(div_code, SF_Type, sf_code, DocCode, Type);

        return iRet;
    }

    //---------- Dr CRM MGR Appoval-------------------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ReCeived_Dr> GetNoof_Service_Recevied(string ModeType, string SS_DivCode)
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string S_Code = string.Empty;
        string SF_Type = string.Empty;
        string Sf_Name = string.Empty;

        DataSet dsFieldForce;

        div_code = SS_DivCode;

        HttpContext.Current.Session["div_code"] = div_code;

        if (sf_code == "admin")
        {
            SF_Type = "1";
        }
        else
        {
            SF_Type = "0";
        }

        //SalesForce sf = new SalesForce();

        //if (SF_Type == "1")
        //{
        //    dsFieldForce = sf.getSecSales_MR(div_code, "admin");
        //}
        //else
        //{
        //    dsFieldForce = sf.getSecSales_MR(div_code, sf_code);
        //}

        List<ReCeived_Dr> objDrDetail = new List<ReCeived_Dr>();

        //if (dsFieldForce.Tables[0].Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dsFieldForce.Tables[0].Rows)
        //    {
        //        string SfCode = dr["SF_Code"].ToString();
        //        S_Code += SfCode + ",";
        //    }

        //    S_Code = S_Code.Substring(0, S_Code.Length - 1);

        //  DataSet dsCount = lst.Get_Dr_No_of_Servcie_Count(S_Code, div_code);

        // if (dsCount.Tables[0].Rows.Count > 0)
        // {

        // }

        DataSet dsListedDR = lst.Get_Dr_Service_CRM_Recevied(sf_code, div_code, ModeType, sf_code);

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            int Cnt = 0;

            foreach (DataRow dr in dsListedDR.Tables[0].Rows)
            {
                Cnt = Cnt + 1;

                ReCeived_Dr objLstDr = new ReCeived_Dr();
                objLstDr.Sl_No = Convert.ToString(Cnt);
                objLstDr.Sf_Code = dr["Sf_Code"].ToString();
                objLstDr.Sf_Name = dr["Sf_Name"].ToString();
                objLstDr.Sf_HQ = dr["Sf_HQ"].ToString();
                objLstDr.Designation = dr["sf_Designation_Short_Name"].ToString();

                if (ModeType == "Doctor")
                {
                    objLstDr.Lst_Dr_Code = dr["ListedDrCode"].ToString();
                    objLstDr.Lst_Dr_Name = dr["ListedDr_Name"].ToString();
                    objLstDr.Doc_Cat_ShortName = dr["Doc_Cat_ShortName"].ToString();
                    objLstDr.Doc_Spec_ShortName = dr["Doc_Spec_ShortName"].ToString();
                    objLstDr.Doc_Class_ShortName = dr["Doc_Class_ShortName"].ToString();
                    objLstDr.Doc_Qua_Name = dr["Doc_Qua_Name"].ToString();
                }
                else
                {
                    objLstDr.Chemists_Code = dr["Chemist_Code"].ToString();
                    objLstDr.Chemists_Name = dr["Chemist_Name"].ToString();
                    objLstDr.Chemists_Contact = dr["Contact"].ToString();
                    objLstDr.Chemists_Address1 = dr["Address"].ToString();
                    objLstDr.Chemists_Phone = dr["Phone"].ToString();
                }
                objLstDr.territory_Name = dr["territory_Name"].ToString();
                objLstDr.Service_Amount = dr["Service_Amt"].ToString();
                objLstDr.Service_Req = dr["Service_Req"].ToString();
                objLstDr.Dr_Sl_No = dr["Sl_No"].ToString();
                objLstDr.Applied_Date = dr["Applied_Date"].ToString();
                objLstDr.Count = Convert.ToString(dsListedDR.Tables[0].Rows.Count);
                objLstDr.Ser_No = dr["Sl_No"].ToString();
                objLstDr.Cur_Tot = dr["CurSup_Tot"].ToString();
                objLstDr.Pot_Tot = dr["Poten_Tot"].ToString();
                objLstDr.TotExpen_Amt = dr["Total_Bus_Exp"].ToString();
                objLstDr.SCode = sf_code;
                objLstDr.F_Code = dr["Sf_Code"].ToString();

                objDrDetail.Add(objLstDr);

            }
        }
        //  }

        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int BulkUpdate_DoctorDetailAdmin(string objDr, string SS_DivCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string SF_Type = string.Empty;
        string Sf_Name = string.Empty;

        div_code = SS_DivCode;

        HttpContext.Current.Session["div_code"] = div_code;

        if (sf_code == "admin")
        {
            SF_Type = "2";
        }
        else if (sf_code.Contains("MGR"))
        {
            SF_Type = "1";
        }

        int iRet = 1;

        string[] Data = objDr.Split('^');
        string Type = Data[0];
        string DocCode = Data[1].TrimEnd(',');

        SecSale SS = new SecSale();
        iRet = SS.Update_Trans_Service_ConfirmAdmin(div_code, SF_Type, sf_code, DocCode, Type);

        return iRet;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Division_DDL> GetDivision_DDL(string SS_SCode, string SS_Stype)
    {

        // string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        Product prd = new Product();
        DataSet dsdiv = prd.getMultiDivsf_Name(SS_SCode);

        List<Division_DDL> objChData = new List<Division_DDL>();

        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                string strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);

                Division dv = new Division();
                DataSet dsDivision = new DataSet();
                dsDivision = dv.getMultiDivision(strMultiDiv);
                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDivision.Tables[0].Rows)
                    {
                        Division_DDL objch = new Division_DDL();
                        objch.Division_Code = dr["Division_Code"].ToString();
                        objch.Division_Name = dr["Division_Name"].ToString();
                        objChData.Add(objch);
                    }
                }
            }
        }

        return objChData;
    }

    //----------- Dr CRM Service Close--------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<ListedDr_Detail> GetListedDoctorDetail(string objDrCode)
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        DataSet dsListedDR = lst.Get_Service_CRM_Dr_Detail(div_code, objDrCode, sf_code);

        List<ListedDr_Detail> objDrDetail = new List<ListedDr_Detail>();

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsListedDR.Tables[0].Rows)
            {
                ListedDr_Detail objLstDr = new ListedDr_Detail();
                objLstDr.DoctorCode = dr["ListedDrCode"].ToString();
                objLstDr.DoctorName = dr["ListedDr_Name"].ToString();
                objLstDr.Category = dr["Doc_Cat_Name"].ToString();
                objLstDr.Speciality = dr["Doc_Special_Name"].ToString();
                objLstDr.Qualification = dr["Doc_QuaName"].ToString();
                objDrDetail.Add(objLstDr);
            }

        }

        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ServiceDr_Det> GetServcieDoctorDetail(string DrCode, string DrSlNo)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string Sf_Name = HttpContext.Current.Session["Sf_MRCode"].ToString();

        String[] myList;

        List<ServiceDr_Det> objServcieData = new List<ServiceDr_Det>();

        SecSale ss = new SecSale();
        DataSet dsService = ss.Get_Close_Service_Detail(div_code, DrCode, Sf_Name, DrSlNo);

        foreach (DataRow dr in dsService.Tables[0].Rows)
        {
            ServiceDr_Det objSerData = new ServiceDr_Det();
            objSerData.Sl_No = dr["Sl_No"].ToString();
            objSerData.Created_Date = dr["Created_Date"].ToString();
            objSerData.Service_Req = dr["Service_Req"].ToString();
            objSerData.Service_Amt = dr["Service_Amt"].ToString();
            objSerData.TargetAmt = dr["Total_Business_Expect"].ToString();
            // objSerData.Close_Status = dr["Close_Service_Dr"].ToString(); 
            objSerData.Close_Status = dr["Ser_Type"].ToString();

            DataSet dsProd = ss.Get_Business_Given_ProductTotal(div_code, DrCode, Sf_Name, objSerData.Sl_No);

            List<string> PrdList = new List<string>();

            foreach (DataRow drPrd in dsProd.Tables[0].Rows)
            {
                objSerData.Trans_SlNo = drPrd["Trans_Sl_No"].ToString();
                objSerData.Product_Total = drPrd["Product_Total"].ToString();
                objSerData.Cls_Service = drPrd["Close_Service_Business"].ToString();

                string strPrd = objSerData.Trans_SlNo + "^" + objSerData.Product_Total + "^" + objSerData.Cls_Service;
                PrdList.Add(strPrd);
            }

            myList = PrdList.ConvertAll<String>(p => p.ToString()).ToArray<String>();

            objSerData.ArrPrdDel = myList;

            objServcieData.Add(objSerData);
        }

        return objServcieData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void Add_Dr_Service_Close(List<string> objServiceDel)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string Sf_Name = HttpContext.Current.Session["Sf_MRCode"].ToString();

        for (int i = 0; i < objServiceDel.Count; i++)
        {
            string[] Data = objServiceDel[i].Split('^');

            string Sl_No = Data[0];
            string Doctor_Code = Data[1];
            string Trans_Sl_No = Data[2];
            string CloseStatus = Data[3];
            string Status = Data[4];

            int iReturn;

            SecSale ss = new SecSale();
            iReturn = ss.Update_Dr_Service_Close(div_code, Doctor_Code, Sf_Name, Sl_No, Trans_Sl_No, CloseStatus, Status);
        }


    }

    //------------ DR CRM Service Status---------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<FieldForce_Name> GetFieldForceName(string SS_DivCode)
    {
        List<FieldForce_Name> objField = new List<FieldForce_Name>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();
            string sf_Type = HttpContext.Current.Session["sf_type"].ToString();

            DataSet dsSalesForce;

            div_code = SS_DivCode;
            HttpContext.Current.Session["div_code"] = div_code;

            if (sf_Type == "1" || sf_Type == "2")
            {
                SecSale ss = new SecSale();
                dsSalesForce = ss.User_MRwise_Hierarchy(div_code, sf_code);
            }
            else
            {
                sf_code = "admin";
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
            }

            //dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSalesForce.Tables[0].Rows)
                {
                    FieldForce_Name objFFDet = new FieldForce_Name();
                    objFFDet.Field_Sf_Code = dr["sf_code"].ToString();
                    objFFDet.Field_Sf_Name = dr["sf_name"].ToString();
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

    #endregion
}

#region Dr_Anly_Param

public class FieldForce_Name
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}
public class Hospital_Detail
{
    public string Hospital_Code { get; set; }
    public string Hospital_Name { get; set; }
}
public class Product_Detail
{
    public string Product_Code { get; set; }
    public string Product_Name { get; set; }
}
public class Doctor_Detail
{
    public string ListedDrCode { get; set; }
    public string ListedDr_Name { get; set; }
}
public class Year_Det
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}
public class Chemist_DDL
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
}
public class ReCeived_Dr
{
    public string Sl_No { get; set; }
    public string Sf_Code { get; set; }
    public string Sf_Name { get; set; }
    public string Sf_HQ { get; set; }
    public string Designation { get; set; }
    public string Lst_Dr_Code { get; set; }
    public string Lst_Dr_Name { get; set; }
    public string Doc_Cat_ShortName { get; set; }
    public string Doc_Spec_ShortName { get; set; }
    public string Doc_Class_ShortName { get; set; }
    public string Doc_Qua_Name { get; set; }
    public string territory_Name { get; set; }
    public string Service_Req { get; set; }
    public string Service_Amount { get; set; }
    public string Count { get; set; }
    public string Dr_Sl_No { get; set; }
    public string Status { get; set; }
    public string Approved_Date { get; set; }
    public string Cur_Tot { get; set; }
    public string Pot_Tot { get; set; }
    public string TotExpen_Amt { get; set; }

    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
    public string Chemists_Contact { get; set; }
    public string Chemists_Address1 { get; set; }
    public string Chemists_Phone { get; set; }

    public string SCode { get; set; }
    public string F_Code { get; set; }

    public string Applied_Date { get; set; }
    public string Ser_No { get; set; }
}
public class Division_DDL
{
    public string Division_Code { get; set; }
    public string Division_Name { get; set; }
}
public class ListedDr_Detail
{
    public string DoctorCode { get; set; }
    public string DoctorName { get; set; }
    public string Category { get; set; }
    public string Speciality { get; set; }
    public string Qualification { get; set; }

}
public class ServiceDr_Det
{
    public string Sl_No { get; set; }
    public string Created_Date { get; set; }
    public string Service_Req { get; set; }
    public string Service_Amt { get; set; }
    public string TargetAmt { get; set; }
    public string Close_Status { get; set; }

    public string Trans_SlNo { get; set; }
    public string Product_Total { get; set; }
    public string Cls_Service { get; set; }

    public Array ArrPrdDel { get; set; }
}

#endregion
