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

public partial class MIS_Reports_rptProduct_Priority_Wise : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    int iMonth = -1;
    int iYear = -1;
    DataSet dsDoctor = null;
    DataSet dsDoctr = null;
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
    string tot_fldwrk = string.Empty;
    string ChemistPOB_visit = string.Empty;

    string tot_Sub_days = string.Empty;
    string tot_dr = string.Empty;
    string Chemist_visit = string.Empty;
    string Stock_Visit = string.Empty;
    string tot_Stock_Calls_Seen = string.Empty;
    string tot_Dcr_Leave = string.Empty;
    string UnlistVisit = string.Empty;
    string tot_dcr_dr = string.Empty;
    string tot_doc_met = string.Empty;
    string tot_doc_calls_seen = string.Empty;
    string tot_doc_Unlstcalls_seen = string.Empty;
    string tot_CSH_calls_seen = string.Empty;
    string Monthsub = string.Empty;
    string strSf_Code = string.Empty;

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    List<string> iInp = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Prod = Request.QueryString["Prod"].ToString();
        Prod_Name = Request.QueryString["Prod_Name"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Product Prioritywise Analysis for the Period of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        if (!Page.IsPostBack)
        {
            string strQry = "";


            strQry = " select LstDr_Priority_Range from admin_setups " +
                     " where Division_Code='" + divcode + "' ";

            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            SalesForce sf = new SalesForce();
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                if ((dsDoctor.Tables[0].Rows[0]["LstDr_Priority_Range"].ToString() != "") && (dsDoctor.Tables[0].Rows[0]["LstDr_Priority_Range"].ToString() != ";nbsp"))
                    {
                    ViewState["priority"] = Convert.ToInt16(dsDoctor.Tables[0].Rows[0]["LstDr_Priority_Range"].ToString());
                }
            }
        }

        if (Prod == "-1")
        {
            lblprd_name.Visible = false;
            //FillAll_Product();
            FillAll_Product1();
        }
        else
        {
            lblname.Text = "<span style='color:#0077FF'> " + "" + Prod_Name + "" + "</span>";
            //FillSF();
            FillSF1();
        }

    }

    private void FillAll_Product1()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));

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

       

        DataTable dtspec = new DataTable();
        dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtspec.Columns["INX"].AutoIncrementSeed = 1;
        dtspec.Columns["INX"].AutoIncrementStep = 1;
        dtspec.Columns.Add("SAMPLECODE", typeof(int));

        for (int i = 1; i <= Convert.ToInt16(ViewState["priority"]); i++)
        {
            dtspec.Rows.Add(null, i);
        }


        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("AllProduct_Prioritywise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Strsample", dtspec);
        cmd.CommandTimeout = 300;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        if (dsts.Tables.Count > 0)
        {
            dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(3);
            dsts.Tables[0].Columns.RemoveAt(1);
            GrdPrdExp.DataSource = dsts;
            GrdPrdExp.DataBind();
        }
    }

    private void FillSF1()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth);
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);


        int iMn = 0; int iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));

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

        DataTable dtspec = new DataTable();
        dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtspec.Columns["INX"].AutoIncrementSeed = 1;
        dtspec.Columns["INX"].AutoIncrementStep = 1;
        dtspec.Columns.Add("SAMPLECODE", typeof(int));

        for (int i = 1; i <= Convert.ToInt16(ViewState["priority"]); i++)
        {
            dtspec.Rows.Add(null, i);
        }

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("Product_Single_Prioritywise", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Prod_Code", Prod);
        cmd.Parameters.AddWithValue("@Strsample", dtspec);
        cmd.CommandTimeout = 150;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        if (dsts.Tables.Count > 0)
        {
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            GrdPrdExp.DataSource = dsts;
            GrdPrdExp.DataBind();
        }


    }
    protected void GrdPrdExp_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (Prod == "-1")
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //
                #region Object
                //Creating a gridview object            
                GridView objGridView = (GridView)sender;

                //Creating a gridview row object
                GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell = new TableCell();
                GridViewRow objgridviewrow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell1 = new TableCell();


                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Product Name / Month", "#0097AC", true);
          

                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());

                AddMergedCells(objgridviewrow, objtablecell, 0, Convert.ToInt16(ViewState["priority"]), "Tagged Drs", "#0097AC", true);

                for (int j = 1; j <= Convert.ToInt16(ViewState["priority"]); j++)
                {
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "P" + j.ToString(), "#0097AC", true);
                    //iLstMonth.Add(cmonth);
                    //iLstYear.Add(cyear);
                    //iInp.Add(j.ToString());
                }



                //string strQry = "";


                //strQry = " select LstDr_Priority_Range from admin_setups " +
                //         " where Division_Code='" + divcode + "' ";

                //DB_EReporting db = new DB_EReporting();
                //dsDoctor = db.Exec_DataSet(strQry);
                SalesForce sf = new SalesForce();

                //ViewState["priority"] = Convert.ToInt16(dsDoctor.Tables[0].Rows[0]["LstDr_Priority_Range"].ToString());

                for (int i = 0; i <= months; i++)
                {
                 
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, Convert.ToInt16(ViewState["priority"]), sTxt, "#0097AC", true);

                    for (int j = 1; j <= Convert.ToInt16(ViewState["priority"]); j++)
                    {
                        AddMergedCells(objgridviewrow1, objtablecell1, 0, 0,"P" + j.ToString(), "#0097AC", true);
                        iLstMonth.Add(cmonth);
                        iLstYear.Add(cyear);
                        iInp.Add(j.ToString());
                    }
                    cmonth = cmonth + 1;

                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                //
                #endregion
                //
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //
                #region Object
                //Creating a gridview object            
                GridView objGridView = (GridView)sender;

                //Creating a gridview row object
                GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell = new TableCell();
                GridViewRow objgridviewrow1 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell1 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);

                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());




                AddMergedCells(objgridviewrow, objtablecell, 0, Convert.ToInt16(ViewState["priority"]), "Tagged Drs", "#0097AC", true);

                for (int j = 1; j <= Convert.ToInt16(ViewState["priority"]); j++)
                {
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "P" + j.ToString(), "#0097AC", true);
                    //iLstMonth.Add(cmonth);
                    //iLstYear.Add(cyear);
                    //iInp.Add(j.ToString());
                }

                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {

                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, Convert.ToInt16(ViewState["priority"]), sTxt, "#0097AC", true);
                    for (int j = 1; j <= Convert.ToInt16(ViewState["priority"]); j++)
                    {
                        AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "P" + j.ToString(), "#0097AC", true);
                        iLstMonth.Add(cmonth);
                        iLstYear.Add(cyear);
                        iInp.Add(j.ToString());
                    }
                    cmonth = cmonth + 1;

                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                //
                #endregion
                //
            }

          
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Style.Add("background-color", backcolor);
        //if (celltext == "Total")
        //{
        //    objtablecell.Style.Add("color", "red");
        //}
        //else
        //{
        //objtablecell.Style.Add("color", "white");
        //}
        //objtablecell.Style.Add("border-color", "black");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdPrdExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Prod == "-1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = Convert.ToInt16(ViewState["priority"]) + 2, j = 0, m = 0; i < e.Row.Cells.Count; i++, m++)
                {
                   
                    if (e.Row.Cells[i].Text != "-" && e.Row.Cells[i].Text != "&nbsp;")
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            string Prd_code = dtrowClr.Rows[iInx][1].ToString();
                            string Prd_Name = dtrowClr.Rows[iInx][2].ToString();
                            int cMnth = iLstMonth[m];
                            int cYr = iLstYear[m];
                            string Priority = iInp[m];

                            if (cMnth == 4)
                            {

                            }

                           
                 
                            //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "')");
                            hLink.Attributes.Add("href", "javascript:show('" + sfCode + "', '" + sfname + "', '" + cYr + "', '" + cMnth + "','" + Prd_Name + "','" + Prd_code + "', '" + sCurrentDate + "','"+Priority+"')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Controls.Add(hLink);
                            //int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                            //e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                            j++;
                        }
                    //}

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }
            }
        }

        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = Convert.ToInt16(ViewState["priority"]) + 4, j = 0, m = 0; i < e.Row.Cells.Count; i++, m++)
                {
                    //if (e.Row.Cells[i].Text != "0")
                    //{
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i].Text;
                    string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                    string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                    int cMnth = iLstMonth[m];
                    int cYr = iLstYear[m];
                    string Priority = iInp[m];

                    if (cMnth == 12)
                    {
                        sCurrentDate = "01-01-" + (cYr + 1).ToString();
                    }
                    else
                    {
                        sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                    }
                    if (dtrowClr.Rows[iInx][1].ToString() != "Total")
                    {
                        hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod_Name + "','" + Prod + "', '" + sCurrentDate + "','"+Priority+"')");
                    }
                    else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                    {
                        hLink.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod_Name + "','" + Prod + "', '" + sCurrentDate + "','"+Priority+"')");
                        Session["Sf_Code_multiple"] = sfCode;
                    }
                    hLink.ToolTip = "Click here";
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[i].Controls.Add(hLink);
                    int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                    j++;
                    //}

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }

                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                {
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Attributes.Add("align", "center");

                }
            }
        }
    }
    
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = pnlContents;
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
