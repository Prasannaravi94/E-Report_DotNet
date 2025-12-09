using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows;
using System.IO;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using Bus_EReport;
using System.Net;
using System.Data.SqlClient;
using DBase_EReport;
public partial class MIS_Reports_rptPersonal_Details_Status : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string sf_name = string.Empty;
    string SName = string.Empty;
    DataSet dsCheck = null;
    string detail = string.Empty;
    string div_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"];
        sf_name = Request.QueryString["sf_name"];
        SName =Request.QueryString["SName"];
        detail = Request.QueryString["detail"];
        div_code = Session["div_code"].ToString();

        fillinfo();
        lblRegionName.Text = SName;

    }

    private void fillinfo()
    {

        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();

        if (detail == "0")
        {

            strQry = "Select convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date,sf_hq,sf_designation_short_name,sf_emp_id,state,Employee_ID,Trans_slno,Permanent_Addr,Present_Addr,case when SF_DOB='1900-01-01' then '' else convert(varchar,sf_Dob,103) end as SF_DOB ,case when sf_DOW='1900-01-01' then '' else convert(varchar,sf_DOW,103) end as sf_DOW,sf_email, " +
                     " Mob_No,Residential_No,Emerg_Contact_No,PAN_No,Aadhar_No from salesforce_personal_info where sf_code='" + sf_code + "' and sf_name='" + sf_name.Trim() + "' ";
            dsCheck = db_ER.Exec_DataSet(strQry);

            if (dsCheck.Tables[0].Rows.Count > 0)
            {
                txtEmpl_name.Text = sf_name;
                txtDOJ.Text = dsCheck.Tables[0].Rows[0]["Sf_Joining_Date"].ToString();
                txtHq.Text = dsCheck.Tables[0].Rows[0]["sf_hq"].ToString();
                txtdesig.Text = dsCheck.Tables[0].Rows[0]["sf_designation_short_name"].ToString();
                txtemp_code.Text = dsCheck.Tables[0].Rows[0]["sf_emp_id"].ToString();
                txtstate.Text = dsCheck.Tables[0].Rows[0]["state"].ToString();
                txtperm_addr.Text = dsCheck.Tables[0].Rows[0]["Permanent_Addr"].ToString();
                txtpres_addr.Text = dsCheck.Tables[0].Rows[0]["Present_Addr"].ToString();
                txtDOB.Text = dsCheck.Tables[0].Rows[0]["sf_DOB"].ToString();
                // txtDOW.Text = dsCheck.Tables[0].Rows[0]["sf_DOW"].ToString();
                txtemail.Text = dsCheck.Tables[0].Rows[0]["sf_email"].ToString();
                txtmob_no.Text = dsCheck.Tables[0].Rows[0]["Mob_No"].ToString();
                txtResi_No.Text = dsCheck.Tables[0].Rows[0]["Residential_No"].ToString();
                txtemerg_contact.Text = dsCheck.Tables[0].Rows[0]["Emerg_Contact_No"].ToString();
                //txtpan_no.Text = dsCheck.Tables[0].Rows[0]["PAN_No"].ToString();
                //txtaadhar.Text = dsCheck.Tables[0].Rows[0]["Aadhar_No"].ToString();

                // ViewState["Trans_slno"] = dsCheck.Tables[0].Rows[0]["Trans_slno"].ToString();
            }
            else
            {
                pnl2.Visible = false;
                lblnorecord.Visible = true;
                lblnorecord.Text = " No Personal Detail for" + " <span style=color:blue>" + sf_name + "</span>";


            }
        }

        else
        {
            pnl2.Visible = false;
            strQry = "EXEC Persal_detl_Status '" + div_code + "','" + sf_code + "'";
            dsCheck = db_ER.Exec_DataSet(strQry);

            if (dsCheck.Tables[0].Rows.Count > 0)
            {
                grdPersonal.Visible = true;
                grdPersonal.DataSource = dsCheck;
                grdPersonal.DataBind();
            }
            else
            {
                grdPersonal.DataSource = dsCheck;
                grdPersonal.DataBind();
            }
            FillgridColor();
        }
    }
    private void FillgridColor()
    {

        foreach (GridViewRow grid_row in grdPersonal.Rows)
        {

            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            //grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
            grid_row.ForeColor = System.Drawing.Color.FromName(bcolor);


        }
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnl;
        Control ctrl = (Control)Session["ctrl"];
        PrintWebControl(ctrl);
    }
    public static void PrintWebControl(Control ControlToPrint)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ControlToPrint is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage);
            ((WebControl)ControlToPrint).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        HtmlForm frm = new HtmlForm();
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ControlToPrint);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();

    }


    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
}