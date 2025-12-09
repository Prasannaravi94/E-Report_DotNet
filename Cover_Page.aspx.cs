using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class Cover_Page : System.Web.UI.Page
{
    string div_code = string.Empty;
    string res = string.Empty;
    string sf_code = string.Empty;
    DataSet dsFile = new DataSet();
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        res = Request.QueryString["res"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            LblUser.Text = " " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();
            if (res == "1")
            {
                pnlfinal.Visible = true;
               
            }
            if (res == "" || res == null)
            {
                pnlfirst.Visible = true;
            }

          //  btnimg.Visible = false;
        //    if (div_code == "1")
        //    {
        //        A1.Visible = true;
        //        A2.Visible = false;
        //    }
        //    else
        //    {
        //        A1.Visible = false;
        //        A2.Visible = true;
        //    }
            AdminSetup div = new AdminSetup();
            dsFile = div.get_FileQuiz(div_code);

            if (dsFile.Tables[0].Rows.Count > 0)
            {
                string str = "Files_1/(" + dsFile.Tables[0].Rows[0]["Effective_Date"].ToString() + ")";
                string FileName = dsFile.Tables[0].Rows[0]["filepath"].ToString();
                string NewFile = FileName.Replace(str, "");
                A1.HRef = "~/MasterFiles/Options/Files/" + NewFile;
            }
        }
        if (res == "1")
        {
            pnlfinal.Visible = true;
            btnimg.Visible = true;

        }
        if (res == "" || res == null)
        {
            pnlfirst.Visible = true;
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 
    protected void btnimg_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string survey = Request.QueryString["Survey_Id"].ToString();
        Response.Redirect("HomePageQuiz.aspx?Survey_Id=" + survey + "");
      //  Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + "");
    }
    protected void OnClick_ShCut(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        if (sf_code.StartsWith("MR"))
        {
            Response.Redirect("~/Default_MR.aspx");
        }
        else if (sf_code.StartsWith("MGR"))
        {
            Response.Redirect("~/Default_MGR.aspx");
        }
    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        //if (div_code == "1")     
        //{                     
            btnimg.Visible = true;
            A1.Visible = true;
        //    A2.Visible = false;
        //}

        //else
        //{
        //    btnimg.Visible = true;
        //    A1.Visible = false;
        //    A2.Visible = true;

        //}
      
    }
}