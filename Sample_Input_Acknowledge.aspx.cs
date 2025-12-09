using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Configuration;

public partial class MasterFiles_MR_Sample_Input_Acknowledge : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string Trans_month_year = string.Empty;
    string cs = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlDataAdapter adapt;
    DataTable dt;
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
    DataSet dsVacant = new DataSet();
    DataSet dssample = null;
    DataSet dsinput = null;
    string pwdDt = string.Empty;
    string month = string.Empty;
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
    private DataSet dsinput1;
    DataSet dsquiz = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome to " + Session["sf_name"];
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        if(!Page.IsPostBack)
        {
            //FillInput();
           GetRestriction();
            Fillsample();

            BindImage1();
            BindImage_FieldForce();
        }
    }
    private void GetRestriction()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Sam_Inp_Restrict_Days from Setup_Others where Division_Code='" + div_code+"'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if(ds.Tables[0].Rows.Count > 0)
        {
            if(ds.Tables[0].Rows[0]["Sam_Inp_Restrict_Days"].ToString() != null)
            {
                int dys = Convert.ToInt32(ds.Tables[0].Rows[0]["Sam_Inp_Restrict_Days"].ToString());
                SqlCommand cmd2 = new SqlCommand(" select  a.Trans_sl_No, Created_Date, Updated_Date, case when  DATEADD(day, "+ dys + ", Created_Date) < getdate() then 'True' else 'False' end flag, " +
                                                 " case when  DATEADD(day, "+ dys + ", Updated_Date) < getdate() then 'True' else 'False' end flag2 from [dbo].[Trans_Sample_Despatch_Head] a where " +
                                                 " a.sf_code = '"+sf_code+"' and  a.Division_Code = '"+div_code+"' and a.Trans_Year=Year(GetDate())  and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) order by Trans_Month ", con);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2);
                if(ds2.Tables[0].Rows.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0]["Updated_Date"].ToString() == "")
                    {
                        if(ds2.Tables[0].Rows[0]["flag"].ToString() == "True")
                        {
                            foreach (ListItem item in rdoyesNo.Items)
                            {
                                if ((item.Text.Trim()) == "No")
                                {
                                    item.Attributes.Add("style", "display:none;");

                                }
                            }
                        }
                    }
                    else
                    {
                        if (ds2.Tables[0].Rows[0]["flag2"].ToString() == "True")
                        {
                            foreach (ListItem item in rdoyesNo.Items)
                            {
                                if ((item.Text.Trim()) == "No")
                                {
                                    item.Attributes.Add("style", "display:none;");

                                }
                            }
                        }
                    }

                }
            }
        }
        con.Close();
    }
    private void Fillsample()
    {
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                //cmd.CommandText = " select top(1)Trans_month_year from Trans_Sample_Despatch_Head where sf_code='" + sf_code + "' and Division_Code='" + div_code + "' order by Trans_month_year desc";



                //cmd.CommandText = "select c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty,Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a, " +
                //                         "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
                //                         "where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
                //                         "and b.productc=c.Product_Code_SlNo and a.Trans_Month=Month(Getdate()) and a.Trans_Year=Year(Getdate()) and a.Division_Code='" + div_code + "'";


               // cmd.CommandText = "select  a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
               //" Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
               //"[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
               //" where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
               //" and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and (a.Trans_Month=Month(Getdate()) " +
               //" or a.Trans_Month=Month(Getdate())-1 or a.Trans_Month=Month(Getdate())+1) order by Trans_Month ";

                cmd.CommandText = "select  a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
               " Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
               "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
               " where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Qty is null " +
               " and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) and a.Trans_Year=YEAR(GETDATE()) " +
               "order by Trans_Month ";


                cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet da = new DataSet();
                    sda.Fill(da);

                    if (da.Tables[0].Rows.Count > 0)
                    {
                        string month = da.Tables[0].Rows[0]["Trans_Month"].ToString();
                        string year = da.Tables[0].Rows[0]["Trans_Year"].ToString();
                        // DateTime month;
                        if (month != "")
                        {
                           // Trans_month = Convert.ToDateTime(month);

                           // Trans_month_year = Trans_month.Month.ToString() + "-" + Trans_month.Day.ToString() + "-" + Trans_month.Year.ToString();

                            SalesForce sf = new SalesForce();
                            string strFrmMonth = sf.getMonthName(month.ToString());
                            //string strToMonth = sf.getMonthName(TMonth);
                            lblmonth.Text = strFrmMonth + " " + year.ToString();
                        }
                    }
                }
               




                //cmd.CommandText = "select * from(select *, DENSE_RANK() OVER ( ORDER BY ord ) row_no from " +
                //"(select  a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty, " +
                //"Despatch_Actual_qty,a.Trans_Month,a.Trans_Year,'1' ord from [dbo].[Trans_Sample_Despatch_Head] a, " +
                //"[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c  " +
                //"where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null  " +
                //"  and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "'  and  " +
                //"  a.Trans_Month=Month(Getdate())-1  " +
                //"  union all  " +
                //"  select  a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty, " +
                //"   Despatch_Actual_qty,a.Trans_Month,a.Trans_Year,'2' ord from [dbo].[Trans_Sample_Despatch_Head] a, " +
                //"   [dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
                //"   where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
                //"   and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "'  and a.Trans_Month=Month(Getdate()) " +

                //"  union all " +
                //"  select  a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty, " +
                //"  Despatch_Actual_qty,a.Trans_Month,a.Trans_Year,'3' ord from [dbo].[Trans_Sample_Despatch_Head] a, " +
                //"  [dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c  " +
                //"   where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
                //"  and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "'  and  a.Trans_Month=Month(Getdate())+1)as dd) as ddd where row_no=1 ";

                cmd.CommandText = "  select  a.Trans_sl_No,c.Product_Detail_Name,b.upl_sl_no,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty, " +
                "   Despatch_Actual_qty,a.Trans_Month,a.Trans_Year,'2' ord from [dbo].[Trans_Sample_Despatch_Head] a, " +
                "   [dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
                "   where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Qty is null " +
                "   and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "'  and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) and a.Trans_Year=YEAR(GETDATE())";




                cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataSet da = new DataSet();
                        sda.Fill(da);

                        if (da.Tables[0].Rows.Count > 0)
                        {
                            grdsample.DataSource = da;
                            grdsample.DataBind();
                        }
                        else
                        {
                            grdsample.DataSource = da;
                            grdsample.DataBind();
                        }
                    }
                    
                }
            }
        }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnsavesample_click(object sender, EventArgs e)
    {
        int iReturn = -1;
        int iReturn2 = -1;
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {

            using (SqlCommand cmd = new SqlCommand())
            {

               // cmd.CommandText = "select a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
               //" Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
               //"[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
               //" where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Actual_qty is null " +
               //" and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and (a.Trans_Month=Month(Getdate()) " +
               //" or a.Trans_Month=Month(Getdate())-1 or a.Trans_Month=Month(Getdate())+1) order by Trans_Month asc";


                cmd.CommandText = "select a.Trans_sl_No,c.Product_Detail_Name,c.Product_Code_SlNo,c.Product_Sale_Unit,b.Despatch_Qty," +
          " Despatch_Actual_qty,a.Trans_Month,a.Trans_Year from [dbo].[Trans_Sample_Despatch_Head] a," +
          "[dbo].[Trans_Sample_Despatch_Details] b,[dbo].[Mas_Product_Detail] c " +
          " where a.Trans_sl_No=b.Trans_sl_No  and a.sf_code='" + sf_code + "' and b.Despatch_Qty is null " +
          " and b.productc=c.Product_Code_SlNo  and  a.Division_Code='" + div_code + "' and (a.Trans_Month=Month(GetDate())-1  or a.Trans_Month=Month(GetDate()) or a.Trans_Month=Month(Getdate())+1) and a.Trans_Year=YEAR(GETDATE()) " +
          "order by Trans_Month asc";




                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataSet da = new DataSet();
                    sda.Fill(da);

                    if (da.Tables[0].Rows.Count > 0)
                    {
                        //  month = da.Tables[0].Rows[0]["Trans_month_year"].ToString();
                        month = da.Tables[0].Rows[0]["Trans_Month"].ToString();
                        // DateTime Trans_month;

                        // Trans_month = Convert.ToDateTime(month);


                        //Trans_month_year = Trans_month.Month.ToString() + "-" + Trans_month.Day.ToString() + "-" + Trans_month.Year.ToString();


                        //lblmonth.Text = Trans_month_year;
                       // lblmonth.Text = month;

                        DateTime dateTime = DateTime.UtcNow.Date;
                       
                    }
                }
            }
        }
        foreach (GridViewRow gridRow in grdsample.Rows)
        {
            int sample_qty = 0;
            int actual_qty = 0;
            int des_qty = 0;
            int sampleason = 0;
            string sample;

            AdminSetup adm = new AdminSetup();

            Label lblprdtcode = (Label)gridRow.Cells[1].FindControl("lblprdtcode");
            Label lbldespatch = (Label)gridRow.Cells[3].FindControl("lbldespatch");
            TextBox txtreceivedqty = (TextBox)gridRow.Cells[4].FindControl("txtreceivedqty");

            Label lblMnth = (Label)gridRow.Cells[5].FindControl("lblprdtcode");

           Label lblnum = (Label)gridRow.Cells[6].FindControl("lblnum");


            DataSet dssample1 = adm.getsample_AsonDate(sf_code, div_code, lblprdtcode.Text);


          
                iReturn = adm.updatesample(sf_code, div_code, lbldespatch.Text, txtreceivedqty.Text, month, lblprdtcode.Text, lblnum.Text);


                if (iReturn > 0)
                {

               if (dssample1.Tables[0].Rows.Count > 0)
                {
                    if (dssample1.Tables[0].Rows[0]["Prod_Detail_Sl_No"].ToString() == lblprdtcode.Text)
                    {
                        int tot_Sample = Convert.ToInt32(dssample1.Tables[0].Rows[0]["Sample_AsonDate"].ToString()) + Convert.ToInt32(txtreceivedqty.Text);
                        //If Already the Product There just Increment the qty as(inhand+Uploaded)
                        iReturn2 = adm.UpdateSample_AS_ON_Date(sf_code, div_code, tot_Sample, lblprdtcode.Text);
                    }
                    //sample = dssample1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    //sample_qty = Convert.ToInt32(sample);
                    //des_qty = Convert.ToInt32(lbldespatch.Text);
                    //actual_qty = Convert.ToInt32(txtreceivedqty.Text);
                    //if (sample_qty == actual_qty)
                    //{
                    //    sampleason = sample_qty;
                    //    iReturn = adm.UpdateSample_AS_ON_Date(sf_code, div_code, sampleason, lblprdtcode.Text);
                    //}
                    //else if ((sample_qty < actual_qty) || (des_qty < actual_qty))
                    //{
                    //    sampleason = sample_qty + (actual_qty - des_qty);
                    //    iReturn = adm.UpdateSample_AS_ON_Date(sf_code, div_code, sampleason, lblprdtcode.Text);
                    //}
                    //else
                    //{

                    //    sampleason = sample_qty - (des_qty - actual_qty);

                    //    iReturn = adm.UpdateSample_AS_ON_Date(sf_code, div_code, sampleason, lblprdtcode.Text);
                    //}



                }
                else
                {

                    iReturn2 = adm.InsertSample_AS_ON_Date(sf_code, div_code, txtreceivedqty.Text, lblprdtcode.Text);
                }

                if (iReturn > 0)

                {


                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update Successfully');</script>");
                }
                

               // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Update Successfully');</script>");

            }
            
            
        }
        Fillsample();
        AdminSetup adm1 = new AdminSetup();
        dssample = adm1.getsample(Session["sf_code"].ToString(), Session["div_code"].ToString());
        if (dssample.Tables[0].Rows.Count > 0)
        {
            System.Threading.Thread.Sleep(time);
            Response.Redirect("Sample_Input_Acknowledge.aspx");
        }
        else
        {
            this.btngohome_click(sender, e);
        }
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
    //private void BindImage1()
    //{
    //    div_code = Session["div_code"].ToString();

    //    if (div_code.Contains(','))
    //    {
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    }
    //    SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    //    con.Open();
    //    SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    //  DataSet dsImage = new DataSet();
    //    da.Fill(dsImage);
    //    con.Close();

    //}

    private void BindImage1()
    {
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() ", con);


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
    protected void btngohome_click(object sender, EventArgs e)
    {

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
            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            AdminSetup dv = new AdminSetup();
            dsadmn = dv.getHome_Dash_Display(div_code);

            //ListedDR LstDoc = new ListedDR();
            //dsDoc = LstDoc.getLstdDr_Wrng_CreationFFWise(sf_code, div_code);

            AdminSetup adminsa_ip = new AdminSetup();
            dsinput = adminsa_ip.getinputEntry(Session["div_code"].ToString());
            if (dsinput.Tables[0].Rows.Count > 0)
            {

                AdminSetup adm1 = new AdminSetup();
                dsinput1 = adm1.getinput(Session["sf_code"].ToString(), Session["div_code"].ToString());
                if (dsinput1.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("~/Input_Acknowlege.aspx");
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
                dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt32(Session["Designation_Code"].ToString()));
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

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

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

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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
                            else
                            {



                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();

                                dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                if (dsReject.Tables[0].Rows.Count > 0)
                                {
                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                }

                                else
                                {

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
                                }
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
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("HomePage_Image.aspx");
            //}
            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Server.Transfer("HomePage_FieldForcewise.aspx");

            //}
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
            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

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


            dsTp = adm.chk_tpbasedsystem_MGR(div_code);

            //if (dsTp.Tables[0].Rows.Count > 0)
            //{
            if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            {
                AdminSetup ad = new AdminSetup();
                dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt32(Session["Designation_Code"].ToString()));
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

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

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

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
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
                            else
                            {



                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();

                                dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                if (dsReject.Tables[0].Rows.Count > 0)
                                {
                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                }

                                else
                                {

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
                                }
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
            else if (dsImage.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("HomePage_Image.aspx");
            }
            else if (dsImage_FF.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("HomePage_FieldForcewise.aspx");

            }
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

    protected void rdoyesNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoyesNo.SelectedValue == "1")
        {
            rdoyesNo.Visible = false;
            btnsavesample.Visible = true;
            btnHome.Visible = false;
        }
        if (rdoyesNo.SelectedValue == "2")
        {
            if (sf_code.Contains("MR"))
            {
                Response.Redirect("Default_MR.aspx");
            }
            else
            {
                Response.Redirect("MGR_dashboard.aspx");
            }
            //this.btngohome_click(sender, e);
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

}
   
