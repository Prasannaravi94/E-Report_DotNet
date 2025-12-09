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

public partial class MasterFiles_Reports_SecSale_Stockist_Entry_Status : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        string sf_code = Session["sf_code"].ToString();
        string div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            // //// menu1.FindControl("btnBack").Visible = false;
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            //ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            //ddlFieldForce.Enabled = false;

        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
        }
        DateTime FromMonth = DateTime.Now;
        DateTime ToMonth = DateTime.Now;
        txtFromMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<Field_Detail> GetFieldForceName()
    {
        List<Field_Detail> objField = new List<Field_Detail>();
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
                    Field_Detail objFFDet = new Field_Detail();
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
    public static List<Year_Detail> FillYear()
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
}

public class Field_Detail
{
    public string Field_Sf_Code { get; set; }
    public string Field_Sf_Name { get; set; }
}
public class Year_Detail
{
    public string Y_Id { get; set; }
    public string Year { get; set; }
}