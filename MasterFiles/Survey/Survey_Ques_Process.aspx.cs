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
using System.Web.Configuration;
public partial class MasterFiles_Survey_Survey_Ques_Process : System.Web.UI.Page
{
    int SurveyId;
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["posted"] != null && Request.QueryString["posted"].ToString() == "1")
            {
                //   DisplayMessage("Survey Posted Successfully");
            }
            LoadSurveyList();


        }

    }
    private void LoadSurveyList()
    {
        //  int userID = Convert.ToInt32(Session["User_id"]);

        string lblProcessed;

        SqlDataAdapter daSurvey;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmdSurvey = new SqlCommand("select  Survey_ID,Survey_Title,Creation_Date,Survey_No_Question," +
                                              " convert(varchar,Effective_From_Date,103)  as From_date, " +
                                              " convert(varchar,Effective_To_Date,103)  as To_Date,Close_flag " +

                                              " from Mas_Question_Survey_Creation_Head where Division_Code='" + div_code + "' and Active_Flag=0 order by Creation_Date desc ", con);



        //sqlcommand cmdsurvey= new sqlcommand("select distinct  a.survey_id,survey_title,creation_date,count(question_id) as survey_no_question, " +
        //                                     "convert(varchar,effective_from_date,103)  as from_date, " +
        //                                     "convert(varchar,effective_to_date,103)  as to_date,close_flag from mas_question_survey_creation_head a ,mas_question_survey_creation_detail b " +
        //                                     "where a.division_code='" + div_code + "' and active_flag=0 and a.survey_id=b.survey_id " +
        //                                     "group by  a.survey_id,survey_title,creation_date,effective_from_date,effective_to_date,close_flag " +
        //                                     "order by creation_date desc ",con);

        DataSet dsSurvey = new DataSet();
        daSurvey = new SqlDataAdapter(cmdSurvey);
        daSurvey.Fill(dsSurvey);
        DataColumn dc = new DataColumn("Processed");
        dsSurvey.Tables[0].Columns.Add(dc);

        DataColumn dc1 = new DataColumn("NQues");
        dsSurvey.Tables[0].Columns.Add(dc1);

        if (dsSurvey.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < dsSurvey.Tables[0].Rows.Count; i++)
            {
                int SurveyId = Convert.ToInt32(dsSurvey.Tables[0].Rows[i]["Survey_ID"].ToString());

                string _qry = "select * from dbo.Mas_Question_Survey_Creation_Detail where Survey_ID=" + SurveyId + " and sf_code is not null";

                cmdSurvey = new SqlCommand(_qry, con);

                DataSet dsProcess = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmdSurvey);
                da.Fill(dsProcess);

                if (dsProcess.Tables[0].Rows.Count > 0)
                {
                    lblProcessed = "Processed";
                    dsSurvey.Tables[0].Rows[i]["Processed"] = lblProcessed;
                }
                else
                {
                    lblProcessed = "";
                    dsSurvey.Tables[0].Rows[i]["Processed"] = lblProcessed;
                }


                //string _Qry1 = "select distinct Survey_No_Question as NQues from dbo.Mas_Question_Survey_Creation_Head where Survey_ID=" + SurveyId + "";
                string _Qry1 = "select distinct count(Question_Id) as NQues from dbo.Mas_Question_Survey_Creation_Head a, Mas_Question_Survey_Creation_Detail b  where a.Survey_ID = b.Survey_ID  and b.Survey_ID = " + SurveyId + " ";
                cmdSurvey = new SqlCommand(_Qry1, con);

                DataSet dsNQA = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(cmdSurvey);
                da1.Fill(dsNQA);


                if (dsNQA.Tables[0].Rows.Count > 0)
                {
                    string val = dsNQA.Tables[0].Rows[0]["NQues"].ToString();

                    dsSurvey.Tables[0].Rows[i]["NQues"] = val;
                }
                else
                {
                    string val = "";
                    dsSurvey.Tables[0].Rows[i]["NQues"] = val;

                }

            }
        }

        grdSurvey.DataSource = dsSurvey;
        grdSurvey.DataBind();
        foreach (GridViewRow row in grdSurvey.Rows)
        {
            Label lblProc = (Label)row.FindControl("lblProcessed");
            HtmlGenericControl lnka = (HtmlGenericControl)row.FindControl("divdata");
            //  HtmlGenericControl divP = (HtmlGenericControl)row.FindControl("divP");
            HtmlGenericControl lnkv = (HtmlGenericControl)row.FindControl("divview");
            HtmlGenericControl lblstatus = (HtmlGenericControl)row.FindControl("div1");
            HtmlGenericControl divq = (HtmlGenericControl)row.FindControl("divq");
            //   HtmlGenericControl mydiv = (HtmlGenericControl)ro.Rows[index].FindControl("divdata");  
            Label lblclose = (Label)row.FindControl("lblclose");
            HtmlGenericControl divclose = (HtmlGenericControl)row.FindControl("divclose");
            HtmlGenericControl divClosed = (HtmlGenericControl)row.FindControl("divClosed");
            if (lblProc.Text == "Processed")
            {
                lnka.Visible = false;
                lnkv.Visible = false;
                lblstatus.Visible = true;
                // lblstatus.Attributes["class"] = "line-through";
                divq.Visible = true;
            }
            else
            {

                lblstatus.Visible = false;
                divq.Visible = false;

            }
            if (lblclose.Text == "1")
            {
                divclose.Visible = false;
                divClosed.Visible = true;
              
            }
            else
            {
                divclose.Visible = true;
                divClosed.Visible = false;
              

            }
        }

        con.Close();
        cmdSurvey.Dispose();
    }
    public void grdSurvey_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{

        //    try
        //    {
        //        int surveyId = Convert.ToInt32(e.CommandArgument);
        //        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        //        con.Open();
        //        SqlCommand cmd;
        //        cmd = new SqlCommand("delete from QuizTitleCreation where Survey_Id=@Survey_Id", con);
        //        cmd.Parameters.Add(new SqlParameter("@Survey_Id", SurveyId));
        //        cmd.ExecuteNonQuery();
        //      //  DisplayMessageAddRedirect("Selected Survey deleted successfully", "SurveyList.aspx");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        // }

        if (e.CommandName == "Deactivate")
        {

            //  int surveyId = Convert.ToInt16(e.CommandArgument);
            string constr = WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {

                string col_sno = e.CommandArgument.ToString();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update Mas_Question_Survey_Creation_Head set Active_Flag=1 where  Survey_ID=@col_sno  and Division_Code='" + div_code + "'";
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@col_sno", col_sno));
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");

                LoadSurveyList();
                con.Close();
            }

        }
        if (e.CommandName == "Close")
        {

            //  int surveyId = Convert.ToInt16(e.CommandArgument);
            string constr2 = WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr2))
            {

                string col_sno = e.CommandArgument.ToString();

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "update Mas_Question_Survey_Creation_Head set Close_flag=1 where  Survey_ID=@col_sno  and Division_Code='" + div_code + "'";
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@col_sno", col_sno));
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Closed Successfully');</script>");

                LoadSurveyList();
                con.Close();
            }


        }

    }
    public void grdSurvey_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void btnADD_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Creation.aspx");
    }
    protected void btnQues_Click(object sender, EventArgs e)
    {
        Response.Redirect("Survey_Ques_Creation.aspx");
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}