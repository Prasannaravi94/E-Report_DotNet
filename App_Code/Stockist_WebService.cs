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
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Collections;
/// <summary>
/// Summary description for Stockist_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Stockist_WebService : System.Web.Services.WebService {

    public Stockist_WebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

   //------------- Stockist HQ Updation Frm--------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Data_HQ> BindStockist_Data(Stockist_Data_HQ objStockist)
    {

        string Sf_Code = string.Empty;
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsFieldForce = new DataSet();
        List<Stockist_Data_HQ> objData = new System.Collections.Generic.List<Stockist_Data_HQ>();

        Stockist_Data_HQ F_Data = new Stockist_Data_HQ();
        F_Data.Sf_Code = objStockist.Sf_Code;
        F_Data.Sf_Name = objStockist.Sf_Name;
        F_Data.Sf_Type = objStockist.Sf_Type;

        DataSet dsStockist;
        Stockist objStock = new Stockist();

        dsStockist = objStock.GetStockist_Detail(div_code, F_Data.Sf_Code);

        foreach (DataRow dr in dsStockist.Tables[0].Rows)
        {
            Stockist_Data_HQ objStockDet = new Stockist_Data_HQ();
            objStockDet.Sf_Code = dr["SF_Code"].ToString();
            objStockDet.Stock_Code = Convert.ToInt32(dr["Stockist_Code"]);
            objStockDet.Stock_Name = dr["Stockist_Name"].ToString();
            objStockDet.HQ_Name = dr["Territory"].ToString();
            objStockDet.ERPCode = dr["Stockist_Designation"].ToString();
            objStockDet.State = dr["State"].ToString();
            objData.Add(objStockDet);
        }


        //  }

        return objData;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Data_HQ> bindHQName(string objState)
    {
        DataSet dsStockist = new DataSet();
        List<Stockist_Data_HQ> objData = new System.Collections.Generic.List<Stockist_Data_HQ>();

        string divcode = HttpContext.Current.Session["div_code"].ToString();

        string Sf_code = "admin";

        Stockist sk = new Stockist();
        dsStockist = sk.Get_HQ_Detail(divcode, Sf_code, objState);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsStockist.Tables[0].Rows.Count; i++)
            {
                objData.Add(new Stockist_Data_HQ
                {
                    PoolId = Convert.ToInt32(dsStockist.Tables[0].Rows[i]["Pool_Id"]),
                    Name = dsStockist.Tables[0].Rows[i]["Pool_Name"].ToString()
                });
            }
        }
        return objData;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Data_HQ> BindState()
    {
        DataSet dsStockist = new DataSet();
        List<Stockist_Data_HQ> objData = new System.Collections.Generic.List<Stockist_Data_HQ>();

        string divcode = HttpContext.Current.Session["div_code"].ToString();

        string Sf_code = "admin";
        string[] statecd;
        string state_cd = string.Empty;
        string sState = string.Empty;
        string state_code = string.Empty;

        Division dv = new Division();
        DataSet dsDivision;
        dsDivision = dv.getStatePerDivision(divcode);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            DataSet dsState;
            //dsState = st.getStateChkBox(state_cd);

            dsState = st.getState_Stock(state_cd, divcode);

            if (dsState.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                {
                    objData.Add(new Stockist_Data_HQ
                    {
                        StateCode = dsState.Tables[0].Rows[j]["state_code"].ToString(),
                        StateName = dsState.Tables[0].Rows[j]["statename"].ToString()
                    });
                }
            }
        }

        return objData;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Data_HQ> GetHQDetail()
    {
        DataSet dsStockist = new DataSet();
        List<Stockist_Data_HQ> objData = new System.Collections.Generic.List<Stockist_Data_HQ>();

        string divcode = HttpContext.Current.Session["div_code"].ToString();

        string Sf_code = "admin";

        Stockist sk = new Stockist();
        dsStockist = sk.getPool_Name(divcode, Sf_code);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsStockist.Tables[0].Rows.Count; i++)
            {
                objData.Add(new Stockist_Data_HQ
                {
                    PoolId = Convert.ToInt32(dsStockist.Tables[0].Rows[i]["Pool_Id"]),
                    Name = dsStockist.Tables[0].Rows[i]["Pool_Name"].ToString()
                });
            }
        }
        return objData;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public void UpdateStockistDet(List<string> objStockData)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        string U_Data;

        StringBuilder Sb_Stockist = new StringBuilder();
        Sb_Stockist.Append("<root>");

        for (int i = 0; i < objStockData.Count; i++)
        {
            U_Data = objStockData[i].ToString();

            string[] values = U_Data.Split('^');

            Stockist_Data_HQ objStock = new Stockist_Data_HQ();
            objStock.Stock_Code = Convert.ToInt32(values[0]);
            objStock.Stock_Name = values[1];
            objStock.ERPCode = values[2];
            objStock.StateName = values[3];
            objStock.HQ_Name = values[4];

            string HQ = values[4].Replace("&", "&amp;");

            Sb_Stockist.Append("<Product Stock_Code='" + Convert.ToInt32(objStock.Stock_Code) + "'  ERPCode='" + objStock.ERPCode + "' StateName='" + objStock.StateName + "' HQ_Name='" + HQ + "'  Div_Code='" + div_code + "'/>");

        }

        Sb_Stockist.Append("</root>");

        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_BulkUpdate_Stockist_Detail", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@XMLTransUpdate_Val", SqlDbType.VarChar);
        cmd.Parameters[0].Value = Sb_Stockist.ToString();
        cmd.Parameters.Add("@retValue", SqlDbType.Int);
        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        int iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        conn.Close();

        // return iReturn;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_Data_HQ> Bind_Stockist_Statewise(Stockist_Data_HQ objState)
    {
        string Sf_Code = string.Empty;
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsFieldForce = new DataSet();
        List<Stockist_Data_HQ> objData = new System.Collections.Generic.List<Stockist_Data_HQ>();

        Stockist_Data_HQ F_Data = new Stockist_Data_HQ();
        F_Data.StateName = objState.StateName;

        Stockist ss = new Stockist();
        DataSet dsStock = ss.Get_Statewise_Stockist_Detail(div_code, F_Data.StateName);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Stockist_Data_HQ objStockDet = new Stockist_Data_HQ();
                objStockDet.Sf_Code = dr["SF_Code"].ToString();
                objStockDet.Stock_Code = Convert.ToInt32(dr["Stockist_Code"]);
                objStockDet.Stock_Name = dr["Stockist_Name"].ToString();
                objStockDet.HQ_Name = dr["Territory"].ToString();
                objStockDet.ERPCode = dr["Stockist_Designation"].ToString();
                objStockDet.State = dr["State"].ToString();
                objData.Add(objStockDet);
            }
        }

        return objData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Year_Det_Stock> FillYear()
    {
        List<Year_Det_Stock> objYearDel = new List<Year_Det_Stock>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            Year_Det_Stock objYear = new Year_Det_Stock();

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
    public List<Stock_Det> GetStockist_List(string objStock)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        List<Stock_Det> objStockist = new List<Stock_Det>();

        Stock_Det objData = new Stock_Det();
        string[] ArrDet = objStock.Split('^');

        objData.Month = ArrDet[0];
        objData.Year = ArrDet[1];

        SecSale ss = new SecSale();
        DataSet dsStock = ss.Get_Statewise_StockistDet(div_code, objData.Month, objData.Year);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Stock_Det objS = new Stock_Det();
                objS.SNo = dr["R_Id"].ToString();
                objS.StateCode = dr["State_Code"].ToString();
                objS.StateName = dr["State_Name"].ToString();
                objS.AvaStockist = dr["Ava_Stockist"].ToString();
                objS.StockistDone = dr["Stockist_Done"].ToString();
                objS.RemainStock = dr["RemainStock"].ToString();

                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Stockist_List> Get_Statewise_Stockist_MRDet(string objStock)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        List<Stockist_List> objStockist = new List<Stockist_List>();
        List<Stockist_List> objStockist1 = new List<Stockist_List>();

        Stockist_List objData = new Stockist_List();
        string[] ArrDet = objStock.Split('^');

        objData.Month = ArrDet[0];
        objData.Year = ArrDet[1];
        objData.StateCode = ArrDet[2];
        //objData.StateName = ArrDet[3];

        String[] myList;
        String[] Stock2;

        SecSale ss = new SecSale();
        DataSet dsStock = ss.Get_Statewise_Stock_MR_Det(div_code, objData.StateCode, objData.Month, objData.Year);

        List<string> LstStock1 = new List<string>();
        if (dsStock.Tables[0].Rows.Count > 0)
        {
            Stockist_List objS = new Stockist_List();
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Stockist_List Arr = new Stockist_List();
                // objS.SNo = dr["S_Id"].ToString();
                objS.StockName = dr["Stockist_Name"].ToString();
                objS.HqName = dr["Territory"].ToString();
                objS.SfCode = dr["MR_Name"].ToString();
                objS.StateName = dr["State"].ToString();
                // LstStock1.Add(objS);

                string ArrStk = objS.StockName + "^" + objS.HqName + "^" + objS.SfCode + "^" + objS.StateName;
                LstStock1.Add(ArrStk);

            }

            myList = LstStock1.ConvertAll<String>(p => p.ToString()).ToArray<String>();
            objS.ArrStock_1 = myList;

            objStockist.Add(objS);
        }
        List<string> LstStock2 = new List<string>();
        if (dsStock.Tables[1].Rows.Count > 0)
        {
            Stockist_List objS = new Stockist_List();
            foreach (DataRow dr in dsStock.Tables[1].Rows)
            {

                Stockist_List Arr = new Stockist_List();
                // objS.SNo = dr["S_Id"].ToString();
                objS.StockName = dr["Stockist_Name"].ToString();
                objS.HqName = dr["Territory"].ToString();
                objS.SfCode = dr["MR_Name"].ToString();
                objS.StateName = dr["State"].ToString();
                objS.SS_Date = dr["SS_Date"].ToString();
                // LstStock2.Add(Arr);

                string ArrStk = objS.StockName + "^" + objS.HqName + "^" + objS.SfCode + "^" + objS.StateName + "^" + objS.SS_Date;
                LstStock2.Add(ArrStk);
            }
            Stock2 = LstStock2.ConvertAll<String>(p => p.ToString()).ToArray<String>();
            objS.ArrStock_2 = Stock2;

            objStockist.Add(objS);
        }

        return objStockist;

    }


    //-------------------- Stockist Bulk DeActivation MR wise--------------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Field_DDL> GetFieldForceName()
    {
        List<Field_DDL> objField = new List<Field_DDL>();
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
                    Field_DDL objFFDet = new Field_DDL();
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
    public List<Stockist_Detail> GetStockist(string objSfCode)
    {
        List<Stockist_Detail> objStockData = new List<Stockist_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = objSfCode;

            SecSale ss = new SecSale();
            DataSet dsSale = ss.GetStock_Detail_Entry(sf_code, div_code);
            if (dsSale.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSale.Tables[0].Rows)
                {
                    Stockist_Detail objch = new Stockist_Detail();
                    objch.Sl_No = dr["Sl_No"].ToString();
                    objch.Stockist_Code = dr["Stockist_Code"].ToString();
                    objch.Stockist_Name = dr["Stockist_Name"].ToString();
                    objch.State = dr["State"].ToString();
                    objch.Territory = dr["Territory"].ToString();
                    objch.Stockist_Address = dr["Stockist_Address"].ToString();
                    objch.Stockist_Mobile = dr["Stockist_Mobile"].ToString();
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
    public int BulkDeactivateStockist(string objStock, string objSCode)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        string StockCode = objStock;

        int iRet = 1;

        SecSale SS = new SecSale();
        iRet = SS.DeActivate_Stockist_Detail(div_code, objSCode, StockCode);

        return iRet;
    }


    /*------------------SecSale HQ wise Sale Report-----------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_SS_HQwise_Sale_Det(string ModeType)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_HQ_wise_Sale_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mode_type", ModeType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    private static Dictionary<string, object> ToJson(DataTable table)
    {
        Dictionary<string, object> d = new Dictionary<string, object>();
        d.Add(table.TableName, RowsToDictionary(table));
        return d;
    }

    private static List<Dictionary<string, object>> RowsToDictionary(DataTable table)
    {
        List<Dictionary<string, object>> objs =
        new List<Dictionary<string, object>>();
        foreach (DataRow dr in table.Rows)
        {
            Dictionary<string, object> drow = new Dictionary<string, object>();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                drow.Add(table.Columns[i].ColumnName, dr[i]);
            }
            objs.Add(drow);
        }

        return objs;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_HQ_Det> Get_All_HQ_Det()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        List<Get_HQ_Det> objStockist = new List<Get_HQ_Det>();

        Get_HQ_Det objData = new Get_HQ_Det();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.GetHQ_AllDetail(div_code, sf_code);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_HQ_Det objS = new Get_HQ_Det();
                objS.Hq_Id = dr["S_Id"].ToString();
                objS.HQ_Name = dr["RptHead"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    /*------------------SecSale HQ wise Sale and Closing Report-----------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_SSPrd_SaleandClosing(string ModeType)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_Product_HQwise_Sale_Closing_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mode_type", ModeType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /*------------------SecSale Free All Modes Report-----------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_parameter> Get_Parameter()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();


        List<Get_parameter> objStockist = new List<Get_parameter>();

        Get_parameter objData = new Get_parameter();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.Get_SS_Parameter(div_code);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_parameter objS = new Get_parameter();
                objS.Sno = dr["SNO"].ToString();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objS.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_SecSale_Free_AllMode()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SP_SecSale_Product_Free_All_Modes_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[0].Copy();
        dtrowClr.TableName = "dtStock";
        // dsts.Tables[1].Columns.Remove("Sl_No");
        //dsts.Tables[1].Columns.Remove("Sl_No1");
        // dsts.Tables[1].Columns.Remove("Product_Code");

        return ToJson(dtrowClr);

        //String daresult = null;
        //DataTable yourDatable = new DataTable();
        //DataSet ds = new DataSet();
        //ds.Tables.Add(dtrowClr);
        //daresult = DataSetToJSON(ds);
        //return daresult;        
    }

    /*------------------Managerwise Stock and Sale Rpt-----------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_Mgr_StockandSale()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SP_SecSale_Mgrwise_StockSale_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[0].Copy();
        dtrowClr.TableName = "dtStock";
        // dsts.Tables[1].Columns.Remove("Sl_No");
        //dsts.Tables[1].Columns.Remove("Sl_No1");
        // dsts.Tables[1].Columns.Remove("Product_Code");

        return ToJson(dtrowClr);

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_parameter> GetSaleClose_Field()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();


        List<Get_parameter> objStockist = new List<Get_parameter>();

        Get_parameter objData = new Get_parameter();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.GetSale_Close_Param(div_code);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_parameter objS = new Get_parameter();
                objS.Sno = dr["SNO"].ToString();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    /*-------------- MR wise Stock and Sale Rpt -----------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_MR_StockandSale_Det()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SP_SecSale_Stock_MRwise_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        // dsts.Tables[1].Columns.Remove("Sl_No");
        //dsts.Tables[1].Columns.Remove("Sl_No1");
        // dsts.Tables[1].Columns.Remove("Product_Code");

        return ToJson(dtrowClr);

    }

    /*-------------- Product State wise Sale Rpt -----------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_State_Det> Get_State_Detail()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();

        List<Get_State_Det> objStockist = new List<Get_State_Det>();

        Get_State_Det objData = new Get_State_Det();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.Get_Stockist_State(div_code, sf_code);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_State_Det objS = new Get_State_Det();
                objS.State_Id = dr["S_Id"].ToString();
                objS.State_Name = dr["RptHead"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_SS_Statewise_Sale_Det()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_Product_Statewise_Sale_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /*------------------- Brand wise Product Sale and Closing Rpt  --------------------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_Brandwise_Prd_SaleAndClosing(string objPrdType, string objSS_Code)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_Brandwise_Sale_Closing_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Prd_Mode", objPrdType);
        cmd.Parameters.AddWithValue("@RptField", objSS_Code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /*-------------- State and HQ wise Sale and Closing Rpt-----------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_StateHQ_SaleAndClosing()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        //SP_SecSale_State_HQ_StockandSale_SS_rpt
        sProc_Name = "SecSale_State_HQ_StockandSale_SS_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mode_Type", OptType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_parameter> GetType_Rpt_Field()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string OptType = HttpContext.Current.Session["OptType"].ToString();

        List<Get_parameter> objStockist = new List<Get_parameter>();

        Get_parameter objData = new Get_parameter();

        //SecSale ss = new SecSale();
        //DataSet dsStock = ss.GetSale_Close_Param(div_code);

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SP_Get_Sale_Close_Field_ModeBased";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Mode_Type", OptType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsStock = new DataSet();
        da.Fill(dsStock);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_parameter objS = new Get_parameter();
                objS.Sno = dr["SNO"].ToString();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    /*-------------------Stockist wise Product Sale and Closing Rpt  --------------------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_Stockistwise_Prd_SaleAndClosing()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_Product_All_Stock_Sale_Close_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@Mode_Type", OptType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_HQ_Det> Get_All_Stockist_Det()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<Get_HQ_Det> objStockist = new List<Get_HQ_Det>();

        Get_HQ_Det objData = new Get_HQ_Det();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.GetStockist_AllDetail(div_code, sf_code);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_HQ_Det objS = new Get_HQ_Det();
                objS.Hq_Id = dr["Stockist_Code"].ToString();
                objS.HQ_Name = dr["Stockist_Name"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    /*------------------- HQ wise Product Sale and Closing Rpt  --------------------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_HQwise_Stockist_SaleAndClosing()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SecSale_HQwise_Stockist_Sale_Closing_rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@Mode_Type", OptType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /*------------------- Subdivsionwise Sale and Closing Rpt  --------------------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_SubDivision_wise_SaleAndClosing()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "SecSale_SubDivisionwise_SaleAndClose_Report";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@Mode_Type", OptType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /*------------------------- Stockist Drop Down   ----------------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_HQ_Det> Get_Stockist_DDl_All_Det(string Sf_Id)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<Get_HQ_Det> objStockist = new List<Get_HQ_Det>();

        Get_HQ_Det objData = new Get_HQ_Det();

        SecSale ss = new SecSale();
        DataSet dsStock = ss.Get_All_Stockist_DropDown(div_code, Sf_Id);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_HQ_Det objS = new Get_HQ_Det();
                objS.Hq_Id = dr["Stockist_Code"].ToString();
                objS.HQ_Name = dr["Stockist_Name"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    /*------------------------- Stockistwise Product All Month Cum   ----------------------------*/
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> GetStockist_Product_Stockist_Month_Cum()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        string StockistCode = HttpContext.Current.Session["Stockist_Code"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SecSale_Product_Stockist_Monthwise_Cum";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Stockist_Code", StockistCode);
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        // var pivotedTable = Pivot(dtrowClr, "Product_code");
        return ToJson(dtrowClr);
    }


    /*------------------------------- Product Stockist (Console) --------------------------------------*/

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_SS_ReportField> Get_SS_RptFiled()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();


        List<Get_SS_ReportField> objStockist = new List<Get_SS_ReportField>();

        Get_SS_ReportField objData = new Get_SS_ReportField();

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SS_Get_Reporting_Field";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_Code", Convert.ToInt32(div_code));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsStock = new DataSet();
        da.Fill(dsStock);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_SS_ReportField objS = new Get_SS_ReportField();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objS.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_SS_AllField> Get_SS_All_RptFiled()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<Get_SS_AllField> objStockist = new List<Get_SS_AllField>();
        Get_SS_AllField objData = new Get_SS_AllField();

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SS_Get_SS_All_ReportingField";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_Code", Convert.ToInt32(div_code));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsStock = new DataSet();
        da.Fill(dsStock);

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_SS_AllField objS = new Get_SS_AllField();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objS.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objS.Sub_Needed = dr["Sub_Needed"].ToString();
                objS.Sub_Label_1 = dr["Sub_Label_1"].ToString();
                objStockist.Add(objS);
            }
        }

        return objStockist;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> GetStockist_Product_All_Parameter(string objData)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string OptType = HttpContext.Current.Session["OptType"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        string[] Data = objData.Split('^');

        string Sale_F = Data[0];
        string Transit = Data[1];
        string Free = Data[2];

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        //sProc_Name = "SecSale_Product_Stockist_AllParam_rpt";
        sProc_Name = "SecSale_Product_Stockist_Transit_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@SS_Code", objData);
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        // var pivotedTable = Pivot(dtrowClr, "Product_code");
        return ToJson(dtrowClr);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> GetProduct_Detail()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SecSale_All_Product_List";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[0].Copy();
        dtrowClr.TableName = "dtProd";
        return ToJson(dtrowClr);
    }

    /*----------------------- Stock and Sale Consolidate ------------------------------------ */
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Get_SS_AllField> Get_All_ParamField()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<Get_SS_AllField> objStockist = new List<Get_SS_AllField>();
        Get_SS_AllField objData = new Get_SS_AllField();

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SS_Get_All_Parameter";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_Code", Convert.ToInt32(div_code));
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsStock = new DataSet();
        da.Fill(dsStock);

        string calc_rate = dsStock.Tables[1].Rows[0][0].ToString();

        if (dsStock.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow dr in dsStock.Tables[0].Rows)
            {
                Get_SS_AllField objS = new Get_SS_AllField();
                objS.Sec_Sale_Code = dr["Sec_Sale_Code"].ToString();
                objS.Sec_Sale_Name = dr["Sec_Sale_Name"].ToString();
                objS.calc_rate = calc_rate;
                objStockist.Add(objS);
            }
        }

        return objStockist;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> GetStock_and_Sale_Consolidate_Cum(string Param)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        string StockistCode = HttpContext.Current.Session["Stockist_Code"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "SecSale_Stock_Sale_Consolidate_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Param", Param);
        cmd.Parameters.AddWithValue("@Stockist_Code", StockistCode);
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        // var pivotedTable = Pivot(dtrowClr, "Product_code");
        return ToJson(dtrowClr);
    }

    /*----------------------- Sales Comparison ------------------------------------ */

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_Sales_Comparison(string objType, string objMonth)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();
        // string StockistCode = HttpContext.Current.Session["Stockist_Code"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        if (objType == "1")
        {
            sProc_Name = "SecSale_Product_Sales_Comparison_Rpt";
        }
        else if (objType == "2")
        {
            sProc_Name = "SecSale_Product_Sales_Comparison_HQwise_Rpt";
        }

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@DataMY", objMonth);
        // cmd.Parameters.AddWithValue("@Stockist_Code", StockistCode);
        cmd.CommandTimeout = 600;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[0].Copy();
        dtrowClr.TableName = "dtStock";
        // var pivotedTable = Pivot(dtrowClr, "Product_code");
        return ToJson(dtrowClr);
    }

    /* ------------------------------- Active and DeActive (Product and Stockist) -----------------------------  */

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_Prod_Stockist_DeActive(string ModeType)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "SS_Stockist_Product_DeActive_Rpt";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mode_type", ModeType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }

    /* ------------------------------- Active and DeActive (Product) -----------------------------  */

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public Dictionary<string, object> Get_ProductList_DeActive(string ModeType)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code_1"].ToString();
        string FMonth = HttpContext.Current.Session["FMonth"].ToString();
        string FYear = HttpContext.Current.Session["FYear"].ToString();
        string TMonth = HttpContext.Current.Session["TMonth"].ToString();
        string TYear = HttpContext.Current.Session["TYear"].ToString();

        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }

        SalesForce sf = new SalesForce();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";



        sProc_Name = "SS_Stockist_Product_Detail_DeActiveList";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mode_type", ModeType);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        DataTable dtrowClr = dsts.Tables[1].Copy();
        dtrowClr.TableName = "dtStock";
        return ToJson(dtrowClr);
    }
}

public class Stockist_Data_HQ
{
    public int Stock_Code { get; set; }
    public string Stock_Name { get; set; }
    public string HQ_Name { get; set; }
    public string Sf_Name { get; set; }
    public string Sf_Code { get; set; }
    public string Sf_Type { get; set; }
    public int PoolId { get; set; }
    public string Name { get; set; }
    public string ERPCode { get; set; }
    public string Sname { get; set; }
    public string State { get; set; }
    public string StateCode { get; set; }
    public string StateName { get; set; }
}

public class Year_Det_Stock
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}

public class Stock_Det
{
    public string SNo { get; set; }
    public string StateCode { get; set; }
    public string StateName { get; set; }
    public string AvaStockist { get; set; }
    public string StockistDone { get; set; }
    public string RemainStock { get; set; }

    public string Month { get; set; }
    public string Year { get; set; }
}

public class Stockist_List
{
    public string SNo { get; set; }
    public string StateCode { get; set; }
    public string StateName { get; set; }
    public string StockName { get; set; }
    public string StockistDone { get; set; }
    public string SfCode { get; set; }
    public string HqName { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
    public string SS_Date { get; set; }

    public Array ArrStock_2 { get; set; }
    public Array ArrStock_1 { get; set; }

}


public class Field_DDL
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}
public class Stockist_Detail
{
    public string Sl_No { get; set; }
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
    public string State { get; set; }
    public string Territory { get; set; }
    public string Stockist_Address { get; set; }
    public string Stockist_Mobile { get; set; }
}


public class Primary_Stockist
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
}

public class Get_HQ_Det
{
    public string Hq_Id { get; set; }
    public string HQ_Name { get; set; }
}

public class Get_parameter
{
    public string Sno { get; set; }
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }
}
public class Get_State_Det
{
    public string State_Id { get; set; }
    public string State_Name { get; set; }
}
public class Get_Product
{
    public string Product_Id { get; set; }
    public string Product_Name { get; set; }
}

public class Get_SS_ReportField
{
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }

}

public class Get_SS_AllField
{
    public string Sec_Sale_Code { get; set; }
    public string Sec_Sale_Name { get; set; }
    public string Sub_Needed { get; set; }
    public string Sub_Label_1 { get; set; }
    public string calc_rate { get; set; }
}