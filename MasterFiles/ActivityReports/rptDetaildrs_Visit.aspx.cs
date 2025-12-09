using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class MasterFiles_Reports_rptExp_Consolidated_View : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = new DataSet();
    string divcode = string.Empty;
    string sfcode = string.Empty;
    DataSet dsSF = new DataSet();

    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string Fmode = string.Empty;
    List<string> Selval = new List<string>();
    string selValues = string.Empty;

    List<int> iLstMonth = new List<int>();
    List<int> iLstYear = new List<int>();
    DataTable dtrowClr = new System.Data.DataTable();

    string strMonth = string.Empty;
    string strYear = string.Empty;
    string strFieldForce = string.Empty;
    string h = string.Empty;
    //int StrVac = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)

    {

        strFieldForce = Request.QueryString["sf_name"].ToString();
        strMonth = Request.QueryString["Frm_Month"].ToString();
        strYear = Request.QueryString["Frm_year"].ToString();
        sfcode = Request.QueryString["sf_code"].ToString();
        Fmode = Request.QueryString["mode"].ToString();
        selValues = Request.QueryString["selValues"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        hidmnth.Value = Request.QueryString["Frm_Month"].ToString();
        hidyr.Value = Request.QueryString["Frm_year"].ToString();
        hiddiv.Value = Convert.ToString(Session["div_code"]);
        //StrVac = Convert.ToInt16(Request.QueryString["StrVac"].ToString());
        Distance_calculation dv = new Distance_calculation();
        DataTable ds = dv.getFieldForce(divcode, sfcode);

        lblHead.Text = "Detailing Drs-Visit wise for the Month of " + getMonthName(Convert.ToInt16(strMonth)) + " - " + strYear.ToString();

        lblFieldForceName.Text = "Field Force Name : " + strFieldForce;
        lblHQ.Text = "HQ : " + ds.Rows[0]["sf_hq"].ToString();
        lblDesig.Text = "Designation : " + ds.Rows[0]["sf_designation_short_name"].ToString();

        dsSubDivision = dv.getFilter_record(divcode, sfcode, strMonth, strYear, Fmode, selValues.Replace("'", ""));
        dtrowClr = dsSubDivision.Tables[0].Copy();

        DataTable mainTable = dsSubDivision.Tables[0];


        string strRbnValue = string.Empty;
        string strRbnText = string.Empty;


        if (strRbnValue.Contains(","))
        {
            strRbnValue = strRbnValue.Remove(strRbnValue.Length - 1);
            strRbnText = strRbnText.Remove(strRbnText.Length - 1);
        }

        if (mainTable.Rows.Count > 0)
        {

            if (strRbnText != "")
            {
                DataSet ds1 = new DataSet();

                mainTable.DefaultView.RowFilter = " status in ('" + strRbnText + "') ";

                //break;
            }
            //mainTable.Columns.Remove("sf_code");
            mainTable.Columns.Remove("sf_code");
            //mainTable.Columns.Remove("product_code");
            mainTable.Columns.Remove("sf_code1");
            mainTable.Columns.Remove("clr");
            //mainTable.Tables[0].Columns.RemoveAt(0);

            grdExpense.Visible = true;
            grdExpense.DataSource = mainTable;
            grdExpense.DataBind();

        }
        else
        {
            grdExpense.DataSource = dsSubDivision;
            grdExpense.DataBind();
            //GrdViewExpense.DataSource = dsSubDivision;
            //GrdViewExpense.DataBind();
        }
    }
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "January";
        }
        else if (iMonth == 2)
        {
            sReturn = "February";
        }
        else if (iMonth == 3)
        {
            sReturn = "March";
        }
        else if (iMonth == 4)
        {
            sReturn = "April";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "August";
        }
        else if (iMonth == 9)
        {
            sReturn = "September";
        }
        else if (iMonth == 10)
        {
            sReturn = "October";
        }
        else if (iMonth == 11)
        {
            sReturn = "November";
        }
        else if (iMonth == 12)
        {
            sReturn = "December";
        }
        return sReturn;
    }

    //added by gowsi

    protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    {
        objtablecell = new TableCell();
        objtablecell.Text = celltext;
        objtablecell.ColumnSpan = colspan;
        if ((colspan == 0) && bRowspan)
        {
            objtablecell.RowSpan = 2;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void grdExpense_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "Sno", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Head Quater", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Employee Code", "#0097AC", true);

            ListedDR LstDR = new ListedDR();
            dsSF = LstDR.Get_DetailDrs_Visit_brand(sf_code, divcode, selValues.Replace("'", ""), Fmode);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();

            if (dsSF.Tables[0].Rows.Count > 0)
            {
                grdExpense.DataSource = dsSF;
                //grdExpense.DataBind();
                int a = dsSF.Tables[0].Rows.Count - 1;
                for (int i = 0; i <= a; i++)
                {
                    AddMergedCells(objgridviewrow, objtablecell, 4, dsSF.Tables[0].Rows[i][0].ToString(), "#0097AC", true);

                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "No of Visit Drs", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "1Visit", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "2Visit", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, "More than 2Visit", "#0097AC", false);

                }
            }

            //int months = 1;          
            //int count = selValues.Count();           

            //if (months >= 0)
            //{

            //    for (int j = 1; j <= months + 1; j++)
            //    {
            //        //iLstMonth.Add(cmonth);
            //        //iLstYear.Add(cyear);
            //        //AddMergedCells(objgridviewrow, objtablecell, 0, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "#0097AC", true);

            //        TableCell objtablecell2 = new TableCell();
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, "No of Visit Drs", "#0097AC", false);
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, "1V", "#0097AC", false);
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, "2V", "#0097AC", false);
            //        AddMergedCells(objgridviewrow2, objtablecell2, 0, "More 2V", "#0097AC", false);

            //        // AddMergedCells(objgridviewrow2, objtablecell2, 0, "Missed", "#0097AC", false);
            //        // iLstVstmnt.Add(cmonth1);
            //        // iLstVstyr.Add(cyear1);


            //    }

            //}
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }

    protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;

            int mnth = Convert.ToInt32(Request.QueryString["Frm_Month"].ToString());
            int Yr = Convert.ToInt32(Request.QueryString["Frm_year"].ToString());

            for (int l1 = 1; l1 < 2; l1++)
            {
                if (e.Row.RowIndex == dtrowClr.Rows.Count)
                {

                    e.Row.Cells[l1].Text = "Grand Total";
                    e.Row.Cells[l1].Attributes.Add("style", "color:red;font-weight:Bold;");

                }
                l1++;
            }

            for (int i = 5; i < e.Row.Cells.Count; i++)
            {
                //added by preethi
                if (dtrowClr.Rows.Count != indx)
                {
                    if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "" && e.Row.Cells[i].Text != "&nbsp;")
                    {
                        HyperLink hLink = new HyperLink();
                        hLink.Text = e.Row.Cells[i].Text;

                        if (dtrowClr.Rows[indx][1].ToString().Contains("MR")|| dtrowClr.Rows[indx][1].ToString().Contains("Grand Total"))
                        {
                            string headerRowText = grdExpense.HeaderRow.Cells[i].Text;
                            string[] split = headerRowText.Split('_');
                            //int d = Convert.ToInt32(split[0]);

                            string selValuesOrder= String.Join(",", selValues.Replace("'", "")
                            .Split(',')
                            .Select(x => int.Parse(x))
                            .OrderBy(x => x));
                            string[] headerOrder = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                "A", "B", "C", "D", "E", "F","G", "H", "I", "J", "K", "L","M", "N", "O", "P", "Q", "R","S", "T","U", "V", "W","X", "Y", "Z",
                                "Z1","Z2","Z3","Z4","Z5","Z6","Z7","Z8","Z9","Z91","Z92","Z93","Z94","Z95","Z96",
                                "Z97","Z98","Z99"
                            };

                            for (int u = 0; u <= selValuesOrder.Replace("'", "").Split(',').Count(); u++)
                            {
                                if (split[0].Trim().TrimStart('0') == headerOrder[u].ToString())
                                {
                                    string[] g = selValuesOrder.Replace("'", "").Split(',');
                                    h = g[u];
                                }
                            }

                            hLink.Attributes.Add("class", "btnDrSn");
                            hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + dtrowClr.Rows[indx][1].ToString() + "', '" + divcode + "', '" + mnth + "', '" + Yr + "', '" + Fmode + "','" + h + "','" + Convert.ToString(dtrowClr.Rows[indx][2].ToString()) + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                            hLink.ToolTip = "Click here";
                            hLink.Attributes.Add("style", "cursor:pointer");
                            hLink.Font.Underline = true;
                            hLink.ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Controls.Add(hLink);
                        }
                    }
                }
                //if (Request.QueryString["mode"].ToString() == "1")
                //{
                e.Row.Cells[i + 4].Visible = false;
                //}
                string sSf_code = dtrowClr.Rows[indx][1].ToString();
                string sSf_name = dtrowClr.Rows[indx][2].ToString();

                if (e.Row.Cells[i].Text == "0")
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                if (e.Row.Cells[i + 1].Text == "0")
                {
                    if (e.Row.Cells[i + 1].Text == "0")
                    {
                        e.Row.Cells[i + 1].Text = "-";
                        e.Row.Cells[i + 1].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                if (e.Row.Cells[i + 2].Text == "0")
                {
                    if (e.Row.Cells[i + 2].Text == "0")
                    {
                        e.Row.Cells[i + 2].Text = "-";
                        e.Row.Cells[i + 2].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                if (e.Row.Cells[i + 3].Text == "0")
                {
                    if (e.Row.Cells[i + 3].Text == "0")
                    {
                        e.Row.Cells[i + 3].Text = "-";
                        e.Row.Cells[i + 3].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
                try
                {
                    int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                    e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
                }
                catch
                {
                    e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
                }
                e.Row.Cells[1].Wrap = false;
                e.Row.Cells[2].Wrap = false;
                e.Row.Cells[3].Wrap = false;
                e.Row.Cells[4].Wrap = false;
                e.Row.Cells[5].Wrap = false;
                e.Row.Cells[6].Wrap = false;
                e.Row.Cells[7].Wrap = false;
                e.Row.Cells[8].Wrap = false;
            
                i = i + 4;

            }

        }
    }

}