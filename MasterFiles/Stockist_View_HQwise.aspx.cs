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

public partial class MasterFiles_Stockist_View_HQwise : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
           
        }
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<HQDetail> BindHQDetail()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        Stockist SS = new Stockist();
        DataSet dsCh = SS.GetHQ_StockistView(div_code, sf_code);
        List<HQDetail> objChData = new List<HQDetail>();
        foreach (DataRow dr in dsCh.Tables[0].Rows)
        {
            HQDetail objch = new HQDetail();
            objch.HQ_Code = dr["Pool_Id"].ToString();
            objch.HQ_Name = dr["Pool_Name"].ToString();          
            objChData.Add(objch);
        }
        return objChData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<StockistDetail> GetStockist_Detail(string SearchText,string SearchOpt)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        Stockist SS = new Stockist();
        DataSet dsStock = SS.GetStockist_HQwise_Contribution(div_code,SearchText, SearchOpt);

        List<StockistDetail> objStockist = new List<StockistDetail>();
        foreach (DataRow dr in dsStock.Tables[0].Rows)
        {
            StockistDetail objStock = new StockistDetail();
            objStock.Stockist_Name = dr["Stockist_Name"].ToString();
            objStock.ERP_Code = dr["ERP_Code"].ToString();
            objStock.HQ_Name = dr["HQ_Name"].ToString();
            objStock.SF_HQ_Code = dr["SF_HQ_Code"].ToString();
            objStock.SF_HQ_Cont = dr["SF_HQ_Cont"].ToString();
            objStockist.Add(objStock);
        }
        return objStockist;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<State_Detail> BindState()
    {
        DataSet dsStockist = new DataSet();
        List<State_Detail> objData = new System.Collections.Generic.List<State_Detail>();

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
             dsState = st.getStateChkBox(state_cd);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                {
                    objData.Add(new State_Detail
                    {
                        StateCode = dsState.Tables[0].Rows[j]["state_code"].ToString(),
                        StateName = dsState.Tables[0].Rows[j]["statename"].ToString()
                    });
                }
            }
        }

        return objData;

    }
}
public class HQDetail
{
    public string HQ_Code { get; set; }
    public string HQ_Name { get; set; }
}
public class StockistDetail
{
    public string Stockist_Name { get; set; }
    public string ERP_Code { get; set; }
    public string HQ_Name { get; set; }
    public string SF_HQ_Code { get; set; }
    public string SF_HQ_Cont { get; set; }
    
}

public class State_Detail
{
    public string StateCode { get; set; }
    public string StateName { get; set; }
}