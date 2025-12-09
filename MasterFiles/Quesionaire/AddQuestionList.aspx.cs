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

public partial class MasterFiles_Quesionaire_AddQuestionList : System.Web.UI.Page
{
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    SqlConnection con;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            LoadSurveyQuestions();
        }
      
    }

    private void LoadSurveyQuestions()
    {
     
        SqlDataAdapter daSurveyQuest;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        con.Open();
        SqlCommand cmdSurveyQuest = new SqlCommand("select Question_Id,Question_Text,Question_Type,Answer_Id,Answer_Type,Answers from Trans_Questionnaire_Head where Division_code ='" + div_code + "'", con);
        DataSet dsSurveyQuest = new DataSet();
        daSurveyQuest = new SqlDataAdapter(cmdSurveyQuest);
        daSurveyQuest.Fill(dsSurveyQuest);
        grdSurveyQuestions.DataSource = dsSurveyQuest;
        grdSurveyQuestions.DataBind();
        con.Close();
        cmdSurveyQuest.Dispose();
    }

    public void grdSurveyQuestions_RowCreated(object sender, GridViewRowEventArgs e)
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
                int questionId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0]);
                int AnsId = Convert.ToInt32(((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3]);
                System.Web.UI.WebControls.Panel pnl = (System.Web.UI.WebControls.Panel)e.Row.FindControl("pnlAnswerOptions");
                if (AnsId == 1)
                {
                    RadioButtonList rbl = new RadioButtonList();
                    cmd = new SqlCommand("SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data FROM   ( SELECT Question_Id,  " +
                           " CAST ('<M>' + REPLACE(Answers, '$', '</M><M>') + '</M>' AS XML) AS Data  " +
                           " FROM  Trans_Questionnaire_Head   where Question_Id=" + questionId + " " +
                           " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else if (AnsId == 2)
                {
                    CheckBoxList rbl = new CheckBoxList();
                    cmd = new SqlCommand("SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data FROM   ( SELECT Question_Id,  " +
                          " CAST ('<M>' + REPLACE(Answers, '$', '</M><M>') + '</M>' AS XML) AS Data  " +
                          " FROM  Trans_Questionnaire_Head   where Question_Id=" + questionId + " " +
                          " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else if (AnsId == 3)
                {
                    System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
                    txt.TextMode = TextBoxMode.MultiLine;
                    pnl.Controls.Add(txt);

                }
                else if (AnsId == 4)
                {
                    DropDownList rbl = new DropDownList();
                    cmd = new SqlCommand("SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data FROM   ( SELECT Question_Id,  " +
                           " CAST ('<M>' + REPLACE(Answers, '$', '</M><M>') + '</M>' AS XML) AS Data  " +
                           " FROM  Trans_Questionnaire_Head   where Question_Id=" + questionId + " " +
                           " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else if (AnsId == 5)
                {
                    System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
                    txt.TextMode = TextBoxMode.MultiLine;
                    pnl.Controls.Add(txt);

                }
                else if (AnsId == 6)
                {
                    ListBox rbl = new ListBox();
                    rbl.SelectionMode = ListSelectionMode.Multiple;
                    cmd = new SqlCommand("SELECT A.Question_Id, Split.a.value('.', 'VARCHAR(100)') AS Data FROM   ( SELECT Question_Id,  " +
                           " CAST ('<M>' + REPLACE(Answers, '$', '</M><M>') + '</M>' AS XML) AS Data  " +
                           " FROM  Trans_Questionnaire_Head   where Question_Id=" + questionId + " " +
                           " ) AS A CROSS APPLY Data.nodes ('/M') AS Split(a); ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    rbl.DataSource = ds;
                    rbl.DataValueField = ds.Tables[0].Columns[0].ColumnName;
                    rbl.DataTextField = ds.Tables[0].Columns[1].ColumnName;
                    rbl.DataBind();
                    pnl.Controls.Add(rbl);
                }
                else if (AnsId == 7)
                {
                    Label lbl = new Label();
                    cmd = new SqlCommand("SELECT  Answers  " +
                          " FROM  Trans_Questionnaire_Head   where Question_Id=" + questionId + " " +
                          " ", con);
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    lbl.Text = ds.Tables[0].Rows[0][0].ToString();
                    pnl.Controls.Add(lbl);
                }


            }
        }
     
    }
   
    public void grdSurveyQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            try
            {
                int questionId = Convert.ToInt32(e.CommandArgument);
                SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                con.Open();
                SqlCommand cmd;
                cmd = new SqlCommand("delete from Trans_Questionnaire_Head where Question_Id=@Question_Id", con);
                cmd.Parameters.Add(new SqlParameter("@Question_Id", questionId));
                cmd.ExecuteNonQuery();


              
                DisplayMessageAddRedirect("Selected Questions deleted successfully", "AddQuestionList.aspx");

            }
            catch (Exception ex)
            {
                DisplayMessage("Error on Page");
            }
        }
    }
    public void grdSurveyQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
 
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("AddQuestions.aspx");
    }
    public void DisplayMessageAddRedirect(string message, string pageName)
    {
        string script = GetAlert(message);
        script += string.Format("location.href='{0}';", pageName);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }
    public void DisplayMessage(string message)
    {
        string script = GetAlert(message);
        ScriptManager.RegisterStartupScript(this, GetType(), "message", script, true);
    }
    private string GetAlert(string message)
    {
        return string.Format("alert(\"{0}\");", message);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/BasicMaster.aspx");
    }
}
