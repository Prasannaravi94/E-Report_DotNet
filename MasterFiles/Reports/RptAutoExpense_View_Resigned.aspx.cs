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
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    bool isSavedPage  = false;
    string sfcode = "";
    string monthId = "";
    string yearID = "";
    string lastdcrdt = "";
    string rsname = "";
    string divCode = "";
    string gt = "";
    string slno = "";
    string rmks;
    string rmks1;
    string empidnew = "";
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
        divCode = Request.QueryString["divCode"].ToString();
        monthId = Request.QueryString["monthId"].ToString();
        yearID = Request.QueryString["yearID"].ToString();
        lastdcrdt = Request.QueryString["LstDCRdt"].ToString();
        empidnew = Request.QueryString["empid"].ToString();
        rsname = Request.QueryString["sname"].ToString();
        slno=Request.QueryString["slno"].ToString();
        mnthtxtId.InnerHtml = months[Request.QueryString["monthId"].ToString()].ToString();
        yrtxtId.InnerHtml = Request.QueryString["yearID"].ToString();
        DataTable ds = Exp.getFieldForceResigned(divCode, sfcode);
       
            //dsSf = sf1.CheckSFName_DCREntry_Check(strSF_Code, Convert.ToInt16(ddlMonth.SelectedValue), Convert.ToInt16(ddlYear.SelectedValue));
        fieldforceId.InnerHtml = "Fieldforce Name :" + rsname;
            hqId.InnerHtml = "Employee Code :" + ds.Rows[0]["sf_hq"].ToString();

        // empid1.InnerHtml = "Employee Id :" + ds.Rows[0]["Employee_Id"].ToString();
        empid1.InnerHtml = "Employee Id :" + empidnew;
        //fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
        //hqId.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
        empId.InnerHtml = "Designation :" + ds.Rows[0]["sf_designation_short_name"].ToString();
        DataTable adminExp = Exp.getSavedAdminExpRecord1Resigned(monthId, yearID, sfcode, slno);
        adminExpGrid.DataSource = adminExp;
        adminExpGrid.DataBind();
        DataTable headerDataSet = Exp.getSavedHeadRecordResigned(monthId, yearID, sfcode, slno);
        if (headerDataSet.Rows.Count > 0)
        {
            string rms = headerDataSet.Rows[0]["Admin_Remarks"].ToString();
            approveTextId.InnerText = rms;
            lblrmks1.InnerText = rms;
        }
        DataTable t1 = Exp.getSavedRecordResigned(monthId, yearID, sfcode, slno);
        double totalAllowance = 0;
        double totalDistance = 0;
        double totalFare = 0;
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


            HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));

            if ("Field Work".Equals(lblWorkType.Text) && (("OS".Equals(lblCat.Text)) || ("OS-EX".Equals(lblCat.Text)) || ("EX".Equals(lblCat.Text))))
            {
                dtlBtn.Style.Add("visibility", "visible");

            }
        }
        DataTable tt = Exp.getAdmnAdjustExpResigned(sfcode, monthId, yearID, slno);
        if(tt.Rows.Count<=0)
            generateEmptyOtherExpControls(Exp);
        double otherExAmnt = 0;
        DataTable customExpTable = Exp.getSavedFixedExpResigned(monthId, yearID, sfcode, slno); 
            otherExpGrid.DataSource = customExpTable;
            otherExpGrid.DataBind();
            foreach (DataRow r in customExpTable.Rows)
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["amount"].ToString());

            }

            DataTable dtExp = Exp.getSavedOtheExpRecordResigned(monthId, yearID, sfcode, slno);
            expGrid.DataSource = dtExp;
            expGrid.DataBind();
            foreach(DataRow r in dtExp.Rows)
            {
                otherExAmnt = otherExAmnt + Convert.ToDouble(r["amt"].ToString());

            }
            double tot = otherExAmnt + grandTotal;
            grandTotalName.InnerHtml = tot.ToString();
            DataTable t2 = Exp.getAdmnAdjustExpResigned(sfcode, monthId, yearID,slno);

            if (t2.Rows.Count > 0)
            {
                isSavedPage = true;
                grandTotalName.InnerHtml = t2.Rows[0]["grand_total"].ToString();
            }

            Distance_calculation dsCa = new Distance_calculation();
            DataSet dsFileName = new DataSet();
            dsFileName = dsCa.getFileNamePath(sfcode, monthId, yearID);

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


 private void generateOtherExpControls(Distance_calculation dist)
 {



    // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
     HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
     DataTable t2 = dist.getAdmnAdjustExpResigned(sfcode, monthId, yearID,slno);

     for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
     {
         htmlTable.Rows.RemoveAt(p);
     }
     double totAmnt = 0;
     for (int i = 0; i < t2.Rows.Count; i++)
     {

         HtmlTableRow r = new HtmlTableRow();

         
         DropDownList d1 = new DropDownList();
         d1.ID = "date_" + i;
         d1.CssClass = "date";
         //d1.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

         d1.Items.Insert(0, new ListItem(" --Select-- ", "0"));

         int dateRange = 31;
         int currentMonth=Convert.ToInt32(monthId);
         int year = Convert.ToInt32(yearID);
         if (currentMonth == 8)
         {
             dateRange = 31;
         }
         else if ((currentMonth%2)==0)
         {
             dateRange = 30;
             if(currentMonth==2)
             {
                 dateRange = 28;
                 
                if((year % 400 == 0) || ((year % 4 == 0) && (year % 100 != 0)))
                {
                    dateRange=29;
                }
             }

         }


         for (int j = 1; j <= dateRange; j++)
         {
         d1.Items.Insert(j, new ListItem(j+"", j+""));
         }
         d1.SelectedValue = t2.Rows[i]["adminAdjDate"].ToString(); 
         HtmlTableCell cell11 = new HtmlTableCell();
         cell11.Controls.Add(d1);
         DataTable otherExp1 = dist.getOthrExp(divCode);
         DropDownList d2 = new DropDownList();
         d2.ID = "misExpList_" + i;
         d2.CssClass = "misExpList";

         //d2.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

         d2.Items.Insert(0, new ListItem(" --Select-- ", "0"));
         //d2.Items.Insert(1, new ListItem("exp1", "1"));
         //d2.Items.Insert(2, new ListItem("exp2", "2"));
         if (otherExp1.Rows.Count > 0)
         {
             foreach (DataRow row in otherExp1.Rows)
             {
                 ListItem list = new ListItem();
                 list.Text = row["Expense_Parameter_Name"].ToString();
                 list.Value = row["Expense_Parameter_Code"].ToString();
                 d2.Items.Add(list);
             }
             //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
             //otherExp.DataTextField = "Expense_Parameter_Name";
         }
         d2.SelectedValue = t2.Rows[i]["Expense_Parameter_Code"].ToString();

         HtmlTableCell cell22 = new HtmlTableCell();
         cell22.Controls.Add(d2);

         r.Cells.Add(cell11);
         r.Cells.Add(cell22);
         
         DropDownList d = new DropDownList();
         d.ID = "Combovalue_" + i;
         d.CssClass = "Combovalue";
         d.Attributes.Add("onchange","adminAdjustCalc(this,0)");
         //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");
             
             d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
             d.Items.Insert(1, new ListItem("+", "1"));
             d.Items.Insert(2, new ListItem("-", "0"));
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


         if("1".Equals(type))
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
         lit.Text = @"<input type='text' value='"+rm+"' name='tP' size='50' maxlength='50' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:90%;height:19px'/>";
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
         lit.Text = @"<input type='text' value='"+amnt+"' name='tAmt' maxlength='50' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:90%;height:19px;text-align:right' />";
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
         lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
         sb = new System.Text.StringBuilder("");
         tw = new System.IO.StringWriter(sb);
         hw = new HtmlTextWriter(tw);
         b2.RenderControl(hw);

         cell5.Controls.Add(lit);

         r.Cells.Add(cell5);

         htmlTable.Rows.Add(r);
     }
     hidtamtval.Value = totAmnt+"";

 }

 private void generateEmptyOtherExpControls(Distance_calculation dist)
 {
     // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
     HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
     DataTable t2 = dist.getAdmnAdjustExpResigned(sfcode, monthId, yearID,slno);

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
         DataTable otherExp1 = dist.getOthrExp(divCode);
         DropDownList d2 = new DropDownList();
         d2.ID = "misExpList_" + 0;
         d2.CssClass = "misExpList";

         //d2.Attributes.Add("onchange", "adminAdjustCalc(this,0)");

         d2.Items.Insert(0, new ListItem(" --Select-- ", "0"));
         //d2.Items.Insert(1, new ListItem("exp1", "1"));
         //d2.Items.Insert(2, new ListItem("exp2", "2"));
         if (otherExp1.Rows.Count > 0)
         {
             foreach (DataRow row in otherExp1.Rows)
             {
                 ListItem list = new ListItem();
                 list.Text = row["Expense_Parameter_Name"].ToString();
                 list.Value = row["Expense_Parameter_Code"].ToString();
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
         d.Attributes.Add("onchange", "adminAdjustCalc(this,0)");
         //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");

         d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
         d.Items.Insert(1, new ListItem("+", "1"));
         d.Items.Insert(2, new ListItem("-", "0"));
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
         lit.Text = @"<input type='text' value='" + "" + "' name='tP' size='50' maxlength='50' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:90%;height:19px'/>";
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
         lit.Text = @"<input type='text' value='" + "" + "' name='tAmt' maxlength='50' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:90%;height:19px;text-align:right' />";
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
         lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForAdmin(this,this.parentNode.parentNode,1)' />";
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
                                </script>";
        base.Response.Write(close);
        
        //Response.Redirect("RptAutoExpense_Approve.aspx");

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
        DataTable ds = Exp.getFieldForce(divCode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        iReturn = Exp.deleteAdminAdjustExpResigned(sfcode, monthId, yearID,slno);
        string[] splitVal = otherExpValues.Value.Split('~');

        string[] pert = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] op = splitVal[2].Split(',');
        string[] date = splitVal[4].Split(',');
        string[] misExpVal = splitVal[5].Split(',');
        
        iReturn=Exp.updateHeadFlgResigned(flag?"2":"3",sfcode, monthId, yearID,rmks,slno);
        for (int p = 0; p < op.Length; p++)
        {
            string[] e = op[p].Split('=');
            string[] dateVal = date[p].Split('=');
            string[] misExp = misExpVal[p].Split('=');
            iReturn = Exp.addAdminAdjustmentExpRecordResigned(dateVal[0], misExp[0], misExp[1], e[0], pert[p] == "" ? "0" : pert[p], amount[p] == "" ? "0" : amount[p], splitVal[3], sfcode, monthId, yearID,slno);
        }
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
    }
}