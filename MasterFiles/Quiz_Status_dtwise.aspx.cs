using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Windows;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Net;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
public partial class MIS_Reports_Quiz_Status_dtwise : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string strMultiDiv = string.Empty;
    DataSet ds = new DataSet();
    string FMonth = string.Empty;
    string FYear = string.Empty;
    DataTable dtrowClr = new DataTable();
    DataTable dtrowMgr = new DataTable();
    DataTable dtsf_code = new DataTable();
    DataTable dtsf_code_Mgr = new DataTable();
   
    DataSet dsdays = new DataSet();
    List<int> iLstVstday = new List<int>();
    string strFieledForceName = string.Empty;
    DataSet dsday = new DataSet();
    string day = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            div_code = Session["div_code"].ToString();
            //sMode = Request.QueryString["cMode"].ToString();
            sf_code = Request.QueryString["sf_code"].ToString();
            FMonth = Request.QueryString["FMonth"].ToString();
            FYear = Request.QueryString["Fyear"].ToString();
            day = Request.QueryString["days"];
            strFieledForceName = Request.QueryString["sf_name"].ToString();
            SalesForce sf = new SalesForce();
            string strFrmMonth = sf.getMonthName(FMonth);
            lblHead.Text = "Quiz Result - " + strFrmMonth + " " + FYear;
            LblForceName.Text = "Field Force Name : " + strFieledForceName;
            FillMgr();
        }

    }
   
   
    private void FillMgr()
    {
        int months = (Convert.ToInt32(FYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(FMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

      

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        //
        string mnt = Convert.ToString(months);
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
        if (day == "ALL")
        {
            DataTable SfCodes = sf1.getMonth_Day(FMonth);
            dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
            dtsf_code.Columns["INX"].AutoIncrementStep = 1;
            dtsf_code.Columns.Add("day");
            for (int i = 0; i < SfCodes.Rows.Count; i++)
            {
                //j += 1;
                //dtsf_code.Rows.Add(j.ToString());

                dtsf_code.Rows.Add(null, SfCodes.Rows[i]["day"]);
            }
            dsday.Tables.Add(dtsf_code);
        }
        else
        {

            DataTable dtDays = new DataTable();
            dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
            dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
            dtsf_code.Columns["INX"].AutoIncrementStep = 1;
            dtsf_code.Columns.Add("day");

            string dd = day.Trim();
            dd = day.Remove(day.Length - 1);

            string[] dayss = { dd };

            dayss = dd.Split(',');

            foreach (string d in dayss)
            {
                dtsf_code.Rows.Add(null, d.ToString());
            }
            dsday.Tables.Add(dtsf_code);
        }
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Quiz_Result_Month_DaywiseNewFF";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dstsmgr = new DataSet();
        da.Fill(dstsmgr);
        con.Close();
        dtrowMgr = dstsmgr.Tables[0].Copy();
        dstsmgr.Tables[0].Columns.RemoveAt(11);
        dstsmgr.Tables[0].Columns.RemoveAt(8);
        dstsmgr.Tables[0].Columns.RemoveAt(1);

        GrdFixation.DataSource = dstsmgr;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            int tmp = 0;
            LinkButton lnk_btn = new LinkButton();
            for (int l = 9, j = 0; l < e.Row.Cells.Count - 4; l++)
            {
                int iQus = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int imark = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
                if (imark != 0)
                {
                    e.Row.Cells[l + 2].Text = (Decimal.Divide((imark * 100), iQus)).ToString("0.##");
                    int fday = iLstVstday[j];
                    //if (div_code != "25" && div_code != "80" || sf_type =="3")
                    //{
                    HyperLink hLink = new HyperLink();
                    hLink.Text = (e.Row.Cells[l + 1].Text);
                    hLink.Attributes.Add("href", "javascript:showQuizDetails('" + dtrowMgr.Rows[indx][1].ToString() + "','" + div_code + "','" + strFieledForceName + "', '" + FMonth + "','" + FYear + "',1,'" + fday + "')");
                    hLink.Style.Add("text-decoration", "Underline");
                    hLink.Style.Add("color", "Blue");
                    hLink.Style.Add("cursor", "hand");
                    e.Row.Cells[l + 1].Controls.Add(hLink);
                    // }
                }



                l += 2;
                j++;
            }

            //#region old

            //if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 1].Text == "")
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 1].Text = "0";
            //}
            //if (e.Row.Cells[e.Row.Cells.Count - 2].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 2].Text == "")
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 2].Text = "0";
            //}
            //if (e.Row.Cells[e.Row.Cells.Count - 3].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 3].Text == "")
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 3].Text = "0";
            //}



            //e.Row.Cells[e.Row.Cells.Count - 1].Text = (Decimal.Divide((Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 2].Text) * 100), (Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 3].Text)))).ToString("0") + "%";


            //if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "0%")
            //{
            //    e.Row.Cells[e.Row.Cells.Count - 1].Text = "";
            //}
            //#endregion


            if (e.Row.Cells[e.Row.Cells.Count - 2].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 2].Text == "")
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = "0";
            }
            if (e.Row.Cells[e.Row.Cells.Count - 3].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 3].Text == "")
            {
                e.Row.Cells[e.Row.Cells.Count - 3].Text = "0";
            }
            if (e.Row.Cells[e.Row.Cells.Count - 4].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 4].Text == "")
            {
                e.Row.Cells[e.Row.Cells.Count - 4].Text = "0";
            }
            if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "-" || e.Row.Cells[e.Row.Cells.Count - 1].Text == "")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "0";
            }
            if (e.Row.Cells[e.Row.Cells.Count - 3].Text != "0" && e.Row.Cells[e.Row.Cells.Count - 4].Text != "0")
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = (Decimal.Divide((Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 3].Text) * 100), (Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 4].Text)))).ToString("0") + "%";
            }
            if ((e.Row.Cells[e.Row.Cells.Count - 1].Text) != "0")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = (Decimal.Divide((Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 3].Text)), (Convert.ToInt32(e.Row.Cells[e.Row.Cells.Count - 1].Text)))).ToString("0");
            }

            if (e.Row.Cells[e.Row.Cells.Count - 2].Text == "0%")
            {
                e.Row.Cells[e.Row.Cells.Count - 2].Text = "";
            }
            if (e.Row.Cells[e.Row.Cells.Count - 1].Text == "0%")
            {
                e.Row.Cells[e.Row.Cells.Count - 1].Text = "";
            }




            for (int l = 9, j = 0; l < e.Row.Cells.Count; l++)
            {
                if (e.Row.Cells[l].Text == "0" || e.Row.Cells[l].Text == "-")
                {
                    e.Row.Cells[l].Text = "";
                }


                e.Row.Cells[l].Attributes.Add("style", "font-weight:bold;font-size:12pt;color:black;border-color:black;text-align:center");

            }
            try
            {

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

        }

    }



    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            //Creating a gridview row object
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();
            TableCell objtablecell2 = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "S.No", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Emp.Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "DOJ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Designation Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "HQ", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "StateName", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "First Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, 0, "Second Level Manager", "#0097AC", true);
            if (day == "ALL")
            {
                SalesForce sf = new SalesForce();
                dsdays = sf.getMonth_Day_Dt(FMonth, FYear);
                for (int w = 0; w < dsdays.Tables[0].Rows.Count; w++)
                {
                    int iCnt = 0;
                    //  iColSpan = ((dsmgrsf.Tables[0].Rows.Count)) * 2;

                    //  TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow, objtablecell, 0, 3, dsdays.Tables[0].Rows[w]["date"].ToString() + " - " + dsdays.Tables[0].Rows[w]["day_name"].ToString(), "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Question.", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total of Correct answers", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Marks in (%) ", "#0097AC", false);
                    iLstVstday.Add(Convert.ToInt32(dsdays.Tables[0].Rows[w]["date"].ToString()));
                }
            }
            else
            {
                for (int w = 0; w < dsday.Tables[0].Rows.Count; w++)
                {
                    int iCnt = 0;
                    //  iColSpan = ((dsmgrsf.Tables[0].Rows.Count)) * 2;

                    //  TableCell objtablecell2 = new TableCell();
                    AddMergedCells(objgridviewrow, objtablecell, 0, 3, dsday.Tables[0].Rows[w]["day"].ToString(), "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Question.", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total of Correct answers", "#0097AC", false);
                    AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Marks in (%) ", "#0097AC", false);
                    iLstVstday.Add(Convert.ToInt32(dsday.Tables[0].Rows[w]["day"].ToString()));
                }

            }
            AddMergedCells(objgridviewrow, objtablecell, 0, 4, "Month Total", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Questions", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Total Of Corrected Answers", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "Marks in %", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, 0, "% Average", "#0097AC", false);


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
        //objtablecell.Font.Size = 12;
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("font-weight", "bold");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.VerticalAlign = VerticalAlign.Middle;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }

}