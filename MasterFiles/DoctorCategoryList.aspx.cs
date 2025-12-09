using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Globalization;

public partial class MasterFiles_DoctorCategoryList : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDocCat = null;
    DataSet dsTerritory = null;
    DataSet dsDoc = null;
    int DocCatCode = 0;
    string divcode = string.Empty;
    string Doc_Cat_SName = string.Empty;
    string DocCatName = string.Empty;
    string Docvisit = string.Empty;
    string sf_Code = string.Empty;
    string sf_type = string.Empty;
    string mode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sf_type = Session["sf_type"].ToString();

        if (Session["sf_Code"] != null && Session["sf_Code"].ToString() != "")
        {
            sf_Code = Session["sf_Code"].ToString();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_Code = Session["sf_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            FillDocCat();
            btnNew.Focus();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
         //   //// menu1.FindControl("btnBack").Visible = false;

            getddlSF_Code();
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
    public void getddlSF_Code()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getSFCode(divcode);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            ddlSFCode.DataTextField = "Sf_Name";
            ddlSFCode.DataValueField = "Sf_Code";
            ddlSFCode.DataSource = dsTerritory;
            ddlSFCode.DataBind();
            if (Session["sf_Code"] == null || Session["sf_Code"].ToString() == "admin")
            {
                ddlSFCode.SelectedIndex = 0;
                sf_Code = ddlSFCode.SelectedValue.ToString();
                Session["sf_Code"] = sf_Code;
            }
        }
    }
    private void FillDoc()
    {
        gvDoctor.Visible = true;

        ListedDR LstDoc = new ListedDR();
        sf_Code = ddlSFCode.SelectedValue;

        dsDoc = LstDoc.getLstdDr_Wrng_CreationDivwise(divcode);

        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            gvDoctor.DataSource = dsDoc;
            gvDoctor.DataBind();
            btnProcess.Visible = true;
        }
        else
        {
            gvDoctor.DataSource = dsDoc;
            gvDoctor.DataBind();
            btnProcess.Visible = false;
        }
    }
    private void FillFieldForce()
    {
        gvDoctorFF.Visible = true;

        ListedDR LstDoc = new ListedDR();
        sf_Code = ddlSFCode.SelectedValue;

        dsDoc = LstDoc.getLstdDr_Wrng_CreationFFWise(sf_Code, divcode);

        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            gvDoctorFF.DataSource = dsDoc;
            gvDoctorFF.DataBind();
            btnProcess.Visible = true;
        }
        else
        {
            gvDoctorFF.DataSource = dsDoc;
            gvDoctorFF.DataBind();
            btnProcess.Visible = false;
        }
    }
    private void FillDocCat()
    {
        Doctor dv = new Doctor();
        dsDocCat = dv.getDocCat(divcode);
        if (dsDocCat.Tables[0].Rows.Count > 0)
        {
            grdDocCat.Visible = true;
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
            foreach (GridViewRow row in grdDocCat.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblCount");
                //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                if(lblCount.Text != "0")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }
        }
        else
        {
            grdDocCat.DataSource = dsDocCat;
            grdDocCat.DataBind();
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
    protected void txtNew_TextChanged(object sender, EventArgs e)
    {
        ddlSFCode_SelectedIndexChanged(sender, e);
    }
    protected void ddlSFCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvDoctor.DataSource = null;
        gvDoctor.DataBind();
        gvDoctorFF.DataSource = null;
        gvDoctorFF.DataBind();

        gvDoctor.Visible = false;
        gvDoctorFF.Visible = false;
        btnProcess.Visible = false;
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (rbtnLstdDrWrngCrtn.SelectedValue == "1")
        {
            FillFieldForce();
        }
        else if (rbtnLstdDrWrngCrtn.SelectedValue == "2")
        {
            FillDoc();
        }

    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            //
            #region Variable
            CheckBox chk;
            Label lblSf_Code;
            Label lblDocCode;
            Label lblCrtDate;
            Label lblActDate;
            string lblDCode;
            string lblCDate;
            string lblADate;
            int iReturn = 0;
            #endregion
            //
            if (rbtnLstdDrWrngCrtn.SelectedValue == "1")
            {
                foreach (GridViewRow gridrow in gvDoctorFF.Rows)
                {
                    #region Variables
                    chk = (CheckBox)gridrow.FindControl("chkListedDR");
                    lblDocCode = (Label)gridrow.FindControl("lblDocCode");
                    lblCrtDate = (Label)gridrow.FindControl("lblCrtDate");
                    lblActDate = (Label)gridrow.FindControl("lblActDate");
                    #endregion

                    if (chk.Checked == true)
                    {
                        if (ddlSFCode.SelectedValue != "0" && lblDocCode.Text != "" && lblCrtDate.Text != "" && lblActDate.Text != "")
                        {
                            iReturn = UpdateDocWrngCreation(ddlSFCode, divcode, lblDocCode, lblCrtDate, lblActDate);
                        }
                    }
                }
            }
            else if (rbtnLstdDrWrngCrtn.SelectedValue == "2")
            {
                foreach (GridViewRow gridrow in gvDoctor.Rows)
                {
                    #region Variables
                    chk = (CheckBox)gridrow.FindControl("chkListedDR");
                    lblSf_Code = (Label)gridrow.FindControl("lblSf_Code");
                    #endregion

                    if (chk.Checked == true)
                    {
                        ListedDR LstDoc = new ListedDR();

                        dsDoc = LstDoc.getLstdDr_Wrng_CreationFFWise(lblSf_Code.Text, divcode);

                        if (dsDoc.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDoc.Tables[0].Rows.Count; i++)
                            {
                                lblDCode = dsDoc.Tables[0].Rows[i]["ListedDrCode"].ToString();
                                lblCDate = dsDoc.Tables[0].Rows[i]["ListedDr_Created_Date"].ToString();
                                lblADate = dsDoc.Tables[0].Rows[i]["Activity_Date"].ToString();

                                iReturn = UpdateDocWrngCreation1(lblSf_Code, divcode, lblDCode, lblCDate, lblADate);
                            }
                        }
                    }
                }
            }
            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Process Completed Successfully');window.location ='" + Request.Url.AbsoluteUri + "';</script>");
            }
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }
    //
    #region UpdateDocWrngCreation
    private int UpdateDocWrngCreation(DropDownList ddlSFCode, string div_code, Label lblDocCode, Label lblCrtDate, Label lblActDate)
    {
        int iReturn = -1;

        ListedDR lst = new ListedDR();

        DateTime CrtDate = DateTime.ParseExact(lblCrtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime ActDate = DateTime.ParseExact(lblActDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        iReturn = lst.RecordUpdateLstdDr_Wrng_Creation(ddlSFCode.SelectedValue, Convert.ToInt32(div_code), Convert.ToInt32(lblDocCode.Text), CrtDate, ActDate);

        return iReturn;
    }
    private int UpdateDocWrngCreation1(Label lblSf_Code, string div_code, string lblDocCode, string lblCrtDate, string lblActDate)
    {
        int iReturn = -1;

        ListedDR lst = new ListedDR();

        DateTime CrtDate = DateTime.ParseExact(lblCrtDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime ActDate = DateTime.ParseExact(lblActDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        iReturn = lst.RecordUpdateLstdDr_Wrng_Creation(lblSf_Code.Text, Convert.ToInt32(div_code), Convert.ToInt32(lblDocCode), CrtDate, ActDate);

        return iReturn;
    }
    #endregion

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void rbtnLstdDrWrngCrtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnLstdDrWrngCrtn.SelectedValue == "1")
        {
            lblSalesforce.Visible = true;
            ddlSFCode.Visible = true;
            txtNew.Visible = true;
            ddlSFCode.SelectedValue = "";
            gvDoctor.DataSource = null;
            gvDoctor.DataBind();
            gvDoctor.Visible = false;
            gvDoctorFF.DataSource = null;
            gvDoctorFF.DataBind();
            gvDoctorFF.Visible = false;
            btnProcess.Visible = false;
            txtNew.Text = string.Empty;
        }
        else if (rbtnLstdDrWrngCrtn.SelectedValue == "2")
        {
            lblSalesforce.Visible = false;
            ddlSFCode.Visible = false;
            txtNew.Visible = false;
            gvDoctor.DataSource = null;
            gvDoctor.DataBind();
            gvDoctor.Visible = false;
            gvDoctorFF.DataSource = null;
            gvDoctorFF.DataBind();
            gvDoctorFF.Visible = false;
            btnProcess.Visible = false;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Doctor dv = new Doctor();
        dtGrid = dv.getDoctorCategorylist_DataTable(divcode);
        return dtGrid;
    }
    protected void grdDocCat_Sorting(object sender, GridViewSortEventArgs e)
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
        grdDocCat.DataSource = dtGrid;
        grdDocCat.DataBind();      
       
        foreach (GridViewRow row in grdDocCat.Rows)
        {
            LinkButton lnkdeact = (LinkButton)row.FindControl("lnkbutDeactivate");
            Label lblimg = (Label)row.FindControl("lblimg");
            if (Convert.ToInt32(dtGrid.Rows[row.RowIndex][4].ToString()) > 0)
            {                
                lnkdeact.Visible = false;
                lblimg.Visible = true;
            }
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Session["backurl"] = "DoctorCategoryList.aspx";
        Response.Redirect("DoctorCategory.aspx");
    }

    protected void grdDocCat_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //This will get invoke when the user clicks Cancel link from "Inline Edit" link
        grdDocCat.EditIndex = -1;
        //Fill the State Grid
        FillDocCat();
    }

    protected void grdDocCat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //This will get invoke when the user clicks "Inline Edit" link
        grdDocCat.EditIndex = e.NewEditIndex;
        //Fill the State Grid
        FillDocCat();
        //Setting the focus to the textbox "Short Name"        
        TextBox ctrl = (TextBox)grdDocCat.Rows[e.NewEditIndex].Cells[2].FindControl("txtDoc_Cat_SName");
        ctrl.Focus();
    }
    protected void grdDocCat_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDocCatCode = (Label)grdDocCat.Rows[e.RowIndex].Cells[1].FindControl("lblDocCatCode");
        DocCatCode = Convert.ToInt16(lblDocCatCode.Text);

        // Delete Doctor Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordDelete(DocCatCode);
         if (iReturn > 0 )
        {
           // menu1.Status = "Doctor Category Deleted Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            //menu1.Status = "Doctor Category cant be deleted";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
        }
        FillDocCat();
    }
    protected void grdDocCat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            DocCatCode = Convert.ToInt16(e.CommandArgument);

            //Deactivate
            Doctor dv = new Doctor();
            int iReturn = dv.DeActivate(DocCatCode);
             if (iReturn > 0 )
            {
                //menu1.Status = "Doctor Category has been Deactivated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
               // menu1.Status = "Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillDocCat();
        }
    }

    protected void grdDocCat_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        grdDocCat.EditIndex = -1;
        int iIndex = e.RowIndex;
        Update(iIndex);
        FillDocCat();
    }
    protected void grdDocCat_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }

    protected void grdDocCat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDocCat.PageIndex = e.NewPageIndex;
        FillDocCat();
    }
    private void Update(int eIndex)
    {
        Label lblDocCatCode = (Label)grdDocCat.Rows[eIndex].Cells[1].FindControl("lblDocCatCode");
        DocCatCode = Convert.ToInt16(lblDocCatCode.Text);
        TextBox txtDoc_Cat_SName = (TextBox)grdDocCat.Rows[eIndex].Cells[2].FindControl("txtDoc_Cat_SName");
        Doc_Cat_SName = txtDoc_Cat_SName.Text;
        TextBox txtDocCatName = (TextBox)grdDocCat.Rows[eIndex].Cells[3].FindControl("txtDocCatName");
        DocCatName = txtDocCatName.Text;
        TextBox txtvisit = (TextBox)grdDocCat.Rows[eIndex].Cells[4].FindControl("txtvisit");
        Docvisit = txtvisit.Text;
        // Update Doctor Category
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdate(DocCatCode, Doc_Cat_SName, DocCatName, Docvisit, divcode);
         if (iReturn > 0 )
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
        else if (iReturn == -2)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
            txtDocCatName.Focus();
        }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
             txtDoc_Cat_SName.Focus();
         }
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocCat.aspx");        
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocCat_SlNo_Gen.aspx");
    }
    protected void btnTransfer_Cat_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Cat_Trans.aspx");
    }
    protected void btnReactivate_Onclick(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Cat_React.aspx");
    }
    protected void lnkbtnLstdrWrngCreation_Click(object sender, EventArgs e)
    {
        if (lnkbtnLstdrWrngCreation.CommandArgument == "Show")
        {
            dvLstdrWrngCreation.Visible = true;
            lnkbtnLstdrWrngCreation.CommandArgument = "Hide";
            dvLstdrWrngCreation.Focus();
        }
        else
        {
            dvLstdrWrngCreation.Visible = false;
            lnkbtnLstdrWrngCreation.CommandArgument = "Show";
        }
    }
    protected void gvDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}