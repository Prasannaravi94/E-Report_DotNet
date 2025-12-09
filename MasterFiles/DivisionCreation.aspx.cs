using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using DBase_EReport;
public partial class MasterFiles_DivisionCreation : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string div_name = string.Empty;
    string div_addr1 = string.Empty;
    string div_addr2 = string.Empty;
    string div_city = string.Empty;
    string div_pin = string.Empty;
    string div_state = string.Empty;
    string div_sname = string.Empty;
    string div_alias = string.Empty;
    string state_code = string.Empty;
    string sChkLocation = string.Empty;
    string div_year = string.Empty;
    string div_weekoff = string.Empty;
    string sf_type = string.Empty;
    string strChkWeekOffValue = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strQry = string.Empty;
    DataSet dsDiv = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        pnlNew.Visible = false;
        txtDivision_Sname.Focus();
       // //menu1.FindControl("btnBack").Visible = false;
        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;
           // Session["backurl"] = "DivisionList.aspx";

            div_code = Request.QueryString["Div_Code"];
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);

            if (div_code != "" && div_code != null)
            {
                Division dv = new Division();
                dsDivision = dv.getDivision(div_code);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                  //  pnlNew.Visible = true;
                    txtDivision_Sname.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtDivision_Sname.BackColor = System.Drawing.SystemColors.Control;

                    txtDivision_Sname.Enabled = false;
                 
                    txtDivision_Name.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtDivision_Add1.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtDivision_Add2.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtCity.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtPincode.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    txtAlias.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    state_code = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    txtYear.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    string str = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    txtImpleDate.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    //txtWeekOff.Text = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();                    
                    HidDivCode.Value = div_code;

                    string strtxtWeeKName = string.Empty;
                    string[] strweek;
                    iIndex = -1;
                    strweek = str.Split(',');
                  //  Session["Value"] = str.Remove(str.Length - 1);
                    Session["Value"] = str;
                    foreach (string Wk in strweek)
                    {
                        for (iIndex = 0; iIndex < Chkweek.Items.Count; iIndex++)
                        {
                            if (Wk == Chkweek.Items[iIndex].Value)
                            {
                                Chkweek.Text = "";
                                Chkweek.Items[iIndex].Selected = true;

                                if (Chkweek.Items[iIndex].Selected == true)
                                {
                                    strtxtWeeKName += Chkweek.Items[iIndex].Text + ",";
                                }
                            }
                        }
                    }

                    if (strtxtWeeKName != "")
                    {
                       txtWeekOff.Text = strtxtWeeKName.Remove(strtxtWeeKName.Length - 1);
                    }
                    BindGridviewData();
                }
            }
            //if (sf_type == "")
            //{
            //    FillCheckBoxList();
            //}
            //else
            //{
                      FillHOState();
            //}
        }
    }

    private void FillCheckBoxList()
    {
        //List of States are loaded into the checkbox list from Division Class
        Division dv = new Division();
        dsDivision = dv.getLocation();
        div.DataTextField = "statename";
        div.DataSource = dsDivision;
        div.DataBind();
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < div.Items.Count; iIndex++)
                {
                    if (st == div.Items[iIndex].Value)
                    {

                        div.Items[iIndex].Selected = true;
                        div.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");

                    }
          
                }
            }
        }
    }
    private void FillHOState()
    {
        //List of States are loaded into the checkbox list from Division Class
        Division dv = new Division();
        state_code = state_code.Substring(0, state_code.Length - 1);
        dsDivision = dv.getLocationHO(state_code);
        div.DataTextField = "statename";
        div.DataSource = dsDivision;
        div.DataBind();
        string[] state;
        if (state_code != "")
        {
            iIndex = -1;
            state = state_code.Split(',');
            foreach (string st in state)
            {
                for (iIndex = 0; iIndex < div.Items.Count; iIndex++)
                {
                    if (st == div.Items[iIndex].Value)
                    {
                        div.Items[iIndex].Selected = true;
                        div.Items[iIndex].Attributes.Add("style", "Color: Red;font-weight:Bold");
                    }

                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        for (int i = 0; i < div.Items.Count; i++)
        {
            if (div.Items[i].Selected)
            {
                sChkLocation = sChkLocation + div.Items[i].Value + ",";
            }
        }

        div_code = Request.QueryString["Div_Code"];
        div_name = txtDivision_Name.Text;
        div_addr1 = txtDivision_Add1.Text;
        div_addr2 = txtDivision_Add2.Text;
        div_city = txtCity.Text;
        div_pin = txtPincode.Text;
        div_sname = txtDivision_Sname.Text;
        div_alias = txtAlias.Text;
        div_year = txtYear.Text;

        strChkWeekOffValue = Session["Value"].ToString();

        if (txtImpleDate.Text != "")
        {

            DateTime implmt_date = Convert.ToDateTime(txtImpleDate.Text);

            txtImpleDate.Text = implmt_date.ToString("MM/dd/yyyy");
        }

        if (div_code == null)
        {
            strChkWeekOffValue = Session["Value"].ToString();
            // Add New Division
            Division dv = new Division();
            int iReturn = dv.RecordAdd(div_name, div_addr1, div_addr2, div_city, div_pin, sChkLocation, div_sname, div_alias, div_year, strChkWeekOffValue, txtImpleDate.Text);

            if (iReturn > 0)
            {
            
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                Resetall();
            }
            else if (iReturn == -2)
            {
              
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Division already Exist.\');", true);
            }

        }
        else
        {
            // Update Division   

            //string div_weekoff1 = "";
            //for (int i = 0; i < Chkweek.Items.Count; i++)
            //{

            //    if (Chkweek.Items[i].Selected)//changed 1 to i  


            //    div_weekoff1 += Chkweek.Items[i].Text.ToString() + ","; //changed 1 to i
            //}

            // txtWeekOff.Text = div_weekoff;
            //txtWeekOff.Text = div_weekoff.TrimEnd(',');
            strChkWeekOffValue = Session["Value"].ToString();
            Division dv = new Division();
            int iReturn = dv.RecordUpdate(div_code, div_name, div_addr1, div_addr2, div_city, div_pin, sChkLocation, div_sname, div_alias, div_year, strChkWeekOffValue, txtImpleDate.Text);
            if (iReturn > 0)
            {
                // menu1.Status = "Division updated Successfully";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Updated Successfully.\');", true);
            }
            else if (iReturn == -2)
            {
                // menu1.Status = "Division exist with the same short name!!";
                ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Already exist with the same short name.\');", true);
            }

        }
    }
    protected void Chkweek_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string name = "";
        //for (int i = 0; i < Chkweek.Items.Count; i++)
        //{
        //    if (Chkweek.Items[i].Selected)
        //    {
        //        name += Chkweek.Items[i].Text + ",";
        //    }
        //}
        //TextBox1.Text = name;

        string s = "";
        string Value = "";


        for (int i = 0; i < Chkweek.Items.Count; i++)
        {

            if (Chkweek.Items[i].Selected)//changed 1 to i  
            {
                s += Chkweek.Items[i].Text.ToString() + ","; //changed 1 to i
                Value += Chkweek.Items[i].Value.ToString() + ",";
            }

        }
        txtWeekOff.Text = s;
        if (Value != "")
        {
            Session["Value"] = Value.Remove(Value.Length - 1);
        }
        txtWeekOff.Text = s.TrimEnd(',');


    }
    private void Resetall()
    {
        txtDivision_Name.Text = "";
        txtDivision_Add1.Text = "";
        txtDivision_Sname.Text = "";
        txtDivision_Add2.Text = "";
        txtCity.Text = "";
        txtPincode.Text = "";
        txtAlias.Text = "";
        txtYear.Text = "";
        txtWeekOff.Text = "";
        //  Chkweek.Text = "";

        for (iIndex = 0; iIndex < div.Items.Count; iIndex++)
        {
            div.Items[iIndex].Selected = false;
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

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        Division divi = new Division();
        for (int i = 0; i < chkNew.Items.Count; i++)
        {
            if (chkNew.Items[0].Selected)
            {
                int iReturn = divi.getProCat_newdivision(Request.QueryString["Div_Code"]);
                if (iReturn > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }
            if (chkNew.Items[1].Selected)
            {
                int iReturnGrp = divi.getProCat_newGroup(Request.QueryString["Div_Code"]);
                if (iReturnGrp > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }
            }
            if (chkNew.Items[2].Selected)
            {
                int iProddet = divi.getNew_ProductDet(Request.QueryString["Div_Code"]);
                if (iProddet > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }
            }
            if (chkNew.Items[3].Selected)
            {
                int iDocCat = divi.getNew_Doc_Category(Request.QueryString["Div_Code"]);
                if (iDocCat > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }
            if (chkNew.Items[4].Selected)
            {
                int iDocCat = divi.getNew_DocSpec(Request.QueryString["Div_Code"]);
                if (iDocCat > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }
            if (chkNew.Items[5].Selected)
            {
                int iDocQua = divi.getNew_DocQua(Request.QueryString["Div_Code"]);
                if (iDocQua > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }
            if (chkNew.Items[6].Selected)
            {
                int iDocCls = divi.getNew_Doc_Cls(Request.QueryString["Div_Code"]);
                if (iDocCls > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }

            if (chkNew.Items[7].Selected)
            {
                int iSetup = divi.getNew_adm_Setup(Request.QueryString["Div_Code"]);
                if (iSetup > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }
                    
            }
            
            if (chkNew.Items[8].Selected)
            {
                int iFlash = divi.getNew_Flash(Request.QueryString["Div_Code"]);
                if (iFlash > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }
                    
            }

            if (chkNew.Items[9].Selected)
            {
                int iFlash = divi.getNew_Desig(Request.QueryString["Div_Code"]);
                if (iFlash > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }

            if (chkNew.Items[10].Selected)
            {
                int iFlash = divi.getNew_Holi(Request.QueryString["Div_Code"]);
                if (iFlash > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'Processed Sucessfully.\');", true);
                }

            }
           
        }

    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        pnlNew.Visible = true;

        div_code = Request.QueryString["Div_Code"];
        DB_EReporting db_ER = new DB_EReporting();

        strQry = "select Location_based,case when Effective_date='1900-01-01' then '' else convert(varchar,Effective_date,103) end as Effective_date from mas_division where Division_Code='" + div_code + "' ";
        dsDiv = db_ER.Exec_DataSet(strQry);

        if (dsDiv.Tables[0].Rows.Count > 0)
        {
            if (dsDiv.Tables[0].Rows[0]["Location_based"].ToString() != "" && dsDiv.Tables[0].Rows[0]["Location_based"].ToString() != null)
            {

                ddllocation.SelectedValue = dsDiv.Tables[0].Rows[0]["Location_based"].ToString();
                txteffective.Text = dsDiv.Tables[0].Rows[0]["Effective_date"].ToString();
            }
        }

    }

    protected void btnlocation_Click(object sender, EventArgs e)
    {
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        div_code = Request.QueryString["Div_Code"];

        DateTime effect_date = new DateTime();
        string date = string.Empty;

        if (txteffective.Text != "")
        {

            effect_date = Convert.ToDateTime(txteffective.Text);
            date = effect_date.ToString("MM/dd/yyyy");
        }

        strQry = "Update mas_division set Location_based='" + ddllocation.SelectedValue + "',Effective_date='" + date + "' where Division_Code='" + div_code + "'";
        iReturn = db.ExecQry(strQry);

        if (iReturn > 0)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved location');</script>");
        }
    }
    protected void bt_upload_OnClick(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        //Label lblSF_Code = (Label)GrdImage.Rows[i].FindControl("lblSF_Code");
        if (FilUpImage.HasFile)
        {
            string strname = FilUpImage.FileName.ToString();
            FilUpImage.PostedFile.SaveAs(Server.MapPath("~/Logo/") + strname);
            con.Open();

            SqlCommand cmd = new SqlCommand("update Mas_Division set div_logo='" + "~/Logo/" + strname + "' where Division_Code='" + Request.QueryString["Div_Code"] + "' ", con);

            cmd.ExecuteNonQuery();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Logo Updated Successfully');</script>");

        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Upload Logo');</script>");
        }
    }
    private void BindGridviewData()
    {




        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select div_logo from Mas_Division where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();

        DataList1.DataSource = ds;
        DataList1.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("DivisionList.aspx");
    }
}