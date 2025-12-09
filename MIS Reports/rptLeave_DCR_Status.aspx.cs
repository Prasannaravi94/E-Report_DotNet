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
using DBase_EReport;
using System.Data.SqlClient;


public partial class MIS_Reports_rptLeave_DCR_Status : System.Web.UI.Page
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
    string Detailed = string.Empty;
    DataSet dsSales = new DataSet();
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    int total = 0;
    int total2 = 0;
    int total3 = 0;
    int total4 = 0;
    int fulltotal = 0;
    string tot = string.Empty;
    string DesName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        Detailed = Request.QueryString["Detailed"].ToString();
        DesName = Request.QueryString["DesName"].ToString();
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Leave Status for the Month of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;

       //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
        if (Detailed == "1")
        {
            //FillSF_Leave_Type();
        }
        else
        {
           // FillSF();
        }
        FillSample_Prd();

    }

    private void FillSample_Prd()
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

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);

        if (Detailed == "1")
        {
            if (DesName == "---select---")
            {
                //Leave_Status_DCR_withtype
                SqlCommand cmd = new SqlCommand("Leave_Status_DCR_withtype_Desig", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@DesName", DesName);
                cmd.CommandTimeout = 150;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(7);
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(2);
                Grdprd.DataSource = dsts;
                Grdprd.DataBind();
            }
            else
            {
                //Leave_Status_DCR_withtype
                SqlCommand cmd = new SqlCommand("Leave_Status_DCR_withtype_Desig", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@DesName", DesName);
                cmd.CommandTimeout = 150;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(7);
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(2);
                Grdprd.DataSource = dsts;
                Grdprd.DataBind();
            }
        }
        else
        {
            if (DesName == "---select---")
            {
               // Leave_Status_DCR
                SqlCommand cmd = new SqlCommand("Leave_Status_DCR_withtype_Days", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@DesName", DesName);
                cmd.CommandTimeout = 150;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(7);
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(2);
                Grdprd.DataSource = dsts;
                Grdprd.DataBind();
            }
            else
            {
                SqlCommand cmd = new SqlCommand("Leave_Status_DCR_withtype_Days", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@DesName", DesName);
                cmd.CommandTimeout = 150;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(7);
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(2);
                Grdprd.DataSource = dsts;
                Grdprd.DataBind();
            }

          
        }

    }

    protected void Grdprd_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (Detailed == "1")
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
                GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell1 = new TableCell();
                GridViewRow objgridviewrow2 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell2 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);

                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
                //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //int cmonth = Convert.ToInt32(FMonth);
                //int cyear = Convert.ToInt32(FYear);

                //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                   


                    Doctor dr = new Doctor();
                    dsDoctor = dr.getDCR_Leave_Type(divcode);
                    AddMergedCells(objgridviewrow, objtablecell, 0, dsDoctor.Tables[0].Rows.Count, sTxt, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, dsDoctor.Tables[0].Rows.Count, "Leave Count", "#0097AC", true);

                    for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                    {
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[k]["Leave_SName"].ToString(), "#0097AC", true);
                    }
                    cmonth = cmonth + 1;

                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "CL Total", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "PL Total", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "SL Total", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "LOP Total", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total", "#0097AC", true);
                //
                objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                objGridView.Controls[0].Controls.AddAt(1, objgridviewrow1);
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
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
                GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell1 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Employee id", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
               

                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());
                //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                //int cmonth = Convert.ToInt32(FMonth);
                //int cyear = Convert.ToInt32(FYear);

                //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {
                    iLstMonth.Add(cmonth);
                    iLstYear.Add(cyear);
                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, 0, sTxt, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, 0, "Leave Count", "#0097AC", true);
              
                    cmonth = cmonth + 1;

                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                   
                }
                AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Total", "#0097AC", true);
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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
        objtablecell.Wrap = false;
       
        
    }

    protected void Grdprd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (Detailed == "1")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = 5, m = 0, n = 0; i < e.Row.Cells.Count; i++,n++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                        int cMnth = iLstMonth[m];
                        int cYr = iLstYear[m];

                      
                        if (cMnth == 12)
                        {
                            sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        }
                        else
                        {
                            sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        }
                        //hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                        //SalesForce sf = new SalesForce();
                        //dsLeave = sf.getDCR_Leave_Count_ToolTip(sSf_code, divcode, cMnth, cYr);

                        //if (dsLeave.Tables[0].Rows.Count > 0)
                        //    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        //hLink.ToolTip = Days;
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        tot = hLink.Text;
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        if (DesName == "---select---")
                        {
                            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][6].ToString()));
                        }
                        // m++;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            fulltotal += Convert.ToInt16(tot);

                            if (n % 4 == 0)
                            {
                                total += Convert.ToInt16(tot);
                            }
                            else if(n % 4 ==1)
                            {
                                total2 += Convert.ToInt16(tot);
                            }
                            else if (n % 4 == 2)
                            {
                                total3 += Convert.ToInt16(tot);
                            }
                            else if (n % 4 == 3)
                            {
                                total4 += Convert.ToInt16(tot);
                            }
                        }
                    }

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }

                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";

                TableCell tblRow_Count = new TableCell();

                tblRow_Count.Text = total.ToString();
                if (tblRow_Count.Text == "0")
                {
                    tblRow_Count.Text = "";
                }

                TableCell tblRow_Count2 = new TableCell();
                tblRow_Count2.Text = total2.ToString();
                if (tblRow_Count2.Text == "0")
                {
                    tblRow_Count2.Text = "";
                }

                TableCell tblRow_Count3 = new TableCell();
                tblRow_Count3.Text = total3.ToString();

                if (tblRow_Count3.Text == "0")
                {
                    tblRow_Count3.Text = "";
                }

                TableCell tblRow_Count4 = new TableCell();
                tblRow_Count4.Text = total4.ToString();

                if (tblRow_Count4.Text == "0")
                {
                    tblRow_Count4.Text = "";
                }

                TableCell tblRow_Count5 = new TableCell();
                tblRow_Count5.Text = fulltotal.ToString();

                if (tblRow_Count5.Text == "0")
                {
                    tblRow_Count5.Text = "";
                }


                e.Row.Cells.Add(tblRow_Count);
                e.Row.Cells.Add(tblRow_Count2);
                e.Row.Cells.Add(tblRow_Count3);
                e.Row.Cells.Add(tblRow_Count4);
                e.Row.Cells.Add(tblRow_Count5);

                total = 0;
                total2 = 0;
                total3 = 0;
                total4 = 0;
                fulltotal = 0;

                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
                e.Row.Cells[3].Wrap = false;
                e.Row.Cells[4].Wrap = false;
            }
        }
        else 
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int iInx = e.Row.RowIndex;
                for (int i = 5, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;

                       

                        //string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sSf_code = dtrowClr.Rows[iInx][2].ToString();
                        int cMnth = iLstMonth[j];
                        int cYr = iLstYear[j];

                        if (cMnth == 12)
                        {
                            sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        }
                        else
                        {
                            sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        }


                        //hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");

                        SalesForce sf = new SalesForce();
                        string sTxt = sf.getMonthName(cMnth.ToString()) + "-" + cYr;
                        hLink.ToolTip = sTxt;
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        tot = hLink.Text;
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;

                        if (DesName == "---select---")
                        {
                            e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][6].ToString()));
                        }
                        j++;
                        if (tot != "-" && tot != "&nbsp;")
                        {
                            total += Convert.ToInt16(tot);
                        }

                        //SalesForce sa = new SalesForce();
                        //dsLeave = sa.getDCR_Leave_Count_ToolTip(sSf_code, divcode, cMnth, cYr);

                        //if (dsLeave.Tables[0].Rows.Count > 0)
                        //    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        //hLink.Text = hLink.Text + " ( " + Days + " ) ";
                        
                    }
                

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "left");

                }
                e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this)";
                e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                TableCell tblRow_Count = new TableCell();
    
                tblRow_Count.Text = total.ToString();
                if (tblRow_Count.Text =="0")
                {
                    tblRow_Count.Text = "";
                }
                e.Row.Cells.Add(tblRow_Count);

                total = 0;

                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
                e.Row.Cells[3].Wrap = false;
                e.Row.Cells[4].Wrap = false;
                
            }
        }
    }


    private void FillSF_Leave_Type()
    {
        string sURL = string.Empty;
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);
        dsSalesForce = sf.ViewDetails_Feedback_new(divcode, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 3;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "#";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 3;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Emp = new TableCell();
            tc_DR_Emp.BorderStyle = BorderStyle.Solid;
            tc_DR_Emp.BorderWidth = 1;
            tc_DR_Emp.Width = 50;
            tc_DR_Emp.RowSpan = 3;
            Literal lit_DR_Emp = new Literal();
            lit_DR_Emp.Text = "<center>Employee Id</center>";
            tc_DR_Emp.BorderColor = System.Drawing.Color.Black;
            tc_DR_Emp.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Emp.Controls.Add(lit_DR_Emp);
            tr_header.Cells.Add(tc_DR_Emp);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 3;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 3;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 3;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            Doctor dr = new Doctor();
            dsDoctor = dr.getDCR_Leave_Type(divcode);

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
                    //tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Leave Count(Days)";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

            if (months >= 0)
            {
                TableRow tr_catg = new TableRow();
                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_catg.Style.Add("Color", "White");
                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

                for (int j = 1; j <= (months + 1) * 1; j++)
                {
                    if (dsDoctor.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                        {
                            TableCell tc_catg_name = new TableCell();
                            tc_catg_name.BorderStyle = BorderStyle.Solid;
                            tc_catg_name.BorderWidth = 1;
                            if ((j % 2) == 1)
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
                            }
                            else
                            {
                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
                            }
                            // tc_catg_name.Width = 30;

                            Literal lit_catg_name = new Literal();
                            lit_catg_name.Text = dataRow["Leave_SName"].ToString();
                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
                            tc_catg_name.Controls.Add(lit_catg_name);
                            tr_catg.Cells.Add(tc_catg_name);
                        }

                        tbl.Rows.Add(tr_catg);
                    }
                }
            }

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;

            //Random rndm = new Random();
            //int t = rndm.Next(1, 28);
            //int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
            //string from_date = t + "-" + mnth.ToString() + "-" + year.ToString();


            //Random ran = new Random();
            //int da = ran.Next(1, 28);
            //int mnths = Convert.ToInt32(TMonth), year1 = Convert.ToInt32(TYear);

            //string to_date = da + "-" + mnths.ToString() + "-" + year1.ToString();

            //dsSales = sf.getLeavesf_code(divcode, Convert.ToDateTime(from_date), Convert.ToDateTime(to_date));


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {

                if (DesName != "---select---")
                {

                    if (drFF["Designation_Short_Name"].ToString() == DesName)
                    {
                        TableRow tr_det = new TableRow();
                        iCount += 1;
                        strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                        //S.No
                        TableCell tc_det_SNo = new TableCell();
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = iCount.ToString();
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det.Cells.Add(tc_det_SNo);
                        tr_det.BackColor = System.Drawing.Color.White;

                        //Emp
                        TableCell tc_det_Emp = new TableCell();
                        Literal lit_det_Emp = new Literal();
                        lit_det_Emp.Text = drFF["sf_emp_id"].ToString();
                        tc_det_Emp.BorderStyle = BorderStyle.Solid;
                        tc_det_Emp.BorderWidth = 1;
                        tc_det_Emp.Attributes.Add("Class", "rptCellBorder");
                        tc_det_Emp.Controls.Add(lit_det_Emp);
                        tr_det.Cells.Add(tc_det_Emp);

                        //SF_code
                        TableCell tc_det_usr = new TableCell();
                        Literal lit_det_usr = new Literal();
                        lit_det_usr.Text =  drFF["Sf_Code"].ToString();
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        tc_det_usr.Visible = false;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(lit_det_usr);
                        tr_det.Cells.Add(tc_det_usr);

                        //SF Name
                        TableCell tc_det_FF = new TableCell();
                        Literal lit_det_FF = new Literal();
                        lit_det_FF.Text = drFF["sf_name"].ToString();
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tr_det.Cells.Add(tc_det_FF);

                        //hq
                        TableCell tc_det_hq = new TableCell();
                        Literal lit_det_hq = new Literal();
                        lit_det_hq.Text =  drFF["sf_hq"].ToString();
                        tc_det_hq.BorderStyle = BorderStyle.Solid;
                        tc_det_hq.BorderWidth = 1;
                        tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                        tc_det_hq.Controls.Add(lit_det_hq);
                        tr_det.Cells.Add(tc_det_hq);

                        //SF Designation Short Name
                        TableCell tc_det_Designation = new TableCell();
                        Literal lit_det_Designation = new Literal();
                        lit_det_Designation.Text =  drFF["Designation_Short_Name"].ToString();
                        tc_det_Designation.BorderStyle = BorderStyle.Solid;
                        tc_det_Designation.BorderWidth = 1;
                        tc_det_Designation.Controls.Add(lit_det_Designation);
                        tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_det_Designation);

                        months = Convert.ToInt16(ViewState["months"].ToString());
                        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                        cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                        if (months >= 0)
                        {

                            for (int j = 1; j <= months + 1; j++)
                            {

                                if (cmonth == 12)
                                {
                                    sCurrentDate = "01-01-" + (cyear + 1);
                                }
                                else
                                {
                                    sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                                }

                                dtCurrent = Convert.ToDateTime(sCurrentDate);

                                if (dsDoctor.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                                    {
                                        dsDoc = sf.getDCR_Leave_Count_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));


                                        if (dsDoc.Tables[0].Rows.Count > 0)
                                            tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        dsLeave = sf.getDCR_Leave_Count_ToolTip_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));

                                        if (dsLeave.Tables[0].Rows.Count > 0)
                                            Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                        TableCell tc_lst_month = new TableCell();
                                        HyperLink hyp_lst_month = new HyperLink();

                                        if (tot_dr != "0")
                                        {
                                            //iTotLstCount += Convert.ToInt16(tot_dr);
                                            hyp_lst_month.Text = tot_dr + " ( " + Days+ " )";
                                            hyp_lst_month.ToolTip = Days;

                                            //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                                            //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                                            ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                            //hyp_lst_month.NavigateUrl = "#";

                                        }

                                        else
                                        {
                                            hyp_lst_month.Text = "";
                                        }


                                        tc_lst_month.BorderStyle = BorderStyle.Solid;
                                        tc_lst_month.BorderWidth = 1;
                                        tc_lst_month.BackColor = System.Drawing.Color.White;
                                        tc_lst_month.Width = 100;
                                        tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                        tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                        tc_lst_month.Controls.Add(hyp_lst_month);
                                        tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                                        tr_det.Cells.Add(tc_lst_month);

                                        tot_dr = "";
                                        Days = "";
                                    }

                                }
                                cmonth = cmonth + 1;
                                if (cmonth == 13)
                                {
                                    cmonth = 1;
                                    cyear = cyear + 1;
                                }

                            }
                            //

                        }

                        tbl.Rows.Add(tr_det);
                    }
                }



                //foreach (DataRow drf in dsSales.Tables[0].Rows)
                //{

                //    if (drFF["sf_code"].ToString() == drf["sf_code"].ToString())
                //    {
                else
                {
                    TableRow tr_det = new TableRow();
                    iCount += 1;
                    strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = iCount.ToString();
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White;

                    //Emp
                    TableCell tc_det_Emp = new TableCell();
                    Literal lit_det_Emp = new Literal();
                    lit_det_Emp.Text = drFF["sf_emp_id"].ToString();
                    tc_det_Emp.BorderStyle = BorderStyle.Solid;
                    tc_det_Emp.BorderWidth = 1;
                    tc_det_Emp.Attributes.Add("Class", "rptCellBorder");
                    tc_det_Emp.Controls.Add(lit_det_Emp);
                    tr_det.Cells.Add(tc_det_Emp);

                    //SF_code
                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = drFF["Sf_Code"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Visible = false;
                    tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);

                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text =  drFF["sf_name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    //hq
                    TableCell tc_det_hq = new TableCell();
                    Literal lit_det_hq = new Literal();
                    lit_det_hq.Text =  drFF["sf_hq"].ToString();
                    tc_det_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_hq.BorderWidth = 1;
                    tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                    tc_det_hq.Controls.Add(lit_det_hq);
                    tr_det.Cells.Add(tc_det_hq);

                    //SF Designation Short Name
                    TableCell tc_det_Designation = new TableCell();
                    Literal lit_det_Designation = new Literal();
                    lit_det_Designation.Text =  drFF["Designation_Short_Name"].ToString();
                    tc_det_Designation.BorderStyle = BorderStyle.Solid;
                    tc_det_Designation.BorderWidth = 1;
                    tc_det_Designation.Controls.Add(lit_det_Designation);
                    tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_det_Designation);

                    months = Convert.ToInt16(ViewState["months"].ToString());
                    cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                    cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                    if (months >= 0)
                    {

                        for (int j = 1; j <= months + 1; j++)
                        {

                            if (cmonth == 12)
                            {
                                sCurrentDate = "01-01-" + (cyear + 1);
                            }
                            else
                            {
                                sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                            }

                            dtCurrent = Convert.ToDateTime(sCurrentDate);

                            if (dsDoctor.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
                                {
                                    dsDoc = sf.getDCR_Leave_Count_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));


                                    if (dsDoc.Tables[0].Rows.Count > 0)
                                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                    dsLeave = sf.getDCR_Leave_Count_ToolTip_Type(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt32(dataRow["Leave_code"].ToString()));

                                    if (dsLeave.Tables[0].Rows.Count > 0)
                                        Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                    TableCell tc_lst_month = new TableCell();
                                    HyperLink hyp_lst_month = new HyperLink();

                                    if (tot_dr != "0")
                                    {
                                        //iTotLstCount += Convert.ToInt16(tot_dr);
                                        hyp_lst_month.Text = tot_dr + " ( " + Days + " )";
                                        hyp_lst_month.ToolTip = Days;

                                        //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                                        //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                                        ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                        //hyp_lst_month.NavigateUrl = "#";

                                    }

                                    else
                                    {
                                        hyp_lst_month.Text = "";
                                    }


                                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                                    tc_lst_month.BorderWidth = 1;
                                    tc_lst_month.BackColor = System.Drawing.Color.White;
                                    tc_lst_month.Width = 100;
                                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                    tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                    tc_lst_month.Controls.Add(hyp_lst_month);
                                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                                    tr_det.Cells.Add(tc_lst_month);

                                    tot_dr = "";
                                    Days = "";
                                }

                            }
                            cmonth = cmonth + 1;
                            if (cmonth == 13)
                            {
                                cmonth = 1;
                                cyear = cyear + 1;
                            }

                        }
                        //

                    }

                    tbl.Rows.Add(tr_det);
                }
                }


                //    }
                //}           
        }
    }
    private void FillSF()
    {
        string sURL = string.Empty;
        tbl.Rows.Clear();

        SalesForce sf = new SalesForce();
       // dsSalesForce = sf.UserList_get_SelfMail(divcode, sfCode);
        dsSalesForce = sf.ViewDetails_Feedback_new(divcode, sfCode);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            TableRow tr_header = new TableRow();
            tr_header.BorderStyle = BorderStyle.Solid;
            tr_header.BorderWidth = 1;
            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
            tr_header.Style.Add("Color", "White");
            tr_header.BorderColor = System.Drawing.Color.Black;

            TableCell tc_SNo = new TableCell();
            tc_SNo.BorderStyle = BorderStyle.Solid;
            tc_SNo.BorderWidth = 1;
            tc_SNo.Width = 50;
            tc_SNo.RowSpan = 2;
            Literal lit_SNo =
                new Literal();
            lit_SNo.Text = "#";
            tc_SNo.BorderColor = System.Drawing.Color.Black;
            tc_SNo.Controls.Add(lit_SNo);
            tc_SNo.Attributes.Add("Class", "rptCellBorder");
            tr_header.Cells.Add(tc_SNo);

            TableCell tc_DR_Code = new TableCell();
            tc_DR_Code.BorderStyle = BorderStyle.Solid;
            tc_DR_Code.BorderWidth = 1;
            tc_DR_Code.Width = 400;
            tc_DR_Code.RowSpan = 2;
            Literal lit_DR_Code = new Literal();
            lit_DR_Code.Text = "<center>SF Code</center>";
            tc_DR_Code.Controls.Add(lit_DR_Code);
            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
            tc_DR_Code.Visible = false;
            tr_header.Cells.Add(tc_DR_Code);

            TableCell tc_DR_Emp = new TableCell();
            tc_DR_Emp.BorderStyle = BorderStyle.Solid;
            tc_DR_Emp.BorderWidth = 1;
            tc_DR_Emp.Width = 50;
            tc_DR_Emp.RowSpan = 2;
            Literal lit_DR_Emp = new Literal();
            lit_DR_Emp.Text = "<center>Employee Id</center>";
            tc_DR_Emp.BorderColor = System.Drawing.Color.Black;
            tc_DR_Emp.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Emp.Controls.Add(lit_DR_Emp);
            tr_header.Cells.Add(tc_DR_Emp);

            TableCell tc_DR_Name = new TableCell();
            tc_DR_Name.BorderStyle = BorderStyle.Solid;
            tc_DR_Name.BorderWidth = 1;
            tc_DR_Name.Width = 200;
            tc_DR_Name.RowSpan = 2;
            Literal lit_DR_Name = new Literal();
            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Name.Controls.Add(lit_DR_Name);
            tr_header.Cells.Add(tc_DR_Name);

            TableCell tc_DR_HQ = new TableCell();
            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
            tc_DR_HQ.BorderWidth = 1;
            tc_DR_HQ.Width = 100;
            tc_DR_HQ.RowSpan = 2;
            Literal lit_DR_HQ = new Literal();
            lit_DR_HQ.Text = "<center>HQ</center>";
            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
            tc_DR_HQ.Controls.Add(lit_DR_HQ);
            tr_header.Cells.Add(tc_DR_HQ);

            TableCell tc_DR_Des = new TableCell();
            tc_DR_Des.BorderStyle = BorderStyle.Solid;
            tc_DR_Des.BorderWidth = 1;
            tc_DR_Des.Width = 80;
            tc_DR_Des.RowSpan = 2;
            Literal lit_DR_Des = new Literal();
            lit_DR_Des.Text = "<center>Designation</center>";
            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
            tc_DR_Des.Controls.Add(lit_DR_Des);
            tr_header.Cells.Add(tc_DR_Des);

            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(FMonth);
            int cyear = Convert.ToInt32(FYear);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;

            if (months >= 0)
            {
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_month = new TableCell();
                    tc_month.ColumnSpan = 1;
                    Literal lit_month = new Literal();
                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
                    tc_month.Attributes.Add("Class", "rptCellBorder");
                    tc_month.BorderStyle = BorderStyle.Solid;
                    tc_month.BorderWidth = 1;
                    tc_month.HorizontalAlign = HorizontalAlign.Center;
                    //tc_month.Width = 200;
                    tc_month.Controls.Add(lit_month);
                    tr_header.Cells.Add(tc_month);
                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
            }

            tbl.Rows.Add(tr_header);

            //Sub Header
            months = Convert.ToInt16(ViewState["months"].ToString());
            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

            if (months >= 0)
            {
                TableRow tr_lst_det = new TableRow();
                for (int j = 1; j <= months + 1; j++)
                {
                    TableCell tc_lst_month = new TableCell();
                    HyperLink lit_lst_month = new HyperLink();
                    lit_lst_month.Text = "Leave Count(Days)";
                    tc_lst_month.BorderStyle = BorderStyle.Solid;
                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                    tc_lst_month.BorderWidth = 1;
                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                    tc_lst_month.Controls.Add(lit_lst_month);
                    tr_lst_det.Cells.Add(tc_lst_month);


                    cmonth = cmonth + 1;
                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }
                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
                tr_lst_det.Style.Add("Color", "White");

                tr_lst_det.Attributes.Add("Class", "Backcolor");

                tbl.Rows.Add(tr_lst_det);
            }

            if (dsSalesForce.Tables[0].Rows.Count > 0)
                ViewState["dsSalesForce"] = dsSalesForce;


            //Random rndm = new Random();
            //int t = rndm.Next(1, 28);
            //int mnth = Convert.ToInt32(FMonth), year = Convert.ToInt32(FYear);
            //string from_date = t + "-" + mnth.ToString() + "-" + year.ToString();


            //Random ran = new Random();
            //int da = ran.Next(1, 28);
            //int mnths = Convert.ToInt32(TMonth), year1 = Convert.ToInt32(TYear);

            //string to_date = da + "-" + mnths.ToString() + "-" + year1.ToString();

            //dsSales = sf.getLeavesf_code(divcode, Convert.ToDateTime(from_date), Convert.ToDateTime(to_date));


            int iCount = 0;
            int iTotLstCount = 0;
            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
            {

                if (DesName != "---select---")
                {
                  
                    if (drFF["Designation_Short_Name"].ToString() == DesName)
                    {
                        TableRow tr_det = new TableRow();
                        iCount += 1;
                        strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                        //S.No
                        TableCell tc_det_SNo = new TableCell();
                        Literal lit_det_SNo = new Literal();
                        lit_det_SNo.Text = iCount.ToString();
                        tc_det_SNo.BorderStyle = BorderStyle.Solid;
                        tc_det_SNo.BorderWidth = 1;
                        tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                        tc_det_SNo.Controls.Add(lit_det_SNo);
                        tr_det.Cells.Add(tc_det_SNo);
                        tr_det.BackColor = System.Drawing.Color.White;

                        //Emp
                        TableCell tc_det_Emp = new TableCell();
                        Literal lit_det_Emp = new Literal();
                        lit_det_Emp.Text = drFF["sf_emp_id"].ToString();
                        tc_det_Emp.BorderStyle = BorderStyle.Solid;
                        tc_det_Emp.BorderWidth = 1;
                        tc_det_Emp.Attributes.Add("Class", "rptCellBorder");
                        tc_det_Emp.Controls.Add(lit_det_Emp);
                        tr_det.Cells.Add(tc_det_Emp);

                        //SF_code
                        TableCell tc_det_usr = new TableCell();
                        Literal lit_det_usr = new Literal();
                        lit_det_usr.Text =  drFF["Sf_Code"].ToString();
                        tc_det_usr.BorderStyle = BorderStyle.Solid;
                        tc_det_usr.BorderWidth = 1;
                        tc_det_usr.Visible = false;
                        tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                        tc_det_usr.Controls.Add(lit_det_usr);
                        tr_det.Cells.Add(tc_det_usr);

                        //SF Name
                        TableCell tc_det_FF = new TableCell();
                        Literal lit_det_FF = new Literal();
                        lit_det_FF.Text =  drFF["sf_name"].ToString();
                        tc_det_FF.BorderStyle = BorderStyle.Solid;
                        tc_det_FF.BorderWidth = 1;
                        tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                        tc_det_FF.Controls.Add(lit_det_FF);
                        tr_det.Cells.Add(tc_det_FF);

                        //hq
                        TableCell tc_det_hq = new TableCell();
                        Literal lit_det_hq = new Literal();
                        lit_det_hq.Text = drFF["sf_hq"].ToString();
                        tc_det_hq.BorderStyle = BorderStyle.Solid;
                        tc_det_hq.BorderWidth = 1;
                        tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                        tc_det_hq.Controls.Add(lit_det_hq);
                        tr_det.Cells.Add(tc_det_hq);

                        //SF Designation Short Name
                        TableCell tc_det_Designation = new TableCell();
                        Literal lit_det_Designation = new Literal();
                        lit_det_Designation.Text = drFF["Designation_Short_Name"].ToString();
                        tc_det_Designation.BorderStyle = BorderStyle.Solid;
                        tc_det_Designation.BorderWidth = 1;
                        tc_det_Designation.Controls.Add(lit_det_Designation);
                        tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                        tr_det.Cells.Add(tc_det_Designation);



                        months = Convert.ToInt16(ViewState["months"].ToString());
                        cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                        cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                        if (months >= 0)
                        {

                            for (int j = 1; j <= months + 1; j++)
                            {

                                if (cmonth == 12)
                                {
                                    sCurrentDate = "01-01-" + (cyear + 1);
                                }
                                else
                                {
                                    sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                                }

                                dtCurrent = Convert.ToDateTime(sCurrentDate);


                                dsDoc = sf.getDCR_Leave_Count(drFF["sf_code"].ToString(), divcode, cmonth, cyear);


                                if (dsDoc.Tables[0].Rows.Count > 0)
                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                dsLeave = sf.getDCR_Leave_Count_ToolTip(drFF["sf_code"].ToString(), divcode, cmonth, cyear);

                                if (dsLeave.Tables[0].Rows.Count > 0)
                                    Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                                TableCell tc_lst_month = new TableCell();
                                HyperLink hyp_lst_month = new HyperLink();

                                if (tot_dr != "0")
                                {
                                    //iTotLstCount += Convert.ToInt16(tot_dr);
                                    hyp_lst_month.Text = tot_dr + " ( " + Days + " ) ";
                                    hyp_lst_month.ToolTip = Days;

                                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                                    //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                                    ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                    //hyp_lst_month.NavigateUrl = "#";

                                }

                                else
                                {
                                    hyp_lst_month.Text = "";
                                }


                                tc_lst_month.BorderStyle = BorderStyle.Solid;
                                tc_lst_month.BorderWidth = 1;
                                tc_lst_month.BackColor = System.Drawing.Color.White;
                                tc_lst_month.Width = 100;
                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                                tc_lst_month.Controls.Add(hyp_lst_month);
                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                                tr_det.Cells.Add(tc_lst_month);

                                cmonth = cmonth + 1;
                                if (cmonth == 13)
                                {
                                    cmonth = 1;
                                    cyear = cyear + 1;
                                }
                                tot_dr = "";
                                Days = "";
                            }
                            //

                        }

                        tbl.Rows.Add(tr_det);



                        //    }
                        //}
                    }
                }

                else
                {

                    //foreach (DataRow drf in dsSales.Tables[0].Rows)
                    //{

                    //    if (drFF["sf_code"].ToString() == drf["sf_code"].ToString())
                    //    {
                    TableRow tr_det = new TableRow();
                    iCount += 1;
                    strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


                    //S.No
                    TableCell tc_det_SNo = new TableCell();
                    Literal lit_det_SNo = new Literal();
                    lit_det_SNo.Text = iCount.ToString();
                    tc_det_SNo.BorderStyle = BorderStyle.Solid;
                    tc_det_SNo.BorderWidth = 1;
                    tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
                    tc_det_SNo.Controls.Add(lit_det_SNo);
                    tr_det.Cells.Add(tc_det_SNo);
                    tr_det.BackColor = System.Drawing.Color.White;

                    //Emp
                    TableCell tc_det_Emp = new TableCell();
                    Literal lit_det_Emp = new Literal();
                    lit_det_Emp.Text =  drFF["sf_emp_id"].ToString();
                    tc_det_Emp.BorderStyle = BorderStyle.Solid;
                    tc_det_Emp.BorderWidth = 1;
                    tc_det_Emp.Attributes.Add("Class", "rptCellBorder");
                    tc_det_Emp.Controls.Add(lit_det_Emp);
                    tr_det.Cells.Add(tc_det_Emp);

                    //SF_code
                    TableCell tc_det_usr = new TableCell();
                    Literal lit_det_usr = new Literal();
                    lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
                    tc_det_usr.BorderStyle = BorderStyle.Solid;
                    tc_det_usr.BorderWidth = 1;
                    tc_det_usr.Visible = false;
                    tc_det_usr.Attributes.Add("Class", "rptCellBorder");
                    tc_det_usr.Controls.Add(lit_det_usr);
                    tr_det.Cells.Add(tc_det_usr);

                    //SF Name
                    TableCell tc_det_FF = new TableCell();
                    Literal lit_det_FF = new Literal();
                    lit_det_FF.Text = drFF["sf_name"].ToString();
                    tc_det_FF.BorderStyle = BorderStyle.Solid;
                    tc_det_FF.BorderWidth = 1;
                    tc_det_FF.Attributes.Add("Class", "rptCellBorder");
                    tc_det_FF.Controls.Add(lit_det_FF);
                    tr_det.Cells.Add(tc_det_FF);

                    //hq
                    TableCell tc_det_hq = new TableCell();
                    Literal lit_det_hq = new Literal();
                    lit_det_hq.Text =  drFF["sf_hq"].ToString();
                    tc_det_hq.BorderStyle = BorderStyle.Solid;
                    tc_det_hq.BorderWidth = 1;
                    tc_det_hq.Attributes.Add("Class", "rptCellBorder");
                    tc_det_hq.Controls.Add(lit_det_hq);
                    tr_det.Cells.Add(tc_det_hq);

                    //SF Designation Short Name
                    TableCell tc_det_Designation = new TableCell();
                    Literal lit_det_Designation = new Literal();
                    lit_det_Designation.Text =  drFF["Designation_Short_Name"].ToString();
                    tc_det_Designation.BorderStyle = BorderStyle.Solid;
                    tc_det_Designation.BorderWidth = 1;
                    tc_det_Designation.Controls.Add(lit_det_Designation);
                    tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
                    tr_det.Cells.Add(tc_det_Designation);



                    months = Convert.ToInt16(ViewState["months"].ToString());
                    cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
                    cyear = Convert.ToInt16(ViewState["cyear"].ToString());


                    if (months >= 0)
                    {

                        for (int j = 1; j <= months + 1; j++)
                        {

                            if (cmonth == 12)
                            {
                                sCurrentDate = "01-01-" + (cyear + 1);
                            }
                            else
                            {
                                sCurrentDate = (cmonth + 1) + "-01-" + cyear;
                            }

                            dtCurrent = Convert.ToDateTime(sCurrentDate);


                            dsDoc = sf.getDCR_Leave_Count(drFF["sf_code"].ToString(), divcode, cmonth, cyear);


                            if (dsDoc.Tables[0].Rows.Count > 0)
                                tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                            dsLeave = sf.getDCR_Leave_Count_ToolTip(drFF["sf_code"].ToString(), divcode, cmonth, cyear);

                            if (dsLeave.Tables[0].Rows.Count > 0)
                                Days = dsLeave.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


                            TableCell tc_lst_month = new TableCell();
                            HyperLink hyp_lst_month = new HyperLink();

                            if (tot_dr != "0")
                            {
                                //iTotLstCount += Convert.ToInt16(tot_dr);
                                hyp_lst_month.Text = tot_dr + " ( " + Days + " ) ";
                                hyp_lst_month.ToolTip = Days;

                                //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

                                //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "')");
                                ////hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
                                //hyp_lst_month.NavigateUrl = "#";

                            }

                            else
                            {
                                hyp_lst_month.Text = "";
                            }


                            tc_lst_month.BorderStyle = BorderStyle.Solid;
                            tc_lst_month.BorderWidth = 1;
                            tc_lst_month.BackColor = System.Drawing.Color.White;
                            tc_lst_month.Width = 100;
                            tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
                            tc_lst_month.VerticalAlign = VerticalAlign.Middle;
                            tc_lst_month.Controls.Add(hyp_lst_month);
                            tc_lst_month.Attributes.Add("Class", "rptCellBorder");
                            tr_det.Cells.Add(tc_lst_month);

                            cmonth = cmonth + 1;
                            if (cmonth == 13)
                            {
                                cmonth = 1;
                                cyear = cyear + 1;
                            }
                            tot_dr = "";
                            Days = "";
                        }
                        //

                    }

                    tbl.Rows.Add(tr_det);



                    //    }
                    //}
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected void chkdatewise_CheckedChanged(object sender, EventArgs e)
    {
        if (chkdatewise.Checked)
        {
            if (Detailed == "1")
            {
                FillSF_Leave_Type();
                Grdprd.Visible = false;
            }
            else
            {
                FillSF();
                Grdprd.Visible = false;
            }
        }
        else
        {
            Grdprd.Visible = true;
            FillSample_Prd();
        }
    }
}