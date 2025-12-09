using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using DBase_EReport;



public partial class MasterFiles_BulkEditSalesforce_New : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSalesForce = null;
    DataSet dsDivision = null;
    DataSet dsSF = null;
    DataSet dsState = null;
    DataSet dsReport = null;
    DataSet dsSubDivision = null;
    DataSet dsSub = null;
    string[] statecd;
    string state_cd = string.Empty;
    string div_code = string.Empty;
    string SF_Code = string.Empty;
    string SF_Name = string.Empty;
    string SF_UserName = string.Empty;
    string state_code = string.Empty;
    string ReportingMGR = string.Empty;
    string sState = string.Empty;
    string search = string.Empty;
    int i;
    int iReturn = -1;
    int iIndex;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DateTime Sf_Joining_Date;
    int time;
    string DCR_Sample = string.Empty;
    string DCR_Input = string.Empty;
    #endregion
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //   FillReporting();
            Session["backurl"] = "SalesForceList.aspx";
            menu1.Title = this.Page.Title;

        }

        //foreach (ListItem item in CblSFCode.Items)
        //{
        //    if (item.Value == "Sf_Name")
        //    {
        //        //item.Text = "Click items to Edit";
        //        item.Attributes.CssStyle.Add("visibility", "hidden");
        //        //item.Attributes.CssStyle.Add("font-weight", "bold");

        //    }
        //}

        FillColor();
    }
    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    private void FillReporting()
    {
        SalesForce sf = new SalesForce();
        //  dsSalesForce = sf.getUserList_Reporting(div_code);
        dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFilter.DataTextField = "sf_name";
            ddlFilter.DataValueField = "sf_code";
            ddlFilter.DataSource = dsSalesForce;
            ddlFilter.DataBind();

            ddlSF.DataTextField = "des_color";
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
            string bcolor = "#" + ColorItems.Text;
            ddlFilter.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_BulkEdit(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
            getState();
            // getDesignation();
        }
        else
        {
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
    }

    protected DataSet FillState()
    {
        string div_code = string.Empty;
        div_code = Session["div_code"].ToString();
        Division dv = new Division();
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
            dsState = st.getState(state_cd);
        }
        return dsState;
    }

    protected DataSet FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        return dsSalesForce;
    }
    private void getState()
    {

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            DropDownList ddlState = (DropDownList)gridRow.Cells[1].FindControl("State_Code");
            Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
            SF_Code = lblSFCode.Text.ToString();
            SalesForce sf = new SalesForce();
            DataSet dsState = sf.getState_BulkEdit(SF_Code, div_code);
            if (dsState.Tables[0].Rows.Count > 0)
            {
                ddlState.SelectedValue = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
        }
    }
    //private void getDesignation()
    //{
    //    foreach (GridViewRow gridRow in grdSalesForce.Rows)
    //    {
    //        DropDownList ddlDesignation = (DropDownList)gridRow.Cells[1].FindControl("Designation_Code");
    //        Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
    //        SF_Code = lblSFCode.Text.ToString();
    //        SalesForce sf = new SalesForce();
    //        DataSet dsState = sf.getDesignation_BulkEdit(SF_Code, div_code);
    //        if (dsState.Tables[0].Rows.Count > 0)
    //        {
    //            ddlDesignation.SelectedValue = dsState.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
    //        }
    //    }
    //}




    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND " + sSearchBy + " like '" + sSearchText + "%' AND  (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') ";
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForce_BulkEditFind(sFind);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        else
        {
            btnUpdate.Visible = false;
            grdSalesForce.DataSource = dsSalesForce;
            grdSalesForce.DataBind();
        }
        getState();
        //getDesignation();

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        tblSalesForce.Visible = true;
        btnUpdate.Visible = true;
        // lblSelect.Visible = false;
        if (ddlFilter.SelectedIndex > 0)
        {
            FillSalesForce_Reporting();
        }
        //else if (ddlFields.SelectedIndex == 0)
        //{
        //    FillSalesForce();
        //}
    }
    private void FillSalesForce_Reporting()
    {
        string sReport = ddlFilter.SelectedValue.ToString();
        SalesForce sf = new SalesForce();
        dsReport = sf.getReportingTo(sReport);
        ReportingMGR = dsReport.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(); //His Reporting Mananger 
        //dsSalesForce = sf.getSalesForce_BulkEdit_Rpt(div_code, sReport, ReportingMGR); 


        //dsSalesForce = sf.UserList_BulkEditStartDate(div_code, sReport);

        DataTable dtUserList = new DataTable();
        dtUserList = sf.getUserListReportingToNew_for_all_withvacant(div_code, sReport, 0, Session["sf_type"].ToString()); // 28-Aug-15 -Sridevi
        if (dtUserList.Rows.Count > 0)
        {
            if (sReport == "admin")
            {
                dtUserList.Rows[0].Delete();
                dtUserList.Rows[0].Delete();
            }
            else
            {
                dtUserList.Rows[1].Delete();
            }
        }
        if (dtUserList.Rows.Count > 0)
        {
            grdSalesForce.Visible = true;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }
        else
        {
            btnUpdate.Visible = false;
            grdSalesForce.DataSource = dtUserList;
            grdSalesForce.DataBind();
        }

        getState();
        // getDesignation();

    }



    //protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdSalesForce.PageIndex = e.NewPageIndex;
    //    FillSalesForce();
    //}

    /*  protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
      {
          if (e.Row.RowType == DataControlRowType.DataRow)
          {

              //for (i = 0; i < CblSFCode.Items.Count; i++)
              //{
              //    if (CblSFCode.SelectedValue == "1")
              //    {
              //        e.Row.Cells[5].Visible = true;

              //        e.Row.Cells[6].Visible = false;
              //        e.Row.Cells[7].Visible = false;
              //        e.Row.Cells[8].Visible = false;
              //    }

              //    else if (CblSFCode.SelectedValue == "2")
              //    {
              //        e.Row.Cells[6].Visible = true;

              //        e.Row.Cells[7].Visible = false;
              //        e.Row.Cells[8].Visible = false;
              //        e.Row.Cells[5].Visible = false;
              //    }
              //    else if (CblSFCode.SelectedValue == "3")
              //    {
              //        e.Row.Cells[7].Visible = true;

              //        e.Row.Cells[8].Visible = false;
              //        e.Row.Cells[5].Visible = false;
              //        e.Row.Cells[6].Visible = false;
              //    }
              //    else if (CblSFCode.SelectedValue == "4")
              //    {
              //        e.Row.Cells[8].Visible = true;

              //        e.Row.Cells[5].Visible = false;
              //        e.Row.Cells[6].Visible = false;
              //        e.Row.Cells[7].Visible = false;
              //    }
              
              //}
              //if (ddlselectfield.SelectedValue != "1")
              //{
              //    for (int i = 5; i < grdSalesForce.Columns.Count; i++)
              //    {
              //        if (i == Convert.ToInt16(ddlselectfield.SelectedValue))
              //        {
                       
              //            grdSalesForce.Columns[i].Visible = true;
              //        }
              //        else
              //        {                      
              //            grdSalesForce.Columns[i].Visible = false;
              //        }

              //    }
              //}

          }
       
      }*/

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlselectfield.SelectedValue == "15")
            {
                for (int i = 5; i < grdSalesForce.Columns.Count; i++)
                {
                    grdSalesForce.Columns[i].Visible = false;
                }
                
                grdSalesForce.Columns[15].Visible = true;
                grdSalesForce.Columns[16].Visible = true;

            }
          
           
            else if (ddlselectfield.SelectedValue == "19")
            {
                for (int i = 5; i < grdSalesForce.Columns.Count; i++)
                {
                    grdSalesForce.Columns[i].Visible = false;
                }
                grdSalesForce.Columns[19].Visible = true;
                grdSalesForce.Columns[20].Visible = true;
                grdSalesForce.Columns[21].Visible = true;
                grdSalesForce.Columns[22].Visible = true;

            }
            else if (ddlselectfield.SelectedValue == "20")
            {
                for (int i = 5; i < grdSalesForce.Columns.Count; i++)
                {
                    grdSalesForce.Columns[i].Visible = false;
                }

                grdSalesForce.Columns[24].Visible = true;
                grdSalesForce.Columns[26].Visible = true;

            }
            else if (ddlselectfield.SelectedValue != "1")
            {
                for (int i = 5; i < grdSalesForce.Columns.Count; i++)
                {
                    if (i == Convert.ToInt16(ddlselectfield.SelectedValue))
                    {
                        grdSalesForce.Columns[19].Visible = false;
                        grdSalesForce.Columns[i].Visible = true;
                    }
                    else
                    {
                       grdSalesForce.Columns[19].Visible = false;
                        grdSalesForce.Columns[i].Visible = false;
                    }

                }
            }
            else
            {
                for (int i = 5; i < grdSalesForce.Columns.Count; i++)
                {
                   grdSalesForce.Columns[19].Visible = false;
                    grdSalesForce.Columns[i].Visible = true;
                }
                grdSalesForce.Columns[23].Visible = false;
                grdSalesForce.Columns[25].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList check = (CheckBoxList)e.Row.FindControl("subdivision_code");
            TextBox txtSubDivision = (TextBox)e.Row.FindControl("TextBox1");
            HiddenField hdnSubDivisionId = (HiddenField)e.Row.FindControl("hdnSubDivisionId");
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            SF_Code = lblSFCode.Text.ToString();
            SalesForce dv = new SalesForce();
            dsSub = dv.getSubDiv_Selected(div_code, SF_Code);

            // DataTable dtDiv = new DataTable();
            string strDiv = dsSub.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            string[] strDivSplit = strDiv.Split(',');
            //if (strDiv == "")
            //{
            //    TextBox1.Text = "NIL";
            //}
            foreach (string strsubdv in strDivSplit)
            {
                if (strsubdv.ToString() != "")
                {

                    dsSubDivision.Tables[0].DefaultView.RowFilter = "subdivision_code in ('" + strsubdv + "')";
                    DataTable dtDiv = dsSubDivision.Tables[0].DefaultView.ToTable();
                    txtSubDivision.Text += dtDiv.Rows[0].ItemArray.GetValue(2).ToString() + ",";
                }

                string[] strchkdiv;
                strchkdiv = txtSubDivision.Text.Split(',');
                foreach (string chkdiv in strchkdiv)
                {
                    for (iIndex = 0; iIndex < check.Items.Count; iIndex++)
                    {
                        if (chkdiv.Trim() == check.Items[iIndex].Text.Trim())
                        {
                            check.Items[iIndex].Selected = true;

                        }

                    }
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlFieldforce_Type = (DropDownList)e.Row.FindControl("Fieldforce_Type");
            DropDownList BankName = (DropDownList)e.Row.FindControl("ddlBankName");

            DropDownList Category = (DropDownList)e.Row.FindControl("Category");
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            //  Label lblSFType = (Label)e.Row.FindControl("lblSFType");
            SF_Code = lblSFCode.Text.ToString();
            // sf_type = lblSFType.Text.ToString();
            SalesForce sf = new SalesForce();
            DataSet dsddl = new DataSet();
            DataSet dsFiledtype = sf.getFieldforcetype_BulkEdit(SF_Code, div_code);
            if (dsFiledtype.Tables[0].Rows.Count > 0)
            {
                ddlFieldforce_Type.SelectedValue = dsFiledtype.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                BankName.SelectedValue = dsFiledtype.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

            DataSet dscategory = sf.getCategorytype_BulkEdit(SF_Code, div_code);
            if (dscategory.Tables[0].Rows.Count > 0)
            {
                Category.SelectedValue = dscategory.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            CheckBox chkSample = (CheckBox)e.Row.FindControl("chkSample");
            Label lblDCR_Sample = (Label)e.Row.FindControl("lblDCR_Sample");
            if (lblDCR_Sample.Text == "1")
            {
                chkSample.Checked = true;
            }

            CheckBox chkInput = (CheckBox)e.Row.FindControl("chkInput");
            Label lblDCR_Input = (Label)e.Row.FindControl("lblDCR_Input");
            if (lblDCR_Input.Text == "1")
            {
                chkInput.Checked = true;
            }

        }
    }
    protected void grdSalesForce_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
            e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        StringBuilder Sb_Val_Update = new StringBuilder();
        Sb_Val_Update.Append("<root>");
        SalesForce sf = new SalesForce();

        string strQry = "";

        foreach (GridViewRow gridRow in grdSalesForce.Rows)
        {
            if (ddlselectfield.SelectedValue == "1")
            {

                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox UsrDfd_UserName = (TextBox)gridRow.FindControl("UsrDfd_UserName");
                TextBox Sf_Password = (TextBox)gridRow.FindControl("Sf_Password");
                TextBox Sf_HQ = (TextBox)gridRow.FindControl("Sf_HQ");
                DropDownList State_Code = (DropDownList)gridRow.FindControl("State_Code");
                TextBox sf_emp_id = (TextBox)gridRow.FindControl("sf_emp_id");
                //DropDownList Designation = (DropDownList)gridRow.FindControl("Designation");

                TextBox Sf_Joining_Date = (TextBox)gridRow.FindControl("Sf_Joining_Date");
                string Joining_Date = Sf_Joining_Date.Text;
                DateTime Joining_Date2 = Convert.ToDateTime(Joining_Date);
                string Joining_Date3 = Joining_Date2.Month.ToString() + '-' + Joining_Date2.Day.ToString() + '-' + Joining_Date2.Year.ToString();


                string SubId = "";
                //  HiddenField hdnSubDivisionId = (HiddenField)gridRow.Cells[1].FindControl("hdnSubDivisionId");
                //CheckBoxList check = (CheckBoxList)gridRow.Cells[1].FindControl(cntrl);
                TextBox TextBox1 = (TextBox)gridRow.Cells[1].FindControl("TextBox1");
                string sub = TextBox1.Text.ToString();


                string[] strSub;
                iIndex = -1;
                strSub = sub.Split(',');
                Session["Value"] = sub;

                foreach (string dg in strSub)
                {
                    for (iIndex = 0; iIndex < subdivision_code.Items.Count; iIndex++)
                    {
                        if (dg == subdivision_code.Items[iIndex].Text)
                        {

                            subdivision_code.Items[iIndex].Selected = true;

                            if (subdivision_code.Items[iIndex].Selected == true)
                            {
                                // strtxtDes_text += ChkDesig.Items[iIndex].Text + ",";
                                SubId += subdivision_code.Items[iIndex].Value + ",";

                            }
                        }
                    }
                }

                DropDownList Fieldforce_Type = (DropDownList)gridRow.FindControl("Fieldforce_Type");
                TextBox SF_Mobile = (TextBox)gridRow.FindControl("SF_Mobile");
                TextBox sf_short_name = (TextBox)gridRow.FindControl("sf_short_name");
                TextBox SF_Cat_Code = (TextBox)gridRow.FindControl("SF_Cat_Code");
                TextBox Approved_By = (TextBox)gridRow.FindControl("Approved_By");
                TextBox SF_DOW = (TextBox)gridRow.FindControl("SF_DOW");
                //TextBox Bank_Name = (TextBox)gridRow.FindControl("Bank_Name");
                DropDownList Bank_Name = (DropDownList)gridRow.FindControl("ddlBankName");
                
                TextBox Bank_AcNo = (TextBox)gridRow.FindControl("Bank_AcNo");
                TextBox IFS_Code = (TextBox)gridRow.FindControl("IFS_Code");

             

                DateTime SF_DOW2;
                string Confirmation_date = string.Empty;

                if (SF_DOW.Text != "")
                {

                    SF_DOW2 = Convert.ToDateTime(SF_DOW.Text);

                    Confirmation_date = SF_DOW2.ToString("MM-dd-yyyy");
                }

                DropDownList Category = (DropDownList)gridRow.FindControl("Category");
                CheckBox chkSample1 = (CheckBox)gridRow.FindControl("chkSample");
                if (chkSample1.Checked)
                {
                    DCR_Sample = "1";
                }
                else
                {
                    DCR_Sample = "0";
                }

                CheckBox chkInput1 = (CheckBox)gridRow.FindControl("chkInput");
                if (chkInput1.Checked)
                {
                    DCR_Input = "1";
                }
                else
                {
                    DCR_Input = "0";
                }

                strQry = " UPDATE Mas_Salesforce " +
                     " SET UsrDfd_UserName='" + UsrDfd_UserName.Text + "',Sf_Password='" + Sf_Password.Text + "', Sf_HQ='" + Sf_HQ.Text + "', State_Code='" + State_Code.SelectedValue.ToString() + "', sf_emp_id='" + sf_emp_id.Text + "', Sf_Joining_Date='" + Joining_Date3 + "' , subdivision_code='" + SubId + "' , Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "' , SF_Mobile='" + SF_Mobile.Text + "',sf_short_name='" + sf_short_name.Text + "', LastUpdt_Date=getdate(),SF_Cat_Code='" + SF_Cat_Code.Text + "',Approved_By='" + Approved_By.Text + "' ,SF_DOW='" + Confirmation_date + "',sf_desgn='" + Category.SelectedValue + "',Bank_Name='" + Bank_Name.SelectedValue + "',Bank_AcNo='" + Bank_AcNo.Text + "',IFS_Code='" + IFS_Code.Text + "', Sf_DCRSample_Valid='" + DCR_Sample + "', Sf_DCRInput_Valid='" + DCR_Input + "'  where sf_code='" + SF_Code + "'  ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' UsrDfd_UserName='" + UsrDfd_UserName.Text + "' Sf_Password='" + Sf_Password.Text + "' Sf_HQ='" + Sf_HQ.Text + "' State_Code='" + State_Code.SelectedValue.ToString() + "' sf_emp_id='" + sf_emp_id.Text + "' Joining_Date3='" + Joining_Date3 + "' SubId='" + SubId + "' Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "' SF_Mobile='" + SF_Mobile.Text + "'/>");

            }
            else if (ddlselectfield.SelectedValue == "5")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox UsrDfd_UserName = (TextBox)gridRow.FindControl("UsrDfd_UserName");

                strQry = " UPDATE Mas_Salesforce " +
                " SET UsrDfd_UserName='" + UsrDfd_UserName.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' UsrDfd_UserName='" + UsrDfd_UserName.Text + "' />");
            }

            else if (ddlselectfield.SelectedValue == "6")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox Sf_Password = (TextBox)gridRow.FindControl("Sf_Password");

                strQry = " UPDATE Mas_Salesforce " +
               " SET Sf_Password='" + Sf_Password.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                // Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Sf_Password='" + Sf_Password.Text + "' />");
            }
            else if (ddlselectfield.SelectedValue == "7")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox Sf_HQ = (TextBox)gridRow.FindControl("Sf_HQ");

                strQry = " UPDATE Mas_Salesforce " +
         " SET Sf_HQ='" + Sf_HQ.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Sf_HQ='" + Sf_HQ.Text + "' />");
            }

            else if (ddlselectfield.SelectedValue == "8")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                DropDownList State_Code = (DropDownList)gridRow.FindControl("State_Code");

                strQry = " UPDATE Mas_Salesforce " +
                " SET State_Code='" + State_Code.SelectedValue + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' State_Code='" + State_Code.SelectedValue + "' />");
            }

            else if (ddlselectfield.SelectedValue == "9")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox sf_emp_id = (TextBox)gridRow.FindControl("sf_emp_id");

                strQry = " UPDATE Mas_Salesforce " +
                " SET sf_emp_id='" + sf_emp_id.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' sf_emp_id='" + sf_emp_id.Text + "' />");
            }

            //else if (ddlselectfield.SelectedValue == "Designation")
            //{
            //    Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
            //    SF_Code = lblSF_Code.Text;
            //    DropDownList Designation = (DropDownList)gridRow.FindControl("Designation");

            //    Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Designation='" + Designation.SelectedValue + "' />");
            //}

            else if (ddlselectfield.SelectedValue == "10")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                TextBox Sf_Joining_Date = (TextBox)gridRow.FindControl("Sf_Joining_Date");
                string Joining_Date = Sf_Joining_Date.Text;
                DateTime Joining_Date2 = Convert.ToDateTime(Joining_Date);
                string Joining_Date3 = Joining_Date2.Month.ToString() + '-' + Joining_Date2.Day.ToString() + '-' + Joining_Date2.Year.ToString();


                strQry = " UPDATE Mas_Salesforce " +
                " SET Sf_Joining_Date='" + Joining_Date3 + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Joining_Date3='" + Joining_Date3 + "' />");
            }

            else if (ddlselectfield.SelectedValue == "11")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                string SubId = "";


                TextBox TextBox1 = (TextBox)gridRow.Cells[1].FindControl("TextBox1");
                string sub = TextBox1.Text.ToString();


                string[] strSub;
                iIndex = -1;
                strSub = sub.Split(',');
                Session["Value"] = sub;

                foreach (string dg in strSub)
                {
                    for (iIndex = 0; iIndex < subdivision_code.Items.Count; iIndex++)
                    {
                        if (dg == subdivision_code.Items[iIndex].Text)
                        {

                            subdivision_code.Items[iIndex].Selected = true;

                            if (subdivision_code.Items[iIndex].Selected == true)
                            {
                                // strtxtDes_text += ChkDesig.Items[iIndex].Text + ",";
                                SubId += subdivision_code.Items[iIndex].Value + ",";

                            }
                        }
                    }
                }



                strQry = " UPDATE Mas_Salesforce " +
         " SET subdivision_code='" + SubId + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                // Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SubId='" + SubId + "' />");
            }

            else if (ddlselectfield.SelectedValue == "12")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                DropDownList Fieldforce_Type = (DropDownList)gridRow.FindControl("Fieldforce_Type");

                strQry = " UPDATE Mas_Salesforce " +
                    " SET Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                // Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "' />");
            }

            else if (ddlselectfield.SelectedValue == "13")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;

                TextBox SF_Mobile = (TextBox)gridRow.FindControl("SF_Mobile");

                strQry = " UPDATE Mas_Salesforce " +
             " SET SF_Mobile='" + SF_Mobile.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SF_Mobile='" + SF_Mobile.Text + "' />");
            }
            else if (ddlselectfield.SelectedValue == "14")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;

                TextBox sf_short_name = (TextBox)gridRow.FindControl("sf_short_name");

                strQry = " UPDATE Mas_Salesforce " +
             " SET sf_short_name='" + sf_short_name.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SF_Mobile='" + SF_Mobile.Text + "' />");
            }

            else if (ddlselectfield.SelectedValue == "15")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;

                TextBox SF_Cat_Code = (TextBox)gridRow.FindControl("SF_Cat_Code");
                TextBox Approved_By = (TextBox)gridRow.FindControl("Approved_By");

                strQry = " UPDATE Mas_Salesforce " +
             " SET SF_Cat_Code='" + SF_Cat_Code.Text + "', Approved_By='" + Approved_By.Text + "' , LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SF_Mobile='" + SF_Mobile.Text + "' />");
            }

            //else if (ddlselectfield.SelectedValue == "16")
            //{
            //    Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
            //    SF_Code = lblSF_Code.Text;

            //    TextBox Approved_By = (TextBox)gridRow.FindControl("Approved_By");

            //    strQry = " UPDATE Mas_Salesforce " +
            // " SET Approved_By='" + Approved_By.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

            //    DB_EReporting db = new DB_EReporting();
            //    iReturn = db.ExecQry(strQry);

            //    //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SF_Mobile='" + SF_Mobile.Text + "' />");
            //}

            else if (ddlselectfield.SelectedValue == "17")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;

                TextBox SF_DOW = (TextBox)gridRow.FindControl("SF_DOW");

                DateTime SF_DOW2;
                string Confirmation_date = string.Empty;

                if (SF_DOW.Text != "")
                {

                    SF_DOW2 = Convert.ToDateTime(SF_DOW.Text);

                    Confirmation_date = SF_DOW2.ToString("MM-dd-yyyy");
                }

                strQry = " UPDATE Mas_Salesforce " +
             " SET SF_DOW='" + Confirmation_date + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                //Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' SF_Mobile='" + SF_Mobile.Text + "' />");
            }

            else if (ddlselectfield.SelectedValue == "18")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                DropDownList Category = (DropDownList)gridRow.FindControl("Category");

                strQry = " UPDATE Mas_Salesforce " +
                    " SET sf_desgn='" + Category.SelectedValue + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                // Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "' />");
            }
            else if (ddlselectfield.SelectedValue == "19")
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;
                //TextBox Bank_Name = (TextBox)gridRow.FindControl("Bank_Name");
                DropDownList Bank_Name = (DropDownList)gridRow.FindControl("ddlBankName");
                TextBox Bank_AcNo = (TextBox)gridRow.FindControl("Bank_AcNo");
                TextBox IFS_Code = (TextBox)gridRow.FindControl("IFS_Code");

                strQry = " UPDATE Mas_Salesforce " +
                    " SET Bank_Name='" + Bank_Name.SelectedValue + "',Bank_AcNo='" + Bank_AcNo.Text + "',IFS_Code='" + IFS_Code.Text + "', LastUpdt_Date=getdate() where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

                // Sb_Val_Update.Append("<row sf_code='" + SF_Code + "' Fieldforce_Type='" + Fieldforce_Type.SelectedValue + "' />");
            }
            else if (ddlselectfield.SelectedValue == "20") // added by Mary
            {
                Label lblSF_Code = (Label)gridRow.FindControl("lblSFCode");
                SF_Code = lblSF_Code.Text;

                CheckBox chkSample1 = (CheckBox)gridRow.FindControl("chkSample");
                if (chkSample1.Checked)
                {
                    DCR_Sample = "1";
                }
                else
                {
                    DCR_Sample = "0";
                }

                CheckBox chkInput1 = (CheckBox)gridRow.FindControl("chkInput");
                if (chkInput1.Checked)
                {
                    DCR_Input = "1";
                }
                else
                {
                    DCR_Input = "0";
                }

                strQry = " UPDATE Mas_Salesforce " +
                " SET Sf_DCRSample_Valid='" + DCR_Sample + "', Sf_DCRInput_Valid ='" + DCR_Input + "',LastUpdt_Date=getdate()  where sf_code='" + SF_Code + "'   ";

                DB_EReporting db = new DB_EReporting();
                iReturn = db.ExecQry(strQry);

            }
            //Bank_Name,Bank_AcNo,IFS_Code

        }

        //Sb_Val_Update.Append("</root>");

        //conn.Open();
        //SqlCommand cmd = new SqlCommand("Bulksalesforce_new", conn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //// cmd.Parameters.AddWithValue("@XMLTransUpdate_Val", Sb_Val_Update.ToString());
        //cmd.Parameters.Add("@XMLTransUpdate_Val", SqlDbType.VarChar);
        //cmd.Parameters[0].Value = Sb_Val_Update.ToString();
        //// int iReturn1 = Convert.ToInt32(cmd.ExecuteNonQuery());
        //cmd.Parameters.Add("@retValue", SqlDbType.Int);
        //cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
        //cmd.Parameters.AddWithValue("@type", ddlselectfield.SelectedValue);
        //cmd.ExecuteNonQuery();
        //iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
        //conn.Close();



        if (iReturn > 0)
        {
            //menu1.Status = "Salesforce detail(s) have been updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully'); </script>");
            grdSalesForce.Visible = false;
            btnUpdate.Visible = false;
            lblFilter.Visible = false;
            ddlFilter.Visible = false;
            btnGo.Visible = false;
        }

    }

    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void subdivision_code_SelectedIndexChanged(object sender, EventArgs e)
    {

        string name1 = "";
        string id1 = "";
        GridViewRow gv = (GridViewRow)((Control)sender).NamingContainer;
        CheckBoxList check = (CheckBoxList)gv.FindControl("subdivision_code");
        TextBox txtSubDivision = (TextBox)gv.FindControl("TextBox1");
        HiddenField hdnSubDivisionId = (HiddenField)gv.FindControl("hdnSubDivisionId");
        txtSubDivision.Text = "";
        hdnSubDivisionId.Value = "";
        for (int i = 0; i < check.Items.Count; i++)
        {
            if (check.Items[i].Selected)
            {
                name1 += check.Items[i].Text + ",";
                id1 += check.Items[i].Value + ",";
            }
        }
        //if (name1 == "")
        //{
        // //   name1 = "NIL";
        //} 


        txtSubDivision.Text = name1.TrimEnd(',');
        hdnSubDivisionId.Value = id1.TrimEnd(',');
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    protected DataSet FillCheckBoxList()
    {

        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(div_code);
        return dsSubDivision;
    }


    protected void btngoo_Click(object sender, EventArgs e)
    {
        FillReporting();
        lblFilter.Visible = true;
        ddlFilter.Visible = true;
        btnGo.Visible = true;
        grdSalesForce.Visible = false;
        btnUpdate.Visible = false;
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForceList.aspx");

    }


}