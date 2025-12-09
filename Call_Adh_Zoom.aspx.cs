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
public partial class Call_Adh_Zoom : System.Web.UI.Page
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
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsSal = new DataSet();
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DateTime dtCurrent;
    DataTable dtrowClr = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();

    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();

            FYear = Request.QueryString["Fyear"].ToString();
            strFieledForceName = Request.QueryString["sf_name"].ToString();

            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth.Trim());
            string strToMonth = sf.getMonthName(TMonth.Trim());
            lblHead.Text = "Call Adherence for the month of " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
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
        string sProc_Name = "";

        sProc_Name = "Manager_Visit_Cat_Zoom";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code.Trim());
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth.Trim());
        cmd.Parameters.AddWithValue("@cYrs", FYear.Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.Remove("sf_code");
        dsts.Tables[0].Columns.Remove("sf_code1");
        dsts.Tables[0].Columns.Remove("clr");
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
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2,0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2,0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2,0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2,0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2,0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2,0, "Emp Id", "#0097AC", true);
            string strQry = " SELECT c.Doc_Cat_Code,c.Doc_Cat_SName AS ShortName, Doc_Cat_SName + ' ( ' +cast(No_of_visit as varchar) + ' Visit ) ' as Doc_Cat_Name,case isnull(c.No_of_visit,'') " +
            " when '' then 1 when 0 then 1 else c.No_of_visit end No_of_visit  " +
           
            " FROM  Mas_Doctor_Category c WHERE c.Doc_Cat_Active_Flag=0 AND c.Division_Code= '" + div_code + "' " +
            " ORDER BY c.Doc_Cat_Code";
            DB_EReporting db = new DB_EReporting();
            dsDoctor = db.Exec_DataSet(strQry);
            for (int j = 0; j < dsDoctor.Tables[0].Rows.Count; j++)
            {
                AddMergedCells(objgridviewrow, objtablecell, 0, 3, dsDoctor.Tables[0].Rows[j]["Doc_Cat_Name"].ToString(), "#0097AC", false);

                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "List", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Met", "#0097AC", false);
                AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Missed", "#0097AC", false);
            }
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);

            #endregion
        }
    }
    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int rowSpan, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        objtablecell.RowSpan = rowSpan;
        //objtablecell.Font.Size = 14;
        //objtablecell.Height = 30;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = true;
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
            for (int l = 6, j = 0; l < e.Row.Cells.Count; l+=3)
            {

                int iTtl_Drs = (e.Row.Cells[l].Text == "-" || e.Row.Cells[l].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int iDrs_Mt = (e.Row.Cells[l + 1].Text == "-" || e.Row.Cells[l + 1].Text == "&nbsp;") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);

                int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                e.Row.Cells[l + 2].Text = iDrs_Msd.ToString();
             
               
            }
            for (int l = 6, j = 0; l < e.Row.Cells.Count; l ++)
            {
                if (e.Row.Cells[l].Text == "0" || e.Row.Cells[l].Text == "-")
                {
                    e.Row.Cells[l].Text = "";
                }
                e.Row.Cells[l].Attributes.Add("align", "center");
            }

            //if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            //{
            //}
            //e.Row.Cells[6].Attributes.Add("align", "center");
            //e.Row.Cells[7].Attributes.Add("align", "center");
         
            //e.Row.Cells[8].Attributes.Add("align", "center");
         
            //    if (e.Row.Cells[6].Text != "0")
            //    {
            //        if (dtrowClr.Rows[indx][1].ToString() != "Total")
            //        {
            //            if (e.Row.Cells[8].Text == "0")
            //            {
            //                e.Row.Cells[8].Text = "";
            //            }
            //            HyperLink hLink = new HyperLink();
            //            hLink.Text = e.Row.Cells[8].Text;
            //            string sSf_code = dtrowClr.Rows[indx][1].ToString();


            //            hLink.Attributes.Add("href", "javascript:showMissedDR('" + sSf_code + "',  '" + FMonth + "', '" + FYear + "',1,'','', '" + div_code + "')");
            //            //hLink.ToolTip = "Click here";
            //            // hLink.ForeColor = System.Drawing.Color.Red;
            //            hLink.Style.Add("text-decoration", "none");
            //            hLink.Style.Add("color", "Red");
            //            hLink.Style.Add("border-color", "Black");
            //            hLink.Style.Add("font-size", "14px");
            //            hLink.Style.Add("cursor", "hand");
            //            e.Row.Cells[8].Controls.Add(hLink);
            //        }

            //    }
            //    else
            //    {
            //        e.Row.Cells[6].Text = "";
            //      //  e.Row.Cells[7].Text = "";
                  
            //    }

              
                //if (e.Row.Cells[8].Text.Contains("-"))
                //{
                //    e.Row.Cells[8].Text = e.Row.Cells[8].Text.Replace("-", "");
                //}
            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
            {
                HyperLink hLink = new HyperLink();
                hLink.Text = e.Row.Cells[1].Text;

                string sSf_code = dtrowClr.Rows[indx][1].ToString();
                string sSf_Name = dtrowClr.Rows[indx][2].ToString();

                hLink.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_code + "',  '" + FMonth + "', '" + FYear + "',  '" + FMonth + "', '" + FYear + "','" + sSf_Name + "',1)");

                //hLink.ToolTip = "Click here";
                //hLink.ForeColor = System.Drawing.Color.Blue;
              //  hLink.Style.Add("text-decoration", "none");
                hLink.Style.Add("color", "black");
                hLink.Style.Add("cursor", "hand");
                e.Row.Cells[1].Controls.Add(hLink);
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }


            if (dtrowClr.Rows[indx][3].ToString() == "Grand Total")
            {
                e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black;text-align:right");
                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
           
              
              
              
            }
            #endregion
            //
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
        }
    }
}