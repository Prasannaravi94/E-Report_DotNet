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
/// Summary description for DrService_Edit_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class DrService_Edit_WebService : System.Web.Services.WebService
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

    #region Dr_Edit

    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsts = new DataSet();

    string S_Code = string.Empty;
    string Mode = string.Empty;

    public DrService_Edit_WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ListedDoctor_Detail> GetListedDoctorDetail(string objDrCode)
    {
        SecSale lst = new SecSale();

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();


        DataSet dsListedDR = lst.Get_Service_CRM_Dr_Detail(div_code, objDrCode, sf_code);

        List<ListedDoctor_Detail> objDrDetail = new List<ListedDoctor_Detail>();

        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsListedDR.Tables[0].Rows)
            {
                ListedDoctor_Detail objLstDr = new ListedDoctor_Detail();
                objLstDr.DoctorCode = dr["ListedDrCode"].ToString();
                objLstDr.DoctorName = dr["ListedDr_Name"].ToString();
                objLstDr.DoctorAddress = dr["ListedDr_Address1"].ToString();
                objLstDr.Category = dr["Doc_Cat_Name"].ToString();
                objLstDr.Speciality = dr["Doc_Special_Name"].ToString();
                objLstDr.Qualification = dr["Doc_QuaName"].ToString();
                objLstDr.Class = dr["Doc_ClsName"].ToString();
                objLstDr.Mobile = dr["ListedDr_Mobile"].ToString();
                objLstDr.Email = dr["ListedDr_Email"].ToString();
                objDrDetail.Add(objLstDr);

            }

        }

        return objDrDetail;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ServiceDetail> BindServiceDDL(string objDrCode)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string[] Data = objDrCode.Split('^');

        SecSale ss = new SecSale();
        DataSet dsProd = ss.Get_Doctor_ServiceDate(div_code, Data[0], Data[1]);

        List<ServiceDetail> objProdDel = new List<ServiceDetail>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ServiceDetail objPrd = new ServiceDetail();
            objPrd.ServiceID = drow["Sl_No"].ToString();
            objPrd.ServiceName = drow["ServiceName"].ToString();

            objProdDel.Add(objPrd);
        }

        return objProdDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void Update_Service_Detail(ListedDoctor_Detail objDrServiceDel)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDoctor_Detail objData = new ListedDoctor_Detail();
        objData.DoctorCode = objDrServiceDel.DoctorCode;
        objData.DoctorName = objDrServiceDel.DoctorName;
        objData.DoctorAddress = objDrServiceDel.DoctorAddress;
        objData.Category = objDrServiceDel.Category;
        objData.Speciality = objDrServiceDel.Speciality;
        objData.Qualification = objDrServiceDel.Qualification;
        objData.Class = objDrServiceDel.Class;
        objData.Mobile = objDrServiceDel.Mobile;
        objData.Email = objDrServiceDel.Email;
        objData.ServiceStateMent = objDrServiceDel.ServiceStateMent;
        objData.CreatedDate = objDrServiceDel.CreatedDate;
        objData.Sl_No = objDrServiceDel.Sl_No;

        SecSale ss = new SecSale();

        int Res = ss.Doctor_Service_Update(objData.Sl_No, objData.DoctorCode, sf_code, div_code, objData.ServiceStateMent);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DrServiceDetail_1> BindDoctorService(DrServiceDetail_1 objDrDetail)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Sf_Type = HttpContext.Current.Session["sf_type"].ToString();

        SecSale ss = new SecSale();
        List<DrServiceDetail_1> objDrService = new List<DrServiceDetail_1>();

        DrServiceDetail_1 objData = new DrServiceDetail_1();
        objData.Sl_No = objDrDetail.Sl_No;

        DataSet dsDr;

        if (objDrDetail.ModeType == "Doctor")
        {
            objData.DoctorCode = objDrDetail.DoctorCode;
            dsDr = ss.Get_DoctorService_CRMDetail(objData.Sl_No, objData.DoctorCode, div_code);
        }
        else
        {
            objData.Chemists_Code = objDrDetail.Chemists_Code;
            dsDr = ss.GetChemist_DetailService(objData.Sl_No, objData.Chemists_Code, div_code, sf_code);
        }


        foreach (DataRow dr in dsDr.Tables[0].Rows)
        {
            DrServiceDetail_1 objDr = new DrServiceDetail_1();
            objDr.Sl_No = dr["Sl_No"].ToString();

            if (objDrDetail.ModeType == "Doctor")
            {
                objDr.DoctorCode = dr["Doctor_Code"].ToString();
                objDr.DoctorName = dr["ListedDr_Name"].ToString();
            }
            else
            {
                objDr.Chemists_Code = dr["Chemists_Code"].ToString();
                objDr.Chemists_Name = dr["Chemists_Name"].ToString();
            }

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
            objDr.BillType = dr["Sale_Type"].ToString();

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
            objDr.Service_Sl_No = dr["Service_Sl_No"].ToString();

            objDr.Cur_Crm_Prd_1 = dr["Cur_Crm_Prd_1"].ToString();
            objDr.Cur_Crm_Prd_2 = dr["Cur_Crm_Prd_2"].ToString();
            objDr.Cur_Crm_Prd_3 = dr["Cur_Crm_Prd_3"].ToString();
            objDr.Cur_Crm_Prd_4 = dr["Cur_Crm_Prd_4"].ToString();
            objDr.Cur_Crm_Prd_5 = dr["Cur_Crm_Prd_5"].ToString();
            objDr.Cur_Crm_Prd_6 = dr["Cur_Crm_Prd_6"].ToString();
            objDr.Cur_Crm_Tot = dr["Cur_Crm_Tot"].ToString();

            objDr.Sf_Type = Sf_Type;

            objDr.Status = dr["Ser_Type"].ToString();

            objDrService.Add(objDr);

        }

        return objDrService;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void Add_Doctor_Service_Detail(DrServiceDetail_1 objDrDet, List<string> objDrServiceDel)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();
        string Crm_Mgr = HttpContext.Current.Session["CrmMgrCode"].ToString();

        string SF_ID = HttpContext.Current.Session["SF_ID"].ToString();

        DrServiceDetail_1 objPrd = new DrServiceDetail_1();

        objPrd.Sl_No = objDrDet.Sl_No;
        objPrd.Prd_Sl_No = objDrDet.Prd_Sl_No;

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

        objPrd.Service_Statement = objDrDet.Service_Statement;

        objPrd.ButtonVal = objDrDet.ButtonVal;

        objPrd.TransMonth = "";
        objPrd.TransYear = "";

        objPrd.Chemists_Code = objDrDet.Chemists_Code;
        objPrd.Mode_Type = objDrDet.Mode_Type;
        objPrd.BillType = objDrDet.BillType;

        objPrd.Rej_Reason = objDrDet.Rej_Reason;
        objPrd.Rej_SfCode = Crm_Mgr;

        SecSale LDr = new SecSale();

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
        DrServiceDetail_1 objDel = new DrServiceDetail_1();
        foreach (DataRow drV in dtVisit.Rows)
        {

            objDel.Sf_Name = drV["Sf_Name"].ToString();
            objDel.Sf_Mgr_1 = drV["Sf_Mgr_1"].ToString();
            objDel.Sf_Mgr_2 = drV["Sf_Mgr_2"].ToString();
            objDel.Sf_Mgr_3 = drV["Sf_Mgr_3"].ToString();
            objDel.Sf_Mgr_4 = drV["Sf_Mgr_4"].ToString();
            objDel.Sf_Mgr_5 = drV["Sf_Mgr_5"].ToString();
        }

        string Approve = string.Empty;

        if (objPrd.ButtonVal == "Approve")
        {
            Approve = "1";
        }
        else if (objPrd.ButtonVal == "Update")
        {
            Approve = "0";
        }
        else if (objPrd.ButtonVal == "Confirm to Process")
        {
            Approve = "2";
        }
        else if (objPrd.ButtonVal == "Reject")
        {
            Approve = "3";
        }

        int iReturn = LDr.Update_Trans_Service_DrHead_Test(objPrd.DoctorCode, sf_code, objPrd.FinancialYear, objPrd.TransMonth, objPrd.TransYear,
            objPrd.ServiceAmt_tillDate, objPrd.Business_Date, objPrd.TotalBusReturn_Amt, objPrd.ROI_Dur_Month, objPrd.Service_Req,
            objPrd.Service_Amt, objPrd.Specific_Act, objPrd.ddlChemist_1, objPrd.ddlChemist_2, objPrd.ddlChemist_3,
            objPrd.ddlStockist_1, objPrd.ddlStockist_2, objPrd.ddlStockist_3, div_code, objPrd.Sl_No, objDel.Sf_Name,
            objDel.Sf_Mgr_1, objDel.Sf_Mgr_2, objDel.Sf_Mgr_3, objDel.Sf_Mgr_4, objDel.Sf_Mgr_5, objPrd.Service_Statement,
            Approve, SF_ID, objPrd.Chemists_Code, objPrd.Mode_Type, objPrd.BillType, objPrd.Rej_Reason, objPrd.Rej_SfCode);

        //  int iReturn = 0;

        if (iReturn != 0)
        {
            ArrayList Arr = new ArrayList();
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();

            dt.Columns.Add("T_Sl_No");
            dr["T_Sl_No"] = objPrd.Sl_No;

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

                dr["Cur_Prod_Code_" + idVal] = Cur_Prd_Dr[0];
                dr["Cur_Prod_Name_" + idVal] = Cur_Prd_Dr[1];
                dr["Cur_Prod_Price_" + idVal] = Cur_Prd_Dr[2];
                dr["Cur_Prod_Qty_" + idVal] = Cur_Prd_Dr[3];
                dr["Cur_Prod_Value_" + idVal] = Cur_Prd_Dr[4];
                dr["Cur_Crm_Prd_" + idVal] = Cur_Prd_Dr[5];

            }

            //string Tot = ArrCurPrd[6].ToString();
            //dt.Columns.Add("Cur_total");
            //dr["Cur_total"] = Tot;

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

                int iReturn1 = LDr.Update_Trans_Service_Dr_Product(objPrd.Prd_Sl_No, objPrd.Sl_No, Cur_Prod_Code_1, Cur_Prod_Price_1, Cur_Prod_Qty_1, Cur_Prod_Value_1,
                    Cur_Prod_Code_2, Cur_Prod_Price_2, Cur_Prod_Qty_2, Cur_Prod_Value_2, Cur_Prod_Code_3, Cur_Prod_Price_3, Cur_Prod_Qty_3, Cur_Prod_Value_3,
                    Cur_Prod_Code_4, Cur_Prod_Price_4, Cur_Prod_Qty_4, Cur_Prod_Value_4, Cur_Prod_Code_5, Cur_Prod_Price_5, Cur_Prod_Qty_5, Cur_Prod_Value_5,
                    Cur_Prod_Code_6, Cur_Prod_Price_6, Cur_Prod_Qty_6, Cur_Prod_Value_6, Cur_Total, Potl_Prod_Code_1, Potl_Prod_Price_1, Potl_Prod_Qty_1, Potl_Prod_Value_1,
                    Potl_Prod_Code_2, Potl_Prod_Price_2, Potl_Prod_Qty_2, Potl_Prod_Value_2, Potl_Prod_Code_3, Potl_Prod_Price_3, Potl_Prod_Qty_3, Potl_Prod_Value_3,
                    Potl_Prod_Code_4, Potl_Prod_Price_4, Potl_Prod_Qty_4, Potl_Prod_Value_4, Potl_Prod_Code_5, Potl_Prod_Price_5, Potl_Prod_Qty_5, Potl_Prod_Value_5,
                    Potl_Prod_Code_6, Potl_Prod_Price_6, Potl_Prod_Qty_6, Potl_Prod_Value_6, Poten_total, Division_Code,
                    Cur_Crm_Prd_1, Cur_Crm_Prd_2, Cur_Crm_Prd_3, Cur_Crm_Prd_4, Cur_Crm_Prd_5, Cur_Crm_Prd_6, Cur_Crm_Tot);

            }

        }

        HttpContext.Current.Session["sf_code"] = Crm_Mgr;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DrServiceDetail_1> Get_Trans_Dr_ServiceDet()
    {
        List<DrServiceDetail_1> obj = new List<DrServiceDetail_1>();

        DataSet dsDrHead = new DataSet();

        foreach (DataRow drow in dsDrHead.Tables[0].Rows)
        {
            DrServiceDetail_1 objData = new DrServiceDetail_1();
            objData.DoctorCode = drow["DoctorCode"].ToString();
            objData.DoctorName = drow["DoctorName"].ToString();
        }

        return obj;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<ProductDetail_1> GetProductDetail()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string F_Code = HttpContext.Current.Session["F_Code"].ToString();
        sf_code = F_Code;

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_1 objProd = new ProductDetail_1();

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

        List<ProductDetail_1> objProdDel = new List<ProductDetail_1>();

        foreach (DataRow drow in dsProd.Tables[0].Rows)
        {
            ProductDetail_1 objPrd = new ProductDetail_1();
            objPrd.Product_Detail_Code = drow["Product_Detail_Code"].ToString();
            objPrd.Product_Detail_Name = drow["Product_Detail_Name"].ToString();


            //DataSet dsSetup_Price = LstDR.getSetup_Price(div_code);
            //objProd.Calc_Rate = dsSetup_Price.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            // objProd.Calc_Rate = "D";
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

        string F_Code = HttpContext.Current.Session["F_Code"].ToString();
        sf_code = F_Code;

        string state_code = "";
        string Prod_Grp = "";
        string sub_code = "";
        string Prd_grp = "";

        ProductDetail_1 objProd = new ProductDetail_1();

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
        //        Prod_Grp = dsRate.Tables[0].Rows[0].ItemArray.GetValue(3).ToString().Trim();

        //}

        DataSet dsSetup_Price = LstDR.getSetup_Price(div_code);
        objProd.Calc_Rate = dsSetup_Price.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //objProd.Calc_Rate = "D";
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
    public List<Chemist_1> GetChemist(string DoctorCode)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        ListedDR ch = new ListedDR();
        DataSet dsCh = ch.BingChemistDDL(DoctorCode, sf_code);

        List<Chemist_1> objChData = new List<Chemist_1>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_1 objch = new Chemist_1();
            objch.Chemists_Code = dr["Chemists_Code"].ToString();
            objch.Chemists_Name = dr["chemists_name"].ToString();
            objChData.Add(objch);
        }

        return objChData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_1> GetStockist()
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        DCR dc = new DCR();
        DataSet dsSale = dc.getStockiest(sf_code, div_code);

        List<Stockist_1> objStockData = new List<Stockist_1>();

        foreach (DataRow dr in dsSale.Tables[0].Rows)
        {
            Stockist_1 objch = new Stockist_1();
            objch.Stockist_Code = dr["Stockist_Code"].ToString();
            objch.Stockist_Name = dr["Stockist_Name"].ToString();
            objStockData.Add(objch);
        }

        return objStockData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Year_Detail> FillYear()
    {
        List<Year_Detail> objYearDel = new List<Year_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            Year_Detail objYear = new Year_Detail();

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
    public int Add_Service_BusinessAgainst_Detail(BussinessDr_Detail objDoctorDetail, List<string> objBusinessDr)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        BussinessDr_Detail objBusData = new BussinessDr_Detail();

        objBusData.Doctor_Code = objDoctorDetail.Doctor_Code;
        objBusData.Trans_Month = objDoctorDetail.Trans_Month;
        objBusData.Trans_Year = objDoctorDetail.Trans_Year;
        objBusData.PrdTotal = objDoctorDetail.PrdTotal;
        objBusData.Service_No = objDoctorDetail.Service_No;
        objBusData.Sf_Code = objDoctorDetail.Sf_Code;

        sf_code = objBusData.Sf_Code;
        int iReturn = 0;
        SecSale ss = new SecSale();


        int Chck_dobl_Sl_No = 0;

        DataSet ds_Chck_dobl_insrt = ss.Get_servAgnst_Chck_dobl_insrt(objBusData.Doctor_Code, div_code, sf_code, objBusData.Trans_Month,
            objBusData.Trans_Year, objBusData.Service_No, objBusData.PrdTotal);

        if (ds_Chck_dobl_insrt.Tables[0].Rows.Count > 0)
        {
            Chck_dobl_Sl_No = Convert.ToInt32(ds_Chck_dobl_insrt.Tables[0].Rows[0]["Sl_No"].ToString());
        }
        else
        {
            Chck_dobl_Sl_No = 0;
        }


        if (Chck_dobl_Sl_No == 0)
        {


            int Trans_Sl_No = ss.Get_Trans_Service_MaxRecordId();

            Trans_Sl_No = Trans_Sl_No + 1;

            //int Service_No = ss.Get_Trans_Service_ServiceNo(objBusData.Doctor_Code, sf_code,div_code);

            //objBusData.Service_No =Service_No

            if (objBusData.PrdTotal == "" || objBusData.PrdTotal == "NaN")
            {
                objBusData.PrdTotal = "0.00";
            }

            int Result = ss.Add_TransService_Business_Against_Head(Trans_Sl_No, objBusData.Doctor_Code, sf_code, objBusData.Trans_Month,
                objBusData.Trans_Year, objBusData.Service_No, div_code, objBusData.PrdTotal);


            int Prod_Id = ss.Get_Trans_Service_Prodcut_MaxRecordId();

            StringBuilder Sb_Product = new StringBuilder();
            Sb_Product.Append("<root>");

            for (int i = 0; i < objBusinessDr.Count; i++)
            {
                string strVAl = objBusinessDr[i].ToString();
                string[] Data = strVAl.Split('^');

                objBusData.Product_Code = Data[0];
                objBusData.Product_Name = Data[1];
                objBusData.Product_Rate = Data[2];
                objBusData.Prodcut_Qty = Data[3];
                objBusData.Product_Value = Data[4];
                objBusData.PrdTotal = objDoctorDetail.PrdTotal;
                Prod_Id = Prod_Id + 1;
                Sb_Product.Append("<Product Prd_Sl_No='" + Prod_Id + "' Trans_Sl_No='" + Convert.ToInt32(Trans_Sl_No) + "' Prod_Code='" + objBusData.Product_Code + "'  Product_Rate='" + objBusData.Product_Rate + "' Prodcut_Qty='" + objBusData.Prodcut_Qty + "' Product_Value='" + objBusData.Product_Value + "'  Div_Code='" + div_code + "'/>");

            }

            Sb_Product.Append("</root>");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("Sp_Trans_Service_Business_Against_ProdcutDet", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@XMLProduct_Det", SqlDbType.VarChar);
            cmd.Parameters[0].Value = Sb_Product.ToString();
            cmd.Parameters.Add("@retValue", SqlDbType.Int);
            cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
            conn.Close();


        }
        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string BusinessTotal(string objDrCode, string ServiceNo)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string[] ArrStr = objDrCode.Split('^');
        string Doc_Code = ArrStr[0];
        sf_code = ArrStr[1];

        SecSale ss = new SecSale();
        DataSet ds = ss.GetTotal_Business(div_code, Doc_Code, sf_code, ServiceNo);

        string Total = "";

        if (ds.Tables[0].Rows.Count > 0)
        {
            Total = ds.Tables[0].Rows[0]["Total"].ToString();
        }

        return Total;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<DrServiceDetail_1> GetService_Req_Detail(string objService)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        if (objService == "")
        {
            objService = "0";
        }

        objService = objService.TrimEnd(',');

        //objService = objService.Remove(objService.Length - 1);

        SecSale ss = new SecSale();
        DataSet dsSale = ss.Get_Doctor_Service_Req(div_code, objService.TrimStart());

        List<DrServiceDetail_1> objDrData = new List<DrServiceDetail_1>();

        if (dsSale.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsSale.Tables[0].Rows)
            {
                DrServiceDetail_1 objDr = new DrServiceDetail_1();

                objDr.SerReq_No = dr["SerReq_No"].ToString();
                objDr.Type = dr["Type"].ToString();
                objDr.Location = dr["Location"].ToString();
                objDr.Hotel_Type = dr["Hotel_Type"].ToString();
                objDr.No_Of_Person = dr["No_Of_Person"].ToString();
                objDr.From_Date = dr["From_Date"].ToString();
                objDr.To_Date = dr["To_Date"].ToString();
                objDr.Arraival_Time = dr["Arraival_Time"].ToString();
                objDr.Departure_Time = dr["Departure_Time"].ToString();
                objDr.Remarks = dr["Remarks"].ToString();
                objDr.Type_of_Travel = dr["Type_of_Travel"].ToString();
                objDr.Date = dr["Date"].ToString();
                objDr.From_Travel = dr["From_Travel"].ToString();
                objDr.To_Travel = dr["To_Travel"].ToString();
                objDr.No_of_Hrs_Km = dr["No_of_Hrs_Km"].ToString();
                objDr.Name = dr["Name"].ToString();
                objDr.Age = dr["Age"].ToString();
                objDr.Sex = dr["Sex"].ToString();
                objDr.Id_Proof = dr["Id_Proof"].ToString();
                objDr.Name_of_Books = dr["Name_of_Books"].ToString();
                objDr.Author = dr["Author"].ToString();
                objDr.Edition = dr["Edition"].ToString();
                objDr.Approx_Cost = dr["Approx_Cost"].ToString();
                objDr.Name_of_Conference = dr["Name_of_Conference"].ToString();
                objDr.Type_Of_Participation = dr["Type_Of_Participation"].ToString();
                objDr.Cost = dr["Cost"].ToString();
                objDr.Cheque_Draft = dr["Cheque_Draft"].ToString();
                objDr.Payable_At = dr["Payable_At"].ToString();
                objDr.Item_Descrp = dr["Item_Descrp"].ToString();
                objDr.Rate = dr["Rate"].ToString();
                objDr.Qty = dr["Qty"].ToString();
                objDr.Optiontype = dr["Other_II"].ToString();
                objDr.OtherRemark = dr["Other_Remark"].ToString();

                objDrData.Add(objDr);
            }
        }

        HttpContext.Current.Session["sf_code"] = HttpContext.Current.Session["S_Code"].ToString();

        return objDrData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public int Add_Dr_Service_Req(List<string> objServiceReq, string Ser_Sl_No)
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

        ServiceReq_1 objService = new ServiceReq_1();
        string[] ArrDr_SlNo = Ser_Sl_No.Split('@');
        string Sl_No = ArrDr_SlNo[0].ToString();

        int iReturn = 0;

        if (Ser_Type == "1")
        {

            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {

                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split(',');

            string[] SerSlNo = Ser_Req_Sl_No.Split(',');

            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            for (int i = 0; i < ArrSerDet.Length; i++)
            {
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

                string Ser_TravelNo = ArrSerVal[8];

                string Dr_Service_SlNo = string.Empty;

                // string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                if (Ser_TravelNo == "")
                {
                    Ser_TravelNo = "0";
                }
                else
                {
                    Ser_TravelNo = ArrSerVal[8];
                }

                string SerSlN = string.Empty;

                iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
           Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
           objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
           objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
           objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
           objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
           objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
           objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
           objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);


                int Service = ss.Get_Dr_Service_Req_ID();

                if (Ser_TravelNo != "0")
                {
                    Dr_Service_SlNo = Ser_Req_Sl_No;
                }
                else
                {
                    Service -= 1;
                    Dr_Service_SlNo = Convert.ToString(Service);
                }

                string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

                DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

                if (dsNum.Tables[0].Rows.Count > 0)
                {
                    string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                    Dr_Service_SlNo = SerVAl + ",";
                }

                int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            }

        }
        else if (Ser_Type == "2")
        {

            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {
                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split('@');

            string[] SerSlNo = ArrSerDet[0].Split('#');

            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            string[] Arr_FromDet = SerSlNo[0].Split(',');

            string[] S_SlNo = Ser_Req_Sl_No.Split(',');

            int SCnt = 0;

            for (int i = 0; i < Arr_FromDet.Length; i++)
            {
                string[] TravelDet = Arr_FromDet[i].Split('^');
                objService.Date = TravelDet[0];
                objService.From_Travel = TravelDet[1];
                objService.To_Travel = TravelDet[2];
                objService.No_of_Hrs_Km = TravelDet[3];

                string Ser_TravelNo = TravelDet[4];

                string Dr_Service_SlNo = string.Empty;

                // string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                objService.Type_of_Travel = SerSlNo[2];

                if (Ser_TravelNo == "")
                {
                    Ser_TravelNo = "0";
                }
                else
                {
                    Ser_TravelNo = TravelDet[4];
                }

                iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
              Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
              objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
              objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
              objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
              objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
              objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
              objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
              objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);

                int Service = ss.Get_Dr_Service_Req_ID();

                if (Ser_TravelNo != "0")
                {
                    Dr_Service_SlNo = Ser_Req_Sl_No;
                }
                else
                {
                    Dr_Service_SlNo = Convert.ToString(Service);
                }

                string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

                DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

                if (dsNum.Tables[0].Rows.Count > 0)
                {
                    string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                    Dr_Service_SlNo = SerVAl + ",";
                }

                int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);
            }

            string[] Arr_NameDet = SerSlNo[1].Split(',');


            string[] ArrSer_Type;

            ArrayList arr = new ArrayList();

            for (int i = 0; i < Arr_NameDet.Length; i++)
            {
                string[] ArrSerVal = Arr_NameDet[i].Split('^');
                //Arr_NameDet = SerSlNo[i].Split('^');
                objService.Name = ArrSerVal[0];
                objService.Age = ArrSerVal[1];
                objService.Sex = ArrSerVal[2];
                objService.Id_Proof = ArrSerVal[3];
                arr.Add(ArrSerVal);

                string Ser_TravelNo = ArrSerVal[4];

                string Dr_Service_SlNo = string.Empty;

                // string Dr_SerSlNo = "";
                string Trans_Sl_No = "";

                objService.Type_of_Travel = SerSlNo[2];

                objService.Type_of_Travel = "";
                objService.Date = "";
                objService.From_Travel = "";
                objService.To_Travel = "";
                objService.No_of_Hrs_Km = "";

                if (Ser_TravelNo == "")
                {
                    Ser_TravelNo = "0";
                }
                else
                {
                    Ser_TravelNo = ArrSerVal[4];
                }
                iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
               Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
               objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
               objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
               objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
               objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
               objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
               objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
               objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);


                int Service = ss.Get_Dr_Service_Req_ID();

                if (Ser_TravelNo != "0")
                {
                    Dr_Service_SlNo = Ser_Req_Sl_No;
                }
                else
                {
                    Service -= 1;
                    Dr_Service_SlNo = Convert.ToString(Service);
                }

                string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

                DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

                if (dsNum.Tables[0].Rows.Count > 0)
                {
                    string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                    Dr_Service_SlNo = SerVAl + ",";
                }

                int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            }

        }
        else if (Ser_Type == "3")
        {
            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {
                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Name_of_Books = ArrSerDet[0];
            objService.Author = ArrSerDet[1];
            objService.Edition = ArrSerDet[2];
            objService.Approx_Cost = ArrSerDet[3];

            string Ser_TravelNo = ArrSerDet[4];

            string[] SerSlNo = Ser_Req_Sl_No.Split(',');
            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            string Dr_Service_SlNo = string.Empty;
            string Trans_Sl_No = "";

            //  for (int i = 0; i < SerSlNo.Length; i++)
            // {
            if (Ser_TravelNo == "")
            {
                Ser_TravelNo = "0";
            }
            else
            {
                Ser_TravelNo = ArrSerDet[4];
            }
            iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
           Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
           objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
           objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
           objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
           objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
           objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
           objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
           objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);

            int Service = ss.Get_Dr_Service_Req_ID();

            if (Ser_TravelNo != "0")
            {
                Dr_Service_SlNo = Ser_Req_Sl_No;
            }
            else
            {
                Service -= 1;
                Dr_Service_SlNo = Convert.ToString(Service);
            }

            string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

            DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

            if (dsNum.Tables[0].Rows.Count > 0)
            {
                string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                Dr_Service_SlNo = SerVAl + ",";
            }

            int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            // }
        }
        else if (Ser_Type == "4")
        {
            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {
                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Name_of_Conference = ArrSerDet[0];
            objService.Date = ArrSerDet[1];
            objService.Type_Of_Participation = ArrSerDet[2];
            objService.Cost = ArrSerDet[3];
            objService.Cheque_Draft = ArrSerDet[4];
            objService.Payable_At = ArrSerDet[5];

            string Ser_TravelNo = ArrSerDet[6];

            string[] SerSlNo = Ser_Req_Sl_No.Split(',');
            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            string Dr_Service_SlNo = string.Empty;
            string Trans_Sl_No = "";

            if (Ser_TravelNo == "")
            {
                Ser_TravelNo = "0";
            }
            else
            {
                Ser_TravelNo = ArrSerDet[6];
            }

            //  for (int i = 0; i < SerSlNo.Length; i++)
            //  {

            iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
           Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
           objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
           objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
           objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
           objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
           objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
           objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
           objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);
            int Service = ss.Get_Dr_Service_Req_ID();

            if (Ser_TravelNo != "0")
            {
                Dr_Service_SlNo = Ser_Req_Sl_No;
            }
            else
            {
                Service -= 1;
                Dr_Service_SlNo = Convert.ToString(Service);
            }

            string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

            DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

            if (dsNum.Tables[0].Rows.Count > 0)
            {
                string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                Dr_Service_SlNo = SerVAl + ",";
            }

            int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            // }

        }
        else if (Ser_Type == "5")
        {
            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {
                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Item_Descrp = ArrSerDet[0];
            objService.Rate = ArrSerDet[1];
            objService.Qty = ArrSerDet[2];
            objService.Cheque_Draft = ArrSerDet[3];
            objService.Payable_At = ArrSerDet[4];

            string Ser_TravelNo = ArrSerDet[5];

            string[] SerSlNo = Ser_Req_Sl_No.Split(',');
            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            string Dr_Service_SlNo = string.Empty;
            string Trans_Sl_No = "";

            if (Ser_TravelNo == "")
            {
                Ser_TravelNo = "0";
            }
            else
            {
                Ser_TravelNo = ArrSerDet[5];
            }

            //  for (int i = 0; i < SerSlNo.Length; i++)
            //  {
            iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
  Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
  objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
  objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
  objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
  objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
  objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
  objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
  objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);

            int Service = ss.Get_Dr_Service_Req_ID();

            if (Ser_Req_Sl_No != "0")
            {
                Dr_Service_SlNo = Ser_Req_Sl_No;
            }
            else
            {
                Service -= 1;
                Dr_Service_SlNo = Convert.ToString(Service);
            }


            string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

            DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

            if (dsNum.Tables[0].Rows.Count > 0)
            {
                string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                Dr_Service_SlNo = SerVAl + ",";
            }

            int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            //  }

        }
        else if (Ser_Type == "6")
        {
            DataSet dsService = ss.Get_Trans_Dr_Service_reqID(DoctorCode, sf_code, div_code, Ser_Type);

            string Ser_Req_Sl_No = string.Empty;

            if (dsService.Tables[0].Rows.Count > 0)
            {
                Ser_Req_Sl_No = dsService.Tables[0].Rows[0]["SerReq_No"].ToString();
            }

            string[] ArrSerDet = SericeDet.Split('^');
            objService.Doctor_Code = DoctorCode;

            objService.Optiontype = ArrSerDet[0];
            objService.OtherRemark = ArrSerDet[1];

            string Ser_TravelNo = ArrSerDet[2];

            string[] SerSlNo = Ser_Req_Sl_No.Split(',');
            string Dr_SlNo = ArrDr_SlNo[1].ToString();

            string Dr_Service_SlNo = string.Empty;
            string Trans_Sl_No = "";

            // for (int i = 0; i < SerSlNo.Length; i++)
            //  {
            if (Ser_TravelNo == "")
            {
                Ser_TravelNo = "0";
            }
            else
            {
                Ser_TravelNo = ArrSerDet[2];
            }

            iReturn = ss.Doctor_Service_Req(DoctorCode, sf_code, div_code,
Ser_Type, objService.Location, objService.Hotel_Type, objService.No_Of_Person,
objService.From_Date, objService.To_Date, objService.Arraival_Time, objService.Departure_Time, objService.Remarks,
objService.Type_of_Travel, objService.Date, objService.From_Travel, objService.To_Travel, objService.No_of_Hrs_Km,
objService.Name, objService.Age, objService.Sex, objService.Id_Proof, objService.Name_of_Books, objService.Author,
objService.Edition, objService.Approx_Cost, objService.Name_of_Conference, objService.Type_Of_Participation,
objService.Cost, objService.Cheque_Draft, objService.Payable_At, objService.Item_Descrp, objService.Rate,
objService.Qty, Ser_TravelNo, Trans_Sl_No, Dr_Service_SlNo,
objService.Optiontype, objService.OtherRemark, Chemist_Cd, ModeType);

            int Service = ss.Get_Dr_Service_Req_ID();


            if (Ser_Req_Sl_No != "0")
            {
                Dr_Service_SlNo = Ser_Req_Sl_No;
            }
            else
            {
                Service -= 1;
                Dr_Service_SlNo = Convert.ToString(Service);
            }

            string SerNo = Convert.ToString(ArrDr_SlNo[0]) + Dr_Service_SlNo;

            DataSet dsNum = ss.Get_Dr_RemoveNumbers(div_code, SerNo);

            if (dsNum.Tables[0].Rows.Count > 0)
            {
                string SerVAl = dsNum.Tables[0].Rows[0]["SerSlNo"].ToString();
                Dr_Service_SlNo = SerVAl + ",";
            }

            int SerDr = ss.Get_Doctor_Service_HeadUpdate(ArrDr_SlNo[1], Dr_Service_SlNo);

            // }

        }

        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Chemist_Detail_1> BindChemist_Detail(string Chemist_Code)
    {

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        SecSale SS = new SecSale();
        DataSet dsCh = SS.Bind_Chemist_Detail(sf_code, div_code, Chemist_Code);

        List<Chemist_Detail_1> objChData = new List<Chemist_Detail_1>();

        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            Chemist_Detail_1 objch = new Chemist_Detail_1();
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

#region DrEdit_Param
public class ListedDoctor_Detail
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
    public string ServiceStateMent { get; set; }
    public string CreatedDate { get; set; }
    public string Sl_No { get; set; }
    public string Financial_Year { get; set; }
    public string Sevice_Amt_till_Date { get; set; }
    public string Busi_till_Date { get; set; }
    public string Service_Req { get; set; }
    public string Service_Amt { get; set; }
    public string Total_Business_Exp_Amt { get; set; }
    public string LastUpdatedDate { get; set; }


}
public class ServiceDetail
{
    public string ServiceID { get; set; }
    public string ServiceName { get; set; }
}
public class ListedDoctor_1
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
}
public class ProductDetail_1
{
    public string Calc_Rate { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Rate { get; set; }
}
public class Chemist_1
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
}
public class Stockist_1
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}
public class Year_Detail
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}
public class DrServiceDetail_1
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

    public string Cur_Crm_Prd_1 { get; set; }
    public string Cur_Crm_Prd_2 { get; set; }
    public string Cur_Crm_Prd_3 { get; set; }
    public string Cur_Crm_Prd_4 { get; set; }
    public string Cur_Crm_Prd_5 { get; set; }
    public string Cur_Crm_Prd_6 { get; set; }
    public string Cur_Crm_Tot { get; set; }


    public string Sl_No { get; set; }
    public string Prd_Sl_No { get; set; }

    public string Service_Sl_No { get; set; }

    public string SerReq_No { get; set; }
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

    public string ButtonVal { get; set; }
    public string Status { get; set; }

    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
    public string Chemists_Contact { get; set; }
    public string Chemists_Address1 { get; set; }
    public string Chemists_Phone { get; set; }
    public string territory_Name { get; set; }
    public string ModeType { get; set; }

    public string Mode_Type { get; set; }
    public string BillType { get; set; }

    public string Rej_Reason { get; set; }
    public string Rej_SfCode { get; set; }
}
public class BussinessDr_Detail
{
    public string Doctor_Code { get; set; }
    public string Sf_Code { get; set; }
    public string Division_Code { get; set; }
    public string Trans_Month { get; set; }
    public string Trans_Year { get; set; }
    public string Service_No { get; set; }

    public string PrdTotal { get; set; }

    public string Product_Code { get; set; }
    public string Product_Name { get; set; }
    public string Prodcut_Qty { get; set; }
    public string Product_Rate { get; set; }
    public string Product_Value { get; set; }
}
public class ServiceReq_1
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
public class Chemist_Detail_1
{
    public string Chemists_Code { get; set; }
    public string Chemists_Name { get; set; }
    public string Chemists_Contact { get; set; }
    public string Chemists_Address1 { get; set; }
    public string Chemists_Phone { get; set; }
    public string territory_Name { get; set; }
}
#endregion
