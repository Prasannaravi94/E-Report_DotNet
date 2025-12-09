using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using System.Net;
using System.Web.UI.DataVisualization.Charting;
public partial class MasterFiles_MR_ProductRate_Mnthwise : System.Web.UI.Page
{
    DataSet dsSF = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strMultiDiv = string.Empty;
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    DataSet dsState = new DataSet();
    string state_code = string.Empty;
    string sub_code = string.Empty;
    string sState = string.Empty;
    DataSet dsDivision = null;
    string[] statecd;
    string state_cd = string.Empty;
    DataSet dsSub = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + Session["sf_code"].ToString() + "');", true);
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        hHeading.InnerText = this.Page.Title;
        if (!Page.IsPostBack)
        {

            // FillProd();
            // menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            lblSelect.Visible = true;
            if (Session["sf_type"].ToString() == "2")
            {
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                        ddlDivision.Visible = true;
                        lblDivision.Visible = true;
                        getDivision();
                    }
                    else
                    {
                        btnGo.Visible = false;
                        lblSelect.Visible = false;
                        ddlDivision.Visible = false;
                        lblDivision.Visible = false;
                        FillProd();
                    }
                }

            }
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                pnlstate.Visible = true;
                FillState(div_code);
                DateTime FromMonth = DateTime.Now;
                txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
                //BindDate();
                // ddlState.SelectedIndex = 0;
            }
        }
        if (Session["sf_type"].ToString() == "1")
        {

            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            pnldivision.Visible = false;

            FillProd();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            pnldivision.Visible = true;
            //  FillProd();

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
            //c1.Title = Page.Title;
            //c1.FindControl("btnBack").Visible = false;
            pnldivision.Visible = false;
            pnlstate.Visible = true;
            //grdProduct.Visible = false;
            pnlprint.Visible = false;

            //  FillState(div_code);
        }

    }
    //private void BindDate()
    //{
    //    TourPlan tp = new TourPlan();
    //    DataSet dsTP = new DataSet();

    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlYear.Items.Add(k.ToString());
    //        }

    //        ddlYear.Text = DateTime.Now.Year.ToString();
    //        SalesForce sf = new SalesForce();
    //        //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
    //        //  ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //        if (ddlYear.SelectedItem.Text == DateTime.Now.Year.ToString())
    //        {
    //            for (int i = 1; i <= 12; i++)
    //            {

    //                //ddlMonth.Items.Add(i.ToString());
    //               // ddlMonth.Items.Add(sf.getMonthName(i.ToString()));
    //                ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(getMonthName(i.ToString()), i.ToString()));
    //            }
    //            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //        }
    //        else
    //        {
    //            for (int i = 1; i <= 12; i++)
    //            {

    //                //ddlMonth.Items.Add(i.ToString());
    //               // ddlMonth.Items.Add(sf.getMonthName(i.ToString()));
    //                ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(getMonthName(i.ToString()), i.ToString()));

    //            }
    //            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //        }

    //        //     ddlMonth.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;

    //    }
    //}
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
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
            dsState = st.getState_new(state_cd);
            ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsState;
            ddlState.DataBind();
        }
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

    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsam1 = (Label)e.Row.FindControl("lblsam1");
            if (lblsam1 != null)
            {
                decimal mrp_price = Convert.ToDecimal(lblsam1.Text.ToString().Trim());
                lblsam1.Text = String.Format("{0:F2}", mrp_price);
            }

            Label lblsam2 = (Label)e.Row.FindControl("lblsam2");
            if (lblsam2 != null)
            {
                decimal ret_price = Convert.ToDecimal(lblsam2.Text.ToString().Trim());
                lblsam2.Text = String.Format("{0:F2}", ret_price);
            }

            Label lblsam3 = (Label)e.Row.FindControl("lblsam3");
            if (lblsam3 != null)
            {
                decimal dist_price = Convert.ToDecimal(lblsam3.Text.ToString().Trim());
                lblsam3.Text = String.Format("{0:F2}", dist_price);
            }

            Label lblsam4 = (Label)e.Row.FindControl("lblsam4");
            if (lblsam4 != null)
            {
                decimal targ_price = Convert.ToDecimal(lblsam4.Text.ToString().Trim());
                lblsam4.Text = String.Format("{0:F2}", targ_price);
            }

        }
    }

    private void FillProd()
    {

        dsdiv = prd.getMultiDivsf_Name(sf_code);
        if (dsdiv.Tables[0].Rows.Count > 0)
        {
            if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
            {
                div_code = ddlDivision.SelectedValue;
            }

        }
        //UnListedDR LstDR = new UnListedDR();
        //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{
        //    state_code = ddlState.SelectedValue.ToString();
        //}
        //else
        //{
        //    dsState = LstDR.getState(sf_code);
        //    if (dsState.Tables[0].Rows.Count > 0)
        //    {
        //        state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    }
        //}
        //SubDivision sb = new SubDivision();
        //dsSub = sb.getSub_sf(sf_code);
        //if (dsSub.Tables[0].Rows.Count > 0)
        //{
        //    sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //}
        Product dv = new Product();
        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);
        //dsSF = dv.GetProdRate_mntwise(div_code, ddlState.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        dsSF = dv.GetProdRate_mntwise(div_code, ddlState.SelectedValue, MonthVal.ToString(), YearVal.ToString());
        if (dsSF.Tables[0].Rows.Count > 0)
        {
            grdProduct.Visible = true;
            grdProduct.DataSource = dsSF;
            grdProduct.DataBind();
        }
        else
        {
            grdProduct.DataSource = dsSF;
            grdProduct.DataBind();
        }
    }

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }


    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Product dv = new Product();
        dtGrid = dv.getProductRate_DataTable(sf_code, div_code);
        return dtGrid;
    }

    protected void grdProduct_Sorting(object sender, GridViewSortEventArgs e)
    {

        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        DataView sortedView = new DataView(BindGridView());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdProduct.DataSource = sortedView;
        grdProduct.DataBind();
    }

    protected void grdProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdProduct.PageIndex = e.NewPageIndex;
        FillProd();
    }
    public string getMonthName(string sMonth)
    {
        string sReturn = string.Empty;

        if (sMonth == "1")
        {
            sReturn = "Jan";
        }
        else if (sMonth == "2")
        {
            sReturn = "Feb";
        }
       
        else if (sMonth == "3")
        {
            sReturn = "Mar";
        }
        else if (sMonth == "4")
        {
            sReturn = "Apr";
        }
        else if (sMonth == "5")
        {
            sReturn = "May";
        }
        else if (sMonth == "6")
        {
            sReturn = "Jun";
        }
        else if (sMonth == "7")
        {
            sReturn = "Jul";
        }
        else if (sMonth == "8")
        {
            sReturn = "Aug";
        }
        else if (sMonth == "9")
        {
            sReturn = "Sep";
        }
        else if (sMonth == "10")
        {
            sReturn = "Oct";
        }
        else if (sMonth == "11")
        {
            sReturn = "Nov";
        }
        else if (sMonth == "12")
        {
            sReturn = "Dec";
        }

        return sReturn;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {

        if (Session["sf_type"].ToString() == "1")
        {
            lblSelect.Visible = false;
            FillProd();
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            lblSelect.Visible = false;
            FillProd();
        }
    }
    protected void btnstate_Click(object sender, EventArgs e)
    {
        grdProduct.Visible = true;
        pnlprint.Visible = true;
        FillProd();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=ProductRate.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        grdProduct.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(grdProduct);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }


    //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ddlMonth.Items.Clear();
    //    SalesForce sf = new SalesForce();
    //    if (ddlYear.SelectedItem.Text == DateTime.Now.Year.ToString())
    //    {
    //        for (int i = 1; i < Convert.ToInt32(DateTime.Now.Month.ToString()); i++)
    //        {

    //            //ddlMonth.Items.Add(i.ToString());
    //            ddlMonth.Items.Add(sf.getMonthName(i.ToString()));

    //        }
    //    }
    //    else if (Convert.ToInt32(ddlYear.SelectedItem.Text) < Convert.ToInt32(DateTime.Now.Year.ToString()))
    //    {
    //        for (int i = 1; i <= 12; i++)
    //        {

    //            //ddlMonth.Items.Add(i.ToString());
    //           // ddlMonth.Items.Add(sf.getMonthName(i.ToString()));
    //            ddlMonth.Items.Add(new System.Web.UI.WebControls.ListItem(sf.getMonthName(i.ToString()), i.ToString()));
                 




    //        }
    //    }
    //    else
    //    {
    //        ddlMonth.Items.Clear();
    //    }

    //}
}