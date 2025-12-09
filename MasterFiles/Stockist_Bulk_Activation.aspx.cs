using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using Bus_EReport;


public partial class MasterFiles_Stockist_Bulk_Activation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsTerritory = null;
    string divcode = string.Empty;
    string stockist_code = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobileno = string.Empty;
    string Territory = string.Empty;
    string State = string.Empty;
    string sf_code = string.Empty;
    string sCmd = string.Empty;
    string sChkSalesforce = string.Empty;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string S_Name = string.Empty;

    string val = string.Empty;

    int time;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        if (!Page.IsPostBack)
        {          
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;          
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            grdAct_Stockist.Visible = false;

            Fill_ReActSam();
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


    private void Fill_ReActSam()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.GetStockist_Sample();
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            gv_Activate.Visible = true;
            gv_Activate.DataSource = dsStockist;
            gv_Activate.DataBind();
        }
        else
        {
            gv_Activate.DataSource = dsStockist;
            gv_Activate.DataBind();
        }
    }

    private void Fill_Activate_Stockist()
    {
        gv_Activate.Visible = false;

        Stockist sk = new Stockist();
        dsStockist = sk.Get_Active_Stockist(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdAct_Stockist.Visible = true;
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }
        else
        {
         
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }
    }

    protected void grdAct_Stockist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Activate")
        {
            stockist_code = Convert.ToString(e.CommandArgument);

            //Deactivate the Stockist Details
            Stockist dv = new Stockist();
            int iReturn = dv.Activate_Stockist(stockist_code);
            if (iReturn > 0)
            {
                //  menu1.Status = "Stockist has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }

           // Fill_Activate_Stockist();

            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
    }

    protected void grdAct_Stockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdAct_Stockist.PageIndex = e.NewPageIndex;
        FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
    }

    // Search By Text
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        if (ddlFields.SelectedValue != "")
        {
            gv_Activate.Visible = false;
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

       // btnSubmit.Visible = true;
       // btnSave.Visible = true;

    }
    private void FindStockist(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;

        if (sSearchBy != "Stockist_Name")
        {
            sSearchText = hdnProduct.Value;
        }
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' ";
        Stockist sk = new Stockist();
        dsStockist = sk.Search_Reactivate_Stock_Name(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdAct_Stockist.Visible = true;
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }
        else
        {
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }
        dsStockist = sk.Search_ReActivate_Stock_Name_Alpha(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.Visible = true;

            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }
        else
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
        }

    }

   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockistList.aspx");
    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
              

        if (sCmd == "All")
        {
            // FillStockist();

            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), sCmd);
        }
        else
        {
            // FillStockist(sCmd);
            FindStockist_Alphabet(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString(), sCmd);
        }

        //grdSalesForce.EditIndex = -1;
        //Fill the SalesForce Grid
        //FillSalesForce();
    }

    private void FindStockist_Alphabet(string sSearchBy, string sSearchText, string div_code, string sCmd)
    {
        string sFind = string.Empty;

        if (sSearchBy != "Stockist_Name")
        {
            sSearchText = hdnProduct.Value;
        }
        if (sCmd == "All")
        {
            sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "'";
        }
        else
        {

            sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' AND LEFT(stockist_name,1) like '" + sCmd + "' ";

        }
        Stockist sk = new Stockist();
        dsStockist = sk.Search_Alphabet__ReActivateStockistName(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdAct_Stockist.Visible = true;
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }
        else
        {
            grdAct_Stockist.DataSource = dsStockist;
            grdAct_Stockist.DataBind();
        }

    }

    //sorting

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
        Stockist sk = new Stockist();

       // string Alpha = Session["Alpha"].ToString();       

        string sFind = string.Empty;

        if (ddlFields.SelectedValue != "Stockist_Name")
        {
            txtsearch.Text = hdnProduct.Value;
        }
        sFind = " AND " + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND Division_Code = '" + Session["div_code"].ToString() + "' ";


        dsStockist = sk.Search_Reactivate_Stock_Name(sFind);
        dtGrid = dsStockist.Tables[0];
        return dtGrid;

    }

    protected void grdAct_Stockist_Sorting(object sender, GridViewSortEventArgs e)
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
        DataTable dtGrid = new DataTable();
        dtGrid = sortedView.ToTable();
        grdAct_Stockist.DataSource = dtGrid;
        grdAct_Stockist.DataBind();

    }

}

