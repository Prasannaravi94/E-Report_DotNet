using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using System.Drawing;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class Delayed_Status_Multiple : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSf = null;
    DataSet dsTP = new DataSet();
    DataTable dtrowClr = new DataTable();
    DataTable dtrowdt = new DataTable();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    int mon;
    int Fmonth;
    int FYear;
    int Tmonth;
    int Tyear;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
         //   ddlDivision.SelectedValue = div_code;
            Session["div_code"] = div_code;
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
      
        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                Filldiv();
                FillMRManagers();
                TourPlan tp = new TourPlan();
                dsTP = tp.Get_TP_Edit_Year_New(div_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        ddlFrmYear.Items.Add(k.ToString());
                        ddlFrmYear.SelectedValue = DateTime.Now.Year.ToString();
                        ddlToYear.Items.Add(k.ToString());
                        ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                        ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
                        ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();

                    }
                }
            }
            else
            {
                lblDivision.Visible = false;
                ddlDivision.Visible = false;
                sf_code = Session["sf_code"].ToString();
                TourPlan tp = new TourPlan();
                dsTP = tp.Get_TP_Edit_Year_New(div_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        ddlFrmYear.Items.Add(k.ToString());
                        ddlFrmYear.SelectedValue = DateTime.Now.Year.ToString();
                        ddlToYear.Items.Add(k.ToString());
                        ddlToYear.SelectedValue = DateTime.Now.Year.ToString();
                        ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
                        ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
                    }
                }
               
                FillMRManagers();
            }
          
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
        {
            div_code = ddlDivision.SelectedValue;
            dsSalesForce = sf.UserListTP_Hierarchy_Sale(div_code, "admin");
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
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
        else if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();

                ddlSF.DataTextField = "Desig_Color";
                ddlSF.DataValueField = "sf_code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();
            }
            ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            ddlFieldForce.Enabled = false;
        }
        else
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
            dsSalesForce = sf.UserListTP_Hierarchy_Sale(div_code, sf_code);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
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
    private void Filldiv()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        //else
        //{
        //    if (strMultiDiv != "")
        //    {
        //        dsDivision = dv.getMultiDivision(strMultiDiv);
        //    }
        //    else
        //    {
        //        dsDivision = dv.getDivision_Name();
        //    }
        //    if (dsDivision.Tables[0].Rows.Count > 0)
        //    {
        //        ddlDivision.DataTextField = "Division_Name";
        //        ddlDivision.DataValueField = "Division_Code";
        //        ddlDivision.DataSource = dsDivision;
        //        ddlDivision.DataBind();
        //    }
        //}

    }


    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
}