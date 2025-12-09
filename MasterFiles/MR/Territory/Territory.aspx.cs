using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
public partial class MasterFiles_MR_Territory : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    DataSet dsListedDR = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string strSF_Index = string.Empty;
    string Territory_SName = string.Empty;
    int i;
    string div_code;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsSalesForce = null;
    string strAdd = string.Empty;
    string strEdit = string.Empty;
    string strView = string.Empty;
    string strDeact = string.Empty;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (Session["sf_code"] != null && Session["sf_code"].ToString() != "")
        {
            sf_code = Session["sf_code"].ToString();
        }
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            btnNew.Focus();
            if (Session["sf_type"].ToString() == "1")
            {
                sf_code = Session["sf_code"].ToString();
                //menu1.Visible = true;
                UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                    Usc_MR.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                }
                // Usc_MR.Title = this.Page.Title;
               // pnlAdmin.Visible = false;
                //menu1.FindControl("btnBack").Visible = false;
                Usc_MR.FindControl("btnBack").Visible = false;
                // btnBack.Visible = false;
                ddlSFCode.Visible = false;
                lblFilter.Visible = false;
                btnGo.Visible = false;
                ViewTerritory();
                //if (div_code != "2")
                //{
                //    btnNew.Visible = false;
                //    btnSlNo_Gen.Visible = false;
                //    grdTerritory.Columns[5].Visible = false;
                //    btnEdit.Visible = false;
                //    btnTranfer.Visible = false;
                //    grdTerritory.Columns[6].Visible = false;
                //    btnreact.Visible = false;
                //    grdTerritory.Columns[7].Visible = false;
                //}
            }
            else
            {
                getddlSF_Code();
               // pnlAdmin.Visible = true;
                // menu1.Visible = false;
                UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                Session["backurl"] = "Territory.aspx";
                //c1.FindControl("btnBack").Visible = false;
                Territory terr = new Territory();
                dsTerritory = terr.getWorkAreaName(div_code);
                if (dsTerritory.Tables[0].Rows.Count > 0)
                {
                    btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
                    //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                    c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                }
                //menu1.FindControl("btnBack").Visible = false;               
                ViewTerritory();

            }

            GetHQ();
            // GetWorkName();
            ButtonDisable();
        }
        else
        {
            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu Usc_MR =
                    (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(Usc_MR);
            }
            else
            {
                UserControl_MenuUserControl c1 =
              (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
            }
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
    private void GetWorkName()
    {
        UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        Territory terr = new Territory();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            btnNew.Text = "Add" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            btnEdit.Text = "Edit all" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            btnTranfer.Text = "Transfer" + " " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
            c1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
        }
    }

    private void GetddlValue()
    {
        Territory terr = new Territory();
        string value = Request.QueryString["SF_Index"];
        dsTerritory = terr.getTerritory(value);
        for (int i = 0; i < ddlSFCode.Items.Count; i++)
        {


            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                grdTerritory.Visible = true;
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();

            }
            else
            {
                grdTerritory.DataSource = dsTerritory;
                grdTerritory.DataBind();

            }
        }
    }



    private void ViewTerritory()
    {
        Territory terr = new Territory();
        for (int i = 1; i < ddlSFCode.Items.Count; i++)
        {
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
            }
            if (ddlSFCode.Items[i].Value == sf_code)
            {
                ddlSFCode.SelectedIndex = i;

            }
        }

        dsTerritory = terr.getTerritory_Create(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            foreach (GridViewRow row in grdTerritory.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblListedDRCnt = (Label)row.FindControl("lblListedDRCnt");
                Label lblChemistsCnt = (Label)row.FindControl("lblChemistsCnt");
                Label lblUnListedDRCnt = (Label)row.FindControl("lblUnListedDRCnt");
                //  if (Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][4].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][5].ToString()) > 0 || Convert.ToInt32(dsTerritory.Tables[0].Rows[row.RowIndex][7].ToString()) > 0)
                if ((lblListedDRCnt.Text != "0") || (lblChemistsCnt.Text != "0") || (lblUnListedDRCnt.Text != "0"))
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }


    }

    private void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();

            if (Session["sf_code"] == null || Session["sf_code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 1;
                sf_code = ddlSFCode.SelectedValue.ToString();
                Session["sf_code_Temp"] = sf_code;
            }

        }

    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("TerritoryCreation.aspx");
    }

    protected void grdTerritory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdTerritory.EditIndex = -1;
        //Fill the Division Grid
        ViewTerritory();
    }

    protected void grdTerritory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdTerritory.EditIndex = e.NewEditIndex;
        //Fill the Division Grid
        ViewTerritory();
        //Setting the focus to the textbox "Division Name"        
        TextBox ctrl = (TextBox)grdTerritory.Rows[e.NewEditIndex].Cells[2].FindControl("txtTerritory_Name");
        ctrl.Focus();
    }

    protected void grdTerritory_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdTerritory.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateTerritory(iIndex);
        ViewTerritory();
    }

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
            if (Territory_Type != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Territory_Cat"].ToString()));
            }
            //Label lblS = (Label)e.Row.FindControl("lblTerritory_Flag");
            //Label lblApp = (Label)e.Row.FindControl("lblApp");
            ////   Label lblDrsCnt = (Label)e.Row.FindControl("lblDrsCnt");
            //if (lblS.Text == "1" || lblS.Text == "2")
            //{
            //    lblApp.ForeColor = System.Drawing.Color.Red;
            //    lblApp.Style.Add("font-size", "12pt");
            //    lblApp.Style.Add("font-weight", "Bold");
            //    lblApp.Text = "(AP.)";
            //}
        }
    }

    protected void grdTerritory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            Territory_Code = Convert.ToString(e.CommandArgument);

            //Deactivate
            Territory Terr = new Territory();
            int iReturn = Terr.DeActivate(Territory_Code);
            if (iReturn > 0)
            {
                // menu1.Status = "Territory has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            ViewTerritory();
        }
    }

    protected void grdTerritory_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdTerritory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTerritory.PageIndex = e.NewPageIndex;

        ViewTerritory();

    }

    private void UpdateTerritory(int eIndex)
    {
        Label lbl_Territory_Code = (Label)grdTerritory.Rows[eIndex].Cells[1].FindControl("lblTerritory_Code");
        Territory_Code = lbl_Territory_Code.Text;
        TextBox txt_Territory_Name = (TextBox)grdTerritory.Rows[eIndex].Cells[2].FindControl("txtTerritory_Name");
        Territory_Name = txt_Territory_Name.Text.Replace(",", "/").ToString();
        TextBox txt_Territory_Sname = (TextBox)grdTerritory.Rows[eIndex].Cells[3].FindControl("txtTerritory_Sname");
        Territory_SName = txt_Territory_Sname.Text;
        DropDownList ddl_Territory_Type = (DropDownList)grdTerritory.Rows[eIndex].Cells[4].FindControl("Territory_Type");
        Territory_Type = ddl_Territory_Type.SelectedValue.ToString();

        // Update Territory
        //if (Session["sf_code_Temp"] != null)
        //{
        //    sf_code = Session["sf_code_Temp"].ToString();
        //}
        //else
        //{
        //    sf_code = Session["sf_code"].ToString();
        //}
        //Territory Terr = new Territory();
        //int iReturn = Terr.RecordUpdate(Territory_Code, Territory_Name, Territory_SName, Territory_Type, sf_code);
        //if (iReturn > 0)
        //{
        //    //menu1.Status = "Territory Updated Successfully ";
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        //}
        //else if (iReturn == -2)
        //{
        //    //menu1.Status = "Territory exist with the same short name!!";
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Exist with the same short Name');</script>");
        //}
        if (Session["sf_code_Temp"] != null)
        {
            sf_code = Session["sf_code_Temp"].ToString();
        }
        else
        {
            sf_code = Session["sf_code"].ToString();
        }
        Territory Terr = new Territory();
        //  iReturn = Terr.RecordUpdate_Dist(Territory_Code, Territory_Name, Territory_SName, Territory_Type, sf_code);
        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction();

            command.Connection = connection;

            command.Transaction = transaction;



            try
            {

                SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                command.CommandText = "UPDATE Mas_Territory_Creation " +
                    " SET Territory_Name = '" + Territory_Name + "', " +
                    " Territory_Cat = '" + Territory_Type + "', " +
                    " Territory_SName = '" + Territory_SName + "', LastUpdt_Date= getdate() " +
                    " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' ";
                command.ExecuteNonQuery();


                command.CommandText = " update mas_distance_Fixation set  " +
                         " To_Code_Code= STUFF(To_Code_Code, LEN(To_Code_Code), 1, '" + Territory_Type + "'), Town_Cat='" + Territory_Type + "' " +
                         " where to_code='" + Territory_Code + "' ";
                command.ExecuteNonQuery();

                command.CommandText = "delete from mas_distance_fixation where from_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                command.ExecuteNonQuery();

                command.CommandText = "delete from mas_distance_fixation where to_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                command.ExecuteNonQuery();
                transaction.Commit();
                connection.Close();
                //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully'); { self.close() };</script>");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }


            catch (Exception ex)
            {

                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                Console.WriteLine("Message: {0}", ex.Message);


                // Attempt to roll back the transaction.
                try
                {

                    transaction.Rollback();

                }

                catch (Exception ex2)
                {

                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                    Console.WriteLine("  Message: {0}", ex2.Message);

                }
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");

            }
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_BulkEdit.aspx");
    }
    protected void btnTransfer_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("TransferTerritory.aspx");
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
        dtGrid = terr.getTerritory_DataTable_Create(sf_code);
        return dtGrid;
    }

    protected void grdTerritory_Sorting(object sender, GridViewSortEventArgs e)
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
        grdTerritory.DataSource = dtGrid;
        grdTerritory.DataBind();

        foreach (GridViewRow row in grdTerritory.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");

            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][4].ToString()) > 0 || Convert.ToInt32(dtGrid.Rows[row.RowIndex][5].ToString()) > 0 || Convert.ToInt32(dtGrid.Rows[row.RowIndex][7].ToString()) > 0)
            //  if((lblListedDRCnt.Text != "0") || (lblChemistsCnt.Text != "0") || (lblUnListedDRCnt.Text != "0"))
            {
                // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_SlNo_Gen.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            sf_code = ddlSFCode.SelectedValue.ToString();
            Session["sf_code_Temp"] = sf_code;
            ViewTerritory();
            GetHQ();

        }
        catch (Exception ex)
        {

        }
    }
    private void GetHQ()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerrritoryView(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            //lblTerrritory.Text = "<span style='font-weight: bold;color:Blue;font-names:verdana'>HQ :</span>" +
            //    "<span style='font-weight: bold;color:Red'>  " + dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString() + "</span>";

            Session["sf_HQ"] = dsTerritory.Tables[0].Rows[0]["Sf_HQ"].ToString();
            Session["sfName"] = dsTerritory.Tables[0].Rows[0]["sfName"].ToString();
            Session["sf_Designation_Short_Name"] = dsTerritory.Tables[0].Rows[0]["sf_Designation_Short_Name"].ToString();
        }
    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    Session["sf_code"] = null;
    //    Response.Redirect("~/BasicMaster.aspx");
    //}
    protected void ButtonDisable()
    {
        try
        {

            if (Session["sf_type"].ToString() == "1")
            {
                AdminSetup adm = new AdminSetup();
                dsSalesForce = adm.Get_Admin_FieldForce_Setup(sf_code, div_code);
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    strAdd = dsSalesForce.Tables[0].Rows[0]["Territory_Add_Option"].ToString();
                    if (strAdd == "1")
                    {
                        btnNew.Visible = false;
                        btnSlNo_Gen.Visible = false;
                        grdTerritory.Columns[5].Visible = false;
                    }
                    strEdit = dsSalesForce.Tables[0].Rows[0]["Territory_Edit_Option"].ToString();
                    if (strEdit == "1")
                    {
                        btnEdit.Visible = false;
                        btnTranfer.Visible = false;
                        grdTerritory.Columns[6].Visible = false;
                    }
                    strDeact = dsSalesForce.Tables[0].Rows[0]["Territory_Deactivate_Option"].ToString();
                    if (strDeact == "1")
                    {
                        btnreact.Visible = false;
                        grdTerritory.Columns[7].Visible = false;
                    }


                    //strView = dsSalesForce.Tables[0].Rows[0]["Territory_View_Option"].ToString();
                    //if (strView == "1")
                    //{

                    //}
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btnreact_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_Reactivation.aspx");
    }
    protected void btndeact_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Territory_Deactivation.aspx");
    }

}