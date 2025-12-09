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

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_ServiceClose : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string div_code = Session["div_code"].ToString();
        string sfCode = Session["sf_code"].ToString();
        Session["backurl"] = "Doctor_Service_CRM_Admin_Approve.aspx";

        string doctorcode = Request.QueryString["ListedDrCode"];

        string Sf_MRCode = Request.QueryString["Sf_MrName"];

        Session["Sf_MRCode"] = Sf_MRCode;

        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = true;
            
        }
    }

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

    public static List<ServiceDr_Det> GetServcieDoctorDetail(string DrCode,string DrSlNo)
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
             
            foreach(DataRow drPrd in dsProd.Tables[0].Rows)
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
    public static void Add_Dr_Service_Close(List<string> objServiceDel)
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