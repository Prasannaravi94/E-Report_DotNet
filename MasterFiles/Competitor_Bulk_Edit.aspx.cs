using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Reflection;
using DBase_EReport;

public partial class MasterFiles_Competitor_Bulk_Edit : System.Web.UI.Page
{

    #region "Declaration"
    string divcode = string.Empty;
    string Comp_Sl_No = string.Empty;
    string Comp_Prd_Sl_No = string.Empty;
    string type = string.Empty;
    string Sl_No = string.Empty;
    string Comp_Name = string.Empty;
    string Comp_Prd_Name = string.Empty;
    string Comp_Prd_name_Mapp = string.Empty;
    string Comp_Name_Mapp_id = string.Empty;
    int Comp_SlNo = 0;
    int Comp_Prd_SlNo = 0;
    int SlNo = 0;
    string Our_Prd_Code = string.Empty;

    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsCom = null;
    DataSet dsCom_prd = null;
    string strQry = string.Empty;
    DB_EReporting db = new DB_EReporting();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
        }

    }
    protected void ddlcompe_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_Comp();
    }

    private void Fill_Comp()
    {
      
     
        
            DB_EReporting db_ER = new DB_EReporting();

            if (ddltype.SelectedValue == "1")
            {
                grdCampet.Columns[2].HeaderText = "Competitor Name";

                strQry = " SELECT Comp_Sl_No,Comp_Name FROM  Mas_Competitor " +
                           " WHERE Active_Flag=0 AND Division_Code=  '" + divcode + "'";

                dsCom = db_ER.Exec_DataSet(strQry);

                if (dsCom.Tables[0].Rows.Count > 0)
                {
                    grdCampet.Visible = true;
                    grdCampet.DataSource = dsCom;
                    grdCampet.DataBind();
                }
                else
                {
                    grdCampet.DataSource = dsCom;
                    grdCampet.DataBind();
                }
            }
            else if (ddltype.SelectedValue == "2")
            {
                grdCampet.Columns[2].HeaderText = "Competitor-Product Name";

                strQry = " SELECT Comp_Prd_Sl_No as Comp_Sl_No,Comp_Prd_Name as Comp_Name FROM  Mas_Competitor_Product " +
               " WHERE Active_Flag=0 AND Division_Code=  '" + divcode + "'";

                dsCom_prd = db_ER.Exec_DataSet(strQry);

                if (dsCom_prd.Tables[0].Rows.Count > 0)
                {
                    grdCampet.Visible = true;
                    grdCampet.DataSource = dsCom_prd;
                    grdCampet.DataBind();
                }
                else
                {
                    grdCampet.DataSource = dsCom_prd;
                    grdCampet.DataBind();
                }
            }

          
    
            
        

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int iReturn = -1;

        if (ddltype.SelectedValue == "1")
        {

            foreach (GridViewRow gridRow in grdCampet.Rows)
            {
                Label lblComp_Sl_No = (Label)gridRow.FindControl("lblComp_Sl_No");

                TextBox txtComp_Name = (TextBox)gridRow.FindControl("txtComp_Name");

                if (lblComp_Sl_No.Text != "")
                {


                    strQry = "update Mas_Competitor set Comp_Name='" + txtComp_Name.Text + "' where Division_Code='" + divcode + "' and Comp_Sl_No='" + lblComp_Sl_No.Text + "'";
                    iReturn = db.ExecQry(strQry);
                }


                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");

                    grdCampet.Visible = false;
                }

            }
        }

        else if (ddltype.SelectedValue == "2")
        {

            foreach (GridViewRow gridRow in grdCampet.Rows)
            {
                Label lblComp_Sl_No = (Label)gridRow.FindControl("lblComp_Sl_No");

                TextBox txtComp_Name = (TextBox)gridRow.FindControl("txtComp_Name");

                if (lblComp_Sl_No.Text != "")
                {


                    strQry = "update Mas_Competitor_Product set Comp_Prd_Name='" + txtComp_Name.Text + "' where Division_Code='" + divcode + "' and Comp_Prd_Sl_No='" + lblComp_Sl_No.Text + "'";
                    iReturn = db.ExecQry(strQry);
                }


                if (iReturn > 0)
                {
                    //Response.Write("DCR Edit Dates have been created successfully");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Released Dates successfully');</script>");

                    grdCampet.Visible = false;
                }

            }
        }
    }
}