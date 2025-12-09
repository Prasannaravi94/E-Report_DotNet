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

public partial class MIS_Reports_Sale_Analysis : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsTP = null;
    DateTime ServerStartTime;
    DataSet dsSecSales = null;
    DataSet dsSale = null;
    DataSet dsState = new DataSet();
    DataSet dsReport = null;
    string state_code = string.Empty;
   
    int FMonth = -1;
    int FYear = -1;
    int TMonth = -1;
    int TYear = -1;
    int stock_code = -1;
    int iDay = -1;
    DateTime SelDate;
    string sDate = string.Empty;
    string sf_name = string.Empty;
    int rpttype = -1;
    DataSet dssf = null;
    DataSet dsStock = null;
    string strFieledForceName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
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
          //  menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
           // //// menu1.FindControl("btnBack").Visible = false;
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                FillYear();
                FillMRManagers();
                FillColor();
               
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                //div_code = Session["div_code"].ToString();
                //if (div_code.Contains(','))
                //{
                //    div_code = div_code.Remove(div_code.Length - 1);
                //}
                div_code = Session["div_code"].ToString();
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
                FillManagers();
                FillColor();
                FillYear();
              
               // strFieledForceName = Request.QueryString["sf_name"].ToString();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                // c1.FindControl("btnBack").Visible = false;
                c1.Title = this.Page.Title;
                FillYear();
                FillMRManagers();
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
                // c1.FindControl("btnBack").Visible = false;

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
                // c1.FindControl("btnBack").Visible = false;

            }
        }
   
        FillColor();

    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }

        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
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
     private void FillManagers()
     {
         SalesForce sf = new SalesForce();

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
     }
     private void FillMRManagers()
     {
         SalesForce sf = new SalesForce();
      
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
     protected void btnGo_Click(object sender, EventArgs e)
     {
         GenerateReport();
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
     

        
     }

     private string getMonthName(int iMonth)
     {
         string sReturn = string.Empty;

         if (iMonth == 1)
         {
             sReturn = "January";
         }
         else if (iMonth == 2)
         {
             sReturn = "February";
         }
         else if (iMonth == 3)
         {
             sReturn = "March";
         }
         else if (iMonth == 4)
         {
             sReturn = "April";
         }
         else if (iMonth == 5)
         {
             sReturn = "May";
         }
         else if (iMonth == 6)
         {
             sReturn = "June";
         }
         else if (iMonth == 7)
         {
             sReturn = "July";
         }
         else if (iMonth == 8)
         {
             sReturn = "August";
         }
         else if (iMonth == 9)
         {
             sReturn = "September";
         }
         else if (iMonth == 10)
         {
             sReturn = "October";
         }
         else if (iMonth == 11)
         {
             sReturn = "November";
         }
         else if (iMonth == 12)
         {
             sReturn = "December";
         }
         return sReturn;
     }


     private void GenerateReport()
     {

         FMonth = Convert.ToInt32(ddlFMonth.SelectedValue);
         FYear = Convert.ToInt32(ddlFYear.SelectedValue);
         TMonth = Convert.ToInt32(ddlTMonth.SelectedValue);
         TYear = Convert.ToInt32(ddlTYear.SelectedValue);
         SalesForce sf = new SalesForce();
         dssf = sf.getSfName(ddlFieldForce.SelectedValue);
         if (dssf.Tables[0].Rows.Count > 0)
             sf_name = Convert.ToString(dssf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
           

         lblText.Text = lblText.Text + " between " + getMonthName(FMonth) + " - " + FYear + " and " + getMonthName(TMonth) + " - " + TYear;

         lblsf.Text = sf_name + " - " + dssf.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() + " - " +  dssf.Tables[0].Rows[0].ItemArray.GetValue(3).ToString()  ;
         tbl.Rows.Clear();

         //Get the state for the MR
         UnListedDR LstDR = new UnListedDR();
         SalesForce sfmr = new SalesForce();
         AdminSetup adm = new AdminSetup();
         if (ddlFieldForce.SelectedValue.Contains("MR"))
         {
             dssf = sfmr.getSfName_sal(ddlFieldForce.SelectedValue,div_code);
         }
         else
         {
             dssf = adm.getMR_Vacant(ddlFieldForce.SelectedValue, div_code);
         }

         //Get the last date of the selected month for the year
         if ((FMonth > 0) && (FYear > 0))
             iDay = GetLastDay(FMonth, FYear);

         sDate = iDay.ToString().Trim() + "-" + FMonth.ToString().Trim() + "-" + FYear.ToString().Trim();
         SelDate = Convert.ToDateTime(sDate);

         //Get Product master data from DB and bind it with Product Repeater


         if (dssf.Tables[0].Rows.Count > 0)
         {

             tbl.BorderStyle = BorderStyle.None;

             //Stockist stk = new Stockist();

             //PopulateSecSales_Month(dsProd, stock_code);
             //    PopulateSecSales_Month(dsProd, Convert.ToInt16(dstRow["Stockist_Code"].ToString()));
             int monthdiff = -1;
             int curmonth = -1;
             int curyear = -1;
             int curindex = -1;

             //  SecSale ss = new SecSale();

             TableRow tr_header = new TableRow();
             tr_header.BorderStyle = BorderStyle.Solid;
             tr_header.BorderWidth = 1;
             tr_header.Attributes.Add("Class", "Backcolor");

             TableCell tc_SNo = new TableCell();
             tc_SNo.BorderStyle = BorderStyle.Solid;
             tc_SNo.BorderWidth = 1;
             tc_SNo.Width = 50;
             //  tc_SNo.RowSpan = 3;
             Literal lit_SNo = new Literal();
             lit_SNo.Text = "#";
             tc_SNo.HorizontalAlign = HorizontalAlign.Center;
             tc_SNo.Attributes.Add("Class", "Backcolor");
             tc_SNo.Controls.Add(lit_SNo);
             tr_header.Cells.Add(tc_SNo);

             TableCell tc_sfname = new TableCell();
             tc_sfname.BorderStyle = BorderStyle.Solid;
             tc_sfname.BorderWidth = 1;
             tc_sfname.Width = 200;
             // tc_sfname.RowSpan = 3;
             Literal lit_sfname = new Literal();
             lit_sfname.Text = "<center>Fieldforce Name</center>";
             tc_sfname.Attributes.Add("Class", "Backcolor");
             tc_sfname.Controls.Add(lit_sfname);
             tr_header.Cells.Add(tc_sfname);

             TableCell tc_hq = new TableCell();
             tc_hq.BorderStyle = BorderStyle.Solid;
             tc_hq.BorderWidth = 1;
             tc_hq.Width = 150;
             // tc_sfname.RowSpan = 3;
             Literal lit_hq = new Literal();
             lit_hq.Text = "<center>HQ</center>";
             tc_hq.Attributes.Add("Class", "Backcolor");
             tc_hq.Controls.Add(lit_hq);
             tr_header.Cells.Add(tc_hq);

             TableCell tc_mgr = new TableCell();
             tc_mgr.BorderStyle = BorderStyle.Solid;
             tc_mgr.BorderWidth = 1;
             tc_mgr.Width = 200;
             // tc_sfname.RowSpan = 3;
             Literal lit_mgr = new Literal();
             lit_mgr.Text = "<center>Manager</center>";
             tc_mgr.Attributes.Add("Class", "Backcolor");
             tc_mgr.Controls.Add(lit_mgr);
             tr_header.Cells.Add(tc_mgr);


             TableRow tr_stock_header = new TableRow();
             tr_stock_header.BorderStyle = BorderStyle.Solid;
             //tr_stock_header.BorderWidth = 1;
             tr_stock_header.Attributes.Add("Class", "Backcolor");
             TableCell tc_stock_name = new TableCell();
             tc_stock_name.BorderStyle = BorderStyle.Solid;
             tc_stock_name.BorderWidth = 1;
             tc_stock_name.Width = 150;
             //    tc_stock_name.RowSpan = 3;
             Literal lit_stock_name = new Literal();
             lit_stock_name.Text = "<center> Stockiest Name</center>";
             tc_stock_name.Attributes.Add("Class", "Backcolor");
             tc_stock_name.Controls.Add(lit_stock_name);
             tr_header.Cells.Add(tc_stock_name);

             TableCell tc_Prod_Code = new TableCell();
             tc_Prod_Code.BorderStyle = BorderStyle.Solid;
             tc_Prod_Code.BorderWidth = 1;
             tc_Prod_Code.Width = 100;
             //  tc_Prod_Code.RowSpan = 3;
             Literal lit_Prod_Code = new Literal();
             lit_Prod_Code.Text = "<center>Prod Code</center>";
             tc_Prod_Code.Attributes.Add("Class", "Backcolor");
             tc_Prod_Code.Controls.Add(lit_Prod_Code);
             tc_Prod_Code.Visible = false;
             tr_header.Cells.Add(tc_Prod_Code);

             TableCell tc_Prod_Name = new TableCell();
             tc_Prod_Name.BorderStyle = BorderStyle.Solid;
             tc_Prod_Name.BorderWidth = 1;
             tc_Prod_Name.Width = 200;
             //   tc_Prod_Name.RowSpan = 3;
             Literal lit_Prod_Name = new Literal();
             lit_Prod_Name.Text = "<center>Product Name</center>";
             tc_Prod_Name.Attributes.Add("Class", "Backcolor");
             tc_Prod_Name.Controls.Add(lit_Prod_Name);
             tr_header.Cells.Add(tc_Prod_Name);

             TableCell tc_Rate = new TableCell();
             tc_Rate.BorderStyle = BorderStyle.Solid;
             tc_Rate.BorderWidth = 1;
             tc_Rate.Width = 70;
             //  tc_Rate.RowSpan = 3;
             Literal lit_Rate = new Literal();
             lit_Rate.Text = "<center>Rate</center>";
             tc_Rate.Attributes.Add("Class", "Backcolor");
             tc_Rate.Controls.Add(lit_Rate);
             tr_header.Cells.Add(tc_Rate);

             if (FYear == TYear)
             {
                 if (TMonth >= FMonth)
                 {
                     monthdiff = (TMonth - FMonth) + 1;
                 }
             }
             else
             {
                 monthdiff = (12 - FMonth) + 1;
                 monthdiff = monthdiff + (TMonth - 0);
             }
             SecSale ss = new SecSale();
             dsSecSales = ss.getrptfield(div_code);
             TableRow tr_sub_header = new TableRow();

             //Month column
             TableRow tr_mth_header = new TableRow();

             curmonth = FMonth;
             curyear = FYear;

             for (curindex = 0; curindex < monthdiff; curindex++)
             {
                      

                 if (dsSecSales.Tables[0].Rows.Count > 0)
                 {
                     foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                     {
                         TableCell tc_sec_name = new TableCell();
                         tc_sec_name.BorderStyle = BorderStyle.Solid;
                         tc_sec_name.BorderWidth = 1;
                         tc_sec_name.Width = 100;
                         //  tc_sec_name.ColumnSpan = 2;
                         Literal lit_sec_name = new Literal();
                         lit_sec_name.Text = "<center> " + getMonthName(curmonth) + " " + dRow["Sec_Sale_Name"].ToString() + " Qty</center>";
                         tc_sec_name.Attributes.Add("Class", "Backcolor");
                         tc_sec_name.Controls.Add(lit_sec_name);
                         //tr_header.Cells.Add(tc_sec_name);
                         tr_header.Cells.Add(tc_sec_name);

                         TableCell tc_qty = new TableCell();
                         tc_qty.BorderStyle = BorderStyle.Solid;
                         tc_qty.BorderWidth = 1;
                         tc_qty.Width = 50;
                         Literal lit_qty = new Literal();
                         lit_qty.Text = "<center> " + getMonthName(curmonth) + " " + dRow["Sec_Sale_Name"].ToString() + "  Value</center>";
                         tc_qty.Attributes.Add("Class", "Backcolor");
                         tc_qty.Controls.Add(lit_qty);
                         tr_header.Cells.Add(tc_qty);
                     }


                 
                 } // End of Sec Sales If condn
                 curmonth += 1;

                 //If To Year > From Year
                 if (curmonth == 13)
                 {
                     curmonth = 1;
                     curyear += 1;
                 }
             }

             tbl.Rows.Add(tr_header);
             //tbl.Rows.Add(tr_mth_header);
             //tbl.Rows.Add(tr_sub_header);

             int iCount = 0;
             int sqty = 0;
             double sval = 0.00;
             string[] arrqty = new string[50];
             string[] arrval = new string[50];
           //  SalesForce sf = new SalesForce();



             if (dssf.Tables[0].Rows.Count > 0)
             {

                 foreach (DataRow dsf in dssf.Tables[0].Rows)
                 {
                     dsState = LstDR.getState(dsf["sf_code"].ToString());
                     if (dsState.Tables[0].Rows.Count > 0)
                         state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                     SecSale ss1 = new SecSale();
                     DataSet dsProd = ss1.getProduct(div_code, state_code, SelDate);
                     dsStock = ss1.getStockiestDet(dsf["sf_code"].ToString(),div_code);
                     foreach (DataRow dstRow in dsStock.Tables[0].Rows)
                     {
                         foreach (DataRow dataRow in dsProd.Tables[0].Rows)
                         {
                             TableRow tr_det = new TableRow();
                             tr_det.BackColor = System.Drawing.Color.White;
                             tr_det.Attributes.Add("Class", "rptCellBorder");
                             iCount += 1;

                             TableCell tc_det_SNo = new TableCell();
                             Literal lit_det_SNo = new Literal();
                             lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                             tc_det_SNo.BorderStyle = BorderStyle.Solid;
                             tc_det_SNo.BorderWidth = 1;
                             tc_det_SNo.Width = 50;
                             tc_det_SNo.Controls.Add(lit_det_SNo);
                             tr_det.Cells.Add(tc_det_SNo);

                             TableCell tc_det_sf_name = new TableCell();
                             Literal lit_det_sf_name = new Literal();
                             lit_det_sf_name.Text = "" + dsf["sf_name"].ToString();
                             tc_det_sf_name.BorderStyle = BorderStyle.Solid;
                             tc_det_sf_name.BorderWidth = 1;
                             tc_det_sf_name.Controls.Add(lit_det_sf_name);
                             tr_det.Cells.Add(tc_det_sf_name);

                             TableCell tc_det_sf_hq = new TableCell();
                             Literal lit_det_sf_hq = new Literal();
                             lit_det_sf_hq.Text = "" + dsf["Sf_HQ"].ToString();
                             tc_det_sf_hq.BorderStyle = BorderStyle.Solid;
                             tc_det_sf_hq.BorderWidth = 1;
                             tc_det_sf_hq.Controls.Add(lit_det_sf_hq);
                             tr_det.Cells.Add(tc_det_sf_hq);

                             TableCell tc_det_mgr = new TableCell();
                             Literal lit_det_mgr = new Literal();
                             lit_det_mgr.Text = "" + dsf["Reporting_To"].ToString();
                             tc_det_mgr.BorderStyle = BorderStyle.Solid;
                             tc_det_mgr.BorderWidth = 1;
                             tc_det_mgr.Controls.Add(lit_det_mgr);
                             tr_det.Cells.Add(tc_det_mgr);

                             TableCell tc_det_st_name = new TableCell();
                             Literal lit_det_st_name = new Literal();
                             lit_det_st_name.Text = "" + dstRow["Stockist_Name"].ToString();
                             tc_det_st_name.BorderStyle = BorderStyle.Solid;
                             tc_det_st_name.BorderWidth = 1;
                             tc_det_st_name.Controls.Add(lit_det_st_name);
                             tr_det.Cells.Add(tc_det_st_name);

                             TableCell tc_det_prod_code = new TableCell();
                             Literal lit_det_prod_code = new Literal();
                             lit_det_prod_code.Text = "" + dataRow["Product_Detail_Code"].ToString();
                             tc_det_prod_code.BorderStyle = BorderStyle.Solid;
                             tc_det_prod_code.BorderWidth = 1;
                             tc_det_prod_code.Controls.Add(lit_det_prod_code);
                             tc_det_prod_code.Visible = false;
                             tr_det.Cells.Add(tc_det_prod_code);

                             TableCell tc_det_prod_name = new TableCell();
                             Literal lit_det_prod_name = new Literal();
                             lit_det_prod_name.Text = "" + dataRow["Product_Detail_Name"].ToString();
                             tc_det_prod_name.BorderStyle = BorderStyle.Solid;
                             tc_det_prod_name.BorderWidth = 1;
                             tc_det_prod_name.Controls.Add(lit_det_prod_name);
                             tr_det.Cells.Add(tc_det_prod_name);

                             TableCell tc_det_prod_rate = new TableCell();
                             Literal lit_det_prod_rate = new Literal();
                             lit_det_prod_rate.Text = "" + dataRow["Distributor_Price"].ToString();
                             tc_det_prod_rate.BorderStyle = BorderStyle.Solid;
                             tc_det_prod_rate.BorderWidth = 1;
                             tc_det_prod_rate.Controls.Add(lit_det_prod_rate);
                             tr_det.Cells.Add(tc_det_prod_rate);

                             sqty = 0;

                             curmonth = FMonth;
                             curyear = FYear;

                             for (curindex = 0; curindex < monthdiff; curindex++)
                             {

                                 //Actual secondary sales values
                                 foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                                 {
                                     //dsReport = ss.getrptvalues(div_code, sf_code, stock_code, FMonth, FYear, TMonth, TYear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                                     dsReport = ss.getrptvalues(div_code, dsf["sf_code"].ToString(), Convert.ToInt32(dstRow["Stockist_Code"].ToString()), curmonth, curyear, curmonth, curyear, dataRow["Product_Detail_Code"].ToString(), Convert.ToDouble(dataRow["Distributor_Price"].ToString()), Convert.ToInt16(dRow["Sec_Sale_Code"].ToString()));
                                     TableCell tc_det_qty = new TableCell();
                                     tc_det_qty.BorderStyle = BorderStyle.Solid;
                                     tc_det_qty.BorderWidth = 1;
                                     tc_det_qty.HorizontalAlign = HorizontalAlign.Right;
                                     tc_det_qty.Width = 50;
                                     Literal lit_det_qty = new Literal();
                                     if (dsReport != null)
                                     {
                                         //lit_det_qty.Text = "<center>" + dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "</center>";
                                         lit_det_qty.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "";
                                         arrqty[sqty] = lit_det_qty.Text;
                                     }
                                     else
                                     {
                                         lit_det_qty.Text = "<center>  </center>";
                                     }
                                     tc_det_qty.Attributes.Add("Class", "Backcolor");
                                     tc_det_qty.Controls.Add(lit_det_qty);
                                     tr_det.Cells.Add(tc_det_qty);

                                     TableCell tc_det_val = new TableCell();
                                     tc_det_val.BorderStyle = BorderStyle.Solid;
                                     tc_det_val.BorderWidth = 1;
                                     tc_det_val.HorizontalAlign = HorizontalAlign.Right;
                                     tc_det_val.Width = 50;
                                     Literal lit_det_val = new Literal();
                                     if (dsReport != null)
                                     {
                                         //lit_det_val.Text = "<center>" + dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</center>";
                                         lit_det_val.Text = dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "";
                                         if (lit_det_val.Text.Trim().Length > 0)
                                         {
                                             if (arrval[sqty] != null)
                                             {
                                                 arrval[sqty] = Convert.ToString(Convert.ToDouble(arrval[sqty]) + Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                                             }
                                             else
                                             {
                                                 arrval[sqty] = Convert.ToString(Convert.ToDouble(dsReport.Tables[0].Rows[0].ItemArray.GetValue(1).ToString()));
                                             }
                                         }
                                     }
                                     else
                                     {
                                         lit_det_val.Text = "<center> </center>";
                                     }
                                     tc_det_val.Attributes.Add("Class", "Backcolor");
                                     tc_det_val.Controls.Add(lit_det_val);
                                     tr_det.Cells.Add(tc_det_val);

                                     sqty += 1;
                                 }


                                 tbl.Rows.Add(tr_det);
                                 curmonth += 1;
                                 //If To Year > From Year
                                 if (curmonth == 13)
                                 {
                                     curmonth = 1;
                                     curyear += 1;
                                 }

                             }
                         }

                     }
                     //}
                 }
                 //rptProduct.DataSource = dsProd;
                 //rptProduct.DataBind();

                 TableRow tr_tot = new TableRow();
                 tr_tot.BackColor = System.Drawing.Color.White;
                 tr_tot.Attributes.Add("Class", "rptCellBorder");

                 TableCell tc_tot = new TableCell();
                 Literal lit_tot = new Literal();
                 lit_tot.Text = "Total";
                 tc_tot.BorderStyle = BorderStyle.Solid;
                 tc_tot.ColumnSpan = 7;
                 tc_tot.BorderWidth = 1;
                 tc_tot.HorizontalAlign = HorizontalAlign.Center;
                 tc_tot.Width = 50;
                 tc_tot.Controls.Add(lit_tot);
                 tr_tot.Cells.Add(tc_tot);

                 sqty = 0;

                 //Actual secondary sales values
                 sqty = 0;
                 curmonth = FMonth;
                 curyear = FYear;

                 for (curindex = 0; curindex < monthdiff; curindex++)
                 {

                     foreach (DataRow dRow in dsSecSales.Tables[0].Rows)
                     {
                         TableCell tc_det_tot = new TableCell();
                         tc_det_tot.BorderStyle = BorderStyle.Solid;
                         tc_det_tot.BorderWidth = 1;
                         Literal lit_det_tot = new Literal();
                         lit_det_tot.Text = "<center>  </center>";
                         tc_det_tot.Attributes.Add("Class", "Backcolor");
                         tc_det_tot.Controls.Add(lit_det_tot);
                         tr_tot.Cells.Add(tc_det_tot);

                         TableCell tc_det_val_tot = new TableCell();
                         tc_det_val_tot.BorderStyle = BorderStyle.Solid;
                         tc_det_val_tot.BorderWidth = 1;
                         tc_det_val_tot.Width = 50;
                         tc_det_val_tot.HorizontalAlign = HorizontalAlign.Right;
                         Literal lit_det_val_tot = new Literal();
                         if (arrval[sqty].Length > 0)
                         {
                             //lit_det_val_tot.Text = "<center>" + arrval[sqty].ToString() + "</center>";
                             lit_det_val_tot.Text = arrval[sqty].ToString() + "";
                         }
                         else
                         {
                             lit_det_val_tot.Text = "<center>  </center>";
                         }
                         tc_det_val_tot.Attributes.Add("Class", "Backcolor");
                         tc_det_val_tot.Controls.Add(lit_det_val_tot);
                         tr_tot.Cells.Add(tc_det_val_tot);

                         sqty += 1;
                     }

                     tbl.Rows.Add(tr_tot);
                     curmonth += 1;
                     //If To Year > From Year
                     if (curmonth == 13)
                     {
                         curmonth = 1;
                         curyear += 1;
                     }

                 }


                 //}


             }

         }
     }


     //Get the last day for the given month & year
     private int GetLastDay(int cMonth, int cYear)
     {
         int cday = 0;

         if (cMonth == 1)
             cday = 31;
         else if (cMonth == 2)
         {
             if (cYear % 4 == 0)
                 cday = 29;
             else
                 cday = 28;
         }
         else if (cMonth == 3)
             cday = 31;
         else if (cMonth == 4)
             cday = 30;
         else if (cMonth == 5)
             cday = 31;
         else if (cMonth == 6)
             cday = 30;
         else if (cMonth == 7)
             cday = 31;
         else if (cMonth == 8)
             cday = 31;
         else if (cMonth == 9)
             cday = 30;
         else if (cMonth == 10)
             cday = 31;
         else if (cMonth == 11)
             cday = 30;
         else if (cMonth == 12)
             cday = 31;

         return cday;
     }


}