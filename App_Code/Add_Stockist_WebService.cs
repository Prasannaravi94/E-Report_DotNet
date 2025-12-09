using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Bus_EReport;
/// <summary>
/// Summary description for Add_Stockist_WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Add_Stockist_WebService : System.Web.Services.WebService 
{

    public Add_Stockist_WebService(){

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //---------- Stockist List Frm--------//

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<StockistDDL> GetDropDown(StockistDDL objDDL)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        List<StockistDDL> objDel = new List<StockistDDL>();

        if (objDDL.Type == "State")
        {
            Division dv = new Division();
            DataSet dsDivision = dv.getStatePerDivision(div_code);
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                string[] statecd;
                string state_cd = string.Empty;
                string sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
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
                DataSet dsState = st.getStateChkBox(state_cd);

                foreach (DataRow dr in dsState.Tables[0].Rows)
                {
                    StockistDDL objStateDet = new StockistDDL();
                    objStateDet.StateName = dr["statename"].ToString();
                    objStateDet.State_Code = dr["state_code"].ToString();
                    objDel.Add(objStateDet);
                }


            }
        }
        if (objDDL.Type == "Territory")
        {
            Stockist sk = new Stockist();
            string Sf_code = "admin";
            DataSet dsStockist = sk.getPool_Name(div_code, Sf_code);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsStockist.Tables[0].Rows)
                {
                    StockistDDL objStateDet = new StockistDDL();
                    objStateDet.HQ_Name = dr["Pool_Name"].ToString();
                    objStateDet.Hq_ID = dr["Pool_Id"].ToString();
                    objDel.Add(objStateDet);
                }

            }
        }

        return objDel;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public  List<StockistList> BindState()
    {
        DataSet dsStockist = new DataSet();
        List<StockistList> objData = new System.Collections.Generic.List<StockistList>();

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
            // dsState = st.getStateChkBox(state_cd);

            dsState = st.getState_Stock(state_cd, divcode);

            if (dsState.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
                {
                    objData.Add(new StockistList
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
    public  List<StockistData> Bind_Statewise_HQ(string objState)
    {
        DataSet dsStockist = new DataSet();
        List<StockistData> objData = new System.Collections.Generic.List<StockistData>();

        string divcode = HttpContext.Current.Session["div_code"].ToString();

        Stockist ss = new Stockist();
        DataSet ds = ss.Get_Statewise_HQ_Det(objState, divcode);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                objData.Add(new StockistData
                {
                    PoolId = Convert.ToInt32(ds.Tables[0].Rows[i]["Pool_Id"]),
                    Name = ds.Tables[0].Rows[i]["Pool_Name"].ToString()
                });
            }
        }
        return objData;

    }

}

public class StockistDDL
{
    public string StateName { get; set; }
    public string State_Code { get; set; }
    public string HQ_Name { get; set; }
    public string Hq_ID { get; set; }
    public string Type { get; set; }

}

public class StockistData
{
    public string Sname { get; set; }
    public string Name { get; set; }
    public int PoolId { get; set; }
}

public class StockistList
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