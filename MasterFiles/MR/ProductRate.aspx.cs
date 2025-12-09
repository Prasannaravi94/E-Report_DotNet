using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_ProductRate : System.Web.UI.Page
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
    string sf_type = string.Empty;
    DataSet dsSub = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["sf_code"] != null)
        {
            sf_code = Request.QueryString["sf_code"].ToString();
            div_code = Request.QueryString["divCode"].ToString();
            sf_type = Request.QueryString["sf_type"].ToString();
        }
        else
        {

            sf_code = Session["sf_code"].ToString();
            div_code = Session["div_code"].ToString();
            sf_type = Session["sf_type"].ToString();
        }
        if (!Page.IsPostBack)
        {
           
           // FillProd();
           // menu1.Title = this.Page.Title;
           // //// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            lblSelect.Visible = true;
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
            if (sf_type == "" || sf_type == "3")
            {
                pnlstate.Visible = true;
                FillState(div_code);
                // ddlState.SelectedIndex = 0;
            }
        }
        if (Request.QueryString["sf_code"] != null)//Dashboard
        {
            pnlstate.Visible = false;
            pnlprint.Visible = false;
            btnGo_Click(btnGo, null);
        }
        else
        {

            if (sf_type == "1")
            {

                UserControl_MR_Menu c1 =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                //// c1.FindControl("btnBack").Visible = false;
                //c1.Title = this.Page.Title;
                pnldivision.Visible = false;


            }
            else if (sf_type == "2")
            {
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                //// c1.FindControl("btnBack").Visible = false;
                //c1.Title = this.Page.Title;
                pnldivision.Visible = true;

            }
            else if (sf_type == "" || sf_type == "3")
            {
                if (Request.QueryString["sf_code"] != null)
                {
                    div_code = Request.QueryString["divCode"].ToString();
                }
                else
                {
                    div_code = Session["div_code"].ToString();
                }
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //// c1.FindControl("btnBack").Visible = false;
                pnldivision.Visible = false;
                pnlstate.Visible = true;
                grdProduct.Visible = false;
                pnlprint.Visible = false;
                //  FillState(div_code);
            }
        }
    }
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
            Label lblSample = (Label)e.Row.FindControl("lblSample");
            if (lblSample != null)
            {
                decimal samp_price = Convert.ToDecimal(lblSample.Text.ToString().Trim());
                lblSample.Text = String.Format("{0:F2}", samp_price);
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
        UnListedDR LstDR = new UnListedDR();
        if (sf_type == "" || sf_type == "3")
        {
            state_code = ddlState.SelectedValue.ToString();
        }
        else
        {
            dsState = LstDR.getState(sf_code);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                state_code = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
        SubDivision sb = new SubDivision();
        dsSub = sb.getSub_sf(sf_code);
        if (dsSub.Tables[0].Rows.Count > 0)
        {
            sub_code = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        Product dv = new Product();

        dsSF = dv.getProductRate_sf(sf_code, div_code, state_code, sub_code);

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
    protected void btnGo_Click(object sender, EventArgs e)
    {     

        if (sf_type == "1")
        {
            lblSelect.Visible = false;
            FillProd();
        }
        else if (sf_type == "2")
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

}