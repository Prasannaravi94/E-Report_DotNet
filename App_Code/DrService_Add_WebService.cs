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
/// Summary description for DrService_Add_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DrService_Add_WebService : System.Web.Services.WebService
{

    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    #endregion


    #region DRAdd

    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsts = new DataSet();

    string S_Code = string.Empty;
    string doctorcode = string.Empty;
    string Mode = string.Empty;
    string SCode = string.Empty;
    string CrmStatus = string.Empty;
    string F_Code = string.Empty;

    public DrService_Add_WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ListedDoctor> GetListedDoctorDetail(string objDrCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR lst = new ListedDR();
        DataSet dsListedDR = lst.ViewListedDr(objDrCode);

        SecSale ss = new SecSale();
        DataSet dsDrLSt = ss.Get_Dr_TotalServiceAmt(objDrCode, div_code);

        List<ListedDoctor> objDrDetail = new List<ListedDoctor>();

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsListedDR.Tables[0].Rows)
            {
                ListedDoctor objLstDr = new ListedDoctor();
                objLstDr.DoctorCode = dr["ListedDrCode"].ToString();
                objLstDr.DoctorName = dr["ListedDr_Name"].ToString();
                objLstDr.DoctorAddress = dr["ListedDr_Address1"].ToString();
                objLstDr.Category = dr["Doc_Cat_Name"].ToString();
                objLstDr.Speciality = dr["Doc_Special_Name"].ToString();
                objLstDr.Qualification = dr["Doc_QuaName"].ToString();
                objLstDr.Class = dr["Doc_ClsName"].ToString();
                objLstDr.Mobile = dr["ListedDr_Mobile"].ToString();
                objLstDr.Email = dr["ListedDr_Email"].ToString();

                if (dsDrLSt.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drlt in dsDrLSt.Tables[0].Rows)
                    {
                        objLstDr.ServiceAmt = drlt["Service_Amt"].ToString();
                        objLstDr.BusinessAmt = drlt["Product_Total"].ToString();
                    }
                }
                else
                {
                    objLstDr.ServiceAmt = "";
                    objLstDr.BusinessAmt = "";
                }

                objDrDetail.Add(objLstDr);

            }

        }

        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public DataTable GetVisitedList(string objNoOfVisit)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;

        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));

        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();

        int Month = Convert.ToInt32(currentMonth);

        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn);
            // months--; cmonth++;
            MonthCnt += 1;
            Month = Convert.ToInt32(Month) - 1;
        }

        int j = 0;
        DataTable SfCodes = sf1.getMRJointWork_camp(div_code, sf_code, 0);
        DataTable dtsf_code = new DataTable();
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        for (int i = 0; i < SfCodes.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"]);
        }

        DataSet dsmgrsf = new DataSet();
        dsmgrsf.Tables.Add(SfCodes);   //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Listeddr_Period_MGR_Proc";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@ListDrCode", objNoOfVisit);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        // dtrowdt = dsts.Tables[1].Copy();   
        // dsts.Tables[0].Columns.RemoveAt(8);

        DataTable dsVisit = new DataTable();
        return dtrowClr;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ProductDetail_Dr> GetProductDetail()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_Dr objProd = new ProductDetail_Dr();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //DataSet dsRate = ss.getAddionalRptSaleMaster(div_code);
        //if (dsRate != null)
        //{
        //    if (dsRate.Tables[0].Rows.Count > 0)

        //        objProd.Calc_Rate = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();

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

        List<ProductDetail_Dr> objProdDel = new List<ProductDetail_Dr>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ProductDetail_Dr objPrd = new ProductDetail_Dr();
            objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
            objPrd.Product_Detail_Name = drow["Product_Detail_Name"].ToString();
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
    public string GetProductPrice(string ProdCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_Dr objProd = new ProductDetail_Dr();

        SecSale ss = new SecSale();

        UnListedDR LstDR = new UnListedDR();
        DataSet dsState = LstDR.getState(sf_code);
        if (dsState.Tables[0].Rows.Count > 0)
            state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

        //DataSet dsRate = ss.getAddionalRptSaleMaster(div_code);
        //if (dsRate != null)
        //{
        //    if (dsRate.Tables[0].Rows.Count > 0)

        //        objProd.Calc_Rate = dsRate.Tables[0].Rows[0].ItemArray.GetValue(2).ToString().Trim();

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

        DataSet dsProd = ss.GetProductPrice(div_code, state_code, ProdCode, sub_code);

        string Rate = string.Empty;

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            if (objProd.Calc_Rate == "R")
            {
                Rate = drow["Retailor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "M")
            {
                Rate = drow["MRP_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "D")
            {
                Rate = drow["Distributor_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "N")
            {
                Rate = drow["NSR_Price"].ToString();
            }
            else if (objProd.Calc_Rate == "T")
            {
                Rate = drow["Target_Price"].ToString();
            }

        }

        return Rate;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Chemist_Dr> GetChemist(string DoctorCode)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR ch = new ListedDR();
        DataSet dsCh = ch.BingChemistDDL(DoctorCode, sf_code);

        List<Chemist_Dr> objChData = new List<Chemist_Dr>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_Dr objch = new Chemist_Dr();
            objch.Chemists_Code = dr["Chemists_Code"].ToString();
            objch.Chemists_Name = dr["chemists_name"].ToString();
            objChData.Add(objch);
        }

        return objChData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Dr> GetStockist()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        DCR dc = new DCR();
        DataSet dsSale = dc.getStockiest(sf_code, div_code);

        List<Stockist_Dr> objStockData = new List<Stockist_Dr>();

        foreach (DataRow dr in dsSale.Tables[0].Rows)
        {
            Stockist_Dr objch = new Stockist_Dr();
            objch.Stockist_Code = dr["Stockist_Code"].ToString();
            objch.Stockist_Name = dr["Stockist_Name"].ToString();
            objStockData.Add(objch);
        }

        return objStockData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void Add_Doctor_Service_Detail(DrServiceDetail objDrDet, List<string> objDrServiceDel)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Crm_Mgr = HttpContext.Current.Session["CrmMgrCode"].ToString();


        DrServiceDetail objPrd = new DrServiceDetail();
        objPrd.DoctorCode = objDrDet.DoctorCode;
        objPrd.DoctorName = objDrDet.DoctorName;
        objPrd.Address = objDrDet.Address;
        objPrd.Category = objDrDet.Category;
        objPrd.Qualification = objDrDet.Qualification;
        objPrd.Speciality = objDrDet.Speciality;
        objPrd.Class = objDrDet.Class;
        objPrd.Mobile = objDrDet.Mobile;
        objPrd.Email = objDrDet.Email;
        objPrd.ServiceAmt_tillDate = objDrDet.ServiceAmt_tillDate;
        objPrd.Business_Date = objDrDet.Business_Date;
        objPrd.TotalBusReturn_Amt = objDrDet.TotalBusReturn_Amt;
        objPrd.ROI_Dur_Month = objDrDet.ROI_Dur_Month;
        objPrd.Service_Req = objDrDet.Service_Req;
        objPrd.Service_Amt = objDrDet.Service_Amt;
        objPrd.Specific_Act = objDrDet.Specific_Act;

        objPrd.ddlChemist_1 = objDrDet.ddlChemist_1;
        objPrd.ddlChemist_2 = objDrDet.ddlChemist_2;
        objPrd.ddlChemist_3 = objDrDet.ddlChemist_3;

        objPrd.ddlStockist_1 = objDrDet.ddlStockist_1;
        objPrd.ddlStockist_2 = objDrDet.ddlStockist_2;
        objPrd.ddlStockist_3 = objDrDet.ddlStockist_3;

        objPrd.FinancialYear = objDrDet.FinancialYear;

        objPrd.TransMonth = "";
        objPrd.TransYear = "";

        objPrd.Chemists_Code = objDrDet.Chemists_Code;
        objPrd.Mode_Type = objDrDet.Mode_Type;
        objPrd.BillType = objDrDet.BillType;

        SecSale LDr = new SecSale();

        int T_Sl_No = 0;

        DataSet ds1 = LDr.Get_Trans_SrveiceDr_Head_ID(objPrd.DoctorCode, div_code, sf_code);
        if (ds1.Tables[0].Rows.Count > 0)
        {
            T_Sl_No = Convert.ToInt32(ds1.Tables[0].Rows[0]["Sl_No"].ToString());
        }
        else
        {
            T_Sl_No = 0;
        }

        DataSet dsService = LDr.Get_Trans_Doctor_Service_reqID(objPrd.DoctorCode, sf_code, div_code);
        string Service_Sl_No = "";

        if (dsService.Tables[0].Rows.Count > 0)
        {
            Service_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString() + ',';
        }
        else
        {

        }

        string Prd_Dr = objDrServiceDel[0].ToString();

        string[] Data = Prd_Dr.Split('@');

        string Cur_Prd = Data[0].ToString();
        string Potential_Prd = Data[1].ToString();
        string VisitDel = Data[2].ToString();

        string[] ArrCurPrd = Cur_Prd.Split(',');
        string[] ArrPotentialPrd = Potential_Prd.Split(',');
        string[] ArrVisitDel = VisitDel.Split(',');

        DataTable dtVisit = new DataTable();
        DataRow drVisit = dtVisit.NewRow();

        dtVisit.Columns.Add("Sf_Name");
        dtVisit.Columns.Add("Sf_Mgr_1");
        dtVisit.Columns.Add("Sf_Mgr_2");
        dtVisit.Columns.Add("Sf_Mgr_3");
        dtVisit.Columns.Add("Sf_Mgr_4");
        dtVisit.Columns.Add("Sf_Mgr_5");

        for (int i = 0; i < ArrVisitDel.Length; i++)
        {
            drVisit[i] = ArrVisitDel[i].ToString();
        }

        dtVisit.Rows.Add(drVisit);
        DrServiceDetail objDel = new DrServiceDetail();
        foreach (DataRow drV in dtVisit.Rows)
        {

            objDel.Sf_Name = drV["Sf_Name"].ToString();
            objDel.Sf_Mgr_1 = drV["Sf_Mgr_1"].ToString();
            objDel.Sf_Mgr_2 = drV["Sf_Mgr_2"].ToString();
            objDel.Sf_Mgr_3 = drV["Sf_Mgr_3"].ToString();
            objDel.Sf_Mgr_4 = drV["Sf_Mgr_4"].ToString();
            objDel.Sf_Mgr_5 = drV["Sf_Mgr_5"].ToString();
        }

        string Ser_type = "0";

        DataSet dsApprove = LDr.Get_Dr_Service_CRM_AppoveMRDet(sf_code, div_code);


        DataTable dtMrDet = new DataTable();
        DataRow drMR = dtMrDet.NewRow();

        dtMrDet.Columns.Add("First_Lev_Name");
        dtMrDet.Columns.Add("Second_Lev_Name");
        dtMrDet.Columns.Add("Third_Lev_Name");
        dtMrDet.Columns.Add("Four_Lev_Name");
        dtMrDet.Columns.Add("Fivth_Lev_Name");
        dtMrDet.Columns.Add("Six_Lev_Name");
        dtMrDet.Columns.Add("Seven_Lev_Name");

        // string[] ArrayMR = new string[] { };

        List<string> ArrayMR = new List<string>();

        if (dsApprove.Tables[0].Rows.Count > 0)
        {
            int Cnt = 0;

            foreach (DataRow dr in dsApprove.Tables[0].Rows)
            {
                //ArrayMR[Cnt] = dr[Cnt].ToString();
                // Cnt = Cnt + 1;
                // ArrayMR.Add(dr[Cnt].ToString());

                foreach (DataColumn column in dsApprove.Tables[0].Columns)
                {
                    ArrayMR.Add(dr[column].ToString());
                }
            }
        }

        for (int n = 0; n < ArrayMR.Count; n++)
        {
            drMR[n] = ArrayMR[n].ToString();
        }

        dtMrDet.Rows.Add(drMR);

        foreach (DataRow drowM in dtMrDet.Rows)
        {
            objPrd.First_Lev_Name = drowM["First_Lev_Name"].ToString();
            objPrd.Second_Lev_Name = drowM["Second_Lev_Name"].ToString();
            objPrd.Third_Lev_Name = drowM["Third_Lev_Name"].ToString();
            objPrd.Four_Lev_Name = drowM["Four_Lev_Name"].ToString();
            objPrd.Fivth_Lev_Name = drowM["Fivth_Lev_Name"].ToString();
            objPrd.Six_Lev_Name = drowM["Six_Lev_Name"].ToString();
            objPrd.Seven_Lev_Name = drowM["Seven_Lev_Name"].ToString();
        }


        DataSet dsCrm = LDr.Get_CrmMgr_Status(div_code);

        string Reporting_To = string.Empty;

        DataSet dsRpt = new DataSet();

        if (dsCrm.Tables[0].Rows.Count > 0)
        {
            string Status = dsCrm.Tables[0].Rows[0]["Crm_Mgr"].ToString();
            string Approval = dsCrm.Tables[0].Rows[0]["Crm_Approval"].ToString();

            if (Status == "A")
            {
                Reporting_To = "admin";
            }
            if (Status == "N")
            {
                dsRpt = LDr.Get_ReportingTo_Status(sf_code);
                if (dsRpt.Tables[0].Rows.Count > 0)
                {
                    Reporting_To = dsRpt.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
                }
            }
            if (Status == "M")
            {
                dsRpt = LDr.Get_ReportingTo_Status(Crm_Mgr);
                if (dsRpt.Tables[0].Rows.Count > 0)
                {
                    Reporting_To = dsRpt.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
                }
            }
            if (Status == "Y")
            {
                if (sf_code.Contains("MR"))
                {
                    dsRpt = LDr.Get_ReportingTo_Status(sf_code);
                    if (dsRpt.Tables[0].Rows.Count > 0)
                    {
                        Reporting_To = dsRpt.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
                    }
                }
                else if (Crm_Mgr.Contains("MGR"))
                {
                    dsRpt = LDr.Get_ReportingTo_Status(Crm_Mgr);
                    if (dsRpt.Tables[0].Rows.Count > 0)
                    {
                        Reporting_To = dsRpt.Tables[0].Rows[0]["Reporting_To_SF"].ToString();
                    }
                }
            }
        }

        if (dsApprove.Tables[0].Rows.Count > 0)
        {

        }

        int iReturn = LDr.Add_Trans_Service_DrHead(objPrd.DoctorCode, sf_code, objPrd.FinancialYear, objPrd.TransMonth, objPrd.TransYear,
            objPrd.ServiceAmt_tillDate, objPrd.Business_Date, objPrd.TotalBusReturn_Amt, objPrd.ROI_Dur_Month, objPrd.Service_Req,
            objPrd.Service_Amt, objPrd.Specific_Act, objPrd.ddlChemist_1, objPrd.ddlChemist_2, objPrd.ddlChemist_3,
            objPrd.ddlStockist_1, objPrd.ddlStockist_2, objPrd.ddlStockist_3, div_code, Convert.ToString(T_Sl_No), objDel.Sf_Name,
            objDel.Sf_Mgr_1, objDel.Sf_Mgr_2, objDel.Sf_Mgr_3, objDel.Sf_Mgr_4, objDel.Sf_Mgr_5, Convert.ToString(Service_Sl_No), Ser_type,
            objPrd.First_Lev_Name, objPrd.Second_Lev_Name, objPrd.Third_Lev_Name, objPrd.Four_Lev_Name, objPrd.Fivth_Lev_Name,
            objPrd.Six_Lev_Name, objPrd.Seven_Lev_Name, objPrd.Chemists_Code, objPrd.Mode_Type, Crm_Mgr, Reporting_To, objPrd.BillType);

        //  int iReturn = 0;

        if (iReturn != 0)
        {

            DataSet ds = LDr.Get_Trans_SrveiceDr_Head_ID(objPrd.DoctorCode, div_code, sf_code);
            if (ds.Tables[0].Rows.Count > 0)
            {
                int T_Sl_No_1 = Convert.ToInt32(ds.Tables[0].Rows[0]["Sl_No"].ToString());

                DataSet dsPrd = LDr.Get_Trans_SrveiceDr_ProductID(div_code, Convert.ToString(T_Sl_No_1));

                if (dsPrd.Tables[0].Rows.Count > 0)
                {
                    int PrdSlNo = Convert.ToInt32(dsPrd.Tables[0].Rows[0]["Prd_Sl_No"].ToString());
                }
                else
                {
                    int PrdSlNo = 0;
                }

                ArrayList Arr = new ArrayList();
                DataTable dt = new DataTable();
                DataRow dr = dt.NewRow();

                dt.Columns.Add("T_Sl_No");
                dr["T_Sl_No"] = T_Sl_No_1;

                for (int i = 0; i < ArrCurPrd.Length - 1; i++)
                {
                    string CurPrd = ArrCurPrd[i].ToString();
                    string[] Cur_Prd_Dr = CurPrd.Split('^');

                    int idVal = i + 1;

                    dt.Columns.Add("Cur_Prod_Code_" + idVal);
                    dt.Columns.Add("Cur_Prod_Name_" + idVal);
                    dt.Columns.Add("Cur_Prod_Price_" + idVal);
                    dt.Columns.Add("Cur_Prod_Qty_" + idVal);
                    dt.Columns.Add("Cur_Prod_Value_" + idVal);
                    dt.Columns.Add("Cur_Crm_Prd_" + idVal);
                    //dt.Columns.Add("CRM_Prd_Tot" + idVal);

                    dr["Cur_Prod_Code_" + idVal] = Cur_Prd_Dr[0];
                    dr["Cur_Prod_Name_" + idVal] = Cur_Prd_Dr[1];
                    dr["Cur_Prod_Price_" + idVal] = Cur_Prd_Dr[2];
                    dr["Cur_Prod_Qty_" + idVal] = Cur_Prd_Dr[3];
                    dr["Cur_Prod_Value_" + idVal] = Cur_Prd_Dr[4];
                    dr["Cur_Crm_Prd_" + idVal] = Cur_Prd_Dr[5];
                    // dr["CRM_Prd_Tot" + idVal] = Cur_Prd_Dr[6];
                }

                string Tot = ArrCurPrd[6].ToString();

                string[] TotData = Tot.Split('^');

                dt.Columns.Add("Cur_total");
                dr["Cur_total"] = TotData[0];

                dt.Columns.Add("Cur_Crm_Tot");
                dr["Cur_Crm_Tot"] = TotData[1];

                for (int i = 0; i < ArrPotentialPrd.Length - 1; i++)
                {
                    string Pot_Prd = ArrPotentialPrd[i].ToString();
                    string[] Pot_Prd_Dr = Pot_Prd.Split('^');

                    int idVal = i + 1;

                    dt.Columns.Add("Potl_Prod_Code_" + idVal);
                    dt.Columns.Add("Potl_Prod_Name_" + idVal);
                    dt.Columns.Add("Potl_Prod_Price_" + idVal);
                    dt.Columns.Add("Potl_Prod_Qty_" + idVal);
                    dt.Columns.Add("Potl_Prod_Value_" + idVal);

                    dr["Potl_Prod_Code_" + idVal] = Pot_Prd_Dr[0];
                    dr["Potl_Prod_Name_" + idVal] = Pot_Prd_Dr[1];
                    dr["Potl_Prod_Price_" + idVal] = Pot_Prd_Dr[2];
                    dr["Potl_Prod_Qty_" + idVal] = Pot_Prd_Dr[3];
                    dr["Potl_Prod_Value_" + idVal] = Pot_Prd_Dr[4];

                }

                string Tot1 = ArrPotentialPrd[6].ToString();
                dt.Columns.Add("Poten_total");
                dr["Poten_total"] = Tot1;

                dt.Columns.Add("Division_Code");
                dr["Division_Code"] = div_code;

                dt.Rows.Add(dr);

                foreach (DataRow r in dt.Rows)
                {
                    string Sl_No = r["T_Sl_No"].ToString();
                    string Cur_Prod_Code_1 = r["Cur_Prod_Code_1"].ToString();
                    string Cur_Prod_Price_1 = r["Cur_Prod_Price_1"].ToString();
                    string Cur_Prod_Qty_1 = r["Cur_Prod_Qty_1"].ToString();
                    string Cur_Prod_Value_1 = r["Cur_Prod_Value_1"].ToString();
                    string Cur_Prod_Code_2 = r["Cur_Prod_Code_2"].ToString();
                    string Cur_Prod_Price_2 = r["Cur_Prod_Price_2"].ToString();
                    string Cur_Prod_Qty_2 = r["Cur_Prod_Qty_2"].ToString();
                    string Cur_Prod_Value_2 = r["Cur_Prod_Value_2"].ToString();
                    string Cur_Prod_Code_3 = r["Cur_Prod_Code_3"].ToString();
                    string Cur_Prod_Price_3 = r["Cur_Prod_Price_3"].ToString();
                    string Cur_Prod_Qty_3 = r["Cur_Prod_Qty_3"].ToString();
                    string Cur_Prod_Value_3 = r["Cur_Prod_Value_3"].ToString();
                    string Cur_Prod_Code_4 = r["Cur_Prod_Code_4"].ToString();
                    string Cur_Prod_Price_4 = r["Cur_Prod_Price_4"].ToString();
                    string Cur_Prod_Qty_4 = r["Cur_Prod_Qty_4"].ToString();
                    string Cur_Prod_Value_4 = r["Cur_Prod_Value_4"].ToString();
                    string Cur_Prod_Code_5 = r["Cur_Prod_Code_5"].ToString();
                    string Cur_Prod_Price_5 = r["Cur_Prod_Price_5"].ToString();
                    string Cur_Prod_Qty_5 = r["Cur_Prod_Qty_5"].ToString();
                    string Cur_Prod_Value_5 = r["Cur_Prod_Value_5"].ToString();
                    string Cur_Prod_Code_6 = r["Cur_Prod_Code_6"].ToString();
                    string Cur_Prod_Price_6 = r["Cur_Prod_Price_6"].ToString();
                    string Cur_Prod_Qty_6 = r["Cur_Prod_Qty_6"].ToString();
                    string Cur_Prod_Value_6 = r["Cur_Prod_Value_6"].ToString();
                    string Cur_Total = r["Cur_Total"].ToString();
                    string Potl_Prod_Code_1 = r["Potl_Prod_Code_1"].ToString();
                    string Potl_Prod_Price_1 = r["Potl_Prod_Price_1"].ToString();
                    string Potl_Prod_Qty_1 = r["Potl_Prod_Qty_1"].ToString();
                    string Potl_Prod_Value_1 = r["Potl_Prod_Value_1"].ToString();
                    string Potl_Prod_Code_2 = r["Potl_Prod_Code_2"].ToString();
                    string Potl_Prod_Price_2 = r["Potl_Prod_Price_2"].ToString();
                    string Potl_Prod_Qty_2 = r["Potl_Prod_Qty_2"].ToString();
                    string Potl_Prod_Value_2 = r["Potl_Prod_Value_2"].ToString();
                    string Potl_Prod_Code_3 = r["Potl_Prod_Code_3"].ToString();
                    string Potl_Prod_Price_3 = r["Potl_Prod_Price_3"].ToString();
                    string Potl_Prod_Qty_3 = r["Potl_Prod_Qty_3"].ToString();
                    string Potl_Prod_Value_3 = r["Potl_Prod_Value_3"].ToString();
                    string Potl_Prod_Code_4 = r["Potl_Prod_Code_4"].ToString();
                    string Potl_Prod_Price_4 = r["Potl_Prod_Price_4"].ToString();
                    string Potl_Prod_Qty_4 = r["Potl_Prod_Qty_4"].ToString();
                    string Potl_Prod_Value_4 = r["Potl_Prod_Value_4"].ToString();
                    string Potl_Prod_Code_5 = r["Potl_Prod_Code_5"].ToString();
                    string Potl_Prod_Price_5 = r["Potl_Prod_Price_5"].ToString();
                    string Potl_Prod_Qty_5 = r["Potl_Prod_Qty_5"].ToString();
                    string Potl_Prod_Value_5 = r["Potl_Prod_Value_5"].ToString();
                    string Potl_Prod_Code_6 = r["Potl_Prod_Code_6"].ToString();
                    string Potl_Prod_Price_6 = r["Potl_Prod_Price_6"].ToString();
                    string Potl_Prod_Qty_6 = r["Potl_Prod_Qty_6"].ToString();
                    string Potl_Prod_Value_6 = r["Potl_Prod_Value_6"].ToString();
                    string Poten_total = r["Poten_total"].ToString();
                    string Division_Code = r["Division_Code"].ToString();

                    string Cur_Crm_Prd_1 = r["Cur_Crm_Prd_1"].ToString();
                    string Cur_Crm_Prd_2 = r["Cur_Crm_Prd_2"].ToString();
                    string Cur_Crm_Prd_3 = r["Cur_Crm_Prd_3"].ToString();
                    string Cur_Crm_Prd_4 = r["Cur_Crm_Prd_4"].ToString();
                    string Cur_Crm_Prd_5 = r["Cur_Crm_Prd_5"].ToString();
                    string Cur_Crm_Prd_6 = r["Cur_Crm_Prd_6"].ToString();
                    string Cur_Crm_Tot = r["Cur_Crm_Tot"].ToString();

                    int iReturn1 = LDr.Add_Trans_Service_Dr_Product(Sl_No, Cur_Prod_Code_1, Cur_Prod_Price_1, Cur_Prod_Qty_1, Cur_Prod_Value_1,
                        Cur_Prod_Code_2, Cur_Prod_Price_2, Cur_Prod_Qty_2, Cur_Prod_Value_2, Cur_Prod_Code_3, Cur_Prod_Price_3, Cur_Prod_Qty_3, Cur_Prod_Value_3,
                        Cur_Prod_Code_4, Cur_Prod_Price_4, Cur_Prod_Qty_4, Cur_Prod_Value_4, Cur_Prod_Code_5, Cur_Prod_Price_5, Cur_Prod_Qty_5, Cur_Prod_Value_5,
                        Cur_Prod_Code_6, Cur_Prod_Price_6, Cur_Prod_Qty_6, Cur_Prod_Value_6, Cur_Total, Potl_Prod_Code_1, Potl_Prod_Price_1, Potl_Prod_Qty_1, Potl_Prod_Value_1,
                        Potl_Prod_Code_2, Potl_Prod_Price_2, Potl_Prod_Qty_2, Potl_Prod_Value_2, Potl_Prod_Code_3, Potl_Prod_Price_3, Potl_Prod_Qty_3, Potl_Prod_Value_3,
                        Potl_Prod_Code_4, Potl_Prod_Price_4, Potl_Prod_Qty_4, Potl_Prod_Value_4, Potl_Prod_Code_5, Potl_Prod_Price_5, Potl_Prod_Qty_5, Potl_Prod_Value_5,
                        Potl_Prod_Code_6, Potl_Prod_Price_6, Potl_Prod_Qty_6, Potl_Prod_Value_6, Poten_total, Division_Code,
                        Cur_Crm_Prd_1, Cur_Crm_Prd_2, Cur_Crm_Prd_3, Cur_Crm_Prd_4, Cur_Crm_Prd_5, Cur_Crm_Prd_6, Cur_Crm_Tot);

                }

            }

        }

        HttpContext.Current.Session["sf_code"] = Crm_Mgr;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Add_Dr_Service_Req(List<string> objServiceReq)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string F_Code = HttpContext.Current.Session["F_Code"].ToString();

        sf_code = F_Code;

        string Service_Dr = objServiceReq[0].ToString();

        string[] Data = Service_Dr.Split('@');

        string SericeDet = Data[0].ToString();
        string DoctorCode = Data[1].ToString();
        string Ser_Type = Data[2].ToString();

        string Chemist_Cd = Data[3].ToString();
        string ModeType = Data[4].ToString();

        SecSale ss = new SecSale();

        ServiceReq objService = new ServiceReq();

        int iReturn = 0;

        if (Ser_Type == "1")
        {
            string[] ArrSerDet = SericeDet.Split(',');

            for (int i = 0; i < ArrSerDet.Length; i++)
            {
                int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

                string[] ArrSerVal = ArrSerDet[i].Split('^');
                objService.Doctor_Code = DoctorCode;
                objService.No_Of_Person = ArrSerVal[0];
                objService.From_Date = ArrSerVal[1];
                objService.To_Date = ArrSerVal[2];
                objService.Arraival_Time = ArrSerVal[3];
                objService.Departure_Time = ArrSerVal[4];
                objService.Remarks = ArrSerVal[5];
                objService.Location = ArrSerVal[6];
                objService.Hotel_Type = ArrSerVal[7];

                string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                    Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                    objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                    objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                    objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                    objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                    objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                    objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                    Chemist_Cd, ModeType);
            }

        }
        else if (Ser_Type == "2")
        {

            string[] ArrSerDet = SericeDet.Split('#');

            string[] ArrTravel = ArrSerDet[0].Split(',');

            for (int i = 0; i < ArrTravel.Length; i++)
            {
                int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

                string[] TravelDet = ArrTravel[i].Split('^');

                objService.Type_of_Travel = ArrSerDet[2];
                objService.Doctor_Code = DoctorCode;

                objService.Date = TravelDet[0];
                objService.From_Travel = TravelDet[1];
                objService.To_Travel = TravelDet[2];
                objService.No_of_Hrs_Km = TravelDet[3];

                string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                    Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                    objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                    objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                    objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                    objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                    objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                    objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                    Chemist_Cd, ModeType);
            }


            string[] ArrMem = ArrSerDet[1].Split(',');

            for (int i = 0; i < ArrMem.Length; i++)
            {
                int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

                string[] MembersDet = ArrMem[i].Split('^');

                objService.Type_of_Travel = ArrSerDet[2];
                objService.Doctor_Code = DoctorCode;

                objService.Name = MembersDet[0];
                objService.Age = MembersDet[1];
                objService.Sex = MembersDet[2];
                objService.Id_Proof = MembersDet[3];

                objService.Type_of_Travel = "";
                objService.Date = "";
                objService.From_Travel = "";
                objService.To_Travel = "";
                objService.No_of_Hrs_Km = "";

                string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                    Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                    objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                    objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                    objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                    objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                    objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                    objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                    Chemist_Cd, ModeType);
            }


            //string[] ArrSerDet = SericeDet.Split('#');
            //string[] TravelDet = ArrSerDet[0].Split('^');
            //string[] MembersDet = ArrSerDet[1].Split('^');

            //objService.Type_of_Travel = ArrSerDet[2];
            //objService.Doctor_Code = DoctorCode;    

            //objService.Date = TravelDet[0];
            //objService.From_Travel = TravelDet[1];
            //objService.To_Travel = TravelDet[2];
            //objService.No_of_Hrs_Km = TravelDet[3];

            //objService.Name = MembersDet[0];
            //objService.Age = MembersDet[1];
            //objService.Sex = MembersDet[2];
            //objService.Id_Proof = MembersDet[3];


            //string Dr_SerSlNo = "";
            //string Trans_Sl_No = "";

            //iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
            //    Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
            //    objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
            //    objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
            //    objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
            //    objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
            //    objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
            //    objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
            //    Chemist_Cd, ModeType); 

        }
        else if (Ser_Type == "3")
        {
            int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Name_of_Books = ArrSerDet[0];
            objService.Author = ArrSerDet[1];
            objService.Edition = ArrSerDet[2];
            objService.Approx_Cost = ArrSerDet[3];

            string Dr_SerSlNo = "";
            string Trans_Sl_No = "";

            iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                Chemist_Cd, ModeType);
        }
        else if (Ser_Type == "4")
        {
            int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Name_of_Conference = ArrSerDet[0];
            objService.Date = ArrSerDet[1];
            objService.Type_Of_Participation = ArrSerDet[2];
            objService.Cost = ArrSerDet[3];
            objService.Cheque_Draft = ArrSerDet[4];
            objService.Payable_At = ArrSerDet[5];

            string Dr_SerSlNo = "";
            string Trans_Sl_No = "";

            iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                Chemist_Cd, ModeType);

        }
        else if (Ser_Type == "5")
        {
            int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Item_Descrp = ArrSerDet[0];
            objService.Rate = ArrSerDet[1];
            objService.Qty = ArrSerDet[2];
            objService.Cheque_Draft = ArrSerDet[3];
            objService.Payable_At = ArrSerDet[4];

            string Dr_SerSlNo = "";
            string Trans_Sl_No = "";

            iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
                Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
                objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
                objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
                objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
                objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
                objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
                objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
                Chemist_Cd, ModeType);
        }

        else if (Ser_Type == "6")
        {
            int Ser_SlNo = ss.Get_Dr_Service_Req_ID();

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Optiontype = ArrSerDet[0];
            objService.OtherRemark = ArrSerDet[1];

            string Dr_SerSlNo = "";
            string Trans_Sl_No = "";

            iReturn = ss.Doctor_Service_Req_Test(DoctorCode, sf_code, div_code,
               Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
               objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
               objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
               objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
               objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
               objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
               objService.Qty, Convert.ToString(Ser_SlNo), Trans_Sl_No, Dr_SerSlNo, objService.Optiontype, objService.OtherRemark,
               Chemist_Cd, ModeType);
        }

        return iReturn;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Chemist_Det_Dr> BindChemist_Detail(string Chemist_Code)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string F_Code = HttpContext.Current.Session["F_Code"].ToString();

        SecSale SS = new SecSale();
        DataSet dsCh = SS.Bind_Chemist_Detail(F_Code, div_code, Chemist_Code);

        List<Chemist_Det_Dr> objChData = new List<Chemist_Det_Dr>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_Det_Dr objch = new Chemist_Det_Dr();
            objch.Chemists_Code = dr["Chemists_Code"].ToString();
            objch.Chemists_Name = dr["Chemists_Name"].ToString();
            objch.Chemists_Contact = dr["Chemists_Contact"].ToString();
            objch.Chemists_Address1 = dr["Chemists_Address1"].ToString();
            objch.Chemists_Phone = dr["Chemists_Phone"].ToString();
            objch.territory_Name = dr["territory_Name"].ToString();
            objChData.Add(objch);
        }

        return objChData;
    }

    #endregion
}

#region DrParam

public class ListedDoctor
{
    public string DoctorCode { get; set; }
    public string DoctorName { get; set; }
    public string DoctorAddress { get; set; }
    public string Category { get; set; }
    public string Speciality { get; set; }
    public string Qualification { get; set; }
    public string Class { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }

    public string ServiceAmt { get; set; }
    public string BusinessAmt { get; set; }
}
public class ProductDetail_Dr
{
    public string Calc_Rate { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Rate { get; set; }
}
public class Chemist_Dr
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
}
public class Stockist_Dr
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}
public class DrServiceDetail
{
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

    public string Sf_Name { get; set; }
    public string Sf_Mgr_1 { get; set; }
    public string Sf_Mgr_2 { get; set; }
    public string Sf_Mgr_3 { get; set; }
    public string Sf_Mgr_4 { get; set; }
    public string Sf_Mgr_5 { get; set; }

    public string Dr_SlNo { get; set; }
    public string First_Lev_Name { get; set; }
    public string Second_Lev_Name { get; set; }
    public string Third_Lev_Name { get; set; }
    public string Four_Lev_Name { get; set; }
    public string Fivth_Lev_Name { get; set; }
    public string Six_Lev_Name { get; set; }
    public string Seven_Lev_Name { get; set; }

    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
    public string Chemists_Contact { get; set; }
    public string Chemists_Address1 { get; set; }
    public string Chemists_Phone { get; set; }
    public string territory_Name { get; set; }

    public string Mode_Type { get; set; }
    public string BillType { get; set; }
}
public class Dr_BusinessAmt
{
    public string ServiceAmt { get; set; }
    public string BusinessAmt { get; set; }
}
public class ServiceReq
{
    public string SerReq_No { get; set; }
    public string Doctor_Code { get; set; }
    public string Sf_Code { get; set; }
    public string Division_Code { get; set; }
    public string Type { get; set; }
    public string Location { get; set; }
    public string Hotel_Type { get; set; }
    public string No_Of_Person { get; set; }
    public string From_Date { get; set; }
    public string To_Date { get; set; }
    public string Arraival_Time { get; set; }
    public string Departure_Time { get; set; }
    public string Remarks { get; set; }
    public string Type_of_Travel { get; set; }
    public string Date { get; set; }
    public string From_Travel { get; set; }
    public string To_Travel { get; set; }
    public string No_of_Hrs_Km { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Sex { get; set; }
    public string Id_Proof { get; set; }
    public string Name_of_Books { get; set; }
    public string Author { get; set; }
    public string Edition { get; set; }
    public string Approx_Cost { get; set; }
    public string Name_of_Conference { get; set; }
    public string Type_Of_Participation { get; set; }
    public string Cost { get; set; }
    public string Cheque_Draft { get; set; }
    public string Payable_At { get; set; }
    public string Item_Descrp { get; set; }
    public string Rate { get; set; }
    public string Qty { get; set; }

    public string Optiontype { get; set; }
    public string OtherRemark { get; set; }

}
public class Chemist_Det_Dr
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
    public string Chemists_Contact { get; set; }
    public string Chemists_Address1 { get; set; }
    public string Chemists_Phone { get; set; }
    public string territory_Name { get; set; }
}

#endregion