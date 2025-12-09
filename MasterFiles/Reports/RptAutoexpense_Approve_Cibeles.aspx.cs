using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Data.SqlClient;
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
    DataTable table = null;
    int slno = 0;
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

            // FillFieldForcediv(divcode);
            ddlSubdiv.Focus();
            GetMyMonthList();
            bind_year_ddl();
        }
        if (Session["sf_type"].ToString() == "3")
        {
            UserControl_MenuUserControl c1 =
           (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
        }
        if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
           (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            // c1.FindControl("btnBack").Visible = false;
        }
        FillFieldForcediv(divcode);
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
        for (int intCount = 2017; intCount <= year + 1; intCount++)
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
        //if (Session["sf_type"].ToString() == "2")
        //{
        //    dsSubDivision = dv.SalesForce_Reporting(sfcode);
        //}
        //else
        //{
        dsSubDivision = dv.SalesForceListMgrGet(divcode, sfcode);
        // }
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlSubdiv.DataTextField = "sf_name";
            ddlSubdiv.DataValueField = "sf_code";
            ddlSubdiv.DataSource = dsSubDivision;
            ddlSubdiv.DataBind();

            ddlSF.DataTextField = "desig_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSubDivision;
            ddlSF.DataBind();

        }
    }
    protected void linkcheck_Click(object sender, EventArgs e)
    {
        FillFieldForcediv(divcode);
        ddlSubdiv.Visible = true;
        //txtNew.Visible = true;
        btnSF.Visible = true;
        FillColor();
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
    protected void btnSF_Click_Process(object sender, EventArgs e)
    {

        Distance_calculation Exp = new Distance_calculation();
        DataTable expprocess = Exp.Expense_pocess_sfcode(monthId.SelectedValue, yearID.SelectedValue);
        foreach (DataRow row2 in expprocess.Rows)

        {
            DataSet dsAllowance = Exp.getAllow(row2["sf_code"].ToString());
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

            string allowStr = "";

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
                    //fare = Convert.ToDouble(row["fare"].ToString());
                    //rangeFare = Convert.ToDouble(row["Range_of_Fare1"].ToString());
                    // rangeFare2 = Convert.ToDouble(row["Range_of_Fare2"].ToString());
                    //  rangeFare3 = Convert.ToDouble(row["Range_of_Fare3"].ToString());
                    allowStr = ex + "@" + os + "@" + hq + "@" + ossm + "@" + osnm;
                    // allowStr2 = ex + "@" + os + "@" + hq + "@" + fare + "@" + ossm + "@" + osnm;
                }
                //allowString.Value = allowStr;
                // allowString2.Value = allowStr2;
            }

            DataTable ds = Exp.getFieldForce(divcode, row2["sf_code"].ToString());
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

                //allowString1.Value = allowStr1;

            }


            DataSet dsleavetyp = null;
            DataSet expenseDataset = null;
            DataSet placeDataset = null;
            expenseDataset = Exp.getExpense_Cibeles(row2["sf_code"].ToString(), monthId.SelectedValue, yearID.SelectedValue, divcode);
            placeDataset = Exp.getPlace_Cibeles(divcode, row2["sf_code"].ToString(), monthId.SelectedValue, yearID.SelectedValue);

            dsleavetyp = Exp.getleaveTyp(monthId.SelectedValue, yearID.SelectedValue, row2["sf_code"].ToString());
            DataTable leavtyp = dsleavetyp.Tables[0];


            DataTable t1 = expenseDataset.Tables[0];
            DataTable t2 = placeDataset.Tables[0];

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

            for (int i = 0; i < t1.Rows.Count; i++)
            {
                try
                {


                    int prevIndex = (i == 0 ? i : i - 1);
                    int nextIndex = (i == t1.Rows.Count - 1 ? i : i + 1);
                    DataRow currRow = t1.Rows[i];
                    string curexpallowcat = currRow["Exp_Allow_Cat"].ToString();
                    // int expcat = 0;
                    if (curexpallowcat != null && curexpallowcat != "" && (currRow["Territory_Cat"].ToString() == "OS" || currRow["Territory_Cat"].ToString() == "OS-EX"))
                    {
                        //expcat = 1;
                        currRow["Allowance"] = (curexpallowcat == "MM" ? os : curexpallowcat == "SM" ? ossm : curexpallowcat == "NM" ? osnm : "0");
                    }
                    else
                    {
                        currRow["Allowance"] = (currRow["Territory_Cat"] == null ? "0" : (currRow["Territory_Cat"].ToString() == "HQ" ? hq : currRow["Territory_Cat"].ToString() == "OS" ? os : currRow["Territory_Cat"].ToString() == "EX" ? ex : currRow["Territory_Cat"].ToString() == "OS-EX" ? os : "0"));
                    }

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

                    currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name1"].ToString());


                    if (currRow["Worktype_Name_B"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_B"].ToString().Equals("Holiday"))
                    {
                        // currRow["Adate"] = "<span style='background-color:#FFE2D5'>" + currRow["Adate"].ToString() + "</span>";
                        currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
                    }


                }
                catch (Exception ex1)
                {
                    Console.WriteLine("Exception Type: {0}", ex1.GetType());
                    Console.WriteLine("  Message: {0}", ex1.Message);
                }

            }








            Dictionary<String, String> values = new Dictionary<String, String>();
            values["sf_code"] = row2["sf_code"].ToString();
            values["div_code"] = divcode;
            values["month"] = monthId.SelectedValue;
            values["year"] = yearID.SelectedValue;
            values["period"] = "";
            values["flag"] = "2";
            values["s_date"] = "2016-01-03 20:43:47.643";
            values["File_Path"] = "";

            //int iReturn = -1;
            Distance_calculation dist = new Distance_calculation();
            DataTable table = dist.getMgrAppr(divcode);
            DataTable ds1 = dist.getFieldForce(divcode, row2["sf_code"].ToString());
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
            //if (table.Rows.Count > 0)
            //{
            //    if ("1".Equals(table.Rows[0]["MgrAppr_Remark"].ToString()))
            //    {
            //        values["flag"] = "7";

            //    }
            //    if (ds1.Rows[0]["reporting_to_sf"].ToString() == "admin")
            //    {
            //        values["flag"] = "1";
            //    }
            //}
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
                       "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,File_Path,frmTovalues,SF_JOINING_DATE,Employee_Id,sf_emp_id,auto_process)" +
                       "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                       values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",'" + values["File_Path"] + "','','" + values["soj"] + "','" + values["EmpNo"] + "','" + values["SfEmpNo"] + "',1)";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = "INSERT INTO Trans_Expense_Head " +
                         "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period,File_Path,frmTovalues,SF_JOINING_DATE,Employee_Id,sf_emp_id,auto_process)" +
                         "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                         values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",getdate()," + values["flag"] + ",'" + values["File_Path"] + "','','" + values["soj"] + "','" + values["EmpNo"] + "','" + values["SfEmpNo"] + "',1)";
                        command.ExecuteNonQuery();
                    }




                    foreach (DataRow dataRow in t1.Rows)

                    {

                        command.CommandText = "INSERT INTO Trans_Expense_Detail " +
                                                           "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,adate1,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,catTemp,Expense_Total,Division_Code,from_place,to_place,rw_amount,rw_rmks,temtyp)" +
                                                           "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                                           dataRow["Adate"].ToString() + "','" + dataRow["theDayName"].ToString().Replace("'", "\"") + "','" + dataRow["Worktype_Name_B"].ToString() + "','" + dataRow["Adate1"].ToString() + "','" + dataRow["Territory_name"].ToString() + "','" + dataRow["Territory_cat"].ToString()
                                                           + "','" + dataRow["allowance"].ToString() + "','','','','" + dataRow["allowance"].ToString() + "','" + values["div_code"] + "','','','','','')";
                        command.ExecuteNonQuery();

                    }


                    DataTable customExpTable = new DataTable();
                    customExpTable.Columns.Add("Expense_Parameter_Name");
                    customExpTable.Columns.Add("amount");
                    customExpTable.Columns.Add("Expense_Parameter_Code");

                    //DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
                    customExpTable.Columns.Add("pro_Rate");

                    DataTable expParamsAmnt = Exp.sp_Get_Fixed_expense(divcode, row2["sf_code"].ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                    DataTable expDCRjoining = Exp.sp_DCRjoining(divcode, row2["sf_code"].ToString(), soj, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
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
                        //otherExpGrid.DataSource = customExpTable;
                        //otherExpGrid.DataBind();
                    }

                    foreach (DataRow fixedrow in customExpTable.Rows)
                    {





                        command.CommandText = "INSERT INTO Exp_Fixed " +
                                      "(sl_No,Expense_Parameter_Code,Amt,SF_Code)" +
                                      "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + row2["sf_code"].ToString() + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                                      fixedrow["Expense_Parameter_Code"].ToString() + "','" + fixedrow["amount"].ToString() + "','" + row2["sf_code"].ToString() + "')";
                        command.ExecuteNonQuery();

                    }

                    ////HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");

                    ////string[] splitVal = otherExpValues.Value.Split('~');

                    ////string[] pert = splitVal[0].Split(',');
                    ////string[] amount = splitVal[1].Split(',');
                    ////string[] date1 = splitVal[2].Split(',');
                    ////string[] misExpVal = splitVal[3].Split(',');
                    ////for (int p = 0; p < misExpVal.Length; p++)
                    ////{
                    ////    isEmpty = false;

                    ////    string[] dateVal = date1[p].Split('=');
                    ////    string[] misExp = misExpVal[p].Split('=');
                    ////    if (pert[p] == "")
                    ////    {
                    ////        pert[p] = "0";
                    ////    }
                    ////    if (amount[p] == "")
                    ////    {
                    ////        amount[p] = "0";
                    ////    }
                    ////    if (!misExp[1].Contains("Select"))
                    ////    {
                    ////        command.CommandText = "INSERT INTO Exp_others " +
                    ////                           "(sl_No,expval,Paritulars,Amt,Remarks,mrAdjDate)" +
                    ////                           "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                    ////                           misExp[0] + "','" + misExp[1] + "'," + amount[p] + ",'" + pert[p] + "','" + dateVal[0] + "')";
                    ////        command.ExecuteNonQuery();
                    ////    }

                    ////}

                    //HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
                    //string[] splitVal = otherExpValues.Value.Split('~');

                    //string[] pert = splitVal[0].Split(',');
                    //string[] amount = splitVal[1].Split(',');
                    //string[] op = splitVal[2].Split(',');
                    //string[] date1 = splitVal[4].Split(',');
                    //string[] misExpVal = splitVal[5].Split(',');


                    //for (int p = 0; p < op.Length; p++)
                    //{
                    //    string[] e = op[p].Split('=');
                    //    string[] dateVal = date1[p].Split('=');
                    //    string[] misExp = misExpVal[p].Split('=');
                    //    if (amount[p] != "")
                    //    {
                    //        command.CommandText = "INSERT INTO Exp_AccInf " +
                    //        "(sl_No,Paritulars,typ,Amt,grand_total,sf_code,Expense_Month,Expense_year,adminAdjDate,Expense_Parameter_Name,Expense_Parameter_Code)" +
                    //        "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + " and sf_joining_date='" + values["soj"] + "'),'" +
                    //         pert[p] + "' ," + e[0] + "," + amount[p] + "," + splitVal[3] + ",'" + values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + dateVal[0] + ",'" + misExp[1] + "','" + misExp[0] + "')";
                    //        command.ExecuteNonQuery();
                    //    }
                    //    //iReturn = Exp.addAdminAdjustmentExpRecord(dateVal[0], misExp[0], misExp[1], e[0], pert[p] == "" ? "0" : pert[p], amount[p] == "" ? "0" : amount[p], splitVal[3], row2["sf_code"].ToString(), monthId, yearID, dt3);
                    //}
                    transaction.Commit();
                    connection.Close();



                    // vacflg = 1;
                    // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Approve.aspx'</script>");

                }
                catch (Exception ex3)
                {

                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                    Console.WriteLine("Message: {0}", ex3.Message);


                    try
                    {

                        transaction.Rollback();

                    }

                    catch (Exception ex2)
                    {


                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                        Console.WriteLine("  Message: {0}", ex2.Message);

                    }

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Session Expired.Kindly Resubmit Again');window.location='RptAutoExpense_Approve.aspx'</script>");



                }
            }
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');window.location='RptAutoExpense_Approve_Cibeles.aspx'</script>");
        }

    }
    protected void btnSF_Click(object sender, EventArgs e)
    {
        if (divcode == "104")
        {
            btnprocess.Visible = true;
        }
        Distance_calculation dv = new Distance_calculation();
        //SalesForce S = new SalesForce();
        //dsSubDivision = S.Hierarchy_Team(divcode, ddlSubdiv.SelectedValue.ToString());
        //if (chkVacant.Checked == true)
        //{
        //    dsSubDivision = dv.getFilterRgn(divcode, ddlSubdiv.SelectedValue.ToString());
        //    //string filter="Active".Equals(dsSubDivision.Rows["sf_Tp_Active_Flag"].ToString());	
        //}
        //else
        //{
        // dsSubDivision = dv.getFilterRgn_Vacant(divcode, ddlSubdiv.SelectedValue.ToString());
        dsSubDivision = dv.getsfExp_approval_Active(divcode, ddlSubdiv.SelectedValue.ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        //}

        DataTable dtAllFare = dv.getAllowFare(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dtAllHQ = dv.getAllowDaysHQ(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dtAllEX = dv.getAllowDaysEX(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dtAllOS = dv.getAllowDaysOS(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt = dv.getOtherExpDetails(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt1 = dv.getFixedClmnName(divcode);
        DataTable dt2 = dv.getmis(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable dt3 = dv.getApproveamnt(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        DataTable mainTable = dsSubDivision.Tables[0];
        table = dv.getMgrAppr(divcode);
        mainTable.Columns.Add("allowance");
        mainTable.Columns.Add("fare");
        mainTable.Columns.Add("Fixed_Column1");
        mainTable.Columns.Add("Fixed_Column2");
        mainTable.Columns.Add("Fixed_Column3");
        mainTable.Columns.Add("Fixed_Column4");
        mainTable.Columns.Add("Fixed_Column5");
        mainTable.Columns.Add("Fixed_Column6");
        mainTable.Columns.Add("Fixed_Column7");
        mainTable.Columns.Add("Fixed_Column8");
        mainTable.Columns.Add("Fixed_Column9");
        mainTable.Columns.Add("Fixed_Column10");
        mainTable.Columns.Add("mis_Amt");
        mainTable.Columns.Add("rw_amount");
        mainTable.Columns.Add("tot");
        mainTable.Columns.Add("Status");
        mainTable.Columns.Add("appAmnt");
        mainTable.Columns.Add("Decrement");
        mainTable.Columns.Add("Increment");
             
        mainTable.Columns.Add("admin_approval_date");
        mainTable.Columns.Add("HQ_Days");
        mainTable.Columns.Add("EX_Days");
        mainTable.Columns.Add("OS_Days");
        if (mainTable.Rows.Count > 0)
        {
            //            double totClaimedAmnt = 0;
            foreach (DataRow row in mainTable.Rows)
            {
                double totClaimedAmnt = 0;
                String filter = "SF_Code='" + row["SF_Code"].ToString() + "'";
                DataRow[] rows = dtAllFare.Select(filter);

                DataRow[] row11 = dtAllHQ.Select(filter);
                DataRow[] row22 = dtAllEX.Select(filter);
                DataRow[] row33 = dtAllOS.Select(filter);

                DataRow[] othRows = dt.Select(filter);
                DataRow[] misRows = dt2.Select(filter);
                DataRow[] appRows = dt3.Select(filter);
                if (appRows.Count() > 0)
                {
                    row["appAmnt"] = appRows[0]["grand_total"];
                    if (appRows[0]["typ"].ToString() == "0")
                    {
                        row["Decrement"] = appRows[0]["amt"];
                    }
                    if (appRows[0]["typ"].ToString() == "1")
                    {
                        row["Increment"] = appRows[0]["amt"];
                    }
                }
                string st = "";
                if (divcode == "104" && (row["SF_Code"].ToString().Contains("MR")|| row["SF_Code"].ToString().Contains("MGR")))
                {
                    st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                }
                else
                {
                    st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                }
                if (rows.Count() > 0)
                {
                    row["allowance"] = rows[0]["allw"];
                    row["fare"] = rows[0]["fare"];
                    if (row11.Count() > 0)
                    {
                        row["HQ_Days"] = row11[0]["allw"];
                    }
                    if (row22.Count() > 0)
                    {
                        row["EX_Days"] = row22[0]["allw"];
                    }
                    if (row33.Count() > 0)
                    {
                        row["OS_Days"] = row33[0]["allw"];
                    }
                    row["rw_amount"] = rows[0]["rw_amount"];
                    //row["Date"] = rows[0]["submission_date"];
                    //row["Approval_Datea"] = rows[0]["Approval_Datea"];
                    row["admin_approval_date"] = rows[0]["submission_date"];
                    st = rows[0]["Status"].ToString();
                    
                    if (st == "1" || st == "8")
                    {
                        if (divcode == "104" && (row["SF_Code"].ToString().Contains("MR") || row["SF_Code"].ToString().Contains("MGR")))
                        {
                            st = "<span style='color:green;font-weight:bold'>Approved</span>";
                        }
                        else
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                        }
                    }
                    else if (st == "2" || st == "6")
                    {
                        st = "<span style='color:green;font-weight:bold'>Approved</span>";
                    }
                    else if (st == "3")
                    {
                        st = "<span style='background-color:brown;font-weight:bold'>Approval Pending</span>";
                    }
                    else if (st == "7")
                    {

                        if (table.Rows.Count > 0)
                        {
                            if ("1".Equals(table.Rows[0]["MgrAppr_Sameadmin"].ToString()))
                            {
                                st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                            }
                            else
                            {
                                st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";

                            }
                        }
                        else
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";
                        }


                        //st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Mgr Approval Pending</span>";
                    }
                    else
                    {
                        if (divcode == "104" && (row["SF_Code"].ToString().Contains("MR") || row["SF_Code"].ToString().Contains("MGR")))
                        {
                            st = "<span style='background-color:yellow;color:blue;font-weight:bold'>Approval Pending</span>";
                        }
                        else
                        {
                            st = "<span style='color:red;font-weight:bold'>Not Prepared</span>";
                        }
                    }

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["allowance"].ToString()) + Convert.ToDouble(row["fare"].ToString()) + Convert.ToDouble(row["rw_amount"].ToString());
                }
                row["Status"] = st;
                if (misRows.Count() > 0)
                {
                    row["mis_Amt"] = misRows[0]["mis_Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(row["mis_Amt"].ToString());
                }
                for (int i = 0; i < othRows.Count(); i++)
                {
                    row["Fixed_Column" + (i + 1)] = othRows[i]["Amt"];

                    totClaimedAmnt = totClaimedAmnt + Convert.ToDouble(othRows[i]["Amt"].ToString());
                }
                row["tot"] = totClaimedAmnt;
            }
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                grdSalesForce.Columns[13 + i].Visible = true;
                grdSalesForce.Columns[13 + i].HeaderText = dt1.Rows[i]["Expense_Parameter_Name"].ToString();

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
                HiddenField hold = (HiddenField)gridRow.FindControl("Hiddenhold");
                Label lblSNo = (Label)gridRow.FindControl("lblSNo");
                if (hold.Value == "H")
                {
                    gridRow.Visible = false;
                }
                else
                {
                    slno += 1;

                    lblSNo.Text = slno.ToString();

                }


                lbl.Text = name.Value;


                link.Text = "<span>" + lbl.Text + "</span>";
                //lbl.Text = name.Value;
                string sURL = "";
                string AdminEnd = "AdmnEnd";
                string sfcd = code.Value;
                if (divcode == "104")
                {
                    if (sfcd.Contains("MR"))
                    {
                        sURL = "../MR/RptAutoExpense_rowwise_Cibeles.aspx?AdminEnd=" + AdminEnd + "&sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                    }
                    else
                    {
                        sURL = "../MgR/RptAutoExpense_Mgr_Cibles.aspx?AdminEnd=" + AdminEnd + "&sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                    }
                }
                else
                {
                    sURL = "RptAutoExpense_view1.aspx?sf_code=" + code.Value + "&monthId=" + monthId.SelectedValue.ToString() + "&yearId=" + yearID.SelectedValue.ToString() + "&divCode=" + divcode + "";
                }
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "','ModalPopUp');";
                link.NavigateUrl = "#";
                //cell.Controls.Add(link);
                Label label = (Label)gridRow.FindControl("lblstatus");
                if (label.Text.Contains("Not Prepared") || label.Text.Contains("Mgr Approval Pending"))
                {
                    gridRow.Cells[2].Controls.Add(lbl);
                }
                else
                {
                    gridRow.Cells[2].Controls.Add(link);

                }

            }

        }
        else
        {
            grdSalesForce.DataSource = dsSubDivision;
            grdSalesForce.DataBind();
        }
    }

    // Sorting
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    protected void grdSalesForce_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        Distance_calculation dv = new Distance_calculation();
        dsSubDivision = dv.getsfExp_approval_Active(divcode, ddlSubdiv.SelectedValue.ToString(), monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());        

        DataView sortedView = new DataView(dsSubDivision.Tables[0]);
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        grdSalesForce.DataSource = sortedView;
        grdSalesForce.DataBind();        
    }
}