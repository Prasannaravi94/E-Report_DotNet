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
using System.Text;
using Bus_EReport;
using System.Net;

public partial class Reports_App_version_View : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsState = null;
    DataSet dsSalesForce = null;
    int iActiveCount = 0;
    int IDActiveCount = 0;
    int iLstActiveCount = 0;
    int iLstDActiveCount = 0;
    int iUnLstActiveCount = 0;
    int iUnLstDActiveCount = 0;
    int iChemistActiveCount = 0;
    int iChemistDActiveCount = 0;
    int iSockistActiveCount = 0;
    int iSockistDActiveCount = 0;
    int ActTerrtotal = 0;
    string strDiv_Code = "";
    int DeActTerrtotal = 0;
    int ActLstDrtotal = 0;
    int DeActive_ListedDR = 0;
    int DeUnLstDrtotal = 0;
    int DeActUnLstDrtotal = 0;
    int ActChemtotal = 0;
    int DeActChemtotal = 0;
    int ActStocktotal = 0;
    int DeActStocktotal = 0;
    string sState = string.Empty;
    string strMultiDiv = string.Empty;
    string sfCode = string.Empty;
    string strStock_Sf_Code = string.Empty;
    string strSf_Code = string.Empty;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    DataSet dsRep = null;
    string[] statecd;
    string slno;
    string state_cd = string.Empty;
    string sf_type = string.Empty;
    bool isff = false;
    DataTable dtrowClr = new System.Data.DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        heading.InnerText = this.Page.Title;
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            Filldiv();


            Product prd = new Product();
            dsdiv = prd.getMultiDivsf_Name(sfCode);
            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                {
                    strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                    ddlDivision.SelectedValue = div_code;
                    ddlDivision.Visible = true;
                    lblDivision.Visible = true;
                    //getDivision();
                    Session["MultiDivision"] = ddlDivision.SelectedValue;
                }
                else
                {
                    Session["MultiDivision"] = "";
                    ddlDivision.Visible = false;
                    lblDivision.Visible = false;
                }
            }

            btnSubmit.Focus();
        }

        //FillColor();
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
            (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;
            ddlDivision.Visible = false;
            lblDivision.Visible = false;

            btnSubmit.Visible = false;
            btnSubmit_Click(sender, e);
            //lblFF.Visible = false;

        }
        else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;

            ddlDivision.Visible = true;
            lblDivision.Visible = true;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
                (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;

            //ddlDivision.Visible = true;
            //lblDivision.Visible = true;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = Page.Title;

            //ddlDivision.Visible = false;
            //lblDivision.Visible = false;

        }


    }
    private void Filldiv()
    {
        Division dv = new Division();
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
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Export.xls";

        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        GrdDoctor.Visible = false;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Division dv = new Division();
        //dsSalesForce = dv.get_App_version(ddlDivision.SelectedValue, sfCode);

        dsSalesForce = dv.get_App_versionNew_Add(ddlDivision.SelectedValue, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            dtrowClr = dsSalesForce.Tables[0].Copy();

            GrdDoctor.DataSource = dsSalesForce;
            GrdDoctor.DataBind();
        }
        else
        {
            GrdDoctor.DataSource = dsSalesForce;
            GrdDoctor.DataBind();
        }
    }

    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;

 
            e.Row.Cells[8].Style.Add("color", "Red");
            //e.Row.Cells[8].Style.Add("border-color", "black");
            e.Row.Cells[8].Style.Add("font-size", "10pt");
            e.Row.Cells[8].Attributes.Add("align", "center");
            e.Row.Cells[8].Font.Bold = true;

            try
            {
                //int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
        }
    }
}