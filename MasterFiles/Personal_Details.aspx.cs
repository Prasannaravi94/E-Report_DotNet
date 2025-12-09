using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Personal_Details : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsSalesforce = null;
    DataSet dsCheck = null;




    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillMRManagers();

        }

        FillColor();

    }

    private void FillMRManagers()
    {

        //SalesForce sf = new SalesForce();
       // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        //dsSalesForce = sf.UserListTP_Hierarchy(div_code, sf_code);
        string strQry = string.Empty;

        DB_EReporting db_ER = new DB_EReporting();

        strQry = "EXEC AllFieldforce_Novacant '" + div_code + "', '" + sf_code + "' ";
        dsSalesForce = db_ER.Exec_DataSet(strQry);

        if (sf_type == "3")
        {
            dsSalesForce.Tables[0].Rows[0].Delete();
            dsSalesForce.Tables[0].Rows[1].Delete();

        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }


        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }


    private void FillInfo()
    {
        // SalesForce sf = new SalesForce();
        // dsSalesforce = sf.getSales_Information(ddlFieldForce.SelectedValue);

        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();

        strQry = "SELECT Sf_Name,convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date,Sf_HQ, " +
                     " sf_designation_short_name,sf_emp_id ,Employee_Id,StateName,designation_code " +
                     " FROM mas_salesforce a, mas_state b " +
                     " WHERE Sf_Code= '" + ddlFieldForce.SelectedValue + "' and a.State_Code=b.State_Code ";

        dsSalesforce = db_ER.Exec_DataSet(strQry);

        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {

            txtEmpl_name.Text = dsSalesforce.Tables[0].Rows[0]["Sf_Name"].ToString();
            txtDOJ.Text = dsSalesforce.Tables[0].Rows[0]["Sf_Joining_Date"].ToString();
            txtdesig.Text = dsSalesforce.Tables[0].Rows[0]["sf_designation_short_name"].ToString();
            txtemp_code.Text = dsSalesforce.Tables[0].Rows[0]["sf_emp_id"].ToString();
            txtstate.Text = dsSalesforce.Tables[0].Rows[0]["StateName"].ToString();
            txtHq.Text = dsSalesforce.Tables[0].Rows[0]["sf_hq"].ToString();
            ViewState["Employee_Id"] = dsSalesforce.Tables[0].Rows[0]["Employee_Id"].ToString();
            ViewState["designation_code"] = dsSalesforce.Tables[0].Rows[0]["designation_code"].ToString();
        }

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        pnl.Visible = true;
        ClearAll();
        check_alreadyexist();
        FillInfo();
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnl.Visible = false;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Str_DOW = string.Empty;
        string Str_DOB = string.Empty;
        string strQry = string.Empty;
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();

        if (txtDOB.Text != "")
        {

            DateTime dt_DOB = Convert.ToDateTime(txtDOB.Text);
            Str_DOB = dt_DOB.ToString("MM/dd/yyyy");
        }
        //if (txtDOW.Text != "")
        //{

        //    DateTime dt_DOW = Convert.ToDateTime(txtDOW.Text);
        //    Str_DOW = dt_DOW.ToString("MM/dd/yyyy");
        //}

        DateTime joining_date = Convert.ToDateTime(txtDOJ.Text);
        string sf_joining_date = joining_date.ToString("MM/dd/yyyy");

    

        string designation_code = ViewState["designation_code"].ToString();
        string Employee_ID = ViewState["Employee_Id"].ToString();
        string[] name = ddlFieldForce.SelectedItem.Text.Split('-');


        if (ViewState["Trans_slno"] != null)
        {
            strQry = " Update salesforce_personal_info set sf_hq='" + txtHq.Text + "',sf_designation_short_name='" + txtdesig.Text + "',designation_code='" + designation_code + "', " +
                    " sf_emp_id='" + txtemp_code.Text + "',state='" + txtstate.Text + "',Employee_ID='" + Employee_ID + "' ,Permanent_Addr='" + txtperm_addr.Text + "',Present_Addr='" + txtpres_addr.Text + "', " +
                    " sf_DOB='" + Str_DOB + "',sf_DOW='" + Str_DOW + "',sf_email='" + txtemail.Text + "',Mob_No='" + txtmob_no.Text + "',Residential_No='" + txtResi_No.Text + "',Emerg_Contact_No='" + txtemerg_contact.Text + "'," +
                    " PAN_No='',Aadhar_No='',Updated_Date=getdate(),Sf_Joining_Date='" + sf_joining_date + "' where Trans_slno='" + ViewState["Trans_slno"] + "' and " +
                    " sf_code='" + ddlFieldForce.SelectedValue + "' and sf_name='" + name[0].Trim() + "' ";
        }
        else
        {

            strQry = " Insert into salesforce_personal_info (sf_code,sf_name,sf_hq,sf_designation_short_name,designation_code, " +
                      " sf_emp_id,state,Employee_ID,Permanent_Addr,Present_Addr,sf_DOB,sf_DOW,sf_email,Mob_No,Residential_No, " +
                      " Emerg_Contact_No,PAN_No,Aadhar_No,Created_Date,Updated_Date,division_code,Sf_Joining_Date) values ('" + ddlFieldForce.SelectedValue + "','" + txtEmpl_name.Text + "', " +
                      " '" + txtHq.Text + "','" + txtdesig.Text + "','" + designation_code + "','" + txtemp_code.Text + "', " +
                      " '" + txtstate.Text + "','" + Employee_ID + "','" + txtperm_addr.Text + "','" + txtpres_addr.Text + "','" + Str_DOB + "','" + Str_DOW + "', " +
                      " '" + txtemail.Text + "','" + txtmob_no.Text + "','" + txtResi_No.Text + "','" + txtemerg_contact.Text + "','','',getdate(),getdate(),'" + div_code + "' ,'" + sf_joining_date + "') ";
        }

        iReturn = db.ExecQry(strQry);

        if (iReturn > 0)
        {
            if (ViewState["Trans_slno"] != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
            }
            

            ClearAll();
            pnl.Visible = false;
        }
    }

    private void ClearAll()
    {
        txtEmpl_name.Text = "";
        txtDOJ.Text = "";
        txtHq.Text = "";
        txtdesig.Text = "";
        txtemp_code.Text = "";
        txtstate.Text = "";
        txtperm_addr.Text = "";
        txtpres_addr.Text = "";
       // txtDOB.Text = "";
      // txtDOW.Text = "";
        txtemail.Text = "";
        txtmob_no.Text = "";
        txtResi_No.Text = "";
        txtemerg_contact.Text = "";
      //  txtpan_no.Text = "";
        //txtaadhar.Text = "";
    }


    private void check_alreadyexist()
    {

        string strQry = string.Empty;
        DB_EReporting db_ER = new DB_EReporting();
        ViewState["Trans_slno"] = null;

        string[] name = ddlFieldForce.SelectedItem.Text.Split('-');



        //name = ddlFieldForce.SelectedValue;

        strQry = "Select Trans_slno,Permanent_Addr,Present_Addr,case when SF_DOB='1900-01-01' then '' else convert(varchar,sf_Dob,103) end as SF_DOB ,case when sf_DOW='1900-01-01' then '' else convert(varchar,sf_DOW,103) end as sf_DOW,sf_email, " +
                 " Mob_No,Residential_No,Emerg_Contact_No,PAN_No,Aadhar_No from salesforce_personal_info where sf_code='" + ddlFieldForce.SelectedValue + "' and sf_name='" + name[0].Trim() + "' ";
        dsCheck = db_ER.Exec_DataSet(strQry);

        if (dsCheck.Tables[0].Rows.Count > 0)
        {
            txtperm_addr.Text = dsCheck.Tables[0].Rows[0]["Permanent_Addr"].ToString();
            txtpres_addr.Text = dsCheck.Tables[0].Rows[0]["Present_Addr"].ToString();
            txtDOB.Text = dsCheck.Tables[0].Rows[0]["sf_DOB"].ToString();
           // txtDOW.Text = dsCheck.Tables[0].Rows[0]["sf_DOW"].ToString();
            txtemail.Text = dsCheck.Tables[0].Rows[0]["sf_email"].ToString();
            txtmob_no.Text = dsCheck.Tables[0].Rows[0]["Mob_No"].ToString();
            txtResi_No.Text = dsCheck.Tables[0].Rows[0]["Residential_No"].ToString();
            txtemerg_contact.Text = dsCheck.Tables[0].Rows[0]["Emerg_Contact_No"].ToString();
           // txtpan_no.Text = dsCheck.Tables[0].Rows[0]["PAN_No"].ToString();
            //txtaadhar.Text = dsCheck.Tables[0].Rows[0]["Aadhar_No"].ToString();

            ViewState["Trans_slno"] = dsCheck.Tables[0].Rows[0]["Trans_slno"].ToString();
        }
       
    }

}
