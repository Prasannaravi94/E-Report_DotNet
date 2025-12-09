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
/// Summary description for SecSale_Stockist_EntryStatus_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class SecSale_Stockist_EntryStatus_WS : System.Web.Services.WebService 
{
    public SecSale_Stockist_EntryStatus_WS () 
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public  List<FieldDet> FieldForce_DDL()
    {
        List<FieldDet> objField = new List<FieldDet>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            string sf_code = HttpContext.Current.Session["sf_code"].ToString();
            string sf_Type = HttpContext.Current.Session["sf_type"].ToString();

            DataSet dsSalesForce;

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
                    FieldDet objFFDet = new FieldDet();
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
    public  List<YearDet> Year_DDL()
    {
        List<YearDet> objYearDel = new List<YearDet>();
        try
        {
            string div_code = HttpContext.Current.Session["div_code"].ToString();
            TourPlan tp = new TourPlan();

            DataSet dsYear = tp.Get_TP_Edit_Year(div_code);

            YearDet objYear = new YearDet();

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
    
}
public class FieldDet
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}
public class YearDet
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}