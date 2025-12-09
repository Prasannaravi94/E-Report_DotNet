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
using iTextSharp.text.xml.simpleparser;
using iTextSharp.text.xml;
using iTextSharp.text.io;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_rpt_Secondary_StkProd_Bill_View : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string Sf_Code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string Mode = string.Empty;
    string HQ_Code = string.Empty;
    string HQ_Name = string.Empty;
    string ERPcode_Duplicat = string.Empty;

    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowClr_view = new System.Data.DataTable();
    string Brand_Code = string.Empty;
    string StName = string.Empty;
    string Brand_Name = string.Empty;
    string strFieledForceName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        Mode = Request.QueryString["Mode"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        if (Mode == "1" || Mode == "2" || Mode == "3")
        {
            div_code = Session["div_code"].ToString();
            Sf_Code = Request.QueryString["Sf_Code"].ToString();
            FillReport();
        }
        else
        {
            div_code = Request.QueryString["div_Code"].ToString();
            StName = Request.QueryString["Div_New"].ToString();
            if (div_code == "ALL")
            {
                if (StName == "ALL")
                {
                    Sf_Code = "admin";
                    div_code = Request.QueryString["sf_code"].ToString();
                }
                else
                {
                    Sf_Code = "admin";
                    div_code = Request.QueryString["Div_New"].ToString();
                }
            }
            else
            {
                Sf_Code = Request.QueryString["sf_code"].ToString();
                div_code = Request.QueryString["div_Code"].ToString();
            }
            Brand_Code = Request.QueryString["Brand_Code"].ToString();
            Brand_Name = Request.QueryString["Brand_Name"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            lblBrndName.Text = "      Brand Name : " + "<span style='color:magenta;'>" + Brand_Name + "</span>";
            LblForceName.Text = "Field Force Name : " + "<span style='color:magenta;'>" + strFieledForceName + "</span>";
            FillReport1();
        }
        

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
        if (Mode == "1")
        {
            lblHead.Text = "Secondary Stockist Wise View From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        }
        else if (Mode == "2")
        {
            lblHead.Text = "Secondary Product Wise View From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        }
        else if (Mode == "3")
        {
            lblHead.Text = "Secondary HQ Wise View From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        }
        else if (Mode == "5")
        {
            lblHead.Text = "Secondary Product Wise View From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        }
        
    }

    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        if (Mode == "1")
        {
            sProc_Name = "Secondary_bill_Stockist_View";
        }
        else if (Mode == "2")
        {
            sProc_Name = "Secondary_bill_Product_View";
        }
        else if (Mode == "3")
        {
            sProc_Name = "Secondary_TargetVsSale_HQwise";
        }

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", Request.QueryString["Sf_Code"].ToString());
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.CommandTimeout = 500;
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        if (Mode == "1")
        {
            dsts.Tables[0].Columns.Remove("Stockist_Code");
            dsts.Tables[0].Columns.Remove("Erp_code");
        }
        else if (Mode == "2")
        {
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(2);
            dsts.Tables[0].Columns.RemoveAt(1);
        }
        else if (Mode == "3")
        {
            dsts.Tables[0].Columns.Remove("MRCount");

            dsts.Tables[0].Columns.Remove("cntRept");
            dsts.Tables[0].Columns.Remove("minMR");
            dsts.Tables[0].Columns.Remove("maxMR");
            dsts.Tables[0].Columns.Remove("HQ_code");
            dsts.Tables[0].Columns.Remove("StkCount");
        }
        //dsts.Tables[0].Columns.RemoveAt(0);
        //dsts.Tables[0].Columns.RemoveAt(3);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }

    private void FillReport1()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        //
        while (months >= 0)
        {
            if (cmonth == 13)
            {
                cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
            }
            else
            {
                iMn = cmonth; iYr = cyear;
            }
            dtMnYr.Rows.Add(null, iMn, iYr);
            months--; cmonth++;
        }
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        //if (Stok_code.Trim() == "-2")
        //{
        //sProc_Name = "TargetVsSales_new";
        sProc_Name = "SecondarySale_Brand_MultiDiv_P";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", (div_code) + ",");
        cmd.Parameters.AddWithValue("@Msf_code", Sf_Code);
        cmd.Parameters.AddWithValue("@Brand_Code", Brand_Code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("Product_Code");
        dsts.Tables[0].Columns.Remove("Product_Brd_Name");
        dsts.Tables[0].Columns.Remove("Product_Brd_Code");
        dsts.Tables[0].Columns.Remove("Product_Code1");


        GrdFixation.DataSource = dsts.Tables[0];
        GrdFixation.DataBind();

    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
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

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell3 = new TableCell();
            #region HQWise
            if (Mode == "3")
            {
                #region Merge cells
                AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Fieldforce Name", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Desigantion", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "HQ Code", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "HQ Name", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 0, "Stk Count", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 0, "FF Count", "#0097AC", true);

                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                for (int i = 0; i <= months1; i++)
                {
                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells(objgridviewrow, objtablecell, 3, sTxt, "#0097AC", true);

                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Target", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 1, "Value", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Sale", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 1, "Value", "#0097AC", false);

                    AddMergedCells(objgridviewrow2, objtablecell2, 1, "Achieve (%)", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 1, "Value", "#0097AC", false);

                    cmonth1 = cmonth1 + 1;
                    if (cmonth1 == 13)
                    {
                        cmonth1 = 1;
                        cyear1 = cyear1 + 1;
                    }
                }
                //lblHead.Text = "Month Wise Visit Details between : " + Convert.ToDateTime("01-" + ttlSpc[0].Substring(0, 2).ToString() + "-" + ttlSpc[0].Substring(3, 4).ToString()).ToString("MMMMM-yyyy")
                //    + " To " + Convert.ToDateTime("01-" + ttlSpc[ttlSpc.Length - 1].Substring(0, 2).ToString() + "-" + ttlSpc[ttlSpc.Length - 1].Substring(3, 4).ToString()).ToString("MMMMM-yyyy");

                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.   
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                //objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                //
                #endregion
            }
            #endregion

            else
            {
                #region Merge cells
                AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
                if (Mode == "1")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist ERP Code", "#0097AC", true);
                }
                else if (Mode == "2" )
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "ERP Code", "#0097AC", true);
                }
                else if (Mode == "5")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 0, "ERP Code", "#0097AC", true);
                }

                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                ViewState["months"] = months1;
                ViewState["cmonth"] = cmonth1;
                ViewState["cyear"] = cyear1;
                //GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                if (months1 >= 0)
                {
                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        //TableCell objtablecell2 = new TableCell();
                        if (Mode == "1")
                        {
                            AddMergedCells(objgridviewrow, objtablecell, 1, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#0097AC", false);
                        }
                        else if (Mode == "2")
                        {
                            AddMergedCells(objgridviewrow, objtablecell, 2, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#0097AC", false);
                        }
                        else if (Mode == "5")
                        {
                            AddMergedCells(objgridviewrow, objtablecell, 3, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                            AddMergedCells(objgridviewrow2, objtablecell2, 1, "Target", "#0097AC", false);
                            //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                            AddMergedCells(objgridviewrow2, objtablecell2, 1, "Sale", "#0097AC", false);
                            //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                            AddMergedCells(objgridviewrow2, objtablecell2, 1, "Previous", "#0097AC", false);
                            //AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                            AddMergedCells(objgridviewrow, objtablecell, 0, "Achieve (%)", "#0097AC", true);
                            AddMergedCells(objgridviewrow, objtablecell, 0, "Growth", "#0097AC", true);
                            AddMergedCells(objgridviewrow, objtablecell, 0, "PCPM", "#0097AC", true);
                        }
                       
                        iLstVstmnt.Add(cmonth1);
                        iLstVstyr.Add(cyear1);

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                }


                //Lastly add the gridrow object to the gridview object at the 0th position
                //Because, the header row position is 0.
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                if (Mode == "5")
                {
                    objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                }
                    #endregion
                }
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            if (Mode == "5")
            {
                objtablecell.RowSpan = 3;
            }
            else
            {
                objtablecell.RowSpan = 2;
            }
        }
        else
        {
            objtablecell.Style.Add("background-color", "#f8f9fa");
            objtablecell.Style.Add("color", "#6c757d");
            objtablecell.Style.Add("border-color", "#DCE2E8");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void AddMergedCells_HQ(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan, bool Clr)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 10;
        //objtablecell.ForeColor = System.Drawing.Color.White;
        //objtablecell.Style.Add("background-color", backcolor);
        if (Clr)
        {
            objtablecell.Style.Add("background-color", "White");
            objtablecell.Style.Add("color", "#6c757d");
            //objtablecell.Style.Add("border-color", "black");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations
            #region HQWise
            if (Mode == "3")
            {
                if (e.Row.Cells[1].Text == "ZZZZZ")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "Grand Total";
                    //e.Row.Cells[3].Text = "";
                    //e.Row.Cells[4].Text = "";

                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }
                float first = 0, second = 0, third = 0, fourth = 0;

                int cnt = 0;

                string saleval = "0";
                string TargetVal = "0";

                int cmonth1 = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
                int cyear1 = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());

                for (int l = 3; l < e.Row.Cells.Count; l++)
                {
                    string value = string.Empty;

                    cnt += 1;
                    if (cnt != 3)
                    {
                        if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[2].Text == "Grand Total")
                            {
                                if (cnt == 2)
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        saleval = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");

                                        HyperLink hLink_sale = new HyperLink();
                                        hLink_sale.Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                        hLink_sale.Attributes.Add("class", "btnDrMsd");
                                        // hLink_sale.Attributes.Add("onclick", "javascript:showModalPopUp('" + Request.QueryString["sf_code"].ToString() + "',  '" + cmonth1 + "', '" + cyear1 + "', '" + cmonth1 + "', '" + cyear1 + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + "3" + "')");
                                        hLink_sale.ToolTip = "Click here";
                                        hLink_sale.Attributes.Add("style", "cursor:pointer");
                                        hLink_sale.Font.Underline = true;
                                        hLink_sale.Font.Bold = true;
                                        hLink_sale.ForeColor = System.Drawing.Color.Red;
                                        e.Row.Cells[l].Controls.Add(hLink_sale);
                                    }
                                    else
                                    {
                                        saleval = "0";
                                    }
                                }
                                else
                                {
                                    TargetVal = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                    e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                }
                                //e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.00");
                            }
                            else
                            {

                                if (cnt == 2)
                                {
                                    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "" && e.Row.Cells[l].Text != "&nbsp;")
                                    {
                                        saleval = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                    }
                                }
                                else
                                {
                                    TargetVal = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                                }
                                //decimal step = (decimal)Math.Pow(10, 2);
                                //decimal tmp = Math.Truncate(step * (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000));
                                //value = (tmp / step).ToString();

                                //e.Row.Cells[l].Text = value;

                                e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.000");
                            }
                            //
                        }
                    }

                    if (cnt == 3)
                    {
                        if (e.Row.Cells[l - 2].Text != "0" && e.Row.Cells[l - 2].Text != "&nbsp;")
                        {
                            if (e.Row.Cells[2].Text == "Grand Total")
                            {
                                //e.Row.Cells[l].Text = (((Convert.ToDecimal(saleval)) / (Convert.ToDecimal(TargetVal))) * 100).ToString("0.00");
                                e.Row.Cells[l].Text = ((((Convert.ToDecimal(saleval)) / (Convert.ToDecimal(TargetVal))) * 100)).ToString("0.000");
                            }
                            else
                            {

                                //decimal step = (decimal)Math.Pow(10, 2);
                                //decimal tmp = Math.Truncate(step * (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100));
                                //value = (tmp / step).ToString();

                                //e.Row.Cells[l].Text = value;

                                e.Row.Cells[l].Text = (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100).ToString("0.000");

                                //e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text)).ToString("0.00");
                            }
                        }

                        saleval = "0";
                        TargetVal = "0";
                        //l += 3;
                        cnt = 0;

                        cmonth1 = cmonth1 + 1;
                        if (cmonth1 == 13)
                        {
                            cmonth1 = 1;
                            cyear1 = cyear1 + 1;
                        }
                    }
                    else
                    {
                        //  l += 1;
                    }


                }

                //for (int l = 5; l < e.Row.Cells.Count; l++)
                //{
                //    if (e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "&nbsp;")
                //    {
                //        e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text) / 100000).ToString("0.00");
                //    }
                //    if (e.Row.Cells[l - 1].Text != "0" && e.Row.Cells[l - 1].Text != "&nbsp;")
                //    {
                //        e.Row.Cells[l - 1].Text = Math.Round(double.Parse(e.Row.Cells[l - 1].Text), 0).ToString();
                //    }
                //    cnt += 1;
                //    if (cnt == 2)
                //    {

                //        if (e.Row.Cells[l + 1].Text != "0" && e.Row.Cells[l + 1].Text != "&nbsp;")
                //        {
                //            e.Row.Cells[l + 1].Text = (((Convert.ToDecimal(e.Row.Cells[l - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 3].Text))) * 100).ToString("0.00");


                //            e.Row.Cells[l + 1].Text = (Convert.ToDecimal(e.Row.Cells[l + 1].Text)).ToString("0.00");
                //        }

                //        if (e.Row.Cells[l + 2].Text != "0" && e.Row.Cells[l + 2].Text != "&nbsp;")
                //        {
                //            e.Row.Cells[l + 2].Text = (((Convert.ToDecimal(e.Row.Cells[l].Text)) / (Convert.ToDecimal(e.Row.Cells[l - 2].Text))) * 100).ToString("0.00");

                //            e.Row.Cells[l + 2].Text = (Convert.ToDecimal(e.Row.Cells[l + 2].Text)).ToString("0.00");
                //        }

                //        l += 3;
                //        cnt = 0;
                //    }
                //    else
                //    {
                //        l += 1;
                //    }
                //}




                for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text == "0" || e.Row.Cells[i].Text == "" || e.Row.Cells[i].Text == "&nbsp;")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "Right");
                }
                try
                {
                    int f = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    // e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[f][8].ToString()));
                }
                catch
                {
                    // e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }
            }
            #endregion
            else
            {

                if (Mode == "1")
                {

                    for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                    {
                        if (e.Row.Cells[l].Text != "&nbsp;" && e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-" && e.Row.Cells[l].Text != "")
                        {
                            e.Row.Cells[l].Text = ((Convert.ToDecimal(e.Row.Cells[l].Text)) / 100000).ToString();
                            //if (e.Row.Cells[l].Text.Contains("."))
                            //{
                            //    e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text)).ToString("#0.00");
                            //}
                        }
                        if (e.Row.Cells[l].Text != "0")
                        {


                        }
                        j++;
                    }

                    for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("align", "Right");
                    }
                }
                else if (Mode == "2")
                {
                    for (int l = 5, j = 0; l < e.Row.Cells.Count; l++)
                    {
                        if (e.Row.Cells[l].Text != "&nbsp;" && e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-" && e.Row.Cells[l].Text != "")
                        {
                            e.Row.Cells[l].Text = ((Convert.ToDecimal(e.Row.Cells[l].Text)) / 100000).ToString();
                            //if (e.Row.Cells[l].Text.Contains("."))
                            //{
                            //    e.Row.Cells[l].Text = (Convert.ToDecimal(e.Row.Cells[l].Text)).ToString("#0.00");
                            //}
                        }

                        l = l + 1;
                    }

                    for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Attributes.Add("align", "Right");
                    }
                }
                else if (Mode == "5")
                {
                    if (e.Row.Cells[2].Text == "ZZZZZ")
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[2].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].ColumnSpan = 5;
                        e.Row.Cells[4].Text = "Grand Total";
                        for (int n = 0; n <= e.Row.Cells.Count - 1; n++)
                        {
                            e.Row.Cells[n].HorizontalAlign = HorizontalAlign.Center;
                            e.Row.Cells[n].Height = 30;

                            e.Row.Cells[n].Attributes.Add("style", "font-weight:bold;");
                            e.Row.Cells[n].Style.Add("font-size", "10pt");
                            e.Row.Cells[n].Style.Add("color", "Red");
                            e.Row.Cells[n].Style.Add("border-color", "#DCE2E8");
                        }
                    }
                    for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
                    {
                        if (e.Row.Cells[i].Text == "0")
                        {
                            e.Row.Cells[i].Text = "";
                        }
                        if (e.Row.Cells[i].Text.Contains("."))
                        {
                            e.Row.Cells[i].Text = Convert.ToDecimal(e.Row.Cells[i].Text).ToString("#0.000");
                        }
                        e.Row.Cells[i].Attributes.Add("align", "Right");
                    }

                     
                }
            }

            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                //e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            if (Mode != "3")
            {

                if (Mode == "1")
                {
                    if (ERPcode_Duplicat == e.Row.Cells[2].Text)
                    {
                        //e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Attributes.Add("style", "background-color:Pink;font-bold:true; font-size:14px; Color:darkpink; border-color:Black");
                    }
                    ERPcode_Duplicat = e.Row.Cells[2].Text;
                    if (dtrowClr.Rows[indx][0].ToString() == "Grand Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].Text = Convert.ToDecimal((e.Row.Cells[i].Text)).ToString("#0.000");
                        }
                    }
                }
                else if (Mode == "2")
                {

                    if(ERPcode_Duplicat== e.Row.Cells[3].Text)
                    {
                        e.Row.Attributes.Add("style", "background-color:Pink;font-bold:true; font-size:14px; Color:darkpink; border-color:Black");
                    }
                    ERPcode_Duplicat = e.Row.Cells[3].Text;

                    if (dtrowClr.Rows[indx][3].ToString() == "Grand Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
                        {
                            e.Row.Cells[i].Text = Convert.ToDecimal((e.Row.Cells[i].Text)).ToString("#0.000");
                            i += 1;
                        }
                    }
                }


                






            }

            e.Row.Cells[1].Wrap = false;

        }


    }


    #region GrdFixationView




    protected void AddMergedCells_View(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Font.Size = 7;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    #endregion


   
}