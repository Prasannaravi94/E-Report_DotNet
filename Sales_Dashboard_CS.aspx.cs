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

public partial class Sales_Dashboard_CS : System.Web.UI.Page
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

    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowClr_view = new System.Data.DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        // div_code = Session["div_code"].ToString();
        div_code = Request.QueryString["div_code"].ToString();
        FMonth = Request.QueryString["Frm_Month"].ToString();
        FYear = Request.QueryString["Frm_year"].ToString();
        TMonth = Request.QueryString["To_Month"].ToString();
        TYear = Request.QueryString["To_year"].ToString();
        Sf_Code = Request.QueryString["Sf_Code"].ToString();
        Mode = Request.QueryString["Mode"].ToString();
     
        if (Mode == "2" || Mode == "4")
        {
            HQ_Code = Request.QueryString["HQ_Code"].ToString();
            HQ_Name = "Common Kolkata";

            if (Mode == "2")
            {
              //  FillReport_view();

            
            }
        }

        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth.Trim());
        string strToMonth = sf.getMonthName(TMonth.Trim());
     
            lblHead.Text = "Primary Stockistwise From " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
        
        FillReport();

        

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
            sProc_Name = "Primary_bill_Stockist_View_crn";
        }
        else if (Mode == "2")
        {
            sProc_Name = "Primary_Sale_Common_Kolkata";
        }
      
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", "admin");
       
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        
            cmd.Parameters.AddWithValue("@HQ_Code", HQ_Code);
        cmd.Parameters.AddWithValue("@Mgr_Hq", "");
        cmd.Parameters.AddWithValue("@mgr_code", Sf_Code);
        // cmd.Parameters.AddWithValue("@stk_Code", Stok_code);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();

        if (Mode == "3")
        {
            dsts.Tables[0].Columns.Remove("cntRept");
            dsts.Tables[0].Columns.Remove("minMR");
            dsts.Tables[0].Columns.Remove("maxMR");
            dsts.Tables[0].Columns.Remove("HQ_code");
            dsts.Tables[0].Columns.Remove("StkCount");
        }
        else if (Mode == "4")
        {
            dsts.Tables[0].Columns.Remove("sf_code");
            dsts.Tables[0].Columns.Remove("sf_TP_Active_Flag");
        }
        else
        {
            dsts.Tables[0].Columns.Remove("Stockist_Code");
            dsts.Tables[0].Columns.Remove("Stockist_Code1");

            if (Mode == "2")
            {
                dsts.Tables[0].Columns.Remove("HQ_Name");
                dsts.Tables[0].Columns.Remove("StkMR_Cnt");
                dsts.Tables[0].Columns.Remove("MaxStkMR_Cnt");
            }
        }
        //dsts.Tables[0].Columns.RemoveAt(0);
        //dsts.Tables[0].Columns.RemoveAt(3);
        GrdFixation.DataSource = dsts;
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

            #region Merge cells


            if (Mode == "3")
            {

                #region Merge cells
                GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell objtablecell2 = new TableCell();
                GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell objtablecell3 = new TableCell();

                AddMergedCells_HQ(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true, false);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Fieldforce Name", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Desigantion", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                AddMergedCells_HQ(objgridviewrow, objtablecell, 3, 0, "HQ Code", "#0097AC", true, false);
                AddMergedCells_HQ(objgridviewrow, objtablecell, 3, 0, "HQ Name", "#0097AC", true, false);
                //AddMergedCells_HQ(objgridviewrow, objtablecell, 3, 0, "Stk Count", "#0097AC", true);

                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                for (int i = 0; i <= months1; i++)
                {

                    int cyear1_last = cyear1 - 1;
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1;
                    string sTxt_last = "&nbsp;" + sf.getMonthName(cmonth1.ToString()) + "-" + cyear1_last;
                    AddMergedCells_HQ(objgridviewrow, objtablecell, 0, 3, sTxt, "#0097AC", true, false);

                    AddMergedCells_HQ(objgridviewrow2, objtablecell2, 0, 1, "Target", "#0097AC", false, false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells_HQ(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false, true);
                    AddMergedCells_HQ(objgridviewrow2, objtablecell2, 0, 1, "Sale", "#0097AC", false, false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells_HQ(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false, false);

                    AddMergedCells_HQ(objgridviewrow2, objtablecell, 0, 1, "Achieve (%)", "#0097AC", false, false);
                    //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Qty", "#0097AC", false);
                    AddMergedCells_HQ(objgridviewrow3, objtablecell3, 0, 0, "Value", "#0097AC", false, false);

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
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                //
                #endregion
            }
            else if (Mode == "4")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", false);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#0097AC", false);
                AddMergedCells(objgridviewrow, objtablecell, 0, "FF HQ Code", "#0097AC", false);
                AddMergedCells(objgridviewrow, objtablecell, 0, "FF HQ Name", "#0097AC", false);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", false);
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            }
            else
            {

                AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);

                if (Mode == "2")
                {
                    //AddMergedCells(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#0097AC", true);
                }
                AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist ERP Code", "#0097AC", true);

                if (Mode == "2")
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, "State Name", "#0097AC", true);
                  //  AddMergedCells(objgridviewrow, objtablecell, 0, "FF HQ Name", "#0097AC", true);
                   // AddMergedCells(objgridviewrow, objtablecell, 0, "FF Count", "#0097AC", true);
                }
                //  AddMergedCells(objgridviewrow, objtablecell, 0, "Rate", "#0097AC", true);
                int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth1 = Convert.ToInt32(FMonth);
                int cyear1 = Convert.ToInt32(FYear);

                ViewState["months"] = months1;
                ViewState["cmonth"] = cmonth1;
                ViewState["cyear"] = cyear1;
                GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                if (months1 >= 0)
                {

                    for (int j = 1; j <= months1 + 1; j++)
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 1, sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);

                        TableCell objtablecell2 = new TableCell();
                        //  AddMergedCells(objgridviewrow2, objtablecell2, 0, "Qty", "#0097AC", false);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, "Value", "#0097AC", false);

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
            objtablecell.RowSpan = 2;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
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
                for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                    {
                      //  e.Row.Cells[i].Text = ((Convert.ToDecimal(e.Row.Cells[i].Text)) / 100000).ToString();
                    }

                    e.Row.Cells[i].Attributes.Add("align", "Right");
                }

                if (dtrowClr.Rows[indx][7].ToString() != e.Row.Cells[4].Text)
                {
                    e.Row.Attributes.Add("style", "  Color:#C71585; border-color:Black");
                }


                e.Row.Cells[1].Wrap = false;

            }

            else if (Mode == "3")
            {

                if (e.Row.Cells[2].Text == "ZZZZZ")
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "Grand Total";
                    //e.Row.Cells[3].Text = "";
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                }

                int cnt = 0;

                for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
                {
                    cnt += 1;
                    if (cnt != 3)
                    {
                        if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "")
                        {
                            e.Row.Cells[i].Text = ((Convert.ToDecimal(e.Row.Cells[i].Text)) / 100000).ToString();
                        }
                    }
                    if (cnt == 3)
                    {
                        if (e.Row.Cells[i - 2].Text != "0" && e.Row.Cells[i - 2].Text != "&nbsp;")
                        {
                            e.Row.Cells[i].Text = (((Convert.ToDecimal(e.Row.Cells[i - 1].Text)) / (Convert.ToDecimal(e.Row.Cells[i - 2].Text))) * 100).ToString("#0.00");
                        }
                        cnt = 0;
                    }
                    e.Row.Cells[i].Attributes.Add("align", "Right");
                }

                e.Row.Cells[1].Wrap = false;

            }


            else if (Mode == "4")
            {
                if (indx == 1)
                {
                    e.Row.Cells[0].Style.Add("background-color", "White");
                    e.Row.Cells[0].Style.Add("color", "#6c757d");
                    e.Row.Cells[0].Style.Add("border-color", "#DCE2E8");
                }

                //string Stk_Name = e.Row.Cells[4].Text;
                e.Row.Cells[4].Text =  (e.Row.Cells[4].Text).Replace("~", "<br/>") ;

                for (int i = 0, j = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("align", "Left");
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
                if (dtrowClr.Rows[indx][0].ToString() == "Grand Total")
                {
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                    if (Mode == "2")
                    {
                        //e.Row.Cells[1].Text = "";
                    }
                    //e.Row.Cells[1].Text = "";
                    decimal total = 0;

                    for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                    {
                        if (e.Row.Cells[l].Text != "&nbsp;" && e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-" && e.Row.Cells[l].Text != "")
                        {
                          //  e.Row.Cells[l].Text = "";
                            total += Convert.ToDecimal(e.Row.Cells[l].Text);

                          
                        }
                    }
                    SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select Relevant_HQ_Code from Trans_Customized where division_code='" + div_code + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DB_EReporting db_ER = new DB_EReporting();
                        DataSet dsmgr = new DataSet();
                        DataSet dsind = new DataSet();
                        string strQry2 = "Exec Common_Kolkata_Hirarchy '" + ds.Tables[0].Rows[0]["Relevant_HQ_Code"].ToString() + "', '" + div_code + "' ";
                        dsmgr = db_ER.Exec_DataSet(strQry2);
                        if (dsmgr.Tables[0].Rows.Count > 0)
                        {
                            lblmrcnt.Text = dsmgr.Tables[0].Rows.Count.ToString();
                            lblbifur.Text = total.ToString() + " / " + lblmrcnt.Text + " = ";

                            lblfinal.Text = (Decimal.Divide(Convert.ToDecimal(total.ToString()), Convert.ToDecimal(lblmrcnt.Text))).ToString("0.##");
                            if (Sf_Code.Contains("MR"))
                            {
                               
                                lblind.Text = lblfinal.Text;
                            }
                            else
                            {
                                string strQry3 = "Exec Common_Hirarchy_Level '" + div_code + "' ,'"+Sf_Code+"', '" + ds.Tables[0].Rows[0]["Relevant_HQ_Code"].ToString() + "'";
                                dsind = db_ER.Exec_DataSet(strQry3);
                                if (dsind.Tables[0].Rows.Count > 0)
                                {
                                    string final = (Decimal.Divide(Convert.ToDecimal(total.ToString()), Convert.ToDecimal(lblmrcnt.Text)) * Convert.ToDecimal(dsind.Tables[0].Rows.Count.ToString())).ToString("0.##");
                                    lblind.Text = final;
                                    lbltot.Text = " ( " + dsind.Tables[0].Rows.Count.ToString() + " MR's )";
                                }

                            }
                        }
                    }
                }
            }

        }

    }


    #region GrdFixationView


    //private void FillReport_view()
    //{

    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    string sProc_Name = "";

    //    sProc_Name = "Sp_PSaleHQwise_Stk_Zoom";

    //    SqlCommand cmd = new SqlCommand(sProc_Name, con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //    cmd.Parameters.AddWithValue("@Msf_code", Sf_Code);
    //    cmd.Parameters.AddWithValue("@HQ_Code", HQ_Code);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);

    //    DataSet dsts_view = new DataSet();
    //    da.Fill(dsts_view);
    //    dtrowClr_view = dsts_view.Tables[0].Copy();

    //    dsts_view.Tables[0].Columns.Remove("sf_code");
    //    dsts_view.Tables[0].Columns.Remove("Stockist_Code");
    //    dsts_view.Tables[0].Columns.Remove("Mode");

    //    GrdFixation_View.DataSource = dsts_view;
    //    GrdFixation_View.DataBind();

    //    GrdFixation_View.Visible = true;
    //}


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


    protected void GrdFixation_View_RowCreated(object sender, GridViewRowEventArgs e)
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
            #region Merge cells

            AddMergedCells_View(objgridviewrow, objtablecell, 0, "#", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "Sl No", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "Stockist ERP Code", "#0097AC", false);

            AddMergedCells_View(objgridviewrow, objtablecell, 0, "State Name", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "FF HQ Code", "#0097AC", false);
            AddMergedCells_View(objgridviewrow, objtablecell, 0, "FF HQ Name", "#0097AC", false);

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            //objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }


    protected void GrdFixation_View_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            e.Row.Font.Size = 7;
            #region Calculations

            for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
            {
                if (e.Row.Cells[l].Text != "0")
                {


                }
                j++;
            }

            //for (int i = 3, j = 0; i < e.Row.Cells.Count; i++)
            //{
            //    e.Row.Cells[i].Attributes.Add("align", "Right");
            //}

            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }
            #endregion
            //
            if (dtrowClr_view.Rows[indx][10].ToString() == "2")
            {
                e.Row.Attributes.Add("style", "  Color:#C71585; border-color:Black");
                //e.Row.Cells[0].Text = "";

                for (int l = 3, j = 0; l < e.Row.Cells.Count; l++)
                {
                    //e.Row.Cells[l].Text = "";
                    l += 1;
                }
            }
            if (indx >= 1)
            {
                if (dtrowClr_view.Rows[indx][0].ToString() == dtrowClr_view.Rows[indx - 1][0].ToString())
                {
                    e.Row.Cells[0].Text = "";
                }
            }
        }
    }
    #endregion

}