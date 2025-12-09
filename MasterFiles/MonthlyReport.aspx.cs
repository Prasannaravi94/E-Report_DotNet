using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_MonthlyReport : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsts = new DataSet();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
        }
        catch
        {
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
        }
        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {

                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                ddlFieldForce.SelectedValue = sf_code;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        
    }
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int i = dsSalesForce.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dsSalesForce.Tables[0].Rows[i];
                if (dr["sf_code"].ToString() == "admin")
                    dr.Delete();
            }

            dsSalesForce.AcceptChanges();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "Monthly_Report";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Mnth", ddlMonth.SelectedValue);
        cmd.Parameters.AddWithValue("@Year", ddlYear.SelectedValue);
        cmd.Parameters.AddWithValue("@Sf_Code", ddlFieldForce.SelectedValue);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        if (dsts.Tables[1].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsts.Tables[1].Rows[0]["sf_name"];
            lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsts.Tables[1].Rows[0]["sf_hq"];
            lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsts.Tables[1].Rows[0]["sf_Designation_Short_Name"];
            lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsts.Tables[1].Rows[0]["sf_emp_id"];
            lblDivision.Text = "<b style='color:#245884'>Division:</b>  " + dsts.Tables[1].Rows[0]["Division_Name"];
            lblMonYr.Text= "<b style='color:#245884'>Month & Year:</b>  " + ddlMonth.SelectedItem.Text+"/ "+ddlYear.SelectedItem.Text;
            lblmsg.Visible = true;
        }
        GrdTimeSt.DataSource = dtrowClr;
        GrdTimeSt.DataBind();
    }
    #endregion
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }

    protected void GrdTimeSt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            for (int j = 3; j < e.Row.Cells.Count - 1; j++)
            {
                HiddenField hdnActivity_Date = (HiddenField)e.Row.FindControl("hdnActivity_Date");
                if (j == 3 && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                {
                    HyperLink hLink = (HyperLink)e.Row.FindControl("linkDrCnt");
                    hLink.Attributes.Add("href", "javascript:showMonthSummaryZoom('" + ddlFieldForce.SelectedValue + "','" + ddlFieldForce.SelectedItem.Text + "','" + hdnActivity_Date.Value + "','" + div_code + "','1')");
                    hLink.Style.Add("text-decoration", "none");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[j].Controls.Add(hLink);
                }
                else if (j == 4 && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                {
                    HyperLink hLink = (HyperLink)e.Row.FindControl("linkChmCnt");
                    hLink.Attributes.Add("href", "javascript:showMonthSummaryZoom('" + ddlFieldForce.SelectedValue + "','" + ddlFieldForce.SelectedItem.Text + "','" + hdnActivity_Date.Value + "','" + div_code + "','2')");
                    hLink.Style.Add("text-decoration", "none");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[j].Controls.Add(hLink);
                }
                else if (j == 5 && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                {
                    HyperLink hLink = (HyperLink)e.Row.FindControl("linkstkCnt");
                    hLink.Attributes.Add("href", "javascript:showMonthSummaryZoom('" + ddlFieldForce.SelectedValue + "','" + ddlFieldForce.SelectedItem.Text + "','" + hdnActivity_Date.Value + "','"+div_code+"','3')");
                    hLink.Style.Add("text-decoration", "none");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[j].Controls.Add(hLink);
                }
                else if (j == 6 && e.Row.Cells[j].Text != "0" && e.Row.Cells[j].Text != "-")
                {
                    HyperLink hLink = (HyperLink)e.Row.FindControl("linkunliedDrCnt");
                    hLink.Attributes.Add("href", "javascript:showMonthSummaryZoom('" + ddlFieldForce.SelectedValue + "','" + ddlFieldForce.SelectedItem.Text + "','" + hdnActivity_Date.Value + "','" + div_code + "','4')");
                    hLink.Style.Add("text-decoration", "none");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[j].Controls.Add(hLink);
                }
            }
        }
    }
}