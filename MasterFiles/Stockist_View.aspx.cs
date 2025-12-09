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

public partial class MasterFiles_Stockist_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsDivision = null;
    DataSet dsReport = null;
    string divcode = string.Empty;
    string stockist_code = string.Empty;
    string sf_code = string.Empty;

    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string state_code = string.Empty;

    int time;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //  Session["backurl"] = "StockistList.aspx";
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {

            //FillStockist();
            //FillSF_Alpha();
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            txtsearch.Style.Add("display", "none");
          

        }

    }
    private void FillStockist()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_View(divcode);

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

    protected void grdStockist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdStockist.PageIndex = e.NewPageIndex;
        // FillStockist();

        FindStockist(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
    }
    private void FillSF_Alpha()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistview_Alphabet(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
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
        sFind = " AND " + ddlFields.SelectedValue + " like '" + txtsearch.Text + "%' AND Division_Code = '" + divcode + "' ";

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

    private void FillStockist(string sAlpha)
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getStockistview_Alphabat(divcode, sAlpha);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
        }
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
        if (sSearchText != "--All--")
        {
            if (sCmd == "All")
            {
                sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "'";
            }
            else
            {
                sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' AND LEFT(stockist_name,1) like '" + sCmd + "' ";
            }
        }
        else
        {
            if (sCmd == "All")
            {
                sFind = "AND Division_Code = '" + div_code + "'";
            }
            else
            {
                sFind = " AND Division_Code = '" + div_code + "' AND LEFT(stockist_name,1) like '" + sCmd + "' ";
            }
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
    }

    private void FindStockist(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;

        if (sSearchBy != "Stockist_Name")
        {
            sSearchText = hdnProduct.Value;
        }
        if (sSearchText == "--All--")
        {
            sFind = " AND Division_Code = '" + div_code + "' ";
        }
        else 
        {
            sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND Division_Code = '" + div_code + "' ";
        }


        Stockist sk = new Stockist();
        dsStockist = sk.Search_Stock_Name(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
           
            grdStockist.Visible = true;
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            pnlprint.Visible = true;
        }
        else
        {
            grdStockist.DataSource = dsStockist;
            grdStockist.DataBind();
            pnlprint.Visible = false;
        }
        dsStockist = sk.Search_Stock_Name_Alpha(sFind);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            dlAlpha.Visible = true;

            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
            pnlprint.Visible = true;
        }
        else
        {
            dlAlpha.DataSource = dsStockist;
            dlAlpha.DataBind();
            pnlprint.Visible = false;
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

}
