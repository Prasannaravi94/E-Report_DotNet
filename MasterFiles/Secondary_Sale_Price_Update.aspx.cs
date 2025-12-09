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


public partial class MasterFiles_Secondary_Sale_Price_Update : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        string sf_code = Session["sf_code"].ToString();
        string div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
        }
        hHeading.InnerText = Page.Title;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Get_Sale_Detail> BindState()
    {
        DataSet dsStockist = new DataSet();
        List<Get_Sale_Detail> objData = new System.Collections.Generic.List<Get_Sale_Detail>();

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
                    objData.Add(new Get_Sale_Detail
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
    public static List<Get_Sale_Detail> FillYear()
    {
        List<Get_Sale_Detail> objYearDel = new List<Get_Sale_Detail>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            Get_Sale_Detail objYear = new Get_Sale_Detail();

            if (dsYear.Tables[0].Rows.Count > 0)            {
               
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
    public static int Update_SecSale_Price(Get_Sale_Detail objDataDet)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        List<Get_Sale_Detail> objData = new List<Get_Sale_Detail>();

        Get_Sale_Detail objSecSale = new Get_Sale_Detail();

        objSecSale.StateCode = objDataDet.StateCode;
        objSecSale.PriceID = objDataDet.PriceID;
        objSecSale.Month = objDataDet.Month;
        objSecSale.Year = objDataDet.Year;

        if (objSecSale.PriceID == "M")
        {
            objSecSale.PriceID = "MRP_Price";
        }
        else if (objSecSale.PriceID == "R")
        {
            objSecSale.PriceID = "Retailor_Price";
        }
        else if (objSecSale.PriceID == "D")
        {
            objSecSale.PriceID = "Distributor_Price";
        }
        else if (objSecSale.PriceID == "T")
        {
            objSecSale.PriceID = "Target_Price";
        }
        else if (objSecSale.PriceID == "N")
        {
            objSecSale.PriceID = "NSR_Price";
        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        conn.Open();
        SqlCommand cmd = new SqlCommand("SP_Update_SecSale_Price", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Division_Code", div_code);
        cmd.Parameters.AddWithValue("@State_Code", objSecSale.StateCode);
        cmd.Parameters.AddWithValue("@PriceDetail", objSecSale.PriceID);
        cmd.Parameters.AddWithValue("@Month", objSecSale.Month);
        cmd.Parameters.AddWithValue("@Year", objSecSale.Year);    
        int iReturn=Convert.ToInt32(cmd.ExecuteNonQuery());  
  
        conn.Close();
        return iReturn;

    }

}

public class Get_Sale_Detail
{
    public string StateCode { get; set; }
    public string StateName { get; set; }
    public string PriceID { get; set; }
    public string PriceName { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }
}