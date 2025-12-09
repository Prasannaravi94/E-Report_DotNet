using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Configuration;
using Bus_EReport;
using System.Globalization;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    bool isSavedPage = false;
    string sfcode = "";
    string monthId = "";
    string yearID = "";
    string divcode = "";
    string gt = "";
    string rmks;
    string rmks1;
    string sfff = "";
    string sff_name = "";
    Hashtable months = new Hashtable();
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {
        rmks = approveTextId.InnerText;
        rmks1 = lblrmks1.InnerText;
        months.Add("1", "January");
        months.Add("2", "Feb");
        months.Add("3", "March");
        months.Add("4", "April");
        months.Add("5", "May");
        months.Add("6", "June");
        months.Add("7", "July");
        months.Add("8", "August");
        months.Add("9", "Sept");
        months.Add("10", "October");
        months.Add("11", "November");
        months.Add("12", "Decemeber");

        gt = grandTotalName.InnerHtml;
        sfcode = Request.QueryString["sf_code"].ToString();
        divcode = Request.QueryString["divCode"].ToString();

        sfCode.Value = sfcode;
        divCode.Value = divcode;
        monthId = Request.QueryString["monthId"].ToString();
        yearID = Request.QueryString["yearID"].ToString();

        DataTable rt = Exp.getMgrAppr(divcode);
        if (rt.Rows.Count > 0)
        {
            if ("Y".Equals(rt.Rows[0]["Row_wise_textbox"].ToString()))
            {
                grdExpMain.Columns[10].Visible = true;
                grdExpMain.Columns[11].Visible = true;

            }
        }



        mnthtxtId.InnerHtml = months[Request.QueryString["monthId"].ToString()].ToString();
        yrtxtId.InnerHtml = Request.QueryString["yearID"].ToString();
        sfff = Session["sf_code"].ToString();
        sff_name = Session["sf_name"].ToString();
        DataTable ds = Exp.getFieldForce(divcode, sfcode);
        DataTable dsdv = Exp.getdiv(divcode);
        if ("2".Equals(ds.Rows[0]["sf_type"].ToString()))
        {
            distview.Visible = false;
            distviewMGR.Visible = true;
            //grdExpMain.Columns[7].Visible = false;
            //    grdExpMain.Columns[10].Visible = true;
            //    grdExpMain.Columns[11].Visible = true;


        }
        else
        {

            distviewMGR.Visible = false;
        }

        excesfare.Enabled = false; excesremks.Enabled = false; excesallw.Enabled = false; excesallwrmks.Enabled = false;

           DataSet dsSf = new DataSet();
        SalesForce sf1 = new SalesForce();
        dsSf = sf1.CheckSFNameVacant_Temp(sfcode, Convert.ToInt16(monthId), Convert.ToInt16(yearID));

        //if (dsSf.Tables[0].Rows.Count > 0)
        //{
        //    string[] strVacant = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().Split(',');

        //    if (strVacant.Count() >= 2)
        //    {
        //        if ("( " + strVacant[0].Trim() + " )" != strVacant[1].Trim())
        //        {
        //            fieldforceId.InnerHtml = "Fieldforce Name :" + strVacant[0] + "<span style='color: red;'>" + strVacant[1] + "</span>" + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
        //        }
        //        else
        //        {
        //            fieldforceId.InnerHtml = "Fieldforce Name :" + strVacant[0];
        //        }
        //    }
        //    else
        //    {
        //        fieldforceId.InnerHtml = "Fieldforce Name :" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(0) + "<span style='color: red;'>" + dsSf.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + "</span>";
        //    }

        //    if (dsSf.Tables[0].Rows[0][2].ToString() != "")
        //    {
        //        fieldforceId.InnerHtml = "Fieldforce Name :" + "<span style='color: red;'> " + dsSf.Tables[0].Rows[0].ItemArray.GetValue(2) + " </span>";

        //    }
        //    empid1.InnerHtml = "Employee Code :" + ds.Rows[0]["Employee_Id"].ToString();
        //}
        //else
        //{
        //dsSf = sf1.CheckSFName_DCREntry_Check(strSF_Code, Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
        fieldforceId.InnerHtml = "Name :" + ds.Rows[0]["sf_name"].ToString();

        empId.InnerHtml = "Emp.ID :" + ds.Rows[0]["Employee_Id"].ToString();
        hqdesig.InnerHtml = "Designation :" + ds.Rows[0]["sf_designation_short_name"].ToString();
        hqid.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
        mgrName.InnerHtml = "Manager Name :" + ds.Rows[0]["report_sf"].ToString();
        divid.InnerHtml = dsdv.Rows[0]["div_name"].ToString();
        DataTable plusMinus = Exp.getPlusMinusDesig(sfcode, divcode);
        DataTable adminExp = Exp.getSavedAdminExpRecord1(monthId, yearID, sfcode);

        // adminExpGrid.DataSource = adminExp;
        //adminExpGrid.DataBind();
        DataTable headerDataSet = Exp.getSavedHeadRecord(monthId, yearID, sfcode);
        if (headerDataSet.Rows.Count > 0)
        {
            string rms = headerDataSet.Rows[0]["Admin_Remarks"].ToString();
            approveTextId.InnerText = rms;
            lblrmks1.InnerText = rms;
        }
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        DataTable t1 = Exp.getSavedRecord(monthId, yearID, sfcode, dt3);
        double totalAllowance = 0;
        double totalDistance = 0;
        double totalFare = 0;
        double totalAdd = 0;
        double grandTotal = 0;
        twdid.Visible = true;
        DataTable TD = Exp.TWD(sfcode, monthId, yearID);
        twd.InnerHtml = TD.Rows[0]["cnt"].ToString();
        DataTable FW = Exp.FW(sfcode, monthId, yearID);
        fw.InnerHtml = FW.Rows[0]["cnt"].ToString();
        DataTable HQ = Exp.THQ(sfcode, monthId, yearID);
        hhq.InnerHtml = HQ.Rows[0]["cnt"].ToString();
        DataTable EX = Exp.TEX(sfcode, monthId, yearID);
        eex.InnerHtml = EX.Rows[0]["cnt"].ToString();
        DataTable OS = Exp.TOS(sfcode, monthId, yearID);
        oos.InnerHtml = OS.Rows[0]["cnt"].ToString();
        DataTable drsmet = Exp.Drsmet(sfcode, monthId, yearID);
        met.InnerHtml = drsmet.Rows[0]["metcnt"].ToString();
        made.InnerHtml = drsmet.Rows[0]["madecnt"].ToString();
        cavg.InnerHtml = drsmet.Rows[0]["calavg"].ToString();
        foreach (DataRow row in t1.Rows)
        {


            totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
            totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
            totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
            totalAdd = totalAdd + Convert.ToDouble(row["rw_amount"].ToString());
            grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
            if (frmTovalues.Value == "")
                frmTovalues.Value = row["frmTovalues"].ToString();

            else
                frmTovalues.Value = frmTovalues.Value + "#" + row["frmTovalues"].ToString();


        }
        t1.Rows.Add();
        t1.Rows[t1.Rows.Count - 1]["Allowance"] = totalAllowance;
        t1.Rows[t1.Rows.Count - 1]["Distance"] = totalDistance;
        t1.Rows[t1.Rows.Count - 1]["Fare"] = totalFare;
        t1.Rows[t1.Rows.Count - 1]["rw_amount"] = totalAdd;
        t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


        misExp.Visible = true;
        grdExpMain.Visible = true;
        grdExpMain.DataSource = t1;
        grdExpMain.DataBind();
        int meetcnt = 0;
        foreach (GridViewRow gridRow in grdExpMain.Rows)
        {
            Label lblCat = (Label)gridRow.FindControl("lblCat");
            Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
            //Label lblDistance = (Label)gridRow.FindControl("lblDistance");
            Label lbl_ADate = (Label)gridRow.FindControl("lbl_ADate");

            // HtmlTable FrmToTable = ((HtmlTable)gridRow.FindControl(lbl_ADate.Text));
            HtmlTable FrmToTable = ((HtmlTable)gridRow.FindControl("MT_table"));
            String sVal = frmTovalues.Value;
            if (FrmToTable != null && (lblCat.Text == "EX" || lblCat.Text == "OS" || lblCat.Text == "OS-EX") && sVal.Contains(lbl_ADate.Text) && sVal.Contains("~"))
            {
                getMTTable(FrmToTable, sVal, lbl_ADate.Text);
            }

            if (lblWorkType.Text == "VAB Meeting OS")
            {
                // gridRow.BackColor = System.Drawing.Color.LightSeaGreen;
                gridRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#cfe7f7");
                // lblWorkType.Attributes.Add("style", "background-color: yellow;");
            }
            //HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));
            //if (plusMinus.Rows.Count > 0)
            //{
            //    dtlBtn.Style.Add("visibility", "hidden");
            //}
            //else
            //{
            //    if ("Field Work".Equals(lblWorkType.Text) && (("OS".Equals(lblCat.Text)) || ("OS-EX".Equals(lblCat.Text)) || ("EX".Equals(lblCat.Text))))
            //    {
            //        dtlBtn.Style.Add("visibility", "visible");

            //    }
            //}
            if (lblWorkType.Text == "Meeting")
            {
                gridRow.BackColor = System.Drawing.Color.LightCyan;
                meetcnt = meetcnt + 1;
            }
        }

        DataTable tt = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);
        if (tt.Rows.Count <= 0)
            generateEmptyOtherExpControls(Exp);
        double otherExAmnt = 0;
        DataTable customExpTable = Exp.getSavedFixedExp(monthId, yearID, sfcode);
        otherExpGrid.DataSource = customExpTable;
        otherExpGrid.DataBind();
        foreach (DataRow r in customExpTable.Rows)
        {
            otherExAmnt = otherExAmnt + Convert.ToDouble(r["amount"].ToString());

        }
        String meetcnt1 = Convert.ToString(meetcnt);
        Metcnt.InnerHtml = meetcnt1.ToString();



        //DataTable dtExp = Exp.getSavedOtheExpRecord(monthId, yearID, sfcode,dt3);
        //expGrid.DataSource = dtExp;
        //expGrid.DataBind();
        //foreach (DataRow r in dtExp.Rows)
        //{
        //    otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());

        //}

        if (!IsPostBack)
        {
            double tot = 0;
            DataTable headR1 = Exp.getHeadRecord(monthId, yearID, sfcode, dt3);
            if (headR1.Rows.Count > 0)
            {
                if (headR1.Rows[0]["Status"].ToString() == "2")
                {
                    bakfldfrce.Visible = false;
                    //btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    btnSave.Visible = false;
                   // lnk.Visible = false;
                }

                DataTable t4 = Exp.getAdmnAdjustExpnotLOP(sfcode, monthId, yearID);
                DataTable t3 = Exp.getAdmnAdjustExpLOP(sfcode, monthId, yearID);
                DataTable t5 = Exp.getAdmnAdjustExpAllw(sfcode, monthId, yearID);
                DataTable t6 = Exp.getAdmnAdjustExpFare(sfcode, monthId, yearID);
                double adminExpSum = 0;
                double adminExpSum1 = 0;
                double adminExpSumallw = 0;
                double adminExpSumfare = 0;
                foreach (DataRow r in t4.Rows)
                {
                    if (r["typ"].ToString() == "1")
                    {
                        adminExpSum = adminExpSum + Convert.ToDouble(r["amt"].ToString());
                    }
                    else if (r["typ"].ToString() == "0")
                    {
                        adminExpSum = adminExpSum - Convert.ToDouble(r["amt"].ToString());
                    }

                }
                foreach (DataRow r in t3.Rows)
                {
                    if (r["typ"].ToString() == "1")
                    {
                        adminExpSum1 = adminExpSum1 + Convert.ToDouble(r["amt"].ToString());
                    }
                    else if (r["typ"].ToString() == "0")
                    {
                        adminExpSum1 = adminExpSum1 - Convert.ToDouble(r["amt"].ToString());
                    }

                }
                foreach (DataRow r in t5.Rows)
                {
                    if (r["typ"].ToString() == "1")
                    {
                        adminExpSumallw = adminExpSumallw + Convert.ToDouble(r["amt"].ToString());
                    }
                    else if (r["typ"].ToString() == "0")
                    {
                        adminExpSumallw = adminExpSumallw - Convert.ToDouble(r["amt"].ToString());
                    }

                }
                foreach (DataRow r in t6.Rows)
                {
                    if (r["typ"].ToString() == "1")
                    {
                        adminExpSumfare = adminExpSumfare + Convert.ToDouble(r["amt"].ToString());
                    }
                    else if (r["typ"].ToString() == "0")
                    {
                        adminExpSumfare = adminExpSumfare - Convert.ToDouble(r["amt"].ToString());
                    }

                }
                double lblfaretot1 = 0;
                double lblallwtot1 = 0;
                double lblmisc1 = 0;
                if (headR1.Rows[0]["Status"].ToString() == "1")
                {
                    excesfare.Text = headR1.Rows[0]["Excess_fare"].ToString();
                    excesremks.Text = headR1.Rows[0]["exce_fare_rmks"].ToString();
                    excesallw.Text = headR1.Rows[0]["Excess_allw"].ToString();
                    excesallwrmks.Text = headR1.Rows[0]["exces_allw_rmks"].ToString();
                    lblfaretot1 = Convert.ToDouble(headR1.Rows[0]["Excess_fare"].ToString()) + totalFare + adminExpSumfare;
                    lblallwtot1 = Convert.ToDouble(headR1.Rows[0]["Excess_allw"].ToString()) + totalAllowance + adminExpSumallw;
                    fartot.Value = headR1.Rows[0]["Excess_fare"].ToString();
                    allwtot.Value = headR1.Rows[0]["Excess_allw"].ToString();
                }
                else
                {
                    excesfare.Text = headR1.Rows[0]["Excess_fare_admin"].ToString();
                    excesremks.Text = headR1.Rows[0]["exce_fare_rmks_admin"].ToString();
                    excesallw.Text = headR1.Rows[0]["Excess_allw_admin"].ToString();
                    excesallwrmks.Text = headR1.Rows[0]["exces_allw_rmks_admin"].ToString();
                    lblfaretot1 = Convert.ToDouble(headR1.Rows[0]["Excess_fare_admin"].ToString()) + totalFare + adminExpSumfare;
                    lblallwtot1 = Convert.ToDouble(headR1.Rows[0]["Excess_allw_admin"].ToString()) + totalAllowance + adminExpSumallw;
                    fartot.Value = headR1.Rows[0]["Excess_fare_admin"].ToString();
                    allwtot.Value = headR1.Rows[0]["Excess_allw_admin"].ToString();
                }
                lblmisc1 = otherExAmnt;
                double lbladddele1 = adminExpSum;
                double lbladddeleloptot = adminExpSum + adminExpSum1 + adminExpSumallw + adminExpSumfare;
                lbllop.Text = adminExpSum1.ToString();
                //totexp1.Text = headR1.Rows[0]["tot_exp"].ToString();

                tot = lblfaretot1 + lblallwtot1 + lbladddele1 + lblmisc1 + adminExpSum1;
                grandTotalName.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                iddNtTot.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblallwtot.Text = lblallwtot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblfaretot.Text = lblfaretot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lbladddele.Text = lbladddele1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

                hidLop.Value = adminExpSum1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                hdnallw.Value = adminExpSumallw.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                hdnfare.Value = adminExpSumfare.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                hidtamtval.Value = lbladddeleloptot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblmisc.Text = lblmisc1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                totexp1.Text = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));


            }
        }
        DataTable ttttr = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);
        if (ttttr.Rows.Count > 0)
            isSavedPage = true;
        //else
        //{
        //    tot = grandTotal;
        //    grandTotalName.InnerHtml = tot.ToString();
        //}
        //DataTable t2 = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);

        //if (t2.Rows.Count > 0)
        //{
        //    isSavedPage = true;
        //    grandTotalName.InnerHtml = t2.Rows[0]["grand_total"].ToString();
        //}

        Distance_calculation dsCa = new Distance_calculation();
        DataSet dsFileName = new DataSet();
        dsFileName = dsCa.getFileNamePath(sfcode, monthId, yearID);
        if (dsFileName.Tables[0].Rows.Count > 0)
        {
            if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
            {
                aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                lblViewAttach.Text = "Bills Download Here";
                //imgViewAttach.Visible = true;
            }
            else
            {
                divAttach.Visible = false;
            }
        }
        else
        {
            divAttach.Visible = false;
        }

    }


    private void generateOtherExpControls(Distance_calculation dist)
    {



        // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable t2 = dist.getAdmnAdjustExp(sfcode, monthId, yearID);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            htmlTable.Rows.RemoveAt(p);
        }
        double totAmnt = 0;
        for (int i = 0; i < t2.Rows.Count; i++)
        {

            HtmlTableRow r = new HtmlTableRow();

            string rdly = t2.Rows[i]["rdonly"].ToString();
            DropDownList d1 = new DropDownList();
            d1.ID = "date_" + i;
            d1.CssClass = "date";
            d1.Style.Add("width", "40px");
            //d1.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

            d1.Items.Insert(0, new ListItem(" --Select-- ", "0"));
            if (rdly == "1")
            {
                d1.Attributes.Add("disabled", "disabled");
            }
            int dateRange = 31;
            int currentMonth = Convert.ToInt32(monthId);
            int year = Convert.ToInt32(yearID);
            if (currentMonth == 8)
            {
                dateRange = 31;
            }
            else if ((currentMonth % 2) == 0)
            {
                dateRange = 30;
                if (currentMonth == 2)
                {
                    dateRange = 28;

                    if ((year % 400 == 0) || ((year % 4 == 0) && (year % 100 != 0)))
                    {
                        dateRange = 29;
                    }
                }

            }


            for (int j = 1; j <= dateRange; j++)
            {
                d1.Items.Insert(j, new ListItem(j + "", j + ""));
            }

            d1.SelectedValue = t2.Rows[i]["adminAdjDate"].ToString();
            HtmlTableCell cell11 = new HtmlTableCell();
            cell11.Controls.Add(d1);
            DataTable otherExp1 = dist.getOthrExp(divcode);
            DropDownList d2 = new DropDownList();
            d2.ID = "misExpList_" + i;
            d2.CssClass = "misExpList";
            d2.Style.Add("width", "100px");
            d2.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

            d2.Items.Insert(0, new ListItem(" --Select-- ", "0"));
            if (rdly == "1")
            {
                d2.Attributes.Add("disabled", "disabled");
            }
            //d2.Items.Insert(1, new ListItem("exp1", "1"));
            //d2.Items.Insert(2, new ListItem("exp2", "2"));
            if (otherExp1.Rows.Count > 0)
            {
                foreach (DataRow row in otherExp1.Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Expense_Parameter_Name"].ToString();
                    list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
                    d2.Items.Add(list);
                }
                //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
                //otherExp.DataTextField = "Expense_Parameter_Name";
            }
            // d2.SelectedValue = t2.Rows[i]["Expense_Parameter_Code"].ToString();
            d2.SelectedValue = t2.Rows[i]["Expense_Parameter_Code"].ToString() + "##" + t2.Rows[i]["Fixed_Amount"].ToString();
         
            d2.SelectedItem.Text = t2.Rows[i]["Expense_Parameter_Name"].ToString() ;
            d2.Items.Insert(0, new ListItem(" --Select-- ", "0"));
            HtmlTableCell cell22 = new HtmlTableCell();
            cell22.Controls.Add(d2);

            r.Cells.Add(cell11);
            r.Cells.Add(cell22);

            DropDownList d = new DropDownList();
            d.ID = "Combovalue_" + i;
            d.CssClass = "Combovalue";
            d.Style.Add("width", "40px");
            d.Attributes.Add("onchange", "adminAdjustCalc(this,0)");
            //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");


            d.Items.Insert(0, new ListItem("+", "1"));
            d.Items.Insert(1, new ListItem("-", "0"));
            //d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
            if (rdly == "1")
            {
                d.Attributes.Add("disabled", "disabled");
            }
            string amnt = t2.Rows[i]["amt"].ToString();
            string rm = t2.Rows[i]["Paritulars"].ToString();

            if (rm == "0")
            {
                rm = "";
            }
            if (amnt == "0")
            {
                amnt = "";
            }
            string type = t2.Rows[i]["typ"].ToString();
            d.SelectedValue = type;


            if ("1".Equals(type))
            {
                if (amnt != "")
                {
                    totAmnt = totAmnt + Convert.ToDouble(amnt);
                }
            }
            else if ("0".Equals(type))
            {
                if (amnt != "")
                {
                    totAmnt = totAmnt - Convert.ToDouble(amnt);
                }
            }
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Controls.Add(d);

            HtmlTableCell cell2 = new HtmlTableCell();
            TextBox box = new TextBox();
            Literal lit = new Literal();
            if (rdly == "1")
            {
                lit.Text = @"<input type='text' value='" + rm + "' name='tP' size='50' disabled maxlength='200' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:450px;height:19px'/>";
            }
            else
            {
                lit.Text = @"<input type='text' value='" + rm + "' name='tP' size='50' Enabled maxlength='200' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:450px;height:19px'/>";
            }
            HtmlTable table = new HtmlTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            System.IO.StringWriter tw = new System.IO.StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            box.RenderControl(hw);
            cell2.Controls.Add(lit);
            r.Cells.Add(cell1);

            HtmlTableCell cell3 = new HtmlTableCell();
            TextBox box1 = new TextBox();
            lit = new Literal();
            if (rdly == "1")
            {
                lit.Text = @"<input type='text' value='" + amnt + "' name='tAmt' disabled maxlength='10' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:50px;height:19px;text-align:right' />";
            }
            else
            {
                lit.Text = @"<input type='text' value='" + amnt + "' name='tAmt' Enabled maxlength='10' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:50px;height:19px;text-align:right' />";
            }
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            box1.RenderControl(hw);

            cell3.Controls.Add(lit);
            r.Cells.Add(cell3);
            r.Cells.Add(cell2);
            HtmlTableCell cell8 = new HtmlTableCell();
            HtmlInputHidden hdnprd = new HtmlInputHidden();
            hdnprd.ID = "hdnprd_Id";
            hdnprd.Name = "rdflg";
            hdnprd.Attributes.Add("runat", "server");
            hdnprd.Attributes.Add("AutoPostback", "true");
            if (rdly == "1")
                hdnprd.Attributes.Add("Value", rdly);
            else
                hdnprd.Attributes.Add("Value", "0");
            r.Cells.Add(cell8);

            HtmlTableCell cell4 = new HtmlTableCell();
            Button b1 = new Button();
            lit = new Literal();
            lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b1.RenderControl(hw);
            cell4.Controls.Add(lit);
            r.Cells.Add(cell4);
            //if (rdly == "1")
            //{
            //    lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' disabled onclick='_AdRowByCurrElem(this)' />"; ;
            //}
            //else
            //{ lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />"; }
                HtmlTableCell cell5 = new HtmlTableCell();
            Button b2 = new Button();
            lit = new Literal();
            if (rdly == "1")
            {
                lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' disabled onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
                
                

            }
            else
            {
                lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
                
            }
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b2.RenderControl(hw);

            cell5.Controls.Add(lit);

            r.Cells.Add(cell5);






            htmlTable.Rows.Add(r);
        }
        hidtamtval.Value = totAmnt + "";

    }

    private void generateEmptyOtherExpControls(Distance_calculation dist)
    {
        // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable t2 = dist.getAdmnAdjustExp(sfcode, monthId, yearID);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            htmlTable.Rows.RemoveAt(p);
        }
        //int totAmnt = 0;
        //for (int i = 0; i < t2.Rows.Count; i++)
        //{

        HtmlTableRow r = new HtmlTableRow();


        DropDownList d1 = new DropDownList();
        d1.ID = "date_" + 0;
        d1.CssClass = "date";
        d1.Style.Add("width", "40px");
        //d1.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

        d1.Items.Insert(0, new ListItem(" --Select-- ", "0"));

        int dateRange = 31;
        int currentMonth = Convert.ToInt32(monthId);
        int year = Convert.ToInt32(yearID);
        if (currentMonth == 8)
        {
            dateRange = 31;
        }
        else if ((currentMonth % 2) == 0)
        {
            dateRange = 30;
            if (currentMonth == 2)
            {
                dateRange = 28;

                if ((year % 400 == 0) || ((year % 4 == 0) && (year % 100 != 0)))
                {
                    dateRange = 29;
                }
            }

        }


        for (int j = 1; j <= dateRange; j++)
        {
            d1.Items.Insert(j, new ListItem(j + "", j + ""));
        }
        //d1.SelectedValue = t2.Rows[i]["adminAdjDate"].ToString();
        HtmlTableCell cell11 = new HtmlTableCell();
        cell11.Controls.Add(d1);
        DataTable otherExp1 = dist.getOthrExp(divcode);
        DropDownList d2 = new DropDownList();
        d2.ID = "misExpList_" + 0;
        d2.CssClass = "misExpList";
        d2.Style.Add("width", "100px");
        d2.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

        d2.Items.Insert(0, new ListItem(" --Select-- ", "0"));
        //d2.Items.Insert(1, new ListItem("exp1", "1"));
        //d2.Items.Insert(2, new ListItem("exp2", "2"));
        if (otherExp1.Rows.Count > 0)
        {
            foreach (DataRow row in otherExp1.Rows)
            {
                ListItem list = new ListItem();
                list.Text = row["Expense_Parameter_Name"].ToString();
                list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
                d2.Items.Add(list);
            }
            //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
            //otherExp.DataTextField = "Expense_Parameter_Name";
        }
        //d2.SelectedValue = t2.Rows[i]["Expense_Parameter_Code"].ToString();

        HtmlTableCell cell22 = new HtmlTableCell();
        cell22.Controls.Add(d2);

        r.Cells.Add(cell11);
        r.Cells.Add(cell22);

        DropDownList d = new DropDownList();
        d.ID = "Combovalue_" + 0;
        d.CssClass = "Combovalue";
        d.Style.Add("width", "40px");
        d.Attributes.Add("onchange", "adminAdjustCalc(this,0)");
        //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");


        d.Items.Insert(0, new ListItem("+", "1"));
        d.Items.Insert(1, new ListItem("-", "0"));
        // d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
        // d.SelectedValue = "1";
        /*string amnt = t2.Rows[i]["amt"].ToString();
        string rm = t2.Rows[i]["Paritulars"].ToString();
        if (rm == "0")
        {
            rm = "";
        }
        if (amnt == "0")
        {
            amnt = "";
        }
        string type = t2.Rows[i]["typ"].ToString();
        d.SelectedValue = type;


        if ("1".Equals(type))
        {
            totAmnt = totAmnt + Convert.ToInt32(amnt);
        }
        else if ("0".Equals(type))
        {
            totAmnt = totAmnt - Convert.ToInt32(amnt);
        }*/
        HtmlTableCell cell1 = new HtmlTableCell();
        cell1.Controls.Add(d);

        HtmlTableCell cell2 = new HtmlTableCell();
        TextBox box = new TextBox();
        Literal lit = new Literal();
        lit.Text = @"<input type='text' value='" + "" + "' name='tP' size='50' maxlength='200' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:450px;height:19px'/>";
        HtmlTable table = new HtmlTable();
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
        System.IO.StringWriter tw = new System.IO.StringWriter(sb);
        HtmlTextWriter hw = new HtmlTextWriter(tw);
        box.RenderControl(hw);


        cell2.Controls.Add(lit);
        r.Cells.Add(cell1);


        HtmlTableCell cell3 = new HtmlTableCell();
        TextBox box1 = new TextBox();
        lit = new Literal();
        lit.Text = @"<input type='text' value='" + "" + "' name='tAmt' maxlength='10' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:50px;height:19px;text-align:right' />";
        sb = new System.Text.StringBuilder("");
        tw = new System.IO.StringWriter(sb);
        hw = new HtmlTextWriter(tw);
        box1.RenderControl(hw);

        cell3.Controls.Add(lit);
        r.Cells.Add(cell3);
        r.Cells.Add(cell2);
        HtmlTableCell cell4 = new HtmlTableCell();
        Button b1 = new Button();
        lit = new Literal();
        lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
        sb = new System.Text.StringBuilder("");
        tw = new System.IO.StringWriter(sb);
        hw = new HtmlTextWriter(tw);
        b1.RenderControl(hw);
        cell4.Controls.Add(lit);
        r.Cells.Add(cell4);
        HtmlTableCell cell5 = new HtmlTableCell();
        Button b2 = new Button();
        lit = new Literal();
        lit.Text = @"<input type='button' id='btndel' value=' - '  class='btnSave' onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
        sb = new System.Text.StringBuilder("");
        tw = new System.IO.StringWriter(sb);
        hw = new HtmlTextWriter(tw);
        b2.RenderControl(hw);

        cell5.Controls.Add(lit);

        r.Cells.Add(cell5);

        htmlTable.Rows.Add(r);
        //}
        hidtamtval.Value = "0";

    }
    protected override void OnLoadComplete(EventArgs e)
    {
        if (isSavedPage)
        {
            Distance_calculation dist = new Distance_calculation();
            generateOtherExpControls(dist);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        saveData(true);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
window.parent.opener.location.reload();
                               </script>";
        base.Response.Write(close);

        //string close = @"<script type='text/javascript'>
        //                        window.returnValue = true;
        //                        window.close();
        //                        </script>";
        //base.Response.Write(close);

        //Response.Redirect("RptAutoExpense_Approve.aspx");

    }
    protected void btnField_Click(object sender, EventArgs e)
    {
        DataTable ds = Exp.getFieldForce(divcode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        Exp.deleteAllExpenseSavedRecord(monthId, yearID, sfcode, dt3);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
        base.Response.Write(close);

        // Response.Redirect("RptAutoExpense_Approve.aspx");
    }

    protected void btnField_Click_Edit(object sender, EventArgs e)
    {


        DataTable ds = Exp.getFieldForce(divcode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        Exp.editAllExpenseSavedRecord(monthId, yearID, sfcode);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
        base.Response.Write(close);


        // Response.Redirect("RptAutoExpense_Approve.aspx");
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {

        saveData(false);
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
        base.Response.Write(close);

        //Response.Redirect("RptAutoExpense_Approve.aspx");

    }

    private void saveData(bool flag)
    {


        isSavedPage = true;
        int iReturn = -1;
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        //rmks = approveTextId.InnerText;
        // String gt = grandTotalName.InnerHtml;
        DataTable ds = Exp.getFieldForce(divcode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        string Excess_fare_admin = excesfare.Text.Replace(",", "");
        string exces_allw_rmks_admin = excesremks.Text;
        string Excess_allw_admin = excesallw.Text.Replace(",", "");
        string exce_fare_rmks_admin = excesallwrmks.Text;
        string allw_tot_admin = allwtot.Value.Replace(",", "");
        string Far_tot_admin = fartot.Value.Replace(",", "");
        string Add_deduct_tot_admin = adddeducttot.Value;
        string Misc_tot_admin = lblmisc.Text.Replace(",", "");
        string lop_admin = "";
        string tot_exp_admin = "";
        iReturn = Exp.deleteAdminAdjustExp(sfcode, monthId, yearID, dt3);
        string[] splitVal = otherExpValues.Value.Split('~');

        string[] pert = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] op = splitVal[2].Split(',');
        string[] date = splitVal[4].Split(',');
        string[] misExpVal = splitVal[5].Split(',');
        string ipaddr = Request.ServerVariables["REMOTE_ADDR"].ToString();

        iReturn = Exp.updateHeadFlg_Tablets(flag ? "2" : "3", sfcode, monthId, yearID, rmks, dt3, Excess_fare_admin, Excess_allw_admin, Far_tot_admin, allw_tot_admin, Add_deduct_tot_admin, Misc_tot_admin, tot_exp_admin, lop_admin, exces_allw_rmks_admin, exce_fare_rmks_admin, ipaddr, sfff, sff_name);
        for (int p = 0; p < op.Length; p++)
        {
            string[] e = op[p].Split('=');
            string[] dateVal = date[p].Split('=');
            string[] misExp = misExpVal[p].Split('=');
            iReturn = Exp.addAdminAdjustmentExpRecord(dateVal[0], misExp[0], misExp[1], e[0], pert[p] == "" ? "0" : pert[p], amount[p] == "" ? "0" : amount[p], splitVal[3], sfcode, monthId, yearID, dt3);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
    }
    private void addColStyle(HtmlTableCell cell)
    {
        cell.Style.Add(HtmlTextWriterStyle.BorderColor, "#9AA3A9");

        cell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
        cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "Solid");
        cell.Style.Add(HtmlTextWriterStyle.Padding, "10");

    }
    private void addHeaderColStyle(HtmlTableCell cell)
    {
        cell.Style.Add(HtmlTextWriterStyle.BorderColor, "#9AA3A9");
        cell.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
        cell.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
        cell.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
        cell.Style.Add(HtmlTextWriterStyle.BorderStyle, "Solid");
        cell.Style.Add(HtmlTextWriterStyle.Padding, "10");

    }

    private HtmlTable getMTTable(HtmlTable htmlTable, String hidValues, String date)
    {

        for (int p = htmlTable.Rows.Count - 1; p >= 0; p--)
        {
            if (htmlTable.Rows.Count > 0)
                htmlTable.Rows.RemoveAt(p);
            else
            {
                //generateOtherExpListData(otherExp1);
            }

        }

        HtmlTableRow r = new HtmlTableRow();
        HtmlTableCell cell1 = new HtmlTableCell();
        Label label = new Label();
        label.Text = "From";
        addHeaderColStyle(cell1);
        cell1.Controls.Add(label);
        addColStyle(cell1);

        HtmlTableCell cell2 = new HtmlTableCell();
        label = new Label();
        label.Text = "To";
        addHeaderColStyle(cell2);
        cell2.Controls.Add(label);
        addColStyle(cell2);


        HtmlTableCell cell3 = new HtmlTableCell();
        label = new Label();
        label.Text = "Distance";
        addHeaderColStyle(cell3);
        cell3.Controls.Add(label);
        addColStyle(cell3);
        cell3.NoWrap = true;

        HtmlTableCell cell4 = new HtmlTableCell();
        label = new Label();
        label.Text = "Fare";
        addHeaderColStyle(cell4);
        cell4.Controls.Add(label);
        addColStyle(cell4);

        r.Cells.Add(cell1);
        r.Cells.Add(cell2);
        r.Cells.Add(cell3);
        r.Cells.Add(cell4);
        // htmlTable.Rows.Add(r);

        hidValues = hidValues + '#';
        int idx = hidValues.IndexOf(date);
        //alert(date);
        //alert(sVal);
        String[] placewithdis = "From$To$0$0".Split('$');
        //alert($st);
        if (idx > -1)
        {
            idx = idx + (date).Length + 9;
            int et = hidValues.IndexOf("#", idx);
            //alert($et);
            placewithdis = hidValues.Substring(idx, et - idx).Split('$');
            //alert(placewithdis[0]);

        }
        Double fareTot = 0;
        Double distTot = 0;
        Dictionary<String, String> distances = new Dictionary<String, String>();
        distances.Add("Row1", "From$To$Distance$Fare");
        for (int i = 0; i < placewithdis.Length; i++)
        {
            String[] data = placewithdis[i].Split('~');
            //if (data.Length > 1)
            //{
            //    if (data[3] == "" || data[3] == "undefined")
            //        data[3] = "0";
            //    if ((data[4] == "") || (data[4] == "undefined"))
            //        data[4] = "0";
            //    String values = data[0] + "$" + data[1] + "$" + data[2] + " X " + data[4] + "$" + data[3];
            //    distances.Add("Row" + (i + 2), values);
            //    if (data[2] == "" || data[2] == "undefined")
            //        data[2] = "0";

            //    fareTot = fareTot + Double.Parse(data[3]);
            //    distTot = distTot + Double.Parse(data[2]);
            //}

            if (data.Length == 1)
            {

                String values = data[0] + "$" + 0 + "$" + 0 + " X " + 0 + "$" + 0;
                distances.Add("Row" + (i + 2), values);

                fareTot = fareTot + Double.Parse("0");
                distTot = distTot + Double.Parse("0");
            }
            else if (data.Length == 2)
            {

                String values = data[0] + "$" + data[1] + "$" + 0 + " X " + 0 + "$" + 0;
                distances.Add("Row" + (i + 2), values);

                fareTot = fareTot + Double.Parse("0");
                distTot = distTot + Double.Parse("0");
            }
            else if (data.Length == 3)
            {

                if (data[2] == "" || data[2] == "undefined")
                    data[2] = "0";

                String values = data[0] + "$" + data[1] + "$" + data[2] + " X " + 0 + "$" + 0;
                distances.Add("Row" + (i + 2), values);

                fareTot = fareTot + Double.Parse("0");
                distTot = distTot + Double.Parse(data[2]);
            }
            else if (data.Length == 4)
            {

                if (data[2] == "" || data[2] == "undefined")
                    data[2] = "0";
                if (data[3] == "" || data[3] == "undefined")
                    data[3] = "0";
                String values = data[0] + "$" + data[1] + "$" + data[2] + " X " + 0 + "$" + data[3];
                distances.Add("Row" + (i + 2), values);

                fareTot = fareTot + Double.Parse(data[3]);
                distTot = distTot + Double.Parse(data[2]);
            }
            else if (data.Length == 5)
            {
                if ((data[4] == "") || (data[4] == "undefined"))
                    data[4] = "0";
                if (data[2] == "" || data[2] == "undefined")
                    data[2] = "0";
                if (data[3] == "" || data[3] == "undefined")
                    data[3] = "0";
                String values = data[0] + "$" + data[1] + "$" + data[2] + " X " + data[4] + "$" + data[3];
                distances.Add("Row" + (i + 2), values);

                fareTot = fareTot + Double.Parse(data[3]);
                distTot = distTot + Double.Parse(data[2]);
            }
        }
        distances.Add("Row" + placewithdis.Length + 1, "Total" + "$$" + distTot + "$" + fareTot);

        foreach (String val in distances.Values)
        {
            String[] rowVal = val.Split('$');


            //HtmlTable htmlTable = new HtmlTable();
            r = new HtmlTableRow();
            cell1 = new HtmlTableCell();
            label = new Label();
            label.Text = rowVal[0] != null ? rowVal[0] : "";
            cell1.Controls.Add(label);
            addColStyle(cell1);

            cell2 = new HtmlTableCell();
            label = new Label();
            label.Text = rowVal[1] != null ? rowVal[1] : "";
            cell2.Controls.Add(label);
            addColStyle(cell2);


            cell3 = new HtmlTableCell();
            label = new Label();
            label.Text = rowVal[2] != null ? rowVal[2] : "";
            cell3.Controls.Add(label);
            addColStyle(cell3);

            cell4 = new HtmlTableCell();
            label = new Label();
            label.Text = rowVal[3] != null ? rowVal[3] : "";
            cell4.Controls.Add(label);
            addColStyle(cell4);

            r.Cells.Add(cell1);
            r.Cells.Add(cell2);
            r.Cells.Add(cell3);
            r.Cells.Add(cell4);
            htmlTable.Rows.Add(r);
        }
        return htmlTable;

    }
}