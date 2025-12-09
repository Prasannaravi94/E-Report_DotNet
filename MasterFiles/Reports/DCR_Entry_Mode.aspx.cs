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
            BindDate();
            //GetMyMonthList();
            //bind_year_ddl();
        }
        if (Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            //c1.Title = this.Page.Title;
            //c1.FindControl("btnBack").Visible = false;
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
    private void BindDate()
    {
        DateTime FromMonth = DateTime.Now;
        txtMonthYear.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
    }
    //public void GetMyMonthList()
    //{
    //    DateTime month = Convert.ToDateTime("1/1/2012");
    //    for (int i = 0; i < 12; i++)
    //    {
    //        DateTime nextMonth = month.AddMonths(i);
    //        ListItem list = new ListItem();
    //        list.Text = nextMonth.ToString("MMMM");
    //        list.Value = nextMonth.Month.ToString();
    //        monthId.Items.Add(list);
    //    }
    //    monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    //}

    //private void bind_year_ddl()
    //{
    //    int year = (System.DateTime.Now.Year);
    //    for (int intCount = 2015; intCount <= year + 1; intCount++)
    //    {
    //        yearID.Items.Add(intCount.ToString());
    //    }
    //    yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    //}
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
        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());

        int MonthVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Month);
        int YearVal = Convert.ToInt32(Convert.ToDateTime(txtMonthYear.Text).Year);

        //DataTable dtAllFare = dv.getDCR_descnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable dtdes = dv.getDCRdesdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable dtmob = dv.getDCRmobdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable dtapp = dv.getDCRappdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable dtoth = dv.getDCRothdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable dtedt = dv.getDCRedtdt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);
        //DataTable iOSdt = dv.getDCRiOS(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), divcode);

        DataTable dtAllFare = dv.getDCR_descnt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable dtdes = dv.getDCRdesdt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable dtmob = dv.getDCRmobdt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable dtapp = dv.getDCRappdt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable dtoth = dv.getDCRothdt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable dtedt = dv.getDCRedtdt(MonthVal.ToString(), YearVal.ToString(), divcode);
        DataTable iOSdt = dv.getDCRiOS(MonthVal.ToString(), YearVal.ToString(), divcode);

        DataTable mainTable = dsSubDivision.Tables[0];

        mainTable.Columns.Add("Desktop");
        mainTable.Columns.Add("Mobile");
        mainTable.Columns.Add("Apps");
        mainTable.Columns.Add("Edt");
        mainTable.Columns.Add("Oths");
        mainTable.Columns.Add("iOS");
        mainTable.Columns.Add("desdt");
        mainTable.Columns.Add("mobdt");
        mainTable.Columns.Add("appdt");
        mainTable.Columns.Add("othdt");
        mainTable.Columns.Add("edtdt");
        mainTable.Columns.Add("iOSdt");
        

        if (mainTable.Rows.Count > 0)
        {
            //            double totClaimedAmnt = 0;
            foreach (DataRow row in mainTable.Rows)
            {

                String filter = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Desktop'";
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
                String filter_Apps = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Apps'";
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
                String filter_Edt = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='Edt'";
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

                String filter_iOS = "sf_code='" + row["SF_Code"].ToString() + "' and Entry_Mode='iOS'";
                DataRow[] iOS = dtAllFare.Select(filter_iOS);

                String filter_iOS1 = "sf_code='" + row["SF_Code"].ToString() + "'";
                DataRow[] iOS1 = iOSdt.Select(filter_iOS1);

                if (iOS.Count() > 0)
                {
                    row["iOS"] = iOS[0]["cnt"];


                }
                if (iOS1.Count() > 0)
                {
                    row["iOSdt"] = iOS1[0]["iOSdt"];

                }

            }
            

            pnlprint.Visible = true;
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
                string sURL = "RptAutoExpense_view1.aspx?sf_code=" + code.Value + "&monthId=" + MonthVal.ToString() + "&yearId=" + YearVal.ToString() + "&divCode=" + divcode + "";
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

}