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
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{

    bool isSavedPage = false;
    bool isEmpty = true;
    string strFileDateTime = string.Empty;
    string sfcode = string.Empty;
    string divcode = string.Empty;
    string osSingleContn = "OS";
    DataSet expenseDataset = null;
    DataSet placeDataset = null;
    DataSet AliasplaceDataset = null;
    DataTable HillStationPlaces = null;
    double otherExpAmnttot = 0;
    int typ = 0;

    string frmToPls = string.Empty;
    ArrayList pList = new ArrayList();
    ArrayList pList1 = new ArrayList();
    string home = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Convert.ToString(Session["Sf_Code"]);
        divcode = Convert.ToString(Session["div_code"]);
        sfCode.Value = sfcode;
        divCode.Value = divcode;

        if (Request.QueryString["home"] != null)
        {
            home = Request.QueryString["home"].ToString();
        }
        if (!IsPostBack)
        {
            GetMyMonthList();
            bind_year_ddl();

        }
        if (Request.QueryString["type"] != null)
        {
            //mainDiv.Style.Remove("background-color");
            //heading.Visible = true;
            string mon = Request.QueryString["mon"].ToString();
            string yr = Request.QueryString["year"].ToString();
            sfcode = Request.QueryString["sf_code"].ToString();
            monthId.SelectedValue = mon;
            yearID.SelectedValue = yr;


            //menu1.Visible = false;
            btnBack.Visible = false;
            //menuId.Visible = false;
            //heading.Visible = true;
            //twdid.Visible = true;
            //misExp.Visible = true;
            //grdExpMain.Visible = true;

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
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2017; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
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
                string p1 = p.Substring(0, p.LastIndexOf('('));

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
                int startInx = p.LastIndexOf('(');
                int endInx = p.LastIndexOf(')') + 1;
                string type = p.Substring(startInx, endInx - startInx);
                string p1 = p.Substring(0, p.LastIndexOf('(')) + "<span style=\"background-color:yellow\">" + type + "</span>";

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        //distinctValues = distinctValues + p1;
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
                    return "'" + i.Substring(0, i.LastIndexOf('(')) + "'";
                }
            }
        }
        return "''";
    }
    private string getOSEXPlace(string places)
    {
        if (null != places && places != "")
        {

            String[] iteams = places.Split(',');
            foreach (string i in iteams)
            {
                if (!i.Contains("(OS)") && i.Contains("(OS-EX"))
                {
                    return "'" + i.Substring(0, i.LastIndexOf('(')) + "'";
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
            string p1 = p.Substring(0, p.LastIndexOf('('));
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
            string p1 = p.Substring(0, p.LastIndexOf('('));
            if (distinctValues != "'")
                distinctValues = distinctValues + ",'" + p1 + "'";
            else
                distinctValues = distinctValues + p1 + "'";
        }

        return distinctValues.Replace("<span style=\"background-color:yellow\">", "");
    }
    private string getDisplayRemoveHQPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                if (p.Contains("(EX"))
                {

                    string p1 = p.Substring(0, p.LastIndexOf('('));

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
        }
        return distinctValues;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //mainDiv.Visible = false;
        //System.Threading.Thread.Sleep(time);
        //_css = "removeMainDiv";
        Response.Redirect("RptAutoExpense_RowWise_Textbox.aspx");
        mainDiv.Style.Remove("background-color");
        //mainDiv.Attributes.Add("class", mainDiv.Attributes["class"].Replace("mainDiv", "removeMainDiv"));
    }
    double fare = 0;
    double rangeFare2 = 0;
    int range2 = 0;
    String rangeType2 = "Consolidated";
    double rangeFare = 0;
    int range = 0;
    String rangeType = "Consolidated";
    double rangeFare3 = 0;
    int range3 = 0;
    String rangeType3 = "Consolidated";
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";
            Distance_calculation Exp = new Distance_calculation();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            DataTable owt = Exp.getotherWorkType(divcode);
            DataTable ofwt = Exp.getotherFieldWorkType(divcode);
            DataTable ofwtexos = Exp.getotherFieldWorkTypeEXOS(divcode);
            menuId.Visible = false;
            heading.Visible = true;
            twdid.Visible = true;
            mnthtxtId.InnerHtml = monthId.SelectedItem.ToString();
            yrtxtId.InnerHtml = yearID.SelectedValue.ToString();
            fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf"].ToString();
            doj.InnerHtml = "DOJ :" + ds.Rows[0]["doj"].ToString();
            empId.InnerHtml = "Employee Code :" + ds.Rows[0]["Employee_Id"].ToString();
            double totalAllowance = 0;
            double totalDistance = 0;
            double totalFare = 0;
            double grandTotal = 0;
            double totaddexp = 0;
            otherExpAmnttot = 0;
            DataTable TD = Exp.TWD(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            twd.InnerHtml = TD.Rows[0]["cnt"].ToString();
            DataTable FW = Exp.FW(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            fw.InnerHtml = FW.Rows[0]["cnt"].ToString();
            DataTable HQ = Exp.THQ(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            hhq.InnerHtml = HQ.Rows[0]["cnt"].ToString();
            DataTable EX = Exp.TEX(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            eex.InnerHtml = EX.Rows[0]["cnt"].ToString();
            DataTable OS = Exp.TOS(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            oos.InnerHtml = OS.Rows[0]["cnt"].ToString();
            DataTable drsmet = Exp.Drsmet(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            met.InnerHtml = drsmet.Rows[0]["metcnt"].ToString();
            made.InnerHtml = drsmet.Rows[0]["madecnt"].ToString();
            cavg.InnerHtml = drsmet.Rows[0]["calavg"].ToString();
            DataTable frmPlaceDT = Exp.getFrmandTo(divcode, sfcode);
            DataTable dist = Exp.getDist(divcode, sfcode);


            ListItem[] toLists = new ListItem[frmPlaceDT.Rows.Count];

            if (frmPlaceDT.Rows.Count > 0)
            {
                for (int i = 0; i < frmPlaceDT.Rows.Count; i++)
                {
                    DataRow row = frmPlaceDT.Rows[i];
                    ListItem list = new ListItem();
                    list.Text = row["Alias_Name"].ToString();
                    list.Value = row["territory_code"].ToString() + "~~" + row["Town_Cat"] + "~~" + row["Exp_Allow_Cat"];
                    toLists[i] = list;
                }
            }




            DataSet dsAllowance = Exp.getAllow(sfcode);
            DataTable allowanceTab = dsAllowance.Tables[0];
            DataTable otherExpTable = Exp.getOthrExp(divcode);

            String ex = "0";
            String os = "0";
            String ossm = "0";
            String osnm = "0";
            String hq = "0";
            String ex11 = "0";
            String os1 = "0";
            String hq1 = "0";
            string hill = "0";
            string wtyp = "";
            DataTable rGdT = new DataTable();
            SalesForce sf = new SalesForce();
            rGdT = sf.GetRange(divcode);
            if (rGdT.Rows.Count > 0)
            {
                range = Convert.ToInt32(rGdT.Rows[0]["Range1_KMS"].ToString());
                range2 = Convert.ToInt32(rGdT.Rows[0]["Range2_KMS"].ToString());
                range3 = Convert.ToInt32(rGdT.Rows[0]["Range3_KMS"].ToString());
                rangeType = rGdT.Rows[0]["Range1_status"].ToString();
                rangeType2 = rGdT.Rows[0]["Range2_status"].ToString();
                rangeType3 = rGdT.Rows[0]["Range3_status"].ToString();

            }

            string allowStr = "";
            string allowStr2 = "";
            if (allowanceTab != null)
            {
                foreach (DataRow row in allowanceTab.Rows)
                {
                    ex = row["ex_allowance"].ToString();
                    os = row["os_allowance"].ToString();
                    ossm = row["OS_SM"].ToString();
                    osnm = row["OS_NM"].ToString();
                    hq = row["hq_allowance"].ToString();
                    hill = row["hill_allowance"].ToString();
                    fare = Convert.ToDouble(row["fare"].ToString());
                    rangeFare = Convert.ToDouble(row["Range_of_Fare1"].ToString());
                    rangeFare2 = Convert.ToDouble(row["Range_of_Fare2"].ToString());
                    rangeFare3 = Convert.ToDouble(row["Range_of_Fare3"].ToString());
                    allowStr = ex + "@" + os + "@" + hq + "@" + fare + "@" + ossm + "@" + osnm;
                   // allowStr2 = ex + "@" + os + "@" + hq + "@" + fare + "@" + ossm + "@" + osnm;
                }
                allowString.Value = allowStr;
               // allowString2.Value = allowStr2;
            }

            String disValue = "";
            foreach (DataRow r in dist.Rows)
            {
                double fare11 = rangeDistanceCalcOtherType(Convert.ToInt32(r["Distance"]), fare, Convert.ToInt32(r["Distance"]));
                disValue = disValue + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "@" + fare11 + "$";
                
            }
            distString.Value = disValue;
            DataSet dsAllowance1 = null;

            if (ds.Rows[0]["sf_desgn"].ToString() == "N")
            {
                dsAllowance1 = Exp.NonMetro(divcode, ds.Rows[0]["Fieldforce_Type"].ToString());
            }
            else
            {
                dsAllowance1 = Exp.getAllowWorkTypeMetro(divcode, ds.Rows[0]["Fieldforce_Type"].ToString());

            }
            DataTable allowanceTab1 = dsAllowance1.Tables[0];
            string allowStr1 = "";
            if (allowanceTab1 != null)
            {
                foreach (DataRow row in allowanceTab1.Rows)
                {

                    ex11 = row["Fixed_amount1"].ToString();
                    os1 = row["Fixed_amount2"].ToString();
                    hq1 = row["Fixed_amount"].ToString();
                    wtyp = row["Work_Type_Name"].ToString();

                    allowStr1 = allowStr1 + (wtyp + "=" + ex11 + "@" + os1 + "@" + hq1 + "$");
                }

                allowString1.Value = allowStr1;

            }
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
            string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
            if (Exp.headRecordExist(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3))
            {
                DataTable t1 = Exp.getSavedRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3);

                foreach (DataRow row in t1.Rows)
                {
                    totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
                    totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
                    totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
                    totaddexp = totaddexp + Convert.ToDouble(row["rw_amount"].ToString());
                    grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
                    String filter = "Work_Type_Name='" + row["Worktype_Name_B"].ToString() + "'";
                    if (frmTovalues.Value == "")
                        frmTovalues.Value = row["frmTovalues"].ToString();

                    else
                        frmTovalues.Value = frmTovalues.Value + "#" + row["frmTovalues"].ToString();

                }
                t1.Rows.Add();
                t1.Rows[t1.Rows.Count - 1]["Allowance"] = totalAllowance;
                t1.Rows[t1.Rows.Count - 1]["Distance"] = totalDistance;
                t1.Rows[t1.Rows.Count - 1]["Fare"] = totalFare;
                t1.Rows[t1.Rows.Count - 1]["rw_amount"] = totaddexp;
                t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                if (!isEmpty)
                {
                    generateOtherExpControls(Exp);
                }
                else
                {
                    generateOtherExpListData(otherExpTable);
                }

                DataTable headR = Exp.getHeadRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3);
                if (headR.Rows[0]["Status"].ToString() == "1" || headR.Rows[0]["Status"].ToString() == "7" || headR.Rows[0]["Status"].ToString() == "8")
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
                else if (headR.Rows[0]["Status"].ToString() == "2")
                {
                    messageId.Visible = true;
                    messageId.Text = "Expense proccessed";
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;

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
                    divAttach.Visible = true;
                }



            }
            else
            {


                DataSet dsExDist = null;
                DataSet dsleavetyp = null;
                DataSet dsExPlaces = null;
                expenseDataset = Exp.getExpense(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                placeDataset = Exp.getPlace(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                dsExDist = Exp.getExDistance(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                dsExPlaces = Exp.exmaxplaces(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                DataTable exDistTab = dsExDist.Tables[0];
                DataTable dsExPlacesTab = dsExPlaces.Tables[0];
                dsleavetyp = Exp.getleaveTyp(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
                DataTable leavtyp = dsleavetyp.Tables[0];
                Dictionary<String, String> allowValues = new Dictionary<String, String>();
                //bool FA = false;
                //bool VA = false;

                DataTable tt = Exp.getMgrAppr(divcode);
                string osContn = "OS";
                if (tt.Rows.Count > 0)
                {
                    if ("EX".Equals(tt.Rows[0]["Last_Os_Wrkconsider"].ToString()))
                    {
                        osContn = "EX";
                    }
                    else
                    {
                        osContn = "OS";
                    }
                }
                if (tt.Rows.Count > 0)
                {
                    if ("Y".Equals(tt.Rows[0]["Row_wise_textbox"].ToString()))
                    {
                        grdExpMain.Columns[10].Visible = true;
                        grdExpMain.Columns[11].Visible = true;

                    }
                }
                
                if (tt.Rows.Count > 0)
                {
                    if ("EX".Equals(tt.Rows[0]["Single_OS_Consider_as"].ToString()))
                    {
                        osSingleContn = "EX";
                    }
                    else
                    {
                        osSingleContn = "OS";
                    }
                }

                DataTable t1 = expenseDataset.Tables[0];
                DataTable t2 = placeDataset.Tables[0];
                if(t1.Rows.Count<=0)
                {
                    btnSave.Visible = false;
                }
                t1.Columns.Add("curterrtyp");
                foreach (DataRow row in t1.Rows)
                {




                    String filter = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                    DataRow[] rows = t2.Select(filter);
                    foreach (DataRow r in rows)
                    {

                        if (row["Territory_Name"] != null && row["Territory_Name"].ToString() != "")
                        {
                            row["Territory_Name"] = row["Territory_Name"].ToString() + "," + r["Territory_Name"] + "(" + r["Territory_Cat"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }
                        else
                        {
                            row["Territory_Name"] = r["Territory_Name"] + "(" + r["Territory_Cat"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }
                        if (row["Territory_Name1"] != null && row["Territory_Name1"].ToString() != "")
                        {
                            row["Territory_Name1"] = row["Territory_Name"].ToString() + "," + r["Territory_Name"] + "(" + r["Territory_Cat1"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }
                        else
                        {
                            row["Territory_Name1"] = r["Territory_Name"] + "(" + r["Territory_Cat1"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }

                        String old = "";
                        String curr = "";
                        string curr1 = r["Hill_station"].ToString();

                        if ((curr1 != "" || curr1 != null) && curr1 == "Y")
                        {
                            row["Hill_Station"] = "Y";
                        }
                        if (row["Territory_Cat"] != null)
                        {
                            old = row["Territory_Cat"].ToString();
                        }
                        if (r["Territory_Cat"] != null)
                        {
                            curr = r["Territory_Cat"].ToString();
                        }
                        if (old == "")
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else if (old.Equals("OS-EX"))
                        {
                        }
                        else if (curr.Equals("OS-EX"))
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else if (curr.Equals("OS") && !old.Equals("OS-EX"))
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else if (old.Equals("OS"))
                        {
                        }
                        else if (old.Equals("EX"))
                        {
                        }
                        else if (old.Equals("HQ") && curr.Equals("EX"))
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else
                        {
                            row["Territory_Cat"] = curr;
                        }




                        String oldalw = "";
                        String curralw = "";
                        if (row["Exp_Allow_Cat"] != null)
                        {
                            oldalw = row["Exp_Allow_Cat"].ToString();
                        }
                        if (r["Exp_Allow_Cat"] != null)
                        {
                            curralw = r["Exp_Allow_Cat"].ToString();
                        }
                        if (oldalw == "")
                        {
                            row["Exp_Allow_Cat"] = curralw;
                        }
                        else if (oldalw.Equals("MM"))
                        {
                            //nothing;
                        }
                        else if (curralw.Equals("MM"))
                        {
                            row["Exp_Allow_Cat"] = curralw;
                        }
                        else if (curralw.Equals("SM") && !oldalw.Equals("MM"))
                        {
                            row["Exp_Allow_Cat"] = curralw;
                        }
                        else if (curralw.Equals("") && !oldalw.Equals(""))
                        {
                            //nothing;
                        }
                        else
                        {
                            row["Exp_Allow_Cat"] = curralw;
                        }
                    }
                    if (!(row["Worktype_Name_B"].Equals("Field Work")))
                    {
                        filter = "Work_Type_Name='" + row["Worktype_Name_B"].ToString() + "'";
                        DataRow[] ss = owt.Select(filter);
                        if (ss.Count() > 0)
                        {
                            row["Territory_Cat"] = ss[0]["Allow_type"];
                        }
                    }
                    row["curterrtyp"] = row["Territory_Cat"];
                }
                t1.Columns.Add("PrevType");
                t1.Columns.Add("NextType");
                t1.Columns.Add("Allowance");
                t1.Columns.Add("Distance");
                t1.Columns.Add("From_place");
                t1.Columns.Add("To_place");
                t1.Columns.Add("Fare");
                t1.Columns.Add("Total");
                t1.Columns.Add("catTemp");
                t1.Columns.Add("temtyp");
                t1.Columns.Add("rw_amount");
                t1.Columns.Add("rw_rmks");
                t1.Columns.Add("PrevPlace");
                t1.Columns.Add("NextPlace");
               // t1.Columns.Add("Exp_Allow_Cat");
                t1.Columns.Add("PrevWtype");
                t1.Columns.Add("NextWtype");

                t1.Rows.Add();
                string toPlace = "";
               
                double tDistance = 0;
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    try
                    {


                        int prevIndex = (i == 0 ? i : i - 1);
                        int nextIndex = (i == t1.Rows.Count - 1 ? i : i + 1);
                        DataRow currRow = t1.Rows[i];
                        DataRow prevRow = t1.Rows[prevIndex];
                        DataRow nextRow = t1.Rows[nextIndex];
                        currRow["PrevType"] = prevRow["Territory_Cat"];
                        currRow["NextType"] = nextRow["Territory_Cat"];
                        String prev = currRow["PrevType"].ToString();
                        String next = currRow["PrevType"].ToString();
                        currRow["PrevPlace"] = prevRow["Territory_Name"];
                        currRow["NextPlace"] = nextRow["Territory_Name"];

                        currRow["PrevWtype"] = prevRow["Worktype_Name_B"];
                        currRow["NextWtype"] = nextRow["Worktype_Name_B"];
                        string curexpallowcat = currRow["Exp_Allow_Cat"].ToString();
                        int expcat = 0;
                        if (curexpallowcat != null && curexpallowcat != "" && (currRow["Territory_Cat"].ToString() == "OS" || currRow["Territory_Cat"].ToString() == "OS-EX"))
                        {
                            expcat = 1;
                            currRow["Allowance"] = (curexpallowcat == "MM" ? os : curexpallowcat == "SM" ? ossm : curexpallowcat == "NM" ? osnm : "0");
                        }
                        else
                        {
                            currRow["Allowance"] = (currRow["Territory_Cat"] == null ? "0" : (currRow["Territory_Cat"].ToString() == "HQ" ? hq : currRow["Territory_Cat"].ToString() == "OS" ? os : currRow["Territory_Cat"].ToString() == "EX" ? ex : currRow["Territory_Cat"].ToString() == "OS-EX" ? os : "0"));
                        }
                        String nextPlace = currRow["NextPlace"].ToString();
                        String currPlace = currRow["Territory_Name"].ToString();
                        string prevPlace = currRow["PrevPlace"].ToString();
                        string curwtype = currRow["Worktype_Name_B"].ToString();
                        string prevwtype = currRow["PrevWtype"].ToString();
                        string nextwtype = currRow["NextWtype"].ToString();

                        String currCatType = "";
                        String prevCatType = "";
                        String nextCatType = "";
                        string curtertyp = "";
                        curtertyp = currRow["curterrtyp"].ToString();

                        if (currRow["Activity_Date"].ToString() != "")
                        {
                            String filterp = "Activity_Date='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] lp = leavtyp.Select(filterp);

                            foreach (DataRow r in lp)
                            {

                                currRow["Worktype_Name_B"] = r["WType_SName"].ToString();
                            }
                        }
                        if (currRow["Territory_Cat"] != null)
                        {
                            currCatType = currRow["Territory_Cat"].ToString();
                        }
                        if (prevRow["prevType"] != null)
                        {
                            prevCatType = currRow["prevType"].ToString();
                        }
                        if (nextRow["nextType"] != null)
                        {
                            nextCatType = currRow["nextType"].ToString();

                        }
                        int intcnt = 0;
                        string temptype = "";
                        if (curwtype != "Field Work")
                        {

                            if (currCatType == "FA" && nextCatType == "FA" && curwtype == nextwtype && curwtype == prevwtype && i != 0 && (prevCatType == "FA" || prevCatType == "FF" || prevCatType == "FM"))
                            {
                                intcnt = 1;
                                temptype = "FM";
                            }
                            else if (currCatType == "FA" && nextCatType == "FA" && curwtype != nextwtype && curwtype != prevwtype && (prevCatType == "FA" || prevCatType == "FF" || prevCatType == "FM"))
                            {
                                intcnt = 1;
                                temptype = "FD";
                            }
                            else if (currCatType == "FA" && nextCatType == "FA" && curwtype != nextwtype && curwtype == prevwtype && (prevCatType == "FA" || prevCatType == "FF" || prevCatType == "FM"))
                            {
                                intcnt = 1;
                                temptype = "FF";
                            }
                            else if (currCatType == "FA" && nextCatType != "FA" && curwtype == nextwtype && curwtype != prevwtype && (prevCatType == "FA" || prevCatType == "FF" || prevCatType == "FM"))
                            {
                                intcnt = 1;
                                temptype = "FF";
                            }
                            else if (currCatType == "FA" && (prevCatType != "FA" || i == 0) && curwtype == nextwtype)
                            {
                                intcnt = 1;
                                temptype = "FF";
                            }
                            else if (currCatType == "FA" && curwtype != nextwtype && curwtype != prevwtype)
                            {
                                intcnt = 1;
                                temptype = "FD";
                            }
                            else if (currCatType == "FA" && curwtype != nextwtype)
                            {
                                intcnt = 1;
                                temptype = "FF";
                            }
                        }
                        currRow["catTemp"] = currCatType;
                        currRow["temtyp"] = temptype;

                        if (currCatType == "HQ")
                        {
                            currRow["From_place"] = "";
                            currRow["To_place"] = "";
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            //currRow["Allowance"] = hq;
                            //currRow["Total"] = hq;
                            //double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            double allow;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                            }
                            else
                            {
                                currRow["Allowance"] = hq;
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            currRow["Total"] = allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + 0;
                            totalFare = totalFare + 0;
                            grandTotal = grandTotal + allow;
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());

                        }
                        else if (currCatType == "EX")
                        {

                            //if (currRow["Worktype_Name_B"].Equals("Field Work"))
                            //{
                            //    currRow["From_place"] = currRow["sf_hq"];
                            //    currRow["To_place"] = getDisplayToPlaces(currRow["Territory_Name"].ToString());
                            //    currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            //}

                            currRow["From_place"] = "";
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }

                            pList.Add(currRow["Territory_Name"].ToString());

                            currRow["To_place"] = "";
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());

                            toPlace = getDistinctPlaces(toPlace);
                            string pp1 = "";

                            pp1 = getDisplayRemoveHQPlaces(toPlace);


                            double osDistance = 0;
                            double rngfare = 0;

                            double rangeMult = 0;
                            getExpenseAlloscondition(Exp, "S-END", pp1, out osDistance, out rngfare, "", "", curtertyp, out typ, out frmToPls, out rangeMult);
                            DataTable InterchangePlaces = null;
                            DataTable Interchangesfcode = null;

                            int altyp = 0;
                            InterchangePlaces = Exp.InterchangePlaces(sfcode, pp1);
                            Interchangesfcode = Exp.Interchangesfcode(sfcode);
                            if (InterchangePlaces.Rows.Count > 0 && Interchangesfcode.Rows.Count > 0)
                            {
                                altyp = 1;
                            }

                            if (altyp == 1)
                            {
                                frmToPls = frmToPls.Replace("Head Place", Interchangesfcode.Rows[0]["from_hq"].ToString());
                            }
                            else
                            {
                                frmToPls = frmToPls.Replace("Head Place", currRow["sf_hq"].ToString());
                            }
                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;

                            //currRow["Distance"] = osDistance;
                            //currRow["Fare"] = rngfare;

                            //double allow;
                            //allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            //double total = rngfare + allow;
                            //currRow["Total"] = total;
                            //totalAllowance = totalAllowance + allow;
                            //totalDistance = totalDistance + osDistance;
                            //totalFare = totalFare + rngfare;
                            //grandTotal = grandTotal + total;

                            String filter = "adate='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] rows = exDistTab.Select(filter);
                            int d = 0;
                            currRow["Distance"] = osDistance;

                            double Totfare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                HillStationPlaces = Exp.HillStationPlaces(sfcode, pp1);
                                currRow["Fare"] = osDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                                Totfare = osDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                Totfare = rngfare;
                            }
                            //foreach (DataRow r in rows)
                            //{
                            //    d = Convert.ToInt32(r["dist"].ToString());
                            //    currRow["Distance"] = d;
                            //}
                            //double totFare = d * fare;
                            //if (range > 0)
                            //{
                            //    totFare = rangeDistanceCalc(d, totFare, d);
                            //}

                            //currRow["Fare"] = totFare;
                            double allow;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                            }
                            else
                            {
                                currRow["Allowance"] = ex;
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            //double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            //currRow["Allowance"] = ex;
                            double total = Totfare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + Totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();

                            toPlace = "";

                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (prevCatType.Equals("HQ") || prevCatType.Equals("EX") || prevCatType.Equals("NA") || prevCatType.Equals("") || prevCatType.Equals("AS-FE") || prevCatType.Equals("FD") || prevCatType.Equals("FF") || i == 0) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FE") || nextCatType.Equals("FA") || nextCatType.Equals("AS-FA") || nextCatType.Equals("FD") || nextCatType.Equals("FF")))
                        {


                            if ("EX".Equals(osSingleContn) && expcat!=1)
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = ex;
                                    currRow["Territory_Cat"] = "EX";
                                }

                            }

                            else if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;

                            }
                            else if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                            {
                                currRow["Allowance"] = os;
                                currRow["Territory_Cat"] = "OS";
                            }

                            currRow["From_place"] = "";
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }

                            pList.Add(currRow["Territory_Name"].ToString());

                            currRow["To_place"] = "";
                            //currRow["To_place"] = getDisplayToPlaces(toPlace);
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());
                            toPlace = getDistinctPlaces(toPlace);




                            string pp1 = "";

                            pp1 = getDisplayToPlaces(toPlace);


                            double osDistance = 0;
                            double rngfare = 0;
                            double rangeMult = 0;
                            getExpenseAlloscondition(Exp, "S-END", pp1, out osDistance, out rngfare, "", "", curtertyp, out typ, out frmToPls, out rangeMult);
                            frmToPls = frmToPls.Replace("Head Place", currRow["sf_hq"].ToString());
                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;

                            currRow["Distance"] = osDistance;
                            double Totfare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                HillStationPlaces = Exp.HillStationPlaces(sfcode, pp1);
                                currRow["Fare"] = osDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                                Totfare = osDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                Totfare = rngfare;
                            }
                            //currRow["Fare"] = rngfare;

                            //double allow;
                            //allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            double allow;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {

                                allow = Convert.ToDouble(hill.ToString());
                            }
                            else
                            {

                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            double total = Totfare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + Totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();

                            toPlace = "";

                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FA") || nextCatType.Equals("AS-FE") || nextCatType.Equals("FA") || nextCatType.Equals("FD") || nextCatType.Equals("FF")))
                        {
                            if ("EX".Equals(osContn) && expcat!=1)
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = ex;
                                    currRow["Territory_Cat"] = "EX";
                                }

                            }
                            else
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = os;

                                   // currRow["Territory_Cat"] = "OS";
                                }

                            }
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;

                            }

                            double osDistance = 0;
                            double rngfare = 0;
                            double rangeMult = 0;


                            currRow["From_place"] = "";


                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }
                            pList.Add(currRow["Territory_Name"].ToString());

                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());
                            string places = getDistinctPlaces(toPlace);
                            //currRow["To_place"] = getDisplayToPlaces(places);
                            currRow["To_place"] = "";




                            string p = getDisplayToPlaces(places);
                            string pp1 = "";
                            string pp = "";

                            string nPlace = "";


                            pp1 = getDisplayToPlaces(places);


                            bool OStoOS = true;
                            // OStoOS = isOStoOSRelation(Exp1,currsfname);
                            if (OStoOS)
                            {
                                if (prevPlace != "" && prevCatType != "HQ")
                                {
                                    string pPlace = getSingleOsDistinctPlacesQry(prevPlace);
                                    String[] s = pPlace.Split(',');
                                    string withsingle = s[s.Length - 1];
                                    pp = withsingle.Substring(1, withsingle.Length - 2);


                                }

                                if (nextPlace != "")
                                {
                                    nPlace = getSingleOsDistinctPlacesQry(nextPlace);
                                    String[] ns = nPlace.Split(',');
                                    string withnsingle = ns[ns.Length - 1];
                                    string np = withnsingle.Substring(1, withnsingle.Length - 2);

                                }
                                //Latest changes

                                string with1 = "";
                                string cpls = "";

                                pp1 = getDisplayToPlaces(places);

                                String[] cs = pp1.Split(',');
                                if (cs.Length == 2)
                                {
                                    with1 = cs[cs.Length - 2];
                                }
                                else if (cs.Length == 1)
                                {
                                    with1 = cs[cs.Length - 1];
                                }
                                else if (cs.Length == 3)
                                {
                                    with1 = cs[cs.Length - 3];
                                }
                                else if (cs.Length == 4)
                                {
                                    with1 = cs[cs.Length - 4];
                                }
                                else
                                {
                                    with1 = cs[cs.Length - 1];
                                }
                                //with1 = cs[cs.Length - 1];
                               // with1 = cs[cs.Length - 2];
                                //cpls = with1.Substring(1, with1.Length - 2);
                                cpls = with1;
                                DataTable getallwdiffos = null;
                                int altyp = 0;
                                getallwdiffos = Exp.getallwdiffos(sfcode, cpls, pp);
                                if (getallwdiffos.Rows.Count > 0)
                                {
                                    altyp = 1;
                                }
                                if (altyp == 0 && cpls != pp && pp != "" && (prevCatType == "OS" || prevCatType == "OS-EX"))
                                {
                                    if ("EX".Equals(osSingleContn) && expcat!=1)
                                    {
                                       
                                            currRow["Allowance"] = ex;
                                            currRow["Territory_Cat"] = "EX";
                                        
                                    }

                                  }
                                //Latest change end
                                if (typ == 1)
                                {
                                    getExpenseAlloscondition(Exp, "START-END", pp1, out osDistance, out rngfare, pp, "", curtertyp, out typ, out frmToPls, out rangeMult);
                                }
                                else
                                {
                                    getExpenseAlloscondition(Exp, "END", pp1, out osDistance, out rngfare, pp, "", curtertyp, out typ, out frmToPls, out rangeMult);
                                }
                                frmToPls = frmToPls.Replace("Head Place", currRow["sf_hq"].ToString());
                                if (frmTovalues.Value == "")
                                    frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                                else
                                    frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;

                            }


                            tDistance = tDistance + osDistance;

                            currRow["Distance"] = tDistance;


                            double totFare = 0;



                            totFare = rngfare;


                            //currRow["Fare"] = totFare;

                            double Totfare1 = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                HillStationPlaces = Exp.HillStationPlaces(sfcode, pp1);
                                currRow["Fare"] = tDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                                Totfare1 = tDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                            }
                            else
                            {
                                currRow["Fare"] = totFare;
                                Totfare1 = totFare;
                            }

                            //double allow;
                            //allow = Convert.ToDouble(currRow["Allowance"].ToString());

                            double allow;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {

                                allow = Convert.ToDouble(hill.ToString());
                            }
                            else
                            {

                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            double total = Totfare1 + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + Totfare1;
                            grandTotal = grandTotal + total;



                            tDistance = 0;

                            pList = new ArrayList();

                            toPlace = "";

                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("")))
                        {


                            double osDistance = 0;
                            double rngfare = 0;
                            double rangeMult = 0;


                            if (toPlace != "")
                            {
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            }
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }

                            pList.Add(currRow["Territory_Name"].ToString());

                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());
                            string places = getDistinctPlaces(toPlace);
                            currRow["To_place"] = "";
                            //currRow["To_place"] = getDisplayToPlaces(places);


                            string p = getDisplayToPlaces(places);

                            string pp1 = "";
                            string pp2 = "";
                            string with1 = "";
                            string cpls = "";

                            pp1 = getDisplayToPlaces(places);
                            pp2 = getDisplayRemoveHQPlaces(places);
                            String[] cs = pp1.Split(',');
                            with1 = cs[cs.Length - 1];
                            //cpls = with1.Substring(1, with1.Length - 2);
                            cpls = with1;

                            string pPlace = "";

                            string withsingle = "";
                            string pp = "";

                            if (prevPlace != "" && prevCatType != "HQ" && i != 0)
                            {
                                pPlace = getSingleOsDistinctPlacesQry(prevPlace);


                                String[] s = pPlace.Split(',');
                                withsingle = s[s.Length - 1];
                                pp = withsingle.Substring(1, withsingle.Length - 2);




                            }

                            string nPlace = getSingleOsDistinctPlacesQry(nextPlace);
                            String[] ns = nPlace.Split(',');
                            string withnsingle = ns[0];
                            string np = withnsingle.Substring(1, withnsingle.Length - 2);

                            DataTable getallwdiffos = null;
                            int altyp = 0;
                            getallwdiffos = Exp.getallwdiffos(sfcode, cpls, np);
                            if (getallwdiffos.Rows.Count > 0)
                            {
                                altyp = 1;
                            }
                            if (altyp == 0 && cpls != np && np != "" && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA" || prevCatType == "FD" || prevCatType == "FF"))
                            {
                                if ("EX".Equals(osSingleContn) && expcat!=1)
                                {

                                    currRow["Allowance"] = ex;
                                    currRow["Territory_Cat"] = "EX";

                                }
                            }

                            string con = "CON";
                            if (typ == 1 || prevCatType == "EX")
                            {
                                con = "START";
                            }

                            if (prevPlace != "")
                            {
                                getExpenseAlloscondition(Exp, con, pp1, out osDistance, out rngfare, pp, np, curtertyp, out typ, out frmToPls, out rangeMult);
                            }
                            else
                            {
                                getExpenseAlloscondition(Exp, "START", pp1, out osDistance, out rngfare, pp, np, curtertyp, out typ, out frmToPls, out rangeMult);
                            }
                            frmToPls = frmToPls.Replace("Head Place", currRow["sf_hq"].ToString());
                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;

                            // getExpenseAlloscondition(Exp1, "START", pp1, out osDistance, out rngfare, pp, pSfcode, nSfcode, currsfname, osexcnt, excnt, types);


                            //if (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX"))
                            //{


                            //    getExpenseAlloscondition(Exp1, "START", pp1, out osDistance, out rngfare, pp, pSfcode, nSfcode, currsfname, osexcnt, excnt, types);
                            //}
                            //else
                            //{
                            //    getExpenseAlloscondition(Exp1, "START", pp1, out osDistance, out rngfare, "", "", nSfcode, currsfname, osexcnt, excnt, types);
                            //}



                            tDistance = tDistance + osDistance;
                            currRow["Distance"] = tDistance;

                            //double totFare = rngfare;

                            //currRow["Fare"] = rngfare;

                            double Totfare1 = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                HillStationPlaces = Exp.HillStationPlaces(sfcode, pp1);
                                currRow["Fare"] = tDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                                Totfare1 = tDistance * Convert.ToDouble(HillStationPlaces.Rows[0]["fare"].ToString());
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                Totfare1 = rngfare;
                            }
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;

                            }
                            double allow;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {

                                allow = Convert.ToDouble(hill.ToString());
                            }
                            else
                            {

                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }

                            //double allow;


                            //allow = Convert.ToDouble(currRow["Allowance"].ToString());


                            double total = Totfare1 + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + Totfare1;
                            grandTotal = grandTotal + total;



                            tDistance = 0;

                            pList = new ArrayList();

                            toPlace = "";


                        }

                        else if (!(currRow["Worktype_Name_B"].Equals("Field Work")) && (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX") || prevCatType.Equals("")))
                        {
                            currRow["Total"] = "0";
                            currRow["From_place"] = "";
                            currRow["To_place"] = "";
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_B"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                if (intcnt == 1)
                                {
                                    currRow["temtyp"] = temptype;
                                }
                                else
                                {
                                    currRow["catTemp"] = ss[0]["Allow_type"];
                                }
                            }
                        }
                        else
                        {
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            currRow["Total"] = allow;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_B"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                if (intcnt == 1)
                                {
                                    currRow["Territory_Cat"] = temptype;
                                }
                                else
                                {
                                    currRow["Territory_Cat"] = ss[0]["Allow_type"];
                                }
                            }
                            if (currRow["Allowance"].ToString().Equals("0"))
                            {
                                currRow["From_place"] = "";
                                currRow["Allowance"] = "0";
                                currRow["Distance"] = "0";
                                currRow["Fare"] = 0;
                            }
                            else
                            {
                                if (currRow["Worktype_Name_B"].Equals("Field Work"))
                                {
                                    currRow["From_place"] = currRow["sf_hq"];
                                }
                                currRow["Allowance"] = "0";
                                currRow["Distance"] = "0";
                                currRow["Fare"] = 0;
                            }

                            currRow["To_place"] = currRow["Territory_Name"];

                            double totFare = fare;

                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + 0;
                            totalFare = totalFare + 0;
                            grandTotal = grandTotal + allow;

                            toPlace = "";
                        }
                        if (currRow["Activity_Date"].ToString() == null || currRow["Activity_Date"].ToString() == "")
                        {
                            currRow["Allowance"] = totalAllowance;
                            currRow["Distance"] = totalDistance;
                            currRow["Fare"] = totalFare;
                            if (totaddexp.ToString() == "")
                            { totaddexp = 0; }
                            currRow["rw_amount"] = totaddexp;
                            currRow["Total"] = grandTotal;
                        }
                        if (currRow["Worktype_Name_B"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_B"].ToString().Equals("Holiday"))
                        {
                            // currRow["Adate"] = "<span style='background-color:#FFE2D5'>" + currRow["Adate"].ToString() + "</span>";
                            currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
                        }
                        if (currRow["Hill_Station"].ToString() == "Y")
                        {
                            currRow["theDayName"] = "<span style='background-color:#98C157' >" + currRow["theDayName"].ToString() + "</span>";
                        }

                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine("Exception Type: {0}", ex1.GetType());
                        Console.WriteLine("  Message: {0}", ex1.Message);
                    }

                }


                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                generateOtherExpListData(otherExpTable);

            }

            DataTable customExpTable = new DataTable();
            customExpTable.Columns.Add("Expense_Parameter_Name");
            customExpTable.Columns.Add("amount");
            customExpTable.Columns.Add("Expense_Parameter_Code");

            //DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
            customExpTable.Columns.Add("pro_Rate");

            DataTable expParamsAmnt = Exp.sp_Get_Fixed_expense(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            DataTable expDCRjoining = Exp.sp_DCRjoining(divcode, sfcode, dt3, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
            double otherExAmnt = 0;
            double twdys = 0;
            if (expParamsAmnt.Rows.Count > 0)
            {
                for (int i = 0; i < expParamsAmnt.Rows.Count; i++)
                {
                    string colName = "Fixed_Column" + (i + 1);
                    string prt = "pr" + (i + 1);
                    string fcdt = "fc" + (i + 1);
                    if (expParamsAmnt.Rows[i][colName].ToString() != "" && expDCRjoining.Rows.Count > 0)
                    {
                        //otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][colName].ToString());
                        twdys = Convert.ToDouble(expParamsAmnt.Rows[i]["cnt"].ToString());
                        otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][prt].ToString());
                    }
                    else
                    {
                        if (expParamsAmnt.Rows[i][colName].ToString() != "")
                        {
                            otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][colName].ToString());
                        }
                    }
                    customExpTable.Rows.Add();
                    if (expDCRjoining.Rows.Count > 0)
                    {
                        twdys1.Visible = true;
                        Tworkdys.InnerHtml = twdys.ToString();
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"] + "<span style=\"background-color:yellow\">" + " ( " + expParamsAmnt.Rows[i][fcdt] + " /- )" + "</span>";
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName].ToString() == "" ? "0" : expParamsAmnt.Rows[i][colName];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["pro_Rate"] = expParamsAmnt.Rows[i][prt].ToString() == "" ? "0" : expParamsAmnt.Rows[i][prt];
                    }
                    else
                    {

                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName].ToString() == "" ? "0" : expParamsAmnt.Rows[i][colName];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["pro_Rate"] = expParamsAmnt.Rows[i][colName].ToString() == "" ? "0" : expParamsAmnt.Rows[i][colName];
                    }
                }
                otherExpGrid.DataSource = customExpTable;
                otherExpGrid.DataBind();
            }


            Othtotal.Value = "0";
            double tot = otherExAmnt + grandTotal + otherExpAmnttot;
            grandTotalName.InnerHtml = tot.ToString();


            foreach (GridViewRow gridRow in grdExpMain.Rows)
            {
                Label lblCat = (Label)gridRow.FindControl("lblCat");
                Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                HiddenField allowTypeVal = (HiddenField)gridRow.FindControl("allowTypeHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden1");
                

                Label frmLbl = ((Label)gridRow.FindControl("lblFrom"));
                Label toLbl = ((Label)gridRow.FindControl("lblTo"));
                HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
                HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
                HiddenField allowString1 = (HiddenField)gridRow.FindControl("allowString1");
                //Label toHidden = (Label)gridRow.FindControl("toHidden");
                DropDownList frmBox = ((DropDownList)gridRow.FindControl("fromPlace"));
                DropDownList toBox = ((DropDownList)gridRow.FindControl("toPlace"));
                DropDownList allowType = ((DropDownList)gridRow.FindControl("AllowType"));
                Label allTypLbl = ((Label)gridRow.FindControl("lblCat"));
                TextBox txtAllow = ((TextBox)gridRow.FindControl("txtAllow"));
                Label allowLbl = ((Label)gridRow.FindControl("lblAllw"));
                TextBox txtFare = ((TextBox)gridRow.FindControl("txtFare"));
                Label fareLbl = ((Label)gridRow.FindControl("lblFare"));
                HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));
                String filter = "Work_Type_Name='" + lblWorkType.Text + "' and Allow_type='" + allTypLbl.Text + "'";
                String filter1 = "Work_Type_Name='" + lblWorkType.Text + "'";

                Label lbladdExp = (Label)gridRow.FindControl("lbladdExp");
                Label lblrmks = (Label)gridRow.FindControl("lblrmks");
                TextBox txtaddexp = ((TextBox)gridRow.FindControl("txtaddexp"));
                TextBox txtrmks = ((TextBox)gridRow.FindControl("txtrmks"));
                Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
                if ("".Equals(lbl_aDate.Text))
                { lbladdExp.Visible = true; }
                if ("Weekly Off".Equals(lblWorkType.Text) || "Holiday".Equals(lblWorkType.Text) || "Leave".Equals(lblWorkType.Text) || "".Equals(lbl_aDate.Text))
                {
                    txtrmks.Visible = false;
                    lblrmks.Visible = true;
                    txtaddexp.Visible = false;

                }

                if ("Field Work".Equals(lblWorkType.Text) && (("OS".Equals(allTypLbl.Text)) || ("OS-EX".Equals(allTypLbl.Text)) || ("EX".Equals(allTypLbl.Text))))
                {
                    dtlBtn.Style.Add("visibility", "visible");

                }

                if ("Field Work".Equals(lblWorkType.Text) && ("".Equals(allTypLbl.Text) || "0".Equals(allTypLbl.Text) || "--Select--".Equals(allTypLbl.Text)))
                {
                    allTypLbl.Text = "AS-FE";
                    allowTypeHidden.Value = "AS-FE";
                }

                if ("Field Work".Equals(lblWorkType.Text))
                {
                    if ("0".Equals(fareLbl.Text) && !("HQ".Equals(lblCat.Text)))
                    {
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        txtFare.Text = "";
                    }
                    DataRow[] dr = ofwt.Select(filter);
                    if (dr.Count() > 0)
                    {
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                    }
                }
                if ("Field Work".Equals(lblWorkType.Text) && ("EX".Equals(allTypLbl.Text) || "OS".Equals(allTypLbl.Text) || "OS-EX".Equals(allTypLbl.Text)))
                {
                    DataRow[] dr1 = ofwtexos.Select(filter1);
                    if (dr1.Count() > 0)
                    {
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                    }
                }
                if ("AA-NF".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("NA-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    allowLbl.Text = "";
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    allowLbl.Text = "0";
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AE-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = true;
                    allowLbl.Visible = false;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AA-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AA-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AS-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = true;
                    allowType.SelectedValue = allowTypeVal.Value;
                    allTypLbl.Visible = false;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AS-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = true;
                    allowType.SelectedValue = allowTypeVal.Value;
                    allTypLbl.Visible = false;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
                else if ("AE-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = true;
                    allowLbl.Visible = false;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(toLists, toLbl, toBox, toHidden.Value);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
            Response.Write(ex.Message);
        }
    }
    protected void lbFileDel_Onclick(object sender, EventArgs e)
    {
        lblAttachment.Text = "";
        lbFileDel.Visible = false;


    }
   

    private double rangeDistanceCalcOtherType(int d, double totFare, int indOs)
    {
        if (range > 0)
        {
            int onewayDistance = indOs;
            if (onewayDistance > range3 && range3 > 0)
            {
                if ("Consolidated".Equals(rangeType3))
                {
                    totFare = d * rangeFare3;
                }
                else
                {

                    double f1 = ((onewayDistance - range3) * rangeFare3);
                    double f2 = ((range3 - range2) * rangeFare2);
                    double f3 = ((range2 - range) * rangeFare);
                    double f4 = (range) * fare;
                    totFare = f1 + f2 + f3 + f4;
                }
            }
            else if (onewayDistance > range2 && range2 > 0)
            {
                if ("Consolidated".Equals(rangeType2))
                {
                    totFare = d * rangeFare2;
                }
                else
                {
                    //double f1 = ((onewayDistance - range2) * rangeFare2)*2;
                    // double f2 = (range2*2) * fare;
                    double f1 = ((onewayDistance - range2) * rangeFare2);
                    double f2 = ((range2 - range) * rangeFare);
                    double f3 = (range) * fare;
                    totFare = f1 + f2 + f3;
                }
            }
            else if (onewayDistance > range && range > 0)
            {
                if ("Consolidated".Equals(rangeType))
                {
                    totFare = d * rangeFare;
                }
                else
                {
                    double f1 = ((onewayDistance - range) * rangeFare);
                    double f2 = (range) * fare;

                    totFare = f1 + f2;
                }
            }
            else
            {
                totFare = d * fare;
            }

        }
        else
        {
            totFare = d * fare;
        }
        return totFare;
    }
    private void generateOtherExpListData(DataTable otherExpTable)
    {
        if (otherExpTable.Rows.Count > 0)
        {
            foreach (DataRow row in otherExpTable.Rows)
            {
                ListItem list = new ListItem();
                list.Text = row["Expense_Parameter_Name"].ToString();
                list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
                otherExp.Items.Add(list);
            }
            //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
            //otherExp.DataTextField = "Expense_Parameter_Name";
        }
    }

    private static void changeFromToCtrl(ListItem[] toLists, Label toLbl, DropDownList toBox, string to)
    {
        //ListItem[] newfrmLists = new ListItem[frmLists.Count()];
        ListItem[] newtoLists = new ListItem[toLists.Count()];
        //for (int i = 0; i < frmLists.Count(); i++)
        //{
        //    ListItem l = new ListItem();
        //    l.Text = frmLists[i].Text;
        //    l.Value = frmLists[i].Value;
        //    newfrmLists[i] = l;
        //}
        for (int i = 0; i < toLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = toLists[i].Text;
            l.Value = toLists[i].Value;
            newtoLists[i] = l;
        }

        //frmLbl.Visible = false;
        //frmBox.Items.AddRange(newfrmLists);
        //if (from != null && from != "")
        //{
        //    frmBox.ClearSelection();
        //    frmBox.Items.FindByText(from).Selected = true;
        //}
        toBox.Visible = true;
        toLbl.Visible = false;
        toBox.Items.AddRange(newtoLists);
        if (to != null && to != "")
        {
            toBox.ClearSelection();
            toBox.Items.FindByText(to).Selected = true;
        }
    }
   
    private void getExpenseAlloscondition(Distance_calculation Exp, string callType, string places, out double osDistance, out double rngfare, string prevPlace, string np, string curtertyp, out int typ, out string frmToPls, out double rangeMult)
    {
        rngfare = Exp.getOSDistanceBySP_bak(callType, places, prevPlace, sfcode, out osDistance, np, curtertyp, out typ, out frmToPls, out rangeMult, divcode);
    }
    
    protected void btn_Go(object sender, EventArgs e)
    {

        lblAttachment.Text = FileUpload1.FileName;
        if (lblAttachment.Text != "")
        {
            strFileDateTime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            Session["strFileDateTime"] = strFileDateTime;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Expense/Attachment/" + strFileDateTime + FileUpload1.FileName));
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

        //ModalPopupExtender1.Show();
        //Button ddl = (Button)sender;
        //PnlAttachment.Style.Add("visibility", "visible");
        //pnlCompose.Style.Add("visibility", "visible");
        //ViewState["PnlAttachment"] = "";
        //ViewState["from"] = "";


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        saveData("1");
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {

        saveData("0");
    }
    private void saveData(string flag)
    {
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
        values["flag"] = flag;
        values["s_date"] = "2016-01-03 20:43:47.643";
        values["File_Path"] = Attachpath;
        HiddenField frmTovalues = (HiddenField)FindControl("frmTovalues");
        values["frmTovalues"] = frmTovalues.Value;
        int iReturn = -1;
        Distance_calculation dist = new Distance_calculation();
        DataTable table = dist.getMgrAppr(divcode);
        DataTable ds1 = dist.getFieldForce(divcode, sfcode);
        if (ds1.Rows.Count > 0)
        {
            values["EmpNo"] = ds1.Rows[0]["SF_Emp_Id"].ToString();
            values["SfEmpNo"] = ds1.Rows[0]["Employee_Id"].ToString();
        }
        else
        {
            values["EmpNo"] = "null";
            values["SfEmpNo"] = "null";
        }
        string soj = ds1.Rows[0]["sf_joining_date"].ToString();
        if (table.Rows.Count > 0)
        {
            if ("1".Equals(table.Rows[0]["MgrAppr_Remark"].ToString()))
            {
                values["flag"] = "7";

            }
            if (ds1.Rows[0]["reporting_to_sf"].ToString() == "admin")
            {
                values["flag"] = "1";
            }
        }
        values["soj"] = soj;





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
                   "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,File_Path,frmTovalues,SF_JOINING_DATE,Employee_Id,sf_emp_id)" +
                   "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                   values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",'" + values["File_Path"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["EmpNo"] + "','" + values["SfEmpNo"] + "')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "INSERT INTO Trans_Expense_Head " +
                     "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,File_Path,frmTovalues,SF_JOINING_DATE,Employee_Id,sf_emp_id)" +
                     "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                     values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",'" + values["File_Path"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["EmpNo"] + "','" + values["SfEmpNo"] + "')";
                    command.ExecuteNonQuery();
                }

                if (Request.QueryString["type"] != null)
                {
                    DateTime dt1 = DateTime.Now;
                    int curr_month = dt1.Month;
                    int curr_year = dt1.Year;

                    var today = DateTime.Today;
                    var mnth = new DateTime(today.Year, today.Month, 1);
                    var first = mnth.AddMonths(-1);
                    var last_month_date = mnth.AddDays(-1);

                    int last_month = last_month_date.Month;
                    int last_month_year = last_month_date.Year;

                    if (curr_month == Convert.ToInt32(monthId.SelectedValue) && curr_year == Convert.ToInt32(yearID.SelectedValue))
                    {

                        int icnt2 = 0;
                        command.CommandText = "select count(sf_code) from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'";
                        icnt2 = (Int32)command.ExecuteScalar();
                        if (icnt2 > 0)
                        {
                            command.CommandText = "update Trans_Expense_Head set resigned_flag=1 WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'";
                            command.ExecuteNonQuery();
                        }
                        else
                        {

                            command.CommandText = "update Trans_Expense_Head set resigned_flag=1 WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + last_month + " and Expense_Year=" + last_month_year + " and sf_joining_date='" + values["soj"] + "'";
                            command.ExecuteNonQuery();
                        }
                    }

                    else if (last_month == Convert.ToInt32(monthId.SelectedValue) && last_month_year == Convert.ToInt32(yearID.SelectedValue))
                    {

                        int iCount3 = 0;
                        command.CommandText = "  SELECT COUNT(trans_slno) FROM DCRMain_trans a , mas_salesforce b WHERE a.sf_code='" + values["sf_code"] + "' " +
                        " and month(activity_date)='" + curr_month + "' and year(activity_date)='" + curr_year + "' " +
                        " and activity_date>=sf_joining_date and a.sf_code=b.sf_code ";
                        iCount3 = (Int32)command.ExecuteScalar();
                        if (iCount3 == 0)
                        {
                            command.CommandText = "update Trans_Expense_Head set resigned_flag=1 WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + last_month + " and Expense_Year=" + last_month_year + " and sf_joining_date='" + values["soj"] + "'";
                            command.ExecuteNonQuery();
                        }
                    }

                }


                Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();

                foreach (GridViewRow gridRow in grdExpMain.Rows)
                {

                    values = new Dictionary<String, String>();
                    values["sf_code"] = sfcode;
                    values["div_code"] = divcode;
                    values["month"] = monthId.SelectedValue.ToString();
                    values["year"] = yearID.SelectedValue.ToString();
                    values["soj"] = soj;
                    Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
                    string date = lbl_aDate.Text;
                    Label lblDayName = (Label)gridRow.FindControl("lblDayName");
                    Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                    Label lblTerrName = (Label)gridRow.FindControl("lblTerrName");
                    Label lblCat = (Label)gridRow.FindControl("lblCat");

                    HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
                    HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
                    HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
                    HiddenField allowTypeHidden2 = (HiddenField)gridRow.FindControl("allowTypeHidden2");
                    Label lblAllw = (Label)gridRow.FindControl("lblAllw");
                    HiddenField allowHidden = (HiddenField)gridRow.FindControl("allowHidden");
                    Label lblFrom = (Label)gridRow.FindControl("lblFrom");

                    Label lblTo = (Label)gridRow.FindControl("lblTo");
                    HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
                    Label lblDistance = (Label)gridRow.FindControl("lblDistance");
                    Label lblFare = (Label)gridRow.FindControl("lblFare");
                    HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
                    Label lbladdExp = (Label)gridRow.FindControl("lbladdExp");
                    HiddenField addExpHidden = (HiddenField)gridRow.FindControl("addExpHidden");
                    TextBox txtaddexp = ((TextBox)gridRow.FindControl("txtaddexp"));
                    TextBox txtrmks = ((TextBox)gridRow.FindControl("txtrmks"));
                    Label lblTotal = (Label)gridRow.FindControl("lblTotal");
                    HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
                    HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
                   

                    values["dayName"] = lblDayName.Text;
                    values["adate"] = date;
                    values["adate1"] = adateHidden.Value;
                    values["dayName"] = lblDayName.Text;
                    values["workType"] = lblWorkType.Text;
                    values["terrName"] = lblTerrName.Text;
                    values["cat"] = allowTypeHidden.Value;
                    values["allowance"] = allowHidden.Value;
                    values["distance"] = distHidden.Value;
                    values["fare"] = fareHidden.Value;
                    values["rw_amount"] = addExpHidden.Value;
                    values["rw_rmks"] = txtrmks.Text;
                    values["total"] = totHidden.Value;
                    values["from"] = "";
                    values["to"] = toHidden.Value;
                    values["catTemp"] = allowTypeHidden1.Value;
                    values["temtyp"] = allowTypeHidden2.Value;

                    if (date != "")
                    {
                        command.CommandText = "INSERT INTO Trans_Expense_Detail " +
                                                           "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,rw_amount,rw_rmks,temtyp)" +
                                                           "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                                           values["adate"].Replace("'", "\"") + "','" + values["dayName"].Replace("'", "\"") + "','" + values["workType"] + "','" + values["adate1"] + "','" + values["terrName"] + "','" + values["cat"]
                                                           + "','" + values["allowance"] + "','" + values["distance"] + "','" + values["fare"] + "','" + values["catTemp"] + "','" + values["total"] + "','" + values["div_code"] + "','" + values["from"] + "','" + values["to"] + "','" + values["rw_amount"] + "','" + values["rw_rmks"] + "','" + values["temtyp"] + "')";
                        command.ExecuteNonQuery();
                    }
                }


                foreach (GridViewRow gridRow in otherExpGrid.Rows)
                {

                    HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
                    Label amnt = (Label)gridRow.FindControl("lblProAmnt");
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


                if (Request.QueryString["type"] != null)
                {
                    SalesForce sf = new SalesForce();
                    iReturn = sf.check_vac_ex(sfcode);

                    if (iReturn > 1)
                    {

                        var today = DateTime.Today;
                        var month = new DateTime(today.Year, today.Month, 1);
                        var first = month.AddMonths(-1);
                        var last_month_date = month.AddDays(-1);

                        int last_month = last_month_date.Month;
                        int last_month_year = last_month_date.Year;

                        DateTime dt = DateTime.Now;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;
                        if (iReturn == 5)
                        {

                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_RowWise_Textbox.aspx?type=" + Request.QueryString["type"] + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode + "'</script>");
                        }
                        else if (iReturn == 6)
                        {

                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_RowWise_Textbox.aspx?type=" + Request.QueryString["type"] + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode + "'</script>");
                        }


                    }
                    else
                    {


                        if (Request.QueryString["type"] == "Promote")
                        {

                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Designation_Name=Promote'</script>");
                        }

                        else if (Request.QueryString["type"] == "DePromote")
                        {

                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Reporting_To_Manager=DePromote'</script>");
                        }

                        else if (Request.QueryString["type"] == "Hold")
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sf_hold=Hold" + " &sfcode=" + sfcode + "'</script>");
                        }

                        else
                        {

                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "'</script>");
                        }
                    }

                }

                else
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_RowWise_Textbox.aspx'</script>");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                Console.WriteLine("Message: {0}", ex.Message);


                try
                {

                    transaction.Rollback();

                }

                catch (Exception ex2)
                {


                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                    Console.WriteLine("  Message: {0}", ex2.Message);

                }
                if (Request.QueryString["type"] != null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Expired.Kindly Resubmit Again');window.location='../../MasterFiles/SalesforceList.aspx'</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Expired.Kindly Resubmit Again');window.location='RptAutoExpense_RowWise_Textbox.aspx'</script>");
                }


            }
        }
    }

    //dist.addHeadRecord(values);
    //    if (Request.QueryString["type"] != null)
    //    {
    //        DateTime dt1 = DateTime.Now;
    //        int curr_month = dt1.Month;
    //        int curr_year = dt1.Year;

    //        var today = DateTime.Today;
    //        var mnth = new DateTime(today.Year, today.Month, 1);
    //        var first = mnth.AddMonths(-1);
    //        var last_month_date = mnth.AddDays(-1);

    //        int last_month = last_month_date.Month;
    //        int last_month_year = last_month_date.Year;

    //        if (curr_month == Convert.ToInt32(monthId.SelectedValue) && curr_year == Convert.ToInt32(yearID.SelectedValue))
    //        {
    //            dist.updateHeadFlgResigned(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);
    //        }

    //        else if (last_month == Convert.ToInt32(monthId.SelectedValue) && last_month_year == Convert.ToInt32(yearID.SelectedValue))
    //        {
    //            dist.updateHeadFlgResigned_lastmonth(sfcode, last_month, last_month_year, curr_month, curr_year, soj);
    //        }

    //    }
    //    Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();
    //    int count = 0;
    //    foreach (GridViewRow gridRow in grdExpMain.Rows)
    //    {

    //        values = new Dictionary<String, String>();
    //        values["sf_code"] = sfcode;
    //        values["div_code"] = divcode;
    //        values["month"] = monthId.SelectedValue.ToString();
    //        values["year"] = yearID.SelectedValue.ToString();
    //        values["soj"] = soj;
    //        Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
    //        string date = lbl_aDate.Text;
    //        Label lblDayName = (Label)gridRow.FindControl("lblDayName");
    //        Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
    //        Label lblTerrName = (Label)gridRow.FindControl("lblTerrName");
    //        Label lblCat = (Label)gridRow.FindControl("lblCat");

    //        HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
    //        HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
    //        HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
    //        HiddenField allowTypeHidden2 = (HiddenField)gridRow.FindControl("allowTypeHidden2");
    //        Label lblAllw = (Label)gridRow.FindControl("lblAllw");
    //        HiddenField allowHidden = (HiddenField)gridRow.FindControl("allowHidden");
    //        Label lblFrom = (Label)gridRow.FindControl("lblFrom");

    //        Label lblTo = (Label)gridRow.FindControl("lblTo");
    //        HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
    //        Label lblDistance = (Label)gridRow.FindControl("lblDistance");
    //        Label lblFare = (Label)gridRow.FindControl("lblFare");
    //        HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
    //        Label lbladdExp = (Label)gridRow.FindControl("lbladdExp");
    //        HiddenField addExpHidden = (HiddenField)gridRow.FindControl("addExpHidden");
    //        Label lblTotal = (Label)gridRow.FindControl("lblTotal");
    //        HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
    //        HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
    //        TextBox txtaddexp = ((TextBox)gridRow.FindControl("txtaddexp"));
    //        TextBox txtrmks = ((TextBox)gridRow.FindControl("txtrmks"));

    //        values["dayName"] = lblDayName.Text;
    //        values["adate"] = date;
    //        values["adate1"] = adateHidden.Value;
    //        values["dayName"] = lblDayName.Text;
    //        values["workType"] = lblWorkType.Text;
    //        values["terrName"] = lblTerrName.Text;
    //        values["cat"] = allowTypeHidden.Value;
    //        values["allowance"] = allowHidden.Value;
    //        values["distance"] = distHidden.Value;
    //        values["fare"] = fareHidden.Value;
    //        values["rw_amount"] = addExpHidden.Value;
    //        values["rw_rmks"] = txtrmks.Text;
    //        values["total"] = totHidden.Value;
    //        values["from"] = "";
    //        values["to"] = toHidden.Value;
    //        values["catTemp"] = allowTypeHidden1.Value;
    //        values["temtyp"] = allowTypeHidden2.Value;

    //        if (date != "")
    //        {
    //            valueList[count] = values;
    //            count = count + 1;
    //            //iReturn = dist.addDetailRecord(values);
    //        }
    //    }
    //    iReturn = dist.addAllDetailRecord(valueList);
    //    dist.deleteFixed(values);

    //    foreach (GridViewRow gridRow in otherExpGrid.Rows)
    //    {

    //        HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
    //        Label amnt = (Label)gridRow.FindControl("lblProAmnt");
    //        values["Amt"] = amnt.Text;
    //        values["Expense_Parameter_Code"] = Expense_Parameter_Code.Value;

    //        iReturn = dist.addFixedRecord(values);

    //    }
    //    HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
    //    dist.deleteOtheExp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);
    //    string[] splitVal = otherExpValues.Value.Split('~');

    //    string[] rms = splitVal[0].Split(',');
    //    string[] amount = splitVal[1].Split(',');
    //    string[] exp = splitVal[2].Split(',');
    //    for (int p = 0; p < exp.Length; p++)
    //    {
    //        isEmpty = false;
    //        string[] e = exp[p].Split('=');
    //        if (!e[1].Contains("--Select--") || amount[p] != "")
    //        {

    //            iReturn = dist.addOthExpRecord(e[0], e[1], amount[p], rms[p], sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);
    //        }
    //    }

    //    if (iReturn > 0)
    //    {
    //        if (Request.QueryString["type"] != null)
    //        {
    //            SalesForce sf = new SalesForce();
    //            iReturn = sf.check_vac_ex(sfcode);

    //            if (iReturn > 1)
    //            {

    //                var today = DateTime.Today;
    //                var month = new DateTime(today.Year, today.Month, 1);
    //                var first = month.AddMonths(-1);
    //                var last_month_date = month.AddDays(-1);

    //                int last_month = last_month_date.Month;
    //                int last_month_year = last_month_date.Year;

    //                DateTime dt = DateTime.Now;
    //                int curr_month = dt.Month;
    //                int curr_year = dt.Year;
    //                if (iReturn == 5)
    //                {
    //                    Response.Redirect("RptAutoExpense_RowWise.aspx?type=" + Request.QueryString["type"] + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
    //                }
    //                else if (iReturn == 6)
    //                {
    //                    Response.Redirect("RptAutoExpense_RowWise.aspx?type=" + Request.QueryString["type"] + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
    //                }


    //            }
    //            else
    //            {

    //                if (Request.QueryString["type"] == "Promote")
    //                {
    //                    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Designation_Name=Promote");
    //                }

    //                else if (Request.QueryString["type"] == "DePromote")
    //                {
    //                    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Reporting_To_Manager=DePromote");
    //                }
    //                else
    //                {
    //                    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode);
    //                }
    //            }
    //        }
    //        else
    //        {

    //            Response.Redirect("RptAutoExpense_RowWise.aspx");
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
    //        }
    //    }
    //}

    protected override void OnLoadComplete(EventArgs e)
    {
        if (isSavedPage)
        {
            Distance_calculation dist = new Distance_calculation();
            if (!isEmpty)
                generateOtherExpControls(dist);
        }
    }

    private void generateOtherExpControls(Distance_calculation dist)
    {
        DataTable ds = dist.getFieldForce(divcode, sfcode);
        string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
        DataTable t2 = dist.getSavedOtheExpRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable otherExp1 = dist.getOthrExp(divcode);

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
                    list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
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
            lit.Text = @"<input type='text' class='textbox' name='OthExpVal' id='OthExpVal_" + i + "' runat='server' value='" + amnt + "' size='6' maxlength=4 onkeyup='OthExpCalc(this)'/>";
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