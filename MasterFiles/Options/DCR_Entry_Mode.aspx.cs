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
    DataSet dsSubDivision1 = null;
    DataTable dtAllFare = null;
    DataTable dtdes = null;
    DataTable dtmob = null;
    DataTable dtapp = null;
    DataTable dtoth = null;
    DataTable dtedt = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string sfcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string sChkdt = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
            
            checkall.Visible = false;
            chkdate.Visible = false;
        }
        if (Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            c1.FindControl("btnBack").Visible = false;
        }
        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            c1.FindControl("btnBack").Visible = false;
        }
        FillColor();

    }

    private void FillColor()
    {
        int j = 0;


        foreach (System.Web.UI.WebControls.ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlSubdiv.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

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
        for (int intCount = 2015; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private void FillFieldForcediv(string divcode)
    {
        SalesForce dv = new SalesForce();
        //if (Session["sf_type"].ToString() == "2")
        //{
        //    sfcode=
        //}
        dsSubDivision = dv.UserList_Hierarchy(divcode, sfcode);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "Sf_Name";
            ddlSubdiv.DataValueField = "Sf_Code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "Sf_Code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();

        }
    }
    

    protected void btnSF_Click(object sender, EventArgs e)
    {
        //GridSales1.Visible = true;
       // grdSalesForce.Visible = true;
        for (int i = 0; i < chkdate.Items.Count; i++)
        {
            if (chkdate.Items[i].Selected)
            {
                sChkdt = sChkdt + chkdate.Items[i].Value + ",";
            }
        }
        string sChkdt1 = sChkdt;
      //  Filldays();
        if(sChkdt!=null&&sChkdt!="")
         sChkdt1 = sChkdt.Remove(sChkdt.Length - 1, 1);
        Distance_calculation dv = new Distance_calculation();
        if (ModeId.SelectedValue == "1" || sChkdt1 == "" || checkall.Checked==true)
        {
            dtAllFare = dv.getDCR_descnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtdes = dv.getDCRdesdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtmob = dv.getDCRmobdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtapp = dv.getDCRappdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtoth = dv.getDCRothdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtedt = dv.getDCRedtdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        }
        else if (ModeId.SelectedValue == "2" && (sChkdt1 == "" || checkall.Checked == true))
        {
            dtAllFare = dv.getDCR_descnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtdes = dv.getDCRdesdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtmob = dv.getDCRmobdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtapp = dv.getDCRappdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtoth = dv.getDCRothdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

            dtedt = dv.getDCRedtdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        }
        else
        {

            dtAllFare = dv.getDCR_descnt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
            dtdes = dv.getDCRdesdt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
            dtmob = dv.getDCRmobdt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
            dtapp = dv.getDCRappdt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
            dtoth = dv.getDCRothdt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
            dtedt = dv.getDCRedtdt_datewise(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode, sChkdt1);
        }


       
        

        
        if (ModeId.SelectedValue.ToString() == "1")
        {
            GridSales1.Visible = false;
            //dsSubDivision = dv.getFilterRgn_Vacant(divcode, ddlSubdiv.SelectedValue.ToString());
            if (chkVacant.Checked == true)
            {
                dsSubDivision = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());
            }
            else
            {
                dsSubDivision = dv.getFilterRgn_Vacant(divcode, ddlSubdiv.SelectedValue.ToString());
            }
            DataTable mainTable = dsSubDivision.Tables[0];

            mainTable.Columns.Add("Desktop");
            mainTable.Columns.Add("Mobile");
            mainTable.Columns.Add("Apps");
            mainTable.Columns.Add("Edt");
            mainTable.Columns.Add("Oths");
            mainTable.Columns.Add("desdt");
            mainTable.Columns.Add("mobdt");
            mainTable.Columns.Add("appdt");
            mainTable.Columns.Add("othdt");
            mainTable.Columns.Add("edtdt");
            if (mainTable.Rows.Count > 0)
            {
                //            double totClaimedAmnt = 0;
                foreach (DataRow row in mainTable.Rows)
                {

                    String filter = "sf_code='" + row["SF_Code"].ToString() + "' and (Entry_Mode='Desktop' OR Entry_Mode='web') ";
                    DataRow[] rows = dtAllFare.Select(filter);
                    String filter1 = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] rows1 = dtdes.Select(filter1);



                    if (rows.Count() > 0)
                    {
                        row["Desktop"] = rows[0]["cnt"];


                    }
                    if (rows1.Count() > 0)
                    {
                        row["desdt"] = rows1[0]["desdt"];

                    }
                    String filter_Mob = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Mobile'";
                    DataRow[] Mob = dtAllFare.Select(filter_Mob);
                    String filter1_Mob = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Mobdt = dtmob.Select(filter1_Mob);


                    if (Mob.Count() > 0)
                    {
                        row["Mobile"] = Mob[0]["cnt"];


                    }
                    if (Mobdt.Count() > 0)
                    {
                        row["mobdt"] = Mobdt[0]["mobdt"];

                    }
                    // String filter_Apps = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Apps'";
                    String filter_Apps = "sf_code='" + row["SF_Code"].ToString() + "' and (Entry_Mode='Apps' or Entry_Mode='app' or Entry_Mode='Android-App' or Entry_Mode='iOS-App' or Entry_Mode='iOS' )";

                    DataRow[] Apps = dtAllFare.Select(filter_Apps);
                    String filter1_Apps = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Apps1 = dtapp.Select(filter1_Apps);


                    if (Apps.Count() > 0)
                    {
                        row["Apps"] = Apps[0]["cnt"];


                    }
                    if (Apps1.Count() > 0)
                    {
                        row["appdt"] = Apps1[0]["appdt"];

                    }
                    // String filter_Edt = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Edt'";

                    String filter_Edt = "sf_code='" + row["SF_Code"].ToString() + "' and  (Entry_Mode='Edt' OR Entry_Mode='Android-Edet' or Entry_Mode='iOS-Edet')";

                    DataRow[] Edt = dtAllFare.Select(filter_Edt);
                    String filter1_Edt = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Edt1 = dtedt.Select(filter1_Edt);



                    if (Edt.Count() > 0)
                    {
                        row["Edt"] = Edt[0]["cnt"];


                    }
                    if (Edt1.Count() > 0)
                    {
                        row["edtdt"] = Edt1[0]["edtdt"];

                    }
                    String filter_Oths = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode=''";
                    DataRow[] Oths = dtAllFare.Select(filter_Oths);

                    String filter_Oths1 = "sf_code='" + row["SF_Code"].ToString() + "'"; 
                    DataRow[] Oths1 = dtoth.Select(filter_Oths1);

                    if (Oths.Count() > 0)
                    {
                        row["Oths"] = Oths[0]["cnt"];


                    }
                    if (Oths1.Count() > 0)
                    {
                        row["othdt"] = Oths1[0]["othdt"];

                    }
                }


                //pnlprint.Visible = true;
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = mainTable;
                grdSalesForce.DataBind();
                foreach (GridViewRow gridRow in grdSalesForce.Rows)
                {

                    //TableCell cell = new TableCell();
                    HyperLink link = new HyperLink();
                    Label lbl = new Label();
                    HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden");
                    HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden");




                    link.Text = "<span>" + name.Value + "</span>";
                    lbl.Text = name.Value;
                    string sURL = "RptAutoExpense_view1.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                    link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    link.NavigateUrl = "#";
                    //cell.Controls.Add(link);

                    gridRow.Cells[1].Controls.Add(lbl);



                }

            }
            else
            {
                grdSalesForce.DataSource = dsSubDivision;
                grdSalesForce.DataBind();
            }
        }
        else  if (ModeId.SelectedValue.ToString() == "2")
        
        {
            grdSalesForce.Visible = false;
            if (chkVacant.Checked == true)
            {
                dsSubDivision1 = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());
            }
            else
            {
                dsSubDivision1 = dv.getFilterRgn_Vacant(divcode, ddlSubdiv.SelectedValue.ToString());
            }
            DataTable mainTable1 = dsSubDivision1.Tables[0];
            mainTable1.Columns.Add("Desktop");
            mainTable1.Columns.Add("Mobile");
            mainTable1.Columns.Add("Apps");
            mainTable1.Columns.Add("Edt");
            mainTable1.Columns.Add("Oths");
            mainTable1.Columns.Add("desdt");
            mainTable1.Columns.Add("mobdt");
            mainTable1.Columns.Add("appdt");
            mainTable1.Columns.Add("othdt");
            mainTable1.Columns.Add("edtdt");
            if (mainTable1.Rows.Count > 0)
            {
                //            double totClaimedAmnt = 0;
                foreach (DataRow row in mainTable1.Rows)
                {

                    String filter = "sf_code='" + row["SF_Code"].ToString() + "' and (Entry_Mode='Desktop' OR Entry_Mode='web') ";
                    DataRow[] rows = dtAllFare.Select(filter);
                    String filter1 = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] rows1 = dtdes.Select(filter1);



                    if (rows.Count() > 0)
                    {
                        row["Desktop"] = rows[0]["cnt"];


                    }
                    if (rows1.Count() > 0)
                    {
                        row["desdt"] = rows1[0]["desdt"];

                    }
                    String filter_Mob = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Mobile'";
                    DataRow[] Mob = dtAllFare.Select(filter_Mob);
                    String filter1_Mob = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Mobdt = dtmob.Select(filter1_Mob);


                    if (Mob.Count() > 0)
                    {
                        row["Mobile"] = Mob[0]["cnt"];


                    }
                    if (Mobdt.Count() > 0)
                    {
                        row["mobdt"] = Mobdt[0]["mobdt"];

                    }
                    // String filter_Apps = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Apps'";
                    String filter_Apps = "sf_code='" + row["SF_Code"].ToString() + "' and (Entry_Mode='Apps' or Entry_Mode='app' or Entry_Mode='Android-App' or Entry_Mode='iOS-App' or Entry_Mode='iOS' )";

                    DataRow[] Apps = dtAllFare.Select(filter_Apps);
                    String filter1_Apps = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Apps1 = dtapp.Select(filter1_Apps);


                    if (Apps.Count() > 0)
                    {
                        row["Apps"] = Apps[0]["cnt"];


                    }
                    if (Apps1.Count() > 0)
                    {
                        row["appdt"] = Apps1[0]["appdt"];

                    }
                    //String filter_Edt = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Edt'";
                    String filter_Edt = "sf_code='" + row["SF_Code"].ToString() + "' and  (Entry_Mode='Edt' OR Entry_Mode='Android-Edet' or Entry_Mode='iOS-Edet')";

                    DataRow[] Edt = dtAllFare.Select(filter_Edt);
                    String filter1_Edt = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Edt1 = dtedt.Select(filter1_Edt);



                    if (Edt.Count() > 0)
                    {
                        row["Edt"] = Edt[0]["cnt"];


                    }
                    if (Edt1.Count() > 0)
                    {
                        row["edtdt"] = Edt1[0]["edtdt"];

                    }
                    String filter_Oths = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode=''";
                    DataRow[] Oths = dtAllFare.Select(filter_Oths);

                    String filter_Oths1 = "sf_code='" + row["SF_Code"].ToString() + "'";
                    DataRow[] Oths1 = dtoth.Select(filter_Oths1);

                    if (Oths.Count() > 0)
                    {
                        row["Oths"] = Oths[0]["cnt"];


                    }
                    if (Oths1.Count() > 0)
                    {
                        row["othdt"] = Oths1[0]["othdt"];

                    }
                }


                //pnlprint.Visible = true;
                GridSales1.Visible = true;
                GridSales1.DataSource = mainTable1;
                GridSales1.DataBind();
                foreach (GridViewRow gridRow in GridSales1.Rows)
                {

                    //TableCell cell = new TableCell();
                    HyperLink link = new HyperLink();
                    Label lbl = new Label();
                    HiddenField name = (HiddenField)gridRow.FindControl("sfNameHidden1");
                    HiddenField code = (HiddenField)gridRow.FindControl("sfCodeHidden1");




                    link.Text = "<span>" + name.Value + "</span>";
                    lbl.Text = name.Value;
                    string sURL = "RptAutoExpense_view1.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                    link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'resizable=yes,scrollbars=1,toolbar=no,menubar=no,status=no,width=500,height=600,left=0,top=0');";
                    link.NavigateUrl = "#";
                    //cell.Controls.Add(link);

                    gridRow.Cells[1].Controls.Add(lbl);



                }

            }
            else
            {
                GridSales1.DataSource = dsSubDivision1;
                GridSales1.DataBind();
            }
        }
    }

    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView grdWTAllowance = (GridView)sender;
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell = new TableCell();

            GridViewRow objgridviewrow1 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //Creating a table cell object
            TableCell objtablecell1 = new TableCell();
       


            AddMergedCells(objgridviewrow, objtablecell, 0, "S.No", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Fieldforce Name", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Head Quater", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Emp Id", "#5E5D8E", true);
            AddMergedCells(objgridviewrow, objtablecell, 0, "Designation", "#5E5D8E", true);


            AddMergedCells(objgridviewrow, objtablecell, 2, "Desktop", "#A6A6D2", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Date", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Count", "#A6A6D2", false);
            


            AddMergedCells(objgridviewrow, objtablecell, 2, "Mobile", "#A6A6D2", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Date", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Count", "#A6A6D2", false);
            

            AddMergedCells(objgridviewrow, objtablecell, 2, "Apps", "#A6A6D2", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Date", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Count", "#A6A6D2", false);
            

            AddMergedCells(objgridviewrow, objtablecell, 2, "E-detailing", "#A6A6D2", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Date", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Count", "#A6A6D2", false);
            

            AddMergedCells(objgridviewrow, objtablecell, 2, "Others", "#A6A6D2", true);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Date", "#A6A6D2", false);
            AddMergedCells(objgridviewrow1, objtablecell1, 0, "Count", "#A6A6D2", false);
            



           

           


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
    private void Filldays()
    {
        
      
            chkdate.Visible = true;
            
           
            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(yearID.SelectedValue), Convert.ToInt32(monthId.SelectedValue));
            //int to_days = 31;
            for (int i = 1; i <= noofdays; i++)
            {

                chkdate.Items.Add("   " + i.ToString());

            }
    

    }
    protected void datechange_SelectedIndexChanged(object sender, EventArgs e)
    {
        // chkdate.Items.Clear();
        GridSales1.Visible = false;
        grdSalesForce.Visible = false;
        if (ModeId.SelectedValue == "2")
        {

            int noofdays = DateTime.DaysInMonth(Convert.ToInt32(yearID.SelectedValue), Convert.ToInt32(monthId.SelectedValue));
            for (int i = 1; i <= noofdays; i++)
            {

                chkdate.Items.Remove("   " + i.ToString());

            }
            checkall.Visible = true;
            chkdate.Visible = true;
            Filldays();
            
            //int to_days = 31;
           
        }
        else if (ModeId.SelectedValue == "1")
        {
            checkall.Visible = false;
            chkdate.Visible = false;
          
        }
      
      
    }


}