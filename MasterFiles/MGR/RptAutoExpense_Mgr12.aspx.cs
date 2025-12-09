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
using DBase_EReport;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{

    bool isSavedPage = false;
    bool isEmpty = true;
    string sfcode = string.Empty;
    string strFileDateTime = string.Empty;
    string divcode = string.Empty;
    DataSet expenseDataset = null;
    DataSet placeDataset = null;
    DataSet MgrAllwTyp = null;
    double otherExpAmnttot = 0;
    string osFrmCode1 = string.Empty;
    int distrng;
    int contos = 0;
    int sNo = 0;
    string home = string.Empty;
    ArrayList pList = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Convert.ToString(Session["Sf_Code"]);
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
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2015; intCount <= year + 1; intCount++)
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
                string p1 = p.Substring(0, p.IndexOf('(')) + "<span style=\"background-color:yellow\">" + type + "</span>";

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
    private string getOSEXPlace(string places)
    {
        if (null != places && places != "")
        {

            String[] iteams = places.Split(',');
            foreach (string i in iteams)
            {
                if (!i.Contains("(OS)") && i.Contains("(OS-EX"))
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
                list.Add(i);
            }
            else
            {
                list.Add(i);
            }

        }

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

        return distinctValues.Replace("<span style=\"background-color:yellow\">", "");
    }
    private string getDisplayRemoveHQEXPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                if (p.Contains("(OS") || p.Contains("(OS-EX"))
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
        }
        return distinctValues;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RptAutoExpense_Mgr.aspx");
        mainDiv.Style.Remove("background-color");

    }
    double fare = 0;
    double rangeFare2 = 0;
    int range2 = 0;
    String rangeType2 = "Consolidated";
    double rangeFare = 0;
    int range = 0;
    String rangeType = "Consolidated";

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";

            Distance_calculation_001 Exp = new Distance_calculation_001();

            Distance_calculation Exp1 = new Distance_calculation();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            DataTable owt = Exp.getotherWorkType(divcode);
            DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode);
            menuId.Visible = false;
            heading.Visible = true;
            twdid.Visible = true;
            mnthtxtId.InnerHtml = monthId.SelectedItem.ToString();
            yrtxtId.InnerHtml = yearID.SelectedValue.ToString();
            fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
            hqId.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
            empId.InnerHtml = "Employee Code :" + ds.Rows[0]["Employee_Id"].ToString();
            double totalAllowance = 0;
            double totalDistance = 0;
            double totalFare = 0;
            double grandTotal = 0;
            otherExpAmnttot = 0;

            DataTable dist = Exp.getDist(divcode, sfcode);





            DataSet dsAllowance = Exp1.getAllow(sfcode);
            DataTable allowanceTab = dsAllowance.Tables[0];
            DataTable otherExpTable = Exp.getOthrExp(divcode);

            String ex = "0";
            String os = "0";
            String hq = "0";
            String hill = "0";

            DataTable rGdT = new DataTable();
            SalesForce sf = new SalesForce();
            rGdT = sf.GetRange(divcode);
            if (rGdT.Rows.Count > 0)
            {
                range = Convert.ToInt32(rGdT.Rows[0]["Range1_KMS"].ToString());
                range2 = Convert.ToInt32(rGdT.Rows[0]["Range2_KMS"].ToString());
                rangeType = rGdT.Rows[0]["Range1_status"].ToString();
                rangeType2 = rGdT.Rows[0]["Range2_status"].ToString();

            }

            string allowStr = "";
            if (allowanceTab != null)
            {
                foreach (DataRow row in allowanceTab.Rows)
                {
                    ex = row["ex_allowance"].ToString();
                    os = row["os_allowance"].ToString();
                    hq = row["hq_allowance"].ToString();
                    hill = row["os_allowance"].ToString();
                    fare = Convert.ToDouble(row["fare"].ToString());
                    rangeFare = Convert.ToDouble(row["Range_of_Fare1"].ToString());
                    rangeFare2 = Convert.ToDouble(row["Range_of_Fare2"].ToString());
                    allowStr = ex + "@" + os + "@" + hq + "@" + fare + "@" + hill;
                }
                allowString.Value = allowStr;
            }



            if (Exp.headRecordExist(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode))
            {
                DataTable t1 = Exp.getSavedRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);

                foreach (DataRow row in t1.Rows)
                {
                    totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
                    totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
                    totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
                    grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
                    String filter = "Work_Type_Name='" + row["Worktype_Name_M"].ToString() + "'";


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
                generateOtherExpControls(Exp1);
                DataTable headR = Exp.getHeadRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
                if (headR.Rows[0]["Status"].ToString() == "1" || headR.Rows[0]["Status"].ToString() == "7" || headR.Rows[0]["Status"].ToString() == "8")
                {
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    messageId.Visible = true;
                    messageId.Text = "Expense yet to proccess";

                }
                else if (headR.Rows[0]["Status"].ToString() == "2")
                {
                    messageId.Visible = true;
                    messageId.Text = "Expense proccessed";
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;


                }
                else
                {
                    messageId.Visible = false;

                }



            }
            else
            {


                DataSet dsExDist = null;
                expenseDataset = Exp.getExpense_MGR_prevnextrelation(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                placeDataset = Exp.getPlaceMGRNEW(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                MgrAllwTyp = Exp.getMGRAllowTyp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());

                Dictionary<String, String> allowValues = new Dictionary<String, String>();
                DataTable tt = Exp1.getMgrAppr(divcode);
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
                string osSingleContn = "OS";
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
                DataTable t7 = MgrAllwTyp.Tables[0];

                foreach (DataRow row in t1.Rows)
                {
                    String filter = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                    DataRow[] rows = t2.Select(filter);

                    if (!(row["Worktype_Name_M"].Equals("Field Work")) && (!(row["Worktype_Name_M"].Equals("Weekly Off")) || !(row["Worktype_Name_M"].Equals("Holiday"))))
                    {

                        row["Type_Code"] = "AS-FE";

                    }

                    foreach (DataRow r in rows)
                    {

                        if (row["TerrPlaces"] != null && row["TerrPlaces"].ToString() != "")
                        {
                            row["TerrPlaces"] = row["TerrPlaces"].ToString() + "," + r["TerrPlaces"] + "(" + r["Type_Code"].ToString() + ")";
                        }
                        else
                        {
                            row["TerrPlaces"] = r["TerrPlaces"] + "(" + r["Type_Code"].ToString() + ")";
                        }

                        String old = "";
                        String curr = "";

                        if (row["Type_Code"] != null)
                        {
                            old = row["Type_Code"].ToString();
                        }
                        if (r["Type_Code"] != null)
                        {
                            curr = r["Type_Code"].ToString();
                        }
                        if (old == "")
                        {
                            row["Type_Code"] = curr;
                        }
                        else if (old.Equals("OS-EX"))
                        {
                        }
                        else if (curr.Equals("OS-EX"))
                        {
                            row["Type_Code"] = curr;
                        }
                        else if (curr.Equals("OS") && !old.Equals("OS-EX"))
                        {
                            row["Type_Code"] = curr;
                        }
                        else if (old.Equals("OS"))
                        {
                        }
                        else if (old.Equals("EX"))
                        {
                        }
                        else if (old.Equals("HQ") && curr.Equals("EX"))
                        {
                            row["Type_Code"] = curr;
                        }
                        else
                        {
                            row["Type_Code"] = curr;
                        }
                    }
                    DataRow[] rows1 = t7.Select(filter);
                    foreach (DataRow r1 in rows1)
                    {
                        string tp1 = r1["typcode"].ToString();
                        row["TypMGR"] = r1["typcode"];

                        if (tp1 == "OS")
                        {
                            row["Type_Code"] = "OS";
                        }

                    }

                }

                t1.Columns.Add("PrevType");
                t1.Columns.Add("NextType");
                t1.Columns.Add("PrevName");
                t1.Columns.Add("NextName");
                t1.Columns.Add("Allowance");
                t1.Columns.Add("Distance");
                t1.Columns.Add("Fare");
                t1.Columns.Add("Total");
                t1.Columns.Add("catTemp");
                t1.Columns.Add("PrevPlace");
                t1.Columns.Add("NextPlace");
                t1.Columns.Add("Nextsfcode");
                t1.Columns.Add("Prevsfcode");
                t1.Rows.Add();
                string toPlace = "";

                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    try
                    {

                        int prevIndex = (i == 0 ? i : i - 1);
                        int nextIndex = (i == t1.Rows.Count - 1 ? i : i + 1);
                        DataRow currRow = t1.Rows[i];
                        DataRow prevRow = t1.Rows[prevIndex];
                        DataRow nextRow = t1.Rows[nextIndex];
                        currRow["PrevType"] = prevRow["Type_Code"];
                        currRow["NextType"] = nextRow["Type_Code"];
                        String prev = currRow["PrevType"].ToString();
                        String next = currRow["PrevType"].ToString();
                        currRow["PrevName"] = prevRow["sf_Name"];
                        currRow["NextName"] = nextRow["sf_Name"];
                        String prev1 = currRow["PrevName"].ToString();
                        String next1 = currRow["NextName"].ToString();
                        currRow["PrevPlace"] = prevRow["TerrPlaces"];
                        currRow["NextPlace"] = nextRow["TerrPlaces"];
                        currRow["Prevsfcode"] = prevRow["Territory_Name1"];
                        currRow["Nextsfcode"] = nextRow["Territory_Name1"];
                        currRow["Allowance"] = (currRow["Type_Code"] == null ? "0" : (currRow["Type_Code"].ToString() == "HQ" ? hq : currRow["Type_Code"].ToString() == "OS" ? os : currRow["Type_Code"].ToString() == "EX" ? ex : currRow["Type_Code"].ToString() == "OS-EX" ? os : "0"));
                        string nextPlace = currRow["NextPlace"].ToString();
                        string currPlace = currRow["TerrPlaces"].ToString();
                        string prevPlace = currRow["PrevPlace"].ToString();
                        string prevsfcode = currRow["Prevsfcode"].ToString();
                        string nextsfcode = currRow["Nextsfcode"].ToString();
                        string currsfcode = currRow["Territory_Name1"].ToString();
                        String currCatType = "";
                        String prevCatType = "";
                        String nextCatType = "";
                        String currsfname = "";
                        currsfname = currRow["sf_name"].ToString();
                        if (currsfname.Contains(','))
                        {
                            currsfname = currRow["Territory_name1"].ToString();
                        }
                        if (currRow["TypMGR"].ToString() == "OS")
                        {
                            currRow["Type_Code"] = "OS";
                        }
                        if (currRow["Type_Code"] != null)
                        {
                            currCatType = currRow["Type_Code"].ToString();
                        }
                        if (prevRow["prevType"] != null)
                        {
                            prevCatType = currRow["prevType"].ToString();
                        }
                        if (nextRow["nextType"] != null)
                        {
                            nextCatType = currRow["nextType"].ToString();

                        }


                        if (currCatType == "HQ")
                        {

                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            double allow;
                            String filter = "sf_code='" + currsfname + "'";
                            DataRow[] ss1 = oscondnMGR.Select(filter);
                            if (ss1.Count() > 0)
                            {
                                currRow["Allowance"] = os;
                                currRow["Type_Code"] = "OS";
                            }
                            else
                            {
                                currRow["Allowance"] = hq;
                            }
                            allow = Convert.ToDouble(currRow["Allowance"].ToString());

                            currRow["Total"] = allow;

                            totalAllowance = totalAllowance + allow;
                            grandTotal = grandTotal + allow;


                        }
                        else if (currCatType == "EX")
                        {
                            int osdist = 0;
                            String filter1 = "sf_code='" + currsfname + "'";
                            DataRow[] ss1 = oscondnMGR.Select(filter1);
                            if (ss1.Count() > 0)
                            {
                                currRow["Allowance"] = os;
                                currRow["Type_Code"] = "OS";
                                osdist = Convert.ToInt32(ss1[0]["Dist"]);
                            }
                            else
                            {
                                currRow["Allowance"] = ex;
                            }
                            dsExDist = Exp.getExDistance(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                            DataTable exDistTab = dsExDist.Tables[0];

                            String filter = "adate='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] rows = exDistTab.Select(filter);
                            int d = 0;
                            foreach (DataRow r in rows)
                            {
                                d = Convert.ToInt32(r["dist"].ToString());
                                currRow["Distance"] = d + (osdist * 2);
                            }
                            double totFare = ((d + (osdist * 2)) * fare);
                            int inddist = d / 2;
                            if (range > 0)
                            {
                                totFare = rangeDistanceCalc(d, totFare, inddist);
                            }

                            currRow["Fare"] = totFare;
                            double allow;

                            allow = Convert.ToDouble(currRow["Allowance"].ToString());


                            double total = totFare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + (d + osdist * 2);
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;
                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (prevCatType.Equals("HQ") || prevCatType.Equals("EX") || prevCatType.Equals("NA") || prevCatType.Equals("") || prevCatType.Equals("AS-FE") || i == 0) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FE") || nextCatType.Equals("AS-FA")))
                        {
                            if ("EX".Equals(osSingleContn))
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = ex;
                                    currRow["Type_Code"] = "EX";
                                }

                            }
                            else
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = os;
                                    currRow["Type_Code"] = "OS";
                                }

                            }

                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["TerrPlaces"];
                            else
                            {
                                toPlace = currRow["TerrPlaces"].ToString();
                            }
                            toPlace = getDistinctPlaces(toPlace);
                            pList.Add(currRow["TerrPlaces"].ToString());

                            string p = getDisplayToPlaces(toPlace);
                            //string pp1 = getDisplayRemoveHQEXPlaces(toPlace);

                            string pp1 = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"] == "OS")
                            {
                                pp1 = getDisplayToPlaces(toPlace);
                            }
                            else
                            {
                                pp1 = getDisplayRemoveHQEXPlaces(toPlace);
                            }

                            double osDistance = 0;
                            double rngfare = 0;
                            getExpenseAlloscondition(Exp1, "S-END", pp1, out osDistance, out rngfare, "", "", "", currsfname);
                            currRow["Distance"] = osDistance;
                            currRow["Fare"] = rngfare;
                            double allow;
                            allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            double total = rngfare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + rngfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FA") || nextCatType.Equals("AS-FE")))
                        {
                            if ("EX".Equals(osContn))
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = ex;
                                    currRow["Type_Code"] = "EX";
                                }

                            }
                            else
                            {
                                if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                {
                                    currRow["Allowance"] = os;
                                    currRow["Type_Code"] = "OS";
                                }

                            }

                            double osDistance = 0;
                            double rngfare = 0;
                            ;

                            // osFlag = false;
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["TerrPlaces"];
                            else
                            {
                                toPlace = currRow["TerrPlaces"].ToString();
                            }
                            pList.Add(currRow["TerrPlaces"].ToString());

                            string places = getDistinctPlaces(toPlace);

                            string p = getDisplayToPlaces(places);
                            string pp1 = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"].ToString() == "OS")
                            {
                                pp1 = getDisplayToPlaces(places);
                            }
                            else
                            {
                                pp1 = getDisplayRemoveHQEXPlaces(places);
                            }
                            string pPlace = "";
                            string withsingle = "";
                            string pp = "";
                            if (prevPlace != "")
                            {
                                pPlace = getSingleOsDistinctPlacesQry(prevPlace);
                                String[] s = pPlace.Split(',');
                                withsingle = s[s.Length - 1];
                                pp = withsingle.Substring(1, withsingle.Length - 2);
                            }
                            string pSfcode = "";
                            String[] ps = prevsfcode.Split(',');
                            if (ps.Length > 0)
                            {
                                pSfcode = ps[ps.Length - 1];
                            }

                            string nSfcode = "";
                            String[] ns = nextsfcode.Split(',');
                            if (ns.Length > 0)
                            {
                                nSfcode = ns[0];
                            }
                            getExpenseAlloscondition(Exp1, "END", pp1, out osDistance, out rngfare, pp, pSfcode, nSfcode, currsfname);


                            currRow["Distance"] = osDistance;





                            currRow["Fare"] = rngfare;
                            double allow;
                            allow = Convert.ToDouble(currRow["Allowance"].ToString());

                            double total = rngfare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + rngfare;
                            grandTotal = grandTotal + total;

                            toPlace = "";



                            pList = new ArrayList();
                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("")))
                        {


                            double osDistance = 0;
                            double rngfare = 0;

                            toPlace = currRow["TerrPlaces"].ToString();
                            pList.Add(currRow["TerrPlaces"].ToString());
                            // currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            string places = getDistinctPlaces(toPlace);
                            string p = getDisplayToPlaces(places);
                            //string pp1 = getDisplayRemoveHQEXPlaces(places);
                            string pp1 = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"].ToString() == "OS")
                            {
                                pp1 = getDisplayToPlaces(places);
                            }
                            else
                            {
                                pp1 = getDisplayRemoveHQEXPlaces(places);
                            }
                            string nSfcode = "";
                            String[] ns = nextsfcode.Split(',');
                            if (ns.Length > 0)
                            {
                                nSfcode = ns[0];
                            }
                            string pPlace = "";
                            string withsingle = "";
                            string pp = "";
                            if (prevPlace != "")
                            {
                                pPlace = getSingleOsDistinctPlacesQry(prevPlace);


                                String[] s = pPlace.Split(',');
                                withsingle = s[s.Length - 1];
                                pp = withsingle.Substring(1, withsingle.Length - 2);
                            }
                            string pSfcode = "";
                            String[] ps = prevsfcode.Split(',');
                            if (ps.Length > 0)
                            {
                                pSfcode = ps[ps.Length - 1];
                            }

                            getExpenseAlloscondition(Exp1, "START", pp1, out osDistance, out rngfare, pp, pSfcode, nSfcode, currsfname);




                            currRow["Distance"] = osDistance;


                            currRow["Fare"] = rngfare;

                            double allow;

                            allow = Convert.ToDouble(currRow["Allowance"].ToString());

                            double total = rngfare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + rngfare;
                            grandTotal = grandTotal + total;


                            toPlace = "";


                            pList = new ArrayList();





                        }
                        //            else if (!(currRow["Worktype_Name_M"].Equals("Field Work")) && (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("")))
                        else if (!(currRow["Worktype_Name_M"].Equals("Field Work")) && !(currRow["Worktype_Name_M"].Equals("Weekly Off")) && !(currRow["Worktype_Name_M"].Equals("Holiday")))
                        {
                            currRow["Total"] = "0";

                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_M"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                currRow["catTemp"] = ss[0]["Allow_type"];
                            }
                        }
                        else
                        {
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            currRow["Total"] = allow;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_M"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                currRow["Type_Code"] = ss[0]["Allow_type"];
                            }
                            if (currRow["Allowance"].ToString().Equals("0"))
                            {

                                currRow["Allowance"] = "";
                                currRow["Distance"] = "0";
                            }
                            else
                            {

                                currRow["Distance"] = "0";
                            }



                            double totFare = fare;

                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + 0;

                            grandTotal = grandTotal + allow;

                            toPlace = "";
                        }
                        if (currRow["Activity_Date"].ToString() == null || currRow["Activity_Date"].ToString() == "")
                        {
                            currRow["Allowance"] = totalAllowance;
                            currRow["Distance"] = totalDistance;
                            currRow["Fare"] = totalFare;
                            currRow["Total"] = grandTotal;
                        }
                        if (currRow["Worktype_Name_M"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_M"].ToString().Equals("Holiday"))
                        {
                            currRow["Adate"] = "<span style='background-color:#FFE2D5'>" + currRow["Adate"].ToString() + "</span>";
                            currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
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
                    customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];

                }
                otherExpGrid.DataSource = customExpTable;
                otherExpGrid.DataBind();
            }

            // fixedExpense.InnerHtml = "180";
            Othtotal.Value = "0";
            double tot = otherExAmnt + grandTotal + otherExpAmnttot;
            grandTotalName.InnerHtml = tot.ToString();

            foreach (GridViewRow gridRow in grdExpMain.Rows)
            {
                Label lblCat = (Label)gridRow.FindControl("lblCat");
                Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                HiddenField allowTypeVal = (HiddenField)gridRow.FindControl("allowTypeHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden1");

                DropDownList allowType = ((DropDownList)gridRow.FindControl("AllowType"));
                Label allTypLbl = ((Label)gridRow.FindControl("lblCat"));
                TextBox txtAllow = ((TextBox)gridRow.FindControl("txtAllow"));
                Label allowLbl = ((Label)gridRow.FindControl("lblAllw"));
                TextBox txtFare = ((TextBox)gridRow.FindControl("txtFare"));
                Label fareLbl = ((Label)gridRow.FindControl("lblFare"));

                String filter = "Work_Type_Name='" + lblWorkType.Text + "' and Allow_type='" + allTypLbl.Text + "'";
                String filter1 = "Work_Type_Name='" + lblWorkType.Text + "'";
                string filter2 = "work_Type_Name='" + lblWorkType.Text + "'";
                Label Distance = (Label)gridRow.FindControl("lblDistance");
                int dis = Convert.ToInt32(Distance.Text);



                if (!"Field Work".Equals(lblWorkType.Text) || !"Weekly Off".Equals(lblWorkType.Text) || !"Holiday".Equals(lblWorkType.Text))
                {

                    if ("AS-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = true;
                        allowType.SelectedValue = allowTypeVal.Value;
                        allTypLbl.Visible = false;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;

                    }
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
        //pnlViewInbox.Style.Add("visibility", "hidden");
        //pnlViewMailInbox.Style.Add("visibility", "hidden");

    }
    private double rangeDistanceCalc(double d, double totFare, double indOs)
    {
        if (range > 0)
        {
            double onewayDistance = indOs;
            if (onewayDistance > range2 && range2 > 0)
            {
                if ("Consolidated".Equals(rangeType2))
                {
                    totFare = d * rangeFare2;
                }
                else
                {
                    //double f1 = ((onewayDistance - range2) * rangeFare2)*2;
                    // double f2 = (range2*2) * fare;
                    double f1 = ((onewayDistance - range2) * rangeFare2) * 2;
                    double f2 = ((range2 - range) * rangeFare) * 2;
                    double f3 = (range * 2) * fare;
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
                    double f1 = ((onewayDistance - range) * rangeFare) * 2;
                    double f2 = (range * 2) * fare;

                    totFare = f1 + f2;
                }
            }
            else
            {
                totFare = d * fare;
            }

        }
        return totFare;
    }
    private double rangeDistanceCalcOS(double d, double totFare, double indOs)
    {
        if (range > 0)
        {
            double onewayDistance = indOs;
            if (onewayDistance > range2 && range2 > 0)
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
                    double f3 = range * fare;
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
                    double f2 = range * fare;

                    totFare = f1 + f2;
                }
            }
            else
            {
                totFare = d * fare;
            }

        }
        return totFare;
    }
    private double rangeDistanceCalcOSEX(double d, double totFare, double indOs)
    {
        if (range > 0)
        {
            double onewayDistance = indOs / 2;
            if (onewayDistance > range2 && range2 > 0)
            {
                if ("Consolidated".Equals(rangeType2))
                {
                    totFare = d * rangeFare2;
                }
                else
                {
                    //double f1 = ((onewayDistance - range2) * rangeFare2)*2;
                    // double f2 = (range2*2) * fare;
                    double f1 = ((onewayDistance - range2) * rangeFare2) * 2;
                    double f2 = ((range2 - range) * rangeFare) * 2;
                    double f3 = (range * 2) * fare;
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
                    double f1 = ((onewayDistance - range) * rangeFare) * 2;
                    double f2 = (range * 2) * fare;

                    totFare = f1 + f2;
                }
            }
            else
            {
                totFare = d * fare;
            }

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
    private bool isRelation(Distance_calculation Exp, string currPlace, string nextPlace, string currCatType, string nextCatType, string fromCode, string currsfname)
    {

        return Exp.isRelationExist(getSingleOsDistinctPlacesQry(currPlace), getSingleOsDistinctPlacesQry(nextPlace), currCatType, nextCatType, currsfname, fromCode);
    }
    private bool isOSRelation(Distance_calculation Exp, string currPlace, string nextPlace, string currCatType, string nextCatType, string fromCode, string currsfname)
    {

        return Exp.isRelationosExist(getSingleOsDistinctPlacesQry(currPlace), getSingleOsDistinctPlacesQry(nextPlace), currCatType, nextCatType, currsfname, fromCode);
    }
    private bool isRelationPrev(Distance_calculation Exp, string currPlace, string nextPlace, string currCatType, string nextCatType, string fromCode, string currsfname)
    {

        return Exp.isRelationExistPrev(getSingleOsDistinctPlacesQry(currPlace), getSingleOsDistinctPlacesQry(nextPlace), currCatType, nextCatType, currsfname, fromCode);
    }
    private bool isOSRelationPrev(Distance_calculation Exp, string currPlace, string nextPlace, string currCatType, string nextCatType, string fromCode, string currsfname)
    {

        return Exp.isRelationosExistPrev(getSingleOsDistinctPlacesQry(currPlace), getSingleOsDistinctPlacesQry(nextPlace), currCatType, nextCatType, currsfname, fromCode);
    }
    private bool isOStoOSRelation(Distance_calculation Exp, string cursfname)
    {

        return Exp.isRelationostoosExist(cursfname);
    }
    private void getExpenseAlloscondition(Distance_calculation Exp, string callType, string places, out double osDistance, out double rngfare, string prevPlace, string prevSf, string nextSf, string cursfname)
    {
        rngfare = Exp.getOSDistanceBySP_bak(callType, places, prevPlace, cursfname, sfcode, prevSf, nextSf, out osDistance);
    }
    private double getOsExDistance(Distance_calculation Exp, string places, double osDistance, string osFrmCode, out double OSEXRangeDist, string cursfname)
    {
        OSEXRangeDist = 0;
        string osPlace = getOSPlace(places);
        foreach (string p in pList)
        {
            string qryPlaces = getDistinctPlacesQry(p);


            if (qryPlaces == "")
            {
                qryPlaces = "''";
            }
            DataSet dsOsExDis = Exp.getOsExDistance(cursfname, qryPlaces, osFrmCode);
            if (dsOsExDis.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsOsExDis.Tables[0].Rows)
                {
                    int dist = Convert.ToInt32(row["distance_in_kms"].ToString());
                    OSEXRangeDist = OSEXRangeDist + Convert.ToDouble(rangeDistanceCalc(dist, 0, dist));

                    osDistance = osDistance + Convert.ToInt32(row["distance_in_kms"].ToString());
                }
            }
        }
        return osDistance;
    }
    private double getOsExDistanceSameOSEX(Distance_calculation Exp, string places, double osDistance, string osFrmCode, out double OSEXRangeDist, string currsfname)
    {
        OSEXRangeDist = 0;
        string osPlace = getOSPlace(places);
        foreach (string p in pList)
        {
            string qryPlaces = getDistinctPlacesQry(p);

            if (qryPlaces == "")
            {
                qryPlaces = "''";
            }
            DataSet dsOsExDis = Exp.getOsExDistance(currsfname, qryPlaces, osFrmCode);
            if (dsOsExDis.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsOsExDis.Tables[0].Rows)
                {
                    int dist = Convert.ToInt32(row["distance_in_kms"].ToString());
                    OSEXRangeDist = OSEXRangeDist + Convert.ToDouble(rangeDistanceCalcOSEX(dist, 0, dist));

                    osDistance = Convert.ToInt32(row["distance_in_kms"].ToString());
                }
            }
        }
        return osDistance;
    }
    private double getOsExDistanceOS(Distance_calculation Exp, string places, double osDistance, string osFrmCode, out double OSEXRangeDist, string currsfname)
    {
        OSEXRangeDist = 0;
        string osPlace = getOSPlace(places);
        foreach (string p in pList)
        {
            string qryPlaces = getDistinctPlacesQry(p);

            if (qryPlaces == "")
            {
                qryPlaces = "''";
            }
            DataSet dsOsExDis = Exp.getOsExDistance(currsfname, qryPlaces, osFrmCode);
            if (dsOsExDis.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsOsExDis.Tables[0].Rows)
                {
                    int dist = Convert.ToInt32(row["distance_in_kms"].ToString());
                    OSEXRangeDist = OSEXRangeDist + Convert.ToDouble(rangeDistanceCalcOSEX(dist, 0, dist));

                    osDistance = osDistance + Convert.ToInt32(row["distance_in_kms"].ToString());
                }
            }
        }
        return osDistance;
    }
    private void getOsEXFrmCode(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {

        string osPlace = getOSEXPlace(places);

        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsEXFrmCode(currsfname, osPlace);
        osFrmCode = "";
        osDistance = 0;
        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["from_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }

    }
    private void getOsEXFrmCoderws(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {

        string osPlace = getOSEXPlace(places);

        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsEXFrmCoderws(currsfname, osPlace);
        osFrmCode = "";
        osDistance = 0;
        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["from_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }

    }

    private void getOsDistAndFrmCode(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {

        string osPlace = getOSPlace(places);
        if ("''" == osPlace)
        {
            getOsEXFrmCode(Exp, places, out osDistance, out osFrmCode, currsfname);
            return;
        }
        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsDistance(currsfname, osPlace);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }

    }

    private void getOsDistAndFrmCodeSameOSEX(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {

        string osPlace = getOSPlace(places);
        if ("''" == osPlace)
        {
            getOsEXFrmCode(Exp, places, out osDistance, out osFrmCode, currsfname);
            return;
        }
        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsDistance(sfcode, osPlace);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {

            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = 0;
        }

    }
    private void getOsDistAndFrmCodeNext(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {

        string osPlace = getOSPlace(places);
        if ("''" == osPlace)
        {
            getOsEXFrmCoderws(Exp, places, out osDistance, out osFrmCode, currsfname);
            return;
        }
        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsDistancenext(currsfname, osPlace);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }


    }
    private void getOsDistAndFrmCodeNextMultipeOS(Distance_calculation Exp, out double osDistance, string osFrmCode, string currsfname)
    {



        DataSet dsDis = null;

        dsDis = Exp.getOsDistancenextMultipleOS(currsfname, osFrmCode);
        osDistance = 0;


        if (dsDis.Tables[0].Rows.Count > 0)
        {

            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }


    }
    private void getSingleOsDistAndFrmCode(Distance_calculation Exp, string places, out double osDistance, out string osFrmCode, string currsfname)
    {
        DataSet dsDis = null;
        string qryPlaces = getSingleOsDistinctPlacesQry(places);
        dsDis = Exp.getSingleOsDistance(currsfname, qryPlaces);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
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


        Dictionary<String, String> values = new Dictionary<String, String>();
        values["sf_code"] = sfcode;
        values["div_code"] = divcode;
        values["month"] = monthId.SelectedValue.ToString();
        values["year"] = yearID.SelectedValue.ToString();
        values["period"] = "";
        if (flag)
        {
            values["flag"] = "1";
        }
        else
        {
            values["flag"] = "0";
        }


        int iReturn = -1;
        Distance_calculation_001 dist = new Distance_calculation_001();
        sNo = dist.getValidheadRecordNo(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
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
        if (Request.QueryString["type"] != null)
        {
            dist.addHeadRecordMgrResigned(values);
        }
        else
        {
            dist.addHeadRecordMgr(values);
        }
        //iReturn = dist.updateHeadFlgMgrLevel1(flag?ex.getTransFlag(sfcode).ToString():"0", sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, sessf_name,false);
        //values["sNo"] = sNo.ToString();

        Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();
        int count = 0;
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
            Label lblTerritoryName = (Label)gridRow.FindControl("lblTerrName");
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
            Label lblDistance = (Label)gridRow.FindControl("lblDistance");
            //TextBox txtRemarks = (TextBox)gridRow.FindControl("txtRemarks");
            Label lblFare = (Label)gridRow.FindControl("lblFare");
            HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
            Label lblTotal = (Label)gridRow.FindControl("lblTotal");
            HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
            HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
            //HiddenField remarksHidden = (HiddenField)gridRow.FindControl("remarksHidden");
            values["dayName"] = lblDayName.Text;
            values["adate"] = date;
            values["adate1"] = adateHidden.Value;
            values["dayName"] = lblDayName.Text;
            values["workType"] = lblWorkType.Text;
            values["terrName"] = lblTerritoryName.Text;
            values["cat"] = allowTypeHidden.Value;
            values["allowance"] = allowHidden.Value;
            values["distance"] = lblDistance.Text;
            values["remarks"] = "";
            values["fare"] = fareHidden.Value;
            values["total"] = totHidden.Value;
            values["from"] = "";
            values["to"] = "";
            values["catTemp"] = allowTypeHidden1.Value;
            if (date != "")
            {
                valueList[count] = values;
                count = count + 1;
                //iReturn = dist.addDetailRecord(values);
            }
        }

        if (Request.QueryString["type"] != null)
        {
            iReturn = dist.addAllDetailRecordMgrResigned(valueList);
        }
        else
        {
            iReturn = dist.addAllDetailRecordMgr(valueList);
        }
        dist.deleteFixed(values);

        foreach (GridViewRow gridRow in otherExpGrid.Rows)
        {

            HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
            Label amnt = (Label)gridRow.FindControl("lblSexpAmnt");
            values["Amt"] = amnt.Text;
            values["Expense_Parameter_Code"] = Expense_Parameter_Code.Value;

            iReturn = dist.addFixedRecord(values);

        }
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        dist.deleteOtheExp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        string[] splitVal = otherExpValues.Value.Split('~');
        string[] rms = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] exp = splitVal[2].Split(',');
        for (int p = 0; p < exp.Length; p++)
        {
            isEmpty = false;
            string[] e = exp[p].Split('=');
            iReturn = dist.addOthExpRecord(e[0], e[1], amount[p], rms[p], sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        }

        if (iReturn > 0)
        {

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
                        Response.Redirect("RptAutoExpense_Mgr.aspx?type=Vacant" + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
                    }
                    else if (iReturn == 6)
                    {
                        Response.Redirect("RptAutoExpense_Mgr.aspx?type=Vacant" + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
                    }


                }
                else
                {
                    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode);
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                if ("1".Equals(home))
                {
                    Response.Redirect("../../Default_MGR.aspx");
                }
                else
                {
                    Response.Redirect("RptAutoExpense_Mgr.aspx");
                }
            }

        }
    }

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
        DataTable t2 = dist.getSavedOtheExpRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
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