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

public partial class MIS_Reports_rptProduct_Exp_specat : System.Web.UI.Page
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
    int fldwrk_total = 0;
    int doctor_total = 0;
    int Chemist_total = 0;
    int Stock_toatal = 0;
    int Stock_calls_Seen_Total = 0;
    int ChemistPOB_total = 0;
    int UnListDoc = 0;
    int doc_met_total = 0;
    int doc_calls_seen_total = 0;
    int CSH_calls_seen_total = 0;
    int Dcr_Sub_days = 0;
    int Dcr_Leave = 0;
    double dblCoverage = 0.00;
    int UnLstdoc_calls_seen_total = 0;
    double dblaverage = 0.00;
    DateTime dtCurrent;
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
    string MultiSf_Code = string.Empty;
    string strspec = string.Empty;
    string strcat = string.Empty;
    string Type = string.Empty;

    string sCurrentDate = string.Empty;
    DataTable dtrowClr = null;
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    List<string> Spec = new List<string>();
    List<string> Spe1 = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        //MultiSf_Code = Session["MultiSf_Code"].ToString();
        divcode = Session["div_code"].ToString();
        sfCode = Request.QueryString["sfcode"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["FYear"].ToString();
        TMonth = Request.QueryString["TMonth"].ToString();
        TYear = Request.QueryString["TYear"].ToString();
        Prod = Request.QueryString["Prod"].ToString();
        Prod_Name = Request.QueryString["Prod_Name"].ToString();
        sfname = Request.QueryString["sf_name"].ToString();
        strspec = Request.QueryString["chkspec"].ToString();
        strcat = Request.QueryString["chkcat"].ToString();
        Type = Request.QueryString["Type"].ToString();

        lblRegionName.Text = sfname;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Product Exposure Analysis for the Period of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;

        if (Prod == "-1")
        {
            lblprd_name.Visible = false;
            //FillAll_Product();
            FillAll_Product1();
        }
        else
        {
            lblname.Text = "<span style='color:#0077FF'> " + "" + Prod_Name + "" + "</span>";
            // FillSF();
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

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        if (Type == "0")
        {

            string[] Spec;
            Spec = strspec.Split(',');

            DataTable dtspec = new DataTable();
            dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtspec.Columns["INX"].AutoIncrementSeed = 1;
            dtspec.Columns["INX"].AutoIncrementStep = 1;
            dtspec.Columns.Add("SPECE", typeof(int));

            foreach (string sp in Spec)
            {

                dtspec.Rows.Add(null, sp);

            }



            SqlCommand cmd = new SqlCommand("AllProduct_Exp_Count_SpeCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Strspec", dtspec);
            cmd.Parameters.AddWithValue("@Type", "0");
            cmd.CommandTimeout = 300;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            //dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(3);
            dsts.Tables[0].Columns.RemoveAt(1);
            GrdPrdExp.DataSource = dsts;
            GrdPrdExp.DataBind();
        }
        else if (Type == "1")
        {
            string[] Spec;
            Spec = strcat.Split(',');

            DataTable dtspec = new DataTable();
            dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtspec.Columns["INX"].AutoIncrementSeed = 1;
            dtspec.Columns["INX"].AutoIncrementStep = 1;
            dtspec.Columns.Add("SPECE", typeof(int));

            foreach (string sp in Spec)
            {

                dtspec.Rows.Add(null, sp);

            }



            SqlCommand cmd = new SqlCommand("AllProduct_Exp_Count_SpeCat", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Strspec", dtspec);
            cmd.Parameters.AddWithValue("@Type", "1");
            cmd.CommandTimeout = 300;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
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

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        if (Type == "0")
        {

            string[] Spec;
            Spec = strspec.Split(',');

            DataTable dtspec = new DataTable();
            dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtspec.Columns["INX"].AutoIncrementSeed = 1;
            dtspec.Columns["INX"].AutoIncrementStep = 1;
            dtspec.Columns.Add("SPECE", typeof(int));

            foreach (string sp in Spec)
            {

                dtspec.Rows.Add(null, sp);

            }

            SqlCommand cmd = new SqlCommand("Product_Exp_Count_Speciality", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Prod_Code", Prod);
            cmd.Parameters.AddWithValue("@Strspec", dtspec);
            cmd.Parameters.AddWithValue("@Type", "0");
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
            dtrowClr = dsts.Tables[0].Copy();
            dsts.Tables[0].Columns.RemoveAt(6);
            dsts.Tables[0].Columns.RemoveAt(5);
            dsts.Tables[0].Columns.RemoveAt(1);
            GrdPrdExp.DataSource = dsts;
            GrdPrdExp.DataBind();
        }
        else if (Type == "1")
        {

            string[] Spec;
            Spec = strcat.Split(',');

            DataTable dtspec = new DataTable();
            dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtspec.Columns["INX"].AutoIncrementSeed = 1;
            dtspec.Columns["INX"].AutoIncrementStep = 1;
            dtspec.Columns.Add("SPECE", typeof(int));

            foreach (string sp in Spec)
            {

                dtspec.Rows.Add(null, sp);

            }

            SqlCommand cmd = new SqlCommand("Product_Exp_Count_Speciality", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
            cmd.Parameters.AddWithValue("@Msf_code", sfCode);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Prod_Code", Prod);
            cmd.Parameters.AddWithValue("@Strspec", dtspec);
            cmd.Parameters.AddWithValue("@Type", "1");
            cmd.CommandTimeout = 150;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            con.Close();
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

            if (Type == "0")
            {
                Doctor dr = new Doctor();
                dsDoctor = dr.getDocSpec_ForExpo(strspec);
            }
            else if (Type == "1")
            {
                Doctor dr = new Doctor();
                dsDoctor = dr.getDocCat_ForExpo(strcat);
            }
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

                GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell2 = new TableCell();


                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Product Name / Month", "#0097AC", true);
                //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No of Doctors - Tagged", "#0097AC", true);


                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());




                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {

                    Spec = new List<string>();

                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, dsDoctor.Tables[0].Rows.Count, sTxt, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, dsDoctor.Tables[0].Rows.Count, "No. of Drs (As Per DCR)", "#0097AC", true);

                    //string[] Spec;

                    for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                    {

                        if (Type == "0")
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Doc_Special_Code"].ToString());
                        }
                        else if (Type == "1")
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Doc_Cat_Code"].ToString());
                        }

                    }
                    Spec.Sort();
                    foreach (string specc in Spec)
                    {
                        Spe1.Add(specc);
                        Doctor doc = new Doctor();
                        iLstMonth.Add(cmonth);
                        iLstYear.Add(cyear);
                        if (Type == "0")
                        {

                            dsDoc = doc.getDocSpec_ForExpo(specc);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Doc_Special_SName"].ToString(), "#0097AC", true);

                        }
                        else if (Type == "1")
                        {
                            dsDoc = doc.getDocCat_ForExpo(specc);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Doc_Cat_SName"].ToString(), "#0097AC", true);
                            //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dataRow["Doc_Cat_SName"].ToString(), "#0097AC", true);
                        }
                    }

                    cmonth = cmonth + 1;

                    if (cmonth == 13)
                    {
                        cmonth = 1;
                        cyear = cyear + 1;
                    }
                }


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
            if (Type == "0")
            {
                Doctor dr = new Doctor();

                dsDoctor = dr.getDocSpec_ForExpo(strspec);
            }
            else if (Type == "1")
            {
                Doctor dr = new Doctor();
                dsDoctor = dr.getDocCat_ForExpo(strcat);
            }

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
                GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //Creating a table cell object
                TableCell objtablecell2 = new TableCell();
                #endregion
                //
                #region Merge cells

                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);

                int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());




                SalesForce sf = new SalesForce();

                for (int i = 0; i <= months; i++)
                {
                    Spec = new List<string>();

                    string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                    AddMergedCells(objgridviewrow, objtablecell, 0, dsDoctor.Tables[0].Rows.Count, sTxt, "#0097AC", true);
                    AddMergedCells(objgridviewrow1, objtablecell1, 0, dsDoctor.Tables[0].Rows.Count, "No. of Drs (As Per DCR)", "#0097AC", true);




                    for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                    {

                        if (Type == "0")
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Doc_Special_Code"].ToString());
                        }
                        else if (Type == "1")
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Doc_Cat_Code"].ToString());
                        }

                    }
                    Spec.Sort();
                    foreach (string specc in Spec)
                    {
                        Spe1.Add(specc);
                        Doctor doc = new Doctor();
                        iLstMonth.Add(cmonth);
                        iLstYear.Add(cyear);
                        if (Type == "0")
                        {

                            dsDoc = doc.getDocSpec_ForExpo(specc);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Doc_Special_SName"].ToString(), "#0097AC", true);

                        }
                        else if (Type == "1")
                        {
                            dsDoc = doc.getDocCat_ForExpo(specc);
                            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Doc_Cat_SName"].ToString(), "#0097AC", true);
                            //AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dataRow["Doc_Cat_SName"].ToString(), "#0097AC", true);
                        }
                    }


                    //for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                    //{
                    //    iLstMonth.Add(cmonth);
                    //    iLstYear.Add(cyear);
                    //    if (Type == "0")
                    //    {
                    //        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[k]["Doc_Special_SName"].ToString(), "#0097AC", true);
                    //    }
                    //    else if (Type == "1")
                    //    {
                    //        AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoctor.Tables[0].Rows[k]["Doc_Cat_SName"].ToString(), "#0097AC", true);
                    //    }
                    //}

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
                objGridView.Controls[0].Controls.AddAt(2, objgridviewrow2);
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
        if (celltext == "Total")
        {
            objtablecell.Style.Add("color", "red");
        }
        else
        {
            //objtablecell.Style.Add("color", "white");
        }
        //objtablecell.Style.Add("border-color", "black");
        if ((colspan != 0 || rowSpan != 0) && celltext != "No. of Drs (As Per DCR)")
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else if (colspan != 0 && celltext == "No. of Drs (As Per DCR)")
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
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
                for (int i = 2, j = 0; i < e.Row.Cells.Count; i++)
                {

                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string Prd_code = dtrowClr.Rows[iInx][1].ToString();
                        string Prd_Name = dtrowClr.Rows[iInx][2].ToString();
                        int cMnth = iLstMonth[j];
                        int cYr = iLstYear[j];
                        string specc = Spe1[j];

                        if (cMnth == 12)
                        {
                            sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        }
                        else
                        {
                            sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        }

                        //hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "')");
                        // hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode + "', '" + sfname + "', '" + cYr + "', '" + cMnth + "','" + Prd_Name + "','" + Prd_code + "', '" + sCurrentDate + "')");

                        if (Type == "0")
                        {

                            hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode + "', '" + sfname + "', '" + cYr + "', '" + cMnth + "', '" + Prd_Name + "', '" + Prd_code + "','" + sCurrentDate + "','" + specc + "','" + Type + "')");

                        }
                        else if (Type == "1")
                        {
                            hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode + "', '" + sfname + "', '" + cYr + "', '" + cMnth + "', '" + Prd_Name + "', '" + Prd_code + "','" + sCurrentDate + "','" + specc + "','" + Type + "')");

                        }



                        hLink.ToolTip = "Click here";
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);

                        j++;
                    }


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
                for (int i = 4, j = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.Cells[i].Text != "0")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;
                        string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                        string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                        int cMnth = iLstMonth[j];
                        int cYr = iLstYear[j];
                        //string Spe1 = Spec1[j];
                        string specc = Spe1[j];


                        if (cMnth == 12)
                        {
                            sCurrentDate = "01-01-" + (cYr + 1).ToString();
                        }
                        else
                        {
                            sCurrentDate = (cMnth + 1).ToString() + "-01-" + cYr.ToString();
                        }
                        // hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod_Name + "','" + Prod + "', '" + sCurrentDate + "')");

                        if (Type == "0")
                        {

                            hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + specc + "','" + Type + "')");

                        }
                        else if (Type == "1")
                        {
                            hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + specc + "','" + Type + "')");

                        }
                        hLink.ToolTip = "Click here";
                        hLink.ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[i].Controls.Add(hLink);
                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                        j++;
                    }

                    if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "-")
                    {
                        e.Row.Cells[i].Text = "-";
                    }
                    e.Row.Cells[i].Attributes.Add("align", "center");

                }
            }


        }
    }
    //private void FillSF()
    //{
    //    string sURL = string.Empty;

    //    tbl.Rows.Clear();
    //    doctor_total = 0;

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.sp_UserList_getMR_Doc_List(divcode, sfCode);

    //    if (Type == "0")
    //    {

    //        if (dsSalesForce.Tables[0].Rows.Count > 0)
    //        {
    //            TableRow tr_header = new TableRow();
    //            tr_header.BorderStyle = BorderStyle.Solid;
    //            tr_header.BorderWidth = 1;
    //            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
    //            tr_header.Style.Add("Color", "White");
    //            tr_header.BorderColor = System.Drawing.Color.Black;

    //            TableCell tc_SNo = new TableCell();
    //            tc_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_SNo.BorderWidth = 1;
    //            tc_SNo.Width = 50;
    //            tc_SNo.RowSpan = 3;
    //            Literal lit_SNo =
    //                new Literal();
    //            lit_SNo.Text = "#";
    //            tc_SNo.BorderColor = System.Drawing.Color.Black;
    //            tc_SNo.Controls.Add(lit_SNo);
    //            tc_SNo.Attributes.Add("Class", "rptCellBorder");
    //            tr_header.Cells.Add(tc_SNo);

    //            TableCell tc_DR_Code = new TableCell();
    //            tc_DR_Code.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Code.BorderWidth = 1;
    //            tc_DR_Code.Width = 400;
    //            tc_DR_Code.RowSpan = 3;
    //            Literal lit_DR_Code = new Literal();
    //            lit_DR_Code.Text = "<center>SF Code</center>";
    //            tc_DR_Code.Controls.Add(lit_DR_Code);
    //            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Code.Visible = false;
    //            tr_header.Cells.Add(tc_DR_Code);

    //            TableCell tc_DR_Name = new TableCell();
    //            tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Name.BorderWidth = 1;
    //            tc_DR_Name.Width = 200;
    //            tc_DR_Name.RowSpan = 3;
    //            Literal lit_DR_Name = new Literal();
    //            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
    //            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Name.Controls.Add(lit_DR_Name);
    //            tr_header.Cells.Add(tc_DR_Name);


    //            TableCell tc_DR_HQ = new TableCell();
    //            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
    //            tc_DR_HQ.BorderWidth = 1;
    //            tc_DR_HQ.Width = 100;
    //            tc_DR_HQ.RowSpan = 3;
    //            Literal lit_DR_HQ = new Literal();
    //            lit_DR_HQ.Text = "<center>HQ</center>";
    //            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_HQ.Controls.Add(lit_DR_HQ);
    //            tr_header.Cells.Add(tc_DR_HQ);


    //            TableCell tc_DR_Des = new TableCell();
    //            tc_DR_Des.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Des.BorderWidth = 1;
    //            tc_DR_Des.Width = 80;
    //            tc_DR_Des.RowSpan = 3;
    //            Literal lit_DR_Des = new Literal();
    //            lit_DR_Des.Text = "<center>Designation</center>";
    //            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Des.Controls.Add(lit_DR_Des);
    //            tr_header.Cells.Add(tc_DR_Des);

    //            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //            int cmonth = Convert.ToInt32(FMonth);
    //            int cyear = Convert.ToInt32(FYear);


    //            ViewState["months"] = months;
    //            ViewState["cmonth"] = cmonth;
    //            ViewState["cyear"] = cyear;

    //            Doctor dr = new Doctor();
    //            dsDoctor = dr.getDocSpec_ForExpo(strspec);

    //            if (months >= 0)
    //            {
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_month = new TableCell();
    //                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
    //                    //tc_month.ColumnSpan = 1;
    //                    Literal lit_month = new Literal();
    //                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
    //                    tc_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_month.BorderStyle = BorderStyle.Solid;
    //                    tc_month.BorderWidth = 1;
    //                    tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                    //tc_month.Width = 200;
    //                    tc_month.Controls.Add(lit_month);
    //                    tr_header.Cells.Add(tc_month);
    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //            }
    //            tbl.Rows.Add(tr_header);

    //            //Sub Header
    //            months = Convert.ToInt16(ViewState["months"].ToString());
    //            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            if (months >= 0)
    //            {
    //                TableRow tr_lst_det = new TableRow();
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_lst_month = new TableCell();
    //                    HyperLink lit_lst_month = new HyperLink();
    //                    lit_lst_month.Text = "No. of Drs";
    //                    tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_lst_month.BorderWidth = 1;
    //                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //                    tc_lst_month.Controls.Add(lit_lst_month);
    //                    tr_lst_det.Cells.Add(tc_lst_month);


    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_lst_det.Style.Add("Color", "White");

    //                tr_lst_det.Attributes.Add("Class", "Backcolor");

    //                tbl.Rows.Add(tr_lst_det);
    //            }

    //            if (months >= 0)
    //            {
    //                TableRow tr_catg = new TableRow();
    //                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_catg.Style.Add("Color", "White");
    //                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //                for (int j = 1; j <= (months + 1) * 1; j++)
    //                {
    //                    if (dsDoctor.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {
    //                            TableCell tc_catg_name = new TableCell();
    //                            tc_catg_name.BorderStyle = BorderStyle.Solid;
    //                            tc_catg_name.BorderWidth = 1;
    //                            if ((j % 2) == 1)
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
    //                            }
    //                            else
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
    //                            }
    //                            // tc_catg_name.Width = 30;

    //                            Literal lit_catg_name = new Literal();
    //                            lit_catg_name.Text = dataRow["Doc_Special_SName"].ToString();
    //                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
    //                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_catg_name.Controls.Add(lit_catg_name);
    //                            tr_catg.Cells.Add(tc_catg_name);
    //                        }

    //                        tbl.Rows.Add(tr_catg);
    //                    }
    //                }
    //            }

    //            if (dsSalesForce.Tables[0].Rows.Count > 0)
    //                ViewState["dsSalesForce"] = dsSalesForce;


    //            int iCount = 0;
    //            int iTotLstCount = 0;
    //            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

    //            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            {
    //                TableRow tr_det = new TableRow();
    //                iCount += 1;
    //                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


    //                //S.No
    //                TableCell tc_det_SNo = new TableCell();
    //                Literal lit_det_SNo = new Literal();
    //                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //                tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //                tc_det_SNo.BorderWidth = 1;
    //                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_SNo.Controls.Add(lit_det_SNo);
    //                tr_det.Cells.Add(tc_det_SNo);
    //                tr_det.BackColor = System.Drawing.Color.White;

    //                //SF_code
    //                TableCell tc_det_usr = new TableCell();
    //                Literal lit_det_usr = new Literal();
    //                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
    //                tc_det_usr.BorderStyle = BorderStyle.Solid;
    //                tc_det_usr.BorderWidth = 1;
    //                tc_det_usr.Visible = false;
    //                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_usr.Controls.Add(lit_det_usr);
    //                tr_det.Cells.Add(tc_det_usr);

    //                //SF Name
    //                TableCell tc_det_FF = new TableCell();
    //                Literal lit_det_FF = new Literal();
    //                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
    //                tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                tc_det_FF.BorderWidth = 1;
    //                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_FF.Controls.Add(lit_det_FF);
    //                tr_det.Cells.Add(tc_det_FF);

    //                //hq
    //                TableCell tc_det_hq = new TableCell();
    //                Literal lit_det_hq = new Literal();
    //                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
    //                tc_det_hq.BorderStyle = BorderStyle.Solid;
    //                tc_det_hq.BorderWidth = 1;
    //                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_hq.Controls.Add(lit_det_hq);
    //                tr_det.Cells.Add(tc_det_hq);

    //                //SF Designation Short Name
    //                TableCell tc_det_Designation = new TableCell();
    //                Literal lit_det_Designation = new Literal();
    //                lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
    //                tc_det_Designation.BorderStyle = BorderStyle.Solid;
    //                tc_det_Designation.BorderWidth = 1;
    //                tc_det_Designation.Controls.Add(lit_det_Designation);
    //                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
    //                tr_det.Cells.Add(tc_det_Designation);

    //                months = Convert.ToInt16(ViewState["months"].ToString());
    //                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


    //                if (months >= 0)
    //                {

    //                    for (int j = 1; j <= months + 1; j++)
    //                    {

    //                        if (cmonth == 12)
    //                        {
    //                            sCurrentDate = "01-01-" + (cyear + 1);
    //                        }
    //                        else
    //                        {
    //                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //                        }

    //                        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //                        if (dsDoctor.Tables[0].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                            {

    //                                dsDoc = sf.getProduct_Exp_Speciality(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()));


    //                                if (dsDoc.Tables[0].Rows.Count > 0)
    //                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


    //                                TableCell tc_lst_month = new TableCell();
    //                                HyperLink hyp_lst_month = new HyperLink();

    //                                if (tot_dr != "0")
    //                                {
    //                                    //iTotLstCount += Convert.ToInt16(tot_dr);
    //                                    hyp_lst_month.Text = tot_dr;

    //                                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

    //                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()) + "','"+Type+"')");
    //                                    //hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
    //                                    hyp_lst_month.NavigateUrl = "#";



    //                                }

    //                                else
    //                                {
    //                                    hyp_lst_month.Text = "";
    //                                }


    //                                tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                                tc_lst_month.BorderWidth = 1;
    //                                tc_lst_month.BackColor = System.Drawing.Color.White;
    //                                tc_lst_month.Width = 200;
    //                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
    //                                tc_lst_month.Controls.Add(hyp_lst_month);
    //                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                                tr_det.Cells.Add(tc_lst_month);
    //                            }
    //                        }
    //                        cmonth = cmonth + 1;
    //                        if (cmonth == 13)
    //                        {
    //                            cmonth = 1;
    //                            cyear = cyear + 1;
    //                        }

    //                    }
    //                    //

    //                }

    //                tbl.Rows.Add(tr_det);

    //            }



    //            //TableRow tr_total = new TableRow();

    //            //TableCell tc_Count_Total = new TableCell();
    //            //tc_Count_Total.BorderStyle = BorderStyle.Solid;
    //            //tc_Count_Total.BorderWidth = 1;
    //            ////tc_catg_Total.Width = 25;
    //            //Literal lit_Count_Total = new Literal();
    //            //lit_Count_Total.Text = "<center>Total</center>";
    //            //tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
    //            //tc_Count_Total.Controls.Add(lit_Count_Total);
    //            //tc_Count_Total.Font.Bold.ToString();
    //            //tc_Count_Total.BackColor = System.Drawing.Color.White;
    //            //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
    //            //tc_Count_Total.ColumnSpan = 4;
    //            //tc_Count_Total.Style.Add("text-align", "left");
    //            //tc_Count_Total.Style.Add("font-family", "Calibri");
    //            //tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
    //            //tc_Count_Total.Style.Add("font-size", "10pt");

    //            //tr_total.Cells.Add(tc_Count_Total);


    //            //Session["Test"] = "";

    //            //Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

    //            //months = Convert.ToInt16(ViewState["months"].ToString());
    //            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            //if (months >= 0)
    //            //{
    //            //    Session["Test"] = "T";
    //            //    for (int j = 1; j <= months + 1; j++)
    //            //    {
    //            //        if (cmonth == 12)
    //            //        {
    //            //            sCurrentDate = "01-01-" + (cyear + 1);
    //            //        }
    //            //        else
    //            //        {
    //            //            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //            //        }

    //            //        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //            //        TableCell tc_tot_month = new TableCell();
    //            //        HyperLink hyp_month = new HyperLink();
    //            //        iTotLstCount = 0;


    //            //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            //        {

    //            //            if (dsDoctor.Tables[0].Rows.Count > 0)
    //            //            {
    //            //                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //            //                {

    //            //                    dsDoc = sf.getProduct_Exp_Speciality(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()));


    //            //                    if (dsDoc.Tables[0].Rows.Count > 0)
    //            //                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //                    if (tot_dr != "0")
    //            //                    {
    //            //                        iTotLstCount += Convert.ToInt16(tot_dr);
    //            //                        hyp_month.Text = iTotLstCount.ToString();

    //            //                        hyp_month.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()) + "','" + Type + "')");
    //            //                    }

    //            //                }

    //            //            }
    //            //            tc_tot_month.BorderStyle = BorderStyle.Solid;
    //            //            tc_tot_month.BorderWidth = 1;
    //            //            tc_tot_month.BackColor = System.Drawing.Color.White;
    //            //            tc_tot_month.Width = 200;
    //            //            tc_tot_month.Style.Add("font-family", "Calibri");
    //            //            tc_tot_month.Style.Add("font-size", "10pt");
    //            //            tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
    //            //            tc_tot_month.VerticalAlign = VerticalAlign.Middle;
    //            //            tc_tot_month.Controls.Add(hyp_month);
    //            //            tc_tot_month.Attributes.Add("style", "font-weight:bold;");
    //            //            tc_tot_month.Attributes.Add("Class", "rptCellBorder");
    //            //            tr_total.Cells.Add(tc_tot_month);
    //            //            Session["Test"] = "G";
    //            //        }

    //            //        cmonth = cmonth + 1;
    //            //        if (cmonth == 13)
    //            //        {
    //            //            cmonth = 1;
    //            //            cyear = cyear + 1;
    //            //        }

    //            //    }
    //            //}

    //            //tbl.Rows.Add(tr_total);
    //        }

    //    }
    //    else if (Type == "1")
    //    {

    //        if (dsSalesForce.Tables[0].Rows.Count > 0)
    //        {
    //            TableRow tr_header = new TableRow();
    //            tr_header.BorderStyle = BorderStyle.Solid;
    //            tr_header.BorderWidth = 1;
    //            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
    //            tr_header.Style.Add("Color", "White");
    //            tr_header.BorderColor = System.Drawing.Color.Black;

    //            TableCell tc_SNo = new TableCell();
    //            tc_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_SNo.BorderWidth = 1;
    //            tc_SNo.Width = 50;
    //            tc_SNo.RowSpan = 3;
    //            Literal lit_SNo =
    //                new Literal();
    //            lit_SNo.Text = "#";
    //            tc_SNo.BorderColor = System.Drawing.Color.Black;
    //            tc_SNo.Controls.Add(lit_SNo);
    //            tc_SNo.Attributes.Add("Class", "rptCellBorder");
    //            tr_header.Cells.Add(tc_SNo);

    //            TableCell tc_DR_Code = new TableCell();
    //            tc_DR_Code.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Code.BorderWidth = 1;
    //            tc_DR_Code.Width = 400;
    //            tc_DR_Code.RowSpan = 3;
    //            Literal lit_DR_Code = new Literal();
    //            lit_DR_Code.Text = "<center>SF Code</center>";
    //            tc_DR_Code.Controls.Add(lit_DR_Code);
    //            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Code.Visible = false;
    //            tr_header.Cells.Add(tc_DR_Code);

    //            TableCell tc_DR_Name = new TableCell();
    //            tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Name.BorderWidth = 1;
    //            tc_DR_Name.Width = 200;
    //            tc_DR_Name.RowSpan = 3;
    //            Literal lit_DR_Name = new Literal();
    //            lit_DR_Name.Text = "<center>Fieldforce&nbspName / Month</center>";
    //            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Name.Controls.Add(lit_DR_Name);
    //            tr_header.Cells.Add(tc_DR_Name);


    //            TableCell tc_DR_HQ = new TableCell();
    //            tc_DR_HQ.BorderStyle = BorderStyle.Solid;
    //            tc_DR_HQ.BorderWidth = 1;
    //            tc_DR_HQ.Width = 100;
    //            tc_DR_HQ.RowSpan = 3;
    //            Literal lit_DR_HQ = new Literal();
    //            lit_DR_HQ.Text = "<center>HQ</center>";
    //            tc_DR_HQ.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_HQ.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_HQ.Controls.Add(lit_DR_HQ);
    //            tr_header.Cells.Add(tc_DR_HQ);


    //            TableCell tc_DR_Des = new TableCell();
    //            tc_DR_Des.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Des.BorderWidth = 1;
    //            tc_DR_Des.Width = 80;
    //            tc_DR_Des.RowSpan = 3;
    //            Literal lit_DR_Des = new Literal();
    //            lit_DR_Des.Text = "<center>Designation</center>";
    //            tc_DR_Des.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Des.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Des.Controls.Add(lit_DR_Des);
    //            tr_header.Cells.Add(tc_DR_Des);

    //            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //            int cmonth = Convert.ToInt32(FMonth);
    //            int cyear = Convert.ToInt32(FYear);


    //            ViewState["months"] = months;
    //            ViewState["cmonth"] = cmonth;
    //            ViewState["cyear"] = cyear;

    //            Doctor dr = new Doctor();
    //            dsDoctor = dr.getDocCat_ForExpo(strcat);

    //            if (months >= 0)
    //            {
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_month = new TableCell();
    //                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
    //                    //tc_month.ColumnSpan = 1;
    //                    Literal lit_month = new Literal();
    //                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
    //                    tc_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_month.BorderStyle = BorderStyle.Solid;
    //                    tc_month.BorderWidth = 1;
    //                    tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                    //tc_month.Width = 200;
    //                    tc_month.Controls.Add(lit_month);
    //                    tr_header.Cells.Add(tc_month);
    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //            }
    //            tbl.Rows.Add(tr_header);

    //            //Sub Header
    //            months = Convert.ToInt16(ViewState["months"].ToString());
    //            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            if (months >= 0)
    //            {
    //                TableRow tr_lst_det = new TableRow();
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_lst_month = new TableCell();
    //                    HyperLink lit_lst_month = new HyperLink();
    //                    lit_lst_month.Text = "No. of Drs";
    //                    tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_lst_month.BorderWidth = 1;
    //                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //                    tc_lst_month.Controls.Add(lit_lst_month);
    //                    tr_lst_det.Cells.Add(tc_lst_month);


    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_lst_det.Style.Add("Color", "White");

    //                tr_lst_det.Attributes.Add("Class", "Backcolor");

    //                tbl.Rows.Add(tr_lst_det);
    //            }

    //            if (months >= 0)
    //            {
    //                TableRow tr_catg = new TableRow();
    //                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_catg.Style.Add("Color", "White");
    //                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //                for (int j = 1; j <= (months + 1) * 1; j++)
    //                {
    //                    if (dsDoctor.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {
    //                            TableCell tc_catg_name = new TableCell();
    //                            tc_catg_name.BorderStyle = BorderStyle.Solid;
    //                            tc_catg_name.BorderWidth = 1;
    //                            if ((j % 2) == 1)
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
    //                            }
    //                            else
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
    //                            }
    //                            // tc_catg_name.Width = 30;

    //                            Literal lit_catg_name = new Literal();
    //                            lit_catg_name.Text = dataRow["Doc_Cat_SName"].ToString();
    //                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
    //                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_catg_name.Controls.Add(lit_catg_name);
    //                            tr_catg.Cells.Add(tc_catg_name);
    //                        }

    //                        tbl.Rows.Add(tr_catg);
    //                    }
    //                }
    //            }

    //            if (dsSalesForce.Tables[0].Rows.Count > 0)
    //                ViewState["dsSalesForce"] = dsSalesForce;


    //            int iCount = 0;
    //            int iTotLstCount = 0;
    //            dsSalesForce = (DataSet)ViewState["dsSalesForce"];

    //            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            {
    //                TableRow tr_det = new TableRow();
    //                iCount += 1;
    //                strSf_Code += "'" + drFF["sf_code"].ToString() + "'" + ",";


    //                //S.No
    //                TableCell tc_det_SNo = new TableCell();
    //                Literal lit_det_SNo = new Literal();
    //                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //                tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //                tc_det_SNo.BorderWidth = 1;
    //                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_SNo.Controls.Add(lit_det_SNo);
    //                tr_det.Cells.Add(tc_det_SNo);
    //                tr_det.BackColor = System.Drawing.Color.White;

    //                //SF_code
    //                TableCell tc_det_usr = new TableCell();
    //                Literal lit_det_usr = new Literal();
    //                lit_det_usr.Text = "&nbsp;" + drFF["Sf_Code"].ToString();
    //                tc_det_usr.BorderStyle = BorderStyle.Solid;
    //                tc_det_usr.BorderWidth = 1;
    //                tc_det_usr.Visible = false;
    //                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_usr.Controls.Add(lit_det_usr);
    //                tr_det.Cells.Add(tc_det_usr);

    //                //SF Name
    //                TableCell tc_det_FF = new TableCell();
    //                Literal lit_det_FF = new Literal();
    //                lit_det_FF.Text = "&nbsp;" + drFF["sf_name"].ToString();
    //                tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                tc_det_FF.BorderWidth = 1;
    //                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_FF.Controls.Add(lit_det_FF);
    //                tr_det.Cells.Add(tc_det_FF);

    //                //hq
    //                TableCell tc_det_hq = new TableCell();
    //                Literal lit_det_hq = new Literal();
    //                lit_det_hq.Text = "&nbsp;" + drFF["sf_hq"].ToString();
    //                tc_det_hq.BorderStyle = BorderStyle.Solid;
    //                tc_det_hq.BorderWidth = 1;
    //                tc_det_hq.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_hq.Controls.Add(lit_det_hq);
    //                tr_det.Cells.Add(tc_det_hq);

    //                //SF Designation Short Name
    //                TableCell tc_det_Designation = new TableCell();
    //                Literal lit_det_Designation = new Literal();
    //                lit_det_Designation.Text = "&nbsp;" + drFF["sf_Designation_Short_Name"].ToString();
    //                tc_det_Designation.BorderStyle = BorderStyle.Solid;
    //                tc_det_Designation.BorderWidth = 1;
    //                tc_det_Designation.Controls.Add(lit_det_Designation);
    //                tc_det_Designation.Attributes.Add("Class", "rptCellBorder");
    //                tr_det.Cells.Add(tc_det_Designation);

    //                months = Convert.ToInt16(ViewState["months"].ToString());
    //                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //                cyear = Convert.ToInt16(ViewState["cyear"].ToString());


    //                if (months >= 0)
    //                {

    //                    for (int j = 1; j <= months + 1; j++)
    //                    {

    //                        if (cmonth == 12)
    //                        {
    //                            sCurrentDate = "01-01-" + (cyear + 1);
    //                        }
    //                        else
    //                        {
    //                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //                        }

    //                        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //                        if (dsDoctor.Tables[0].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                            {

    //                                dsDoc = sf.getProduct_Exp_Category(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));


    //                                if (dsDoc.Tables[0].Rows.Count > 0)
    //                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


    //                                TableCell tc_lst_month = new TableCell();
    //                                HyperLink hyp_lst_month = new HyperLink();

    //                                if (tot_dr != "0")
    //                                {
    //                                    //iTotLstCount += Convert.ToInt16(tot_dr);
    //                                    hyp_lst_month.Text = tot_dr;

    //                                    //sURL = "&sf_code=" + drFF["Sf_Code"] + "&sf_name=" + drFF["sf_name"] + "&Year=" + cyear + "&Month=" + cmonth + "&Prod_Name=" + Prod_Name + "&Prod=" + Prod + "&sCurrentDate=" + sCurrentDate + "";

    //                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + drFF["sf_code"].ToString() + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "','" + Type + "')");
    //                                    //hyp_lst_month.Attributes["onclick"] = "javascript:showModalPopUp('" + sURL + "')";
    //                                    hyp_lst_month.NavigateUrl = "#";



    //                                }

    //                                else
    //                                {
    //                                    hyp_lst_month.Text = "";
    //                                }


    //                                tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                                tc_lst_month.BorderWidth = 1;
    //                                tc_lst_month.BackColor = System.Drawing.Color.White;
    //                                tc_lst_month.Width = 200;
    //                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
    //                                tc_lst_month.Controls.Add(hyp_lst_month);
    //                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                                tr_det.Cells.Add(tc_lst_month);
    //                            }
    //                        }
    //                        cmonth = cmonth + 1;
    //                        if (cmonth == 13)
    //                        {
    //                            cmonth = 1;
    //                            cyear = cyear + 1;
    //                        }

    //                    }
    //                    //

    //                }

    //                tbl.Rows.Add(tr_det);

    //            }



    //            //TableRow tr_total = new TableRow();

    //            //TableCell tc_Count_Total = new TableCell();
    //            //tc_Count_Total.BorderStyle = BorderStyle.Solid;
    //            //tc_Count_Total.BorderWidth = 1;
    //            ////tc_catg_Total.Width = 25;
    //            //Literal lit_Count_Total = new Literal();
    //            //lit_Count_Total.Text = "<center>Total</center>";
    //            //tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
    //            //tc_Count_Total.Controls.Add(lit_Count_Total);
    //            //tc_Count_Total.Font.Bold.ToString();
    //            //tc_Count_Total.BackColor = System.Drawing.Color.White;
    //            //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
    //            //tc_Count_Total.ColumnSpan = 4;
    //            //tc_Count_Total.Style.Add("text-align", "left");
    //            //tc_Count_Total.Style.Add("font-family", "Calibri");
    //            //tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
    //            //tc_Count_Total.Style.Add("font-size", "10pt");

    //            //tr_total.Cells.Add(tc_Count_Total);


    //            //Session["Test"] = "";

    //            //Session["Sf_Code_multiple"] = strSf_Code.Remove(strSf_Code.Length - 1);

    //            //months = Convert.ToInt16(ViewState["months"].ToString());
    //            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            //if (months >= 0)
    //            //{
    //            //    Session["Test"] = "T";
    //            //    for (int j = 1; j <= months + 1; j++)
    //            //    {
    //            //        if (cmonth == 12)
    //            //        {
    //            //            sCurrentDate = "01-01-" + (cyear + 1);
    //            //        }
    //            //        else
    //            //        {
    //            //            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //            //        }

    //            //        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //            //        TableCell tc_tot_month = new TableCell();
    //            //        HyperLink hyp_month = new HyperLink();
    //            //        iTotLstCount = 0;

    //            //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            //        {
    //            //            if (dsDoctor.Tables[0].Rows.Count > 0)
    //            //            {
    //            //                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //            //                {

    //            //                    dsDoc = sf.getProduct_Exp_Category(drFF["sf_code"].ToString(), divcode, cmonth, cyear, Convert.ToInt16(Prod), sCurrentDate, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));


    //            //                    if (dsDoc.Tables[0].Rows.Count > 0)
    //            //                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //                    if (tot_dr != "0")
    //            //                    {
    //            //                        iTotLstCount += Convert.ToInt16(tot_dr);
    //            //                        hyp_month.Text = iTotLstCount.ToString();

    //            //                        hyp_month.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + drFF["sf_name"] + "', '" + cyear + "', '" + cmonth + "', '" + Prod_Name + "', '" + Prod + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "','" + Type + "')");
    //            //                    }
    //            //                }



    //            //                tc_tot_month.BorderStyle = BorderStyle.Solid;
    //            //                tc_tot_month.BorderWidth = 1;
    //            //                tc_tot_month.BackColor = System.Drawing.Color.White;
    //            //                tc_tot_month.Width = 200;
    //            //                tc_tot_month.Style.Add("font-family", "Calibri");
    //            //                tc_tot_month.Style.Add("font-size", "10pt");
    //            //                tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
    //            //                tc_tot_month.VerticalAlign = VerticalAlign.Middle;
    //            //                tc_tot_month.Controls.Add(hyp_month);
    //            //                tc_tot_month.Attributes.Add("style", "font-weight:bold;");
    //            //                tc_tot_month.Attributes.Add("Class", "rptCellBorder");
    //            //                tr_total.Cells.Add(tc_tot_month);
    //            //                Session["Test"] = "G";
    //            //            }
    //            //        }

    //            //        cmonth = cmonth + 1;
    //            //        if (cmonth == 13)
    //            //        {
    //            //            cmonth = 1;
    //            //            cyear = cyear + 1;
    //            //        }

    //            //    }
    //            //}

    //            //tbl.Rows.Add(tr_total);
    //        }

    //    }

    //}
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

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }

    //private void FillAll_Product()
    //{
    //    string sURL = string.Empty;

    //    tbl.Rows.Clear();
    //    doctor_total = 0;

    //    SalesForce sf = new SalesForce();
    //    dsSalesForce = sf.getProduct_Exp(divcode);

    //    if (Type == "0")
    //    {

    //        if (dsSalesForce.Tables[0].Rows.Count > 0)
    //        {
    //            TableRow tr_header = new TableRow();
    //            tr_header.BorderStyle = BorderStyle.Solid;
    //            tr_header.BorderWidth = 1;
    //            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
    //            tr_header.Style.Add("Color", "White");
    //            tr_header.BorderColor = System.Drawing.Color.Black;

    //            TableCell tc_SNo = new TableCell();
    //            tc_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_SNo.BorderWidth = 1;
    //            tc_SNo.Width = 50;
    //            tc_SNo.RowSpan = 3;
    //            Literal lit_SNo =
    //                new Literal();
    //            lit_SNo.Text = "#";
    //            tc_SNo.BorderColor = System.Drawing.Color.Black;
    //            tc_SNo.Controls.Add(lit_SNo);
    //            tc_SNo.Attributes.Add("Class", "rptCellBorder");
    //            tr_header.Cells.Add(tc_SNo);

    //            TableCell tc_DR_Code = new TableCell();
    //            tc_DR_Code.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Code.BorderWidth = 1;
    //            tc_DR_Code.Width = 400;
    //            tc_DR_Code.RowSpan = 3;
    //            Literal lit_DR_Code = new Literal();
    //            lit_DR_Code.Text = "<center>Product Code</center>";
    //            tc_DR_Code.Controls.Add(lit_DR_Code);
    //            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Code.Visible = false;
    //            tr_header.Cells.Add(tc_DR_Code);

    //            TableCell tc_DR_Name = new TableCell();
    //            tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Name.BorderWidth = 1;
    //            tc_DR_Name.Width = 200;
    //            tc_DR_Name.RowSpan = 3;
    //            Literal lit_DR_Name = new Literal();
    //            lit_DR_Name.Text = "<center>Product Name / Month</center>";
    //            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Name.Controls.Add(lit_DR_Name);
    //            tr_header.Cells.Add(tc_DR_Name);




    //            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //            int cmonth = Convert.ToInt32(FMonth);
    //            int cyear = Convert.ToInt32(FYear);


    //            ViewState["months"] = months;
    //            ViewState["cmonth"] = cmonth;
    //            ViewState["cyear"] = cyear;

    //            Doctor dr = new Doctor();
    //            dsDoctor = dr.getDocSpec_ForExpo(strspec);


    //            if (months >= 0)
    //            {
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_month = new TableCell();
    //                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
    //                    //tc_month.ColumnSpan = 1;
    //                    Literal lit_month = new Literal();
    //                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
    //                    tc_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_month.BorderStyle = BorderStyle.Solid;
    //                    tc_month.BorderWidth = 1;
    //                    tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                    //tc_month.Width = 200;
    //                    tc_month.Controls.Add(lit_month);
    //                    tr_header.Cells.Add(tc_month);
    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //            }
    //            tbl.Rows.Add(tr_header);

    //            //Sub Header
    //            months = Convert.ToInt16(ViewState["months"].ToString());
    //            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            if (months >= 0)
    //            {
    //                TableRow tr_lst_det = new TableRow();
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_lst_month = new TableCell();
    //                    HyperLink lit_lst_month = new HyperLink();
    //                    lit_lst_month.Text = "No. of Drs";
    //                    tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_lst_month.BorderWidth = 1;
    //                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //                    tc_lst_month.Controls.Add(lit_lst_month);
    //                    tr_lst_det.Cells.Add(tc_lst_month);


    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_lst_det.Style.Add("Color", "White");

    //                tr_lst_det.Attributes.Add("Class", "Backcolor");

    //                tbl.Rows.Add(tr_lst_det);
    //            }

    //            if (months >= 0)
    //            {
    //                TableRow tr_catg = new TableRow();
    //                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_catg.Style.Add("Color", "White");
    //                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //                for (int j = 1; j <= (months + 1) * 1; j++)
    //                {
    //                    if (dsDoctor.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {
    //                            TableCell tc_catg_name = new TableCell();
    //                            tc_catg_name.BorderStyle = BorderStyle.Solid;
    //                            tc_catg_name.BorderWidth = 1;
    //                            if ((j % 2) == 1)
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
    //                            }
    //                            else
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
    //                            }
    //                            // tc_catg_name.Width = 30;

    //                            Literal lit_catg_name = new Literal();
    //                            lit_catg_name.Text = dataRow["Doc_Special_SName"].ToString();
    //                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
    //                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_catg_name.Controls.Add(lit_catg_name);
    //                            tr_catg.Cells.Add(tc_catg_name);
    //                        }

    //                        tbl.Rows.Add(tr_catg);
    //                    }
    //                }
    //            }

    //            if (dsSalesForce.Tables[0].Rows.Count > 0)
    //                ViewState["dsSalesForce"] = dsSalesForce;


    //            int iCount = 0;
    //            int iTotLstCount = 0;
    //            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
    //            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            {
    //                TableRow tr_det = new TableRow();
    //                iCount += 1;

    //                //S.No
    //                TableCell tc_det_SNo = new TableCell();
    //                Literal lit_det_SNo = new Literal();
    //                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //                tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //                tc_det_SNo.BorderWidth = 1;
    //                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_SNo.Controls.Add(lit_det_SNo);
    //                tr_det.Cells.Add(tc_det_SNo);
    //                tr_det.BackColor = System.Drawing.Color.White;

    //                //Product Code
    //                TableCell tc_det_usr = new TableCell();
    //                Literal lit_det_usr = new Literal();
    //                lit_det_usr.Text = "&nbsp;" + drFF["Product_Code_SlNo"].ToString();
    //                tc_det_usr.BorderStyle = BorderStyle.Solid;
    //                tc_det_usr.BorderWidth = 1;
    //                tc_det_usr.Visible = false;
    //                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_usr.Controls.Add(lit_det_usr);
    //                tr_det.Cells.Add(tc_det_usr);


    //                //Product Name
    //                TableCell tc_det_FF = new TableCell();
    //                Literal lit_det_FF = new Literal();
    //                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
    //                tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                tc_det_FF.BorderWidth = 1;
    //                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_FF.Controls.Add(lit_det_FF);
    //                tr_det.Cells.Add(tc_det_FF);

    //                months = Convert.ToInt16(ViewState["months"].ToString());
    //                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //                if (months >= 0)
    //                {
    //                    for (int j = 1; j <= months + 1; j++)
    //                    {
    //                        if (cmonth == 12)
    //                        {
    //                            sCurrentDate = "01-01-" + (cyear + 1);
    //                        }
    //                        else
    //                        {
    //                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //                        }

    //                        if (dsDoctor.Tables[0].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                            {

    //                                dtCurrent = Convert.ToDateTime(sCurrentDate);

    //                                dsDoc = sf.getProduct_Exp_Speciality(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate, Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()));

    //                                if (dsDoc.Tables[0].Rows.Count > 0)
    //                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


    //                                TableCell tc_lst_month = new TableCell();
    //                                HyperLink hyp_lst_month = new HyperLink();

    //                                if (tot_dr != "0")
    //                                {
    //                                    hyp_lst_month.Text = tot_dr;

    //                                    //sURL = "rptProduct_Exp_Detail1.aspx?sf_Name=" + "&sf_code=" + sfCode + "&sf_name=" + sfname + "&Year=" + cyear + "&Month=" + cmonth + "&Prod=" + drFF["Product_Code_SlNo"] + "&Prod_Name=" + drFF["Product_Detail_Name"] + "&sCurrentDate=" + sCurrentDate +  "";

    //                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()) + "','" + Type + "')");

    //                                    //hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=700,height=600,left=0,top=0');";
    //                                    hyp_lst_month.NavigateUrl = "#";


    //                                }

    //                                else
    //                                {
    //                                    hyp_lst_month.Text = "";
    //                                }


    //                                tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                                tc_lst_month.BorderWidth = 1;
    //                                tc_lst_month.BackColor = System.Drawing.Color.White;
    //                                tc_lst_month.Width = 200;
    //                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
    //                                tc_lst_month.Controls.Add(hyp_lst_month);
    //                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                                tr_det.Cells.Add(tc_lst_month);
    //                            }
    //                        }

    //                        iTotLstCount += Convert.ToInt16(tot_dr);


    //                        cmonth = cmonth + 1;
    //                        if (cmonth == 13)
    //                        {
    //                            cmonth = 1;
    //                            cyear = cyear + 1;
    //                        }

    //                    }
    //                    //

    //                }

    //                tbl.Rows.Add(tr_det);

    //            }
    //            //TableRow tr_total = new TableRow();

    //            //TableCell tc_Count_Total = new TableCell();
    //            //tc_Count_Total.BorderStyle = BorderStyle.Solid;
    //            //tc_Count_Total.BorderWidth = 1;
    //            ////tc_catg_Total.Width = 25;
    //            //Literal lit_Count_Total = new Literal();
    //            //lit_Count_Total.Text = "<center>Total</center>";
    //            //tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
    //            //tc_Count_Total.Controls.Add(lit_Count_Total);
    //            //tc_Count_Total.Font.Bold.ToString();
    //            //tc_Count_Total.BackColor = System.Drawing.Color.White;
    //            //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
    //            //tc_Count_Total.ColumnSpan = 2;
    //            //tc_Count_Total.Style.Add("text-align", "left");
    //            //tc_Count_Total.Style.Add("font-family", "Calibri");
    //            //tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
    //            //tc_Count_Total.Style.Add("font-size", "10pt");

    //            //tr_total.Cells.Add(tc_Count_Total);


    //            //Session["Test"] = "";

    //            //months = Convert.ToInt16(ViewState["months"].ToString());
    //            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            //if (months >= 0)
    //            //{
    //            //    Session["Test"] = "T";
    //            //    for (int j = 1; j <= months + 1; j++)
    //            //    {
    //            //        if (cmonth == 12)
    //            //        {
    //            //            sCurrentDate = "01-01-" + (cyear + 1);
    //            //        }
    //            //        else
    //            //        {
    //            //            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //            //        }

    //            //        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //            //        TableCell tc_tot_month = new TableCell();
    //            //        HyperLink hyp_month = new HyperLink();
    //            //        iTotLstCount = 0;
    //            //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            //        {
    //            //            if (dsDoctor.Tables[0].Rows.Count > 0)
    //            //            {
    //            //                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //            //                {

    //            //                    dsDoc = sf.getProduct_Exp_Speciality(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate, Convert.ToInt32(dataRow["Doc_Special_Code"].ToString()));


    //            //                    if (dsDoc.Tables[0].Rows.Count > 0)
    //            //                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //                    if (tot_dr != "0")
    //            //                    {

    //            //                        iTotLstCount += Convert.ToInt16(tot_dr);
    //            //                        hyp_month.Text = iTotLstCount.ToString();

    //            //                    }
    //            //                }
    //            //            }
    //            //        }

    //            //        tc_tot_month.BorderStyle = BorderStyle.Solid;
    //            //        tc_tot_month.BorderWidth = 1;
    //            //        tc_tot_month.BackColor = System.Drawing.Color.White;
    //            //        tc_tot_month.Width = 200;
    //            //        tc_tot_month.Style.Add("font-family", "Calibri");
    //            //        tc_tot_month.Style.Add("font-size", "10pt");
    //            //        tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
    //            //        tc_tot_month.VerticalAlign = VerticalAlign.Middle;
    //            //        tc_tot_month.Controls.Add(hyp_month);
    //            //        tc_tot_month.Attributes.Add("style", "font-weight:bold;");
    //            //        tc_tot_month.Attributes.Add("Class", "rptCellBorder");
    //            //        tr_total.Cells.Add(tc_tot_month);
    //            //        Session["Test"] = "G";
    //            //        cmonth = cmonth + 1;
    //            //        if (cmonth == 13)
    //            //        {
    //            //            cmonth = 1;
    //            //            cyear = cyear + 1;
    //            //        }

    //            //    }
    //            //}

    //            //tbl.Rows.Add(tr_total);
    //        }
    //    }

    //    else if (Type == "1")
    //    {
    //         if (dsSalesForce.Tables[0].Rows.Count > 0)

    //        {
    //            TableRow tr_header = new TableRow();
    //            tr_header.BorderStyle = BorderStyle.Solid;
    //            tr_header.BorderWidth = 1;
    //            tr_header.BackColor = System.Drawing.Color.FromName("#0097AC");
    //            tr_header.Style.Add("Color", "White");
    //            tr_header.BorderColor = System.Drawing.Color.Black;

    //            TableCell tc_SNo = new TableCell();
    //            tc_SNo.BorderStyle = BorderStyle.Solid;
    //            tc_SNo.BorderWidth = 1;
    //            tc_SNo.Width = 50;
    //            tc_SNo.RowSpan = 3;
    //            Literal lit_SNo =
    //                new Literal();
    //            lit_SNo.Text = "#";
    //            tc_SNo.BorderColor = System.Drawing.Color.Black;
    //            tc_SNo.Controls.Add(lit_SNo);
    //            tc_SNo.Attributes.Add("Class", "rptCellBorder");
    //            tr_header.Cells.Add(tc_SNo);

    //            TableCell tc_DR_Code = new TableCell();
    //            tc_DR_Code.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Code.BorderWidth = 1;
    //            tc_DR_Code.Width = 400;
    //            tc_DR_Code.RowSpan = 3;
    //            Literal lit_DR_Code = new Literal();
    //            lit_DR_Code.Text = "<center>Product Code</center>";
    //            tc_DR_Code.Controls.Add(lit_DR_Code);
    //            tc_DR_Code.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Code.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Code.Visible = false;
    //            tr_header.Cells.Add(tc_DR_Code);

    //            TableCell tc_DR_Name = new TableCell();
    //            tc_DR_Name.BorderStyle = BorderStyle.Solid;
    //            tc_DR_Name.BorderWidth = 1;
    //            tc_DR_Name.Width = 200;
    //            tc_DR_Name.RowSpan = 3;
    //            Literal lit_DR_Name = new Literal();
    //            lit_DR_Name.Text = "<center>Product Name / Month</center>";
    //            tc_DR_Name.BorderColor = System.Drawing.Color.Black;
    //            tc_DR_Name.Attributes.Add("Class", "rptCellBorder");
    //            tc_DR_Name.Controls.Add(lit_DR_Name);
    //            tr_header.Cells.Add(tc_DR_Name);




    //            int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //            int cmonth = Convert.ToInt32(FMonth);
    //            int cyear = Convert.ToInt32(FYear);


    //            ViewState["months"] = months;
    //            ViewState["cmonth"] = cmonth;
    //            ViewState["cyear"] = cyear;

    //            Doctor dr = new Doctor();
    //            dsDoctor = dr.getDocCat_ForExpo(strcat);


    //            if (months >= 0)
    //            {
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_month = new TableCell();
    //                    tc_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count * 1;
    //                    //tc_month.ColumnSpan = 1;
    //                    Literal lit_month = new Literal();
    //                    Monthsub = sf.getMonthName(cmonth.ToString()) + "-" + cyear;
    //                    lit_month.Text = Monthsub.Substring(0, 3) + "-" + cyear;
    //                    tc_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_month.BorderStyle = BorderStyle.Solid;
    //                    tc_month.BorderWidth = 1;
    //                    tc_month.HorizontalAlign = HorizontalAlign.Center;
    //                    //tc_month.Width = 200;
    //                    tc_month.Controls.Add(lit_month);
    //                    tr_header.Cells.Add(tc_month);
    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //            }
    //            tbl.Rows.Add(tr_header);

    //            //Sub Header
    //            months = Convert.ToInt16(ViewState["months"].ToString());
    //            cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            if (months >= 0)
    //            {
    //                TableRow tr_lst_det = new TableRow();
    //                for (int j = 1; j <= months + 1; j++)
    //                {
    //                    TableCell tc_lst_month = new TableCell();
    //                    HyperLink lit_lst_month = new HyperLink();
    //                    lit_lst_month.Text = "No. of Drs";
    //                    tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                    tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                    tc_lst_month.BorderWidth = 1;
    //                    tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                    tc_lst_month.ColumnSpan = dsDoctor.Tables[0].Rows.Count;
    //                    tc_lst_month.Controls.Add(lit_lst_month);
    //                    tr_lst_det.Cells.Add(tc_lst_month);


    //                    cmonth = cmonth + 1;
    //                    if (cmonth == 13)
    //                    {
    //                        cmonth = 1;
    //                        cyear = cyear + 1;
    //                    }
    //                }
    //                tr_lst_det.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_lst_det.Style.Add("Color", "White");

    //                tr_lst_det.Attributes.Add("Class", "Backcolor");

    //                tbl.Rows.Add(tr_lst_det);
    //            }

    //            if (months >= 0)
    //            {
    //                TableRow tr_catg = new TableRow();
    //                tr_catg.BackColor = System.Drawing.Color.FromName("#0097AC");
    //                tr_catg.Style.Add("Color", "White");
    //                //  tr_catg.BackColor = System.Drawing.Color.FromName("#A6A6D2");

    //                for (int j = 1; j <= (months + 1) * 1; j++)
    //                {
    //                    if (dsDoctor.Tables[0].Rows.Count > 0)
    //                    {
    //                        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                        {
    //                            TableCell tc_catg_name = new TableCell();
    //                            tc_catg_name.BorderStyle = BorderStyle.Solid;
    //                            tc_catg_name.BorderWidth = 1;
    //                            if ((j % 2) == 1)
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.LavenderBlush;
    //                            }
    //                            else
    //                            {
    //                                //tc_catg_name.BackColor = System.Drawing.Color.PapayaWhip;
    //                            }
    //                            // tc_catg_name.Width = 30;

    //                            Literal lit_catg_name = new Literal();
    //                            lit_catg_name.Text = dataRow["Doc_Cat_SName"].ToString();
    //                            tc_catg_name.Attributes.Add("Class", "rptCellBorder");
    //                            tc_catg_name.HorizontalAlign = HorizontalAlign.Center;
    //                            tc_catg_name.Controls.Add(lit_catg_name);
    //                            tr_catg.Cells.Add(tc_catg_name);
    //                        }

    //                        tbl.Rows.Add(tr_catg);
    //                    }
    //                }
    //            }

    //            if (dsSalesForce.Tables[0].Rows.Count > 0)
    //                ViewState["dsSalesForce"] = dsSalesForce;


    //            int iCount = 0;
    //            int iTotLstCount = 0;
    //            dsSalesForce = (DataSet)ViewState["dsSalesForce"];
    //            foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            {
    //                TableRow tr_det = new TableRow();
    //                iCount += 1;

    //                //S.No
    //                TableCell tc_det_SNo = new TableCell();
    //                Literal lit_det_SNo = new Literal();
    //                lit_det_SNo.Text = "<center>" + iCount.ToString() + "</center>";
    //                tc_det_SNo.BorderStyle = BorderStyle.Solid;
    //                tc_det_SNo.BorderWidth = 1;
    //                tc_det_SNo.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_SNo.Controls.Add(lit_det_SNo);
    //                tr_det.Cells.Add(tc_det_SNo);
    //                tr_det.BackColor = System.Drawing.Color.White;

    //                //Product Code
    //                TableCell tc_det_usr = new TableCell();
    //                Literal lit_det_usr = new Literal();
    //                lit_det_usr.Text = "&nbsp;" + drFF["Product_Code_SlNo"].ToString();
    //                tc_det_usr.BorderStyle = BorderStyle.Solid;
    //                tc_det_usr.BorderWidth = 1;
    //                tc_det_usr.Visible = false;
    //                tc_det_usr.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_usr.Controls.Add(lit_det_usr);
    //                tr_det.Cells.Add(tc_det_usr);


    //                //Product Name
    //                TableCell tc_det_FF = new TableCell();
    //                Literal lit_det_FF = new Literal();
    //                lit_det_FF.Text = "&nbsp;" + drFF["Product_Detail_Name"].ToString();
    //                tc_det_FF.BorderStyle = BorderStyle.Solid;
    //                tc_det_FF.BorderWidth = 1;
    //                tc_det_FF.Attributes.Add("Class", "rptCellBorder");
    //                tc_det_FF.Controls.Add(lit_det_FF);
    //                tr_det.Cells.Add(tc_det_FF);

    //                months = Convert.ToInt16(ViewState["months"].ToString());
    //                cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //                cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //                if (months >= 0)
    //                {
    //                    for (int j = 1; j <= months + 1; j++)
    //                    {
    //                        if (cmonth == 12)
    //                        {
    //                            sCurrentDate = "01-01-" + (cyear + 1);
    //                        }
    //                        else
    //                        {
    //                            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //                        }

    //                        if (dsDoctor.Tables[0].Rows.Count > 0)
    //                        {
    //                            foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //                            {

    //                                dtCurrent = Convert.ToDateTime(sCurrentDate);

    //                                dsDoc = sf.getProduct_Exp_Category(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));

    //                                if (dsDoc.Tables[0].Rows.Count > 0)
    //                                    tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();


    //                                TableCell tc_lst_month = new TableCell();
    //                                HyperLink hyp_lst_month = new HyperLink();

    //                                if (tot_dr != "0")
    //                                {
    //                                    hyp_lst_month.Text = tot_dr;

    //                                    //sURL = "rptProduct_Exp_Detail1.aspx?sf_Name=" + "&sf_code=" + sfCode + "&sf_name=" + sfname + "&Year=" + cyear + "&Month=" + cmonth + "&Prod=" + drFF["Product_Code_SlNo"] + "&Prod_Name=" + drFF["Product_Detail_Name"] + "&sCurrentDate=" + sCurrentDate +  "";

    //                                    hyp_lst_month.Attributes.Add("href", "javascript:showModalPopUp('" + sfCode.ToString() + "', '" + sfname + "', '" + cyear + "', '" + cmonth + "', '" + drFF["Product_Detail_Name"] + "', '" + drFF["Product_Code_SlNo"] + "','" + sCurrentDate + "','" + Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()) + "','" + Type + "')");

    //                                    //hyp_lst_month.Attributes["onclick"] = "javascript:window.open('" + sURL + "','_blank','PopUp',0,'resizable=yes,toolbar=no,menubar=no,status=no,width=700,height=600,left=0,top=0');";
    //                                    hyp_lst_month.NavigateUrl = "#";


    //                                }

    //                                else
    //                                {
    //                                    hyp_lst_month.Text = "";
    //                                }


    //                                tc_lst_month.BorderStyle = BorderStyle.Solid;
    //                                tc_lst_month.BorderWidth = 1;
    //                                tc_lst_month.BackColor = System.Drawing.Color.White;
    //                                tc_lst_month.Width = 200;
    //                                tc_lst_month.HorizontalAlign = HorizontalAlign.Center;
    //                                tc_lst_month.VerticalAlign = VerticalAlign.Middle;
    //                                tc_lst_month.Controls.Add(hyp_lst_month);
    //                                tc_lst_month.Attributes.Add("Class", "rptCellBorder");
    //                                tr_det.Cells.Add(tc_lst_month);
    //                            }
    //                        }

    //                        iTotLstCount += Convert.ToInt16(tot_dr);


    //                        cmonth = cmonth + 1;
    //                        if (cmonth == 13)
    //                        {
    //                            cmonth = 1;
    //                            cyear = cyear + 1;
    //                        }

    //                    }
    //                    //

    //                }

    //                tbl.Rows.Add(tr_det);

    //            }

    //            //TableRow tr_total = new TableRow();

    //            //TableCell tc_Count_Total = new TableCell();
    //            //tc_Count_Total.BorderStyle = BorderStyle.Solid;
    //            //tc_Count_Total.BorderWidth = 1;
    //            ////tc_catg_Total.Width = 25;
    //            //Literal lit_Count_Total = new Literal();
    //            //lit_Count_Total.Text = "<center>Total</center>";
    //            //tc_Count_Total.Attributes.Add("style", "color:Red;font-weight:bold;");
    //            //tc_Count_Total.Controls.Add(lit_Count_Total);
    //            //tc_Count_Total.Font.Bold.ToString();
    //            //tc_Count_Total.BackColor = System.Drawing.Color.White;
    //            //tc_Count_Total.Attributes.Add("Class", "tbldetail_main");
    //            //tc_Count_Total.ColumnSpan = 2;
    //            //tc_Count_Total.Style.Add("text-align", "left");
    //            //tc_Count_Total.Style.Add("font-family", "Calibri");
    //            //tc_Count_Total.Attributes.Add("Class", "rptCellBorder");
    //            //tc_Count_Total.Style.Add("font-size", "10pt");

    //            //tr_total.Cells.Add(tc_Count_Total);


    //            //Session["Test"] = "";

    //            //months = Convert.ToInt16(ViewState["months"].ToString());
    //            //cmonth = Convert.ToInt16(ViewState["cmonth"].ToString());
    //            //cyear = Convert.ToInt16(ViewState["cyear"].ToString());

    //            //if (months >= 0)
    //            //{
    //            //    Session["Test"] = "T";
    //            //    for (int j = 1; j <= months + 1; j++)
    //            //    {
    //            //        if (cmonth == 12)
    //            //        {
    //            //            sCurrentDate = "01-01-" + (cyear + 1);
    //            //        }
    //            //        else
    //            //        {
    //            //            sCurrentDate = (cmonth + 1) + "-01-" + cyear;
    //            //        }

    //            //        dtCurrent = Convert.ToDateTime(sCurrentDate);

    //            //        TableCell tc_tot_month = new TableCell();
    //            //        HyperLink hyp_month = new HyperLink();
    //            //        iTotLstCount = 0;
    //            //        foreach (DataRow drFF in dsSalesForce.Tables[0].Rows)
    //            //        {
    //            //            if (dsDoctor.Tables[0].Rows.Count > 0)
    //            //            {
    //            //                foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
    //            //                {

    //            //                    dsDoc = sf.getProduct_Exp_Category(sfCode, divcode, cmonth, cyear, Convert.ToInt16(drFF["Product_Code_SlNo"].ToString()), sCurrentDate, Convert.ToInt32(dataRow["Doc_Cat_Code"].ToString()));


    //            //                    if (dsDoc.Tables[0].Rows.Count > 0)
    //            //                        tot_dr = dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

    //            //                    if (tot_dr != "0")
    //            //                    {

    //            //                        iTotLstCount += Convert.ToInt16(tot_dr);
    //            //                        hyp_month.Text = iTotLstCount.ToString();

    //            //                    }
    //            //                }
    //            //            }
    //            //        }

    //            //        tc_tot_month.BorderStyle = BorderStyle.Solid;
    //            //        tc_tot_month.BorderWidth = 1;
    //            //        tc_tot_month.BackColor = System.Drawing.Color.White;
    //            //        tc_tot_month.Width = 200;
    //            //        tc_tot_month.Style.Add("font-family", "Calibri");
    //            //        tc_tot_month.Style.Add("font-size", "10pt");
    //            //        tc_tot_month.HorizontalAlign = HorizontalAlign.Center;
    //            //        tc_tot_month.VerticalAlign = VerticalAlign.Middle;
    //            //        tc_tot_month.Controls.Add(hyp_month);
    //            //        tc_tot_month.Attributes.Add("style", "font-weight:bold;");
    //            //        tc_tot_month.Attributes.Add("Class", "rptCellBorder");
    //            //        tr_total.Cells.Add(tc_tot_month);
    //            //        Session["Test"] = "G";
    //            //        cmonth = cmonth + 1;
    //            //        if (cmonth == 13)
    //            //        {
    //            //            cmonth = 1;
    //            //            cyear = cyear + 1;
    //            //        }

    //            //    }
    //            //}

    //            //tbl.Rows.Add(tr_total);
    //        }

    //    }

    //}
}
