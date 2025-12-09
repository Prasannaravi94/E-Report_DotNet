using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_Subdiv_Salesforcewise : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDivision = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DataSet dsSalesForce = new DataSet();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataTable table = null;
    int slno = 0;
    double BA = 0;
    double BA1 = 0;
    double BA2 = 0;
    double BA3 = 0;
    double BA4 = 0;
    double BA5 = 0;
    double BA6 = 0;
    double BA7 = 0;
    double BA8 = 0;
    double BA9 = 0;
    double BA10 = 0;
    double BA11 = 0;
    double BA12 = 0;
    double BA13 = 0;
    double BA14 = 0;
    double BA15 = 0;
    double BA16 = 0;
    double BA17 = 0;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        menu1.Title = Page.Title;
        // menu1.FindControl("btnBack").Visible = false;
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]); 
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

           
            
            GetMyMonthList();
            bind_year_ddl();
        }
       
        

    }

    
    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2017; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private void FillBaseLevel()
    {
        SalesForce tp = new SalesForce();
        dsSalesForce = tp.Exp_Sts(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdWTAllowance.DataSource = dsSalesForce;
            grdWTAllowance.DataBind();
            
        }
    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        FillBaseLevel();
    }
   
    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView grdWTAllowance = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();



            AddMergedCells(objgridviewrow, objtablecell, 0, "#", "#4da6ff", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Division_name", "#4da6ff", true);



            AddMergedCells(objgridviewrow, objtablecell, 3, "Base Level", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);




            AddMergedCells(objgridviewrow, objtablecell, 3, "Mgr-Automatic", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);

            AddMergedCells(objgridviewrow, objtablecell, 3, "Mgr-Semi-Automatic", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);

            AddMergedCells(objgridviewrow, objtablecell, 3, "Mgr-Manual", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);

            AddMergedCells(objgridviewrow, objtablecell, 3, "Mgr-Others", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);

            AddMergedCells(objgridviewrow, objtablecell, 2, "Total", "#4da6ff", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Applied", "#4da6ff", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Approved", "#4da6ff", false);
            //AddMergedCells(objgridviewrow1, objtablecell1, 0, "Active", "#4da6ff", false);




            grdWTAllowance.Controls[0].Controls.AddAt(0, objgridviewrow);
            grdWTAllowance.Controls[0].Controls.AddAt(1, objgridviewrow1);

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
        objtablecell.Style.Add("background-color", backcolor);
        objtablecell.Style.Add("color", "white");
        objtablecell.Style.Add("font-weight", "bold");
        //objtablecell.Style.Add("BorderWidth", "1px");
        // objtablecell.Style.Add("BorderStyle", "solid");
        // objtablecell.Style.Add("BorderColor", "Black");

        objtablecell.HorizontalAlign = HorizontalAlign.Center;
        objtablecell.Wrap = false;
        objgridviewrow.Cells.Add(objtablecell);
    }
    protected void grdExpense_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblBA = (Label)e.Row.FindControl("lblBA");
            if (lblBA.Text != "")
            {
                BA += Convert.ToDouble(lblBA.Text);
            }
            Label lblBA1 = (Label)e.Row.FindControl("lblBA1");
            if (lblBA1.Text != "")
            {
                BA1 += Convert.ToDouble(lblBA1.Text);
            } 
            Label lblBA2 = (Label)e.Row.FindControl("lblBA2");
            if (lblBA2.Text != "")
            {
                BA2 += Convert.ToDouble(lblBA2.Text);
            } 
            Label lblBA3 = (Label)e.Row.FindControl("lblBA3");
            if (lblBA3.Text != "")
            {
                BA3 += Convert.ToDouble(lblBA3.Text);
            } 
            Label lblBA4 = (Label)e.Row.FindControl("lblBA4");
            if (lblBA4.Text != "")
            {
                BA4 += Convert.ToDouble(lblBA4.Text);
            } 
            Label lblBA5 = (Label)e.Row.FindControl("lblBA5");
            if (lblBA5.Text != "")
            {
                BA5 += Convert.ToDouble(lblBA5.Text);
            } 
            Label lblBA6 = (Label)e.Row.FindControl("lblBA6");
            if (lblBA6.Text != "")
            {
                BA6 += Convert.ToDouble(lblBA6.Text);
            } 
            Label lblBA7 = (Label)e.Row.FindControl("lblBA7");
            if (lblBA7.Text != "")
            {
                BA7 += Convert.ToDouble(lblBA7.Text);
            } 
            Label lblBA8 = (Label)e.Row.FindControl("lblBA8");
            if (lblBA8.Text != "")
            {
                BA8 += Convert.ToDouble(lblBA8.Text);
            } 
            Label lblBA9 = (Label)e.Row.FindControl("lblBA9");
            if (lblBA9.Text != "")
            {
                BA9 += Convert.ToDouble(lblBA9.Text);
            } 
            Label lblBAA1 = (Label)e.Row.FindControl("lblBAA1");
            if (lblBAA1.Text != "")
            {
                BA10 += Convert.ToDouble(lblBAA1.Text);
            }
            Label lblBAA2 = (Label)e.Row.FindControl("lblBAA2");
            if (lblBAA2.Text != "")
            {
                BA11 += Convert.ToDouble(lblBAA2.Text);
            }
            Label lblBAA3 = (Label)e.Row.FindControl("lblBA12");
            if (lblBAA3.Text != "")
            {
                BA12 += Convert.ToDouble(lblBAA3.Text);
            }
            Label lblBAA4 = (Label)e.Row.FindControl("lblBA13");
            if (lblBAA4.Text != "")
            {
                BA13 += Convert.ToDouble(lblBAA4.Text);
            }
            Label lblBAA5 = (Label)e.Row.FindControl("lblBA14");
            if (lblBAA5.Text != "")
            {
                BA14 += Convert.ToDouble(lblBAA5.Text);
            }
            Label lblBAA6 = (Label)e.Row.FindControl("lblBA15");
            if (lblBAA6.Text != "")
            {
                BA15 += Convert.ToDouble(lblBAA6.Text);
            }
            Label lblBAA7 = (Label)e.Row.FindControl("lblBA16");
            if (lblBAA7.Text != "")
            {
                BA16 += Convert.ToDouble(lblBAA7.Text);
            }
            //Label lblBAA8 = (Label)e.Row.FindControl("lblBA17");
            //if (lblBAA8.Text != "")
            //{
            //    BA17 += Convert.ToDouble(lblBAA8.Text);
            //}
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label ftlblBA = (Label)e.Row.FindControl("ftlblBA");
            if (BA != 0)
            {
                ftlblBA.Text = BA.ToString();
            }
            Label ftlblBA1 = (Label)e.Row.FindControl("ftlblBA1");
            if (BA1 != 0)
            {
                ftlblBA1.Text = BA1.ToString();
            }
            Label ftlblBA2 = (Label)e.Row.FindControl("ftlblBA2");
            if (BA2 != 0)
            {
                ftlblBA2.Text = BA2.ToString();
            }
            Label ftlblBA3 = (Label)e.Row.FindControl("ftlblBA3");
            if (BA3 != 0)
            {
                ftlblBA3.Text = BA3.ToString();
            }
            Label ftlblBA4 = (Label)e.Row.FindControl("ftlblBA4");
            if (BA4 != 0)
            {
                ftlblBA4.Text = BA4.ToString();
            }
            Label ftlblBA5 = (Label)e.Row.FindControl("ftlblBA5");
            if (BA5 != 0)
            {
                ftlblBA5.Text = BA5.ToString();
            }
            Label ftlblBA6 = (Label)e.Row.FindControl("ftlblBA6");
            if (BA6 != 0)
            {
                ftlblBA6.Text = BA6.ToString();
            }
            Label ftlblBA7 = (Label)e.Row.FindControl("ftlblBA7");
            if (BA7 != 0)
            {
                ftlblBA7.Text = BA7.ToString();
            }
            Label ftlblBA8 = (Label)e.Row.FindControl("ftlblBA8");
            if (BA8 != 0)
            {
                ftlblBA8.Text = BA8.ToString();
            }
            Label ftlblBA9 = (Label)e.Row.FindControl("ftlblBA9");
            if (BA9 != 0)
            {
                ftlblBA9.Text = BA9.ToString();
            }
            Label ftlblBAA1 = (Label)e.Row.FindControl("ftlblBAA1");
            if (BA10 != 0)
            {
                ftlblBAA1.Text = BA10.ToString();
            }
            Label ftlblBAA2 = (Label)e.Row.FindControl("ftlblBAA2");
            if (BA11 != 0)
            {
                ftlblBAA2.Text = BA11.ToString();
            }
            Label ftlblBAA3 = (Label)e.Row.FindControl("ftlblBA12");
            if (BA12 != 0)
            {
                ftlblBAA3.Text = BA12.ToString();
            }
            Label ftlblBAA4 = (Label)e.Row.FindControl("ftlblBA13");
            if (BA13 != 0)
            {
                ftlblBAA4.Text = BA13.ToString();
            }
            Label ftlblBAA5 = (Label)e.Row.FindControl("ftlblBA14");
            if (BA14 != 0)
            {
                ftlblBAA5.Text = BA14.ToString();
            }
            Label ftlblBAA6 = (Label)e.Row.FindControl("ftlblBA15");
            if (BA15 != 0)
            {
                ftlblBAA6.Text = BA15.ToString();
            }
            Label ftlblBAA7 = (Label)e.Row.FindControl("ftlblBA16");
            if (BA16 != 0)
            {
                ftlblBAA7.Text = BA16.ToString();
            }
            //Label ftlblBAA8 = (Label)e.Row.FindControl("ftlblBA17");
            //if (BA17 != 0)
            //{
            //    ftlblBAA8.Text = BA17.ToString();
            //}
        }



    }

}