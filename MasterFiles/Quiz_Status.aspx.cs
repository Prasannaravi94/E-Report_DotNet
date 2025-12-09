using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Windows;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Net;
using System.Drawing.Imaging;

public partial class MIS_Reports_Quiz_Status : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string strMultiDiv = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        dheading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                FillMRManagers();
                FillColor();
                BindDate();

                Filldays();
                setValueToChkBoxList();
                DataSet dsdiv = new DataSet();
                Product prd = new Product();
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                        ddlDivision.Visible = true;
                        lblDivision.Visible = true;
                        getDivision();
                        Session["MultiDivision"] = ddlDivision.SelectedValue;
                    }
                    else
                    {
                        Session["MultiDivision"] = "";
                        ddlDivision.Visible = false;
                        lblDivision.Visible = false;
                        //FillProd();
                    }
                    FillMRManagers();
                }
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
                //c1.FindControl("btnBack").Visible = false;
                FillManagers();
                FillColor();
                BindDate();

                Filldays();
                setValueToChkBoxList();
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
                FillColor();
                BindDate();
                FillMRManagers();

                Filldays();
                setValueToChkBoxList();
                // lblFilter.Visible = false;
                // ddlFieldForce.Visible = false;
                // ddlFFType.Visible = false;
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
                //c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                 div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
                FillColor();
                BindDate();
            }
        }

        FillColor();

    }
    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }
    private void Filldays()
    {
        int to_days = DateTime.DaysInMonth(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue));


        for (int i = 1; i <= to_days; i++)
        {

            chkdate.Items.Add("   " + i.ToString());

        }


    }


    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdate.Items.Clear();
        ChkAll.Checked = false;
        Filldays();
        setValueToChkBoxList();
    }

    private void setValueToChkBoxList()
    {
        try
        {

            foreach (System.Web.UI.WebControls.ListItem item in chkdate.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }

        }
        catch (Exception)
        {
        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        double percent = 0.00;
        decimal Roundper = new decimal();
        double percent1 = 0.00;
        decimal Roundper1 = new decimal();
        decimal tot = new decimal();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //   Label lblBackColor = (Label)e.Row.FindControl("lblCorrect");
            //string bcolor = "#" + lblBackColor.Text;
            //e.Row.BackColor = System.Drawing.Color.FromName(bcolor);

         
           
                Label lblpercent = (Label)e.Row.FindControl("lblPercent");
                Label lblpercent2 = (Label)e.Row.FindControl("lblPercent2");
                Label lbltot = (Label)e.Row.FindControl("lbltot");
                Label lblQues = (Label)e.Row.FindControl("lblQus");
                LinkButton lnkAns = (LinkButton)e.Row.FindControl("lnkcount");
                LinkButton lnkAns1 = (LinkButton)e.Row.FindControl("lnkcount1");
              //  lblResult.Text = dsquiz.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //    = dsquiz.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() * 100;
                if (lblQues.Text != "0")
                {
                    percent = Convert.ToDouble((Convert.ToDecimal(lnkAns.Text) / Convert.ToDecimal(lblQues.Text))) * 100;
                    Roundper = Math.Round((decimal)percent);
                    lblpercent.Text = "&nbsp;" + Roundper;
                    percent1 = Convert.ToDouble((Convert.ToDecimal(lnkAns1.Text) / Convert.ToDecimal(lblQues.Text))) * 100;
                    Roundper1 = Math.Round((decimal)percent1);
                    lblpercent2.Text = "&nbsp;" + Roundper1;
                    tot = ((Roundper + Roundper1) / 2);
                    lbltot.Text = tot.ToString();
                    if (lbltot.Text.Trim() == "100")
                    {
                        e.Row.Attributes.Add("style", "background-color:Yellow;font-bold:true; font-size:16px; Color:Red; border-color:Black");
                    }
                }


        }

    }
    private void FillManagers()
    {
        if (Session["sf_type"].ToString() == "2")
        {
            if (Session["MultiDivision"] != "")
            {
                div_code = ddlDivision.SelectedValue.ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
        }
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserListTP_Alpha(div_code, "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(div_code, "admin");
        }

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
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }
    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserListTP_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
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

    }
    private void BindDate()
    {
        div_code = Session["div_code"].ToString();
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }
    }
    protected void ddldivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }

    private void FillMRManagers()
    {
        if (Session["sf_type"].ToString() == "2")
        {
            if (Session["MultiDivision"] != "")
            {
                div_code = ddlDivision.SelectedValue.ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
        }
        SalesForce sf = new SalesForce();
        ddlFFType.Visible = false;
        ddlAlpha.Visible = false;
        DataSet dsmgrsf = new DataSet();
        SalesForce ds = new SalesForce();

        // Check if the manager has a team
        DataSet DsAudit = ds.SF_Hierarchy(div_code, sf_code);
        if (DsAudit.Tables[0].Rows.Count > 0)
        {

            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {

            // Fetch Managers Audit Team
            DataTable dt = ds.getAuditManagerTeam(div_code, sf_code, 0);
            dsmgrsf.Tables.Add(dt);
            dsSalesForce = dsmgrsf;
        }


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
        FillColor();


    }

    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdSalesForce.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);


        }
       
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1")
        {
           // div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlFieldForce.SelectedValue.ToString();
        }
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        // dsSalesForce = sf.getSales(div_code, sReport, Image_Id);
        dsSalesForce = sf.Quiz_Status_Temp(div_code, sf_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        FillgridColor();
    }
  private void FillQuiz()
    {

        if (Session["sf_type"].ToString() == "1")
        {
            // div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }
        else
        {
            sf_code = ddlFieldForce.SelectedValue.ToString();
        }
        SalesForce sf = new SalesForce();
        // dsSalesForce = sf.getSalesForcelist_Reporting(div_code, sReport);
        // dsSalesForce = sf.getSales(div_code, sReport, Image_Id);
        dsSalesForce = sf.Quiz_Status_Temp(div_code, sf_code, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        FillgridColor();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        FillQuiz();
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
        grdSalesForce.Visible = false;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}