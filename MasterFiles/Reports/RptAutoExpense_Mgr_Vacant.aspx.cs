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
    int osexcnt = 0;
    int excnt = 0;
    bool isSavedPage = false;
    bool isEmpty = true;
    string sfcode = string.Empty;
    string strFileDateTime = string.Empty;
    string divcode = string.Empty;
    string mon = string.Empty;
    string yr = string.Empty;
    string s4 = string.Empty;
    string s5 = string.Empty;
    string home = string.Empty;
    DataSet expenseDataset = null;
    DataSet placeDataset = null;
    DataSet MgrAllwTyp = null;
    double otherExpAmnttot = 0;
    string osFrmCode1 = string.Empty;
    int distrng;
    int contos = 0;
    int sNo = 0;
    int typ = 0;
    String ex11 = "0";
    String os1 = "0";
    String hq1 = "0";
    string wtyp = "";
    string frmToPls = string.Empty;
    Hashtable months = new Hashtable();
    ArrayList pList = new ArrayList();
    public Dictionary<String, String> colors = new Dictionary<string, string>();
    public ArrayList colList = new ArrayList();
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
        divcode = Convert.ToString(Session["div_code"]);
        mon = Request.QueryString["mon"].ToString();
        yr = Request.QueryString["year"].ToString();
        sfcode = Request.QueryString["sf_code"].ToString();
        string[] sf = sfcode.Split('$');
        sfcode = sf[0];
        s4 = sf[1];
        s5 = sf[2];

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

        grdExp();
      
       
      
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

   
    double fare = 0;
    double rangeFare2 = 0;
    int range2 = 0;
    String rangeType2 = "Consolidated";
    double rangeFare = 0;
    int range = 0;
    String rangeType = "Consolidated";

    protected void grdExp()
    {

        mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";
            Distance_calculation_001 Exp = new Distance_calculation_001();
            Distance_calculation Exp1 = new Distance_calculation();
            DataTable ds = Exp1.getFieldForce_Vacant(divcode, sfcode, s4, s5);
            DataTable owt = Exp.getotherWorkTypeMGR(divcode, ds.Rows[0]["Designation_Code"].ToString());
            //DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode);
            heading.Visible = true;
            twdid.Visible = true;
            mnthtxtId.InnerHtml = months[mon.ToString()].ToString();
            yrtxtId.InnerHtml = yr;
            fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
            doj.InnerHtml = "DOJ :" + ds.Rows[0]["doj"].ToString();
            empId.InnerHtml = "Employee Code :" + ds.Rows[0]["Employee_Id"].ToString();
            DivId.InnerHtml = "Division Name :" + ds.Rows[0]["Division_name"].ToString();
            StsId.InnerHtml = "Status :" + ds.Rows[0]["Fieldforce_Type1"].ToString();
            Confirmdt.InnerHtml = "Confirmed date :" + ds.Rows[0]["sf_date_confirm"].ToString();
            double totalAllowance = 0;
            double totalDistance = 0;
            double totalFare = 0;
            double grandTotal = 0;
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

            DataSet dsAllowance1 = null;
             string fieldtype = "";
            if (ds.Rows[0]["Fieldforce_Type"].ToString() == null || ds.Rows[0]["Fieldforce_Type"].ToString() == "")
            {
                fieldtype = "3";
            }
            else 
            {
                fieldtype = ds.Rows[0]["Fieldforce_Type"].ToString();
            }
            if (ds.Rows[0]["sf_desgn"].ToString() == "N")
            {
                dsAllowance1 = Exp1.NonMetroMGR(divcode, fieldtype, ds.Rows[0]["Designation_Code"].ToString());
            }
            else
            {
                dsAllowance1 = Exp1.getAllowWorkTypeMetroMGR(divcode, fieldtype, ds.Rows[0]["Designation_Code"].ToString());

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

            String disValue = "";
            foreach (DataRow r in dist.Rows)
            {
                // disValue = disValue + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "$";
                double fare11 = rangeDistanceCalcOtherType(Convert.ToInt32(r["Distance"]), fare, Convert.ToInt32(r["Distance"]));
                disValue = disValue + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "@" + fare11 + "$";

            }
            distString.Value = disValue;

            Distance_calculation dsCa = new Distance_calculation();
            DataTable headDetailsDt = dsCa.getFieldForce_Vacant_slno(sfcode, mon, yr, s4, s5);
            DataSet dsFileName = new DataSet();
            //dsFileName = dsCa.getFileNamePath(sfcode, mon, yr);
            if (headDetailsDt.Rows.Count > 0)
            {
                dsFileName = dsCa.getFileNamePath_Vacant(sfcode, mon, yr, headDetailsDt.Rows[0]["sl_no"].ToString());
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
            }
            else
            {
            dvPage.Visible = true;
            divAttach.Visible = true;
            }


            string dt3 = ds.Rows[0]["sf_joining_date"].ToString();
              if (headDetailsDt.Rows.Count > 0)
            {
                DataTable t1 = Exp1.getSavedRecord_Vacant(mon, yr, sfcode, headDetailsDt.Rows[0]["sl_no"].ToString());

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
                generateOtherExpControls(Exp1);
                DataTable headR = Exp1.getHeadRecord_vacant(mon, yr, sfcode, headDetailsDt.Rows[0]["sl_no"].ToString());
                if (headR.Rows[0]["Status"].ToString() == "1" || headR.Rows[0]["Status"].ToString() == "7" || headR.Rows[0]["Status"].ToString() == "8")
                {
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    messageId.Visible = true;
                    messageId.Text = "Expense yet to proccess";
                    if (dsFileName.Tables[0].Rows[0][0].ToString() != "")
                    {
                        divLinkattach.Visible = true;
                        dvPage.Visible = false;
                        divAttach.Visible = false;
                        aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                        string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                        lblViewAttach.Text = "Bills Download Here";
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
                        dvPage.Visible = false;
                        divAttach.Visible = false;
                        aTagAttach.HRef = "~/" + dsFileName.Tables[0].Rows[0]["File_Path"].ToString();
                        string[] str = dsFileName.Tables[0].Rows[0]["File_Path"].ToString().Split('/');
                        lblViewAttach.Text = "Bills Download Here";
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
                expenseDataset = Exp.getExpense_MGR_prevnextrelation_vacant(sfcode, mon, yr,s4,s5);
                if (expenseDataset.Tables[0].Rows.Count == 0)
                {
                    btnSave.Visible = false;
                    bakfldfrce.Visible = false;
                }
                placeDataset = Exp.getPlaceMGRNEW(divcode,sfcode, mon, yr);
                MgrAllwTyp = Exp.getMGRAllowTyp(sfcode, mon, yr);
                dsExDist = Exp.getExDistance(divcode, sfcode, mon, yr);
                DataTable excondnMGR = Exp1.EXCondnMGR(divcode, sfcode);
                DataTable exDistTab = dsExDist.Tables[0];
                dsleavetyp = Exp1.getleaveTyp(mon, yr, sfcode);
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
                    if ("EX".Equals(tt.Rows[0]["Last_Os_Wrkconsider"].ToString()))
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
                    String filter = "dcr_date1='" + row["ADate"].ToString() + "'";
                   // String filter = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                    DataRow[] rows = t2.Select(filter);



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

                    string curterr = row["TerrPlaces"].ToString();
                    if (!(row["Worktype_Name_M"].Equals("Field Work")) && (curterr == "" || curterr==null))
                   // if (!(row["Worktype_Name_M"].Equals("Field Work")))
                    {
                        string filter8 = "Work_Type_Name='" + row["Worktype_Name_M"].ToString() + "'";
                        DataRow[] ss = owt.Select(filter8);
                        if (ss.Count() > 0)
                        {
                            row["Type_Code"] = ss[0]["Work_Type_Code"];
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
                t1.Columns.Add("temtyp");
                t1.Columns.Add("PrevPlace");
                t1.Columns.Add("NextPlace");
                t1.Columns.Add("Nextsfcode");
                t1.Columns.Add("Prevsfcode");
                t1.Columns.Add("PrevMGRType");
                t1.Columns.Add("NextMGRType");
                t1.Columns.Add("PrevWtype");
                t1.Columns.Add("NextWtype");
                t1.Rows.Add();
                string toPlace = "";
                double tDistance = 0;
                for (int i = 0; i < t1.Rows.Count; i++)
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
                        //currRow["PrevName"] = prevRow["sf_Name"];
                        //currRow["NextName"] = nextRow["sf_Name"];
                        currRow["PrevName"] = prevRow["territory_code2"];
                        currRow["NextName"] = nextRow["territory_code2"];
                        String prev1 = currRow["PrevName"].ToString();
                        String next1 = currRow["NextName"].ToString();
                        currRow["PrevPlace"] = prevRow["TerrPlaces"];
                        currRow["NextPlace"] = nextRow["TerrPlaces"];
                        currRow["PrevWtype"] = prevRow["Worktype_Name_M"];
                        currRow["NextWtype"] = nextRow["Worktype_Name_M"];
                        currRow["Prevsfcode"] = prevRow["Territory_Name3"];
                        currRow["Nextsfcode"] = nextRow["Territory_Name3"];
                        currRow["Allowance"] = (currRow["Type_Code"] == null ? "0" : (currRow["Type_Code"].ToString() == "HQ" ? hq : currRow["Type_Code"].ToString() == "OS" ? os : currRow["Type_Code"].ToString() == "EX" ? ex : currRow["Type_Code"].ToString() == "OS-EX" ? os : "0"));
                        string nextPlace = currRow["NextPlace"].ToString();
                        string currPlace = currRow["TerrPlaces"].ToString();
                        string prevPlace = currRow["PrevPlace"].ToString();
                        string curwtype = currRow["Worktype_Name_M"].ToString();
                        string prevwtype = currRow["PrevWtype"].ToString();
                        string nextwtype = currRow["NextWtype"].ToString();
                        string prevsfcode = currRow["Prevsfcode"].ToString();
                        string nextsfcode = currRow["Nextsfcode"].ToString();
                        string currsfcode = currRow["Territory_Name3"].ToString();
                        string currsfname1 = currRow["Territory_Name2"].ToString();
                        string prevMGRTyp = currRow["PrevMGRType"].ToString();
                        string nextMGRTyp = currRow["NextMGRType"].ToString();
                        string currMGRTyp = currRow["TypMGR"].ToString();
                        String currCatType = "";
                        String prevCatType = "";
                        String nextCatType = "";
                        String currsfname = "";

                        if (curwtype != "" && curwtype != "Field Work" && currsfname1 != "")
                        {
                            String filterp1 = "Work_Type_Name='" + currRow["Worktype_Name_M"].ToString() + "'";
                            DataRow[] othallw = allowanceTab1.Select(filterp1);
                            if (othallw.Count() > 0)
                            {
                                foreach (DataRow r1 in othallw)
                                {

                                    ex = r1["Fixed_amount1"].ToString();
                                    os = r1["Fixed_amount2"].ToString();
                                    hq = r1["Fixed_amount"].ToString();
                                }
                            }
                            else
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
                            }
                        }
                        else
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
                        }

                        if (currRow["Activity_Date"].ToString() != "")
                        {
                            String filterp = "Activity_Date='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] lp = leavtyp.Select(filterp);

                            foreach (DataRow r in lp)
                            {

                                currRow["Worktype_Name_M"] = r["WType_SName"].ToString();
                            }
                        }
                        currsfname = currRow["territory_code2"].ToString();
                        if (currsfname.Contains(','))
                        {
                            currsfname = currRow["Territory_Name3"].ToString();
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
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            double allow;
                            String filter = "sf_code='" + currsfname + "'";
                            currRow["Allowance"] = hq;
                            currRow["Type_Code"] = "HQ";
                            DataRow[] ss2 = excondnMGR.Select(filter);
                            if (ss2.Count() > 0)
                            {
                                currRow["Allowance"] = ex;
                                currRow["Type_Code"] = "EX";
                            }
                            DataTable oscondnMGR = Exp1.OSCondnMGR(divcode, sfcode, currsfname);

                            if (oscondnMGR.Rows.Count > 0)
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


                            
                            getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);

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
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";

                        }
                        else if (currCatType == "EX" && currRow["Territory_Name2"].ToString() != "")
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
                            
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
                            getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);

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
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";
                        }

                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (prevCatType.Equals("HQ") || prevCatType.Equals("EX") || prevCatType.Equals("NA") || prevCatType.Equals("") || prevCatType.Equals("AS-FA") || prevCatType.Equals("FA") || prevCatType.Equals("FD") || prevCatType.Equals("FF") || prevCatType.Equals("AS-FE") || prevCatType.Equals("--Select--") || i == 0) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FE") || nextCatType.Equals("AS-FA") || nextCatType.Equals("FA") || nextCatType.Equals("FD") || nextCatType.Equals("FF") || nextCatType.Equals("--Select--")))
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
                            if (currRow["Worktype_Name_M"].Equals("Field Work"))
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
                                        currRow["Allowance"] = ex;
                                        currRow["Type_Code"] = "EX";
                                    }

                                }
                            }
                            else 
                            {
                                currRow["Allowance"] = os;
                                currRow["Type_Code"] = "OS";
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

                            getExpenseAlloscondition(Exp1, "S-END", pp1, "", "", "", "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);

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
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totfare;
                            grandTotal = grandTotal + total;
                            pList = new ArrayList();
                            toPlace = "";
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA") || nextCatType.Equals("") || nextCatType.Equals("AS-FA") || nextCatType.Equals("FA") || nextCatType.Equals("AS-FE") || nextCatType.Equals("--Select--")))
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
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
                                //pp1 = getDisplayRemoveHQEXPlaces(places);
                                pp1 = getDisplayToPlaces(places);
                                types = getAllTypes(toPlace);
                            }
                            double OSEXRangeDist = 0;
                            double individualDis = 0;
                            bool OStoOS = true;
                            // OStoOS = isOStoOSRelation(Exp1,currsfname);
                            //if (OStoOS)
                            // {
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

                            if (typ == 1)
                            {
                                getExpenseAlloscondition(Exp1, "S-END", pp1, pp, "", pSfcode, "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);

                                if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && typ == 1)
                                {
                                    currRow["Allowance"] = ex;
                                    currRow["Type_Code"] = "EX";
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
                                getExpenseAlloscondition(Exp1, "END", pp1, pp, "", pSfcode, "", currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
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
                            double total = totFare + allow;
                            currRow["Total"] = total;
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
                            currRow["Allowance"] = os;
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
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
                                // pp1 = getDisplayRemoveHQEXPlaces(places);
                                pp1 = getDisplayToPlaces(places);
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
                            if (typ == 1 || prevCatType == "EX")
                            {
                                con = "START";
                            }
                            if (prevPlace != "" && prevCatType != "HQ")
                            {
                                if (typ == 1)
                                {
                                    con = "START1";
                                    getExpenseAlloscondition(Exp1, con, pp1, "", np, "", nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
                                }
                                else
                                {
                                    getExpenseAlloscondition(Exp1, con, pp1, pp, np, pSfcode, nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
                                }
                                if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA") && currsfname != nSfcode)
                                {
                                    if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                    {
                                        currRow["Allowance"] = ex;
                                        currRow["Type_Code"] = "EX";

                                    }


                                }
                            }
                            else
                            {
                                getExpenseAlloscondition(Exp1, "START", pp1, pp, np, pSfcode, nSfcode, currsfname, Hill_fare, out osDistance, out rngfare, out typ, out frmToPls);
                                if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA") && currsfname != nSfcode)
                                //if (typ == 1 && (prevCatType == "HQ" || prevCatType == "EX" || prevCatType == "" || prevCatType == "NA"))
                                {
                                    if (currCatType.Equals("OS") || currCatType.Equals("OS-EX"))
                                    {
                                        currRow["Allowance"] = ex;
                                        currRow["Type_Code"] = "EX";
                                        // typ123 = 0;
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




                            double total = totFare + allow;
                            currRow["Total"] = total;
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
                            currRow["Total"] = "0";

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
                        }
                        else
                        {
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["territory_name2"].ToString());
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

                            currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
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

            // DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
            customExpTable.Columns.Add("pro_Rate");

            DataTable expParamsAmnt = Exp.sp_Get_Fixed_expense(divcode, sfcode, mon, yr);
            DataTable expDCRjoining = Exp.sp_DCRjoining(divcode, sfcode, dt3, mon, yr);
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
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["pro_Rate"] = expParamsAmnt.Rows[i][prt] == "" ? "0" : expParamsAmnt.Rows[i][prt];
                    }
                    else
                    {

                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];
                        customExpTable.Rows[customExpTable.Rows.Count - 1]["pro_Rate"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];
                    }
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
                HtmlButton dtlBtn = ((HtmlButton)gridRow.FindControl("dtlBtn"));
                String filter = "Work_Type_Name='" + lblWorkType.Text + "' and Allow_type='" + allTypLbl.Text + "'";
                String filter1 = "Work_Type_Name='" + lblWorkType.Text + "'";
                string filter2 = "work_Type_Name='" + lblWorkType.Text + "'";


                if (!"".Equals(lblTerrName.Text) && (("OS".Equals(allTypLbl.Text)) || ("OS-EX".Equals(allTypLbl.Text)) || ("EX".Equals(allTypLbl.Text))))
                {
                    dtlBtn.Style.Add("visibility", "visible");

                }
                if ("Field Work".Equals(lblWorkType.Text) && ("".Equals(allTypLbl.Text) || "0".Equals(allTypLbl.Text) || "--Select--".Equals(allTypLbl.Text)))
                {
                    allTypLbl.Text = "FD";
                    allowTypeHidden.Value = "FD";
                }
                if (!"Field Work".Equals(lblWorkType.Text) || !"Weekly Off".Equals(lblWorkType.Text) || !"Holiday".Equals(lblWorkType.Text) || !"Leave".Equals(lblWorkType.Text))
                {

                    if ("AA-NF".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("NA-FA".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
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
                    else if ("AE-FE".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = true;
                        allowLbl.Visible = false;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AA-FE".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {

                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AA-FA".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = false;
                        allTypLbl.Visible = true;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = false;
                        fareLbl.Visible = true;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AS-FA".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
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
                    else if ("FA".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
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
                    else if ("AS-FE".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        allowType.Visible = true;
                        allowType.SelectedValue = allowTypeVal.Value;
                        allTypLbl.Visible = false;
                        txtAllow.Visible = false;
                        allowLbl.Visible = true;
                        txtFare.Visible = true;
                        fareLbl.Visible = false;
                        changeFromToCtrl(toLists, toBox, toHidden.Value, colors);
                    }
                    else if ("AE-FA".Equals(allowTypeHidden2.Value, StringComparison.OrdinalIgnoreCase))
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
    private double rangeDistanceCalcOtherType(int d, double totFare, int indOs)
    {
        if (range > 0)
        {
            int onewayDistance = indOs;
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
                list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount1"].ToString();
                otherExp.Items.Add(list);
            }

        }
    }

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

    private void getExpenseAlloscondition(Distance_calculation Exp, string callType, string places, string prevPlace, string nextplace, string prevSf, string nextSf, string cursfname, double Hillfare, out double osDistance, out double rngfare, out int typs, out string frmToPls)
    {
        rngfare = Exp.getOSDistanceBySP_bak_onlineMGR_FMS(callType, places, prevPlace, nextplace, cursfname, sfcode, prevSf, nextSf, out osDistance, out typs, out frmToPls, divcode, Hillfare);
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
    //private void getOsDistAndFrmCodeNextMultipeOS(Distance_calculation Exp, out double osDistance, string osFrmCode, string currsfname)
    //{



    //    DataSet dsDis = null;

    //    dsDis = Exp.getOsDistancenextMultipleOS(currsfname, osFrmCode);
    //    osDistance = 0;


    //    if (dsDis.Tables[0].Rows.Count > 0)
    //    {

    //        osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
    //    }


    //}
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
    protected void btnField_Click(object sender, EventArgs e)
    {

        Distance_calculation dsCa = new Distance_calculation();

        DataTable headDetailsDt = dsCa.getFieldForce_Vacant_slno(sfcode, mon, yr, s4, s5);
        if (headDetailsDt.Rows.Count > 0)
        {
            dsCa.backtoentry(sfcode, mon, yr, headDetailsDt.Rows[0]["sl_no"].ToString());
        }
        string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                window.parent.opener.location.reload();
                                </script>";
        base.Response.Write(close);

        // Response.Redirect("RptAutoExpense_Approve.aspx");
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
        values["month"] = mon;
        values["year"] = yr;
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


        int iReturn = -1;
        string slno = "";
        Distance_calculation dist = new Distance_calculation();
        DataTable ds1 = dist.getFieldForce_Vacant(divcode, sfcode, s4, s5);
        string soj = ds1.Rows[0]["Sf_Joining_Date"].ToString();
        DataTable sfvacant = dist.getFieldForce_Vacant_month(sfcode, mon, yr, s5);
        if (sfvacant.Rows.Count > 0)
        {
            DataTable sfvacslno = dist.getFieldForce_Vacant_slno(sfcode, mon, yr, s4, s5);
            if (sfvacslno.Rows.Count > 0)
            {
                slno = sfvacslno.Rows[0]["sl_no"].ToString();
            }
        }

        values["soj"] = soj;
        // dist.addHeadRecord(values);
        values["sl_no"] = slno;

        if (sfvacant.Rows.Count > 0)
        {
            dist.addHeadRecord_Vacant_Last(values);
        }
        else
        {
            dist.addHeadRecord_Vacant(values);
        }
       
       


        Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();
        int count = 0;
        foreach (GridViewRow gridRow in grdExpMain.Rows)
        {

            values = new Dictionary<String, String>();
            values["sf_code"] = sfcode;
            values["div_code"] = divcode;
            values["month"] = mon;
            values["year"] = yr;
            Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
            string date = lbl_aDate.Text;
            Label lblDayName = (Label)gridRow.FindControl("lblDayName");
            Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
            Label lblTerritoryName = (Label)gridRow.FindControl("lblTerrName");
            Label lblCat = (Label)gridRow.FindControl("lblCat");
            HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
            HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
            HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
            HiddenField allowTypeHidden2 = (HiddenField)gridRow.FindControl("allowTypeHidden2");
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
            values["soj"] = soj;
            values["sl_no"] = slno;
            values["adate1"] = adateHidden.Value;
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
            values["temtyp"] = allowTypeHidden2.Value;
            values["rw_amount"] = "";
            values["rw_rmks"] = "";
            if (date != "")
            {
                valueList[count] = values;
                count = count + 1;
                //iReturn = dist.addDetailRecord(values);
            }
        }


        //iReturn = dist.addAllDetailRecordMgr(valueList);
        if (sfvacant.Rows.Count > 0)
        {
            iReturn = dist.addAllDetailRecord_vacant_Last(valueList);
        }
        else
        {
            iReturn = dist.addAllDetailRecord_vacant(valueList);
        }

        //dist.deleteFixed(values);

        foreach (GridViewRow gridRow in otherExpGrid.Rows)
        {

            HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
            Label amnt = (Label)gridRow.FindControl("lblProAmnt");
            values["Amt"] = amnt.Text;
            values["Expense_Parameter_Code"] = Expense_Parameter_Code.Value;

            //iReturn = dist.addFixedRecord(values);
            if (sfvacant.Rows.Count > 0)
            {
                iReturn = dist.addFixedRecord_vacant_Last(values);
            }
            else
            {
                iReturn = dist.addFixedRecord_vacant(values);
            }

        }
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
       // dist.deleteOtheExp(sfcode, mon, yr, soj);
        string[] splitVal = otherExpValues.Value.Split('~');
        string[] rms = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] exp = splitVal[2].Split(',');
        for (int p = 0; p < exp.Length; p++)
        {
            isEmpty = false;
            string[] e = exp[p].Split('=');
            //iReturn = dist.addOthExpRecord(e[0], e[1], amount[p], rms[p], sfcode, mon, yr, soj);
            if (sfvacant.Rows.Count > 0)
            {
                iReturn = dist.addOthExpRecord_vacant_Last(e[0], e[1], amount[p], rms[p], sfcode, mon, yr, values["sl_no"]);
            }
            else
            {
                iReturn = dist.addOthExpRecord_vacant(e[0], e[1], amount[p], rms[p], sfcode, mon, yr);
            }
        }


        if (iReturn > 0)
        {
           string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                window.parent.opener.location.reload();
                                </script>";
            base.Response.Write(close);

            

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
        DataTable headDetailsDt = dist.getFieldForce_Vacant_slno(sfcode, mon, yr, s4, s5);
        DataTable t2 = dist.getSavedOtheExpRecord_Vacant(mon, yr, sfcode, headDetailsDt.Rows[0]["sl_no"].ToString());
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