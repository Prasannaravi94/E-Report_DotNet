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

public partial class MasterFiles_Stockist_Bulk_DeActivation : System.Web.UI.Page
{

    #region "Declaration"

    DataSet dsStockist = null;
    DataSet dsSalesForce = null;
    DataSet dsadm = null;
    string div_code = string.Empty;
    string sfCode = string.Empty;
    int search = 0;
    string Stockist_Code = string.Empty;
    string Stockist_Name = string.Empty;
    string Stockist_Address = string.Empty;
    string State = string.Empty;
    string HQ_Name = string.Empty;
    string MobilNo = string.Empty;
    string Field_ForceName = string.Empty;
    int iCnt = -1;
    string sf_code = string.Empty;
    DataSet dsTerritory = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int i = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();

        if (!Page.IsPostBack)
        {
            // FillStockist();

            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;

            btnSubmit.Visible = false;
            btnSave.Visible = false;
            lblValue.Visible = true;

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

    private void FillStockist()
    {
        lblValue.Visible = false;
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_View(div_code);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
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

        string sFind = string.Empty;

        if (ddlFields.SelectedValue != "Stockist_Name")
        {
            txtsearch.Text = hdnProduct.Value;
        }
        sFind = " AND " + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND Division_Code = '" + div_code + "' ";


        dsStockist = sk.Search_Stock_Name(sFind);
        dtGrid = dsStockist.Tables[0];
        return dtGrid;

    }

    protected void grdStockist_Sorting(object sender, GridViewSortEventArgs e)
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
        grdStockist.DataSource = dtGrid;
        grdStockist.DataBind();

    }

    protected void grdStockist_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.ClassName='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.ClassName='Normal'");
            // e.Row.Cells[1].Attributes.Add("colspan", "1");

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdStockist.Rows)
        {
            CheckBox chkStockist = (CheckBox)gridRow.Cells[0].FindControl("chkStockist");
            bool bCheck = chkStockist.Checked;
            Label lblStockistCode = (Label)gridRow.Cells[2].FindControl("lblSF_Code");
            string StockCode = lblStockistCode.Text.ToString();
            int iflag = 1;

            if ((StockCode.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Listed Doctor
                Stockist lstDR = new Stockist();
                iReturn = lstDR.DeActivate_Stockist(div_code, StockCode, iflag);
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
            if (iflag == 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
                // FillStockist();

                FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());

            }
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivation Approval sent for the Manager');</script>");
            //    FillStockist();
            //}
        }

        if (iReturn != -1)
        {
            //menu1.Status = "Listed Doctor De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            // FillStockist();

            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);
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
        dsStockist = sk.Search_Alphabet_StockistName(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }

    }


    // Search By Text
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        if (ddlFields.SelectedValue != "")
        {
            FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }
        lblValue.Visible = false;
    }


    private void FillStockist(string sAlpha)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistview_Alphabat(div_code, sAlpha);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
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
        dsStockist = sk.Search_Stock_Name(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();

            btnSubmit.Visible = true;
            btnSave.Visible = true;        
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            btnSubmit.Visible = false;
            btnSave.Visible = false;
        }
        dsStockist = sk.Search_Stock_Name_Alpha(sFind);
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtsearch.Text = "";
        ddlFields.SelectedValue = "";
        //  FillStockist();
        //grdStockist.Visible = true;
        //grdStockist.DataSource = dsStockist;
        //grdStockist.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StockistList.aspx");
    }
}

