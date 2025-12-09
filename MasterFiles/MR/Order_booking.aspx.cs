using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Data.SqlClient;



public partial class MasterFiles_MR_Order_booking : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    DataSet dsField = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        Divid.Controls.Add(c1);
        if (!Page.IsPostBack)
        {
            //  menu.Visible = false;
            hdnsf_code.Value = sf_code;
            hdndiv_code.Value = div_code;            
            Fill_Base();

        }

    }

    private void Fill_Base()
    {
        strQry = " select a.Sf_Name +' - ' +sf_Designation_Short_Name+' - '+a.Sf_HQ as sf_name,sf_emp_id, subdivision_code," +
                " CONVERT(varchar,Sf_Joining_Date,103) as Sf_Joining_Date , " +
                " (select count(distinct ListedDrCode ) from Mas_ListedDr where sf_code=a.sf_code and ListedDr_Active_Flag=0) tot_Dr , " +
                " (select count(distinct Chemists_Code ) from Mas_Chemists where sf_code=a.sf_code and Chemists_Active_Flag=0) tot_Chem , " +
                " (select count(distinct Hospital_Code ) from Mas_Hospital where sf_code=a.sf_code and Hospital_Active_Flag=0) tot_Hosp , " +
                " (select count(distinct Stockist_Code ) from Mas_Stockist where Stockist_Active_Flag=0 " +
                " and Sf_Code like '%' + a.sf_code + '%' and Stockist_Name not like 'Direct%') tot_Stockist " +
                " from Mas_Salesforce a  where a.Sf_Code='" + hdnsf_code.Value + "' ";

        dsField = db_ER.Exec_DataSet(strQry);

        if (dsField.Tables[0].Rows.Count > 0)
        {
            lblname.Text = dsField.Tables[0].Rows[0]["sf_name"].ToString();
            totdr.Text = dsField.Tables[0].Rows[0]["tot_Dr"].ToString();
            totpharm.Text = dsField.Tables[0].Rows[0]["tot_Chem"].ToString();
            tothosp.Text = dsField.Tables[0].Rows[0]["tot_Hosp"].ToString();
            totstockist.Text = dsField.Tables[0].Rows[0]["tot_Stockist"].ToString();
            hdnsub_div.Value = dsField.Tables[0].Rows[0]["subdivision_code"].ToString();
        }

        //SecSale ss = new SecSale();
        //DataSet dsSale = ss.get_product_list(sf_code, div_code, hdnsub_div.Value);
        //if (dsSale.Tables[0].Rows.Count > 0)
        //{
        //    grdPrd.DataSource = dsSale;
        //    grdPrd.DataBind();
        //}
        //else
        //{
        //    grdPrd.DataSource = dsSale;
        //    grdPrd.DataBind();
        //}

    }

    //[WebMethod(EnableSession = true)]
    //[ScriptMethod]
    //public List<Stock_Detail2> GetStockist(string objSfCode)
    //{
    //    List<Stock_Detail2> objStockData = new List<Stock_Detail2>();
    //    try
    //    {
    //        string div_code = HttpContext.Current.Session["div_code"].ToString();
    //        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

    //        //DCR dc = new DCR();
    //        //DataSet dsSale = dc.getStockiest_SSentry(sf_code, div_code);

    //        SecSale ss = new SecSale();
    //        DataSet dsSale = ss.GetStockistDet_DDL(sf_code, div_code);

    //        if (dsSale.Tables[0].Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dsSale.Tables[0].Rows)
    //            {
    //                Stock_Detail2 objch = new Stock_Detail2();
    //                objch.Stockist_Code = dr["Stockist_Code"].ToString();
    //                objch.Stockist_Name = dr["Stockist_Name"].ToString();
    //                objStockData.Add(objch);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //    return objStockData;
    //}


    [WebMethod]
    public static string getstock(string mode)
    {


        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["sf_code"].ToString();

        // string query = "select ROW_NUMBER() over(order by(select 1)) Sl_no, District_Sl_No,District_Name,StateName from Mas_District where Active_Flag=0 and division_code='" + div_code + "' ";

        using (var con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        using (var cmd = new SqlCommand("select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) from Mas_Stockist where Stockist_Active_Flag=0 and Sf_Code like '%" + sf_code + "%' and Division_code = '" + div_code + "' and Stockist_Name not like 'Direct%' ", con))
        {
            con.Open();
            //object val = cmd.ExecuteScalar();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);


            return JsonConvert.SerializeObject(dt);
        }
    }

}

public class Stock_Detail2
{
    public string Stockist_Code { get; set; }
    public string Stockist_Name { get; set; }
}