using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
using System.Configuration;

public partial class MasterFiles_Survey_Survey_Preview : System.Web.UI.Page
{
    public SqlConnection con;
    public SqlCommand com;
    string constr;
    string div_code = string.Empty;
    static string divcode = string.Empty;
    string Process_type = string.Empty;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
   
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
            LoadSurveyQuestions();


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
       // SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID,Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
         //                                          " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
          //                                         " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type " +
          //                                         " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + div_code + "' " +
          //                                         "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);


        SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID, Survey_Title, S.Creation_Date as dt, count(D.Question_Id) as Survey_No_Question," +
                                                    "convert(varchar(10), Effective_From_Date, 103) as From_date, convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id," +
                                                    "Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date, cast(D.Question_Id as int) as id, Processing_Type as Process_Type " +
                                                    "from Mas_Question_Survey_Creation_Head S, Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where S.Division_Code = '" + div_code + "' " +
                                                    "and S.Active_Flag = 0 and S.Survey_ID = D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id " +
                                                    "group by S.Survey_ID, Survey_Title, s.Creation_Date, Effective_From_Date, Effective_To_Date, Control_Id, Control_Name, Control_Para, " +
                                                    "Question_Name, Question_Add_Names, q.Creation_Date, D.Question_Id, Processing_Type ", con);


      DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);
        //GridView1.DataSource = dsSurveyQuest;
        //GridView1.DataBind();
        if (dsSurveyQuest.Tables[0].Rows.Count > 0)
        {
            lbltitle.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_Title"].ToString();
            lblefffrom.Text = dsSurveyQuest.Tables[0].Rows[0]["From_date"].ToString();
            lbleffto.Text = dsSurveyQuest.Tables[0].Rows[0]["To_Date"].ToString();
            lblcrt.Text = dsSurveyQuest.Tables[0].Rows[0]["dt"].ToString();
            lblques.Text = dsSurveyQuest.Tables[0].Rows[0]["Survey_No_Question"].ToString();
           
            GridView1.DataSource = dsSurveyQuest;
            GridView1.DataBind();
            foreach (GridViewRow gridRow in GridView1.Rows)
            {

                string Ques_id = GridView1.Rows[gridRow.RowIndex].Cells[1].Text;
                CheckBoxList chkpro = (CheckBoxList)gridRow.Cells[1].FindControl("chkpro");
                Process_type = "";


                SqlCommand cmdSurvey = new SqlCommand("select distinct S.Survey_ID, Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
                                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
                                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,Processing_Type " +
                                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + div_code + "' " +
                                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id and D.Question_Id ='" + Ques_id + "'", con);
                DataSet dsSurvey = new DataSet();
                daSurvey = new SqlDataAdapter(cmdSurvey);
                daSurvey.Fill(dsSurvey);

                string[] strchkstate;
                if (dsSurvey.Tables[0].Rows.Count > 0)
                {
                    string process = dsSurvey.Tables[0].Rows[0]["Processing_Type"].ToString();
                    if (process != "")
                    {
                        strchkstate = process.Split(',');
                        foreach (string chkst in strchkstate)
                        {
                            for (int iIndex = 0; iIndex < chkpro.Items.Count; iIndex++)
                            {
                                if (chkst.Trim() == chkpro.Items[iIndex].Value.Trim())
                                {
                                    chkpro.Items[iIndex].Selected = true;

                                }
                            }
                        }
                    }
                }
            }

        }
        con.Close();
        //  cmdSurveyQuest.Dispose();


    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Ques_Process.aspx");
    }
    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.DataItem != null)
            {
               // int questionId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[1]);
                string questionId = DataBinder.Eval(e.Row.DataItem, "id").ToString();
                string control = DataBinder.Eval(e.Row.DataItem, "Control").ToString();
              //  string control = ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString();
                System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlAnswerOptions");
                if (control == "Selectable - Single")
                {
                    RadioButtonList rbl = new RadioButtonList();
                    cmd = new SqlCommand("	SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data into #ques FROM   ( SELECT Question_Id, " +
                          " CAST ('<M>' + REPLACE(Question_Add_Names, ',', '</M><M>') + '</M>' AS XML) AS Data   " +
                          " FROM  Mas_Question_Creation  A  where Question_Id='" + questionId + "' " +
                          " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); " +
                          " select * from #ques where Data != '' " +
                          " drop table #ques ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else if (control.Trim() == "Selectable- Multiple")
                {
                    CheckBoxList rbl = new CheckBoxList();
                    cmd = new SqlCommand("	SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data into #ques FROM   ( SELECT Question_Id, " +
                          " CAST ('<M>' + REPLACE(Question_Add_Names, ',', '</M><M>') + '</M>' AS XML) AS Data   " +
                          " FROM  Mas_Question_Creation  A  where Question_Id='" + questionId + "' " +
                          " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); " +
                          " select * from #ques where Data != '' " +
                          " drop table #ques ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else
                {
                    System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
                    txt.TextMode = TextBoxMode.MultiLine;
                    pnl.Controls.Add(txt);

                }
               

            }
        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadSurveyQuestions();
    }

}