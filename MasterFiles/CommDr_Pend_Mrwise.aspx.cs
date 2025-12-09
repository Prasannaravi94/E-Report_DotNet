using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_CommDr_Pend_Mrwise : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    DataSet dsPend = null;
    DB_EReporting db_ER = new DB_EReporting();
    string strQry = string.Empty;
    int Tot_ListDr = 0;
    int Exisiting_Dr_Approval_Count = 0;
    int New_Dr_Approval_Count = 0;
    int total_appr_pen_count;


    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {

            fillPending_count();
          
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

              

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;
              

            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

           

            }

        }

        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                // c1.FindControl("btnBack").Visible = false;

            }


        }


    }

    private void fillPending_count()
    {

        strQry = "EXEC CommonDr_approvalpenTot '"+div_code+"','"+sf_code+"' ";
        dsPend = db_ER.Exec_DataSet(strQry);

        if (dsPend.Tables[0].Rows.Count > 0)
        {
            grdPenDr.DataSource = dsPend;
            grdPenDr.DataBind();
        }
        else
        {
            grdPenDr.DataSource = dsPend;
            grdPenDr.DataBind();
        }
    }

    protected void grdPenDr_Rowdatabound(object sender, GridViewRowEventArgs e)
    {

      

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            Label lbtot_count = (Label)e.Row.FindControl("lbtot_count");
            Label lblpend_exist = (Label)e.Row.FindControl("lblpend_exist");
            Label lblpend_new = (Label)e.Row.FindControl("lblpend_new");
            Label lblpend_tot = (Label)e.Row.FindControl("lblpend_tot");


            Tot_ListDr +=Convert.ToInt32(lbtot_count.Text);

            Exisiting_Dr_Approval_Count += Convert.ToInt32(lblpend_exist.Text);
            New_Dr_Approval_Count += Convert.ToInt32(lblpend_new.Text);
            total_appr_pen_count += Convert.ToInt32(lblpend_tot.Text);


            if (lblpend_new.Text == "0")
            {
                lblpend_new.Text = "";
            }

            if (lblpend_exist.Text == "0")
            {
                lblpend_exist.Text = "";
            }
            if (lblpend_tot.Text == "0")
            {
                lblpend_tot.Text = "";
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTot_dr = (Label)e.Row.FindControl("lblTot_dr");
            Label lblExisiting_Dr = (Label)e.Row.FindControl("lblExisiting_Dr");
            Label lblNew_Dr = (Label)e.Row.FindControl("lblNew_Dr");
            Label lbltot_appcnt = (Label)e.Row.FindControl("lbltot_appcnt");


            lblTot_dr.Text = Tot_ListDr.ToString();
            lblExisiting_Dr.Text = Exisiting_Dr_Approval_Count.ToString();
            lblNew_Dr.Text = New_Dr_Approval_Count.ToString();
            lbltot_appcnt.Text = total_appr_pen_count.ToString();

        }
    }

}