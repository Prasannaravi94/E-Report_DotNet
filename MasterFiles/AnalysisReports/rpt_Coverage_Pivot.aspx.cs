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
using System.Text;


public partial class MasterFiles_AnalysisReports_rpt_Coverage_Pivot : System.Web.UI.Page
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
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDocMet = null;
    DataSet dsCov = null;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataSet dsUndoc = new DataSet();
    DataSet dsFw = new DataSet();
    DataSet dsField = new DataSet();
    DataSet dsNoFW = new DataSet();
    DataSet dsleave = new DataSet();
    DataSet dsCall = new DataSet();
    DataSet dsworkday = new DataSet();
    DataSet dsJwMet = new DataSet();
    DataSet dsJwSeen = new DataSet();
    DataSet dsdocseen = new DataSet();
    SalesForce dcrdoc = new SalesForce();
    List<int> iLstVstCnt = new List<int>();
    DataSet dschem = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    int imissed_dr = 0;
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {


        div_code = Session["div_code"].ToString();
        //sMode = Request.QueryString["cMode"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        FMonth = Request.QueryString["FMonth"].ToString();
        FYear = Request.QueryString["Fyear"].ToString();
        strmode = Request.QueryString["Vacant"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        SalesForce sf = new SalesForce();
        string strFrmMonth = sf.getMonthName(FMonth);
        //string strToMonth = sf.getMonthName(TMonth);
        lblHead.Text = "Coverage Analysis 1 - " + strFrmMonth + " " + FYear;
        LblForceName.Text = "Field Force Name : " + strFieledForceName;
        lblFrom.Text = strFieledForceName;
        txtsub.Text = "Coverage Analysis - " + strFrmMonth + " " + FYear;
        screen_name = "Coverage_Analysis_1";
        if (!Page.IsPostBack)
        {
            FillList();
            FillReport();

        }
       

    }
    private void FillList()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {

            lstFruits.DataTextField = "sf_name";
            lstFruits.DataValueField = "sf_code";
            lstFruits.DataSource = dsSalesForce;
            lstFruits.DataBind();

        }

    }
    private void FillReport()
    {
        // int months = (Convert.ToInt32(TYear) - Convert.ToInt32(FYear)) * 12 + Convert.ToInt32(TMonth) - Convert.ToInt32(FMonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        int cmonth = Convert.ToInt32(FMonth);
        int cyear = Convert.ToInt32(FYear);

        //int iMn = 0, iYr = 0;
        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));
        ////
        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}
        //
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        string sProc_Name = "";

        sProc_Name = "Coverage_Analysis";

        //string sqry = "EXEC visit_fixation_Cat " + Convert.ToInt32(div_code) + ",'" + ddlFieldForce.SelectedValue.ToString() + "',"+dtMnYr+"";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@cMnth", cmonth);
        cmd.Parameters.AddWithValue("@cYrs", cyear);
        cmd.Parameters.AddWithValue("@Vacant", strmode);
        cmd.CommandTimeout = 600;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        con.Close();
        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(7);
        dsts.Tables[0].Columns.RemoveAt(9);
        dsts.Tables[0].Columns.RemoveAt(1);
        //changes done by Vasanthi-Begin
        dsts.Tables[0].Columns["sf_name"].SetOrdinal(1);
        dsts.Tables[0].Columns["hq"].SetOrdinal(2);
        dsts.Tables[0].Columns["desg"].SetOrdinal(3);
        //End
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            if (dtrowClr.Rows[indx][1].ToString().Substring(0, 3) == "MGR" || dtrowClr.Rows[indx][1].ToString().ToLower().Trim() == "admin")
            {
            }

            for (int l = 8; l < e.Row.Cells.Count; l++)
            {
                int iDys = (e.Row.Cells[l + 6].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 6].Text);
                int iTtl_Drs = (e.Row.Cells[l].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l].Text);
                int iDrs_Mt = (e.Row.Cells[l + 1].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 1].Text);
                int iDrs_Sn = (e.Row.Cells[l + 10].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 10].Text);
                int iChem_Sn = (e.Row.Cells[l + 12].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 12].Text);
                int iDrs_Msd = iTtl_Drs - iDrs_Mt;
                e.Row.Cells[l + 3].Text = iDrs_Msd.ToString();
                int iJDys = (e.Row.Cells[l + 14].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 14].Text);
                int iJDrs_Seen = (e.Row.Cells[l + 16].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 16].Text);


                int iDrs_Rpt = (e.Row.Cells[l + 18].Text == "-") ? 0 : Convert.ToInt32(e.Row.Cells[l + 18].Text);
                if (iTtl_Drs != 0)
                {
                    e.Row.Cells[l + 2].Text = (Decimal.Divide((iDrs_Mt * 100), iTtl_Drs)).ToString("0.##");
                    e.Row.Cells[l + 19].Text = (Decimal.Divide((iDrs_Rpt * 100), iTtl_Drs)).ToString("0.##");
                    // e.Row.Cells[l + 9].Text = (Decimal.Divide((iDrs_Msd * 100), iTtl_Drs)).ToString("0.##");
                    //  e.Row.Cells[l + 9].Text = (Decimal.Divide((iDrs_Rpt * 100), iTtl_Drs)).ToString("0.##");
                }
                if (iDys != 0)
                {
                    e.Row.Cells[l + 11].Text = (Decimal.Divide(iDrs_Sn, iDys)).ToString("0.##");
                    e.Row.Cells[l + 13].Text = (Decimal.Divide(iChem_Sn, iDys)).ToString("0.##");

                }
                if (iJDys != 0)
                {
                    e.Row.Cells[l + 17].Text = (Decimal.Divide(iJDrs_Seen, iJDys)).ToString("0.##");
                }

                l += 20;
            }

            for (int i = 8, j = 0; i < e.Row.Cells.Count; i++)
            {

                if (e.Row.Cells[i].Text != "0" && e.Row.Cells[i].Text != "-")
                {/*
                    HyperLink hLnk = new HyperLink();
                    hLnk.Text = e.Row.Cells[i].Text;
                    hLnk.NavigateUrl = "#";
                    hLnk.ForeColor = System.Drawing.Color.Black;
                    hLnk.Font.Underline = false;
                    hLnk.ToolTip = "Click to View Details";
                    e.Row.Cells[i].Controls.Add(hLnk);*/
                }
                else if (e.Row.Cells[i].Text == "0")
                {
                    if (e.Row.Cells[i].Text == "0")
                    {
                        e.Row.Cells[i].Text = "-";
                        e.Row.Cells[i].Attributes.Add("style", "color:black;font-weight:normal;");
                    }
                }
                e.Row.Cells[i].Attributes.Add("align", "center");
            }


            //RP 
            int A = 9;
            int i1 = 18;
            //for (int i = 17, j = 0; i < e.Row.Cells.Count; i++)
            {
                //if (e.Row.Cells[i1].Text != "&nbsp;" && e.Row.Cells[i1].Text != "0" && e.Row.Cells[i1].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i1].Text;
                    hLink.Attributes.Add("class", "btnDrSn");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUp('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "2" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.Font.Underline = true;
                    hLink.ForeColor = System.Drawing.Color.Blue;
                    e.Row.Cells[i1].Controls.Add(hLink);
                }

                int i2 = 20;
                //for (int i = 17, j = 0; i < e.Row.Cells.Count; i++)

                // if (e.Row.Cells[i2].Text != "&nbsp;" && e.Row.Cells[i2].Text != "0" && e.Row.Cells[i2].Text != "-")
                {
                    HyperLink hLink = new HyperLink();
                    hLink.Text = e.Row.Cells[i2].Text;
                    hLink.Attributes.Add("class", "btnDrMt");
                    hLink.Attributes.Add("onclick", "javascript:showModalPopUpChmst('" + sf_code + "',  '" + FMonth + "', '" + FYear + "', '" + TMonth + "', '" + TYear + "','" + "2" + "','" + Request.QueryString["sf_name"].ToString() + "','" + Convert.ToString(dtrowClr.Rows[indx][1].ToString()) + "')");
                    hLink.ToolTip = "Click here";
                    hLink.Attributes.Add("style", "cursor:pointer");
                    hLink.Font.Underline = true;
                    hLink.ForeColor = System.Drawing.Color.Fuchsia;
                    e.Row.Cells[i2].Controls.Add(hLink);
                }
            }
            //RP  

            try
            {
                int j = Convert.ToInt32(e.Row.Cells[0].Text) - 1;
                e.Row.Attributes.Add("style", "background-color:" + "#" + Convert.ToString(dtrowClr.Rows[j][7].ToString()));
            }
            catch
            {
                e.Row.Attributes.Add("style", "background-color:" + "#FFFFFF");
            }

            #endregion
            //
            //e.Row.Cells[1].Wrap = false;
            e.Row.Cells[2].Wrap = false;
            e.Row.Cells[3].Wrap = false;
            e.Row.Cells[4].Wrap = false;
            e.Row.Cells[5].Wrap = false;
            e.Row.Cells[6].Wrap = false;
            e.Row.Cells[7].Wrap = false;
          
        }

        Chemist chem = new Chemist();

        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
        if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("column_name"),
                             Ch_Code = data.Field<string>("column_name")
                         };
            var listOfGrades = result.ToList();
            cblGridColumnList.Visible = true;
            cblGridColumnList.DataSource = listOfGrades;
            cblGridColumnList.DataTextField = "Ch_Name";
            cblGridColumnList.DataValueField = "Ch_Code";
            cblGridColumnList.DataBind();

            string headerText = string.Empty;

            for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
            {
                headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

                System.Web.UI.WebControls.ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                if (ddl != null)
                {
                    if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                    }
                    else
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                    }
                    //if (headerText == "FieldForce Name" || headerText == "HQ" || headerText == "Designation Name")
                    //{
                    //    cblGridColumnList.Items.FindByValue(headerText).Enabled = false;
                    //}
                }

                if (!Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                {
                    int j = i + 4;

                    e.Row.Cells[j].Visible = false;
                }
            }
        }
    }
    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
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

            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "FieldForce Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "HQ", "#0097AC", true);
           

            AddMergedCells(objgridviewrow, objtablecell, 0, "Design Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp.Code", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "DOJ", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "First Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Second Level Manager", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Call Details", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 5, "Attendance", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Summary", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 4, "Joint Work", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 2, "Repeated Calls", "#0097AC", true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);

            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell objtablecell2 = new TableCell();
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Master List Doctors", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Doctors Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Coverage (%)", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Listed Drs Missed", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "UnListed Drs Met", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Worked", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Field", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Half Day Work", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days Non Field", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days On Leave", "#0097AC", false);


            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Doctors Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Chemist Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);

            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Days", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Calls Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Calls Seen", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Call Average", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Met", "#0097AC", false);
            AddMergedCells(objgridviewrow2, objtablecell2, 0, "Coverage (%)", "#0097AC", false);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
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
            objtablecell.RowSpan = 2;
        }
        if (objgridviewrow.RowIndex == 1)
        {
            objtablecell.Attributes.Add("class", "stickyFirstRow");
        }
        else
        {
            objtablecell.Attributes.Add("class", "stickySecondRow");
        }
        //objtablecell.Style.Add("background-color", backcolor);
        //objtablecell.Style.Add("color", "white");
        //objtablecell.Style.Add("border-color", "black");
        //objtablecell.Style.Add("border-color","#b8c5d1"); 
        objtablecell.HorizontalAlign = HorizontalAlign.Center;


        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);

        if ( objtablecell.Text == "Emp.Code" || objtablecell.Text == "DOJ" || objtablecell.Text == "First Level Manager" || objtablecell.Text == "Second Level Manager")
        {

            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideInsert(screen_name, objtablecell.Text, sf_code, true,4);
        }

        dsGridShowHideColumn1 = chem.GridColumnShowHideGet1(screen_name, objtablecell.Text, sf_code);
        if (dsGridShowHideColumn1.Tables[0].Rows.Count > 0)
        {
            if (dsGridShowHideColumn1.Tables[0].Rows[0]["visible"].ToString() == "False")
            {
                //objtablecell.Visible = false;
                objtablecell.Style.Add("display", "none");
            }
        }

        objgridviewrow.Cells.Add(objtablecell);
    }


    protected void Submit(object sender, EventArgs e)
    {
        string message = "";
        foreach (System.Web.UI.WebControls.ListItem item in lstFruits.Items)
        {
            if (item.Selected)
            {
                message += item.Text + " " + item.Value + "\\n";
            }
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('" + message + "');", true);
    }
    protected void btnSend_Onclick(object sender, EventArgs e)
    {
        int iReturn = -1;
        string Mr_Name = "";
        string Mr_code = "";
        foreach (System.Web.UI.WebControls.ListItem item in lstFruits.Items)
        {
            if (item.Selected)
            {
                Mr_Name += item.Text + ",";
                Mr_code += item.Value + ",";

            }
        }
        AdminSetup adm = new AdminSetup();

        dsMailCompose = adm.ComposeMail(Request.QueryString["sf_code"].ToString(), Mr_code, txtsub.Text.Trim(), txtMessage.Text.Trim(), "", "", "", Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "", "", Request.QueryString["sf_name"].ToString(), Mr_Name);
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mail has been sent successfully');</script>");
        }
    }
   
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && sf_code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, sf_code);
        }

        Response.Redirect(Request.RawUrl);
    }

   
}