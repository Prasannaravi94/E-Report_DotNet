using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    DataSet dsDivision = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsAdmin1 = null;
    DataSet dsdiv = null;
    DataSet dsrep = new DataSet();
    public DataSet dsDoc;

    DataSet dsFlash = null;
    //added by sri for HO Users
    string sf_type = string.Empty;
    string HO_ID = string.Empty;
    string division_code = string.Empty;
    string div_codeadm = string.Empty;
    string Flash = string.Empty;
    string FlashNews = string.Empty;
    int time;
    string Support = string.Empty;
    string SupportName = string.Empty;
    int div_code;
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        //added by sri for HO Users
        sf_type = Session["sf_type"].ToString();

        HO_ID = Session["HO_ID"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            if (division_code == "14,")
            {
                joinee_kit();
                Recmmd();
                //linkjoinee.Visible = true;
                //linkjoin.Visible = true;
                //lblrecmd.Visible = true;
                //lblre.Visible = true;
                car_service();
                //linkService.Visible = true;
                //lblcar.Visible = true;

                Asse_MR();
                //lblMR.Visible = true;
                //lblMRIn.Visible = true;
                Asse_MGR();
                //lblMGR.Visible = true;
                //lblMGRIn.Visible = true;

                imagedatecar();
                JoineekitDate();
                RmdDate();
                Asse_MRDate();
                Asse_MGRDate();
                newMsg.Visible = true;

            }
            else
            {
                //linkjoinee.Visible = false;
                //linkjoin.Visible = false;
                //lblrecmd.Visible = false;
                //lblre.Visible = false;
                //lblService.Visible = false;
                //linkService.Visible = false;
                //lblcar.Visible = false;

                //lblMR.Visible = false;
                //lblMRIn.Visible = false;
                //lblMGR.Visible = false;
                //lblMGRIn.Visible = false;
                newMsg.Visible = false;
            }



            //  btnSelect.Focus();
            // MailCount();
            FillDivision();
            LoadMailCount();
            //  menu.FindControl("pnlHeader").Visible = false;
            div.SelectedIndex = 0;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            string script = "$(document).ready(function () { $('[id*= menu]').click(); });";
            //  ddldivision_DataBound(sender, e);
            gettalk();
            AdminSetup adm1 = new AdminSetup();
            Division dv = new Division();
            string[] strDivSplit1 = division_code.Split(',');
            foreach (string strdiv1 in strDivSplit1)
            {
                if (strdiv1 != "")
                {
                    dsFlash = adm1.Get_Flash_News_adm(strdiv1);

                    if (dsFlash.Tables[0].Rows.Count > 0)
                    {
                        dsdiv = dv.getDivisionHO(strdiv1);
                        Flash = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() + " ---> " + dsFlash.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        FlashNews = Flash;
                        lblFlash.InnerText += FlashNews + ". ";
                    }

                }
            }
            //if (Session["Sub_HO_ID"].ToString() != "0")
            //{
            //    if (Session["AdminDashHome"].ToString() == "Dash")
            //    {
            //        Session["AdminDashHome"] = "";
            //        //btndash_Click(sender, e);
            //    }
            //}
        }
    }

    private void FillDivision()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    if (dsdiv.Tables[0].Rows.Count > 0)
                    {
                        ListItem liTerr = new ListItem();
                        liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        div.Items.Add(liTerr);

                        ListItem lidiv = new ListItem();
                        lidiv.Value = dsdiv.Tables[0].Rows[0]["Division_Code"].ToString();
                        lidiv.Text = dsdiv.Tables[0].Rows[0]["Division_SName"].ToString();
                        ddldivision.Items.Add(lidiv);
                    }
                }
            }
        }
        else if (sf_type == "" || sf_type == null)
        {
            dsDivision = dv.getDivision_list();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                div.DataValueField = "Division_Code";
                div.DataTextField = "Division_Name";
                div.SelectedIndex = 0;

                div.DataSource = dsDivision;
                div.DataBind();
                //  btnSelect.Visible = false;
                //   lblenter.Visible = false;
            }
        }
    }
    private void gettalk()
    {
        AdminSetup adm = new AdminSetup();
        string[] strDivSplit = division_code.Split(',');
        foreach (string strdiv in strDivSplit)
        {
            if (strdiv != "")
            {
                dsAdmin = adm.Get_talktous(strdiv);

                if (dsAdmin.Tables[0].Rows.Count > 0)
                {
                    Support = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    SupportName = Support;
                    //  lblSupport.Text += SupportName + "&nbsp;&nbsp;| &nbsp; &nbsp;";
                }

            }
        }
    }
    private void MailCount()
    {
        AdminSetup ad = new AdminSetup();
        DataSet dsMailCount = new DataSet();

        int count;
        count = ad.get_MailCount(division_code, HO_ID);
        if (count != 0)
        {
            //    lblNoMail.Visible = false;
            //    LnkNoMail.Visible = true;
            //    lblimg.Visible = true;
            //    LnkNoMail.Text = "You have " + count + " New Mail(s) in your Mail Box";
        }
        else
        {
            // lblNoMail.Visible = true;
        }
    }
    //protected void ddldivision_DataBound(object sender, EventArgs e)
    //{


    //    ListBox list = sender as ListBox;

    //    if (list != null)
    //    {

    //        foreach (ListItem li in list.Items)
    //        {
    //            Division dv1 = new Division();
    //            dsDivision = dv1.Division_State(Convert.ToInt16(li.Value));              
    //            li.Attributes["title"] = dsDivision.Tables[0].Rows[0]["statename"].ToString();


    //        }

    //    }

    //}
    protected void Page_PreRender(object sender, EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // string[] str = dd1division.SelectedItem.Text.ToString().Split('.');
        Session["div_code"] = div.SelectedItem.Value.ToString();
        Session["div_name"] = div.SelectedItem.Text.ToString();

        Server.Transfer("Admin_Dashboard.aspx");
    }
    protected void btndash_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        // string[] str = dd1division.SelectedItem.Text.ToString().Split('.');
        Session["div_code"] = div.SelectedItem.Value.ToString();
        Session["div_name"] = div.SelectedItem.Text.ToString();
        Session["Div_color"] = GetbgColor(div.SelectedItem.Value.ToString());
        // Server.Transfer("BasicMaster.aspx");
        Server.Transfer("BasicMaster.aspx");
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
            return GetDiv_Code = dt.Rows[0]["div_color"].ToString();
        }
    }
    //protected void ddldivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadMailCount();
    //}
    private void LoadMailCount()
    {
        AdminSetup ads = new AdminSetup();

        DataSet unreadcount = ads.get_UnreadMailCount(ddldivision.SelectedItem.Value, HO_ID);
        lblunread.Text = unreadcount.Tables[0].Rows[0]["Mail_Count"].ToString();

        DataSet Totalcount = ads.get_TotalMailCount(ddldivision.SelectedItem.Value, HO_ID);
        lblInbox.Text = Totalcount.Tables[0].Rows[0]["Mail_Count"].ToString();

    }

    private void Recmmd()
    {

        Rep rr = new Rep();
        division_code = division_code.TrimEnd(',');
        dsrep = rr.getrecmdcnt(division_code);

        if (dsrep.Tables[0].Rows.Count > 0)
        {
            linkrecommend.Text = dsrep.Tables[0].Rows[0]["cnt"].ToString();
        }

    }
    private void joinee_kit()
    {

        Rep rr = new Rep();
        division_code = division_code.TrimEnd(',');
        dsrep = rr.gettraineekitcnt(division_code);

        if (dsrep.Tables[0].Rows.Count > 0)
        {
            linkjoincnt.Text = dsrep.Tables[0].Rows[0]["cnt"].ToString();
        }

    }
    private void car_service()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.GetCount(division_code);

        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            linkService.Text = dsDoc.Tables[0].Rows[0]["cnt"].ToString();
        }
    }

    private void Asse_MR()
    {
        SalesForce rr = new SalesForce();
        division_code = division_code.TrimEnd(',');
        DataSet dsMR = rr.getMR_Cnt(division_code);

        if (dsMR.Tables[0].Rows.Count > 0)
        {
            linkAssMRcnt.Text = dsMR.Tables[0].Rows[0]["cnt"].ToString();
        }
    }

    private void Asse_MGR()
    {
        SalesForce rr = new SalesForce();
        division_code = division_code.TrimEnd(',');
        DataSet dsMR = rr.getMGR_Cnt(division_code);

        if (dsMR.Tables[0].Rows.Count > 0)
        {
            linkAssMGRcnt.Text = dsMR.Tables[0].Rows[0]["cnt"].ToString();
        }

    }


    private void JoineekitDate()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.GetJoineekitDate(division_code);

        if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
        {

            Image1.Visible = false;
        }
        else
        {
            Image1.Visible = true;
        }
    }

    private void RmdDate()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.GetRmdDate(division_code);

        if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
        {

            Image2.Visible = false;
        }
        else
        {
            Image2.Visible = true;
        }
    }
    private void imagedatecar()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.Getimagedatecar(division_code);

        if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
        {

            Image3.Visible = false;
        }
        else
        {
            Image3.Visible = true;
        }

    }
    private void Asse_MRDate()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.GetAsse_MRDate(division_code);

        if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
        {

            Image4.Visible = false;
        }
        else
        {
            Image4.Visible = true;
        }
    }
    private void Asse_MGRDate()
    {
        ListedDR LstDoc = new ListedDR();
        division_code = division_code.TrimEnd(',');
        dsDoc = LstDoc.GetAsse_MGRDate(division_code);

        if (dsDoc.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
        {

            Image5.Visible = false;
        }
        else
        {
            Image5.Visible = true;
        }
    }


    protected void ddldivision_SelectedIndexChanged1(object sender, EventArgs e)
    {
        LoadMailCount();
    }
    protected void lblunread_Click(object sender, EventArgs e)
    {
        string divCode = ddldivision.SelectedItem.Value;

        Response.Redirect("MasterFiles/Mails/Mail_Head.aspx?div_Code=" + divCode);

    }

    protected void lblInbox_Click(object sender, EventArgs e)
    {
        string divCode = ddldivision.SelectedItem.Value;

        Response.Redirect("MasterFiles/Mails/Mail_Head.aspx?div_Code=" + divCode);
    }
}