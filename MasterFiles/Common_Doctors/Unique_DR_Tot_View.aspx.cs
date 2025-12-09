using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Net;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class MasterFiles_Common_Doctors_Unique_DR_Tot_View : System.Web.UI.Page
{
    DataTable dtrowClr = new DataTable();
    DataSet dscnt = new DataSet();
    DataSet dsUnique = new DataSet();
    DataSet dsNew = new DataSet();
    DataSet dsPen = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = Page.Title;
        // menu1.FindControl("btnBack").Visible = false;
        FillReport();
        FillReport_NewUnique();
        FillDrCnt();
    }
    private void FillDrCnt()
    {
        ListedDR lst = new ListedDR();
        //dscnt = lst.tot_unique();
        //if (dscnt.Tables[0].Rows.Count > 0)
        //{
        //    lblUni.Text = dscnt.Tables[0].Rows[0]["tot"].ToString();
        //}
        dsUnique = lst.Selected_unique();
        if (dsUnique.Tables[0].Rows.Count > 0)
        {
            lblSelect.Text = dsUnique.Tables[0].Rows[0]["uni"].ToString();
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(" select count(*) as tot from mas_common_drs where C_Active_Flag=0 and New_Unique='' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(dscnt);
        con.Close();
        if (dscnt.Tables[0].Rows.Count > 0)
        {
            lblUni.Text = dscnt.Tables[0].Rows[0]["tot"].ToString();
        }

        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd2 = new SqlCommand(" select count(*) as New from mas_common_drs where C_Active_Flag=0 and New_Unique='N' ", con2);
        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
        DataSet ds2 = new DataSet();
        da2.Fill(dsNew);
        con.Close();
        if (dsNew.Tables[0].Rows.Count > 0)
        {
            lblNew.Text = dsNew.Tables[0].Rows[0]["New"].ToString();
        }
       int tot = Convert.ToInt32(lblUni.Text) + Convert.ToInt32(lblNew.Text);
       lbltot.Text = Convert.ToString(tot);
        //SqlConnection con3 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //con.Open();
        //SqlCommand cmd3 = new SqlCommand(" select count(*) as Pen from mas_listeddr_One where listeddr_active_flag in (2,5) ", con2);
        //SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
        //DataSet ds3 = new DataSet();
        //da3.Fill(dsPen);
        //con.Close();
        //if (dsPen.Tables[0].Rows.Count > 0)
        //{
        //    lblpen.Text = dsPen.Tables[0].Rows[0]["Pen"].ToString();
        //}
    }
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "DR_count_View";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);

        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdFixation.DataSource = dsts;
        GrdFixation.DataBind();


    }
    private void FillReport_NewUnique()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";


        sProc_Name = "DR_count_New_Unique_View";
        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet dsts = new DataSet();
        da.Fill(dsts);

        dtrowClr = dsts.Tables[0].Copy();
        dsts.Tables[0].Columns.RemoveAt(2);
        dsts.Tables[0].Columns.RemoveAt(1);
        GrdNewUnique.DataSource = dsts;
        GrdNewUnique.DataBind();


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

            #region Merge cells

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Existing + New drs (Approved)", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Pending Drs (Existing)", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "Admin Pending Drs (New)", "#0097AC", true);

            AddMergedCells(objgridviewrow, objtablecell, 0, "Total Drs (New + Existing) incl. Pending", "#0097AC", true);

            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            //  objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            #endregion
        }
    }
    protected void GrdNewUnique_RowCreated(object sender, GridViewRowEventArgs e)
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

            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Division Name", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total TM's Raised", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "RM's Pending", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Admin Pending", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total Rejected", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "RM's Rejected", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Admin Rejected", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Total Approved", "#0097AC", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Existing + New Drs (Approved)", "#0097AC", true);
            //Lastly add the gridrow object to the gridview object at the 0th position
            //Because, the header row position is 0.
            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);


            //  objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
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
            objtablecell.RowSpan = 1;
        }
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("border-color", "black");
        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objgridviewrow.Cells.Add(objtablecell);
    }

    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int indx = e.Row.RowIndex;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (dtrowClr.Rows[indx][0].ToString() == "0")
            {
                for (int l = 0, j = 0; l < e.Row.Cells.Count; l++)
                {
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                }
            }
        }
    }
    protected void GrdNewUnique_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int indx = e.Row.RowIndex;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (dtrowClr.Rows[indx][0].ToString() == "0")
            {
                for (int l = 0, j = 0; l < e.Row.Cells.Count; l++)
                {
                    e.Row.Attributes.Add("style", "background-color:LightBlue;font-bold:true; font-size:14px; Color:Red; border-color:Black");
                    e.Row.Cells[0].Text = "";
                }
            }
        }
    }

}