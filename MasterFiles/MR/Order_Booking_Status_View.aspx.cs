#region Assembly
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
using DBase_EReport;
using System.Net;
using System.Data;
using System.Data.SqlClient;
#endregion

public partial class MasterFiles_MR_Order_Booking_Status_View : System.Web.UI.Page
{
    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();

    string Trans_SlNo = string.Empty;
    string Stockist_Name = string.Empty;
    string DHP_Code = string.Empty;
    string DHP_Name = string.Empty;
    string Order_Date = string.Empty;
    string Mode = string.Empty;
    string Order_Flag = string.Empty;
    DB_EReporting db = new DB_EReporting();
    string strQry = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();

        sf_code = Request.QueryString["Sf_code"].ToString();
        Trans_SlNo = Request.QueryString["Trans_SlNo"].ToString();
        Stockist_Name = Request.QueryString["Stockist_Name"].ToString();
        DHP_Code = Request.QueryString["DHP_Code"].ToString();
        DHP_Name = Request.QueryString["DHP_Name"].ToString();
        Order_Date = Request.QueryString["Order_Date"].ToString();
        Mode = Request.QueryString["Mode"].ToString();
        Order_Flag = Request.QueryString["Order_Flag"].ToString();
        //lblHead.Text = " Order booking View";
        //LblForceName.Text = "Field Force Name : " + strFieledForceName;

        if (Mode == "Pharmacist/Chemist" || Mode == "Pharma")
        {
            strQry = " select distinct Chemists_Code as Code,Chemists_Name as Name,Chemists_Address1 as Address,(select Territory_Name from mas_territory_Creation where Territory_Code=a.Territory_Code)Territory_Name,Chemists_Phone as Contact from Mas_Chemists a where Chemists_Code='" + DHP_Code + "' ";
            dsDoctor = db.Exec_DataSet(strQry);
        }
        else if (Mode == "Hospital")
        {
            strQry = " select distinct Hospital_Code as Code,Hospital_Name as Name,Hospital_Address1 as Address,(select Territory_Name from mas_territory_Creation where Territory_Code=a.Territory_Code)Territory_Name,Hospital_Phone as Contact from mas_Hospital a where Hospital_Code='" + DHP_Code + "' ";
            dsDoctor = db.Exec_DataSet(strQry);
        }
        else if (Mode == "Doctor")
        {
            strQry = " select distinct ListedDrCode as Code,ListedDr_Name as Name,ListedDr_Address1 as Address,(select Territory_Name from mas_territory_Creation where Territory_Code=a.Territory_Code)Territory_Name,ListedDr_Phone as Contact from mas_listeddr a where ListedDrCode='" + DHP_Code + "' ";
            dsDoctor = db.Exec_DataSet(strQry);
        }

        string Address = string.Empty;
        string Contact = string.Empty;
        string Town = string.Empty;

        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            Address = dsDoctor.Tables[0].Rows[0]["Address"].ToString();
            Contact = dsDoctor.Tables[0].Rows[0]["Contact"].ToString();
            Town = dsDoctor.Tables[0].Rows[0]["Territory_Name"].ToString();

        }
        txtNo.Text = Trans_SlNo;
        txtDate.Text = Order_Date;
        txtCus.Text = DHP_Name;

        txtAddres.Text = Address;
        txtMbl.Text = Contact;
        txtTown.Text = Town;
        txtSupp.Text = Stockist_Name;


        if (Order_Flag == "2")
        {
            tblChk.Visible = false;
            btnSave.Visible = false;
        }

        if (Order_Flag == "3")
        {
            //chkinvoiced.Enabled = false;
            //chkdespatched.Enabled = true;
            //chkdelivered.Enabled = false;

            chkinvoiced.Visible = false;
            lblchkinv.Visible = false;
            imgCross1.Visible = true;
            lblInv.Visible = true;

            chkdespatched.Visible = true;
            lblchkDesp.Visible = true;
            imgCross3.Visible = false;
            lblDesp.Visible = false;

            chkdelivered.Visible = false;
            lblchkdeli.Visible = false;
            imgCross.Visible = true;
            lbldeli.Visible = true;
        }
        else if (Order_Flag == "4")
        {
            //chkinvoiced.Enabled = false;
            //chkdespatched.Enabled = false;
            //chkdelivered.Enabled = true;

            chkinvoiced.Visible = false;
            lblchkinv.Visible = false;
            imgCross1.Visible = true;
            lblInv.Visible = true;

            chkdespatched.Visible = false;
            lblchkDesp.Visible = false;
            imgCross3.Visible = true;
            lblDesp.Visible = true;

            chkdelivered.Visible = true;
            lblchkdeli.Visible = true;
            imgCross.Visible = false;
            lbldeli.Visible = false;
        }
        else if (Order_Flag == "5")
        {
            //chkinvoiced.Enabled = false;
            //chkdespatched.Enabled = false;
            //chkdelivered.Enabled = false;

            chkinvoiced.Visible = false;
            lblchkinv.Visible = false;
            imgCross1.Visible = true;
            lblInv.Visible = true;

            chkdespatched.Visible = false;
            lblchkDesp.Visible = false;
            imgCross3.Visible = true;
            lblDesp.Visible = true;

            chkdelivered.Visible = false;
            lblchkdeli.Visible = false;
            imgCross.Visible = true;
            lbldeli.Visible = true;
        }
        else
        {
            //chkinvoiced.Enabled = true;
            //chkdespatched.Enabled = false;
            //chkdelivered.Enabled = false;


            chkinvoiced.Visible = true;
            lblchkinv.Visible = true;
            imgCross1.Visible = false;
            lblInv.Visible = false;

            chkdespatched.Visible = false;
            lblchkDesp.Visible = false;
            imgCross3.Visible = true;
            lblDesp.Visible = true;

            chkdelivered.Visible = false;
            lblchkdeli.Visible = false;
            imgCross.Visible = true;
            lbldeli.Visible = true;
        }

        if (Order_Flag != "6" && Order_Flag != "3" && Order_Flag != "4" && Order_Flag != "5" && Order_Flag != "2")
        {
            strQry = "Update trans_Order_Book_Head set Order_Flag=6 where Sf_Code='" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo + "' ";
            int iReturn = db.ExecQry(strQry);
        }
        FillReport();
    }

    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Rpt_Order_Booking_Status_View";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Sf_Code", sf_code);
        cmd.Parameters.AddWithValue("@Dates", Order_Date);
        cmd.Parameters.AddWithValue("@Trans_SlNo", Trans_SlNo);

        cmd.CommandTimeout = 150;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        con.Close();
        // result = dsts.Tables[1].AsEnumerable()
        //.GroupBy(row => row.Field<string>("VST"))
        //.Select(g => g.CopyToDataTable()).ToList();

        if (dsts.Tables[0].Rows.Count > 0)
        {
            dsts.Tables[0].Columns.RemoveAt(1);

            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
            Table1.Visible = true;
        }
        else
        {
            GrdFixation.DataSource = dsts;
            GrdFixation.DataBind();
        }
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;


            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell = new TableCell();
            #endregion
            //
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "SKU .No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "SKU Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Qty", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "BONUS", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "UNIT PRICE", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "DISCOUNT", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "VAT", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "NRV", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "TOTAL PRICE", "#0097AC", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            //
            #endregion
            //
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 1;
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("color", "white");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
        objtablecell.Style.Add("CssClass", "stickyFirstRow");
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            //e.Row.Cells[2].Wrap = false;
            //e.Row.Cells[3].Wrap = false;
            //e.Row.Cells[4].Wrap = false;
            //e.Row.Cells[5].Wrap = false;
            //e.Row.Cells[6].Wrap = false;
            //e.Row.Cells[7].Wrap = false;
            //e.Row.Cells[8].Wrap = false;
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (chkinvoiced.Checked)
        {
            strQry = "Update trans_Order_Book_Head set Order_Flag=3 where Sf_Code='" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo + "' ";
            int iReturn = db.ExecQry(strQry);
        }
        else if (chkdespatched.Checked)
        {
            strQry = "Update trans_Order_Book_Head set Order_Flag=4 where Sf_Code='" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo + "' ";
            int iReturn = db.ExecQry(strQry);
        }
        else if (chkdelivered.Checked)
        {
            strQry = "Update trans_Order_Book_Head set Order_Flag=5 where Sf_Code='" + sf_code + "' and Trans_SlNo ='" + Trans_SlNo + "' ";
            int iReturn = db.ExecQry(strQry);
        }

        string strScript = "window.close();";
        ScriptManager.RegisterStartupScript(this, typeof(string), "key", strScript, true);

        //Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");

    }
}