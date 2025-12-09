using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class Notfication_Msg : System.Web.UI.Page
{
    string div_code=string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string sdesig = string.Empty;
    string sSubdiv = string.Empty;
    string field = string.Empty;
    string sStatename = string.Empty;
    DataSet dsDivision = new DataSet();
    DataSet dsdesig = new DataSet();
    DataSet dsState = new DataSet();
    DataSet dsSubDivision = new DataSet();
    DataSet dsSalesForce = new DataSet();
    string[] statecd;
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code=Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
       

        if (!Page.IsPostBack)
        {
            FillDesignation();
            FillState();
            FillSubdiv();
            FillMRManagers();
            FillgridColor();
           
           
        }
    }

    //protected void gvFF_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    CheckBox chkSf = (CheckBox)sender;
    //    GridViewRow row1 = (GridViewRow)chkSf.Parent.Parent;
    //    row1.Focus();
    //    int count = 0;
    //    foreach (GridViewRow row in grdnotify.Rows)
    //    {
    //        CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSf");
    //        if (ChkBoxRows.Checked == true)
    //        {
    //            count++;
    //            lblSelectedCount.Text = "No.of Fieldforce Selected : " + "<span style='color:red'>" + count + "</span>";
    //        }
    //    }
    //}
    private void FillDesignation()
    {
        Designation desig = new Designation();
        dsdesig = desig.getDesignationmsg(div_code);

        if (dsdesig.Tables[0].Rows.Count > 0)
        {
            lstDesig.DataValueField = "Designation_Code";
            lstDesig.DataTextField = "Designation_Short_Name";
            lstDesig.DataSource = dsdesig;
            lstDesig.DataBind(); 
        }
    }
    private void FillState()
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
            dsState = st.getSt(state_cd);
            lstState.DataTextField = "statename";
            lstState.DataValueField = "state_code";
            lstState.DataSource = dsState;
            lstState.DataBind();
        }
    }
    private void FillSubdiv()
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdivision(div_code);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            lstsubdiv.DataTextField = "subdivision_name";
            lstsubdiv.DataValueField = "subdivision_code";
            lstsubdiv.DataSource = dsSubDivision;
            lstsubdiv.DataBind();
        }
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstfieldforce.DataTextField = "sf_name";
            lstfieldforce.DataValueField = "sf_code";
            lstfieldforce.DataSource = dsSalesForce;
            lstfieldforce.DataBind();
        }
       
    }
    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in grdnotify.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }
    }
   
    private void fillgriddesig()
    {
        SalesForce sal = new SalesForce();

        foreach (System.Web.UI.WebControls.ListItem item in lstDesig.Items)
        {
            if (item.Selected)
            {
                sdesig += item.Value + ",";
            }
        }

     
            dsSalesForce = sal.getfield_notifydesig(div_code,sdesig);
        

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            lnk2.Visible = true;
            txtmsg.Visible = true;
            Span3.Visible = false;
            lbleffeFrom.Visible = true;
            txtEffFrom.Visible = true;
            lblEffTo.Visible = true;
            txtEffTo.Visible = true;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            lnk2.Visible = false;
            txtmsg.Visible = false;
            Span3.Visible = false;
            lbleffeFrom.Visible = false;
            txtEffFrom.Visible = false;
            lblEffTo.Visible = false;
            txtEffTo.Visible = false;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
    }
    private void fillgridstate()
    {
         SalesForce sal = new SalesForce();
         foreach (System.Web.UI.WebControls.ListItem item in lstState.Items)
         {
             if (item.Selected)
             {

                 sStatename += item.Value + ",";

             }
         }
         
        
            dsSalesForce = sal.getfield_notifystate(div_code, sStatename);
        

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            lnk2.Visible = true;
            txtmsg.Visible = true;
            Span3.Visible = true;
            lbleffeFrom.Visible = true;
            txtEffFrom.Visible = true;
            lblEffTo.Visible = true;
            txtEffTo.Visible = true;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            lnk2.Visible = false;
            txtmsg.Visible = false;
            Span3.Visible = false;
            lbleffeFrom.Visible = false;
            txtEffFrom.Visible = false;
            lblEffTo.Visible = false;
            txtEffTo.Visible = false;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
    }
   
    private void fillgridsubdiv()
    {
         SalesForce sal = new SalesForce();
         foreach (System.Web.UI.WebControls.ListItem item in lstsubdiv.Items)
         {
             if (item.Selected)
             {

                 sSubdiv += item.Value + ",";

             }
         }
      
            dsSalesForce = sal.getfield_notifysub(div_code, sSubdiv);
        

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            lnk2.Visible = true;
            txtmsg.Visible = true;
            Span3.Visible = true;
            lbleffeFrom.Visible = true;
            txtEffFrom.Visible = true;
            lblEffTo.Visible = true;
            txtEffTo.Visible = true;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            lnk2.Visible = false;
            txtmsg.Visible = false;
            Span3.Visible = false;
            lbleffeFrom.Visible = false;
            txtEffFrom.Visible = false;
            lblEffTo.Visible = false;
            txtEffTo.Visible = false;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
    }
    protected void btnlink_click(object sender, EventArgs e)
    {
        FillgridColor();
    }
    private void fillgridfieldforce()
    {
        SalesForce sal = new SalesForce();
        //foreach (System.Web.UI.WebControls.ListItem item in lstfieldforce.Items)
        //{
        //    if (item.Selected)
        //    {

        //        field += item.Value + ",";

        //    }
        //}

     
            dsSalesForce = sal.getfield_notifyfieldall(div_code, lstfieldforce.SelectedValue);
        
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            btnSubmit.Visible = true;
            lnk2.Visible = true;
            txtmsg.Visible = true;
            Span3.Visible = true;
            lbleffeFrom.Visible = true;
            txtEffFrom.Visible = true;
            lblEffTo.Visible = true;
            txtEffTo.Visible = true;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }
        else
        {
            btnSubmit.Visible = false;
            lnk2.Visible = false;
            txtmsg.Visible = false;
            Span3.Visible = false;
            lbleffeFrom.Visible = false;
            txtEffFrom.Visible = false;
            lblEffTo.Visible = false;
            txtEffTo.Visible = false;
            grdnotify.DataSource = dsSalesForce;
            grdnotify.DataBind();
        }


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(time);
        string Trans_Sl_No = string.Empty;
        string SF_Code = string.Empty;
        string SF_Name = string.Empty;
        int iReturn = -1;

        SalesForce sf = new SalesForce();

        foreach (GridViewRow grid_row in grdnotify.Rows)
        {
            Label lblsf_Code = (Label)grid_row.FindControl("lblsf_Code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");

            if (chkSf.Checked)
            {
                SF_Code = SF_Code + lblsf_Code.Text.ToString() + ',';
                SF_Name = SF_Name + lblSf_Name.Text.ToString() + ',';
            }

        }
        txtmsg.Text = txtmsg.Text.Replace("'", "~");

        iReturn = sf.AddNotification(div_code, SF_Code, SF_Name, txtmsg.Text, Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Submitted Successfully');</script>");
        }
        FillgridColor(); 

        foreach (GridViewRow row in grdnotify.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
           
            txtmsg.Text = string.Empty;
        }
        txtEffFrom.Text = "";
        txtEffTo.Text = "";

    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/MasterFiles/Options/Mob_App_Setting.aspx");
        Response.Redirect("~/MasterFiles/Options/Audit_setup.aspx");
    }
    protected void lnk2_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Options/Mob_App_Setting2.aspx");

    }
    protected void lnk3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Options/Mob_App_Setting3.aspx");

    }
    protected void btn_goclick(object sender, EventArgs e)
    {
        if (ddlwise.SelectedValue == "0")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Filter by');</script>");
        }
      
        if (ddlwise.SelectedValue == "1")
        {
            fillgriddesig();
           
        }
        else if (ddlwise.SelectedValue == "2")
        {
            fillgridstate();
            
        }
        else if (ddlwise.SelectedValue == "3")
        {
            fillgridsubdiv();
           
        }
        else if (ddlwise.SelectedValue == "4")
        {
            fillgridfieldforce();
           
        }
       
        FillgridColor();
        txtEffFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtEffTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
    }
}