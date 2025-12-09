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
using System.Data.SqlClient;

public partial class MasterFiles_MGR_RptAutoExpense_Mgr : System.Web.UI.Page
{

    bool isSavedPage = false;
    bool isEmpty = true;
    string sfcode = string.Empty;
    string divcode = string.Empty;
    string sessf_name = string.Empty;
    string strFileDateTime = string.Empty;
    DataSet expenseDataset = null;
    DataSet placeDataset = null;
    double otherExpAmnttot = 0;
    ArrayList pList = new ArrayList();
    int sNo = 0;
    string home = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Convert.ToString(Session["Sf_Code"]);
        sessf_name = Session["sf_name"].ToString();
        divcode = Convert.ToString(Session["div_code"]);
        if (Request.QueryString["home"] != null)
        {
            home = Request.QueryString["home"].ToString();
        }

        sfCode.Value = sfcode;
        divCode.Value = divcode;

        if (!IsPostBack)
        {
            GetMyMonthList();
            bind_year_ddl();
            if ("1".Equals(home))
            {
                menuId.Visible = false;
                btnBack.Visible = false;
            }

        }
        if (Request.QueryString["type"] != null)
        {

            string mon = Request.QueryString["mon"].ToString();
            string yr = Request.QueryString["year"].ToString();
            sfcode = Request.QueryString["sf_code"].ToString();
            monthId.SelectedValue = mon;
            yearID.SelectedValue = yr;



            btnBack.Visible = false;


            btnSubmit_Click(btnSubmit, null);
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
    private string getDisplayToPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                string p1 = p.Substring(0, p.IndexOf('('));

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        distinctValues = distinctValues + "," + p1;
                    else
                        distinctValues = distinctValues + "," + p1;
                }
                else
                    distinctValues = p1;


                counter++;

            }
        }
        return distinctValues;
    }
    private string getDisplayPlaceOfWork(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                int startInx = p.IndexOf('(');
                int endInx = p.LastIndexOf(')') + 1;
                string type = p.Substring(startInx, endInx - startInx);
                string p1 = p.Substring(0, p.IndexOf('(')) + type;

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        distinctValues = distinctValues + "," + p1;
                    else
                        distinctValues = distinctValues + "," + p1;

                }
                else
                    distinctValues = p1;

                counter++;
            }
        }
        return distinctValues;
    }

    private string getDistinctPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            foreach (string p in distSet)
            {
                if (distinctValues != "")
                    distinctValues = distinctValues + "," + p;
                else
                    distinctValues = p;
            }
        }
        return distinctValues;
    }
    private string getOSPlace(string places)
    {
        if (null != places && places != "")
        {

            String[] iteams = places.Split(',');
            foreach (string i in iteams)
            {
                if (i.Contains("(OS") && !i.Contains("(OS-EX"))
                {
                    return "'" + i.Substring(0, i.IndexOf('(')) + "'";
                }
            }
        }
        return "''";
    }
    private string getDistinctPlacesQry(string places)
    {

        HashSet<string> temp = new HashSet<string>();
        String[] iteams = places.Split(',');
        ArrayList list = new ArrayList();

        foreach (string i in iteams)
        {
            if (i.Contains("(OS") && !i.Contains("(OS-EX"))
            {

            }
            else
            {
                list.Add(i);
            }

        }
        //var distSet = new HashSet<String>(iteams);
        string distinctValues = "";
        foreach (string p in list)
        {
            string p1 = p.Substring(0, p.IndexOf('('));
            if (distinctValues != "")
                distinctValues = distinctValues + ",'" + p1 + "'";
            else
                distinctValues = "'" + distinctValues + p1 + "'";
        }

        return distinctValues;
    }
    private string getSingleOsDistinctPlacesQry(string places)
    {

        HashSet<string> temp = new HashSet<string>();
        String[] iteams = places.Split(',');

        var distSet = new HashSet<String>(iteams);
        string distinctValues = "'";
        foreach (string p in distSet)
        {
            string p1 = p.Substring(0, p.IndexOf('('));
            if (distinctValues != "'")
                distinctValues = distinctValues + ",'" + p1 + "'";
            else
                distinctValues = distinctValues + p1 + "'";
        }

        return distinctValues;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //mainDiv.Visible = false;
        //System.Threading.Thread.Sleep(time);
        //_css = "removeMainDiv";
        Response.Redirect("../../MGR_Home.aspx");
        mainDiv.Style.Remove("background-color");
        //mainDiv.Attributes.Add("class", mainDiv.Attributes["class"].Replace("mainDiv", "removeMainDiv"));
    }
   
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Check all the DCR dates are approved by manager and Submit Your Expense...');</script>");
            mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";
            //_css = "mainDiv";
            Distance_calculation_001 Exp = new Distance_calculation_001();
            Distance_calculation Exp1 = new Distance_calculation();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            DataTable owt = Exp.getotherWorkType(divcode);
            // mainDiv.Visible = true; 
            // mainDiv.Attributes.Add("class", mainDiv.Attributes["class"].Replace("removeMainDiv", "mainDiv"));
            menuId.Visible = false;
            monthyearDiv.Visible = false;
            heading.Visible = true;
            mnthtxtId.InnerHtml = monthId.SelectedItem.ToString();
            yrtxtId.InnerHtml = yearID.SelectedValue.ToString();
            fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
            hqId.InnerHtml = "Employee Code :" + ds.Rows[0]["Employee_Id"].ToString();
            empId.InnerHtml = "Division Name :" + ds.Rows[0]["Division_Name"].ToString();


            double totalAllowance = 0;
            double totalDistance = 0;
            double totalFare = 0;
            double grandTotal = 0;
            otherExpAmnttot = 0;
            DataTable frmPlaceDT = Exp.getFrmandTo(divcode, sfcode);
            //DataTable dist = Exp.getDist(divcode, sfcode);

            ListItem[] frmLists = new ListItem[frmPlaceDT.Rows.Count + ds.Rows.Count];
            ListItem[] toLists = new ListItem[frmPlaceDT.Rows.Count];

            if (frmPlaceDT.Rows.Count > 0)
            {
                for (int i = 0; i < frmPlaceDT.Rows.Count; i++)
                {
                    DataRow row = frmPlaceDT.Rows[i];
                    ListItem list = new ListItem();
                    list.Text = row["Territory_Name"].ToString();
                    list.Value = row["territory_code"].ToString() + "~~" + row["Town_Cat"];
                    frmLists[i] = list;
                    toLists[i] = list;
                }
            }
            if (ds.Rows.Count > 0)
            {
                for (int i = frmPlaceDT.Rows.Count; i < (ds.Rows.Count + frmPlaceDT.Rows.Count); i++)
                {
                    int j = i - (frmPlaceDT.Rows.Count);
                    DataRow row = ds.Rows[j];
                    ListItem list = new ListItem();
                    list.Text = row["sf_hq"].ToString();
                    list.Value = row["sf_code"].ToString();
                    frmLists[i] = list;
                }
            }

            String disValue = "";
           // foreach (DataRow r in dist.Rows)
            //{
              //  disValue = disValue + r["FrmTown"].ToString().Trim() + "#" + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "::" + r["amount"] + "$";

            //}
            distString.Value = disValue;

            DataSet dsAllowance = Exp.getAllow(sfcode);
            DataTable allowanceTab = dsAllowance.Tables[0];
            DataTable otherExpTable = Exp.getOthrExpMGR(divcode);

            String ex = "0";
            String os = "0";
            String hq = "0";
            String ht = "0";
            double fare = 0;
            string allowStr = "";
            if (allowanceTab != null)
            {
                foreach (DataRow row in allowanceTab.Rows)
                {
                    ex = row["ex_allowance"].ToString();
                    os = row["os_allowance"].ToString();
                    hq = row["hq_allowance"].ToString();
                    ht = row["Hill_Allowance"].ToString();
                    fare = Convert.ToDouble(row["fare"].ToString());

                }
            }
            allowStr = ex + "@" + os + "@" + hq + "@" + ht + "@" + fare;
            allowString.Value = allowStr;
            string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
            DataTable headDetailsDt = Exp.getValidheadRecordStaus(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
            int status = 0;
            Distance_calculation dsCa = new Distance_calculation();
            DataSet dsFileName = new DataSet();
            dsFileName = dsCa.getFileNamePath(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());

            if (dsFileName.Tables[0].Rows.Count > 0)
            {
                if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
                {
                    divLinkattach.Visible = true;

                    aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                    string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                    lblViewAttach.Text = "Bills Download Here";
                }
                else
                {
                    lnkAttachment.Visible = false;
                    lbFileDel.Visible = false;
                    divAttach.Visible = false;
                    dvPage.Visible = false;
                    divAttach.Visible = false;
                }
            }
            else
            {
                dvPage.Visible = true;
                divAttach.Visible = true;
            }
            if (headDetailsDt.Rows.Count > 0)
            {
                status = Convert.ToInt32(headDetailsDt.Rows[0]["status"].ToString());
                rejectedBy.InnerHtml = "Rejected By :" + headDetailsDt.Rows[0]["rejectedBy"].ToString();
                rejectedReason.InnerHtml = "Rejected Reason :" + headDetailsDt.Rows[0]["rejectedReason"].ToString();
            }
            if (status == 10)
                rejectedDiv.Visible = true;

            if (Exp.headRecordExist(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3))
            {
                DataTable t1 = Exp.getSaveDraft(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);

                expenseDataset = Exp.getExpense_MGR(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                foreach (DataRow row in t1.Rows)
                {
                    totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
                    totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
                    totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
                    grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
                    String filter = "Work_Type_Name='" + row["Worktype_Name_M"].ToString() + "'";
                    if (!"Field Work".Equals(row["Worktype_Name_M"].ToString()))
                    {
                        /*DataRow[] dr =owt.Select(filter);
                        if (dr.Count() > 0)
                        {
                            row["Territory_Cat"] = dr[0]["Allow_type"];
                        }*/
                    }

                }
/*                for (int i = 0; i < expenseDataset.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        DataRow row = expenseDataset.Tables[0].Rows[i];
                       // t1.Rows.Add();
                        t1.Rows[t1.Rows.Count - 1]["adate"] = row["adate"].ToString();
                        t1.Rows[t1.Rows.Count - 1]["theday_name"] = row["theday_name"].ToString();
                        t1.Rows[t1.Rows.Count - 1]["worktype_name_m"] = row["worktype_name_m"].ToString();
                        t1.Rows[t1.Rows.Count - 1]["worktype_name_b"] = row["worktype_name_M"].ToString();
                       // t1.Rows[t1.Rows.Count - 1]["Expense_date"] = row["activity_date"].ToString();
                        t1.Rows[t1.Rows.Count - 1]["place_of_work"] = parePalces(row["territory_name"].ToString());
                        t1.Rows[t1.Rows.Count - 1]["adate1"] = row["adate1"].ToString();

                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine("Exception Type: {0}", ex1.GetType());
                        Console.WriteLine("  Message: {0}", ex1.Message);
                    }
                }*/
                t1.Rows.Add();
                t1.Rows[t1.Rows.Count - 1]["Allowance"] = totalAllowance;
                t1
                    .Rows[t1.Rows.Count - 1]["Distance"] = totalDistance;
                t1.Rows[t1.Rows.Count - 1]["Fare"] = totalFare;
                t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                //if (!isEmpty)
                //{
                generateOtherExpControls(Exp);
                //}
                //else
                //{
                //generateOtherExpListData(otherExpTable);
                //}

                //Distance_calculation dsCa = new Distance_calculation();
                //DataSet dsFileName = new DataSet();
                //dsFileName = dsCa.getFileNamePath(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());

                //if (dsFileName.Tables[0].Rows.Count > 0)
                //{
                //    if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
                //    {
                //        string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                //        lblAttachment.Text = str[3].Remove(0, 19);
                //        //lnkAttachment.Visible = false;
                //        //lbFileDel.Visible = false;
                //        dvPage.Visible = true;
                //        divAttach.Visible = true;
                //    }
                //    else
                //    {
                //        lnkAttachment.Visible = false;
                //        lbFileDel.Visible = false;
                //        divAttach.Visible = false;
                //    }
                //}
                //else
                //{
                //    dvPage.Visible = true;
                //    divAttach.Visible = true;
                //}


                DataTable headR = Exp.getHeadRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode,dt3);
                if (headR.Rows[0]["Status"].ToString() == "6")
                {
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    messageId.Visible = true;
                    messageId.Text = "Expense proccessed";
                    if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
                    {
                        divLinkattach.Visible = true;
                        aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                        string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                        lblViewAttach.Text = "Bills Download Here";
                        dvPage.Visible = false;
                        divAttach.Visible = false;
                    }
                    else
                    {
                        lnkAttachment.Visible = false;
                        lbFileDel.Visible = false;
                        divAttach.Visible = false;
                    }
                }
                else if (headR.Rows[0]["Status"].ToString() == "10")
                {
                    btnSave.Visible = true;
                    btnDrftSave.Visible = true;
                    messageId.Visible = false;
                    
                }
                else if (headR.Rows[0]["Status"].ToString() != "0")
                {
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    messageId.Visible = true;
                    messageId.Text = "Expense yet to proccess";

                    if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
                    {
                        divLinkattach.Visible = true;
                        aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                        string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                        lblViewAttach.Text = "Bills Download Here";
                        dvPage.Visible = false;
                        divAttach.Visible = false;
                    }
                    else
                    {
                        lnkAttachment.Visible = false;
                        lbFileDel.Visible = false;
                        divAttach.Visible = false;
                    }
                }
                else
                {
                    messageId.Visible = false;
                    btnSave.Visible = true;
                    btnDrftSave.Visible = false;
                    

                }

            }
            else
            {
                //DataSet dsExDist = null;
                DataSet dsleavetyp = null;
                expenseDataset = Exp.getExpense_MGR(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                dsleavetyp = Exp1.getleaveTyp(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
                DataTable leavtyp = dsleavetyp.Tables[0];

                expenseDataset.Tables[0].Columns.Add("Allowance");
                expenseDataset.Tables[0].Columns.Add("Distance");
                expenseDataset.Tables[0].Columns.Add("Fare");
                expenseDataset.Tables[0].Columns.Add("Total");
                expenseDataset.Tables[0].Columns.Add("Mgr_remarks");
                if (expenseDataset.Tables[0].Rows.Count <= 0)
                {
                    btnSave.Visible = false;
                }
                for (int i = 0; i < expenseDataset.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        DataRow row = expenseDataset.Tables[0].Rows[i];
                        row["territory_name"]=parePalces(row["territory_name"].ToString());
                        if (row["Activity_Date"].ToString() != "")
                        {
                            String filterp = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                            DataRow[] lp = leavtyp.Select(filterp);

                            foreach (DataRow r in lp)
                            {

                                row["Worktype_Name_M"] = r["WType_SName"].ToString();
                            }
                        }

                    }
                catch (Exception ex1)
                {
                    Console.WriteLine("Exception Type: {0}", ex1.GetType());
                    Console.WriteLine("  Message: {0}", ex1.Message);
                }

                }


                expenseDataset.Tables[0].Rows.Add();
                expenseDataset.Tables[0].Rows[expenseDataset.Tables[0].Rows.Count - 1]["Allowance"] = totalAllowance;
                expenseDataset.Tables[0].Rows[expenseDataset.Tables[0].Rows.Count - 1]["Distance"] = totalDistance;
                expenseDataset.Tables[0].Rows[expenseDataset.Tables[0].Rows.Count - 1]["Fare"] = totalFare;
                expenseDataset.Tables[0].Rows[expenseDataset.Tables[0].Rows.Count - 1]["Total"] = grandTotal;



                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = expenseDataset;
                grdExpMain.DataBind();
                generateOtherExpListData(otherExpTable);

            }

            DataTable customExpTable = new DataTable();
            customExpTable.Columns.Add("Expense_Parameter_Name");
            customExpTable.Columns.Add("amount");
            customExpTable.Columns.Add("Expense_Parameter_Code");

            DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
            double otherExAmnt = 0;
            if (expParamsAmnt.Rows.Count > 0)
            {
                for (int i = 0; i < expParamsAmnt.Rows.Count; i++)
                {
                    string colName = "Fixed_Column" + (i + 1);
                    if (expParamsAmnt.Rows[i][colName].ToString() != "")
                    {
                        otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][colName].ToString());
                    }
                    customExpTable.Rows.Add();

                    customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"];
                    customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                    customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName].ToString() == "" ? "0" : expParamsAmnt.Rows[i][colName];

                }
                otherExpGrid.DataSource = customExpTable;
                otherExpGrid.DataBind();
            }

            // fixedExpense.InnerHtml = "180";
            Othtotal.Value = otherExpAmnttot + "";
            double tot = otherExAmnt + grandTotal + otherExpAmnttot;
            grandTotalName.InnerHtml = tot.ToString();
            //}
            // else
            // {
            //    grdSalesForce.DataSource = dsExpense;
            //     grdSalesForce.DataBind();
            // }

            foreach (GridViewRow gridRow in grdExpMain.Rows)
            {
                Label date = (Label)gridRow.FindControl("lbl_ADate");
                TextBox lblDistance = (TextBox)gridRow.FindControl("lblDistance");
               
                Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                HiddenField allowTypeVal = (HiddenField)gridRow.FindControl("allowTypeHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden1");
                // String filter = "Allow_type='" + lblTerrName.Text + "'";
                // string type = "";

                /*   if (!"Field Work".Equals(lblWorkType.Text))
                   {
                       DataRow[] dr = owt.Select(filter);
                       if (dr.Count() > 0)
                       {
                           type = dr[0]["Allow_type"].ToString();
                       }
                   }*/
                Label frmLbl = ((Label)gridRow.FindControl("lblFrom"));
                Label toLbl = ((Label)gridRow.FindControl("lblTo"));
                HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
                HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
                //Label toHidden = (Label)gridRow.FindControl("toHidden");
                DropDownList frmBox = ((DropDownList)gridRow.FindControl("fromPlace"));
                DropDownList toBox = ((DropDownList)gridRow.FindControl("toPlace"));
                DropDownList allowType = ((DropDownList)gridRow.FindControl("AllowType"));
                DropDownList allowTypeother = ((DropDownList)gridRow.FindControl("allowTypeother"));
                TextBox txtRemarks = (TextBox)gridRow.FindControl("txtRemarks");
                if (divcode.ToString() == "2" || divcode.ToString() == "6")
                {
                    allowType.Visible = true;
                }
                else
                {
                    allowTypeother.Visible = true;
                }

                Label allTypLbl = ((Label)gridRow.FindControl("lblCat"));
                TextBox txtAllow = ((TextBox)gridRow.FindControl("txtAllow"));
                Label allowLbl = ((Label)gridRow.FindControl("lblAllw"));
                TextBox txtFare = ((TextBox)gridRow.FindControl("txtFare"));
                Label fareLbl = ((Label)gridRow.FindControl("lblFare"));

                Label sNoLbl = ((Label)gridRow.FindControl("lblSNo"));

                allowType.SelectedValue = allowTypeVal.Value;
                lblDistance.Attributes.Add("readonly", "readonly");
                txtFare.Attributes.Add("readonly", "readonly");
                if (date.Text.Equals("") || lblWorkType.Text == "Weekly Off" || lblWorkType.Text == "Leave(CL)" || lblWorkType.Text == "Leave(PL)" || lblWorkType.Text == "Leave(SL)" || lblWorkType.Text == "Leave(TL)" || lblWorkType.Text == "Leave(ML)" || lblWorkType.Text == "Holiday")
                {
                    txtFare.Visible = false;
                    lblDistance.Visible = false;
                    allowType.Visible = false;
                    allowTypeother.Visible = false;
                    txtAllow.Visible = false;
                    lblDistance.Visible = false;
                    txtRemarks.Visible = false;
                    sNoLbl.Text = "";
                }
                if (date.Text.Equals("") || lblWorkType.Text == "Weekly Off" || lblWorkType.Text == "Leave(CL)" || lblWorkType.Text == "Leave(PL)" || lblWorkType.Text == "Leave(SL)" || lblWorkType.Text == "Leave(TL)" || lblWorkType.Text == "Leave(ML)" || lblWorkType.Text == "Holiday")
                {
                    lblDistance.Visible = false;
                    allowType.Visible = false;
                    allowTypeother.Visible = false;
                    TextBox allowtxt = (TextBox)gridRow.FindControl("txtFare");
                    allowtxt.Visible = false;
                    Label allow = (Label)gridRow.FindControl("lblFare");
                    allow.Visible = true;

                    TextBox txt1 = (TextBox)gridRow.FindControl("txtRemarks");
                    txt1.Visible = false;
                    txtAllow.Visible = false;
                    lblDistance.Visible = false;
                    txtRemarks.Visible = false;
                    
                }


            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
        }
    }

    protected void lbFileDel_Onclick(object sender, EventArgs e)
    {
        lblAttachment.Text = "";
        lbFileDel.Visible = false;
        //pnlViewInbox.Style.Add("visibility", "hidden");
        //pnlViewMailInbox.Style.Add("visibility", "hidden");

    }
    private string parePalces(string places)
    {
        if (places == null || places == "")
            return "";
        return places.Replace(",","<br/>");

    }
    private void generateOtherExpListData(DataTable otherExpTable)
    {
        if (otherExpTable.Rows.Count > 0)
        {
            foreach (DataRow row in otherExpTable.Rows)
            {
                ListItem list = new ListItem();
                list.Text = row["Expense_Parameter_Name"].ToString();
                list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount1"].ToString();
                otherExp.Items.Add(list);
            }
            //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
            //otherExp.DataTextField = "Expense_Parameter_Name";
        }
    }

    private static void changeFromToCtrl(ListItem[] frmLists, ListItem[] toLists, Label frmLbl, Label toLbl, DropDownList frmBox, DropDownList toBox, string from, string to)
    {
        ListItem[] newfrmLists = new ListItem[frmLists.Count()];
        ListItem[] newtoLists = new ListItem[toLists.Count()];
        for (int i = 0; i < frmLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = frmLists[i].Text;
            l.Value = frmLists[i].Value;
            newfrmLists[i] = l;
        }
        for (int i = 0; i < toLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = toLists[i].Text;
            l.Value = toLists[i].Value;
            newtoLists[i] = l;
        }
        frmBox.Visible = true;
        frmLbl.Visible = false;
        frmBox.Items.AddRange(newfrmLists);
        if (from != null && from != "")
        {
            frmBox.ClearSelection();
            frmBox.Items.FindByText(from).Selected = true;
        }
        toBox.Visible = true;
        toLbl.Visible = false;
        toBox.Items.AddRange(newtoLists);
        if (to != null && to != "")
        {
            toBox.ClearSelection();
            toBox.Items.FindByText(to).Selected = true;
        }
    }

    private int getOsExDistance(Distance_calculation_001 Exp, string places, out double osAmount, int osDistance, double amount, string osFrmCode)
    {
        string osPlace = getOSPlace(places);
        osAmount = amount;
        foreach (string p in pList)
        {
            string qryPlaces = getDistinctPlacesQry(p);

            if (qryPlaces == "")
            {
                qryPlaces = "''";
            }
            DataSet dsOsExDis = Exp.getOsExDistance(sfcode, qryPlaces, osFrmCode);
            if (dsOsExDis.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsOsExDis.Tables[0].Rows)
                {
                    osDistance = osDistance + Convert.ToInt32(row["distance_in_kms"].ToString());
                    osAmount = osAmount + Convert.ToDouble(row["amount"].ToString());
                }
            }
        }
        return osDistance;
    }

    private void getOsDistAndFrmCode(Distance_calculation_001 Exp, string places, out double osAmount, out int osDistance, out string osFrmCode)
    {

        string osPlace = getOSPlace(places);

        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsDistance(sfcode, osPlace, qryPlaces);
        osDistance = 0;
        osAmount = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
            osAmount = Convert.ToDouble(dsDis.Tables[0].Rows[0]["amount"].ToString());
        }

    }
    private void getSingleOsDistAndFrmCode(Distance_calculation_001 Exp, string places, out double osAmount, out int osDistance, out string osFrmCode)
    {
        DataSet dsDis = null;
        string qryPlaces = getSingleOsDistinctPlacesQry(places);
        dsDis = Exp.getSingleOsDistance(sfcode, qryPlaces);
        osDistance = 0;
        osAmount = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
            osAmount = Convert.ToDouble(dsDis.Tables[0].Rows[0]["amount"].ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        saveData(true);
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {

        saveData(false);
    }
    private void saveData(bool flag)
    {
        Expense ex = new Expense();
        isSavedPage = true;
        string Attachpath = string.Empty;

        string strFileName = "";
        lblAttachment.Text = FileUpload2.FileName;
        if (lblAttachment.Text != "")
        {
            strFileDateTime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            Session["strFileDateTime"] = strFileDateTime;
            FileUpload2.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Expense/Attachment/" + strFileDateTime + FileUpload2.FileName));
        }
        lblAttachment.Focus();

        if (lblAttachment.Text == "")
        {
            lbFileDel.Visible = false;
        }
        else
        {
            lbFileDel.Visible = true;
        }

        if (Session["strFileDateTime"] != null)
        {
            strFileName = Session["strFileDateTime"].ToString();
        }

        if (Session["strFileDateTime"] != null)
        {
            strFileName = Session["strFileDateTime"].ToString();
        }

        string filename = lblAttachment.Text;
        //FileUpload2.FileName=FileUpload1.FileName;
        if (filename != "")
        {
            //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + filename));            
            Attachpath = "MasterFiles/Expense/Attachment/" + strFileName + filename;   // Modified by Sridevi - To add ToString()         
        }
        Dictionary<String, String> values = new Dictionary<String, String>();
        values["sf_code"] = sfcode;
        values["div_code"] = divcode;
        values["month"] = monthId.SelectedValue.ToString();
        values["year"] = yearID.SelectedValue.ToString();
        values["period"] = "";
        values["File_Path"] = Attachpath;
        values["frmTovalues"] = "";
        if (flag)
        {
            values["flag"] = "1";
        }
        else
        {
            values["flag"] = "0";
        }
        Distance_calculation dist1 = new Distance_calculation();
        DataTable table = dist1.getMgrAppr(divcode);
        if (table.Rows.Count > 0)
        {
            if ("1".Equals(table.Rows[0]["MgrAppr_Remark"].ToString()))
            {
                values["flag"] = "7";

            }
        }
        values["s_date"] = "2016-01-03 20:43:47.643";
        
        Distance_calculation_001 dist = new Distance_calculation_001();
        DataTable ds1 = dist.getFieldForce(divcode, sfcode);
        string soj = ds1.Rows[0]["sf_joining_date"].ToString();
        values["soj"] = soj;
        sNo = dist.getValidheadRecordNo(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode,soj);
        values["sNo"] = sNo.ToString();
       
        DataTable ds = dist.getFieldForce(divcode, sfcode);
        if (ds.Rows.Count > 0)
        {
            values["EmpNo"] = ds.Rows[0]["SF_Emp_Id"].ToString();
            values["SfEmpNo"] = ds.Rows[0]["Employee_Id"].ToString();
        }
        else
        {
            values["EmpNo"] = "null";
            values["SfEmpNo"] = "null";
        }
        if (ds.Rows[0]["reporting_to_sf"].ToString() == "admin")
        {
            values["flag"] = "1";
        }

        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction();

            command.Connection = connection;

            command.Transaction = transaction;



            try
            {



                Int32 count1 = 0;
                command.CommandText = "SELECT COUNT(sl_No) FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'";
                count1 = (Int32)command.ExecuteScalar();
                if (count1 > 0)
                {
                    command.CommandText = "delete from Exp_others " +
                     "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE Resigned_flag is null and SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "') ";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Exp_fixed " +
                         "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE Resigned_flag is null and SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "') ";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from exp_accinf where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "')";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Trans_Expense_detail " +
                     "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "') ";
                    command.ExecuteNonQuery();
                    command.CommandText = "delete from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "  and sf_joining_date='" + values["soj"] + "'";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Trans_Expense_Head " +
                    "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,Status_flg,Employee_Id,sf_emp_id,frmTovalues,sf_joining_date,file_path)" +
                    "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                    values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",0,'" + values["EmpNo"] + "','" + values["SfEmpNo"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["File_Path"] + "')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "INSERT INTO Trans_Expense_Head " +
                    "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,Status_flg,Employee_Id,sf_emp_id,frmTovalues,sf_joining_date,file_path)" +
                    "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                    values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",0,'" + values["EmpNo"] + "','" + values["SfEmpNo"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["File_Path"] + "')";
                    command.ExecuteNonQuery();
                }


            
        Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();
        
        foreach (GridViewRow gridRow in grdExpMain.Rows)
        {

            values = new Dictionary<String, String>();
            values["sf_code"] = sfcode;
            values["div_code"] = divcode;
            values["month"] = monthId.SelectedValue.ToString();
            values["year"] = yearID.SelectedValue.ToString();
            Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
            string date = lbl_aDate.Text;
            Label lblDayName = (Label)gridRow.FindControl("lblDayName");
            Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
            Label lblTerritoryName = (Label)gridRow.FindControl("lblTerritoryName");
            Label lblCat = (Label)gridRow.FindControl("lblCat");

            HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
            HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
            HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
            Label lblAllw = (Label)gridRow.FindControl("lblAllw");
            HiddenField allowHidden = (HiddenField)gridRow.FindControl("allowHidden");
            Label lblFrom = (Label)gridRow.FindControl("lblFrom");
            HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
            Label lblTo = (Label)gridRow.FindControl("lblTo");
            HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
            TextBox lblDistance = (TextBox)gridRow.FindControl("lblDistance");
            TextBox txtFare = (TextBox)gridRow.FindControl("txtFare");
            
            TextBox txtRemarks = (TextBox)gridRow.FindControl("txtRemarks");
            Label lblFare = (Label)gridRow.FindControl("lblFare");
            HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
            Label lblTotal = (Label)gridRow.FindControl("lblTotal");
            HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
            HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
            //HiddenField remarksHidden = (HiddenField)gridRow.FindControl("remarksHidden");
            values["dayName"] = lblDayName.Text;
            values["adate"] = date;
            values["soj"] = soj;
            values["adate1"] = adateHidden.Value;
            values["dayName"] = lblDayName.Text;
            values["workType"] = lblWorkType.Text;
            values["terrName"] = lblTerritoryName.Text;
            values["cat"] = allowTypeHidden.Value;
            values["allowance"] = allowHidden.Value;
            values["distance"] = lblDistance.Text;
            values["remarks"] = txtRemarks.Text;
            values["fare"] = txtFare.Text;
            values["total"] = totHidden.Value;
            values["from"] = "";
            values["to"] = "";
            values["catTemp"] = allowTypeHidden1.Value;
            if (date != "")
            {
                        command.CommandText = "INSERT INTO Trans_Expense_Detail " +
                                           "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow,Mgr_amount,Mgr_Remarks,Mgr_total,exp_remarks)" +
                                           "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                           values["adate"].Replace("'", "\"") + "','" + values["dayName"].Replace("'", "\"") + "','" + values["workType"] + "','" + values["adate1"] + "','" + values["terrName"] + "','" + values["cat"]
                                           + "','" + values["allowance"] + "','" + values["distance"] + "','" + values["fare"] + "','" + values["catTemp"] + "','" + values["total"] + "','" + values["div_code"] + "','" + values["from"] + "','" + values["to"] + "','" + values["allowance"] + "','" + values["fare"] + "','" + values["remarks"] + "','" + values["total"] + "','" + values["remarks"] + "')";
                        command.ExecuteNonQuery();
                    }
        }
       
        foreach (GridViewRow gridRow in otherExpGrid.Rows)
        {

            HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
            Label amnt = (Label)gridRow.FindControl("lblSexpAmnt");
            values["Amt"] = amnt.Text;
            values["Expense_Parameter_Code"] = Expense_Parameter_Code.Value;

                    command.CommandText = "INSERT INTO Exp_Fixed " +
                            "(sl_No,Expense_Parameter_Code,Amt,SF_Code)" +
                            "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                            values["Expense_Parameter_Code"] + "','" + values["Amt"] + "','" + values["sf_code"] + "')";
                    command.ExecuteNonQuery();

                }
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        
        string[] splitVal = otherExpValues.Value.Split('~');

        string[] rms = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] exp = splitVal[2].Split(',');
        for (int p = 0; p < exp.Length; p++)
        {
            isEmpty = false;
            string[] e = exp[p].Split('=');
                    if (amount[p] == "")
                    {
                        amount[p] = "0";
                    }
                    if (!e[1].Contains("Select"))
                    {
                        command.CommandText = "INSERT INTO Exp_others " +
                                       "(sl_No,expval,Paritulars,Amt,Remarks)" +
                                       "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                       e[0] + "','" + e[1] + "'," + amount[p] + ",'" + rms[p] + "')";
                        command.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                connection.Close();
                if ("1".Equals(home))
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../Default_MGR.aspx'</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Mgr_old.aspx'</script>");
                }


            }
            catch (Exception ex1)
            {

                Console.WriteLine("Commit Exception Type: {0}", ex1.GetType());

                Console.WriteLine("Message: {0}", ex1.Message);


                try
                {

                    transaction.Rollback();

                }

                catch (Exception ex2)
                {


                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                    Console.WriteLine("  Message: {0}", ex2.Message);

                }

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Expired.Kindly Resubmit Again');window.location='RptAutoExpense_Mgr_old.aspx'</script>");



            }
        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (isSavedPage)
        {
            Distance_calculation_001 dist = new Distance_calculation_001();
            if (!isEmpty)
                generateOtherExpControls(dist);
        }
    }

    private void generateOtherExpControls(Distance_calculation_001 dist)
    {
        DataTable ds = dist.getFieldForce(divcode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        DataTable t2 = dist.getSavedOtheExpRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode,dt3);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable otherExp1 = dist.getOthrExpMGR(divcode);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            if (t2.Rows.Count > 0)
                htmlTable.Rows.RemoveAt(p);
            else
            {
                generateOtherExpListData(otherExp1);
            }

        }
        for (int i = 0; i < t2.Rows.Count; i++)
        {

            HtmlTableRow r = new HtmlTableRow();
            DropDownList d = new DropDownList();
            d.ID = "otherExp_" + i;
            d.CssClass = "otherExp";


            if (otherExp1.Rows.Count > 0)
            {
                foreach (DataRow row in otherExp1.Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Expense_Parameter_Name"].ToString();
                    list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount1"].ToString();
                    d.Items.Add(list);
                }
                d.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            string amnt = t2.Rows[i]["amt"].ToString();
            otherExpAmnttot = otherExpAmnttot + Convert.ToInt32(amnt);
            string rm = t2.Rows[i]["remarks"].ToString();
            d.Text = t2.Rows[i]["Paritulars"].ToString();
            d.Items.FindByText(t2.Rows[i]["Paritulars"].ToString()).Selected = true;
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Controls.Add(d);
            r.Cells.Add(cell1);

            HtmlTableCell cell2 = new HtmlTableCell();
            TextBox box = new TextBox();
            Literal lit = new Literal();
            lit.Text = @"<input type='text' class='textbox' name='OthExpVal' id='OthExpVal_" + i + "' runat='server' value='" + amnt + "' size='6' maxlength=6 onkeyup='OthExpCalc(this)'/>";
            HtmlTable table = new HtmlTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            System.IO.StringWriter tw = new System.IO.StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            box.RenderControl(hw);


            cell2.Controls.Add(lit);
            r.Cells.Add(cell2);
            HtmlTableCell cell3 = new HtmlTableCell();
            TextBox box1 = new TextBox();
            lit = new Literal();
            lit.Text = @"<input type='text' class='textbox' name='OthExpRmk' id='OthExpRmk_" + i + "' runat='server' value='" + rm + "' size='50' onkeyup='OthExpCalc(this)'/>";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            box1.RenderControl(hw);

            cell3.Controls.Add(lit);
            r.Cells.Add(cell3);
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
            lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForOthExp(this,this.parentNode.parentNode,1)' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b2.RenderControl(hw);

            cell5.Controls.Add(lit);
            r.Cells.Add(cell5);

            htmlTable.Rows.Add(r);
        }
    }


}