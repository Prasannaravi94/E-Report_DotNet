using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomePageQuiz : System.Web.UI.Page
{
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!Page.IsPostBack)
        {
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();
        }
    }
    protected void btnimg_Click(object sender, ImageClickEventArgs e)
    {
        System.Threading.Thread.Sleep(time);
       // Response.Redirect("MasterFiles/Options/Preview_Quiz.aspx?Survey_Id=" + Request.QueryString["Survey_Id"].ToString() + "");

        Response.Redirect("MasterFiles/Options/Quiz_Test.aspx?Survey_Id=" + Request.QueryString["Survey_Id"].ToString().TrimEnd(' ') + "");
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
}