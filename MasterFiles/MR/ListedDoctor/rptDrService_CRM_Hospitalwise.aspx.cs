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

public partial class MasterFiles_MR_ListedDoctor_rptDrService_CRM_Hospitalwise : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string Stok_code = string.Empty;
    string StName = string.Empty;
    int Sec_Cnt = 0;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;

    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsSecSales = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();

    DataSet dsDrHead = new System.Data.DataSet();
    DataTable dtDr = new System.Data.DataTable();

    string Hospitalval = "";
    string TypeofMode = "";
    string PrdMode = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["FYear"].ToString();
            TMonth = Request.QueryString["TMonth"].ToString();
            TYear = Request.QueryString["TYear"].ToString();
            strFieledForceName = Request.QueryString["Sf_Name"].ToString();
            Hospitalval = Request.QueryString["TypeVal"].ToString();
            TypeofMode = Request.QueryString["Mode_Type"].ToString();
            PrdMode = Request.QueryString["ProductType"].ToString();

            //Stok_code = Request.QueryString["Stok_code"].Trim().ToString();
            // StName = Request.QueryString["sk_Name"].Trim().ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
           
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            // lblstock.Text = "Stockist Name : " + "<span style='font-weight: bold;color:Red;'> " + StName + "</span>";

            if (TypeofMode == "1")
            {
                FillReport();
                lblHead.Text = " Service Report Hospitalwise " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            }
            else if (TypeofMode == "3")
            {
                Fill_Doctor();
                lblHead.Text = " Service Report Doctorwise " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            }
            else if (TypeofMode == "2")
            {
                Fill_Product();
                lblHead.Text = " Service Report Productwise " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            }
            else if (TypeofMode == "4")
            {
                Fill_Chemists();
                lblHead.Text = " Service Report Chemistswise " + strFrmMonth + " " + FYear + " to " + " " + strToMonth + " " + " " + TYear;
            }
        }
    }

    private void FillReport()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //  DataTable dtDr = new DataTable();
        dtDr.Columns.Add("S_Id", typeof(int));
        dtDr.Columns.Add("Sec_Sale_Name", typeof(string));

        dtDr.Rows.Add(1, "No.Of Hospital");
        dtDr.Rows.Add(2, "Service Amount Spent");     

        //dtDr.Fill(dsDrHead);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        if (Stok_code.Trim() == "-2")
        {
            sProc_Name = "SP_Get_Dr_Service_Hospitalwise_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Hospital_Code", Hospitalval);
            cmd.CommandTimeout = 600;          
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();
         
            dsts.Tables[1].Columns.Remove("Sl_No");
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Stockist_code1");
           
            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();
        }
        else
        {
            sProc_Name = "SP_Get_Dr_Service_Hospitalwise_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Hospital_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[1].Copy();
            
            dsts.Tables[1].Columns.Remove("sf_code");
            dsts.Tables[1].Columns.Remove("S_Code");
           
            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();

        }

    }


    private void Fill_Doctor()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //  DataTable dtDr = new DataTable();
        dtDr.Columns.Add("S_Id", typeof(int));
        dtDr.Columns.Add("Sec_Sale_Name", typeof(string));

        dtDr.Rows.Add(1, "No.Of Doctors");
        dtDr.Rows.Add(2, "Service Amount Spent");

        //dtDr.Fill(dsDrHead);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        if (Stok_code.Trim() == "-2")
        {
            

            sProc_Name = "SP_Get_Dr_Service_Doctorwise_rpt_All";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Doctor_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();

            dsts.Tables[1].Columns.Remove("Sl_No");
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Stockist_code1");

            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();
        }
        else
        {
            sProc_Name = "SP_Get_Dr_Service_Doctorwise_rpt_All";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Doctor_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);

            if (dsts.Tables[0].Rows.Count > 0)
            {

                dtrowClr = dsts.Tables[1].Copy();

                dsts.Tables[1].Columns.Remove("sf_code");
                dsts.Tables[1].Columns.Remove("S_Code");

                GrdFixation.DataSource = dsts.Tables[1];
                GrdFixation.DataBind();
            }

        }

        if (GrdFixation.Rows.Count > 0)
        {
            for (int i = 0; i < GrdFixation.Rows.Count; i++)
            {
                GrdFixation.Rows[i].Cells[0].Text = (i + 1).ToString();

                if (i == GrdFixation.Rows.Count - 1)
                {
                    GrdFixation.Rows[i].Cells[0].Text = "";
                }
            }
        }

    }

    private void Fill_Product()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //  DataTable dtDr = new DataTable();
        dtDr.Columns.Add("S_Id", typeof(int));
        dtDr.Columns.Add("Sec_Sale_Name", typeof(string));

        dtDr.Rows.Add(1, "No.Of Doctors");
        dtDr.Rows.Add(2, "No.Of Products");
        dtDr.Rows.Add(3, "Service Amount Spent");

        //dtDr.Fill(dsDrHead);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        if (Stok_code.Trim() == "-2")
        {

            if (PrdMode == "2")
            {
                sProc_Name = "SP_Get_Dr_Service_Productwise_rpt_Chemistswise";
            }
            else
            {
                

                sProc_Name = "SP_GetDrService_Productwise_rptDoctorwise_CRM";
            }
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Prd_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();

            //dsts.Tables[1].Columns.Remove("INX_1");
            dsts.Tables[1].Columns.Remove("sf_code");
            dsts.Tables[1].Columns.Remove("S_Code");
            dsts.Tables[1].Columns.Remove("State_Code");
            dsts.Tables[1].Columns.Remove("subdivision_code");

            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();
        }
        else
        {
           

            if (PrdMode == "2")
            {
                sProc_Name = "SP_Get_Dr_Service_Productwise_rpt_Chemistswise";
            }
            else
            {
                sProc_Name = "SP_GetDrService_Productwise_rptDoctorwise_CRM";
            }

            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Prd_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);

            if (dsts.Tables[0].Rows.Count > 0)
            {
                // dtrowClr = dsts.Tables[1].Copy();

                dsts.Tables[1].Columns.Remove("INX1");
                dtrowClr = dsts.Tables[1].Copy();
                dsts.Tables[1].Columns.Remove("sf_code");
                dsts.Tables[1].Columns.Remove("S_Code");
                dsts.Tables[1].Columns.Remove("State_Code");
                dsts.Tables[1].Columns.Remove("subdivision_code");

                GrdFixation.DataSource = dsts.Tables[1];
                GrdFixation.DataBind();
            }

        }

        if (GrdFixation.Rows.Count > 0)
        {
            for (int i = 0; i < GrdFixation.Rows.Count; i++)
            {
                GrdFixation.Rows[i].Cells[0].Text = (i + 1).ToString();

                if (i == GrdFixation.Rows.Count - 1)
                {
                    GrdFixation.Rows[i].Cells[0].Text = "";
                }
            }
        }
    }

    private void Fill_Chemists()
    {
        int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //  DataTable dtDr = new DataTable();
        dtDr.Columns.Add("S_Id", typeof(int));
        dtDr.Columns.Add("Sec_Sale_Name", typeof(string));

        dtDr.Rows.Add(1, "No.Of Chemists");
        dtDr.Rows.Add(2, "Service Amount Spent");

        //dtDr.Fill(dsDrHead);

        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
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

        if (Stok_code.Trim() == "-2")
        {
            sProc_Name = "SP_Get_Dr_Service_Chemistwise_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Chemist_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[0].Copy();

            dsts.Tables[1].Columns.Remove("Sl_No");
            dsts.Tables[1].Columns.Remove("Stockist_Code");
            dsts.Tables[1].Columns.Remove("Stockist_code1");

            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();
        }
        else
        {
            sProc_Name = "SP_Get_Dr_Service_Chemistwise_rpt";
            SqlCommand cmd = new SqlCommand(sProc_Name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
            cmd.Parameters.AddWithValue("@Msf_code", sf_code);
            cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
            cmd.Parameters.AddWithValue("@Chemist_Code", Hospitalval);
            cmd.CommandTimeout = 600;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dsts = new DataSet();
            da.Fill(dsts);
            dtrowClr = dsts.Tables[1].Copy();

            dsts.Tables[1].Columns.Remove("sf_code");
            dsts.Tables[1].Columns.Remove("S_Code");

            GrdFixation.DataSource = dsts.Tables[1];
            GrdFixation.DataBind();

        }

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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Employee Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#0097AC", true);

            int months1 = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth1 = Convert.ToInt32(FMonth);
            int cyear1 = Convert.ToInt32(FYear);

            ViewState["months"] = months1;
            ViewState["cmonth"] = cmonth1;
            ViewState["cyear"] = cyear1;
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();

            dsSecSales.Tables.Add(dtDr);

            // dsSecSales = ss.Get_Sec_Sale_Code(div_code);
            Sec_Cnt = (dsSecSales.Tables[0].Rows.Count) * 2;
            if (months1 >= 0)
            {

                for (int j = 1; j <= months1 + 1; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, (dsSecSales.Tables[0].Rows.Count), sf.getMonthName(cmonth1.ToString()).Substring(0, 3) + " - " + cyear1, "#0097AC", true);


                    if (dsSecSales.Tables[0].Rows.Count > 0)
                    {
                        for (int k = 0; k < dsSecSales.Tables[0].Rows.Count; k++)
                        {
                            TableCell objtablecell2 = new TableCell();
                            TableCell objtablecell3 = new TableCell();

                            //if (k == 0)
                            //{

                            //    string str = "<a id=hlDrEdit>" + dsSecSales.Tables[0].Rows[k]["Sec_Sale_Name"].ToString() + "</a>";
                            //}
                            //else
                            //{

                            AddMergedCells(objgridviewrow2, objtablecell2, 1, dsSecSales.Tables[0].Rows[k]["Sec_Sale_Name"].ToString(), "#0097AC", false);
                            // AddMergedCells(objgridviewrow3, objtablecell3, 0, "Qty", "#0097AC", false);
                            // AddMergedCells(objgridviewrow3, objtablecell3, 0, "Value", "#0097AC", false);
                            //}
                        }
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
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
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
            objtablecell.RowSpan = 3;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        // int t = 1;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int qty = 0;
            #region Calculations



            for (int m = 5; m < e.Row.Cells.Count; m++)
            {
                if (e.Row.Cells[m].Text == "0")
                {
                    e.Row.Cells[m].Text = "";
                }
            }

            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                //  e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][5].ToString()));

                //int j = t - 1;

                int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                int cmonth = Convert.ToInt32(FMonth);
                int cyear = Convert.ToInt32(FYear);
                int iMn = 0, iYr = 0;

                int ncnt = 5;
                int MgrC = 6;
                int PCnt = 6;

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

                    string sURL = string.Empty;
                    string str = string.Empty;

                    if (e.Row.Cells[ncnt].Text != "0")
                    {
                        if (TypeofMode == "1")
                        {
                            sURL = "rptService_Hospitalwise_DoctorDetail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "";

                            str = "<a id=hlDrEdit href='#' " +
                            "style='font-weight: bold; " +
                            " text-align: center' onclick=javascript:window.open('" + sURL + "',null,'')>" + e.Row.Cells[ncnt].Text + "</a>";

                        }
                        else if (TypeofMode == "3")
                        {
                            sURL = "rptService_Doctorwise_Detail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "&Dr_Code=" + Hospitalval + "";

                            str = "<a id=hlDrEdit href='#' " +
                            "style=' font-weight: bold;" +
                            " text-align: center' onclick=javascript:window.open('" + sURL + "',null,'')>" + e.Row.Cells[ncnt].Text + "</a>";

                        }
                        else if (TypeofMode == "2")
                        {
                            //if (PrdMode == "1")
                            //{
                            //    sURL = "rptService_Product_wise_Detail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "";
                            //}
                            //else
                            //{
                            //    sURL = "rptService_Product_wise_ChemistsDetail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "";
                            //}

                            //str = "<a id=hlDrEdit href='#' " +
                            //"style='color: DarkBlue; font-weight: bold; font-size:x-small" +
                            //"font-family: Verdana; text-align: center' onclick=javascript:window.open('" + sURL + "',null,'resizable=yes,toolbar=no,menubar=no,scrollbars=1,status=no,width=500,height=600,left=0,top=0')>" + e.Row.Cells[ncnt].Text + "</a>";

                            str = e.Row.Cells[ncnt].Text;
                        }
                        else if (TypeofMode == "4")
                        {
                            sURL = "rptService_Chemist_wise_Detail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "";

                            str = "<a id=hlDrEdit href='#' " +
                            "style=' font-weight: bold; " +
                            "text-align: center' onclick=javascript:window.open('" + sURL + "',null,'')>" + e.Row.Cells[ncnt].Text + "</a>";

                        }

                        e.Row.Cells[ncnt].Text = str;
                    }

                    ncnt += 2;

                    if (TypeofMode == "2")
                    {
                        if (e.Row.Cells[PCnt].Text != "0")
                        {

                            if (TypeofMode == "2")
                            {
                                if (PrdMode == "1")
                                {
                                    sURL = "rptService_Product_Doctorwise_DetailCRM.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "&Prd_Code=" + Hospitalval + "";
                                }
                                else
                                {
                                    sURL = "rptService_Product_wise_ChemistsDetail.aspx?sf_code=" + Convert.ToString(dtrowClr.Rows[j][1].ToString()) + "&Month=" + iMn + "&Year=" + iYr + "&S_Name=" + strFieledForceName.ToString().Replace(" ", "") + "&Prd_Code=" + Hospitalval + "";
                                }

                                str = "<a id=hlDrEdit href='#' " +
                                "style=' font-weight: bold; " +
                                "text-align: center' onclick=javascript:window.open('" + sURL + "',null,'')>" + e.Row.Cells[PCnt].Text + "</a>";

                            }

                            e.Row.Cells[PCnt].Text = str;
                        }
                    }

                    PCnt += 3;


                    months--; cmonth++;

                }
                // t += 1;

            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion

        }


    }
    
}