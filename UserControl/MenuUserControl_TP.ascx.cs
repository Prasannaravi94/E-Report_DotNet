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

public partial class UserControl_MenuUserControl_TP : System.Web.UI.UserControl
{
    string _isErr;
    string _sURL;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string menu_name = string.Empty;
    DataSet dsdiv = new DataSet();
    DataSet dsSub = new DataSet();
    string sub_id = string.Empty;
    DataSet dsHODivision = new DataSet();
    string Menu_Name2 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        //LblUser.Text = Session["Corporate"].ToString();
        if (Session["div_name"] != null)
        {
            lbldivision.Text = Session["div_name"].ToString();

        }
        if (!Page.IsPostBack)
        {

            //ServerStartTime = DateTime.Now;
            //base.OnPreRender(e);
            //Session["Reset"] = true;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "OnClientClicking", "noBack()", true);
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
                lblTerritory.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                lblRoute.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
                lblTerr.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + " - Listed Doctor";
            }
            DataSet ds = new DataSet();
            UserLogin ul = new UserLogin();
            ds = ul.Master_Menu_View(Session["HO_ID"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                menu_name = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            UserLogin dv = new UserLogin();
            dsSub = dv.Get_Sub_Id(Session["HO_ID"].ToString());
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                sub_id = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (sub_id != "0")
            {
                dsHODivision = dv.Master_Menu_View(sub_id);
                if (dsHODivision.Tables[0].Rows.Count > 0)
                {
                    Menu_Name2 = dsHODivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }

            }
            menu_name = menu_name + Menu_Name2;
            string[] menu;

            menu = menu_name.Split(',');
            foreach (string st in menu)
            {
                validateMenuItem(ulMas, st);
                validateMenuItem(ulAct, st);
                validateMenuItem(ulActR, st);
                validateMenuItem(ulmis, st);
                validateMenuItem(ulopt, st);
            }
            liSaleDash.Visible = false;
            ulliRetail.Visible = false;

            if (div_code == "35" || div_code == "47" || div_code == "46")
            {

                liOp44.Visible = false;

            }

            if (div_code == "28")
            {
                liStck.Visible = true;
            }

            if (div_code == "27")
            {
                LocFin.Visible = true;
            }
            if (div_code == "14")
            {
                ulliSamIn.Visible = true;
                ululSamIn.Visible = true;
                liMis25.Visible = true;
                liMis26.Visible = true;
            }

            if (div_code == "17")
            {
                liTown.Visible = true;
            }
            if (div_code == "23")
            {
                Liicar.Visible = true;
            }
           else if (div_code == "104")
            {
                LiExpCib.Visible = true;
                Licibles.Visible = true;
            }
            else
            {
                Licom.Visible = true;
                LiRe.Visible = true;
                LResig.Visible = true;
            }
            if (div_code == "19" || div_code == "20")
            {
                listarea.Visible = true;
            }
            else
            {
                listarea.Visible = true;
            }
            if (div_code == "104")
            {
                lisbill.Visible = true;
                liviwbillS.Visible = true;
                
            }
            else
            {
                lisbill.Visible = false;
                liviwbillS.Visible = false;
               // Licom.Visible = true;
            }

            UserLogin uu = new UserLogin();
            DataSet dstp = new DataSet();
            dstp = uu.Tp_Auto_Setup_new(div_code);

            if (dstp.Tables[0].Rows.Count > 0)
            {

                if (dstp.Tables[0].Rows[0]["SingleDr_WithMultiplePlan_Required"].ToString() == "0")
                {
                    literrmapp.Visible = false;
                }
                else
                {
                    literrmapp.Visible = true;
                }
            }

            else
            {
                literrmapp.Visible = false;

            }
            liA19.Visible = true;
            ululTarget.Visible = true;
            //if (div_code != "2")
            //{
            //    liNewUni.Visible = true;
            //    liA1.Visible = false;
            //    liA2.Visible = false;
            //    liOp29.Visible = false;
            //    liOp30.Visible = false;
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
            //lblHeading.Text = value;
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

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Context.Session != null)
        {
            if (Session.IsNewSession)
            {
                HttpCookie newSessionIdCookie = Request.Cookies["ASP.NET_SessionId"];
                if (newSessionIdCookie != null)
                {
                    string newSessionIdCookieValue = newSessionIdCookie.Value;
                    if (newSessionIdCookieValue != string.Empty)
                    {
                        // This means Session was timed Out and New Session was started
                        Response.Redirect("~/Index.aspx");
                    }
                }
            }
        }
    }

}