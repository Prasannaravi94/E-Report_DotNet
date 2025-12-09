using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_Reports_DCRCount_frmDCRCountStateView : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsUserList = new DataSet();
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    int StrTotal = 0;
    int iFieldTotal = 0;
    string sf_code = string.Empty;
    string sf_type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            sf_code = Session["sf_code"].ToString();
            sf_type = Session["sf_type"].ToString();
            heading.InnerText = this.Page.Title;
            if (sf_type == "3")
            {
                div_code = Session["division_code"].ToString();
            }
            else
            {
                div_code = Session["div_code"].ToString();
            }
            Product prd = new Product();
            if (!Page.IsPostBack)
            {
                DataSet dsTP = new DataSet();
                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                Filldiv();
                if (Session["sf_type"].ToString() == "2")
                {
                    dsdiv = prd.getMultiDivsf_Name(sf_code);
                    if (dsdiv.Tables[0].Rows.Count > 0)
                    {
                        if (dsdiv.Tables[0].Rows[0]["IsMultiDivision"].ToString() == "1")
                        {
                            strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
                            ddlDivision.Visible = true;
                            lblDivision.Visible = true;
                            getDivision();
                        }
                        else
                        {
                            ddlDivision.Visible = false;
                            lblDivision.Visible = false;
                        }
                    }
                }
                BindDate();
            }

           
            btnSubmit.Focus();

            if (Session["sf_type"].ToString() == "1")
            {
                UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                UserControl_pnlMenu c1 =
                    (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
                Divid.Controls.Add(c1);
                //c1.FindControl("btnBack").Visible = false;
                c1.Title = Page.Title;
            }
            else if (Session["sf_type"].ToString() == "2")
            {
                UserControl_MGR_Menu c1 =
                (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.FindControl("btnBack").Visible = false;
                c1.Title = Page.Title;
            }
        }
        catch (Exception ex)
        {

        }

        
    }

    private void Filldiv()
        {
            Division dv = new Division();
            if (sf_type == "3")
            {
                string[] strDivSplit = div_code.Split(',');
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
                dsDivision = dv.getDivision_Name(div_code);
                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    ddlDivision.DataTextField = "Division_Name";
                    ddlDivision.DataValueField = "Division_Code";
                    ddlDivision.DataSource = dsDivision;
                    ddlDivision.DataBind();
                }
            }
        }

    private void getDivision()
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        dsDivision = dv.getMultiDivision(strMultiDiv);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            ddlDivision.DataTextField = "Division_Name";
            ddlDivision.DataValueField = "Division_Code";
            ddlDivision.DataSource = dsDivision;
            ddlDivision.DataBind();
        }
    }

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue.ToString());
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                //ddlYear.Items.Add(k.ToString());
            }

            //ddlYear.Text = DateTime.Now.Year.ToString();
            //ddlMonth.Text = DateTime.Now.Month.ToString();

            //ddlMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            DateTime FromMonth = DateTime.Now;
            txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
            int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        SalesForce sf = new SalesForce();
        DataSet ds = new DataSet();
        string strVacant;
        
        if (chkWOVacant.Checked)
        {
            strVacant = "1";
        }
        else
        {
            strVacant = "0";
        }
        div_code = ddlDivision.SelectedValue;
        ds = sf.sp_Get_DCRCount_StateWise(div_code, MonthVal.ToString(), YearVal.ToString(), strVacant);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblHead.Text = "DCR Count View - State Wise";

            GvDcrCount.DataSource = ds;
            GvDcrCount.DataBind();
        }
        else
        {
            lblHead.Visible = false;
        }
      

        }
        catch (Exception ex)
        {
           Response.Write(ex.Message);
        }
    }

    protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hypDCRCount = (HyperLink)e.Row.FindControl("hypDCRCount");
            if (hypDCRCount.Text != "" && hypDCRCount.Text != "0")
            {
                StrTotal += Convert.ToInt16(hypDCRCount.Text);
            }
            

            Label lblFieldCount = (Label)e.Row.FindControl("lblFieldCount");
            if (hypDCRCount.Text != "")
            {
                iFieldTotal += Convert.ToInt16(lblFieldCount.Text);
            }
            

            Label lblVacant = (Label)e.Row.FindControl("lblVacant");

            if (chkWOVacant.Checked)
            {
                lblVacant.Text = "1";
            }
            else
            {
                lblVacant.Text = "0";
            }


        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = (Label)e.Row.FindControl("lblTotalCount");
            if (StrTotal != 0)
            {
                lblTotal.Text = StrTotal.ToString();
            }

            Label FlblFieldCount = (Label)e.Row.FindControl("FlblFieldCount");
            if (iFieldTotal != 0)
            {
                FlblFieldCount.Text = iFieldTotal.ToString();
            }
        }
    }
}