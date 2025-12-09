using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_TerritoryCreation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Alias_SName = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    int iCnt = -1;
    string Town_Name = string.Empty;
    string Territory_Visit = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";

            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            UserControl_MGR_Menu Usc_MGR =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(Usc_MGR);
            Usc_MGR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";

            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MGR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }
        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            UserControl_MenuUserControl c1 =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;

            Session["backurl"] = "Territory.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:#696D6E;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:#696D6E;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
            }
        }
        if (div_code.Trim() == "17")
        {
            grdTerritory.Columns[3].Visible = false;
            grdTerritory.Columns[4].Visible = true;
            grdTerr.Columns[2].Visible = false;
            grdTerr.Columns[3].Visible = true;
            FillCity();
        }
        else
        {
            grdTerritory.Columns[3].Visible = true;
            grdTerritory.Columns[4].Visible = false;
            grdTerr.Columns[2].Visible = true;
            grdTerr.Columns[3].Visible = false;
        }

        if (!Page.IsPostBack)
        {
            Session["backurl"] = "Territory.aspx";
            GetWorkName();
            FillTerritory();
            ViewTerritory();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            //grdTerr.UseAccessibleHeader = true;
            //grdTerr.HeaderRow.TableSection = TableRowSection.TableHeader;       
        }
    }

    protected DataSet FillCity()
    {
        ListedDR lstDR = new ListedDR();
        SalesForce sf = new SalesForce();
        DataSet dscity = new DataSet();
        DataSet dsst = new DataSet();
        dsst = sf.CheckStatecode(sf_code);
        if (dsst.Tables[0].Rows.Count > 0)
        {
            dscity = lstDR.FetchTownCity(div_code, dsst.Tables[0].Rows[0]["State_Code"].ToString());

        }
        return dscity;
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

    private void FillTerritory()
    {
        Territory terr = new Territory();
        iCnt = terr.RecordCount(sf_code);
        ViewState["iCnt"] = iCnt.ToString();
        dsTerritory = terr.getEmptyTerritory();
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
    }

    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerr.Visible = true;
            grdTerr.DataSource = dsTerritory;
            grdTerr.DataBind();


        }
    }
    protected void getTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSNo = (Label)e.Row.FindControl("lblSNo");
            lblSNo.Text = Convert.ToString(Convert.ToInt32(lblSNo.Text) + Convert.ToInt32(ViewState["iCnt"].ToString()));

            DropDownList Territory_Visit = (DropDownList)e.Row.FindControl("Territory_Visit");
            if (Territory_Visit != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory_Visit.SelectedIndex = Territory_Visit.Items.IndexOf(Territory_Visit.Items.FindByText(row["Territory_Visit"].ToString()));
            }
        }

    }
    // Sorting 
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
        Territory terr = new Territory();
        dtGrid = terr.getTerritory_DataTable(sf_code);
        return dtGrid;
    }
    protected void grdTerr_Sorting(object sender, GridViewSortEventArgs e)
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
        grdTerr.DataSource = sortedView;
        grdTerr.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        int iflag = -1;
        foreach (GridViewRow gridRow in grdTerritory.Rows)
        {
            TextBox txtTerritory_Name = (TextBox)gridRow.Cells[1].FindControl("Territory_Name");
            Territory_Name = txtTerritory_Name.Text.Replace(",", "/").ToString();
            DropDownList ddlTerritory_Type = (DropDownList)gridRow.Cells[1].FindControl("Territory_Type");
            Territory_Type = ddlTerritory_Type.SelectedValue.ToString();

            DropDownList ddl_Territory_Visit = (DropDownList)gridRow.Cells[2].FindControl("Territory_Visit");

            Territory_Visit = ddl_Territory_Visit.SelectedValue.ToString();

            if ((Territory_Name.Trim().Length > 0) && (Territory_Type.Trim().Length > 0))
            {
                Territory terr = new Territory();
                if (Session["sf_code_Temp"] != null)
                {
                    sf_code = Session["sf_code_Temp"].ToString();
                }
                else if (Session["sf_code"] != null)
                {
                    sf_code = Session["sf_code"].ToString();
                }


                if (Territory_Visit.Trim() == "")
                {
                    Territory_Visit = "0";
                }


                if (div_code.Trim() == "17")
                {
                    DropDownList ddltown = (DropDownList)gridRow.Cells[1].FindControl("City_Code");
                    Town_Name = ddltown.SelectedItem.Text.Trim();
                    if (Town_Name.Trim() == "---Select---")
                    {
                        Town_Name = "";
                    }
                    iReturn = terr.RecordAdd_Town(Territory_Name, Town_Name, Territory_Type, sf_code, Territory_Visit);
                }
                else
                {
                    TextBox txtAlias_Name = (TextBox)gridRow.Cells[1].FindControl("Alias_Name");
                    Alias_SName = txtAlias_Name.Text.Replace(",", "/").ToString();
                    iReturn = terr.RecordAdd(Territory_Name, Alias_SName, Territory_Type, sf_code, Territory_Visit);
                }
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            FillTerritory();
            ViewTerritory();
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        FillTerritory();
    }
    private void GetWorkName()
    {
        UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "Creation";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Territory.aspx");
    }
}