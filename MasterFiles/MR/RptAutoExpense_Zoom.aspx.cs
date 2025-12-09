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
    string divCode = "";
    string gt = "";
    Hashtable months = new Hashtable();
    Distance_calculation Exp = new Distance_calculation();
    protected void Page_Load(object sender, EventArgs e)
    {
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
        divCode = Convert.ToString(Session["div_code"]);
        monthId = Request.QueryString["month"].ToString();
        yearID = Request.QueryString["year"].ToString();
        //mnthtxtId.InnerHtml = monthId;
        mnthtxtId.InnerHtml = months[Request.QueryString["month"].ToString()].ToString();
        yrtxtId.InnerHtml = yearID;
        DataTable rt = Exp.getMgrAppr(divCode);
        if (rt.Rows.Count > 0)
        {
            if ("Y".Equals(rt.Rows[0]["Row_wise_textbox"].ToString()))
            {
                grdExpMain.Columns[10].Visible = true;
                grdExpMain.Columns[11].Visible = true;

            }
        }
        DataTable table = Exp.getSavedHeadRecord(monthId, yearID, sfcode);

        if (table.Rows.Count > 0)
        {
            if ("2".Equals(table.Rows[0]["sndhqfl"].ToString()) || "8".Equals(table.Rows[0]["sndhqfl"].ToString()))
            {
                msgId.Visible = false;
                MainId1.Visible = true;
                
                
                

            }
            else
            {
                msgId.Visible = true;
                MainId1.Visible = false;

            }
        }
        else
        {
            msgId.Visible = true;
            return;
        }
        DataTable ds = Exp.getFieldForce(divCode, sfcode);
        DataTable dsdv = Exp.getdiv(divCode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        if ("2".Equals(ds.Rows[0]["sf_type"].ToString()))
        {
            grdExpMain.Columns[7].Visible = false;



        }
        fieldforceId.InnerHtml = "Name :" + ds.Rows[0]["sf_name"].ToString();

        empId.InnerHtml = "Emp.ID :" + "<u>"+ds.Rows[0]["Employee_Id"].ToString()+"</u>";
        hqdesig.InnerHtml = "Designation :" + "<u>" + ds.Rows[0]["sf_designation_short_name"].ToString() + "</u>";
        hqid.InnerHtml = "HQ :" + "<u>" + ds.Rows[0]["sf_hq"].ToString() + "</u>";
        mgrName.InnerHtml = "Manager Name :" + ds.Rows[0]["report_sf"].ToString();
        divid.InnerHtml = dsdv.Rows[0]["div_name"].ToString();
        DataTable headerDataSet = Exp.getSavedHeadRecord(monthId, yearID, sfcode);
        if (headerDataSet.Rows.Count > 0)
        {
            string rms = headerDataSet.Rows[0]["Admin_Remarks"].ToString();
            approveTextId.InnerText = rms;
            string rms1 = headerDataSet.Rows[0]["Remarks"].ToString();
            //MgrTextId.InnerText = rms1;
            //lblrmks1.InnerText = rms;
        }
        DataTable t1 = Exp.getSavedRecord(monthId, yearID, sfcode,dt3);
        double totalAllowance = 0;
        double totalDistance = 0;
        double totalFare = 0;
        double totaddExp = 0;
        double grandTotal = 0;
        foreach (DataRow row in t1.Rows)
        {
            totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
            totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
            totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
            totaddExp = totaddExp + Convert.ToDouble(row["rw_amount"].ToString());
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
        t1.Rows[t1.Rows.Count - 1]["rw_amount"] = totaddExp;
        t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


        misExp.Visible = true;
        grdExpMain.Visible = true;
        grdExpMain.DataSource = t1;
        grdExpMain.DataBind();
     

        foreach (GridViewRow gridRow in grdExpMain.Rows)
        {
            Label lblCat = (Label)gridRow.FindControl("lblCat");
            Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
            //Label lblDistance = (Label)gridRow.FindControl("lblDistance");


            //HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));
            
            //    if ("Field Work".Equals(lblWorkType.Text) && (("OS".Equals(lblCat.Text)) || ("OS-EX".Equals(lblCat.Text)) || ("EX".Equals(lblCat.Text))))
            //    {
            //        dtlBtn.Style.Add("visibility", "visible");

            //    }




Label lbl_ADate = (Label)gridRow.FindControl("lbl_ADate");

// HtmlTable FrmToTable = ((HtmlTable)gridRow.FindControl(lbl_ADate.Text));
HtmlTable FrmToTable = ((HtmlTable)gridRow.FindControl("MT_table"));
String sVal = frmTovalues.Value;
            if (FrmToTable != null && (lblCat.Text == "EX" || lblCat.Text == "OS" || lblCat.Text == "OS-EX") && sVal.Contains(lbl_ADate.Text))
            {
            getMTTable(FrmToTable, sVal, lbl_ADate.Text);
                }
            
        }
        //generateOtherExpControls(Exp);
        double otherExAmnt = 0;
        DataTable customExpTable = Exp.getSavedFixedExp(monthId, yearID, sfcode);
        otherExpGrid.DataSource = customExpTable;
        otherExpGrid.DataBind();
        foreach (DataRow r in customExpTable.Rows)
        {
            otherExAmnt = otherExAmnt + Convert.ToDouble(r["amount"].ToString());

        }

        //DataTable dtExp = Exp.getSavedOtheExpRecord(monthId, yearID, sfcode,dt3);
        //expGrid.DataSource = dtExp;
        //expGrid.DataBind();
        //foreach (DataRow r in dtExp.Rows)
        //{
        //    otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());

        //}
        DataTable adminExp = Exp.getSavedAdminExpRecord(monthId, yearID, sfcode);
        adminExpGrid.DataSource = adminExp;
        adminExpGrid.DataBind();
        foreach (DataRow r in adminExp.Rows)
        {
            if (r["typ"].ToString() == "1")
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());
            }
            else if (r["typ"].ToString() == "0")
            {
                otherExAmnt = otherExAmnt - Convert.ToDouble(r["amt"].ToString());
            }

        }
        double tot = otherExAmnt + grandTotal;
        grandTotalName.InnerHtml = tot.ToString();
        DataTable t2 = Exp.getAdmnAdjustExp(sfcode, monthId, yearID);

        if (t2.Rows.Count > 0)
        {
            isSavedPage = true;
            grandTotalName.InnerHtml = t2.Rows[0]["grand_total"].ToString();
        }

        
        foreach (GridViewRow gridRow in adminExpGrid.Rows)
        {
            Label lblTyp = (Label)gridRow.FindControl("lblTyp");
            if (lblTyp.Text == "0")
            {
                lblTyp.Text = " - ";
            }
            else if (lblTyp.Text == "1")
            {
                lblTyp.Text = " + ";
            }
        }
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



        double tot1 = 0;
        DataTable headR1 = Exp.getHeadRecord(monthId, yearID, sfcode, dt3);
        if (headR1.Rows.Count > 0)
        {

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
                // fartot.Value = headR1.Rows[0]["Excess_fare"].ToString();
                // allwtot.Value = headR1.Rows[0]["Excess_allw"].ToString();
            }
            else
            {
                excesfare.Text = headR1.Rows[0]["Excess_fare_admin"].ToString();
                excesremks.Text = headR1.Rows[0]["exce_fare_rmks_admin"].ToString();
                excesallw.Text = headR1.Rows[0]["Excess_allw_admin"].ToString();
                excesallwrmks.Text = headR1.Rows[0]["exces_allw_rmks_admin"].ToString();
                lblfaretot1 = Convert.ToDouble(headR1.Rows[0]["Excess_fare_admin"].ToString()) + totalFare + adminExpSumfare;
                lblallwtot1 = Convert.ToDouble(headR1.Rows[0]["Excess_allw_admin"].ToString()) + totalAllowance + adminExpSumallw;
                // fartot.Value = headR1.Rows[0]["Excess_fare_admin"].ToString();
                //allwtot.Value = headR1.Rows[0]["Excess_allw_admin"].ToString();
            }
            lblmisc1 = Convert.ToDouble(headR1.Rows[0]["Misc_tot"].ToString());
            double lbladddele1 = adminExpSum;
            double lbladddeleloptot = adminExpSum + adminExpSum1 + adminExpSumallw + adminExpSumfare;
            lbllop.Text = adminExpSum1.ToString();
            //totexp1.Text = headR1.Rows[0]["tot_exp"].ToString();

            tot1 = lblfaretot1 + lblallwtot1 + lbladddele1 + lblmisc1 + adminExpSum1;
            grandTotalName.InnerHtml = Math.Round(tot1).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            iddNtTot.InnerHtml = Math.Round(tot1).ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            lblallwtot.Text = lblallwtot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            lblfaretot.Text = lblfaretot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            lbladddele.Text = lbladddele1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

            //hidLop.Value = adminExpSum1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            //hdnallw.Value = adminExpSumallw.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            //hdnfare.Value = adminExpSumfare.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            hidtamtval.Value = lbladddeleloptot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            lblmisc.Text = lblmisc1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            totexp1.Text = tot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
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
            if ((data[4] == "") || (data[4] == "undefined"))
                data[4] = "0";
            String values = data[0] + "$" + data[1] + "$" + data[2] + " X " + data[4] + "$" + data[3];
            distances.Add("Row" + (i + 2), values);
            if (data[2] == "" || data[2] == "undefined")
                data[2] = "0";
            if (data[3] == "" || data[3] == "undefined")
                data[3] = "0";
            fareTot = fareTot + Double.Parse(data[3]);
            distTot = distTot + Double.Parse(data[2]);
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