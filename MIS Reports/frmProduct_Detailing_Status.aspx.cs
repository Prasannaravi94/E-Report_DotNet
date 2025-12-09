using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MIS_Reports_frmProduct_Detailing_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsSalesForce=new DataSet();
    DataSet dsDocDate = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();


        if (!Page.IsPostBack)
        {
            FillMRManagers();
            BindDate();
        }
        
        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MenuUserControl MUCtrl = new UserControl_MenuUserControl();
            Divid.Controls.Add(MUCtrl);
        }
        else if (Session["sf_type"].ToString() == "2")
        {

        }
        else
        {
            UserControl_MenuUserControl c1 =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
        }

        FillColor();
    }

    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.UserList_Hierarchy(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Des_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
            FillColor();

        }
    }

    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();

        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFrmYear.Items.Add(k.ToString());
                ddlToYear.Items.Add(k.ToString());
            }

            ddlFrmYear.Text = DateTime.Now.Year.ToString();
            ddlToYear.Text = DateTime.Now.Year.ToString();

            //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlFrmMonth.SelectedValue = DateTime.Now.Month.ToString();
            ddlToMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DCR dcr = new DCR();
        DataSet ds = new DataSet();

        dsSalesForce = dcr.GetDCR_View_All_Dates_Detailing(ddlFieldForce.SelectedValue, div_code, ddlFrmMonth.SelectedValue, ddlToMonth.SelectedValue, ddlFrmYear.SelectedValue, ddlToYear.SelectedValue);

        string sMonth = " Product - Detaling Status for " + getMonthName(Convert.ToInt16(ddlFrmMonth.SelectedValue)) + " - " + ddlFrmYear.SelectedValue;// +" to " + getMonthName(Convert.ToInt16(ddlToMonth.SelectedValue)) + " - " + ddlToYear.SelectedValue;
        lblHead.Text = sMonth;

        dsSalesForce.Tables[0].DefaultView.RowFilter = "sf_type=2";
        DataTable dt = dsSalesForce.Tables[0].DefaultView.ToTable("table1");

        DataSet dsTe = new DataSet();
        DataTable dtT = new DataTable();

        dtT.Columns.Add("Product_Count");
        dtT.Columns.Add("sf_code");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dsTe = dcr.Get_sp_GetProduct_MGR_Sum(dt.Rows[i]["sf_code"].ToString(), ddlFrmMonth.SelectedValue, ddlToYear.SelectedValue, div_code);
            dtT.Rows.Add(dsTe.Tables[0].Rows[0][0], dsTe.Tables[0].Rows[0][1]);

        }
        dsDocDate.Merge(dtT);

        //FillSF();
        GvDcrCount.DataSource = dsSalesForce;
        GvDcrCount.DataBind();
    }
    protected void GvDcrCount_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        double Total=0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "desig_color")));

            Label lblSf_code = (Label)e.Row.FindControl("lblSfcode");
            DataSet ds = new DataSet();
            DCR dcr=new DCR();
            if (lblSf_code.Text.Contains("MGR"))
            {
                dsDocDate.Tables[0].DefaultView.RowFilter = "sf_code='" + lblSf_code.Text + "' ";
                DataTable dt = dsDocDate.Tables[0].DefaultView.ToTable("table1");
                //ds = dcr.Get_sp_GetProduct_MGR_Sum(lblSf_code.Text, ddlFrmMonth.SelectedValue, ddlToYear.SelectedValue, div_code);
                HyperLink hypDCRCount = (HyperLink)e.Row.FindControl("hypDCRCount");
                hypDCRCount.Text = dt.Rows[0][0].ToString();
            }
            
            //if (lblSf_code.Text.Contains("MR"))
            //{
            //    Total += Convert.ToInt16(hypDCRCount.Text);
            //}
        }
    }
    private void FillSF()
    {
        tbl.Rows.Clear();
        //doctor_total = 0;
        //BindSf_Code();

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            //tc_SNo.RowSpan = 2;
            Literal lit_SNo = new Literal();
            lit_SNo.Text = "#";
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_SNo.Style.Add("color", "white");
            tc_SNo.Style.Add("font-weight", "bold");
            tc_SNo.Style.Add("border-color", "Black");
            tc_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_SNo.Style.Add("font-family", "Calibri");
            tr_header.Cells.Add(tc_SNo);
            //tr_header.BackColor = System.Drawing.Color.FromName("#A6A6D2");

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 40;
            //tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Code.Style.Add("color", "white");
            tc_DR_Code.Style.Add("font-weight", "bold");
            tc_DR_Code.Style.Add("font-family", "Calibri");
            tc_DR_Code.Style.Add("border-color", "Black");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            //tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Field Force</center>";
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tc_DR_Name.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Name.Style.Add("color", "white");
            tc_DR_Name.Style.Add("font-weight", "bold");
            tc_DR_Name.Style.Add("font-family", "Calibri");
            tc_DR_Name.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            //tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tc_DR_HQ.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_HQ.Style.Add("color", "white");
            tc_DR_HQ.Style.Add("font-weight", "bold");
            tc_DR_HQ.Style.Add("font-family", "Calibri");
            tc_DR_HQ.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Designation = new TableCell();
            tc_DR_Designation.BorderStyle = BorderStyle.Solid;
            tc_DR_Designation.BorderWidth = 1;
            tc_DR_Designation.Width = 200;
            //tc_DR_Designation.RowSpan = 2;
            Literal lit_DR_Designation = new Literal();
            lit_DR_Designation.Text = "<center>Designation</center>";
            tc_DR_Designation.Controls.Add(lit_DR_Designation);
            tc_DR_Designation.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Designation.Style.Add("color", "white");
            tc_DR_Designation.Style.Add("font-weight", "bold");
            tc_DR_Designation.Style.Add("font-family", "Calibri");
            tc_DR_Designation.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Designation);

            TableCell tc_DR_Empcode = new TableCell();
            tc_DR_Empcode.BorderStyle = BorderStyle.Solid;
            tc_DR_Empcode.BorderWidth = 1;
            tc_DR_Empcode.Width = 200;
            //tc_DR_Empcode.RowSpan = 2;
            Literal lit_DR_Empcode = new Literal();
            lit_DR_Empcode.Text = "<center>Emp Code</center>";
            tc_DR_Empcode.Controls.Add(lit_DR_Empcode);
            tc_DR_Empcode.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Empcode.Style.Add("color", "white");
            tc_DR_Empcode.Style.Add("font-weight", "bold");
            tc_DR_Empcode.Style.Add("font-family", "Calibri");
            tc_DR_Empcode.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Empcode);

            TableCell tc_DR_Minutes = new TableCell();
            tc_DR_Minutes.BorderStyle = BorderStyle.Solid;
            tc_DR_Minutes.BorderWidth = 1;
            tc_DR_Minutes.Width = 200;
            //tc_DR_Minutes.RowSpan = ;
            Literal lit_DR_Minutes = new Literal();
            lit_DR_Minutes.Text = "<center>Minutes</center>";
            tc_DR_Minutes.Controls.Add(lit_DR_Minutes);
            tc_DR_Minutes.BackColor = System.Drawing.ColorTranslator.FromHtml("#0097AC");
            tc_DR_Minutes.Style.Add("color", "white");
            tc_DR_Minutes.Style.Add("font-weight", "bold");
            tc_DR_Minutes.Style.Add("font-family", "Calibri");
            tc_DR_Minutes.Style.Add("border-color", "Black");
            tr_header.Cells.Add(tc_DR_Minutes);

            tbl.Rows.Add(tr_header);



            // Details Section
            string sURL = string.Empty;
            int iCount = 0;

            DCR dcs = new DCR();

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {


                TableRow tr_det = new TableRow();

                if (Session["sf_type"].ToString() == "1")
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["Des_Color"].ToString());
                }
                else
                {
                    tr_det.Attributes.Add("style", "background-color:" + "#" + drFF["desig_color"].ToString());
                }

                iCount += 1;
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.BorderStyle = BorderStyle.Solid;
                tc_det_SNo.BorderWidth = 1;
                tc_det_SNo.Width = 50;
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);

                TableCell tc_det_doc_code = new TableCell();
                Literal lit_det_doc_code = new Literal();
                lit_det_doc_code.Text = "&nbsp;" + drFF["sf_code"].ToString();
                tc_det_doc_code.BorderStyle = BorderStyle.Solid;
                tc_det_doc_code.BorderWidth = 1;
                tc_det_doc_code.Controls.Add(lit_det_doc_code);
                tc_det_doc_code.Visible = false;
                tr_det.Cells.Add(tc_det_doc_code);

                TableCell tc_det_doc_name = new TableCell();
                HyperLink lit_det_doc_name = new HyperLink();
                lit_det_doc_name.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_doc_name.HorizontalAlign = HorizontalAlign.Left;
                tc_det_doc_name.BorderStyle = BorderStyle.Solid;
                tc_det_doc_name.Style.Add("font-family", "Calibri");
                tc_det_doc_name.BorderWidth = 1;
                tc_det_doc_name.Width = 200;
                tc_det_doc_name.Controls.Add(lit_det_doc_name);
                tr_det.Cells.Add(tc_det_doc_name);


                TableCell tc_det_HQ = new TableCell();
                Literal lit_det_HQ = new Literal();
                lit_det_HQ.Text = "&nbsp;" + drFF["Sf_HQ"].ToString();
                tc_det_HQ.BorderStyle = BorderStyle.Solid;
                tc_det_HQ.BorderWidth = 1;
                tc_det_HQ.Style.Add("font-family", "Calibri");
                tc_det_HQ.HorizontalAlign = HorizontalAlign.Center;
                tc_det_HQ.Controls.Add(lit_det_HQ);
                tc_det_HQ.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_HQ);

                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                tc_det_Designation.BorderStyle = BorderStyle.Solid;
                tc_det_Designation.BorderWidth = 1;
                tc_det_Designation.Style.Add("font-family", "Calibri");
                tc_det_Designation.HorizontalAlign = HorizontalAlign.Center;
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tc_det_Designation.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_Designation);

                TableCell tc_det_EmpCode = new TableCell();
                Literal lit_det_EmpCode = new Literal();
                lit_det_EmpCode.Text = "&nbsp;" + drFF["sf_emp_id"].ToString();
                tc_det_EmpCode.BorderStyle = BorderStyle.Solid;
                tc_det_EmpCode.BorderWidth = 1;
                tc_det_EmpCode.Style.Add("font-family", "Calibri");
                tc_det_EmpCode.HorizontalAlign = HorizontalAlign.Center;
                tc_det_EmpCode.Controls.Add(lit_det_EmpCode);
                tc_det_EmpCode.Width = 100;
                //tc_det_sf_HQ.Visible = false;
                tr_det.Cells.Add(tc_det_EmpCode);

                DataSet ds = new DataSet();
                DCR dcr = new DCR();

                TableCell tc_det_sf_Minutes = new TableCell();
                HyperLink hyperDrClick = new HyperLink();
                //if (drFF["sf_Code"].ToString().Contains("MR"))
                //{
                ds = dcr.Get_sp_GetProductSum(drFF["sf_Code"].ToString(), ddlFrmMonth.SelectedValue, ddlFrmYear.SelectedValue, div_code);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    sURL = "frmProductMinus_Dls.aspx?sfcode=" + drFF["sf_Code"].ToString() + "&Mon=" + ddlFrmMonth.SelectedValue + "&Year=" + ddlFrmYear.SelectedValue + "&div_code=" + div_code + "";

                    hyperDrClick.Attributes["onclick"] = "javascript:window.open('" + sURL + "' ,'','resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0');";
                    hyperDrClick.NavigateUrl = "#";
                    hyperDrClick.Text = "&nbsp;" + ds.Tables[0].Rows[0]["Doctor_Duration"].ToString();

                }
                else
                {
                    hyperDrClick.Text = "";
                }

                //}
                tc_det_sf_Minutes.BorderStyle = BorderStyle.Solid;
                tc_det_sf_Minutes.BorderWidth = 1;
                tc_det_sf_Minutes.Style.Add("font-family", "Calibri");
                tc_det_sf_Minutes.HorizontalAlign = HorizontalAlign.Center;
                tc_det_sf_Minutes.Controls.Add(hyperDrClick);
                tc_det_sf_Minutes.Width = 100;
                tr_det.Cells.Add(tc_det_sf_Minutes);


                tbl.Rows.Add(tr_det);

            }
        }
    }
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }
}