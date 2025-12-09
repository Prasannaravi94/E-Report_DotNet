using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
public partial class MasterFiles_Quesionaire_Questionnaire_View : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dt = new DataTable();
    string strQry = string.Empty;
    DataSet dsDoctor = new DataSet();
    DataSet dsDes = new DataSet();
    int search = 0;
    DataSet dsListedDR = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            FillSubdiv();
            FillProd();
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

                //FillColor();
                // FillMRManagers1();
                BindDate();
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;


                FillMRManagers();
                BindDate();
            }
        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
        }
        FillColor();

    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //dsSalesForce.Tables[0].Rows[1].Delete();
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

        FillColor();
    }
    private void FillSubdiv()
    {
        //List of Sub division are loaded into the checkbox list from Division Class
        DataSet dsSubDivision = new DataSet();
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSub.DataValueField = "subdivision_code";
            ddlSub.DataTextField = "subdivision_name";
            ddlSub.DataSource = dsSubDivision;
            ddlSub.DataBind();
        }
    }
    
    private void FillProd()
    {
        lstProd.Items.Clear();
        Product sf = new Product();
        DataSet dsProd = new DataSet();
       

            string sProcName = "", sTblName = "";

            sProcName = "Get_Product_Sub_wise";

            DataSet dsts = new DataSet();
            if (sProcName != "")
            {
                string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                SqlConnection con = new SqlConnection(strConn);
                con.Open();
                SqlCommand cmd = new SqlCommand(sProcName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Division_Code", div_code);

                cmd.Parameters.AddWithValue("@SuBCode", ddlSub.SelectedValue);

                cmd.CommandTimeout = 600;

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dsts);
                if (dsts.Tables[0].Rows.Count > 0)
                {

                    lstProd.DataTextField = "Product_Detail_Name";
                    lstProd.DataValueField = "Product_Code_SlNo";
                    lstProd.DataSource = dsts;
                    lstProd.DataBind();

                }
                else
                {
                    lstProd.Items.Clear();
                }
                con.Close();
            }
       

    }
    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
  

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());
                ddlToYear.Items.Add(k.ToString());
            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();
            ddlToYear.Text = DateTime.Now.Year.ToString();

            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }





    protected void ddlSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProd();
    }
}