using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Designation_SlNo : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDesignation = null;
    int Designation_Code = 0;
    string Designation_Short_Name = string.Empty;
    string Designation_Name = string.Empty;
    string Design_Code = string.Empty;
    string txtSlNo = string.Empty;
    string txtBaseSlNo = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    int time;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = this.Page.Title;
       // //// menu1.FindControl("btnBack").Visible = false;
        ServerStartTime = DateTime.Now;
        base.OnPreInit(e);
        Session["backurl"] = "Designation.aspx";
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            Filldiv();
            ddlDivision.SelectedIndex = 0;
        FillBaseleve();
        FillManagers();
        }

    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }

    protected void grdBaselevel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ChkTagged = (CheckBox)e.Row.FindControl("ChkTagged");
            DataRowView drv = (DataRowView)e.Row.DataItem;

            // Example: If column "IsChecked" = 1, mark checked
            if (drv["Tag_Needed"] != DBNull.Value && Convert.ToInt32(drv["Tag_Needed"]) == 1)
            {
                ChkTagged.Checked = true;
            }
        }
    }


    protected void grdmanager_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ChkTagged = (CheckBox)e.Row.FindControl("ChkTagged");
            DataRowView drv = (DataRowView)e.Row.DataItem;

            // Example: If column "IsChecked" = 1, mark checked
            if (drv["Tag_Needed"] != DBNull.Value && Convert.ToInt32(drv["Tag_Needed"]) == 1)
            {
                ChkTagged.Checked = true;
            }
        }
    }


    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }
    }
    private void FillBaseleve()
    {
        Designation des = new Designation();
       //dsDesignation = des.getDesign_Baselevel(ddlDivision.SelectedValue.ToString());

        dsDesignation = des.getDesign_Baselevel_new(ddlDivision.SelectedValue.ToString()); //added tagneed
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            divid.Visible = false;
            grdBaselevel.DataSource = dsDesignation;
            grdBaselevel.DataBind();
        }
        else
        {
            divid.Visible = true;
            grdBaselevel.DataSource = dsDesignation;
            grdBaselevel.DataBind();
        }
    }
    private void FillManagers()
    {
        Designation des = new Designation();
      //  dsDesignation = des.getDesign_Managerlevel(ddlDivision.SelectedValue.ToString());
        dsDesignation = des.getDesign_Managerlevel_new(ddlDivision.SelectedValue.ToString());

        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            divid1.Visible = false;
            grdmanager.DataSource = dsDesignation;
            grdmanager.DataBind();
        }
        else
        {
            divid1.Visible = true;
            grdmanager.DataSource = dsDesignation;
            grdmanager.DataBind();
        }
             
    }
  
    protected void btnbase_Click(object sender, EventArgs e)
    {
        string ChkTag = string.Empty;

        System.Threading.Thread.Sleep(time);
        // Save
        foreach (GridViewRow gridRow in grdBaselevel.Rows)
        {
            TextBox txtBasno = (TextBox)gridRow.Cells[3].FindControl("txtBaseSlNo");
           // txtBaseSlNo = txtBaseSlno.Text;

            Label Designation_Code = (Label)gridRow.Cells[0].FindControl("lblDesign_code");
            Design_Code = Designation_Code.Text;

            CheckBox ChkTagged = (CheckBox)gridRow.Cells[0].FindControl("ChkTagged");
            HiddenField HdnTag_Needed = (HiddenField)gridRow.Cells[0].FindControl("Tag_Needed");


            if (ChkTagged.Checked)
            {
                ChkTag = "1"; // Checkbox is checked (Yes)
            }
            else
            {
                ChkTag = "0"; // Checkbox is unchecked (No)
            }

            // Update Division
            Designation des = new Designation();
          //  int iReturn = des.Update_BaselevelSno(Design_Code, txtBasno.Text, ddlDivision.SelectedValue.ToString());

            int iReturn = des.Update_BaselevelSno_new(Design_Code, txtBasno.Text, ddlDivision.SelectedValue.ToString(), ChkTag);//add 25-10-25

            if (iReturn > 0)
            {
               
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "SlNo could not be updated!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
            }
        }
    }
    protected void btnManager_Click(object sender, EventArgs e)
    {
        string ChkTag = string.Empty;

        System.Threading.Thread.Sleep(time);
        // Save
        foreach (GridViewRow gridRow in grdmanager.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[3].FindControl("txtManSlNo");
           

            Label Designation_Code = (Label)gridRow.Cells[0].FindControl("Des_Code");
            Design_Code = Designation_Code.Text;

            CheckBox ChkTagged = (CheckBox)gridRow.Cells[0].FindControl("ChkTagged");
            HiddenField HdnTag_Needed = (HiddenField)gridRow.Cells[0].FindControl("Tag_Needed");



            
            if (ChkTagged.Checked)
            {
                ChkTag = "1"; // Checkbox is checked (Yes)
            }
            else
            {
                ChkTag = "0"; // Checkbox is unchecked (No)
            }



            Designation des = new Designation();
          //  int iReturn = des.Update_ManagerSno(Design_Code, txtSNo.Text, ddlDivision.SelectedValue.ToString()); //cmd 25-10-25

            int iReturn = des.Update_ManagerSno_new(Design_Code, txtSNo.Text, ddlDivision.SelectedValue.ToString(), ChkTag);//add 25-10-25

            if (iReturn > 0)
            {
                //menu1.Status = "Sl No Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');</script>");
            }
            else if (iReturn == -2)
            {
                //  menu1.Status = "SlNo could not be updated!!";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('SlNo could not be updated');</script>");
            }
        }
    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillBaseleve();
        FillManagers();
    }
   
    protected void btnback_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("Designation.aspx");
    }
}