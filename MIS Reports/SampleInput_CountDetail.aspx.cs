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
using System.Drawing;

public partial class MIS_Reports_SampleInput_CountDetail : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = new DataSet();
    string divcode = string.Empty;
    string sfcode = string.Empty;
    DataSet dsSF = new DataSet();

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string FYear = string.Empty;
    string mode = string.Empty;
    List<string> Selval = new List<string>();
    string selValues = string.Empty;

    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    //DataTable dtrowClr = new System.Data.DataTable();

    string strMonth = string.Empty;
    string strYear = string.Empty;
    string div_name = string.Empty;
    string h = string.Empty;
    DataTable dtrowClr = new  DataTable();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();

    //int StrVac = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        div_name = Request.QueryString["Div_name"].ToString();
        div_code = Request.QueryString["Div_code"].ToString();
        mode = Request.QueryString["mode"].ToString();
        lblHead.Text = "Division Name : " + div_name;
        if (!Page.IsPostBack)
        {
            GrdFFcountdetail();
        }
        
    }
    private void GrdFFcountdetail()
    {
        
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        if (mode == "S")
        {
            sProc_Name = "SampleInput_Not_At_All_Users";
        }
        else if (mode == "I")
        {
            sProc_Name = "SampleInput_Not_At_All_Users_I";
        }
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        //cmd.Parameters.AddWithValue("@Mode", mode);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
       // dsts.Tables[0].Columns.Remove("Division_code");
        dsts.Tables[0].Columns.Remove("Sf_code");
        dsts.Tables[0].Columns.Remove("Desig_Color");
        if (mode == "S")
        {
            dsts.Tables[0].Columns.Remove("sample");
        }
        if(mode == "I")
        {
            dsts.Tables[0].Columns.Remove("Input");
        }        
       // dsts.Tables[0].Columns.Remove("Ord");
        GrdFFcount.DataSource = dsts;
        GrdFFcount.DataBind();
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

    protected void GrdFFcount_RowCreated(object sender, GridViewRowEventArgs e)
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

          //AddMergedCells(objgridviewrow, objtablecell, 0, "Sno", "#0097AC", true);

          //  AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting To MGR1", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting To MGR2", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Reporting To MGR3", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "status", "#0097AC", true);



            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            // objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }
    protected void GrdFFcount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int s = 0;

        
            for (int i = 6, j = 0; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text == "1")
                {
                    e.Row.Cells[i].Text = "Yes";
                    e.Row.Cells[i].Attributes.Add("style", "color:Green;font-weight:normal;");
                }
                else
                {
                    e.Row.Cells[i].Text = "-";
                    e.Row.Cells[i].Attributes.Add("style", "color:Red;font-weight:normal;");
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }
        }
        //for (int i1 = 7; i1 < e.Row.Cells.Count; i1++)
        //{
        //    e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
        //    if (e.Row.Cells[i1].Text == "0" || e.Row.Cells[i1].Text == "&nbsp;" || e.Row.Cells[i1].Text == "")
        //    {
        //        e.Row.Cells[i1].Text = "-";
        //        //e.Row.Cells[i1].Attributes.Add("style", "color:black;font-weight:normal;");
        //    }
        //    e.Row.Cells[i1].Attributes.Add("align", "center");

        //}
    }

    //protected void GrdFFcount_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        int indx = e.Row.RowIndex;
    //        int k = e.Row.Cells.Count - 5;

    //        int mnth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
    //        int Yr = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());

    //        for (int l1 = 1; l1 < 2; l1++)
    //        {
    //            if (e.Row.RowIndex == dtrowClr.Rows.Count)
    //            {

    //                e.Row.Cells[l1].Text = "Grand Total";
    //                e.Row.Cells[l1].Attributes.Add("style", "color:red;font-weight:Bold;");

    //            }
    //            l1++;
    //        }

    //        for (int i = 5; i < e.Row.Cells.Count; i++)
    //        {
    //            //added by preethi
    //            if (dtrowClr.Rows.Count != indx)
    //            {
    //                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text != "&nbsp;")
    //                {
    //                    HyperLink hLink = new HyperLink();
    //                    hLink.Text = e.Row.Cells[i].Text;

    //                    if (dtrowClr.Rows[indx][1].ToString().Contains("MR")|| dtrowClr.Rows[indx][1].ToString().Contains("Grand Total"))
    //                    {
    //                        string headerRowText = GrdFFcount.HeaderRow.Cells[i].Text;
    //                        string[] split = headerRowText.Split('_');
    //                        //int d = Convert.ToInt32(split[0]);

    //                        string selValuesOrder= String.Join(",", selValues.Replace("'", "")
    //                        .Split(',')
    //                        .Select(x => int.Parse(x))
    //                        .OrderBy(x => x));
    //                        string[] headerOrder = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9",
    //                            "A", "B", "C", "D", "E", "F","G", "H", "I", "J", "K", "L","M", "N", "O", "P", "Q", "R","S", "T","U", "V", "W","X", "Y", "Z",
    //                            "Z1","Z2","Z3","Z4","Z5","Z6","Z7","Z8","Z9","Z91","Z92","Z93","Z94","Z95","Z96",
    //                            "Z97","Z98","Z99"
    //                        };

    //                        for (int u = 0; u <= selValuesOrder.Replace("'", "").Split(',').Count(); u++)
    //                        {
    //                            if (split[0].Trim().TrimStart('0') == headerOrder[u].ToString())
    //                            {
    //                                string[] g = selValuesOrder.Replace("'", "").Split(',');
    //                                h = g[u];
    //                            }
    //                        }

    //                        hLink.Attributes.Add("class", "btnDrSn");
    //                        hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + dtrowClr.Rows[indx][1].ToString() + "', '" + divcode + "', '" + mnth + "', '" + Yr + "', '" + Fmode + "','" + h + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
    //                        hLink.ToolTip = "Click here";
    //                        hLink.Attributes.Add("style", "cursor:pointer");
    //                        hLink.Font.Underline = true;
    //                        hLink.ForeColor = System.Drawing.Color.Blue;
    //                        e.Row.Cells[i].Controls.Add(hLink);
    //                    }
    //                }
    //            }
    //            //if (Request.QueryString["mode"].ToString() == "1")
    //            //{
    //            e.Row.Cells[i + 4].Visible = false;
    //            //}
    //            string sSf_code = dtrowClr.Rows[indx][1].ToString();
    //            string sSf_name = dtrowClr.Rows[indx][2].ToString();

    //            if (e.Row.Cells[i].Text == "0")
    //            {
    //                if (e.Row.Cells[i].Text == "0")
    //                {
    //                    e.Row.Cells[i].Text = "-";
    //                    e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
    //                }
    //            }
    //            if (e.Row.Cells[i + 1].Text == "0")
    //            {
    //                if (e.Row.Cells[i + 1].Text == "0")
    //                {
    //                    e.Row.Cells[i + 1].Text = "-";
    //                    e.Row.Cells[i + 1].Attributes.Add("style", "color:black;font-weight:normal;");
    //                }
    //            }
    //            if (e.Row.Cells[i + 2].Text == "0")
    //            {
    //                if (e.Row.Cells[i + 2].Text == "0")
    //                {
    //                    e.Row.Cells[i + 2].Text = "-";
    //                    e.Row.Cells[i + 2].Attributes.Add("style", "color:black;font-weight:normal;");
    //                }
    //            }
    //            if (e.Row.Cells[i + 3].Text == "0")
    //            {
    //                if (e.Row.Cells[i + 3].Text == "0")
    //                {
    //                    e.Row.Cells[i + 3].Text = "-";
    //                    e.Row.Cells[i + 3].Attributes.Add("style", "color:black;font-weight:normal;");
    //                }
    //            }
    //            e.Row.Cells[i].Attributes.Add("align", "center");
    //            try
    //            {
    //                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
    //                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
    //            }
    //            catch
    //            {
    //                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
    //            }
    //            e.Row.Cells[1].Wrap = false;
    //            e.Row.Cells[2].Wrap = false;
    //            e.Row.Cells[3].Wrap = false;
    //            e.Row.Cells[4].Wrap = false;
    //            e.Row.Cells[5].Wrap = false;
    //            e.Row.Cells[6].Wrap = false;
    //            e.Row.Cells[7].Wrap = false;
    //            e.Row.Cells[8].Wrap = false;

    //            i = i + 4;

    //        }

    //    }
    //}

}