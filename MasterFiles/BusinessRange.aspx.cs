using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_BusinessRange : System.Web.UI.Page
{
    #region Declaration
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sCmd = string.Empty;
    string Feedback_Id = string.Empty;
    int Id = 0;
    DataSet dsdiv = new DataSet();
    DataSet dsFeedback = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        div_code = Convert.ToString(Session["div_code"]);

        if(sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else 
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "3")
            {
                FillRange();
                menu1.Title = this.Page.Title;
                menu1.FindControl("btnBack").Visible = false;
                ServerStartTime = DateTime.Now;
                base.OnPreInit(e);
            }
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }

    private void FillRange()
    {
        AdminSetup adm = new AdminSetup();
        dsFeedback = adm.getRange(div_code);
        if (dsFeedback.Tables[0].Rows.Count > 0)
        {
            btnaddnew.Visible = false;
            txtaddnew.Visible = false;
            txtaddnew1.Visible = false;
            btnUpdate.Visible = true;
            grdRange.DataSource = dsFeedback;
            grdRange.DataBind();
        }
          else
        {
            btnaddnew.Visible = true;
            txtaddnew.Text = string.Empty;
            txtaddnew.Visible = true;
            btnUpdate.Visible = false;
            grdRange.DataSource = dsFeedback;
            grdRange.DataBind();
        }
    }
    protected void grdFeedback_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void grdFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRange.PageIndex = e.NewPageIndex;
        sCmd = Session["GetcmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillRange();
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string slno = string.Empty;
        string Fromrange = string.Empty;
        string Torange = string.Empty;
        string Fromrange1 = string.Empty;
        string Torange1 = string.Empty;
        string Name = string.Empty;

        AdminSetup adm = new AdminSetup();

        foreach (GridViewRow gridRow in grdRange.Rows)
        {
            int iReturn = -1;
            AdminSetup ad = new AdminSetup();
            TextBox txt_fmrange = (TextBox)grdRange.FooterRow.FindControl("txt_fmrange");
            Fromrange1 = txt_fmrange.Text.ToString();
            TextBox txt_torange = (TextBox)grdRange.FooterRow.FindControl("txt_torange");
            Torange1 = txt_torange.Text.ToString();

            if (Fromrange1 == "")
            {

            }
            else
            {
                iReturn = adm.RangeAdd1(Fromrange1, Torange1, div_code);


                if (iReturn > 0)
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                    FillRange();
                }
            }

            Label lblno = (Label)gridRow.Cells[1].FindControl("lblno");
            slno = lblno.Text.ToString();
            TextBox txt_fmrange1 = (TextBox)gridRow.Cells[1].FindControl("txtfmRange");
            Fromrange = txt_fmrange1.Text.ToString();
            TextBox txt_torange1 = (TextBox)gridRow.Cells[2].FindControl("txttoRange");
            Torange = txt_torange1.Text.ToString();

            //Update

            iReturn = adm.RangeUpdate1(Convert.ToInt16(slno), Fromrange, Torange,div_code);

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                FillRange();
            }
        }
    }

    protected void grdFeedback_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        AdminSetup adm = new AdminSetup();

        TextBox txtfmRange = (TextBox)grdRange.FooterRow.FindControl("txt_fmrange");
        TextBox txttoRange = (TextBox)grdRange.FooterRow.FindControl("txt_torange");

        //if (txtfmRange.Text == "")
        //{
        //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Name');</script>");
        //    txtfmRange.Focus();
        //}
        //else
        //{
        if (txtfmRange.Text != "")
        {
            int iReturn = adm.RangeAdd1(txtfmRange.Text, txttoRange.Text, div_code);
       
            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                FillRange();
            }
        }
    }

    protected void grdFeedback_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Deactivate")
        {
            Feedback_Id = Convert.ToString(e.CommandArgument);

            //Deactivate
            AdminSetup adm = new AdminSetup();
            int iReturn = adm.FeedbackDelete(Feedback_Id);
            if (iReturn > 0)
            {
                
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Deleted Successfully.\');", true);
            }
            else
            {
                // menu1.Status ="Unable to Deactivate";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Unable to Deactivate.\');", true);
            }
            FillRange();
            
        }
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string slno = string.Empty;
        string Fromrange = string.Empty;
        Fromrange = txtaddnew.Text;
        string Torange = string.Empty;
        Torange = txtaddnew1.Text;

        if (slno == "")
        {
            AdminSetup adm = new AdminSetup();


            int iReturn = adm.RangeAdd1(Fromrange, Torange, div_code);

            if (iReturn > 0)
            {
                FillRange();
            }
        }
    }
    
}