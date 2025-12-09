using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MIS_Reports_Statusview_Prd_Camp : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dschm = null;
    DataSet dsstk = null;
    DataSet dsDoc5 = null;
    DataSet dsDoc6 = null;
    string tot_chm6 = string.Empty;
    DataSet dsstk1 = null;
    DataSet dsstk2 = null;
    string tot_chm = string.Empty;
    string tot_stk = string.Empty;
    string tot_stk1 = string.Empty;
    string tot_stk2 = string.Empty;
    string tot_chm1 = string.Empty;
	string total_doc_reg = string.Empty;
    string tot_dr_draft = string.Empty;

    DataSet dsDoc = null;
    DataSet dsDoc1 = null;
    DataSet dsDoc2 = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_draft = string.Empty;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_dr1 = string.Empty;
    string total_doc1 = string.Empty;
    string tot_dr2 = string.Empty;
    string total_doc2 = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string sfcsts = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable dtrowClr = new DataTable();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

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
            Filldiv();
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ////// menu1.FindControl("btnBack").Visible = false;
            FillManagers();
        }
        FillColor();
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

    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
        pnlprint.Visible = false;
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
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
    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
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
        FillColor();

    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }

    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "2")
        {
            chkVacant.Visible = true;
        }
        else
        {
            chkVacant.Visible = false;
        }
        pnlprint.Visible = false;
        pnlContents1.Visible = false;
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;

            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
		if (ddlmode.SelectedValue == "11")
        {
            linkDetail.Visible = false;
            linkDetail.Attributes.Add("href", "javascript:showModalPopUp_VC('" + ddlFieldForce.SelectedValue.ToString() + "', '" + ddlFieldForce.SelectedItem.ToString() + "', '" + "" + "', '" + "" + "', '" + ddlDivision.SelectedValue.ToString() + "')");
        }
        else
        {
            linkDetail.Visible = false;
        }
        FillSalesForce();
    }

 protected void linkDetail_Click(object sender, EventArgs e)
    {

        linkDetail.Attributes.Add("href", "javascript:showModalPopUp_VC('" + ddlFieldForce.SelectedValue.ToString() + "', '" + ddlFieldForce.SelectedItem.ToString() + "', '" + "" + "', '" + "" + "', '" + ddlDivision.SelectedValue.ToString() + "')");

    }
    protected void linkstatuscount_click(object sender, EventArgs e)
    {

        pnlContents1.Visible = true;
        GrdFFcount1();

    }
    private void GrdFFcount1()
    {
        SqlConnection con = new SqlConnection(strConn);
        DataSet dsts = new DataSet();
        string sProc_Name = "";


        sProc_Name = "SampleInput_FF_Count";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("Division_code");
        GrdFFcount.DataSource = dsts;
        GrdFFcount.DataBind();
    }
    protected void GrdFFcount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;

            e.Row.Cells[0].Attributes.Add("style", "color:black;font-weight:normal;");
            e.Row.Cells[1].Attributes.Add("style", "color:black;font-weight:normal;");
            e.Row.Cells[2].Attributes.Add("style", "color:black;font-weight:normal;");
            //e.Row.Cells[3].Attributes.Add("style", "color:black;font-weight:normal;");

            //e.Row.Cells[4].Attributes.Add("style", "color:black;font-weight:normal;");
            //e.Row.Cells[5].Attributes.Add("style", "color:black;font-weight:normal;");

            for (int i1 = 7; i1 < e.Row.Cells.Count; i1++)
            {
                e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
                if (e.Row.Cells[i1].Text == "0" || e.Row.Cells[i1].Text == "&nbsp;" || e.Row.Cells[i1].Text == "")
                {
                    e.Row.Cells[i1].Text = "-";
                    //e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
                }
                e.Row.Cells[i1].Attributes.Add("align", "center");

            }
            e.Row.Cells[0].Attributes.Add("align", "center");
            e.Row.Cells[1].Attributes.Add("align", "center");

            HyperLink hyp_lst_month = new HyperLink();
            HyperLink hyp_lst_month1 = new HyperLink();

            if (e.Row.Cells[1].Text != "0" && e.Row.Cells[1].Text != "-" && e.Row.Cells[1].Text != "-")
            {
                hyp_lst_month.Text = e.Row.Cells[1].Text;

                string sURL = "&Division_code=" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()) + "&Division_name=" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "&Mode=S";

                hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUpcount('" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','S')");
                hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUpcount('" + sURL + "')";
                hyp_lst_month.NavigateUrl = "#";
                hyp_lst_month.BackColor = System.Drawing.Color.White;
                hyp_lst_month.ToolTip = "Click here";
                hyp_lst_month.Attributes.Add("style", "cursor:pointer");
                hyp_lst_month.Font.Underline = true;
                hyp_lst_month.Font.Bold = true;
                hyp_lst_month.ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[1].Controls.Add(hyp_lst_month);
                e.Row.Cells[1].Attributes.Add("align", "center");

            }

            if (e.Row.Cells[2].Text != "0" && e.Row.Cells[2].Text != "-" && e.Row.Cells[2].Text != "-")
            {
                hyp_lst_month1.Text = e.Row.Cells[2].Text;

                string sURL = "&Division_code=" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()) + "&Division_name=" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "&Mode=I";
                hyp_lst_month1.Attributes.Add("href", "javascript:showModalPopUpcount('" + Convert.ToString(dtrowClr.Rows[indx][0].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','I')");
                hyp_lst_month1.Attributes["onclick"] = "javascript:showModalPopUpcount('" + sURL + "')";
                hyp_lst_month1.NavigateUrl = "#";
                hyp_lst_month1.BackColor = System.Drawing.Color.White;
                hyp_lst_month1.ToolTip = "Click here";
                hyp_lst_month1.Attributes.Add("style", "cursor:pointer");
                hyp_lst_month1.Font.Underline = true;
                hyp_lst_month1.Font.Bold = true;
                hyp_lst_month1.ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[2].Controls.Add(hyp_lst_month1);
                e.Row.Cells[2].Attributes.Add("align", "center");

            }

        }
    }
    protected void GrdFFcount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        SalesForce sf = new SalesForce();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            HyperLink hlink = new HyperLink();

            #region Merge cells
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();


            //  AddMergedCells(objgridviewrow, objtablecell, 1, 0, "S.No", "#336277", true);
            //  AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Division Name", "#336277", true);
            //AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Sample Count", "#336277", true);
            //AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Input Count", "#336277", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "FieldForce Count", "#0097AC", true);
            //AddMergedCells(objgridviewrow, objtablecell, 1, 0, "FF Count", "#0097AC", true);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Sample", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Input", "#0097AC", false);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

            #endregion
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
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }
    private void FillSalesForce()
    {
        string sURL = string.Empty;

        tbl.Rows.Clear();
        //doctor_total = 0;

        SalesForce sf = new SalesForce();
        if (ddlmode.SelectedValue == "4")
        {
            dsSalesForce = sf.SalesForceListMgrGet_MgrOnly(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
        }
        else if (ddlmode.SelectedValue == "3")
        {
            dsSalesForce = sf.SalesForceListMgrGet(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
        }
        else if (ddlmode.SelectedValue == "2")
        {
            if (chkVacant.Checked == true)
            {
                dsSalesForce = sf.SalesForceList_New_GetMr(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
            }
            else
            {
                dsSalesForce = sf.sp_get_Rep_access_with_HQ_wtoutvacant(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
            }
        }
        else
        {
            dsSalesForce = sf.SalesForceList_New_GetMr(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            pnlprint.Visible = true;

            TableRow tr_header = new TableRow();
            //tr_header.BorderStyle = BorderStyle.Solid;
            //tr_header.BorderWidth = 1;

            TableRow tr_det1 = new TableRow();
            tr_det1.BorderStyle = BorderStyle.Solid;
            tr_det1.BorderWidth = 1;
            tr_det1.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            //tr_det1.Style.Add("Color", "White");
            tr_det1.BorderColor = System.Drawing.Color.Black;

            tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            //tr_header.Style.Add("Color", "White");
            //tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            //tc_SNo.BorderStyle = BorderStyle.Solid;
            //tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 30;
            if (ddlmode.SelectedValue == "4")
            {
                tc_SNo.RowSpan = 2;
            }
            else
            {
                tc_SNo.RowSpan = 1;
            }
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "<center>#</center>";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Style.Add("border-bottom", "10px solid #fff");
            tc_SNo.Attributes.Add("Class", "tr_Sno");
            tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#414d55");
            tc_SNo.Style.Add("color", "white");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.Width = 400;
            if (ddlmode.SelectedValue == "4")
            {
                tc_DR_Code.RowSpan = 2;
            }
            else
            {
                tc_DR_Code.RowSpan = 1;
            }
            
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "tr_th");
            tc_DR_Code.Style.Add("padding", "20px 5px");
            tc_DR_Code.Style.Add("vertical-align", "inherit");
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.Width = 300;
            if (ddlmode.SelectedValue == "4")
            {
                tc_DR_Name.RowSpan = 2;
            }
            else
            {
                tc_DR_Name.RowSpan = 1;
            }
            
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName</center>";
            tc_DR_Name.Attributes.Add("Class", "tr_th");
            tc_DR_Name.Style.Add("padding", "20px 5px");
            tc_DR_Name.Style.Add("vertical-align", "inherit");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);


            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.Width = 100;
            if (ddlmode.SelectedValue == "4")
            {
                tc_DR_HQ.RowSpan = 2;
            }
            else
            {
                tc_DR_HQ.RowSpan = 1;
            }
            
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.Attributes.Add("Class", "tr_th");
            tc_DR_HQ.Style.Add("padding", "20px 5px");
            tc_DR_HQ.Style.Add("vertical-align", "inherit");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);


            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.Width = 80;
            if (ddlmode.SelectedValue == "4")
            {
                tc_DR_Des.RowSpan = 2;
            }
            else
            {
                tc_DR_Des.RowSpan = 1;
            }
            
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Design</center>";
            tc_DR_Des.Attributes.Add("Class", "tr_th");
            tc_DR_Des.Style.Add("padding", "20px 5px");
            tc_DR_Des.Style.Add("vertical-align", "inherit");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            TableCell tc_status = new TableCell();
            tc_status.Width = 80;
            tc_status.RowSpan = 1;
            Literal lit_status = new Literal();
            if (ddlmode.SelectedValue == "5")
            {
                lit_status.Text = "<center>No.of.Lst.drs</center>";
            }
            else
            {
                lit_status.Text = "<center>Status</center>";
            }
            tc_status.Attributes.Add("Class", "tr_th");
            tc_status.Style.Add("padding", "20px 5px");
            tc_status.Style.Add("vertical-align", "inherit");
            tc_status.Style.Add("border-radius", "0px 8px 8px 0px");
            tc_status.Controls.Add(lit_status);
            tr_header.Cells.Add(tc_status);
			if (ddlmode.SelectedValue == "11")
            {
                TableCell tc_reg = new TableCell();
                //tc_reg.BorderStyle = BorderStyle.Solid;
                //tc_reg.BorderWidth = 1;
                tc_reg.Width = 80;
                tc_reg.RowSpan = 1;
                Literal lit_reg = new Literal();
                lit_reg.Text = "<center>Registration Number Status</center>";
                //tc_reg.BorderColor = System.Drawing.Color.Black;
                tc_reg.Attributes.Add("Class", "rptCellBorder");
                tc_reg.Style.Add("font-family", "Calibri");
                tc_reg.Style.Add("font-size", "10pt");
                tc_reg.Controls.Add(lit_reg);
                tr_header.Cells.Add(tc_reg);
            }
            if (ddlmode.SelectedValue == "5")
            {
                TableCell tc_status2 = new TableCell();
                tc_status2.Width = 80;
                tc_status2.RowSpan = 1;
                Literal lit_status2 = new Literal();
                lit_status2.Text = "<center>Tagged drs</center>";
                tc_status2.Attributes.Add("Class", "tr_th");
                tc_status2.Style.Add("padding", "20px 5px");
                tc_status2.Style.Add("vertical-align", "inherit");
                tc_status2.Controls.Add(lit_status2);
                tr_header.Cells.Add(tc_status2);

                TableCell tc_status3 = new TableCell();
                tc_status3.Width = 80;
                tc_status3.RowSpan = 1;
                Literal lit_status3 = new Literal();
                lit_status3.Text = "<center>UnTagged drs</center>";
                tc_status3.BorderColor = System.Drawing.Color.Black;
                tc_status3.Attributes.Add("Class", "tr_th");
                tc_status3.Style.Add("padding", "20px 5px");
                tc_status3.Style.Add("vertical-align", "inherit");
                // tc_status3.Style.Add("border-radius", "0px 8px 8px 0px");
                tc_status3.Controls.Add(lit_status3);
                tr_header.Cells.Add(tc_status3);

                TableCell tc_status4 = new TableCell();
                tc_status4.Width = 80;
                tc_status4.RowSpan = 1;
                Literal lit_status4 = new Literal();
                lit_status4.Text = "<center>No.of.Lst.Chm</center>";
                tc_status4.BorderColor = System.Drawing.Color.Black;
                tc_status4.Attributes.Add("Class", "tr_th");
                tc_status4.Style.Add("padding", "20px 5px");
                tc_status4.Style.Add("vertical-align", "inherit");
                tc_status4.Controls.Add(lit_status4);
                tr_header.Cells.Add(tc_status4);

                TableCell tc_status5 = new TableCell();
                tc_status5.Width = 80;
                tc_status5.RowSpan = 1;
                Literal lit_status5 = new Literal();
                lit_status5.Text = "<center>Tagged Chm</center>";
                tc_status5.BorderColor = System.Drawing.Color.Black;
                tc_status5.Attributes.Add("Class", "tr_th");
                tc_status5.Style.Add("padding", "20px 5px");
                tc_status5.Style.Add("vertical-align", "inherit");
                tc_status5.Controls.Add(lit_status5);
                tr_header.Cells.Add(tc_status5);

                TableCell tc_status6 = new TableCell();
                tc_status6.Width = 80;
                tc_status6.RowSpan = 1;
                Literal lit_status6 = new Literal();
                lit_status6.Text = "<center>UnTagged Chm</center>";
                tc_status6.BorderColor = System.Drawing.Color.Black;
                tc_status6.Attributes.Add("Class", "tr_th");
                tc_status6.Style.Add("padding", "20px 5px");
                tc_status6.Style.Add("vertical-align", "inherit");
                tc_status6.Controls.Add(lit_status6);
                tr_header.Cells.Add(tc_status6);

                TableCell tc_status7 = new TableCell();
                tc_status7.Width = 80;
                tc_status7.RowSpan = 1;
                Literal lit_status7 = new Literal();
                lit_status7.Text = "<center>No.of.Lst.Stk</center>";
                tc_status7.BorderColor = System.Drawing.Color.Black;
                tc_status7.Attributes.Add("Class", "tr_th");
                tc_status7.Style.Add("padding", "20px 5px");
                tc_status7.Style.Add("vertical-align", "inherit");
                tc_status7.Controls.Add(lit_status7);
                tr_header.Cells.Add(tc_status7);

                TableCell tc_status8 = new TableCell();
                tc_status8.Width = 80;
                tc_status8.RowSpan = 1;
                Literal lit_status8 = new Literal();
                lit_status8.Text = "<center>Tagged Stk</center>";
                tc_status8.BorderColor = System.Drawing.Color.Black;
                tc_status8.Attributes.Add("Class", "tr_th");
                tc_status8.Style.Add("padding", "20px 5px");
                tc_status8.Style.Add("vertical-align", "inherit");
                tc_status8.Controls.Add(lit_status8);
                tr_header.Cells.Add(tc_status8);

                TableCell tc_status9 = new TableCell();
                tc_status9.Width = 80;
                tc_status9.RowSpan = 1;
                Literal lit_status9 = new Literal();
                lit_status9.Text = "<center>UnTagged Stk</center>";
                tc_status9.BorderColor = System.Drawing.Color.Black;
                tc_status9.Attributes.Add("Class", "tr_th");
                tc_status9.Style.Add("padding", "20px 5px");
                tc_status9.Style.Add("vertical-align", "inherit");
                tc_status9.Style.Add("border-radius", "0px 8px 8px 0px");
                tc_status9.Controls.Add(lit_status9);
                tr_header.Cells.Add(tc_status9);
            }
            else if (ddlmode.SelectedValue == "4")
            {
                lit_status.Text = "<center>Status</center>";
                //tc_status.BorderColor = System.Drawing.Color.Black;
                tc_status.ColumnSpan = 3;
                tc_status.RowSpan = 1;
                tc_status.Attributes.Add("Class", "tr_th");
                tc_status.Style.Add("padding", "20px 5px");
                tc_status.Style.Add("border-radius", "0px 8px 8px 0px");
                tc_status.Controls.Add(lit_status);
                tr_header.Cells.Add(tc_status);


                TableCell tc_status_sngl = new TableCell();
                Literal lit_status_sngl = new Literal();
                lit_status_sngl.Text = "<center>Draft Save</center>";
                tc_status_sngl.Attributes.Add("Class", "tr_th");
                tc_status_sngl.Style.Add("border-bottom", "10px solid #fff");
                tc_status_sngl.Style.Add("padding", "20px 5px");
                tc_status_sngl.Style.Add("vertical-align", "inherit");
                tc_status_sngl.Style.Add("border-radius", "0px 8px 8px 0px");
                tc_status_sngl.Controls.Add(lit_status_sngl);
                tr_det1.Cells.Add(tc_status_sngl);


                TableCell tc_status_Tot = new TableCell();
                Literal lit_status_Tot = new Literal();
                lit_status_Tot.Text = "<center>Approved</center>";
                tc_status_Tot.Attributes.Add("Class", "tr_th");
                tc_status_Tot.Style.Add("padding", "20px 5px");
                tc_status_Tot.Style.Add("vertical-align", "inherit");
                tc_status_Tot.Style.Add("border-radius", "0px 8px 8px 0px");
                tc_status_Tot.Controls.Add(lit_status_Tot);
                tr_det1.Cells.Add(tc_status_Tot);

            }
            if (ddlmode.SelectedValue == "4")
            {
                tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
                tc_SNo.BackColor = System.Drawing.ColorTranslator.FromHtml("#F1F5F8");
                
            }
            tbl.Rows.Add(tr_header);
            tbl.Rows.Add(tr_det1);

            if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                tr_header.BackColor = System.Drawing.Color.FromName("#F1F5F8");
            }

            if (dsSalesForce.Tables[0].Rows.Count > 0)
            { ViewState["dsSalesForce"] = dsSalesForce; }


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {
                TableRow tr_det = new TableRow();
                iCount += 1;
                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                //S.No
                TableCell tc_det_SNo = new TableCell();
                Literal lit_det_SNo = new Literal();
                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
                tc_det_SNo.Style.Add("vertical-align", "inherit");
                tc_det_SNo.Controls.Add(lit_det_SNo);
                tr_det.Cells.Add(tc_det_SNo);
                tr_det.BackColor = System.Drawing.Color.White;

                //SF_code
                TableCell tc_det_usr = new TableCell();
                Literal lit_det_usr = new Literal();
                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                tc_det_usr.Visible = false;
                tc_det_usr.Style.Add("vertical-align", "inherit");
                tc_det_usr.Controls.Add(lit_det_usr);
                tr_det.Cells.Add(tc_det_usr);

                //SF Name
                TableCell tc_det_FF = new TableCell();
                Literal lit_det_FF = new Literal();
                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
                tc_det_FF.Style.Add("vertical-align", "inherit");
                tc_det_FF.Controls.Add(lit_det_FF);
                tr_det.Cells.Add(tc_det_FF);

                //hq
                TableCell tc_det_hq = new TableCell();
                Literal lit_det_hq = new Literal();
                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
                tc_det_hq.Style.Add("vertical-align", "inherit");
                tc_det_hq.Controls.Add(lit_det_hq);
                tr_det.Cells.Add(tc_det_hq);

                //SF Designation Short Name
                TableCell tc_det_Designation = new TableCell();
                Literal lit_det_Designation = new Literal();
                if (ddlmode.SelectedValue == "4")
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["Designation_Short_Name"].ToString();
                }
                else
                {
                    lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
                }
                tc_det_Designation.Style.Add("vertical-align", "inherit");
                tc_det_Designation.Controls.Add(lit_det_Designation);
                tr_det.Cells.Add(tc_det_Designation);


                TableCell tc_tot_month = new TableCell();
                HyperLink hyp_month = new HyperLink();
                iTotLstCount = 0;


                //if (ddlmode.SelectedValue == "1")
                //{

                dsDoc = sf.getDrprdMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                dschm = sf.getchmMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                dsstk = sf.getstkMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                //}



                TableCell tc_lst_month = new TableCell();
                HyperLink hyp_lst_month = new HyperLink();
                HyperLink hyp_lst_month1 = new HyperLink();
                if (ddlmode.SelectedValue == "3")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        sfcsts = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    if (sfcsts == "*")
                    {
                        hyp_lst_month.ImageUrl = "../Images/correct.png";
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "7")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_dr != "0")
                    {

                        hyp_lst_month.Text = tot_dr;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp2('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp2('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;
                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "8")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        hyp_lst_month.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                else if (ddlmode.SelectedValue == "9")
                {
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        hyp_lst_month.Text = "Yes";
                        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        hyp_lst_month.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }
                //else if (ddlmode.SelectedValue == "4")
                //{
                //    if (dsDoc.Tables[0].Rows.Count > 0)
                //    {
                //        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                //    }
                //    else
                //    {
                //        tot_dr = "0";
                //    }


                //    if (tot_dr != "0")
                //    {

                //        hyp_lst_month.Text = tot_dr;
                //        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "";

                //        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp1('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "')");
                //        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp1('" + sURL + "')";
                //        hyp_lst_month.NavigateUrl = "#";
                //        //hyp_lst_month.BackColor = System.Drawing.Color.Yellow;                       
                //        //hyp_lst_month.Width = 50;


                //    }

                //    else
                //    {
                //        hyp_lst_month.Text = "-";
                //    }
                //}
                else if (ddlmode.SelectedValue == "4")
                {
                    TableCell tc_lst_month_Draft = new TableCell();
                    tot_dr = "0";
                    if (dsDoc.Tables[0].Rows.Count > 0)

                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    tot_draft = "0";
                    if (dsDoc.Tables[1].Rows.Count > 0)
                        tot_draft = dsDoc.Tables[1].Rows[0].ItemArray.GetValue(0).ToString();

                    if ((tot_draft != "0" && tot_draft != "") && (tot_dr == "0" || tot_dr == ""))
                    {

                        hyp_lst_month1.Text = tot_draft;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&div_code=" + ddlDivision.SelectedValue.ToString() + "";

                        hyp_lst_month1.Attributes.Add("href", "javascript:showModalPopUp1('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "','" + ddlDivision.SelectedValue.ToString() + "')");
                        hyp_lst_month1.Attributes["onclick"] = "javascript:showModalPopUp1('" + sURL + "')";
                        hyp_lst_month1.NavigateUrl = "#";
                        hyp_lst_month1.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;    
                        hyp_lst_month.Text = "-";

                    }

                    else
                    {
                        hyp_lst_month1.Text = "-";
                    }

                    if (tot_dr != "0" && tot_dr != "")
                    {
                        //if (tot_draft != "0" && tot_draft != "")
                        //{
                        //    hyp_lst_month.Text = "";
                        //}
                        //else
                        //{
                        hyp_lst_month.Text = tot_dr;


                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&div_code=" + ddlDivision.SelectedValue.ToString() + "";

                        hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp1('" + drFF["sf_code"].ToString() + "','" + drFF["sf_name"] + "','" + ddlDivision.SelectedValue.ToString() + "')");
                        hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp1('" + sURL + "')";
                        hyp_lst_month.NavigateUrl = "#";
                        hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;
                        //}
                        hyp_lst_month1.Text = "-";
                    }
                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                    //tc_lst_month_Draft.BorderStyle = BorderStyle.Solid;
                    //tc_lst_month_Draft.BorderWidth = 1;
                    //tc_lst_month_Draft.BackColor = System.Drawing.Color.White;
                    tc_lst_month_Draft.Width = 200;
                    tc_lst_month_Draft.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month_Draft.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month_Draft.Controls.Add(hyp_lst_month1);
                    tc_lst_month_Draft.Style.Add("font-family", "Calibri");
                    tc_lst_month_Draft.Style.Add("font-size", "10pt");
                    tc_lst_month_Draft.Style.Add("text-align", "center");
                    tc_lst_month_Draft.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_month_Draft);
                }
                else
                {
                    if (dschm.Tables[0].Rows.Count > 0)
                    {
                        tot_chm = dschm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        tot_chm = "0";
                    }

                    if (dsstk.Tables[0].Rows.Count > 0)
                    {
                        tot_stk = dsstk.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        tot_stk = "0";
                    }
                    if (ddlmode.SelectedValue == "5")
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                    else
                    {
                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        total_doc = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        if (ddlmode.SelectedValue == "11")
                        {
                            total_doc_reg = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        }
                    }

                    if (tot_dr != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        if (ddlmode.SelectedValue == "5")
                        {
                            hyp_lst_month.Text = tot_dr;
                        }
                        else
                        {
                            hyp_lst_month.Text =   (( tot_dr) + "|" + total_doc).ToString() ;
                            if (ddlmode.SelectedValue == "1" || ddlmode.SelectedValue == "6" || ddlmode.SelectedValue == "2" || ddlmode.SelectedValue == "12" || ddlmode.SelectedValue == "11")
                            {
                                sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";
                                if (ddlmode.SelectedValue == "1")
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                }
                                else if (ddlmode.SelectedValue == "2")
                                {
                                    string URL = sURL;
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp22('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "','" + ddlDivision.SelectedValue.ToString() + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp22('" + URL + "&Div_code=" + ddlDivision.SelectedValue.ToString() + "')";
                                }
                                else if (ddlmode.SelectedValue == "12")
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUpCheMap('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUpCheMap('" + sURL + "')";
                                }
								else if (ddlmode.SelectedValue == "11")
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp_VC('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "','" + ddlDivision.SelectedValue + "',1)");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp_VC('" + sURL + "')";
                                }
                                else
                                {
                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp5('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                                    hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp5('" + sURL + "')";
                                }
                                hyp_lst_month.NavigateUrl = "#";
                                // hyp_lst_month.BackColor = System.Drawing.Color.Yellow;                               
                            }
                        }

                        if (ddlmode.SelectedValue == "5")
                        {
                            sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";

                            hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp3('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                            hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp3('" + sURL + "')";
                            hyp_lst_month.NavigateUrl = "#";
                            hyp_lst_month.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            // hyp_lst_month.BackColor = System.Drawing.Color.Yellow;
                           
                        }

                    }

                    else
                    {
                        hyp_lst_month.Text = "-";
                    }
                }


                tc_lst_month.BackColor = System.Drawing.Color.White;
                tc_lst_month.Width = 200;
                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                tc_lst_month.Controls.Add(hyp_lst_month);
                tc_lst_month.Style.Add("vertical-align", "inherit");
                tr_det.Cells.Add(tc_lst_month);

                TableCell tcl_reg_val = new TableCell();
                HyperLink hyp_lst_month5 = new HyperLink();

                if (ddlmode.SelectedValue == "11")  //added by sujee
                {
                    tot_dr_draft = "0";
                    if (dsDoc.Tables[0].Rows.Count > 0)
                    { tot_dr_draft = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(1).ToString(); }
                    if (tot_dr_draft != "0" && tot_dr_draft != "")
                    {
                        hyp_lst_month5.Text = total_doc_reg + "/" + total_doc;
                        //hyp_lst_month5.Text = total_doc + "/" + total_doc_reg;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&div_code=" + ddlDivision.SelectedValue.ToString() + "";

                        hyp_lst_month5.Attributes.Add("href", "javascript:showModalPopUp_VC('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "','" + ddlDivision.SelectedValue + "',2)");
                        hyp_lst_month5.Attributes["onclick"] = "javascript:showModalPopUp_VC('" + sURL + "')";
                        hyp_lst_month5.NavigateUrl = "#";
                        hyp_lst_month5.BackColor = System.Drawing.Color.Yellow;
                        //hyp_lst_month.Width = 50;                      

                    }

                    else
                    {
                        hyp_lst_month5.Text = "-";
                    }

                    //tcl_reg_val.BorderStyle = BorderStyle.Solid;
                    //tcl_reg_val.BorderWidth = 1;
                    tcl_reg_val.BackColor = System.Drawing.Color.White;
                    tcl_reg_val.Width = 200;
                    tcl_reg_val.HorizontalAlign = HorizontalAlign.Center;
                    tcl_reg_val.VerticalAlign = VerticalAlign.Middle;
                    tcl_reg_val.Controls.Add(hyp_lst_month5);
                    tcl_reg_val.Style.Add("font-family", "Calibri");
                    tcl_reg_val.Style.Add("font-size", "10pt");
                    tcl_reg_val.Style.Add("text-align", "center");
                    tcl_reg_val.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tcl_reg_val);
                }
                if (ddlmode.SelectedValue == "5")
                {

                    dsDoc1 = sf.getDrgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_month1 = new TableCell();
                    //HyperLink hyp_lst_month1 = new HyperLink();
                    if (dsDoc1.Tables[0].Rows.Count > 0)


                        tot_dr1 = dsDoc1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();






                    if (tot_dr1 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month1.Text = tot_dr1;
                        //sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //sURL = "Geo_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        //hyp_lst_month1.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        //hyp_lst_month1.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month1.Text = "-";
                    }


                    tc_lst_month1.BackColor = System.Drawing.Color.White;
                    tc_lst_month1.Width = 200;
                    tc_lst_month1.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month1.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month1.Controls.Add(hyp_lst_month1);
                    tc_lst_month1.Style.Add("vertical-align", "inherit");
                    tr_det.Cells.Add(tc_lst_month1);

                    dsDoc2 = sf.getUnDrgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_month2 = new TableCell();
                    HyperLink hyp_lst_month2 = new HyperLink();
                    if (dsDoc2.Tables[0].Rows.Count > 0)

                        tot_dr2 = dsDoc2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_dr2 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_month2.Text = tot_dr2;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //hyp_lst_month1.Attributes.Add("href", "javascript:showModalPopUp4('" + drFF["sf_code"].ToString() + "')");
                        //hyp_lst_month1.Attributes["onclick"] = "javascript:showModalPopUp4('" + sURL + "')";
                        //hyp_lst_month1.NavigateUrl = "#";

                        sURL = "GeoUnList_ShowMap.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        //hyp_lst_month2.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        //hyp_lst_month2.NavigateUrl = "#";




                    }

                    else
                    {
                        hyp_lst_month2.Text = "-";
                    }



                    tc_lst_month2.BackColor = System.Drawing.Color.White;
                    tc_lst_month2.Width = 200;
                    tc_lst_month2.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month2.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_month2.Controls.Add(hyp_lst_month2);
                    tc_lst_month2.Style.Add("vertical-align", "inherit");
                    tr_det.Cells.Add(tc_lst_month2);

                    TableCell tc_lst_chm = new TableCell();
                    HyperLink hyp_lst_chm = new HyperLink();

                    if (tot_chm != "0")
                    {
                        hyp_lst_chm.Text = tot_chm;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";

                        hyp_lst_chm.Attributes.Add("href", "javascript:showModalPopUpchm('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                        hyp_lst_chm.Attributes["onclick"] = "javascript:showModalPopUpchm('" + sURL + "')";
                        hyp_lst_chm.NavigateUrl = "#";
                        hyp_lst_chm.BackColor = System.Drawing.Color.White;
                    }

                    else
                    {
                        hyp_lst_chm.Text = "-";
                    }

                    tc_lst_chm.BorderStyle = BorderStyle.Solid;
                    tc_lst_chm.BorderWidth = 1;
                    tc_lst_chm.BackColor = System.Drawing.Color.White;
                    tc_lst_chm.Width = 200;
                    tc_lst_chm.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_chm.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_chm.Controls.Add(hyp_lst_chm);
                    tc_lst_chm.Style.Add("font-family", "Calibri");
                    tc_lst_chm.Style.Add("font-size", "10pt");
                    tc_lst_chm.Style.Add("text-align", "center");
                    tc_lst_chm.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_chm);

                    dsDoc5 = sf.getChmgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_chm1 = new TableCell();
                    HyperLink hyp_lst_chm1 = new HyperLink();
                    if (dsDoc5.Tables[0].Rows.Count > 0)

                        tot_chm1 = dsDoc5.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_chm1 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_chm1.Text = tot_chm1;
                        //sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //sURL = "Geo_ShowMap_Chm.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        //hyp_lst_chm1.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        //hyp_lst_chm1.NavigateUrl = "#";
                    }
                    else
                    {
                        hyp_lst_chm1.Text = "-";
                    }

                    tc_lst_chm1.BorderStyle = BorderStyle.Solid;
                    tc_lst_chm1.BorderWidth = 1;
                    tc_lst_chm1.BackColor = System.Drawing.Color.White;
                    tc_lst_chm1.Width = 200;
                    tc_lst_chm1.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_chm1.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_chm1.Controls.Add(hyp_lst_chm1);
                    tc_lst_chm1.Style.Add("font-family", "Calibri");
                    tc_lst_chm1.Style.Add("font-size", "10pt");
                    tc_lst_chm1.Style.Add("text-align", "center");
                    tc_lst_chm1.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_chm1);

                    dsDoc6 = sf.getUnChmgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_chm2 = new TableCell();
                    HyperLink hyp_lst_chm2 = new HyperLink();
                    if (dsDoc6.Tables[0].Rows.Count > 0)

                        tot_chm6 = dsDoc6.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_chm6 != "0")
                    {
                        hyp_lst_chm2.Text = tot_chm6;
                    }
                    else
                    {
                        hyp_lst_chm2.Text = "-";
                    }
                    tc_lst_chm2.BorderStyle = BorderStyle.Solid;
                    tc_lst_chm2.BorderWidth = 1;
                    tc_lst_chm2.BackColor = System.Drawing.Color.White;
                    tc_lst_chm2.Width = 200;
                    tc_lst_chm2.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_chm2.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_chm2.Controls.Add(hyp_lst_chm2);
                    tc_lst_chm2.Style.Add("font-family", "Calibri");
                    tc_lst_chm2.Style.Add("font-size", "10pt");
                    tc_lst_chm2.Style.Add("text-align", "center");
                    tc_lst_chm2.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_chm2);
                    TableCell tc_lst_stk = new TableCell();
                    HyperLink hyp_lst_stk = new HyperLink();

                    if (tot_stk != "0")
                    {
                        hyp_lst_stk.Text = tot_stk;
                        sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&sf_short=" + drFF["sf_Designation_Short_Name"] + "&sf_hq=" + drFF["Sf_HQ"] + "";

                        hyp_lst_stk.Attributes.Add("href", "javascript:showModalPopUpstk('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "','" + drFF["sf_Designation_Short_Name"] + "','" + drFF["Sf_HQ"] + "')");
                        hyp_lst_stk.Attributes["onclick"] = "javascript:showModalPopUpstk('" + sURL + "')";
                        hyp_lst_stk.NavigateUrl = "#";
                        hyp_lst_stk.BackColor = System.Drawing.Color.White;
                    }
                    else
                    {
                        hyp_lst_stk.Text = "-";
                    }

                    tc_lst_stk.BorderStyle = BorderStyle.Solid;
                    tc_lst_stk.BorderWidth = 1;
                    tc_lst_stk.BackColor = System.Drawing.Color.White;
                    tc_lst_stk.Width = 200;
                    tc_lst_stk.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_stk.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_stk.Controls.Add(hyp_lst_stk);
                    tc_lst_stk.Style.Add("font-family", "Calibri");
                    tc_lst_stk.Style.Add("font-size", "10pt");
                    tc_lst_stk.Style.Add("text-align", "center");
                    tc_lst_stk.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_stk);

                    dsstk1 = sf.getStkgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_stk1 = new TableCell();
                    HyperLink hyp_lst_stk1 = new HyperLink();
                    if (dsstk1.Tables[0].Rows.Count > 0)
                        tot_stk1 = dsstk1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    if (tot_stk1 != "0")
                    {
                        //iTotLstCount += Convert.ToInt16(tot_dr);
                        hyp_lst_stk1.Text = tot_stk1;
                        //sURL = "&sf_code=" + drFF["Sf_Code"] + "";
                        //sURL = "Geo_ShowMap_stk.aspx?sfcode=" + drFF["sf_code"].ToString() + " ";
                        //hyp_lst_stk1.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=1000,height=600,left=0,top=0');";
                        //hyp_lst_stk1.NavigateUrl = "#";
                    }
                    else
                    {
                        hyp_lst_stk1.Text = "-";
                    }
                    tc_lst_stk1.BorderStyle = BorderStyle.Solid;
                    tc_lst_stk1.BorderWidth = 1;
                    tc_lst_stk1.BackColor = System.Drawing.Color.White;
                    tc_lst_stk1.Width = 200;
                    tc_lst_stk1.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_stk1.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_stk1.Controls.Add(hyp_lst_stk1);
                    tc_lst_stk1.Style.Add("font-family", "Calibri");
                    tc_lst_stk1.Style.Add("font-size", "10pt");
                    tc_lst_stk1.Style.Add("text-align", "center");
                    tc_lst_stk1.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_stk1);

                    dsstk2 = sf.getUnStkgeoMap_Status(drFF["sf_code"].ToString(), ddlDivision.SelectedValue.ToString(), ddlmode.SelectedValue);
                    TableCell tc_lst_stk2 = new TableCell();
                    HyperLink hyp_lst_stk2 = new HyperLink();
                    if (dsstk2.Tables[0].Rows.Count > 0)
                        tot_stk2 = dsstk2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                    if (tot_stk2 != "0")
                    {
                        hyp_lst_stk2.Text = tot_stk2;
                    }
                    else
                    {
                        hyp_lst_stk2.Text = "-";
                    }
                    tc_lst_stk2.BorderStyle = BorderStyle.Solid;
                    tc_lst_stk2.BorderWidth = 1;
                    tc_lst_stk2.BackColor = System.Drawing.Color.White;
                    tc_lst_stk2.Width = 200;
                    tc_lst_stk2.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_stk2.VerticalAlign = VerticalAlign.Middle;
                    tc_lst_stk2.Controls.Add(hyp_lst_stk2);
                    tc_lst_stk2.Style.Add("font-family", "Calibri");
                    tc_lst_stk2.Style.Add("font-size", "10pt");
                    tc_lst_stk2.Style.Add("text-align", "center");
                    tc_lst_stk2.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_lst_stk2);

                }
                tbl.Rows.Add(tr_det);
            }

        }
        else
        {
            TableRow tr_det_sno = new TableRow();
            TableCell tc_det_SNo = new TableCell();
            Literal lit_det_SNo = new Literal();
            lit_det_SNo.Text = "No Record Found";
            tc_det_SNo.BorderStyle = BorderStyle.Solid;
            // tc_det_SNo.Attributes.Add("Class", "no-result-area");
            tc_det_SNo.Style.Add("border", "solid 1px #d1e2ea");
            tc_det_SNo.Style.Add("text-align", "center");
            tc_det_SNo.Style.Add("padding", "10px");
            tc_det_SNo.Style.Add("color", "#696d6e");
            tc_det_SNo.Style.Add(" font-size", " 18px");
            tc_det_SNo.Style.Add(" margin-top", " 15px");
            tc_det_SNo.Style.Add(" background", "none");

            tc_det_SNo.HorizontalAlign = HorizontalAlign.Center;
            tc_det_SNo.BorderWidth = 1;
            tc_det_SNo.BorderStyle = BorderStyle.None;
            tc_det_SNo.Controls.Add(lit_det_SNo);
            tr_det_sno.Cells.Add(tc_det_SNo);

            tbl.Rows.Add(tr_det_sno);
            pnlprint.Visible = false;
        }
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {
        //string strFileName = "UserList";
        //Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //HtmlForm frm = new HtmlForm();
        //ListItem .HeaderRow.Style.Add("font-size", "10px");
        //grdSalesForce.Style.Add("text-decoration", "none");
        //grdSalesForce.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //grdSalesForce.Style.Add("font-size", "8px");

        //grdSalesForce.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(grdSalesForce);
        //frm.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        ////  Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 4f);

        //Document pdfDoc = new Document(PageSize.A2, 7f, 7f, 7f, 0f);

        //iTextSharp.text.html.simpleparser.HTMLWorker htmlparser = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //Response.Write(pdfDoc);
        //Response.End();
    }

   
}