using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class Index : System.Web.UI.Page
{
    DataSet dsLogin = null;
    DataSet dsSalesForce = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsImage = new DataSet();
    DataSet dsBirth = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsadmn = null;
    DataSet dsAdmNB = null;
    DataSet dsLogin1 = null;
    DataSet dspwd = new DataSet();
    DataSet dsDoc = null;
    DataSet dsFeed = new DataSet();
    DataSet dsFeedpost = new DataSet();
    DataSet dsVacant = new DataSet();
    string pwdDt = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    int time;
    int tp_month4;
    string from_year = string.Empty;
    int from_year2;
    int next_month4;
    int to_year2;
    string to_year = string.Empty;
    DataSet dsTp = null;
    DataSet dsDesig = null;
    DataSet dsquiz = new DataSet();
    DataSet dssample = null;
    DataSet dsinput = null;
    private DataSet dsinput1;
    protected void Page_Load(object sender, EventArgs e)
    {
        // lblCurDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        //lblCurTime.Text = DateTime.Now.ToString("hh:mm:ss tt");

        //Response.Redirect("Security.aspx");

        if (Request.QueryString["sf_username"] != null && Request.QueryString["sf_password"] != null)
        {
            strUserName = Request.QueryString["sf_username"].ToString();
            strPassword = Request.QueryString["sf_password"].ToString();
            btnLogin_Click(sender, e);
        }

        string str = "";
        if (Request.Cookies["Username"] != null)
        {
            str = Request.Cookies["Username"].Value.ToString();
        }

        //if (Application["Username"] != null)
        //{
        //    str = Application["Username"].ToString();
        //    //Response.Write(Name);
        //}  


        //ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert('" + str + "');", true);

        //if (str.Contains("mmpplsfa.info"))
        //{
        //ImgFull.Src = "Images/Home page.jpg";



        //}



        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreRender(e);
            BindImage();
            Session["ID"] = null;
            Session.Abandon();
            txtUserName.Focus();


        }
        // BindImage1();
        //BindImage_FieldForce();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    }
    private void BindImage()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_LoginPage_Image", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        //DataList1.DataSource = ds;
        //DataList1.DataBind();
    }
    private void BindImage1()
    {
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlCommand cmd = new SqlCommand("select ROW_NUMBER() OVER (ORDER BY Id ) SL, FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() order by SL DESC ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataSet dsImage = new DataSet();
        da.Fill(dsImage);
        con.Close();

    }
    private void BindImage_FieldForce()
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }


        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where sf_code='" + sf_code + "' and Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtUserName.Value = "";
        txtPassWord.Value = "";
    }
    string GetbgColor(string GetDiv_Code)
    {
        DataTable dt = new DataTable();
        //GetDiv_Code = GetDiv_Code.TrimEnd(',');	
        string[] GetDiv_CodeSplit = GetDiv_Code.Split(',');
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_ColorCodeDetail", con);

            cmd.CommandTimeout = 600;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", GetDiv_CodeSplit[0]);
            cmd.Parameters.AddWithValue("@Record", "select");
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            dt.AcceptChanges();
            if (dt.Rows.Count > 0)
            {
                GetDiv_Code = dt.Rows[0]["div_color"].ToString();
            }
            else { GetDiv_Code = "#e8ebec";
            }
            return GetDiv_Code;
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        UserLogin ul = new UserLogin();
        DataSet dsListeddr = null;
        if (Request.QueryString["sf_username"] != null && Request.QueryString["sf_password"] != null)
        {
            txtUserName.Value = strUserName;
            txtPassWord.Value = strPassword;

            txtUserName.Value = txtUserName.Value.Replace("--", "");
            txtUserName.Value = txtUserName.Value.Replace("'", "");
            txtPassWord.Value = txtPassWord.Value.Replace("--", "");
            txtPassWord.Value = txtPassWord.Value.Replace("'", "");
        }
        txtUserName.Value = txtUserName.Value.Replace("--", "");
        txtUserName.Value = txtUserName.Value.Replace("'", "");
        txtPassWord.Value = txtPassWord.Value.Replace("--", "");
        txtPassWord.Value = txtPassWord.Value.Replace("'", "");
        dsLogin = ul.Process_Login(txtUserName.Value.Trim(), txtPassWord.Value.Trim());
        if (dsLogin.Tables[0].Rows.Count == 0)
        {
            dsLogin1 = ul.HO_Login(txtUserName.Value.Trim(), txtPassWord.Value.Trim());
            if (dsLogin1.Tables[0].Rows.Count > 0)
            {
                if (dsLogin1.Tables[0].Rows.Count != 0 && (dsLogin1.Tables[0].Rows[0]["standby"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["standby"].ToString() == ""))
                {
                    Session["sf_code"] = "admin";
                    Session["HO_ID"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    Session["division_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    Session["sf_name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    Session["Corporate"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    Session["div_Name"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    Session["Division_SName"] = dsLogin1.Tables[0].Rows[0]["Division_SName"].ToString();
                    Session["Sub_HO_ID"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    Session["sf_type"] = "3";
                    Session["Designation_Short_Name"] = "";
                    Session["Sf_HQ"] = "";
                     
                    Session["Div_color"] = GetbgColor(dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                    //  Session["div_code"] = "";
                    // if (dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() != "0")
                    // {
                    //Server.Transfer("Default.aspx");
                    //Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");
                    // }
                    // else 
                    if (dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() == "0" || dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() == null || dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() == "" || dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() != "0")
                    {
                        AdminSetup ad = new AdminSetup();
                        dsFeedpost = ad.Feedback_post();
                        if (dsFeedpost.Tables[0].Rows.Count > 0)
                        {
                            if (dsFeedpost.Tables[0].Rows[0]["Activate_Flag"].ToString() == "1")
                            {
                                int status = 1;
                                dsFeed = ad.Feedback_Exist(Session["division_code"].ToString(), status);
                                if (dsFeed.Tables[0].Rows.Count > 0)
                                {

                                }
                                else
                                {
                                    Response.Redirect("Feed_Back_Form.aspx");
                                }
                            }
                        }
                        dspwd = ad.getHo_Pwd_Uptdt(Session["HO_ID"].ToString());
                        if (dspwd.Tables[0].Rows.Count > 0)
                        {
                            pwdDt = dspwd.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        }
                        if (pwdDt == "" || pwdDt == null)
                        {
                            Response.Redirect("Ho_Id_Pwd_Chg.aspx");
                        }
                        else if (dsLogin1.Tables[0].Rows[0]["Sub_HO_ID"].ToString() != "0")
                        {
                            Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");
                        }
                        else
                        {
                            DateTime startDate = DateTime.Parse(pwdDt);
                            DateTime expiryDate = startDate.AddDays(45);
                            if (DateTime.Now > expiryDate)
                            {

                                Response.Redirect("Ho_Id_Pwd_Chg.aspx");
                            }
                            else
                            {
                                Response.Redirect("Default.aspx");
                            }
                        }
                    }
                }
                else if (dsLogin1.Tables[0].Rows[0]["standby"].ToString() == "1")
                {
                    Session["sf_type"] = "3";
                    Session["division_code"] = dsLogin1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();

                    Response.Redirect("Standby.aspx");
                }
            }

            else
            {
                dsVacant = ul.Process_Login(txtUserName.Value.Trim() + "V", txtPassWord.Value.Trim());
                if (dsVacant.Tables[0].Rows.Count > 0)
                {
                    if ((dsVacant.Tables[0].Rows[0]["sf_TP_Active_Flag"].ToString() == "1") && (dsVacant.Tables[0].Rows[0]["sf_vacantblock"].ToString() == "R") && (dsVacant.Tables[0].Rows[0]["sf_status"].ToString() == "0"))
                    {
                        Response.Redirect("Vacant.aspx");
                    }
                    else if ((dsVacant.Tables[0].Rows[0]["sf_TP_Active_Flag"].ToString() == "1") && (dsVacant.Tables[0].Rows[0]["sf_vacantblock"].ToString() == "H") && (dsVacant.Tables[0].Rows[0]["sf_status"].ToString() == "0"))
                    {
                        Session["sf_code"] = dsVacant.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        Session["div_code"] = dsVacant.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        Response.Redirect("Hold.aspx");

                    }
                }
                else
                {
                    txtPassWord.Value = "";
                    txtUserName.Value = "";
                    msg.Visible = true;
                    msg.Text = "Invalid User Name and Password.";
                    txtUserName.Focus();
                }
            }
        }
        else if (dsLogin.Tables[0].Rows[0]["standby"].ToString() == "1")
        {
            Session["sf_type"] = "";
            Response.Redirect("Standby.aspx");
        }
        else if ((dsLogin.Tables[0].Rows[0]["sf_TP_Active_Flag"].ToString() == "1") && (dsLogin.Tables[0].Rows[0]["sf_vacantblock"].ToString() == "R") && (dsLogin.Tables[0].Rows[0]["sf_status"].ToString() == "0"))
        {
            Response.Redirect("Vacant.aspx");
        }
        else if ((dsLogin.Tables[0].Rows[0]["sf_TP_Active_Flag"].ToString() == "1") && (dsLogin.Tables[0].Rows[0]["sf_vacantblock"].ToString() == "H") && (dsLogin.Tables[0].Rows[0]["sf_status"].ToString() == "0"))
        {
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Response.Redirect("Hold.aspx");

        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "1")
        {
            Session["sf_type"] = "3";
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Response.Redirect("Block_Vacant.aspx");
        }
        else if (dsLogin.Tables[0].Rows[0]["SF_Status"].ToString() == "2")
        {
            txtPassWord.Value = "";
            txtUserName.Value = "";
            msg.Visible = true;
            msg.Text = "Invalid User Name and Password.";
            txtUserName.Focus();
        }
        //else if (dsLogin.Tables[0].Rows[0]["standby"].ToString() == "1")
        //{
        //    Response.Redirect("Standby.aspx");
        //}
        else
        {
            Session["sf_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            Session["div_code"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            Session["sf_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            Session["sf_type"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            Session["Designation_Short_Name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Session["Sf_HQ"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            Session["HO_ID"] = "";
            Session["Corporate"] = "";
            Session["division_code"] = "";
            Session["div_name"] = dsLogin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            Session["Div_color"] = GetbgColor(dsLogin.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
            Session["Designation_Code"] = dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString();
            BindImage1();
           BindImage_FieldForce();
            //DataSet dsMailCheck = ul.Check_Mail(Session["sf_code"].ToString(), Session["div_code"].ToString());
            if (Session["sf_type"].ToString() == "1") // MR Login
            {
                UserLogin astp = new UserLogin();
                int iRet = astp.Login_details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
                AdminSetup admin = new AdminSetup();
                dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
                AdminSetup adm_Nb = new AdminSetup();
                dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
                //SalesForce sf_img = new SalesForce();
                //dsImage_FF = sf_img.Sales_Image(Session["div_code"].ToString(), Session["sf_code"].ToString());
                ListedDR lstDr = new ListedDR();

                 if (dsImage.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("HomePage_Image.aspx");
                }
                else if (dsImage_FF.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("HomePage_FieldForcewise.aspx");

                }








                //latest added
                TP_New tp_new2 = new TP_New();
                DataSet dsExpense = new DataSet();
                dsExpense = tp_new2.get_Expense_Range(Session["sf_code"].ToString());

                if (dsExpense.Tables[0].Rows.Count > 0 && dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "0" && dsExpense.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "28")
                {

                    if (dsExpense.Tables[0].Rows[0][0].ToString() != "")
                    {
                        DateTime dt = DateTime.Now;
                        int day = dt.Day;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;

                        var now = DateTime.Now;
                        var startOfMonth = new DateTime(now.Year, now.Month, 1);
                        int day1 = startOfMonth.Day;
                        var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                        var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                        //int exp_start = Convert.ToInt16(dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        int exp_end = Convert.ToInt16(dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                        if (dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "0")
                        {
                            if (day >= 1 && day <= exp_end)
                            {

                                DateTime dat = DateTime.Now;
                                dat = dat.AddMonths(-1);
                                int last_month = dat.Month;
                                int Yearr = dat.Year;

                                TP_New tt = new TP_New();
                                bool isExp = false;
                                isExp = tt.IsExpChk_lastMonth(sf_code, div_code, last_month, Yearr);//chk current month tp

                                if (isExp == false)
                                {
                                    ViewState["txtUserName"] = txtUserName.Value;
                                    ViewState["txtPassWord"] = txtPassWord.Value;
                                    Response.Redirect("~/HomePage_Expense.aspx?exp=" + "1" + "&txtUserName=" + txtUserName.Value + "&txtPassWord=" + txtPassWord.Value + "&enddate=" + exp_end);
                                }
                            }
                            else if (day > exp_end)
                            {
                                DateTime dat = DateTime.Now;
                                dat = dat.AddMonths(-1);
                                int last_month = dat.Month;
                                int Yearr = dat.Year;

                                TP_New tt = new TP_New();
                                bool isExp = false;
                                isExp = tt.IsExpChk_lastMonth(sf_code, div_code, last_month, Yearr);//chk current month tp

                                if (isExp == false)
                                {
                                    Response.Redirect("~/HomePage_Expense.aspx?exp=" + "2" + "&txtUserName=" + txtUserName.Value + "&txtPassWord=" + txtPassWord.Value);
                                }
                            }
                        }
                    }
                }
                //latest added end



                //BindImage1();

                int Count;
               
                Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

                AdminSetup dv = new AdminSetup();
                dsadmn = dv.getHome_Dash_Display(div_code);

                //ListedDR LstDoc = new ListedDR();
                //dsDoc = LstDoc.getLstdDr_Wrng_CreationFFWise(sf_code, div_code);


                System.Threading.Thread.Sleep(time);
                Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");


                AdminSetup adminsa_ip = new AdminSetup();
                dssample = adminsa_ip.getsampleEntry(Session["div_code"].ToString());

                if (dssample.Tables[0].Rows.Count > 0)
                {
                    AdminSetup adm1 = new AdminSetup();
                    dssample = adm1.getsample(Session["sf_code"].ToString(), Session["div_code"].ToString());
                    if (dssample.Tables[0].Rows.Count > 0)
                    {
                        System.Threading.Thread.Sleep(time);
                        Response.Redirect("Sample_Input_Acknowledge.aspx");
                    }
                    else
                    {

                    }

                }

                //command By vasanthi
                dsinput = adminsa_ip.getinputEntry(Session["div_code"].ToString());
                if (dsinput.Tables[0].Rows.Count > 0)
                {
                    AdminSetup adm1 = new AdminSetup();
                    dsinput1 = adm1.getinput(Session["sf_code"].ToString(), Session["div_code"].ToString());
                    if (dsinput1.Tables[0].Rows.Count > 0)
                    {
                        System.Threading.Thread.Sleep(time);
                        Response.Redirect("Input_Acknowlege.aspx");
                    }
                    else
                    {

                    }

                }

                #region tp_validate

                //DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
                //string sMonthName = dtDate.ToString("MMM");
                //TP_New tp = new TP_New();
                //DataSet dsTP = new DataSet();
                ////dsTP = tp.get_TP_Active_Date_New_Index(sf_code);
                //dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);
                //DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
                //string sMonthName1 = dtDate1.ToString();

                //string tp_month = dtDate1.ToString("MMM");

                //string tp_month1 = dtDate1.ToString();

                //string[] tp_month3 = tp_month1.Split('/');

                //tp_month4 = Convert.ToInt32(tp_month3[1]);
                //from_year = tp_month3[2];
                //from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


                //string next_month1 = DateTime.Now.AddMonths(1).ToString();

                //string[] next_month2 = next_month1.Split('/');

                //next_month4 = Convert.ToInt32(next_month2[1]);
                //to_year = next_month2[2];
                //to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

                //string Current_month = DateTime.Now.Month.ToString("MMM");
                //string next_month = DateTime.Now.AddMonths(1).ToString("MMM");


                //dsTp = adm.chk_tpbasedsystem_MR(div_code);

                ////if (dsTp.Tables[0].Rows.Count > 0)
                ////{
                //if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                //{
                //    AdminSetup ad = new AdminSetup();
                //    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                //    if (dsDesig.Tables[0].Rows.Count > 0)
                //    {

                //        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                //        {
                //            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                //            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                //            DateTime dt = DateTime.Now;
                //            int day = dt.Day;
                //            int curr_month = dt.Month;
                //            int curr_year = dt.Year;

                //            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                //            {

                //                var now = DateTime.Now;
                //                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                //                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                //                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                //                if (startdate <= day && enddate >= day)
                //                {
                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                    DataSet dsReject = new DataSet();

                //                    if (tp_month4 == curr_month && from_year2 == curr_year)
                //                    {
                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                            if (isRepSt == false)
                //                            {
                //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                            }
                //                            else
                //                            {
                //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                //                                if (isRepSt == false)
                //                                {
                //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                    if (iReturn > 0)
                //                                    {
                //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {

                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                //                            if (isRepSt == false)
                //                            {
                //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                if (iReturn > 0)
                //                                {
                //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                //                                }
                //                            }

                //                        }
                //                    }
                //                }
                //                else if (enddate <= DaysInMonth && enddate <= day)
                //                {
                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                    DataSet dsReject = new DataSet();


                //                    if (tp_month4 == curr_month && from_year2 == curr_year)
                //                    {
                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                            if (isRepSt == false)
                //                            {
                //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                            }
                //                            else
                //                            {
                //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                //                                if (isRepSt == false)
                //                                {
                //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                    if (iReturn > 0)
                //                                    {
                //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {

                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                //                            if (isRepSt == false)
                //                            {
                //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                if (iReturn > 0)
                //                                {
                //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                //                                }
                //                            }


                //                        }
                //                    }
                //                }
                //                else
                //                {

                //                    var today = DateTime.Today;
                //                    var month = new DateTime(today.Year, today.Month, 1);
                //                    var first = month.AddMonths(-1);
                //                    var last_month_date = month.AddDays(-1);

                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                     DataSet dsReject = new DataSet();

                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }

                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                            if (isRepSt == false)
                //                            {

                //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                //                                if (tp_month4 == curr_month && from_year2 == curr_year)
                //                                {
                //                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                                }
                //                                else
                //                                {
                //                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                    if (iReturn > 0)
                //                                    {
                //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                                    }
                //                                }
                //                            }

                //                        }
                //                }


                //            }

                //        }



                //    }


                //}

                #endregion



                DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
                string sMonthName = dtDate.ToString("MMM");
                TP_New tp = new TP_New();
                DataSet dsTP = new DataSet();
                // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

                dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);



                DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
                string sMonthName1 = dtDate1.ToString();

                string tp_month = dtDate1.ToString("MMM");

                string tp_month1 = dtDate1.ToString();

                string[] tp_month3 = tp_month1.Split('/');

                tp_month4 = Convert.ToInt32(tp_month3[1]);
                from_year = tp_month3[2];
                from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


                string next_month1 = DateTime.Now.AddMonths(1).ToString();

                string[] next_month2 = next_month1.Split('/');

                next_month4 = Convert.ToInt32(next_month2[1]);
                to_year = next_month2[2];
                to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

                string Current_month = DateTime.Now.Month.ToString("MMM");
                string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last_month_date = month.AddDays(-1);



                var threemth_back = month.AddMonths(-3);
                DateTime three_month_ago = Convert.ToDateTime(threemth_back);
                DateTime three_mnth_ago_lst_date = new DateTime(three_month_ago.Year, three_month_ago.Month, 1).AddMonths(1).AddDays(-1);

                var twomth_back = month.AddMonths(-2);
                DateTime two_month_ago = Convert.ToDateTime(twomth_back);
                DateTime two_mnth_ago_lst_date = new DateTime(two_month_ago.Year, two_month_ago.Month, 1).AddMonths(1).AddDays(-1);

                int two_mnt = two_mnth_ago_lst_date.Month;
                int tw_year = two_mnth_ago_lst_date.Year;

                int one_mnt = last_month_date.Month;
                int onemth_year = last_month_date.Year;


                dsTp = adm.chk_tpbasedsystem_MR(div_code);

                //if (dsTp.Tables[0].Rows.Count > 0)
                //{
                if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                {
                    AdminSetup ad = new AdminSetup();
                    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                    if (dsDesig.Tables[0].Rows.Count > 0)
                    {

                        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                        {
                            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                            DateTime dt = DateTime.Now;
                            int day = dt.Day;
                            int curr_month = dt.Month;
                            int curr_year = dt.Year;

                            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                            {

                                var now = DateTime.Now;
                                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                                if (startdate <= day && enddate >= day)//range
                                {
                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;

                                    DataSet dsReject = new DataSet();


                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }


                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {

                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                        }
                                        else
                                        {

                                            //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                            //if (dsReject.Tables[0].Rows.Count > 0)
                                            //{
                                            //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                                            //}
                                            //else
                                            //{


                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                        }
                                                    }
                                                }
                                           // }

                                        }
                                    }
                                    else if (tp_month4 == curr_month && from_year2 == curr_year)
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp


                                        if (isRepSt == false)
                                        {
                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                        }
                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                }
                                            }
                                        }

                                    }

                                    else
                                    {

                                        //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        //if (dsReject.Tables[0].Rows.Count > 0)
                                        //{
                                        //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        //}
                                        //else
                                        //{

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                }
                                            }
                                        //}

                                    }
                                }//end of range
                                else if (enddate <= DaysInMonth && enddate <= day)
                                {
                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;
                                    DataSet dsReject = new DataSet();


                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {

                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                        string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                        }
                                        else
                                        {

                                            //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                            //if (dsReject.Tables[0].Rows.Count > 0)
                                            //{
                                            //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                            //}
                                            //else
                                            //{

                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {

                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                        string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                        }
                                                    }
                                                }
                                           // }

                                        }
                                    }

                                    else
                                    {

                                        //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        //if (dsReject.Tables[0].Rows.Count > 0)
                                        //{
                                        //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        //}
                                        //else
                                        //{

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                }
                                            }
                                       // }
                                    }


                                }
                                else
                                {



                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;
                                    DataSet dsReject = new DataSet();

                                    //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    //if (dsReject.Tables[0].Rows.Count > 0)
                                    //{
                                    //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    //}

                                    //else
                                    //{

                                        ///start here for check last two months check
                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                                }

                                            }

                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                                if (isRepSt == false)
                                                {

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                    string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                    if (isRepSt == false)
                                                    {
                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                        string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }

                                            }

                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    if (tp_month4 == curr_month && from_year2 == curr_year)
                                                    {
                                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                    else
                                                    {
                                                        string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                if (tp_month4 == curr_month && from_year2 == curr_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                                else
                                                {
                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                  //  }
                                    //end here


                                }


                            }

                        }



                    }


                }

                //if (sMonthName1.Substring(2, 9) == DateTime.Now.Date.ToString().Substring(2, 9) && (Session["div_code"].ToString() == "23" || Session["div_code"].ToString() == "21"))
                //{
                //    System.Threading.Thread.Sleep(time);
                //    Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //}
                // Response.Redirect("Sale_Dashboard.aspx");
                AdminSetup admquiz = new AdminSetup();
                dsquiz = admquiz.Get_Quiz_Process(Session["div_code"].ToString(), Session["sf_code"].ToString());
                if (dsquiz.Tables[0].Rows.Count > 0)
                {
                    if (dsquiz.Tables[0].Rows[0]["res"].ToString() == "1" || dsquiz.Tables[0].Rows[0]["res"].ToString() == null || dsquiz.Tables[0].Rows[0]["res"].ToString() == "")
                    {
                        Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["res"].ToString() + "");
                    }
                }
                if (Count != 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");

                }
               
                else if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("Quote_Design.aspx");

                }
                else if (dsAdmNB.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("NoticeBoard_design.aspx");

                }
                else if (dsAdm.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("FlashNews_Design.aspx");
                }
                else if (dsadmn.Tables[0].Rows.Count > 0 && dsadmn.Tables[0].Rows[0]["DOB_DOW"].ToString() == "1")
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("DOB_DOW_ListedDr.aspx");
                }
                //else if (dsDoc.Tables[0].Rows.Count > 0)
                //{
                //    System.Threading.Thread.Sleep(time);
                //    Response.Redirect("Wrong_Creation.aspx");
                //}
                else if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("Birthday_Wish.aspx");
                }

                else
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("~/Default_MR.aspx");
                }
            }
            else if (Session["sf_type"].ToString() == "2") // MGR Login
            {               

                UserLogin astp = new UserLogin();
                int iRet = astp.Login_details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
                AdminSetup adm = new AdminSetup();
                dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
                AdminSetup admin = new AdminSetup();
                dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
                AdminSetup adm_Nb = new AdminSetup();
                dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
                SalesForce sf = new SalesForce();
                dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
                ListedDR lstDr = new ListedDR();

                if (dsImage.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("HomePage_Image.aspx");
                }
                else if (dsImage_FF.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Server.Transfer("HomePage_FieldForcewise.aspx");

                }

                int Count;

                Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

                DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
                string sMonthName = dtDate.ToString("MMM");
                TP_New tp = new TP_New();
                DataSet dsTP = new DataSet();
                // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

                dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);

                System.Threading.Thread.Sleep(time);
                Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");

                AdminSetup adminsa_ip = new AdminSetup();
                dssample = adminsa_ip.getsampleEntry(Session["div_code"].ToString());

                if (dssample.Tables[0].Rows.Count > 0)
                {
                    AdminSetup adm1 = new AdminSetup();
                    dssample = adm1.getsample(Session["sf_code"].ToString(), Session["div_code"].ToString());
                    if (dssample.Tables[0].Rows.Count > 0)
                    {
                        System.Threading.Thread.Sleep(time);
                        Response.Redirect("Sample_Input_Acknowledge.aspx");
                    }
                    else
                    {

                    }

                }
                //Comand by vasanthi
                dsinput = adminsa_ip.getinputEntry(Session["div_code"].ToString());
                if (dsinput.Tables[0].Rows.Count > 0)
                {
                    AdminSetup adm1 = new AdminSetup();
                    dsinput1 = adm1.getinput(Session["sf_code"].ToString(), Session["div_code"].ToString());
                    if (dsinput1.Tables[0].Rows.Count > 0)
                    {
                        System.Threading.Thread.Sleep(time);
                        Response.Redirect("Input_Acknowlege.aspx");
                    }
                    else
                    {

                    }

                }

                #region tp_validate

                //TP_New tp = new TP_New();
                //DataSet dsTP = new DataSet();

                //dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);

                //DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
                //string sMonthName1 = dtDate1.ToString();

                //string tp_month = dtDate1.ToString("MMM");

                //string tp_month1 = dtDate1.ToString();

                //string[] tp_month3 = tp_month1.Split('/');

                //tp_month4 = Convert.ToInt32(tp_month3[1]);
                //from_year = tp_month3[2];
                //from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


                //string next_month1 = DateTime.Now.AddMonths(1).ToString();

                //string[] next_month2 = next_month1.Split('/');

                //next_month4 = Convert.ToInt32(next_month2[1]);
                //to_year = next_month2[2];
                //to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

                //string Current_month = DateTime.Now.Month.ToString("MMM");
                //string next_month = DateTime.Now.AddMonths(1).ToString("MMM");


                //dsTp = adm.chk_tpbasedsystem_MGR(div_code);

                ////if (dsTp.Tables[0].Rows.Count > 0)
                ////{
                //if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                //{
                //    AdminSetup ad = new AdminSetup();
                //    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                //    if (dsDesig.Tables[0].Rows.Count > 0)
                //    {

                //        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                //        {
                //            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                //            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                //            DateTime dt = DateTime.Now;
                //            int day = dt.Day;
                //            int curr_month = dt.Month;
                //            int curr_year = dt.Year;

                //            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                //            {

                //                var now = DateTime.Now;
                //                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                //                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                //                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                //                if (startdate <= day && enddate >= day)
                //                {
                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                    DataSet dsReject = new DataSet();


                //                    if (tp_month4 == curr_month && from_year2 == curr_year)
                //                    {
                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                            if (isRepSt == false)
                //                            {
                //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                            }
                //                            else
                //                            {

                //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                //                                if (isRepSt == false)
                //                                {
                //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                    //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                    if (iReturn > 0)
                //                                    {
                //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {

                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                //                            if (isRepSt == false)
                //                            {
                //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                if (iReturn > 0)
                //                                {
                //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                //                                }
                //                            }
                //                        }

                //                    }
                //                }
                //                else if (enddate <= DaysInMonth && enddate <= day)
                //                {
                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                    DataSet dsReject = new DataSet();

                //                    if (tp_month4 == curr_month && from_year2 == curr_year)
                //                    {
                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                            if (isRepSt == false)
                //                            {
                //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                            }
                //                            else
                //                            {
                //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                //                                if (isRepSt == false)
                //                                {
                //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                    if (iReturn > 0)
                //                                    {
                //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                        if (dsReject.Tables[0].Rows.Count > 0)
                //                        {
                //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                        }
                //                        else
                //                        {

                //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                //                            if (isRepSt == false)
                //                            {
                //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                //                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                if (iReturn > 0)
                //                                {
                //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                //                                }
                //                            }
                //                        }
                //                    }

                //                }
                //                else
                //                {

                //                    var today = DateTime.Today;
                //                    var month = new DateTime(today.Year, today.Month, 1);
                //                    var first = month.AddMonths(-1);
                //                    var last_month_date = month.AddDays(-1);

                //                    int iReturn = -1; TP_New tp_new = new TP_New();
                //                    bool isRepSt = false;
                //                    DataSet dsReject = new DataSet();

                //                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                //                    if (dsReject.Tables[0].Rows.Count > 0)
                //                    {
                //                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                //                    }
                //                    else
                //                    {

                //                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                //                        if (isRepSt == false)
                //                        {

                //                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                //                            if (tp_month4 == curr_month && from_year2 == curr_year)
                //                            {
                //                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                            }
                //                            else
                //                            {
                //                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                //                                if (iReturn > 0)
                //                                {
                //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                //                                }
                //                            }
                //                        }
                //                    }

                //                }


                //            }

                //        }


                //    }


                //}

                #endregion



                DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
                string sMonthName1 = dtDate1.ToString();

                string tp_month = dtDate1.ToString("MMM");

                string tp_month1 = dtDate1.ToString();

                string[] tp_month3 = tp_month1.Split('/');

                tp_month4 = Convert.ToInt32(tp_month3[1]);
                from_year = tp_month3[2];
                from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


                string next_month1 = DateTime.Now.AddMonths(1).ToString();

                string[] next_month2 = next_month1.Split('/');

                next_month4 = Convert.ToInt32(next_month2[1]);
                to_year = next_month2[2];
                to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

                string Current_month = DateTime.Now.Month.ToString("MMM");
                string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-1);
                var last_month_date = month.AddDays(-1);



                var threemth_back = month.AddMonths(-3);
                DateTime three_month_ago = Convert.ToDateTime(threemth_back);
                DateTime three_mnth_ago_lst_date = new DateTime(three_month_ago.Year, three_month_ago.Month, 1).AddMonths(1).AddDays(-1);

                var twomth_back = month.AddMonths(-2);
                DateTime two_month_ago = Convert.ToDateTime(twomth_back);
                DateTime two_mnth_ago_lst_date = new DateTime(two_month_ago.Year, two_month_ago.Month, 1).AddMonths(1).AddDays(-1);

                int two_mnt = two_mnth_ago_lst_date.Month;
                int tw_year = two_mnth_ago_lst_date.Year;

                int one_mnt = last_month_date.Month;
                int onemth_year = last_month_date.Year;


                dsTp = adm.chk_tpbasedsystem_MGR(div_code);

                //if (dsTp.Tables[0].Rows.Count > 0)
                //{
                if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                {
                    AdminSetup ad = new AdminSetup();
                    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                    if (dsDesig.Tables[0].Rows.Count > 0)
                    {

                        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                        {
                            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                            DateTime dt = DateTime.Now;
                            int day = dt.Day;
                            int curr_month = dt.Month;
                            int curr_year = dt.Year;

                            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                            {

                                var now = DateTime.Now;
                                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                                if (startdate <= day && enddate >= day)//range
                                {
                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;

                                    DataSet dsReject = new DataSet();


                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }


                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {

                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                        }
                                        else
                                        {

                                            //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                            //if (dsReject.Tables[0].Rows.Count > 0)
                                            //{
                                            //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                                            //}
                                            //else
                                            //{


                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                        }
                                                    }
                                                }
                                           // }

                                        }
                                    }
                                    else if (tp_month4 == curr_month && from_year2 == curr_year)
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp


                                        if (isRepSt == false)
                                        {
                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                        }
                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                }
                                            }
                                        }

                                    }

                                    else
                                    {

                                        //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        //if (dsReject.Tables[0].Rows.Count > 0)
                                        //{
                                        //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        //}
                                        //else
                                        //{

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                }
                                            }
                                       // }

                                    }
                                }//end of range
                                else if (enddate <= DaysInMonth && enddate <= day)
                                {
                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;
                                    DataSet dsReject = new DataSet();


                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {

                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                        string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                        }
                                        else
                                        {

                                            //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                            //if (dsReject.Tables[0].Rows.Count > 0)
                                            //{
                                            //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                            //}
                                            //else
                                            //{

                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {

                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                    if (isRepSt == false)
                                                    {
                                                        string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                        string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                        }
                                                    }
                                                }
                                           // }

                                        }
                                    }

                                    else
                                    {

                                        //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        //if (dsReject.Tables[0].Rows.Count > 0)
                                        //{
                                        //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        //}
                                        //else
                                        //{

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                            if (isRepSt == false)
                                            {
                                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                }
                                            }
                                      //  }
                                    }


                                }
                                else
                                {



                                    int iReturn = -1; TP_New tp_new = new TP_New();
                                    bool isRepSt = false;
                                    DataSet dsReject = new DataSet();

                                    //dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    //if (dsReject.Tables[0].Rows.Count > 0)
                                    //{
                                    //    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    //}

                                    //else
                                    //{

                                        ///start here for check last two months check
                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                                }

                                            }

                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                                if (isRepSt == false)
                                                {

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                    string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                    }
                                                }
                                                else
                                                {
                                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                    if (isRepSt == false)
                                                    {
                                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                        string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }

                                            }

                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                                if (isRepSt == false)
                                                {

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    if (tp_month4 == curr_month && from_year2 == curr_year)
                                                    {
                                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                    else
                                                    {
                                                        string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                        iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                        if (iReturn > 0)
                                                        {
                                                            //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                if (tp_month4 == curr_month && from_year2 == curr_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                                else
                                                {
                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                   // }
                                    //end here


                                }


                            }

                        }



                    }


                }

                //  Response.Redirect("Sale_Dashboard.aspx");
                AdminSetup admquiz = new AdminSetup();
                dsquiz = admquiz.Get_Quiz_Process(Session["div_code"].ToString(), Session["sf_code"].ToString());
                if (dsquiz.Tables[0].Rows.Count > 0)
                {
                    if (dsquiz.Tables[0].Rows[0]["res"].ToString() == "1" || dsquiz.Tables[0].Rows[0]["res"].ToString() == null || dsquiz.Tables[0].Rows[0]["res"].ToString() == "")
                    {
                        Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["res"].ToString() + "");
                    }
                }
                if (Count != 0)
                {
                    //System.Threading.Thread.Sleep(time);
                    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
                }
                //else if (dsImage.Tables[0].Rows.Count > 0)
                //{
                //    Response.Redirect("HomePage_Image.aspx");
                //}
                //else if (dsImage_FF.Tables[0].Rows.Count > 0)
                //{
                //    Server.Transfer("HomePage_FieldForcewise.aspx");

                //}
                else if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("Quote_Design.aspx");

                }
                else if (dsAdmNB.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("NoticeBoard_design.aspx");

                }
                else if (dsAdm.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("FlashNews_Design.aspx");
                }

                else if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("Birthday_Wish.aspx");
                }

                else
                {
                    Server.Transfer("~/Default_MGR.aspx");
                }
            }

            else
            {
                //Server.Transfer("Default.aspx");
                Server.Transfer("Default_admin.aspx");
            }
        }
    }

}