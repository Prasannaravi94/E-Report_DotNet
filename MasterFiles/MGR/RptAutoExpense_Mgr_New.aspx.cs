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
using System.Data.SqlClient;
using System.Globalization;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{
    int osexcnt = 0;
    int excnt = 0;
    bool isSavedPage = false;
    bool isEmpty = true;
    string sfcode = string.Empty;
    string strFileDateTime = string.Empty;
    string divcode = string.Empty;
    string home = string.Empty;
    DataSet expenseDataset = null;

    DataSet placeDataset = null;
    DataSet MgrAllwTyp = null;
    DataSet MgrAllwTypCA = null;
    double otherExpAmnttot = 0;
    string osFrmCode1 = string.Empty;
    int distrng;
    int contos = 0;
    int sNo = 0;
    int typ = 0;
    string frmToPls = string.Empty;

    ArrayList pList = new ArrayList();
    public Dictionary<String, String> colors = new Dictionary<string, string>();
    public ArrayList colList = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {

        sfcode = Convert.ToString(Session["Sf_Code"]);
        divcode = Convert.ToString(Session["div_code"]);
        if (Request.QueryString["home"] != null)
        {
            home = Request.QueryString["home"].ToString();
        }
        colList.Add("LightCoral");
        colList.Add("Green");
        colList.Add("Yellow");
        colList.Add("Orange");
        colList.Add("Aquamarine");
        colList.Add("DarkSalmon");
        colList.Add("MediumSlateBlue");
        colList.Add("OliveDrab");
        colList.Add("LightSeaGreen");
        colList.Add("MediumSeaGreen ");
        colList.Add("LightCoral");
        colList.Add("Green");
        colList.Add("Yellow");
        colList.Add("Orange");
        colList.Add("Aquamarine");
        colList.Add("DarkSalmon");
        colList.Add("MediumSlateBlue");
        colList.Add("OliveDrab");
        colList.Add("LightSeaGreen");
        colList.Add("MediumSeaGreen ");
        colList.Add("LightCoral");
        colList.Add("Green");
        colList.Add("Yellow");
        colList.Add("Orange");
        colList.Add("Aquamarine");
        colList.Add("DarkSalmon");
        colList.Add("MediumSlateBlue");
        colList.Add("OliveDrab");
        colList.Add("LightSeaGreen");
        colList.Add("MediumSeaGreen ");
        colList.Add("LightCoral");
        colList.Add("Green");
        colList.Add("Yellow");
        colList.Add("Orange");
        colList.Add("Aquamarine");
        colList.Add("DarkSalmon");
        colList.Add("MediumSlateBlue");
        colList.Add("OliveDrab");
        colList.Add("LightSeaGreen");
        colList.Add("MediumSeaGreen ");
        colList.Add("LightCoral");
        colList.Add("Green");
        colList.Add("Yellow");
        colList.Add("Orange");
        colList.Add("Aquamarine");
        colList.Add("DarkSalmon");
        colList.Add("MediumSlateBlue");
        colList.Add("OliveDrab");
        colList.Add("LightSeaGreen");
        colList.Add("MediumSeaGreen ");
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
    private string getAllTypes(string places)
    {
        string distinctPlaces = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            osexcnt = 0;
            excnt = 0;
            foreach (string p in distSet)
            {
                string p1 = "";
                if ((p.Length - p.LastIndexOf('(')) == 4)
                {
                    p1 = p.Substring(p.LastIndexOf('(') + 1, 2);
                }
                else
                    p1 = p.Substring(p.LastIndexOf('(') + 1, 5);
                {
                }
                if ("OS-EX" == p1)
                {
                    osexcnt++;
                }
                else if ("EX" == p1)
                {
                    excnt++;
                }
                p1 = p.Substring(0, p.IndexOf('('));

                if (distinctPlaces != "")
                {
                    if ((counter % 2) == 0)
                        distinctPlaces = distinctPlaces + ",'" + p1 + "'";
                    else
                        distinctPlaces = distinctPlaces + ",'" + p1 + "'";
                }
                else
                    distinctPlaces = "'" + p1 + "'";



                counter++;

            }
        }
        return distinctPlaces;
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
                if (p.Contains("(EX") || p.Contains("(OS") || p.Contains("(OS-EX"))
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

        Response.Redirect("RptAutoExpense_Mgr_New.aspx");
        mainDiv.Style.Remove("background-color");

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
            excesid.Visible = true;
            mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";

            Distance_calculation_001 Exp = new Distance_calculation_001();

            Distance_calculation Exp1 = new Distance_calculation();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            DataTable ds1 = Exp1.getFieldForce(divcode, sfcode);
            DataTable dsdv = Exp1.getdiv(divcode);
            DataTable owt = Exp.getotherWorkTypeMGR(divcode, ds.Rows[0]["Designation_Code"].ToString());
            //DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode);
            menuId.Visible = false;
            monthyearDiv.Visible = false;
            heading.Visible = true;
            twdid.Visible = true;
            mnthtxtId.InnerHtml = monthId.SelectedItem.ToString();
            yrtxtId.InnerHtml = yearID.SelectedValue.ToString();
            fieldforceId.InnerHtml = "Name :" + ds1.Rows[0]["sf_name"].ToString();

            empId.InnerHtml = "Emp.ID :" + ds1.Rows[0]["Employee_Id"].ToString();
            hqdesig.InnerHtml = "Designation :" + ds1.Rows[0]["sf_designation_short_name"].ToString();
            hqid.InnerHtml = "HQ :" + ds1.Rows[0]["sf_hq"].ToString();
            mgrName.InnerHtml = "Manager Name :" + ds1.Rows[0]["report_sf"].ToString();
            divid.InnerHtml = dsdv.Rows[0]["div_name"].ToString();
            double totalAllowance = 0;
            double totalDistance = 0;
            double totalAlwconvey = 0;
            double totalFare = 0;
            double grandTotal = 0;
            double Local_conveytot = 0;
            otherExpAmnttot = 0;


            DataTable frmPlaceDT = Exp.getFrmandTo(divcode, sfcode);
            DataTable dist = Exp.getDist(divcode, sfcode);


            ListItem[] toLists = new ListItem[frmPlaceDT.Rows.Count];

            if (frmPlaceDT.Rows.Count > 0)
            {
                int cnt = 0;
                for (int i = 0; i < frmPlaceDT.Rows.Count; i++)
                {
                    DataRow row = frmPlaceDT.Rows[i];
                    ListItem list = new ListItem();
                    list.Text = row["Territory_Name"].ToString();
                    list.Value = row["territory_code"].ToString() + "~" + row["Town_Cat"] + "~" + row["sf_code"] + "~" + row["Town_Cat1"];
                    toLists[i] = list;
                    if (!colors.ContainsKey(row["sf_code"].ToString()))
                    {
                        colors.Add(row["sf_code"].ToString(), colList[cnt].ToString());
                        cnt++;
                    }
                }
            }
            DataSet dsAllowance = Exp1.getAllow(sfcode);
            DataTable allowanceTab = dsAllowance.Tables[0];
            DataTable otherExpTable = Exp.getOthrExpMGR(divcode);

            String ex = "0";
            String os = "0";
            String hq = "0";
            String hill = "0";
            String ex11 = "0";
            String os1 = "0";
            String hq1 = "0";
            String oslmt = "0";
            String lclconvey = "0";

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
            if (allowanceTab != null)
            {
                foreach (DataRow row in allowanceTab.Rows)
                {
                    ex = row["ex_allowance"].ToString();
                    os = row["os_allowance"].ToString();
                    hq = row["hq_allowance"].ToString();
                    hill = row["os_allowance"].ToString();
                    oslmt = row["os_with_lmt"].ToString();
                    lclconvey = row["loal_convey"].ToString();
                    fare = Convert.ToDouble(row["fare"].ToString());
                    rangeFare = Convert.ToDouble(row["Range_of_Fare1"].ToString());
                    rangeFare2 = Convert.ToDouble(row["Range_of_Fare2"].ToString());
                    rangeFare3 = Convert.ToDouble(row["Range_of_Fare3"].ToString());
                    allowStr = ex + "@" + os + "@" + hq + "@" + fare + "@" + hill + "@" + oslmt + "@" + lclconvey;
                }
                allowString.Value = allowStr;
            }

            DataSet dsAllowance1 = null;

            if (ds.Rows[0]["sf_desgn"].ToString() == "N")
            {
                dsAllowance1 = Exp1.NonMetroMGR(divcode, ds.Rows[0]["Fieldforce_Type"].ToString(), ds.Rows[0]["Designation_Code"].ToString());
            }
            else
            {
                dsAllowance1 = Exp1.getAllowWorkTypeMetroMGR(divcode, ds.Rows[0]["Fieldforce_Type"].ToString(), ds.Rows[0]["Designation_Code"].ToString());

            }
            DataTable
                allowanceTab1 = dsAllowance1.Tables[0];
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
            String disValue = "";
            foreach (DataRow r in dist.Rows)
            {
                // disValue = disValue + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "$";
                double fare11 = rangeDistanceCalcOtherType(Convert.ToInt32(r["Distance"]), fare, Convert.ToInt32(r["Distance"]));
                disValue = disValue + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "@" + fare11 + "$";

            }
            distString.Value = disValue;





            string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
            if (Exp.headRecordExist(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3))
            {
                DataTable t1 = Exp.getSavedRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3);

                foreach (DataRow row in t1.Rows)
                {
                    totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
                    totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
                    totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
                    grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
                    String filter = "Work_Type_Name='" + row["Worktype_Name_M"].ToString() + "'";
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
                //generateOtherExpControls(Exp);
                if (!isEmpty)
                {
                    generateOtherExpControls(Exp1);
                }
                else
                {
                    generateEmptyOtherExpControls(Exp1);
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

                }

            }
            else
            {


                DataSet dsExDist = null;
                DataSet dsleavetyp = null;
                expenseDataset = Exp.getExpense_MGR_prevnextrelation(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                //placeDataset = Exp.getPlaceMGRNEW(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                //MgrAllwTyp = Exp.getMGRAllowTyp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                placeDataset = Exp.MGR_Placeofwork_CA(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                MgrAllwTyp = Exp.getMGRAllowTyp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                MgrAllwTypCA = Exp.getMGRAllowTyp_CA(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                dsExDist = Exp.getExDistance(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                DataTable excondnMGR = Exp1.EXCondnMGR(divcode, sfcode);
                DataTable exDistTab = dsExDist.Tables[0];
                dsleavetyp = Exp1.getleaveTyp(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
                DataTable leavtyp = dsleavetyp.Tables[0];
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
                DataTable tCA = MgrAllwTypCA.Tables[0];
                if (t1.Rows.Count <= 0)
                {
                    btnSave.Visible = false;
                }
                foreach (DataRow row in t1.Rows)
                {
                    String filter = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                    DataRow[] rows = t2.Select(filter);


                    if (!(row["Worktype_Name_M"].Equals("Field Work")))
                    {
                        string filter8 = "Work_Type_Name='" + row["Worktype_Name_M"].ToString() + "'";
                        DataRow[] ss = owt.Select(filter8);
                        if (ss.Count() > 0)
                        {
                            row["Type_Code"] = ss[0]["Work_Type_Code"];
                        }
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
                        string curr1 = r["Hill_station"].ToString();

                        if ((curr1 != "" || curr1 != null) && curr1 == "Y")
                        {
                            row["Hill_Station"] = "Y";
                            row["Hill_Fare"] = r["Fare"].ToString();
                        }
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
                    DataRow[] rows2 = tCA.Select(filter);
                    foreach (DataRow r2 in rows2)
                    {
                        string tp2 = r2["typcode"].ToString();
                        row["CATyp"] = r2["typcode"];


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
                t1.Columns.Add("temtyp");
                t1.Columns.Add("PrevPlace");
                t1.Columns.Add("NextPlace");
                t1.Columns.Add("Nextsfcode");
                t1.Columns.Add("Prevsfcode");
                t1.Columns.Add("PrevMGRType");
                t1.Columns.Add("NextMGRType");
                t1.Columns.Add("PrevWtype");
                t1.Columns.Add("NextWtype");
                t1.Columns.Add("Local_convey");
                t1.Columns.Add("Total_allowance");
                t1.Columns.Add("TypCA");
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
                        currRow["PrevType"] = prevRow["Type_Code"];
                        currRow["NextType"] = nextRow["Type_Code"];
                        currRow["PrevMGRType"] = prevRow["TypMGR"];
                        currRow["NextMGRType"] = nextRow["TypMGR"];
                        String prev = currRow["PrevType"].ToString();
                        String next = currRow["PrevType"].ToString();
                        currRow["PrevName"] = prevRow["sf_Name"];
                        currRow["NextName"] = nextRow["sf_Name"];
                        String prev1 = currRow["PrevName"].ToString();
                        String next1 = currRow["NextName"].ToString();
                        currRow["PrevPlace"] = prevRow["TerrPlaces"];
                        currRow["NextPlace"] = nextRow["TerrPlaces"];
                        currRow["PrevWtype"] = prevRow["Worktype_Name_M"];
                        currRow["NextWtype"] = nextRow["Worktype_Name_M"];
                        currRow["Prevsfcode"] = prevRow["Territory_Name1"];
                        currRow["Nextsfcode"] = nextRow["Territory_Name1"];

                        currRow["Allowance"] = (currRow["Type_Code"] == null ? "0" : (currRow["Type_Code"].ToString() == "HQ" ? hq : currRow["Type_Code"].ToString() == "OS" ? os : currRow["Type_Code"].ToString() == "EX" ? ex : currRow["Type_Code"].ToString() == "OS-EX" ? os : "0"));
                        if (currRow["Type_Code"].ToString() == "OS" || currRow["Type_Code"].ToString() == "OS-EX")
                        {
                            currRow["Local_convey"] = 0;
                        }
                        string nextPlace = currRow["NextPlace"].ToString();
                        string currPlace = currRow["TerrPlaces"].ToString();
                        string prevPlace = currRow["PrevPlace"].ToString();
                        string curwtype = currRow["Worktype_Name_M"].ToString();
                        string prevwtype = currRow["PrevWtype"].ToString();
                        string nextwtype = currRow["NextWtype"].ToString();
                        string prevsfcode = currRow["Prevsfcode"].ToString();
                        string nextsfcode = currRow["Nextsfcode"].ToString();
                        string currsfcode = currRow["Territory_Name1"].ToString();
                        string prevMGRTyp = currRow["PrevMGRType"].ToString();
                        string nextMGRTyp = currRow["NextMGRType"].ToString();
                        string currMGRTyp = currRow["TypMGR"].ToString();
                        String currCatType = "";
                        String prevCatType = "";
                        String nextCatType = "";
                        String currsfname = "";

                        if (currRow["Activity_Date"].ToString() != "")
                        {
                            String filterp = "Activity_Date='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] lp = leavtyp.Select(filterp);

                            foreach (DataRow r in lp)
                            {

                                currRow["Worktype_Name_M"] = r["WType_SName"].ToString();
                            }
                        }
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
                        currRow["temtyp"] = temptype;
                        if (currCatType == "HQ")
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            double allow;
                            String filter = "sf_code='" + currsfname + "'";
                            DataRow[] ss2 = excondnMGR.Select(filter);
                            DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode, currsfname);
                            if (ss2.Count() > 0)
                            {
                                currRow["Allowance"] = ex;
                                currRow["Type_Code"] = "EX";
                            }


                            else if (oscondnMGR.Rows.Count > 0)
                            {
                                currRow["Allowance"] = os;
                                currRow["Type_Code"] = "OS";
                            }

                            //else
                            //{
                            //    currRow["Allowance"] = hq;
                            //}

                            double Hill_fare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }
                            else
                            {
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            //currRow["Total"] = allow;

                            //totalAllowance = totalAllowance + allow;
                            //grandTotal = grandTotal + allow;
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["TerrPlaces"];
                            else
                            {
                                toPlace = currRow["TerrPlaces"].ToString();
                            }
                            pList.Add(currRow["TerrPlaces"].ToString());
                            toPlace = getDistinctPlaces(toPlace);
                            string pp1 = "";
                            pp1 = getDisplayToPlaces(toPlace);
                            double osDistance = 0;
                            double rngfare = 0;



                            //getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
                            getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);

                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;
                            currRow["Distance"] = osDistance;
                            double totfare = 0;
                            if (Hill_fare > 0)
                            {
                                currRow["Fare"] = osDistance * Hill_fare;
                                totfare = osDistance * Hill_fare;
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                totfare = rngfare;
                            }
                            currRow["Total_allowance"] = allow;

                            double total = totfare + allow;
                            currRow["Total"] = total;
                            totalAlwconvey = totalAlwconvey + allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";


                        }
                        else if (currCatType == "EX" && curwtype == "Field Work")
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            int osdist = 0;
                            DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode, currsfname);

                            if (oscondnMGR.Rows.Count > 0)
                            {
                                currRow["Allowance"] = os;
                                currRow["Type_Code"] = "OS";
                                //osdist = Convert.ToInt32(ss1[0]["Dist"]);
                            }
                            else
                            {
                                currRow["Allowance"] = ex;
                            }

                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["TerrPlaces"];
                            else
                            {
                                toPlace = currRow["TerrPlaces"].ToString();
                            }
                            pList.Add(currRow["TerrPlaces"].ToString());
                            toPlace = getDistinctPlaces(toPlace);
                            string pp1 = "";
                            pp1 = getDisplayToPlaces(toPlace);
                            double osDistance = 0;
                            double rngfare = 0;

                            double allow;
                            double Hill_fare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }
                            else
                            {
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }
                            getExpenseAllosconditionex(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);

                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;



                            currRow["Distance"] = osDistance;
                            double totfare = 0;
                            if (Hill_fare > 0)
                            {
                                currRow["Fare"] = osDistance * Hill_fare;
                                totfare = osDistance * Hill_fare;
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                totfare = rngfare;
                            }


                            double total = totfare + allow;
                            currRow["Total"] = total;
                            currRow["Total_allowance"] = allow;
                            totalAlwconvey = totalAlwconvey + allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";
                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (prevCatType.Equals("HQ") || prevCatType.Equals("EX") || prevCatType.Equals("NA") || prevCatType.Equals("") || prevCatType.Equals("AS-FA") || prevCatType.Equals("FA") || prevCatType.Equals("FD") || prevCatType.Equals("FF") || prevCatType.Equals("AS-FE") || prevCatType.Equals("AE-FE") || prevCatType.Equals("--Select--") || i == 0) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FE") || nextCatType.Equals("AS-FA") || nextCatType.Equals("AE-FE") || nextCatType.Equals("FA") || nextCatType.Equals("FD") || nextCatType.Equals("FF") || nextCatType.Equals("--Select--")))
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
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

                            //string p = getDisplayToPlaces(toPlace);
                            //string pp1 = getDisplayRemoveHQEXPlaces(toPlace);
                            string types = "";
                            string pp1 = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"].ToString() == "OS")
                            {
                                pp1 = getDisplayToPlaces(toPlace);
                                types = getAllTypes(toPlace);
                            }
                            else
                            {
                                pp1 = getDisplayToPlaces(toPlace);
                                types = getAllTypes(toPlace);
                            }
                            double osDistance = 0;
                            double rngfare = 0;
                            double allow;
                            double Hill_fare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }
                            else
                            {
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }

                            // getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                            if (currRow["Territory_Name"].ToString().Contains("OS-EX"))
                            {
                                getExpenseAllosconditionosexstartend(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "OS-EX");
                            }
                            else
                            {
                                getExpenseAllosconditionex(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                            }
                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;

                            currRow["Distance"] = osDistance;

                            double totfare = 0;
                            if (Hill_fare > 0)
                            {
                                currRow["Fare"] = osDistance * Hill_fare;
                                totfare = osDistance * Hill_fare;
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                totfare = rngfare;
                            }
                            double total = 0;
                            if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                total = totfare + allow;
                            else
                                total = totfare + allow;
                            currRow["Total_allowance"] = allow;
                            currRow["Total"] = total;
                            totalAlwconvey = totalAlwconvey + allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FA") || nextCatType.Equals("FA") || nextCatType.Equals("AS-FE") || nextCatType.Equals("AE-FE") || nextCatType.Equals("--Select--")))
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
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
                            bool isPvPkg = false;

                            string osFrmCode;

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
                            string types = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"].ToString() == "OS")
                            {
                                pp1 = getDisplayToPlaces(places);
                                types = getAllTypes(toPlace);
                            }
                            else
                            {
                                pp1 = getDisplayRemoveHQEXPlaces(places);
                                types = getAllTypes(toPlace);
                            }
                            double OSEXRangeDist = 0;
                            double individualDis = 0;
                            bool OStoOS = true;

                            string withsingle = "";
                            string pPlace = getSingleOsDistinctPlacesQry(prevPlace);
                            String[] s = pPlace.Split(',');
                            withsingle = s[s.Length - 1];
                            string pp = withsingle.Substring(1, withsingle.Length - 2);

                            string pSfcode = "";
                            string nPlace = "";
                            string np = "";

                            String[] ps = prevsfcode.Split(',');
                            if (ps.Length > 0)
                            {
                                pSfcode = ps[ps.Length - 1];
                            }
                            if (nextPlace != "")
                            {
                                nPlace = getSingleOsDistinctPlacesQry(nextPlace);


                                String[] s1 = nPlace.Split(',');
                                withsingle = s1[0];
                                np = withsingle.Substring(1, withsingle.Length - 2);
                            }
                            string nSfcode = "";
                            String[] ns = nextsfcode.Split(',');
                            if (ns.Length > 0)
                            {
                                nSfcode = ns[0];
                            }
                            double allow;
                            double Hill_fare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }
                            else
                            {
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }


                            if (currRow["Territory_Name"].ToString().Contains("OS-EX"))
                            {
                                getExpenseAllosconditionosexend(Exp1, "", pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "OS-EX");

                            }
                            else if (currRow["Territory_Name"].ToString().Contains("EX"))
                            {
                                getExpenseAllosconditionexMiddle(Exp1, "END", pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "EX");

                            }
                            else if (typ == 1)
                            {
                                getExpenseAlloscondition(Exp1, "S-END", pp1, pp, "", pSfcode, "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);

                                if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && typ == 1)
                                {
                                    if ("EX".Equals(osSingleContn))
                                    {
                                        if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                        {
                                            currRow["Allowance"] = ex;
                                            currRow["Type_Code"] = "EX";
                                        }

                                    }
                                    // typ123 = 0;
                                }

                                //if (currRow["Hill_Station"].ToString() == "Y")
                                //{
                                //    currRow["Allowance"] = hill;
                                //    allow = Convert.ToDouble(hill.ToString());
                                //    Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                                //}
                                //else
                                //{
                                //    currRow["Allowance"] = ex;
                                //    currRow["Type_Code"] = "EX";
                                //    allow = Convert.ToDouble(currRow["Allowance"].ToString());
                                //}

                                //getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
                            }
                            else
                            {
                                getExpenseAlloscondition(Exp1, "END", pp1, pp, "", pSfcode, "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                            }

                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;


                            //}

                            if (currCatType.Equals("OS-EX"))
                            {
                                //individualDis = osDistance - individualDis;
                            }

                            tDistance = tDistance + osDistance;

                            currRow["Distance"] = tDistance;



                            double totFare = 0;


                            if (Hill_fare > 0)
                            {
                                totFare = tDistance * Hill_fare;
                            }
                            else
                            {
                                totFare = rngfare;
                            }


                            currRow["Fare"] = totFare;


                            // double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            //double total = totFare + allow;

                            double total = 0;
                            if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                total = totFare + allow;
                            else
                                total = totFare + allow;
                            currRow["Total"] = total;
                            currRow["Total_allowance"] = allow;
                            totalAlwconvey = totalAlwconvey + allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;

                            toPlace = "";
                            // osCalculated = false;
                            tDistance = 0;

                            pList = new ArrayList();
                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("")))
                        {

                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            double osDistance = 0;
                            double rngfare = 0;

                            toPlace = currRow["TerrPlaces"].ToString();
                            pList.Add(currRow["TerrPlaces"].ToString());
                            // currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            string places = getDistinctPlaces(toPlace);
                            string p = getDisplayToPlaces(places);
                            //string pp1 = getDisplayRemoveHQEXPlaces(places);
                            string pp1 = "";
                            string types = "";
                            if (currsfname.Contains(',') || currRow["TypMGR"].ToString() == "OS")
                            {
                                pp1 = getDisplayToPlaces(places);
                                types = getAllTypes(toPlace);
                            }
                            else
                            {
                                pp1 = getDisplayRemoveHQEXPlaces(places);
                                types = getAllTypes(toPlace);
                            }
                            string nSfcode = "";
                            string pSfcode = "";
                            string pPlace = "";
                            string nPlace = "";
                            string withsingle = "";
                            string pp = "";
                            string np = "";
                            if (prevPlace != "" && i != 0)
                            {
                                if (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX"))
                                {
                                    pPlace = getSingleOsDistinctPlacesQry(prevPlace);


                                    String[] s = pPlace.Split(',');
                                    withsingle = s[s.Length - 1];
                                    pp = withsingle.Substring(1, withsingle.Length - 2);
                                    String[] ps = prevsfcode.Split(',');
                                    if (ps.Length > 0)
                                    {
                                        pSfcode = ps[ps.Length - 1];
                                    }
                                }
                            }


                            if (nextPlace != "")
                            {
                                nPlace = getSingleOsDistinctPlacesQry(nextPlace);


                                String[] s = nPlace.Split(',');
                                withsingle = s[0];
                                np = withsingle.Substring(1, withsingle.Length - 2);
                                String[] ns = nextsfcode.Split(',');
                                if (ns.Length > 0)
                                {
                                    nSfcode = ns[0];
                                }
                            }
                            double allow;

                            double Hill_fare = 0;
                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }


                            string con = "CON";
                            //if (typ == 1 || prevCatType == "EX")
                                if (typ == 1 || (prevCatType != "OS" && prevCatType != "OS-EX"))
                                {
                                con = "START";
                            }



                            if (currRow["Territory_Name"].ToString().Contains("OS-EX"))
                            {
                                getExpenseAllosconditionosex(Exp1, "", pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "OS-EX");

                            }
                            else if (currRow["Territory_Name"].ToString().Contains("EX"))
                            {
                                if (pSfcode != "" && pSfcode == currsfname)
                                    getExpenseAllosconditionexMiddle(Exp1, "CON", pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "EX");
                                else
                                    getExpenseAllosconditionexMiddle(Exp1, con, pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, "EX");

                            }
                            else if (prevPlace != "" && prevCatType != "HQ")
                            {
                                if (typ == 1)
                                {
                                    con = "START1";
                                    getExpenseAlloscondition(Exp1, con, pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                                }
                                else
                                {
                                    getExpenseAlloscondition(Exp1, con, pp1, pp, np, pSfcode, nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                                }
                                if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA") && currsfname != nSfcode)

                                {
                                    if ("EX".Equals(osSingleContn))
                                    {
                                        if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                        {
                                            currRow["Allowance"] = ex;
                                            currRow["Type_Code"] = "EX";
                                        }

                                    }


                                }
                            }
                            else
                            {
                                getExpenseAlloscondition(Exp1, "START", pp1, pp, np, pSfcode, nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls, currCatType);
                                if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA") && currsfname != nSfcode)
                                //if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA"))
                                {
                                    if ("EX".Equals(osSingleContn))
                                    {
                                        if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                        {
                                            currRow["Allowance"] = ex;
                                            currRow["Type_Code"] = "EX";
                                        }

                                    }


                                }
                            }

                            if (frmTovalues.Value == "")
                                frmTovalues.Value = currRow["Activity_Date"].ToString() + frmToPls;
                            else
                                frmTovalues.Value = frmTovalues.Value + "#" + currRow["Activity_Date"].ToString() + frmToPls;



                            if (currRow["Hill_Station"].ToString() == "Y")
                            {
                                currRow["Allowance"] = hill;
                                allow = Convert.ToDouble(hill.ToString());
                                Hill_fare = Convert.ToDouble(currRow["Hill_Fare"].ToString());
                            }
                            else
                            {
                                allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            }


                            tDistance = tDistance + osDistance;
                            currRow["Distance"] = tDistance;

                            double totFare = 0;
                            if (Hill_fare > 0)
                            {
                                currRow["Fare"] = tDistance * Hill_fare;
                                totFare = tDistance * Hill_fare;
                            }
                            else
                            {
                                currRow["Fare"] = rngfare;
                                totFare = rngfare;
                            }




                            //double total = totFare + allow;
                            double total = 0;
                            if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                total = totFare + allow;
                            else
                                total = totFare + allow;
                            currRow["Total_allowance"] = allow;
                            currRow["Total"] = total;
                            totalAlwconvey = totalAlwconvey + allow;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;


                            toPlace = "";
                            tDistance = 0;

                            pList = new ArrayList();





                        }

                        else if (!(currRow["Worktype_Name_M"].Equals("Field Work")) && !(currRow["Worktype_Name_M"].Equals("Weekly Off")) && !(currRow["Worktype_Name_M"].Equals("Holiday")))
                        {
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            currRow["Total"] = allow;

                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_M"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                if (intcnt == 1)
                                {
                                    currRow["temtyp"] = temptype;
                                    currRow["type_code"] = temptype;
                                }
                                else
                                {
                                    currRow["catTemp"] = ss[0]["Allow_type"];
                                }
                            }
                            double totFare = fare;

                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + 0;

                            grandTotal = grandTotal + allow;
                        }
                        else
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
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
                            currRow["Total_allowance"] = totalAlwconvey;
                            currRow["Local_convey"] = Local_conveytot;
                        }
                        if (currRow["Worktype_Name_M"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_M"].ToString().Equals("Holiday"))
                        {

                            currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
                        }

                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine("Exception Type: {0}", ex1.GetType());
                        Console.WriteLine("  Message: {0}", ex1.Message);
                    }

                }


                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                //generateOtherExpListData(otherExpTable);
                generateEmptyOtherExpControls(Exp1);

            }

            DataTable customExpTable = new DataTable();
            customExpTable.Columns.Add("Expense_Parameter_Name");
            customExpTable.Columns.Add("amount");
            customExpTable.Columns.Add("Expense_Parameter_Code");

            // DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
            customExpTable.Columns.Add("pro_Rate");

            DataTable expParamsAmnt = Exp.sp_Get_Fixed_expense_MGR(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
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
                    if (expParamsAmnt.Rows[i]["MGR_Lines"].ToString() == "1")
                    {
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
                }
                otherExpGrid.DataSource = customExpTable;
                otherExpGrid.DataBind();
            }

            // fixedExpense.InnerHtml = "180";
            Othtotal.Value = "0";
            //double tot = otherExAmnt + grandTotal + otherExpAmnttot;
            //grandTotalName.InnerHtml = tot.ToString();
            double tot = 0;

            DataTable headR1 = Exp1.getHeadRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, dt3);
            if (headR1.Rows.Count > 0)
            {
                excesfare.Text = headR1.Rows[0]["Excess_fare"].ToString();
                excesremks.Text = headR1.Rows[0]["exce_fare_rmks"].ToString();
                excesallw.Text = headR1.Rows[0]["Excess_allw"].ToString();
                excesallwrmks.Text = headR1.Rows[0]["exces_allw_rmks"].ToString();
                double lblfaretot1 = Convert.ToDouble(headR1.Rows[0]["Excess_fare"].ToString()) + totalFare;
                double lblallwtot1 = Convert.ToDouble(headR1.Rows[0]["Excess_allw"].ToString()) + totalAllowance;
                double lblmisc1 = Convert.ToDouble(headR1.Rows[0]["Misc_tot"].ToString());
                double lbladddele1 = Convert.ToDouble(headR1.Rows[0]["Add_deduct_tot"].ToString());
                //lbllop.Text = headR1.Rows[0]["lop"].ToString();
                //totexp1.Text = headR1.Rows[0]["tot_exp"].ToString();

                tot = lblfaretot1 + lblallwtot1 + lbladddele1 + lblmisc1;
                grandTotalName.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                iddNtTot.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblallwtot.Text = lblallwtot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblfaretot.Text = lblfaretot1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lbladddele.Text = lbladddele1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                fartot.Value = headR1.Rows[0]["Excess_fare"].ToString();
                allwtot.Value = headR1.Rows[0]["Excess_allw"].ToString();
                lblmisc.Text = lblmisc1.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                totexp1.Text = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));

            }
            else
            {
                tot = otherExAmnt + grandTotal + otherExpAmnttot;
                grandTotalName.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                iddNtTot.InnerHtml = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblallwtot.Text = totalAllowance.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblfaretot.Text = totalFare.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                lblmisc.Text = otherExAmnt.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
                totexp1.Text = tot.ToString("0,0.00", CultureInfo.CreateSpecificCulture("hi-IN"));
            }

            foreach (GridViewRow gridRow in grdExpMain.Rows)
            {
                Label lblCat = (Label)gridRow.FindControl("lblCat");
                Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                HiddenField allowTypeVal = (HiddenField)gridRow.FindControl("allowTypeHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden1");
                HiddenField allowTypeHidden2 = (HiddenField)gridRow.FindControl("allowTypeHidden2");
                DropDownList allowType = ((DropDownList)gridRow.FindControl("AllowType"));
                DropDownList toBox = ((DropDownList)gridRow.FindControl("toPlace"));
                Label allTypLbl = ((Label)gridRow.FindControl("lblCat"));
                TextBox txtAllow = ((TextBox)gridRow.FindControl("txtAllow"));
                Label allowLbl = ((Label)gridRow.FindControl("lblAllw"));
                TextBox txtFare = ((TextBox)gridRow.FindControl("txtFare"));
                Label fareLbl = ((Label)gridRow.FindControl("lblFare"));
                HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
                Label lblTerrName = (Label)gridRow.FindControl("lblTerrName");
                TextBox txtterr = ((TextBox)gridRow.FindControl("txtterr"));
                HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));
                String filter = "Work_Type_Name='" + lblWorkType.Text + "' and Allow_type='" + allTypLbl.Text + "'";
                String filter1 = "Work_Type_Name='" + lblWorkType.Text + "'";
                string filter2 = "work_Type_Name='" + lblWorkType.Text + "'";
                HiddenField hotelTypeHidden = (HiddenField)gridRow.FindControl("hotelTypeHidden");
                DropDownList hotelType = ((DropDownList)gridRow.FindControl("hotelType"));
                hotelType.SelectedValue = hotelTypeHidden.Value;
                if (allowTypeVal.Value == "OS")
                {
                    hotelType.Style.Add("display", "block");
                }
                if (lblWorkType.Text == "")
                {
                    //hotelType.Visible = false;
                    hotelType.Style.Add("display", "none");
                    allowLbl.Style.Add("display", "block");
                    txtAllow.Style.Add("display", "none");
                }
                if ("Field Work".Equals(lblWorkType.Text) && (("OS".Equals(allTypLbl.Text)) || ("OS-EX".Equals(allTypLbl.Text)) || ("EX".Equals(allTypLbl.Text))))
                {
                    dtlBtn.Style.Add("visibility", "visible");

                }
                if ("Field Work".Equals(lblWorkType.Text) && ("".Equals(allTypLbl.Text) || "0".Equals(allTypLbl.Text) || "--Select--".Equals(allTypLbl.Text)))
                {
                    allTypLbl.Text = "AS-FE";
                    allowTypeHidden.Value = "AS-FE";
                    allowType.SelectedValue = allowTypeVal.Value;
                    allTypLbl.Visible = false;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                }
                if (!"Field Work".Equals(lblWorkType.Text) || !"Weekly Off".Equals(lblWorkType.Text) || !"Holiday".Equals(lblWorkType.Text) || !"Leave".Equals(lblWorkType.Text))
                {

                    if ("AA-NF".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
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
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AE-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = true;
                        allowLbl.Visible = false;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AA-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AA-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
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
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("FF".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("FM".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("FD".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AS-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = true;
                        allowType.SelectedValue = allowTypeVal.Value;
                        allTypLbl.Visible = false;
                        //txtAllow.Visible = true;
                        //allowLbl.Visible = true;
                        txtAllow.Style.Add("display", "block");
                        allowLbl.Style.Add("display", "none");
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        lblTerrName.Visible = false;
                        txtterr.Visible = true;
                        // changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AE-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = true;
                        allowLbl.Visible = false;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                }

            }



        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
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

    //private void generateOtherExpListData(DataTable otherExpTable)
    //{
    //    if (otherExpTable.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in otherExpTable.Rows)
    //        {
    //            ListItem list = new ListItem();
    //            list.Text = row["Expense_Parameter_Name"].ToString();
    //            list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount1"].ToString();
    //            otherExp.Items.Add(list);
    //        }

    //    }
    //}

    private static void changeFromToCtrl(ListItem[] toLists, DropDownList toBox, string to, Dictionary<String, String> colors)
    {

        ListItem[] newtoLists = new ListItem[toLists.Count()];

        for (int i = 0; i < toLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = toLists[i].Text;
            l.Value = toLists[i].Value;
            String[] s = toLists[i].Value.Split('~');
            l.Attributes.Add("style", "background-color:" + colors[s[2]]);
            newtoLists[i] = l;
        }

        toBox.Visible = true;
        toBox.Items.AddRange(newtoLists);
        if (to != null && to != "")
        {
            toBox.ClearSelection();
            toBox.Items.FindByText(to).Selected = true;
        }
    }


    //private void getExpenseAlloscondition(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls)
    //{
    //    rngfare = Exp.getOSDistanceBySP_bak_onlineMGR_FMS(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare);
    //}

    private void getExpenseAlloscondition(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {
        // rngfare = Exp.getOSDistanceBySP_bak_onlineMGR_FMS_OSEX(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
        rngfare = Exp.getOSDistanceBySP_bak_onlineMGR_FMS(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare);
    }


    private void getExpenseAllosconditionosex(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_latestosex(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
    }
    //osexstartend
    private void getExpenseAllosconditionosexstartend(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_latestosex_startend(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
    }
    //Midelosex
    private void getExpenseAllosconditionex(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_latest(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
    }

    private void getExpenseAllosconditionosexstart(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_latestosexstart(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
    }


    private void getExpenseAllosconditionosexend(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_latestosexEnd(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
    }

    private void getExpenseAllosconditionexMiddle(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls, string currtyp)
    {

        rngfare = Exp.ExpenseAllos_condition_MGR_OSEX_MiddleEX(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare, currtyp);
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
        //Expense ex = new Expense();
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
        HiddenField frmTovalues = (HiddenField)FindControl("frmTovalues");
        values["frmTovalues"] = frmTovalues.Value;
        if (flag)
        {
            values["flag"] = "1";
        }
        else
        {
            values["flag"] = "0";
        }
        values["txtexcessfare"] = excesfare.Text;
        values["txtexcessrmks"] = excesremks.Text;
        values["txtexcessallw"] = excesallw.Text;
        values["excesallwrmks"] = excesallwrmks.Text;
        values["lblallwtot"] = allwtot.Value;
        values["lblfaretot"] = fartot.Value;
        values["lbladddele"] = adddeducttot.Value;
        values["lblmisc"] = lblmisc.Text;
        Distance_calculation dist1 = new Distance_calculation();
        DataTable table = dist1.getMgrAppr(divcode);

        if (table.Rows.Count > 0)
        {
            if ("1".Equals(table.Rows[0]["MgrAppr_Remark"].ToString()))
            {
                values["flag"] = "7";

            }
        }

        int iReturn = -1;
        Distance_calculation_001 dist = new Distance_calculation_001();
        DataTable ds1 = dist.getFieldForce(divcode, sfcode);
        string soj = ds1.Rows[0]["sf_joining_date"].ToString();
        values["soj"] = soj;
        sNo = dist.getValidheadRecordNo(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode, soj);
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
        if (ds1.Rows[0]["reporting_to_sf"].ToString() == "admin")
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
                    "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,Status_flg,Employee_Id,sf_emp_id,frmTovalues,sf_joining_date,file_path,Excess_fare,Excess_allw,Far_tot,allw_tot,Add_deduct_tot,Misc_tot,exces_allw_rmks,exce_fare_rmks)" +
                    "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                    values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",0,'" + values["EmpNo"] + "','" + values["SfEmpNo"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["File_Path"] + "','" + values["txtexcessfare"] + "','" + values["txtexcessallw"] + "','" + values["lblfaretot"] + "','" + values["lblallwtot"] + "','" + values["lbladddele"] + "','" + values["lblmisc"] + "','" + values["excesallwrmks"] + "','" + values["txtexcessrmks"] + "')";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "INSERT INTO Trans_Expense_Head " +
                    "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,Status_flg,Employee_Id,sf_emp_id,frmTovalues,sf_joining_date,file_path,Excess_fare,Excess_allw,Far_tot,allw_tot,Add_deduct_tot,Misc_tot,exces_allw_rmks,exce_fare_rmks)" +
                    "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                    values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",0,'" + values["EmpNo"] + "','" + values["SfEmpNo"] + "','" + values["frmTovalues"] + "','" + values["soj"] + "','" + values["File_Path"] + "','" + values["txtexcessfare"] + "','" + values["txtexcessallw"] + "','" + values["lblfaretot"] + "','" + values["lblallwtot"] + "','" + values["lbladddele"] + "','" + values["lblmisc"] + "','" + values["excesallwrmks"] + "','" + values["txtexcessrmks"] + "')";
                    command.ExecuteNonQuery();
                }


                if (Request.QueryString["type"] != null)
                {
                    //dist.updateHeadFlgResigned(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);

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
                    Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
                    string date = lbl_aDate.Text;
                    Label lblDayName = (Label)gridRow.FindControl("lblDayName");
                    Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                    Label lbllocal = (Label)gridRow.FindControl("lbllocalconvey");
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
                    DropDownList hotelType = (DropDownList)gridRow.FindControl("hotelType");
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
                    values["distance"] = distHidden.Value;
                    values["remarks"] = "";
                    values["fare"] = fareHidden.Value;
                    values["total"] = totHidden.Value;
                    values["from"] = "";
                    values["to"] = toHidden.Value;
                    values["catTemp"] = allowTypeHidden1.Value;
                    values["billtype"] = hotelType.SelectedValue;
                    values["local"] = lbllocal.Text;
                    if (date != "")
                    {
                        command.CommandText = "INSERT INTO Trans_Expense_Detail " +
                                  "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,Mgr_allow,Mgr_amount,Mgr_Remarks,Mgr_total,exp_remarks,Bill_Type,local_convey)" +
                                  "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                  values["adate"].Replace("'", "\"") + "','" + values["dayName"].Replace("'", "\"") + "','" + values["workType"] + "','" + values["adate1"] + "','" + values["terrName"] + "','" + values["cat"]
                                  + "','" + values["allowance"] + "','" + values["distance"] + "','" + values["fare"] + "','" + values["catTemp"] + "','" + values["total"] + "','" + values["div_code"] + "','" + values["from"] + "','" + values["to"] + "','" + values["allowance"] + "','" + values["fare"] + "','" + values["remarks"] + "','" + values["total"] + "','" + values["remarks"] + "','" + values["billtype"] + "','" + values["local"] + "')";
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





                string[] pert = splitVal[0].Split(',');
                string[] amount = splitVal[1].Split(',');
                string[] op = splitVal[2].Split(',');
                string[] date1 = splitVal[4].Split(',');
                string[] misExpVal = splitVal[5].Split(',');


                //for (int p = 0; p < op.Length; p++)
                //{
                //    string[] e = op[p].Split('=');
                //    string[] dateVal = date[p].Split('=');
                //    string[] misExp = misExpVal[p].Split('=');
                //    iReturn = Exp.addAdminAdjustmentExpRecord(dateVal[0], misExp[0], misExp[1], e[0], pert[p] == "" ? "0" : pert[p], amount[p] == "" ? "0" : amount[p], splitVal[3], sfcode, monthId, yearID, dt3);
                //    "(sl_No,Paritulars,typ,Amt,grand_total,sf_code,Expense_Month,Expense_year,adminAdjDate,Expense_Parameter_Name,Expense_Parameter_Code)" +
                //        expValue + "'," + type + "," + amnt + "," + gt + ",'" + sf_code + "'," + month + "," + year + "," + date + ",'" + expName + "','" + expCode + "')";
                //}

                //string[] pert = splitVal[0].Split(',');
                //string[] amount = splitVal[1].Split(',');
                //string[] date1 = splitVal[2].Split(',');
                //string[] misExpVal = splitVal[3].Split(',');
                for (int p = 0; p < misExpVal.Length; p++)
                {
                    isEmpty = false;
                    string[] e = op[p].Split('=');
                    string[] dateVal = date1[p].Split('=');
                    string[] misExp = misExpVal[p].Split('=');
                    if (pert[p] == "")
                    {
                        pert[p] = "0";
                    }
                    if (amount[p] == "")
                    {
                        amount[p] = "0";
                    }
                    if (!misExp[1].Contains("Select"))
                    {
                        command.CommandText = "INSERT INTO Exp_others " +
                                           "(sl_No,expval,Paritulars,Amt,Remarks,mrAdjDate,typ,typ_txt,Expense_Parameter_Code,Expense_Parameter_Name)" +
                                           "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                           misExp[0] + "','" + misExp[1] + "'," + amount[p] + ",'" + pert[p] + "','" + dateVal[0] + "','" + e[0] + "','" + e[1] + "','" + misExp[0] + "','" + misExp[1] + "')";
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
                            // Response.Redirect("RptAutoExpense_Mgr.aspx?type=" + Request.QueryString["type"] + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Mgr_New.aspx?type=" + Request.QueryString["type"] + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode + "'</script>");
                        }
                        else if (iReturn == 6)
                        {
                            // Response.Redirect("RptAutoExpense_Mgr.aspx?type=" + Request.QueryString["type"] + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Mgr_New.aspx?type=" + Request.QueryString["type"] + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode + "'</script>");
                        }


                    }
                    else
                    {
                        //if (Request.QueryString["type"] == "Promote")
                        //{
                        //    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Designation_Name=Promote");
                        //}
                        //else if (Request.QueryString["type"] == "DePromote")
                        //{
                        //    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode + "&Reporting_To_Manager=DePromote");
                        //}
                        //else
                        //{
                        //    Response.Redirect("~/MasterFiles/Salesforce.aspx?sf_hq=Vacant" + "&sfcode=" + sfcode);
                        //}

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

                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                    if ("1".Equals(home))
                    {
                        // Response.Redirect("../../Default_MGR.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='../../Default_MGR.aspx'</script>");
                    }
                    else
                    {
                        //Response.Redirect("RptAutoExpense_Mgr.aspx");
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Mgr_New.aspx'</script>");
                    }
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
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Expired.Kindly Resubmit Again');window.location='RptAutoExpense_Mgr_New.aspx'</script>");
                }


            }
        }
    }

    //dist.addHeadRecordMgr(values);
    //    if (Request.QueryString["type"] != null)
    //    {
    //        //dist.updateHeadFlgResigned(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);

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
    //        Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
    //        string date = lbl_aDate.Text;
    //        Label lblDayName = (Label)gridRow.FindControl("lblDayName");
    //        Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
    //        Label lblTerritoryName = (Label)gridRow.FindControl("lblTerrName");
    //        Label lblCat = (Label)gridRow.FindControl("lblCat");
    //        HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
    //        HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
    //        HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
    //        Label lblAllw = (Label)gridRow.FindControl("lblAllw");
    //        HiddenField allowHidden = (HiddenField)gridRow.FindControl("allowHidden");
    //        Label lblFrom = (Label)gridRow.FindControl("lblFrom");
    //        HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
    //        Label lblTo = (Label)gridRow.FindControl("lblTo");
    //        HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
    //        Label lblDistance = (Label)gridRow.FindControl("lblDistance");
    //        //TextBox txtRemarks = (TextBox)gridRow.FindControl("txtRemarks");
    //        Label lblFare = (Label)gridRow.FindControl("lblFare");
    //        HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
    //        Label lblTotal = (Label)gridRow.FindControl("lblTotal");
    //        HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
    //        HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
    //        //HiddenField remarksHidden = (HiddenField)gridRow.FindControl("remarksHidden");
    //        values["dayName"] = lblDayName.Text;
    //        values["adate"] = date;
    //        values["soj"] = soj;
    //        values["adate1"] = adateHidden.Value;
    //        values["dayName"] = lblDayName.Text;
    //        values["workType"] = lblWorkType.Text;
    //        values["terrName"] = lblTerritoryName.Text;
    //        values["cat"] = allowTypeHidden.Value;
    //        values["allowance"] = allowHidden.Value;
    //        values["distance"] = distHidden.Value;
    //        values["remarks"] = "";
    //        values["fare"] = fareHidden.Value;
    //        values["total"] = totHidden.Value;
    //        values["from"] = "";
    //        values["to"] = toHidden.Value;
    //        values["catTemp"] = allowTypeHidden1.Value;
    //        if (date != "")
    //        {
    //            valueList[count] = values;
    //            count = count + 1;
    //            //iReturn = dist.addDetailRecord(values);
    //        }
    //    }


    //    iReturn = dist.addAllDetailRecordMgr(valueList);

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
    //        iReturn = dist.addOthExpRecord(e[0], e[1], amount[p], rms[p], sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), soj);
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
    //                    Response.Redirect("RptAutoExpense_Mgr.aspx?type=" + Request.QueryString["type"] + "&mon=" + last_month + "&year=" + last_month_year + "&sf_code=" + sfcode);
    //                }
    //                else if (iReturn == 6)
    //                {
    //                    Response.Redirect("RptAutoExpense_Mgr.aspx?type=" + Request.QueryString["type"] + "&mon=" + curr_month + "&year=" + curr_year + "&sf_code=" + sfcode);
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

    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
    //            if ("1".Equals(home))
    //            {
    //                Response.Redirect("../../Default_MGR.aspx");
    //            }
    //            else
    //            {
    //                Response.Redirect("RptAutoExpense_Mgr.aspx");
    //            }
    //        }

    //    }
    //}

    private void generateOtherExpControls(Distance_calculation dist)
    {



        // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable t2 = dist.getMRAdjustExp(sfcode, monthId.SelectedValue, yearID.SelectedValue);

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
            int currentMonth = Convert.ToInt32(monthId.SelectedValue);
            int year = Convert.ToInt32(yearID.SelectedValue);
            if (currentMonth == 8)
            {
                dateRange = 31;
            }
            else if ((currentMonth % 2) == 0)
            {
                dateRange = 31;
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
            d1.SelectedValue = t2.Rows[i]["mrAdjDate"].ToString();
            HtmlTableCell cell11 = new HtmlTableCell();
            cell11.Controls.Add(d1);
            DataTable otherExp1 = dist.getOthrExpNotLOP(divcode);
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
            d.Attributes.Add("onchange", "adminAdjustCalc(this,0)");
            //d.Attributes.AddAttributes("onchange", "adminAdjustCalc(this,0)");

            d.Items.Insert(0, new ListItem(" --Select-- ", "2"));
            d.Items.Insert(1, new ListItem("+", "1"));
            d.Items.Insert(2, new ListItem("-", "0"));
            string amnt = t2.Rows[i]["amt"].ToString();
            string rm = t2.Rows[i]["Remarks"].ToString();
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
            lit.Text = @"<input type='text' value='" + rm + "' name='tP' size='50' maxlength='50' class='textbox'  onkeydown='if(event.shiftKey==true && event.keyCode==9){this.parentNode.previousSibling.focus();return false}' style='width:90%;height:19px'/>";
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
            lit.Text = @"<input type='text' value='" + amnt + "' name='tAmt' maxlength='50' onkeypress='_fNvALIDeNTRY(D,7)' onkeyup='adminAdjustCalc(this,0);' class='textbox' style='width:90%;height:19px;text-align:right' />";
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
        hidtamtval.Value = totAmnt + "";

    }
    private void generateEmptyOtherExpControls(Distance_calculation dist)
    {
        // DataTable t2 = dist.getSavedOtheExpRecord(monthId, yearID, sfcode);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable t2 = dist.getMRAdjustExp(sfcode, monthId.SelectedValue, yearID.SelectedValue);

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
        int currentMonth = Convert.ToInt32(monthId.SelectedValue);
        int year = Convert.ToInt32(yearID.SelectedValue);
        if (currentMonth == 8)
        {
            dateRange = 31;
        }
        else if ((currentMonth % 2) == 0)
        {
            dateRange = 31;
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
        DataTable otherExp1 = dist.getOthrExpNotLOP(divcode);
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
            if (!isEmpty)
                generateOtherExpControls(dist);

        }
    }


}