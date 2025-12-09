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

public partial class MIS_Reports_rptTP_Deviation_At_Glance : System.Web.UI.Page
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
    DataSet dsSales = new DataSet();
    DataSet dsmgrsf = new DataSet();
    DataSet dsDoc = null;
    DataSet dsLeave = null;
    string Monthsub = string.Empty;
    string tot_dr = string.Empty;
    string Days = string.Empty;
    string strSf_Code = string.Empty;
    string sCurrentDate = string.Empty;
    string strDayName = string.Empty;
    int count = 0;
    string type = string.Empty;
    int iIndex;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();


    protected void Page_Load(object sender, EventArgs e)
    {

        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "TP Deviation for the Period of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;
        //FillSF();   
        //BindGrid(); 

        //lblIDMonth.Visible = false;
        //lblIDYear.Visible = false;


        type = sfCode;

        //if (type.Contains("MR"))
        //{
        //    type = "MR";
        //}
        //else if (type.Contains("MGR"))
        //{
        //    type = "MGR";
        //}

        // FillDeviation();
        FillAll_Product1();
    }

    private void FillAll_Product1()
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

        SqlCommand cmd = new SqlCommand("Input_Sample_Gift_Detail", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
        cmd.Parameters.AddWithValue("@Msf_code", sfCode);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(6);
        dsts.Tables[0].Columns.RemoveAt(5);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdPrdExp.DataSource = dsts;
        GrdPrdExp.DataBind();
    }

    private void FillDeviation()
    {

        SalesForce sal = new SalesForce();
        dsSalesForce = sal.getTP_Deviation_At_glance(divcode, sfCode, Convert.ToInt16(FMonth), Convert.ToInt16(FYear), type);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            //grdTP.Visible = true;
            //grdTP.DataSource = dsSalesForce;
            //grdTP.DataBind();
        }
        else
        {
            //grdTP.DataSource = dsSalesForce;
            //grdTP.DataBind();
        }


    }

    //private void BindGrid()
    //{
    //    int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //    int cmonth = Convert.ToInt32(FMonth);
    //    int cyear = Convert.ToInt32(FYear);


    //    int iMn = 0, iYr = 0;
    //    DataTable dtMnYr = new DataTable();
    //    dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
    //    dtMnYr.Columns["INX"].AutoIncrementStep = 1;
    //    dtMnYr.Columns.Add("MNTH", typeof(int));
    //    dtMnYr.Columns.Add("YR", typeof(int));
    //    //
    //    while (months >= 0)
    //    {
    //        if (cmonth == 13)
    //        {
    //            cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //        }
    //        else
    //        {
    //            iMn = cmonth; iYr = cyear;
    //        }
    //        dtMnYr.Rows.Add(null, iMn, iYr);
    //        months--; cmonth++;
    //    }
    //    //
    //    SalesForce sf = new SalesForce();
    //    DCR dcc = new DCR();
    //    DB_EReporting db = new DB_EReporting();
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);

    //    SqlCommand cmd = new SqlCommand("TP_Deviation_New", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
    //    cmd.Parameters.AddWithValue("@Msf_code", sfCode);
    //    cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet dsts = new DataSet();
    //    da.Fill(dsts);
    //    dtrowClr = dsts.Tables[0].Copy();
    //    dsts.Tables[0].Columns.RemoveAt(6);
    //    dsts.Tables[0].Columns.RemoveAt(5);
    //    dsts.Tables[0].Columns.RemoveAt(1);
    //    GrdInput.DataSource = dsts;
    //    GrdInput.DataBind();
    //}

    protected void GrdPrdExp_RowCreated(object sender, GridViewRowEventArgs e)
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
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "HQ", "#0097AC", true);

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
                AddMergedCells(objgridviewrow, objtablecell, 1, 0, sTxt, "#0097AC", true);
                cmonth = cmonth + 1;

                if (cmonth == 13)
                {
                    cmonth = 1;
                    cyear = cyear + 1;
                }
            }
            //
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            //
            #endregion
            //
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
    }
    protected void GrdPrdExp_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int iInx = e.Row.RowIndex;

            for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
            {
                //if (e.Row.Cells[i].Text != "0")
                //{
                HyperLink hLink = new HyperLink();
                //hLink.Text = e.Row.Cells[i].Text;
                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
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

                type = sSf_code;

                if (type.Contains("MR"))
                {
                    type = "MR";
                }
                else if (type.Contains("MGR"))
                {
                    type = "MGR";
                }

                if (type == "MR")
                {

                    SalesForce sal = new SalesForce();
                    dsSalesForce = sal.getTP_Deviation_At_glance(divcode, sSf_code, Convert.ToInt16(cMnth), Convert.ToInt16(cYr), type);
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        for (int s = 0; s < dsSalesForce.Tables[0].Rows.Count; s++)
                        {

                            if ((dsSalesForce.Tables[0].Rows[s]["WorkType_Name"].ToString() == "Field Work") && (dsSalesForce.Tables[0].Rows[s]["Worktype_Name_B"].ToString() == "Field Work"))
                            {
                                //SalesForce  = new SalesForce();
                                dsSales = sal.getTP_Deviation_Transl_terr(dsSalesForce.Tables[0].Rows[s]["trans_slno"].ToString());
                                string DCR_Terr = string.Empty;
                                foreach (DataRow drFF in dsSales.Tables[0].Rows)
                                {
                                    DCR_Terr += drFF[1].ToString() + ',';
                                }

                                //string fullName = DCR_worked_with;
                                //var names = fullName.Split(' ');
                                bool same = false;
                                string stringToCheck = dsSalesForce.Tables[0].Rows[s]["Asper_Tp"].ToString();
                                string[] name;
                                name = stringToCheck.Split(',');
                                string[] stringArray = { DCR_Terr };

                                foreach (string check in name)
                                {
                                    foreach (string x in stringArray)
                                    {
                                        if (x.Contains(check))
                                        {


                                            same = true;
                                            //break;  
                                        }

                                        else
                                        {
                                            same = false;
                                            count += 1;
                                            hLink.Text = count.ToString();


                                        }

                                    }

                                    if (same == true)
                                    {
                                        break;
                                    }
                                }

                            }


                            else
                            {
                                string WorkType_Name = dsSalesForce.Tables[0].Rows[s]["WorkType_Name"].ToString();
                                WorkType_Name = WorkType_Name.ToUpper();

                                string Worktype_Name_B = dsSalesForce.Tables[0].Rows[s]["Worktype_Name_B"].ToString();
                                Worktype_Name_B = Worktype_Name_B.ToUpper();

                                if (WorkType_Name != Worktype_Name_B)
                                {
                                    count += 1;
                                }
                                else
                                {

                                }
                            }
                        }
                    }


                }
                else if (type == "MGR")
                {
                    SalesForce sal = new SalesForce();
                    dsSalesForce = sal.getTP_Deviation_At_glance(divcode, sSf_code, Convert.ToInt16(cMnth), Convert.ToInt16(cYr), type);
                    if (dsSalesForce.Tables[0].Rows.Count > 0)
                    {
                        for (int s = 0; s < dsSalesForce.Tables[0].Rows.Count; s++)
                        {

                            if ((dsSalesForce.Tables[0].Rows[s]["WorkType_Name"].ToString() == "Field Work") && (dsSalesForce.Tables[0].Rows[s]["Worktype_Name_B"].ToString() == "Field Work"))
                            {
                                string DCR_worked_with = string.Empty;
                                dsSales = sal.getTP_Deviation_Transl(dsSalesForce.Tables[0].Rows[s]["trans_slno"].ToString());

                                foreach (DataRow drFF in dsSales.Tables[0].Rows)
                                {
                                    DCR_worked_with += drFF[0].ToString();
                                }

                                //string fullName = DCR_worked_with;
                                //var names = fullName.Split(' ');

                                bool same = false;
                                string stringToCheck = dsSalesForce.Tables[0].Rows[s]["Asper_Tp"].ToString();
                                string[] name;
                                name = stringToCheck.Split(',');
                                string[] stringArray = { DCR_worked_with };

                                foreach (string check in name)
                                {
                                    foreach (string x in stringArray)
                                    {
                                        if (x.Contains(check))
                                        {


                                            //break;  
                                        }
                                        else if (x == "SELF," && check == "Independent")
                                        {

                                            same = true;
                                        }
                                        else if (x == "SELF, " && check == "Independent")
                                        {

                                            same = true;
                                        }
                                        else
                                        {
                                            same = false;
                                            count += 1;
                                        }

                                    }

                                    if (same == true)
                                    {
                                        break;
                                    }
                                }

                            }
                            else
                            {

                                string WorkType_Name = dsSalesForce.Tables[0].Rows[s]["WorkType_Name"].ToString();
                                WorkType_Name = WorkType_Name.ToUpper();

                                string Worktype_Name_B = dsSalesForce.Tables[0].Rows[s]["Worktype_Name_B"].ToString();
                                Worktype_Name_B = Worktype_Name_B.ToUpper();

                                if (WorkType_Name != Worktype_Name_B)
                                {
                                    count += 1;
                                }
                                else
                                {

                                }
                            }

                        }
                    }
                }





                //hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + sCurrentDate + "')");
                //hLink.NavigateUrl = "#";
                hLink.ToolTip = "Click here";
                hLink.ForeColor = System.Drawing.Color.Blue;

                hLink.Text = count.ToString();

                if (hLink.Text == "0")
                {
                    hLink.Text = "";
                }
                e.Row.Cells[i].Controls.Add(hLink);
                int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "')");
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                j++;

                count = 0;




                //if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                //{
                //    e.Row.Cells[i].Text = "";
                //}
                e.Row.Cells[i].Attributes.Add("align", "center");



                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                {
                   // e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                   
                }
                //}

            }

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strFileName = Page.Title;
        string attachment = "attachment; filename='" + strFileName + "'.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
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

    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (type == "MR")
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblterritory_Code = (Label)e.Row.FindControl("lblterritory_Code");
                Label lblASper_Dcr = (Label)e.Row.FindControl("lblASper_Dcr");
                Label lbldate = (Label)e.Row.FindControl("lbldate");
                Label lblDate_Name = (Label)e.Row.FindControl("lblDate_Name");
                Label lblAsper_Tp = (Label)e.Row.FindControl("lblAsper_Tp");
                Label lblASper_Dcr_match = (Label)e.Row.FindControl("lblASper_Dcr_match");
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lbltrans_sl = (Label)e.Row.FindControl("lbltrans_sl");
                Label lblworkDCR = (Label)e.Row.FindControl("lblworkDCR");
                string workDCR = lblworkDCR.Text;
                string workDCR1 = workDCR.ToUpper();
                Label lblworkTP = (Label)e.Row.FindControl("lblworkTP");
                string workTP = lblworkTP.Text;
                string workTP1 = workTP.ToUpper();

                DateTime date = Convert.ToDateTime(lbldate.Text);

                lbldate.Text = date.ToString("dd/MM/yyyy");
                //lbldate.Text = date.Day.ToString();
                string DCR_Terr = string.Empty;

                if ((lblworkDCR.Text == "Field Work") && (lblworkTP.Text == "Field Work"))
                {
                    SalesForce sal = new SalesForce();
                    dsSales = sal.getTP_Deviation_Transl_terr(lbltrans_sl.Text);

                    foreach (DataRow drFF in dsSales.Tables[0].Rows)
                    {
                        DCR_Terr += drFF[1].ToString() + ',';
                    }

                    //string fullName = DCR_worked_with;
                    //var names = fullName.Split(' ');
                    bool same = false;
                    string stringToCheck = lblAsper_Tp.Text;
                    string[] name;
                    name = stringToCheck.Split(',');
                    string[] stringArray = { DCR_Terr };

                    foreach (string check in name)
                    {
                        foreach (string x in stringArray)
                        {
                            if (x.Contains(check))
                            {

                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                                //break;  
                            }

                            else
                            {
                                same = false;
                                count += 1;
                                lblSNo.Text = Convert.ToString(count);
                                e.Row.Cells[0].Visible = true;
                                e.Row.Cells[3].Visible = true;
                                e.Row.Cells[4].Visible = true;
                                e.Row.Cells[5].Visible = true;
                                e.Row.Cells[6].Visible = true;
                                lblAsper_Tp.Text = stringToCheck + "&nbsp;&nbsp;&nbsp;(Field Work)";
                                lblASper_Dcr_match.Text = DCR_Terr + "&nbsp;&nbsp;&nbsp;(Field Work)";

                            }

                        }

                        if (same == true)
                        {
                            break;
                        }
                    }

                }
                else
                {
                    if (workDCR1 != workTP1)
                    {
                        if (workDCR1 == "FIELD WORK")
                        {
                            lblASper_Dcr_match.Text = lblASper_Dcr_match.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        else if (workTP1 == "FIELD WORK")
                        {
                            lblAsper_Tp.Text = lblAsper_Tp.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        count += 1;
                        lblSNo.Text = Convert.ToString(count);
                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[3].Visible = true;
                        e.Row.Cells[4].Visible = true;
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                    }
                }
            }
        }
        else if (type == "MGR")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblterritory_Code = (Label)e.Row.FindControl("lblterritory_Code");
                Label lblASper_Dcr = (Label)e.Row.FindControl("lblASper_Dcr");
                Label lbldate = (Label)e.Row.FindControl("lbldate");
                Label lblDate_Name = (Label)e.Row.FindControl("lblDate_Name");
                Label lblAsper_Tp = (Label)e.Row.FindControl("lblAsper_Tp");
                Label lblASper_Dcr_match = (Label)e.Row.FindControl("lblASper_Dcr_match");
                Label lblSNo = (Label)e.Row.FindControl("lblSNo");
                Label lblworkDCR = (Label)e.Row.FindControl("lblworkDCR");
                string workDCR = lblworkDCR.Text;
                string workDCR1 = workDCR.ToUpper();
                Label lblworkTP = (Label)e.Row.FindControl("lblworkTP");
                string workTP = lblworkTP.Text;
                string workTP1 = workTP.ToUpper();

                Label lbltrans_sl = (Label)e.Row.FindControl("lbltrans_sl");

                DateTime date = Convert.ToDateTime(lbldate.Text);

                lbldate.Text = date.ToString("dd/MM/yyyy");
                //string str = lblASper_Dcr_match.Text;
                string DCR_worked_with = string.Empty;

                if ((lblworkDCR.Text == "Field Work") && (lblworkTP.Text == "Field Work"))
                {
                    SalesForce sal = new SalesForce();
                    dsSales = sal.getTP_Deviation_Transl(lbltrans_sl.Text);

                    foreach (DataRow drFF in dsSales.Tables[0].Rows)
                    {
                        DCR_worked_with += drFF[0].ToString();
                    }

                    //string fullName = DCR_worked_with;
                    //var names = fullName.Split(' ');
                    bool same = false;
                    string stringToCheck = lblAsper_Tp.Text;
                    string[] name;
                    name = stringToCheck.Split(',');
                    string[] stringArray = { DCR_worked_with };

                    foreach (string check in name)
                    {
                        foreach (string x in stringArray)
                        {
                            if (x.Contains(check))
                            {

                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                                //break;  
                            }
                            else if (x == "SELF," && check == "Independent")
                            {
                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                            }
                            else if (x == "SELF, " && check == "Independent")
                            {
                                e.Row.Cells[0].Visible = false;
                                e.Row.Cells[3].Visible = false;
                                e.Row.Cells[4].Visible = false;
                                e.Row.Cells[5].Visible = false;
                                e.Row.Cells[6].Visible = false;
                                same = true;
                            }
                            else
                            {
                                same = false;
                                count += 1;
                                lblSNo.Text = Convert.ToString(count);
                                e.Row.Cells[0].Visible = true;
                                e.Row.Cells[3].Visible = true;
                                e.Row.Cells[4].Visible = true;
                                e.Row.Cells[5].Visible = true;
                                e.Row.Cells[6].Visible = true;
                                lblAsper_Tp.Text = stringToCheck + "&nbsp;&nbsp;&nbsp;(Field Work)";
                                lblASper_Dcr_match.Text = DCR_worked_with + "&nbsp;&nbsp;&nbsp;(Field Work)";

                            }

                        }

                        if (same == true)
                        {
                            break;
                        }
                    }

                }
                else
                {
                    if (workDCR1 != workTP1)
                    {
                        if (workDCR1 == "FIELD WORK")
                        {
                            lblASper_Dcr_match.Text = lblASper_Dcr_match.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        else if (workTP1 == "FIELD WORK")
                        {
                            lblAsper_Tp.Text = lblAsper_Tp.Text + "&nbsp;&nbsp;&nbsp;(Field Work)";
                        }
                        count += 1;
                        lblSNo.Text = Convert.ToString(count);
                        e.Row.Cells[0].Visible = true;
                        e.Row.Cells[3].Visible = true;
                        e.Row.Cells[4].Visible = true;
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                        e.Row.Cells[3].Visible = false;
                        e.Row.Cells[4].Visible = false;
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                    }
                }
            }
        }
    }
}
