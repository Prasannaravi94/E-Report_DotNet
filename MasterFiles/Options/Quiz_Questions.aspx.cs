using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using Bus_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;

public partial class MasterFiles_Options_Quiz_Questions : System.Web.UI.Page
{
    SqlConnection con;
    DataSet ds;
    SqlDataAdapter da;
    SqlCommand cmd;
    DataSet dsProduct = null;
    string div_code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    static int surveyID;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();


        surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);

        // Session["backurl"] = "AddQuizQuestions.aspx?surveyId=" + hidSurveyId.Value + "";

        Session["backurl"] = "Quiz_List.aspx";

        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            if (Request.QueryString["Survey_Id"] != null)
            {
                hidSurveyId.Value = Request.QueryString["Survey_Id"].ToString();

                Session["Survey_Id"] = hidSurveyId.Value;
            }
            LoadQuestionType();
            //    FillProd();
        }
        hHeading.InnerText = Page.Title;
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
    private void LoadQuestionType()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("select Question_Type_Id,Question_Type_Name from QuestionType where Division_Code='" + div_code + "' ", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlQuestionType.DataSource = ds;
        ddlQuestionType.DataValueField = ds.Tables[0].Columns["Question_Type_Id"].ColumnName;
        ddlQuestionType.DataTextField = ds.Tables[0].Columns["Question_Type_Name"].ColumnName;

        ddlQuestionType.DataBind();
        //ListItem li = new ListItem();
        //li.Text = "-- Select Question Type--";
        //li.Value = "0";
        //ddlQuestionType.Items.Insert(0, li);
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Quiz_List.aspx");
    }
}


