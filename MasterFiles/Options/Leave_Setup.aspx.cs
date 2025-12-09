using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;
using System.Data.Sql;
using System.Data.SqlClient;

public partial class MasterFiles_Options_Leave_Setup : System.Web.UI.Page
{

    string div_code = string.Empty;
    int iReturn = -1;
    int ireturn = -2;
    int Leave_Code = 0;
    DataSet ds = new DataSet();
    DataSet dsmr = new DataSet();
    DataSet dsVal = new DataSet();
    DataSet dsMer = new DataSet();
    DataSet dsleave = new DataSet();
    DataSet dslve = new DataSet();
    DataSet LeaveStatus = new DataSet();
    DataSet LeaveCount = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        hHeading.InnerText = this.Page.Title;

        if (!Page.IsPostBack)
        {
            FillType();
            FillEditQty();
            FillLeave();
            menu1.Title = this.Page.Title;
            // menu1.FindControl("btnBack").Visible = false;
            Holiday ss = new Holiday();
            foreach (RepeaterItem ri in rpttype.Items)
            {

                Repeater rptDetSecSale = (Repeater)ri.FindControl("rptDetSecSale");
                HiddenField hidPCode = (HiddenField)ri.FindControl("hidleave_Code");

                foreach (RepeaterItem checkItem in rptDetSecSale.Items)
                {

                    HiddenField hidSF_code = (HiddenField)checkItem.FindControl("hidlvecode");
                    CheckBox chkleave = (CheckBox)checkItem.FindControl("chkleave");

                    dsMer = ss.Leave_Entry_Edit(div_code, hidPCode.Value.ToString(), hidSF_code.Value.ToString());

                    if (dsMer.Tables[0].Rows.Count > 0)
                    {
                        chkleave.Checked = true;
                        chkdefault.Checked = false;
                    }

                }

            }

            dsMer = ss.Leave_Entry_EditDefault(div_code, "D");

            if (dsMer.Tables[0].Rows.Count > 0)
            {
                chkdefault.Checked = true;
            }

            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            string ChkReord = "select * from Mas_Leave_Setup where Division_code='" + div_code + "' and Active_Flg='0'";
            SqlCommand cmd;
            cmd = new SqlCommand(ChkReord, con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtR = new DataTable();
            da.Fill(dtR);
            if (dtR.Rows.Count != null && dtR.Rows.Count > 0)
            {
                chkdefault.Checked = false;
            }
            else
            {
                chkdefault.Checked = true;
            }
        }
    }
    //protected void grdleave_RowCommand(object sender, GridViewCommandEventArgs e)
    //{

    //    if (e.CommandName == "Deactivate")
    //    {

    //        Leave_Code = Convert.ToInt16(e.CommandArgument);

    //        AdminSetup adm = new AdminSetup();
    //        int iReturn = adm.DeActLeave_Id(Leave_Code);

    //        if (iReturn > 0)
    //        {
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
    //        }

    //        else if (iReturn == -2)
    //        {
    //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Delete');</script>");
    //        }
    //        FillLeave();
    //        FillType();
    //        FillEditQty();
    //    }
    //}

    protected void grdleave_RowCommand(object sender, GridViewCommandEventArgs e)  // changes done by  buvana
    {
        if (e.CommandName == "ToggleStatus")
        {

            Leave_Code = Convert.ToInt16(e.CommandArgument);
            int iReturn = -1;
            string currentStatus = string.Empty;
            string Status_Mode = string.Empty;

            AdminSetup adm = new AdminSetup();

            LeaveStatus = adm.GetLeaveStatus(Leave_Code);

            if (LeaveStatus.Tables[0].Rows.Count > 0)
            {
                currentStatus = LeaveStatus.Tables[0].Rows[0]["Active_Flag"].ToString();


                if (currentStatus == "1")
                {
                    iReturn = adm.DeActLeave_Id_New(Leave_Code, Convert.ToInt16(div_code), 0);
                    Status_Mode = "Activate";
                }
                else if (currentStatus == "0")
                {
                    iReturn = adm.DeActLeave_Id_New(Leave_Code, Convert.ToInt16(div_code), 1);
                    Status_Mode = "De-Activate";
                }
            }

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<script type='text/javascript'>alert('" + Status_Mode + " Successfully');</script>");
            }
            else if (iReturn <= 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Process.Try Again.');</script>");
            }
            FillLeave();
            FillType();
            FillEditQty();
        }
    }

    private void FillType()
    {
        string strQry = string.Empty;
        int iReturn = -1;
        DB_EReporting db_ER = new DB_EReporting();

        strQry = "select ROW_NUMBER()  OVER (ORDER BY  category) As SrNo,* from (select  'Probation' as category,'2' value,'PR' as shtnme " +
                 "union select 'Confirmed' as category,'3' value,'CO' as shtnme  union  " +
                 "select 'Trainee' as category,'1' as value,'TR' as shtnme )as tt order by value asc";

        dsVal = db_ER.Exec_DataSet(strQry);

        if (dsVal.Tables.Count > 0)
        {
            rpttype.DataSource = dsVal;
            rpttype.DataBind();
        }

        DataTable dtSfCode = new DataTable();
        dtSfCode.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtSfCode.Columns["INX"].AutoIncrementSeed = 1;
        dtSfCode.Columns["INX"].AutoIncrementStep = 1;
        dtSfCode.Columns.Add("Leave_code", typeof(string));
        dtSfCode.Columns.Add("Leave_SName", typeof(string));
        dtSfCode.Columns.Add("Leave_Name", typeof(string));

        SalesForce sf = new SalesForce();

        ds = sf.getleave_type(div_code);


        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            dtSfCode.Rows.Add(null, Convert.ToString(ds.Tables[0].Rows[i]["Leave_code"]), Convert.ToString(ds.Tables[0].Rows[i]["Leave_SName"]), Convert.ToString(ds.Tables[0].Rows[i]["Leave_Name"]));
        }

        dsmr.Tables.Add(dtSfCode);
        rptleavetypeHeader.DataSource = dsmr;
        rptleavetypeHeader.DataBind();

    }


    private void FillEditQty()
    {

        string strQry = string.Empty;
        int iReturn = -1;
        DB_EReporting db_ER = new DB_EReporting();

        strQry = "select ROW_NUMBER()  OVER (ORDER BY  category) As SrNo,* from (select  'Probation' as category,'2' value,'PR' as shtnme " +
                 "union select 'Confirmed' as category,'3' value,'CO' as shtnme  union " +
                 "select 'Trainee' as category,'1' as value,'TR' as shtnme )as tt order by value asc";

        dsVal = db_ER.Exec_DataSet(strQry);

        if (dsVal.Tables.Count > 0)
        {
            rpttype.DataSource = dsVal;
            rpttype.DataBind();
        }

        //string mSf_code = dsts.Tables[1].Rows[0]["sf_code"].ToString();
        DataTable dt = new DataTable();
        dt.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dt.Columns["INX"].AutoIncrementSeed = 1;
        dt.Columns["INX"].AutoIncrementStep = 1;
        dt.Columns.Add("Leave_code", typeof(string));
        dt.Columns.Add("Leave_SName", typeof(string));
        dt.Columns.Add("Leave_Name", typeof(string));

        SalesForce sf = new SalesForce();
        ds = sf.getleave_type(div_code);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            dt.Rows.Add(null, Convert.ToString(ds.Tables[0].Rows[i]["Leave_code"]), Convert.ToString(ds.Tables[0].Rows[i]["Leave_SName"]), Convert.ToString(ds.Tables[0].Rows[i]["Leave_Name"]));
        }

        rptleavetypeHeader.DataSource = dt;
        rptleavetypeHeader.DataBind();



        foreach (RepeaterItem ri in rpttype.Items)
        {

            Repeater rptDetSecSale = (Repeater)ri.FindControl("rptDetSecSale");
            //HiddenField hicatCode = (HiddenField)ri.FindControl("hicatCode");
            rptDetSecSale.DataSource = dt;
            rptDetSecSale.DataBind();
            //  dsmgrsf.Tables.Add(dt);

        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

        string ChkReord = "select * from Mas_Leave_Setup where Division_code='" + div_code + "' ";
        SqlCommand cmd;
        cmd = new SqlCommand(ChkReord, con);
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtR = new DataTable();
        da.Fill(dtR);
        if (dtR.Rows.Count > 0)
        {
            string Qry = "delete from Mas_Leave_Setup where Division_code='" + div_code + "' ";
            cmd = new SqlCommand(Qry, con);

            int _res1 = cmd.ExecuteNonQuery();
        }
        Holiday lvesvdefault = new Holiday();
        if (chkdefault.Checked == true)
        {
            ireturn = lvesvdefault.LeaveInsertDefault(rdomandt.SelectedValue, 10, "Y", "D", "0", div_code);
        }
        if (ireturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>");
        }
        else if (chkdefault.Checked == false)
        {

            foreach (RepeaterItem pitem in rpttype.Items)
            {
                Repeater rptchktype = (Repeater)pitem.FindControl("rptDetSecSale");
                HiddenField hidleave_Code = (HiddenField)pitem.FindControl("hidleave_Code");
                HiddenField hdnshtnme = (HiddenField)pitem.FindControl("hdnshtnme");


                string Leave_Code_value = string.Empty;
                Leave_Code_value = Convert.ToString(hidleave_Code.Value);

                foreach (RepeaterItem item in rptchktype.Items)
                {
                    HiddenField hidlvecode = (HiddenField)item.FindControl("hidlvecode");
                    HiddenField hidlvename = (HiddenField)item.FindControl("hidlvename");
                    CheckBox chkleave = (CheckBox)item.FindControl("chkleave");

                    string name = string.Empty;

                    if (chkleave.Checked == true)
                    {
                        name = "Y";
                    }
                    else
                    {
                        name = "N";
                    }
                    Holiday lvesv = new Holiday();
                    iReturn = lvesv.LeaveInsert(rdomandt.SelectedValue, Leave_Code_value, name, hdnshtnme.Value, hidlvecode.Value, div_code);

                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully')</script>");
                    }

                }

            }
        }

    }
    private void FillLeave()
    {
        AdminSetup adm = new AdminSetup();
        //dslve = adm.getLeaveName(div_code);
        dslve = adm.getLeaveName_New(div_code); // changes done by  buvana

        if (dslve.Tables[0].Rows.Count > 0)
        {
            grdleave.DataSource = dslve;
            grdleave.DataBind();

        }
        else
        {
            grdleave.DataSource = dslve;
            grdleave.DataBind();
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string Leave_Id = string.Empty;
        string LeaveShtnme = string.Empty;
        string LeaveName = string.Empty;

        foreach (GridViewRow gridRow in grdleave.Rows)
        {
            AdminSetup adm = new AdminSetup();
            Label Leave_code = (Label)gridRow.Cells[1].FindControl("lblcode");
            Leave_Id = Leave_code.Text.ToString();
            TextBox txtlve_Sname = (TextBox)gridRow.Cells[1].FindControl("txtlve_Sname");
            LeaveShtnme = txtlve_Sname.Text.ToString();
            TextBox txtlve_name = (TextBox)gridRow.Cells[1].FindControl("txtlve_name");
            LeaveName = txtlve_name.Text.ToString();

            //Update 
            //  iReturn = adm.LeaveUpdate(Convert.ToInt16(Leave_Id), LeaveShtnme, LeaveName, div_code);
            if (LeaveShtnme != "" && LeaveName != "")
            {
                iReturn = adm.LeaveUpdate(Convert.ToInt16(Leave_Id), LeaveShtnme, LeaveName, div_code);
            }

            if (iReturn > 0)
            {
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                FillLeave();
            }
        }
    }


    //protected void grdleave_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    AdminSetup adm = new AdminSetup();
    //    TextBox txt_SName = (TextBox)grdleave.FooterRow.FindControl("txt_SName");
    //    TextBox txt_Name = (TextBox)grdleave.FooterRow.FindControl("txt_Name");

    //    int iReturn = adm.LeaveTypeAdd(txt_SName.Text, txt_Name.Text, div_code);

    //    if (iReturn > 0)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
    //        FillLeave();
    //        FillType();
    //        FillEditQty();
    //    }
    //}
    protected string GetClientClickConfirmation(object activeFlag) //changes done by  buvana
    {
        if (activeFlag != null && activeFlag.ToString() == "0")
        {
            return "return confirm('Do you want to deactivate the Leave?');";
        }
        else
        {
            return "return confirm('Do you want to activate the Leave?');";
        }
    }
    protected void grdleave_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)  // changes done by  buvana
    {

        int iReturn = -1;
        AdminSetup adm = new AdminSetup();

        Control control = null;
        if (grdleave.FooterRow != null)
        {
            control = grdleave.FooterRow;
        }
        else
        {
            control = grdleave.Controls[0].Controls[0];
        }

        TextBox txt_SName = (TextBox)control.FindControl("txt_SName");
        TextBox txt_Name = (TextBox)control.FindControl("txt_Name");



        LeaveCount = adm.GetLeaveCount(div_code);
        if (LeaveCount.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToInt16(LeaveCount.Tables[0].Rows[0]["Leave_Count"].ToString()) < 8)
            {
                if (txt_SName.Text != "" && txt_Name.Text != "")
                {
                    iReturn = adm.LeaveTypeAdd(txt_SName.Text, txt_Name.Text, div_code);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave-Short Name and Name  Should not be Empty. ');</script>");
                }

            }
            else
            {
                iReturn = -3;
            }
        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            FillLeave();
            FillType();
            FillEditQty();
        }
        else if (iReturn == -3)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not allowed  More than 8 Leaves');</script>");
            FillLeave();
            FillType();
            FillEditQty();
        }
    }

    protected void grdleave_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdleave.PageIndex = e.NewPageIndex;
        FillLeave();
    }
    protected void grdleave_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
}
  //public void rptleavetypeHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
  //{
    

  //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
  //    {
  //        //string litSfName = rptleavetypeHeader.Controls[0].Controls[0].FindControl("litSfName").ToString();

  //        RepeaterItem item = e.Item;

  //        //Reference the Controls.
  //        string litSfName = (item.FindControl("litSfName") as Literal).Text;

  //        if (Request.QueryString["txt_SName"].ToString() == litSfName)
  //        {

  //             foreach (RepeaterItem ri in rpttype.Items)
  //             {

  //                 Repeater rptDetSecSale = (Repeater)ri.FindControl("rptDetSecSale");
  //                 HiddenField hidPCode = (HiddenField)ri.FindControl("hidleave_Code");

  //                 foreach (RepeaterItem checkItem in rptDetSecSale.Items)
  //                 {
  //                     HiddenField hidSF_code = (HiddenField)checkItem.FindControl("hidlvecode");
  //                     CheckBox chkleave = (CheckBox)checkItem.FindControl("chkleave");
  //                     chkleave.Checked = true;
  //                 }
  //             }

  //        }
          
  //    }
  //}
   
