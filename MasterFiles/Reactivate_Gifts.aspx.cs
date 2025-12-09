using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_Reactivate_Gifts : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsEffDate = null;
    DataSet dsGift = null;
    DataSet dsDivision = null;
    DataSet dsState = null;

    string[] statecd;
    string Gift_Code = string.Empty;
    string State_Code = string.Empty;
    string state_cd = string.Empty;
    string div_code = string.Empty;
    string GiftCode = string.Empty;
    string giftcode = string.Empty;
    string GName = string.Empty;
    string GSname = string.Empty;
    string GVal = string.Empty;
    string GType = string.Empty;
    string Gift_Type = string.Empty;
    string sState = string.Empty;
    string[] Dt;
    string EffDate = string.Empty;
    int gifttype = 0;
    int i;
    string sCmd = string.Empty;
    string val = string.Empty;
    DataSet dsProd = null;
    string ProdCode = string.Empty;
    int search = 0;
    int state_code;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "ProductReminderList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            Session["GetcmdArgChar"] = "All";
            Input_New pv = new Input_New();
            dsEffDate = pv.getEffDate_deact(div_code);
            Ddl_EffDate.DataTextField = "Effective_From_To";
            Ddl_EffDate.DataValueField = "Effective_From_To";
            Ddl_EffDate.DataSource = dsEffDate;
            Ddl_EffDate.DataBind();
            FillGift();
            FillGift_Alpha();
            FillState(div_code);
            menu1.Title = this.Page.Title;

           // TextBox1.Text = DateTime.Today.ToString("dd/MM/yyyy");

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }

    private void FillGift()
    {
        Input_New dv = new Input_New();
        if (Ddl_EffDate.Text == "All")
        {
            dsGift = dv.getGift_React(div_code);
            if (dsGift.Tables[0].Rows.Count > 0)
            {
               // dlAlpha.Visible = true;
                grdGift.Visible = true;
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
            else
            {                
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
        }
        else
        {
            EffDate = Ddl_EffDate.Text;
            Dt = EffDate.Split(' ');
            string EffFrom = string.Empty;
            string EffTo = string.Empty;
            if (Dt.Length > 0)
            {
                EffFrom = Dt[0];
                EffTo = Dt[2];
                dsGift = dv.getGift_React(div_code, Convert.ToDateTime(EffFrom), Convert.ToDateTime(EffTo));
                //dsGift = dv.getGift(div_code,EffFrom,EffTo);
                if (dsGift.Tables[0].Rows.Count > 0)
                {
                   // dlAlpha.Visible = true;
                    grdGift.Visible = true;
                    grdGift.DataSource = dsGift;
                    grdGift.DataBind();
                }
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

        Input_New dv = new Input_New();
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            dtGrid = dv.getGiftlist_DataTable_React(div_code);
        }
        else if (sCmd != "")    
        {
            sCmd = Session["GetcmdArgChar"].ToString();
            dtGrid = dv.getGiftlist_React_Sort(div_code, sCmd);
        }
        else if (txtsearch.Text != "")
        {
            dtGrid = dv.getGiftlist_React_Sort(div_code, txtsearch.Text);
        }

        else if (Ddl_EffDate.SelectedValue != "")
        {
            EffDate = Ddl_EffDate.Text;
            Dt = EffDate.Split(' ');
            string EffFrom = string.Empty;
            string EffTo = string.Empty;
            if (Dt.Length > 0)
            {
                EffFrom = Dt[0];
                EffTo = Dt[2];
                dtGrid = dv.getGiftDateDiff(div_code, Convert.ToDateTime(EffFrom), Convert.ToDateTime(EffTo));
            }
        }
        return dtGrid;
    }

    protected void grdGift_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
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
            grdGift.DataSource = sortedView;
            grdGift.DataBind();
        }
        catch (Exception ex)
        {

        }


    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductReminder.aspx");
    }
    protected void grdGift_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdGift.EditIndex = -1;
        //Fill the Division Grid
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillGift();

        }
        else if (sCmd != "")
        {
            FillGift(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            Btnsrc_Click(sender, e);
        }
        else if (Ddl_EffDate.SelectedValue != "")
        {
            FillGift();
        }
    }

    protected void grdGift_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdGift.EditIndex = e.NewEditIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillGift();
        }
        else if (sCmd != "")
        {
            FillGift(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            Btnsrc_Click(sender, e);
        }
        //Setting the focus to the textbox "Product Name"        
        TextBox ctrl = (TextBox)grdGift.Rows[e.NewEditIndex].Cells[2].FindControl("GiftName");
        ctrl.Focus();
    }

    protected void grdGift_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdGift.EditIndex = -1;
        int iIndex = e.RowIndex;
        UpdateProd(iIndex);
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillGift();
        }
        else if (sCmd != "")
        {
            FillGift(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            Btnsrc_Click(sender, e);
        }
        else if (Ddl_EffDate.SelectedValue != "")
        {
            FillGift();
        }
    }

    protected void grdGift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlGiftType = (DropDownList)e.Row.FindControl("ddlGiftType");
            Label lblFlag = (Label)e.Row.FindControl("lblFlag");
            Label lblGiftName = (Label)e.Row.FindControl("lblGiftName");
            //CommandField CommandField = (CommandField)e.Row.FindControl("lnkbutReactivate");
            LinkButton lnkdeact = (LinkButton)e.Row.FindControl("lnkbutReactivate");
            Label lblimg = (Label)e.Row.FindControl("lblimg");
            if (ddlGiftType != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                ddlGiftType.SelectedIndex = ddlGiftType.Items.IndexOf(ddlGiftType.Items.FindByText(row["Gift_Type"].ToString()));
            }
            if (lblFlag.Text == "1")
            {
                lblGiftName.Text = lblGiftName.Text + "<span style='font-weight: bold;color:Red; font-size:10px'> (Deactivate) </span>";
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        } 



        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    DropDownList ddlGiftType = (DropDownList)e.Row.FindControl("ddlGiftType");
        //    if (ddlGiftType != null)
        //    {
        //        DataRowView row = (DataRowView)e.Row.DataItem;
        //        ddlGiftType.SelectedIndex = ddlGiftType.Items.IndexOf(ddlGiftType.Items.FindByText(row["Gift_Type"].ToString()));
        //    }

        //    Label lblflag = (Label)e.Row.FindControl("lblflag");
        //    LinkButton lnkbutDeactivate = (LinkButton)e.Row.FindControl("lnkbutDeactivate");

        //    if (lblflag.Text == "0")
        //    {
        //        lnkbutDeactivate.Text = "Deactivate";
        //        lnkbutDeactivate.ForeColor = System.Drawing.Color.Red;
        //        lnkbutDeactivate.CommandName = "Deactivate";
        //    }
        //    else if (lblflag.Text == "1")
        //    {
        //        lnkbutDeactivate.Text = "Reactivate";
        //        lnkbutDeactivate.CommandName = "Reactivate";
        //    }
        //}
    }

    protected void grdGift_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Deactivate")
        {
            GiftCode = Convert.ToString(e.CommandArgument);

            //Deactivate
            Input_New dv = new Input_New();
            int iReturn = dv.DeActivateGift_New(GiftCode, div_code);
            if (iReturn > 0)
            {
                // menu1.Status = "Gift has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                //menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillGift();
        }
        //if (e.CommandName == "Deactivate")
        //{
        //    GiftCode = Convert.ToString(e.CommandArgument);

        //    //Deactivate
        //    Product dv = new Product();
        //    int iReturn = dv.DeActivateGift(GiftCode);
        //    if (iReturn > 0)
        //    {
        //        // menu1.Status = "Gift has been Deactivated Successfully";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
        //    }
        //    else
        //    {
        //        //menu1.Status = "Unable to Deactivate";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
        //    }
        //    FillGift();
        //}
        //else if (e.CommandName == "Reactivate")
        //{
        //    GiftCode = Convert.ToString(e.CommandArgument);

        //    //Deactivate
        //    Product dv = new Product();
        //    int iReturn = dv.ReActivateGift(GiftCode);
        //    if (iReturn > 0)
        //    {
        //        // menu1.Status = "Gift has been Deactivated Successfully";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Reactivated Successfully');</script>");
        //    }
        //    else
        //    {
        //        //menu1.Status = "Unable to Deactivate";
        //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Reactivate');</script>");
        //    }
        //    FillGift();
        //}
    }

    //protected void grdGift_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Deactivate")
    //    {
    //        GiftCode = Convert.ToString(e.CommandArgument);

    //        //Deactivate
    //        Product dv = new Product();
    //        int iReturn = dv.DeActivateGift(GiftCode);
    //        if (iReturn > 0)
    //        {
    //            // menu1.Status = "Gift has been Deactivated Successfully";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
    //        }
    //        else
    //        {
    //            //menu1.Status = "Unable to Deactivate";
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
    //        }
    //        FillGift();
    //    }
    //}

    protected void grdGift_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            //e.Row.Attributes.Add("onmouseout", "this.className='normal'");

            //CheckBox Value = e.Row.FindControl("chkAll") as CheckBox;
            //Value.Text ="";
        }
    }

    protected void grdGift_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdGift.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();
            
        if (sCmd == "All")
        {
            FillGift();
        }
        else if (sCmd != "")
        {
            FillGift(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            Btnsrc_Click(sender, e);
        }
        else if (Ddl_EffDate.SelectedValue != "")
        {
            FillGift();
        }
    }

    private void UpdateProd(int eIndex)
    {
        Label lblGiftCode = (Label)grdGift.Rows[eIndex].Cells[1].FindControl("lblGiftCode");
        GiftCode = lblGiftCode.Text;
        TextBox GiftName = (TextBox)grdGift.Rows[eIndex].Cells[2].FindControl("GiftName");
        GName = GiftName.Text;
        TextBox txtGiftSN = (TextBox)grdGift.Rows[eIndex].Cells[3].FindControl("txtGiftSN");
        GSname = txtGiftSN.Text;
        TextBox txtGiftval = (TextBox)grdGift.Rows[eIndex].Cells[5].FindControl("txtGiftval");
        GVal = txtGiftval.Text;
        DropDownList ddlGiftType = (DropDownList)grdGift.Rows[eIndex].Cells[4].FindControl("ddlGiftType");
        gifttype = Convert.ToInt32(ddlGiftType.SelectedValue);

        Input_New dv = new Input_New();
        int iReturn = dv.RecordUpdateGift_new(GiftCode, GName, GSname, GVal, gifttype, Convert.ToInt32(div_code));
        if (iReturn > 0)
        {
            //menu1.Status = "Gift details updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Gift details already exist!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already exist');</script>");
        }
    }
    protected void Ddl_EffDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        grdGift.PageIndex = 0;
        FillGift();
        txtsearch.Text = string.Empty;
        Session["GetCmdArgChar"] = string.Empty;
    }
    //alphabetical
    private void FillGift(string sAlpha)
    {
        Input_New dv = new Input_New();
        dsGift = dv.getProdgift(div_code, sAlpha);
        if (dsGift.Tables[0].Rows.Count > 0)
        {
           // dlAlpha.Visible = true;
            grdGift.Visible = true;
            grdGift.DataSource = dsGift;
            grdGift.DataBind();
        }
    }

    private void FillGift_Alpha()
    {
        Input_New dv = new Input_New();
        dsGift = dv.getProdgift_Alphabet(div_code);
        if (dsGift.Tables[0].Rows.Count > 0)
        {
            //dlAlpha.DataSource = dsGift;
           // dlAlpha.DataBind();
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetcmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdGift.PageIndex = 0;
            FillGift();
        }
        else
        {
            grdGift.PageIndex = 0;
            FillGift(sCmd);
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductReminder_View.aspx");
    }
    //Changes done by Priya
    protected void Btnsrc_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetcmdArgChar"] = string.Empty;
        grdGift.PageIndex = 0;
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        Input_New dv = new Input_New();

        if (search == 1)
        {
            FillGift();
            grdGift.Columns[12].Visible = true;
            grdGift.Columns[10].Visible = false;
        }
        if (search == 2)
        {
            dsGift = dv.getGiftName_React(div_code, txtsearch.Text);
            if (dsGift.Tables[0].Rows.Count > 0)
            {
             //   dlAlpha.Visible = true;
                grdGift.Visible = true;
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
                grdGift.Columns[11].Visible = true;
                grdGift.Columns[12].Visible = false;

                btnActivate.Visible = true;
                btnDeActivate.Visible = false;   
            }
            else
            {
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
        }
        if (search == 3)
        {
            Input_New dv1 = new Input_New();
            if (ddlSrc2.SelectedItem.ToString() == "---All---")
            {
                FillGift();
                grdGift.Columns[12].Visible = true;
                grdGift.Columns[10].Visible = false;
            }
            else
            {
                dsGift = dv1.getStategift_React(div_code, Convert.ToInt32(ddlSrc2.SelectedValue.ToString()));
                if (dsGift.Tables[0].Rows.Count > 0)
                {
                    //  dlAlpha.Visible = true;
                    grdGift.Visible = true;
                    grdGift.DataSource = dsGift;
                    grdGift.DataBind();
                    grdGift.Columns[12].Visible = true;
                    grdGift.Columns[10].Visible = false;

                    btnActivate.Visible = true;
                    btnDeActivate.Visible = false;   
                }
                else
                {
                    grdGift.DataSource = dsGift;
                    grdGift.DataBind();
                }
            }
        }

        if (search == 4)
        {
            dsGift = dv.getGiftDeactivated_React(div_code, (ddlSrch.SelectedItem.ToString()));
            if (dsGift.Tables[0].Rows.Count > 0)
            {
                // dlAlpha.Visible = true;
                grdGift.Visible = true;
                grdGift.DataSource = dsGift;             
                grdGift.DataBind();

                grdGift.Columns[12].Visible = true;
                grdGift.Columns[10].Visible = false;
               
                btnActivate.Visible = true;
                btnDeActivate.Visible = false;   
            }
            else
            {
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
        }

        if (search == 5)
        {
            dsGift = dv.getGiftOnlyClosedDate_React(div_code, (ddlSrch.SelectedItem.ToString())); 
            if (dsGift.Tables[0].Rows.Count > 0)
            {
                // dlAlpha.Visible = true;
                grdGift.Visible = true;
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
                grdGift.Columns[12].Visible = true;
                grdGift.Columns[10].Visible = false;

                btnActivate.Visible = true;
                btnDeActivate.Visible = false;   
            }
            else
            {
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
        }

        if (search == 6) 
        {
            dsGift = dv.getGiftOnly_BulkDeactive(div_code, (ddlSrch.SelectedItem.ToString()));
            if (dsGift.Tables[0].Rows.Count > 0)
            {
                // dlAlpha.Visible = true;
                //dsGift.Tables[0].Rows.RemoveAt(0);
                grdGift.Visible = true;
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
                grdGift.Columns[12].Visible = false;
                grdGift.Columns[10].Visible = true;

                btnActivate.Visible = false;   
                btnDeActivate.Visible = true;   
            }
            else
            {
                grdGift.DataSource = dsGift;
                grdGift.DataBind();
            }
        }
    }
    //end
    //Changes done by Priya--jul24
    protected void ddlSrch_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = Convert.ToInt32(ddlSrch.SelectedValue);
        if (search == 2)
        {
            txtsearch.Visible = true;
            Btnsrc.Visible = true;
            ddlSrc2.Visible = false;
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc2.Visible = false;
            Btnsrc.Visible = false;
            FillGift();
        }
        if (search == 3)
        {
            ddlSrc2.Visible = true;
            FillState(div_code);
            Btnsrc.Visible = true;
        }
    }
    //end

    //Changes done by Priya
    private void FillState(string div_code)
    {
        Input_New dv = new Input_New();
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

            Input_New st = new Input_New();
            dsState = st.getState(state_cd);
            ddlSrc2.DataTextField = "statename";
            ddlSrc2.DataValueField = "state_code";
            ddlSrc2.DataSource = dsState;
            ddlSrc2.DataBind();
        }
    }


    protected void ddlSrc2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //end   
   
    protected void btnDeActivate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        int iReturn = -1;
        foreach (GridViewRow gridRow in grdGift.Rows)
        {
            CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkGifts");
            bool bCheck = chkDR.Checked;
            Label lblGiftCode = (Label)gridRow.Cells[2].FindControl("lblGiftCode");
            string Gift_Code = lblGiftCode.Text.ToString();

            if ((Gift_Code.Trim().Length > 0) && (bCheck == true))
            {
                // De-Activate Chemists                
                Input_New Div = new Input_New();
                iReturn = Div.DeActivate_Input(Gift_Code); 
            }
            else
            {
                //menu1.Status = "Enter all the values!!";
            }
        }

        if (iReturn != -1)
        {
            //  menu1.Status = "Chemists De-Activated Successfully!!";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('De-Activated Successfully');</script>");
            FillGift();
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        //if ((txtEffTo.Text).ToString() != "")
        {
            System.Threading.Thread.Sleep(time);
            int iReturn = -1;
            foreach (GridViewRow gridRow in grdGift.Rows)
            {
                CheckBox chkDR = (CheckBox)gridRow.Cells[0].FindControl("chkGifts");
                bool bCheck = chkDR.Checked;
                Label lblGiftCode = (Label)gridRow.Cells[2].FindControl("lblGiftCode");
                string Gift_Code = lblGiftCode.Text.ToString();

                if ((Gift_Code.Trim().Length > 0) && (bCheck == true))
                {
                    // De-Activate Chemists                
                    Input_New Div = new Input_New();
                    iReturn = Div.Re_Activate_Input(Gift_Code, Convert.ToDateTime(txtEffTo.Text));
                } 
                else
                {
                    //menu1.Status = "Enter all the values!!";
                }
            }

            if (iReturn != -1)
            {
                //  menu1.Status = "Chemists De-Activated Successfully!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Re-Activated Successfully');</script>");
                FillGift();
            }
        }

        //else
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Eff.To Date');</script>");
        //}
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("ProductReminderList.aspx");
    }
     
}