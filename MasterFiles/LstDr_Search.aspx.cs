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


public partial class MasterFiles_LstDr_Search : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsUserList = null;
    DataSet dsDivision = null;
    DataSet dsSalesForce = null;
    DataSet dsAT = null;
    DataSet dsATM = null;

    string div_code = string.Empty;
    string ProdCode = string.Empty;
    string ProdSaleUnit = string.Empty;
    string ProdName = string.Empty;
    string sf_type = string.Empty;
    SalesForce sf = new SalesForce();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Product prd = new Product();
    DataSet dsdiv = new DataSet();
    string strMultiDiv = string.Empty;
    string sf_code = string.Empty;
    string bcolor = string.Empty;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        heading.InnerText = this.Page.Title;
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
            div_code = div_code.Substring(0,div_code.Length - 1);
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                //Filldiv();
                FillHQ();
                btnGo.Focus();
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                Product prd = new Product();
                DataSet dsdiv = new DataSet();
                dsdiv = prd.getMultiDivsf_Name(sf_code);
                if (dsdiv.Tables[0].Rows.Count > 0)
                {
                    if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                    {
                        strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);

                        btnGo.Visible = true;
                    }
                    else
                    {
                        btnGo.Visible = false;
                    }
                }
            }
        }

        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            c1.FindControl("btnBack").Visible = false;
            //grdSalesForce.Columns[8].Visible = false;

        }
        else if (Session["sf_type"].ToString() == "")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            //c1.FindControl("btnBack").Visible = false;
        }
        else if (Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
           (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = Page.Title;
            //c1.FindControl("btnBack").Visible = false;
        }
    }

    //private void Filldiv()
    //{
    //    Division dv = new Division();
    //    if (sf_type == "3")
    //    {
    //        string[] strDivSplit = div_code.Split(',');
    //        foreach (string strdiv in strDivSplit)
    //        {
    //            if (strdiv != "")
    //            {
    //                dsdiv = dv.getDivisionHO(strdiv);
    //                System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
    //                liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //                liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
    //                ddlDivision.Items.Add(liTerr);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        dsDivision = dv.getDivision_Name();
    //        if (dsDivision.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDivision.DataTextField = "Division_Name";
    //            ddlDivision.DataValueField = "Division_Code";
    //            ddlDivision.DataSource = dsDivision;
    //            ddlDivision.DataBind();
    //        }
    //    }
    //}



    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdSalesForce.Visible = false;
        FillHQ();
    }
 

    private void FillHQ()
    {
        ListedDR LstDoc = new ListedDR();
        DataSet dsHQ = new DataSet();
        if (sf_type == "3" || sf_type == "")
        {
                    dsdiv = LstDoc.getListedDr_Hq(div_code, "");
                    ddlHQ.DataTextField = "Sf_HQ";
                    ddlHQ.DataValueField = "sf_code";
                    ddlHQ.DataSource = dsdiv;
                    ddlHQ.DataBind();  
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

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
                        ListedDR LstDoc = new ListedDR();
                        DataSet dsDesignation = new DataSet();
                        dsDesignation = LstDoc.BindListedDr(div_code, txtNew.Text, ddlHQ.SelectedValue);
                        if (dsDesignation.Tables[0].Rows.Count > 0)
                        {
                            grdSalesForce.Visible = true;
                            grdSalesForce.DataSource = dsDesignation;
                            grdSalesForce.DataBind();
                        }
                        else
                        {
                            grdSalesForce.DataSource = dsDesignation;
                            grdSalesForce.DataBind();
                        }
    }

}