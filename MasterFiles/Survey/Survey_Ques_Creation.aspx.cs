using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
public partial class MasterFiles_Survey_Survey_Ques_Creation : System.Web.UI.Page
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
    static string divcode;
    int time;
    static int SlNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        divcode = Session["div_code"].ToString();

        //  surveyID = Convert.ToInt32(Request.QueryString["Survey_Id"]);

        // Session["backurl"] = "AddQuizQuestions.aspx?surveyId=" + hidSurveyId.Value + "";

        //  Session["backurl"] = "Quiz_List.aspx";

        if (!Page.IsPostBack)
        {
            //   menu1.Title = this.Page.Title;
            //if (Request.QueryString["Survey_Id"] != null)
            //{
            //    hidSurveyId.Value = Request.QueryString["Survey_Id"].ToString();

            //    Session["Survey_Id"] = hidSurveyId.Value;
            //}
            LoadQuestionType();
            LoadSurveyQuestions();
            //    FillProd();
        }
    }
    private void LoadSurveyQuestions()
    {

        // int surveyID = Convert.ToInt32(hidSurveyId.Value);

        //   int surveyID = Convert.ToInt32(Session["SurveyId"].ToString());

        SqlDataAdapter daSurveyQuest;
        SqlDataAdapter daSurvey;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmdSurveyQuest = new SqlCommand("select Question_Id as id,Control_Id,Control_Name as Control,Control_Para,Question_Name as Question,Question_Add_Names,Division_Code,Active_Flag,Creation_Date from Mas_Question_Creation where Active_Flag=0 and Division_Code='" + div_code + "' order by Creation_Date desc ", con);
        DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);

        if (dsSurveyQuest.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dsSurveyQuest;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = dsSurveyQuest;
            GridView1.DataBind();
        }
    }
    private void LoadQuestionType()
    {
        con = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        cmd = new SqlCommand("select Control_Id,Control_Name from Mas_Control_One ", con);
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        ddlQuestionType.DataSource = ds;
        ddlQuestionType.DataValueField = ds.Tables[0].Columns["Control_Id"].ColumnName;
        ddlQuestionType.DataTextField = ds.Tables[0].Columns["Control_Name"].ColumnName;

        ddlQuestionType.DataBind();
        ListItem li = new ListItem();
        li.Text = "-- Select Question Type--";
        li.Value = "0";
        ddlQuestionType.Items.Insert(0, li);
    }
    [WebMethod]
    public static string InsertQus(string cont_id, string cont_txt, string cont_para, string Ques_Name, string Ques_Add)
    {
        DataSet dssl = new DataSet();
        DataSet dsname = new DataSet();
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        {
            string Sl_No = "SELECT isnull(max(Question_Id)+1,'1') Question_Id from Mas_Question_Creation ";
            SqlCommand cmd1;
            cmd1 = new SqlCommand(Sl_No, con);
            // connection.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            da1.Fill(dssl);
            if (dssl.Tables[0].Rows.Count > 0)
            {
                SlNo = Convert.ToInt32(dssl.Tables[0].Rows[0]["Question_Id"].ToString());
            }
            SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Mas_Question_Creation] ([Question_Id],[Control_Id],[Control_Name],[Control_Para]" +
                             " ,[Question_Name],[Question_Add_Names],[Division_Code],[Active_Flag],[Creation_Date])  VALUES " +
            " ('" + SlNo + "','" + cont_id + "' ,'" + cont_txt + "','" + cont_para + "','" + Ques_Name + "','" + Ques_Add + "','" + divcode + "',0,getdate())", con);
            {
                con.Open();
                cmd.ExecuteNonQuery();
              
                return "True";

            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Deactivate")
        {
            string constr = WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(constr))
            {
                string col_sno = e.CommandArgument.ToString();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update Mas_Question_Creation set Active_Flag=1 where  Question_Id=@col_sno  and Division_Code='" + div_code + "'";
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@col_sno", col_sno));
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");

                LoadSurveyQuestions();
                con.Close();
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadSurveyQuestions();
    }
    protected void btnADD_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Ques_Process.aspx");
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Creation.aspx");
    }
}