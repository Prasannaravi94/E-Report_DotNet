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
public partial class Product_Detail_Zoom : System.Web.UI.Page
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
    List<int> iLstProd = new List<int>();
    List<string> iLstProdName = new List<string>();
    DataTable dtProd = new DataTable();
    DataSet dsP = new DataSet();
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
            lblHead.Text = "Product Detailing Doctors for the month of " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillProd();
            FillReport();
        }
    }
    private void FillProd()
    {

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Product_Det_Graph_Code";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code.Trim());
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth.Trim());
        cmd.Parameters.AddWithValue("@cYrs", FYear.Trim());
        SqlDataAdapter da = new SqlDataAdapter(cmd);
     
        da.Fill(dsP);

       
        dtProd.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtProd.Columns["INX"].AutoIncrementSeed= 1;
        dtProd.Columns["INX"].AutoIncrementStep = 1;
        dtProd.Columns.Add("Prod", typeof(int));

        if (dsP.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsP.Tables[0].Rows.Count; i++)
            {
                dtProd.Rows.Add(null, dsP.Tables[0].Rows[i]["Product_Code_SlNo"].ToString());
            }
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

        sProc_Name = "Product_Det_Graph_Zoom_2";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code.Trim());
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", FMonth.Trim());
        cmd.Parameters.AddWithValue("@cYrs", FYear.Trim());
        cmd.Parameters.AddWithValue("@Prod_Code", dtProd);
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
            GridViewRow objgridviewrow2 = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);

            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            TableCell objtablecell2 = new TableCell();
            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0,0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0,0, "Emp Id", "#0097AC", true);

            if (dsP.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < dsP.Tables[0].Rows.Count; j++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 0, 0, dsP.Tables[0].Rows[j]["Prod_Name"].ToString(), "#0097AC", true);
                    int prod = Convert.ToInt32(dsP.Tables[0].Rows[j]["Product_Code_SlNo"].ToString());
                    string PrdName=dsP.Tables[0].Rows[j]["Prod_Name"].ToString();
                    iLstProd.Add(prod);
                    iLstProdName.Add(PrdName);
                }

            }
             
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

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
        objtablecell.Wrap = false;
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

            for (int l = 6, j = 0; l < e.Row.Cells.Count; l++)
            {
                if (e.Row.Cells[l].Text == "0" || e.Row.Cells[l].Text == "-")
                {
                    e.Row.Cells[l].Text = "";
                  
                }
                //if (dtrowClr.Rows[indx][1].ToString().Substring(0, 2) == "MR")
                //{
                    if (e.Row.Cells[l].Text != "&nbsp;" && e.Row.Cells[l].Text != "0" && e.Row.Cells[l].Text != "-" && e.Row.Cells[l].Text != "")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[l].Text;
                        int prod = iLstProd[j];
                        string prodName = iLstProdName[j];
                        string sSf_code = dtrowClr.Rows[indx][1].ToString();
                        if (dtrowClr.Rows[indx][3].ToString() == "Grand Total")
                        {
                            e.Row.Cells[0].Text = "";
                            hLink.Attributes.Add("href", "javascript:showDR('admin','" + FMonth + "', '" + FYear + "',2,'','" + prod + "', '" + div_code + "','" + prodName + "')");
                        }
                        else
                        {
                            hLink.Attributes.Add("href", "javascript:showDR('" + sSf_code + "',  '" + FMonth + "', '" + FYear + "',1,'','" + prod + "', '" + div_code + "','" + prodName + "')");
                        }
                        //hLink.ToolTip = "Click here";
                        //hLink.ForeColor = System.Drawing.Color.Blue;
                        hLink.Style.Add("text-decoration", "none");
                        //hLink.Style.Add("color", "black");
                        hLink.Style.Add("cursor", "hand");
                        e.Row.Cells[l].Controls.Add(hLink);
                    }
                //}
                e.Row.Cells[l].Attributes.Add("align", "center");
                j++;
            }
            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
            }
            catch
            {
               // e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
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