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


public partial class MasterFiles_Survey_Survey_Process_Screen : System.Web.UI.Page
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
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    DataSet dsDesig = new DataSet();
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    string map_sfcode = string.Empty;
    string map_cat = string.Empty;
    string map_spec = string.Empty;
    string map_cls = string.Empty;
    string map_chem = string.Empty;
    string map_hos = string.Empty;
    string map_stk = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!IsPostBack)
        {
          

            Get_State();
            FillSub_Division();
            FillSF();
            FillCat();
            FillSpec();
            FillCls();
            FillChem();
            FillStock();
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
        //SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID,Survey_Title, s.Creation_Date as  dt,Survey_No_Question, " +
        //                                           " convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date, Control_Id, " +
        //                                           " Control_Name as Control, Control_Para, Question_Name as Question, Question_Add_Names, q.Creation_Date as Date,cast(D.Question_Id as int) as id,Processing_Type as Process_Type,SF_Code, " +
        //                                           " Doctor_Category,Doctor_Speclty,Doctor_Cls,Hospital_Class,Chemist_Category,Stockist_State,Stockist_HQ" +
        //                                           " from Mas_Question_Survey_Creation_Head S,Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + div_code + "' " +
        //                                           "  and S.Active_Flag = 0 and S.Survey_ID =D.Survey_ID and S.Survey_ID = '" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id ", con);
        SqlCommand cmdSurveyQuest = new SqlCommand("select distinct S.Survey_ID,Survey_Title, s.Creation_Date as dt,count(d.Question_Id) as Survey_No_Question, " +
                                                    "convert(varchar(10), Effective_From_Date, 103) as From_date,convert(varchar(10), Effective_To_Date, 103) as To_Date,SF_Code, " +
                                                    "Doctor_Category,Doctor_Speclty,Doctor_Cls,Hospital_Class,Chemist_Category,Stockist_State,Stockist_HQ " +
                                                    "from Mas_Question_Survey_Creation_Head S, Mas_Question_Survey_Creation_Detail D, Mas_Question_Creation q where s.Division_Code = '" + div_code + "' " +
                                                    "and S.Active_Flag = 0 and S.Survey_ID = D.Survey_ID and D.Survey_ID ='" + Request.QueryString["Survey_Id"].ToString() + "' and D.Question_Id = q.Question_Id " +
                                                    "group by S.Survey_ID,Survey_Title,s.Creation_Date,Effective_From_Date,Effective_To_Date, SF_Code,Doctor_Category,Doctor_Speclty," +
                                                    "Doctor_Cls,Hospital_Class,Chemist_Category,Stockist_State,Stockist_HQ", con);



      
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

            string mSF_Code = dsSurveyQuest.Tables[0].Rows[0]["SF_Code"].ToString();
            string Doctor_Category = dsSurveyQuest.Tables[0].Rows[0]["Doctor_Category"].ToString();
            string Doctor_Speclty = dsSurveyQuest.Tables[0].Rows[0]["Doctor_Speclty"].ToString();
            string Doctor_Cls = dsSurveyQuest.Tables[0].Rows[0]["Doctor_Cls"].ToString();
            string Hospital_Class = dsSurveyQuest.Tables[0].Rows[0]["Hospital_Class"].ToString();
            string Chemist_Category = dsSurveyQuest.Tables[0].Rows[0]["Chemist_Category"].ToString();
            string Stockist_HQ = dsSurveyQuest.Tables[0].Rows[0]["Stockist_HQ"].ToString();
            if (mSF_Code != null)
            {
                string[] strStateSplit = mSF_Code.Split(',');
                foreach (string strstate in strStateSplit)
                {

                    for (int iIndex = 0; iIndex < lstSf.Items.Count; iIndex++)
                    {
                        if (strstate.Trim() == lstSf.Items[iIndex].Value.Trim())
                        {
                            lstSf.Items[iIndex].Selected = true;

                        }
                    }

                }
            }


            if (Doctor_Category != null)
            {
                string[] strcatSplit = Doctor_Category.Split(',');
                foreach (string strcat in strcatSplit)
                {

                    for (int iIndex = 0; iIndex < lstCat.Items.Count; iIndex++)
                    {
                        if (strcat.Trim() == lstCat.Items[iIndex].Value.Trim())
                        {
                            lstCat.Items[iIndex].Selected = true;

                        }
                    }

                }
            }
            if (Doctor_Speclty != null)
            {
                string[] strspecSplit = Doctor_Speclty.Split(',');
                foreach (string strspec in strspecSplit)
                {

                    for (int iIndex = 0; iIndex < lstSpec.Items.Count; iIndex++)
                    {
                        if (strspec.Trim() == lstSpec.Items[iIndex].Value.Trim())
                        {
                            lstSpec.Items[iIndex].Selected = true;

                        }
                    }

                }
            }
            if (Doctor_Cls != null)
            {
                string[] strclsSplit = Doctor_Cls.Split(',');
                foreach (string strcls in strclsSplit)
                {

                    for (int iIndex = 0; iIndex < lstCls.Items.Count; iIndex++)
                    {
                        if (strcls.Trim() == lstCls.Items[iIndex].Value.Trim())
                        {
                            lstCls.Items[iIndex].Selected = true;

                        }
                    }

                }
            }
            if (Chemist_Category != null)
            {
                string[] strchemSplit = Chemist_Category.Split(',');
                foreach (string strchem in strchemSplit)
                {

                    for (int iIndex = 0; iIndex < lstChem.Items.Count; iIndex++)
                    {
                        if (strchem.Trim() == lstChem.Items[iIndex].Value.Trim())
                        {
                            lstChem.Items[iIndex].Selected = true;

                        }
                    }

                }
            }
            if (Hospital_Class != null)
            {
                string[] strhosSplit = Hospital_Class.Split(',');
                foreach (string strhos in strhosSplit)
                {

                    for (int iIndex = 0; iIndex < lstHos.Items.Count; iIndex++)
                    {
                        if (strhos.Trim() == lstHos.Items[iIndex].Value.Trim())
                        {
                            lstHos.Items[iIndex].Selected = true;

                        }
                    }

                }
            }
            if (Stockist_HQ != null)
            {
                string[] strhqSplit = Stockist_HQ.Split(',');
                foreach (string strhq in strhqSplit)
                {

                    for (int iIndex = 0; iIndex < lsthq.Items.Count; iIndex++)
                    {
                        if (strhq.Trim() == lsthq.Items[iIndex].Value.Trim())
                        {
                            lsthq.Items[iIndex].Selected = true;

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
    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            DataSet dsState;
            // dsState = st.getStateChkBox(state_cd);

            dsState = st.getStcode(state_cd);

            lstst.DataTextField = "statename";
            lstst.DataValueField = "state_code";
            lstst.DataSource = dsState;
            lstst.DataBind();
            // ddlst.Items.Insert(0, new ListItem("ALL", "0"));

        }
    }
    private void FillSub_Division()
    {
        SubDivision sf = new SubDivision();
        DataSet dsDiv = new DataSet();
        dsDiv = sf.getSubDiv_Name(div_code);
        if (dsDiv.Tables[0].Rows.Count > 0)
        {
            lstsub.DataTextField = "subdivision_name";
            lstsub.DataValueField = "subdivision_code";
            lstsub.DataSource = dsDiv;
            lstsub.DataBind();
        }
    }
    private void FillSF()
    {
        SalesForce sf = new SalesForce();
        DataSet dssal = new DataSet();
        dssal = sf.SalesForceListMgrGet(div_code, "admin");
        if (dssal.Tables[0].Rows.Count > 0)
        {
            lstSf.DataTextField = "sf_name";
            lstSf.DataValueField = "sf_code";
            lstSf.DataSource = dssal;
            lstSf.DataBind();
        }
    }

    private void FillCat()
    {
        ListedDR lst = new ListedDR();
        DataSet dscat = new DataSet();
        dscat = lst.Category_doc(div_code);
        if (dscat.Tables[0].Rows.Count > 0)
        {
            lstCat.DataTextField = "Doc_Cat_SName";
            lstCat.DataValueField = "Doc_Cat_Code";
            lstCat.DataSource = dscat;
            lstCat.DataBind();
        }
    }
    private void FillCls()
    {
        ListedDR lst = new ListedDR();
        DataSet dscls = new DataSet();
        dscls = lst.Class_doc(div_code);
        if (dscls.Tables[0].Rows.Count > 0)
        {
            lstCls.DataTextField = "Doc_ClsName";
            lstCls.DataValueField = "Doc_ClsCode";
            lstCls.DataSource = dscls;
            lstCls.DataBind();
        }
    }
    private void FillSpec()
    {
        ListedDR lst = new ListedDR();
        DataSet dsspec = new DataSet();
        dsspec = lst.Speciality_doc(div_code);
        if (dsspec.Tables[0].Rows.Count > 0)
        {
            lstSpec.DataTextField = "Doc_Special_SName";
            lstSpec.DataValueField = "Doc_Special_Code";
            lstSpec.DataSource = dsspec;
            lstSpec.DataBind();
        }
    }
    private void FillChem()
    {
        Chemist lst = new Chemist();
        DataSet dschem = new DataSet();
        dschem = lst.FetchCategory_chem(div_code);
        if (dschem.Tables[0].Rows.Count > 0)
        {
            lstChem.DataTextField = "Chem_Cat_SName";
            lstChem.DataValueField = "Cat_Code";
            lstChem.DataSource = dschem;
            lstChem.DataBind();
        }
    }
    private void FillStock()
    {
        Stockist st = new Stockist();
        DataSet dsstok = new DataSet();
        dsstok = st.Get_st_HQ(div_code);
        if (dsstok.Tables[0].Rows.Count > 0)
        {
            lsthq.DataTextField = "Pool_Name";
            lsthq.DataValueField = "Pool_Id";
            lsthq.DataSource = dsstok;
            lsthq.DataBind();
        }
    }
    public void lstst_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string st = string.Empty;
        string sub = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstst.Items)
        {
            if (item.Selected)
            {
                st += item.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item in lstsub.Items)
        {
            if (item.Selected)
            {
                sub += item.Value + ',';
            }
        }
        SalesForce sf = new SalesForce();
        DataSet dssal = new DataSet();
        if (sub != "")
        {
            sub = sub.Remove(sub.Length - 1);
        }
        else
        {
            sub = "";
        }
        if (st != "")
        {
            st = st.Remove(st.Length - 1);
        }
        else
        {
            st = "";
        }
        lstSf.ClearSelection();
        dssal = sf.Survey_Hirarchy_St_Subdiv(div_code, "admin", st, sub);
        if (dssal.Tables[0].Rows.Count > 0)
        {
            lstSf.DataTextField = "sf_name";
            lstSf.DataValueField = "sf_code";
            lstSf.DataSource = dssal;
            lstSf.DataBind();
        }

    }

    public void lstsub_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string st = string.Empty;
        string sub = string.Empty;
        foreach (System.Web.UI.WebControls.ListItem item in lstsub.Items)
        {
            if (item.Selected)
            {
                sub += item.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item in lstst.Items)
        {
            if (item.Selected)
            {
                st += item.Value + ',';
            }
        }
        SalesForce sf = new SalesForce();
        DataSet dssal = new DataSet();
        if (sub != "")
        {
            sub = sub.Remove(sub.Length - 1);
        }
        else
        {
            sub = "";
        }
        if (st != "")
        {
            st = st.Remove(st.Length - 1);
        }
        else
        {
            st = "";
        }

        lstSf.ClearSelection();
        dssal = sf.Survey_Hirarchy_St_Subdiv(div_code, "admin", st, sub);
        if (dssal.Tables[0].Rows.Count > 0)
        {
            lstSf.DataTextField = "sf_name";
            lstSf.DataValueField = "sf_code";
            lstSf.DataSource = dssal;
            lstSf.DataBind();
        }

    }
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        foreach (System.Web.UI.WebControls.ListItem item in lstSf.Items)
        {
            if (item.Selected)
            {
                map_sfcode += item.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item1 in lstCat.Items)
        {
            if (item1.Selected)
            {
                map_cat += item1.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item2 in lstSpec.Items)
        {
            if (item2.Selected)
            {
                map_spec += item2.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item3 in lstCls.Items)
        {
            if (item3.Selected)
            {
                map_cls += item3.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item4 in lstChem.Items)
        {
            if (item4.Selected)
            {
                map_chem += item4.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item5 in lstHos.Items)
        {
            if (item5.Selected)
            {
                map_hos += item5.Value + ',';
            }
        }
        foreach (System.Web.UI.WebControls.ListItem item6 in lsthq.Items)
        {
            if (item6.Selected)
            {
                map_stk += item6.Value + ',';
            }
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        {
            SqlCommand cmd = new SqlCommand("update Mas_Question_Survey_Creation_Detail set  SF_Code='" + map_sfcode.Trim() + "',Doctor_Category='" + map_cat.Trim() + "',Doctor_Speclty='" + map_spec.Trim() + "',Doctor_Cls='" + map_cls.Trim() + "',Hospital_Class='" + map_hos.Trim() + "',Chemist_Category='" + map_chem.Trim() + "',Stockist_State='',Stockist_HQ='" + map_stk.Trim() + "',Updated_date=getdate() where Survey_ID='" + Request.QueryString["Survey_Id"].ToString() + "'  ", con);
            {
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Successfully');window.location='Survey_Ques_Process.aspx'</script>");

            }
        }
    }
}