using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.Data;
using Bus_EReport;

public partial class UserControl_MR_TP_Menu : System.Web.UI.UserControl
{
    string _isErr;
    string _sURL;
    string div_code = string.Empty;
    string menu_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsTerritory = null;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
        }
        if (!Page.IsPostBack)
        {

            //ServerStartTime = DateTime.Now;
            //base.OnPreRender(e);
            //Session["Reset"] = true;
            //Configuration conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);

            //SessionStateSection section = (SessionStateSection)conf.GetSection("system.web/sessionState");
            //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            //string script = "<script language='JavaScript'>SessionExpireAlert(" + timeout + ");</script>";
            //Page.RegisterStartupScript("myscript", script);
            DataSet dsTerritory = new DataSet();
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
                lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                lblTerritory1.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }


            //if (div_code == "21")
            //{
            //    lblChange.Visible = false;
            //    liChange.Visible = false;
            //}
              DataSet ds = new DataSet();
            UserLogin ul = new UserLogin();
            ds = ul.MR_Menu_View_Div(Session["div_code"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                menu_name = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] menu;

            menu = menu_name.Split(',');
            foreach (string st in menu)
            {

                validateMenuItem(ul_Inf, st);
                validateMenuItem(ul_Act, st);
                validateMenuItem(ul_Mis, st);
                validateMenuItem(ul_Opt, st);

                //if (LiDiv.ID == st)
                //{
                //    LiDiv.Visible = false;
                //}
          

                //if (LiDiv.ID == st)
                //{
                //    LiDiv.Visible = false;
                //}
            }

            ulliEx.Visible = false;

           // ulliCamp.Visible = false;
            if (div_code == "15")
            {
                // lblChange.Visible = false;
                liMRIn14.Visible = true;
                
            }
            if (div_code == "65")
            {
                lib1sst.Visible = true;
                lib2ppt.Visible = true;
            }
            if (div_code == "28")
            {
                liStck.Visible = false;
            }

            if (div_code == "1" || div_code == "36")
            {
            
                liTpTerr.Visible = true;
                liMrAct5.Visible = false;
            }
            else
            {
                liTpTerr.Visible = false;
            }
            if(div_code == "3" || div_code == "4")
            {
                liA7.Visible = true;
            }

            if (div_code == "17")
            {
                listp.Visible = true;
                listptp.Visible = true;
                liMrAct5.Visible = false;
            }
            else
            {
                listp.Visible = false;
                listptp.Visible = false;
                liMrAct5.Visible = true;
            }

            UserLogin uu = new UserLogin();
            DataSet dstp = new DataSet();
            dstp = uu.Tp_Auto_Setup_new(div_code);

            if (dstp.Tables[0].Rows[0]["SingleDr_WithMultiplePlan_Required"].ToString() == "1")
            {
                litp_auto.Visible = true;
            }
            else
            {
                litp_auto.Visible = false;
            }

            DataTable DCExp = new DataTable();
            Distance_calculation Exp = new Distance_calculation();
            DCExp = Exp.osCalcRwwisestpCnt(div_code);
            DataTable DCExp1 = new DataTable();
            DCExp1 = Exp.osCalcRwwisestpCnt1(div_code);
            //if (div_code == "19" || div_code == "20")
            //{
            //    liMrAnthem.Visible = true;
            //}
            //else if (div_code == "34")
            //{
            //    liINDSWFT.Visible = true;
            //    //liMRRwExp.Visible = true;
            //}
            DataTable DCExp2 = new DataTable();
            DCExp2 = Exp.anthemDirectAmt(div_code);
            DataTable DCExp3 = new DataTable();
            DCExp3 = Exp.OSEXNormalLogic(div_code);
            if (div_code != "104")
            {
                if ("1".Equals(DCExp2.Rows[0]["Anthem_direct_amt"].ToString()))
                {
                    liMrAnthem.Visible = true;
                }
                else if ("1".Equals(DCExp3.Rows[0]["Normal_Expense_Logic"].ToString()))
                {
                    liINDSWFT.Visible = true;
                    //liMRRwExp.Visible = true;
                }
                else
                {
                    if ("1".Equals(DCExp1.Rows[0]["Row_wise_textbox"].ToString()) && "1".Equals(DCExp.Rows[0]["rwwise_calc"].ToString()))
                    {
                        liMRRwExp.Visible = true;
                    }
                    else if ("1".Equals(DCExp.Rows[0]["rwwise_calc"].ToString()))
                    {
                        liMRRwExp.Visible = true;
                    }
                    else
                    {
                        liMRRwExp.Visible = true;
                    }

                }
            }
            //if (div_code != "2")
            //{
            //    liunique.Visible = true;
            //    liMrAct8.Visible = false;
            //}
            //if (div_code != "2")
            //{
            //    ulliDoc.Visible = false;
            //}
            //else
            //{
            //    ulliDoc.Visible = true;
            //}
        }
    }
 private void validateMenuItem(System.Web.UI.HtmlControls.HtmlGenericControl ulMenu, string menuId)
    {
        foreach (Control element in ulMenu.Controls)
        {
            //  list.Add(element);
            if (element.ID != null)
            {
                if (element.ID == menuId || element.ID.Contains("ulli"))
                {
                    if (element.ID == menuId)
                    {
                        element.Visible = false;
                        break;
                    }
                    else
                    {
                        foreach (Control ulliMenu in FindControl(element.ID).Controls)
                        {
                            if (ulliMenu.ID != null)
                            {
                                if (ulliMenu.ID == menuId || ulliMenu.ID.Contains("ulul"))
                                {
                                    if (ulliMenu.ID == menuId)
                                    {
                                        ulliMenu.Visible = false;
                                        break;
                                    }
                                    else
                                    {
                                        foreach (Control ululMenu in FindControl(ulliMenu.ID).Controls)
                                        {
                                            if (ululMenu.ID == menuId && ululMenu.ID != null)
                                            {
                                                ululMenu.Visible = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    string _title;
    public string Title
    {
        get
        {
            return this._title;
        }
        set
        {
            this._title = value;
            lblHeading.Text = value;
        }
    }

    string _status;
    public string Status
    {
        get
        {
            return this._status;
        }
        set
        {
            this._status = value;
            lblStatus.Text = value;
            if (_isErr == "error")
                lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }


    public string isERR
    {
        get
        {
            return this._isErr;
        }
        set
        {
            this._isErr = value;
        }
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect(Session["backurl"].ToString());
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        AdminSetup adm = new AdminSetup();
        int iReturn = adm.AddQuery(ddlProb.SelectedItem.Text, txtQuery.Text, div_code, Session["sf_code"].ToString());

        if (iReturn > 0)
        {



        }
        ddlProb.SelectedIndex = -1;
        txtQuery.Text = "";


    }

  
}