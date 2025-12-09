using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MIS_Reports_rptPrescriber_report : System.Web.UI.Page
{
    string Month = string.Empty;
    string Year = string.Empty;

    string sf_name = string.Empty;
    string div_code = string.Empty;
    string SF_code = string.Empty;
    string Desig = string.Empty;
    string Spec = string.Empty;
    DataTable dtSpclty = new DataTable();
    DataTable dtRangeVal = new DataTable();
    DataTable dtrowClr = null;
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        SF_code = Request.QueryString["sfcode"].ToString();
        Month = Request.QueryString["Month"].ToString();
        Year = Request.QueryString["Year"].ToString();
        sf_name = Request.QueryString["sf_name"].ToString();
        Desig = Request.QueryString["Desig"].ToString();
        Spec = Request.QueryString["Spec"].ToString();

        SalesForce sf = new SalesForce();
        string strMonth = sf.getMonthName(Month.Trim());
        lblFieldForce.Text= "<b>Field Force Name : </b> " + sf_name +" ( "+ strMonth + " - " + Year + ")";
        lblDesignSelected.Text = "<b>Designation Selected : </b> " + Desig;
        FillPresciber();
    }
    private void FillPresciber()
    {
        DataTable dtDesig = new DataTable();
        dtDesig.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtDesig.Columns["INX"].AutoIncrementSeed = 1;
        dtDesig.Columns["INX"].AutoIncrementStep = 1;
        dtDesig.Columns.Add("CODE", typeof(string));

        //Desig = Desig.Remove(Desig.LastIndexOf(','));

        dtDesig.Rows.Add(null, Desig);

        dtSpclty.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSpclty.Columns["INX"].AutoIncrementSeed = 1;
        dtSpclty.Columns["INX"].AutoIncrementStep = 1;
        dtSpclty.Columns.Add("CODE", typeof(int));

        Spec = Spec.Remove(Spec.LastIndexOf(','));

        foreach (string sSpclty in Spec.Split(','))
        {
            if (sSpclty != "")
                dtSpclty.Rows.Add(null, Convert.ToInt32(sSpclty));
        }

        SqlConnection con = new SqlConnection(strConn);
        DataSet dsts = new DataSet();

        SqlCommand cmd = new SqlCommand("SP_Prescribers_Det", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", SF_code);
        cmd.Parameters.AddWithValue("@cMnth", Month);
        cmd.Parameters.AddWithValue("@cYr", Year);
        cmd.Parameters.AddWithValue("@desg", dtDesig);
        cmd.Parameters.AddWithValue("@Spclty", dtSpclty);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(dsts);
        //dtrowClr = dsts.Tables[0].Copy();

        dsts.Tables[0].Columns.Remove("MGR");
        GrdPrescriber.DataSource = dsts;
        GrdPrescriber.DataBind();

    }

    protected void GrdPrescriber_RowCreated(object sender, GridViewRowEventArgs e)
    {
        ListedDoctor dr = new ListedDoctor();
        SalesForce sf = new SalesForce();
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsListedDR = null;
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
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "slno", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "HQ Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Total No.Of Prescribers", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Total Business Value", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Avg Business/ Prescriber", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "1 visit", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "2 visit", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "3 visit", "#0097AC", true);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            SecSale ss = new SecSale();
            TableCell objtablecell2 = new TableCell();
            TableCell objtablecell3 = new TableCell();

            string strQry = string.Empty;
            for (int j = 0; j < dtSpclty.Rows.Count; j++)
            {
                strQry = "SELECT Doc_Special_Code,Doc_Special_SName FROM Mas_Doctor_Speciality WHERE Doc_Special_Code='" + dtSpclty.Rows[j]["CODE"].ToString() + "'";
                dsListedDR = db_ER.Exec_DataSet(strQry);

                AddMergedCells(objgridviewrow, objtablecell, 1, 0, dsListedDR.Tables[0].Rows[0]["Doc_Special_SName"].ToString(), "#0097AC", true);
            }
            AddMergedCells(objgridviewrow, objtablecell, 1, 0, "Not Visit Drs", "#0097AC", true);
            strQry = string.Empty;
            strQry = "SELECT From_Range , To_Range From Mas_DrBusiness_Range WHERE division_code='" + div_code + "'";
            dsListedDR = db_ER.Exec_DataSet(strQry);
            dtRangeVal = dsListedDR.Tables[0].Copy();
            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsListedDR.Tables[0].Rows.Count; i++)
                {
                    if (dsListedDR.Tables[0].Rows[i]["To_Range"].ToString() == string.Empty || dsListedDR.Tables[0].Rows[i]["To_Range"].ToString() == "0")
                    {
                        AddMergedCells(objgridviewrow, objtablecell, 1, 0, "No of drs business value (" + dsListedDR.Tables[0].Rows[i]["From_Range"] + " above)", "#0097AC", true);
                    }
                    else {
                        AddMergedCells(objgridviewrow, objtablecell, 1, 0, "No of drs business value (" + dsListedDR.Tables[0].Rows[i]["From_Range"] + "-" + dsListedDR.Tables[0].Rows[i]["To_Range"] + ")", "#0097AC", true);
                    }
                }
            }
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            #endregion
        }
    }

    protected void GrdPrescriber_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int specCell = dtSpclty.Rows.Count;
            int range = dtRangeVal.Rows.Count;
            for (int i1 = 1; i1 < e.Row.Cells.Count; i1++)
            {
                if (i1 < 3)
                { e.Row.Cells[i1].Style.Add("background-color", "#f3f8c7"); }
                else if (i1 < 6)
                { e.Row.Cells[i1].Style.Add("background-color", "#c7f2f8"); }
                else if (i1 < 9)
                { e.Row.Cells[i1].Style.Add("background-color", "#c7f8d2"); }
                else if (i1 < (9 + specCell))
                { e.Row.Cells[i1].Style.Add("background-color", "#f8e0c7"); }
                else if (i1 < (9 + specCell+1))
                { e.Row.Cells[i1].Style.Add("background-color", "#c7f8d2"); }
                else if (i1 < (9 + specCell + range+1))
                { e.Row.Cells[i1].Style.Add("background-color", "#f3f8c7"); }

                //e.Row.Cells[i1].Attributes.Add("style", "word-wrap: break-word;");
            }
        }
      
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        objtablecell.Font.Size = 11;
        objtablecell.Width = 20;
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
        objgridviewrow.Cells.Add(objtablecell);
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
}