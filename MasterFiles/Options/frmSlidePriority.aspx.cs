using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Data;
using Newtonsoft.Json;
using Bus_EReport;
using System.IO;


public partial class MasterFiles_Options_frmSlidePriority : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            c1.Title = "Slide Priority";

            Divid.Controls.Add(c1);
        }
        catch (Exception ex)
        {

        }

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> FillSpecGroup()
    {
        List<userData> u = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Doctor Doc = new Doctor();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Doc.getDocSpec(div_code);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            u.Add(new userData
            {
                id_ = column["Doc_Cat_Code"].ToString(),
                name_ = column["Doc_Cat_Name"].ToString(),

            });

        }
        return u;
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> FillTheroGroup()
    {
        List<userData> u = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.getProductGroup(div_code);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            u.Add(new userData
            {
                id_ = column["Product_Grp_Code"].ToString(),
                name_ = column["Product_Grp_Name"].ToString(),

            });

        }
        return u;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> GetSelect()
    {
        List<userData> u = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.GetSelect(div_code);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
          userData data = new userData();
                data.id_ = column["Value"].ToString();
                data.name_ = column["Text"].ToString();

            u.Add(data);

        }
        return u;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> GetProductSlidePriorty(string Mode, string ddlFiler)
    {
        List<userData> u = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string strFilter = string.Empty;
        Product Prd = new Product();
        DataSet dsFillPriorty = new DataSet();

        dsFillPriorty = Prd.GetProductSlidePriorty(div_code, ddlFiler, Mode);
        foreach (DataRow column in dsFillPriorty.Tables[0].Rows)
        {
            userData data = new userData();
            data.Product_Brand = column["Product_Brand"].ToString();
            data.Img_Name = column["Img_Name"].ToString();
            data.Img_Count = column["Img_Count"].ToString();
            data.Image = column["Image"].ToString();
            strFilter = strFilter + data.Product_Brand + ",";
            u.Add(data);



        }

        userData data1 = new userData();
        data1.FilterArray = strFilter;
        u.Add(data1);
        return u;
    }

    public class userData
    {
        public string Product_Brand;
        public string Img_Name;
        public string Img_Count;
        public string Image;

        public string id_;
        public string name_;

        public string FilterArray { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static string Save_Priority(string PrddataValue)
    {
        int iReturn = -1;

        string div_code = HttpContext.Current.Session["div_code"].ToString();
        Product prd=new Product();
        DataTable dtValue = (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(PrddataValue, (typeof(DataTable)));
        DataSet ds = new DataSet();
        ds.Merge(dtValue);

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            iReturn = prd.Save_Slide(dr["Brand_Name"].ToString(), dr["Img_Name"].ToString(), dr["Priority"].ToString(), div_code);
        }
        
        return "0";
    }

}