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


public partial class UserControl_MGR_TP_Menu : System.Web.UI.UserControl
{
      string _isErr;
    string _sURL;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string div_code = string.Empty;
    string menu_name = string.Empty;
    DataSet dsdiv = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        div_code = Session["div_code"].ToString();
        if (Session["div_name"] != null)
        {
            lblHeading.Text = Session["div_name"].ToString();
        }
        if (!Page.IsPostBack)
        {

          //  ServerStartTime = DateTime.Now;
         //   base.OnPreRender(e);
            //Session["Reset"] = true;
            //Configuration config = WebConfigurationManager.AppSettings.Get("");
            //Configuration conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //SessionStateSection section = (SessionStateSection)conf.GetSection("system.web/sessionState");
            //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
            //string script = "<script language='JavaScript'>SessionExpireAlert(" + timeout + ");</script>";
            //Page.RegisterStartupScript("myscript", script);

            Division div = new Division();
            dsdiv = div.getLogo(div_code);

            if (dsdiv.Tables[0].Rows.Count > 0)
            {
                img.Src = dsdiv.Tables[0].Rows[0]["div_logo"].ToString();
            }

            DataSet dsTerritory = new DataSet();
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                //menu1.Title = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"] + " " + "List";
               // lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                lblRoute.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
            }
         

            DataSet ds = new DataSet();
            UserLogin ul = new UserLogin();
            ds = ul.MGR_Menu_View_Div(Session["div_code"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                menu_name = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            string[] menu;

            menu = menu_name.Split(',');
            foreach (string st in menu)
            {
                validateMenuItem(ul_MGRInf, st);
                validateMenuItem(ul_Act, st);
                validateMenuItem(ul_Mis, st);
                validateMenuItem(ul_Opt, st);
            }
            li8tpdev.Visible = false;
            lib1sst.Visible = true;
            lib2ppt.Visible = true;
            liStck.Visible = true;
            if (Session["Designation_Short_Name"].ToString() == "RBM" || Session["Designation_Short_Name"].ToString() == "SRBM")
            {

                Likpikra.Visible = true;


            }
            if (Session["Designation_Short_Name"].ToString() == "DBM" || Session["Designation_Short_Name"].ToString() == "SDBM" || Session["Designation_Short_Name"].ToString() == "Sr.DBM" || Session["Designation_Short_Name"].ToString() == "Sr DGM" || Session["Designation_Short_Name"].ToString() == "Sr AGM" || Session["Designation_Short_Name"].ToString() == "DGM" || Session["Designation_Short_Name"].ToString() == "AGM" || Session["Designation_Short_Name"].ToString() == "PTM" || Session["Designation_Short_Name"].ToString() == "PTM ONE" || Session["Designation_Short_Name"].ToString() == "CSMO" || Session["Designation_Short_Name"].ToString() == "GM" || Session["Designation_Short_Name"].ToString() == "ZBM" || Session["Designation_Short_Name"].ToString() == "DYDBM" || Session["Designation_Short_Name"].ToString() == "KAM" || Session["Designation_Short_Name"].ToString() == "HSM")
            {

                Likpikradbm.Visible = true;


            }
            if (Session["Designation_Short_Name"].ToString() == "SM")
            {
                Likpikrasm.Visible = true;
            }
            if (div_code == "28")
            {
                liStck.Visible = true;
            }
            if (div_code == "14")
            {
                Lispecial.Visible = true;
            }
            else
            {
                Lispecial.Visible = false;
            }
            if (div_code == "3" || div_code == "4")
            {
                //LiDrPotienal.Visible = true;
                //ululDocBus.Visible = true;
                liA7.Visible = false;
            }
            if (div_code == "35" || div_code == "47" || div_code == "46")
            {
               
                liMgrOpt8.Visible = false;
                //liaimil123.Visible = true;

            }
            if (div_code == "65")
            {
                lib1sst.Visible = true;
                lib2ppt.Visible = true;
            }
            if (div_code == "3")
            {
                liMgrAct8.Visible = false;
            }
            //if (div_code != "2")
            //{
            //    liIn7.Visible = false;
            //    liIn8.Visible = false;

            //}
            //if (div_code == "8" || div_code == "9" || div_code == "10")
            //{
            //    liMgrAct8.Visible = false;
            //}
            DataTable DCExp = new DataTable();
            Distance_calculation Exp = new Distance_calculation();
            DCExp = Exp.MGRstpCntMode(div_code, Session["Designation_Short_Name"].ToString());
            DataTable DCExp1 = new DataTable();
            DCExp1 = Exp.osCalcRwwisestpCnt1(div_code);
            if (div_code != "104")
            {
                if (DCExp.Rows.Count > 0)
            {
                if ("1".Equals(DCExp1.Rows[0]["Row_wise_textbox"].ToString()) && "A".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    liMGRRWTxt.Visible = true;
                }
                else if ("A".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    liaimil123.Visible = true;
                }
                else if ("SA".Equals(DCExp.Rows[0]["Designation_Mode"].ToString()))
                {
                    liSemiAuto.Visible = true;
                }
                else
                {
                    lioths.Visible = true;
                }
            }
            else
            {
                lioths.Visible = true;
            }
             }


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
            //lblStatus.Text = value;
            //if (_isErr == "error")
            //    lblStatus.ForeColor = System.Drawing.Color.Red;
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
        Response.Redirect(Session["backurl"].ToString());
    }
  
}
