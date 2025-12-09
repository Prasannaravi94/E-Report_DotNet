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
using System.Drawing;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class MasterFiles_Survey_Rpt_Survey_Process_View_Zoom : System.Web.UI.Page
{
    DataSet dsDoctor = null;
    DataSet dsMailCompose = null;
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string Survey_Id = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    string Ques_Id = string.Empty;
    string DCHP_Mode = string.Empty;

    string HQ = string.Empty;
    string Desig = string.Empty;
    string Emp_id = string.Empty;
    string Question = string.Empty;

    List<int> iLstVstCnt = new List<int>();
    DataSet dschem = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    int imissed_dr = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            Survey_Id = Request.QueryString["Survey_Id"].ToString();
            DCHP_Mode = Request.QueryString["Survey_Mode"].ToString();
            Ques_Id = Request.QueryString["Ques_Id"].ToString();

            strFieledForceName = Request.QueryString["Sf_Name"].ToString();
            HQ = Request.QueryString["HQ"].ToString();
            Desig = Request.QueryString["Designation"].ToString();
            Question = Request.QueryString["Ques_Name"].ToString();
            Emp_id = Request.QueryString["Emp_id"].ToString();
            SalesForce sf = new SalesForce();
            lblHead.Text = " Survey Answer ";
            //LblForceName.Text = "Field Force Name : " + strFieledForceName;

            LblForceName.Text =  strFieledForceName;
            txtHQ.Text = HQ;
            txtDesig.Text = Desig;
            txtQues.Text = Question;

            LblForceName.Attributes.Add("style", "Fore-color:#ff6666");
            txtHQ.Attributes.Add("style", "Fore-color:#ff6666");
            txtDesig.Attributes.Add("style", "Fore-color:#ff6666");
            txtQues.Attributes.Add("style", "Fore-color:#ff6666");

            FillReport();
        }
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

        sProc_Name = "Survey_Process_view_Zoom";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@div_code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@sf_code", sf_code);
        cmd.Parameters.AddWithValue("@Survey_Id", Survey_Id);
        cmd.Parameters.AddWithValue("@DCHP_Mode", DCHP_Mode);
        cmd.Parameters.AddWithValue("@Ques_id", Ques_Id);

        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("Active_Flag");
        dsts.Tables[0].Columns.Remove("Question_Name");

        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }

    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;
            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells
            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#0097AC", true);

            if (Request.QueryString["Survey_Mode"].ToString() == "D")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Doctor Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Category", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Speciality", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Qual", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Class", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory Name", "#0097AC", true);
            }
            else if (Request.QueryString["Survey_Mode"].ToString() == "C")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Chemist Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory Name", "#0097AC", true);
            }
            else if (Request.QueryString["Survey_Mode"].ToString() == "S")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Stockist Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory Name", "#0097AC", true);
            }
            else if (Request.QueryString["Survey_Mode"].ToString() == "H")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Hospital Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Territory Name", "#0097AC", true);
            }
            else if (Request.QueryString["Survey_Mode"].ToString() == "P")
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, "Product Name", "#0097AC", true);
                AddMergedCells(objgridviewrow, objtablecell, 0, "Pack", "#0097AC", true);
            }
            AddMergedCells(objgridviewrow, objtablecell, 0, "Answer", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Sub.Date", "#0097AC", true);

            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            #endregion
        }
    }

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        //if ((colspan == 0) && bRowspan)
        //{
        //    objtablecell.RowSpan = 2;
        //}
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
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

            for (int i = 8, j = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
               // e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
            }
            catch
            {
                //e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
        }
    }
}


