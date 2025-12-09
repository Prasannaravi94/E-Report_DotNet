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

public partial class MIS_Reports_rptSamplePrd_Rxqty : System.Web.UI.Page
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
    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    List<int> iInput = new List<int>();
    List<string> iInp = new List<string>();
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
    DataSet dsprd = new DataSet();
    DataTable dtrowClr = null;
    string MultiProd_Code = string.Empty;
    string Multi_Prod = string.Empty;
    string tot = string.Empty;
    int total;
    string sCurrentDate = string.Empty;
    string sample_code = string.Empty;
    string sample_name = string.Empty;
    List<string> Spec = new List<string>();
    DataSet dsDoc = null;
    List<string> Spe1 = new List<string>();
    string type = string.Empty;
    string excel = string.Empty;
    DataTable dt = new DataTable();

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
        sample_code = Request.QueryString["sample_code"].ToString();
        // input = Session["input"].ToString();
        sample_name = Request.QueryString["sample_name"].ToString();
        type = Request.QueryString["Type"].ToString();
        excel = Request.QueryString["excel"];




        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        string strFMonthName = mfi.GetMonthName(Convert.ToInt16(FMonth)).ToString().Substring(0, 3);
        string strTMonthName = mfi.GetMonthName(Convert.ToInt16(TMonth)).ToString().Substring(0, 3);

        lblHead.Text = "Sample Product Rx Quantity of " + strFMonthName + " " + FYear + " " + "To" + " " + strTMonthName + " " + TYear;


        lblIDMonth.Visible = false;
        lblIDYear.Visible = false;
        //FillSF(); 

        if (excel == "1")
        {
            BindGrid_excel();
        }
        else
        {
            BindGrid();
        }
    }

    private void BindGrid_excel()
    {
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand("SampleRxQty_Total_allPrd", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", divcode);
        cmd.Parameters.AddWithValue("@sf_code", sfCode);
        cmd.Parameters.AddWithValue("@cmonth", FMonth);
        cmd.Parameters.AddWithValue("@cyear", FYear);
        cmd.Parameters.AddWithValue("@prod",sample_code.ToString().TrimEnd(','));
        //cmd.Parameters.AddWithValue("@prod",string.IsNullOrEmpty(sample_code));
       
        cmd.Parameters.AddWithValue("@type", type);

        cmd.CommandTimeout = 8000;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        

        con.Close();
        //
        dt = ds.Tables[0];
        // ExportDataSetToExcel(ds);      
        HttpResponse response = HttpContext.Current.Response;

        // first let's clean up the response.object
        response.Clear();
        response.Charset = "";

        // set the response mime type for excel
        response.ContentType = "application/vnd.ms-excel";
        response.AddHeader("Content-Disposition", "attachment;filename=\"Product_Rx.xls\"");


        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds.Tables[0];
                dg.DataBind();
                dg.HeaderStyle.Font.Bold = true;
                dg.Font.Name = "Verdana";
                dg.Font.Size = 10;
                dg.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                dg.HeaderStyle.BackColor = System.Drawing.Color.Chocolate;
                dg.HeaderStyle.ForeColor = System.Drawing.Color.White;

                dg.RenderControl(htw);

                response.Write(sw.ToString());
                response.End();

            }
        
    }

}

private void BindGrid()
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

        if (FMonth == TMonth && FYear == TYear)
        {


            if (type == "0")
            {

                string inpu = sample_code.Remove(sample_code.Length - 1);
                string[] Spec;
                Spec = inpu.Split(',');

                DataTable dtspec = new DataTable();
                dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
                dtspec.Columns["INX"].AutoIncrementSeed = 1;
                dtspec.Columns["INX"].AutoIncrementStep = 1;
                dtspec.Columns.Add("SAMPLECODE", typeof(int));

                foreach (string sp in Spec)
                {

                    dtspec.Rows.Add(null, sp);

                }

                SqlCommand cmd = new SqlCommand("Sample_Product_DRCOUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Strsample", dtspec);
                cmd.Parameters.AddWithValue("@fromyear", FYear);
                cmd.Parameters.AddWithValue("@toyear", TYear);
                cmd.Parameters.AddWithValue("@from_month", FMonth);
                cmd.Parameters.AddWithValue("@to_month", TMonth);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(5);
                dsts.Tables[0].Columns.RemoveAt(1);
                dsts.Tables[0].Columns["hq"].SetOrdinal(2);
                GrdInput.DataSource = dsts;
                GrdInput.DataBind();
            }
            else if (type == "1")
            {
                string inpu = sample_code.Remove(sample_code.Length - 1);
                string[] Spec;
                Spec = inpu.Split(',');

                DataTable dtspec = new DataTable();
                dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
                dtspec.Columns["INX"].AutoIncrementSeed = 1;
                dtspec.Columns["INX"].AutoIncrementStep = 1;
                dtspec.Columns.Add("SAMPLECODE", typeof(int));

                foreach (string sp in Spec)
                {

                    dtspec.Rows.Add(null, sp);

                }

                SqlCommand cmd = new SqlCommand("Sample_Product_Brand_DRCOUNT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Strsample", dtspec);
                cmd.Parameters.AddWithValue("@fromyear", FYear);
                cmd.Parameters.AddWithValue("@toyear", TYear);
                cmd.Parameters.AddWithValue("@from_month", FMonth);
                cmd.Parameters.AddWithValue("@to_month", TMonth);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(5);
                dsts.Tables[0].Columns.RemoveAt(1);
                dsts.Tables[0].Columns["hq"].SetOrdinal(2);
                GrdInput.DataSource = dsts;
                GrdInput.DataBind();
            }
        }
        else
        {
            if (type == "0")
            {

                string inpu = sample_code.Remove(sample_code.Length - 1);
                string[] Spec;
                Spec = inpu.Split(',');

                DataTable dtspec = new DataTable();
                dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
                dtspec.Columns["INX"].AutoIncrementSeed = 1;
                dtspec.Columns["INX"].AutoIncrementStep = 1;
                dtspec.Columns.Add("SAMPLECODE", typeof(int));

                foreach (string sp in Spec)
                {

                    dtspec.Rows.Add(null, sp);

                }

                SqlCommand cmd = new SqlCommand("Sample_Product_DRCOUNT_Multi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Strsample", dtspec);
                cmd.Parameters.AddWithValue("@fromyear", FYear);
                cmd.Parameters.AddWithValue("@toyear", TYear);
                cmd.Parameters.AddWithValue("@from_month", FMonth);
                cmd.Parameters.AddWithValue("@to_month", TMonth);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(5);
                dsts.Tables[0].Columns.RemoveAt(1);
                dsts.Tables[0].Columns["hq"].SetOrdinal(2);
                GrdInput.DataSource = dsts;
                GrdInput.DataBind();
            }
            else if (type == "1")
            {
                string inpu = sample_code.Remove(sample_code.Length - 1);
                string[] Spec;
                Spec = inpu.Split(',');

                DataTable dtspec = new DataTable();
                dtspec.Columns.Add("INX", typeof(int)).AutoIncrement = true;
                dtspec.Columns["INX"].AutoIncrementSeed = 1;
                dtspec.Columns["INX"].AutoIncrementStep = 1;
                dtspec.Columns.Add("SAMPLECODE", typeof(int));

                foreach (string sp in Spec)
                {

                    dtspec.Rows.Add(null, sp);

                }

                SqlCommand cmd = new SqlCommand("Sample_Product_Brand_DRCOUNT_Multi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(divcode));
                cmd.Parameters.AddWithValue("@Msf_code", sfCode);
                cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                cmd.Parameters.AddWithValue("@Strsample", dtspec);
                cmd.Parameters.AddWithValue("@fromyear", FYear);
                cmd.Parameters.AddWithValue("@toyear", TYear);
                cmd.Parameters.AddWithValue("@from_month", FMonth);
                cmd.Parameters.AddWithValue("@to_month", TMonth);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsts = new DataSet();
                da.Fill(dsts);
                dtrowClr = dsts.Tables[0].Copy();
                dsts.Tables[0].Columns.RemoveAt(6);
                dsts.Tables[0].Columns.RemoveAt(5);
                dsts.Tables[0].Columns.RemoveAt(1);
                dsts.Tables[0].Columns["hq"].SetOrdinal(2);
                GrdInput.DataSource = dsts;
                GrdInput.DataBind();
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
    protected void GrdInput_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (FMonth == TMonth && FYear == TYear)
            {

                if (type == "0")
                {

                    //
                    #region Object
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

                    GridViewRow objgridviewrow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell objtablecell4 = new TableCell();
                    #endregion
                    //
                    #region Merge cells

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                   
                    AddMergedCells(objgridviewrow, objtablecell, 2, 2, "Total No.Of Rxers(No.of Rx Drs)", "#0097AC", true);

                    // AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxers(No.of Rx Drs - Unique)", "#0097AC", true);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Multiple", "#0097AC", true);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Unique", "#0097AC", true);




                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxns(Total Rx Qty)", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FWD", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No.of Drs Met", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Drs Seen", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Drs Call Avg", "#0097AC", true);

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No.of Chem Met", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem Seen", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem Call Avg", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem POB", "#0097AC", true);

                    int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                    int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


                    //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    //int cmonth = Convert.ToInt32(FMonth);
                    //int cyear = Convert.ToInt32(FYear);

                    //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

                    SalesForce sf = new SalesForce();

                    //string spclty = Request.QueryString["cbTxt"].ToString();
                    sample_code = sample_code.Remove(sample_code.LastIndexOf(','));
                    string[] ttlSpc = sample_code.Split(',');

                    foreach (string inp in ttlSpc)
                    {
                        iInp.Add(inp);
                    }


                    string strspec2 = sample_code.Remove(sample_code.Length - 1);
                    Doctor dr = new Doctor();
                    dsDoctor = dr.getRx_code(sample_code);

                    for (int i = 0; i <= months; i++)
                    {
                        int icolspan = ttlSpc.Length;

                        Spec = new List<string>();

                        string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                        AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Seen", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Average", "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, icolspan, "Rx Qty given to Drs", "#0097AC", true);


                        sample_name = sample_name.Remove(sample_name.Length - 1);
                        string[] inpu_name = sample_name.Split(',');




                        for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Product_Code_SlNo"].ToString());

                        }
                        Spec.Sort();
                        foreach (string specc in Spec)
                        {
                            Spe1.Add(specc);
                            Doctor doc = new Doctor();
                            iLstMonth.Add(cmonth);
                            iLstYear.Add(cyear);


                            dsDoc = doc.getRx_code(specc);
                            // AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Detail_Name"].ToString(), "#0097AC", true);

                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Detail_Name"].ToString(), "#0097AC", true);
                        }


                        //Doctor dr = new Doctor();
                        //dsDoctor = dr.getPrd_rx(sample_code);

                        // foreach (string inp_name in inpu_name)
                        // {

                        // //for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        // //{
                        //     iLstMonth.Add(cmonth);
                        //     iLstYear.Add(cyear);


                        //     //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoctor.Tables[0].Rows[k]["Product_Detail_Name"].ToString(), "#0097AC", true);
                        //     AddMergedCells(objgridviewrow3, objtablecell3, 0, 0,inp_name, "#0097AC", true);
                        //// }

                        // }

                        cmonth = cmonth + 1;

                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                    //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total", "#0097AC", true);




                    //Total No.Of Rxers(No.of Rx Drs)
                    //Total No.Of Rxns(Total Rx Qty)


                    //
                    objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                    objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                    objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                    // objGridView.Controls[0].Controls.AddAt(3, objgridviewrow4);
                    //
                    #endregion
                    //
                }

                else if (type == "1")
                {
                    //
                    #region Object
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

                    GridViewRow objgridviewrow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell objtablecell4 = new TableCell();
                    #endregion
                    //
                    #region Merge cells

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                    
                    AddMergedCells(objgridviewrow, objtablecell, 2, 2, "Total No.Of Rxers(No.of Rx Drs)", "#0097AC", true);

                    // AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxers(No.of Rx Drs - Unique)", "#0097AC", true);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Multiple", "#0097AC", true);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Unique", "#0097AC", true);




                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxns(Total Rx Qty)", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FWD", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No.of Drs Met", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Drs Seen", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Drs Call Avg", "#0097AC", true);

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "No.of Chem Met", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem Seen", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem Call Avg", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Chem POB", "#0097AC", true);

                    int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                    int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


                    SalesForce sf = new SalesForce();

                    sample_code = sample_code.Remove(sample_code.LastIndexOf(','));
                    string[] ttlSpc = sample_code.Split(',');

                    foreach (string inp in ttlSpc)
                    {
                        iInp.Add(inp);
                    }


                    string strspec2 = sample_code.Remove(sample_code.Length - 1);
                    Doctor dr = new Doctor();
                    dsDoctor = dr.getRx_Brand_code(sample_code);

                    for (int i = 0; i <= months; i++)
                    {
                        int icolspan = ttlSpc.Length;

                        Spec = new List<string>();

                        string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                        AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Seen", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Average", "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, icolspan, "Rx Qty given to Drs", "#0097AC", true);


                        sample_name = sample_name.Remove(sample_name.Length - 1);
                        string[] inpu_name = sample_name.Split(',');




                        for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Product_Brd_Code"].ToString());

                        }
                        Spec.Sort();
                        foreach (string specc in Spec)
                        {
                            Spe1.Add(specc);
                            Doctor doc = new Doctor();
                            iLstMonth.Add(cmonth);
                            iLstYear.Add(cyear);


                            dsDoc = doc.getRx_Brand_code(specc);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Brd_Name"].ToString(), "#0097AC", true);
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
                    objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                    objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                    // objGridView.Controls[0].Controls.AddAt(3, objgridviewrow4);
                    //
                    #endregion
                    //
                }
            }
            else
            {
                if (type == "0")
                {

                    //
                    #region Object
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

                    GridViewRow objgridviewrow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell objtablecell4 = new TableCell();
                    #endregion
                    //
                    #region Merge cells

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                   
                    AddMergedCells(objgridviewrow, objtablecell, 2, 2, "Total No.Of Rxers(No.of Rx Drs)", "#0097AC", true);

                    // AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxers(No.of Rx Drs - Unique)", "#0097AC", true);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Multiple", "#0097AC", true);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Unique", "#0097AC", true);




                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxns(Total Rx Qty)", "#0097AC", true);


                    int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                    int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


                    //int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    //int cmonth = Convert.ToInt32(FMonth);
                    //int cyear = Convert.ToInt32(FYear);

                    //  int sMode = Convert.ToInt32(Request.QueryString["cMode"].ToString());

                    SalesForce sf = new SalesForce();

                    //string spclty = Request.QueryString["cbTxt"].ToString();
                    sample_code = sample_code.Remove(sample_code.LastIndexOf(','));
                    string[] ttlSpc = sample_code.Split(',');

                    foreach (string inp in ttlSpc)
                    {
                        iInp.Add(inp);
                    }


                    string strspec2 = sample_code.Remove(sample_code.Length - 1);
                    Doctor dr = new Doctor();
                    dsDoctor = dr.getRx_code(sample_code);

                    for (int i = 0; i <= months; i++)
                    {
                        int icolspan = ttlSpc.Length;

                        Spec = new List<string>();

                        string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                        AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Seen", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Average", "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, icolspan, "Rx Qty given to Drs", "#0097AC", true);


                        sample_name = sample_name.Remove(sample_name.Length - 1);
                        string[] inpu_name = sample_name.Split(',');




                        for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Product_Code_SlNo"].ToString());

                        }
                        Spec.Sort();
                        foreach (string specc in Spec)
                        {
                            Spe1.Add(specc);
                            Doctor doc = new Doctor();
                            iLstMonth.Add(cmonth);
                            iLstYear.Add(cyear);


                            dsDoc = doc.getRx_code(specc);
                            // AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Detail_Name"].ToString(), "#0097AC", true);

                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Detail_Name"].ToString(), "#0097AC", true);
                        }


                        //Doctor dr = new Doctor();
                        //dsDoctor = dr.getPrd_rx(sample_code);

                        // foreach (string inp_name in inpu_name)
                        // {

                        // //for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        // //{
                        //     iLstMonth.Add(cmonth);
                        //     iLstYear.Add(cyear);


                        //     //AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoctor.Tables[0].Rows[k]["Product_Detail_Name"].ToString(), "#0097AC", true);
                        //     AddMergedCells(objgridviewrow3, objtablecell3, 0, 0,inp_name, "#0097AC", true);
                        //// }

                        // }

                        cmonth = cmonth + 1;

                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                    //AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total", "#0097AC", true);




                    //Total No.Of Rxers(No.of Rx Drs)
                    //Total No.Of Rxns(Total Rx Qty)


                    //
                    objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
                    objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                    objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                    // objGridView.Controls[0].Controls.AddAt(3, objgridviewrow4);
                    //
                    #endregion
                    //
                }

                else if (type == "1")
                {
                    //
                    #region Object
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

                    GridViewRow objgridviewrow4 = new GridViewRow(4, 0, DataControlRowType.Header, DataControlRowState.Insert);
                    TableCell objtablecell4 = new TableCell();
                    #endregion
                    //
                    #region Merge cells

                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "#", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "FieldForce Name", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "HQ", "#0097AC", true);
                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Designation", "#0097AC", true);
                   
                    AddMergedCells(objgridviewrow, objtablecell, 2, 2, "Total No.Of Rxers(No.of Rx Drs)", "#0097AC", true);

                    // AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxers(No.of Rx Drs - Unique)", "#0097AC", true);

                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Multiple", "#0097AC", true);
                    AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, "Unique", "#0097AC", true);




                    AddMergedCells(objgridviewrow, objtablecell, 3, 0, "Total No.Of Rxns(Total Rx Qty)", "#0097AC", true);


                    int months = (Convert.ToInt32(Request.QueryString["TYear"].ToString()) - Convert.ToInt32(Request.QueryString["FYear"].ToString())) * 12 + Convert.ToInt32(Request.QueryString["TMonth"].ToString()) - Convert.ToInt32(Request.QueryString["FMonth"].ToString()); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                    int cmonth = Convert.ToInt32(Request.QueryString["FMonth"].ToString());
                    int cyear = Convert.ToInt32(Request.QueryString["FYear"].ToString());


                    SalesForce sf = new SalesForce();

                    sample_code = sample_code.Remove(sample_code.LastIndexOf(','));
                    string[] ttlSpc = sample_code.Split(',');

                    foreach (string inp in ttlSpc)
                    {
                        iInp.Add(inp);
                    }


                    string strspec2 = sample_code.Remove(sample_code.Length - 1);
                    Doctor dr = new Doctor();
                    dsDoctor = dr.getRx_Brand_code(sample_code);

                    for (int i = 0; i <= months; i++)
                    {
                        int icolspan = ttlSpc.Length;

                        Spec = new List<string>();

                        string sTxt = "&nbsp;" + sf.getMonthName(cmonth.ToString()) + "-" + cyear;
                        AddMergedCells(objgridviewrow, objtablecell, 0, icolspan, sTxt, "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Seen", "#0097AC", true);
                        //AddMergedCells(objgridviewrow2, objtablecell2, 2, 0, "Call Average", "#0097AC", true);
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, icolspan, "Rx Qty given to Drs", "#0097AC", true);


                        sample_name = sample_name.Remove(sample_name.Length - 1);
                        string[] inpu_name = sample_name.Split(',');




                        for (int k = 0; k < dsDoctor.Tables[0].Rows.Count; k++)
                        {
                            Spec.Add(dsDoctor.Tables[0].Rows[k]["Product_Brd_Code"].ToString());

                        }
                        Spec.Sort();
                        foreach (string specc in Spec)
                        {
                            Spe1.Add(specc);
                            Doctor doc = new Doctor();
                            iLstMonth.Add(cmonth);
                            iLstYear.Add(cyear);


                            dsDoc = doc.getRx_Brand_code(specc);
                            AddMergedCells(objgridviewrow3, objtablecell3, 0, 0, dsDoc.Tables[0].Rows[0]["Product_Brd_Name"].ToString(), "#0097AC", true);
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
                    objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
                    objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);
                    // objGridView.Controls[0].Controls.AddAt(3, objgridviewrow4);
                    //
                    #endregion
                    //
                }

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
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");;
        objtablecell.Style.Add("white-space", "unset");
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else if (objgridviewrow.RowIndex == 2)
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickyThirdRow");
        }
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
        objtablecell.Wrap = false;

    }
    protected void GrdInput_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (FMonth == TMonth && FYear == TYear)
            {
                if (type == "0")
                {

                    int iInx = e.Row.RowIndex;

                    for (int i = 4, j = 0, m = 0; i < e.Row.Cells.Count; i++)
                    {

                        if (i == 4)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 5)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 6)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 7)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 8)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 9)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                        else if (i == 10)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 11)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 12)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 13)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 14)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                        else
                        {

                            if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[i].Text;
                                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                                int cMnth = iLstMonth[j];
                                int cYr = iLstYear[j];
                                //string Prod2 = iInp[m];
                                string Prod2 = Spe1[m];

                                //m++;
                                //if (iInp.Count == m)
                                //{
                                //    m = 0;
                                //}

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
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");

                                }
                                else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");
                                    Session["Sf_Code_multiple"] = sfCode;
                                }
                                //hLink.NavigateUrl = "#";
                                hLink.ToolTip = "Click here";
                                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    hLink.ForeColor = System.Drawing.Color.Blue;
                                }
                                e.Row.Cells[i].Controls.Add(hLink);

                                tot = hLink.Text;
                                if (tot != "-" && tot != "&nbsp;")
                                {
                                    total += Convert.ToInt32(tot);
                                }

                                j++;
                                m++;

                            }
                        }
                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                        {
                            e.Row.Cells[i].Text = "-";
                        }
                        e.Row.Cells[i].Attributes.Add("align", "center");

                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                    }

                    if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Attributes.Add("align", "center");

                    }

                    //TableCell Row_Total = new TableCell();
                    //Row_Total.Text = total.ToString();

                    //if (Row_Total.Text == "0")
                    //{
                    //    Row_Total.Text = "";
                    //}
                    //e.Row.Cells.Add(Row_Total);
                    //Row_Total.Attributes.Add("align", "right");
                    //total = 0;

                    //e.Row.Cells[1].Wrap = false;
                    //e.Row.Cells[2].Wrap = false;
                    //e.Row.Cells[3].Wrap = false;
                }

                else if (type == "1")
                {
                    int iInx = e.Row.RowIndex;

                    for (int i = 4, j = 0, m = 0; i < e.Row.Cells.Count; i++)
                    {

                        if (i == 4)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 5)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 6)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 7)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 8)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 9)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                        else if (i == 10)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 11)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 12)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 13)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 14)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                        else
                        {

                            if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[i].Text;
                                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                                int cMnth = iLstMonth[j];
                                int cYr = iLstYear[j];
                                //string Prod2 = iInp[m];
                                string Prod2 = Spe1[m];

                                //m++;
                                //if (iInp.Count == m)
                                //{
                                //    m = 0;
                                //}

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
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");

                                }
                                else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");
                                    Session["Sf_Code_multiple"] = sfCode;
                                }
                                //hLink.NavigateUrl = "#";
                                hLink.ToolTip = "Click here";
                                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    hLink.ForeColor = System.Drawing.Color.Blue;
                                }
                                e.Row.Cells[i].Controls.Add(hLink);

                                tot = hLink.Text;
                                if (tot != "-" && tot != "&nbsp;")
                                {
                                    total += Convert.ToInt32(tot);
                                }

                                j++;
                                m++;

                            }
                        }
                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                        {
                            e.Row.Cells[i].Text = "-";
                        }
                        e.Row.Cells[i].Attributes.Add("align", "center");

                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                    }

                    if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Attributes.Add("align", "center");

                    }

                    //TableCell Row_Total = new TableCell();
                    //Row_Total.Text = total.ToString();

                    //if (Row_Total.Text == "0")
                    //{
                    //    Row_Total.Text = "";
                    //}
                    //e.Row.Cells.Add(Row_Total);
                    //Row_Total.Attributes.Add("align", "right");
                    //total = 0;

                    //e.Row.Cells[1].Wrap = false;
                    //e.Row.Cells[2].Wrap = false;
                    //e.Row.Cells[3].Wrap = false;
                }
            }

            else
            {
                if (type == "0")
                {

                    int iInx = e.Row.RowIndex;

                    for (int i = 4, j = 0, m = 0; i < e.Row.Cells.Count; i++)
                    {

                        if (i == 4)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 5)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 6)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }


                        else
                        {

                            if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[i].Text;
                                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                                int cMnth = iLstMonth[j];
                                int cYr = iLstYear[j];
                                //string Prod2 = iInp[m];
                                string Prod2 = Spe1[m];

                                //m++;
                                //if (iInp.Count == m)
                                //{
                                //    m = 0;
                                //}

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
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");

                                }
                                else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");
                                    Session["Sf_Code_multiple"] = sfCode;
                                }
                                //hLink.NavigateUrl = "#";
                                hLink.ToolTip = "Click here";
                                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    hLink.ForeColor = System.Drawing.Color.Blue;
                                }
                                e.Row.Cells[i].Controls.Add(hLink);

                                tot = hLink.Text;
                                if (tot != "-" && tot != "&nbsp;")
                                {
                                    total += Convert.ToInt32(tot);
                                }

                                j++;
                                m++;

                            }
                        }
                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                        {
                            e.Row.Cells[i].Text = "-";
                        }
                        e.Row.Cells[i].Attributes.Add("align", "center");

                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                    }

                    if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Attributes.Add("align", "center");

                    }

                    //TableCell Row_Total = new TableCell();
                    //Row_Total.Text = total.ToString();

                    //if (Row_Total.Text == "0")
                    //{
                    //    Row_Total.Text = "";
                    //}
                    //e.Row.Cells.Add(Row_Total);
                    //Row_Total.Attributes.Add("align", "right");
                    //total = 0;

                    //e.Row.Cells[1].Wrap = false;
                    //e.Row.Cells[2].Wrap = false;
                    //e.Row.Cells[3].Wrap = false;
                }

                else if (type == "1")
                {
                    int iInx = e.Row.RowIndex;

                    for (int i = 4, j = 0, m = 0; i < e.Row.Cells.Count; i++)
                    {

                        if (i == 4)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 5)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }

                        else if (i == 6)
                        {
                            HyperLink hLink = new HyperLink();
                            hLink.Text = e.Row.Cells[i].Text;
                            //string Pro_code = dtrowClr.Rows[iInx][1].ToString();
                            //string Pro_Name = dtrowClr.Rows[iInx][2].ToString();
                            //hLink.Attributes.Add("href", "javascript:showModal('" + sfCode + "', '" + sfname + "','" + Pro_Name + "','" + Pro_code + "')");
                            hLink.ToolTip = "Click here";
                            hLink.ForeColor = System.Drawing.Color.Indigo;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }


                        else
                        {

                            if (e.Row.Cells[i].Text != "&nbsp;" && e.Row.Cells[i].Text != "-")
                            {
                                HyperLink hLink = new HyperLink();
                                hLink.Text = e.Row.Cells[i].Text;
                                string sSf_code = dtrowClr.Rows[iInx][1].ToString();
                                string sSf_name = dtrowClr.Rows[iInx][2].ToString();
                                int cMnth = iLstMonth[j];
                                int cYr = iLstYear[j];
                                //string Prod2 = iInp[m];
                                string Prod2 = Spe1[m];

                                //m++;
                                //if (iInp.Count == m)
                                //{
                                //    m = 0;
                                //}

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
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");

                                }
                                else if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.Attributes.Add("href", "javascript:showModalPopUp('" + 0 + "', '" + sSf_name + "', '" + cYr + "', '" + cMnth + "','" + Prod2 + "', '" + sCurrentDate + "','" + type + "')");
                                    Session["Sf_Code_multiple"] = sfCode;
                                }
                                //hLink.NavigateUrl = "#";
                                hLink.ToolTip = "Click here";
                                if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                                {
                                    hLink.ForeColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    hLink.ForeColor = System.Drawing.Color.Blue;
                                }
                                e.Row.Cells[i].Controls.Add(hLink);

                                tot = hLink.Text;
                                if (tot != "-" && tot != "&nbsp;")
                                {
                                    total += Convert.ToInt32(tot);
                                }

                                j++;
                                m++;

                            }
                        }
                        if (e.Row.Cells[i].Text == "&nbsp;" || e.Row.Cells[i].Text == "0")
                        {
                            e.Row.Cells[i].Text = "-";
                        }
                        e.Row.Cells[i].Attributes.Add("align", "center");

                        int k = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                        e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[k][5].ToString()));
                    }

                    if (dtrowClr.Rows[iInx][1].ToString() == "Total")
                    {
                        e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Attributes.Add("align", "center");

                    }

                    //TableCell Row_Total = new TableCell();
                    //Row_Total.Text = total.ToString();

                    //if (Row_Total.Text == "0")
                    //{
                    //    Row_Total.Text = "";
                    //}
                    //e.Row.Cells.Add(Row_Total);
                    //Row_Total.Attributes.Add("align", "right");
                    //total = 0;

                    //e.Row.Cells[1].Wrap = false;
                    //e.Row.Cells[2].Wrap = false;
                    //e.Row.Cells[3].Wrap = false;
                }
            }
        }
    
        
    }
}
