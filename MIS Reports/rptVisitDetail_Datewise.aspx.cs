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

public partial class MIS_Reports_rptVisitDetail_Datewise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string Prod = string.Empty;
    string Prod_Name = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;
    DataSet dsDCR = null;
    DataSet dsDCR_Tot = null;
    int count = 0;
    string checkmatrix = string.Empty;
    string ddlmatrix = string.Empty;
    string test1 = string.Empty;

    int startvalue;
    int endvalue;
    int startvalue2;
    int endvalue2;
    int startvalue3;
    int endvalue3;
    DataTable dt = new DataTable();
    DataSet dsMerge = new DataSet();
    string mode = string.Empty;
    string cMode = string.Empty;

    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Request.QueryString["sfcode"].ToString();
        divcode = Request.QueryString["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        checkmatrix = Request.QueryString["checkmatrix"].ToString();
        lblRegionName.Text = sfname;
        mode = Request.QueryString["mode"];
        screen_name = "rptVisitDetail_Datewise";

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        if (!Page.IsPostBack)
        {
            if (mode == "2")
            {
                lblHead.Text = "Campaign Drs - DateWise for the Month of " + strFMonthName + " " + FYear;
            }
            else
            {

                lblHead.Text = "Visit Detail - DateWise for the Month of " + strFMonthName + " " + FYear;
            }

            lblIDMonth.Visible = false;
            lblIDYear.Visible = false;

            if (checkmatrix == "0")
            {

                FillDoctor();
            }
            else if (checkmatrix == "1")
            {

                //string test1;

                //string startvalue;
                //string endvalue;
                ddlmatrix = Request.QueryString["ddlmatrix"].ToString();

                string[] test = ddlmatrix.Split('[');
                test1 = test[1];
                test1 = test1.TrimEnd(']');

                string[] test2 = test1.Split('-');
                startvalue = Convert.ToInt32(test2[0]);
                endvalue = Convert.ToInt32(test2[1]);

                startvalue2 = startvalue;
                endvalue2 = endvalue;

                startvalue3 = startvalue;
                endvalue3 = endvalue;

                //FillMatrix();

                FillMatrix1();

            }
        }
    }

    private void FillMatrix1()
    {
        while (Convert.ToInt32(startvalue2) <= Convert.ToInt32(endvalue2))
        {

            sCurrentDate = (Convert.ToInt16(FMonth)) + "-" + startvalue2 + "-" + Convert.ToInt16(FYear);
            Doctor dc = new Doctor();
            dsDoctor = dc.Visit_Doctor_DCR_Matrix(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), startvalue2, sCurrentDate);

            dsMerge.Merge(dsDoctor.Tables[0]);

            dt = dsMerge.Tables[0];
            startvalue2++;

        }

        if (dt.Rows.Count > 0)
        {
            //grdTP.Visible = true;


            DLoneVisitDoc.DataSource = dt;
            DLoneVisitDoc.DataBind();

        }


    }

    protected void ItemDB(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.AlternatingItem | e.Item.ItemType == ListItemType.Item)
        {

            Label lblDr_Name = (Label)e.Item.FindControl("lblDr_Name");
            Label lblQual = (Label)e.Item.FindControl("lblQual");
            Label lblprod = (Label)e.Item.FindControl("lblprod");
            Label lblspec = (Label)e.Item.FindControl("lblspec");
            Label lblcat = (Label)e.Item.FindControl("lblcat");
            Label lblclass = (Label)e.Item.FindControl("lblclass");

            if (lblQual.Text == "." || lblprod.Text == "." || lblspec.Text == "." || lblcat.Text == "." || lblclass.Text == ".")
            {
                lblQual.Text = "";
                lblprod.Text = "";
                lblspec.Text = "";
                lblcat.Text = "";
                lblclass.Text = "";
            }

            if (lblDr_Name.Text == "Listed Doctor Name")
            {
                lblDr_Name.Font.Bold = true;
                lblDr_Name.Font.Size = 10;
                lblDr_Name.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblDr_Name.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblDr_Name.Attributes.Add("class", "dataalignment");
                lblDr_Name.Style.Add("display", "grid");
            }
            if (lblQual.Text == "Qualification")
            {
                lblQual.Font.Bold = true;
                lblQual.Font.Size = 10;
                lblQual.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblQual.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblQual.Attributes.Add("class", "dataalignment");
                lblQual.Style.Add("display", "grid");
            }
            if (lblDr_Name.Text == "Weekly Off" || lblDr_Name.Text == "Leave" || lblDr_Name.Text == "Meeting" || lblDr_Name.Text == "Holiday" || lblDr_Name.Text == "Transit" || lblDr_Name.Text == "No Field Work" || lblDr_Name.Text == "Training" || lblDr_Name.Text == "Super Stockist Work" || lblDr_Name.Text == "Camp Work" || lblDr_Name.Text == "Induction Work" || lblDr_Name.Text == "Cycle Meeting" || lblDr_Name.Text == "Stockist Work" || lblDr_Name.Text == "Admin Work" || lblDr_Name.Text == "Drs Survey")
            {
                lblDr_Name.Font.Size = 12;
                lblDr_Name.Font.Bold = true;
                //lblQual.Visible = false;
            }
            if (lblprod.Text == "Product Name")
            {
                lblprod.Font.Bold = true;
                lblprod.Font.Size = 10;
                lblprod.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblprod.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblprod.Attributes.Add("class", "dataalignment");
                lblprod.Style.Add("display", "grid");
            }

            if (lblprod.Text != "Product Name" || lblprod.Text != ".")
            {
                lblprod.Text = lblprod.Text.Replace("~$#", ",").Trim();
                //lblprod.Text = lblprod.Text.Replace("$", ",").Trim();
                lblprod.Text = "&nbsp;&nbsp;" + lblprod.Text.Replace("~$", ",").Trim();
            }
            if (lblspec.Text == "Speciality")
            {
                lblspec.Font.Bold = true;
                lblspec.Font.Size = 10;
                lblspec.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblspec.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblspec.Attributes.Add("class", "dataalignment");
                lblspec.Style.Add("display", "grid");
            }
            if (lblcat.Text == "Category")
            {
                lblcat.Font.Bold = true;
                lblcat.Font.Size = 10;
                lblcat.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblcat.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblcat.Attributes.Add("class", "dataalignment");
                lblcat.Style.Add("display", "grid");
            }
            if (lblclass.Text == "Class")
            {
                lblclass.Font.Bold = true;
                lblclass.Font.Size = 10;
                lblclass.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                lblclass.ForeColor = System.Drawing.Color.FromName("#636d73");
                lblclass.Attributes.Add("class", "dataalignment");
                lblclass.Style.Add("display", "grid");
            }


        }


    }


    private void FillDoctor()
    {

        DateTime dtCurrent;
        string sCurrentDate = string.Empty;

        if (Convert.ToInt16(FMonth) == 12)
        {
            sCurrentDate = "01-01-" + (Convert.ToInt16(FYear) + 1);
        }
        else
        {
            sCurrentDate = (Convert.ToInt16(FMonth) + 1) + "-01-" + Convert.ToInt16(FYear);
        }

        dtCurrent = Convert.ToDateTime(sCurrentDate);
        string mode = Request.QueryString["mode"].ToString();
        string sURL = string.Empty;
        tbl.Rows.Clear();

        Doctor dc = new Doctor();
        if (mode == "1")
        dsDoctor = dc.Visit_Doctor_DCR(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent);
        else if (mode == "2")
            dsDoctor = dc.Visit_Camp(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), dtCurrent, mode);

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;

            //tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            //tr_header.Style.Add("Color", "White");
            //tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            //tc_SNo.BorderStyle = BorderStyle.Solid;
            //tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>#</center>";
            //tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            //tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Style.Add("font-size", "10pt");
            //tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            //tc_DR_Code.BorderStyle = BorderStyle.Solid;
            //tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 1;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>Listed Doctor Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            //tc_DR_Code.Style.Add("font-family", "Calibri");
            //tc_DR_Code.Style.Add("font-size", "10pt");
            //tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            //tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            //tc_DR_Name.BorderStyle = BorderStyle.Solid;
            //tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 500;
            tc_DR_Name.RowSpan = 1;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Listed Doctor Name</center>";
            //tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Name.Style.Add("font-family", "Calibri");
            //tc_DR_Name.Style.Add("font-size", "10pt");
            //tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            Territory terr = new Territory();
            DataSet dsTerritory = new DataSet();
            dsTerritory = terr.getWorkAreaName(divcode);

            TableCell tc_DR_Terr = new TableCell();
            //tc_DR_Terr.BorderStyle = BorderStyle.Solid;
            //tc_DR_Terr.BorderWidth = 1;
            tc_DR_Terr.Width = 200;
            tc_DR_Terr.RowSpan = 1;
            Literal lit_DR_Terr = new Literal();
            lit_DR_Terr.Text = "<center>" + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + "</center>";
            //tc_DR_Terr.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Terr.Style.Add("font-family", "Calibri");
            //tc_DR_Terr.Style.Add("font-size", "10pt");
            //tc_DR_Terr.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Terr.Controls.Add(lit_DR_Terr);
            tr_header.Cells.Add(tc_DR_Terr);

            TableCell tc_DR_Des = new TableCell();
            //tc_DR_Des.BorderStyle = BorderStyle.Solid;
            //tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 120;
            tc_DR_Des.RowSpan = 1;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Category</center>";
            //tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Des.Style.Add("font-family", "Calibri");
            //tc_DR_Des.Style.Add("font-size", "10pt");
            //tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Attributes.Add("Class", "stickyFirstRow");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);


            TableCell tc_DR_HQ = new TableCell();
            //tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            //tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 200;
            tc_DR_HQ.RowSpan = 1;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "Qualification";
            //tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            //tc_DR_HQ.Style.Add("font-family", "Calibri");
            //tc_DR_HQ.Style.Add("font-size", "10pt");
            //tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Attributes.Add("Class", "stickyFirstRow");

            // Hide/Show Column - Begin
            Chemist chem = new Chemist();
            //dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sfCode);

            if (lit_DR_HQ.Text == "Qualification")
            {

                Chemist lst = new Chemist();
                int iReturn = -1;

                iReturn = lst.GridColumnShowHideInsert(screen_name, lit_DR_HQ.Text, sf_code, true, 3);
            }

            dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, lit_DR_HQ.Text, sf_code);
            if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
            {
                if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
                {
                    tc_DR_HQ.Style.Add("display", "none");
                }
            }
            // Hide/Show Column - End
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);
        

            TableCell tc_DR_Spe = new TableCell();
            //tc_DR_Spe.BorderStyle = BorderStyle.Solid;
            //tc_DR_Spe.BorderWidth = 1;
            tc_DR_Spe.Width = 120;
            tc_DR_Spe.RowSpan = 1;
            Literal lit_DR_Spe = new Literal();
            lit_DR_Spe.Text = "Specialty";
            //tc_DR_Spe.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Spe.Style.Add("font-family", "Calibri");
            //tc_DR_Spe.Style.Add("font-size", "10pt");
            //tc_DR_Spe.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Spe.Attributes.Add("Class", "stickyFirstRow");

            // Hide/Show Column - Begin
            //Chemist chem = new Chemist();
            //dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sfCode);

            if (lit_DR_Spe.Text == "Specialty")
            {

                Chemist lst = new Chemist();
                int iReturn = -1;

                iReturn = lst.GridColumnShowHideInsert(screen_name, lit_DR_Spe.Text, sf_code, true, 3);
            }

            dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, lit_DR_Spe.Text, sf_code);
            if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
            {
                if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
                {
                    tc_DR_Spe.Style.Add("display", "none");
                }
            }
            // Hide/Show Column - End
            tc_DR_Spe.Controls.Add(lit_DR_Spe);
            tr_header.Cells.Add(tc_DR_Spe);

            TableCell tc_DR_Class = new TableCell();
            //tc_DR_Class.BorderStyle = BorderStyle.Solid;
            //tc_DR_Class.BorderWidth = 1;
            tc_DR_Class.Width = 120;
            tc_DR_Class.RowSpan = 1;
            Literal lit_DR_Class = new Literal();
            lit_DR_Class.Text = "Class";
            //tc_DR_Class.BorderColor = System.Drawing.Color.Black;
            //tc_DR_Class.Style.Add("font-family", "Calibri");
            //tc_DR_Class.Style.Add("font-size", "10pt");
            //tc_DR_Class.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Class.Attributes.Add("Class", "stickyFirstRow");

            // Hide/Show Column - Begin
            //Chemist chem = new Chemist();
            //dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sfCode);

            if (lit_DR_Class.Text == "Class")
            {

                Chemist lst = new Chemist();
                int iReturn = -1;

                iReturn = lst.GridColumnShowHideInsert(screen_name, lit_DR_Class.Text, sf_code, true, 3);
            }

            dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, lit_DR_Class.Text, sf_code);
            if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
            {
                if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
                {
                    tc_DR_Class.Style.Add("display", "none");
                }
            }
            // Hide/Show Column - End
            tc_DR_Class.Controls.Add(lit_DR_Class);
            tr_header.Cells.Add(tc_DR_Class);

            tbl.Rows.Add(tr_header);

            tot_days = getmaxdays_month(Convert.ToInt16(FMonth));

            TableRow tr_day_header = new TableRow();
            //tr_day_header.BorderStyle = BorderStyle.Solid;
            //tr_day_header.BorderWidth = 1;

            while (cday <= tot_days)
            {
                TableCell tc_day = new TableCell();
                //tc_day.BorderStyle = BorderStyle.Solid;
                //tc_day.BorderWidth = 1;
                tc_day.Width = 50;
                tc_day.HorizontalAlign = HorizontalAlign.Center;
                Literal lit_day = new Literal();
                lit_day.Text = cday.ToString();
                tc_day.Controls.Add(lit_day);
                tr_header.Cells.Add(tc_day);

                cday = cday + 1;
                tc_day.Attributes.Add("Class", "stickyFirstRow");
            }

            TableCell tc_tot = new TableCell();
            //tc_tot.BorderStyle = BorderStyle.Solid;
            //tc_tot.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Tot = new Literal();
            lit_Count_Tot.Text = "<center>Total</center>";
            tc_tot.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_tot.Controls.Add(lit_Count_Tot);
            tc_tot.Font.Bold.ToString();
            //tc_tot.BackColor = System.Drawing.Color.White;
            //tc_tot.Attributes.Add("Class", "tbldetail_main");
            tc_tot.ColumnSpan = 7;
            //tc_tot.Style.Add("text-align", "left");
            //tc_tot.Style.Add("font-family", "Calibri");
            tc_tot.Attributes.Add("Class", "stickyFirstRow");
            //tc_tot.Style.Add("font-size", "10pt");
            tr_header.Cells.Add(tc_tot);
            //tr_header.Attributes.Add("class", "stickyFirstRow");

            if (dsDoctor.Tables[0].Rows.Count > 0)
                ViewState["dsDoctor"] = dsDoctor;

            int iCount = 0;
            string iTotLstCount = string.Empty;
            int FullTotLstCount = 0;
            dsDoctor = (DataSet)ViewState["dsDoctor"];

            foreach (DataRow drFF in dsDoctor.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                //strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = iCount.ToString();
                //tc_det_SNo.BorderStyle = BorderStyle.Solid;
                //tc_det_SNo.BorderWidth = 1;
                //tc_det_SNo.Style.Add("font-family", "Calibri");
                //tc_det_SNo.Style.Add("font-size", "10pt");
                //tc_det_SNo.Style.Add("text-align", "left");
                //tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["ListedDrCode"].ToString();
                //tc_det_usr.BorderStyle = BorderStyle.Solid;
                //tc_det_usr.BorderWidth = 1;
                tc_det_usr.Visible = false;
                //tc_det_usr.Style.Add("font-family", "Calibri");
                //tc_det_usr.Style.Add("font-size", "10pt");
                //tc_det_usr.Style.Add("text-align", "left");
                //tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);


                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["ListedDr_Name"].ToString();
                //tc_det_FF.BorderStyle = BorderStyle.Solid;
                //tc_det_FF.BorderWidth = 1;
                //tc_det_FF.Style.Add("font-family", "Calibri");
                //tc_det_FF.Style.Add("font-size", "10pt");
                //tc_det_FF.Style.Add("text-align", "left");
                //tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                TableCell tc_det_terr = new TableCell();
                Literal lit_det_terr = new Literal();
                lit_det_terr.Text = "&nbsp;" + drFF["territory_Name"].ToString();
                //tc_det_terr.BorderStyle = BorderStyle.Solid;
                //tc_det_terr.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                //tc_det_terr.Style.Add("text-align", "left");
                //tc_det_terr.Attributes.Add("Class", "rptCellBorder");
                tc_det_terr.Controls.Add(lit_det_terr);
                tr_det.Cells.Add(tc_det_terr);


                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                lit_det_Designation.Text = "&nbsp;" + drFF["Doc_Cat_ShortName"].ToString();
                //tc_det_Designation.BorderStyle = BorderStyle.Solid;
                //tc_det_Designation.BorderWidth = 1;
                //tc_det_Designation.Style.Add("font-family", "Calibri");
                //tc_det_Designation.Style.Add("font-size", "10pt");
                //tc_det_Designation.Style.Add("text-align", "left");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                //tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Designation);

                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["Doc_Qua_Name"].ToString();
                //tc_det_hq.BorderStyle = BorderStyle.Solid;
                //tc_det_hq.BorderWidth = 1;
                //tc_det_hq.Style.Add("font-family", "Calibri");
                //tc_det_hq.Style.Add("font-size", "10pt");
                //tc_det_hq.Style.Add("text-align", "left");
                //tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                TableCell tc_det_Spec = new TableCell();
                Literal lit_det_Spec = new Literal();
                lit_det_Spec.Text = "&nbsp;" + drFF["Doc_Spec_ShortName"].ToString();
                //tc_det_Spec.BorderStyle = BorderStyle.Solid;
                //tc_det_Spec.BorderWidth = 1;
                //tc_det_Spec.Style.Add("font-family", "Calibri");
                //tc_det_Spec.Style.Add("font-size", "10pt");
                //tc_det_Spec.Style.Add("text-align", "left");
                tc_det_Spec.Controls.Add(lit_det_Spec);
                //tc_det_Spec.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Spec);

                TableCell tc_det_Class = new TableCell();
                Literal lit_det_Class = new Literal();
                lit_det_Class.Text = "&nbsp;" + drFF["Doc_Class_ShortName"].ToString();
                //tc_det_Class.BorderStyle = BorderStyle.Solid;
                //tc_det_Class.BorderWidth = 1;
                //tc_det_Class.Style.Add("font-family", "Calibri");
                //tc_det_Class.Style.Add("font-size", "10pt");
                //tc_det_Class.Style.Add("text-align", "left");
                tc_det_Class.Controls.Add(lit_det_Class);
                //tc_det_Class.Attributes.Add("Class", "rptCellBorder");
                tr_det.Cells.Add(tc_det_Class);


                dsDCR = dc.Visit_Doctor_DCR_Dates(sfCode, divcode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), drFF["ListedDrCode"].ToString());
                if (dsDCR.Tables[0].Rows.Count > 0)
                {
                    ddate = 1;
                    foreach (DataRow datarow in dsDCR.Tables[0].Rows)
                    {
                        while (ddate <= Convert.ToInt16(datarow["Activity_Date"].ToString()))
                        {
                            if (ddate == Convert.ToInt16(datarow["Activity_Date"].ToString()))
                            {

                                sDCR = "✔";
                                count += 1;
                                iTotLstCount = Convert.ToString(count);
                            }
                            else
                            {
                                sDCR = "";
                                iTotLstCount = "";
                            }
                            TableCell tc_det_day = new TableCell();
                            //tc_det_day.BorderStyle = BorderStyle.Solid;
                            //tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            tc_det_day.ToolTip = Convert.ToString(ddate);
                            //tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            //tc_det_day.ForeColor = System.Drawing.Color.Red;
                            tr_det.Cells.Add(tc_det_day);
                            ddate = ddate + 1;

                        }
                    }
                    sDCR = "";
                    if (tot_days >= ddate)
                    {
                        cday = ddate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            //tc_det_day.BorderStyle = BorderStyle.Solid;
                            //tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            //tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }
                else
                {
                    sDCR = "";
                    ddate = 1;
                    if (tot_days >= ddate)
                    {
                        cday = ddate;
                        while (cday <= tot_days)
                        {
                            TableCell tc_det_day = new TableCell();
                            //tc_det_day.BorderStyle = BorderStyle.Solid;
                            //tc_det_day.BorderWidth = 1;
                            tc_det_day.Width = 50;
                            tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                            Literal lit_det_day = new Literal();
                            lit_det_day.Text = sDCR;
                            tc_det_day.Controls.Add(lit_det_day);
                            //tc_det_day.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_det_day);

                            cday = cday + 1;
                        }
                    }
                }

                TableCell tc_det = new TableCell();
                Literal lit_det = new Literal();
                lit_det.Text = "<center>" + iTotLstCount.ToString() + "</center>";
                //tc_det.BorderStyle = BorderStyle.Solid;
                //tc_det.BorderWidth = 1;
                //tc_det.Attributes.Add("Class", "rptCellBorder");
                tc_det.Controls.Add(lit_det);
                tc_det.Font.Bold = true;
                tr_det.Cells.Add(tc_det);
                //tr_det.BackColor = System.Drawing.Color.White;



                iTotLstCount = "";
                count = 0;
                tbl.Rows.Add(tr_det);

                // Hide/Show Column - Begin
                Chemist chem1 = new Chemist();

                dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
                if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
                {
                    var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                                 select new
                                 {
                                     Ch_Name = data.Field<string>("column_name"),
                                     Ch_Code = data.Field<string>("column_name")
                                 };
                    var listOfGrades = result.ToList();
                    cblGridColumnList.Visible = true;
                    cblGridColumnList.DataSource = listOfGrades;
                    cblGridColumnList.DataTextField = "Ch_Name";
                    cblGridColumnList.DataValueField = "Ch_Code";
                    cblGridColumnList.DataBind();

                    string headerText = string.Empty;

                    for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
                    {
                        headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

                        System.Web.UI.WebControls.ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                        if (ddl != null)
                        {
                            if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                            {
                                cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                            }
                            else
                            {
                                cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                            }

                        }

                        if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                        {
                            if (i == 0)
                            {
                                tc_det_hq.Style.Add("display", "none");
                            }
                            else if (i == 1)
                            {
                                tc_det_Spec.Style.Add("display", "none");
                            }
                            else
                            {
                                tc_det_Class.Style.Add("display", "none");
                            }
                        }
                    }
                }
                // Hide/Show Column - End
            }

            TableRow tr_total = new TableRow();

            TableCell tc_Count_Total = new TableCell();
            //tc_Count_Total.BorderStyle = BorderStyle.Solid;
            //tc_Count_Total.BorderWidth = 1;
            //tc_catg_Total.Width = 25;
            Literal lit_Count_Total = new Literal();
            lit_Count_Total.Text = "<center>Total</center>";
            tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
            tc_Count_Total.Controls.Add(lit_Count_Total);
            tc_Count_Total.Font.Bold.ToString();
            //tc_Count_Total.BackColor = System.Drawing.Color.White;
            //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
            tc_Count_Total.ColumnSpan = 7;
            //tc_Count_Total.Style.Add("text-align", "left");
            //tc_Count_Total.Style.Add("font-family", "Calibri");
            //tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
            //tc_Count_Total.Style.Add("font-size", "10pt");

            tr_total.Cells.Add(tc_Count_Total);

            sDCR = "";
            ddate = 1;
            if (tot_days >= ddate)
            {
                cday = ddate;
                while (cday <= tot_days)
                {
                    TableCell tc_det_day = new TableCell();
                    //tc_det_day.BorderStyle = BorderStyle.Solid;
                    //tc_det_day.BorderWidth = 1;
                    tc_det_day.Width = 50;
                    tc_det_day.HorizontalAlign = HorizontalAlign.Center;
                    Literal lit_det_day = new Literal();
                    dsDCR_Tot = dc.Visit_Doctor_DCR_Dates_Total(sfCode, divcode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), cday);
                    sDCR = dsDCR_Tot.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    lit_det_day.Text = sDCR;
                    FullTotLstCount += Convert.ToInt16(sDCR);
                    tc_det_day.Controls.Add(lit_det_day);
                    tc_det_day.Font.Bold = true;
                    //tc_det_day.Attributes.Add("Class", "rptCellBorder");
                    tr_total.Cells.Add(tc_det_day);

                    cday = cday + 1;
                }
            }

            TableCell tc_fullTot = new TableCell();
            Literal lit_fullTot = new Literal();
            lit_fullTot.Text = "<center>" + FullTotLstCount.ToString() + "</center>";
            //tc_fullTot.BorderStyle = BorderStyle.Solid;
            //tc_fullTot.BorderWidth = 1;
            //tc_fullTot.Attributes.Add("Class", "rptCellBorder");
            tc_fullTot.Controls.Add(lit_fullTot);
            tc_fullTot.Font.Bold = true;
            tr_total.Cells.Add(tc_fullTot);
            tr_total.BackColor = System.Drawing.Color.White;


            tbl.Rows.Add(tr_total);

        }
        else
        {
            TableRow tr_header = new TableRow();
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;

            //tr_header.BackColor = System.Drawing.Color.FromName("#666699");
            //tr_header.Style.Add("Color", "Black");
            //tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            //tc_SNo.BorderStyle = BorderStyle.Solid;
            //tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 1;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>No Records Found</center>";
            //tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            //tc_SNo.Style.Add("font-family", "Calibri");
            //tc_SNo.Style.Add("font-size", "10pt");
            tc_SNo.Style.Add("background-color", "white");
            tc_SNo.Style.Add("border", "solid 1px #d1e2ea");
            tc_SNo.Style.Add("color", "#636d73");

            tr_header.Cells.Add(tc_SNo);

            tbl.Rows.Add(tr_header);
        }
    }

    private int getmaxdays_month(int imonth)
    {
        int idays = -1;

        if (imonth == 1)
            idays = 31;
        else if (imonth == 2)
            idays = 29;
        else if (imonth == 3)
            idays = 31;
        else if (imonth == 4)
            idays = 30;
        else if (imonth == 5)
            idays = 31;
        else if (imonth == 6)
            idays = 30;
        else if (imonth == 7)
            idays = 31;
        else if (imonth == 8)
            idays = 31;
        else if (imonth == 9)
            idays = 30;
        else if (imonth == 10)
            idays = 31;
        else if (imonth == 11)
            idays = 30;
        else if (imonth == 12)
            idays = 31;

        return idays;
    }

    //protected void btnPrint_Click(object sender, EventArgs e)
    //{
    //    Session["ctrl"] = pnlContents;
    //    Control ctrl = (Control)Session["ctrl"];
    //    PrintWebControl(ctrl);
    //}
    //public static void PrintWebControl(Control ControlToPrint)
    //{
    //    StringWriter stringWrite = new StringWriter();
    //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
    //    if (ControlToPrint is WebControl)
    //    {
    //        Unit w = new Unit(100, UnitType.Percentage);
    //        ((WebControl)ControlToPrint).Width = w;
    //    }
    //    Page pg = new Page();
    //    pg.EnableEventValidation = false;
    //    HtmlForm frm = new HtmlForm();
    //    pg.Controls.Add(frm);
    //    frm.Attributes.Add("runat", "server");
    //    frm.Controls.Add(ControlToPrint);
    //    pg.DesignerInitialize();
    //    pg.RenderControl(htmlWrite);
    //    string strHTML = stringWrite.ToString();
    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Write(strHTML);
    //    HttpContext.Current.Response.Write("<script>window.print();</script>");
    //    HttpContext.Current.Response.End();

    //}

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=DCRView.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        form1.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && sf_code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, sf_code);
        }

        Response.Redirect(Request.RawUrl);
    }


}