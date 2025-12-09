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

public partial class MIS_Reports_frmDTENotSubmittedDays : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsSf = new DataSet();
    SalesForce sf1 = new SalesForce();
    DataSet dsdiv = new DataSet();
    DataSet dsDivision = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
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
            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                Filldiv();

                TourPlan tp = new TourPlan();
                DataSet dsTP = new DataSet();
                dsTP = tp.Get_TP_Edit_Year(ddlDivision.SelectedValue);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        //ddlYear.Items.Add(k.ToString());
                        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    }

                    //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                    DateTime FromMonth = DateTime.Now;
                    txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
                }

                DataSet dsmgrsf = new DataSet();
                SalesForce sf = new SalesForce();
                DataSet DsAudit = sf.SF_Hierarchy(ddlDivision.SelectedValue, sf_code);
                if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
                {
                    if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
                    {
                        FillManagers();
                        FillColor();
                    }
                }

            }
            if (Session["sf_type"].ToString() == "2")
            {
                ddlDivision.Visible = false;
                lblDivision.Visible = false;
                TourPlan tp = new TourPlan();
                DataSet dsTP = new DataSet();
                dsTP = tp.Get_TP_Edit_Year(div_code);
                if (dsTP.Tables[0].Rows.Count > 0)
                {
                    for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                    {
                        //ddlYear.Items.Add(k.ToString());
                        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                    }

                    //ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                    DateTime FromMonth = DateTime.Now;
                    txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();

                }
                FillManagers();
                FillColor();
            }
        }
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            UserControl_pnlMenu c1 =
            (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            c1.Title = "Not Submitted Analysis";
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            c1.Title = "Not Submitted Analysis";


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
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
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

    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }



    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        if (Session["sf_type"].ToString() == "2")
        {
            ddlFFType.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
        }
        else
        {
            if (ddlFFType.SelectedValue.ToString() == "1")
            {
                ddlAlpha.Visible = false;
                //dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
                dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue, "admin");

            }
            else if (ddlFFType.SelectedValue.ToString() == "0")
            {
                //FillSF_Alpha();
                ddlAlpha.Visible = true;
                dsSalesForce = sf.UserListTP_Alpha(ddlDivision.SelectedValue, "admin");
            }
            else if (ddlFFType.SelectedValue.ToString() == "2")
            {
                dsSalesForce = sf.UserList_HQ(ddlDivision.SelectedValue, "admin");
            }
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);

        //objtablecell.Style.Add("color", "#636d73");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdDoctor_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#414D55", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp_Code", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Desig", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Joining_Date", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Resigned_Date", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "State_Name", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting_Manager1", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting_Manager2", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "DCR Not Submitted Days", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "TP Not Submitted</br>(Y/N)", "#F1F5F8", true);
            AddMergedCells(objgridviewrow, objtablecell, 3, "Expense Status", "#F1F5F8", true);


            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();


            AddMergedCells(objgridviewrow2, objtablecell2, 0, "MR", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "MGR", "#F1F5F8", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Admin", "#F1F5F8", false);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }

    protected void GrdDoctor_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        string strMR = "";
        string strMGR = "";
        string strAdmin = "";
        string strTP_Not_Submit = "";
        string strDCR = "";
        string strFieldforce = "";
        string strSF_Code = "";
        DateTime strDcrDate;
        DateTime strJoinDate;
        DateTime LastDCRDate;

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Desig_color")));

            strDcrDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "sf_TP_Active_Dt").ToString());
            strFieldforce = DataBinder.Eval(e.Row.DataItem, "sf_name").ToString();
            Label lblSf = (Label)e.Row.FindControl("lblSf");
            strSF_Code = DataBinder.Eval(e.Row.DataItem, "Sf_Code").ToString();
            strJoinDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "sf_joining_date").ToString());
            LastDCRDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Last_DCR_Date").ToString());
            Label lblResigned_Date = (Label)e.Row.FindControl("lblResigned_Date");

            dsSf = sf1.CheckSFNameVacant_Temp(strSF_Code, Convert.ToInt16(MonthVal.ToString()), Convert.ToInt16(YearVal.ToString()));
            //Label lblSf_Name = (Label)e.Row.FindControl("lblSf_Name");
            if (lblSf.Text != "")
            {
                if (dsSf.Tables[0].Rows.Count > 0)
                {
                    string[] str = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

                    if (str.Count() >= 2)
                    {
                        if ("( " + str[0].Trim() + " )" != str[1].Trim())
                        {
                            lblSf.Text = str[0] + "<span style='color: red;'>" + str[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                            lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                        }
                        else
                        {
                            lblSf.Text = str[0];
                        }
                    }
                    else
                    {
                        lblSf.Text = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
                        if (dsSf.Tables[0].Rows[0][1].ToString() != "")
                        {
                            lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                        }
                    }

                    if (dsSf.Tables[0].Rows[0][2].ToString() != "")
                    {
                        lblSf.Text = "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";
                        lblResigned_Date.Text = dsSf.Tables[0].Rows[0][4].ToString();
                    }
                }
                else
                {
                    if (LastDCRDate.Month == strJoinDate.Month && strJoinDate.Year == LastDCRDate.Year)
                    {
                        if (LastDCRDate.Month > Convert.ToInt16(MonthVal.ToString()) && LastDCRDate.Year >= Convert.ToInt16(YearVal.ToString()))
                        {
                            lblSf.Text = "<span style='color: red;'> Vacant </span>";
                        }
                    }
                    else
                    {
                        lblSf.Text = lblSf.Text;
                    }
                }
            }

            //if (ddlMonth.SelectedValue + "/" + ddlYear.SelectedValue == strDcrDate.Month.ToString() + "/" + strDcrDate.Year)
            //{
            //    lblSf.Text = strFieldforce + "<span style='color:Red'> (  New Join ) </span>";
            //}


            strDCR = DataBinder.Eval(e.Row.DataItem, "DCR_Not_Submit").ToString();
            Label lblDCR_Not_Submit = (Label)e.Row.FindControl("lblDCR_Not_Submit");
            if (strDCR.Contains('-'))
            {
                lblDCR_Not_Submit.Text = strDCR.Remove(0, 1);
            }
            lblDCR_Not_Submit.ToolTip = "DCR";

            strTP_Not_Submit = DataBinder.Eval(e.Row.DataItem, "TP_Not_Submit").ToString();
            Label lblimgTP_Not_Submit = (Label)e.Row.FindControl("imgTP_Not_Submit");
            if (strTP_Not_Submit != "0" && strTP_Not_Submit != "")
            {
                lblimgTP_Not_Submit.Visible = true;
                lblimgTP_Not_Submit.ToolTip = "TP";
            }
            else
            {
                lblimgTP_Not_Submit.Visible = true;
                lblimgTP_Not_Submit.Text = "N";
                lblimgTP_Not_Submit.ForeColor = System.Drawing.Color.Red;
                lblimgTP_Not_Submit.ToolTip = "TP";
            }

            strMR = DataBinder.Eval(e.Row.DataItem, "MR").ToString();

            Label lblMR = (Label)e.Row.FindControl("imgAdmin1");
            Label lblMGR = (Label)e.Row.FindControl("imgAdmin2");
            Label lblAdmin_Mgr = (Label)e.Row.FindControl("imgAdmin3");
            //lblMGR.Text = "✔";
            if (strMR != "")
            {
                lblMR.ToolTip = "MR";
                lblMGR.Text = "N";
                lblMGR.ForeColor = System.Drawing.Color.Red;
                lblMR.Text = "Y";
                lblAdmin_Mgr.Text = "-";
            }


            strMGR = DataBinder.Eval(e.Row.DataItem, "MGR").ToString();

            if (strMGR != "" && strMGR != null)
            {
                lblAdmin_Mgr.Text = "N";
                lblAdmin_Mgr.ForeColor = System.Drawing.Color.Red;
                lblMR.Text = "Y";
                lblMGR.Text = "Y";


            }

            strAdmin = DataBinder.Eval(e.Row.DataItem, "Admin_Mgr").ToString();

            if (strAdmin != "")
            {
                lblAdmin_Mgr.Text = "Y";

                lblMR.Text = "Y";

                lblMGR.Text = "Y";

            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        if (Session["sf_type"].ToString() == "2")
        {
            dsSalesForce = sf.sp_get_Consolidate_Not_Submit_Days(ddlFieldForce.SelectedValue, div_code, MonthVal.ToString(), YearVal.ToString());
        }
        else
        {
            dsSalesForce = sf.sp_get_Consolidate_Not_Submit_Days(ddlFieldForce.SelectedValue, ddlDivision.SelectedValue, MonthVal.ToString(), YearVal.ToString());
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            div1.Visible = false;
            GrdDoctor.DataSource = dsSalesForce;
            GrdDoctor.DataBind();
            btnExcel.Visible = true;
        }
        else
        {
            div1.Visible = true;
            GrdDoctor.DataSource = null;
            GrdDoctor.DataBind();
            btnExcel.Visible = false;
        }
        FillColor();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Report.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        GrdDoctor.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(GrdDoctor);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
}