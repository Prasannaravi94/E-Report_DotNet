//
#region Assembly
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
#endregion
//
//
public partial class MasterFiles_Calendar_Consolidated : System.Web.UI.Page
{
    //
    #region Variables
    DataSet dsHoliday = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string state_code = string.Empty;
    DataSet dsState = null;   
    string sState = string.Empty;
    string slno;   
    string state_cd = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    DataSet dsTP = null;
    #endregion
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        //
        #region variables
        State st = new State();
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        Session["backurl"] = "HolidayList.aspx";
        #endregion
        //
        dsHoliday = st.getState();
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            state_code = dsHoliday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            FillState(div_code);
        }
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == null || Session["sf_type"].ToString() == "")
        {
            UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //// c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
        }
    }
    //
    #region FillState dropdownlist and Show GridView 
    private void FillState(string div_code)
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStcode(state_cd);
            string[] stateCount;
            stateCount = state_cd.Split(',');
            bool bNoRecordsFound = true;
            for (int j = 0; j < dsState.Tables[0].Rows.Count; j++)
            {
                GridView gv = new GridView();
                gv.CssClass = "table";
                gv.GridLines = GridLines.None;
                gv.Attributes.Add("class", "aclass");
               
                Label lbl = new Label();
                lbl.CssClass = "Gridlabel";
                lbl.Attributes.Add("class", "lbl");
                DataSet ds = null;
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    lbl.Text = dsState.Tables[0].Rows[j]["StateName"].ToString();
                    state_code = dsState.Tables[0].Rows[j]["State_Code"].ToString();
                    Holiday hol = new Holiday();
                    
                    foreach (string st1 in stateCount)
                    {
                        if (Convert.ToString(state_code) == st1)
                        {
                            ds = hol.getHolidays_Consol(st1, div_code, ddlYear.SelectedValue);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gv.DataSource = ds;
                                gv.DataBind();                                
                            }
                        }
                    }
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbl.Attributes.Add("align", "center");
                    pnl.Controls.Add(lbl);
                    //
                    for (int k = 0; k < ds.Tables[0].Columns.Count; k++)
                    {
                        BoundField boundfield = new BoundField();
                        boundfield.DataField = ds.Tables[0].Columns[k].ColumnName.ToString();
                        boundfield.HeaderText = ds.Tables[0].Columns[k].ColumnName.ToString();
                        gv.Columns.Add(boundfield);
                    }
                    for (int x = 0; x < gv.Columns.Count; x++)
                    {
                        int colWidth = 70;
                        if (x == 3 || x == 4)
                        {
                            if (x == 3)
                                colWidth = 300;
                            else
                                colWidth = 200;
                        }
                        else if (x == 1)
                        {
                            colWidth = 100;
                        }
                        else
                        {
                            colWidth = 50;
                        }
                        gv.Columns[x].ItemStyle.Width = colWidth;
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.AutoGenerateColumns = false;
                        gv.DataSource = ds;
                        gv.DataBind();
                        reOrderGridView(ds, gv);
                        bNoRecordsFound = false;
                    }
                    gv.HeaderStyle.CssClass = "gvheader";
                    gv.RowStyle.CssClass = "gvrowstyle";
                    //
                    pnl.Controls.Add(gv);
                }
                else
                {
                   // lblNoRecord.Visible = true;
                }               
            }
            if (bNoRecordsFound)
            {
                GridView gvs = new GridView();
                //gvs.EmptyDataRowStyle.BackColor = System.Drawing.Color.Wheat;
                gvs.CssClass = "no-result-area";
                gvs.EmptyDataText = "No Data Found!";
                gvs.Width = 700;
                gvs.BorderWidth = 1;
                gvs.EmptyDataRowStyle.HorizontalAlign = HorizontalAlign.Center;
                gvs.EmptyDataRowStyle.Font.Bold = true;
                gvs.DataSource = null;
                gvs.DataBind();
                pnl.Controls.Add(gvs);
            }
        }
    }
    #endregion
    //
    private void reOrderGridView(DataSet ds, GridView gvs)
    {
        if (ds.Tables[0].Rows.Count>0)
        {
            string sDate = "";
            List<string> olstDate = new List<string>();
            foreach (GridViewRow gvRow in gvs.Rows)
            {
                string txt = gvRow.Cells[0].Text;
                if (sDate != gvRow.Cells[1].Text)
                {
                    sDate = gvRow.Cells[1].Text;
                }
                else
                {
                    olstDate.Add(gvRow.Cells[1].Text);                    
                }
            }
            foreach (GridViewRow grdRow in gvs.Rows)
            {
                foreach (string item in olstDate)
                {
                    if (item==grdRow.Cells[1].Text)
                    {
                        grdRow.Cells[0].BackColor = System.Drawing.Color.Red;
                        grdRow.Cells[1].BackColor = System.Drawing.Color.Red;
                        grdRow.Cells[2].BackColor = System.Drawing.Color.Red;
                        grdRow.Cells[3].BackColor = System.Drawing.Color.Red;
                        grdRow.Cells[4].BackColor = System.Drawing.Color.Red;

                        grdRow.Cells[0].ForeColor = System.Drawing.Color.White;
                        grdRow.Cells[1].ForeColor = System.Drawing.Color.White;
                        grdRow.Cells[2].ForeColor = System.Drawing.Color.White;
                        grdRow.Cells[3].ForeColor = System.Drawing.Color.White;
                        grdRow.Cells[4].ForeColor = System.Drawing.Color.White;

                        grdRow.Cells[0].Attributes.Add("class", "blink_me");
                        grdRow.Cells[1].Attributes.Add("class", "blink_me");
                        grdRow.Cells[2].Attributes.Add("class", "blink_me");
                        grdRow.Cells[3].Attributes.Add("class", "blink_me");
                        grdRow.Cells[4].Attributes.Add("class", "blink_me");
                    }
                }
            }
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillState(div_code);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {        
        Response.Redirect(Session["backurl"].ToString());
    }
}